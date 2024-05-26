using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COMMON_frmManageDevice : System.Web.UI.Page
{
    private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
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
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnSearchWallet_Click(object sender, EventArgs e)
    {
        LoadWalletDetailGrid();
    }

    private void LoadWalletDetailGrid()
    {
        try
        {
            string walletId = txtWalletId.Text.Trim();

            if (walletId.Equals(""))
            {
                lblMsg.Text = "Please input wallet number";
                return;
            }

            string strSql = "SELECT DL.DEVICE_LIST_ID, AL.ACCNT_NO, CL.CLINT_NAME, DL.TERMINAL_NAME, DL.TERMINAL_SERIAL_NO, DECODE(DL.ACTIVE_STATUS,'A','Active','Inactive') ACTIVE_STATUS, TO_CHAR(DL.ACTIVATION_DATE) ACTIVATION_DATE FROM ACCOUNT_LIST AL, DEVICE_LIST DL, DEVICE_ACCOUNT_LIST DAL, CLIENT_LIST CL WHERE AL.ACCNT_ID = DAL.ACCNT_ID AND DL.DEVICE_LIST_ID = DAL.DEVICE_LIST_ID AND AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_NO = '" + walletId + "'";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvWalletDetails.DataSource = oDs;
            gdvWalletDetails.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void gdvWalletDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strId = gdvWalletDetails.DataKeys[e.RowIndex].Values[0].ToString();

            string strMsg = objServiceHandler.DeleteDeviceDetail(strId);
            if (strMsg == "Successful")
            {
                lblMsg.Text = "Data Deleted Successfully";
            }
            else
            {
                lblMsg.Text = "Data Deleted Failed";
            }

            LoadWalletDetailGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}