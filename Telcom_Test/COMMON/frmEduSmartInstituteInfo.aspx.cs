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

public partial class COMMON_frmEduSmartInstituteInfo : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Purpose"] = "I";
        if (!IsPostBack)
        {
            try
            {

                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
                //LoadREBAccountsDetails(Session["Purpose"].ToString());
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
    }

    private void LoadGrid()
    {
        try
        {
            string strSql = " SELECT DISTINCT EDU_INS_PK_ID, EDU_INS_NAME, EDU_INST_REF_ID, ACCOUNT_NO, STATUS, OWNER_CODE, BDMIT_INTERNAL_INS_CODE FROM EDUCATIONAL_INSTITUTE ORDER BY EDU_INS_PK_ID DESC ";
            DataSet ods = objServiceHandler.ExecuteQuery(strSql);
            grvEduIns.DataSource = ods;
            grvEduIns.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadGridForAmount()
    {
        try
        {
            string strSql = " SELECT EI.EDU_INS_NAME, EIA.EDU_INS_AMT_ID, EIA.PURPOSE_CODE, EIA.AMOUNT, EIA.PURPOSE_CODE_NAME, EIA.DESCRIPTION FROM EDUCATIONAL_INSTITUTE_AMOUNT EIA INNER JOIN EDUCATIONAL_INSTITUTE EI ON EIA.EDU_INS_PK_ID = EI.EDU_INS_PK_ID ";

            DataSet ods = objServiceHandler.ExecuteQuery(strSql);
            gvInstituteAmount.DataSource = ods;
            gvInstituteAmount.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void DropDownListInstituteName()
    {
        try
        {
            string strSql = " SELECT EDU_INS_PK_ID, EDU_INS_NAME FROM EDUCATIONAL_INSTITUTE ORDER BY EDU_INS_NAME ";

            DataSet ods = objServiceHandler.ExecuteQuery(strSql);
            ddlInstituteName.DataValueField = "EDU_INS_PK_ID";
            ddlInstituteName.DataTextField = "EDU_INS_NAME";
            ddlInstituteName.DataSource = ods;
            ddlInstituteName.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void grvEduIns_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvEduIns.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnWithPurposeFees_Click(object sender, EventArgs e)
    {
        btnWithPurposeFees.BorderStyle = BorderStyle.Inset;
        btnWithoutPurposeFees.BorderStyle = BorderStyle.Outset;
        MultiView1.ActiveViewIndex = 0;
        LoadGrid();
    }

    protected void btnWithoutPurposeFees_Click(object sender, EventArgs e)
    {
        btnWithPurposeFees.BorderStyle = BorderStyle.Outset;
        btnWithoutPurposeFees.BorderStyle = BorderStyle.Inset;
        MultiView1.ActiveViewIndex = 1;
        LoadGridForAmount();
    }

    private void Clear()
    {
        txtName.Text = "";
        txtId.Text = "";
        txtWallet.Text = "";
        txtOwnerCode.Text = "";
        txtBdMitInternalCode.Text = "";
    }

    protected void btnAddInstitute_Click(object sender, EventArgs e)
    {
        if (txtBdMitInternalCode.Text.Length != 3)
        {
            lblCodeLength.Text = "3 digit Code";
            lblCodeLength.ForeColor = System.Drawing.Color.Red;
            return;
        }

        try
        {
            string strName = txtName.Text.Trim();
            string strId = txtId.Text.Trim();
            string strAccountNo = txtWallet.Text.Trim();
            string strOwnerCode = txtOwnerCode.Text.Trim();
            string strBdMitInternalCode = txtBdMitInternalCode.Text.Trim();

            string strAddInstitute = objServiceHandler.AddTstInstituteInfo(strName, strId, strAccountNo, strOwnerCode, strBdMitInternalCode);

            if (strAddInstitute == "Successful")
            {
                lblMsg.Text = "Data Saved Successfully";
                Clear();
                LoadGrid();
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void ClearAmount()
    {
        ddlInstituteName.SelectedIndex = 0;
        txtPurposeName.Text = "";
        txtPurposeCode.Text = "";
        txtDescription.Text = "";
        txtPurposeAmount.Text = "";
    }

    protected void btnSaveAmount_Click(object sender, EventArgs e)
    {
        try
        {
            string strInstituteName = ddlInstituteName.SelectedValue.Trim();
            string strPurposeName = txtPurposeName.Text.Trim();
            string strPurposeCode = txtPurposeCode.Text.Trim();
            string strDescription = txtDescription.Text.Trim();
            string strAmount = txtPurposeAmount.Text.Trim();

            string strAddInstitute = objServiceHandler.AddInstitutePayment(strInstituteName, strPurposeName, strPurposeCode, strDescription, strAmount);

            if (strAddInstitute == "Successful")
            {
                lblMsg.Text = "Data Saved Successfully";
                ClearAmount();
                LoadGridForAmount();
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void gvInstituteAmount_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvInstituteAmount.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gvInstituteAmount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvInstituteAmount.EditIndex = -1;
            LoadGridForAmount();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gvInstituteAmount_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvInstituteAmount.EditIndex = e.NewEditIndex;
            LoadGridForAmount();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gvInstituteAmount_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblPkId = (Label)gvInstituteAmount.Rows[e.RowIndex].FindControl("lblPkId");
            string strPkId = lblPkId.Text;

            TextBox txtPurposeName = (TextBox)gvInstituteAmount.Rows[e.RowIndex].FindControl("txtPurposeName");
            string strPurposeName = txtPurposeName.Text;

            TextBox txtPurposeCode = (TextBox)gvInstituteAmount.Rows[e.RowIndex].FindControl("txtPurposeCode");
            string strPurposeCode = txtPurposeCode.Text;

            TextBox txtDescription = (TextBox)gvInstituteAmount.Rows[e.RowIndex].FindControl("txtDescription");
            string strDescription = txtDescription.Text;

            TextBox txtAmount = (TextBox)gvInstituteAmount.Rows[e.RowIndex].FindControl("txtAmount");
            string strAmount = txtAmount.Text;

            string strUpdateSuccMsg = objServiceHandler.UpdateInstituteAmount(strPkId, strPurposeName, strPurposeCode, strDescription, strAmount);

            if (strUpdateSuccMsg == "Successfull.")
            {
                lblMsg.Text = "Data Updated Successfully";
                SaveAuditInfo("Update", "Institute Amount");

                gvInstituteAmount.EditIndex = -1;
                LoadGridForAmount();
            }
            else
            {
                lblMsg.Text = "Data Update Failed";
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
}
