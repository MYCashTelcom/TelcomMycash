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
                strMobileNumber = " SELECT CL.*,CM.SERIAL_NO FROM ACCOUNT_LIST AL,CLIENT_LIST CL,"
                               + " ACCOUNT_SERIAL_DETAIL CM WHERE AL.CLINT_ID=CL.CLINT_ID AND "
                               + " AL.ACCNT_MSISDN=CM.CUSTOMER_MOBILE_NO AND AL.ACCNT_NO='" + txtWalletID.Text.ToString() + "' ORDER BY TO_NUMBER(CM.SERIAL_NO) DESC";
            }
            else
            {
                lblMsg.Text = "Please isert correct Wallet ID.";
            }
        }
        else if (txtMobileNumber.Text != "")
        {
            strMobileNumber = " SELECT CL.*,CM.SERIAL_NO FROM ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_SERIAL_DETAIL CM "
                           + " WHERE AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_MSISDN=CM.CUSTOMER_MOBILE_NO"
                           + " AND AL.ACCNT_MSISDN='" + txtMobileNumber.Text.Trim() + "' ORDER BY TO_NUMBER(CM.SERIAL_NO) DESC";
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
            strChkWallet = txtMobileNumber.Text.Substring(3, 11) + "1";
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
        if (dsClientList.Tables[0].Rows.Count>0)
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
                ddlUISCAgent.SelectedValue = pRow["UISC_AGENT"].ToString();
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

        /* DataSet dsBank = null;
         dsBank = objServiceHandler.GetTableValue("BANK_ACCOUNT", "CLIENT_ID", txtClientID.Text.Trim());
         if (dsBank.Tables[0].Rows.Count > 0)
         {
             foreach (DataRow bRow in dsBank.Tables[0].Rows)
             {
                 txtBankName.Text = bRow["BANK_NAME"].ToString();
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
                 txtBankName.Text = idRow["CLINT_IDENT_NAME"].ToString();
                 txtBankRemarks.Text = idRow["REMARKS"].ToString();
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
                 txtNomnePrcent.Text = nRow["PERCENTAGE"].ToString();
                 txtNomneRemarks.Text = nRow["REMARKS"].ToString();
             }
         }*/

        string strSqlIdentityType = " SELECT  IDS.IDNTIFCTION_ID, IDS.IDNTIFCTION_NAME FROM IDENTIFICATION_SETUP IDS, CLIENT_IDENTIFICATION CI "
                                    + " WHERE IDS.IDNTIFCTION_ID = CI.IDNTIFCTION_ID AND CI.CLIENT_ID = '" + txtClientID.Text.Trim() + "'";
        sdsClientIdentificationSetUp.SelectCommand = strSqlIdentityType;
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


        string strIdentityNo = objServiceHandler.GetIdentityNo(txtClientID.Text.Trim());
        txtIdenID.Text = strIdentityNo;

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

        txtBankName.Text = "";
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
            //}

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
                    if (txtBankName.Text != "" && txtBranchName.Text != "")
                    {
                        blnBank = objServiceHandler.SaveBankAccount(txtClientID.Text.Trim(), txtBankName.Text.Trim(), txtBranchName.Text.Trim(),
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
