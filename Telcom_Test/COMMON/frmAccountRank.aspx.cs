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

public partial class COMMON_frmAccountRank : System.Web.UI.Page
{
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
    protected void dtvRank_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Insert", "Account Rank");
    }
    protected void gdvAccntRank_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Account Rank");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void dtvRank_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)dtvRank.FindControl("ddlStatus");
        e.Values["STATUS"] = ddlStatus.SelectedValue.ToString();
    }
    protected void gdvAccntRank_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)gdvAccntRank.Rows[e.RowIndex].FindControl("ddlStatus");
        e.NewValues["STATUS"] = ddlStatus.SelectedValue.ToString();
    }
}
