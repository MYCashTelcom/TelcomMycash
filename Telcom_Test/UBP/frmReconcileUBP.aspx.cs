using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UBP_frmReconcileUBP : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PopulateReconcileBill();
    }

    private void PopulateReconcileBill()
    {
        string strSelectedText = ddlSearchType.SelectedItem.Value;
        string strSearchText = txtSearchText.Text.Trim();
        string strSubQuery = "";

        if (txtSearchText.Equals(""))
        {
            lblMsg.Text = "Please input search text";
            return;
        }

        if (strSelectedText.Equals("AccountNumber"))
        {
            strSubQuery = "AND UT.ACCOUNT_NUMBER = '" + strSearchText + "'";
        }
        else if (strSelectedText.Equals("BillNumber"))
        {
            strSubQuery = "AND UT.BILL_NUMBER = '" + strSearchText + "'";
        }
        else if (strSelectedText.Equals("RequestId"))
        {
            strSubQuery = "AND SR.REQUEST_ID = '" + strSearchText + "'";
        }

        //string strSql = "SELECT UT.UTILITY_TRAN_ID, UT.ACCOUNT_NUMBER ACCNT_NUMBER, UT.BILL_NUMBER, UT.REQUEST_ID SMSC_REF_ID, UT.OWNER_CODE, UT.TOTAL_BILL_AMOUNT TOTAL_AMOUNT, UT.REVERSE_STATUS UT_REV_STE, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, SR.REQUEST_ID SUCCESS_ID, CAT.CAS_TRAN_STATUS CAS_REV_STE FROM UTILITY_TRANSACTION UT, SERVICE_REQUEST SR, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE UT.REQUEST_ID = SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID = CAT.REQUEST_ID " + strSubQuery + " GROUP BY UT.UTILITY_TRAN_ID, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.REQUEST_ID, UT.OWNER_CODE, UT.TOTAL_BILL_AMOUNT, UT.REVERSE_STATUS, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, SR.REQUEST_ID, CAT.CAS_TRAN_STATUS ORDER BY UT.UTILITY_TRAN_ID DESC";
		
		string strSql = "SELECT UT.UTILITY_TRAN_ID, UT.ACCOUNT_NUMBER ACCNT_NUMBER, UT.BILL_NUMBER, UT.REQUEST_ID SMSC_REF_ID, UT.OWNER_CODE, UT.TOTAL_BILL_AMOUNT TOTAL_AMOUNT, UT.REVERSE_STATUS UT_REV_STE, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, SR.REQUEST_ID SUCCESS_ID, CAT.CAS_TRAN_STATUS CAS_REV_STE, UT.SOURCE_ACC_NO, SR.REQUEST_TIME FROM UTILITY_TRANSACTION UT, SERVICE_REQUEST SR, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE UT.REQUEST_ID = SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID = CAT.REQUEST_ID " + strSubQuery + " GROUP BY UT.UTILITY_TRAN_ID, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.REQUEST_ID, UT.OWNER_CODE, UT.TOTAL_BILL_AMOUNT, UT.REVERSE_STATUS, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, SR.REQUEST_ID, CAT.CAS_TRAN_STATUS, UT.SOURCE_ACC_NO, SR.REQUEST_TIME ORDER BY UT.UTILITY_TRAN_ID DESC";

        DataSet dts = objServiceHandler.ExecuteQuery(strSql);
        gdvReconcileBillDetail.DataSource = dts;
        gdvReconcileBillDetail.DataBind();
    }
    protected void gdvReconcileBillDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label ResponseMessage = (Label)e.Row.FindControl("lblResponseMessage");
            string lblResponseMessage = ResponseMessage.Text;
            Label SuccessStatus = (Label)e.Row.FindControl("lblSuccessStatus");
            string lblSuccessStatus = SuccessStatus.Text;

            if (lblResponseMessage.ToUpper().Equals("Success".ToUpper()) || lblSuccessStatus.Equals("R") ||
            lblResponseMessage.ToUpper().Equals("Top-up request accepted successfully.".ToUpper()) || lblResponseMessage.Equals("Data Transaction Successfull"))
            {
                Button btnButton = (Button)e.Row.FindControl("btnUpdate");
                btnButton.Enabled = false;
            }
        }
    }
    protected void gdvReconcileBillDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label RequestId = gdvReconcileBillDetail.Rows[e.RowIndex].FindControl("lblRequestId") as Label;
            string strRequestId = RequestId.Text;

            string strReverse = objServiceHandler.ReverseRequestId(strRequestId);

            if (strReverse.Equals("SUCCESS"))
            {
                PopulateReconcileBill();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}