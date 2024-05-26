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

public partial class Forms_frmManageKYCVerification : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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
        lblMessage.Text = "";
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
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnResetPIN_Click(object sender, EventArgs e)
    {
        if (dtvClient.Rows.Count > 0)
        {
            string strMSISDN, strProMsg = "", strRequistID="";
            string strAccountID;
            string strToken;
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] IPaddr = ipEntry.AddressList;
            string strIP = "";

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
            //DataSet dtsAccDetail = objServiceHandler.GetAccountDetail(ddlWallet.Text.Substring(3)+"1");

            DataSet dtsAccDetail = objServiceHandler.GetAccountDetailKYC(ddlWallet.Text);
            if (dtsAccDetail.Tables["ACCOUNT_LIST_ONLY_NEW"].Rows.Count > 0)
            {
                DataRow dRow = dtsAccDetail.Tables["ACCOUNT_LIST_ONLY_NEW"].Rows[0];
                strAccountID = dRow["ACCNT_ID"].ToString();
                strToken = dRow["ACCCT_AC_TOKEN"].ToString();
                //string strReturn = objServiceHandler.ResetPIN(strAccountID, strMSISDN, "", strIP, strHostName + " [" + System.Environment.UserName + "]");
                string strReturn = objServiceHandler.KYCVerifyed(strAccountID, strToken, ddlWallet.SelectedValue.ToString(), "", strIP, strHostName + " [" + System.Environment.UserName + "]");
                SaveAuditInfo("Verify", "KYC Verification");

                //--------------- Update Client List table for verification -----------------------
                string strUpdateDate = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", DateTime.Now);
                objServiceHandler.UpdateClientListForVerification(Session["AccountID"].ToString(), strUpdateDate, ddlWallet.SelectedValue.ToString());
                //---------------------------------------------------------------------------------
                //-------------- Checking Requist ID for commission---------------------
                strRequistID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_SERIAL_DETAIL", "REQUEST_ID", "CUSTOMER_MOBILE_NO", ddlWallet.SelectedValue.ToString());
                if (strRequistID != "")
                {
                    strProMsg = objServiceHandler.ExecAccRGComm(ddlWallet.SelectedValue.ToString(), strAccountID, strRequistID);
                }
                //------------------------ Execute Commission Procedure for Distributor--------------------------
                string strSQLProcedure = "";
                //strSQLProcedure = "APSNG101.PKG_MB_NATIVE_TRANSACTION.PRO_DISTRIBUTOR_REG_COMI('" + ddlWallet.SelectedValue.ToString() + "')";
                //string strMsg12 = objServiceHandler.ExecuteProcedure(strSQLProcedure);
				
				if (txtAccountNo.Text.Trim().Length == 12)
				{
					objServiceHandler.GenerateQrCodeManually(txtAccountNo.Text.Trim());
				}
				
                lblMessage.Text = "Verifyed successfully.";
                dtvClient.Visible = false;
                btnResetPIN.Visible = false;
            }
            else
            {
                lblMessage.Text = "Insert correct wallet ID";
            }
            txtAccountNo.Text = "";
        }
        else
        {
            lblMessage.Text = "Please Select A Wallet ID";
            dtvClient.Visible = false;
            btnResetPIN.Visible = false;
        }
    }
    protected void btnForwardPIN_Click(object sender, EventArgs e)
    {

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        //MODIFIED BY KOWSHIK START(02-08-2012)  
        lblMessage.Text = "";
        string strAccState = "", strAccUpState = "", strAccountID = "";
        string strMSISDN = txtAccountNo.Text.Trim();
        string strMessage = objServiceHandler.ShowAccountVerificationInformation(strMSISDN);
        if (strMessage == "")
        {
            strAccountID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", txtAccountNo.Text.Trim());
            strAccState = objServiceHandler.ShowAccountVerificationState(strMSISDN);
            strAccUpState = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST_ONLY_NEW", "ACCNT_UPLOAD_STATE", "ACCNT_ID", strAccountID);
            if (strAccState == "A" && strAccUpState == "V")
            {
                lblMessage.Text = "This Account is already verified.";
            }
            else if (strAccState == "I" && strAccUpState == "V")
            {
                lblMessage.Text = "This number is already registered.Please change the status from Manage wallet.";
            }
            else
            {
                ddlWallet.ClearSelection();
                try
                {
                    string strMobileNo = "+88" + (txtAccountNo.Text.Trim()).Substring(0, 11);
                    ddlWallet.Items.FindByText(strMobileNo).Selected = true;
                }
                catch (Exception exx)
                {
                    lblMessage.Text = "This Account is not in the List";
                }
            }
        }
        else
        {
            lblMessage.Text = "This Account is not Registered.";
        }
        //MODIFIED BY KOWSHIK END(02-08-2012)
    }
    override protected void OnInit(EventArgs e)
    {
        btnView.Attributes.Add("onclick", "javascript:" +
                  btnView.ClientID + ".disabled=true;" +
                  this.GetPostBackEventReference(btnView));
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {
        this.btnView.Click +=
                new System.EventHandler(this.btnView_Click);
        this.Load += new System.EventHandler(this.Page_Load);
    }
    protected void btnViewDetailsView_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string strSQL = "";

        strSQL = " SELECT * FROM VW_KYC_VERIFICATION WHERE ACCNT_MSISDN='" + ddlWallet.SelectedValue.ToString() + "'";
        //strSQL= " SELECT C.CLINT_NAME,A.ACCNT_NO,A.ACCNT_PIN,A.ACCNT_MSISDN,SP.SERVICE_PKG_NAME, AR.RANK_TITEL, C.CLINT_FATHER_NAME, C.CLINT_MOTHER_NAME "
        //                        + ",C.CLIENT_DOB,C.OCCUPATION,C.WORK_EDU_BUSINESS,C.PUR_OF_TRAN, C.CLIENT_OFFIC_ADDRESS, C.CLINT_ADDRESS1,C.CLINT_ADDRESS2, BA.BANK_BR_NAME,BA.BANK_ACCNT_NO"
        //                        + ",IDS.IDNTIFCTION_NAME,CI.CLINT_IDENT_NAME,INF.INTRODCR_NAME,INF.INTRODCR_MOBILE,INF.INTRODCR_ADDRESS,INF.INTRODCR_OCCUPATION,"
        //                        + " NI.NOMNE_NAME,NI.NOMNE_MOBILE,NI.RELATION,NI.PERCENTAGE FROM CLIENT_LIST C,ACCOUNT_LIST A,"
        //                        + " ACCOUNT_RANK AR,SERVICE_PACKAGE SP,BANK_ACCOUNT BA,CLIENT_IDENTIFICATION CI,IDENTIFICATION_SETUP IDS,INTRODUCER_INFO INF "
        //                        + " ,NOMINEE_INFO NI WHERE A.CLINT_ID=C.CLINT_ID AND A.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND SP.SERVICE_PKG_ID=A.SERVICE_PKG_ID AND C.CLINT_ID=BA.CLIENT_ID(+) "
        //                        + "  AND C.CLINT_ID=CI.CLIENT_ID(+) AND IDS.IDNTIFCTION_ID(+)=CI.IDNTIFCTION_ID  AND C.CLINT_ID=INF.CLIENT_ID(+) AND C.CLINT_ID=NI.CLIENT_ID(+) AND A.ACCNT_MSISDN='" + ddlWallet.SelectedValue.ToString() + "'";
        try
        {
            sdsClientList.SelectCommand = strSQL;
            sdsClientList.DataBind();
            dtvClient.DataBind();
            if (dtvClient.Rows.Count > 0)
            {
                dtvClient.Visible = true;
                btnResetPIN.Visible = true;
            }
            else
            {
                dtvClient.Visible = false;
                btnResetPIN.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }  
}
