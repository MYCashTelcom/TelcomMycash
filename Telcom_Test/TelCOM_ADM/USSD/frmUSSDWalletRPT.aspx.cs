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

public partial class USSD_frmUSSDWalletRPT : System.Web.UI.Page
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
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        string strSql = ""; 
        string strDateRange = "";   
        if (rbtnAllDateRange.SelectedValue == "0")
        {
            strSql = "SELECT DISTINCT CL.CLINT_NAME WALLET_NAME,AL.ACCNT_NO WALLET_ID,SR.REQUEST_PARTY MOBILE_NO,AR.RANK_TITEL,"
                  + " CL.CREATION_DATE FROM SERVICE_REQUEST SR ,SERVICE_RESPONSE RSP, ACCOUNT_LIST AL,ACCOUNT_RANK AR,"
                  + " CLIENT_LIST CL WHERE SR.REQUEST_ID = RSP.REQUEST_ID AND SR.RECEIPENT_PARTY='AIRTEL_USSD' "
                  + " AND SR.REQUEST_PARTY=AL.ACCNT_MSISDN AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND "
                  + " AL.CLINT_ID=CL.CLINT_ID AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2))='REG' "
                  + " ORDER BY AR.RANK_TITEL,CL.CREATION_DATE DESC ";
 
        }
        else if (rbtnAllDateRange.SelectedValue == "1")
        {          
            strSql = "SELECT DISTINCT CL.CLINT_NAME WALLET_NAME,AL.ACCNT_NO WALLET_ID,SR.REQUEST_PARTY MOBILE_NO,AR.RANK_TITEL,"
                   + " CL.CREATION_DATE FROM SERVICE_REQUEST SR ,SERVICE_RESPONSE RSP, ACCOUNT_LIST AL,ACCOUNT_RANK AR,"
                   + " CLIENT_LIST CL WHERE SR.REQUEST_ID = RSP.REQUEST_ID AND SR.RECEIPENT_PARTY='AIRTEL_USSD' AND "
                   + " SR.REQUEST_PARTY=AL.ACCNT_MSISDN AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.CLINT_ID=CL.CLINT_ID "
                   + " AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2))='REG' AND "
                   + " TO_CHAR(TO_DATE(CL.CREATION_DATE,'DD/MM/YYYY')) BETWEEN TO_DATE ('" + dptFromDate.DateString 
                   + "') AND TO_DATE ('" + dtpToDate.DateString + "')  ORDER BY AR.RANK_TITEL,CL.CREATION_DATE DESC ";
        }

        try
        {
            SaveAuditInfo("View", "USSD Wallet Reports");
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../USSD/frmUSSDWalletRPT.aspx";
            Session["ReportFile"] = "../USSD/crptUSSDWallet.rpt";           
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    private void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
