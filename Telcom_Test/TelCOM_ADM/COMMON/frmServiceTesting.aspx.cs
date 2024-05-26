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
using System.Drawing;

public partial class COMMON_frmServiceTesting : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();

                if (ddlServiceList.SelectedValue == "CN" || ddlServiceList.SelectedValue == "CCT" ||
                     ddlServiceList.SelectedValue == "SW" || ddlServiceList.SelectedValue == "FM" ||
                     ddlServiceList.SelectedValue == "CT")
                {
                    lblDPSAcc.Visible = false;
                    txtDPSAccNo.Visible = false;
                    lblOTPPin.Visible = false;
                    txtPinForOTP.Visible = false;
                    lblMP.Visible = false;
                    txtMPRef.Visible = false;

                    lblMessage.Text = "";
                }

                else if (ddlServiceList.SelectedValue == "MTP")
                {
                    lblDPSAcc.Visible = false;
                    txtDPSAccNo.Visible = false;
                    lblOTPPin.Visible = false;
                    txtPinForOTP.Visible = false;
                    lblMP.Visible = false;
                    txtMPRef.Visible = false;
                }


                else if (ddlServiceList.SelectedValue == "FT")
                {
                    lblDPSAcc.Visible = false;
                    txtDPSAccNo.Visible = false;
                    lblOldPIn.Visible = false;
                    txtPinForOTP.Visible = false;
                }

                else if (ddlServiceList.SelectedValue == "MP")
                {
                    lblDPSAcc.Visible = false;
                    txtDPSAccNo.Visible = false;
                    lblOTPPin.Visible = false;
                    txtPinForOTP.Visible = false;
                    lblMP.Visible = true;
                    txtMPRef.Visible = true;

                    lblMessage.Text = "";
                }

                else if (ddlServiceList.SelectedValue == "QT" || ddlServiceList.SelectedValue == "BI")
                {
                    Label3.Visible = false;
                    txtDestinationWallet.Visible = false;
                    Label4.Visible = false;
                    txtAmount.Visible = false;
                    lblDPSAcc.Visible = false;
                    txtDPSAccNo.Visible = false;
                    lblOTPPin.Visible = true;
                    txtPinForOTP.Visible = true;
                    lblMP.Visible = false;
                    txtMPRef.Visible = false;

                    lblMessage.Text = "";
                }

                else if (ddlServiceList.SelectedValue == "BD")
                {
                    lblDPSAcc.Visible = true;
                    txtDPSAccNo.Visible = true;
                    lblOTPPin.Visible = false;
                    txtPinForOTP.Visible = false;
                    Label3.Visible = true;
                    txtDestinationWallet.Visible = true;
                    lblMP.Visible = false;
                    txtMPRef.Visible = false;

                    lblMessage.Text = "";
                }

                else if (ddlServiceList.SelectedValue == "OTP")
                {
                    Label2.Visible = true;
                    txtSourceWallet.Visible = true;
                    Label3.Visible = false;
                    txtDestinationWallet.Visible = false;
                    lblOTPPin.Visible = true;
                    txtPinForOTP.Visible = true;
                    Label4.Visible = false;
                    txtAmount.Visible = false;
                    lblDPSAcc.Visible = false;
                    txtDPSAccNo.Visible = false;
                    lblMP.Visible = false;
                    txtMPRef.Visible = false;

                    lblMessage.Text = "";
                }

                else if (ddlServiceList.SelectedValue == "CPIN1" || ddlServiceList.SelectedValue == "CPIN2")
                {
                    Label2.Visible = true;
                    txtSourceWallet.Visible = true;
                    Label3.Visible = false;
                    txtDestinationWallet.Visible = false;
                    lblOldPIn.Visible = true;
                    txtOldPIN.Visible = true;
                    lblNewPIN.Visible = true;
                    txtNewPIN.Visible = true;

                    lblOTPPin.Visible = false;
                    txtPinForOTP.Visible = false;
                    Label4.Visible = false;
                    txtAmount.Visible = false;
                    lblDPSAcc.Visible = false;
                    txtDPSAccNo.Visible = false;
                    lblMP.Visible = false;
                    txtMPRef.Visible = false;

                    lblMessage.Text = "";
                    
                }

                else if (   ddlServiceListOTP.SelectedValue == "FT" || ddlServiceListOTP.SelectedValue == "FM" ||
                            ddlServiceListOTP.SelectedValue == "CN" || ddlServiceListOTP.SelectedValue == "CCT" ||
                            ddlServiceListOTP.SelectedValue == "SW" || ddlServiceListOTP.SelectedValue == "CT" )
                {
                    lblDpsAccNoOTP.Visible = false;
                    txtDpsAccOtp.Visible = false;

                    lblMessage.Text = "";
                }

                else if (ddlServiceListOTP.SelectedValue == "BD")
                {
                    lblDpsAccNoOTP.Visible = true;
                    txtDpsAccOtp.Visible = true;

                    lblMessage.Text = "";
                }

                else if (ddlServiceListOTP.SelectedValue == "MTP")
                {
                   lblMessage.Text = "";
                }

                else if (ddlServiceListOTP.SelectedValue == "MP")
                {
                    lblMpRef.Visible = true;
                    txtMpRefOtp.Visible = true;
                    lblDpsAccNoOTP.Visible = false;
                    txtDpsAccOtp.Visible = false;

                    lblMessage.Text = "";
                }

            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        //}
        
        //try
        //{
        //    strUserName = Session["UserLoginName"].ToString();
        //    strPassword = Session["Password"].ToString();
        //}
        //catch
        //{
        //    Session.Clear();
        //    Response.Redirect("../frmSeesionExpMesage.aspx");
           
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
        //}
    }

    #region for USSD

    protected void btnSave_Click(object sender, EventArgs e)
    {
        clsServiceHandler objSerHndler = new clsServiceHandler();
        string strAmount = "", strRequestparty = "", strRequestPartyN = "", strRecepentParty = "", strReqText = "", strRecepentNo, strRequestPartyWallet = "";

        try
        {
            
        #region for CN
            if (ddlServiceList.SelectedValue == "CN")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 12);
                strAmount = txtAmount.Text;
                strReqText = "*" + ddlServiceList.SelectedValue.ToString() + "*" + strRequestPartyWallet + "*" + strAmount + "*" + strRecepentNo + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }
        #endregion

        #region for FM
            else if (ddlServiceList.SelectedValue == "FM")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 12);
                strAmount = txtAmount.Text;
                strReqText = "*" + ddlServiceList.SelectedValue.ToString() + "*" + strRequestPartyWallet + "*" + strAmount + "*" + strRecepentNo + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }
        #endregion
        
        #region for CCT
            else if (ddlServiceList.SelectedValue == "CCT")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 12);
                strAmount = txtAmount.Text;
                strReqText = "*" + ddlServiceList.SelectedValue.ToString() + "*" + strRequestPartyWallet + "*" + strAmount + "*" + strRecepentNo + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }
        #endregion

        #region for CT(Agent Cash Out)

            else if (ddlServiceList.SelectedValue == "CT")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 12);
                strAmount = txtAmount.Text;
                strReqText = "*" + ddlServiceList.SelectedValue.ToString() + "*" + strRequestPartyWallet + "*" + strAmount + "*" + strRecepentNo + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }

        #endregion

        #region for SW

            else if (ddlServiceList.SelectedValue == "SW")
            {
                string strRequestPartyW = txtSourceWallet.Text;
                strRequestPartyWallet = strRequestPartyW.Substring(0, 11) + "2";
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 12);
                strAmount = txtAmount.Text;
                strReqText = "*" + ddlServiceList.SelectedValue.ToString() + "*" + strRequestPartyWallet + "*" + strAmount + "*" + strRecepentNo + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }
        #endregion

        #region for MP
            else if (ddlServiceList.SelectedValue == "MP")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 12);
                strAmount = txtAmount.Text;
                string strMPRefNo = txtMPRef.Text;
                strReqText = "*" + ddlServiceList.SelectedValue.ToString() + "*" + strRequestPartyWallet + "*" + strAmount + "*" + strRecepentNo + "*" + strMPRefNo + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }
        #endregion
        
        #region for FT
            else if (ddlServiceList.SelectedValue == "FT")
            {

                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 12);
                strReqText = "*" + ddlServiceList.SelectedValue.ToString() + "*" + strRequestPartyWallet + "*" + txtAmount.Text + "*" + strRecepentNo + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();

                //strRequestPartyWallet = txtSourceWallet.Text;
                //strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                //strRequestPartyN = strRequestparty.Substring(0, 14);
                //strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                //strRecepentNo = txtDestinationWallet.Text.Substring(0, 12);
                //strReqText = "*" + ddlServiceList.SelectedValue + "*" + strRequestPartyWallet + "*" + txtAmount.Text + "*" + strRecepentNo + "#";
                //objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                //SaveAuditInfo("Insert", "Insert Data");
                //lblMessage.Text = "Successful...";
                //AllControlClear();
            }
        #endregion

        #region for mini statement and balance enquiry
            else if (ddlServiceList.SelectedValue == "QT" || ddlServiceList.SelectedValue == "BI")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strReqText = "*" + ddlServiceList.SelectedValue + "*" + strRequestPartyWallet + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }
        #endregion

        #region  for bank deposit
            else if (ddlServiceList.SelectedValue == "BD")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 11);
                strReqText = "*" + ddlServiceList.SelectedValue + "*" + strRequestPartyWallet + "*" + txtAmount.Text + "*" + txtDPSAccNo.Text + "*" + strRecepentNo + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }
        #endregion

        #region for otp grneration
            else if (ddlServiceList.SelectedValue == "OTP")
            {
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                string strRequestState = "P";
                string strReqPartyType = ddlChannelTypeList.SelectedValue;
                strReqText = "*" + ddlServiceList.SelectedValue.ToString() + "*" + txtPinForOTP.Text+"#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, strRequestState, strReqText, strReqPartyType);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();
            }
        #endregion
        
        #region CPIN
            else if (ddlServiceList.SelectedValue == "CPIN1" || ddlServiceList.SelectedValue == "CPIN2")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                string strRequestState = "P";
                string strReqPartyType = ddlChannelTypeList.SelectedValue;
                
                string strOldPIN, strNewPin;
                strOldPIN = txtOldPIN.Text;
                strNewPin = txtNewPIN.Text;
                strReqText = "*" + ddlServiceList.SelectedValue + "*" + strOldPIN + "*" + strNewPin + "*" + strNewPin + "#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, strRequestState, strReqText, strReqPartyType);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();

            }
        #endregion

        #region MTP
            else if (ddlServiceList.SelectedValue == "MTP")
            {
                strRequestPartyWallet = txtSourceWallet.Text;
                strRequestparty = "+88" + txtSourceWallet.Text.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);
                strRecepentParty = ddlChannelTypeList.SelectedValue.ToString();
                strRecepentNo = txtDestinationWallet.Text.Substring(0, 11);
                strReqText = "*" + ddlServiceList.SelectedValue + "*" + strRequestPartyWallet + "*" + txtAmount.Text + "*" + strRecepentNo + "*0#";
                objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeList.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClear();

            }
        #endregion
            //else
            //{
            //    lblMessage.Text = "Please Choose/Select Appropriate Option for Transaction.";
            //    lblMessage.ForeColor = Color.Red;
            //}
        }
        catch (Exception exception)
        {
            lblMessage.Text = exception.Message;
        }
    }
    #endregion
    
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    private void AllControlClear()
    {
        txtAmount.Text = "";
        txtDestinationWallet.Text = "";
        txtSourceWallet.Text = "";
        txtDPSAccNo.Text = "";
        txtPinForOTP.Text = "";
        txtOldPIN.Text = "";
        txtNewPIN.Text = "";
        txtMPRef.Text = "";
        //ddlChannelTypeList.SelectedIndex = -1;
        //ddlServiceList.SelectedIndex = -1;
    }

    #region for using OTP

    protected void btnSaveOTP_Click(object sender, EventArgs e)
    {
        clsServiceHandler objSerHndler = new clsServiceHandler();
        string strMsg = "", strRequestparty = "", strRequestPartyN = "", strRecepentParty = "", strReqText = "", strRecepentNo="", strRequestPartyWallet = "";
        try
        {
            #region old Code

            //if (ddlServiceListOTP.SelectedValue == "CN")
            //{

            //    //string strReceipentID = "CN" + DateTime.Now.ToString();
            //    //strReceipentID = strReceipentID.Replace("/", "");
            //    //strReceipentID = strReceipentID.Replace(":", "");
            //    //strReceipentID = strReceipentID.Replace(" ", "");


            //    strRequestPartyWallet = txtSourceWalletOTP.Text;
            //    strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
            //    strRequestPartyN = strRequestparty.Substring(0, 14);
            //    strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
            //    strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
            //    string strRequestState = "P";
            //    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + txtOTP.Text + "*" + txtAmountOTP + "*" + strRecepentNo + "#";
            //    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
            //    SaveAuditInfo("Insert", "Insert Data");
            //    lblMessage.Text = "Successful...";
            //    AllControlClearOTP();
            //}

            //else if (ddlServiceListOTP.SelectedValue == "MP")
            //{
            //    strRequestPartyWallet = txtSourceWalletOTP.Text;
            //    strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
            //    strRequestPartyN = strRequestparty.Substring(0, 14);
            //    strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
            //    strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
            //    string strRequestState = "P";
            //    string strMpRefCode = txtMpRefOtp.Text;
            //    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + txtOTP.Text + "*" + txtAmountOTP + "*" + strRecepentNo + "#";
            //    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
            //    SaveAuditInfo("Insert", "Insert Data");
            //    lblMessage.Text = "Successful...";
            //    AllControlClearOTP();
            //}

            //else if (ddlServiceListOTP.SelectedValue == "FM")
            //{
            //    strRequestPartyWallet = txtSourceWalletOTP.Text;
            //    strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
            //    strRequestPartyN = strRequestparty.Substring(0, 14);
            //    strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
            //    strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
            //    string strRequestState = "P";
            //    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + txtOTP.Text + "*" + txtAmountOTP + "*" + strRecepentNo + "#";
            //    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
            //    SaveAuditInfo("Insert", "Insert Data");
            //    lblMessage.Text = "Successful...";
            //    AllControlClearOTP();
            //}

            //else if (ddlServiceListOTP.SelectedValue == "FT")
            //{
            //    strRequestPartyWallet = txtSourceWalletOTP.Text;
            //    strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
            //    strRequestPartyN = strRequestparty.Substring(0, 14);
            //    strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
            //    strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
            //    string strRequestState = "P";
            //    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + txtOTP.Text + "*" + txtAmountOTP + "*" + strRecepentNo + "#";
            //    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
            //    SaveAuditInfo("Insert", "Insert Data");
            //    lblMessage.Text = "Successful...";
            //    AllControlClearOTP();
            //}

            //else if (ddlServiceListOTP.SelectedValue == "CCT")
            //{
            //    strRequestPartyWallet = txtSourceWalletOTP.Text;
            //    strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
            //    strRequestPartyN = strRequestparty.Substring(0, 14);
            //    strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
            //    strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
            //    string strRequestState = "P";
            //    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + txtOTP.Text + "*" + txtAmountOTP + "*" + strRecepentNo + "#";
            //    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
            //    SaveAuditInfo("Insert", "Insert Data");
            //    lblMessage.Text = "Successful...";
            //    AllControlClearOTP();
            //}


            //else if (ddlServiceListOTP.SelectedValue == "SW")
            //{
            //    strRequestPartyWallet = txtSourceWalletOTP.Text;
            //    strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
            //    strRequestPartyN = strRequestparty.Substring(0, 14);
            //    strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
            //    strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
            //    string strRequestState = "P";
            //    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + txtOTP.Text + "*" + txtAmountOTP + "*" + strRecepentNo + "#";
            //    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
            //    SaveAuditInfo("Insert", "Insert Data");
            //    lblMessage.Text = "Successful...";
            //    AllControlClearOTP();
            //}


            //else if (ddlServiceListOTP.SelectedValue == "BD")
            //{
            //    strRequestPartyWallet = txtSourceWalletOTP.Text;
            //    strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
            //    strRequestPartyN = strRequestparty.Substring(0, 14);
            //    strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
            //    strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
            //    string strDpsAccNo = txtDpsAccOtp.Text;
            //    string strRequestState = "P";
            //    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + strRequestPartyWallet + "*" + txtAmountOTP.Text + "*" + txtDpsAccOtp.Text + "*" + strRecepentNo + "#";
            //    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
            //    SaveAuditInfo("Insert", "Insert Data");
            //    lblMessage.Text = "Successful...";
            //    AllControlClearOTP();

            //}

            //else if (ddlServiceListOTP.SelectedValue == "MTP")
            //{
            //    strRequestPartyWallet = txtSourceWalletOTP.Text;
            //    strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
            //    strRequestPartyN = strRequestparty.Substring(0, 14);
            //    strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
            //    strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
            //    string strRequestState = "P";
            //    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + strRequestPartyWallet + "*" + txtAmountOTP.Text + "*" + strRecepentNo + "*0#";
            //    //"*" + ddlServiceList.SelectedValue + "*" + strRequestPartyWallet + "*" + txtAmount.Text + "*" + strRecepentNo + "*0#";
            //    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
            //    SaveAuditInfo("Insert", "Insert Data");
            //    lblMessage.Text = "Successful...";
            //    AllControlClearOTP();

            //}

            ////else if (ddlServiceListOTP.SelectedValue == "QT" || ddlServiceListOTP.SelectedValue == "BI")
            ////{
            ////    strRequestPartyWallet = txtSourceWalletOTP.Text;
            ////    strRequestparty = "+88" + txtSourceWalletOTP.Text.Substring(0, 12);
            ////    strRequestPartyN = strRequestparty.Substring(0, 14);
            ////    strRecepentParty = ddlChannelTypeListOTP.SelectedValue.ToString();
            ////    strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + strRequestPartyWallet + "#";
            ////    objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, "P", strReqText, ddlChannelTypeListOTP.SelectedValue);
            ////    SaveAuditInfo("Insert", "Insert Data");
            ////    lblMessage.Text = "Successful...";
            ////    AllControlClear();
            ////}

            ////else if (ddlServiceListOTP.SelectedValue == "OTP")
            ////{
            ////    strRequestparty = "+88" + txtSourceWalletOTP.Text.Substring(0, 12);
            ////    strRequestPartyN = strRequestparty.Substring(0, 14);
            ////    strRecepentParty = ddlChannelTypeListOTP.SelectedValue.ToString();
            ////    string strRequestState = "P";
            ////    string strReqPartyType = ddlChannelTypeListOTP.SelectedValue;
            ////    strReqText = "*" + ddlServiceListOTP.SelectedValue.ToString() + "*" + txtPINOTP.Text + "#";
            ////    objSerHndler.SaveServiceReqInfoForVariousFund(strRequestPartyN, strRecepentParty, strRequestState, strReqText, strReqPartyType);
            ////    SaveAuditInfo("Insert", "Insert Data");
            ////    lblMessage.Text = "Successful...";
            ////    AllControlClear();
            ////}

            ////else
            ////{
            ////    lblMessage.Text = "Please Choose/Select Appropriate Option for Transaction.";
            ////    lblMessage.ForeColor = Color.Red;
            ////}

            #endregion

            if (ddlServiceListOTP.SelectedValue == "CN")
            {
                strRequestPartyWallet = txtSourceWalletOTP.Text;
                strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);

                string strReceipentID = "CN" + DateTime.Now.ToString();
                strReceipentID = strReceipentID.Replace("/", "");
                strReceipentID = strReceipentID.Replace(":", "");
                strReceipentID = strReceipentID.Replace(" ", "");
                //strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
                strRecepentParty = strReceipentID;

                strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
                string strRequestState = "P";
                string strOtp = txtOTP.Text;
                string strAmountOtp = txtAmountOTP.Text;
                strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + strOtp + "*" + strAmountOtp + "*" + strRecepentNo + "#";
                objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClearOTP();
            }

            else if (ddlServiceListOTP.SelectedValue == "CCT")
            {
                strRequestPartyWallet = txtSourceWalletOTP.Text;
                strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);

                string strReceipentID = "CCT" + DateTime.Now.ToString();
                strReceipentID = strReceipentID.Replace("/", "");
                strReceipentID = strReceipentID.Replace(":", "");
                strReceipentID = strReceipentID.Replace(" ", "");
                //strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
                strRecepentParty = strReceipentID;

                strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
                string strRequestState = "P";
                string strOtp = txtOTP.Text;
                string strAmountOtp = txtAmountOTP.Text;
                strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + strOtp + "*" + strAmountOtp + "*" + strRecepentNo + "#";
                objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClearOTP();
            }

            else if (ddlServiceListOTP.SelectedValue == "FM")
            {
                strRequestPartyWallet = txtSourceWalletOTP.Text;
                strRequestparty = "+88" + strRequestPartyWallet.Substring(0, 12);
                strRequestPartyN = strRequestparty.Substring(0, 14);

                string strReceipentID = "FM" + DateTime.Now.ToString();
                strReceipentID = strReceipentID.Replace("/", "");
                strReceipentID = strReceipentID.Replace(":", "");
                strReceipentID = strReceipentID.Replace(" ", "");
                //strRecepentParty = ddlChannelTypeListOTP.SelectedValue;
                strRecepentParty = strReceipentID;

                strRecepentNo = txtDestinationWalletOTP.Text.Substring(0, 12);
                string strRequestState = "P";
                string strOtp = txtOTP.Text;
                string strAmountOtp = txtAmountOTP.Text;
                strReqText = "*" + ddlServiceListOTP.SelectedValue + "*" + strOtp + "*" + strAmountOtp + "*" + strRecepentNo + "#";
                objSerHndler.AddServiceRequestForOTP(strRequestPartyN, strRecepentParty, strRequestState, strReqText, ddlChannelTypeListOTP.SelectedValue);
                SaveAuditInfo("Insert", "Insert Data");
                lblMessage.Text = "Successful...";
                AllControlClearOTP();
            }



        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion 

    private void AllControlClearOTP()
    {
        txtOTP.Text = "";
        txtAmountOTP.Text = "";
        txtDestinationWalletOTP.Text = "";
        txtSourceWalletOTP.Text = "";
        txtDpsAccOtp.Text = "";
        txtPINOTP.Text = "";
        //ddlChannelTypeListOTP.SelectedIndex = -1;
        //ddlServiceListOTP.SelectedIndex = -1;
    }

}
