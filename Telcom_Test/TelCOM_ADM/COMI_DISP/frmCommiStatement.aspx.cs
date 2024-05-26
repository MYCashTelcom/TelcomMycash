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

public partial class COMI_DISP_frmCommiStatement : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    DateTime dt = DateTime.Now;
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

    protected void btnBroadcast_Click(object sender, EventArgs e)
    {
        String strSQL = "SELECT DISTINCT CL.CLINT_NAME, CL.CLINT_CONTACT_F_NAME, CL.CLIENT_RSP_NAME,CL.CLINET_RSP_CODE,CL.CLINT_ADDRESS1,CL.DISTRIBUTOR_NAME,CL.DISTRIBUTOR_CODE, "
                      + "AL.ACCNT_MSISDN,AL.ACCNT_NO,CM.COMI_STRAT_DATE,CM.COMI_END_DATE,CT.COMI_TYPE_NAME,C.COMMISSION_AMOUNT FROM COMMISSION_MASTER CM,COMMISSION_DATA C,ACCOUNT_LIST AL,"
                      + "CLIENT_LIST CL,COMMISSION_TYPES CT WHERE CM.COMI_MASTER_ID=C.COMI_MASTER_ID AND C.COMI_MASTER_ID='" + ddlLoadedFile.SelectedValue.ToString() + "' AND "
                      + "TRIM('+'||C.RETAILER_MSISDN)=TRIM(SUBSTR(AL.ACCNT_MSISDN,1)) AND Al.CLINT_ID=CL.CLINT_ID AND C.COMI_TYPE_CODE=CT.COMI_TYPE_CODE";
        try
        {
            Session["CompanyBranch"] = "ROBI";
            Session["ReportSQL"] = strSQL;
            Session["RequestForm"] = "../COMI_DISP/frmCommiStatement.aspx";
            Session["ReportFile"] = "../COMI_DISP/COMI_RPT_Comi_Statement.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
            SaveAuditInfo("Preview", "Commission Statement");
        }
        catch (Exception ex)
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
