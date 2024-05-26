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

public partial class BANKING_frmMNgBankServiceFee2 : System.Web.UI.Page
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
                LoadBankList();
                LoadServiceList();
                LoadAccountRank();
                LoadChannelType();
                LoadGrid();

                ddlBranch.SelectedValue = Session["Branch_ID"].ToString();
                ddlBranch.DataBind();
                if (Session["Branch_Type"].Equals("A"))
                {
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlBranch.Enabled = false;
                }
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
            string strSql = "SELECT distinct CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH";
            sdsBranch.SelectCommand = strSql;
            sdsBranch.DataBind();
            ddlBranch.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadBankList()
    {
        try
        {
            string strSql = "SELECT BANK_NAME,BANK_INTERNAL_CODE FROM BANK_LIST WHERE BANK_STATUS='A' ORDER BY BANK_NAME";
            sdsBankList.SelectCommand = strSql;
            sdsBankList.DataBind();
            ddlBankList.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadServiceList()
    {
        try
        {
            string strSql = "SELECT SERVICE_ID, SERVICE_TITLE FROM SERVICE_LIST ORDER BY SERVICE_TITLE";
            sdsService.SelectCommand = strSql;
            sdsService.DataBind();
            ddlService.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadAccountRank()
    {
        try
        {
            string strSql = " SELECT ACCNT_RANK_ID, RANK_TITEL FROM ACCOUNT_RANK WHERE STATUS='A' ORDER BY ACCNT_RANK_ID ASC";
            sdsAccountRank.SelectCommand = strSql;
            sdsAccountRank.DataBind();
            ddlAccountRank.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadChannelType()
    {
        try
        {
            string strSql = " SELECT * FROM CHANNEL_TYPE ORDER BY CHANNEL_TYPE";
            sdsChannelType.SelectCommand = strSql;
            sdsChannelType.DataBind();
            ddlChannelName.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadGrid()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT * FROM BANK_SERVICE_FEE WHERE SERVICE_ID = '"+ ddlService.SelectedValue +"' "
                    + " AND ACCNT_RANK_ID = '"+ddlAccountRank.SelectedValue+"' AND CHANNEL_TYPE_ID = '"+ ddlChannelName.SelectedValue +"' " 
                    + " AND CMP_BRANCH_ID = '"+ ddlBranch.SelectedValue +"' AND BANK_CODE = '"+ ddlBankList.SelectedValue +"' ";

            DataSet odSet = objServiceHandler.ExecuteQuery(strSql);
            gdvServiceFee.DataSource = odSet;
            gdvServiceFee.DataBind();


        }

        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    
    
    
    protected void ddlAccountRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void ddlChannelName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gdvServiceFee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvServiceFee.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gdvServiceFee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gdvServiceFee.EditIndex = -1;
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gdvServiceFee_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gdvServiceFee.EditIndex = e.NewEditIndex;
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gdvServiceFee_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblServiceFeeId = (Label)gdvServiceFee.Rows[e.RowIndex].FindControl("Label2");
            string strServiceFeeId = lblServiceFeeId.Text;

            Label lblServiceId = (Label)gdvServiceFee.Rows[e.RowIndex].FindControl("Label1");
            string strServiceId = lblServiceId.Text;

            TextBox txtServiceFeeName = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox2");
            string strServiceFeeName = txtServiceFeeName.Text;

            TextBox txtStartAmount = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox3");
            string strStartAmount = txtStartAmount.Text;

            TextBox txtMaxAmount = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox4");
            string strMaxAmount = txtMaxAmount.Text;

            TextBox txtBankSErvFeeAmount = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox5");
            string strBankSErvFeeAmount = txtBankSErvFeeAmount.Text;

            TextBox txtMinimumFee = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox6");
            string strMinimumFee = txtMinimumFee.Text;

            TextBox txtVatTax = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox7");
            string strVatTax = txtVatTax.Text;

            TextBox txtAit = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox8");
            string strAit = txtAit.Text;

            TextBox txtFeesPaidByBank = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox9");
            string strFeesPaidByBank = txtFeesPaidByBank.Text;

            TextBox txtFeesPaidByInitiator = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox10");
            string strFeesPaidByInitiator = txtFeesPaidByInitiator.Text;

            TextBox txtFeesPaidByReceipent = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox11");
            string strFeesPaidByReceipent = txtFeesPaidByReceipent.Text;

            TextBox txtBankCommission = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox12");
            string strBankCommission = txtBankCommission.Text;

            TextBox txtAgentCommission = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox13");
            string strAgentCommission = txtAgentCommission.Text;

            TextBox txtPoolAdjustment = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox14");
            string strPoolAdjustment = txtPoolAdjustment.Text;

            TextBox txtVendorCommission = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox15");
            string strVendorCommission = txtVendorCommission.Text;

            TextBox txtThirdPartyCommission = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox16");
            string strThirdPartyCommission = txtThirdPartyCommission.Text;

            TextBox txtChannelCommission = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox17");
            string strChannelCommission = txtChannelCommission.Text;

            TextBox txtCommonOperatorCommission = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("TextBox18");
            string strCommonOperatorCommission = txtCommonOperatorCommission.Text;

            DropDownList drpAgentOperatorCommissionId = (DropDownList)gdvServiceFee.Rows[e.RowIndex].FindControl("ddlAgntOpertrComID91");
            string strAgentOperatorCommissionId = drpAgentOperatorCommissionId.SelectedValue;

            DropDownList drpTaxPaidBy = (DropDownList)gdvServiceFee.Rows[e.RowIndex].FindControl("DropDownList8");
            string strTaxPaidBy = drpTaxPaidBy.SelectedValue;

            DropDownList drpFeesPaidBy = (DropDownList)gdvServiceFee.Rows[e.RowIndex].FindControl("ddlFeesPaidby1");
            string strFeesPaidBy = drpFeesPaidBy.SelectedValue;

            DropDownList drpFeesIncludeVatTax = (DropDownList)gdvServiceFee.Rows[e.RowIndex].FindControl("DropDownList11");
            string strFeesIncludeVatTax = drpFeesIncludeVatTax.SelectedValue;

            TextBox txtSubServiceCode = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("txtSubServiceCodeGv");
            string strSubServiceCode = txtSubServiceCode.Text;
			
			TextBox txtSubWalletNumber = (TextBox)gdvServiceFee.Rows[e.RowIndex].FindControl("txtSubWalletNumberGv");
            string strSubWalletNumber = txtSubWalletNumber.Text;

            string strUpdateSuccMsg = objServiceHandler.UpdateServiceFeeNew2(strServiceFeeId, strServiceId,
                strServiceFeeName, strStartAmount,
                strMaxAmount, strBankSErvFeeAmount, strMinimumFee, strVatTax, strAit, strFeesPaidByBank,
                strFeesPaidByInitiator, strFeesPaidByReceipent,
                strBankCommission, strAgentCommission, strPoolAdjustment, strVendorCommission, strThirdPartyCommission,
                strChannelCommission,
                strCommonOperatorCommission, strAgentOperatorCommissionId, strTaxPaidBy, strFeesPaidBy,
                strFeesIncludeVatTax, strSubServiceCode, strSubWalletNumber);

            if (strUpdateSuccMsg == "Successfull.")
            {
                lblMsg.Text = "Data Updated Successfully";
                SaveAuditInfo("Update", "Manage Service Fee");

                gdvServiceFee.EditIndex = -1;
                LoadGrid();
            }
            else
            {
                lblMsg.Text = "Data UpdateFailed";
            }


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string strCmpBranchId = ddlBranch.SelectedValue;
            string strBankCode = ddlBankList.SelectedValue;
            string strServiceId = ddlService.SelectedValue;
            string strAccountRankId = ddlAccountRank.SelectedValue;
            string strChannelTypeId = ddlChannelName.SelectedValue;


            string strFeeName = txtFeeName.Text.Trim();
            string strStartAmount = txtStartAmount.Text.Trim();
            string strMaxAmount = txtMaxAmount.Text.Trim();
            string strFee = txtFeePercnt.Text.Trim();
            string strMinFee = txtMinFee.Text.Trim();
            string strVatNTax = txtVatNTaxPercent.Text.Trim();
            string strAit = txtAitPercent.Text.Trim();
            string strFeePaidByBank = txtFeePaidByBankPercent.Text.Trim();
            string strFeePaidByInitiator = txtFeePaidByInitPercent.Text.Trim();
            string strFeesPaidByAgent = txtFeePaidByReceptPercent.Text.Trim();
            string strBankCommiAmount = txtBankCommissionPercent.Text.Trim();
            string strAgtCommAmount = txtAgentCommissionPercent.Text.Trim();
            string strPoolAdjustment = txtPoolAdjustmentPercent.Text.Trim();
            string strVendorCommission = txtVendorCommissionPercent.Text.Trim();
            string str3RdPartyCommission = txt3rdPartyCommiPercent.Text.Trim();
            string strChannelCommission = txtChannelCommiPercent.Text.Trim();
            string strAgentCommonOperatorCommission = txtCommonOperatorCommPercent.Text.Trim();
            string strAgtOperatorCommiId = ddlAgntOpCommissionIDAdd.SelectedValue;
            string strTaxPaidById = drpTaxPaidByAdd.SelectedValue;
            string strFeePaidBy = drpFeesPaidByAdd.SelectedValue;
            string strFeeIncludeVatTax = drpFeeIncludeVatTaxAdd.SelectedValue;
            string strSubServiceCode = txtSubServiceCode.Text.Trim();
			string strSubWalletNumber = txtSubWalletNumber.Text.Trim();

            string strAddSuccMsg = objServiceHandler.AddBankServiceFee2(strServiceId, strFeeName, strStartAmount,
                strMaxAmount, strFee, strVatNTax, strTaxPaidById, strFeeIncludeVatTax, strAccountRankId, strFeePaidByBank,
                strFeePaidByInitiator, strAit, strAgtCommAmount, strBankCommiAmount, strFeePaidBy, strPoolAdjustment,
                strVendorCommission, str3RdPartyCommission, strChannelCommission, strChannelTypeId, strFeesPaidByAgent,
                strMinFee, strCmpBranchId, strBankCode, strAgentCommonOperatorCommission, strAgtOperatorCommiId, strSubServiceCode, strSubWalletNumber);

            if (strAddSuccMsg == "Successfull.")
            {
                lblMsg.Text = "Data Saved Successfully";
                Clear();
                LoadGrid();
                SaveAuditInfo("Insert", "Manage Service Fee");
            }
            else
            {
                lblMsg.Text = "Data Save Failed";
                LoadGrid();
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void Clear()
    {
        txtFeeName.Text = "";
        txtStartAmount.Text = "";
        txtMaxAmount.Text = "";
        txtFeePercnt.Text = "";
        txtMinFee.Text = "";
        txtVatNTaxPercent.Text = "";
        txtAitPercent.Text = "";
        txtFeePaidByBankPercent.Text = "";
        txtFeePaidByInitPercent.Text = "";
        txtFeePaidByReceptPercent.Text = "";
        txtBankCommissionPercent.Text = "";
        txtAgentCommissionPercent.Text = "";
        txtPoolAdjustmentPercent.Text = "";
        txtVendorCommissionPercent.Text = "";
        txt3rdPartyCommiPercent.Text = "";
        txtChannelCommiPercent.Text = "";
        txtCommonOperatorCommPercent.Text = "";
        //ddlAgntOpCommissionIDAdd.SelectedIndex = 0;
        //drpTaxPaidByAdd.SelectedIndex = 0;
        //drpFeesPaidByAdd.SelectedIndex = 0;
        //drpFeeIncludeVatTaxAdd.SelectedIndex = 0;
        txtSubServiceCode.Text = "";
		txtSubWalletNumber.Text = "";
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gdvServiceFee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strId = gdvServiceFee.DataKeys[e.RowIndex].Values[0].ToString();

            string strMsg = objServiceHandler.DeleteBankserviceFeeNew(strId);
            if (strMsg == "Successful.")
            {
                lblMsg.Text = "Data Deleted Successfully";
            }
            else
            {
                lblMsg.Text = "Data Deleted Failed";
            }

            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
