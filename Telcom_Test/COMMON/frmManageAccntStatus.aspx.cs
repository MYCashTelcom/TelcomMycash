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
using System.Net;

public partial class Forms_frmManageAccntStatus : System.Web.UI.Page
{
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
                LoadSearchResult();
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
            lblMessage.Text = "";
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
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    

    protected void gdvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
		gdvSearch.PageIndex = e.NewPageIndex;
        LoadSearchResult();
    }
    public void LoadSearchResult()
    {
        string strChecking = "";
        string strSubQuery = "";

        lblMessage.Text = "";
        string strSql = "", strAddSQL = "";

        if (!txtWalletNumber.Text.Trim().Equals(""))
        {
            strSubQuery = " AND ACCNT_NO = '" + txtWalletNumber.Text.Trim() +"'";
        }
         
        string strHitCount = objServiceHandler.ReturnString("SELECT DISTINCT AUTH_TRY_COUNT_MAX FROM CM_SYSTEM_INFO");
         
        strSql = " SELECT AL.ACCNT_NO, DECODE(AL.ACCNT_STATE,'L','Locked','A','Active','Idle') ACCNT_STATE, CL.CLINT_NAME, CL.CLINT_ADDRESS1, AL.ACCNT_LOCKED_TIME "
        + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND ACCNT_STATE = 'L' AND INVALID_LOGIN_ATTEMPT_COUNT = '" + strHitCount + "'" + strSubQuery;

        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

        try
        {
            gdvSearch.DataSource = oDs;
            gdvSearch.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = " You are not authorized to see this account.";
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        string strAccountno = gvr.Cells[0].Text;

        string strMSISDN;
        string strAccountID;
        string strHostName = Dns.GetHostName();
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] IPaddr = ipEntry.AddressList;
        string strIP = "";
        // clsSyncIVR_DB objIVR_DB = new clsSyncIVR_DB();
        Random RandNum = new Random();
        string strPIN1 = String.Format("{0:0000}", RandNum.Next(1111, 9999).ToString());
        string strReturn;

        lblMessage.Text = "";
        foreach (IPAddress IP in IPaddr)
        {
            string strAddressFamily = IP.AddressFamily.ToString();
            if (strAddressFamily.Equals("InterNetwork"))
            {
                strIP = IP.ToString();
                break;
            }
        }
        DataSet dtsAccDetail = objServiceHandler.GetAccountDetail(strAccountno);
        if (dtsAccDetail.Tables["ACCOUNT_LIST"].Rows.Count > 0)
        {
            DataRow dRow = dtsAccDetail.Tables["ACCOUNT_LIST"].Rows[0];
            strAccountID = dRow["ACCNT_ID"].ToString();
            strMSISDN = dRow["ACCNT_MSISDN"].ToString();
            //strReturn = objIVR_DB.Update_Caller_PIN(txtWallet.Text.Substring(0,txtWallet.Text.Length-1), strPIN1);
            //if (strReturn.Equals(""))
            //{
            strReturn = objServiceHandler.ResetAccountStatus(strAccountID, strMSISDN, "", strIP, strHostName + " [" + System.Environment.UserName + "]", strPIN1);
            lblMessage.Text = "PIN has been reset successfully";
            // dtvClient.Visible = false;
           // btnResetPIN.Visible = false;
           // txtWallet.Text = "";
              SaveAuditInfo("Update", "PIN reset of " + strMSISDN + " by " + Session["UserDname"].ToString());
            //}
            //else
            //{
            //    lblMessage.Text = strReturn;
            //}
        }
        else
        {
            lblMessage.Text = "Insert correct wallet ID";
        }
        LoadSearchResult();

    }
    //protected void btnResetPIN_Click(object sender, EventArgs e)
    //{
    //    string strMSISDN;
    //    string strAccountID;
    //    string strHostName = Dns.GetHostName();
    //    IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
    //    IPAddress[] IPaddr = ipEntry.AddressList;
    //    string strIP = "";
    //   // clsSyncIVR_DB objIVR_DB = new clsSyncIVR_DB();
    //    Random RandNum = new Random();
    //    string strPIN1 = String.Format("{0:0000}", RandNum.Next(1111, 9999).ToString());
    //    string strReturn;
       
    //    lblMessage.Text = "";
    //    foreach (IPAddress IP in IPaddr)
    //    {
    //        string strAddressFamily = IP.AddressFamily.ToString();
    //        if (strAddressFamily.Equals("InterNetwork"))
    //        {
    //            strIP = IP.ToString();
    //            break;
    //        }
    //    }
    //    DataSet dtsAccDetail = objServiceHandler.GetAccountDetail(txtWallet.Text);
    //    if (dtsAccDetail.Tables["ACCOUNT_LIST"].Rows.Count > 0)
    //    {
    //        DataRow dRow = dtsAccDetail.Tables["ACCOUNT_LIST"].Rows[0];
    //        strAccountID = dRow["ACCNT_ID"].ToString();
    //        strMSISDN = dRow["ACCNT_MSISDN"].ToString();
    //        //strReturn = objIVR_DB.Update_Caller_PIN(txtWallet.Text.Substring(0,txtWallet.Text.Length-1), strPIN1);
    //        //if (strReturn.Equals(""))
    //        //{
    //            strReturn = objServiceHandler.ResetPIN(strAccountID, strMSISDN, "", strIP, strHostName + " [" + System.Environment.UserName + "]", strPIN1);
    //            lblMessage.Text = "PIN has been reset successfully";
    //           // dtvClient.Visible = false;
    //            btnResetPIN.Visible = false;
    //            txtWallet.Text = "";
    //          //  SaveAuditInfo("Update", "PIN reset of " + strMSISDN + " by " + Session["UserDname"].ToString());
    //        //}
    //        //else
    //        //{
    //        //    lblMessage.Text = strReturn;
    //        //}
    //    }
    //    else
    //    {
    //        lblMessage.Text = "Insert correct wallet ID";
    //    }
        
        
    //}
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }



    protected void btnSearchByWallet_Click(object sender, EventArgs e)
    {
        LoadSearchResult();
    }
}
