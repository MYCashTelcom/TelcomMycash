using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UBP_frmKgdclReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
          
        //    try
        //    {
        //        strUserName = Session["UserLoginName"].ToString();
        //        strPassword = Session["Password"].ToString();
        //        //int addhour = 1;
        //        DateTime dt = DateTime.Now;
        //        //dtpFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(-24));
        //        dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);
        //        //LoadREBAccountsReport(Fromdate, Todate);
        //       // lblprocessqty.Text = "";
        //    }
        //    catch
        //    {
        //        Session.Clear();
        //        Response.Redirect("../frmSeesionExpMesage.aspx");
        //    }
        //}
        //// Start - Check active session
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
        // End - Check active session
    }

    /********ProcessREBBill By Mithu * ********/
   //private void ProcessREBBill()
   // {
   //    string WaletNo = txtWaletNumber.Text.Trim();
   //    if(WaletNo=="")
   //    {
   //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please, enter the Walet ID.')", true);
   //        return;
   //    }

   //    else
   //    {
   //        clsServiceHandler obj = new clsServiceHandler();
   //        DataTable dt = obj.REBBillProcess(WaletNo);
   //        string ProcessREBBill = dt.Rows[0]["ProcessREBBill"].ToString();
   //        lblprocessqty.Text ="Process " + ProcessREBBill;
   //    }
        
   // }

   /********UtilityTransctionBillProcess By Mithu * ********/
   private void ProcessTransactionUtilityBill()
   {
       clsServiceHandler obj = new clsServiceHandler();
       bool Process = obj.UtilityTransctionBillProcess();
   }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //SaveSecurityInfo("Select", "Reb_BILL_PAY_Report");
        
        //DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
        DateTime Todate = Convert.ToDateTime(dtpTodate.Date.ToString());
        DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
        //if (dtpFromDate.Date != null)
        //    Fromdate = Convert.ToDateTime(dtpFromDate.Date, CultureInfo.InvariantCulture);

        //if (dtpToDate.Date != null)
        //    Todate = Convert.ToDateTime(dtpToDate.Date, CultureInfo.InvariantCulture);

        //LoadREBAccountsReport(Fromdate, Todate);
        LoadAffair(Todate, Fromdate);
        
    }

    protected void SaveSecurityInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }  

    /********LoadREBAccountsReport By Mithu * ********/
    private void LoadREBAccountsReport(DateTime Fromdate, DateTime Todate)
    {
        clsServiceHandler obj = new clsServiceHandler();
        DataTable dt = obj.GetREBAccountsRepor(Todate);        
        if (dt.Rows.Count > 0)
        {
            gdvREBAccountsReport.DataSourceID = string.Empty;
            ViewState["Paging"] = dt;
            gdvREBAccountsReport.DataSource = dt;
            gdvREBAccountsReport.DataBind();
        }

        else
        {
            gdvREBAccountsReport.DataSource = null;
            gdvREBAccountsReport.DataBind();
        }
       
    }

 
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }


    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        DateTime Todate = Convert.ToDateTime(dtpTodate.Date.ToString());
        DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
        ExportReport(Todate,Fromdate);      
    }

    /********Export Report By Mithu * ********/
    private void ExportReport(DateTime Todate,DateTime Fromdate)
    {
        int addhour = 1;
        //DateTime Todate = Convert.ToDateTime(dtpToDate.Date.ToString()); ;
        //if (dtpToDate.Date != null)
        //    Todate = Convert.ToDateTime(dtpToDate.Date.AddDays(addhour).AddMilliseconds(-1));

        try
        {

            string fileName = "", strHTML = "";
            //int totalAmount = 0;
            DataTable dtsAccount = new DataTable();
            fileName = "KGDCL_Bill_Inquiry_Report";
            //------------------------------------------Report File xl processing   -------------------------------------

            clsServiceHandler obj = new clsServiceHandler();
            dtsAccount = obj.GetKGDCLAccountsRepor(Todate, Fromdate);  //.GetPostingDetail(Fromdate, Todate);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>KGDCL Bill Inquiry Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date: " + dtpFromDate.DateString+ " To " +dtpTodate.DateString + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >UTILITY_TRAN_ID</td>";
            strHTML = strHTML + "<td valign='middle' >REQUEST_ID</td>";
            strHTML = strHTML + "<td valign='middle' >TRANSA_DATE</td>";
            strHTML = strHTML + "<td valign='middle' >SOURCE_ACC_NO</td>";
            strHTML = strHTML + "<td valign='middle' >NAME</td>";
            strHTML = strHTML + "<td valign='middle' >BILL_MONTH</td>";
            strHTML = strHTML + "<td valign='middle' >BILL_YEAR</td>";
            strHTML = strHTML + "<td valign='middle' >TOTAL_BILL_AMOUNT</td>";
            strHTML = strHTML + "<td valign='middle' >TRANSACTION_STATUS</td>";
            strHTML = strHTML + "</tr>";
            

            if (dtsAccount.Rows.Count > 0)
            {
                int SerialNo = 1;
                string processing = "Request Processing";
                string noDataFound = "No Data Found";
                string paid = "...";
                foreach (DataRow prow in dtsAccount.Rows)
                {
                    
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UTILITY_TRAN_ID"] + "</td>";
                        strHTML = strHTML + " <td >' " + prow["REQUEST_ID"].ToString() + "</td>";
                        strHTML = strHTML + " <td >' " + prow["TRANSA_DATE"].ToString() + "</td>";
                        strHTML = strHTML + " <td >' " + prow["SOURCE_ACC_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td >' " + prow["NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td >'" + prow["BILL_MONTH"].ToString() + "</td>";
                        strHTML = strHTML + " <td >' " + prow["BILL_YEAR"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_BILL_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_STATUS"].ToString() + "</td>";
                        strHTML = strHTML + "</tr>";

                        SerialNo = SerialNo + 1;
                    }


                    
        
                
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "Total Amount" + " </td>";
            //strHTML = strHTML + " <td > " + totalAmount.ToString() + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

           // SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");



            //lblMsg.ForeColor = Color.White;
            //lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    //private void ExportReport()
    //{
    //    int addhour = 1;
    //    DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
    //    DateTime Todate = Fromdate.AddDays(addhour).AddMilliseconds(-1);
    //    if (dtpFromDate.Date != null)
    //        Fromdate = Convert.ToDateTime(dtpFromDate.Date, CultureInfo.InvariantCulture);

    //    if (dtpToDate.Date != null)
    //        Todate = Convert.ToDateTime(dtpToDate.Date.AddDays(addhour).AddMilliseconds(-1));

    //    try
    //    {
    //        string fileName = "", strHTML = "";
    //        //int totalAmount = 0;
    //        DataTable dtsAccount = new DataTable();
    //        fileName = "REB_Bill_Inquiry_Report";
    //        //------------------------------------------Report File xl processing   -------------------------------------



    //        clsServiceHandler obj = new clsServiceHandler();
    //        dtsAccount = obj.GetREBAccountsRepor(Fromdate, Todate);

    //        strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
    //        strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
    //        strHTML = strHTML + "</table>";
    //        strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
    //        strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
    //        strHTML = strHTML + "</table>";
    //        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
    //        strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>REB Bill Inquiry Report</h2></td></tr>";
    //        strHTML = strHTML + "</table>";
    //        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
    //        strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpFromDate.DateString + "' To '" + dtpToDate.DateString + "'</h2></td></tr>";
    //        strHTML = strHTML + "</table>";
    //        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
    //        strHTML = strHTML + "<tr>";

    //        strHTML = strHTML + "<td valign='middle' >Sl</td>";
    //        strHTML = strHTML + "<td valign='middle' >REB Account ID</td>";
    //        strHTML = strHTML + "<td valign='middle' >Bill Pay Month</td>";
    //        strHTML = strHTML + "<td valign='middle' >Bill Status</td>";
    //        strHTML = strHTML + "<td valign='middle' >Reference Number</td>";
    //        strHTML = strHTML + "<td valign='middle' >Bill Purpose</td>";
    //        strHTML = strHTML + "<td valign='middle' >Date</td>";
    //        strHTML = strHTML + "<td valign='middle' >Bill Amount</td>";
    //        strHTML = strHTML + "<td valign='middle' >Remarks Old</td>";
    //        strHTML = strHTML + "<td valign='middle' >Remarks</td>";

    //        //strHTML = strHTML + "</tr>";


    //        if (dtsAccount.Rows.Count > 0)
    //        {
    //            int SerialNo = 1;
    //            foreach (DataRow prow in dtsAccount.Rows)
    //            {
    //                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["UBP_REB_ACCOUNT_ID"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_MONTH"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_STATUS"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_REF"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_PURPOSE"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_TIME"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["TOTAL_BILL_AMOUNT"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["CHEQUE_REMARKS_OLD"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["CHEQUE_REMARKS"].ToString() + "</td>";

    //                //strHTML = strHTML + " </tr> ";
    //                SerialNo = SerialNo + 1;
    //                //totalAmount = totalAmount + Convert.ToInt32(prow["AMOUNT"]);
    //            }
    //        }

    //        strHTML = strHTML + "<tr>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "Total Amount" + " </td>";
    //        //strHTML = strHTML + " <td > " + totalAmount.ToString() + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";

    //        strHTML = strHTML + " </tr>";
    //        strHTML = strHTML + " </table>";

    //        SaveAuditInfo("Preview", fileName);
    //        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

    //        //lblMsg.ForeColor = Color.White;
    //        //lblMsg.Text = "Report Generated Successfully...";
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //}


    /********REB Bill Inquiry By Mithu * ********/
    //protected void btnInquiry_Click(object sender, EventArgs e)
    //{
    //    lblprocessqty.Text = "";
    //    ProcessREBBill();
    //    ProcessTransactionUtilityBill();
    //    SaveSecurityInfo("Process", "Reb_BILL_PAY_Report");
    //    btnInquiry.Enabled = true;

    //}

    /********Added By Mithu * ********/
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

    /********Added By Mithu * ********/
    protected void Gridpaging(object sender, GridViewPageEventArgs e)
    {
        gdvREBAccountsReport.PageIndex = e.NewPageIndex;
        gdvREBAccountsReport.DataSource = ViewState["Paging"];
        gdvREBAccountsReport.DataBind();
    }

    /********Added By Mithu * ********/
    private void SortGridView(string sortExpression, string direction)
    {
        //  You can cache the DataTable for improving performance
        dynamic dt = ViewState["Paging"];
        DataTable dtsort = dt;
        DataView dv = new DataView(dtsort);
        dv.Sort = sortExpression + direction;

        gdvREBAccountsReport.DataSource = dv;
        gdvREBAccountsReport.DataBind();
    }

    /********Added By Mithu * ********/
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
    protected void LoadAffair(DateTime Todate,DateTime Fromdate)
    {
        //clsGeneralLedger objGeneralLedger = new clsGeneralLedger();
        //Step 1: C# Code to Create ASP.Net DataTable
        // Initialize a DataTable
        DataTable myDataTable = new DataTable();
        // Initialize DataColumn
        DataColumn myDataColumn = new DataColumn();

        //// 1| Add and Create a first DataColumn
        //myDataTable.Columns.Add(myDataColumn);
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Sl.";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "UTILITY_TRAN_ID";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        // 2| initialize a new instance of DataColumn to add another column with different properties.


        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "REQUEST_ID";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        // 2| initialize a new instance of DataColumn to add another column with different properties.
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "TRANSA_DATE";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //3| --------------------------------------------------------------
        // 2| initialize a new instance of DataColumn to add another column with different properties.
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "SOURCE_ACC_NO";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //3| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "NAME";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //4| -------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "BILL_MONTH";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //5| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "BILL_YEAR";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //6| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "TOTAL_BILL_AMOUNT";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //7| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "TRANSACTION_STATUS";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        



        //Step 2: C# Code to Add New Rows to ASP.Net DataTable
        // create a new row using NewRow() function of DataTable.
        // dataRow object will inherit the schema of myDataTable to create a new rowte
        double dblOpenBal = 0;
        double dblCurBal = 0;
        double dblDebit = 0;
        double dblCredit = 0;
        double dblEndBalance = 0;
        string strBranchType = "All";

        //DataSet dtsServiceReq = new DataSet();
        int addhour = 1;
        //DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
        //DateTime Todate = Fromdate.AddDays(addhour).AddMilliseconds(-1);
        //if (dtpFromDate.Date != null)
        //    Fromdate = Convert.ToDateTime(dtpFromDate.Date, CultureInfo.InvariantCulture);

        //if (dtpToDate.Date != null)
        //    Todate = Convert.ToDateTime(dtpToDate.Date.AddDays(addhour).AddMilliseconds(-1));
        clsServiceHandler obj = new clsServiceHandler();
        DataTable dtsServiceReq = obj.GetKGDCLAccountsRepor(Todate, Fromdate);
        ///##################

        ///##################
        try
        {
            int sl = 1;
            string processing = "Request Processing";
            string noDataFound = "No Data Found.....";
            string paid = "...";
            if (dtsServiceReq.Rows.Count > 0)
            {
                foreach (DataRow pRow in dtsServiceReq.Rows)
                {

                    DataRow dataRow = myDataTable.NewRow();
                    dataRow[0] = sl;
                    string strRow0 = Convert.ToString(dataRow[0]);
                    dataRow[1] = pRow["UTILITY_TRAN_ID"];
                    string strRow1 = Convert.ToString(dataRow[1]);

                    dataRow[2] = pRow["REQUEST_ID"];
                    string strRow2 = Convert.ToString(dataRow[2]);

                    dataRow[3] = pRow["TRANSA_DATE"];
                    string strRow3 = Convert.ToString(dataRow[3]);

                    dataRow[4] = pRow["SOURCE_ACC_NO"];
                    string strRow4 = Convert.ToString(dataRow[4]);

                    dataRow[5] = pRow["NAME"];
                    string strRow5 = Convert.ToString(dataRow[5]);

                    dataRow[6] = pRow["BILL_MONTH"];
                    string strRow6 = Convert.ToString(dataRow[6]);

                    dataRow[7] = pRow["BILL_YEAR"];
                    string strRow7 = Convert.ToString(dataRow[7]);

                    dataRow[8] = pRow["TOTAL_BILL_AMOUNT"];
                    string strRow8 = Convert.ToString(dataRow[8]);

                    dataRow[9] = pRow["TRANSACTION_STATUS"];
                    string strRow9 = Convert.ToString(dataRow[9]);


                    //------------------------------------------------------
                    myDataTable.Rows.Add(dataRow);



                    sl++;

                }
            }
            else {

                DataRow dataRow = myDataTable.NewRow();
                dataRow[0] = noDataFound;
                string strRow0 = Convert.ToString(dataRow[0]);
                myDataTable.Rows.Add(dataRow);
            
            }
        }
        catch (NullReferenceException ex)
        {
            Response.Write(ex.Message.ToString());
        }
        gdvREBAccountsReport.DataSource = myDataTable;
        gdvREBAccountsReport.DataBind();
        ViewState["Paging"] = myDataTable;
    }
}