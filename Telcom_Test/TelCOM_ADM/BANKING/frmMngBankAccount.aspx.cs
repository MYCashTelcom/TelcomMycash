using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls.TableRow;

public partial class COMMON_frmMngBankAccount : System.Web.UI.Page  
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
   
    protected void gdvSearch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LoadSearchResult();
    }
    protected void gdvSearch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        LoadSearchResult();
        gdvSearch.SelectedIndex = e.NewEditIndex;
    }
    protected void gdvSearch_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        LoadSearchResult();
        string strWalletID = "", strFisWallet = "";        
        SaveAuditInfo("Update", "Manage CBS Account Mapping");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LoadSearchResult();        
    }
    public void LoadSearchResult()
    {
        lblMsg.Text = "";
        gdvSearch.Visible = false;
        string strSql = "", strAddSQL = "";

        if (txtWalletNo.Text == "")
        {
            lblMsg.Text = "Please insert wallet ID.";
            return;
        }
        else
        {
            strSql = " SELECT CBA.CLINT_BANK_ACC_ID,CL.CLINT_NAME, CBA.CLINT_BANK_ACC_NO,CBA.CLINT_BANK_ACC_LOGIN  "
                   + " FROM CLIENT_BANK_ACCOUNT CBA ,ACCOUNT_LIST AL,CLIENT_LIST CL WHERE AL.ACCNT_ID=CBA.ACCNT_ID "
                   + " AND CL.CLINT_ID=AL.CLINT_ID AND CBA.CLINT_BANK_ACC_NO='" + txtWalletNo.Text.ToString() + "'";
        }
        try
        {
            sdsClientBankAccnt.SelectCommand = strSql;            
            sdsClientBankAccnt.DataBind();
            gdvSearch.DataBind();
            if (gdvSearch.Rows.Count > 0)
            {
                gdvSearch.Visible = true;
                lblMsg.Text = "";
            }
            else
            {
                gdvSearch.Visible = false;
                lblMsg.Text = "Sorry, no data found.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        LoadSearchResult();        
    }
    protected void gdvSearch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strWalletID = "", strFisWallet = "";

        GridViewRow row = (GridViewRow)gdvSearch.Rows[e.RowIndex];
        //DropDownList ddlSrvcPkg = (DropDownList)row.FindControl("ddlEIPackage");
        //strSrvcPkg = ddlSrvcPkg.SelectedItem.ToString();

        Label lblWalletID = (Label)row.FindControl("Label2");
        strWalletID = lblWalletID.Text.ToString();

        TextBox txtFisWallet = (TextBox)row.FindControl("TextBox1");
        strFisWallet = txtFisWallet.Text.ToString();
        SaveAuditInfo("Update", "Manage CBS Account Mapping,Client Bank Acc No=" + strWalletID + ",FisWallet=" + strFisWallet);

    }
}
