using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmClientReport : System.Web.UI.Page
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
   
        String strSQL= "SELECT CL.CLINT_ID,CL.CLINT_NAME,CL.CLINT_FATHER_NAME,CL.CLINT_MOTHER_NAME,CL.CLIENT_DOB,CL.OCCUPATION,CL.CLIENT_OFFIC_ADDRESS,CL.CLINT_ADDRESS1,CL.CLINT_ADDRESS2,AL.ACCNT_MSISDN,CL.PUR_OF_TRAN FROM CLIENT_LIST CL,ACCOUNT_LIST AL WHERE CL.CLINT_ID=AL.CLINT_ID AND CL.CLINT_ID='" + DropDownList1.SelectedValue + "'ORDER BY TRIM(CL.CLINT_NAME)";

        try
        {

            Session["CompanyBranch"] = "ROBI";
            Session["ReportSQL"] = strSQL;
            Session["RequestForm"] = "../BANKING/frmClientReport.aspx";
            Session["ReportFile"] = "../BANKING/ClientReport.rpt";
            SaveAuditInfo("View", "KYC Report");
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch(Exception ex)
        {
            ex.Message.ToString();
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
