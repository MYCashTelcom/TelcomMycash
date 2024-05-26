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

public partial class COMMON_frmKYCUpdateAndVerificationReport : System.Web.UI.Page
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
    protected void btnKYCAll_Click(object sender, EventArgs e)
    {
        string strSql = "", strDateRange = "", strAddDate="";
        if (rbtnAllKYC.SelectedValue == "1")
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CL.UPDATE_DATE,'DD/MM/YYYY')) BETWEEN TO_DATE(\'" + dptFromDateAllKYC.DateString
                         + "\',\'DD/MM/YYYY\') AND TO_DATE(\'" + dtpToDateAllKYC.DateString + "\',\'DD/MM/YYYY\') ORDER BY CL.UPDATE_DATE";
            strAddDate = ",'" + dptFromDateAllKYC.DateString + "' F_DATE,'" + dtpToDateAllKYC.DateString + "' T_DATE ";
        }
        else if (rbtnAllKYC.SelectedValue == "0")
        {
            strDateRange = "";
            strAddDate = " ,'' F_DATE , '' T_DATE ";
        }
        strSql = " SELECT AL.ACCNT_NO,CL.CLINT_NAME,CL.UPDATE_DATE,CSU.SYS_USR_LOGIN_NAME,SERIAL_NO,SP.SERVICE_PKG_NAME " + strAddDate
               + " FROM ACCOUNT_LIST AL,CLIENT_LIST CL ,CM_SYSTEM_USERS CSU,ACCOUNT_SERIAL_DETAIL ASD, SERVICE_PACKAGE SP "
               + " WHERE CL.CLINT_ID=AL.CLINT_ID AND CSU.ACCNT_ID=CL.KYC_UPDATED_BY AND CSU.SYS_USR_GRP_ID  IN('12052901001001','12050401001001','12082901001001') AND AL.ACCNT_MSISDN=ASD.CUSTOMER_MOBILE_NO AND SP.SERVICE_PKG_ID=AL.SERVICE_PKG_ID " + strDateRange;

        try
        {
            SaveAuditInfo("View", "KYC Update All");
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmKYCUpdateAndVerificationReport.aspx";
            Session["ReportFile"] = "../COMMON/KYC_UPDATE.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnKYCIndividual_Click(object sender, EventArgs e)
    {
        string strSql = "", strDateRange = "",strAddDate="";
        if (rbtnIndividualKYC.SelectedValue == "1")
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CL.UPDATE_DATE,'DD/MM/YYYY')) BETWEEN TO_DATE(\'" + dptFromDateIndividualKYC.DateString
                         + "\',\'DD/MM/YYYY\') AND TO_DATE(\'" + dptToIndividualKYC.DateString + "\',\'DD/MM/YYYY\') ORDER BY CL.UPDATE_DATE";
            strAddDate = " ,'" + dptFromDateIndividualKYC.DateString + "' F_DATE,'" + dptToIndividualKYC.DateString + "' T_DATE ";
        }
        else if (rbtnIndividualKYC.SelectedValue == "0")
        {
            strDateRange = "";
            strAddDate = " ,'' F_DATE , '' T_DATE ";
        }

        strSql = " SELECT AL.ACCNT_NO,CL.CLINT_NAME,CL.UPDATE_DATE,CSU.SYS_USR_LOGIN_NAME,SERIAL_NO,SP.SERVICE_PKG_NAME"+strAddDate
               + " FROM ACCOUNT_LIST AL,CLIENT_LIST CL ,CM_SYSTEM_USERS CSU,ACCOUNT_SERIAL_DETAIL ASD, SERVICE_PACKAGE SP "
               + " WHERE CL.CLINT_ID=AL.CLINT_ID AND CSU.ACCNT_ID=CL.KYC_UPDATED_BY AND CSU.SYS_USR_GRP_ID  IN('12052901001001','12050401001001','12082901001001','14081401001001') AND AL.ACCNT_MSISDN=ASD.CUSTOMER_MOBILE_NO AND SP.SERVICE_PKG_ID=AL.SERVICE_PKG_ID AND CSU.ACCNT_ID='"
               + ddlIndividualKYCUpdate .SelectedValue.ToString()+ "' " + strDateRange;

        try
        {
            SaveAuditInfo("View", "KYC Update Individual");
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmKYCUpdateAndVerificationReport.aspx";
            Session["ReportFile"] = "../COMMON/KYC_UPDATE_INDIVIDUAL.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnKYCVerificationAll_Click(object sender, EventArgs e)
    {
        string strSql = "", strDateRange = "", strAddDate = "";

        if (rblAllVerification.SelectedValue == "1")
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CL.VERIFIED_DATE,'DD/MM/YYYY')) BETWEEN TO_DATE(\'" + dptFromDateAllKYCVerification.DateString
                         + "\',\'DD/MM/YYYY\') AND TO_DATE(\'" + dptToDateAllKYCVerification.DateString + "\',\'DD/MM/YYYY\') ORDER BY CL.VERIFIED_DATE,ACCNT_NO";
            strAddDate = " ,'" + dptFromDateAllKYCVerification.DateString + "' F_DATE,'" + dptToDateAllKYCVerification.DateString + "' T_DATE ";
        }
        else if (rblAllVerification.SelectedValue == "0")
        {
            strDateRange = "";
            strAddDate = " ,'' F_DATE , '' T_DATE ";
        }

        strSql = " SELECT DISTINCT AL.ACCNT_NO,CL.CLINT_NAME,CL.VERIFIED_DATE,CSU.SYS_USR_LOGIN_NAME,SERIAL_NO,SP.SERVICE_PKG_NAME " + strAddDate + " FROM ACCOUNT_LIST AL,"
               + " CLIENT_LIST CL ,CM_SYSTEM_USERS CSU,ACCOUNT_SERIAL_DETAIL ASD, SERVICE_PACKAGE SP WHERE CL.CLINT_ID=AL.CLINT_ID AND CSU.ACCNT_ID=CL.VERIFIED_BY AND AL.ACCNT_MSISDN=ASD.CUSTOMER_MOBILE_NO AND SP.SERVICE_PKG_ID=AL.SERVICE_PKG_ID " + strDateRange;
        try
        {
            SaveAuditInfo("View", "KYC Verification All");
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmAccountHierarchyReport.aspx";
            Session["ReportFile"] = "../COMMON/KYC_VERIFIED.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnKYCVericIndi_Click(object sender, EventArgs e)
    {
        string strSql = "", strDateRange = "", strAddDate="";

        if (rblIndividualVerification.SelectedValue == "1")
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CL.VERIFIED_DATE,'DD/MM/YYYY')) BETWEEN TO_DATE(\'" + dtpFromIndiviDate.DateString
                         + "\',\'DD/MM/YYYY\') AND TO_DATE(\'" + dtpToIndivDate.DateString + "\',\'DD/MM/YYYY\') ORDER BY CL.VERIFIED_DATE";
            strAddDate = ",'" + dtpFromIndiviDate.DateString + "' F_DATE,'" + dtpToIndivDate.DateString + "' T_DATE " ;
        }
        else if (rblIndividualVerification.SelectedValue == "0")
        {
            strDateRange = "";
            strAddDate = " ,'' F_DATE , '' T_DATE ";
        }

        strSql = " SELECT AL.ACCNT_NO,CL.CLINT_NAME,CL.VERIFIED_DATE,CSU.SYS_USR_LOGIN_NAME,SERIAL_NO,SP.SERVICE_PKG_NAME " + strAddDate + " FROM ACCOUNT_LIST AL,"
               + " CLIENT_LIST CL ,CM_SYSTEM_USERS CSU,ACCOUNT_SERIAL_DETAIL ASD, SERVICE_PACKAGE SP WHERE CL.CLINT_ID=AL.CLINT_ID "
               + " AND CSU.ACCNT_ID=CL.VERIFIED_BY AND CSU.SYS_USR_GRP_ID  IN('12052901001001','12050401001001','12082901001001','14081401001001') "
               + " AND AL.ACCNT_MSISDN=ASD.CUSTOMER_MOBILE_NO AND SP.SERVICE_PKG_ID=AL.SERVICE_PKG_ID "
               + " AND  CSU.ACCNT_ID='" + ddlIndividualVerification.SelectedValue.ToString() + "' " + strDateRange;
        try
        {
            SaveAuditInfo("View", "KYC Verification Individual");
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmAccountHierarchyReport.aspx";
            Session["ReportFile"] = "../COMMON/KYC_VERIFIED_INDIVIDUAL.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
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
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType,IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnShowMicareReport_Click(object sender, EventArgs e)
    {
        string strSql = "", strDateRange = "", strAddDate = "";
        if (rblMicareKYCUpdate.SelectedValue == "1")
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CL.UPDATE_DATE,'DD/MM/YYYY')) BETWEEN TO_DATE(\'" +dtpMicareFromDate.DateString
                         + "\',\'DD/MM/YYYY\') AND TO_DATE(\'" + dtpMicateToDate.DateString + "\',\'DD/MM/YYYY\') ORDER BY CL.UPDATE_DATE";
            strAddDate= ",'" + dtpMicareFromDate.DateString + "' F_DATE,'" + dtpMicateToDate.DateString + "' T_DATE ";
        }
        else if (rblMicareKYCUpdate.SelectedValue == "0")
        {
            strDateRange = "";
            strAddDate = " ,'' F_DATE , '' T_DATE ";
        }
        strSql = " SELECT AL1.ACCNT_NO UPDATED_BY, AL.ACCNT_NO,CL.CLINT_NAME,CL.UPDATE_DATE,SERIAL_NO,SP.SERVICE_PKG_NAME " + strAddDate
               + " FROM ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_SERIAL_DETAIL ASD,SERVICE_PACKAGE SP,ACCOUNT_LIST AL1 "
               + " WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.ACCNT_MSISDN=ASD.CUSTOMER_MOBILE_NO AND AL1.ACCNT_ID=CL.KYC_UPDATED_BY"
               + " AND SP.SERVICE_PKG_ID=AL.SERVICE_PKG_ID AND CL.REQUEST_PARTY_TYPE='MICARE' " + strDateRange;

        try
        {
            SaveAuditInfo("View", "KYC Update All for Micare");
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmKYCUpdateAndVerificationReport.aspx";
            Session["ReportFile"] = "../COMMON/KYC_UPDATE_MICARE.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
