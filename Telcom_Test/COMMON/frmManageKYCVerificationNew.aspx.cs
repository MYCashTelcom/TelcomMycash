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
using System.Net;

public partial class COMMON_frmPINInformation : System.Web.UI.Page
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string strAccState = "";
        string strMessage = objServiceHandler.ShowAccountVerificationInformation(txtAccountNo.Text.ToString());
        if (strMessage == "")
        {
            strAccState = objServiceHandler.ShowAccountVerificationState(txtAccountNo.Text.ToString());
            if (strAccState == "A")
            {
                lblMessage.Text = "This Account is already verified.";
            }
            else
            {
                ShowDataInGrid();
            }
        }
        else
        {
            lblMessage.Text = "This Account is not Registered.";
        }
    }

    private void ShowDataInGrid()
    {
        try
        {
            sdsClientList.SelectCommand = " SELECT DISTINCT CM.SERIAL_NO, MD.DISTRICT_NAME,MT.THANA_NAME,C.CLINT_NAME,C.HUSBAND_NAME,A.ACCNT_NO,A.ACCNT_PIN,A.ACCNT_MSISDN,SP.SERVICE_PKG_NAME, AR.RANK_TITEL, C.CLINT_FATHER_NAME, C.CLINT_MOTHER_NAME "
                                        + ",C.CLIENT_DOB,C.OCCUPATION,C.WORK_EDU_BUSINESS,C.PUR_OF_TRAN, C.CLIENT_OFFIC_ADDRESS, C.CLINT_ADDRESS1,C.CLINT_ADDRESS2, BA.BANK_BR_NAME,BA.BANK_ACCNT_NO"
                                        + ",IDS.IDNTIFCTION_NAME,CI.CLINT_IDENT_NAME,INF.INTRODCR_NAME,INF.INTRODCR_MOBILE,INF.INTRODCR_ADDRESS,INF.INTRODCR_OCCUPATION,"
                                        + " NI.NOMNE_NAME,NI.NOMNE_MOBILE,NI.RELATION,NI.PERCENTAGE FROM CLIENT_LIST C,ACCOUNT_LIST A,"
                                        + " ACCOUNT_RANK AR,SERVICE_PACKAGE SP,BANK_ACCOUNT BA,CLIENT_IDENTIFICATION CI,IDENTIFICATION_SETUP IDS,INTRODUCER_INFO INF "
                                        + " ,NOMINEE_INFO NI,MANAGE_THANA MT,MANAGE_DISTRICT MD,ACCOUNT_SERIAL_DETAIL CM WHERE A.CLINT_ID=C.CLINT_ID AND A.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND SP.SERVICE_PKG_ID=A.SERVICE_PKG_ID AND C.CLINT_ID=BA.CLIENT_ID(+) "
                                        + "  AND C.CLINT_ID=CI.CLIENT_ID(+) AND IDS.IDNTIFCTION_ID(+)=CI.IDNTIFCTION_ID  AND C.CLINT_ID=INF.CLIENT_ID(+) AND C.CLINT_ID=NI.CLIENT_ID(+) AND A.ACCNT_NO='"
                                        + txtAccountNo.Text.ToString() + "' AND C.THANA_ID=MT.THANA_ID(+) AND MD.DISTRICT_ID(+)=MT.DISTRICT_ID AND C.CLINT_MOBILE=CM.CUSTOMER_MOBILE_NO(+) ORDER BY TO_NUMBER(CM.SERIAL_NO) ASC";//AND CM.BANK_CODE='MBL'
            sdsClientList.DataBind();
            dtvClient.DataBind();
            if (dtvClient.Rows.Count > 0)
            {
                dtvClient.Visible = true;
                btnVerify.Visible = true;
            }
            else
            {
                dtvClient.Visible = false;
                btnVerify.Visible = false;
            }
            SaveAuditInfo("View", "KYC Verification New");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    //----------------------- save audit information --------------------
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    //override protected void OnInit(EventArgs e)
    //{
    //    btnVerify.Attributes.Add("onclick", "javascript:" +
    //              btnVerify.ClientID + ".disabled=true;" +
    //              this.GetPostBackEventReference(btnVerify));
    //   //InitializeComponent();
    //    base.OnInit(e);
    //}
    //private void InitializeComponent()
    //{
    //    this.btnVerify.Click +=
    //            new System.EventHandler(this.btnVerify_Click);
    //    this.Load += new System.EventHandler(this.Page_Load);
    //}
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string strFromSLNo = "";
        if (dtvClient.Rows.Count > 0 && txtAccountNo.Text != "")
        {
            if (Session["AccountID"].ToString() != "")
            {
                Label lblFormSLNo = (Label)dtvClient.FindControl("lblFormSLNo");
                strFromSLNo = lblFormSLNo.Text.ToString();
                if (Convert.ToDouble(strFromSLNo) < 200000000)
                {
                    string strMSISDN, strProMsg = "", strRequistID = "";
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
                    strMSISDN = "+88" + (txtAccountNo.Text.ToString()).Substring(0, 11);

                    DataSet dtsAccDetail = objServiceHandler.GetAccountDetailKYC(strMSISDN);
                    if (dtsAccDetail.Tables["ACCOUNT_LIST_ONLY_NEW"].Rows.Count > 0)
                    {
                        DataRow dRow = dtsAccDetail.Tables["ACCOUNT_LIST_ONLY_NEW"].Rows[0];
                        strAccountID = dRow["ACCNT_ID"].ToString();
                        strToken = dRow["ACCCT_AC_TOKEN"].ToString();
                        //-------------------   KYC Verified ------------------------------
                        string strReturn = objServiceHandler.KYCVerifyed(strAccountID, strToken, strMSISDN, "", strIP, strHostName + " [" + System.Environment.UserName + "]");
                        SaveAuditInfo("Verify", "KYC Verification,Verified_No=" + txtAccountNo.Text.ToString() + "");

                        //--------------- Update Client List table for verification -----------------------
                        string strUpdateDate = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", DateTime.Now);
                        string strUpdateMsg = objServiceHandler.UpdateClientListForVerification(Session["AccountID"].ToString(), strUpdateDate, strMSISDN);
                        //---------- checking requist id for commission --------------------------- 
                        // strRequistID = objServiceHandler.ReturnOneColumnValue(strMSISDN);
                        //strRequistID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_SERIAL_DETAIL", "REQUEST_ID", "CUSTOMER_MOBILE_NO", strMSISDN);
                        //if (strRequistID != "")
                        //{
                        //    //------------------ execute procedure -------------------
                        //    strProMsg = objServiceHandler.ExecAccRGComm(strMSISDN, strAccountID, strRequistID);
                        //}
                        //-------------------Account_List Status and Account_Serial_Detail verification Status--------- 
                        string strMsg = objServiceHandler.UpdateAccountList(strAccountID);
                        string strMsgStatus = objServiceHandler.UpdateVerifiedStatus(strMSISDN);
                        //------------------------ Execute Commission Procedure for Distributor--------------------------
                        //string strSQLProcedure = "";
                        //strSQLProcedure = "PKG_MB_NATIVE_TRANSACTION.PRO_DISTRIBUTOR_REG_COMI('" + strMSISDN.ToString() + "')";
                        //string strMsg12 = objServiceHandler.ExecuteProcedure(strSQLProcedure);
						
						if (txtAccountNo.Text.Trim().Length == 12)
                        {
                            objServiceHandler.GenerateQrCodeManually(txtAccountNo.Text.Trim());
                        }
						
                        txtAccountNo.Text = "";
                        dtvClient.Visible = false;
                        btnVerify.Visible = false;
                        lblMessage.Text = "Verifyed successfully.";
                    }
                    else
                    {
                        txtAccountNo.Text = "";
                        lblMessage.Text = "Insert correct wallet ID";
                    }
                }
                else
                {
                    dtvClient.Visible = false;
                    btnVerify.Visible = false;
                    lblMessage.Text = "Please Update this form serial no.";
                }
            }
            else
            {
                dtvClient.Visible = false;
                btnVerify.Visible = false;
                lblMessage.Text = "Please login again.";
            }
        }
        else
        {
            dtvClient.Visible = false;
            btnVerify.Visible = false;
            lblMessage.Text = "Please insert a wallet ID";
        }
    }
}
