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

public partial class COMMON_frmRebBulkBillPayment : System.Web.UI.Page
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
                //GetPaidBill();
                //BulkBillPendingData();
            }
            catch (Exception exxx)
            {

            }

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
        lblError.Text = "";
        LabelSucc.Text = "";
        if (DDLPaymentMonth.SelectedValue.ToString() == "")
        {
            lblError.Text = "Please select payment month";
            return;
        }
        else if (DDLPaymentMonth.SelectedValue.ToString() == "1")
        {
            string strSqlProc = "PROCESS_REB_BILL_BULK_PAY_TEST('000000000701')";
            //strMSG = objsrvsHndlr.ExecuteProcedure(strSqlProc);
            if (strMSG == "Successfull.")
            {
                LabelSucc.Text = "Successfully executed.";
            }
            else
            {
                lblError.Text = "Failed!!";
            }
        }
        else if (DDLPaymentMonth.SelectedValue.ToString() == "2")
        {
            string strSqlProc = "PROCESS_REB_BILL_BULK_PAY('000000000701')";
            // strMSG = objsrvsHndlr.ExecuteProcedure(strSqlProc);
            if (strMSG == "Successfull.")
            {
                LabelSucc.Text = "Successfully executed.";
            }
            else
            {
                lblError.Text = "Failed!!";
            }
        }
        else
        {
            lblError.Text = "wrong selection!!";
        }
        
    }

    protected void btnCheckBalance_Click(object sender, EventArgs e)
    {
        string Balance = objsrvsHndlr.getRebBalance();
        txtBalance.Text = Balance;
    }
    
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    GetPaidBill();
    //}

    protected void gdvBillPendingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvBillPendingList.PageIndex = e.NewPageIndex;
        BulkBillPendingData();
    }
    protected void gdvPaidBillList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvPaidBillList.PageIndex = e.NewPageIndex;
        GetPaidBill();
    }

    //protected void gdvBranchList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    gdvBranchList.EditIndex = -1;
    //    GetData();
    //}

    //protected void gdvBranchList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{

    //}

    //protected void gdvBranchList_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    //gdvBranchList.EditIndex = e.NewEditIndex;
    //}

    //protected void gdvBranchList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    //TextBox txtBranchName = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtBranchName") as TextBox;
    //    //TextBox txtEmpNamee = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtEmployeeName") as TextBox;
    //    //TextBox txtEmpMobilee = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtEmployeeMobile") as TextBox;
    //    //TextBox txtDesignation = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtDesignation") as TextBox;
    //    //TextBox txtCellCode = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtCellCode") as TextBox;
    //    //TextBox txtCellName = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtCellName") as TextBox;

    //    //TextBox tstatus = gdvBranchList.Rows[e.RowIndex].FindControl("txtStatus") as TextBox;

    //    //Label lblId = gdvBranchList.Rows[e.RowIndex].FindControl("lblBranchId") as Label;
    //    //string name = txtBranchName.Text.Trim();
    //    //string empName = txtEmpNamee.Text.Trim();
    //    //string empMobile = txtEmpMobilee.Text.Trim();
    //    //string branchId = lblId.Text.Trim();
    //    //string designation = txtEmpMobilee.Text.Trim();
    //    //string cellCode = txtCellCode.Text.Trim();
    //    //string cellName = txtCellName.Text.Trim();

    //    //string status = tstatus.Text.Trim();
    //    ////txtSearch.Text = branchId;
    //    //string updateRes = objsrvsHndlr.UpdatePlilBranchInfo(branchId, name, "", empName, empMobile, status);
    //    //lblMessage.Text = updateRes;
    //    //gdvBranchList.EditIndex = -1;

    //    GetData();
    //}

    //protected void gdvBranchList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{

    //}
    protected void GetPaidBill()
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        string curDate = "221117";
        //string curDate = dateTime.ToString("yyMMdd");
        string strQry = "SELECT * FROM UTILITY_TRANSACTION UT,UBP_REB_BULK_BILL_PAY_STATUS US,SERVICE_REQUEST SR WHERE  UT.REQUEST_ID=SR.REQUEST_ID AND US.UBP_REB_BBPS_REF=SR.SMSC_REFERENCE_NO  AND UT.SERVICE='UBPREB' AND UT.UTILITY_TRAN_ID LIKE '%"+curDate+"%' AND UT.RESPONSE_LOG_BP LIKE '%#RESPONSE_CODE#:1200%' order by UT.UTILITY_TRAN_ID desc"; 
        strQry=strQry.Replace('#','"');
       
        DataSet dt = objsrvsHndlr.ExecuteQuery(strQry);
        int CountRow = dt.Tables[0].Rows.Count;
        PaidBillList.Text = CountRow.ToString();
        gdvPaidBillList.DataSource = dt;
        gdvPaidBillList.DataBind();
        gdvPaidBillList.DataSource = dt;
        gdvPaidBillList.DataBind();
    }
    protected void BulkBillPendingData()
    {
        //string strQry = "SELECT * FROM UBP_REB_BULK_BILL_PAY WHERE UBP_REB_ACCOUNT_STATUS='A'  AND UBP_REB_PURPOSE ='P' AND IS_PAID='N' AND UBP_REB_BBP_ID NOT IN"
        //          +"(SELECT UBP_REB_BBP_ID FROM UBP_REB_BULK_BILL_PAY_STATUS WHERE UBP_REB_BBPS_MONTH=TO_CHAR(SYSDATE,'MM') "
        //          +" AND UBP_REB_BBPS_YEAR=TO_CHAR(SYSDATE,'YYYY') AND UBP_REB_BBPS_PURPOSE='P' )";
        string strQry = "SELECT * FROM UBP_REB_BULK_BILL_PAY WHERE UBP_REB_ACCOUNT_STATUS='A'  AND UBP_REB_PURPOSE ='P' AND IS_PAID='Y' AND UBP_REB_BBP_ID NOT IN"
                 + "(SELECT UBP_REB_BBP_ID FROM UBP_REB_BULK_BILL_PAY_STATUS WHERE UBP_REB_BBPS_MONTH=TO_CHAR(SYSDATE,'MM') "
                 + " AND UBP_REB_BBPS_YEAR=TO_CHAR(SYSDATE,'YYYY') AND UBP_REB_BBPS_PURPOSE='P' )";  
        DataSet dt1 = objsrvsHndlr.ExecuteQuery(strQry);
        int row = dt1.Tables[0].Rows.Count;
        PendingBillCount.Text = row.ToString();
        gdvBillPendingList.DataSource = dt1;
        gdvBillPendingList.DataBind();
        gdvBillPendingList.DataSource = dt1;
        gdvBillPendingList.DataBind();
    }
}