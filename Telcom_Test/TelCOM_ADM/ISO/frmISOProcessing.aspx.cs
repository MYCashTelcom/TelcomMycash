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

public partial class COMMON_frmISOProcessing : System.Web.UI.Page
{
    private clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private clsServiceHandler objServiceHandler = new clsServiceHandler();
    private string strUserName = string.Empty;
    private string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //UpdateIsoProcessing();
        //LoadGrid();
        //LoadISOProcessCodeData();

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


    protected void btnInsertPrcCode_Click(object sender, EventArgs e)
    {
        string strBankId = ddlBankList.SelectedValue;
        string strServiceId = ddlServiceList.SelectedValue;
        string strProcessingCode = txtProcessCode.Text;
        string strRemarks = txtRemarks.Text;
        string strSql = "";
        string strExistData = "";

        try
        {

            strExistData = objServiceHandler.IfExistBankAndServiceCode(strBankId, strServiceId);

            if (strExistData != "")
            {
                strSql = " SELECT ISO_PRO_CODE_ID, SERVICE_ID, BANK_ID, PROCESSING_CODE, REMARKS "
                       + " FROM ISO_PROCESSING_CODE WHERE ISO_PRO_CODE_ID = '" + strExistData + "'";
                sdsProcessingCode.SelectCommand = strSql;
                sdsProcessingCode.DataBind();
                grdIsoProcess.DataBind();

                lblMsg.Text = "Data Already Exist";
                Clear();
            }

            else
            {
                objServiceHandler.SaveToIsoProcessingCode(strBankId, strServiceId, strProcessingCode, strRemarks);
                string strIsoProcessingCode = objServiceHandler.FindIsoProcessingCodeID(strBankId, strServiceId, strProcessingCode, strRemarks);

                strSql = " SELECT ISO_PRO_CODE_ID, SERVICE_ID, BANK_ID, PROCESSING_CODE, REMARKS "
                       + " FROM ISO_PROCESSING_CODE WHERE ISO_PRO_CODE_ID = '" + strIsoProcessingCode + "'";
                sdsProcessingCode.SelectCommand = strSql;
                sdsProcessingCode.DataBind();
                grdIsoProcess.DataBind();

                lblMsg.Text = "Saved Successfully...";
                Clear();

                SaveAuditInfo("Insert", "Iso Processing Code");
            }

        }
        catch (Exception exception)
        {
            lblMsg.Text = exception.Message.ToString();
        }
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    private void Clear()
    {
        txtProcessCode.Text = "";
        txtRemarks.Text = "";
        //ddlBankList.SelectedIndex = -1;
        //ddlServiceList.SelectedIndex = -1;
    }


    private void LoadISOProcessCodeData()
    {
        try
        {
            foreach (GridViewRow grow in grdIsoProcess.Rows)
            {
                Label lblVal = ((Label)grow.FindControl("Label1"));
                string strExLACCId = lblVal.Text.ToString();

                string strCMD = " SELECT ISO_PRO_CODE_ID, SERVICE_ID, BANK_ID, PROCESSING_CODE, REMARKS FROM ISO_PROCESSING_CODE WHERE ISO_PRO_CODE_ID = '" + strExLACCId + "' ";
                try
                {
                    sdsProcessingCode.SelectCommand = strCMD;
                    sdsProcessingCode.DataBind();
                    grdIsoProcess.DataBind();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        catch (Exception exception)
        {
            lblMsg.Text = exception.Message.ToString();
        }
        lblMsg.Text = "";
    }
    protected void grdIsoProcess_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIsoProcessing();
        LoadGrid();
        LoadISOProcessCodeData();
    }
    protected void grdIsoProcess_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        UpdateIsoProcessing();
        LoadGrid();
        LoadISOProcessCodeData();
    }
    protected void grdIsoProcess_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdIsoProcess.EditIndex = e.NewEditIndex;
        LoadGrid();
        LoadISOProcessCodeData();
    }
    protected void grdIsoProcess_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LoadGrid();
        LoadISOProcessCodeData();
    }
    protected void grdIsoProcess_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        LoadGrid();
        LoadISOProcessCodeData();
    }
    protected void grdIsoProcess_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LoadISOProcessCodeData();
    }
    protected void grdIsoProcess_PageIndexChanged(object sender, EventArgs e)
    {
        LoadISOProcessCodeData();
    }
    protected void ddlBankList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
        LoadISOProcessCodeData();
        lblMsg.Text = "";
    }
    protected void ddlServiceList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
        LoadISOProcessCodeData();
        lblMsg.Text = "";
    }

    private void LoadGrid()
    {
        string strBankCode = ddlBankList.SelectedValue;
        string strServiceCode = ddlServiceList.SelectedValue;
        string strSql = "";

        strSql = " SELECT ISO_PRO_CODE_ID, SERVICE_ID, BANK_ID, PROCESSING_CODE, REMARKS FROM ISO_PROCESSING_CODE "
               + " WHERE SERVICE_ID = '" + strServiceCode + "' AND  BANK_ID ='" + strBankCode + "'";

        sdsProcessingCode.SelectCommand = strSql;
        sdsProcessingCode.DataBind();
        grdIsoProcess.DataBind();
        lblMsg.Text = "";

    }

    private void UpdateIsoProcessing()
    {
        try
        {
            foreach (GridViewRow grow in grdIsoProcess.Rows)
            {
                Label lblValIsoPrcId = ((Label)grow.FindControl("Label1"));
                string strPrcId = lblValIsoPrcId.Text.ToString();

                TextBox txtValIsoPrcId = (TextBox)grow.FindControl("TextBox3");
                string strPrcCode = txtValIsoPrcId.Text.ToString();

                TextBox txtValRemarks = (TextBox)grow.FindControl("TextBox4");
                string strRemarks = txtValRemarks.Text.ToString();

                objServiceHandler.UpdateISOProcessingCode(strPrcId, strPrcCode, strRemarks);
                lblMsg.Text = "";

            }

        }
        catch (Exception exception)
        {
            lblMsg.Text = exception.Message.ToString();
        }
    }

    //protected void Page_PreRenderComplete(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        UpdateIsoProcessing();
    //        LoadGrid();
    //        LoadISOProcessCodeData();
    //    }
    //}


}
