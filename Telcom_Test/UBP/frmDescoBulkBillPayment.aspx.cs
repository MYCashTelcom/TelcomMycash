using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Net;

using Newtonsoft.Json.Converters;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Newtonsoft.Json;
public partial class UBP_frmDescoBulkBillPayment : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objsrvsHndlr = new clsServiceHandler();
    DataSet dsData;
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        lblMessage.Text = "";
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

    protected void btnSearchOffice_Click(object sender, EventArgs e)
    {


    }
    protected void btnCheckPendingBill(object sender, EventArgs e)
    {
        BulkBillPendingData();
    }
    protected void btnCheckPaidBill(object sender, EventArgs e)
    {
        GetPaidBill();
    }
    protected void btnPaid_Click(object sender, EventArgs e)
    {
       string strMSG = "";
       // ... other code
          if (strMSG == "Successfull.")
          {
            LabelSucc.Text = "Successfully executed.";
          
          }
             else
           {
            lblError.Text = "Failed!!";
           }
       

                }

    protected void btnCheckBalance_Click(object sender, EventArgs e)
    {
        string Balance = objsrvsHndlr.getRebBalance();
        txtBalance.Text = Balance;
    }
    protected void gdcBillPendingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdcBillPendingList.PageIndex = e.NewPageIndex;
        BulkBillPendingData();
    }
    protected void gdcPaidBillList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdcPaidBillList.PageIndex = e.NewPageIndex;
        GetPaidBill();
    }

    protected void GetPaidBill()
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        string curDate = "221117";
        //string curDate = dateTime.ToString("yyMMdd");
        //fahim---Selecte query
        string strQry = "SELECT * FROM UTILITY_TRANSACTION UT, EDOTCO_UTILITY_PAY_STATUS US, SERVICE_REQUEST SR WHERE UT.REQUEST_ID=SR.REQUEST_ID AND US.UBPS_REF=SR.SMSC_REFERENCE_NO AND UT.OWNER_CODE='DS' AND UT.UTILITY_TRAN_ID LIKE '%230917%' AND UT.RESPONSE_LOG_BP LIKE '%message:''DescoBill Payment Information Received.''%' ORDER BY UT.UTILITY_TRAN_ID DESC;";
        strQry = strQry.Replace('#', '"');


        DataSet dt = objsrvsHndlr.ExecuteQuery(strQry);
        int CountRow = dt.Tables[0].Rows.Count;
        PaidBillList.Text = CountRow.ToString();
        gdcPaidBillList.DataSource = dt;
        gdcPaidBillList.DataBind();
        gdcPaidBillList.DataSource = dt;
        gdcPaidBillList.DataBind();
    }
    protected void BulkBillPendingData()
    {
        //string strQry = "SELECT * FROM UBP_REB_BULK_BILL_PAY WHERE UBP_REB_ACCOUNT_STATUS='A'  AND UBP_REB_PURPOSE ='P' AND IS_PAID='N' AND UBP_REB_BBP_ID NOT IN"
        //          +"(SELECT UBP_REB_BBP_ID FROM UBP_REB_BULK_BILL_PAY_STATUS WHERE UBP_REB_BBPS_MONTH=TO_CHAR(SYSDATE,'MM') "
        //          +" AND UBP_REB_BBPS_YEAR=TO_CHAR(SYSDATE,'YYYY') AND UBP_REB_BBPS_PURPOSE='P' )";
        //fahim pending query

        // Example correction for the BulkBillPendingData method
        string strQry = "SELECT * FROM EDOTCO_UTILITY_BILL_PAY WHERE ACCOUNT_STATUS='A' AND PURPOSE ='P' AND IS_PAID='L' AND BILL_TYPE='DS' AND EDOTCO_BILL_PAY_ID NOT IN" +
                        "(SELECT EDOTCO_BILL_PAY_ID FROM EDOTCO_UTILITY_PAY_STATUS WHERE UBPS_MONTH=TO_CHAR(add_months(trunc(sysdate,'MM'),-1),'MM') " +
                        " AND UBPS_YEAR=TO_CHAR(SYSDATE,'YYYY') AND UBPS_PURPOSE='P')";

        DataSet dt1 = objsrvsHndlr.ExecuteQuery(strQry);
        int row = dt1.Tables[0].Rows.Count;
        PendingBillCount.Text = row.ToString();
        gdcBillPendingList.DataSource = dt1;
        gdcBillPendingList.DataBind();
        gdcBillPendingList.DataSource = dt1;
        gdcBillPendingList.DataBind();
    }

}