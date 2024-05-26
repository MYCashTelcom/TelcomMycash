﻿using System;
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

public partial class COMI_DISP_frmBroadcastSummary : System.Web.UI.Page
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
    protected void btnBroadcast_Click(object sender, EventArgs e)
    {
        String strSQL = "SELECT CM.COMI_MASTER_NAME,CM.COMI_STRAT_DATE,CM.COMI_END_DATE, BS.CLINT_NAME,"
                      + "BS.ACCNT_NO,BS.REQUEST_PARTY,BS.CIMISSION_MONTH,BS.TOTAL_BRADCASTED,BS.ACKNOW_STATUS "
                      + "FROM COM_BROADCAST_SUMMARY BS,COMMISSION_MASTER CM WHERE BS.COMMISSION_ID=CM.COMI_MASTER_ID "
                      + "AND  BS.COMMISSION_ID='"+ddlLoadedFile.SelectedValue+"'";

        try
        {
            Session["CompanyBranch"] = "ROBI";
            Session["ReportSQL"] = strSQL;
            Session["RequestForm"] = "../COMI_DISP/frmBroadcastSummary.aspx";
            Session["ReportFile"] = "../COMI_DISP/COMI_RPT_BC_Summary.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
            SaveAuditInfo("Preview", "System Menu Manager");
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
