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

public partial class MIS_frmMISTopRequest : System.Web.UI.Page
{
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
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
            LoadThanaDistrict();
            ddlThana.Enabled = false;
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (rblAllRank.SelectedValue == "0")
        {
            AllRankReport();
        }
        else if (rblAllRank.SelectedValue == "1")
        {
            IndividualRankReport();
        }       
    }

    private void IndividualRankReport()
    {
        string strSql = "", strTop = "", strDateRange = "",strQuery="";

        if (rbtnDateRange.Checked == true)
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CAT.CAS_TRAN_DATE,'DD/MM/YYYY')) BETWEEN  TO_DATE('" + dptFromDate.DateString + "') AND TO_DATE('" + dtpToDate.DateString + "')";
        }
        strSql = " SELECT CAL.CAS_ACC_NO,CL.CLINT_NAME,AR.RANK_TITEL,COUNT(DISTINCT CAT.REQUEST_ID)TOTAL_REQUEST ,'" + ddlRankList.SelectedItem.ToString() + " Performance Report' REPORT_NAME,'"+dptFromDate.DateString+"' F_DATE,'"+dtpToDate.DateString+"' T_DATE"
               + " FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT,BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,CLIENT_BANK_ACCOUNT CBA,ACCOUNT_LIST AL,"
               + " CLIENT_LIST CL,ACCOUNT_RANK AR WHERE CAT.CAS_ACC_ID=CAL.CAS_ACC_ID AND AR.RANK_TITEL='"+ddlRankList.SelectedItem.ToString()+"' AND "
               + " CAL.CAS_ACC_NO=CBA.CLINT_BANK_ACC_NO AND CBA.ACCNT_ID= AL.ACCNT_ID AND AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID "
               + " AND CAT.REQUEST_ID IS NOT NULL AND AR.STATUS='A' " + strDateRange
               + " GROUP BY CAS_ACC_NO,CLINT_NAME,RANK_TITEL ORDER BY TOTAL_REQUEST desc";

        if (txtFirst.Text != "")
        {            
            strQuery = " SELECT * from(" + strSql + ")where rownum<=" + Convert.ToInt32(txtFirst.Text) + "";
        }
        else
        {
            strQuery = strSql;
        }

        try
        {
            Session["ReportSQL"] = strQuery;
            Session["RequestForm"] = "../MIS/frmMISTopRequest.aspx";
            Session["ReportFile"] = "../MIS/MIS_TOP_REQUEST.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
            SaveAuditInfo("View", "Top Request");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void AllRankReport()
    {
        string strSql = "", strTop = "", strDateRange = "", strQuery = "";
             
  

        if (rbtnDateRange.Checked == true)
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CAT.CAS_TRAN_DATE,'DD/MM/YYYY')) BETWEEN  TO_DATE('" + dptFromDate.DateString + "') AND TO_DATE('" + dtpToDate.DateString + "')";
        }
        strSql = " SELECT CAL.CAS_ACC_NO,CL.CLINT_NAME,AR.RANK_TITEL,COUNT(DISTINCT CAT.REQUEST_ID)TOTAL_REQUEST ,'" + rblAllRank.SelectedItem + " Performance Report' REPORT_NAME,'" + dptFromDate.DateString + "' F_DATE,'" + dtpToDate.DateString + "' T_DATE"
               + " FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT,BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,CLIENT_BANK_ACCOUNT CBA,ACCOUNT_LIST AL,"
               + " CLIENT_LIST CL,ACCOUNT_RANK AR WHERE CAT.CAS_ACC_ID=CAL.CAS_ACC_ID AND "
               + " CAL.CAS_ACC_NO=CBA.CLINT_BANK_ACC_NO AND CBA.ACCNT_ID= AL.ACCNT_ID AND AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID "
               + " AND CAT.REQUEST_ID IS NOT NULL AND AR.STATUS='A' " + strDateRange
               + " GROUP BY CAS_ACC_NO,CLINT_NAME,RANK_TITEL ORDER BY TOTAL_REQUEST desc";

        if (txtFirst.Text != "")
        {          
            strQuery = " SELECT * from(" + strSql + ")where rownum<=" + Convert.ToInt32(txtFirst.Text) + "";
        }
        else
        {
            strQuery = strSql;
        }
        try
        {
            Session["ReportSQL"] = strQuery;
            SaveAuditInfo("View", "Top Request");
            Session["RequestForm"] = "../MIS/frmMISTopRequest.aspx";
            Session["ReportFile"] = "../MIS/MIS_TOP_REQUEST.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");           
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        //objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
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


    protected void btnViewThanaDist_Click(object sender, EventArgs e)
    {
        if (rblAllIndiThanaDist.SelectedValue == "0")
        {
            AllThanaDistrictRankReport();  
        }
        else if (rblAllIndiThanaDist.SelectedValue == "1")
        {
            IndividualThanaDistrictRankReport();
        }   
    }

    private void IndividualThanaDistrictRankReport()
    {
        string strSql = "", strTop = "", strDateRange = "", strQuery = "", strSQLThanaDist = "";

        //----------- Thana and District Query------------------
        if (rblAllArea.SelectedValue == "0")
        {
            strSQLThanaDist = " AND MD.DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "'";
        }
        else if (rblAllArea.SelectedValue == "1")
        {
            strSQLThanaDist = " AND MD.DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "' AND MT.THANA_ID='" + ddlThana.SelectedValue.ToString() + "'";
        }
        //--------------- Date range Query ------------------
        if (rbtnThanaDistDateRange.Checked == true)
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CAT.CAS_TRAN_DATE,'DD/MM/YYYY')) BETWEEN  TO_DATE('" + dtpThanaDistFdate.DateString + "') AND TO_DATE('" + dtpThanaDistTdate.DateString + "')";
        }
        //------------------------ Main query start here --------------------
        strSql = " SELECT CAL.CAS_ACC_NO,CL.CLINT_NAME,MT.THANA_NAME,MD.DISTRICT_NAME,AR.RANK_TITEL,COUNT(DISTINCT CAT.REQUEST_ID)TOTAL_REQUEST ,'" + ddlIndividualRank.SelectedItem + " Performance Report' REPORT_NAME,'" + dtpThanaDistFdate.DateString + "' F_DATE,'" + dtpThanaDistTdate.DateString + "' T_DATE"
               + " FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT,BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,CLIENT_BANK_ACCOUNT CBA,ACCOUNT_LIST AL,"
               + " CLIENT_LIST CL,ACCOUNT_RANK AR,MANAGE_THANA MT,MANAGE_DISTRICT MD WHERE CAT.CAS_ACC_ID=CAL.CAS_ACC_ID AND AR.RANK_TITEL='" + ddlIndividualRank.SelectedItem.ToString() + "' AND "
               + " CAL.CAS_ACC_NO=CBA.CLINT_BANK_ACC_NO AND CBA.ACCNT_ID= AL.ACCNT_ID AND AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID " + strSQLThanaDist
               + " AND CAT.REQUEST_ID IS NOT NULL AND AR.STATUS='A' AND CL.THANA_ID=MT.THANA_ID AND MD.DISTRICT_ID=MT.DISTRICT_ID " + strDateRange
               + " GROUP BY CAS_ACC_NO,CLINT_NAME,RANK_TITEL,THANA_NAME,DISTRICT_NAME ORDER BY TOTAL_REQUEST desc";

        if (txtTopPerformer.Text != "")
        {
            strQuery = " SELECT * from(" + strSql + ")where rownum<=" + Convert.ToInt32(txtTopPerformer.Text) + "";
        }
        else
        {
            strQuery = strSql;
        }
        try
        {
            Session["ReportSQL"] = strQuery;
            SaveAuditInfo("View", "Top Request");
            Session["RequestForm"] = "../MIS/frmMISTopRequest.aspx";
            if (rblAllArea.SelectedValue == "0")
            {
                Session["ReportFile"] = "../MIS/MIS_TOP_REQUEST_DISTRICT_WISE.rpt";
            }
            else if (rblAllArea.SelectedValue == "1")
            {
                Session["ReportFile"] = "../MIS/MIS_TOP_REQUEST_THANA_WISE.rpt";
            }

            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void AllThanaDistrictRankReport()
    {
        string strSql = "", strTop = "", strDateRange = "", strQuery = "", strSQLThanaDist = "";

        //----------- Thana and District Query------------------
        if (rblAllArea.SelectedValue == "0")
        {
            strSQLThanaDist = " AND MD.DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "'";
        }
        else if (rblAllArea.SelectedValue == "1")
        {
            strSQLThanaDist = " AND MD.DISTRICT_ID='" + ddlDistrict.SelectedValue.ToString() + "' AND MT.THANA_ID='" + ddlThana.SelectedValue.ToString() + "'";
        }
        //--------------- Date range Query ------------------
        if (rbtnThanaDistDateRange.Checked == true)
        {
            strDateRange = " AND TO_CHAR(TO_DATE(CAT.CAS_TRAN_DATE,'DD/MM/YYYY')) BETWEEN  TO_DATE('" + dtpThanaDistFdate.DateString + "') AND TO_DATE('" + dtpThanaDistTdate.DateString + "')";
        }
        //------------------------ Main query start here --------------------
        strSql = " SELECT CAL.CAS_ACC_NO,CL.CLINT_NAME,MT.THANA_NAME,MD.DISTRICT_NAME,AR.RANK_TITEL,COUNT(DISTINCT CAT.REQUEST_ID)TOTAL_REQUEST ,'" + rblAllIndiThanaDist.SelectedItem + " Performance Report' REPORT_NAME,'" + dtpThanaDistFdate.DateString + "' F_DATE,'" + dtpThanaDistTdate.DateString + "' T_DATE"
               + " FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT,BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,CLIENT_BANK_ACCOUNT CBA,ACCOUNT_LIST AL,"
               + " CLIENT_LIST CL,ACCOUNT_RANK AR,MANAGE_THANA MT,MANAGE_DISTRICT MD WHERE CAT.CAS_ACC_ID=CAL.CAS_ACC_ID AND "
               + " CAL.CAS_ACC_NO=CBA.CLINT_BANK_ACC_NO AND CBA.ACCNT_ID= AL.ACCNT_ID AND AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID " + strSQLThanaDist
               + " AND CAT.REQUEST_ID IS NOT NULL AND AR.STATUS='A' AND CL.THANA_ID=MT.THANA_ID AND MD.DISTRICT_ID=MT.DISTRICT_ID " + strDateRange
               + " GROUP BY CAS_ACC_NO,CLINT_NAME,RANK_TITEL,THANA_NAME,DISTRICT_NAME ORDER BY TOTAL_REQUEST desc";

        if (txtTopPerformer.Text != "")
        {
            strQuery = " SELECT * from(" + strSql + ")where rownum<=" + Convert.ToInt32(txtTopPerformer.Text) + "";
        }
        else
        {
            strQuery = strSql;
        }
        try
        {
            Session["ReportSQL"] = strQuery;
            SaveAuditInfo("View", "Top Request");
            Session["RequestForm"] = "../MIS/frmMISTopRequest.aspx";
            if (rblAllArea.SelectedValue == "0")
            {
                Session["ReportFile"] = "../MIS/MIS_TOP_REQUEST_DISTRICT_WISE.rpt";
            }
            else if (rblAllArea.SelectedValue == "1")
            {
                Session["ReportFile"] = "../MIS/MIS_TOP_REQUEST_THANA_WISE.rpt";
            }

            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
