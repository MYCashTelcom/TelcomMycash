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

public partial class Forms_frmManageAccntPIN : System.Web.UI.Page
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
    protected void btnResetPIN_Click(object sender, EventArgs e)
    {
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
        DataSet dtsAccDetail = objServiceHandler.GetAccountDetail(txtWallet.Text);
        if (dtsAccDetail.Tables["ACCOUNT_LIST"].Rows.Count > 0)
        {
            DataRow dRow = dtsAccDetail.Tables["ACCOUNT_LIST"].Rows[0];
            strAccountID = dRow["ACCNT_ID"].ToString();
            strMSISDN = dRow["ACCNT_MSISDN"].ToString();
            //strReturn = objIVR_DB.Update_Caller_PIN(txtWallet.Text.Substring(0,txtWallet.Text.Length-1), strPIN1);
            //if (strReturn.Equals(""))
            //{
                strReturn = objServiceHandler.ResetPIN(strAccountID, strMSISDN, "", strIP, strHostName + " [" + System.Environment.UserName + "]", strPIN1);
                lblMessage.Text = "PIN has been reset successfully";
                dtvClient.Visible = false;
                btnResetPIN.Visible = false;
                txtWallet.Text = "";
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
        
        
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnForwardPIN_Click(object sender, EventArgs e)
    {

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
		dtvManagePIN2.Visible = false;
        btnSendPIN2.Visible = false;
		
        lblMessage.Text = "";
        sdsClientList.SelectCommand = " SELECT DISTINCT C.CLINT_NAME,A.ACCNT_NO,A.ACCNT_PIN,A.ACCNT_MSISDN,SP.SERVICE_PKG_NAME, AR.RANK_TITEL, C.CLINT_FATHER_NAME, C.CLINT_MOTHER_NAME "
                                    + ",C.CLIENT_DOB,C.OCCUPATION,C.WORK_EDU_BUSINESS,C.PUR_OF_TRAN, C.CLIENT_OFFIC_ADDRESS, C.CLINT_ADDRESS1,C.CLINT_ADDRESS2, BA.BANK_BR_NAME,BA.BANK_ACCNT_NO"
                                    + ",IDS.IDNTIFCTION_NAME,CI.CLINT_IDENT_NAME,INF.INTRODCR_NAME,INF.INTRODCR_MOBILE,INF.INTRODCR_ADDRESS,INF.INTRODCR_OCCUPATION,"
                                    + " NI.NOMNE_NAME,NI.NOMNE_MOBILE,NI.RELATION,NI.PERCENTAGE FROM CLIENT_LIST C,ACCOUNT_LIST A,"
                                    + " ACCOUNT_RANK AR,SERVICE_PACKAGE SP,BANK_ACCOUNT BA,CLIENT_IDENTIFICATION CI,IDENTIFICATION_SETUP IDS,INTRODUCER_INFO INF "
                                    + " ,NOMINEE_INFO NI WHERE A.CLINT_ID=C.CLINT_ID AND A.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND SP.SERVICE_PKG_ID=A.SERVICE_PKG_ID AND C.CLINT_ID=BA.CLIENT_ID(+) "
                                    + "  AND C.CLINT_ID=CI.CLIENT_ID(+) AND IDS.IDNTIFCTION_ID(+)=CI.IDNTIFCTION_ID  AND C.CLINT_ID=INF.CLIENT_ID(+) AND C.CLINT_ID=NI.CLIENT_ID(+) AND A.ACCNT_NO='" + txtWallet.Text.ToString() + "'";



        //sdsClientList.SelectCommand = "SELECT CLINT_NAME, CLINT_ADDRESS1, CLINT_ADDRESS2, CLINT_FATHER_NAME, CLINT_MOTHER_NAME, CLIENT_DOB, "
        //                            + "CLIENT_OFFIC_ADDRESS, CLINT_N_ID, CLINT_PASSPORT_NO FROM CLIENT_LIST C, ACCOUNT_LIST A WHERE A.CLINT_ID=C.CLINT_ID "
        //                            + "AND A.ACCNT_NO LIKE '"+  txtWallet.Text +"%'";
        sdsClientList.DataBind();
        dtvClient.DataBind();
        if (dtvClient.Rows.Count > 0)
        {
            dtvClient.Visible = true;
            btnResetPIN.Visible = true;
        }
        else
        {
            lblMessage.Text = "Information Not Found.";
            btnResetPIN.Visible = false;
            dtvClient.Visible = false;
        }
    }
	
	protected void btnManagePin2_Click(object sender, EventArgs e)
    {
        btnResetPIN.Visible = false;
        dtvClient.Visible = false;

        lblMessage.Text = "";
        string strSql = "";

        strSql = " SELECT CL.CLINT_NAME, AL.ACCNT_NO, AL.ACCNT_STATE, AR.RANK_TITEL FROM ACCOUNT_LIST AL, CLIENT_LIST CL,"
                + " ACCOUNT_RANK AR WHERE AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID"
                + " AND AL.ACCNT_RANK_ID IN ('120519000000000003','180128000000000006','180305000000000003') AND AL.ACCNT_NO='" + txtWallet.Text.ToString() + "' ";

        DataSet oDs = new DataSet();
        oDs = objServiceHandler.ExecuteQuery(strSql);

        dtvManagePIN2.DataSource = oDs;
        dtvManagePIN2.DataBind();

        if (oDs.Tables[0].Rows.Count > 0)
        {
            dtvManagePIN2.Visible = true;
            btnSendPIN2.Visible = true;
        }
        else
        {
            lblMessage.Text = "Please input correct distributor wallet Id";
            dtvManagePIN2.Visible = false;
            btnSendPIN2.Visible = false;
        }
    }
    protected void btnSendPIN2_Click(object sender, EventArgs e)
    {
        btnResetPIN.Visible = false;
        dtvClient.Visible = false;

        lblMessage.Text = "";
        string strSql = "";

        strSql = " SELECT ACCNT_PIN2, ACCNT_MSISDN FROM ACCOUNT_LIST WHERE ACCNT_RANK_ID IN ('120519000000000003','180128000000000006','180305000000000003') AND ACCNT_NO='" + txtWallet.Text.ToString() + "' ";

        DataSet oDs = new DataSet();
        oDs = objServiceHandler.ExecuteQuery(strSql);

        if (oDs.Tables[0].Rows.Count > 0)
        {
            string accountPIN2 = oDs.Tables[0].Rows[0]["ACCNT_PIN2"].ToString();
            string accountMSISDN = oDs.Tables[0].Rows[0]["ACCNT_MSISDN"].ToString();

            string message = "*TXTFS*Your account PIN 2 is : "+accountPIN2+" Thanks MYCash#";

            string pinSend = objServiceHandler.ResendAccountPIN2(message, accountMSISDN);

            if (pinSend.Equals("Success"))
            {
                lblMessage.Text = "Account PIN2 has been sent";
                txtWallet.Text = "";
                dtvManagePIN2.Visible = false;
                btnSendPIN2.Visible = false;
            }
            else
            {
                lblMessage.Text = "Unsuccessful. Please contact with administrator";
            }
        }
        else
        {
            lblMessage.Text = "Account has problem. Please contact with administrator";
        }
    }
}
