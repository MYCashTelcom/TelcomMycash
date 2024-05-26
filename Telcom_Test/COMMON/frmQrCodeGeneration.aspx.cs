using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COMMON_frmQrCodeGeneration : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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
            catch (Exception ex)
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
                ex.Message.ToString();
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

    private void SaveAuditLog(string strOperationType, string strRemarks)
    {
        objSysAdmin.SetSeessionData(Session["Branch_ID"].ToString());
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    protected void btnGenerateQr_Click(object sender, EventArgs e)
    {
        string[] strWalletNumbers = txtWalletNumber.Text.Trim().Split(',');
        string walletNumber = "";

        for (int i = 0; i < strWalletNumbers.Length; i++)
        {
            string strWalletNumber = strWalletNumbers[i].Trim().ToString();

            if (strWalletNumber.Length == 12)
            {
                objServiceHandler.GenerateQrCodeManually(strWalletNumber);
                walletNumber = walletNumber + "'" + strWalletNumber + "',";
            }
        }

        walletNumber = walletNumber.Remove(walletNumber.Length - 1);

        string strSql = "SELECT AL.ACCNT_NO,APL.ACCOUNT_NAME,APL.CARD_NUMBER,APL.CLINT_BANK_ACC_NO FROM ACCOUNT_LIST AL, ACCOUNT_POS_LIST APL WHERE AL.ACCNT_ID = APL.ACCNT_ID AND AL.ACCNT_NO IN (" + walletNumber + ")";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
        gdvQrCodeDetail.DataSource = oDs;
        gdvQrCodeDetail.DataBind();

        //SaveAuditLog("GenerateQr", "Qr Code Generate Manually");
        ClearControl();
    }

    private void ClearControl()
    {
        txtWalletNumber.Text = "";
    }
}