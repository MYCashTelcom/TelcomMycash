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
using System.Drawing;

public partial class MIS_frmMBL_Various_MIS_Report_4 : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBranch();
            LoadBankList();
            LoadServiceList();
            LoadAccountRank();
            LoadChannelType();
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

    private void LoadBranch()
    {
        try
        {
            string strSql = "SELECT distinct CMP_BRANCH_ID, CMP_BRANCH_NAME FROM CM_CMP_BRANCH";
            DataSet oDs = new DataSet();
            oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlBranch.DataSource = oDs;
            ddlBranch.DataValueField = "CMP_BRANCH_ID";
            ddlBranch.DataTextField = "CMP_BRANCH_NAME";
            ddlBranch.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadBankList()
    {
        try
        {
            string strSql = "SELECT BANK_NAME,BANK_INTERNAL_CODE FROM BANK_LIST WHERE BANK_STATUS='A' ORDER BY BANK_NAME";
            DataSet oDs = new DataSet();
            oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlBankList.DataSource = oDs;
            ddlBankList.DataValueField = "BANK_INTERNAL_CODE";
            ddlBankList.DataTextField = "BANK_NAME";
            ddlBankList.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadServiceList()
    {
        try
        {
            string strSql = "SELECT SERVICE_ID, SERVICE_TITLE FROM SERVICE_LIST ORDER BY SERVICE_TITLE";
            DataSet oDs = new DataSet();
            oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlService.DataSource = oDs;
            ddlService.DataValueField = "SERVICE_ID";
            ddlService.DataTextField = "SERVICE_TITLE";
            ddlService.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadAccountRank()
    {
        try
        {
            string strSql = " SELECT ACCNT_RANK_ID, RANK_TITEL FROM ACCOUNT_RANK WHERE STATUS='A' ORDER BY ACCNT_RANK_ID ASC";
            DataSet oDs = new DataSet();
            oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlAccountRank.DataSource = oDs;
            ddlAccountRank.DataValueField = "ACCNT_RANK_ID";
            ddlAccountRank.DataTextField = "RANK_TITEL";
            ddlAccountRank.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadChannelType()
    {
        try
        {
            string strSql = " SELECT * FROM CHANNEL_TYPE ORDER BY CHANNEL_TYPE";
            DataSet oDs = new DataSet();
            oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlChannelName.DataSource = oDs;
            ddlChannelName.DataValueField = "CHANNEL_TYPE_ID";
            ddlChannelName.DataTextField = "CHANNEL_TYPE";
            ddlChannelName.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnMobCCT_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT   MIS.REQUEST_ID, MIS.TRANSACTION_DATE, MIS.SERVICE_CODE, "
                + " MIS.TRANSACTION_AMOUNT, MIS.REQUEST_PARTY, MIS.RECEPENT_PARTY FROM   ACCOUNT_LIST AL, "
                + " TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE   AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('CCT', 'SW') "
                + " AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpMobiFrDate.DateString + "' AND '" + dtpMobiToDate.DateString + "' "
                + " AND AL.ACCNT_RANK_ID = '140514000000000001' ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "MobiCash_CCT_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MobiCash CCT Transaction Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpMobiFrDate.DateString + "' To '" + dtpMobiToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date </td>";
            strHTML = strHTML + "<td valign='middle' >Serevice Code</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >MobiCash Agent Wallet</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "MobiCash_CCT_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }

        catch(Exception ex)
        {
            
        }
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    protected void btnCn_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT MIS.REQUEST_ID, AL.ACCNT_NO AGENT_NO, MIS.RECEPENT_PARTY CUSTOMER_NO, "
                    + " MIS.SERVICE_CODE, MIS.TRANSACTION_DATE, MIS.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, "
                    + " TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE AL.ACCNT_RANK_ID = '140410000000000004' "
                    + " AND SUBSTR(AL.ACCNT_NO, 1, 11) = MIS.REQUEST_PARTY AND MIS.SERVICE_CODE = 'CN' "
                    + " AND TRUNC(MIS.TRANSACTION_DATE) BETWEEN '" + dtpCnMobiFrDate.DateString + "' "
                    + " AND '" + dtpCnMobiToDate.DateString + "' ORDER BY MIS.TRANSACTION_DATE ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "MobiCash_CN_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MobiCash CN Transaction Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpCnMobiFrDate.DateString + "' To '" + dtpCnMobiToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date </td>";
            strHTML = strHTML + "<td valign='middle' >Serevice Code</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >MobiCash Agent Wallet</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUSTOMER_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "MobiCash_CN_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnDisCorpList_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, THA.SA_ACCNT_NO DSE_NO, ALC.ACCNT_NO CORP_NO "
                    + " FROM ACCOUNT_HIERARCHY AH, ACCOUNT_LIST ALC, ACCOUNT_LIST ALD, "
                    + " ACCOUNT_RANK AR, TEMP_HIERARCHY_LIST_ALL THA WHERE ALC.ACCNT_ID = AH.ACCNT_ID "
                    + " AND ALC.ACCNT_RANK_ID = '140917000000000004' AND AH.HIERARCHY_ACCNT_ID = ALD.ACCNT_ID "
                    + " AND ALD.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND ALD.ACCNT_NO =  THA.SA_ACCNT_NO "
                    + " ORDER BY THA.DEL_ACCNT_NO, THA.SA_ACCNT_NO";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Dis_CR_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor wise Corporate Agent List Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distirbutor</td>";
            strHTML = strHTML + "<td valign='middle' >DSE </td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Agent </td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_NO"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Dis_CR_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try 
        {
            string strSql = "";
            strSql = " SELECT DISTINCT ALA.ACCNT_NO PW_NO, AR.RANK_TITEL PW_RANK, CLA.CLINT_NAME PW_NAME, "
                + " MT.THANA_NAME PW_THANA, MD.DISTRICT_NAME PW_DISTRICT, CAB.CAS_ACCNT_BALANCE, "
                + " CASE WHEN CAB.CAS_ACCNT_BALANCE IS NOT NULL  THEN CAS_ACCNT_BALANCE WHEN CAB.CAS_ACCNT_BALANCE IS NULL THEN 0 END AS PW_BALANCE, "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpPwFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpPwToDate.DateString + "'))) NO_OF_RG, "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpPwFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpPwToDate.DateString + "')),'CN')NO_OF_CASIN, "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpPwFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpPwToDate.DateString + "')),'CN')CASIN_AMT,   "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpPwFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpPwToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT,  "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpPwFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpPwToDate.DateString + "')),'CCT,SW')CASOUT_AMT, "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_CNT (ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpPwFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpPwToDate.DateString + "'))) NO_OF_BP,  "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_AMT (ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpPwFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpPwToDate.DateString + "'))) BP_AMOUNT, "
                + " APSNG101.FUNC_PWAGT_FM_TRX_AMT (ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpPwFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpPwToDate.DateString + "'))) FM_AMT "
                + " FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, ACCOUNT_RANK AR, MANAGE_THANA MT, MANAGE_DISTRICT MD, "
                + " BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL "
                + " WHERE ALA.ACCNT_RANK_ID = '161215000000000004' AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID = AR.ACCNT_RANK_ID "
                + " AND CLA.THANA_ID = MT.THANA_ID AND MT.DISTRICT_ID = MD.DISTRICT_ID AND ALA.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID "
                + " ORDER BY ALA.ACCNT_NO ASC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Pw_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Paywell Agent Performance Report</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Paywell No </td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Rank </td>";
            strHTML = strHTML + "<td valign='middle' >Agent Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Agent District </td>";
            strHTML = strHTML + "<td valign='middle' >Agent Balance </td>";
            strHTML = strHTML + "<td valign='middle' >No of Registration</td>";
            strHTML = strHTML + "<td valign='middle' >No of Cashin </td>";
            strHTML = strHTML + "<td valign='middle' >Cashin Amount </td>";
            strHTML = strHTML + "<td valign='middle' >No of Cashout</td>";
            strHTML = strHTML + "<td valign='middle' >Cashout Amount </td>";
            strHTML = strHTML + "<td valign='middle' >No of Billpay</td>";
            strHTML = strHTML + "<td valign='middle' >Billpay Amount </td>";
            strHTML = strHTML + "<td valign='middle' >FM Amount </td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["PW_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["PW_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["PW_RANK"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["PW_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["PW_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["PW_BALANCE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_RG"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_CASIN"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CASIN_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_CASOUT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CASOUT_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_BP"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BP_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["FM_AMT"].ToString() + "</td>";
                    
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Dis_CR_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnServiceReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT * FROM BANK_SERVICE_FEE WHERE SERVICE_ID = '" + ddlService.SelectedValue + "' "
                    + " AND ACCNT_RANK_ID = '" + ddlAccountRank.SelectedValue + "' AND CHANNEL_TYPE_ID = '" + ddlChannelName.SelectedValue + "' "
                    + " AND CMP_BRANCH_ID = '" + ddlBranch.SelectedValue + "' AND BANK_CODE = '" + ddlBankList.SelectedValue + "' ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Manage_Service_Fee_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Manage Service Fee. Service Type : " + ddlService.SelectedItem.Text + " ; Account Rank : " + ddlAccountRank.SelectedItem.Text + " ; Channel Name : " + ddlChannelName.SelectedItem.Text + " </h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Fee Name</td>";
            strHTML = strHTML + "<td valign='middle' >Start Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Max Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Fee</td>";
            strHTML = strHTML + "<td valign='middle' >Minimum Fee</td>";
            strHTML = strHTML + "<td valign='middle' >Vat & Tax</td>";
            strHTML = strHTML + "<td valign='middle' >AIT (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Fees Paid By Bank (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Fees Paid By Initiator (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Fees Paid By Receipent(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Bank Commission (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Commission (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Pool Adjustment (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Vendor Commission (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Third Party Commission (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Channel Commission (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Common Operator Commission (%)</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Service Code</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Operator Commission</td>";
            strHTML = strHTML + "<td valign='middle' >Tax Paid By</td>";
            strHTML = strHTML + "<td valign='middle' >Fees Paid By</td>";
            strHTML = strHTML + "<td valign='middle' >Fees Include Vat/Tax</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BANK_SRV_FEE_TITLE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["START_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MAX_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BANK_SRV_FEE_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MIN_FEE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["VAT_TAX"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AIT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["FEES_PAID_BY_BANK"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["FEES_PAID_BY_CUSTOMER"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["FEES_PAID_BY_AGENT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BANK_COMM_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_COMM_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["POOL_ADJUSTMENT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["VENDOR_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CHANNEL_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_OPERATOR_COMI"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_SERVICE_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_OPARETOR_COMI_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TAX_PAID_BY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["FEES_PAID_BY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["FEE_INCLUDE_VAT_TAX"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }
            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";
            SaveAuditInfo("Preview", "Manage_Service_Fee_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnDisAmt_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;

            string strGetDistributorInfo = objServiceHandler.GetDistributorInfo(txtDisCommWallet.Text.Trim());

            string strSql = "";

            strSql = " SELECT TO_CHAR(TM.TRANSACTION_DATE) TRANSACTION_DATE, NVL(SUM(DECODE(TM.SERVICE_CODE, 'CN', TM.TRANSACTION_AMOUNT)), 0) CN,"
                    + " NVL(SUM(DECODE(TM.SERVICE_CODE, 'CCT', TM.TRANSACTION_AMOUNT)), 0) CCT, NVL(SUM(DECODE(TM.SERVICE_CODE, 'SW',"
                    + " TM.TRANSACTION_AMOUNT)), 0) SW, PKG_MIS_REPORTS.FUNC_DIS_UBP_AMT('" + txtDisCommWallet.Text.Trim() + "', TM.TRANSACTION_DATE) UBP,"
                    + " PKG_MIS_REPORTS.FUNC_DIS_CORP_COMM_AMT('" + txtDisCommWallet.Text.Trim() + "', TM.TRANSACTION_DATE) FM FROM TEMP_HIERARCHY_LIST_ALL THA,"
                    + " ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A'"
                    + " AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND (THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' OR THA.A_ACCNT_NO = TM.RECEPENT_PARTY)"
                    + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY TM.TRANSACTION_DATE"
                    + " ORDER BY TM.TRANSACTION_DATE ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Dis_Tran_Amt_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor(Individual) Transaction Amount Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor : '" + strGetDistributorInfo + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisCommFDate.DateString + "' To '" + dtpDisCommToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Date</td>";
            strHTML = strHTML + "<td valign='middle' >Cash In</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Out</td>";
            strHTML = strHTML + "<td valign='middle' >Salary Withdrawal</td>";
            strHTML = strHTML + "<td valign='middle' >Utility Bill Payment</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Transaction</td>";
            strHTML = strHTML + "</tr>";

            //if (dtsAccount != null)
            //{
                
            //}

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CN"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SW"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UBP"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["FM"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Dis_Comm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnAllDisAmt_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;

            string strSql = "";

            //strSql = " SELECT TM.TRANSACTION_DATE, ALD.ACCNT_NO, NVL(SUM(DECODE(TM.SERVICE_CODE, 'CN',"
            //        + " TM.TRANSACTION_AMOUNT)), 0) CN, NVL(SUM(DECODE(TM.SERVICE_CODE, 'CCT',"
            //        + " TM.TRANSACTION_AMOUNT)), 0) CCT, NVL(SUM(DECODE(TM.SERVICE_CODE, 'SW',"
            //        + " TM.TRANSACTION_AMOUNT)), 0) SW, PKG_MIS_REPORTS.FUNC_DIS_UBP_AMT(ALD.ACCNT_NO,"
            //        + " TM.TRANSACTION_DATE) UBP, PKG_MIS_REPORTS.FUNC_DIS_CORP_COMM_AMT(ALD.ACCNT_NO,"
            //        + " TM.TRANSACTION_DATE) FM FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD,"
            //        + " TEMP_MIS_TRANSACTIONS_REPORT TM WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND"
            //        + " ALD.ACCNT_STATE = 'A' AND (THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' OR"
            //        + " THA.A_ACCNT_NO = TM.RECEPENT_PARTY) AND TRUNC(TM.TRANSACTION_DATE)"
            //        + " BETWEEN '" + dtpAllDisAmtFDate.DateString + "' AND '" + dtpAllDisAmtTDate.DateString + "'"
            //        + " GROUP BY TM.TRANSACTION_DATE, ALD.ACCNT_NO ORDER BY TM.TRANSACTION_DATE ";

            //strSql = " SELECT TO_CHAR(TM.TRANSACTION_DATE) TRANSACTION_DATE, ALD.ACCNT_NO DIS_ACCNT_NO, CL.CLINT_NAME DIS_NAME,"
            //        + " AL.ACCNT_NO TO_ACCNT_NO, CLT.CLINT_NAME TO_NAME, NVL(SUM(DECODE(TM.SERVICE_CODE, 'CN', TM.TRANSACTION_AMOUNT)), 0) CN,"
            //        + " NVL(SUM(DECODE(TM.SERVICE_CODE, 'CCT', TM.TRANSACTION_AMOUNT)), 0) CCT, NVL(SUM(DECODE(TM.SERVICE_CODE, 'SW',"
            //        + " TM.TRANSACTION_AMOUNT)), 0) SW, PKG_MIS_REPORTS.FUNC_DIS_UBP_AMT(ALD.ACCNT_NO, TM.TRANSACTION_DATE) UBP,"
            //        + " PKG_MIS_REPORTS.FUNC_DIS_CORP_COMM_AMT(ALD.ACCNT_NO, TM.TRANSACTION_DATE) FM FROM TEMP_HIERARCHY_LIST_ALL THA,"
            //        + " ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM, MANAGE_TERRITORY_HIERARCHY MTH, ACCOUNT_LIST AL, CLIENT_LIST CL,"
            //        + " CLIENT_LIST CLT WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND (THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1'"
            //        + " OR THA.A_ACCNT_NO = TM.RECEPENT_PARTY) AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID AND MTH.HIERARCHY_ACCNT_ID = AL.ACCNT_ID"
            //        + " AND ALD.CLINT_ID = CL.CLINT_ID AND AL.CLINT_ID = CLT.CLINT_ID AND TRUNC(TM.TRANSACTION_DATE)"
            //        + " BETWEEN '" + dtpAllDisAmtFDate.DateString + "' AND '" + dtpAllDisAmtTDate.DateString + "'"
            //        + " GROUP BY TM.TRANSACTION_DATE, ALD.ACCNT_NO, CL.CLINT_NAME, AL.ACCNT_NO, CLT.CLINT_NAME ORDER BY TM.TRANSACTION_DATE ";

            //strSql = " SELECT TO_CHAR(T1.TRANSACTION_DATE) TRANSACTION_DATE, T1.DIS_ACCNT_NO, T1.CLINT_NAME DIS_NAME, T1.TO_ACCNT_NO, AL.ACCNT_NO, CL.CLINT_NAME TO_NAME,"
            //        + " T1.CN, T1.CCT_SW, T1.UBP, T1.FM FROM (SELECT TM.TRANSACTION_DATE, ALD.ACCNT_NO DIS_ACCNT_NO, CL.CLINT_NAME,"
            //        + " NVL(MTH.HIERARCHY_ACCNT_ID, 'NOT ASSIGN') TO_ACCNT_NO, NVL(SUM(DECODE(TM.SERVICE_CODE, 'CN', TM.TRANSACTION_AMOUNT)), 0) CN,"
            //        + " NVL(SUM(DECODE(TM.SERVICE_CODE, 'CCT', TM.TRANSACTION_AMOUNT)), 0) + NVL(SUM(DECODE(TM.SERVICE_CODE, 'SW',"
            //        + " TM.TRANSACTION_AMOUNT)), 0) CCT_SW, PKG_MIS_REPORTS.FUNC_DIS_UBP_AMT(ALD.ACCNT_NO, TM.TRANSACTION_DATE) UBP,"
            //        + " PKG_MIS_REPORTS.FUNC_DIS_CORP_COMM_AMT(ALD.ACCNT_NO, TM.TRANSACTION_DATE) FM FROM TEMP_HIERARCHY_LIST_ALL THA,"
            //        + " ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM, MANAGE_TERRITORY_HIERARCHY MTH, CLIENT_LIST CL"
            //        + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND (THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' OR"
            //        + " THA.A_ACCNT_NO = TM.RECEPENT_PARTY) AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND ALD.CLINT_ID = CL.CLINT_ID"
            //        + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpAllDisAmtFDate.DateString + "' AND '" + dtpAllDisAmtTDate.DateString + "'"
            //        + " GROUP BY TM.TRANSACTION_DATE, ALD.ACCNT_NO, CL.CLINT_NAME, MTH.HIERARCHY_ACCNT_ID ORDER BY"
            //        + " TM.TRANSACTION_DATE) T1, ACCOUNT_LIST AL, CLIENT_LIST CL WHERE T1.TO_ACCNT_NO = AL.ACCNT_ID(+) AND AL.CLINT_ID = CL.CLINT_ID(+)";

            //strSql = " SELECT TO_CHAR(T1.TRANSACTION_DATE) TRANSACTION_DATE, T1.DIS_ACCNT_NO, T1.CLINT_NAME DIS_NAME, T1.TO_ACCNT_NO, AL.ACCNT_NO,"
            //        + " CL.CLINT_NAME TO_NAME, T1.CN, T1.CCT_SW, T1.UBP  FROM (SELECT TM.TRANSACTION_DATE, ALD.ACCNT_NO DIS_ACCNT_NO, CL.CLINT_NAME,"
            //        + " NVL(MTH.HIERARCHY_ACCNT_ID, 'NOT ASSIGN') TO_ACCNT_NO, NVL(SUM(DECODE(TM.SERVICE_CODE, 'CN', TM.TRANSACTION_AMOUNT)), 0) CN,"
            //        + " NVL(SUM(DECODE(TM.SERVICE_CODE, 'CCT', TM.TRANSACTION_AMOUNT)), 0) + NVL(SUM(DECODE(TM.SERVICE_CODE, 'SW', TM.TRANSACTION_AMOUNT)), 0) CCT_SW,"
            //        + " PKG_MIS_REPORTS.FUNC_DIS_UBP_AMT(ALD.ACCNT_NO, TM.TRANSACTION_DATE) UBP FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD,"
            //        + " TEMP_MIS_TRANSACTIONS_REPORT TM, MANAGE_TERRITORY_HIERARCHY MTH, CLIENT_LIST CL WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO"
            //        + " AND ALD.ACCNT_STATE = 'A' AND (THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' OR THA.A_ACCNT_NO = TM.RECEPENT_PARTY)"
            //        + " AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND ALD.CLINT_ID = CL.CLINT_ID"
            //        + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpAllDisAmtFDate.DateString + "' AND '" + dtpAllDisAmtTDate.DateString + "'"
            //        + " GROUP BY TM.TRANSACTION_DATE, ALD.ACCNT_NO, CL.CLINT_NAME, MTH.HIERARCHY_ACCNT_ID ORDER BY TM.TRANSACTION_DATE) T1, ACCOUNT_LIST AL,"
            //        + " CLIENT_LIST CL WHERE T1.TO_ACCNT_NO = AL.ACCNT_ID(+) AND AL.CLINT_ID = CL.CLINT_ID(+) ";

            strSql = " SELECT TO_CHAR(T1.TRANSACTION_DATE) TRANSACTION_DATE, T1.DIS_ACCNT_NO, T1.CLINT_NAME DIS_NAME, T1.TO_ACCNT_NO, AL.ACCNT_NO, CL.CLINT_NAME TO_NAME, T1.CN, T1.CCT_SW  FROM (SELECT TM.TRANSACTION_DATE, ALD.ACCNT_NO DIS_ACCNT_NO, CL.CLINT_NAME, NVL(MTH.HIERARCHY_ACCNT_ID, 'NOT ASSIGN') TO_ACCNT_NO, NVL(SUM(DECODE(TM.SERVICE_CODE, 'CN', TM.TRANSACTION_AMOUNT)), 0) CN, NVL(SUM(DECODE(TM.SERVICE_CODE, 'CCT', TM.TRANSACTION_AMOUNT)), 0) + NVL(SUM(DECODE(TM.SERVICE_CODE, 'SW', TM.TRANSACTION_AMOUNT)), 0) CCT_SW FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM, MANAGE_TERRITORY_HIERARCHY MTH, CLIENT_LIST CL WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND (THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' OR THA.A_ACCNT_NO = TM.RECEPENT_PARTY) AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND ALD.CLINT_ID = CL.CLINT_ID AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpAllDisAmtFDate.DateString + "' AND '" + dtpAllDisAmtTDate.DateString + "' GROUP BY TM.TRANSACTION_DATE, ALD.ACCNT_NO, CL.CLINT_NAME, MTH.HIERARCHY_ACCNT_ID ORDER BY TM.TRANSACTION_DATE) T1, ACCOUNT_LIST AL, CLIENT_LIST CL WHERE T1.TO_ACCNT_NO = AL.ACCNT_ID(+) AND AL.CLINT_ID = CL.CLINT_ID(+) ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "All_Dis_Tran_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>All Distributor Transaction Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpAllDisAmtFDate.DateString + "' To '" + dtpAllDisAmtTDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO No.</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Cash In</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Out + SW</td>";
            //strHTML = strHTML + "<td valign='middle' >Utility Bill Payment</td>";
            //strHTML = strHTML + "<td valign='middle' >Corporate Transaction</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CN"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_SW"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["UBP"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > '" + prow["FM"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Dis_Comm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnADBPReport_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;

            string strSql = "";

            strSql = " SELECT Q1.OWNER_CODE, Q1.TRANSA_DATE, Q1.TO_ACCNT_NO, Q1.TO_NAME, Q1.DIS_ACCNT_TO, Q1.DIS_NAME, Q1.AGENT_DIST,"
                    + " COUNT(Q1.SOURCE_ACC_NO) COUNT_AGENT, SUM(Q1.COUNT_BILL) COUNT_BILL, SUM(Q1.BILL_AMOUNT) BILL_AMOUNT FROM (SELECT UT.OWNER_CODE,"
                    + " TO_CHAR(UT.TRANSA_DATE) TRANSA_DATE, ALT.ACCNT_NO TO_ACCNT_NO, CLT.CLINT_NAME TO_NAME, THLA.DEL_ACCNT_NO DIS_ACCNT_TO,"
                    + " CLD.CLINT_NAME DIS_NAME, MDA.DISTRICT_NAME AGENT_DIST, UT.SOURCE_ACC_NO, COUNT(UT.REQUEST_ID) COUNT_BILL, SUM(UT.TOTAL_BILL_AMOUNT)"
                    + " BILL_AMOUNT FROM TEMP_HIERARCHY_LIST_ALL THLA, MANAGE_TERRITORY_HIERARCHY MTH, SERVICE_REQUEST_OLD SR, UTILITY_TRANSACTION_OLD UT,"
                    + " ACCOUNT_LIST ALA, ACCOUNT_LIST ALT, ACCOUNT_LIST ALD, CLIENT_LIST CLT, CLIENT_LIST CLD, CLIENT_LIST CLA, MANAGE_THANA MTA,"
                    + " MANAGE_DISTRICT MDA WHERE THLA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID"
                    + " AND THLA.DEL_ACCNT_ID = ALD.ACCNT_ID AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND ALD.CLINT_ID = CLD.CLINT_ID AND THLA.A_ACCNT_ID = ALA.ACCNT_ID"
                    + " AND ALA.CLINT_ID = CLA.CLINT_ID AND CLA.THANA_ID = MTA.THANA_ID AND MTA.DISTRICT_ID = MDA.DISTRICT_ID"
                    + " AND '+88' || THLA.A_ACCNT_NO = SR.REQUEST_PARTY || 1 AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpADBPReportFr.DateString + "'"
                    + " AND '" + dtpADBPReportTo.DateString + "' AND UT.RESPONSE_STATUS_BP = '000' GROUP BY UT.OWNER_CODE, TO_CHAR(UT.TRANSA_DATE), ALT.ACCNT_NO,"
                    + " CLT.CLINT_NAME, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, MDA.DISTRICT_NAME, UT.SOURCE_ACC_NO) Q1 GROUP BY Q1.OWNER_CODE, Q1.TRANSA_DATE,"
                    + " Q1.TO_ACCNT_NO, Q1.TO_NAME, Q1.DIS_ACCNT_TO, Q1.DIS_NAME, Q1.AGENT_DIST ORDER BY Q1.OWNER_CODE, Q1.TRANSA_DATE ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "MBL_Dis_Bill_Pay_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MBL Distributor Bill Pay Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpADBPReportFr.DateString + "' To '" + dtpADBPReportTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Type</td>";
            strHTML = strHTML + "<td valign='middle' >Date</td>";
            strHTML = strHTML + "<td valign='middle' >TO No.</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent District</td>";
            strHTML = strHTML + "<td valign='middle' >Count Agent</td>";
            strHTML = strHTML + "<td valign='middle' >Count Bill</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSA_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ACCNT_TO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_DIST"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["COUNT_AGENT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["COUNT_BILL"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Dis_Comm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnServiceWiseDisComm_Click(object sender, EventArgs e)
    {
        string date = "01-" + ddlMonth.SelectedValue + "-" + ddlYear.SelectedValue;
        string accountId = "";

        if (ddlServiceCode.SelectedValue == "UBP")
        {
            accountId = "00000000000019";
        }
        else if (ddlServiceCode.SelectedValue == "CN")
        {
            accountId = "00000000000032";
        }
        else if (ddlServiceCode.SelectedValue == "CCT")
        {
            accountId = "00000000000033";
        }

        try
        {
            int SerialNo = 1;

            string strSql = "";

            //strSql = " SELECT THLA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, SUM(TM.TRANSACTION_AMOUNT) TOTAL_AMOUNT, SUM(TM.THIRDPARTY_COM_AMOUNT) TOTAL_COMMISSION FROM ACCOUNT_LIST AL, TEMP_HIERARCHY_LIST_ALL THLA, TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD, CLIENT_LIST CLD WHERE AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') AND AL.ACCNT_NO = THLA.A_ACCNT_NO AND TM.REQUEST_PARTY || '1' = AL.ACCNT_NO AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND TM.SERVICE_CODE IN ('" + serviceCode + "') AND TM.TRANSACTION_DATE BETWEEN TRUNC(TO_DATE('" + date + "') , 'MONTH') AND LAST_DAY(TRUNC(TO_DATE('" + date + "'), 'MONTH')) GROUP BY THLA.DEL_ACCNT_NO, CLD.CLINT_NAME ";

            strSql = " SELECT THLA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, SUM(TM.TRANSACTION_AMOUNT) TOTAL_AMOUNT, SUM(CAT.CAS_TRAN_AMT) TOTAL_COMMISSION FROM ACCOUNT_LIST AL, TEMP_HIERARCHY_LIST_ALL THLA, TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD, CLIENT_LIST CLD, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL WHERE AL.ACCNT_RANK_ID IN ('120519000000000005', '180128000000000008','161215000000000004','170422000000000004') AND AL.ACCNT_NO = THLA.A_ACCNT_NO AND (TM.REQUEST_PARTY || '1' = AL.ACCNT_NO OR TM.RECEPENT_PARTY = AL.ACCNT_NO) AND CAT.REQUEST_ID = TM.REQUEST_ID AND CAT.CAS_ACC_ID = CAL.CAS_ACC_ID AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CAL.CAS_ACC_NO IN ('" + accountId + "') AND TM.TRANSACTION_DATE BETWEEN TRUNC(TO_DATE('" + date + "'), 'MONTH') AND LAST_DAY(TRUNC(TO_DATE('" + date + "'), 'MONTH')) GROUP BY THLA.DEL_ACCNT_NO, CLD.CLINT_NAME ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "MBL_Dis_Bill_Pay_Comm_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MBL Distributor Service wise Commission Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Month: '" + ddlMonth.SelectedItem.Text + "', '" + ddlYear.SelectedItem.Text + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Wallet Id</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Service_Wise_Dis_Comm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnRankWiseBalance_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;

            string strSql = "";

            strSql = " SELECT TO_CHAR(TRAN_DATE) TRAN_DATE, SUM(MBL_BRANCH) MBL_BRANCH, SUM(MBL_DISTRIBUTOR) MBL_DISTRIBUTOR, SUM(MBL_DSE) MBL_DSE, SUM(MBL_AGENT) MBL_AGENT, SUM(MBL_CUSTOMER) MBL_CUSTOMER, GET_FIS_BALANCE_BY_DATE('017334477261',TRAN_DATE) GP_ACCOUNT FROM (SELECT DISTINCT TRAN_DATE, ACC_ID, DECODE(ACC_ID, '205050500',BDMIT_ERP_101.FUNC_GET_GL_ACC_BAL_BY_DATE(ACC_ID, TRAN_DATE)) MBL_BRANCH, DECODE(ACC_ID, '205051000',BDMIT_ERP_101.FUNC_GET_GL_ACC_BAL_BY_DATE(ACC_ID, TRAN_DATE)) MBL_DISTRIBUTOR, DECODE(ACC_ID, '205052000',BDMIT_ERP_101.FUNC_GET_GL_ACC_BAL_BY_DATE(ACC_ID, TRAN_DATE)) MBL_DSE, DECODE(ACC_ID, '205052500',BDMIT_ERP_101.FUNC_GET_GL_ACC_BAL_BY_DATE(ACC_ID, TRAN_DATE)) MBL_AGENT, DECODE(ACC_ID, '205053000',BDMIT_ERP_101.FUNC_GET_GL_ACC_BAL_BY_DATE(ACC_ID, TRAN_DATE)) MBL_CUSTOMER FROM BDMIT_ERP_101.GL_DAILY_TRAN WHERE TRUNC(TRAN_DATE) BETWEEN '" + dtRankWiseBalanceFrom.DateString + "' AND '" + dtRankWiseBalanceTo.DateString + "' AND ACC_ID IN ('205050500','205053000', '205051000', '205052000', '205052500') ORDER BY TRAN_DATE, ACC_ID) GROUP BY TRAN_DATE ORDER BY TRAN_DATE ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "MBL_Rank_Wise_Balance_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MBL Rank Wise Balance Detail Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtRankWiseBalanceFrom.DateString + "' To '" + dtRankWiseBalanceTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Branch</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor</td>";
            strHTML = strHTML + "<td valign='middle' >DSE</td>";
            strHTML = strHTML + "<td valign='middle' >Agent</td>";
            strHTML = strHTML + "<td valign='middle' >Customer</td>";
            strHTML = strHTML + "<td valign='middle' >GP Account</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MBL_BRANCH"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MBL_DISTRIBUTOR"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MBL_DSE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MBL_AGENT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MBL_CUSTOMER"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["GP_ACCOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + (Convert.ToInt32(prow["MBL_DISTRIBUTOR"]) + Convert.ToInt32(prow["MBL_DSE"]) + Convert.ToInt32(prow["MBL_AGENT"]) + Convert.ToInt32(prow["MBL_CUSTOMER"]) + Convert.ToInt32(prow["GP_ACCOUNT"])) + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "MBL_Rank_Wise_Balance_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnCBSTransactionDetail_Click(object sender, EventArgs e)
    {
        try
		{
			int appsInstall = 0;
			int cbsActivation = 0;
			int balanceEnquiry = 0;
			int totalDeposit = 0;
			double depositAmount = 0.0;
			int totalWithdrawal = 0;
			double withdrawalAmount = 0.0;

			int SerialNo = 1;
			string strSql = "";

			strSql = " SELECT SERVICE_PKG_NAME, SUM(ACTIVATION) APPS_INSTALLATION, SUM(CBS_ACTIVE) CBS_ACTIVE, SUM(BDBI) BALANCE_ENQUIRY, SUM(BDLA) TOTAL_BANK_DEPOSIT, SUM(BDLA_AMT) BANK_DEPOSIT_AMOUNT, SUM(WD) TOTAL_WITHDRAWAL, SUM(WD_AMT) TOTAL_WITHDRAWAL_AMOUNT FROM (SELECT SERVICE_PKG_NAME, COUNT(DL.ACCNT_ID) ACTIVATION, DECODE(CDT.TRAN_TYPE,'BDBI',COUNT(CDT.CAS_ACC_ID)) BDBI, DECODE(CDT.RESPONSE_MESSAGE,'Success',DECODE(CDT.TRAN_TYPE,'BDLA',COUNT(CDT.CAS_ACC_ID))) BDLA, DECODE(CDT.RESPONSE_MESSAGE,'Success',DECODE(CDT.TRAN_TYPE,'BDLA',SUM(CDT.CAS_TRAN_AMT))) BDLA_AMT, DECODE(CDT.RESPONSE_MESSAGE,'Success',DECODE(CDT.TRAN_TYPE,'WD',COUNT(CDT.CAS_ACC_ID))) WD, DECODE(CDT.RESPONSE_MESSAGE,'Success',DECODE(CDT.TRAN_TYPE,'WD',SUM(CDT.CAS_TRAN_AMT))) WD_AMT,0 CBS_ACTIVE  FROM ACCOUNT_LIST AL, DEVICE_LIST DL, SERVICE_PACKAGE SP, CLIENT_BANK_ACCOUNT CBA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT WHERE AL.ACCNT_ID = DL.ACCNT_ID AND AL.SERVICE_PKG_ID IN ('1205190003','1209270001') AND AL.SERVICE_PKG_ID = SP.SERVICE_PKG_ID AND AL.ACCNT_ID = CBA.ACCNT_ID AND CBA.CLINT_BANK_ACC_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CDT.CAS_ACC_ID AND TRUNC(CDT.CAS_TRAN_DATE) BETWEEN '" + dtCBSTranFrom.DateString + "' AND '" + dtCBSTranTo.DateString + "' GROUP BY SP.SERVICE_PKG_NAME, CDT.TRAN_TYPE,CDT.RESPONSE_MESSAGE UNION ALL SELECT SERVICE_PKG_NAME, 0 ACTIVATION,0 BDBI,0 BDLA,0 BDLA_AMT,0 WD,0 WD_AMT, COUNT(AL.ACCNT_ID) CBS_ACTIVE FROM ACCOUNT_LIST AL, SERVICE_PACKAGE SP, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_DPS_MSS_LIST ML WHERE AL.SERVICE_PKG_ID IN ('1205190003','1209270001') AND AL.SERVICE_PKG_ID = SP.SERVICE_PKG_ID AND AL.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = ML.CAS_ACC_ID AND ML.CAS_CBS_ACNT_STATUS = 'A' AND TRUNC(ML.ACTIVATION_DATE) BETWEEN '" + dtCBSTranFrom.DateString + "' AND '" + dtCBSTranTo.DateString + "' GROUP BY SERVICE_PKG_NAME) GROUP BY SERVICE_PKG_NAME ";

			string fileName = "", strHTML = "";
			DataSet dtsAccount = new DataSet();
			fileName = "CBS_Transaction_Rpt";

			dtsAccount = objServiceHandler.ExecuteQuery(strSql);

			strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
			strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
			strHTML = strHTML + "</table>";
			strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
			strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
			strHTML = strHTML + "</table>";
			strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
			strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>CBS Transaction Detail Report</h2></td></tr>";
			strHTML = strHTML + "</table>";
			strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
			strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtCBSTranFrom.DateString + "' To '" + dtCBSTranTo.DateString + "'</h2></td></tr>";
			strHTML = strHTML + "</table>";
			strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
			strHTML = strHTML + "<tr>";

			strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
			strHTML = strHTML + "<td valign='middle' >Service Package</td>";
			strHTML = strHTML + "<td valign='middle' >Total Apps Install</td>";
			strHTML = strHTML + "<td valign='middle' >Total CBS Activation</td>";
			strHTML = strHTML + "<td valign='middle' >Total Balance Enquiry</td>";
			strHTML = strHTML + "<td valign='middle' >Total MYCash-CBS Deposit</td>";
			strHTML = strHTML + "<td valign='middle' >Total Amount of MYCash-CBS Deposit</td>";
			strHTML = strHTML + "<td valign='middle' >Total CBS-MYCash Deposit</td>";
			strHTML = strHTML + "<td valign='middle' >Total Amount of CBS-MYCash Deposit </td>";
			strHTML = strHTML + "</tr>";

			if (dtsAccount.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow prow in dtsAccount.Tables[0].Rows)
				{
					strHTML = strHTML + " <tr><td>" + SerialNo.ToString() + "</td>";
					strHTML = strHTML + " <td > " + prow["SERVICE_PKG_NAME"].ToString() + " </td>";
					strHTML = strHTML + " <td > " + prow["APPS_INSTALLATION"].ToString() + " </td>";
					strHTML = strHTML + " <td > " + prow["CBS_ACTIVE"].ToString() + " </td>";
					strHTML = strHTML + " <td > " + prow["BALANCE_ENQUIRY"].ToString() + " </td>";
					strHTML = strHTML + " <td > " + prow["TOTAL_BANK_DEPOSIT"].ToString() + " </td>";
					strHTML = strHTML + " <td > " + Convert.ToDouble(prow["BANK_DEPOSIT_AMOUNT"]).ToString("N2") + " </td>";
					strHTML = strHTML + " <td > " + prow["TOTAL_WITHDRAWAL"].ToString() + " </td>";
					strHTML = strHTML + " <td > " + Convert.ToDouble(prow["TOTAL_WITHDRAWAL_AMOUNT"]).ToString("N2") + " </td>";

					strHTML = strHTML + " </tr> ";
					SerialNo = SerialNo + 1;

					appsInstall = appsInstall + Convert.ToInt32(prow["APPS_INSTALLATION"]);
					cbsActivation = cbsActivation + Convert.ToInt32(prow["CBS_ACTIVE"]);
					balanceEnquiry = balanceEnquiry + Convert.ToInt32(prow["BALANCE_ENQUIRY"]);
					totalDeposit = totalDeposit + Convert.ToInt32(prow["TOTAL_BANK_DEPOSIT"]);
					depositAmount = depositAmount + Convert.ToDouble(prow["BANK_DEPOSIT_AMOUNT"]);
					totalWithdrawal = totalWithdrawal + Convert.ToInt32(prow["TOTAL_WITHDRAWAL"]);
					withdrawalAmount = withdrawalAmount + Convert.ToDouble(prow["TOTAL_WITHDRAWAL_AMOUNT"]);
				}
			}

			strHTML = strHTML + "<tr style='font-weight:bolder;font-size:18px'>";
			strHTML = strHTML + " <td > " + "" + " </td>";
			strHTML = strHTML + " <td > " + "Total" + " </td>";
			strHTML = strHTML + " <td > " + appsInstall.ToString() + " </td>";
			strHTML = strHTML + " <td > " + cbsActivation.ToString() + " </td>";
			strHTML = strHTML + " <td > " + balanceEnquiry.ToString() + " </td>";
			strHTML = strHTML + " <td > " + totalDeposit.ToString() + " </td>";
			strHTML = strHTML + " <td > " + depositAmount.ToString("N2") + " </td>";
			strHTML = strHTML + " <td > " + totalWithdrawal.ToString() + " </td>";
			strHTML = strHTML + " <td > " + withdrawalAmount.ToString("N2") + " </td>";

			strHTML = strHTML + " </tr>";
			strHTML = strHTML + " </table>";

			SaveAuditInfo("Preview", fileName);
			clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

			lblMsg.ForeColor = Color.White;
			lblMsg.Text = "Report Generated Successfully...";
		}
		catch (Exception exception)
		{
			exception.Message.ToString();
		}
	}
	
	protected void btnAppTranDetail_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;
            int totalTransaction = 0;
            double totalTransactionAmount = 0;
            double totalTransactionCommission = 0;

            string strSql = "";

            strSql = " SELECT SL.SERVICE_TITLE, COUNT(TM.REQUEST_ID) TOTAL_TRANSACTION, SUM(TM.TRANSACTION_AMOUNT) TRANSACTION_AMOUNT, SUM(TM.BANK_COMMISSION) BANK_COMMISSION, SUM(AGENT_OPT_COMMISSION) AGENT_OPT_COMMISSION FROM SERVICE_REQUEST SR, TEMP_MIS_TRANSACTIONS_REPORT TM, SERVICE_LIST SL, ACCOUNT_LIST AL WHERE SR.REQUEST_ID = TM.REQUEST_ID AND REQUEST_PARTY_TYPE = 'MCOM_GATEWAY' AND TM.SERVICE_CODE = SL.SERVICE_ACCESS_CODE AND TM.REQUEST_PARTY || '1' = AL.ACCNT_NO AND TM.SERVICE_CODE NOT IN ('UBPDP') AND TRUNC(REQUEST_TIME) BETWEEN '" + dtpAppTranFrom.DateString + "' AND '" + dtpAppTranTo.DateString + "' AND AL.ACCNT_RANK_ID NOT IN ('161215000000000004') GROUP BY SL.SERVICE_TITLE ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "QR_Transaction_Detail_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Apps Transaction Details</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpAppTranFrom.DateString + "' To '" + dtpAppTranTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Service Type</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["SERVICE_TITLE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_TRANSACTION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["BANK_COMMISSION"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    totalTransaction = totalTransaction + Convert.ToInt32(prow["TOTAL_TRANSACTION"]);
                    totalTransactionAmount = totalTransactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"]);
                    totalTransactionCommission = totalTransactionCommission + Convert.ToDouble(prow["BANK_COMMISSION"]);
                }
            }
			
			strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + totalTransaction.ToString() + " </td>";
            strHTML = strHTML + " <td > " + totalTransactionAmount.ToString() + " </td>";
            strHTML = strHTML + " <td > " + totalTransactionCommission.ToString() + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnOmSubMerchantSetupDetail_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;
 
            string strSql = "";

            strSql = " SELECT AL.ACCNT_NO, CL.CLINT_NAME, ML.MERCHANT_ID, CT.CHANNEL_TYPE, ML.START_AMOUNT, TO_CHAR(TO_DATE(SUBSTR(ML.MERCHANT_ID,1,6),'YY-MM-DD')) SETUP_DATE FROM MERCHANT_LIST ML, ACCOUNT_LIST AL, CLIENT_LIST CL, CHANNEL_TYPE CT WHERE ML.MERCHANT_ACCNT_ID = AL.ACCNT_ID AND AL.CLINT_ID = CL.CLINT_ID AND ML.CHANNEL_TYPE_ID = CT.CHANNEL_TYPE_ID AND AL.ACCNT_RANK_ID = '180128000000000003' AND TRUNC(TO_DATE(SUBSTR(ML.MERCHANT_ID,1,6),'YY-MM-DD')) BETWEEN '" + dtpOSSFromDate.DateString + "' AND '" + dtpOSSToDate.DateString + "' ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "OM_Sub_Merchant_Setup_Detail_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>OM Submerchant Setup Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpOSSFromDate.DateString + "' To '" + dtpOSSToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Merchant Name</td>";
            strHTML = strHTML + "<td valign='middle' >Setup Date</td>";
            strHTML = strHTML + "<td valign='middle' >Channel Type</td>";
            strHTML = strHTML + "<td valign='middle' >Start Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["SETUP_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["CHANNEL_TYPE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["START_AMOUNT"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }
            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btndtTranscom_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;

            string strSql = "";

            strSql = " SELECT DESTINATION_ACCNT_NO,SOURCE_ACCNT_NO,REQUEST_ID,RESPONSE_LOG,DECODE(IS_NOTIFIED,'Y','Yes','No') IS_NOTIFIED,TRANSACTION_DATE " +
                     " FROM TRANSCOM_NOTIFICATION WHERE TRANSACTION_DATE BETWEEN '" + dtTranscomFrom.DateString + "' AND '" + dtTranscomTo.DateString + "'";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Transcom_Transaction_Detail_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Apps Transaction Details</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtTranscomFrom.DateString + "' To '" + dtTranscomTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >DESTINATION ACCNT NO</td>";
            strHTML = strHTML + "<td valign='middle' >SOURCE ACCNT NO</td>";
            strHTML = strHTML + "<td valign='middle' >REQUEST ID</td>";
            strHTML = strHTML + "<td valign='middle' >RESPONSE LOG</td>";
            strHTML = strHTML + "<td valign='middle' >IS NOTIFIED</td>";
            strHTML = strHTML + "<td valign='middle' >TRANSACTION DATE</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DESTINATION_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["RESPONSE_LOG"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["IS_NOTIFIED"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TRANSACTION_DATE"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }
            
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btndtProvitaCorporate_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;
            double totalTransaction_Amount_Sum = 0;
            double totalTotal_Commission = 0;
            double totalVAT = 0;
            double totalAmount_After_VAT = 0;
            double totalAIT = 0;
            double totalAmount_After_AIT = 0;
            double totalDistributor_Commission = 0;
            double totalMBL_Commission = 0;


            string strSql = "";

            //strSql = " SELECT AL.ACCNT_NO MBL_DISTRIBUTOR_ACCOUNT_NO,CT.CLINT_NAME MBL_MYCASH_DISTRIBUTOR_NAME,CT.CLINT_ADDRESS1 MBL_MYCASH_DISTRIBUTOR_ADDRESS, MT.THANA_NAME MBL_DISTRIBUTOR_THANA, MD.DISTRICT_NAME MBL_DISTRIBUTOR_DISTRICT, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT_SUM, ROUND(SUM(TRANSACTION_AMOUNT)*0.002,4) TOTAL_COMMISSION,ROUND(SUM(TRANSACTION_AMOUNT)*0.002*15/115,4) VAT, ROUND(SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115,4) AMOUNT_AFTER_VAT, ROUND((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115)/10,4) AIT, ROUND((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115) - ((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115)/10),4) AMOUNT_AFTER_AIT, '60%' DISTRIBUTOR_COMMISION_RATE, ROUND(((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115) - ((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115)/10))*0.6,4) DISTRIBUTOR_COMMISSION, '40%' MBL_RATE, ROUND(((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115) - ((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115)/10))*0.4,4) MBL_COMMISSION FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL, CLIENT_LIST CT, MANAGE_THANA MT, MANAGE_DISTRICT MD WHERE TM.REQUEST_PARTY || '1' = AL.ACCNT_NO AND TM.RECEPENT_PARTY IN (SELECT ACCNT_NO FROM ACCOUNT_LIST WHERE ACCNT_RANK_ID = '190519000000000003')  AND AL.CLINT_ID = CT.CLINT_ID AND CT.THANA_ID = MT.THANA_ID AND MT.DISTRICT_ID = MD.DISTRICT_ID AND ACCNT_RANK_ID = '120519000000000004' AND TM.TRANSACTION_DATE BETWEEN '" + dtProvitaCorporateFrom.DateString + "' AND '" + dtProvitaCorporateTo.DateString + "' GROUP BY AL.ACCNT_NO, CT.CLINT_NAME, CT.CLINT_ADDRESS1, MT.THANA_NAME, MD.DISTRICT_NAME";
			
			strSql = " SELECT DISTINCT DIS.DEL_ACCNT_NO MBL_DISTRIBUTOR_ACCOUNT_NO, CLD.CLINT_NAME MBL_MYCASH_DISTRIBUTOR_NAME,CLD.CLINT_ADDRESS1 MBL_MYCASH_DISTRIBUTOR_ADDRESS, MTD.THANA_NAME MBL_DISTRIBUTOR_THANA, MDD.DISTRICT_NAME MBL_DISTRIBUTOR_DISTRICT, DSE.* FROM (SELECT DISTINCT AL.ACCNT_NO MBL_DSE_ACCOUNT_NO,SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT_SUM, ROUND(SUM(TRANSACTION_AMOUNT)*0.002,4) TOTAL_COMMISSION,ROUND(SUM(TRANSACTION_AMOUNT)*0.002*15/115,4) VAT,ROUND(SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115,4) AMOUNT_AFTER_VAT, ROUND((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115)/10,4) AIT, ROUND((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115) - ((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115)/10),4) AMOUNT_AFTER_AIT, '60%' DISTRIBUTOR_COMMISION_RATE, ROUND(((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115) - ((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115)/10))*0.6,4) DISTRIBUTOR_COMMISSION, '40%' MBL_RATE, ROUND(((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115) - ((SUM(TRANSACTION_AMOUNT)*0.002 - SUM(TRANSACTION_AMOUNT)*0.002*15/115)/10))*0.4,4) MBL_COMMISSION FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL WHERE TM.REQUEST_PARTY || '1' = AL.ACCNT_NO AND TM.RECEPENT_PARTY IN (SELECT ACCNT_NO FROM ACCOUNT_LIST WHERE ACCNT_RANK_ID = '190519000000000003') AND AL.ACCNT_RANK_ID = '120519000000000004' AND TM.TRANSACTION_DATE BETWEEN '" + dtProvitaCorporateFrom.DateString + "' AND '" + dtProvitaCorporateTo.DateString + "' GROUP BY AL.ACCNT_NO) DSE, (SELECT DEL_ACCNT_NO,SA_ACCNT_NO FROM TEMP_HIERARCHY_LIST_ALL) DIS, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE DSE.MBL_DSE_ACCOUNT_NO = DIS.SA_ACCNT_NO(+) AND DIS.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+)";
			
            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Provita_Corporate_Collection_Detail_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Apps Transaction Details</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtProvitaCorporateFrom.DateString + "' To '" + dtProvitaCorporateTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Distributor Account No</td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Thana</td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount Sum</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission 0.20%</td>";
            strHTML = strHTML + "<td valign='middle' >VAT 15%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After VAT</td>";
            strHTML = strHTML + "<td valign='middle' >AIT 10%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After AIT</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commision Rate</td>";

            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Rate</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["MBL_DISTRIBUTOR_ACCOUNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["MBL_MYCASH_DISTRIBUTOR_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["MBL_MYCASH_DISTRIBUTOR_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["MBL_DISTRIBUTOR_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["MBL_DISTRIBUTOR_DISTRICT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TRANSACTION_AMOUNT_SUM"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["VAT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["AMOUNT_AFTER_VAT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["AIT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["AMOUNT_AFTER_AIT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DISTRIBUTOR_COMMISION_RATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DISTRIBUTOR_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["MBL_RATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["MBL_COMMISSION"].ToString() + " </td>";


                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    totalTransaction_Amount_Sum = totalTransaction_Amount_Sum + Convert.ToInt32(prow["TRANSACTION_AMOUNT_SUM"]);
                    totalTotal_Commission = totalTotal_Commission + Convert.ToDouble(prow["TOTAL_COMMISSION"]);
                    totalVAT = totalVAT + Convert.ToDouble(prow["VAT"]);
                    totalAmount_After_VAT = totalAmount_After_VAT + Convert.ToDouble(prow["AMOUNT_AFTER_VAT"]);
                    totalAIT = totalAIT + Convert.ToDouble(prow["AIT"]);
                    totalAmount_After_AIT = totalAmount_After_AIT + Convert.ToDouble(prow["AMOUNT_AFTER_AIT"]);
                    totalDistributor_Commission = totalDistributor_Commission + Convert.ToDouble(prow["DISTRIBUTOR_COMMISSION"]);
                    totalMBL_Commission = totalMBL_Commission + Convert.ToDouble(prow["MBL_COMMISSION"]);
                }
            }
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + totalTransaction_Amount_Sum.ToString() + " </td>";
            strHTML = strHTML + " <td > " + totalTotal_Commission.ToString() + " </td>";
            strHTML = strHTML + " <td > " + totalVAT.ToString() + " </td>";
            strHTML = strHTML + " <td > " + totalAmount_After_VAT.ToString() + " </td>";
            strHTML = strHTML + " <td > " + totalAIT.ToString() + " </td>";
            strHTML = strHTML + " <td > " + totalAmount_After_AIT.ToString() + " </td>";
            strHTML = strHTML + " <td > " + " </td>";
            strHTML = strHTML + " <td > " + totalDistributor_Commission + " </td>";
            strHTML = strHTML + " <td > " + " </td>";
            strHTML = strHTML + " <td > " + totalMBL_Commission.ToString() + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnAgentFundManagementReport_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;
            string agentWallet = txtAgentNumber.Text.Trim();

            if (agentWallet.Equals(""))
            {
                lblMsg.Text = "Please input agent number";
                return;
            }

            string strSql = "";

            strSql = " SELECT ACCNT_NO,CLINT_NAME, TO_CHAR(CAS_TRAN_DATE) CAS_TRAN_DATE, SUM(DEBIT) DEBIT, SUM(CREDIT) CREDIT FROM (SELECT AL.ACCNT_NO, CL.CLINT_NAME, CAT.CAS_TRAN_DATE, NVL(DECODE(CAT.CAS_TRAN_TYPE,'D',SUM(CAT.CAS_TRAN_AMT)),0) DEBIT, NVL(DECODE(CAT.CAS_TRAN_TYPE,'C',SUM(CAT.CAS_TRAN_AMT)),0) CREDIT     FROM ACCOUNT_LIST AL, CLIENT_LIST CL, CLIENT_BANK_ACCOUNT CBA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_ID = CBA.ACCNT_ID AND CBA.CLINT_BANK_ACC_LOGIN = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND AL.ACCNT_RANK_ID = '120519000000000005' AND CAT.ACCESS_CODE = 'FM' AND TRUNC(CAS_TRAN_DATE) BETWEEN '" + dtpAFMDetailFrom.DateString + "' AND '" + dtpAFMDetailTo.DateString + "' AND AL.ACCNT_NO = '" + agentWallet + "' GROUP BY AL.ACCNT_NO, CL.CLINT_NAME, CAT.CAS_TRAN_DATE, CAT.CAS_TRAN_TYPE ORDER BY CAT.CAS_TRAN_DATE) GROUP BY ACCNT_NO,CLINT_NAME, TO_CHAR(CAS_TRAN_DATE) ORDER BY TO_CHAR(CAS_TRAN_DATE)";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Agent_FM_Transaction_Detail_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Fund Management Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpAFMDetailFrom.DateString + "' To '" + dtpAFMDetailTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Debit</td>";
            strHTML = strHTML + "<td valign='middle' >Credit</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DEBIT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["CREDIT"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnAgentTransactionSummary_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;
            string agentWallet = txtATSAgentNumber.Text.Trim();

            string strSqlAgent = "SELECT CL.CLINT_NAME FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_NO = '" + agentWallet + "'";
            string strAgentName = objServiceHandler.ReturnString(strSqlAgent);

            if (agentWallet.Equals(""))
            {
                lblMsg.Text = "Please input agent number";
                return;
            }
            string strSqlAA = "SELECT COUNT(*) FROM ACCOUNT_LIST WHERE ACCNT_NO = '" + agentWallet + "' AND ACCNT_RANK_ID = '120519000000000005'";
            string strAgentAccount = objServiceHandler.ReturnString(strSqlAA);

            if (strAgentAccount.Equals("0"))
            {
                lblMsg.Text = "Please input correct agent number";
                return;
            }

            string strSql = "SELECT SL.SERVICE_TITLE, CAT.ACCESS_CODE,DECODE(CAT.CAS_TRAN_TYPE,'C','CREDIT','DEBIT') CAS_TRAN_TYPE, DECODE(CAT.CAS_TRAN_STATUS,'A','ACTIVE','REVERSE') CAS_TRAN_STATUS, DECODE(CAT.CAS_TRAN_PURPOSE_CODE,'AGNCOM','AGENT_COMMISSION','TOTAMT','TOTAL_AMOUNT','FRTAMT','FROM_AMOUNT','FEEBYI','SERVICE_FEE','OTHERS') PURPOSE, SUM(CAT.CAS_TRAN_AMT) TOTAL_AMOUNT FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, APSNG101.SERVICE_LIST SL WHERE CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND CAT.ACCESS_CODE = SL.SERVICE_ACCESS_CODE AND CAL.CAS_ACC_NO = '" + agentWallet + "' AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpATSFromDate.DateString + "' AND '" + dtpATSToDate.DateString + "' GROUP BY SL.SERVICE_TITLE, CAT.ACCESS_CODE, CAT.CAS_TRAN_TYPE, CAT.CAS_TRAN_STATUS, CAT.CAS_TRAN_PURPOSE_CODE ORDER BY CAT.CAS_TRAN_STATUS, CAT.ACCESS_CODE, CAT.CAS_TRAN_TYPE";

            string strSqlPreviouseBalance = "SELECT GET_FIS_BALANCE_BY_DATE('" + agentWallet + "',TO_DATE('" + dtpATSFromDate.DateString + "')-1) FROM DUAL";
            string strPreviousBalance = objServiceHandler.ReturnString(strSqlPreviouseBalance);

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Agent_Transaction_Summary_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Trunsaction Summary Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Name : " + strAgentName.ToString() + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpATSFromDate.DateString + "' To '" + dtpATSToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Service Name</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Type</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Purpose</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                string strServiceCode = "";
                double crFM = 0, drFM = 0, crDPDCP = 0, drDPDCP = 0, crOthers = 0, drOthers = 0;

                strHTML = strHTML + " </tr> ";
                strHTML = strHTML + " <tr><td colspan='4'; style='text-align:right;' >Previous Day Balance </td>";
                strHTML = strHTML + " <td > " + strPreviousBalance.ToString() + " </td>";

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["ACCESS_CODE"].ToString().Equals("FM"))
                    {
                        strServiceCode = prow["ACCESS_CODE"].ToString();

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_TITLE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CAS_TRAN_TYPE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["PURPOSE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TOTAL_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";

                        if (prow["CAS_TRAN_TYPE"].ToString().Equals("CREDIT"))
                        {
                            crFM = Convert.ToDouble(prow["TOTAL_AMOUNT"].ToString());
                        }
                        else
                        {
                            drFM = Convert.ToDouble(prow["TOTAL_AMOUNT"].ToString());
                        }
                        SerialNo = SerialNo + 1;
                    }
                }
                strHTML = strHTML + " </tr> ";
                strHTML = strHTML + " <tr><td colspan='4'; style='text-align:right;' >Total " + strServiceCode.ToString() + "</td>";
                strHTML = strHTML + " <td > " + (crFM - drFM).ToString() + " </td>";

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["ACCESS_CODE"].ToString().Equals("UBPDP"))
                    {
                        strServiceCode = prow["ACCESS_CODE"].ToString();

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_TITLE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CAS_TRAN_TYPE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["PURPOSE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TOTAL_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";

                        if (prow["CAS_TRAN_TYPE"].ToString().Equals("CREDIT"))
                        {
                            crDPDCP = crDPDCP + Convert.ToDouble(prow["TOTAL_AMOUNT"].ToString());
                        }
                        else
                        {
                            drDPDCP = drDPDCP + Convert.ToDouble(prow["TOTAL_AMOUNT"].ToString());
                        }
                        SerialNo = SerialNo + 1;
                    }
                }
                strHTML = strHTML + " </tr> ";
                strHTML = strHTML + " <tr><td colspan='4' style='text-align:right;' >Total " + strServiceCode.ToString() + "</td>";
                strHTML = strHTML + " <td > " + (drDPDCP - crDPDCP).ToString() + " </td>";

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (!prow["ACCESS_CODE"].ToString().Equals("FM") && !prow["ACCESS_CODE"].ToString().Equals("UBPDP"))
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_TITLE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CAS_TRAN_TYPE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["PURPOSE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TOTAL_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";

                        if (prow["CAS_TRAN_TYPE"].ToString().Equals("CREDIT"))
                        {
                            crOthers = crOthers + Convert.ToDouble(prow["TOTAL_AMOUNT"].ToString());
                        }
                        else
                        {
                            drOthers = drOthers + Convert.ToDouble(prow["TOTAL_AMOUNT"].ToString());
                        }
                        SerialNo = SerialNo + 1;
                    }
                }
                strHTML = strHTML + " </tr> ";
                strHTML = strHTML + " <tr><td colspan='4' style='text-align:right;' > Closing Balance As On " + dtpATSToDate.DateString + " </td>";
                strHTML = strHTML + " <td > " + ((crFM + crDPDCP + crOthers + Convert.ToDouble(strPreviousBalance)) - (drFM + drDPDCP + drOthers)).ToString() + " </td>";
            }

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	protected void chkAllDistributor_CheckedChanged(object sender, EventArgs e)
    {
        txtPBazarDistributor.Text = "";
        if (chkAllDistributor.Checked){
            txtPBazarDistributor.Enabled = false;
        }
        else {
            txtPBazarDistributor.Enabled = true;
        }
    }
	protected void btnPBazarReport_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;
            string strSql = "";
            string strSqlFilter = "";
            if (!string.IsNullOrEmpty(txtPBazarDistributor.Text))
            {
                strSqlFilter = " AND THA.DEL_ACCNT_NO = '" + txtPBazarDistributor.Text.Trim() + "'";
            }
            else
            {
                strSqlFilter = "";
            }
            strSql = "SELECT   THA.DEL_ACCNT_NO DIS_NO,  CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRESS,GET_FIS_BALANCE (THA.DEL_ACCNT_NO) DIS_BAL "
                    + " FROM   TEMP_HIERARCHY_LIST_ALL THA,ACCOUNT_LIST ALD,CLIENT_LIST CLD, ACCOUNT_RANK AR "
                    + " WHERE       THA.DEL_ACCNT_ID = ALD.ACCNT_ID  AND ALD.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND ALD.CLINT_ID = CLD.CLINT_ID "
                    + " AND AR.ACCNT_RANK_ID = '180128000000000006' " + strSqlFilter
                    + " GROUP BY  THA.DEL_ACCNT_NO,CLD.CLINT_NAME,CLD.CLINT_ADDRESS1,GET_FIS_BALANCE (THA.DEL_ACCNT_NO)ORDER BY CLD.CLINT_NAME ";


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            DataSet dtsDSE = new DataSet();
            DataSet dtsAgent = new DataSet();
            fileName = "PBazar_Current_Balance_Details_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>PBazar Current Balance Details Report</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Balance</td>";
            strHTML = strHTML + "<td valign='middle' >DSE No</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Name</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Address</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Balance</td>";
            strHTML = strHTML + "<td valign='middle' >Agent No</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Address</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Balance</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    string strDistributor = prow["DIS_NO"].ToString();
                    // Distributor List
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DIS_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DIS_BAL"].ToString() + " </td>";
                    strHTML = strHTML + " <td COLSPAN=8> ";

                    string strDSE = "SELECT   THA.SA_ACCNT_NO DSE_NO, CLE.CLINT_NAME DSE_NAME, CLE.CLINT_ADDRESS1 DSE_ADDRESS, GET_FIS_BALANCE (THA.SA_ACCNT_NO) DSE_BAL "
                                    + "  FROM   TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD,ACCOUNT_LIST ALE, CLIENT_LIST CLE, ACCOUNT_RANK AR "
                                    + " WHERE       THA.DEL_ACCNT_ID = ALD.ACCNT_ID AND ALD.ACCNT_RANK_ID = AR.ACCNT_RANK_ID "
                                    + " AND ALD.CLINT_ID = CLD.CLINT_ID AND  THA.SA_ACCNT_NO = ALE.ACCNT_NO AND ALE.CLINT_ID = CLE.CLINT_ID "
                                    + " AND AR.ACCNT_RANK_ID = '180128000000000006' AND THA.DEL_ACCNT_NO = '" + strDistributor.Trim() + "'"
                                    + " GROUP BY THA.SA_ACCNT_NO, CLE.CLINT_NAME, CLE.CLINT_ADDRESS1,GET_FIS_BALANCE (THA.SA_ACCNT_NO) ORDER BY  CLE.CLINT_NAME";
                    dtsDSE = objServiceHandler.ExecuteQuery(strDSE);
                    strHTML = strHTML + "<table border=\"1\" width=\"100%\">";

                    if (dtsDSE.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow prowDSE in dtsDSE.Tables[0].Rows)
                        {
                            string strDSENo = prowDSE["DSE_NO"].ToString();
                            strHTML = strHTML + "<tr>";
                            strHTML = strHTML + " <td > '" + prowDSE["DSE_NO"].ToString() + " </td>";
                            strHTML = strHTML + " <td > " + prowDSE["DSE_ADDRESS"].ToString() + " </td>";
                            strHTML = strHTML + " <td > " + prowDSE["DSE_ADDRESS"].ToString() + " </td>";
                            strHTML = strHTML + " <td > " + prowDSE["DSE_BAL"].ToString() + " </td>";
                            strHTML = strHTML + " <td COLSPAN=4> ";
                            string strAgent = "SELECT THA.A_ACCNT_NO AGENT_NO, CLA.CLINT_NAME AGENT_NAME, CLA.CLINT_ADDRESS1 AGENT_ADDRESS, GET_FIS_BALANCE "
                                            + " (THA.A_ACCNT_NO) AGENT_BAL FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, ACCOUNT_LIST ALA, "
                                            + " CLIENT_LIST CLA, ACCOUNT_RANK AR WHERE THA.DEL_ACCNT_ID = ALD.ACCNT_ID AND ALD.ACCNT_RANK_ID = AR.ACCNT_RANK_ID "
                                            + " AND ALD.CLINT_ID = CLD.CLINT_ID AND  THA.A_ACCNT_NO = ALA.ACCNT_NO  "
                                            + " AND ALA.CLINT_ID=CLA.CLINT_ID AND AR.ACCNT_RANK_ID = '180128000000000006' AND THA.DEL_ACCNT_NO='" + strDistributor.Trim() + "' AND THA.SA_ACCNT_NO = '" + strDSENo.Trim() + "'"
                                            + " GROUP BY THA.A_ACCNT_NO, CLA.CLINT_NAME,CLA.CLINT_ADDRESS1,GET_FIS_BALANCE (THA.A_ACCNT_NO) ORDER BY CLA.CLINT_NAME";
                            dtsAgent = objServiceHandler.ExecuteQuery(strAgent);
                            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                            if (dtsAgent.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow prowAgent in dtsAgent.Tables[0].Rows)
                                {
                                    strHTML = strHTML + "<tr>";
                                    strHTML = strHTML + " <td > '" + prowAgent["AGENT_NO"].ToString() + " </td>";
                                    strHTML = strHTML + " <td > " + prowAgent["AGENT_NAME"].ToString() + " </td>";
                                    strHTML = strHTML + " <td > " + prowAgent["AGENT_ADDRESS"].ToString() + " </td>";
                                    strHTML = strHTML + " <td > " + prowAgent["AGENT_BAL"].ToString() + " </td>";
                                    strHTML = strHTML + " </tr> ";

                                }
                            }


                            strHTML = strHTML + " </table>";
                            strHTML = strHTML + " </td>";
                            strHTML = strHTML + " </tr> ";
                        }
                    }
                    strHTML = strHTML + " </table>";
                    strHTML = strHTML + " </td>";
                    strHTML = strHTML + " </tr> ";


                    SerialNo = SerialNo + 1;

                }
            }

            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnDPDCPAgent_Click(object sender, EventArgs e)
    {
        try
        {
            int SerialNo = 1;
            string agentWallet = txtAgentNumber.Text.Trim();

            string strSql = "";

            strSql = " SELECT ACCNT_NO,CLINT_NAME, TO_CHAR(CAS_TRAN_DATE) CAS_TRAN_DATE, SUM(DEBIT) DEBIT, SUM(CREDIT) CREDIT FROM (SELECT AL.ACCNT_NO, CL.CLINT_NAME, CAT.CAS_TRAN_DATE, NVL(DECODE(CAT.CAS_TRAN_TYPE,'D',SUM(CAT.CAS_TRAN_AMT)),0) DEBIT, NVL(DECODE(CAT.CAS_TRAN_TYPE,'C',SUM(CAT.CAS_TRAN_AMT)),0) CREDIT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, CLIENT_BANK_ACCOUNT CBA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_ID = CBA.ACCNT_ID AND CBA.CLINT_BANK_ACC_LOGIN = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND AL.ACCNT_RANK_ID = '200203000000000001' AND CAT.ACCESS_CODE = 'FM' AND TRUNC(CAS_TRAN_DATE) BETWEEN '" + dtpAFMDetailFrom.DateString + "' AND '" + dtpAFMDetailTo.DateString + "' GROUP BY AL.ACCNT_NO, CL.CLINT_NAME, CAT.CAS_TRAN_DATE, CAT.CAS_TRAN_TYPE ORDER BY CAT.CAS_TRAN_DATE) GROUP BY ACCNT_NO,CLINT_NAME, TO_CHAR(CAS_TRAN_DATE) ORDER BY TO_CHAR(CAS_TRAN_DATE)";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Agent_FM_Transaction_Detail_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Fund Management Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpAFMDetailFrom.DateString + "' To '" + dtpAFMDetailTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Debit</td>";
            strHTML = strHTML + "<td valign='middle' >Credit</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DEBIT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["CREDIT"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void DPDCbtn_Click(object sender, EventArgs e)
    {

        string sl = "0" ;
        string agentAccNo = "";
        string ctmrMeter = "";
        double billAmnt =  0;
        string trnsTyp = "";
        double fees = 0;
        double vat1 = 0;
        double afterVat1 = 0;
        double tax1 = 0;
        double afterTax1 = 0;
        double bankCom = 0;
        double agentCom = 0;
        double trdPartyCom = 0;
        double MBLCom = 0;
        double DPDcCom = 0;
        

        try
        {
           
            string strSql = "";

            strSql = "SELECT ROW_NUMBER() Over (Order by AGENT_ACCNT_NO) As SL, AGENT_ACCNT_NO,CUS_METER_ID ,BILL_AMOUNT,TRANS_TYPE,TRANS_TIME  FROM DPDC_PREPAID_BILL_COL_DETAIL where trans_time between ('8-Aug-2019') and  ('8-Aug-2019') and bill_amount ='1300' or bill_amount ='505' ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "DPDC BILL COLLECTION";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);


            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>DPDC BILL COLLECTION REPORT</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpAFMDetailFrom.DateString + "' To '" + dtpAFMDetailTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl. No.</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Meter Number</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Tran Type</td>";
            strHTML = strHTML + "<td valign='middle' >Tran Time</td>";
            strHTML = strHTML + "<td valign='middle' >Fees</td>";
            strHTML = strHTML + "<td valign='middle' >Vat 1</td>"; 
            strHTML = strHTML + "<td valign='middle' >After Vat 1</td>";
            strHTML = strHTML + "<td valign='middle' >Tax 1</td>";
            strHTML = strHTML + "<td valign='middle' >After Tax 1</td>";
            strHTML = strHTML + "<td valign='middle' >Bank Commission</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Commission</td>";
            strHTML = strHTML + "<td valign='middle' >Third Party Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";
            strHTML = strHTML + "<td valign='middle' >DPDC Commission</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                     billAmnt = Int64.Parse(prow["BILL_AMOUNT"].ToString());

                     if (billAmnt == 0)
                     {
                         fees = 0;
                     }
                    if (billAmnt <= 400 )                    
                    {
                        fees = 5;                                           
                    }
                    if ((billAmnt > 400 && billAmnt < 1501))
                    {
                        fees = 10;
                    }
                    if (billAmnt > 1500 && billAmnt <5001 )
                    {
                        fees = 15;
                    }
                    if (billAmnt >= 5001)
                    {
                        fees = 25;
                    }

                    vat1 = fees * 15 / 115;
                    afterVat1 = fees - vat1;
                    tax1 = afterVat1 * 10 / 100;
                    afterTax1 = afterVat1 - tax1;
                    bankCom = afterTax1 * 30 / 100;
                    agentCom = afterTax1 * 50 / 100;
                    trdPartyCom = afterTax1 * 20 / 100;
                    MBLCom = bankCom * 1 / 3;
                    DPDcCom = bankCom * 2 / 3;


                    strHTML = strHTML + " <tr><td > '" + prow["SL"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUS_METER_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TRANS_TYPE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TRANS_TIME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + fees.ToString() + " </td>";
                    strHTML = strHTML + " <td > " + vat1.ToString("0.0000") + " </td>";
                    strHTML = strHTML + " <td > " + afterVat1.ToString("0.0000") + " </td>";
                    strHTML = strHTML + " <td > " + tax1.ToString("0.0000") + " </td>";
                    strHTML = strHTML + " <td > " + afterTax1.ToString("0.0000") + " </td>";
                    strHTML = strHTML + " <td > " + bankCom.ToString("0.0000") + " </td>";
                    strHTML = strHTML + " <td > " + agentCom.ToString("0.0000") + " </td>";
                    strHTML = strHTML + " <td > " + trdPartyCom.ToString("0.0000") + " </td>";
                    strHTML = strHTML + " <td > " + MBLCom.ToString("0.0000") + " </td>";
                    strHTML = strHTML + " <td > " + DPDcCom.ToString("0.0000") + " </td>";

                    strHTML = strHTML + " </tr> ";


                }
            }

           
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
