using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class System_SYS_System_Users : System.Web.UI.Page
{   
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    private static string strAccnt_ID = string.Empty;
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
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void dtvSysUsr_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        SaveAuditInfo("Insertion","Create System User");
        //----------------------------- Inserting account ID -----------------------------
        //clsServiceHandler objServiceHandler = new clsServiceHandler();
        //string strAccountID = "";
        //TextBox dtvAccountID = (TextBox)dtvSysUsr.FindControl("dtvAccntID");
        //strAccountID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", dtvAccountID.Text.ToString());
        //if (strAccountID == "")
        //{
        //    lblMsg.Text = "Insert Correct Account Wallet.";
        //}
        //else
        //{
        //    e.Values["ACCNT_ID"] = strAccountID;
        //}
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        string strAccountID = "";
        strAccountID=objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO",txtAccountNo.Text.Trim().ToString());
        if (strAccountID != "")
        {
            lblAccountID.Text = "Your Account ID is " + strAccountID + "";
        }
        else
        {
            lblAccountID.Text = "Invalid Account Wallet.";
        }
    }
}
