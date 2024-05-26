using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class COMMON_frmTranReverseTopUp : System.Web.UI.Page  
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
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
            catch (Exception ex)
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
                ex.Message.ToString();
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
   
    private void SaveAuditLog(string strOperationType, string strRemarks)
    {
        objSysAdmin.SetSeessionData(Session["Branch_ID"].ToString());
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType,IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);     
    }
    protected void btnReverse_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        string[] strRequestIds = txtReqID.Text.Trim().Split(',');

        for (int i = 0; i < strRequestIds.Length; i++)
        {
            string strRequestId = strRequestIds[i].Trim().ToString();

            string strSql = "SELECT COUNT(*) FROM TOPUP_TRANSACTION WHERE REVERSE_STATUS = 'R' AND REQUEST_ID = '" + strRequestId + "'";
            string isReversed = objServiceHandler.ReturnString(strSql);

            if (strRequestId.Length == 14 && isReversed.Equals("0"))
            {
                objServiceHandler.ReverseTopupTransaction(strRequestId);
            }
        }
		SaveAuditLog("ReverseTopup", "Topup Transaction Reverse Manually");
        ClearControl();
		PopulateGridViewForReverseStatus();
    }

    private void ClearControl()
    {
        txtReqID.Text = "";
    }
	
	private void PopulateGridViewForReverseStatus()
    {
        string strSql = @"SELECT TOPUP_TRAN_ID,REQUEST_ID,SOURCE_ACCNT_NO,TRAN_DATE,TRAN_AMOUNT,SUBSCRIBER_MOBILE_NO,REVERSE_STATUS,REVERSE_NOTE FROM TOPUP_TRANSACTION WHERE REQUEST_STATUS = 'E' AND SUCCESSFUL_STATUS = 'F' AND SSL_FINAL_STATUS = '999' AND SSL_FINAL_MESSAGE = 'Failed' AND ALL_FINAL_STATUS = 'F' AND REVERSE_STATUS = 'N'";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

        gdvReverseStatus.DataSource = oDs;
        gdvReverseStatus.DataBind();
    }
    protected void btnGetStatus_Click(object sender, EventArgs e)
    {
        PopulateGridViewForReverseStatus();
    }
}
