using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Forms_frmMngAccount : System.Web.UI.Page
{
    private string strConString = ConfigurationSettings.AppSettings["dbConnectionString"];
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();    
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objSrvHndlr = new clsServiceHandler();
    DataSet oDS;
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
        if (MultiView1.ActiveViewIndex == 1)
        {
            MultiView1.ActiveViewIndex = 1;
            lblCheck.Text = "";
        }
        else if (MultiView1.ActiveViewIndex == 0)
        {
            lblMsg.Text = "";
            MultiView1.ActiveViewIndex = 0;            
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

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (MultiView1.ActiveViewIndex == 1)
            {
                DropDownList20_SelectedIndexChanged(new object(), new EventArgs());
            }
        }
    }

    protected void btnAccountList_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnNewAccount_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void gdvSearch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LoadSearchResult();
    }
    protected void gdvSearch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        LoadSearchResult();
        gdvSearch.SelectedIndex = e.NewEditIndex;
    }
    protected void gdvSearch_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        LoadSearchResult();
        int index = gdvSearch.EditIndex;
        GridViewRow grow = gdvSearch.Rows[index];
        
        string strMessage = txtMSISDN.Text.Trim() + " updated account status";
        SaveAuditInfo("Update", strMessage);
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
            LoadSearchResult();        
    }
    public void LoadSearchResult()
    {
        string strChecking = "";

        lblMsg.Text = "";
        gdvSearch.Visible = false;
        string strSql = "", strAddSQL = "";

        if (txtName.Text.ToString().Trim() == "" && txtAccCode.Text.ToString().Trim() == "" && txtMSISDN.Text.ToString().Trim() == "")
        {
            lblMsg.Text = "Enter Client Name or wallet ID or Mobile No.";
        }
        else if (txtName.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() != "" && txtMSISDN.Text.ToString().Trim() != "")
        {
            lblMsg.Text = "Enter Client Name or wallet ID or Mobile No.";
        }
        else if (txtName.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() != "")
        {
            lblMsg.Text = "Enter Client Name or wallet ID.";
        }
        else if (txtName.Text.ToString().Trim() != "" && txtMSISDN.Text.ToString().Trim() != "")
        {
            lblMsg.Text = "Enter Client Name or Mobile No.";
        }
        else if (txtMSISDN.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() != "")
        {
            lblMsg.Text = "Enter Wallet ID or Mobile No.";
        }
        else
        {
            if (txtName.Text.ToString().Trim() != "")
            {
                strAddSQL = " C.CLINT_NAME LIKE '" + txtName.Text.ToString().Trim() + "%' ";                
            }
            else if (txtAccCode.Text.ToString().Trim() != "")
            {
                strAddSQL = " A.ACCNT_NO='" + txtAccCode.Text.ToString().Trim() + "' ";
                strChecking = (txtAccCode.Text.ToString().Trim()).Substring(0,5);
            }
            else if (txtMSISDN.Text.ToString().Trim() != "")
            {
                strAddSQL = " A.ACCNT_MSISDN='" + txtMSISDN.Text.ToString().Trim() + "' ";
                strChecking = (txtMSISDN.Text.ToString().Trim()).Substring(0,5);
            }

            if (strChecking != "00000" && strChecking != "+8800")
            {
                strSql = " SELECT A.CLINT_ID, ACCNT_ID, ACCNT_NO, ACCNT_MSISDN, CLINT_NAME,A.STATE_NOTE, "
                       + " A.SERVICE_PKG_ID, SERVICE_PKG_NAME, ACCNT_STATE, ACCNT_RANK_ID "
                       + " FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S "
                       + " WHERE A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID AND " + strAddSQL;

                try
                {
                    sdsClientAccount2.SelectCommand = strSql;
                    sdsClientAccount2.DataBind();
                    gdvSearch.DataBind();
                    if (gdvSearch.Rows.Count > 0)
                    {
                        gdvSearch.Visible = true;
                        lblMsg.Text = "";
                    }
                    else
                    {
                        gdvSearch.Visible = false;
                        lblMsg.Text = "Sorry, no data found.";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = " You are not authorized to see this account.";
                }
            }
            else
            {
                lblMsg.Text = " You are not authorized to see this account.";
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {   
        LoadSearchResult();
    }
    protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
      //  sdsClientAccount.InsertParameters["ACCNT_MSISDN"].DefaultValue=        
    }

    public string searchEasyLoadNo(string strEasyNo)
    {        
        string DepoID = "";       
        clsAccountHandler objAcc = new clsAccountHandler();
        DataSet ods = new DataSet();
        ods = objAcc.GetDuplecateID(strEasyNo);
        foreach (DataRow row in ods.Tables["ACCOUNT_LIST"].Rows)
        {
            DepoID = row["ACCNT_MSISDN"].ToString();
            if (DepoID == strEasyNo)
            {                
                return lblCheck.Text = "Duplicate EasyLoad Number";
            }
        }    
        return null;
    }
    protected void Btn_save_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text.ToString().Trim() == "")
        {
            lblCheck.Text = "Enter MSISDN";
            return;
        }
        else if (TextBox3.Text.ToString().Length < 14)
        {
            lblCheck.Text = "Invalid MSISDN";
            return;
        }
        lblCheck.Text = "";
        searchEasyLoadNo(TextBox3.Text);
        clsAccountHandler objAcc = new clsAccountHandler();
        if (lblCheck.Text == "")
        {
            lblCheck.Text = objAcc.AddAccount("", DropDownList19.SelectedValue.ToString(), DropDownList20.SelectedValue.ToString(),
                                               TextBox2.Text, TextBox3.Text, DropDownList18.SelectedValue.ToString());
            TextBox2.Text = "";
            TextBox3.Text = "";
           SaveAuditInfo("Save", "Manage Wallet");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        sdsClientList.DataBind();
    }
    protected void DropDownList20_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cId = DropDownList20.SelectedValue.ToString();
        clsAccountHandler objAH = new clsAccountHandler();
        DataSet oDSChannelInfo = new DataSet();
        oDSChannelInfo = objAH.GetChannelInfo(cId);
        if (oDSChannelInfo.Tables[0].Rows.Count > 0)
        {
            //foreach (DataRow prow in oDSChannelInfo.Tables["CHANNEL_INFO"].Rows)
            //{
            //TextBox2.Text = oDSChannelInfo.Tables[0].Rows[0]["CHANNEL_CODE"].ToString();
            //TextBox2.Text = oDSChannelInfo.Tables[0].Rows[0]["MSISDN"].ToString().Substring(3)+"1";
            //TextBox2.Text = oDSChannelInfo.Tables[0].Rows[0]["MSISDN"].ToString();            
            TextBox3.Text = oDSChannelInfo.Tables[0].Rows[0]["MSISDN"].ToString();
        }
        else
        {
            TextBox3.Text = "";
        }
    }
    //protected void gdvSearch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    //############## Insert audit info #####################
    //    string strSrvcPkg = "", strWalletID = "", strAccRank = "", strStatus = "", strRemarks = "", strPreRank="";

    //    GridViewRow row = (GridViewRow)gdvSearch.Rows[e.RowIndex];
    //    DropDownList ddlSrvcPkg = (DropDownList)row.FindControl("ddlEIPackage");
    //    strSrvcPkg = ddlSrvcPkg.SelectedItem.ToString();

    //    Label lblWalletID = (Label)row.FindControl("Label2");
    //    strWalletID = lblWalletID.Text.ToString();

    //    DropDownList ddlAccRank = (DropDownList)row.FindControl("ddlAccntrankID");
    //    strAccRank = ddlAccRank.SelectedItem.ToString();

    //    //DropDownList ddlAccRankPre = (DropDownList)row.FindControl("ddlAccntRankName");
    //    //strPreRank = ddlAccRankPre.SelectedItem.ToString();

    //    DropDownList ddlStatus = (DropDownList)row.FindControl("DropDownList1");
    //    strStatus = ddlStatus.SelectedItem.ToString();
       
    //    //##################################################

    //    //---------- chaecking balance for rank changing
    //    string strFisBalance = objSrvHndlr.GetAccountBalance(strWalletID);
    //    double dblFisBalance = Convert.ToDouble(strFisBalance);
    //    if (dblFisBalance > 1)
    //    {
    //        lblMsg.Text = "You are not allowed to change the rank due to sufficient balance.";
    //        e.Cancel = true;
    //        return;
    //    }

    //    //################# Updating  Cash Accnt Type ID ######################
        
    //    DataSet dsAccntNo = null;
    //    string strAccntID = "", strAccRankID = "", strSrvcPkgID = "", strMsg = "", strAccountID = "", strCashAccntTypeID = "";
    //    Label lblAccntID = (Label)row.FindControl("Label1");
    //    strAccntID = lblAccntID.Text.ToString();

    //    DropDownList ddlAccRankID = (DropDownList)row.FindControl("ddlAccntrankID");
    //    strAccRankID = ddlAccRankID.SelectedValue.ToString();

    //    DropDownList ddlSrvcPkgID = (DropDownList)row.FindControl("ddlEIPackage");
    //    strSrvcPkgID = ddlSrvcPkgID.SelectedValue.ToString();

    //    strCashAccntTypeID = objSrvHndlr.GettingCashAccountID(strAccRankID);

    //    dsAccntNo = objSrvHndlr.GetAllWalletAccount(strWalletID);

    //    if (dsAccntNo.Tables[0].Rows.Count > 0)
    //    {
    //        foreach (DataRow pRow in dsAccntNo.Tables[0].Rows)
    //        {
    //            objSrvHndlr.UpdateCashAccTypeID(strCashAccntTypeID, pRow["CAS_ACC_NO"].ToString());
    //        }
    //    }

    //    lblMsg.Text = "Successfull.";
    //    //################# Updating  Cash Accnt Type ID ######################

    //    //########################## Audit Log #####################################
    //    strRemarks = strSrvcPkg + "," + strWalletID + ",New Rank=" + strAccRank + " Pre Rank=" + strPreRank + "," + strStatus + "," + strCashAccntTypeID;
    //    SaveAuditInfo("Update", strRemarks);
    //    //##########################################################################

    //}




    protected void gdvSearch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        clsServiceHandler objSrvHndlr = new clsServiceHandler();
        //############## Insert audit info #####################
        string strSrvcPkg = "", strWalletID = "", strAccRank = "", strStatus = "", strRemarks = "";

        string strPresentRankId = "";
        string strPresentWalletState = "";
        string strPresentPackageId = "";

        string strUpdateRankId = "";
        string strUpdatedPackageId = "";
        string strUpdatedWalletState = "";


        GridViewRow row = (GridViewRow)gdvSearch.Rows[e.RowIndex];
        DropDownList ddlSrvcPkg = (DropDownList)row.FindControl("ddlEIPackage");
        strSrvcPkg = ddlSrvcPkg.SelectedItem.ToString();
        strUpdatedPackageId = ddlSrvcPkg.SelectedValue.ToString();

        Label lblWalletID = (Label)row.FindControl("Label2");
        strWalletID = lblWalletID.Text.ToString();

        DropDownList ddlAccRank = (DropDownList)row.FindControl("ddlAccntrankID");
        strAccRank = ddlAccRank.SelectedItem.ToString();
        strUpdateRankId = ddlAccRank.SelectedValue.ToString();

        DropDownList ddlStatus = (DropDownList)row.FindControl("DropDownList1");
        strStatus = ddlStatus.SelectedItem.ToString();
        strUpdatedWalletState = ddlStatus.SelectedValue.ToString();
        //##################################################




        //string strPresentRankId = objSrvHndlr.GetAccountRankIdByWallet(strWalletID);
        string strFisBalance = objSrvHndlr.GetAccountBalance(strWalletID);
        double dblFisBalance = Convert.ToDouble(strFisBalance);
        //if ((dblFisBalance > 1) && (strAccRank != strPresentRankId))
        //{
        //    lblMsg.Text = "You are not allowed to change the rank due to sufficient balance.";
        //    e.Cancel = true;
        //    return;
        //}




        string strSql = "";
        strSql = " SELECT DISTINCT AL.ACCNT_ID, AL.ACCNT_NO, AL.ACCNT_RANK_ID, AL.SERVICE_PKG_ID, AL.ACCNT_STATE "
                 + " FROM ACCOUNT_LIST AL WHERE AL.ACCNT_NO = '" + strWalletID + "' ";
        DataSet oDataSet = objSrvHndlr.ExecuteQuery(strSql);
        if (oDataSet.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in oDataSet.Tables[0].Rows)
            {
                strPresentRankId = prow["ACCNT_RANK_ID"].ToString();
                strPresentWalletState = prow["ACCNT_STATE"].ToString();
                strPresentPackageId = prow["SERVICE_PKG_ID"].ToString();
            }
        }

        //if ((dblFisBalance > 1) && (strAccRank != strPresentRankId) && (strPresentPackageId )
        //    && ((strStatus == strPresentWalletState) || (strStatus != strPresentWalletState)))
        //{
        //    lblMsg.Text = "You are not allowed to change the rank due to sufficient balance.";
        //    e.Cancel = true;
        //    return;
        //}

        if ((dblFisBalance > 1) && (strPresentRankId != strUpdateRankId))
        {
            lblMsg.Text = "You are not allowed to change the rank due to sufficient balance.";
            e.Cancel = true;
            return;
        }

        else if ((dblFisBalance > 1) && (strPresentPackageId != strUpdatedPackageId))
        {
            lblMsg.Text = "You are not allowed to change the package due to sufficient balance.";
            e.Cancel = true;
            return;
        }







        //################# Updating  Cash Accnt Type ID ######################

        DataSet dsAccntNo = null;
        string strAccntID = "", strAccRankID = "", strSrvcPkgID = "", strMsg = "", strAccountID = "", strCashAccntTypeID = "";
        Label lblAccntID = (Label)row.FindControl("Label1");
        strAccntID = lblAccntID.Text.ToString();

        DropDownList ddlAccRankID = (DropDownList)row.FindControl("ddlAccntrankID");
        strAccRankID = ddlAccRankID.SelectedValue.ToString();

        DropDownList ddlSrvcPkgID = (DropDownList)row.FindControl("ddlEIPackage");
        strSrvcPkgID = ddlSrvcPkgID.SelectedValue.ToString();

        strCashAccntTypeID = objSrvHndlr.GettingCashAccountID(strAccRankID);

        dsAccntNo = objSrvHndlr.GetAllWalletAccount(strWalletID);

        if (dsAccntNo.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow pRow in dsAccntNo.Tables[0].Rows)
            {
                objSrvHndlr.UpdateCashAccTypeID(strCashAccntTypeID, pRow["CAS_ACC_NO"].ToString());
            }
        }

        lblMsg.Text = "Successfull.";
        //################# Updating  Cash Accnt Type ID ######################
        //########################## Audit Log #####################################
        strRemarks = strSrvcPkg + "," + strWalletID + "," + strAccRank + "," + strStatus + "," + strCashAccntTypeID;
        SaveAuditInfo("Update", strRemarks);
        //##########################################################################

    }
















}
