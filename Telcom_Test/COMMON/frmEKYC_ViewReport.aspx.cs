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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.Globalization;

using Ext.Net.Examples.Restful;

using RestSharp;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class COMMON_frmEKYC_ViewReport : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private string strUserName = string.Empty;
    private string strPassword = string.Empty;
    // private string strDigitalKYC = string.Empty;
    private string clientId = string.Empty;


    private string agentName = string.Empty;
    private string agentMobile = string.Empty;
    private string agentAddress = string.Empty;
    private string imageLocation = string.Empty;
    private string strMSISDN = string.Empty;////Modified By A. Salam 19.8.2019
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
                // strDigitalKYC = Session["DigitalKYC"].ToString();
                if (!String.IsNullOrEmpty(Session["DigitalKYC"].ToString()))
                {
                    LoadRequestList(Session["DigitalKYC"].ToString());
                }

                PopulateDDLForIdentityType();
                PopulateDDLForRelationshipType();
                PopulateDDLForOccupationType();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
    //public void LoadDataInGridView()
    //{
    //    LoadRequestList(strDigitalKYC);
    //}

    public void LoadRequestList(string id)
    {
        string strSQL = "";
       

        strSQL = "SELECT  DIGITAL_KYC_ID   ,  CLINT_NAME            , CLINT_MOBILE           ,  CLINT_NATIONAL_ID    ,  CLINT_FATHER_NAME    ,  CLINT_MOTHER_NAME    ,  CLIENT_DOB           ,  CLIENT_PRE_ADDRESS   ,  CLIENT_PER_ADDRESS   , CLIENT_GENDER      ,  TRANSC_PURPOSE         , NOMINEE_NAME          ,NOMINEE_ADDRESS  ,      RELATIONSHIP_ID        ,NOMINEE_PERCENTAGE  ,    OCCUPATION_ID         ,CLINT_IMG              ,SIGNATURE_IMG  ,        NID_FRONT_IMG         ,NID_BACK_IMG         , IS_REGISTER          ,IS_UPDATE             ,VERIFICATION_CODE      , UNIQUE_KEY             ,  IDENTITY_TYPE         , REGISTRATION_DATE     ,  AGENT_ACCNT_NO         , (SELECT CLINT_NAME FROM ACCOUNT_LIST ALS , CLIENT_LIST CLS WHERE ALS.ACCNT_NO = AGENT_ACCNT_NO AND ALS.CLINT_ID = CLS.CLINT_ID ) AGENT_NAME,  REQUEST_BY         , IS_PROCESSING          ,  CLIENT_PRE_THANA_ID   , CLIENT_PER_THANA_ID  ,  CLIENT_PER_DIST_ID    , CLIENT_PRE_DIST_ID    , LOCATION_TYPE         , FATHR_HUSBND_TYPE    , CLINT_HUSBAND_NAME   ,  SERIAL_NO             , IMG_FILE              , REMARKS                ,  CANCEL_DATE            ,  OCR_SYSTEM_REF_ID      , FACE_SYSTEM_REF_ID    , REQUEST_REF_ID ,         KONA_NID_FRONT_URL    ,  KONA_NID_BACK_URL     ,  KONA_USER_IMAGE_URL   , CLINT_BANGLA_NAME      , KONA_IS_SAVED         ,  KONA_EKYC_TRACKING_ID   FROM DIGITAL_KYC_INFO WHERE DIGITAL_KYC_ID = '" + id + "'";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSQL);
       

        try
        {
            foreach (DataRow rows in oDs.Tables[0].Rows)
            {
                lblSerialNumber.Text = rows["SERIAL_NO"].ToString();
                txtCName.Text = rows["CLINT_NAME"].ToString();
                txtCMobileNo.Text = rows["CLINT_MOBILE"].ToString();

                txtCFNameOrHusband.Text = !String.IsNullOrEmpty(rows["FATHR_HUSBND_TYPE"].ToString().Trim()) && rows["FATHR_HUSBND_TYPE"].ToString().Trim() == "H" ? rows["CLINT_HUSBAND_NAME"].ToString() : rows["CLINT_FATHER_NAME"].ToString();
                rblFatherHusband.SelectedValue = rows["FATHR_HUSBND_TYPE"].ToString().Trim();
                txtCMName.Text = rows["CLINT_MOTHER_NAME"].ToString();
                txtCPreAddress.Text = rows["CLIENT_PRE_ADDRESS"].ToString();
                txtCPermaAddress.Text = rows["CLIENT_PER_ADDRESS"].ToString();
                rdoCGender.SelectedValue = rows["CLIENT_GENDER"].ToString();
                //ddlPurOfTran.SelectedValue = rows["TRANSC_PURPOSE"].ToString();
                txtNomineeName.Text = rows["NOMINEE_NAME"].ToString();
                txtCPermaAddress.Text = rows["CLIENT_PER_ADDRESS"].ToString();
                txtNomineeAddress.Text = rows["NOMINEE_ADDRESS"].ToString();
                //txtRelationshipId.Text = rows["RELATIONSHIP_ID"].ToString();
                ddlRelationship.SelectedValue = rows["RELATIONSHIP_ID"].ToString();
                txtNomineePercentage.Text = rows["NOMINEE_PERCENTAGE"].ToString();
                //txtOccupationId.Text = rows["OCCUPATION_ID"].ToString();
                txtVerificationCode.Text = rows["VERIFICATION_CODE"].ToString();
                txtUniqueKey.Text = rows["UNIQUE_KEY"].ToString();
                ddlIdenType.SelectedValue = rows["IDENTITY_TYPE"].ToString();
                txtCNationalId.Text = rows["CLINT_NATIONAL_ID"].ToString();
                txtRegistrationDate.Text = rows["REGISTRATION_DATE"].ToString();
                txtAgentAccNo.Text = rows["AGENT_ACCNT_NO"].ToString();
                txtAgentName.Text = rows["AGENT_NAME"].ToString();
                //string dobString = rows["CLIENT_DOB"].ToString();
                //DateTime dtDOB = rows["CLIENT_DOB"] != null ? DateTime.ParseExact(dobString,
                //    "M-d-yyyy", CultureInfo.InvariantCulture) : DateTime.Now;
                txtCDOB.Text = rows["CLIENT_DOB"] != null ? Convert.ToDateTime(rows["CLIENT_DOB"].ToString()).ToString("dd/MM/yyyy") : null;
                string districtPer = rows["CLIENT_PER_DIST_ID"].ToString();
                string thanaPer = rows["CLIENT_PER_THANA_ID"].ToString();
                string districtPre = rows["CLIENT_PRE_DIST_ID"].ToString();
                string thanaPre = rows["CLIENT_PRE_THANA_ID"].ToString();
                LoadThanaDistrict(districtPer, thanaPer, districtPre, thanaPre);
                rblUrbanOrRural.SelectedValue = rows["LOCATION_TYPE"].ToString();
                lblSerialNumber.Text = rows["SERIAL_NO"].ToString();
                if (rows["IS_UPDATE"].ToString() == "N" && rows["IS_REGISTER"].ToString() == "Y")
                {
                    btnUpdate.Visible = true;
                }
                //rblFatherHusband.
                //txtRemarks.Text = rows["REMARKS"].ToString();
                //txtIsRegister.Text = rows["IS_REGISTER"].ToString();
                //txtIsUpdated.Text = rows["IS_UPDATE"].ToString();

                // imageLocation = "http://10.11.1.9:98/KYC_Files/";
                // imageLocation =@"D:\MPAY\MT_WS_MYCASH_QR_38\KYC_Files\";//demo path
                //imageLocation = @"D:\MPAY\MT_WS_MYCASH_QR\MT_WS_MYCASH_QR_45\KYC_Files\";//Live Path

                imageLocation = @"E:\\KYC_Registration_Picture\\";



                var webClient = new WebClient();
                byte[] imageBytes;
                string TINimageBase64Data = "";
                if (!string.IsNullOrEmpty(rows["CLINT_IMG"].ToString()))
                {
                    imageBytes = webClient.DownloadData(imageLocation + rows["CLINT_IMG"].ToString().Replace("jpg", "png"));
                    TINimageBase64Data = Convert.ToBase64String(imageBytes);
                    ImgCustomer.ImageUrl = string.Format("data:image/png;base64,{0}", TINimageBase64Data);
                }
                if (!string.IsNullOrEmpty(rows["SIGNATURE_IMG"].ToString()))
                {
                    imageBytes = webClient.DownloadData(imageLocation + rows["SIGNATURE_IMG"].ToString().Replace("jpg", "png"));
                    TINimageBase64Data = Convert.ToBase64String(imageBytes);
                    ImgSignature.ImageUrl = string.Format("data:image/png;base64,{0}", TINimageBase64Data);
                }
                if (!string.IsNullOrEmpty(rows["NID_FRONT_IMG"].ToString()))
                {
                    imageBytes = webClient.DownloadData(imageLocation + rows["NID_FRONT_IMG"].ToString().Replace("jpg", "png"));
                    TINimageBase64Data = Convert.ToBase64String(imageBytes);
                    ImgNIDFront.ImageUrl = string.Format("data:image/png;base64,{0}", TINimageBase64Data);
                }
                if (!string.IsNullOrEmpty(rows["NID_BACK_IMG"].ToString()))
                {
                    imageBytes = webClient.DownloadData(imageLocation + rows["NID_BACK_IMG"].ToString().Replace("jpg", "png"));
                    TINimageBase64Data = Convert.ToBase64String(imageBytes);
                    ImgNIDBack.ImageUrl = string.Format("data:image/png;base64,{0}", TINimageBase64Data);
                }


                //    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/images"));
                //   FileInfo fi = di.GetObjectData(di.f);

                //  ImgCustomer.ImageUrl = imageLocation + rows["CLINT_IMG"].ToString();
                //   ImgSignature.ImageUrl = imageLocation + rows["SIGNATURE_IMG"].ToString();
                //  ImgNIDFront.ImageUrl = imageLocation + rows["NID_FRONT_IMG"].ToString();
                // ImgNIDBack.ImageUrl = imageLocation + rows["NID_BACK_IMG"].ToString();

                //clientId = objServiceHandler.GetAccountIdByMobileNo(txtCMobileNo.Text.Trim() + "1");
                DataSet ds = objServiceHandler.GetAccountDetail(txtCMobileNo.Text.Trim() + "1");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                clientId = dr[0].ToString();
                txtClientID.Text = clientId;


                DataSet dsAgent = objServiceHandler.GetAccountDetail(txtAgentAccNo.Text.Trim());
                DataTable dtAgent = dsAgent.Tables[0];
                DataRow drAgent = dtAgent.Rows[0];
                string clientIdFromAgent = drAgent[0].ToString();

                dsAgent = objServiceHandler.GetClientList("SELECT * FROM CLIENT_LIST WHERE CLINT_ID = '" + clientIdFromAgent + "'");
                dtAgent = dsAgent.Tables[0];
                drAgent = dtAgent.Rows[0];
                agentAddress = drAgent[3].ToString();
                agentMobile = drAgent["CLINT_MOBILE"].ToString();
                agentName = drAgent[1].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        string strAccountID = "";
        Session["ItVerified"] = true;
        var id = txtClientID.Text.Trim();

        if (txtCNationalId.Text == "")
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Identity Could not be Null";
            return;
        }
        if (ddlThanaPer.SelectedItem.ToString() == "Select a Thana" || ddlThanaPer.SelectedItem.ToString() == "No Data"
            || ddlThanaPre.SelectedItem.ToString() == "Select a Thana" || ddlThanaPre.SelectedItem.ToString() == "No Data")
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Please select a thana and district.";
            return;
        }
        if (rdoCGender.SelectedValue == null)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Please select gender.";
            return;
        }
        if (txtCNationalId.Text == "")
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Client Id is not valid";
            return;
        }

        try
        {
            if (Session["AccountID"] != null && Session["DigitalKYC"] != null)
            {
                string strAccountId = "SELECT ACCNT_ID FROM ACCOUNT_LIST WHERE ACCNT_MSISDN = '" + "+88" + txtCMobileNo.Text.Trim() + "'";
                strAccountID = objServiceHandler.ReturnString(strAccountId);
                bool blnClient = false, blnBank = false, blnIden = false, blnInto = false, blnNomnee = false;
                string strUpdateMessage = "", strDateTime = "";
                strDateTime = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", DateTime.Now);

                string strSuccMsg = "";
                string strClientIdIfExist = "";
                strClientIdIfExist = objServiceHandler.ClientIdIfExist(txtClientID.Text.Trim());
                if (strClientIdIfExist == "")
                {
                    if (ddlIdenType.SelectedItem.Text == "NATIONAL ID")
                    {
                        if (txtCNationalId.Text.Length == 13 || txtCNationalId.Text.Length == 10)
                        {
                            blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtCNationalId.Text.Trim(), ddlIdenType.SelectedValue.ToString(), "");//txtIdenRemarks.Text.Trim()
                        }

                        else if (txtCNationalId.Text.Length == 17)
                        {
                            blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtCNationalId.Text.Trim(), ddlIdenType.SelectedValue.ToString(), "");//txtIdenRemarks.Text.Trim()
                        }

                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "National Id Must be 13 or 17 Digit in Length";
                            return;
                        }
                    }
                    else if (ddlIdenType.SelectedItem.Text == "SMART CARD")
                    {
                        if (txtCNationalId.Text.Length == 10)
                        {
                            blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtCNationalId.Text.Trim(), ddlIdenType.SelectedValue.ToString(), "");//txtIdenRemarks.Text.Trim()
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Smart Card Must be 10 Digit in Length";
                            return;
                        }
                    }
                    else
                    {
                        blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtCNationalId.Text.Trim(), ddlIdenType.SelectedValue.ToString(), "");//txtIdenRemarks.Text.Trim()
                    }
                }
                else
                {
                    if (ddlIdenType.SelectedItem.Text == "NATIONAL ID")
                    {
                        if (txtCNationalId.Text.Length == 13 || txtCNationalId.Text.Length == 10)
                        {
                            strSuccMsg = objServiceHandler.UpdateClientIdentification(txtClientID.Text.Trim(), txtCNationalId.Text.Trim(),
                                                                       ddlIdenType.SelectedValue.ToString(), "");//txtIdenRemarks.Text.Trim()
                            if (strSuccMsg != "Successfull.")
                            {
                                return;
                            }
                        }

                        else if (txtCNationalId.Text.Length == 17)
                        {
                            strSuccMsg = objServiceHandler.UpdateClientIdentification(txtClientID.Text.Trim(), txtCNationalId.Text.Trim(),
                                                                        ddlIdenType.SelectedValue.ToString(), "");//txtIdenRemarks.Text.Trim()
                            if (strSuccMsg != "Successfull.")
                            {
                                return;
                            }
                        }

                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "National Id Must be 13 or 17 Digit in Length";
                            return;
                        }
                    }
                    else if (ddlIdenType.SelectedItem.Text == "SMART CARD")
                    {
                        if (txtCNationalId.Text.Length == 10)
                        {
                            strSuccMsg = objServiceHandler.UpdateClientIdentification(txtClientID.Text.Trim(), txtCNationalId.Text.Trim(),
                                                                       ddlIdenType.SelectedValue.ToString(), "");//txtIdenRemarks.Text.Trim()
                            if (strSuccMsg != "Successfull.")
                            {
                                return;
                            }
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Smart Card Must be 10 Digit in Length";
                            return;
                        }
                    }
                    else
                    {
                        strSuccMsg = objServiceHandler.UpdateClientIdentification(txtClientID.Text.Trim(), txtCNationalId.Text.Trim(),
                                                                        ddlIdenType.SelectedValue.ToString(), "");//txtIdenRemarks.Text.Trim()
                    }
                }

                if (txtCMobileNo.Text.Trim() != "")
                {
                    strUpdateMessage = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("CLIENT_LIST", "KYC_UPDATED_BY", "CLINT_ID", txtClientID.Text.Trim());
                    //-------------------- checking for father's or husband name ----------------
                    string strFatherName = "", strHusbandName = "";
                    if (rblFatherHusband.SelectedValue == "F")
                    {
                        strFatherName = txtCFNameOrHusband.Text.ToString();
                    }
                    else
                    {
                        strHusbandName = txtCFNameOrHusband.Text.ToString();
                    }
                    string customerArea = rblUrbanOrRural.SelectedValue;
                    //--------------------------------------------------------------------------

                    blnClient = objServiceHandler.UpdateClientList(txtCName.Text.Trim(), strFatherName,
                                                    txtCMName.Text.Trim(), txtCDOB.Text,
                                                    ddlOccupation.SelectedValue, "",
                                                    ddlPurOfTran.SelectedValue, "", "",
                                                    txtCPreAddress.Text.Trim().Replace("'", ""), txtCPermaAddress.Text.Trim().Replace("'", ""),
                                                    ddlThanaPre.SelectedValue.ToString(), txtClientID.Text.Trim(),
                                                    Session["AccountID"].ToString(), strDateTime, "WEB", strUpdateMessage, strHusbandName, "",
                                                    ddlThanaPer.SelectedValue.ToString(), customerArea, rdoCGender.SelectedValue);

                }
                else
                {
                    blnClient = true;
                }
                if (blnClient == true)
                {
                    if (agentName != "")
                    {
                        blnInto = objServiceHandler.SaveIntroducerInfo(txtClientID.Text.Trim(), agentName, agentMobile, agentAddress, ddlOccupation.SelectedValue, "");
                    }
                    else
                    {
                        blnInto = true;
                    }
                    if (blnInto == true)
                    {
                        if (txtNomineeName.Text != "")
                        {
                            decimal dcmPercent = 0;

                            if (txtNomineePercentage.Text != "")
                            {
                                dcmPercent = Convert.ToDecimal(txtNomineePercentage.Text);
                            }
                            blnNomnee = objServiceHandler.SaveNomineeInfo(txtClientID.Text.Trim(), txtNomineeName.Text.Trim(),
                                                           "", ddlRelationship.SelectedValue, dcmPercent,
                                                           "");//txtNomneMobile.Text.Trim()   txtNomneRemarks.Text.Trim()
                        }
                        else
                        {
                            blnNomnee = true;
                        }
                        if (blnNomnee == true)
                        {
                            try
                            {
                                //Modified By Abdul Bari 15.9.2022
                                strMSISDN = "+88" + (txtCMobileNo.Text.ToString()).Substring(0, 11);
                                string customerWalletID = txtCMobileNo.Text.ToString() + "1";
                                string result;
                                string strVerifyResult = objServiceHandler.VerifyAccount(customerWalletID);
                                string strMsg = objServiceHandler.UpdateAccountList(strAccountID);
                                //string strMsgStatus = objServiceHandler.UpdateVerifiedStatus(strMSISDN);
                           

                            //Modified By A. Salam 19.8.2019

                            DataSet dtsAccDetail = objServiceHandler.GetAccountDetailKYC(strMSISDN);
                            if (dtsAccDetail.Tables["ACCOUNT_LIST_ONLY_NEW"].Rows.Count > 0)
                            {
                                DataRow dRow = dtsAccDetail.Tables["ACCOUNT_LIST_ONLY_NEW"].Rows[0];
                                strAccountID = dRow["ACCNT_ID"].ToString();
                                string strToken = dRow["ACCCT_AC_TOKEN"].ToString();

                                string strHostName = Dns.GetHostName();
                                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                                IPAddress[] IPaddr = ipEntry.AddressList;
                                string strIP = "";

                                lblMsg.Text = "";
                                foreach (IPAddress IP in IPaddr)
                                {
                                    string strAddressFamily = IP.AddressFamily.ToString();
                                    if (strAddressFamily.Equals("InterNetwork"))
                                    {
                                        strIP = IP.ToString();
                                        break;
                                    }
                                }
                                // Send Activation Message.
                                string strReturn = objServiceHandler.KYCVerifyed(strAccountID, strToken, strMSISDN, "", strIP, strHostName + " [" + System.Environment.UserName + "]");

                                if (txtCMobileNo.Text.Trim().Length == 11)
                                {
                                    objServiceHandler.GenerateQrCodeManually(txtCMobileNo.Text.Trim() + "1");
                                }

                                lblMsg.Visible = true;
                                lblMsg.Text = "All Information Saved Successfully.";
                                SaveAuditInfo("Update", "KYC Update,Update_No=" + txtCMobileNo.Text.ToString() + "");
                                objServiceHandler.UpdateDIGITAL_KYC_INFO(Session["DigitalKYC"].ToString());
                                //LoadDataInGridView();
                                //LoadThanaDistrict();
                            }


                            if (strVerifyResult == "Verifyed successfully." && strMsg == "Successfull.")
                            {
                                //string x = txtCNationalId.Text.ToString();
                                //string y=txtCMobileNo.Text.ToString();
                                // Calling API Save KONA EKYC
                                var userData = objServiceHandler.KONA_eKYC_NID_Data(txtCNationalId.Text.ToString(), txtCMobileNo.Text.ToString());

                                DateTime dTime = Convert.ToDateTime(userData["dateOfBirth"].ToString());
                                string formatedDate = dTime.ToString("dd-MM-yyyy");

                                //string formattedDate = date.ToString("dd-MM-yyyy");

                                var response = KONA_eKYC_NID_Save_Data(
                                    userData["nid"], userData["name"], userData["fatherName"], userData["motherName"], userData["spouseName"], formatedDate, userData["gender"], userData["presentAddress"], userData["permanentAddress"], userData["contactNo"], customerWalletID, userData["nomineeName"], userData["nomineeDob"], "MFS", userData["ocrReferenceID"], userData["faceMatchReferenceId"], userData["nidFrontUrl"], userData["nidBackUrl"], userData["userImageUrl"]);  // customerWalletID = accountNumber


                                var deserializeResponse = objServiceHandler.DataDeserialization(response);
                                // Need to insert data to database :TODO
                                
                                var isUpdated = objServiceHandler.UpdateUserData(deserializeResponse["isSaved"], deserializeResponse["ekycTrackingId"], txtCMobileNo.Text, txtCNationalId.Text);

                                if (deserializeResponse["status"] == "SUCCESSFUL" && isUpdated == "Updated")
                                {
                                    result = "Success";
                                }
                                else
                                {
                                    result = "Failure";
                                }
                            }
                            else
                            {
                                result = "Failure";
                            }


                            }
                            catch (Exception ex)
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = ex.Message.ToString();
                                return;
                            }

                            //end Modified By Abdul Bari 15.9.2022


                            //End Modified By A. Salam 19.8.2019
                            try
                            {
                                //string url = "~/Common/frmDigitalKYC_Information.aspx";
                                //Response.Redirect(url, false);
                                Response.Redirect("../Common/frmEKYC_Information.aspx", false);
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = ex.Message.ToString();
                                return;
                            }
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Nominee Information Not Saved.";
                            return;
                        }
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Introducer Information Not Saved.";
                        return;
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Client Identification Not Saved.";
                    return;
                }
            }
            else
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        catch (Exception exception)
        {
            lblMsg.Visible = true;
            lblMsg.Text = exception.Message.ToString();
            return;
        }
    }


    // [WebMethod(Description = "KONA eKYC DPS – Save eKYC")]
    public static string KONA_eKYC_NID_Save_Data(string nid, string name, string fatherName, string motherName, string spouseName, string dateOfBirth, string gender, string presentAddress, string permanentAddress, string contactNo, string accountNumber,
        string nomineeName, string nomineeDob, string kycTypeEnum, string ocrReferenceID, string faceMatchReferenceId, string nidFrontUrl, string nidBackUrl, string userImageUrl)
    {
       

        object userData = null;

        if (kycTypeEnum == "MFS")
        {
            // nominee data in anonymous object
            var nomineeData = new
            {
                nomineeName,
                nomineeDob,                   // dob format dd-MM-yyyy
                nomineeAddress = "N/A",
                percentage = "N/A",
                nomineeNid = "N/A",
                nomineeSelfieUrl = "N/A",
                authorizedDrawerName = "N/A",
                authorizedDrawerAddress = "N/A",
                authorizedDrawerRelation = "N/A",
                authorizedDrawerNid = "N/A",
                nomineeMobile = "N/A",
                nomineeFatherName = "N/A",
                nomineeMotherName = "N/A",
                documentTypeEnum = "N/A",
                nomineeRelation = "N/A"
            };

            // List of object
            var nomineeInfoList = new List<dynamic>();
            nomineeInfoList.Add(nomineeData);

            // full user data in json
            userData = JsonConvert.SerializeObject(new
            {
                nid,
                name,
                fatherName,
                motherName,
                spouseName,
                dateOfBirth,
                gender,
                presentAddress,
                permanentAddress,
                contactNo,
                accountNumber,
                nomineeInfoList,
                ekycFormType = "SIMPLIFIED",
                ekycOrigin = "APP",
                kycTypeEnum,
                additionalInfoTypeEnum = "BANK",
                ocrReferenceID,
                faceMatchReferenceId,
                nidFrontUrl,
                nidBackUrl,
                userImageUrl
            });
        }

        if (kycTypeEnum == "ABS")
        {
            // full user data in json
            userData = JsonConvert.SerializeObject(new
            {
                nid,
                name,
                fatherName,
                motherName,
                spouseName,
                dateOfBirth,
                gender,
                presentAddress,
                permanentAddress,
                contactNo,
                accountNumber,
                ekycFormType = "SIMPLIFIED",
                ekycOrigin = "ABS",
                kycTypeEnum,
                additionalInfoTypeEnum = "BANK",
                ocrReferenceID,
                faceMatchReferenceId,
                nidFrontUrl,
                nidBackUrl,
                userImageUrl
            });
        }


        string url = "http://10.99.99.70:30000/data-processing-system/saveEkycData";


        try
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("username", "MYCASH");
            request.AddHeader("password", "ekyc@mycash");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", userData, RestSharp.ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var responseText = response.Content;
         
            var deserializeResponse = JsonConvert.DeserializeObject<dynamic>(responseText);

            string isSaved = deserializeResponse["isSaved"].ToString();
            string status = deserializeResponse["status"].ToString();
            string message = deserializeResponse["message"].ToString();
            string ekycTrackingId = deserializeResponse["ekycTrackingId"].ToString();

            //objLogWriter.WriteKONA_EKYC_LOG(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " :" + " EKYC Tracking ID: " + ekycTrackingId + "" + Environment.NewLine + "KONA eKYC DPS – Save eKYC - API Request : " + userData + "" + Environment.NewLine + "KONA eKYC DPS – Save eKYC - API Response Result : " + responseText + "" + Environment.NewLine + Environment.NewLine);

            return "status~" + status + "*isSaved~" + isSaved + "*message~" + message + "*ekycTrackingId~" + ekycTrackingId;
        }
        catch (Exception ex)
        {
            return "status~Failure";
        }

    }
     
    protected void ddlDistrictPer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlThanaPer.Items.Clear();
            string strSql = "";
            strSql = " SELECT  MT.THANA_ID, MT.THANA_NAME FROM MANAGE_THANA MT, MANAGE_DISTRICT MD "
                   + " WHERE  MD.DISTRICT_ID=MT.DISTRICT_ID  AND  MD.DISTRICT_ID='" + ddlDistrictPer.SelectedValue.ToString() + "'";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            ddlThanaPer.DataSource = oDs;
            ddlThanaPer.DataValueField = "THANA_ID";
            ddlThanaPer.DataTextField = "THANA_NAME";
            ddlThanaPer.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ddlDistrictPre_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlThanaPre.Items.Clear();
            string strSql = "";
            strSql = " SELECT  MT.THANA_ID, MT.THANA_NAME FROM MANAGE_THANA MT, MANAGE_DISTRICT MD "
                   + " WHERE  MD.DISTRICT_ID=MT.DISTRICT_ID  AND  MD.DISTRICT_ID='" + ddlDistrictPre.SelectedValue.ToString() + "'";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlThanaPre.DataSource = oDs;
            ddlThanaPre.DataValueField = "THANA_ID";
            ddlThanaPre.DataTextField = "THANA_NAME";
            ddlThanaPre.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    public void PopulateDDLForIdentityType()
    {
        string strSql = "SELECT * FROM IDENTIFICATION_SETUP";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
        ddlIdenType.DataSource = oDs;
        ddlIdenType.DataValueField = "IDNTIFCTION_ID";
        ddlIdenType.DataTextField = "IDNTIFCTION_NAME";
        ddlIdenType.DataBind();
    }

    public void PopulateDDLForRelationshipType()
    {
        string strSql = "SELECT * FROM RELATIONSHIP";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
        ddlRelationship.DataSource = oDs;
        ddlRelationship.DataValueField = "RELATIONSHIP_ID";
        ddlRelationship.DataTextField = "RELATIONSHIP_TITLE";
        ddlRelationship.DataBind();
    }

    public void PopulateDDLForOccupationType()
    {
        string strSql = "SELECT * FROM OCCUPATION";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
        ddlOccupation.DataSource = oDs;
        ddlOccupation.DataValueField = "OCCUPATION_ID";
        ddlOccupation.DataTextField = "OCCUPATION_TITLE";
        ddlOccupation.DataBind();
    }

    private void LoadThanaDistrict(string districtPer, string thanaPer, string districtPre, string thanaPre)
    {
        try
        {

        
        string strDistrict = " SELECT DISTRICT_ID ,DISTRICT_NAME FROM MANAGE_DISTRICT ";
        DataSet oDsDistrict = objServiceHandler.ExecuteQuery(strDistrict);

        if (!districtPer.Equals(""))
        {
            ddlDistrictPer.DataSource = oDsDistrict;
            ddlDistrictPer.DataValueField = "DISTRICT_ID";
            ddlDistrictPer.DataTextField = "DISTRICT_NAME";
            ddlDistrictPer.DataBind();

            ddlDistrictPer.SelectedValue = districtPer;

            // Loading Permanent Address Thana
            string strThanaPer = "SELECT THANA_ID,THANA_NAME FROM MANAGE_THANA WHERE DISTRICT_ID='" + districtPer + "' ORDER BY THANA_ID";
            DataSet oDsThanaPer = objServiceHandler.ExecuteQuery(strThanaPer);

            ddlThanaPer.DataSource = oDsThanaPer;
            ddlThanaPer.DataValueField = "THANA_ID";
            ddlThanaPer.DataTextField = "THANA_NAME";
            ddlThanaPer.DataBind();

            ddlThanaPre.SelectedValue = thanaPre;
        }
        else
        {
            ddlDistrictPer.DataSource = oDsDistrict;
            ddlDistrictPer.DataValueField = "DISTRICT_ID";
            ddlDistrictPer.DataTextField = "DISTRICT_NAME";
            ddlDistrictPer.DataBind();

            ddlDistrictPer.Items.Add("No Data");
            ddlThanaPer.Items.Add("No Data");
        }

        if (!districtPre.Equals(""))
        {
            ddlDistrictPre.DataSource = oDsDistrict;
            ddlDistrictPre.DataValueField = "DISTRICT_ID";
            ddlDistrictPre.DataTextField = "DISTRICT_NAME";
            ddlDistrictPre.DataBind();

            ddlDistrictPre.SelectedValue = districtPre;

            // Loading Present Address Thana
            //string strThanaPre = "SELECT THANA_ID,THANA_NAME FROM MANAGE_THANA WHERE DISTRICT_ID='" + districtPre + "' ORDER BY THANA_ID";
            //DataSet oDsThanaPre = objServiceHandler.ExecuteQuery(strThanaPre);
            //ddlThanaPre.DataSource = oDsThanaPre;
            //ddlThanaPre.DataValueField = "THANA_ID";
            //ddlThanaPre.DataTextField = "THANA_NAME";
            //ddlThanaPre.DataBind();

            // Loading Permanent Address Thana
            string strThanaPer = "SELECT THANA_ID,THANA_NAME FROM MANAGE_THANA WHERE DISTRICT_ID='" + districtPre + "' ORDER BY THANA_ID";
            DataSet oDsThanaPer = objServiceHandler.ExecuteQuery(strThanaPer);

            ddlThanaPre.DataSource = oDsThanaPer;
            ddlThanaPre.DataValueField = "THANA_ID";
            ddlThanaPre.DataTextField = "THANA_NAME";
            ddlThanaPre.DataBind();


            ddlThanaPre.SelectedValue = thanaPre;
        }
        else
        {
            ddlDistrictPre.DataSource = oDsDistrict;
            ddlDistrictPre.DataValueField = "DISTRICT_ID";
            ddlDistrictPre.DataTextField = "DISTRICT_NAME";
            ddlDistrictPre.DataBind();

            ddlDistrictPre.Items.Add("No Data");
            ddlThanaPre.Items.Add("No Data");
        }
        }
        catch (Exception ex)
        {

        }
    }
    private void Clear()
    {
        lblRemarks.Text = "";
        txtClientID.Text = "";
        txtCMobileNo.Text = "";
        txtAgentName.Text = "";
        txtCName.Text = "";
        //txtFormSerialNo.Text = "";
        txtCFNameOrHusband.Text = "";

        rblFatherHusband.SelectedValue = "";
        txtCMName.Text = "";
        //txtOccupationId.Text = "";
        //txtWorkEduBusines.Text = "";
        //ddlPurOfTran.SelectedValue = "";
        //ddlUISCAgent.SelectedIndex = -1;
        ddlDistrictPer.Items.Clear();
        ddlThanaPer.Items.Clear();
        //txtOffceAddress.Text = "";
        //txtPreAddress.Text = "";
        //txtPerAddress.Text = "";
        //txtWalletID.Text = "";
        txtCMobileNo.Text = "";

        //txtBankName.Text = "";
        //txtBranchName.Text = "";
        //txtAccountNo.Text = "";
        //txtBankRemarks.Text = "";

        txtCNationalId.Text = "";
        //txtIdenRemarks.Text = "";

        //txtIntroducerName.Text = "";
        //txtIntroMobile.Text = "";
        //txtIntroAddress.Text = "";
        //txtIntroOccupation.Text = "";
        //txtIntroRemarks.Text = "";

        txtNomineeName.Text = "";
        //txtNomneMobile.Text = "";
        txtNomineeAddress.Text = "";
        //txtRelationshipId.Text = "";
        txtNomineePercentage.Text = "";
        //txtNomneRemarks.Text = "";
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvRequest_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //protected void gdvRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        string strStatus = e.Row.Cells[14].Text;

    //        if (strStatus.Equals("000"))
    //        {
    //            Button btnSuccess = (Button)e.Row.FindControl("Button1");
    //            btnSuccess.Enabled = false;
    //            Button btnReverse = (Button)e.Row.FindControl("Button2");
    //            btnReverse.Enabled = false;
    //        }
    //    }
    //}
    protected void ButtonReport_Click(object sender, EventArgs e)
    {
        string strSql = "";
        string strClientSig = "";
        try
        {
            strSql = "SELECT   d.IMG_FILE LOGO,         d.IMG_FILE IMG_CUSTOMER,         d.IMG_FILE IMG_SINATURE,         d.IMG_FILE IMG_NID_FONT,         d.IMG_FILE IMG_NID_BACK,         d.*,         c.*,         I.IDNTIFCTION_NAME,         asd.SERIAL_NO SN,         (SELECT   DISTRICT_NAME            FROM   MANAGE_DISTRICT           WHERE   DISTRICT_ID = d.CLIENT_PRE_DIST_ID)            PREDIS,         (SELECT   THANA_NAME            FROM   MANAGE_THANA           WHERE   THANA_ID = d.CLIENT_PRE_THANA_ID)            PRETHANA,         (SELECT   DISTRICT_NAME            FROM   MANAGE_DISTRICT           WHERE   DISTRICT_ID = d.CLIENT_PER_DIST_ID)            PERDIS,         (SELECT   THANA_NAME            FROM   MANAGE_THANA           WHERE   THANA_ID = d.CLIENT_PER_THANA_ID)            PERTHANA,         R.RELATIONSHIP_TITLE,         O.OCCUPATION_TITLE,         (SELECT   CLINT_ADDRESS1            FROM      CLIENT_LIST CL                   INNER JOIN                      ACCOUNT_LIST AL                   ON CL.CLINT_ID = AL.CLINT_ID           WHERE   AL.ACCNT_NO = D.AGENT_ACCNT_NO)            AgentAddress,            '+88' || d.CLINT_MOBILE , c.CLINT_MOBILE  FROM   DIGITAL_KYC_INFO d,         CLIENT_LIST c,         ACCOUNT_SERIAL_DETAIL asd,         IDENTIFICATION_SETUP I,         RELATIONSHIP R,         OCCUPATION O WHERE       DIGITAL_KYC_ID = '" + Session["DigitalKYC"].ToString() + "'         AND '+88' || d.CLINT_MOBILE = c.CLINT_MOBILE         AND '+88' || d.CLINT_MOBILE = asd.CUSTOMER_MOBILE_NO         AND d.IDENTITY_TYPE = I.IDNTIFCTION_ID         AND R.RELATIONSHIP_ID = d.RELATIONSHIP_ID         AND O.OCCUPATION_ID = d.OCCUPATION_ID";

            DataSet ds = objServiceHandler.ExecuteQuery(strSql);
            //imageLocation = "http://10.11.1.9:98/KYC_Files/";
            //ImgCustomer.ImageUrl = imageLocation + rows["CLINT_IMG"].ToString();
            //ImgSignature.ImageUrl = imageLocation + rows["SIGNATURE_IMG"].ToString();
            //ImgNIDFront.ImageUrl = imageLocation + rows["NID_FRONT_IMG"].ToString();
            //ImgNIDBack.ImageUrl = imageLocation + rows["NID_BACK_IMG"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int index = 0; index < ds.Tables[0].Rows.Count; index++)
                {
                    if (ds.Tables[0].Rows[index]["CLINT_IMG"].ToString() != "")
                    {
                        strClientSig = imageLocation + ds.Tables[0].Rows[index]["CLINT_IMG"].ToString();
                        // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                        //byte[] b = imageToByteArray(ImgCustomer.);
                        LoadImage(ds.Tables[0].Rows[index], "IMG_CUSTOMER", strClientSig);
                    }
                    if (ds.Tables[0].Rows[index]["SIGNATURE_IMG"].ToString() != "")
                    {
                        strClientSig = imageLocation + ds.Tables[0].Rows[index]["SIGNATURE_IMG"].ToString();
                        // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                        LoadImage(ds.Tables[0].Rows[index], "IMG_SINATURE", strClientSig);
                    }
                    if (ds.Tables[0].Rows[index]["NID_FRONT_IMG"].ToString() != "")
                    {
                        strClientSig = imageLocation + ds.Tables[0].Rows[index]["NID_FRONT_IMG"].ToString();
                        // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                        LoadImage(ds.Tables[0].Rows[index], "IMG_NID_FONT", strClientSig);
                    }
                    if (ds.Tables[0].Rows[index]["NID_BACK_IMG"].ToString() != "")
                    {
                        strClientSig = imageLocation + ds.Tables[0].Rows[index]["NID_BACK_IMG"].ToString();
                        // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                        LoadImage(ds.Tables[0].Rows[index], "IMG_NID_BACK", strClientSig);
                    }

                    strClientSig = this.Server.MapPath("~/Images/mycash_logo.jpg");
                    // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                    LoadImage(ds.Tables[0].Rows[index], "LOGO", strClientSig);
                }

                Session["Dual"] = ds.Tables[0];
                Session["ReportSQL"] = "NOTSQL";



                SaveAuditInfo("View", " Employee Report");
                //Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMMON/frmDigitalKYC_ViewReport.aspx";
                Session["ReportFile"] = "../COMMON/DigitalKYC_ViewReport.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Wrong", "alert('Client Not Registered!');", true);
            }


        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        //if (txtRemarks.Text == "")
        //{

        //    lblMsg.Visible = true;
        //    lblMsg.Text = "Please Mention The Reason On Remarks!";
        //    lblRemarks.Text = "Please Mention The Reason!";
        //    return;
        //}
        string strCancelResult = objServiceHandler.UpdateCancelStatus(Session["UserLoginName"].ToString(), Session["DigitalKYC"].ToString(), txtRemarks.Text.Trim());

        if (strCancelResult.Equals("Successful"))
        {
            try
            {
                strMSISDN = "+88" + (txtCMobileNo.Text.ToString()).Substring(0, 11);
                string strSql = "SELECT SUBSTR(AGENT_ACCNT_NO,1,11) AGENT_ACCNT_NO FROM DIGITAL_KYC_INFO WHERE CLINT_MOBILE = '" + txtCMobileNo.Text.ToString() + "' AND ROWNUM = 1";
                string strAgentNumber = objServiceHandler.ReturnString(strSql);


                string strFrom = strMSISDN;
                string strTo = "MCOM_GATEWAY";
                string strMessage = "Sorry,Your MYCash Registration for the Mobile Number:" + txtCMobileNo.Text.ToString() + "has been CANCELED for WRONG Information.Plz contact your Agent:" + strAgentNumber + "for Details.MYCash";
                string strAccID = "";
                string strMessagePurpose = "MCOM_GATEWAY";
                string strRefNo = "";

                //string strDeleteResult = objServiceHandler.DeleteSingleAccount(strMSISDN);

                //if (strDeleteResult.Equals("Successfull."))
                //{
                    //objServiceHandler.ForwardMessage(strFrom, strTo, strMessage, strAccID, strMessagePurpose, strRefNo);
                    objServiceHandler.UpdateDeleteResult(Session["DigitalKYC"].ToString());
                    objServiceHandler.UpdateAccountState(strMSISDN, "L");
                //}
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        Session["DigitalKYC"] = null;
        Session["ItCanceled"] = true;
        Response.Redirect("../Common/frmDigitalKYC_Information.aspx", false);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Common/frmEKYC_Information.aspx", false);
    }
    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        return ms.ToArray();
    }
    private void LoadImage(DataRow objDataRow, string strImageField, string FilePath)
    {
        try
        {
            //FileStream fs = new FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //byte[] Image = new byte[fs.Length];
            //fs.Read(Image, 0, Convert.ToInt32(fs.Length));
            //fs.Close();
            //System.Drawing.Image img = System.Drawing.Image.FromFile(FilePath);
            //byte[] bArr = imageToByteArray(img);
            byte[] imageBytes = null;
            using (var webClient = new WebClient())
            {
                imageBytes = webClient.DownloadData(FilePath);
            }
            objDataRow[strImageField] = imageBytes;
        }
        catch (Exception ex)
        {
            //lblMessage2.Text = "NO Image found.";
        }
    }




}