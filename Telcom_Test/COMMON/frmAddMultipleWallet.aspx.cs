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

public partial class COMMON_frmAddMultipleWallet : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private string strLoginName = string.Empty;
    private string strPassword = string.Empty;
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
                LoadBankCode();
            }
            catch
            {
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        try
        {
            strLoginName = Session["UserLoginName"].ToString();
            strPassword = Session["Password"].ToString();
        }
        catch
        {
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

    private void LoadBankCode()
    {
        string strBankCode = "";
        clsServiceHandler objSerHndlr = new clsServiceHandler();
        //############################ Getting Bank Code for login user #################
        strBankCode = objSerHndlr.GettingBankCode(Session["UserLoginName"].ToString());
        txtBankCode.Text = strBankCode;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string strSQL = "", strAccntMSISDN = "", strTwoWallet = "", strChkTwoWallet = "", strBankCode = "", strMsg = "";
        clsServiceHandler objSerHndlr = new clsServiceHandler();
        try
        {
            //############################ checking Mobile number #############################
            strAccntMSISDN = objSerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_MSISDN", "ACCNT_MSISDN", txtMobileNo.Text.ToString());
            if (strAccntMSISDN != "")
            {
                //######################## checking duplicate Two account ####################

                strTwoWallet = (txtMobileNo.Text.ToString()).Substring(3, 11) ;

                strChkTwoWallet = objSerHndlr.CheckDuplicateTwoAccount(strTwoWallet+ txtDigit.Text.ToString());
                if (strChkTwoWallet == "")
                {                    
                    strMsg = objSerHndlr.OpenWalletTwo(txtMobileNo.Text, txtBankCode.Text.ToString(), txtDigit.Text.ToString());
                    //ADD_NEW_WALLET('+8801711539963','MBL','2');
                    SaveAuditInfo("Insert", "PR_Month_Creation");
                    lblMsg.Text = strMsg;
                    ClearControl();
                    //################################################################################
                }
                else
                {
                    lblMsg.Text = "Two acccount already created.";
                }
                //###############################################################################
            }
            else
            {
                lblMsg.Text = "This mobile is not registered.";
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
           // objCashAccnt = null;
        }
    }

    private void ClearControl()
    {
        txtMobileNo.Text = "";
        txtDigit.Text = "";
    }
   
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

}
