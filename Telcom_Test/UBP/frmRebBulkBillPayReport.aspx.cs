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

public partial class UBP_frmRebBulkBillPayReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
                //int addhour = 1;
                DateTime dt = DateTime.Now;
                //dtpFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(-24));
                dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);
                //LoadREBAccountsReport(Fromdate, Todate);
               // lblprocessqty.Text = "";
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        // Start - Check active session
        try
        {
            string sess_id = HttpContext.Current.Session.SessionID;
            string strSessID = objSysAdmin.GetActiveSess(sess_id, Session["UserID"].ToString());

            if (strSessID == Session["Sess_ID"].ToString())
            {
                Session.Timeout = Convert.ToInt32(Session["SessionOut"].ToString());
            }
            else
            {
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            Response.Redirect("../frmSeesionExpMesage.aspx");
        }
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
        SaveSecurityInfo("Select", "Reb_BILL_PAY_Report");
        
        //DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Date.ToString());
        DateTime Todate = Convert.ToDateTime(dtpToDate.Date.ToString());
        //if (dtpFromDate.Date != null)
        //    Fromdate = Convert.ToDateTime(dtpFromDate.Date, CultureInfo.InvariantCulture);

        //if (dtpToDate.Date != null)
        //    Todate = Convert.ToDateTime(dtpToDate.Date, CultureInfo.InvariantCulture);

        //LoadREBAccountsReport(Fromdate, Todate);
        LoadAffair(Todate);
        
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
        ExportReport();      
    }

    /********Export Report By Mithu * ********/
    private void ExportReport()
    {
        int addhour = 1;
        DateTime Todate = Convert.ToDateTime(dtpToDate.Date.ToString()); ;
        if (dtpToDate.Date != null)
            Todate = Convert.ToDateTime(dtpToDate.Date.AddDays(addhour).AddMilliseconds(-1));

        try
        {

            string fileName = "", strHTML = "";
            //int totalAmount = 0;
            DataTable dtsAccount = new DataTable();
            fileName = "REB_Bill_Inquiry_Report";
            //------------------------------------------Report File xl processing   -------------------------------------

            clsServiceHandler obj = new clsServiceHandler();
            dtsAccount = obj.GetREBAccountsRepor(Todate);  //.GetPostingDetail(Fromdate, Todate);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>REB Bill Inquiry Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date: '" + dtpToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >SMS AC No</td>";
            strHTML = strHTML + "<td valign='middle' >REB bill Month</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Year</td>";
            strHTML = strHTML + "<td valign='middle' >Due Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Issue Date</td>";
            strHTML = strHTML + "<td valign='middle' >Due Date</td>";
            strHTML = strHTML + "<td valign='middle' >LPC Date</td>";
            strHTML = strHTML + "<td valign='middle' >Due LPC Amount</td>";
            strHTML = strHTML + "<td valign='middle' >PBS Name E</td>";
            strHTML = strHTML + "<td valign='middle' >PBS Code</td>";
            strHTML = strHTML + "<td valign='middle' >Paid Status</td>";
            strHTML = strHTML + "<td valign='middle' >Ref Number</td>";
            strHTML = strHTML + "<td valign='middle' >Enquiry Month</td>";
            strHTML = strHTML + "<td valign='middle' >Enquiry Time</td>";


            if (dtsAccount.Rows.Count > 0)
            {
                int SerialNo = 1;
                string processing = "Request Processing";
                string noDataFound = "No Data Found";
                string paid = "...";
                foreach (DataRow prow in dtsAccount.Rows)
                {
                    if (prow["CHEQUE_REMARKS"].ToString().Contains("{RESPONSE_MSG:BILL ALREADY HAS BEEN PAID,RESPONSE_CODE:1278}"))
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UBP_REB_ACCOUNT_ID"] + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + paid + "</td>";
                        strHTML = strHTML + " <td > '" + "BILL ALREADY HAS BEEN PAID" + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_REF"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_MONTH"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_TIME"].ToString() + "</td>";
                    }

                    else if (prow["CHEQUE_REMARKS"].ToString().Contains("{RESPONSE_MSG:NO DATA FOUND,RESPONSE_CODE:1290}"))
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UBP_REB_ACCOUNT_ID"] + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + noDataFound + "</td>";
                        strHTML = strHTML + " <td > '" + "BILL ALREADY HAS BEEN PAID" + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_REF"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_MONTH"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_TIME"].ToString() + "</td>";
                    }
                    else
                    {
                        if (prow["CHEQUE_REMARKS"].ToString() != "")
                        {
                            string s = prow["CHEQUE_REMARKS"].ToString();
                            string[] sList = s.Split(',');
                            string billno = sList[0].Trim().Replace("BILL_NO:", "");
                            string bookno = sList[1].Trim().Replace("BOOK_NO:", "");
                            string smsAcnt = prow["UBP_REB_ACCOUNT_ID"].ToString(); //sList[2].Trim().Replace("SMS_AC_NO:", "");
                            string rebBillMonth = sList[3].Trim().Replace("BILL_MONTH:", "");
                            string billYear = sList[4].Trim().Replace("BILL_YEAR:", "");
                            string isuDate = sList[5].Trim().Replace("ISSUE_DATE:", "");
                            string dueDate = sList[6].Trim().Replace("DUE_DATE:", "");
                            string dueAmt = sList[7].Trim().Replace("DUE_AMOUNT:", "");
                            string lpcDate = sList[9].Trim().Replace("LPC_DATE:", "");
                            //string dueLpcAmt = sList[9].Trim().Replace("DUE_LPC_AMOUNT:", "");
                            string paidStatus = sList[10].Trim().Replace("PAID_STATUS:", "");
                            string pbsCode = sList[11].Trim().Replace("PBS_CODE:", "");
                            string pbsAmtB = sList[12].Trim().Replace("PBS_NAME_B:", "");
                            string pbsNameE = sList[13].Trim().Replace("PBS_NAME_E:", "");
                            pbsNameE = pbsNameE.Remove(pbsNameE.Length - 1, 1);
                            string responseCode = sList[14].Trim().Replace("RESPONSE_CODE:", "");
                            string billInqMonth = prow["UBP_REB_BBPS_MONTH"].ToString();
                            strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + (smsAcnt == "null" ? noDataFound : smsAcnt) + "</td>";
                            strHTML = strHTML + " <td > '" + (rebBillMonth == "null" ? noDataFound : rebBillMonth) + "</td>";
                            strHTML = strHTML + " <td > '" + (billYear == "null" ? noDataFound : billYear) + "</td>";
                            strHTML = strHTML + " <td > '" + (dueAmt == "null" ? noDataFound : dueAmt) + "</td>";
                            strHTML = strHTML + " <td > '" + (isuDate == "null" ? noDataFound : isuDate) + "</td>";
                            strHTML = strHTML + " <td > '" + (dueDate == "null" ? noDataFound : dueDate) + "</td>";
                            strHTML = strHTML + " <td > '" + (lpcDate == "null" ? noDataFound : lpcDate) + "</td>";
                            strHTML = strHTML + " <td > '" + "" + "</td>";
                            strHTML = strHTML + " <td > '" + (lpcDate == "null" ? noDataFound : pbsNameE) + "</td>";
                            strHTML = strHTML + " <td > '" + (lpcDate == "null" ? noDataFound : pbsCode) + "</td>";
                            strHTML = strHTML + " <td > '" + (lpcDate == "null" ? noDataFound : paidStatus) + "</td>";
                            //strHTML = strHTML + " <td > '" + pbsAmtB.ToString() + "</td>";
                            //strHTML = strHTML + " <td > '" + responseCode.ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_REF"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + billInqMonth.ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_TIME"].ToString() + "</td>";
                        }
                        else
                        {
                            strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["UBP_REB_ACCOUNT_ID"] + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            strHTML = strHTML + " <td > '" + processing + "</td>";
                            //strHTML = strHTML + " <td > '" + pbsAmtB.ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_REF"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_MONTH"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["UBP_REB_BBPS_TIME"].ToString() + "</td>";
                        }


                    }





                    //strHTML = strHTML + " <td > '" + prow["CHEQUE_REMARKS"].ToString() + "</td>";



                    //BILL_MONTH:
                    //PAID_STATUS:
                    //PBS_CODE:
                    //PBS_NAME_B:
                    //PBS_NAME_E:
                    //RESPONSE_CODE:
                    //strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                    //totalAmount = totalAmount + Convert.ToInt32(prow["AMOUNT"]);
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

            SaveAuditInfo("Preview", fileName);
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
    protected void LoadAffair(DateTime Todate)
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
        myDataColumn.ColumnName = "SMS AC No";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        // 2| initialize a new instance of DataColumn to add another column with different properties.


        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "REB Bill Month";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        // 2| initialize a new instance of DataColumn to add another column with different properties.
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Bill Year";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //3| --------------------------------------------------------------
        // 2| initialize a new instance of DataColumn to add another column with different properties.
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Due Amount";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //3| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Issue Date";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //4| -------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Due Date";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //5| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "LPC Date";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //6| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Due LPC Amount";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //7| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "PBS Name E";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //8| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "PBS Code";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //9| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Paid Status";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //10| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Ref Num";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Bill Inquiry Month";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        //11| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Enquiry Time";
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
        DataTable dtsServiceReq = obj.GetREBAccountsRepor(Todate);
        ///##################

        ///##################
        try
        {
            int sl = 1;
            string processing = "Request Processing";
            string noDataFound = "No Data Found";
            string paid = "...";
            foreach (DataRow pRow in dtsServiceReq.Rows)
            {
                if (pRow["CHEQUE_REMARKS"].ToString().Contains("{RESPONSE_MSG:BILL ALREADY HAS BEEN PAID,RESPONSE_CODE:1278}"))
                {
                    DataRow dataRow = myDataTable.NewRow();
                    dataRow[0] = sl;
                    string strRow0 = Convert.ToString(dataRow[0]);
                    dataRow[1] = pRow["UBP_REB_ACCOUNT_ID"];
                    string strRow1 = Convert.ToString(dataRow[1]);
                    dataRow[2] = paid;
                    string strRow2 = Convert.ToString(dataRow[2]);
                    dataRow[3] = paid;
                    string strRow3 = Convert.ToString(dataRow[3]);
                    dataRow[4] = paid;
                    string strRow4 = Convert.ToString(dataRow[4]);
                    dataRow[5] = paid;
                    string strRow5 = Convert.ToString(dataRow[5]);
                    dataRow[6] = paid;
                    string strRow6 = Convert.ToString(dataRow[6]);
                    dataRow[7] = paid;
                    string strRow7 = Convert.ToString(dataRow[7]);
                    dataRow[8] = paid;
                    string strRow8 = Convert.ToString(dataRow[8]);
                    dataRow[9] = paid;
                    string strRow9 = Convert.ToString(dataRow[9]);
                    dataRow[10] = paid;
                    string strRow10 = Convert.ToString(dataRow[10]);
                    dataRow[11] = "BILL ALREADY HAS BEEN PAID";
                    string strRow11 = Convert.ToString(dataRow[11]);
                    dataRow[12] = pRow["UBP_REB_BBPS_REF"].ToString();
                    string strRow12 = Convert.ToString(dataRow[12]);
                    dataRow[13] = pRow["UBP_REB_BBPS_MONTH"].ToString();
                    string strRow13 = Convert.ToString(dataRow[13]);
                    dataRow[14] = pRow["UBP_REB_BBPS_TIME"].ToString();
                    string strRow14 = Convert.ToString(dataRow[14]);
                    //------------------------------------------------------
                    myDataTable.Rows.Add(dataRow);
                }
                else if (pRow["CHEQUE_REMARKS"].ToString().Contains("{RESPONSE_MSG:NO DATA FOUND,RESPONSE_CODE:1290}"))
                {
                    DataRow dataRow = myDataTable.NewRow();
                    dataRow[0] = sl;
                    string strRow0 = Convert.ToString(dataRow[0]);
                    dataRow[1] = pRow["UBP_REB_ACCOUNT_ID"];
                    string strRow1 = Convert.ToString(dataRow[1]);
                    dataRow[2] = noDataFound;
                    string strRow2 = Convert.ToString(dataRow[2]);
                    dataRow[3] = noDataFound;
                    string strRow3 = Convert.ToString(dataRow[3]);
                    dataRow[4] = noDataFound;
                    string strRow4 = Convert.ToString(dataRow[4]);
                    dataRow[5] = noDataFound;
                    string strRow5 = Convert.ToString(dataRow[5]);
                    dataRow[6] = noDataFound;
                    string strRow6 = Convert.ToString(dataRow[6]);
                    dataRow[7] = noDataFound;
                    string strRow7 = Convert.ToString(dataRow[7]);
                    dataRow[8] = noDataFound;
                    string strRow8 = Convert.ToString(dataRow[8]);
                    dataRow[9] = noDataFound;
                    string strRow9 = Convert.ToString(dataRow[9]);
                    dataRow[10] = noDataFound;
                    string strRow10 = Convert.ToString(dataRow[10]);
                    dataRow[11] = noDataFound;
                    string strRow11 = Convert.ToString(dataRow[11]);
                    dataRow[12] = pRow["UBP_REB_BBPS_REF"].ToString();
                    string strRow12 = Convert.ToString(dataRow[12]);
                    dataRow[13] = pRow["UBP_REB_BBPS_MONTH"].ToString();
                    string strRow13 = Convert.ToString(dataRow[13]);
                    dataRow[14] = pRow["UBP_REB_BBPS_TIME"].ToString();
                    string strRow14 = Convert.ToString(dataRow[14]);
                    //------------------------------------------------------
                    myDataTable.Rows.Add(dataRow);
                }
                else
                {

                    if (pRow["CHEQUE_REMARKS"].ToString() != "")
                    {
                        string s = pRow["CHEQUE_REMARKS"].ToString();
                        string[] sList = s.Split(',');
                        string billno = sList[0].Trim().Replace("BILL_NO:", "");
                        string bookno = sList[1].Trim().Replace("BOOK_NO:", "");
                        string smsAcnt = sList[2].Trim().Replace("SMS_AC_NO:", "");
                        string rebbillMonth = sList[3].Trim().Replace("BILL_MONTH:", "");
                        string billYear = sList[4].Trim().Replace("BILL_YEAR:", "");
                        string isuDate = sList[5].Trim().Replace("ISSUE_DATE:", "");
                        string dueDate = sList[6].Trim().Replace("DUE_DATE:", "");
                        string dueAmt = sList[7].Trim().Replace("DUE_AMOUNT:", "");
                        string lpcDate = sList[9].Trim().Replace("LPC_DATE:", "");
                        //string dueLpcAmt = sList[9].Trim().Replace("DUE_LPC_AMOUNT:", "");
                        string paidStatus = sList[10].Trim().Replace("PAID_STATUS:", "");
                        string pbsCode = sList[11].Trim().Replace("PBS_CODE:", "");
                        string pbsAmtB = sList[12].Trim().Replace("PBS_NAME_B:", "");
                        string pbsNameE = sList[13].Trim().Replace("PBS_NAME_E:", "");
                        pbsNameE = pbsNameE.Remove(pbsNameE.Length - 1, 1);
                        string responseCode = sList[14].Trim().Replace("RESPONSE_CODE:", "");
                        string billInqMonth = pRow["UBP_REB_BBPS_MONTH"].ToString();

                        DataRow dataRow = myDataTable.NewRow();
                        dataRow[0] = sl;
                        string strRow0 = Convert.ToString(dataRow[0]);
                        dataRow[1] = pRow["UBP_REB_ACCOUNT_ID"].ToString();
                        string strRow1 = Convert.ToString(dataRow[1]);
                        dataRow[2] = (rebbillMonth == "null" ? noDataFound : rebbillMonth);
                        string strRow2 = Convert.ToString(dataRow[2]);
                        dataRow[3] = (billYear == "null" ? noDataFound : billYear);
                        string strRow3 = Convert.ToString(dataRow[3]);
                        dataRow[4] = (dueAmt == "null" ? noDataFound : dueAmt);
                        string strRow4 = Convert.ToString(dataRow[4]);
                        dataRow[5] = (isuDate == "null" ? noDataFound : isuDate);
                        string strRow5 = Convert.ToString(dataRow[5]);
                        dataRow[6] = (dueDate == "null" ? noDataFound : dueDate);
                        string strRow6 = Convert.ToString(dataRow[6]);
                        dataRow[7] = (lpcDate == "null" ? noDataFound : lpcDate);
                        string strRow7 = Convert.ToString(dataRow[7]);
                        dataRow[8] = "";// (dueLpcAmt == "null" ? noDataFound : dueLpcAmt);
                        string strRow8 = Convert.ToString(dataRow[8]);
                        dataRow[9] = (pbsNameE == "null" ? noDataFound : pbsNameE);
                        string strRow9 = Convert.ToString(dataRow[9]);
                        dataRow[10] = (pbsCode == "null" ? noDataFound : pbsCode);
                        string strRow10 = Convert.ToString(dataRow[10]);
                        dataRow[11] = (paidStatus == "null" ? noDataFound : paidStatus);
                        string strRow11 = Convert.ToString(dataRow[11]);
                        dataRow[12] = pRow["UBP_REB_BBPS_REF"].ToString();
                        string strRow12 = Convert.ToString(dataRow[12]);
                        dataRow[13] = billInqMonth;
                        string strRow13 = Convert.ToString(dataRow[13]);
                        dataRow[14] = pRow["UBP_REB_BBPS_TIME"].ToString();
                        string strRow14 = Convert.ToString(dataRow[14]);

                        //------------------------------------------------------
                        myDataTable.Rows.Add(dataRow);
                    }
                    else
                    {

                        DataRow dataRow = myDataTable.NewRow();
                        dataRow[0] = sl;
                        string strRow0 = Convert.ToString(dataRow[0]);
                        dataRow[1] = pRow["UBP_REB_ACCOUNT_ID"].ToString();
                        string strRow1 = Convert.ToString(dataRow[1]);
                        dataRow[2] = processing;
                        string strRow2 = Convert.ToString(dataRow[2]);
                        dataRow[3] = processing;
                        string strRow3 = Convert.ToString(dataRow[3]);
                        dataRow[4] = processing;
                        string strRow4 = Convert.ToString(dataRow[4]);
                        dataRow[5] = processing;
                        string strRow5 = Convert.ToString(dataRow[5]);
                        dataRow[6] = processing;
                        string strRow6 = Convert.ToString(dataRow[6]);
                        dataRow[7] = processing;
                        string strRow7 = Convert.ToString(dataRow[7]);
                        dataRow[8] = processing;
                        string strRow8 = Convert.ToString(dataRow[8]);
                        dataRow[9] = processing;
                        string strRow9 = Convert.ToString(dataRow[9]);
                        dataRow[10] = processing;
                        string strRow10 = Convert.ToString(dataRow[10]);
                        dataRow[11] = processing;
                        string strRow11 = Convert.ToString(dataRow[11]);
                        dataRow[12] = pRow["UBP_REB_BBPS_REF"].ToString();
                        string strRow12 = Convert.ToString(dataRow[12]);
                        dataRow[13] = pRow["UBP_REB_BBPS_MONTH"].ToString();
                        string strRow13 = Convert.ToString(dataRow[13]);
                        dataRow[14] = pRow["UBP_REB_BBPS_TIME"].ToString();
                        string strRow14 = Convert.ToString(dataRow[14]);
                        //------------------------------------------------------
                        myDataTable.Rows.Add(dataRow);
                    }
                }

                sl++;

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