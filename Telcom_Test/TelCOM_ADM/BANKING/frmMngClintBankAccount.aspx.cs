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

public partial class Forms_frmMngClintBankAcc : System.Web.UI.Page
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
        //sdsClientAccount.DataBind();
       // GetAcc_Syn();
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
    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        GetAcc_Syn();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

        
    }
    protected void ddlClAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAcc_Syn();
    }

    protected void GetAcc_Syn()
    {
        clsAccountHandler objACCSYN = new clsAccountHandler();
        DataSet dtsAppInfo = objACCSYN.GetAcc_Synonym(ddlClAccount.SelectedValue.ToString());

        foreach (DataRow pRow in dtsAppInfo.Tables["ACCOUNT_LIST"].Rows)
        {
           // txtAcc_Syn.Text = pRow["CLINT_MOBILE"].ToString().Trim() == "" ? "0" : pRow["CLINT_MOBILE"].ToString();
            txtAcc_Syn.Text = pRow["ACCNT_MSISDN"].ToString();
                            
        }
    }
    protected void btnAddClient_Click(object sender, EventArgs e)
    {
        string strReturn = "";
        clsAccountHandler objACCSYN = new clsAccountHandler();
        strReturn = objACCSYN.AddClient("",ddlClAccount.SelectedValue.ToString(),dlBank.SelectedValue.ToString(),txtAcc_Syn.Text,txtAccNo.Text,txtPawd.Text);
        lblMsg.Text = strReturn;
        SaveAuditInfo("Insert", "Client Bank Account New");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAccNo.Text = "";
        txtPawd.Text = "";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowGridViewData();        
    }

    private void ShowGridViewData()
    {
        string strSql = "";
        if (txtWalletID.Text.ToString() != "" && txtMSISDN.Text.ToString() == "")
        {
            strSql = " SELECT CBA.*,AL.ACCNT_MSISDN FROM CLIENT_BANK_ACCOUNT CBA,ACCOUNT_LIST AL WHERE CBA.CLINT_BANK_ACC_NO=AL.ACCNT_NO AND"
                         + " CBA.CLINT_BANK_ACC_NO='" + txtWalletID.Text.Trim() + "' ORDER BY CLINT_BANK_ACC_ID DESC";
        }
        else if (txtWalletID.Text.Trim() == "" && txtMSISDN.Text.Trim() != "")
        {
            strSql = " SELECT CBA.*,AL.ACCNT_MSISDN FROM CLIENT_BANK_ACCOUNT CBA,ACCOUNT_LIST AL WHERE CBA.CLINT_BANK_ACC_NO=AL.ACCNT_NO AND"
                                   + " AL.ACCNT_MSISDN='" + txtMSISDN.Text.Trim() + "' ORDER BY CLINT_BANK_ACC_ID DESC";
        }
        try
        {
            sdsCliBankAcc.SelectCommand = strSql;
            sdsCliBankAcc.DataBind();

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ShowGridViewData();
    }
   
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        ShowGridViewData();
        SaveAuditInfo("Update", "Client Bank Account New");
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ShowGridViewData();
    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SaveAuditInfo("Delete", "Client Bank Account New");
    }
}
