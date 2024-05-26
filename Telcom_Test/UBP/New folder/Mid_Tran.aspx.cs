﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;


public partial class UBP_Mid_Tran : System.Web.UI.Page 
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsDpsHandler objDpsHandeler = new clsDpsHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    DataTable oDataTable = new DataTable();
    DataSet ds = new DataSet();
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        //    if (!IsPostBack)
        //    {

        //        try
        //        {
        //            strUserName = Session["UserLoginName"].ToString();
        //            strPassword = Session["Password"].ToString();
        //            //int addhour = 1;
        //            DateTime dt = DateTime.Now;
        //            //dtpFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-10));
        //            dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(10));
        //            //LoadREBAccountsReport(Fromdate, Todate);
        //            lblprocessqty.Text = "";
        //        }
        //        catch
        //        {
        //            Session.Clear();
        //            Response.Redirect("../frmSeesionExpMesage.aspx");
        //        }
        //    }
        //    // Start - Check active session
        //    try
        //    {
        //        string sess_id = HttpContext.Current.Session.SessionID;
        //        string strSessID = objSysAdmin.GetActiveSess(sess_id, Session["UserID"].ToString());

        //        if (strSessID == Session["Sess_ID"].ToString())
        //        {
        //            Session.Timeout = Convert.ToInt32(Session["SessionOut"].ToString());
        //        }
        //        else
        //        {
        //            Response.Redirect("../frmSeesionExpMesage.aspx");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        Response.Redirect("../frmSeesionExpMesage.aspx");
        //    }
        //    // End - Check active session
    }



    


    private void LoadGrid()
    {
        string strSearchText = requestIdTxt.Text.Trim();
        DateTime Todate = Convert.ToDateTime(dtpToDate.Date.ToString());
        DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
        string strSubQuery = "";
        string strSubQueryTime= "";
        //string strSubQuery2 = accNoTxt.Text.Trim();

        grvRequestList.DataSource = null;
        grvRequestList.DataBind();

        try
        {
            oDataTable = objServiceHandler.GetDetails_Middleware(Todate, Fromdate, strSearchText);
            ds.Tables.Add(oDataTable);
            ViewState["Paging"] = oDataTable;
            grvRequestList.DataSource = oDataTable;
            grvRequestList.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }

    }

    protected void Gridsorting(object sender, GridViewSortEventArgs e)
    {
        string ColumnTosort = e.SortExpression;

        if (CurrentSortDirection == SortDirection.Ascending)
        {
            CurrentSortDirection = SortDirection.Descending;
            SortGridView(ColumnTosort, DESCENDING);
        }
        else
        {
            CurrentSortDirection = SortDirection.Ascending;
            SortGridView(ColumnTosort, ASCENDING);
        }

    }

    protected void Gridpaging(object sender, GridViewPageEventArgs e)
    {
        grvRequestList.PageIndex = e.NewPageIndex;
        grvRequestList.DataSource = ViewState["Paging"];
        grvRequestList.DataBind();
    }

    private void SortGridView(string sortExpression, string direction)
    {
        //  You can cache the DataTable for improving performance
        dynamic dt = ViewState["Paging"];
        DataTable dtsort = dt;
        DataView dv = new DataView(dtsort);
        dv.Sort = sortExpression + direction;

        grvRequestList.DataSource = dv;
        grvRequestList.DataBind();
    }


    public SortDirection CurrentSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
            {
                ViewState["sortDirection"] = SortDirection.Ascending;
            }

            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;

        }
    }


    protected void grvRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label ResponseMessage = (Label)e.Row.FindControl("lblRESPONSE_MSG");
            string lblResponseMessage = ResponseMessage.Text;
            //Label SuccessStatus = (Label)e.Row.FindControl("lblRESPONSE_MSG");
            //string lblSuccessStatus = SuccessStatus.Text;
            Label strIsReversed = (Label)e.Row.FindControl("lblIS_REVERSED");
            string lblIsReversed = strIsReversed.Text;


            if (lblResponseMessage.Equals("Success") || lblIsReversed.Equals("Y"))
            {
                Button btnButton = (Button)e.Row.FindControl("btnUpdate");
                btnButton.Enabled = false;
            }
        }
       
    }
    protected void grvRequestList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label RequestId = grvRequestList.Rows[e.RowIndex].FindControl("lblREQUEST_ID") as Label;
            Button btn = grvRequestList.Rows[e.RowIndex].FindControl("btnUpdate") as Button;

            string strRequestId = RequestId.Text;

            string strReverse = objServiceHandler.Middleware_Reverse(strRequestId);
            //string strReverse = "";

            if (strReverse.Equals("00"))
            {
               
               LoadGrid();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnInquiry_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void btnAll_Click(object sender, EventArgs e)
    {
        requestIdTxt.Text = String.Empty;
        LoadGrid();
    }
    private void ExportReport(DateTime Todate, DateTime Fromdate)
    {
        

        //try
        //{

        //    string fileName = "", strHTML = "";
        //    //int totalAmount = 0;
        //    DataTable dtsAccount = new DataTable();
        //    fileName = "MIDDLEWARE TRANSACTION REPORT";
        //    string strSearchText = requestIdTxt.Text.Trim();
        //    string strSubQuery = "";
        //   // string strSubQuery2 = accNoTxt.Text.Trim() ;

        //    string strSubQueryTime = "";


           

        //    //------------------------------------------Report File xl processing   -------------------------------------

        //    //clsDpsHandler obj = new clsDpsHandler();
        //    dtsAccount = objServiceHandler.GetDetails_Middleware(Todate, Fromdate);

        //    strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
        //    strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
        //    strHTML = strHTML + "</table>";
        //    strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
        //    strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Agent Banking</h3></td></tr>";
        //    strHTML = strHTML + "</table>";
        //    strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        //    strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>CBS Report</h2></td></tr>";
        //    strHTML = strHTML + "</table>";
        //    strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        //    strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date: '" + Todate + "'</h2></td></tr>";
        //    strHTML = strHTML + "</table>";
        //    strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        //    strHTML = strHTML + "<tr>";

        //    strHTML = strHTML + "<td valign='middle' > SL</td>";
        //    strHTML = strHTML + "<td valign='middle' > REQUEST ID.</td>";
        //    strHTML = strHTML + "<td valign='middle' > CBS ID.</td>";
        //    strHTML = strHTML + "<td valign='middle' > ACCOUNT No.</td>";
        //    strHTML = strHTML + "<td valign='middle' > CBS ACC No. </td>";
        //    strHTML = strHTML + "<td valign='middle' > TRANSACTION DATE</td>";
        //    strHTML = strHTML + "<td valign='middle' > BRANCH NAME</td>";
        //    strHTML = strHTML + "<td valign='middle' > AMOUNT</td>";
        //    strHTML = strHTML + "<td valign='middle' > FEE</td>";
        //    strHTML = strHTML + "<td valign='middle' > STATUS</td>";

        //    strHTML = strHTML + "</tr>";


        //    if (dtsAccount.Rows.Count > 0)
        //    {
        //        int SerialNo = 1;

        //        foreach (DataRow prow in dtsAccount.Rows)
        //        {
        //            strHTML = strHTML + "<tr>";
        //            strHTML = strHTML + " <td > " + SerialNo.ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["REQUEST_ID"].ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["RESPONSE_1"].ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["REQUEST_PARTY"].ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["CBS_ACC_NO"].ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["CAS_TRAN_DATE"].ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["BRANCH_NAME"].ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["TRAN_AMOUNT"].ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["SRV_FEE"].ToString() + "</td>";
        //            strHTML = strHTML + " <td > " + prow["STATUS"].ToString() + "</td>";
        //            strHTML = strHTML + "</tr>";
        //            SerialNo++;

        //        }

        //        strHTML = strHTML + "<tr>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";
        //        strHTML = strHTML + " <td > " + "" + " </td>";

        //        strHTML = strHTML + " </tr>";
        //        strHTML = strHTML + " </table>";

        //        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        //    }
        //}


        //catch (Exception ex)
        //{
        //    ex.Message.ToString();
        //}
    }

    protected void btnAllExport_Click(object sender, EventArgs e)
    {
        DateTime Todate = Convert.ToDateTime(dtpToDate.Date.ToString());
        DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());

        ExportReport(Todate, Fromdate);

    }
}
