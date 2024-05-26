using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//-------------added by chamak --------------------
public partial class MIS_frmCBS_TRAN_Report : System.Web.UI.Page 
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsDpsHandler objServiceHandler = new clsDpsHandler();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            

            //try
            //{
            //    strUserName = Session["UserLoginName"].ToString();
            //    strPassword = Session["Password"].ToString();
            //    //int addhour = 1;
            //    DateTime dt = DateTime.Now;
            //    //dtpFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-10));
            //    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(10));
            //    //LoadREBAccountsReport(Fromdate, Todate);
            //    lblprocessqty.Text = "";
            //}
            //catch
            //{
            //    Session.Clear();
            //    Response.Redirect("../frmSeesionExpMesage.aspx");
            //}
        }
        // Start - Check active session
        //try
        //{
        //    string sess_id = HttpContext.Current.Session.SessionID;
        //    string strSessID = objSysAdmin.GetActiveSess(sess_id, Session["UserID"].ToString());

        //    if (strSessID == Session["Sess_ID"].ToString())
        //    {
        //        Session.Timeout = Convert.ToInt32(Session["SessionOut"].ToString());
        //    }
        //    else
        //    {
        //        Response.Redirect("../frmSeesionExpMesage.aspx");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ex.Message.ToString();
        //    Response.Redirect("../frmSeesionExpMesage.aspx");
        //}
        //// End - Check active session
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
        gdvDPSReport.PageIndex = e.NewPageIndex;
        gdvDPSReport.DataSource = ViewState["Paging"];
        gdvDPSReport.DataBind();
    }

    private void SortGridView(string sortExpression, string direction)
    {
        //  You can cache the DataTable for improving performance
        dynamic dt = ViewState["Paging"];
        DataTable dtsort = dt;
        DataView dv = new DataView(dtsort);
        dv.Sort = sortExpression + direction;

        gdvDPSReport.DataSource = dv;
        gdvDPSReport.DataBind();
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

    protected void btnAll_Click(object sender, EventArgs e)
    {
        requestIdTxt.Text = String.Empty;
        LoadAffair();
    }
    protected void btnInquiry_Click(object sender, EventArgs e)
    {   
        LoadAffair();
    }

    protected void LoadAffair()
    {

        DataTable myDataTable = new DataTable();
        DataColumn myDataColumn = new DataColumn();
        

        //1/ Sl.

        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "SL.";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        //2/ REQUEST ID.
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "REQUEST ID.";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        //3/ Account No

        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "ACCOUNT NO";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        //4/ CBS ACC

        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "CBS ACC NO./CARD NO. ";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        //5/ Date
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "TRANSACTION DATE";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        //6/ Amount
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "AMOUNT";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);


       // 7/ STATUS
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "STATUS";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        // 7/ STATUS
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "TYPE";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);




        string strReqID = requestIdTxt.Text.Trim();
        DateTime Todate = Convert.ToDateTime(dtpToDate.Date.ToString());
        DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
        string strSubQuery = "";
        string strSubQueryTime = "";
        string strType = ddlSearchType.SelectedItem.Value;



        //if (strSearchText != null && !String.IsNullOrEmpty(strSearchText))
        //{
        //    strSubQuery = "AND CD.REQUEST_ID='" + strSearchText + "' ";

        //}
        //else if (dtpToDate.Date != null && dtpFromDate.Date != null)
        //{
        //    if (strSelectedText == "BDCP")
        //    {
        //        strSubQueryTime = "AND CD.TRANSA_DATE BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";
        //    }
        //    else
        //    {
        //        strSubQueryTime = "AND CD.CAS_TRAN_DATE BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";
        //    }
        //}




        clsDpsHandler obj = new clsDpsHandler();
        DataTable dtsServiceReq = obj.GetDetailsDps(Fromdate, Todate, strType, strReqID);


          try
        {
            int sl = 1;
            
            foreach (DataRow pRow in dtsServiceReq.Rows)
            {
                DataRow dataRow = myDataTable.NewRow();
                dataRow[0] = sl;
                string strRow0 = Convert.ToString(dataRow[0]);
                dataRow[1] = pRow["REQUEST_ID"].ToString();
                string strRow1 = Convert.ToString(dataRow[1]);
                dataRow[2] = pRow["CAS_ACC_NO"].ToString();
                string strRow2 = Convert.ToString(dataRow[2]);
                dataRow[3] = pRow["DPS_REF_CODE"].ToString();
                string strRow3 = Convert.ToString(dataRow[3]);
                dataRow[4] = pRow["CAS_TRAN_DATE"].ToString();
                string strRow4 = Convert.ToString(dataRow[4]);
                dataRow[5] = pRow["CAS_TRAN_AMT"].ToString();
                string strRow5 = Convert.ToString(dataRow[5]);
                dataRow[6] = pRow["RESPONSE_MESSAGE"].ToString();
                string strRow6 = Convert.ToString(dataRow[6]);
                dataRow[7] = pRow["TRAN_TYPE"].ToString();
                string strRow7 = Convert.ToString(dataRow[7]);
                myDataTable.Rows.Add(dataRow);
                sl++;

            }

            


        }
          catch (NullReferenceException ex)
          {
              Response.Write(ex.Message.ToString());
          }
          gdvDPSReport.DataSource = myDataTable;
          gdvDPSReport.DataBind();
          ViewState["Paging"] = myDataTable;

    }


    protected void btnAllExport_Click(object sender, EventArgs e)
    {
        //string strSearchText = requestIdTxt.Text.Trim();
        //DateTime Todate = Convert.ToDateTime(dtpToDate.Date.ToString());
        //DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
        //string strSubQuery = "";
        //string strSubQueryTime = "";
        //string strSelectedText = ddlSearchType.SelectedItem.Value;



        //if (strSearchText != null && !String.IsNullOrEmpty(strSearchText))
        //{
        //    strSubQuery = "AND CD.REQUEST_ID='" + strSearchText + "' ";

        //}
        //else if (dtpToDate.Date != null && dtpFromDate.Date != null)
        //{
        //    strSubQueryTime = "AND CD.CAS_TRAN_DATE BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";
        //}

        ExportReport();
       
    }

   
    private void ExportReport()
    {
        int addhour = 1;
        
        try
        {
            string strReqID = requestIdTxt.Text.Trim();
            DateTime Todate = Convert.ToDateTime(dtpToDate.Date.ToString());
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
            string strSubQuery = "";
            string strSubQueryTime = "";
            string strType = ddlSearchType.SelectedItem.Value;

            string fileName = "", strHTML = "";
            //int totalAmount = 0;
            DataTable dtsAccount = new DataTable();
            fileName = "DPS_TRAN_REPORT";
            //------------------------------------------Report File xl processing   -------------------------------------

            clsDpsHandler obj = new clsDpsHandler();
            dtsAccount = obj.GetDetailsDps(Fromdate, Todate, strType, strReqID);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MyCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>DPS Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date: '" + Todate + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' > SL</td>";
            strHTML = strHTML + "<td valign='middle' > REQUEST ID.</td>";
            strHTML = strHTML + "<td valign='middle' > ACCOUNT NO</td>";
            strHTML = strHTML + "<td valign='middle' > CBS ACC NO./CARD NO. </td>";
            strHTML = strHTML + "<td valign='middle' > TRANSACTION DATE</td>";
            strHTML = strHTML + "<td valign='middle' > AMOUNT</td>";
            strHTML = strHTML + "<td valign='middle' > STATUS</td>";
            strHTML = strHTML + "<td valign='middle' > TYPE</td>";

           
            
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Rows.Count > 0)
            {
                int SerialNo = 1;
               
                foreach (DataRow prow in dtsAccount.Rows)
                    {
                            strHTML = strHTML + "<tr>";
                            strHTML = strHTML + " <td > " + SerialNo.ToString() + "</td>";
                            strHTML = strHTML + " <td > " + prow["REQUEST_ID"].ToString() + "</td>";
                            strHTML = strHTML + " <td > " + prow["CAS_ACC_NO"].ToString() + "</td>";
                            strHTML = strHTML + " <td > " + prow["DPS_REF_CODE"].ToString() + "</td>";
                            strHTML = strHTML + " <td > " + prow["CAS_TRAN_DATE"].ToString() + "</td>";
                            
                            strHTML = strHTML + " <td > " + prow["CAS_TRAN_AMT"].ToString() + "</td>";
                            strHTML = strHTML + " <td > " + prow["RESPONSE_MESSAGE"].ToString() + "</td>";
                            strHTML = strHTML + " <td > " + prow["TRAN_TYPE"].ToString() + "</td>";
                            
                            strHTML = strHTML + "</tr>";
                            SerialNo++;

                     }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
           
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                }
            }

        
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }




}
