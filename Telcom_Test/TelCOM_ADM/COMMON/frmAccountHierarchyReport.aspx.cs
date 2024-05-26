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

public partial class COMMON_frmAccountHierarchyReport : System.Web.UI.Page
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string strSql = "";
        if (ddlSelectHierarchyType.SelectedValue.Equals("B"))
        {
            try
            {
                strSql = " SELECT CL.CLINT_NAME WALLET_NAME,AL.ACCNT_NO WALLET_ID,CL.WORK_EDU_BUSINESS,CL.CLIENT_OFFIC_ADDRESS,AR.RANK_TITEL WALLET_RANK,CLIENT_RANK(AH.HIERARCHY_ACCNT_ID)PARENT_INFO,'Account Bounded' REPORT_HEADER  "
                       + " FROM ACCOUNT_HIERARCHY AH, ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_RANK AR WHERE AH.ACCNT_ID=AL.ACCNT_ID AND "
                       + " AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID ORDER BY AL.ACCNT_RANK_ID,CL.CLINT_NAME ";
                Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMMON/frmAccountHierarchyReport.aspx";
                Session["ReportFile"] = "../COMMON/crptAccHirchyBounded.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        else if (ddlSelectHierarchyType.SelectedValue.Equals("N"))
        {
            try
            {
                //strSql = " SELECT DISTINCT CL.CLINT_NAME CLINT_NAME,ACCNT_NO ,CL.WORK_EDU_BUSINESS,CL.CLIENT_OFFIC_ADDRESS,ACCNT_MSISDN ,"
                //       + " AR.RANK_TITEL ,'Account Not Bounded' REPORT_HEADER FROM ACCOUNT_LIST AL,ACCOUNT_RANK AR, CLIENT_LIST CL "
                //       + " WHERE AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID= "
                //       + "CL.CLINT_ID AND AL.ACCNT_RANK_ID IN(120519000000000002,120519000000000003, "
                //       + "120519000000000004,120519000000000005,120712000000000002,120712000000000001,"
                //       + "120712000000000003,120712000000000004,120712000000000006,120712000000000007, "
                //       + "120712000000000008,120712000000000009) AND AL.ACCNT_ID NOT IN "
                //       + "(SELECT ACCNT_ID FROM ACCOUNT_HIERARCHY)";

                //strSql = " SELECT DISTINCT ACCNT_ID,ACCNT_NO,ACCNT_MSISDN,CLINT_NAME,'Account Not Bounded' REPORT_HEADER FROM ACCOUNT_LIST AL,CLIENT_LIST CL "
                //       + " WHERE AL.CLINT_ID=CL.CLINT_ID AND ACCNT_ID NOT IN (SELECT DISTINCT ACCNT_ID FROM ACCOUNT_HIERARCHY) ORDER BY CLINT_NAME";



                strSql =  " SELECT DISTINCT CL.CLINT_NAME CLINT_NAME,ACCNT_NO ,CL.WORK_EDU_BUSINESS,CL.CLIENT_OFFIC_ADDRESS,ACCNT_MSISDN , CL.THANA_ID,  MT.THANA_NAME, "
                        + " MD.DISTRICT_ID, MD.DISTRICT_NAME, AR.RANK_TITEL ,'Account Not Bounded' REPORT_HEADER FROM ACCOUNT_LIST AL,ACCOUNT_RANK AR, CLIENT_LIST CL, " 
                        + " MANAGE_THANA MT, MANAGE_DISTRICT MD WHERE AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID= CL.CLINT_ID " 
                        + " AND AL.ACCNT_RANK_ID IN(120519000000000002,120519000000000003, 120519000000000004,120519000000000005,120712000000000002,120712000000000001,"
                        + " 120712000000000003,120712000000000004,120712000000000006,120712000000000007, 120712000000000008,120712000000000009) AND "
                        + " CL.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID(+) AND AL.ACCNT_ID NOT IN  (SELECT ACCNT_ID FROM ACCOUNT_HIERARCHY)";
                
                
                Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMMON/frmAccountHierarchyReport.aspx";
                Session["ReportFile"] = "../COMMON/crptAccHirchyNotBounded.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        SaveAuditInfo("View", "Account Hierarchy Report");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnParentInfoSearch_Click(object sender, EventArgs e)
    {
        string strSqlData = "";
        btnReportView.Visible = true;
        try
        {
            strSqlData = " SELECT AL.ACCNT_MSISDN ,CL.CLINT_NAME,SP.SERVICE_PKG_NAME ,AR.RANK_TITEL FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR "
                       + "  WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.ACCNT_NO='" + txtAccountNo.Text.ToString() + "'";

            sdsParentReportInfo.SelectCommand = strSqlData;
            sdsParentReportInfo.DataBind();
            dtvParentReportInfo.DataBind();
            if (dtvParentReportInfo.Rows.Count > 0)
            {
                dtvParentReportInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnReportView_Click(object sender, EventArgs e)
    {
        string strAccountID = "", strSql = "";
        strAccountID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", txtAccountNo.Text.Trim());
        if (strAccountID != "")
        {
            try
            {
                Label dtvParentName = (Label)dtvParentReportInfo.FindControl("lblClientName");
                Label dtvParentMobile = (Label)dtvParentReportInfo.FindControl("lblAccMobile"); 
                Label dtvParentServicePackage = (Label)dtvParentReportInfo.FindControl("lblServicePackage"); 
                Label dtvParentRank = (Label)dtvParentReportInfo.FindControl("lblRankTitle");
                strSql = " SELECT AC.CLINT_NAME,AC.ACCNT_NO,AC.ACCNT_MSISDN,AC.RANK_TITEL,AC.SERVICE_PKG_NAME,'" + dtvParentName.Text + "' PARENT_NAME,'" + txtAccountNo.Text.Trim()+ "' PARENT_MOBILE,'" + dtvParentServicePackage.Text.Trim() + "' PARENT_SERVICE_PACKAGE,'" + dtvParentRank.Text + "' PARENT_RANK  FROM ALL_HIERARCHY_ACCNT AC WHERE HIERARCHY_ACCNT_ID='" + strAccountID + "'";
                Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMMON/frmAccountHierarchyReport.aspx";
                Session["ReportFile"] = "../COMMON/crptAccParentReport.rpt";
                SaveAuditInfo("View", "Account Hierarchy Report");
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
    protected void ddlSelectHierarchyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnReport.Visible = true;
    }
}
