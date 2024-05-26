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

public partial class MANAGE_TM_TO_frmModifyKPITargetForTO : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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
        LoadGrid();
    }

    private void LoadGrid()
    {
        try
        {
            grvKpi.Visible = true;
            txtToAccNo.Enabled = false;
            drpYear.Enabled = false;
            string strSql = "";
            strSql = " SELECT MT.KPI_TARGET_ID, MT.TO_ACCNT_ID, MT.TO_ACCNT_ID, AL.ACCNT_NO, MT.CUST_ACQU_TARGET, MT.DPS_ACC_ACQU_TARGET, "
                     + " MT.TRX_AMT_TARGET, MT.ACTIVE_AGENTNO_TARGET, MT.ACTIVE_AGENT_TRXAMT_TARGET, MT.CORP_COLLECTION_TARGET, "
                     + " MT.COMPLIANCE_TARGET, MT.VISIBILITY_TARGET, LIFTING_AMOUNT_TARGET, MT.TARGET_YEAR,MT.UTILITY_AMOUNT_TARGET, MT.TARGET_MONTH, MT.REMARKS "
                     + " FROM MANAGE_KPI_TARGET MT, ACCOUNT_LIST AL WHERE AL.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "' "
                     + " AND AL.ACCNT_ID = MT.TO_ACCNT_ID AND MT.TARGET_YEAR = '" + drpYear.SelectedValue + "' ORDER BY MT.KPI_TARGET_ID ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvKpi.DataSource = oDataSet;
            grvKpi.DataBind();
            if (grvKpi.Rows.Count == 0)
            {
                lblMsg.Text = "No Data Found";
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void grvKpi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grvKpi.EditIndex = -1;
            LoadGrid();
        }
        catch (Exception exception )
        {
            exception.Message.ToString();
        }
    }
    protected void grvKpi_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grvKpi.EditIndex = e.NewEditIndex;
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grvKpi_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblId = (Label)grvKpi.Rows[e.RowIndex].FindControl("Label1");
            string strid = lblId.Text;
            Label txtAccNo = (Label)grvKpi.Rows[e.RowIndex].FindControl("Label13");
            string strAccNo = txtAccNo.Text;
            TextBox txtCustAcq = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox3");
            int strCustAcq = Convert.ToInt32(txtCustAcq.Text);
            if (strCustAcq < 50)
            {
                lblMsg.Text = "Customer Acquisition Count Should be 50 or more";
                return;
            }
            //TextBox txtDpsAcq = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox4");
            //int strDpsAcq = Convert.ToInt32(txtDpsAcq.Text);
            TextBox txtTrxAmt = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox5");
            int strTrxAmt = Convert.ToInt32(txtTrxAmt.Text);
            TextBox txtAgtNo = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox6");
            int strAgtNo = Convert.ToInt32(txtAgtNo.Text);
           
            TextBox txtCorpCol = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox8");
            int strCorpColl = Convert.ToInt32(txtCorpCol.Text);           

            TextBox txtLifting = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox2");
            int strLiftingTarget = Convert.ToInt32(txtLifting.Text);

            TextBox txtAgtTrxAmt = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox7");
            int strAgtTrxAmt = Convert.ToInt32(txtAgtTrxAmt.Text);


            TextBox txtUtility = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBoxUtility");
            int strUtilityAmt = Convert.ToInt32(txtUtility.Text);  
            
            TextBox txtRmks = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox10");
            string strRmks = txtRmks.Text;

            string strUpdateSuccsMsg = objServiceHandler.UpdateTOIndividualMonthlyTarget(strid, strCustAcq,
                strTrxAmt, strAgtNo, strCorpColl, strLiftingTarget, strRmks, strUtilityAmt, strAgtTrxAmt);
            if (strUpdateSuccsMsg == "Successfull.")
            {
                lblMsg.Text = "Saved Successfully";
                SaveAuditInfo("Update", "Id:" + strid + ", AccNo:" + strAccNo);
            }


            grvKpi.EditIndex = -1;
            LoadGrid();
            SaveAuditInfo("Update", "");
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
