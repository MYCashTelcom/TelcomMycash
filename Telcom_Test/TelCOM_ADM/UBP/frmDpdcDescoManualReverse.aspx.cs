using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UBP_frmDpdcDescoManualReverse : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            dtpFrDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-7));

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

            LoadGrid();

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

    private void LoadGrid()
    {
        try
        {
            string strSql = "";
            //strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, CAT.REQUEST_ID, UT.TRANSA_DATE CAS_TRAN_DATE, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER,  "
            //         + " UT.OWNER_CODE, UT.SOURCE_ACC_NO, UT.REVERSE_STATUS, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.PAYER_MOBILE_NO, "
            //         + " UT.CHECK_STATUS, UT.CANCLE_RESPONSE FROM UTILITY_TRANSACTION UT, "
            //         + " SERVICE_REQUEST SR, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE UT.SERVICE = 'UBP' "
            //         + " AND UT.OWNER_CODE IN ('DPDC', 'DS') AND UT.TOTAL_BILL_AMOUNT IS NOT NULL AND UT.REVERSE_STATUS = 'N' "
            //         + " AND UT.RESPONSE_STATUS_BP IS NULL AND UT.CHECK_STATUS = 'Y' AND UT.CANCLE_RESPONSE NOT IN ('status:804', '804') "
            //         + " AND UT.CHEQUE_REMARKS NOT LIKE ('%You do not have enough balance for this transaction%') "
            //         + " AND UT.CHEQUE_REMARKS NOT LIKE ('%You are not allowed for this transaction%') AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
            //         + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_STATUS = 'A' AND TRUNC(UT.TRANSA_DATE) "
            //         + " BETWEEN '" + dtpFrDate.DateString + "' AND '" + dtpToDate.DateString + "' ORDER BY UT.TRANSA_DATE ASC ";


            //strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, CAT.REQUEST_ID, UT.TRANSA_DATE , UT.ACCOUNT_NUMBER, UT.BILL_NUMBER,  "
            //         + " UT.OWNER_CODE, UT.SOURCE_ACC_NO, UT.REVERSE_STATUS, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.PAYER_MOBILE_NO, "
            //         + " UT.CHECK_STATUS, UT.CANCLE_RESPONSE FROM UTILITY_TRANSACTION UT, "
            //         + " SERVICE_REQUEST SR, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE UT.SERVICE = 'UBP' "
            //         + " AND UT.OWNER_CODE IN ('DPDC', 'DS') AND UT.TOTAL_BILL_AMOUNT IS NOT NULL AND UT.REVERSE_STATUS = 'N' "
            //         + " AND UT.RESPONSE_STATUS_BP IS NULL AND UT.CHECK_STATUS = 'Y' AND (UT.CANCLE_RESPONSE NOT IN ('status:804', '804') OR UT.CANCLE_RESPONSE IS NULL) "
            //         + " AND UT.CHEQUE_REMARKS NOT LIKE ('%You do not have enough balance for this transaction%') "
            //         + " AND UT.CHEQUE_REMARKS NOT LIKE ('%You are not allowed for this transaction%') AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
            //         + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_STATUS = 'A' AND TRUNC(UT.TRANSA_DATE) "
            //         + " BETWEEN '" + dtpFrDate.DateString + "' AND '" + dtpToDate.DateString + "' ORDER BY UT.TRANSA_DATE ASC ";


            //strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, CAT.REQUEST_ID, UT.TRANSA_DATE , UT.ACCOUNT_NUMBER, UT.BILL_NUMBER,  "
            //         + " UT.OWNER_CODE, UT.SOURCE_ACC_NO, UT.REVERSE_STATUS, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.PAYER_MOBILE_NO, "
            //         + " UT.CHECK_STATUS, UT.CANCLE_RESPONSE FROM UTILITY_TRANSACTION UT, "
            //         + " SERVICE_REQUEST SR, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE UT.SERVICE = 'UBP' "
            //         + " AND UT.OWNER_CODE IN ('DPDC', 'DS') AND UT.REVERSE_STATUS = 'N' "
            //         + " AND UT.RESPONSE_STATUS_BP IS NULL AND UT.CHECK_STATUS = 'Y' AND (UT.CANCLE_RESPONSE NOT IN ('status:804', '804') OR UT.CANCLE_RESPONSE IS NULL) "
            //         + " AND UT.CHEQUE_REMARKS NOT LIKE ('%You do not have enough balance for this transaction%') "
            //         + " AND UT.CHEQUE_REMARKS NOT LIKE ('%You are not allowed for this transaction%') AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
            //         + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_STATUS = 'A' AND TRUNC(UT.TRANSA_DATE) "
            //         + " BETWEEN '" + dtpFrDate.DateString + "' AND '" + dtpToDate.DateString + "' ORDER BY UT.TRANSA_DATE ASC ";

            //strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, CAT.REQUEST_ID, UT.TRANSA_DATE , UT.ACCOUNT_NUMBER, UT.TOTAL_BILL_AMOUNT, UT.BILL_NUMBER,"
            //        + " UT.OWNER_CODE, UT.SOURCE_ACC_NO, UT.REVERSE_STATUS, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.PAYER_MOBILE_NO,"
            //        + " UT.CHECK_STATUS, UT.CANCLE_RESPONSE FROM UTILITY_TRANSACTION UT, SERVICE_REQUEST SR,"
            //        + " BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE UT.SERVICE IN ('UBP', 'UWZP') AND UT.OWNER_CODE IN ('DPDC', 'DS', 'WZ')"
            //        + " AND UT.REVERSE_STATUS = 'N' AND (UT.CANCLE_RESPONSE NOT IN ('status:804') OR UT.CANCLE_RESPONSE IS NULL)"
            //        + " AND UT.CHEQUE_REMARKS NOT LIKE ('%You do not have enough balance for this transaction%') AND UT.CHEQUE_REMARKS NOT LIKE"
            //        + " ('%You are not allowed for this transaction%') AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID = CAT.REQUEST_ID"
            //        + " AND CAT.CAS_TRAN_STATUS = 'A' AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpFrDate.DateString + "' AND '" + dtpToDate.DateString + "'"
            //        + " AND (RESPONSE_STATUS_BP IS NULL OR RESPONSE_STATUS_BP IN ('999', '804', '411', '102', '801', '901')) AND RESPONSE_MSG_BP IS NOT NULL ORDER BY UT.TRANSA_DATE ASC ";

            strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, CAT.REQUEST_ID, UT.TRANSA_DATE , UT.ACCOUNT_NUMBER, UT.TOTAL_BILL_AMOUNT,"
                    + " UT.BILL_NUMBER, UT.OWNER_CODE, UT.SOURCE_ACC_NO, UT.REVERSE_STATUS, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP,"
                    + " UT.PAYER_MOBILE_NO, UT.CHECK_STATUS, UT.CANCLE_RESPONSE FROM UTILITY_TRANSACTION UT, SERVICE_REQUEST SR,"
                    + " BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE UT.SERVICE IN ('UBP', 'UWZP', 'UBPW','UBPDSP')"
                    + " AND UT.OWNER_CODE IN ('DPDC', 'DS', 'WZ', 'WS', 'DSP') AND UT.REVERSE_STATUS = 'N'"
                    + " AND (UT.CANCLE_RESPONSE NOT IN ('status:804') OR UT.CANCLE_RESPONSE IS NULL)"
                    + " AND (UT.CHEQUE_REMARKS NOT LIKE ('%You do not have enough balance for this transaction%')"
                    + " OR UT.CHEQUE_REMARKS NOT LIKE ('%You are not allowed for this transaction%') OR UT.CHEQUE_REMARKS IS NULL)"
                    + " AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_STATUS = 'A'"
                    + " AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpFrDate.DateString + "' AND '" + dtpToDate.DateString + "'"
                    + " AND (RESPONSE_STATUS_BP IS NULL OR RESPONSE_STATUS_BP IN ('999', '804', '411', '102', '801', '901', '0', '5', '500'))"
                    + " AND (RESPONSE_MSG_BP IS NOT NULL OR RESPONSE_MSG_BP IS NULL) ORDER BY UT.TRANSA_DATE ASC ";

            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvRequestList.DataSource = oDataSet;
            grvRequestList.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void grvRequestList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvRequestList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void lnkButtonReverse_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;

            Label lblUtlTrxidId = (Label)Grow.FindControl("Label1");
            string strUtlTrxid = lblUtlTrxidId.Text.ToString();

            Label lblRequestId = (Label)Grow.FindControl("lblRequestId");
            string strRequestId = lblRequestId.Text.ToString();

            string strReverse = objServiceHandler.ReverseRequestId(strRequestId);

            if (strReverse == "Success")
            {
                objServiceHandler.UpdateBillTrxIdCancelToReverse(strUtlTrxid);

                for (int count = 0; count < 10; count++)
                {
                    // checking reverse status is R
                    string strBillPayReverseStatus = " SELECT DISTINCT ACCOUNT_NUMBER, BILL_NUMBER, OWNER_CODE, REVERSE_STATUS FROM UTILITY_TRANSACTION WHERE UTILITY_TRAN_ID = '" + strUtlTrxid + "'";
                    DataSet oSet = objServiceHandler.ExecuteQuery(strBillPayReverseStatus);
                    if (oSet.Tables[0].Rows.Count > 0)
                    {
                        string strGetReverseStatus = "";
                        string strAccountNo = "";
                        string strBillNo = "";
                        string strOwnerCode = "";

                        foreach (DataRow prow in oSet.Tables[0].Rows)
                        {
                            strGetReverseStatus = prow["REVERSE_STATUS"].ToString();
                            strAccountNo = prow["ACCOUNT_NUMBER"].ToString();
                            strBillNo = prow["BILL_NUMBER"].ToString();
                            strOwnerCode = prow["OWNER_CODE"].ToString();
                        }

                        string strRemarks = "UtlTRxId:" + strUtlTrxid + ", User Id:" +
                                            Session["UserLoginName"].ToString() + ", AccountNo:" + strAccountNo + ", BillNo:"
                                            + strBillNo + ", OwnerCode:" + strOwnerCode;

                        if (strGetReverseStatus == "R")
                        {
                            SaveAuditInfo("Reverse", strRemarks + ", Reverse Status:" + strGetReverseStatus);
                            LoadGrid();
                            dtvDetails.Visible = false;
                            lblMessage.Text = "";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Bill payment successfully reversed');", true);
                            return;
                        }
                        else
                        {
                            Thread.Sleep(20000);
                        }
                    }
                }
            }
            else
            {
                lblMessage.ForeColor = Color.White;
                lblMessage.Text = "Reverse Failed. Please Try Again.";
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }


    protected void lnkButtonDetails_Click(object sender, EventArgs e)
    {
        try
        {
            dtvDetails.Visible = true;
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;

            Label lblUtlTrxidId = (Label)Grow.FindControl("Label1");
            string strUtlTrxid = lblUtlTrxidId.Text.ToString();

            string strSql = " SELECT DISTINCT UTILITY_TRAN_ID, STAKEHOLDER_ID, LOCATION_ID, ACCOUNT_NUMBER, "
                            + " BILL_NUMBER, TOTAL_DPDC_AMOUNT, VAT_AMOUNT, PAYMENT_MOOD, BILL_MONTH, BILL_YEAR, "
                            + " CHEQUE_REMARKS, CHANNEL_ID, SERVICE, REQUEST_ID, OWNER_CODE, TRANSACTION_STATUS, "
                            + " FINAL_STATUS, SOURCE_ACC_NO, TRANSA_DATE, BILL_DUE_DATE, BILL_STATUS, TOTAL_BILL_AMOUNT, "
                            + " NET_DPDC_AMOUNT, RESPONSE_STATUS, RESPONSE_MSG, REVERSE_STATUS, REVERSE_NOTE, "
                            + " REQUEST_LOG, RESPONSE_LOG, ST_CHARGE, BILL_AMT_AFTERDUEDATE, REQUEST_LOG_BP, RESPONSE_LOG_BP, "
                            + " RESPONSE_MSG_BP, RESPONSE_STATUS_BP, REQUEST_PARTY_TYPE, ORGANIZATION_CODE, PAYER_MOBILE_NO, "
                            + " CHECK_STATUS, CHECK_STATUS_RESPONSE, CHECK_STATUS_REQ_LOG, CHECH_STATUS_RES_LOG, "
                            + " CANCLE_RESPONSE, CANCLE_REQ_LOG, CANCLE_RES_LOG, CANCLE_MESSAGE "
                           
                            + " FROM UTILITY_TRANSACTION WHERE UTILITY_TRAN_ID = '" + strUtlTrxid + "'";

            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            dtvDetails.DataSource = oDataSet;
            dtvDetails.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
