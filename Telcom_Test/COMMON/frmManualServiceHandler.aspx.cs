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

public partial class COMMON_frmManualServiceHandler : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objServiceHandler = new clsServiceHandler();

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
    protected void btnAddRequest_Click(object sender, EventArgs e)
    {
        string strSender = "", strReceipent = "", strMsg = "", strAccID = "", strReturnMessage = "";

        strSender = txtSender.Text.Trim();
        strReceipent = txtReceipentParty.Text.Trim();
        strMsg = "*" + txtMsg.Text.Trim() + "#";
        strAccID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST",
                                     "ACCNT_ID", "ACCNT_MSISDN", txtSender.Text.Trim());
        strReturnMessage = objServiceHandler.SaveServiceRequest(strSender, strReceipent, strMsg, strAccID);
        lblMessage.Text = strReturnMessage;
        SaveAuditInfo("Manual Service Handler", "Remarks");
        clearControl();

    }

    private void clearControl()
    {
        txtMsg.Text = "";
        txtReceipentParty.Text = "";
        txtSender.Text = "";
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType,
            IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
