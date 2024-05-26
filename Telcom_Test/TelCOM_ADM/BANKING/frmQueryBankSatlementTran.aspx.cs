using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Forms_frmQueryBankSatlementTran : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
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
        DateTime dt = DateTime.Now;
        if (txtFromDate.Text.Equals(""))
        {
            txtFromDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt.AddHours(-6));
            txtToDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt.AddHours(6));
            LoadRequestList();
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
    public void LoadRequestList()
    {
        String strSQL = "SELECT BANK_TRAN_ID, BANK_TRAN_DESC, BANK_TRAN_DTL_ID, BANK_ACCOUNT_NO, AC_NAME, CLINT_BANK_ACC_NO,"
                      + "BANK_INTERNAL_CODE, BANK_TRAN_DATE, DEBIT, CREDIT, IS_SATLEMENT_TRAN FROM ALL_BANK_TRANSACTION WHERE IS_SATLEMENT_TRAN='Y' AND BANK_TRAN_DATE "
                      + "BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";

        //+ " SR.ACCNT_ID='" + ddlAccountList.SelectedValue + "' AND SR.REQUEST_TIME "
        sdsBankTrans.SelectCommand = strSQL;
        sdsBankTrans.DataBind();
        grvRequestList.DataBind();
    }
    protected void ddlAccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadRequestList();
        SaveAuditInfo("Search", "Inter Bank Settlement");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
