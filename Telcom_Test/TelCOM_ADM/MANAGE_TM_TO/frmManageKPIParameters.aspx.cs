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

public partial class MANAGE_TM_TO_frmManageKPIParameters : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

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
            LoadGrid();
            LoadGrid2();
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

    private void LoadGrid()
    {
        try
        {
            string strSql = " SELECT MK.MANAGE_KPI_PARAMETERS_ID, MK.PARAMETER_NAME, MK.BENCHMARK FROM MANAGE_KPI_PARAMETERS MK WHERE STATS = 'A' AND KPI_AREA='U'";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvKpi.DataSource = oDataSet;
            grvKpi.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    private void LoadGrid2()
    {
        try
        {
            string strSql = " SELECT MK.MANAGE_KPI_PARAMETERS_ID, MK.PARAMETER_NAME, MK.BENCHMARK FROM MANAGE_KPI_PARAMETERS MK WHERE STATS = 'A' AND KPI_AREA='NU'";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvKpiNu.DataSource = oDataSet;
            grvKpiNu.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void grdKPI_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grvKpi.EditIndex = e.NewEditIndex;

            // LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grdKPI_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            grvKpi.EditIndex = -1;

            LoadGrid();
            
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grdKPI_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblId = (Label)grvKpi.Rows[e.RowIndex].FindControl("LabelId");
            string strKpiId = lblId.Text;

            TextBox txtBnchmrk = (TextBox)grvKpi.Rows[e.RowIndex].FindControl("TextBox");
            string strbnchmrk = txtBnchmrk.Text;

            string strUpdate = " UPDATE MANAGE_KPI_PARAMETERS   SET BENCHMARK ='" + strbnchmrk + "' WHERE MANAGE_KPI_PARAMETERS_ID='" + strKpiId + "'";
            objServiceHandler.ExecuteQuery(strUpdate);
            lblMsg.Text = "Update Successfully";
         
           
            grvKpi.EditIndex = -1;
            LoadGrid();
          
            //  LoadVoucherDetails(strVoucher);
            string strRemarks = "kpi_id:" + strKpiId + ", Benchmark : " + strbnchmrk;
            SaveAuditLog("Update", strRemarks);
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void grdKPI_RowEditing2(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grvKpiNu.EditIndex = e.NewEditIndex;

            // LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grdKPI_RowCancelingEdit2(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            grvKpiNu.EditIndex = -1;

            LoadGrid2();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grdKPI_RowUpdating2(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblId = (Label)grvKpiNu.Rows[e.RowIndex].FindControl("LabelId1");
            string strKpiId = lblId.Text;

            TextBox txtBnchmrk = (TextBox)grvKpiNu.Rows[e.RowIndex].FindControl("TextBox1");
            string strbnchmrk = txtBnchmrk.Text;

            string strUpdate = " UPDATE MANAGE_KPI_PARAMETERS   SET BENCHMARK ='" + strbnchmrk + "' WHERE MANAGE_KPI_PARAMETERS_ID='" + strKpiId + "'";
            objServiceHandler.ExecuteQuery(strUpdate);
            lblMsg.Text = "Update Successfully";


            grvKpiNu.EditIndex = -1;
            LoadGrid2();

            //  LoadVoucherDetails(strVoucher);
            string strRemarks = "kpi_id:" + strKpiId + ", Benchmark : " + strbnchmrk;
            SaveAuditLog("Update", strRemarks);
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    private void SaveAuditLog(string strOperationType, string strRemarks)
    {
        objSysAdmin.SetSeessionData(Session["Branch_ID"].ToString());
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}