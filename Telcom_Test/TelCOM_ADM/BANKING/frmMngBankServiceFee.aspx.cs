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

public partial class BANKING_frmMngBankServiceFee : System.Web.UI.Page
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
                ddlBranch.SelectedValue = Session["Branch_ID"].ToString();
                ddlBranch.DataBind();
                if (Session["Branch_Type"].Equals("A"))
                {
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlBranch.Enabled = false;
                }                
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
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
    protected void dtvServiceFee_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        sdsServiceFee.InsertParameters["SERVICE_ID"].DefaultValue = ddlService.SelectedValue.ToString();
        sdsServiceFee.InsertParameters["ACCNT_RANK_ID"].DefaultValue = ddlAccountRank.SelectedValue.ToString();
    }
    protected void gdvServiceFee_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        sdsServiceFee.UpdateParameters["SERVICE_ID"].DefaultValue = ddlService.SelectedValue.ToString();
        sdsServiceFee.UpdateParameters["ACCNT_RANK_ID"].DefaultValue = ddlAccountRank.SelectedValue.ToString();
    }

    protected void dtvServiceFee_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Insert", "Manage Service Fee");
    }
    protected void gdvServiceFee_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SaveAuditInfo("Delete", "Manage Service Fee");
    }
    protected void gdvServiceFee_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Manage Service Fee");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    //protected void ddlAccountRank_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string strSql = "";
    //    strSql = " SELECT * FROM BANK_SERVICE_FEE WHERE (SERVICE_ID=:SERVICE_ID) AND ACCNT_RANK_ID ='" + ddlAccountRank.SelectedValue.ToString() + "'";
    //    try
    //    {
    //        sdsServiceFee.SelectCommand = strSql;
    //        sdsServiceFee.DataBind();
    //        gdvServiceFee.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //}
}
