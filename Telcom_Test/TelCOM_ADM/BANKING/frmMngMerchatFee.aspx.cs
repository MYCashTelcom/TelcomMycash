using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class BANKING_frmMngMerchatFee : System.Web.UI.Page 
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                LoadBranch();
                //pnlInsert.Visible = false;
                //ddlBranch.SelectedValue = Session["Branch_ID"].ToString();
                //ddlBranch.DataBind();
                //if (Session["Branch_Type"].Equals("A"))
                //{
                //    ddlBranch.Enabled = true;
                //}
                //else
                //{
                //    ddlBranch.Enabled = false;
                //}
                LoadBankList();
                LoadServiceList();
                AccountRank();
                ChannelType();
                LoadData();
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

    private void LoadBranch()
    {
        try
        {
            string strSql = " SELECT CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH ORDER BY CMP_BRANCH_NAME ASC ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            ddlBranch.DataSource = oDs;
            ddlBranch.DataValueField = "CMP_BRANCH_ID";
            ddlBranch.DataTextField = "CMP_BRANCH_NAME";
            ddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void LoadBankList()
    {
        try
        {
            string strSql = " SELECT BANK_NAME,BANK_INTERNAL_CODE FROM BANK_LIST WHERE BANK_STATUS='A' ORDER BY BANK_NAME ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            ddlBankList.DataSource = oDs;
            ddlBankList.DataValueField = "BANK_INTERNAL_CODE";
            ddlBankList.DataTextField = "BANK_NAME";
            ddlBankList.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void LoadServiceList()
    {
        try
        {
            string strSql = " SELECT SERVICE_ID, SERVICE_TITLE FROM SERVICE_LIST ORDER BY SERVICE_TITLE ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            ddlService.DataSource = oDs;
            ddlService.DataValueField = "SERVICE_ID";
            ddlService.DataTextField = "SERVICE_TITLE";
            ddlService.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void AccountRank()
    {
        try
        {
            string strSql = " SELECT ACCNT_RANK_ID, RANK_TITEL FROM ACCOUNT_RANK WHERE STATUS='A' ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            ddlAccountRank.DataSource = oDs;
            ddlAccountRank.DataValueField = "ACCNT_RANK_ID";
            ddlAccountRank.DataTextField = "RANK_TITEL";
            ddlAccountRank.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void ChannelType()
    {
        try
        {
            string strSql = " SELECT * FROM CHANNEL_TYPE ORDER BY CHANNEL_TYPE ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            ddlChannelName.DataSource = oDs;
            ddlChannelName.DataValueField = "CHANNEL_TYPE_ID";
            ddlChannelName.DataTextField = "CHANNEL_TYPE";
            ddlChannelName.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void LoadData()
    {
        try
        {
            string strChannelId = "";

            foreach (ListItem item in ddlChannelName.Items)
            {
                if (item.Selected)
                {
                    strChannelId += "'" + item.Value + "',";
                }
            }

            if (!strChannelId.Equals(""))
            {
                strChannelId = strChannelId.Substring(0, strChannelId.Length - 1);
            }

            string strSql = " SELECT AL.ACCNT_NO, CT.CHANNEL_TYPE, ML.* FROM MERCHANT_LIST ML, ACCOUNT_LIST AL, CHANNEL_TYPE CT WHERE ML.MERCHANT_ACCNT_ID = AL.ACCNT_ID AND ML.CHANNEL_TYPE_ID = CT.CHANNEL_TYPE_ID AND ML.SERVICE_ID = '" + ddlService.SelectedItem.Value + "' AND ML.ACCNT_RANK_ID = '" + ddlAccountRank.SelectedItem.Value + "' AND ML.CHANNEL_TYPE_ID IN (" + strChannelId + ") AND ML.CMP_BRANCH_ID = '" + ddlBranch.SelectedItem.Value + "' AND ML.BANK_CODE = '" + ddlBankList.SelectedItem.Value + "' ORDER BY MERCHANT_ID ";

            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvServiceFee.DataSource = oDs;
            gdvServiceFee.DataBind();

            if (gdvServiceFee.Rows.Count > 0)
            {
                dvGridView.Visible = true;
            }
            else
            {
                dvGridView.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void gdvServiceFee_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string strMerchantId = gdvServiceFee.DataKeys[e.RowIndex].Value.ToString();

            TextBox StartAmount = gdvServiceFee.Rows[e.RowIndex].FindControl("txtStartAmount") as TextBox;
            string strStartAmount = StartAmount.Text;
            TextBox MaxAmount = gdvServiceFee.Rows[e.RowIndex].FindControl("txtMaxAmount") as TextBox;
            string strMaxAmount = MaxAmount.Text;
            TextBox Fee = gdvServiceFee.Rows[e.RowIndex].FindControl("txtFee") as TextBox;
            string strFee = Fee.Text;
            TextBox MinimumFee = gdvServiceFee.Rows[e.RowIndex].FindControl("txtMinimumFee") as TextBox;
            string strMinimumFee = MinimumFee.Text;
            TextBox VatTax = gdvServiceFee.Rows[e.RowIndex].FindControl("txtVatTax") as TextBox;
            string strVatTax = VatTax.Text;
            TextBox AIT = gdvServiceFee.Rows[e.RowIndex].FindControl("txtAIT") as TextBox;
            string strAIT = AIT.Text;
            TextBox FeesPaidByBank = gdvServiceFee.Rows[e.RowIndex].FindControl("txtFeesPaidByBank") as TextBox;
            string strFeesPaidByBank = FeesPaidByBank.Text;
            TextBox FeesPaidByInitiator = gdvServiceFee.Rows[e.RowIndex].FindControl("txtFeesPaidByInitiator") as TextBox;
            string strFeesPaidByInitiator = FeesPaidByInitiator.Text;
            TextBox FeesPaidByReceipent = gdvServiceFee.Rows[e.RowIndex].FindControl("txtFeesPaidByReceipent") as TextBox;
            string strFeesPaidByReceipent = FeesPaidByReceipent.Text;
            TextBox BankCommission = gdvServiceFee.Rows[e.RowIndex].FindControl("txtBankCommission") as TextBox;
            string strBankCommission = BankCommission.Text;
            TextBox AgentCommission = gdvServiceFee.Rows[e.RowIndex].FindControl("txtAgentCommission") as TextBox;
            string strAgentCommission = AgentCommission.Text;
            TextBox PoolAdjustment = gdvServiceFee.Rows[e.RowIndex].FindControl("txtPoolAdjustment") as TextBox;
            string strPoolAdjustment = PoolAdjustment.Text;
            TextBox VendorCommission = gdvServiceFee.Rows[e.RowIndex].FindControl("txtVendorCommission") as TextBox;
            string strVendorCommission = VendorCommission.Text;
            TextBox ThirdPartyCommission = gdvServiceFee.Rows[e.RowIndex].FindControl("txtThirdPartyCommission") as TextBox;
            string strThirdPartyCommission = ThirdPartyCommission.Text;
            TextBox ChannelCommission = gdvServiceFee.Rows[e.RowIndex].FindControl("txtChannelCommission") as TextBox;
            string strChannelCommission = ChannelCommission.Text;
            TextBox AgentOperatorCommission = gdvServiceFee.Rows[e.RowIndex].FindControl("txtAgentOperatorCommission") as TextBox;
            string strAgentOperatorCommission = AgentOperatorCommission.Text;

            DropDownList TaxPaidBy = (DropDownList)gdvServiceFee.Rows[e.RowIndex].FindControl("ddlTaxPaidByEdit");
            string strTaxPaidBy = TaxPaidBy.Text;
            DropDownList FeesPaidby = (DropDownList)gdvServiceFee.Rows[e.RowIndex].FindControl("ddlFeesPaidbyEdit");
            string strFeesPaidby = FeesPaidby.Text;
            DropDownList FeeIncludeVatTax = (DropDownList)gdvServiceFee.Rows[e.RowIndex].FindControl("ddlFeeIncludeVatTaxEdit");
            string strFeeIncludeVatTax = FeeIncludeVatTax.Text;
            DropDownList FeesPaidByMM = (DropDownList)gdvServiceFee.Rows[e.RowIndex].FindControl("ddlFeesPaidByMMEdit");
            string strFeesPaidByMM = FeesPaidByMM.Text;

            string strUpdateResult = objServiceHandler.UpdateMerchantInfo(strMerchantId, strStartAmount, strMaxAmount, strFee, strMinimumFee, strVatTax, strAIT, strFeesPaidByBank, strFeesPaidByInitiator, strFeesPaidByReceipent, strBankCommission, strAgentCommission, strPoolAdjustment, strVendorCommission, strThirdPartyCommission, strChannelCommission, strAgentOperatorCommission, strTaxPaidBy, strFeesPaidby, strFeeIncludeVatTax, strFeesPaidByMM);

            if (strUpdateResult.Equals("Successful"))
            {
                lblMsg.Text = "Merchant information updated successfully";
            }
            else
            {
                lblMsg.Text = "Merchant information updated failed!";
            }
            gdvServiceFee.EditIndex = -1;

            if (txtSearchWalletID.Text.Length == 12)
            {
                SearchByMerchantWallet();
            }
            else
            {
                LoadData();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void gdvServiceFee_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SaveAuditInfo("Delete", "Manage Service Fee");

        if (txtSearchWalletID.Text.Length == 12)
        {
            SearchByMerchantWallet();
        }
        else
        {
            LoadData();
        }
    }
    protected void gdvServiceFee_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Manage Service Fee");

        if (txtSearchWalletID.Text.Length == 12)
        {
            SearchByMerchantWallet();
        }
        else
        {
            LoadData();
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnSearchWallet_Click(object sender, EventArgs e)
    {
        SearchByMerchantWallet();
    }

    private void SearchByMerchantWallet()
    {
        try
        {
            string strSql = " SELECT AL.ACCNT_NO, CT.CHANNEL_TYPE, ML.* FROM MERCHANT_LIST ML, ACCOUNT_LIST AL, CHANNEL_TYPE CT WHERE ML.MERCHANT_ACCNT_ID = AL.ACCNT_ID AND ML.CHANNEL_TYPE_ID = CT.CHANNEL_TYPE_ID AND AL.ACCNT_NO = '" + txtSearchWalletID.Text.Trim() + "' ORDER BY MERCHANT_ID";

            clsServiceHandler objServiceHandler = new clsServiceHandler();
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvServiceFee.DataSource = oDs;
            gdvServiceFee.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ddlChannelName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSearchWalletID.Text.Length == 12)
        {
            SearchByMerchantWallet();
        }
        else
        {
            LoadData();
        }
    }
    protected void ddlAccountRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSearchWalletID.Text.Length == 12)
        {
            SearchByMerchantWallet();
        }
        else
        {
            LoadData();
        }
    }
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSearchWalletID.Text.Length == 12)
        {
            SearchByMerchantWallet();
        }
        else
        {
            LoadData();
        }
    }
    protected void gdvServiceFee_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gdvServiceFee.EditIndex = e.NewEditIndex;

            if (txtSearchWalletID.Text.Length == 12)
            {
                SearchByMerchantWallet();
            }
            else
            {
                LoadData();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void gdvServiceFee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gdvServiceFee.EditIndex = -1;

            if (txtSearchWalletID.Text.Length == 12)
            {
                SearchByMerchantWallet();
            }
            else
            {
                LoadData();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void gdvServiceFee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvServiceFee.PageIndex = e.NewPageIndex;

            if (txtSearchWalletID.Text.Length == 12)
            {
                SearchByMerchantWallet();
            }
            else
            {
                LoadData();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int intMerchantCount = 0;
            int intChannelCount = 0;
        
            string strMerchantAccount = txtWalletAccount.Text.Trim();
            string merchantAccountId = "";
            string strFeeName = txtFeeName.Text.Trim();
            string strStartAmount = txtStartAmount.Text.Trim();
            string strMaxAmount = txtMaxAmount.Text.Trim();
            string strFee = txtFee.Text.Trim();
            string strMinimumFee = txtMinimumFee.Text.Trim();
            string strVatTax = txtVATTAX.Text.Trim();
            string strAit = txtAIT.Text.Trim();
            string strFeesPaidByBank = txtFeesPaidByBank.Text.Trim();
            string strFeesPaidByInitiator = txtFeesPaidByInitiator.Text.Trim();
            string strFeesPaidByReceipent = txtFeesPaidByReceipent.Text.Trim();
            string strBankCommission = txtBankCommission.Text.Trim();
            string strAgentCommission = txtAgentCommission.Text.Trim();
            string strPoolAdjustment = txtPoolAdjustment.Text.Trim();
            string strVendorCommission = txtVendorCommission.Text.Trim();
            string strThirdPartyCommission = txtThirdPartyCommission.Text.Trim();
            string strChannelCommission = txtChannelCommission.Text.Trim();
            string strAgentOperatorCommission = txtAgentOperatorCommission.Text.Trim();
            string strTaxPaidBy = ddlTaxPaidBy.SelectedItem.Value;
            string strFeesPaidBy = ddlFeesPaidBy.SelectedItem.Value;
            string strFeesIncludeVatTax = ddlFeesIncludeVatTax.SelectedItem.Value;
            string strFeesPaidByMotherMerchant = ddlFeesPaidByMotherMerchant.SelectedItem.Value;

            string[] merchantAccountList = strMerchantAccount.Split(',');

            Session["InsertData"] = null;

            //For Print Insert Status
            DataTable dt = new DataTable();
            dt.Columns.Add("MerchantWallet");
            dt.Columns.Add("ChannelId");
            dt.Columns.Add("Status");
            DataRow dr = null;

            for (int i = 0; i < merchantAccountList.Length; i++)
            {
                string merchantAccount = merchantAccountList[i].ToString();
                string strSql = " SELECT ACCNT_ID FROM ACCOUNT_LIST WHERE ACCNT_NO = '" + merchantAccount.Trim() + "' ";
                merchantAccountId = objServiceHandler.ReturnString(strSql);
                string strBranch = ddlBranch.SelectedItem.Value;
                string strBank = ddlBankList.SelectedItem.Value;
                string strService = ddlService.SelectedItem.Value;
                string strAccountRank = ddlAccountRank.SelectedItem.Value;

                if (!merchantAccountId.Equals(""))
                {
                    intMerchantCount += 1;

                    foreach (ListItem channelId in ddlChannelName.Items)
                    {
                        if (channelId.Selected)
                        {
                            string strExistingMerchant = "SELECT MERCHANT_ID FROM MERCHANT_LIST WHERE MERCHANT_ACCNT_ID = '" + merchantAccountId + "' AND CHANNEL_TYPE_ID = '" + channelId.Value + "'";
                            string strIsExist = objServiceHandler.ReturnString(strExistingMerchant);

                            if (strIsExist.Equals(""))
                            {
                                objServiceHandler.CreateMerchant(strBranch, strBank, strService, strAccountRank, channelId.Value, merchantAccountId, strFeeName, strStartAmount, strMaxAmount, strFee, strMinimumFee, strVatTax, strAit, strFeesPaidByBank, strFeesPaidByInitiator, strFeesPaidByReceipent, strBankCommission, strAgentCommission, strPoolAdjustment, strVendorCommission, strThirdPartyCommission, strChannelCommission, strAgentOperatorCommission, strTaxPaidBy, strFeesPaidBy, strFeesIncludeVatTax, strFeesPaidByMotherMerchant);

                                intChannelCount += 1;

                                dr = dt.NewRow();
                                dr["MerchantWallet"] = merchantAccount.Trim();
                                dr["ChannelId"] = channelId.Value;
                                dr["Status"] = "Success";
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                dr = dt.NewRow();
                                dr["MerchantWallet"] = merchantAccount.Trim();
                                dr["ChannelId"] = channelId.Value;
                                dr["Status"] = "Failed";
                                dt.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Please select the channel";
                        }
                    }
                }
                else
                {
                    dr = dt.NewRow();
                    dr["MerchantWallet"] = merchantAccount.Trim();
                    dr["ChannelId"] = "";
                    dr["Status"] = "Invalid Number";
                    dt.Rows.Add(dr);
                }
            }

            intChannelCount = intChannelCount != 0 && intMerchantCount != 0 ? intChannelCount / intMerchantCount : 0;

            if (intMerchantCount > 0 && intChannelCount > 0)
            {
                lblMsg.Text = intChannelCount + " Channel " + intMerchantCount + " Merchant Setup Done!";

                if (txtSearchWalletID.Text.Length == 12)
                {
                    SearchByMerchantWallet();
                }
                else
                {
                    LoadData();
                }
                Session["InsertData"] = dt;
                ClearField();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void ExportUnsuccessfulReport()
    {
        DataTable dt = Session["InsertData"] as DataTable;

        try
        {
            string strHTML = "", fileName = "Insert_Merchant_Status_Rpt";

            //------------------------------------------Report File xl processing   -------------------------------------

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Insert Merchant Status Report</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Channel Name</td>";
            strHTML = strHTML + "<td valign='middle' >Status</td>";
            strHTML = strHTML + "</tr>";

            if (dt.Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dt.Rows)
                {
                    string strSql = "SELECT CHANNEL_TYPE FROM CHANNEL_TYPE WHERE CHANNEL_TYPE_ID = '" + prow["ChannelId"] + "'";
                    string strChannelName = objServiceHandler.ReturnString(strSql);

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["MerchantWallet"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + strChannelName + " </td>";
                    strHTML = strHTML + " <td > '" + prow["Status"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Insert_Failed_Merchant_Info");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void ClearField()
    {
        txtWalletAccount.Text = string.Empty;
        txtFeeName.Text = string.Empty;
        txtStartAmount.Text = string.Empty;
        txtMaxAmount.Text = string.Empty;
        txtFee.Text = string.Empty;
        txtMinimumFee.Text = string.Empty;
        txtVATTAX.Text = string.Empty;
        txtAIT.Text = string.Empty;
        txtFeesPaidByBank.Text = string.Empty;
        txtFeesPaidByInitiator.Text = string.Empty;
        txtFeesPaidByReceipent.Text = string.Empty;
        txtBankCommission.Text = string.Empty;
        txtAgentCommission.Text = string.Empty;
        txtPoolAdjustment.Text = string.Empty;
        txtVendorCommission.Text = string.Empty;
        txtThirdPartyCommission.Text = string.Empty;
        txtChannelCommission.Text = string.Empty;
        txtAgentOperatorCommission.Text = string.Empty;
        ddlTaxPaidBy.SelectedIndex = 0;
        ddlFeesPaidBy.SelectedIndex = 0;
        ddlFeesIncludeVatTax.SelectedIndex = 0;
        ddlFeesPaidByMotherMerchant.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlBankList.SelectedIndex = 0;
        //ddlService.SelectedIndex = 0;
        //ddlAccountRank.SelectedIndex = 0;
        
        //foreach (ListItem item in ddlChannelName.Items)
        //{
        //    if (item.Selected)
        //    {
        //        item.Selected = false;
        //    }
        //}
    }
    protected void gdvServiceFee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strMerchantId = gdvServiceFee.DataKeys[e.RowIndex].Value.ToString();

            string strDeleteResult = objServiceHandler.DeleteMerchant(strMerchantId);

            if (strDeleteResult.Equals("Successful"))
            {
                lblMsg.Text = "Merchant remove from list";

                if (txtSearchWalletID.Text.Length == 12)
                {
                    SearchByMerchantWallet();
                }
                else
                {
                    LoadData();
                }
            }
            else
            {
                lblMsg.Text = "Merchant remove failed!";
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        ExportUnsuccessfulReport();
    }
}
