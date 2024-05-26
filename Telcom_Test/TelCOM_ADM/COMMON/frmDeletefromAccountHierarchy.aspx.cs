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

public partial class COMMON_frmReplaceMobileNo : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objservicerHndlr = new clsServiceHandler();
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
        lblMsg.Text = "";
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
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        //############ delete account id from account hierarchy table ################
        lblMsg.Text = "";
        string strAccID="",strAccHirID="",strAccIDExists="",strMsg="",strAccRankID="",strShortCode="";
        strAccID = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID",
                                                                    "ACCNT_NO", txtWalletID.Text.Trim());
        if (strAccID != "")
        {           

            strAccRankID = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_RANK_ID",
                                                                    "ACCNT_NO", txtWalletID.Text.Trim());

            strShortCode = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_RANK", "SHORT_CODE",
                                                                    "ACCNT_RANK_ID", strAccRankID);
            if (strShortCode == "C")
            {
                strAccIDExists = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_HIERARCHY", "ACCNT_ID",
                                                                        "ACCNT_ID", strAccID);
                if (strAccIDExists != "")
                {
                    strAccHirID = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_HIERARCHY", "HIERARCHY_ACCNT_ID",
                                                                       "HIERARCHY_ACCNT_ID", strAccID);
                    if (strAccHirID == "")
                    {
                        strMsg = objservicerHndlr.DeleteAccountHierarchy(strAccID);
                        lblMsg.Text = strMsg;
                        SaveAuditInfo("Delete", "Account Hierarchy" + txtWalletID.Text.Trim());
                    }
                    else
                    {
                        lblMsg.Text = "It has child account in account Hierarchy.";
                    }
                }
                else
                {
                    lblMsg.Text = "This acount is not added in account Hierarchy.";
                }

            }
            else
            {
                lblMsg.Text = "This account is not customer.";
            }            
        }
        else
        {
            lblMsg.Text = "This account is not registered.";
        }

      //#######################################################################
    }
    private void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
