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

public partial class COMMON_frmKYCUpdate : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    DataSet dsClientList;
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
        
        LoadNullSDS();
        LoadThanaDistrict();
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
    private void LoadThanaDistrict()
    {
        string strDistrict = "";
        strDistrict = " SELECT DISTRICT_ID ,DISTRICT_NAME FROM MANAGE_DISTRICT ";
        sdsDistrict.SelectCommand = strDistrict;
        sdsDistrict.DataBind();
        ddlDistrict.DataBind();

        string strThana = "";
        strThana = "SELECT THANA_ID,THANA_NAME FROM MANAGE_THANA WHERE DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "' ORDER BY THANA_ID";
        sdsThana.SelectCommand = strThana;
        sdsThana.DataBind();
        ddlThana.DataBind();



        string strRELATIONSHIP = "";
        strRELATIONSHIP = " SELECT RELATIONSHIP_ID ,RELATIONSHIP_TITLE FROM RELATIONSHIP ";
        sdsRELATIONSHIP.SelectCommand = strRELATIONSHIP;
        sdsRELATIONSHIP.DataBind();
        ddlRELATIONSHIP.DataBind();
    }
    private void LoadNullSDS()
    {       

        string strSqlBank = "";
        sdsBankAccount.SelectCommand = strSqlBank;
        sdsBankAccount.DataBind();
       // grdBankAccount.DataBind();

        string strSqlIden = "";
        sdsClientIdentification.SelectCommand = strSqlIden;
        sdsClientIdentification.DataBind();
        //grdClientIdentification.DataBind();

        string strSqlIntro = "";
        sdsIntroducerInformation.SelectCommand = strSqlIntro;
        sdsIntroducerInformation.DataBind();
        //grdIntroducerInfo.DataBind();

        string strSqlNominee = "";
        sdsNomineeInformation.SelectCommand = strSqlNominee;
        sdsNomineeInformation.DataBind();
       // grdNomineeInfo.DataBind();
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //    ddlThana.ClearSelection();        
    //    string strThana = "", strThanaID = "", strDistrictID = "";
    //    string strSql = "", strFatherName = "", strHusbandName = "", strIncompleteKYC = ""; 
    //    lblMsg.Text = "";
    //    string strSql3 = "";
    //    dsClientList = null;
    //    string strMobileNumber = "";
    //    if (txtWalletID.Text != "" && txtMobileNumber.Text != "")
    //    {
    //        lblMsg.Text = "Please Select Wallet ID or Mobile Number.";
    //        Clear();
    //        return;
    //    }
    //    else if (txtWalletID.Text == "" && txtMobileNumber.Text == "")
    //    {
    //        lblMsg.Text = "Please Select Wallet ID or Mobile Number.";
    //        Clear();
    //        return;
    //    }
    //    else if (txtWalletID.Text != "")
    //    {
    //        if (txtWalletID.Text.Length.Equals(12))
    //        {
    //            strMobileNumber = " SELECT CL.*,CM.SERIAL_NO FROM ACCOUNT_LIST AL,CLIENT_LIST CL,"
    //                           + " ACCOUNT_SERIAL_DETAIL CM WHERE AL.CLINT_ID=CL.CLINT_ID AND "
    //                           + " AL.ACCNT_MSISDN=CM.CUSTOMER_MOBILE_NO AND AL.ACCNT_NO='" + txtWalletID.Text.ToString() + "' ORDER BY TO_NUMBER(CM.SERIAL_NO) DESC";
    //        }
    //        else
    //        {
    //            lblMsg.Text = "Please isert correct Wallet ID.";
    //        }
    //    }
    //    else if (txtMobileNumber.Text != "")
    //    {
    //        strMobileNumber = "SELECT CL.*,CM.SERIAL_NO FROM ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_SERIAL_DETAIL CM "
    //                       + " WHERE AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_MSISDN=CM.CUSTOMER_MOBILE_NO"
    //                       + " AND AL.ACCNT_MSISDN='" + txtMobileNumber.Text.Trim() + "' ORDER BY TO_NUMBER(CM.SERIAL_NO) DESC";
    //    }
    //    strSql = strMobileNumber;
    //    dsClientList = objServiceHandler.GetClientList(strSql);
    //    //--------------------- Check CN Amount 25 taka -----------------------------
    //    string strChkWallet = "", strStatus = "", strChkStatus = "";
    //    if (txtWalletID.Text != "")
    //    {
    //        strChkWallet = txtWalletID.Text.Trim();
    //    }
    //    else
    //    {
    //        strChkWallet = txtMobileNumber.Text.Substring(3, 11) + "1";
    //    }

    //    strStatus = objServiceHandler.GetCNStatus(strChkWallet);

    //    if (Convert.ToInt32(strStatus) == 0)
    //    {
    //        strChkStatus = "N";
    //    }
    //    else if (Convert.ToInt32(strStatus) > 0)
    //    {
    //        strChkStatus = "Y";
    //    }
    //    //---------------------------------------------------------------------------
    //    //################ show data in text box #####################
    //    if (dsClientList.Tables[0].Rows.Count>0)
    //    {
    //        foreach (DataRow pRow in dsClientList.Tables[0].Rows)
    //        {

    //            txtClientID.Text = pRow["CLINT_ID"].ToString();
    //            txtClientMobile.Text = pRow["CLINT_MOBILE"].ToString();
    //            txtClientName.Text = pRow["CLINT_NAME"].ToString();

    //            strFatherName = pRow["CLINT_FATHER_NAME"].ToString();
    //            strHusbandName = pRow["HUSBAND_NAME"].ToString();
    //            strIncompleteKYC = pRow["INCOMPLETE_KYC"].ToString();
    //            rblInComptKYC.SelectedValue = strIncompleteKYC;
    //            rblCashIn.SelectedValue = strChkStatus;
    //            if (strFatherName != "")
    //            {

    //                txtFatherHusbandName.Text = strFatherName;
    //                rblFatherHusband.SelectedValue = "0";
    //            }
    //            else if (strHusbandName != "")
    //            {
    //                txtFatherHusbandName.Text = strHusbandName;
    //                rblFatherHusband.SelectedValue = "1";
    //            }
    //            //txtFathersName.Text = pRow["CLINT_FATHER_NAME"].ToString();
    //            txtMothersName.Text = pRow["CLINT_MOTHER_NAME"].ToString();
    //            dptDateOfBirth.DateString = pRow["CLIENT_DOB"].ToString();
    //            txtOccupation.Text = pRow["OCCUPATION"].ToString();
    //            txtWorkEduBusines.Text = pRow["WORK_EDU_BUSINESS"].ToString();
    //            ddlPurOfTran.SelectedValue = pRow["PUR_OF_TRAN"].ToString();
    //            ddlUISCAgent.SelectedValue = pRow["UISC_AGENT"].ToString();
    //            txtOffceAddress.Text = pRow["CLIENT_OFFIC_ADDRESS"].ToString();
    //            txtPreAddress.Text = pRow["CLINT_ADDRESS1"].ToString();
    //            txtPerAddress.Text = pRow["CLINT_ADDRESS2"].ToString();
    //            txtFormSerialNo.Text = pRow["SERIAL_NO"].ToString();
    //            rdoClientGender.SelectedValue = pRow["CLINT_GENDER"].ToString();
    //            //txtHusbandName.Text = pRow["HUSBAND_NAME"].ToString();
    //            try
    //            {
    //                 //############### add thana and district data #######################
    //                strThanaID = pRow["THANA_ID"].ToString();
    //                if (strThanaID != "")
    //                {
    //                    strDistrictID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_THANA", "DISTRICT_ID", "THANA_ID", strThanaID);

    //                    ddlDistrict.Items.Clear();
    //                    strSql3 = "SELECT  DISTRICT_ID ,DISTRICT_NAME FROM MANAGE_DISTRICT ";
    //                    sdsDistrict.SelectCommand = strSql3;
    //                    sdsDistrict.DataBind();
    //                    ddlDistrict.DataBind();
    //                    ddlDistrict.SelectedValue = strDistrictID;
    //                    ddlThana.Items.Clear();
    //                    strThana = "SELECT THANA_ID,THANA_NAME FROM MANAGE_THANA WHERE DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "' ORDER BY THANA_ID";
    //                    sdsThana.SelectCommand = strThana;
    //                    sdsThana.DataBind();
    //                    ddlThana.DataBind();
    //                    ddlThana.SelectedValue = strThanaID;
    //                }
    //                else
    //                {
    //                    ddlDistrict.Items.Clear();
    //                    ddlThana.Items.Clear();
    //                    LoadThanaDistrict();
    //                }
    //                  //#######################################
    //            }
    //            catch (Exception ex)
    //            {
    //                ex.Message.ToString();
    //            }                
    //        }
    //        LoadDataInGridView();
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Information Not Found.";
    //        Clear();
    //        LoadDataInGridView();
    //    }

    //     DataSet dsBank = null;
    //     dsBank = objServiceHandler.GetTableValue("BANK_ACCOUNT", "CLIENT_ID", txtClientID.Text.Trim());
    //     if (dsBank.Tables[0].Rows.Count > 0)
    //     {
    //         foreach (DataRow bRow in dsBank.Tables[0].Rows)
    //         {
    //             txtBankName.Text = bRow["BANK_NAME"].ToString();
    //             txtBranchName.Text = bRow["BANK_BR_NAME"].ToString();
    //             txtAccountNo.Text = bRow["BANK_ACCNT_NO"].ToString();
    //             txtBankRemarks.Text = bRow["REMARKS"].ToString();
    //         }
    //     }
    //     DataSet dsIden = null;
    //     dsIden = objServiceHandler.GetTableValue("CLIENT_IDENTIFICATION", "CLIENT_ID", txtClientID.Text.Trim());
    //     if (dsIden.Tables[0].Rows.Count > 0)
    //     {
    //         foreach (DataRow idRow in dsIden.Tables[0].Rows)
    //         {
    //             txtBankName.Text = idRow["CLINT_IDENT_NAME"].ToString();
    //             txtBankRemarks.Text = idRow["REMARKS"].ToString();
    //         }
    //     }
    //     DataSet dsIntro = null;
    //     dsIntro = objServiceHandler.GetTableValue("INTRODUCER_INFO", "CLIENT_ID", txtClientID.Text.Trim());
    //     if (dsIntro.Tables[0].Rows.Count > 0)
    //     {
    //         foreach (DataRow iRow in dsIntro.Tables[0].Rows)
    //         {
    //             txtIntroducerName.Text = iRow["INTRODCR_NAME"].ToString();
    //             txtIntroMobile.Text = iRow["INTRODCR_MOBILE"].ToString();
    //             txtIntroAddress.Text = iRow["INTRODCR_ADDRESS"].ToString();
    //             txtIntroOccupation.Text = iRow["INTRODCR_OCCUPATION"].ToString();
    //             txtIntroRemarks.Text = iRow["REMARKS"].ToString();
    //         }
    //     }
    //     DataSet dsNomnee = null;
    //     dsNomnee = objServiceHandler.GetTableValue("NOMINEE_INFO", "CLIENT_ID", txtClientID.Text.Trim());
    //     if (dsNomnee.Tables[0].Rows.Count > 0)
    //     {
    //         foreach (DataRow nRow in dsNomnee.Tables[0].Rows)
    //         {
    //             txtNomineeName.Text = nRow["NOMNE_NAME"].ToString();
    //             txtNomneMobile.Text = nRow["NOMNE_MOBILE"].ToString();
    //             txtNomneRelation.Text = nRow["RELATION"].ToString();
    //             txtNomnePrcent.Text = nRow["PERCENTAGE"].ToString();
    //             txtNomneRemarks.Text = nRow["REMARKS"].ToString();
    //         }
    //     }

    //    string strSqlIdentityType = " SELECT  IDS.IDNTIFCTION_ID, IDS.IDNTIFCTION_NAME FROM IDENTIFICATION_SETUP IDS, CLIENT_IDENTIFICATION CI "
    //                                + " WHERE IDS.IDNTIFCTION_ID = CI.IDNTIFCTION_ID AND CI.CLIENT_ID = '" + txtClientID.Text.Trim() + "'";
    //    sdsClientIdentificationSetUp.SelectCommand = strSqlIdentityType;
    //    sdsClientIdentificationSetUp.DataBind();
    //    ddlIdenName.DataBind();

    //    if (ddlIdenName.SelectedValue == "")
    //    {
    //        string strSqlIden = "";
    //        strSqlIden = " SELECT * FROM IDENTIFICATION_SETUP";
    //        sdsClientIdentificationSetUp.SelectCommand = strSqlIden;
    //        sdsClientIdentificationSetUp.DataBind();
    //        ddlIdenName.DataBind();
    //    }


    //    string strIdentityNo = objServiceHandler.GetIdentityNo(txtClientID.Text.Trim());
    //    txtIdenID.Text = strIdentityNo;

    //    }
    //    catch (Exception exception)
    //    {
    //        exception.Message.ToString();
    //    }


    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            ddlThana.ClearSelection();
            string strThana = "", strThanaID = "", strDistrictID = "";
            string strSql = "", strFatherName = "", strHusbandName = "", strIncompleteKYC = "";
            lblMsg.Text = "";
            string strSql3 = "";
            dsClientList = null;
            string strMobileNumber = "";
            if (txtWalletID.Text != "" && txtMobileNumber.Text != "")
            {
                lblMsg.Text = "Please Select Wallet ID or Mobile Number.";
                Clear();
                return;
            }
            else if (txtWalletID.Text == "" && txtMobileNumber.Text == "")
            {
                lblMsg.Text = "Please Select Wallet ID or Mobile Number.";
                Clear();
                return;
            }
            else if (txtWalletID.Text != "")
            {
                if (txtWalletID.Text.Length.Equals(12))
                {
                    //strMobileNumber = " SELECT CL.*,CM.SERIAL_NO FROM ACCOUNT_LIST AL,CLIENT_LIST CL,"
                    //               + " ACCOUNT_SERIAL_DETAIL CM WHERE AL.CLINT_ID=CL.CLINT_ID AND "
                    //               + " AL.ACCNT_MSISDN=CM.CUSTOMER_MOBILE_NO AND AL.ACCNT_NO='" + txtWalletID.Text.ToString() + "' ORDER BY TO_NUMBER(CM.SERIAL_NO) DESC";
                    strMobileNumber = "SELECT CL.CLINT_ID,CASE WHEN (CL.CLINT_NAME is null OR CL.CLINT_NAME ='' OR CL.CLINT_NAME LIKE '+8801%') THEN dki.CLINT_NAME ELSE CL.CLINT_NAME END CLINT_NAME,"

                        + "CASE WHEN (CL.CLINT_ADDRESS1 is null OR CL.CLINT_ADDRESS1 ='') THEN dki.CLIENT_PRE_ADDRESS ELSE CL.CLINT_ADDRESS1 END CLINT_ADDRESS1,"

                        + "CASE WHEN (CL.CLINT_ADDRESS2 is null OR CL.CLINT_ADDRESS2 ='') THEN dki.CLIENT_PER_ADDRESS ELSE CL.CLINT_ADDRESS2 END CLINT_ADDRESS2,"

                        + "CASE WHEN (CL.CLINT_PASS is null OR CL.CLINT_PASS ='') THEN '' ELSE CL.CLINT_ADDRESS1 END CLINT_PASS,"

                        + "CASE WHEN (CL.CLINT_MOBILE is null OR CL.CLINT_MOBILE ='' OR CL.CLINT_MOBILE LIKE '+88%') THEN dki.CLINT_MOBILE ELSE CL.CLINT_MOBILE END CLINT_MOBILE,"

                        + "CASE WHEN (CL.CLINT_N_ID is null OR CL.CLINT_N_ID ='') THEN dki.CLINT_NATIONAL_ID ELSE CL.CLINT_N_ID END CLINT_N_ID,"

                        + "CASE WHEN (CL.CLINT_TOWN is null OR CL.CLINT_TOWN ='') THEN '' ELSE CL.CLINT_TOWN END CLINT_TOWN,"

                        + "CASE WHEN (CL.CLINT_POSTCODE is null OR CL.CLINT_POSTCODE ='') THEN '' ELSE CL.CLINT_POSTCODE END CLINT_POSTCODE,"

                        + "CASE WHEN (CL.CLINT_CITY is null OR CL.CLINT_CITY ='') THEN '' ELSE CL.CLINT_CITY END CLINT_CITY,"

                        + "CASE WHEN (CL.COUNTRY_ID is null OR CL.COUNTRY_ID ='') THEN '' ELSE CL.COUNTRY_ID END COUNTRY_ID,"

                        + "CASE WHEN (CL.CLINT_CONTACT_F_NAME is null OR CL.CLINT_CONTACT_F_NAME ='') THEN '' ELSE CL.CLINT_CONTACT_F_NAME END CLINT_CONTACT_F_NAME,"

                        + "CASE WHEN (CL.CLINT_CONTACT_M_NAME is null OR CL.CLINT_CONTACT_M_NAME ='') THEN '' ELSE CL.CLINT_CONTACT_M_NAME END CLINT_CONTACT_M_NAME,"

                        + "CASE WHEN (CL.CLINT_CONTACT_L_NAME is null OR CL.CLINT_CONTACT_L_NAME ='') THEN '' ELSE CL.CLINT_CONTACT_L_NAME END CLINT_CONTACT_L_NAME,"

                        + "CASE WHEN (CL.CLINT_JOB_TITLE is null OR CL.CLINT_JOB_TITLE ='') THEN '' ELSE CL.CLINT_JOB_TITLE END CLINT_JOB_TITLE,"

                        + "CASE WHEN (CL.CLINT_CONTACT_EMAIL is null OR CL.CLINT_CONTACT_EMAIL ='') THEN '' ELSE CL.CLINT_CONTACT_EMAIL END CLINT_CONTACT_EMAIL,"

                        + "CASE WHEN (CL.CLINT_LAND_LINE is null OR CL.CLINT_LAND_LINE ='') THEN '' ELSE CL.CLINT_LAND_LINE END CLINT_LAND_LINE,"

                        + "CASE WHEN (CL.CLINT_FAX is null OR CL.CLINT_FAX ='') THEN '' ELSE CL.CLINT_FAX END CLINT_FAX,"

                        + "CASE WHEN (CL.CREATION_DATE is null OR CL.CREATION_DATE ='') THEN dki.REGISTRATION_DATE ELSE CL.CREATION_DATE END CREATION_DATE,"

                        + "CASE WHEN (CL.CLINT_TITLE is null OR CL.CLINT_TITLE ='') THEN '' ELSE CL.CLINT_TITLE END CLINT_TITLE,"

                        + "CASE WHEN (CL.CLINT_M_NAME is null OR CL.CLINT_M_NAME ='') THEN '' ELSE CL.CLINT_M_NAME END CLINT_M_NAME,"

                        + "CASE WHEN (CL.CLINT_L_NAME is null OR CL.CLINT_L_NAME ='') THEN '' ELSE CL.CLINT_L_NAME END CLINT_L_NAME,"

                        + "CASE WHEN (CL.CLINT_CONTACT_TITLE is null OR CL.CLINT_CONTACT_TITLE ='') THEN '' ELSE CL.CLINT_CONTACT_TITLE END CLINT_CONTACT_TITLE,"

                        + "CASE WHEN (CL.CLINT_GENDER is null OR CL.CLINT_GENDER ='') THEN '' ELSE CL.CLINT_GENDER END CLINT_GENDER,"

                        + "CASE WHEN (CL.CLINT_PASSPORT_NO is null OR CL.CLINT_PASSPORT_NO ='') THEN '' ELSE CL.CLINT_PASSPORT_NO END CLINT_PASSPORT_NO,"

                        + "CASE WHEN (CL.CLI_ZONE_ID is null OR CL.CLI_ZONE_ID ='') THEN '' ELSE CL.CLI_ZONE_ID END CLI_ZONE_ID,"

                        + "CASE WHEN (CL.CLINET_RSP_CODE is null OR CL.CLINET_RSP_CODE ='') THEN '' ELSE CL.CLINET_RSP_CODE END CLINET_RSP_CODE,"

                        + "CASE WHEN (CL.CLIENT_RSP_NAME is null OR CL.CLIENT_RSP_NAME ='') THEN '' ELSE CL.CLIENT_RSP_NAME END CLIENT_RSP_NAME,"

                        + "CASE WHEN (CL.DISTRIBUTOR_NAME is null OR CL.DISTRIBUTOR_NAME ='') THEN '' ELSE CL.DISTRIBUTOR_NAME END DISTRIBUTOR_NAME,"

                        + "CASE WHEN (CL.DISTRIBUTOR_CODE is null OR CL.DISTRIBUTOR_CODE ='') THEN '' ELSE CL.DISTRIBUTOR_CODE END DISTRIBUTOR_CODE,"

                        + "CASE WHEN (CL.DISTRIBUTOR_ZONE_ID is null OR CL.DISTRIBUTOR_ZONE_ID ='') THEN '' ELSE CL.DISTRIBUTOR_ZONE_ID END DISTRIBUTOR_ZONE_ID,"

                        + "CASE WHEN (CL.OWNER_NAME is null OR CL.OWNER_NAME ='') THEN '' ELSE CL.OWNER_NAME END OWNER_NAME,"

                        + "CASE WHEN (CL.OWNER_MOBILE is null OR CL.OWNER_MOBILE ='') THEN '' ELSE CL.OWNER_MOBILE END OWNER_MOBILE,"

                        + "CASE WHEN (CL.OWNER_NID is null OR CL.OWNER_NID ='') THEN '' ELSE CL.OWNER_NID END OWNER_NID,"

                        + "CASE WHEN (CL.ACCOUNT_NO is null OR CL.ACCOUNT_NO ='') THEN '' ELSE CL.ACCOUNT_NO END ACCOUNT_NO,"

                        + "CASE WHEN (CL.KTOKD is null OR CL.KTOKD ='') THEN '' ELSE CL.KTOKD END KTOKD,"

                        + "CASE WHEN (CL.KDGRP is null OR CL.KDGRP ='') THEN '' ELSE CL.KDGRP END KDGRP,"

                        + "CASE WHEN (CL.EASYLOAD_NO is null OR CL.EASYLOAD_NO ='') THEN '' ELSE CL.EASYLOAD_NO END EASYLOAD_NO,"

                        + "CASE WHEN (CL.NICK_NAME is null OR CL.NICK_NAME ='') THEN '' ELSE CL.NICK_NAME END NICK_NAME,"

                        + "CASE WHEN (CL.CLINT_OFFICE_PHONE is null OR CL.CLINT_OFFICE_PHONE ='') THEN '' ELSE CL.CLINT_OFFICE_PHONE END CLINT_OFFICE_PHONE,"




                     + "CASE WHEN (CL.CLINT_FATHER_NAME is null OR CL.CLINT_FATHER_NAME ='') THEN to_char(dki.CLINT_FATHER_NAME) ELSE CL.CLINT_FATHER_NAME END CLINT_FATHER_NAME,"

                     + "CASE WHEN (CL.CLINT_MOTHER_NAME is null OR CL.CLINT_MOTHER_NAME ='') THEN to_char(dki.CLINT_MOTHER_NAME) ELSE CL.CLINT_MOTHER_NAME END CLINT_MOTHER_NAME,"


                        + "CASE WHEN (CL.CLIENT_DOB is null OR CL.CLIENT_DOB ='') THEN dki.CLIENT_DOB ELSE CL.CLIENT_DOB END CLIENT_DOB,"

                        + "CASE WHEN (CL.CLIENT_OFFIC_ADDRESS is null OR CL.CLIENT_OFFIC_ADDRESS ='') THEN '' ELSE CL.CLIENT_OFFIC_ADDRESS END CLIENT_OFFIC_ADDRESS,"

                        + "CASE WHEN (CL.OCCUPATION is null OR CL.OCCUPATION ='') THEN dki.OCCUPATION_ID ELSE CL.OCCUPATION END OCCUPATION,"

                        + "CASE WHEN (CL.PUR_OF_TRAN is null OR CL.PUR_OF_TRAN ='') THEN '' ELSE CL.PUR_OF_TRAN END PUR_OF_TRAN,"

                        + "CASE WHEN (CL.REFERRED_MOBILE is null OR CL.REFERRED_MOBILE ='') THEN '' ELSE CL.REFERRED_MOBILE END REFERRED_MOBILE,"

                        + "CASE WHEN (CL.SPOUSE_TITEL is null OR CL.SPOUSE_TITEL ='') THEN '' ELSE CL.SPOUSE_TITEL END SPOUSE_TITEL,"

                        + "CASE WHEN (CL.SPOUSE_NAME is null OR CL.SPOUSE_NAME ='') THEN '' ELSE CL.SPOUSE_NAME END SPOUSE_NAME,"

                        + "CASE WHEN (CL.ACCOUNT_NAME is null OR CL.ACCOUNT_NAME ='') THEN '' ELSE CL.ACCOUNT_NAME END ACCOUNT_NAME, "

                        + "CASE WHEN (CL.WORK_EDU_BUSINESS is null OR CL.WORK_EDU_BUSINESS ='') THEN '' ELSE CL.WORK_EDU_BUSINESS END WORK_EDU_BUSINESS,"

                        + "CASE WHEN (CL.SYS_USR_LOGIN_NAME is null OR CL.SYS_USR_LOGIN_NAME ='') THEN '' ELSE CL.SYS_USR_LOGIN_NAME END SYS_USR_LOGIN_NAME,"

                        + "CASE WHEN (CL.UISC_AGENT is null OR CL.UISC_AGENT ='') THEN '' ELSE CL.UISC_AGENT END UISC_AGENT,"

                        + "CASE WHEN (CL.THANA_ID is null OR CL.THANA_ID ='') THEN dki.CLIENT_PRE_THANA_ID ELSE CL.THANA_ID END THANA_ID,"

                        + "CASE WHEN (CL.KYC_UPDATED_BY is null OR CL.KYC_UPDATED_BY ='') THEN '' ELSE CL.KYC_UPDATED_BY END KYC_UPDATED_BY,"

                        + "CASE WHEN (CL.UPDATE_DATE is null OR CL.UPDATE_DATE ='') THEN '' ELSE to_char(CL.UPDATE_DATE) END UPDATE_DATE,"

                        + "CASE WHEN (CL.REQUEST_PARTY_TYPE is null OR CL.REQUEST_PARTY_TYPE ='') THEN '' ELSE CL.REQUEST_PARTY_TYPE END REQUEST_PARTY_TYPE,"

                        + "CASE WHEN (CL.VERIFIED_BY is null OR CL.VERIFIED_BY ='') THEN '' ELSE CL.VERIFIED_BY END VERIFIED_BY,"

                        + "CASE WHEN (CL.VERIFIED_DATE is null OR CL.VERIFIED_DATE ='') THEN '' ELSE to_char(CL.VERIFIED_DATE) END VERIFIED_DATE,"

                        + "CASE WHEN (CL.HUSBAND_NAME is null OR CL.HUSBAND_NAME ='') THEN '' ELSE CL.HUSBAND_NAME END HUSBAND_NAME,"

                        + "CASE WHEN (CL.OTHER_IDENTIFICATION is null OR CL.OTHER_IDENTIFICATION ='') THEN '' ELSE CL.OTHER_IDENTIFICATION END OTHER_IDENTIFICATION,"

                        + "CASE WHEN (CL.INCOMPLETE_KYC is null OR CL.INCOMPLETE_KYC ='') THEN '' ELSE CL.INCOMPLETE_KYC END INCOMPLETE_KYC,"

                        + "CASE WHEN (CL.IS_BULK_REG is null OR CL.IS_BULK_REG ='') THEN '' ELSE CL.IS_BULK_REG END IS_BULK_REG,"

                        + "CASE WHEN (CL.THANA_ID_PERMANENT is null OR CL.THANA_ID_PERMANENT ='') THEN dki.CLIENT_PER_THANA_ID ELSE CL.THANA_ID_PERMANENT END THANA_ID_PERMANENT,"

                        + "CASE WHEN (CL.CUSTOMER_AREA is null OR CL.CUSTOMER_AREA ='') THEN '' ELSE CL.CUSTOMER_AREA END CUSTOMER_AREA,"

                        + "CASE WHEN (dki.SERIAL_NO is null OR dki.SERIAL_NO ='') THEN '' ELSE to_char(dki.SERIAL_NO) END SERIAL_NO, "

                        + "CASE WHEN dki.CLINT_MOBILE =substr('" + txtWalletID.Text.ToString() + "',1,11) THEN 'Registered By E-KYC:  Y' ELSE 'Registered By E-KYC:  N' END IS_EKYC "


                        + "FROM ACCOUNT_LIST AL LEFT JOIN CLIENT_LIST CL ON CL.CLINT_ID = AL.CLINT_ID "
                        + "LEFT JOIN DIGITAL_KYC_INFO dki ON dki.CLINT_MOBILE = SUBSTR(AL.ACCNT_NO, 1, 11) WHERE "
                        + "AL.ACCNT_NO='" + txtWalletID.Text.ToString() + "' AND rownum=1";

                }
                else
                {
                    lblMsg.Text = "Please isert correct Wallet ID.";
                }
            }
            else if (txtMobileNumber.Text != "")
            {
                if (txtMobileNumber.Text.Length.Equals(14))
                {
                    txtMobileNumber.Text = txtMobileNumber.Text.Substring(3, 11);
                }
                //strMobileNumber = " SELECT CL.*,CM.SERIAL_NO FROM ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_SERIAL_DETAIL CM"
                //               + " WHERE AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_MSISDN=CM.CUSTOMER_MOBILE_NO"
                //               + " AND AL.ACCNT_MSISDN='" + txtMobileNumber.Text.Trim() + "' ORDER BY TO_NUMBER(CM.SERIAL_NO) DESC";
                strMobileNumber = "SELECT CL.CLINT_ID, CASE WHEN (CL.CLINT_NAME is null OR CL.CLINT_NAME ='') THEN dki.CLINT_NAME ELSE CL.CLINT_NAME END CLINT_NAME,"

                       + "CASE WHEN (CL.CLINT_ADDRESS1 is null OR CL.CLINT_ADDRESS1 ='') THEN dki.CLIENT_PRE_ADDRESS ELSE CL.CLINT_ADDRESS1 END CLINT_ADDRESS1,"

                       + "CASE WHEN (CL.CLINT_ADDRESS2 is null OR CL.CLINT_ADDRESS2 ='') THEN dki.CLIENT_PER_ADDRESS ELSE CL.CLINT_ADDRESS2 END CLINT_ADDRESS2,"

                       + "CASE WHEN (CL.CLINT_PASS is null OR CL.CLINT_PASS ='') THEN '' ELSE CL.CLINT_ADDRESS1 END CLINT_PASS,"

                       + "CASE WHEN (CL.CLINT_MOBILE is null OR CL.CLINT_MOBILE ='' OR CL.CLINT_MOBILE LIKE '+88%') THEN dki.CLINT_MOBILE ELSE CL.CLINT_MOBILE END CLINT_MOBILE,"

                       + "CASE WHEN (CL.CLINT_N_ID is null OR CL.CLINT_N_ID ='') THEN dki.CLINT_NATIONAL_ID ELSE CL.CLINT_N_ID END CLINT_N_ID,"

                       + "CASE WHEN (CL.CLINT_TOWN is null OR CL.CLINT_TOWN ='') THEN '' ELSE CL.CLINT_TOWN END CLINT_TOWN,"

                       + "CASE WHEN (CL.CLINT_POSTCODE is null OR CL.CLINT_POSTCODE ='') THEN '' ELSE CL.CLINT_POSTCODE END CLINT_POSTCODE,"

                       + "CASE WHEN (CL.CLINT_CITY is null OR CL.CLINT_CITY ='') THEN '' ELSE CL.CLINT_CITY END CLINT_CITY,"

                       + "CASE WHEN (CL.COUNTRY_ID is null OR CL.COUNTRY_ID ='') THEN '' ELSE CL.COUNTRY_ID END COUNTRY_ID,"

                       + "CASE WHEN (CL.CLINT_CONTACT_F_NAME is null OR CL.CLINT_CONTACT_F_NAME ='') THEN '' ELSE CL.CLINT_CONTACT_F_NAME END CLINT_CONTACT_F_NAME,"

                       + "CASE WHEN (CL.CLINT_CONTACT_M_NAME is null OR CL.CLINT_CONTACT_M_NAME ='') THEN '' ELSE CL.CLINT_CONTACT_M_NAME END CLINT_CONTACT_M_NAME,"

                       + "CASE WHEN (CL.CLINT_CONTACT_L_NAME is null OR CL.CLINT_CONTACT_L_NAME ='') THEN '' ELSE CL.CLINT_CONTACT_L_NAME END CLINT_CONTACT_L_NAME,"

                       + "CASE WHEN (CL.CLINT_JOB_TITLE is null OR CL.CLINT_JOB_TITLE ='') THEN '' ELSE CL.CLINT_JOB_TITLE END CLINT_JOB_TITLE,"

                       + "CASE WHEN (CL.CLINT_CONTACT_EMAIL is null OR CL.CLINT_CONTACT_EMAIL ='') THEN '' ELSE CL.CLINT_CONTACT_EMAIL END CLINT_CONTACT_EMAIL,"

                       + "CASE WHEN (CL.CLINT_LAND_LINE is null OR CL.CLINT_LAND_LINE ='') THEN '' ELSE CL.CLINT_LAND_LINE END CLINT_LAND_LINE,"

                       + "CASE WHEN (CL.CLINT_FAX is null OR CL.CLINT_FAX ='') THEN '' ELSE CL.CLINT_FAX END CLINT_FAX,"

                       + "CASE WHEN (CL.CREATION_DATE is null OR CL.CREATION_DATE ='') THEN dki.REGISTRATION_DATE ELSE CL.CREATION_DATE END CREATION_DATE,"

                       + "CASE WHEN (CL.CLINT_TITLE is null OR CL.CLINT_TITLE ='') THEN '' ELSE CL.CLINT_TITLE END CLINT_TITLE,"

                       + "CASE WHEN (CL.CLINT_M_NAME is null OR CL.CLINT_M_NAME ='') THEN '' ELSE CL.CLINT_M_NAME END CLINT_M_NAME,"

                       + "CASE WHEN (CL.CLINT_L_NAME is null OR CL.CLINT_L_NAME ='') THEN '' ELSE CL.CLINT_L_NAME END CLINT_L_NAME,"

                       + "CASE WHEN (CL.CLINT_CONTACT_TITLE is null OR CL.CLINT_CONTACT_TITLE ='') THEN '' ELSE CL.CLINT_CONTACT_TITLE END CLINT_CONTACT_TITLE,"

                       + "CASE WHEN (CL.CLINT_GENDER is null OR CL.CLINT_GENDER ='') THEN '' ELSE CL.CLINT_GENDER END CLINT_GENDER,"

                       + "CASE WHEN (CL.CLINT_PASSPORT_NO is null OR CL.CLINT_PASSPORT_NO ='') THEN '' ELSE CL.CLINT_PASSPORT_NO END CLINT_PASSPORT_NO,"

                       + "CASE WHEN (CL.CLI_ZONE_ID is null OR CL.CLI_ZONE_ID ='') THEN '' ELSE CL.CLI_ZONE_ID END CLI_ZONE_ID,"

                       + "CASE WHEN (CL.CLINET_RSP_CODE is null OR CL.CLINET_RSP_CODE ='') THEN '' ELSE CL.CLINET_RSP_CODE END CLINET_RSP_CODE,"

                       + "CASE WHEN (CL.CLIENT_RSP_NAME is null OR CL.CLIENT_RSP_NAME ='') THEN '' ELSE CL.CLIENT_RSP_NAME END CLIENT_RSP_NAME,"

                       + "CASE WHEN (CL.DISTRIBUTOR_NAME is null OR CL.DISTRIBUTOR_NAME ='') THEN '' ELSE CL.DISTRIBUTOR_NAME END DISTRIBUTOR_NAME,"

                       + "CASE WHEN (CL.DISTRIBUTOR_CODE is null OR CL.DISTRIBUTOR_CODE ='') THEN '' ELSE CL.DISTRIBUTOR_CODE END DISTRIBUTOR_CODE,"

                       + "CASE WHEN (CL.DISTRIBUTOR_ZONE_ID is null OR CL.DISTRIBUTOR_ZONE_ID ='') THEN '' ELSE CL.DISTRIBUTOR_ZONE_ID END DISTRIBUTOR_ZONE_ID,"

                       + "CASE WHEN (CL.OWNER_NAME is null OR CL.OWNER_NAME ='') THEN '' ELSE CL.OWNER_NAME END OWNER_NAME,"

                       + "CASE WHEN (CL.OWNER_MOBILE is null OR CL.OWNER_MOBILE ='') THEN '' ELSE CL.OWNER_MOBILE END OWNER_MOBILE,"

                       + "CASE WHEN (CL.OWNER_NID is null OR CL.OWNER_NID ='') THEN '' ELSE CL.OWNER_NID END OWNER_NID,"

                       + "CASE WHEN (CL.ACCOUNT_NO is null OR CL.ACCOUNT_NO ='') THEN '' ELSE CL.ACCOUNT_NO END ACCOUNT_NO,"

                       + "CASE WHEN (CL.KTOKD is null OR CL.KTOKD ='') THEN '' ELSE CL.KTOKD END KTOKD,"

                       + "CASE WHEN (CL.KDGRP is null OR CL.KDGRP ='') THEN '' ELSE CL.KDGRP END KDGRP,"

                       + "CASE WHEN (CL.EASYLOAD_NO is null OR CL.EASYLOAD_NO ='') THEN '' ELSE CL.EASYLOAD_NO END EASYLOAD_NO,"

                       + "CASE WHEN (CL.NICK_NAME is null OR CL.NICK_NAME ='') THEN '' ELSE CL.NICK_NAME END NICK_NAME,"

                       + "CASE WHEN (CL.CLINT_OFFICE_PHONE is null OR CL.CLINT_OFFICE_PHONE ='') THEN '' ELSE CL.CLINT_OFFICE_PHONE END CLINT_OFFICE_PHONE,"

                       + "CASE WHEN (CL.CLINT_FATHER_NAME is null OR CL.CLINT_FATHER_NAME ='') THEN to_char(dki.CLINT_FATHER_NAME) ELSE CL.CLINT_FATHER_NAME END CLINT_FATHER_NAME,"

                       + "CASE WHEN (CL.CLINT_MOTHER_NAME is null OR CL.CLINT_MOTHER_NAME ='') THEN to_char(dki.CLINT_MOTHER_NAME) ELSE CL.CLINT_MOTHER_NAME END CLINT_MOTHER_NAME,"


                       + "CASE WHEN (CL.CLIENT_DOB is null OR CL.CLIENT_DOB ='') THEN dki.CLIENT_DOB ELSE CL.CLIENT_DOB END CLIENT_DOB,"

                       + "CASE WHEN (CL.CLIENT_OFFIC_ADDRESS is null OR CL.CLIENT_OFFIC_ADDRESS ='') THEN '' ELSE CL.CLIENT_OFFIC_ADDRESS END CLIENT_OFFIC_ADDRESS,"

                       + "CASE WHEN (CL.OCCUPATION is null OR CL.OCCUPATION ='') THEN dki.OCCUPATION_ID ELSE CL.OCCUPATION END OCCUPATION,"

                       + "CASE WHEN (CL.PUR_OF_TRAN is null OR CL.PUR_OF_TRAN ='') THEN '' ELSE CL.PUR_OF_TRAN END PUR_OF_TRAN,"

                       + "CASE WHEN (CL.REFERRED_MOBILE is null OR CL.REFERRED_MOBILE ='') THEN '' ELSE CL.REFERRED_MOBILE END REFERRED_MOBILE,"

                       + "CASE WHEN (CL.SPOUSE_TITEL is null OR CL.SPOUSE_TITEL ='') THEN '' ELSE CL.SPOUSE_TITEL END SPOUSE_TITEL,"

                       + "CASE WHEN (CL.SPOUSE_NAME is null OR CL.SPOUSE_NAME ='') THEN '' ELSE CL.SPOUSE_NAME END SPOUSE_NAME,"

                       + "CASE WHEN (CL.ACCOUNT_NAME is null OR CL.ACCOUNT_NAME ='') THEN '' ELSE CL.ACCOUNT_NAME END ACCOUNT_NAME, "

                       + "CASE WHEN (CL.WORK_EDU_BUSINESS is null OR CL.WORK_EDU_BUSINESS ='') THEN '' ELSE CL.WORK_EDU_BUSINESS END WORK_EDU_BUSINESS,"

                       + "CASE WHEN (CL.SYS_USR_LOGIN_NAME is null OR CL.SYS_USR_LOGIN_NAME ='') THEN '' ELSE CL.SYS_USR_LOGIN_NAME END SYS_USR_LOGIN_NAME,"

                       + "CASE WHEN (CL.UISC_AGENT is null OR CL.UISC_AGENT ='') THEN '' ELSE CL.UISC_AGENT END UISC_AGENT,"

                       + "CASE WHEN (CL.THANA_ID is null OR CL.THANA_ID ='') THEN dki.CLIENT_PRE_THANA_ID ELSE CL.THANA_ID END THANA_ID,"

                       + "CASE WHEN (CL.KYC_UPDATED_BY is null OR CL.KYC_UPDATED_BY ='') THEN '' ELSE CL.KYC_UPDATED_BY END KYC_UPDATED_BY,"

                       + "CASE WHEN (CL.UPDATE_DATE is null OR CL.UPDATE_DATE ='') THEN '' ELSE to_char(CL.UPDATE_DATE) END UPDATE_DATE,"

                       + "CASE WHEN (CL.REQUEST_PARTY_TYPE is null OR CL.REQUEST_PARTY_TYPE ='') THEN '' ELSE CL.REQUEST_PARTY_TYPE END REQUEST_PARTY_TYPE,"

                       + "CASE WHEN (CL.VERIFIED_BY is null OR CL.VERIFIED_BY ='') THEN '' ELSE CL.VERIFIED_BY END VERIFIED_BY,"

                       + "CASE WHEN (CL.VERIFIED_DATE is null OR CL.VERIFIED_DATE ='') THEN '' ELSE to_char(CL.VERIFIED_DATE) END VERIFIED_DATE,"

                       + "CASE WHEN (CL.HUSBAND_NAME is null OR CL.HUSBAND_NAME ='') THEN '' ELSE CL.HUSBAND_NAME END HUSBAND_NAME,"

                       + "CASE WHEN (CL.OTHER_IDENTIFICATION is null OR CL.OTHER_IDENTIFICATION ='') THEN '' ELSE CL.OTHER_IDENTIFICATION END OTHER_IDENTIFICATION,"

                       + "CASE WHEN (CL.INCOMPLETE_KYC is null OR CL.INCOMPLETE_KYC ='') THEN '' ELSE CL.INCOMPLETE_KYC END INCOMPLETE_KYC,"

                       + "CASE WHEN (CL.IS_BULK_REG is null OR CL.IS_BULK_REG ='') THEN '' ELSE CL.IS_BULK_REG END IS_BULK_REG,"


                       + "CASE WHEN (CL.THANA_ID_PERMANENT is null OR CL.THANA_ID_PERMANENT ='') THEN dki.CLIENT_PER_THANA_ID ELSE CL.THANA_ID_PERMANENT END THANA_ID_PERMANENT,"

                       + "CASE WHEN (CL.CUSTOMER_AREA is null OR CL.CUSTOMER_AREA ='') THEN '' ELSE CL.CUSTOMER_AREA END CUSTOMER_AREA,"

                       + "CASE WHEN (dki.SERIAL_NO is null OR dki.SERIAL_NO ='') THEN '' ELSE to_char(dki.SERIAL_NO) END SERIAL_NO, "

                        + "CASE WHEN dki.CLINT_MOBILE ='" + txtMobileNumber.Text.ToString() + "' THEN 'Registered By E-KYC:  Y' ELSE 'Registered By E-KYC:  N' END IS_EKYC "

                       + "FROM ACCOUNT_LIST AL LEFT JOIN CLIENT_LIST CL ON CL.CLINT_ID = AL.CLINT_ID "
                       + "LEFT JOIN DIGITAL_KYC_INFO dki ON dki.CLINT_MOBILE = SUBSTR(AL.ACCNT_NO, 1, 11) WHERE "
                       + "AL.ACCNT_MSISDN='" + "+88" + txtMobileNumber.Text.ToString() + "' AND rownum=1";
            }
            strSql = strMobileNumber;
            dsClientList = objServiceHandler.GetClientList(strSql);
            //--------------------- Check CN Amount 25 taka -----------------------------
            string strChkWallet = "", strStatus = "", strChkStatus = "";
            if (txtWalletID.Text != "")
            {
                strChkWallet = txtWalletID.Text.Trim();
            }
            else
            {
                  strChkWallet = txtMobileNumber.Text + "1";
            }


            strStatus = objServiceHandler.GetCNStatus(strChkWallet);

            if (Convert.ToInt32(strStatus) == 0)
            {
                strChkStatus = "N";
            }
            else if (Convert.ToInt32(strStatus) > 0)
            {
                strChkStatus = "Y";
            }
            //---------------------------------------------------------------------------
            //################ show data in text box #####################
            if (dsClientList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow pRow in dsClientList.Tables[0].Rows)
                {

                    txtClientID.Text = pRow["CLINT_ID"].ToString();
                    txtClientMobile.Text = pRow["CLINT_MOBILE"].ToString();
                    txtClientName.Text = pRow["CLINT_NAME"].ToString();

                    strFatherName = pRow["CLINT_FATHER_NAME"].ToString();
                    strHusbandName = pRow["HUSBAND_NAME"].ToString();
                    strIncompleteKYC = pRow["INCOMPLETE_KYC"].ToString();
                    rblInComptKYC.SelectedValue = strIncompleteKYC;
                    rblCashIn.SelectedValue = strChkStatus;
                    if (strFatherName != "")
                    {

                        txtFatherHusbandName.Text = strFatherName;
                        rblFatherHusband.SelectedValue = "0";
                    }
                    else if (strHusbandName != "")
                    {
                        txtFatherHusbandName.Text = strHusbandName;
                        rblFatherHusband.SelectedValue = "1";
                    }
                    //txtFathersName.Text = pRow["CLINT_FATHER_NAME"].ToString();
                    txtMothersName.Text = pRow["CLINT_MOTHER_NAME"].ToString();
                    dptDateOfBirth.DateString = pRow["CLIENT_DOB"].ToString();
                    txtOccupation.Text = pRow["OCCUPATION"].ToString();
                    txtWorkEduBusines.Text = pRow["WORK_EDU_BUSINESS"].ToString();
                    ddlPurOfTran.SelectedValue = pRow["PUR_OF_TRAN"].ToString();
                    if (!string.IsNullOrEmpty(pRow["UISC_AGENT"].ToString()))
                    {
                        ddlUISCAgent.SelectedValue = pRow["UISC_AGENT"].ToString();
                    }
                    
                    txtOffceAddress.Text = pRow["CLIENT_OFFIC_ADDRESS"].ToString();
                    txtPreAddress.Text = pRow["CLINT_ADDRESS1"].ToString();
                    txtPerAddress.Text = pRow["CLINT_ADDRESS2"].ToString();
                    txtFormSerialNo.Text = pRow["SERIAL_NO"].ToString();
                    rdoClientGender.SelectedValue = pRow["CLINT_GENDER"].ToString();
                    //txtHusbandName.Text = pRow["HUSBAND_NAME"].ToString();
                    try
                    {
                        //############### add thana and district data #######################
                        strThanaID = pRow["THANA_ID"].ToString();
                        if (strThanaID != "")
                        {
                            strDistrictID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_THANA", "DISTRICT_ID", "THANA_ID", strThanaID);

                            ddlDistrict.Items.Clear();
                            strSql3 = "SELECT  DISTRICT_ID ,DISTRICT_NAME FROM MANAGE_DISTRICT ";
                            sdsDistrict.SelectCommand = strSql3;
                            sdsDistrict.DataBind();
                            ddlDistrict.DataBind();
                            ddlDistrict.SelectedValue = strDistrictID;
                            ddlThana.Items.Clear();
                            strThana = "SELECT THANA_ID,THANA_NAME FROM MANAGE_THANA WHERE DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "' ORDER BY THANA_ID";
                            sdsThana.SelectCommand = strThana;
                            sdsThana.DataBind();
                            ddlThana.DataBind();
                            ddlThana.SelectedValue = strThanaID;
                        }
                        else
                        {
                            ddlDistrict.Items.Clear();
                            ddlThana.Items.Clear();
                            LoadThanaDistrict();
                        }
                        //#######################################
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                }
                LoadDataInGridView();
            }
            else
            {
                lblMsg.Text = "Information Not Found.";
                Clear();
                LoadDataInGridView();
            }

            DataSet dsBank = null;
            dsBank = objServiceHandler.GetTableValue("BANK_ACCOUNT", "CLIENT_ID", txtClientID.Text.Trim());
            if (dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow bRow in dsBank.Tables[0].Rows)
                {
                    txtEIdenName.Text = bRow["BANK_NAME"].ToString();
                    txtBranchName.Text = bRow["BANK_BR_NAME"].ToString();
                    txtAccountNo.Text = bRow["BANK_ACCNT_NO"].ToString();
                    txtBankRemarks.Text = bRow["REMARKS"].ToString();
                }
            }
            DataSet dsIden = null;
            dsIden = objServiceHandler.GetTableValue("CLIENT_IDENTIFICATION", "CLIENT_ID", txtClientID.Text.Trim());
            if (dsIden.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow idRow in dsIden.Tables[0].Rows)
                {
                    txtEIdenName.Text = idRow["CLINT_IDENT_NAME"].ToString();
                    txtBankRemarks.Text = idRow["REMARKS"].ToString();
                }
            }
            else
            {
                dsIden = objServiceHandler.GetTableValue("DIGITAL_KYC_INFO", "CLINT_MOBILE", txtClientMobile.Text.Trim());
                if (dsIden.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow idRow in dsIden.Tables[0].Rows)
                    {
                        txtIdenID.Text = idRow["CLINT_NATIONAL_ID"].ToString();
                        //txtBankRemarks.Text = idRow["REMARKS"].ToString();
                    }
                }
            }
            DataSet dsIntro = null;
            dsIntro = objServiceHandler.GetTableValue("INTRODUCER_INFO", "CLIENT_ID", txtClientID.Text.Trim());
            if (dsIntro.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow iRow in dsIntro.Tables[0].Rows)
                {
                    txtIntroducerName.Text = iRow["INTRODCR_NAME"].ToString();
                    txtIntroMobile.Text = iRow["INTRODCR_MOBILE"].ToString();
                    txtIntroAddress.Text = iRow["INTRODCR_ADDRESS"].ToString();
                    txtIntroOccupation.Text = iRow["INTRODCR_OCCUPATION"].ToString();
                    txtIntroRemarks.Text = iRow["REMARKS"].ToString();
                }
            }
            DataSet dsNomnee = null;
            dsNomnee = objServiceHandler.GetTableValue("NOMINEE_INFO", "CLIENT_ID", txtClientID.Text.Trim());
            if (dsNomnee.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow nRow in dsNomnee.Tables[0].Rows)
                {
                    txtNomineeName.Text = nRow["NOMNE_NAME"].ToString();
                    txtNomneMobile.Text = nRow["NOMNE_MOBILE"].ToString();
                    txtNomneRelation.Text = nRow["RELATION"].ToString();
                    ddlRELATIONSHIP.SelectedValue = "1907030000000004";
                    txtNomnePrcent.Text = nRow["PERCENTAGE"].ToString();
                    txtNomneRemarks.Text = nRow["REMARKS"].ToString();
                }
            }
            else
            {
                dsNomnee = objServiceHandler.GetTableValue("DIGITAL_KYC_INFO", "CLINT_MOBILE", txtClientMobile.Text.Trim());
                if (dsNomnee.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow nRow in dsNomnee.Tables[0].Rows)
                    {
                        txtNomineeName.Text = nRow["NOMINEE_NAME"].ToString();
                        txtNomneRelation.Text = nRow["RELATIONSHIP_ID"].ToString();
                        txtNomnePrcent.Text = nRow["NOMINEE_PERCENTAGE"].ToString();
                        
                    }
                }
            }
            string strSqlIdentityType = " SELECT  IDS.IDNTIFCTION_ID, IDS.IDNTIFCTION_NAME FROM IDENTIFICATION_SETUP IDS, CLIENT_IDENTIFICATION CI "
                                        + " WHERE IDS.IDNTIFCTION_ID = CI.IDNTIFCTION_ID AND CI.CLIENT_ID = '" + txtClientID.Text.Trim() + "'";
            sdsClientIdentificationSetUp.SelectCommand = strSqlIdentityType;
            sdsClientIdentificationSetUp.DataBind();
            ddlIdenName.DataBind();

            if (ddlIdenName.SelectedValue == "")
            {
                string strSqlIdentityType1 = "SELECT  IDS.IDNTIFCTION_ID, IDS.IDNTIFCTION_NAME FROM IDENTIFICATION_SETUP IDS, DIGITAL_KYC_INFO DKI  WHERE IDS.IDNTIFCTION_ID = DKI.IDENTITY_TYPE AND DKI.CLINT_MOBILE= '" + txtClientMobile.Text.Trim() + "'";
                sdsClientIdentificationSetUp.SelectCommand = strSqlIdentityType1;
                sdsClientIdentificationSetUp.DataBind();
                ddlIdenName.DataBind();
                if (ddlIdenName.SelectedValue == "")
                {
                    string strSqlIden = "";
                    strSqlIden = " SELECT * FROM IDENTIFICATION_SETUP";
                    sdsClientIdentificationSetUp.SelectCommand = strSqlIden;
                    sdsClientIdentificationSetUp.DataBind();
                    ddlIdenName.DataBind();
                }
                    
            }

            if (string.IsNullOrEmpty(txtIdenID.Text))
            {
                string strIdentityNo = objServiceHandler.GetIdentityNo(txtClientID.Text.Trim());
                txtIdenID.Text = strIdentityNo;
            }
          

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }


    }

    private void LoadDataInGridView()
    {
        string strSqlBank = "";
        strSqlBank = " SELECT * FROM BANK_ACCOUNT WHERE CLIENT_ID='" + txtClientID.Text.ToString() + "'";
        sdsBankAccount.SelectCommand = strSqlBank;
        sdsBankAccount.DataBind();

        string strSqlIden = "";
        strSqlIden = " SELECT * FROM CLIENT_IDENTIFICATION WHERE CLIENT_ID='" + txtClientID.Text.ToString() + "'";
        sdsClientIdentification.SelectCommand = strSqlIden;
        sdsClientIdentification.DataBind();

        string strSqlIntro = "";
        strSqlIntro = " SELECT * FROM INTRODUCER_INFO WHERE CLIENT_ID='" + txtClientID.Text.ToString() + "'";
        sdsIntroducerInformation.SelectCommand = strSqlIntro;
        sdsIntroducerInformation.DataBind();

        string strSqlNominee = "";
        strSqlNominee = " SELECT * FROM NOMINEE_INFO WHERE CLIENT_ID='" + txtClientID.Text.ToString() + "'";
        sdsNomineeInformation.SelectCommand = strSqlNominee;
        sdsNomineeInformation.DataBind();
    }
        //############### clear all control ##################### 
    private void Clear()
    {
        txtClientID.Text = "";
        txtClientMobile.Text = "";
        txtClientName.Text = "";
        txtFormSerialNo.Text = "";
        txtFatherHusbandName.Text = "";

        rblFatherHusband.SelectedValue = "";
        txtMothersName.Text = "";       
        txtOccupation.Text = "";
        txtWorkEduBusines.Text = "";
        ddlPurOfTran.SelectedValue = "";
        ddlUISCAgent.SelectedIndex = -1;
        ddlDistrict.Items.Clear();
        ddlThana.Items.Clear();
        txtOffceAddress.Text = "";
        txtPreAddress.Text = "";
        txtPerAddress.Text = "";
        txtWalletID.Text = "";
        txtMobileNumber.Text = "";

        txtEIdenName.Text = "";
        txtBranchName.Text = "";
        txtAccountNo.Text = "";
        txtBankRemarks.Text = "";

        txtIdenID.Text = "";
        txtIdenRemarks.Text = "";

        txtIntroducerName.Text = "";
        txtIntroMobile.Text = "";
        txtIntroAddress.Text = "";
        txtIntroOccupation.Text = "";
        txtIntroRemarks.Text = "";

        txtNomineeName.Text = "";
        txtNomneMobile.Text = "";
        txtNomneRelation.Text = "";
        txtNomnePrcent.Text = "";
        txtNomneRemarks.Text = "";
    }
    protected void txtMobileNumber_TextChanged(object sender, EventArgs e)
    {
        ShowRelatedData(txtMobileNumber.Text.ToString());
    }
    private void ShowRelatedData(string strSearch)
    {
        
    }
    //override protected void OnInit(EventArgs e)
    //{
    //    txtUpdate.Attributes.Add("onclick", "javascript:" +
    //              txtUpdate.ClientID + ".disabled=true;" +
    //              this.GetPostBackEventReference(txtUpdate));
    //    //InitializeComponent();
    //    base.OnInit(e);
    //}
    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            string strSql = "SELECT DISTINCT AL.ACCNT_NO "
                        + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL, CLIENT_IDENTIFICATION CI, IDENTIFICATION_SETUP IDS "
                        + " WHERE CI.CLINT_IDENT_NAME = '" + txtIdenID.Text.Trim().ToString() + "' AND CI.IDNTIFCTION_ID = '" + ddlIdenName.SelectedValue.ToString() + "' "
                        + " AND AL.CLINT_ID = CL.CLINT_ID AND CL.CLINT_ID = CI.CLIENT_ID AND CI.IDNTIFCTION_ID = IDS.IDNTIFCTION_ID";
            DataSet ods1 = objServiceHandler.CheckNID(strSql);
            string mobile = "";
            mobile = txtMobileNumber.Text;
            if (mobile == "")
            {
                mobile = txtWalletID.Text.Substring(0, 11);
            }
            string strSql2 = "select * from DIGITAL_KYC_INFO where IS_UPDATE='N' AND IS_REGISTER='Y' AND CLINT_MOBILE='" + mobile + "'";
            DataSet ods2 = objServiceHandler.CheckNID(strSql2);
            if (ods2.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "Information do not update before account verified";
                return;
            }
            try
            {
                if (ods1.Tables[0].Rows.Count > 0)
                {
                    string account = "";
                    foreach (DataRow prow in ods1.Tables[0].Rows)
                    {
                        account = prow["ACCNT_NO"].ToString();
                        break;
                    }

                    Page page = HttpContext.Current.Handler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + "The NID: " + txtIdenID.Text.Trim().ToString() + " is already used in MYCash Account: " + account + "; " + "');", true);
                    return;
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        

        bool blnClient = false, blnBank = false, blnIden = false, blnInto = false, blnNomnee = false;
        string strUpdateMessage = "", strDateTime = "";
        strDateTime = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", DateTime.Now);

        if (txtIdenID.Text == "")
        {
                #region
                //if (ddlIdenName.SelectedItem.Text == "NATIONAL ID")
                //{
                //    if (txtIdenID.Text.Length == 13)
                //    {
                //        blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                //                                                    ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                //        //if (blnIden != blnIden)
                //        //{
                //        //    return;
                //        //}
                //    }

                //    else if (txtIdenID.Text.Length == 17)
                //    {
                //        blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                //                                                    ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                //        //if (blnIden != blnIden)
                //        //{
                //        //    return;
                //        //}
                //    }

                //    else
                //    {
                //        lblMsg.Text = "National Id Must be 13 or 17 Digit in Length";
                //        return;
                //    }
                //
                #endregion

            lblMsg.Text = "Identity Could not be Null";
            return;

        }

        if (rdoClientGender.SelectedValue != null)
        {
            try
            {
                string strClientGender = rdoClientGender.SelectedValue;
                objServiceHandler.UpdateClientGender(txtClientID.Text.Trim(), strClientGender);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        if (txtIdenID.Text != "")
        {
            string strSuccMsg = "";
            string strClientIdIfExist = "";
            strClientIdIfExist = objServiceHandler.ClientIdIfExist(txtClientID.Text.Trim());
            if (strClientIdIfExist == "")
            {
                if (ddlIdenName.SelectedItem.Text == "NATIONAL ID")
                {
                    if (txtIdenID.Text.Length == 13)
                    {
                        blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(), ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                    }

                    else if (txtIdenID.Text.Length == 17)
                    {
                        blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(), ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                    }

                    else
                    {
                        lblMsg.Text = "National Id Must be 13 or 17 Digit in Length";
                        return;
                    }
                }
                else if (ddlIdenName.SelectedItem.Text == "SMART CARD")
                {
                    if (txtIdenID.Text.Length == 10)
                    {
                        blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(), ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                    }
                    else
                    {
                        lblMsg.Text = "Smart Card Must be 10 Digit in Length";
                        return;
                    }
                }
                else
                {
                    blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(), ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                }
            }

            else
            {
                if (ddlIdenName.SelectedItem.Text == "NATIONAL ID")
                {
                    if (txtIdenID.Text.Length == 13)
                    {
                        strSuccMsg = objServiceHandler.UpdateClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                                                                   ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                        if (strSuccMsg != "Successfull.")
                        {
                            return;
                        }
                    }

                    else if (txtIdenID.Text.Length == 17)
                    {
                        strSuccMsg = objServiceHandler.UpdateClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                                                                    ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                        if (strSuccMsg != "Successfull.")
                        {
                            return;
                        }
                    }

                    else
                    {
                        lblMsg.Text = "National Id Must be 13 or 17 Digit in Length";
                        return;
                    }
                }
                else if (ddlIdenName.SelectedItem.Text == "SMART CARD")
                {
                    if (txtIdenID.Text.Length == 10)
                    {
                        strSuccMsg = objServiceHandler.UpdateClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                                                                   ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                        if (strSuccMsg != "Successfull.")
                        {
                            return;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Smart Card Must be 10 Digit in Length";
                        return;
                    }
                }
                else
                {
                    strSuccMsg = objServiceHandler.UpdateClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                                                                    ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                }
            }
        }

        else
        {
            lblMsg.Text = "Client Id is not valid";
        }




        if (txtClientID.Text != "")
        {
            if (ddlThana.SelectedItem.ToString() != "Select a Thana")
            {
                if (txtClientMobile.Text.Trim() != "")
                {
                    strUpdateMessage = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("CLIENT_LIST", "KYC_UPDATED_BY", "CLINT_ID", txtClientID.Text.Trim());
                    //-------------------- checking for father's or husband name ----------------
                    string strFatherName="",strHusbandName="";
                    if (rblFatherHusband.SelectedValue == "0")
                    {
                        strFatherName = txtFatherHusbandName.Text.ToString();
                    }
                    else if (rblFatherHusband.SelectedValue == "1")
                    {
                        strHusbandName = txtFatherHusbandName.Text.ToString();
                    }
                    //--------------------------------------------------------------------------
                    blnClient = objServiceHandler.UpdateClientList(txtClientName.Text.Trim(), strFatherName, 
                                                  txtMothersName.Text.Trim(), dptDateOfBirth.DateString,
                                                  txtOccupation.Text.Trim(), txtWorkEduBusines.Text.Trim(),
                                                  ddlPurOfTran.SelectedValue, ddlUISCAgent.SelectedValue, txtOffceAddress.Text.Trim(),
                                                  txtPreAddress.Text.Trim(), txtPerAddress.Text.Trim(),
                                                  ddlThana.SelectedValue.ToString(), txtClientID.Text.Trim(),
                                                  Session["AccountID"].ToString(), strDateTime, "WEB", strUpdateMessage, strHusbandName, rblInComptKYC.SelectedValue.ToString());
                }
                else
                {
                    blnClient = true;
                }
                if (blnClient == true)
                {
                    if (txtEIdenName.Text != "" && txtBranchName.Text != "")
                    {
                        blnBank = objServiceHandler.SaveBankAccount(txtClientID.Text.Trim(), txtEIdenName.Text.Trim(), txtBranchName.Text.Trim(),
                                          txtAccountNo.Text.Trim(), txtBankRemarks.Text.Trim());
                    }
                    else
                    {
                        blnBank = true;
                    }
                    if (blnBank == true)
                    {
                        if (txtIdenID.Text != "")
                        {
                            //if (ddlIdenName.SelectedItem.Text == "NATIONAL ID")
                            //{
                            //    if (txtIdenID.Text.Length == 13)
                            //    {
                            //        blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                            //                                                    ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                            //    }

                            //    else if (txtIdenID.Text.Length == 17)
                            //    {
                            //        blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                            //                                                    ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                            //    }

                            //    else
                            //    {
                            //        lblMsg.Text = "National Id Must be 13 or 17 Digit";
                            //    }
                            //}

                            //else
                            //{
                            //    blnIden = objServiceHandler.SaveClientIdentification(txtClientID.Text.Trim(), txtIdenID.Text.Trim(),
                            //                                                    ddlIdenName.SelectedValue.ToString(), txtIdenRemarks.Text.Trim());
                            //}

                            blnIden = true;


                        }
                        else
                        {
                            blnIden = true;
                        }
                        if (blnIden == true)
                        {
                            if (txtIntroducerName.Text != "")
                            {
                                blnInto = objServiceHandler.SaveIntroducerInfo(txtClientID.Text.Trim(), txtIntroducerName.Text.Trim(),
                                                         txtIntroMobile.Text.Trim(), txtIntroAddress.Text.Trim(), txtIntroOccupation.Text.Trim(),
                                                         txtIntroRemarks.Text.Trim());
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

                                    if (txtNomnePrcent.Text != "")
                                    {
                                        dcmPercent = Convert.ToDecimal(txtNomnePrcent.Text);
                                    }
                                    blnNomnee = objServiceHandler.SaveNomineeInfo(txtClientID.Text.Trim(), txtNomineeName.Text.Trim(),
                                                                   txtNomneMobile.Text.Trim(), txtNomneRelation.Text.Trim(), dcmPercent,
                                                                   txtNomneRemarks.Text.Trim());
                                }
                                else
                                {
                                    blnNomnee = true;
                                }
                                if (blnNomnee == true)
                                {
                                    lblMsg.Text = "All Information Saved Successfully.";
                                    SaveAuditInfo("Update", "KYC Update,Update_No=" + txtClientMobile.Text.ToString() + "");
                                    Clear();
                                    LoadDataInGridView();
                                    Clear();
                                    LoadThanaDistrict();
                                    
                                }
                                else
                                {
                                    lblMsg.Text = "Nominee Information Not Saved.";
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Introducer Information Not Saved.";
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Client Identification Not Saved.";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Bank Account Not Saved.";
                    }
                }
                else
                {
                    lblMsg.Text = "Client Information Not Updated.";
                }
            }
            else
            {
                lblMsg.Text = "Please select a thana and district.";
            }
        }
        else
        {
            lblMsg.Text = "Please select a wallet";
        }

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }

    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void grdBankAccount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        SelectBankInfo();
    }
    protected void grdBankAccount_RowEditing(object sender, GridViewEditEventArgs e)
    {
        SelectBankInfo();
    }
    protected void grdBankAccount_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SelectBankInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdClientIdentification_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        SelectIdenInfo();
    }
    protected void grdClientIdentification_RowEditing(object sender, GridViewEditEventArgs e)
    {
        SelectIdenInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdClientIdentification_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SelectIdenInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdIntroducerInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        SelectIntroInfo();
    }
    protected void grdIntroducerInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        SelectIntroInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdIntroducerInfo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SelectIntroInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdNomineeInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        SelectNomineeInfo();
    }
    protected void grdNomineeInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        SelectNomineeInfo();
    }
    protected void grdNomineeInfo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SelectNomineeInfo();
    }
    protected void grdBankAccount_PageIndexChanged(object sender, EventArgs e)
    {
        SelectBankInfo();        
    }
    private void SelectBankInfo()
    {
        string strSqlBank = "";
        strSqlBank = " SELECT * FROM BANK_ACCOUNT WHERE CLIENT_ID='" + txtClientID.Text.ToString() + "'";
        sdsBankAccount.SelectCommand = strSqlBank;
        sdsBankAccount.DataBind();
    }
    protected void grdClientIdentification_PageIndexChanged(object sender, EventArgs e)
    {
        SelectIdenInfo();        
    }
    private void SelectIdenInfo()
    {
        string strSqlIden = "";
        strSqlIden = "SELECT * FROM CLIENT_IDENTIFICATION WHERE CLIENT_ID='" + txtClientID.Text.ToString() + "'";
        sdsClientIdentification.SelectCommand = strSqlIden;
        sdsClientIdentification.DataBind();
    }
    protected void grdIntroducerInfo_PageIndexChanged(object sender, EventArgs e)
    {
        SelectIntroInfo();       
    }
    private void SelectIntroInfo()
    {
        string strSqlIntro = "";
        strSqlIntro = "SELECT * FROM INTRODUCER_INFO WHERE CLIENT_ID='" + txtClientID.Text.ToString() + "'";
        sdsIntroducerInformation.SelectCommand = strSqlIntro;
        sdsIntroducerInformation.DataBind();
    }
    protected void grdNomineeInfo_PageIndexChanged(object sender, EventArgs e)
    {
        SelectNomineeInfo();
       
    }
    private void SelectNomineeInfo()
    {
        string strSqlNominee = "";
        strSqlNominee = " SELECT * FROM NOMINEE_INFO WHERE CLIENT_ID='" + txtClientID.Text.ToString() + "'";
        sdsNomineeInformation.SelectCommand = strSqlNominee;
        sdsNomineeInformation.DataBind();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Clear();
        txtMobileNumber.Text = "";
        txtClientID.Text = "";
        txtWalletID.Text = "";
        grdBankAccount.DataBind();
        grdClientIdentification.DataBind();
        grdIntroducerInfo.DataBind();
        grdNomineeInfo.DataBind();
    }   
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlThana.Items.Clear();
            string strSql = "";
            strSql = " SELECT  MT.THANA_ID, MT.THANA_NAME FROM MANAGE_THANA MT, MANAGE_DISTRICT MD "
                   + " WHERE  MD.DISTRICT_ID=MT.DISTRICT_ID  AND  MD.DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "'";
            sdsThana.SelectCommand = strSql;
            sdsThana.DataBind();
            ddlThana.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void grdNomineeInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SelectNomineeInfo();
    }
    protected void grdNomineeInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SelectNomineeInfo();
    }
    protected void grdIntroducerInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SelectIntroInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdIntroducerInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SelectIntroInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdClientIdentification_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SelectIdenInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdClientIdentification_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SelectIdenInfo();
        SaveAuditInfo("Update", "KYC Update");
    }
    protected void grdBankAccount_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SelectBankInfo();
    }
    protected void grdBankAccount_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SelectBankInfo();
    }
    protected void grdClientIdentification_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int j = e.RowIndex;
        DropDownList ddlIdenti = (DropDownList)grdClientIdentification.Rows[j].FindControl("DropDownList6");
        TextBox txtIden = (TextBox)grdClientIdentification.Rows[j].FindControl("txtEIdenName");
        string strSql = "SELECT DISTINCT AL.ACCNT_NO "
                        + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL, CLIENT_IDENTIFICATION CI, IDENTIFICATION_SETUP IDS "
                        + " WHERE CI.CLINT_IDENT_NAME = '" + txtIden.Text.Trim().ToString() + "' AND CI.IDNTIFCTION_ID = '" + ddlIdenti.SelectedValue.ToString() + "' "
                        + " AND AL.CLINT_ID = CL.CLINT_ID AND CL.CLINT_ID = CI.CLIENT_ID AND CI.IDNTIFCTION_ID = IDS.IDNTIFCTION_ID";
        DataSet ods1 = objServiceHandler.CheckNID(strSql);
        try
        {
            if (ods1.Tables[0].Rows.Count > 0)
            {
                string account = "";
                foreach (DataRow prow in ods1.Tables[0].Rows)
                {
                     account = prow["ACCNT_NO"].ToString();
                     break;
                }

                Page page = HttpContext.Current.Handler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + "The NID: " + txtIden.Text.Trim().ToString() + " is already used in MYCash Account: " + account + "; " + "');", true);
                e.Cancel = true;
            }
            else
            {
                SelectIdenInfo();
                SaveAuditInfo("Update", "KYC Update");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }
}
