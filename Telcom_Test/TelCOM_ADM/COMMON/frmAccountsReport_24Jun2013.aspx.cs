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

public partial class COMMON_frmAccountsReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //try
            //{
            //    strUserName = Session["UserLoginName"].ToString();
            //    strPassword = Session["Password"].ToString();
            //}
            //catch
            //{
            //    Session.Clear();
            //    Response.Redirect("../frmSeesionExpMesage.aspx");
            //}
            dptFromDate.DateString = Convert.ToString(TimeSpan.Zero);
            LoadThanaDistrict();
            ddlThana.Enabled = false;
        }
    }
    private void LoadThanaDistrict()
    {
        string strDistrict = "";
        strDistrict = " SELECT DISTRICT_ID ,DISTRICT_NAME FROM MANAGE_DISTRICT ";
        sdsDistrict.SelectCommand = strDistrict;
        sdsDistrict.DataBind();
        ddlDistrict.DataBind();
        
        string strThana = "";
        strThana = "SELECT THANA_ID,THANA_NAME FROM MANAGE_THANA WHERE DISTRICT_ID='"
                   + ddlDistrict.SelectedValue.ToString() + "' ORDER BY THANA_ID";
        sdsThana.SelectCommand = strThana;
        sdsThana.DataBind();
        ddlThana.DataBind();
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        //-------------------- Rank wise wallet Report ---------------------
        string strSql = "", strDateRange = "";
        try
        {
            if (rbtnRankListItem.SelectedValue == "Date")
            {
                strDateRange = " AND AB.CREATION_DATE BETWEEN TO_DATE ('" + dptFDate.DateString + "','dd/mm/yyyy HH24:MI:SS') AND TO_DATE ('" + dptTDate.DateString + "','dd/mm/yyyy HH24:MI:SS')";
            }
            if (rbtnSelectRank.SelectedValue == "0")
            {
                strSql = "SELECT AB.*,'All Distributor' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='0' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + " AND ACCNT_RANK_ID IN ('120519000000000002','120712000000000001','120712000000000006') ORDER BY ACCNT_NO ";
            }
            else if (rbtnSelectRank.SelectedValue == "1")
            {
                strSql = "SELECT AB.*,'All Delar' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='1' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + " ORDER BY CLINT_NAME ";
            }
            else if (rbtnSelectRank.SelectedValue == "2")
            {
                strSql = "SELECT AB.*,'All Super Agent' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='2' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + " ORDER BY CLINT_NAME ";
            }
            else if (rbtnSelectRank.SelectedValue == "3")
            {
                strSql = "SELECT AB.*,'All Agent' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='3' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + " ORDER BY CLINT_NAME ";
            }
            else if (rbtnSelectRank.SelectedValue == "4")
            {
                strSql = "SELECT AB.*,'All Customer' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='4' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + " ORDER BY CLINT_NAME ";
            }
            Session["CompanyBranch"] = "ROBI";
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmAccountsReport.aspx";
            Session["ReportFile"] = "../COMMON/crptAccountState.rpt";
            SaveAuditInfo("View", "Wallet Reports");
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
        }
    } 
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        //########## Wallet Report ##################
        string strSql = "", strDateRange = "";       
        if (rbtnAllDateRange.SelectedValue == "1")
        {
            strDateRange = " AND CREATION_DATE BETWEEN TO_DATE ('" + dptFromDate.DateString + "','dd/mm/yyyy HH24:MI:SS') AND TO_DATE ('" + dtpToDate.DateString + "','dd/mm/yyyy HH24:MI:SS')";
        }
        if (rbtnSelectState.SelectedValue == "AC")
        {
            strSql = " SELECT AB.*,'All Wallet' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE ACCNT_RANK_ID NOT IN ('120519000000000001','120618000000000002','120618000000000001','120618000000000003','120618000000000004') AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + "  ORDER BY CLINT_NAME ";
        }
        else if (rbtnSelectState.SelectedValue == "A")
        {
            strSql = " SELECT AB.*,'Active Wallet' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE ACCNT_STATE='A' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + " AND ACCNT_RANK_ID NOT IN ('120519000000000001','120618000000000002','120618000000000001','120618000000000003','120618000000000004') ORDER BY CLINT_NAME ASC ";
        }
        else if (rbtnSelectState.SelectedValue == "I")
        {
            strSql = " SELECT AB.*,'Idle Wallet' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE ACCNT_STATE='I' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + " AND ACCNT_RANK_ID NOT IN ('120519000000000001','120618000000000002','120618000000000001','120618000000000003','120618000000000004') ORDER BY CLINT_NAME ASC ";
        }
        else if (rbtnSelectState.SelectedValue == "U")
        {
            strSql = " SELECT AB.*,'UISC Wallet' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE UISC_AGENT='130619002' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strDateRange + " AND ACCNT_RANK_ID NOT IN ('120519000000000001','120618000000000002','120618000000000001','120618000000000003','120618000000000004') ORDER BY CLINT_NAME ASC ";
        }
        try
        {
            Session["CompanyBranch"] = "ROBI";
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmAccountsReport.aspx";
            Session["ReportFile"] = "../COMMON/crptAccountState.rpt";
            SaveAuditInfo("View", "Wallet Reports");
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        //########## Wallet Report ##################
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        //objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    protected void rbtnSelectRank_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlThana.Items.Clear();
            string strSql = "";
            strSql = " SELECT  MT.THANA_ID, MT.THANA_NAME FROM MANAGE_THANA MT, MANAGE_DISTRICT MD "
                   + " WHERE  MD.DISTRICT_ID=MT.DISTRICT_ID  AND  MD.DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "'";
            sdsThana.SelectCommand = strSql;
            sdsThana.DataBind();
            ddlThana.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnAreaWisePreView_Click(object sender, EventArgs e)
    {
        //########## Areawise wallet Report ##################

        string strSql = "", strSQLCondition = "", strThanaDistrict = "", strDateRange="";
        try
        {
            //---------------Add Date Range -----------------
            if (rblAreaWise.SelectedValue == "Date")
            {
                strDateRange = " AND AB.CREATION_DATE BETWEEN TO_DATE ('" + dptAreaWiseFDate.DateString + "','dd/mm/yyyy HH24:MI:SS') AND TO_DATE ('" + dptAreaWiseTDate.DateString + "','dd/mm/yyyy HH24:MI:SS')";
            }
            //------------------ check district and thana query -----------------
            if (rblAllArea.SelectedValue == "0")
            {
                strSQLCondition = " AND DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "'";
            }
            else if (rblAllArea.SelectedValue == "1")
            {
                strSQLCondition = " AND DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "' AND THANA_ID='" + ddlThana.SelectedValue.ToString() + "'";
            }
            //---------------Main Report Query start here------------------
            if (rblRankType.SelectedValue == "0")
            {
                strSql = "SELECT AB.*,'All Distributor' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='0' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strSQLCondition + " " + strDateRange + " AND ACCNT_RANK_ID IN ('120519000000000002','120712000000000001','120712000000000006') ORDER BY CLINT_NAME ";
            }
            else if (rblRankType.SelectedValue == "1")
            {
                strSql = "SELECT AB.*,'All Delar' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='1' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strSQLCondition + " " + strDateRange + " ORDER BY CLINT_NAME ";
            }
            else if (rblRankType.SelectedValue == "2")
            {
                strSql = "SELECT AB.*,'All Super Agent' REPORT_NAME ,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='2' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strSQLCondition + " " + strDateRange + " ORDER BY CLINT_NAME ";
            }
            else if (rblRankType.SelectedValue == "3")
            {
                strSql = "SELECT AB.*,'All Agent' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='3' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strSQLCondition + " " + strDateRange + " ORDER BY CLINT_NAME ";
            }
            else if (rblRankType.SelectedValue == "4")
            {
                strSql = "SELECT AB.*,'All Customer' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE GRADE='4' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT " + strSQLCondition + " " + strDateRange + " ORDER BY CLINT_NAME ";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
        }
        Session["CompanyBranch"] = "MBL";
        Session["ReportSQL"] = strSql;
        Session["RequestForm"] = "../COMMON/frmAccountsReport.aspx";
        Session["ReportFile"] = "../COMMON/ACC_STATE_AREAWISE.rpt";
        SaveAuditInfo("View", "Wallet Reports");
        Response.Redirect("../COM/COM_ReportView.aspx");

        //########## Areawise wallet Report ##################
    }
    protected void rblAllArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAllArea.SelectedValue == "0")
        {
            ddlDistrict.Enabled = true;
            ddlThana.Enabled = false;
        }
        else if (rblAllArea.SelectedValue == "1")
        {
            ddlDistrict.Enabled = true;
            ddlThana.Enabled = true;
        }
    }
    protected void btnShowIndividualRankReport_Click(object sender, EventArgs e)
    {
        string strSql = "", strDateRange = "";
        try
        {
            if (rblIndividualRank.SelectedValue == "D")
            {
                strDateRange = " AND AB.CREATION_DATE BETWEEN TO_DATE ('" + dtpFromIndiviDate.DateString + "','dd/mm/yyyy HH24:MI:SS') AND TO_DATE ('" + dtpToIndivDate.DateString + "','dd/mm/yyyy HH24:MI:SS')";
            }

            strSql = " SELECT AB.*,'" + ddlIndividualRank.SelectedValue.ToString() + "' REPORT_NAME,TPA.AGENT_CATEGORY FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA WHERE RANK_TITEL='" + ddlIndividualRank.SelectedValue.ToString().Trim() + "' " + strDateRange + " AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT ORDER BY CLINT_NAME ";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
        }
        Session["CompanyBranch"] = "ROBI";
        Session["ReportSQL"] = strSql;
        Session["RequestForm"] = "../COMMON/frmAccountsReport.aspx";
        Session["ReportFile"] = "../COMMON/crptAccountState.rpt";
        SaveAuditInfo("View", "Wallet Reports");
        Response.Redirect("../COM/COM_ReportView.aspx");
    }
}
