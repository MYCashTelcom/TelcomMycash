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
    DataSet dsData;
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
        //############ delete account from table ################
        lblMsg.Text = "";
        string strAccount = "", strMsg = "", strSQL = "";
        strAccount = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("CLIENT_LIST", "CLINT_MOBILE",
                                                                    "CLINT_MOBILE", txtMobileNo.Text.Trim());
        //######### account registration checking ###############
        if (strAccount != "")
        {
            strSQL = " SELECT * FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION"
                   + " CAT WHERE CAL.CAS_ACC_ID=CAT.CAS_ACC_ID AND CAL.CAS_ACC_NO= "
                   + " SUBSTR('" + txtMobileNo.Text.Trim() + "',4,11)||1 ";

            //########## account transaction checking ############
            dsData = objservicerHndlr.ReturnDataSet(strSQL);
            if (dsData.Tables["Table1"].Rows.Count > 0)
            {
                lblMsg.Text = "This account has transaction.It cannot be delete.";
            }
            else
            {
                strMsg = objservicerHndlr.DeleteSingleAccount(txtMobileNo.Text.ToString());
                lblMsg.Text = strMsg;
                SaveAuditInfo("Delete", " Account Delete=" + txtMobileNo.Text.ToString().Trim());
                
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
