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

public partial class frmDPDC_Pos : System.Web.UI.Page
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
                //ddlBranch.SelectedValue = Session["Branch_ID"].ToString();
                //if (Session["Branch_Type"].Equals("A"))
                //{
                //    ddlBranch.Enabled = true;
                //}
                //else
                //{
                //    ddlBranch.Enabled = false;
                //}
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
        // Start - Check Active User Session
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
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        catch
        {
            Session.Clear();
            Response.Redirect("../frmSeesionExpMesage.aspx");
        }
        // End - Check Active User Session
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvChannelType_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Cash Account Type");
    }
   
    protected void dlvPOS_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Save", "Cash Account Type");
    }
    protected void gdvCashAccType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //DropDownList ddlAccntID = (DropDownList)gdvCashAccType.Rows[e.RowIndex].FindControl("ddlChartOfAccount");
        //e.NewValues["ACC_INT_ID"] = ddlAccntID.SelectedValue.ToString();
    }
    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string strSQL = "";
    //        strSQL = "SELECT * FROM CAS_ACCOUNT_TYPE WHERE (CMP_BRANCH_ID = :CMP_BRANCH_ID) ORDER BY CAS_ACC_TYPE_NAME ASC ";
    //        sdsCashAccntType.SelectCommand = strSQL;
    //        sdsCashAccntType.DataBind();
    //        gdvCashAccType.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //}
}