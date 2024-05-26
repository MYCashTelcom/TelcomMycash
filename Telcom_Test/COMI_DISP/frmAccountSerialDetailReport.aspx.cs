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

public partial class COMI_DISP_frmAccountSerialDetailReport : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsAccountHandler objAccount = new clsAccountHandler();
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
        lblResult.Text = "";
        txtMobileNo.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
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
    protected void btnReportPrint_Click(object sender, EventArgs e)
    {
        string strSql = "", strMobileNo = "", strReportFile = "";
        try
        {
            if (txtStartSLNo.Text.ToString() != "" && txtEndSLNo.Text.ToString() != "" && txtMobileNo.Text != "")
            {
                lblResult.Text = "Please Select Mobile Number or Form Serial Number.";
            }
            else if (txtStartSLNo.Text.ToString() != "" && txtEndSLNo.Text.ToString() == "")
            {
                strSql = " SELECT * FROM  ACCOUNT_SERIAL_DETAIL WHERE SERIAL_NO ='" + txtStartSLNo.Text.ToString().Trim() + "' ORDER BY ACCNT_SL_DETIL_ID ASC";
                strReportFile = "../COMI_DISP/rptAccountSlInfoDetails.rpt";
                //Session["CompanyBranch"] = "MBL";
                Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMI_DISP/frmAccountSerialDetailReport.aspx";
                Session["ReportFile"] = strReportFile;
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            else if (txtStartSLNo.Text.ToString() != "" && txtEndSLNo.Text.ToString() != "")
            {
                if (Convert.ToInt32(txtStartSLNo.Text) < Convert.ToInt32(txtEndSLNo.Text))
                {
                    strSql = " SELECT * FROM  ACCOUNT_SERIAL_DETAIL WHERE SERIAL_NO BETWEEN " + txtStartSLNo.Text.ToString().Trim() + " AND " + txtEndSLNo.Text.ToString().Trim() + " ORDER BY ACCNT_SL_DETIL_ID ASC";
                    strReportFile = "../COMI_DISP/rptAccountSlInfoDetails.rpt";
                    //Session["CompanyBranch"] = "MBL";
                    Session["ReportSQL"] = strSql;
                    Session["RequestForm"] = "../COMI_DISP/frmAccountSerialDetailReport.aspx";
                    Session["ReportFile"] = strReportFile;
                    Response.Redirect("../COM/COM_ReportView.aspx");
                }
                else
                {
                    lblResult.Text = "From SL No should be Less than To SL No.";
                }
            }
            else if (txtMobileNo.Text != "")
            {
                strMobileNo = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_SERIAL_DETAIL", "CUSTOMER_MOBILE_NO", "CUSTOMER_MOBILE_NO", txtMobileNo.Text.Trim());

                if (strMobileNo != "")
                {
                    strSql = " SELECT * FROM  ACCOUNT_SERIAL_DETAIL WHERE CUSTOMER_MOBILE_NO='" + txtMobileNo.Text.Trim() + "'";
                    strReportFile = "../COMI_DISP/rptAccountSlInfoDetails.rpt";
                    //Session["CompanyBranch"] = "MBL";
                    Session["ReportSQL"] = strSql;
                    Session["RequestForm"] = "../COMI_DISP/frmAccountSerialDetailReport.aspx";
                    Session["ReportFile"] = strReportFile;
                    Response.Redirect("../COM/COM_ReportView.aspx");
                }
                else
                {
                    strSql = " SELECT CLINT_MOBILE,CREATION_DATE  FROM CLIENT_LIST CL WHERE CL.CLINT_MOBILE='" + txtMobileNo.Text.Trim() + "'";
                    strReportFile = "../COMI_DISP/ACCOUNT_SERIAL_DETAIL.rpt";
                   // Session["CompanyBranch"] = "MBL";
                    Session["ReportSQL"] = strSql;
                    Session["RequestForm"] = "../COMI_DISP/frmAccountSerialDetailReport.aspx";
                    Session["ReportFile"] = strReportFile;
                    Response.Redirect("../COM/COM_ReportView.aspx");
                }
            }
            else
            {
                strSql = " SELECT * FROM  ACCOUNT_SERIAL_DETAIL WHERE STATUS= '" + rdbSelectiontype.SelectedValue.ToString() + "' ORDER BY ACCNT_SL_DETIL_ID ASC";
                strReportFile = "../COMI_DISP/rptAccountSlInfoDetails.rpt";
              //  Session["CompanyBranch"] = "MBL";
                Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMI_DISP/frmAccountSerialDetailReport.aspx";
                Session["ReportFile"] = strReportFile;
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            SaveAuditInfo("View", "Account Serial Detail Report");
        }
        catch (Exception ex)
        {
            lblResult.Text = ex.ToString();
        }
    }
}
