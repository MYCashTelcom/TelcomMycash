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

public partial class COMMON_frmSMSOption : System.Web.UI.Page 
{
   
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsAccountHandler objAccount = new clsAccountHandler();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSMSContent.Attributes.Add("maxlength", txtSMSContent.MaxLength.ToString());
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
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnSMSSend_Click(object sender, EventArgs e) 
    {
        string strMsg = "", strSql = "", strReportFile = "";  
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        try
        {
            string strSMSContent = "";
            strSMSContent=(txtSMSContent.Text.ToString()).Replace("'", "");
            strMsg = objServiceHandler.GetSMSBroadcastMsg(strSMSContent, rdbSelectiontype.SelectedValue.ToString(), ddlRank.SelectedValue.ToString());
            lblMsg.Text = strMsg;
            SaveAuditInfo("Insert", "SMS Option");
            txtSMSContent.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.ToString();
        }
    }
    protected void btnPkgSMSSend_Click(object sender, EventArgs e)
    {
        string strMsg = "", strSql = "", strReportFile = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        try
        {
            string strSMSContent = "";
            strSMSContent = (txtPkgSMSContent.Text.ToString()).Replace("'", "");
            if (chkActive.Checked)
            {
                strMsg = objServiceHandler.GetPkgSMSBroadcastMsgOnlyActive(strSMSContent, rblPkgAccType.SelectedValue.ToString(), ddlPkgList.SelectedValue.ToString());
            }
            else
            {
                strMsg = objServiceHandler.GetPkgSMSBroadcastMsg(strSMSContent, rblPkgAccType.SelectedValue.ToString(), ddlPkgList.SelectedValue.ToString());
            }
            lblMsg.Text = strMsg;
            SaveAuditInfo("Insert", "Package SMS Option");
            txtPkgSMSContent.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.ToString();
        }
    }
}
