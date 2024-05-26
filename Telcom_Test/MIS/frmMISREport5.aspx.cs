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

public partial class MIS_frmMISREport5 : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();


                DateTime dt = DateTime.Now;
                dtpTmToFromDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));

                dtpTmToAgtFromDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));
                dtpTmToAgtToDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));
                //dtpToRgAcv.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));

                LoadBankBranch();

                dtpDisBalFromDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));
                dtpDisBalToDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
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


        if (rbAllDis.SelectedValue == "AllDistributor")
        {
            txtDisCusWallet.Text = "";
            txtDisCusWallet.Enabled = false;            
        }


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

    private void LoadBankBranch()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT BANK_BRNCH_ID, BRANCH_NAME FROM CM_BANK_BRANCH ORDER BY BRANCH_NAME ASC";
            sdsBranch.SelectCommand = strSql;
            sdsBranch.DataBind();
            drpBankBranch.DataBind();

            //DataSet ods = objServiceHandler.ExecuteQuery(strSql);

        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }


    protected void btnTmToSumm_Click(object sender, EventArgs e)
    {
        try
        {
            string strDate = "";
            strDate = dtpTmToFromDate.DateString;
            //strDate = strDate.Replace("-", "/");
            string strMonth = "";
            string[] dateArray = strDate.Split('-');
            if (dateArray[1].ToString() == "Jan")
            {
                strMonth = "1";
            }
            else if (dateArray[1].ToString() == "Feb")
            {
                strMonth = "2";
            }
            else if (dateArray[1].ToString() == "Mar")
            {
                strMonth = "3";
            }
            else if (dateArray[1].ToString() == "Apr")
            {
                strMonth = "4";
            }
            else if (dateArray[1].ToString() == "May")
            {
                strMonth = "5";
            }
            else if (dateArray[1].ToString() == "Jun")
            {
                strMonth = "6";
            }
            else if (dateArray[1].ToString() == "Jul")
            {
                strMonth = "7";
            }
            else if (dateArray[1].ToString() == "Aug")
            {
                strMonth = "8";
            }
            else if (dateArray[1].ToString() == "Sep")
            {
                strMonth = "9";
            }
            else if (dateArray[1].ToString() == "Oct")
            {
                strMonth = "10";
            }
            else if (dateArray[1].ToString() == "Nov")
            {
                strMonth = "11";
            }
            else if (dateArray[1].ToString() == "Dec")
            {
                strMonth = "12";
            }
            else
            {
                // donothing
            }


            string strNewDateFormat = strMonth + "/" + dateArray[0].ToString() + "/" + dateArray[2].ToString();

            string strNewDateFormat10am = strNewDateFormat + " 10:00:00 AM";
            string strNewDateFormat1159am = strNewDateFormat + " 11:59:59 AM";
            string strNewDateFormat12pm = strNewDateFormat + " 12:00:00 PM";
            string strNewDateFormat0159pm = strNewDateFormat + " 01:59:59 PM";
            string strNewDateFormat0200pm = strNewDateFormat + " 02:00:00 PM";
            string strNewDateFormat0600pm = strNewDateFormat + " 06:00:00 PM";

            string strSql = "";
            //strSql = " SELECT DISTINCT MR.REGION_NAME, MATM.AREA_NAME TM_AREA, ALTM.ACCNT_NO TM_NO, "
            //        + " CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALDIS.ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, "
            //        + " MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, "
            //        + " 'CASH IN' MARKET_TYPE, CAB.CAS_ACCNT_BALANCE DIS_BALANCE, CAB.UPTO_DATE BAL_DATE, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(ALDIS.ACCNT_NO,TO_CHAR(TO_DATE(SYSDATE-3)), TO_CHAR(TO_DATE(SYSDATE-3))) PREV_3DAY_LIFTING, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(ALDIS.ACCNT_NO,TO_CHAR(TO_DATE(SYSDATE-2)), TO_CHAR(TO_DATE(SYSDATE-2))) PREV_2DAY_LIFTING, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(ALDIS.ACCNT_NO,TO_CHAR(TO_DATE(SYSDATE-1)), TO_CHAR(TO_DATE(SYSDATE-1))) PREV_DAY_LIFTING "
            //        + " FROM ACCOUNT_LIST ALTM, MANAGE_TERRITORY_RANK MTRM, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTO, CLIENT_LIST CLTM, "
            //        + " MANAGE_TERRITORY_AREA MTATM, MANAGE_AREA MATM, CLIENT_LIST CLTO, MANAGE_REGION MR, MANAGE_TERRITORY_HIERARCHY MTHDIS, "
            //        + " ACCOUNT_LIST ALDIS, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, CLIENT_LIST CLDIS, "
            //        + " MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE ALTM.TERRITORY_RANK_ID = MTRM.TERRITORY_RANK_ID(+) "
            //        + " AND ALTM.TERRITORY_RANK_ID = '150121000000000001' AND ALTM.ACCNT_ID = MTHTO.HIERARCHY_ACCNT_ID(+) "
            //        + " AND MTHTO.ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTATM.ACCNT_ID(+) "
            //        + " AND MTATM.AREA_ID = MATM.AREA_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MATM.REGION_ID = MR.REGION_ID(+) "
            //        + " AND ALTO.ACCNT_ID = MTHDIS.HIERARCHY_ACCNT_ID(+) AND MTHDIS.ACCNT_ID = ALDIS.ACCNT_ID(+) AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) "
            //        + " AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND  MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) AND ALDIS.ACCNT_NO = CAL.CAS_ACC_NO(+) "
            //        + " AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID(+) ORDER BY ALTO.ACCNT_NO ASC ";


            //strSql = " SELECT DISTINCT MR.REGION_NAME, MATM.AREA_NAME TM_AREA, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
            //        + " MTRM.TERRITORY_RANK_NAME TM_RANK, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALDIS.ACCNT_NO DIS_NO, "
            //        + " CLDIS.CLINT_NAME DIS_NAME, MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, "
            //        + " 'CASH IN' MARKET_TYPE, CAB.CAS_ACCNT_BALANCE DIS_BALANCE, CAB.UPTO_DATE BAL_DATE, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(ALDIS.ACCNT_NO,TO_CHAR(TO_DATE(SYSDATE-3)), TO_CHAR(TO_DATE(SYSDATE-3))) PREV_3DAY_LIFTING, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(ALDIS.ACCNT_NO,TO_CHAR(TO_DATE(SYSDATE-2)), TO_CHAR(TO_DATE(SYSDATE-2))) PREV_2DAY_LIFTING, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(ALDIS.ACCNT_NO,TO_CHAR(TO_DATE(SYSDATE-1)), TO_CHAR(TO_DATE(SYSDATE-1))) PREV_DAY_LIFTING, "
            //        + " FUNC_AMT_OF_DIS_LFT_WTH_TIME(ALDIS.ACCNT_NO,'" + strNewDateFormat10am + "', '" + strNewDateFormat1159am + "' ) PREV_DAY_LFT_10TO12, "
            //        + " FUNC_AMT_OF_DIS_LFT_WTH_TIME(ALDIS.ACCNT_NO,'" + strNewDateFormat12pm + "', '" + strNewDateFormat0159pm + "' ) PREV_DAY_LFT_12TO2, "
            //        + " FUNC_AMT_OF_DIS_LFT_WTH_TIME(ALDIS.ACCNT_NO,'" + strNewDateFormat0200pm + "', '" + strNewDateFormat0600pm + "' ) PREV_DAY_LFT_2TO6 "
            //        + " FROM ACCOUNT_LIST ALTM, MANAGE_TERRITORY_RANK MTRM, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTO, CLIENT_LIST CLTM, "
            //        + " MANAGE_TERRITORY_AREA MTATM, MANAGE_AREA MATM, CLIENT_LIST CLTO, MANAGE_REGION MR, MANAGE_TERRITORY_HIERARCHY MTHDIS, "
            //        + " ACCOUNT_LIST ALDIS, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, CLIENT_LIST CLDIS, "
            //        + " MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE ALTM.TERRITORY_RANK_ID = MTRM.TERRITORY_RANK_ID(+) AND ALTM.TERRITORY_RANK_ID = '150121000000000001' "
            //        + " AND ALTM.ACCNT_ID = MTHTO.HIERARCHY_ACCNT_ID(+) AND MTHTO.ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) "
            //        + " AND ALTM.ACCNT_ID = MTATM.ACCNT_ID(+) AND MTATM.AREA_ID = MATM.AREA_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) "
            //        + " AND MATM.REGION_ID = MR.REGION_ID(+) AND ALTO.ACCNT_ID = MTHDIS.HIERARCHY_ACCNT_ID(+) AND MTHDIS.ACCNT_ID = ALDIS.ACCNT_ID(+) "
            //        + " AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND  MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) "
            //        + " AND ALDIS.ACCNT_NO = CAL.CAS_ACC_NO(+) AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID(+) ORDER BY ALTO.ACCNT_NO ASC ";

            strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME TM_AREA, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, "
                    + " CLTO.CLINT_NAME TO_NAME, TMDIS.DISTRIBUTOR_NO DIS_NO, TMDIS.DIS_NAME, TMDIS.THANA_NAME DIS_THANA, TMDIS.DISTRICT_NAME DIS_DISTRICT, "
                    + " 'CASH IN' MARKET_TYPE, CAB.CAS_ACCNT_BALANCE DIS_BALANCE, CAB.UPTO_DATE BAL_DATE, "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(TMDIS.DISTRIBUTOR_NO,TO_CHAR(TO_DATE(SYSDATE-3)), TO_CHAR(TO_DATE(SYSDATE-3))) PREV_3DAY_LIFTING,  "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(TMDIS.DISTRIBUTOR_NO,TO_CHAR(TO_DATE(SYSDATE-2)), TO_CHAR(TO_DATE(SYSDATE-2))) PREV_2DAY_LIFTING,  "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(TMDIS.DISTRIBUTOR_NO,TO_CHAR(TO_DATE(SYSDATE-1)), TO_CHAR(TO_DATE(SYSDATE-1))) PREV_DAY_LIFTING,  "
                    + " FUNC_AMT_OF_DIS_LFT_WTH_TIME(TMDIS.DISTRIBUTOR_NO,'" + strNewDateFormat10am + "', '" + strNewDateFormat1159am + "' ) PREV_DAY_LFT_10TO12, "
                    + " FUNC_AMT_OF_DIS_LFT_WTH_TIME(TMDIS.DISTRIBUTOR_NO,'" + strNewDateFormat12pm + "', '" + strNewDateFormat0159pm + "' ) PREV_DAY_LFT_12TO2, "
                    + " FUNC_AMT_OF_DIS_LFT_WTH_TIME(TMDIS.DISTRIBUTOR_NO,'" + strNewDateFormat0200pm + "', '" + strNewDateFormat0600pm + "' ) PREV_DAY_LFT_2TO6 "
                    + " FROM (SELECT DISTINCT THA.DEL_ACCNT_NO DISTRIBUTOR_NO, ALD.ACCNT_ID DIS_ACC_ID, CLD.CLINT_NAME DIS_NAME, MTD.THANA_NAME, MDD.DISTRICT_NAME "
                    + " FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE THA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) "
                    + " AND ALD.ACCNT_STATE IN ('A') AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALD.ACCNT_RANK_ID IN ('120519000000000003') ) TMDIS,  "
                    + " MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, "
                    + " MANAGE_REGION MR, CLIENT_LIST CLTM, CLIENT_LIST CLTO, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB  "
                    + " WHERE TMDIS.DIS_ACC_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) "
                    + " AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID (+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) "
                    + " AND MA.REGION_ID = MR.REGION_ID(+)  AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND TMDIS.DISTRIBUTOR_NO = CAL.CAS_ACC_NO(+) "
                    + " AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID(+) ORDER BY ALTO.ACCNT_NO DESC ";



            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "TM_TO_Summary_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Opening Balance(Date: '" + dtpTmToFromDate.DateString + "')</h2></td></tr>";
            strHTML = strHTML + "</table>";
            
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Market Type</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Opening Balance</td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor Balance Date</td>";

            strHTML = strHTML + "<td valign='middle' >Last Day Lifting(10 AM- 12 PM)</td>";
            strHTML = strHTML + "<td valign='middle' >Last Day Lifting(12 PM- 2 PM)</td>";
            strHTML = strHTML + "<td valign='middle' >Last Day Lifting (2 PM-6 PM)</td>";



            strHTML = strHTML + "<td valign='middle' >Last Day Lifting</td>";
            strHTML = strHTML + "<td valign='middle' >Previous 2nd Day Lifting</td>";
            strHTML = strHTML + "<td valign='middle' >Previous 3rd Day Lifting</td>";
            
            
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_AREA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MARKET_TYPE"].ToString() + "</td>";
                    if (prow["DIS_BALANCE"].ToString() == "")
                    {
                        strHTML = strHTML + " <td > '" + " " + " </td>";
                    }
                    else
                    {
                        strHTML = strHTML + " <td > '" + System.Math.Round(Convert.ToDouble(prow["DIS_BALANCE"].ToString()), 2) + " </td>";
                    }
                    
                    //if (prow["BAL_DATE"].ToString() == "")
                    //{
                    //    strHTML = strHTML + " <td > '" + " " + " </td>";
                    //}
                    //else
                    //{
                    //    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["BAL_DATE"].ToString())) + " </td>";
                    //}

                    strHTML = strHTML + " <td > '" + prow["PREV_DAY_LFT_10TO12"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["PREV_DAY_LFT_12TO2"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["PREV_DAY_LFT_2TO6"].ToString() + " </td>";



                    strHTML = strHTML + " <td > '" + prow["PREV_DAY_LIFTING"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["PREV_2DAY_LIFTING"].ToString() + " </td>";                    
                    strHTML = strHTML + " <td > '" + prow["PREV_3DAY_LIFTING"].ToString() + "</td>";
                    
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    

                }
            }

            strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TM_TO_Summary_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        
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

    protected void btnAllBrnchregRpt_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT CBB.BRANCH_NAME, ASD.REQUEST_ID REQ_ID, ASD.ACTIVATION_DATE, ASD.CUSTOMER_MOBILE_NO, "
                    + " CLC.CLINT_NAME CUST_NAME, CLC.UPDATE_DATE, CLC.VERIFIED_DATE, ARC.RANK_TITEL, ASD.AGENT_MOBILE_NO, "
                    + " CLB.CLINT_NAME AGT_NAME FROM SERVICE_REQUEST SR, ACCOUNT_SERIAL_DETAIL ASD, CM_BANK_BRANCH CBB, ACCOUNT_LIST ALC, "
                    + " CLIENT_LIST CLC, ACCOUNT_RANK ARC, ACCOUNT_LIST ALB, CLIENT_LIST CLB WHERE SR.BANK_BRANCH_ID IS NOT NULL "
                    + " AND SR.REQUEST_ID = ASD.REQUEST_ID AND SR.BANK_BRANCH_ID = CBB.BANK_BRNCH_ID "
                    + " AND ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND ALC.CLINT_ID = CLC.CLINT_ID "
                    + " AND ALC.ACCNT_RANK_ID = ARC.ACCNT_RANK_ID AND ASD.AGENT_MOBILE_NO = ALB.ACCNT_MSISDN "
                    + " AND ALB.CLINT_ID = CLB.CLINT_ID  AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpBBrnFromDate.DateString + "' AND '" + dtpBBrnToDate.DateString + "' "
                    + " ORDER BY CBB.BRANCH_NAME, ASD.ACTIVATION_DATE ASC ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "BR_Reg_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Branch wise Customer Registration Report(All Branch)</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBBrnFromDate.DateString + "' To '" + dtpBBrnToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Branch Name</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Mobile No</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Rank</td>";            
            strHTML = strHTML + "<td valign='middle' >KYC Update Date</td>";
            strHTML = strHTML + "<td valign='middle' >KYC Verify Date </td>";
            strHTML = strHTML + "<td valign='middle' >BPE No</td>";
            strHTML = strHTML + "<td valign='middle' >BPE Name </td>";

            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BRANCH_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQ_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ACTIVATION_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUSTOMER_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + "</td>";
                    
                    if (prow["UPDATE_DATE"].ToString() == "")
                    {
                        strHTML = strHTML + " <td > '" + "" + "</td>";
                    }
                    else
                    {
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["UPDATE_DATE"].ToString())) + " </td>";
                    }
                    if (prow["VERIFIED_DATE"].ToString() == "")
                    {
                        strHTML = strHTML + " <td > '" + "" + "</td>";
                    }
                    else
                    {
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                    }
                    strHTML = strHTML + " <td > '" + prow["AGENT_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + " </td>";

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

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "BR_Reg_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnBrnchregRpt_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = " SELECT DISTINCT CB.BRANCH_NAME, AL.ACCNT_NO CUST_NO, CL.CLINT_NAME CUST_NAME, "
                            + " AR.RANK_TITEL, ASD.ACTIVATION_DATE, CL.UPDATE_DATE, CL.VERIFIED_DATE, "
                            + " ALA.ACCNT_NO AGT_NNO , CLA.CLINT_NAME AGT_NAME FROM CM_BANK_BRANCH CB, "
                            + " ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST AL, CLIENT_LIST CL, ACCOUNT_RANK AR, "
                            + " ACCOUNT_LIST ALA, CLIENT_LIST CLA WHERE CB.BANK_BRNCH_ID = ASD.BANK_BRANCH_ID "
                            + " AND ASD.CUSTOMER_MOBILE_NO = AL.ACCNT_MSISDN AND AL.CLINT_ID = CL.CLINT_ID "
                            + " AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND ASD.AGENT_MOBILE_NO = ALA.ACCNT_MSISDN "
                            + " AND ALA.CLINT_ID = CLA.CLINT_ID AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpBBrnFromDate.DateString + "' AND '" + dtpBBrnToDate.DateString + "' "
                            + " AND CB.BANK_BRNCH_ID = '" + drpBankBranch.SelectedValue + "' ORDER BY ASD.ACTIVATION_DATE DESC ";


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "BR_Reg_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Branch wise Customer Registration Report(" + drpBankBranch.SelectedItem.Text + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBBrnFromDate.DateString + "' To '" + dtpBBrnToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Branch Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
            strHTML = strHTML + "<td valign='middle' >KYC Update Date</td>";
            strHTML = strHTML + "<td valign='middle' >KYC Verify Date </td>";
            strHTML = strHTML + "<td valign='middle' >BPE No</td>";
            strHTML = strHTML + "<td valign='middle' >BPE Name </td>";

            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BRANCH_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ACTIVATION_DATE"].ToString())) + " </td>";
                    if (prow["UPDATE_DATE"].ToString() == "")
                    {
                        strHTML = strHTML + " <td > '" + "" + "</td>";
                    }
                    else
                    {
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["UPDATE_DATE"].ToString())) + " </td>";
                    }
                    if (prow["VERIFIED_DATE"].ToString() == "")
                    {
                        strHTML = strHTML + " <td > '" + "" + "</td>";
                    }
                    else
                    {
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                    }
                    strHTML = strHTML + " <td > '" + prow["AGT_NNO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + " </td>";

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

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "BR_Reg_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnTmToAgtReport_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            DateTime dt1 = dtpTmToAgtFromDate.Date;
            DateTime dt2 = dtpTmToAgtToDate.Date;
            double dateCount = (dt2 - dt1).TotalDays;
            //if (dateCount > 2)
            //{
            //    lblMsg.Text = "Maximium Date Range is 3 days";
            //    return;
            //}


            string strSql = "";
            //strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
            //        + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALDIS.ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, "
            //        + " CLDIS.CLINT_ADDRESS1 DIS_ADDRESS, MTT.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, "
            //        + " ALA.ACCNT_NO AGT_NO, ARA.RANK_TITEL AGT_RANK, CAB.CAS_ACCNT_BALANCE AGT_BALANCE, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_RG, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')NO_OF_CASIN, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')CASIN_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')CASOUT_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')NO_OF_BD, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')BD_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_CNT (ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_BP, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_AMT (ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) BP_AMOUNT "
            //        + " FROM MANAGE_REGION MR, MANAGE_AREA MA, MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, MANAGE_TERRITORY_HIERARCHY MTATM, "
            //        + " ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALDIS, ACCOUNT_RANK ARDIS, CLIENT_LIST CLDIS, "
            //        + " MANAGE_THANA MTT, MANAGE_DISTRICT MDD, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, "
            //        + " BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, ACCOUNT_RANK ARA, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, CLIENT_LIST CLTM, CLIENT_LIST CLTO "
            //        + " WHERE MR.REGION_ID = MA.REGION_ID(+) AND MA.AREA_ID = MTA.AREA_ID(+) AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) "
            //        + " AND ALTM.ACCNT_ID = MTATM.HIERARCHY_ACCNT_ID(+) AND MTATM.ACCNT_ID = ALTO.ACCNT_ID(+) AND MTATM.ACCNT_ID = MTHTO.HIERARCHY_ACCNT_ID(+) "
            //        + " AND MTHTO.ACCNT_ID = ALDIS.ACCNT_ID(+) AND ALDIS.ACCNT_RANK_ID = ARDIS.ACCNT_RANK_ID(+) AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) "
            //        + " AND CLDIS.THANA_ID = MTT.THANA_ID(+) AND MTT.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO(+) "
            //        + " AND THA.A_ACCNT_NO = ALA.ACCNT_NO(+) AND ALA.ACCNT_NO = CAL.CAS_ACC_NO(+) AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID(+) "
            //        + " AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' "
            //        + " AND CAT.ACCESS_CODE IN ('CN', 'CCT', 'SW', 'BD', 'UBP', 'UBPW' ) AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID(+) "
            //        + " AND ALA.ACCNT_RANK_ID = '120519000000000005' AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) "
            //        + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) ORDER BY ALTM.ACCNT_NO ASC ";


            //strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME , ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, "
            //        + " CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRESS, MTD.THANA_NAME DIS_THANA, "
            //        + " MDD.DISTRICT_NAME DIS_DISTRICT, TM_AG.AGENT_NO AGT_NO, TM_AG.AGT_RANK, TM_AG.AGENT_BALANCE AGT_BALANCE, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG(TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_RG,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')NO_OF_CASIN, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')CASIN_AMT,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')CASOUT_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')NO_OF_BD,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')BD_AMT,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_CNT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_BP,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_AMT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) BP_AMOUNT, "
            //        + " APSNG101.FUNC_AGT_FM_TRX_AMT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) FM_AMT "
            //        + " FROM (SELECT DISTINCT ALA.ACCNT_ID AGENT_ACC_ID, ALA.ACCNT_NO AGENT_NO, CAB.CAS_ACCNT_BALANCE  AGENT_BALANCE, ARA.RANK_TITEL AGT_RANK FROM ACCOUNT_LIST ALA, "
            //        + " BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, ACCOUNT_RANK ARA "
            //        + " WHERE ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID  AND ALA.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID "
            //        + " AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "') TM_AG, TEMP_HIERARCHY_LIST_ALL THA, "
            //        + " CLIENT_LIST CLD, ACCOUNT_LIST ALD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, "
            //        + " MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR "
            //        + " WHERE TM_AG.AGENT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) "
            //        + " AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)  "
            //        + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) "
            //        + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) "
            //        + " ORDER BY ALTO.ACCNT_NO DESC ";

            //strSql = "  SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME , ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO,  "
            //        + " CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRESS, "
            //        + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, "
            //        + " TM_AG.AGENT_NO AGT_NO, TM_AG.AGT_RANK, TM_AG.AGT_VRFY_DATE AGT_VRFY_DATE, TM_AG.AGENT_BALANCE AGT_BALANCE, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG(TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_RG,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')NO_OF_CASIN, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')CASIN_AMT,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')CASOUT_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')NO_OF_BD,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')BD_AMT,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_CNT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_BP,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_AMT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) BP_AMOUNT, "
            //        + " APSNG101.FUNC_AGT_FM_TRX_AMT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) FM_AMT "
            //        + " FROM (SELECT DISTINCT ALA.ACCNT_ID AGENT_ACC_ID, ALA.ACCNT_NO AGENT_NO, CLA.VERIFIED_DATE AGT_VRFY_DATE, CAB.CAS_ACCNT_BALANCE  AGENT_BALANCE, ARA.RANK_TITEL AGT_RANK "
            //        + " FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, ACCOUNT_RANK ARA "
            //        + " WHERE ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID IN ('120519000000000005') AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID  AND ALA.ACCNT_NO = CAL.CAS_ACC_NO "
            //        + " AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID  AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "') TM_AG, "
            //        + " TEMP_HIERARCHY_LIST_ALL THA,  CLIENT_LIST CLD, ACCOUNT_LIST ALD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, MANAGE_TERRITORY_HIERARCHY MTHD, "
            //        + " ACCOUNT_LIST ALTO, CLIENT_LIST CLTO,  MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, "
            //        + " MANAGE_REGION MR, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE WHERE TM_AG.AGENT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) "
            //        + " AND CLD.THANA_ID = MTD.THANA_ID(+)  AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) "
            //        + " AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)   AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) "
            //        + " AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+)  AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) "
            //        + " AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO(+) AND ALDSE.CLINT_ID = CLDSE.CLINT_ID(+) "
            //        + " ORDER BY ALTO.ACCNT_NO DESC ";

            strSql = " SELECT REGION_NAME, AREA_NAME, TM_NO, TM_NAME, TO_NO, TO_NAME, DIS_NO, DIS_NAME, CLINT_ADDRESS1 DIS_ADDRESS, THANA_NAME DIS_THANA, DISTRICT_NAME DIS_DISTRICT, DSE_NO, DSE_NAME, AGENT_NO AGT_NO, RANK_TITEL AGT_RANK, VERIFIED_DATE AGT_VRFY_DATE, NVL(AGT_BALANCE,0) AGT_BALANCE, NVL(SUM (NO_OF_RG),0) NO_OF_RG, NVL(SUM (NO_OF_CASIN),0) NO_OF_CASIN, NVL(SUM (CASIN_AMT),0) CASIN_AMT, NVL(SUM (NO_OF_CASOUT),0) NO_OF_CASOUT, NVL(SUM (CASOUT_AMT),0) CASOUT_AMT, NVL(SUM (NO_OF_BD),0) NO_OF_BD, NVL(SUM (BD_AMT),0) BD_AMT, NVL(SUM (NO_OF_BP),0) NO_OF_BP, NVL(SUM (BP_AMOUNT),0) BP_AMOUNT, NVL(SUM (FM_AMT),0) FM_AMT FROM ( "
            + " SELECT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALDIS.ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, ALA.ACCNT_NO AGENT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, GET_FIS_BALANCE_BY_DATE(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) AGT_BALANCE, COUNT(ASD.CUSTOMER_MOBILE_NO) NO_OF_RG, 0 NO_OF_CASIN, 0 CASIN_AMT, 0 NO_OF_CASOUT, 0 CASOUT_AMT, 0 NO_OF_BD, 0 BD_AMT, 0 NO_OF_BP, 0 BP_AMOUNT, 0 FM_AMT FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, ACCOUNT_RANK ARA, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, ACCOUNT_SERIAL_DETAIL ASD WHERE ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID AND ALA.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.SA_ACCNT_ID = ALDSE.ACCNT_ID(+) AND ALDSE.CLINT_ID = CLDSE.CLINT_ID(+) AND THA.DEL_ACCNT_ID = ALDIS.ACCNT_ID(+) AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) AND ALDIS.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTM.ACCNT_ID(+) AND MTHTM.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND '+88' || SUBSTR(THA.A_ACCNT_NO,1,11) = ASD.AGENT_MOBILE_NO AND ASD.BANK_CODE='MBL' AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, ALDIS.ACCNT_NO, CLDIS.CLINT_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO, CLDSE.CLINT_NAME, ALA.ACCNT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, GET_FIS_BALANCE_BY_DATE(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) "
            + " UNION ALL " 
            + " SELECT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALDIS.ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, ALA.ACCNT_NO AGENT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, GET_FIS_BALANCE_BY_DATE(ALA.ACCNT_NO, TRUNC(SYSDATE-1)) AGT_BALANCE, 0 NO_OF_RG, DECODE(TM.SERVICE_CODE, 'CN', COUNT(TM.REQUEST_PARTY)) NO_OF_CASIN, DECODE(TM.SERVICE_CODE, 'CN', SUM(TM.TRANSACTION_AMOUNT)) CASIN_AMT, NVL(DECODE(TM.SERVICE_CODE, 'CCT', COUNT(TM.REQUEST_PARTY)), 0) + NVL(DECODE(TM.SERVICE_CODE, 'SW', COUNT(TM.REQUEST_PARTY)),0) NO_OF_CASOUT, NVL(DECODE(TM.SERVICE_CODE, 'CCT', SUM(TM.TRANSACTION_AMOUNT)), 0) + NVL(DECODE(TM.SERVICE_CODE, 'SW', SUM(TM.TRANSACTION_AMOUNT)),0) CASOUT_AMT, NVL(DECODE(TM.SERVICE_CODE, 'BD', COUNT(TM.REQUEST_PARTY)),0) NO_OF_BD, NVL(DECODE(TM.SERVICE_CODE, 'BD', SUM(TM.TRANSACTION_AMOUNT)),0) BD_AMT, 0 NO_OF_BP, 0 BP_AMOUNT, 0 FM_AMT FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, ACCOUNT_RANK ARA, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID AND ALA.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.SA_ACCNT_ID = ALDSE.ACCNT_ID(+) AND ALDSE.CLINT_ID = CLDSE.CLINT_ID(+) AND THA.DEL_ACCNT_ID = ALDIS.ACCNT_ID(+) AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) AND ALDIS.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTM.ACCNT_ID(+) AND MTHTM.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND (THA.A_ACCNT_NO = TM.REQUEST_PARTY || '1' OR THA.A_ACCNT_NO = TM.RECEPENT_PARTY) AND TM.SERVICE_CODE IN ('CN', 'BD', 'CCT', 'SW') AND TM.TRANSACTION_DATE BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, ALDIS.ACCNT_NO, CLDIS.CLINT_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO, CLDSE.CLINT_NAME, ALA.ACCNT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, TM.SERVICE_CODE, GET_FIS_BALANCE_BY_DATE(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) "
            + " UNION ALL "
            + " SELECT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALDIS.ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, ALA.ACCNT_NO AGENT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, GET_FIS_BALANCE_BY_DATE(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) AGT_BALANCE, 0 NO_OF_RG, 0 NO_OF_CASIN, 0 CASIN_AMT, 0 NO_OF_CASOUT, 0 CASOUT_AMT, 0 NO_OF_BD, 0 BD_AMT, COUNT(UT.REQUEST_ID) NO_OF_BP, SUM(UT.TOTAL_BILL_AMOUNT) BP_AMOUNT, 0 FM_AMT FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, ACCOUNT_RANK ARA, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, TEMP_MIS_TRANSACTIONS_REPORT TM, SERVICE_REQUEST SR, UTILITY_TRANSACTION UT, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID AND ALA.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.SA_ACCNT_ID = ALDSE.ACCNT_ID(+) AND ALDSE.CLINT_ID = CLDSE.CLINT_ID(+) AND THA.DEL_ACCNT_ID = ALDIS.ACCNT_ID(+) AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) AND ALDIS.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTM.ACCNT_ID(+) AND MTHTM.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THA.A_ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.REQUEST_ID = SR.REQUEST_ID AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND TM.SERVICE_CODE IN ('UBP', 'UBPW', 'UWZP','UBPDSP','UBPREB','UBPKG') AND TM.TRANSACTION_DATE BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, ALDIS.ACCNT_NO, CLDIS.CLINT_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO, CLDSE.CLINT_NAME, ALA.ACCNT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, TM.SERVICE_CODE, GET_FIS_BALANCE_BY_DATE(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) "
			+ " UNION ALL "
            + " SELECT MR.REGION_NAME,MA.AREA_NAME,ALTM.ACCNT_NO TM_NO,CLTM.CLINT_NAME TM_NAME,ALTO.ACCNT_NO TO_NO,CLTO.CLINT_NAME TO_NAME,ALDIS.ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME,CLDIS.CLINT_ADDRESS1,MTDIS.THANA_NAME,MDDIS.DISTRICT_NAME,ALDSE.ACCNT_NO DSE_NO,CLDSE.CLINT_NAME DSE_NAME,ALA.ACCNT_NO AGENT_NO,ARA.RANK_TITEL,CLA.VERIFIED_DATE,GET_FIS_BALANCE_BY_DATE (ALA.ACCNT_NO,TO_CHAR (TO_DATE ('05-SEP-2018'))) AGT_BALANCE, 0 NO_OF_RG,0 NO_OF_CASIN,0 CASIN_AMT,0 NO_OF_CASOUT,0 CASOUT_AMT,0 NO_OF_BD,0 BD_AMT,COUNT (UT.REQUEST_ID) NO_OF_BP,SUM (UT.TOTAL_BILL_AMOUNT) BP_AMOUNT,0 FM_AMT FROM ACCOUNT_LIST ALA,CLIENT_LIST CLA,ACCOUNT_RANK ARA,TEMP_HIERARCHY_LIST_ALL THA,ACCOUNT_LIST ALDSE,CLIENT_LIST CLDSE,ACCOUNT_LIST ALDIS,CLIENT_LIST CLDIS,MANAGE_THANA MTDIS,MANAGE_DISTRICT MDDIS,MANAGE_TERRITORY_HIERARCHY MTHTO,ACCOUNT_LIST ALTO,CLIENT_LIST CLTO,MANAGE_TERRITORY_HIERARCHY MTHTM,ACCOUNT_LIST ALTM,CLIENT_LIST CLTM,MANAGE_TERRITORY_AREA MTA,MANAGE_AREA MA,MANAGE_REGION MR,TEMP_MIS_TRANSACTIONS_REPORT TM, UTILITY_TRANSACTION UT,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID AND ALA.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.SA_ACCNT_ID = ALDSE.ACCNT_ID(+) AND ALDSE.CLINT_ID = CLDSE.CLINT_ID(+) AND THA.DEL_ACCNT_ID = ALDIS.ACCNT_ID(+) AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) AND ALDIS.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTM.ACCNT_ID(+) AND MTHTM.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THA.A_ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.REQUEST_ID = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' AND TM.SERVICE_CODE IN ('UBPDP') AND TM.TRANSACTION_DATE BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' AND TM.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, ALDIS.ACCNT_NO, CLDIS.CLINT_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO, CLDSE.CLINT_NAME, ALA.ACCNT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, TM.SERVICE_CODE "
            + " UNION ALL "
            + " SELECT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALDIS.ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, ALA.ACCNT_NO AGENT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, GET_FIS_BALANCE_BY_DATE(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) AGT_BALANCE, 0 NO_OF_RG, 0 NO_OF_CASIN, 0 CASIN_AMT, 0 NO_OF_CASOUT, 0 CASOUT_AMT, 0 NO_OF_BD, 0 BD_AMT, 0 NO_OF_BP, 0 BP_AMOUNT, NVL(DECODE(TM.SERVICE_CODE, 'FM', SUM(TM.TRANSACTION_AMOUNT)),0) FM_AMT FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, ACCOUNT_RANK ARA, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID AND ALA.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.SA_ACCNT_ID = ALDSE.ACCNT_ID(+) AND ALDSE.CLINT_ID = CLDSE.CLINT_ID(+) AND THA.DEL_ACCNT_ID = ALDIS.ACCNT_ID(+) AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) AND ALDIS.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTM.ACCNT_ID(+) AND MTHTM.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THA.A_ACCNT_NO = TM.RECEPENT_PARTY(+) AND TM.SERVICE_CODE IN ('FM') AND TM.TRANSACTION_DATE BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, ALDIS.ACCNT_NO, CLDIS.CLINT_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME, ALDSE.ACCNT_NO, CLDSE.CLINT_NAME, ALA.ACCNT_NO, ARA.RANK_TITEL, CLA.VERIFIED_DATE, TM.SERVICE_CODE, GET_FIS_BALANCE_BY_DATE(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) "
            + " ) GROUP BY REGION_NAME, AREA_NAME, TM_NO, TM_NAME, TO_NO, TO_NAME, DIS_NO, DIS_NAME, CLINT_ADDRESS1, THANA_NAME, DISTRICT_NAME, DSE_NO, DSE_NAME, AGENT_NO, RANK_TITEL, VERIFIED_DATE, AGT_BALANCE ORDER BY TO_NO DESC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "TM_TO_Agt_Trx_Rpt";
            string strDate = dtpTmToAgtFromDate.DateString + " To " + dtpTmToAgtToDate.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=29 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=29 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=29 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Transaction Performance Report(TM-TO wise)('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";

            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Name</td>";

            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Rank</td>";

            strHTML = strHTML + "<td valign='middle' >Agent Verification Date</td>";

            strHTML = strHTML + "<td valign='middle' >Agent Balance</td>";
            strHTML = strHTML + "<td valign='middle' >FM Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No of Registration</td>";
            strHTML = strHTML + "<td valign='middle' >No of CashIn</td>";
            strHTML = strHTML + "<td valign='middle' >CashIn Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No of Cashout</td>";
            strHTML = strHTML + "<td valign='middle' >Cashout Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No of Bank Deposit</td>";
            strHTML = strHTML + "<td valign='middle' >Bank Deposit Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No of BIll Payment</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Payment Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Cumulative Amount</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    count = count + 1;

                    if (count == 202)
                    {
                        string str = "";
                    }

                    string strAgt = prow["AGT_NO"].ToString();

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_RANK"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["AGT_VRFY_DATE"].ToString() + "</td>";
                    
                    if (prow["AGT_BALANCE"].ToString() == "")
                    {
                        strHTML = strHTML + " <td > '" + " " + " </td>";
                    }
                    else
                    {
                        strHTML = strHTML + " <td > '" + System.Math.Round(Convert.ToDouble(prow["AGT_BALANCE"].ToString()), 2) + " </td>";
                    }

                    strHTML = strHTML + " <td > '" + prow["FM_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_RG"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_CASIN"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CASIN_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_CASOUT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CASOUT_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_BD"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BD_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_BP"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BP_AMOUNT"].ToString() + "</td>";
                    double intCumuAmt = double.Parse(prow["CASIN_AMT"].ToString()) + double.Parse(prow["CASOUT_AMT"].ToString()) + double.Parse(prow["BD_AMT"].ToString()) + double.Parse(prow["BP_AMOUNT"].ToString());
                    strHTML = strHTML + " <td > '" + intCumuAmt + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;



                }
            }
			/*
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
			*/
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TM_TO_Agt_Trx_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }

        catch (Exception ex)
        {
            
            ex.Message.ToString();
        }
    }

    #region Dis agent report
    //added by chamak 10.12.2021
    protected void btnDisAgent_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = FromDateDisAgent.Date;
            DateTime dt2 = ToDateDisAgent.Date;
            double dateCount = (dt2 - dt1).TotalDays;
            //if (dateCount >= 2)
            //{
            //    lblMsg.Text = "Maximium Date Range is 2 days";
            //    return;
            //}

            string strSql = "";
            strSql = "SELECT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, TEMP.DISTRIBUTOR_ACC_ID, TEMP.DIS_ACC_NO DISTRIBUTOR_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT,SUM(TRANSACTION_AMOUNT) DIS_CORP_COLL,SUM(DECODE(TEMP.ACCNT_RANK_ID2,'140917000000000004',TEMP.TRANSACTION_AMOUNT,0)) CORPORATE_AGENT,SUM(DECODE(TEMP.ACCNT_RANK_ID2,'160306000000000002',TEMP.TRANSACTION_AMOUNT,0)) CORPORATE_SUB_AGENT,SUM(DECODE(TEMP.ACCNT_RANK_ID2,'181219000000000002',TEMP.TRANSACTION_AMOUNT,0)) NLI_CORPORATE_AGENT,SUM(DECODE(TEMP.ACCNT_RANK_ID2,'190519000000000003',TEMP.TRANSACTION_AMOUNT,0)) PROVITA_CORPORATE_AGENT  FROM ( SELECT DISTINCT ALDIS.ACCNT_ID DISTRIBUTOR_ACC_ID,ALDIS.ACCNT_RANK_ID, THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID,ALCOR.ACCNT_RANK_ID ACCNT_RANK_ID2 FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE TMIS.SERVICE_CODE = 'FM' AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + FromDateDisAgent.DateString + "' AND '" + ToDateDisAgent.DateString + "' AND TMIS.REQUEST_PARTY||'1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002', '181219000000000002', '190519000000000003') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO  AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) ORDER BY THA.DEL_ACCNT_NO ASC) TEMP, MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR WHERE TEMP.DISTRIBUTOR_ACC_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+)  AND MA.REGION_ID = MR.REGION_ID(+) GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, TEMP.DISTRIBUTOR_ACC_ID, TEMP.DIS_ACC_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "DIS_REPORT";
            string strDate = FromDateDisAgent.DateString + " To " + ToDateDisAgent.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>DIS_REPORT('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account Id</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Corp. Coll. Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Agent Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Sub Agent Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Nli Corporate Agent Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Provita Corporate Agent Amount</td>";

            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                //string strSQL1;

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    //strSQL1 = objServiceHandler.GetCorpCollAmount(prow["DISTRIBUTOR_NO"].ToString(), dtpTrxSummFromDate.DateString, dtpTrxSummToDate.DateString);

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_ACC_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_CORP_COLL"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CORPORATE_AGENT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORPORATE_SUB_AGENT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NLI_CORPORATE_AGENT"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["PROVITA_CORPORATE_AGENT"].ToString() + " </td>";





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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "DIS_REPORT");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    protected void btnTmToAgtList_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = dtpTmToAgtFromDate.Date;
            DateTime dt2 = dtpTmToAgtToDate.Date;
            double dateCount = (dt2 - dt1).TotalDays;
            //if (dateCount > 2)
            //{
            //    lblMsg.Text = "Maximium Date Range is 3 days";
            //    return;
            //}


            string strSql = "";
            //strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
            //        + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALDIS.ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, "
            //        + " CLDIS.CLINT_ADDRESS1 DIS_ADDRESS, MTT.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, "
            //        + " ALA.ACCNT_NO AGT_NO, ARA.RANK_TITEL AGT_RANK, CAB.CAS_ACCNT_BALANCE AGT_BALANCE, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG(ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_RG, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')NO_OF_CASIN, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')CASIN_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')CASOUT_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')NO_OF_BD, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (ALA.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')BD_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_CNT (ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_BP, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_AMT (ALA.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) BP_AMOUNT "
            //        + " FROM MANAGE_REGION MR, MANAGE_AREA MA, MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, MANAGE_TERRITORY_HIERARCHY MTATM, "
            //        + " ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALDIS, ACCOUNT_RANK ARDIS, CLIENT_LIST CLDIS, "
            //        + " MANAGE_THANA MTT, MANAGE_DISTRICT MDD, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, "
            //        + " BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, ACCOUNT_RANK ARA, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, CLIENT_LIST CLTM, CLIENT_LIST CLTO "
            //        + " WHERE MR.REGION_ID = MA.REGION_ID(+) AND MA.AREA_ID = MTA.AREA_ID(+) AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) "
            //        + " AND ALTM.ACCNT_ID = MTATM.HIERARCHY_ACCNT_ID(+) AND MTATM.ACCNT_ID = ALTO.ACCNT_ID(+) AND MTATM.ACCNT_ID = MTHTO.HIERARCHY_ACCNT_ID(+) "
            //        + " AND MTHTO.ACCNT_ID = ALDIS.ACCNT_ID(+) AND ALDIS.ACCNT_RANK_ID = ARDIS.ACCNT_RANK_ID(+) AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) "
            //        + " AND CLDIS.THANA_ID = MTT.THANA_ID(+) AND MTT.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO(+) "
            //        + " AND THA.A_ACCNT_NO = ALA.ACCNT_NO(+) AND ALA.ACCNT_NO = CAL.CAS_ACC_NO(+) AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID(+) "
            //        + " AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' "
            //        + " AND CAT.ACCESS_CODE IN ('CN', 'CCT', 'SW', 'BD', 'UBP', 'UBPW' ) AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID(+) "
            //        + " AND ALA.ACCNT_RANK_ID = '120519000000000005' AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) "
            //        + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) ORDER BY ALTM.ACCNT_NO ASC ";


            //strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME , ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, "
            //        + " CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRESS, MTD.THANA_NAME DIS_THANA, "
            //        + " MDD.DISTRICT_NAME DIS_DISTRICT, TM_AG.AGENT_NO AGT_NO, TM_AG.AGT_RANK, TM_AG.AGENT_BALANCE AGT_BALANCE, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG(TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_RG,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')NO_OF_CASIN, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')CASIN_AMT,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')CASOUT_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')NO_OF_BD,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')BD_AMT,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_CNT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_BP,  "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_AMT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) BP_AMOUNT, "
            //        + " APSNG101.FUNC_AGT_FM_TRX_AMT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) FM_AMT "
            //        + " FROM (SELECT DISTINCT ALA.ACCNT_ID AGENT_ACC_ID, ALA.ACCNT_NO AGENT_NO, CAB.CAS_ACCNT_BALANCE  AGENT_BALANCE, ARA.RANK_TITEL AGT_RANK FROM ACCOUNT_LIST ALA, "
            //        + " BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, ACCOUNT_RANK ARA "
            //        + " WHERE ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID  AND ALA.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID "
            //        + " AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "') TM_AG, TEMP_HIERARCHY_LIST_ALL THA, "
            //        + " CLIENT_LIST CLD, ACCOUNT_LIST ALD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, "
            //        + " MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR "
            //        + " WHERE TM_AG.AGENT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) "
            //        + " AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)  "
            //        + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) "
            //        + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) "
            //        + " ORDER BY ALTO.ACCNT_NO DESC ";

            strSql = "  SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME , ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO,  "
                    + " CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRESS, "
                    + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, "
                    + " TM_AG.AGENT_NO AGT_NO, TM_AG.AGT_RANK, TM_AG.AGT_VRFY_DATE AGT_VRFY_DATE, TM_AG.AGENT_BALANCE AGT_BALANCE, "
                    + " '' NO_OF_RG,  "
                    + " '' NO_OF_CASIN, "
                    + " '' CASIN_AMT,  "
                    + " '' NO_OF_CASOUT, "
                    + " '' CASOUT_AMT, "
                    + " ''  NO_OF_BD,  "
                    + " '' BD_AMT,  "
                    + " '' NO_OF_BP,  "
                    + " '' BP_AMOUNT, "
                    + " '' FM_AMT "
                    + " FROM (SELECT DISTINCT ALA.ACCNT_ID AGENT_ACC_ID, ALA.ACCNT_NO AGENT_NO, CLA.VERIFIED_DATE AGT_VRFY_DATE, CAB.CAS_ACCNT_BALANCE  AGENT_BALANCE, ARA.RANK_TITEL AGT_RANK "
                    + " FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, ACCOUNT_RANK ARA "
                    + " WHERE ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID  AND ALA.ACCNT_NO = CAL.CAS_ACC_NO "
                    + " AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID  AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "') TM_AG, "
                    + " TEMP_HIERARCHY_LIST_ALL THA,  CLIENT_LIST CLD, ACCOUNT_LIST ALD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, MANAGE_TERRITORY_HIERARCHY MTHD, "
                    + " ACCOUNT_LIST ALTO, CLIENT_LIST CLTO,  MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, "
                    + " MANAGE_REGION MR, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE WHERE TM_AG.AGENT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) "
                    + " AND CLD.THANA_ID = MTD.THANA_ID(+)  AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) "
                    + " AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)   AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) "
                    + " AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+)  AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) "
                    + " AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO(+) AND ALDSE.CLINT_ID = CLDSE.CLINT_ID(+) "
                    + " ORDER BY ALTO.ACCNT_NO DESC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "TM_TO_Agt_Trx_Rpt";
            string strDate = dtpTmToAgtFromDate.DateString + " To " + dtpTmToAgtToDate.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=29 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=29 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=29 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Transaction Performance Report(TM-TO wise)('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";

            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Name</td>";

            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Rank</td>";

            strHTML = strHTML + "<td valign='middle' >Agent Verification Date</td>";

            strHTML = strHTML + "<td valign='middle' >Agent Balance</td>";
            strHTML = strHTML + "<td valign='middle' >FM Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No of Registration</td>";
            strHTML = strHTML + "<td valign='middle' >No of CashIn</td>";
            strHTML = strHTML + "<td valign='middle' >CashIn Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No of Cashout</td>";
            strHTML = strHTML + "<td valign='middle' >Cashout Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No of Bank Deposit</td>";
            strHTML = strHTML + "<td valign='middle' >Bank Deposit Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No of BIll Payment</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Payment Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Cumulative Amount</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_RANK"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["AGT_VRFY_DATE"].ToString() + "</td>";



                    if (prow["AGT_BALANCE"].ToString() == "")
                    {
                        strHTML = strHTML + " <td > '" + " " + " </td>";
                    }
                    else
                    {
                        strHTML = strHTML + " <td > '" + System.Math.Round(Convert.ToDouble(prow["AGT_BALANCE"].ToString()), 2) + " </td>";
                    }

                    strHTML = strHTML + " <td > '" + prow["FM_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_RG"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_CASIN"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CASIN_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_CASOUT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CASOUT_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_BD"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BD_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["NO_OF_BP"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BP_AMOUNT"].ToString() + "</td>";
                    int intCumuAmt = 0;
                    strHTML = strHTML + " <td > '" + intCumuAmt + "</td>";

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

            SaveAuditInfo("Preview", "TM_TO_Agt_Trx_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }




    protected void btnToRgReport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpRgMonth.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpRgYear.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }

            DateTime dt1 = dtpToRgAcvFromDate.Date;
            DateTime dt2 = dtpToRgAcvToDate.Date;
            double dateDiff = ((dt2 - dt1).TotalDays)+1;

            double noofDaysInAMonth = 0;
            if(drpRgMonth.SelectedValue == "Jan" )
            {
                noofDaysInAMonth = 31;
            }
            else if(drpRgMonth.SelectedValue == "Feb")
            {
                if (int.Parse(drpRgYear.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = 29;
                }
                else
                {
                    noofDaysInAMonth = 28;
                }
                
            }
            else if(drpRgMonth.SelectedValue == "Mar")
            {
                noofDaysInAMonth = 31;
            }
            else if(drpRgMonth.SelectedValue == "Apr")
            {
                noofDaysInAMonth = 30;
            }
            else if(drpRgMonth.SelectedValue == "May")
            {
                noofDaysInAMonth = 31;
            }
            else if(drpRgMonth.SelectedValue == "Jun")
            {
                noofDaysInAMonth = 30;
            }
            else if(drpRgMonth.SelectedValue == "Jul")
            {
                noofDaysInAMonth = 31;
            }
            else if(drpRgMonth.SelectedValue == "Aug")
            {
                noofDaysInAMonth = 31;
            }
            else if(drpRgMonth.SelectedValue == "Sep")
            {
                noofDaysInAMonth = 30;
            }
            else if(drpRgMonth.SelectedValue == "Oct")
            {
                noofDaysInAMonth = 31;
            }
            else if(drpRgMonth.SelectedValue == "Nov")
            {
                noofDaysInAMonth = 30;
            }
            else if(drpRgMonth.SelectedValue == "Dec")
            {
                noofDaysInAMonth = 31;
            }
            
            else 
            {
                // do nothing
            }

            string strSql = "";
            strSql = " SELECT DISTINCT TMP.AREA_NAME, TMP.TM_NO, TMP.TM_NAME, TMP.TO_ACC_ID, TMP.TO_NO, "
                    + " TMP.TO_NAME, MKT.TARGET_MONTH||'-'||MKT.TARGET_YEAR TARGET_MNT_YR, TRUNC(SYSDATE-1) CURR_DATE, "
                    + " MKT.CUST_ACQU_TARGET, APSNG101.FUNC_TO_RG_ACHEIVEMENT ( TMP.TO_NO, '" + dtpToRgAcvFromDate.DateString + "', '" + dtpToRgAcvToDate.DateString + "' ) REG_COUNT "
                    + " FROM (SELECT DISTINCT MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_ID TO_ACC_ID, ALTO.ACCNT_NO TO_NO, "
                    + " CLTO.CLINT_NAME TO_NAME FROM MANAGE_AREA MA, MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, "
                    + " MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO WHERE MA.AREA_ID = MTA.AREA_ID(+) "
                    + " AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) "
                    + " AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)) TMP, MANAGE_KPI_TARGET MKT "
                    + " WHERE TMP.TO_ACC_ID = MKT.TO_ACCNT_ID(+) AND MKT.TARGET_MONTH = '" + drpRgMonth.SelectedValue + "' AND MKT.TARGET_YEAR = '" + drpRgYear.SelectedValue + "' "
                    + " ORDER BY TMP.AREA_NAME ASC ";

            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "TO_Rg_Acv_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TO Monthly Registration Achievement Report("+ drpRgMonth.SelectedValue +"-"+drpRgYear.SelectedValue+" )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=left> Date Range: " + dtpToRgAcvFromDate.DateString + " to " + dtpToRgAcvToDate.DateString + " </h4></td> <td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td> </tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Target Month-Year</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Target</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Landing as per Trend</td>";
            strHTML = strHTML + "<td valign='middle' >MTD Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Pending</td>";
            strHTML = strHTML + "<td valign='middle' >Daily Require</td>";
            strHTML = strHTML + "</tr>";
            
            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TARGET_MNT_YR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_TARGET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REG_COUNT"].ToString() + " </td>";
                    double dblLandingasPerTrend = System.Math.Round(((int.Parse(prow["REG_COUNT"].ToString()) / dateDiff) * noofDaysInAMonth), 2)  ;
                    strHTML = strHTML + " <td > '" + dblLandingasPerTrend + " </td>";

                    int intRgTarget = int.Parse(prow["CUST_ACQU_TARGET"].ToString());
                    int intRgAchieveed = int.Parse(prow["REG_COUNT"].ToString());

                    double dblMtdDate = intRgTarget / noofDaysInAMonth;
                    double dblMtdTarget = dblMtdDate * dateDiff;
                    double dblMtdAchv = intRgAchieveed / dblMtdTarget;

                    double dblMtdAchievement = System.Math.Round((dblMtdAchv * 100), 2); 
                        
                    strHTML = strHTML + " <td > '" + dblMtdAchievement + "% </td>";
                    int intPending = int.Parse(prow["CUST_ACQU_TARGET"].ToString()) - int.Parse(prow["REG_COUNT"].ToString());
                    strHTML = strHTML + " <td > '" + intPending + " </td>";
                    double dblDailyRequire = Convert.ToDouble(intPending / (noofDaysInAMonth - dateDiff));
                    strHTML = strHTML + " <td > '" + System.Math.Round(dblDailyRequire,0) + " </td>";

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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TO_Rg_Acv_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnToDpsReport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpMonthMydps.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpYearMyDps.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }

            DateTime dt1 = dtpToMyDpsAcvFromDate.Date;
            DateTime dt2 = dtpToMyDpsAcvToDate.Date;
            double dateDiff = ((dt2 - dt1).TotalDays) + 1;

            double noofDaysInAMonth = 0;
            if (drpMonthMydps.SelectedValue == "Jan")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthMydps.SelectedValue == "Feb")
            {
                if (int.Parse(drpYearMyDps.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = 29;
                }
                else
                {
                    noofDaysInAMonth = 28;
                }

            }
            else if (drpMonthMydps.SelectedValue == "Mar")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthMydps.SelectedValue == "Apr")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthMydps.SelectedValue == "May")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthMydps.SelectedValue == "Jun")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthMydps.SelectedValue == "Jul")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthMydps.SelectedValue == "Aug")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthMydps.SelectedValue == "Sep")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthMydps.SelectedValue == "Oct")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthMydps.SelectedValue == "Nov")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthMydps.SelectedValue == "Dec")
            {
                noofDaysInAMonth = 31;
            }

            else
            {
                // do nothing
            }



            string strSql = "";
            strSql = " SELECT DISTINCT TMP.AREA_NAME, TMP.TM_NO, TMP.TM_NAME, TMP.TO_ACC_ID, TMP.TO_NO, TMP.TO_NAME, "
                    + " MKT.TARGET_MONTH||'-'||MKT.TARGET_YEAR TARGET_MNT_YR, TRUNC(SYSDATE-1) CURR_DATE, MKT.DPS_ACC_ACQU_TARGET DPS_TARGET, "
                    + " FUNC_TO_DPS_ACHEIVEMENT (TMP.TO_NO, '" + dtpToMyDpsAcvFromDate.DateString + "', '" + dtpToMyDpsAcvToDate.DateString + "' ) DPS_ACQ "
                    + " FROM (SELECT DISTINCT MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_ID TO_ACC_ID, ALTO.ACCNT_NO TO_NO, "
                    + " CLTO.CLINT_NAME TO_NAME FROM MANAGE_AREA MA, MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, "
                    + " MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO WHERE MA.AREA_ID = MTA.AREA_ID(+) "
                    + " AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) "
                    + " AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)) TMP, MANAGE_KPI_TARGET MKT "
                    + " WHERE TMP.TO_ACC_ID = MKT.TO_ACCNT_ID(+) AND MKT.TARGET_MONTH = '" + drpMonthMydps.SelectedValue + "' AND MKT.TARGET_YEAR = '" + drpYearMyDps.SelectedValue + "' "
                    + " ORDER BY TMP.AREA_NAME ASC ";


            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "TO_Dps_Acv_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TO Monthly MyDPS Achievement Report(" + drpMonthMydps.SelectedValue + "-" + drpYearMyDps.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=left> Date Range: " + dtpToMyDpsAcvFromDate.DateString + " to " + dtpToMyDpsAcvToDate.DateString + " </h4></td> <td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td> </tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Target Month-Year</td>";
            strHTML = strHTML + "<td valign='middle' >MyDps Target</td>";
            strHTML = strHTML + "<td valign='middle' >MyDps Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Landing as per Trend</td>";
            strHTML = strHTML + "<td valign='middle' >MTD Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Pending</td>";
            strHTML = strHTML + "<td valign='middle' >Daily Require</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TARGET_MNT_YR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_TARGET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_ACQ"].ToString() + " </td>";
                    double dblLandingasPerTrend = System.Math.Round(((int.Parse(prow["DPS_ACQ"].ToString()) / dateDiff) * noofDaysInAMonth), 2);
                    strHTML = strHTML + " <td > '" + dblLandingasPerTrend + " </td>";

                    int intRgTarget = int.Parse(prow["DPS_TARGET"].ToString());
                    int intRgAchieveed = int.Parse(prow["DPS_ACQ"].ToString());

                    double dblMtdDate = intRgTarget / noofDaysInAMonth;
                    double dblMtdTarget = dblMtdDate * dateDiff;
                    double dblMtdAchv = intRgAchieveed / dblMtdTarget;

                    double dblMtdAchievement = System.Math.Round((dblMtdAchv * 100), 2);

                    strHTML = strHTML + " <td > '" + dblMtdAchievement + "% </td>";
                    int intPending = int.Parse(prow["DPS_TARGET"].ToString()) - int.Parse(prow["DPS_ACQ"].ToString());
                    strHTML = strHTML + " <td > '" + intPending + "  </td>";
                    double dblDailyRequire = Convert.ToDouble(intPending / (noofDaysInAMonth - dateDiff));
                    strHTML = strHTML + " <td > '" + System.Math.Round(dblDailyRequire,0) + " </td>";

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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TO_Dps_Acv_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";





        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }



    protected void btnToLiftingReport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpMonthLifting.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpYearLifting.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }

            DateTime dt1 = dtpToLiftingAcvFromDate.Date;
            DateTime dt2 = dtpToLiftingAcvToDate.Date;
            double dateDiff = ((dt2 - dt1).TotalDays) + 1;

            double noofDaysInAMonth = 0;
            if (drpMonthLifting.SelectedValue == "Jan")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthLifting.SelectedValue == "Feb")
            {
                if (int.Parse(drpYearLifting.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = 29;
                }
                else
                {
                    noofDaysInAMonth = 28;
                }

            }
            else if (drpMonthLifting.SelectedValue == "Mar")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthLifting.SelectedValue == "Apr")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthLifting.SelectedValue == "May")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthLifting.SelectedValue == "Jun")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthLifting.SelectedValue == "Jul")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthLifting.SelectedValue == "Aug")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthLifting.SelectedValue == "Sep")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthLifting.SelectedValue == "Oct")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthLifting.SelectedValue == "Nov")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthLifting.SelectedValue == "Dec")
            {
                noofDaysInAMonth = 31;
            }

            else
            {
                // do nothing
            }


            string strSql = "";
            strSql = " SELECT DISTINCT TMP.AREA_NAME, TMP.TM_NO, TMP.TM_NAME, TMP.TO_ACC_ID, TMP.TO_NO, TMP.TO_NAME, "
                    + " MKT.TARGET_MONTH||'-'||MKT.TARGET_YEAR TARGET_MNT_YR, TRUNC(SYSDATE-1) CURR_DATE, "
                    + " MKT.LIFTING_AMOUNT_TARGET LIFTING_TARGET, "
                    + " APSNG101.FUNC_TO_LIFTING_ACHEIVEMENT(TMP.TO_NO, '" + dtpToLiftingAcvFromDate.DateString + "', '" + dtpToLiftingAcvToDate.DateString + "' ) LIFTING_ACHV "
                    + " FROM (SELECT DISTINCT MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_ID TO_ACC_ID, "
                    + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME FROM MANAGE_AREA MA, MANAGE_TERRITORY_AREA MTA, "
                    + " ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO "
                    + " WHERE MA.AREA_ID = MTA.AREA_ID(+) AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) "
                    + " AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+) "
                    + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)) TMP, MANAGE_KPI_TARGET MKT WHERE TMP.TO_ACC_ID = MKT.TO_ACCNT_ID(+) "
                    + " AND MKT.TARGET_MONTH = '" + drpMonthLifting.SelectedValue + "' AND MKT.TARGET_YEAR = '" + drpYearLifting.SelectedValue + "' "
                    + " ORDER BY TMP.AREA_NAME ASC ";


            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "TO_Lifting_Acv_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TO Monthly Lifting Achievement Report(" + drpMonthLifting.SelectedValue + "-" + drpYearLifting.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=left> Date Range: " + dtpToLiftingAcvFromDate.DateString + " to " + dtpToLiftingAcvToDate.DateString + " </h4></td> <td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td> </tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Target Month-Year</td>";
            strHTML = strHTML + "<td valign='middle' >Lifting Target</td>";
            strHTML = strHTML + "<td valign='middle' >Lifting Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Landing as per Trend</td>";
            strHTML = strHTML + "<td valign='middle' >MTD Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Pending</td>";
            strHTML = strHTML + "<td valign='middle' >Daily Require</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TARGET_MNT_YR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["LIFTING_TARGET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["LIFTING_ACHV"].ToString() + " </td>";
                    double dblLandingasPerTrend = System.Math.Round(((int.Parse(prow["LIFTING_ACHV"].ToString()) / dateDiff) * noofDaysInAMonth), 2);
                    strHTML = strHTML + " <td > '" + dblLandingasPerTrend + " </td>";

                    int intRgTarget = 0;
                    if(prow["LIFTING_TARGET"].ToString() == "")
                    {
                        intRgTarget = 0;
                    }
                    else
                    {
                        intRgTarget = int.Parse(prow["LIFTING_TARGET"].ToString());
                    }

                    int intRgAchieveed = 0;
                    if (prow["LIFTING_ACHV"].ToString() == "")
                    {
                        intRgAchieveed = 0;
                    }
                    else
                    { 
                        intRgAchieveed = int.Parse(prow["LIFTING_ACHV"].ToString());
                    }
                    
                    
                    double dblMtdDate = intRgTarget / noofDaysInAMonth;
                    double dblMtdTarget = dblMtdDate * dateDiff;
                    double dblMtdAchv = intRgAchieveed / dblMtdTarget;

                    double dblMtdAchievement = System.Math.Round((dblMtdAchv * 100), 2);

                    strHTML = strHTML + " <td > '" + dblMtdAchievement + "% </td>";
                    //int intPending = int.Parse(prow["LIFTING_TARGET"].ToString()) - int.Parse(prow["LIFTING_ACHV"].ToString());
                    int intPending = intRgTarget - intRgAchieveed;
                    strHTML = strHTML + " <td > '" + intPending + "  </td>";
                    double dblDailyRequire = Convert.ToDouble(intPending / (noofDaysInAMonth - dateDiff));
                    strHTML = strHTML + " <td > '" + System.Math.Round(dblDailyRequire, 0) + " </td>";

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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TO_Lifting_Acv_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";




        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnToCorpReport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpMonthCorpColl.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpYearCorpColl.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }

            DateTime dt1 = dtpToCorpAcvFromDate.Date;
            DateTime dt2 = dtpToCorpAcvToDate.Date;
            double dateDiff = ((dt2 - dt1).TotalDays) + 1;

            double noofDaysInAMonth = 0;
            if (drpMonthCorpColl.SelectedValue == "Jan")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthCorpColl.SelectedValue == "Feb")
            {
                if (int.Parse(drpYearCorpColl.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = 29;
                }
                else
                {
                    noofDaysInAMonth = 28;
                }

            }
            else if (drpMonthCorpColl.SelectedValue == "Mar")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthCorpColl.SelectedValue == "Apr")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthCorpColl.SelectedValue == "May")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthCorpColl.SelectedValue == "Jun")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthCorpColl.SelectedValue == "Jul")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthCorpColl.SelectedValue == "Aug")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthCorpColl.SelectedValue == "Sep")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthCorpColl.SelectedValue == "Oct")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthCorpColl.SelectedValue == "Nov")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthCorpColl.SelectedValue == "Dec")
            {
                noofDaysInAMonth = 31;
            }

            else
            {
                // do nothing
            }


            string strSql = "";
            strSql = " SELECT DISTINCT TMP.AREA_NAME, TMP.TM_NO, TMP.TM_NAME, TMP.TO_ACC_ID, TMP.TO_NO, TMP.TO_NAME, "
                    + " MKT.TARGET_MONTH||'-'||MKT.TARGET_YEAR TARGET_MNT_YR, TRUNC(SYSDATE-1) CURR_DATE, "
                    + " MKT.CORP_COLLECTION_TARGET CORP_COLL_TARGET, "
                    + " APSNG101.FUNC_TO_CORP_COLL_ACHVMENT ( TMP.TO_NO, '"+dtpToCorpAcvFromDate.DateString+"', '"+ dtpToCorpAcvToDate.DateString +"' ) CORP_COLL_ACHV "
                    + " FROM (SELECT DISTINCT MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
                    + " ALTO.ACCNT_ID TO_ACC_ID, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME FROM MANAGE_AREA MA, "
                    + " MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM, "
                    + " ACCOUNT_LIST ALTO, CLIENT_LIST CLTO WHERE MA.AREA_ID = MTA.AREA_ID(+) "
                    + " AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) "
                    + " AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+) "
                    + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)) TMP, MANAGE_KPI_TARGET MKT WHERE TMP.TO_ACC_ID = MKT.TO_ACCNT_ID(+) "
                    + " AND MKT.TARGET_MONTH = '"+drpMonthCorpColl.SelectedValue +"' AND MKT.TARGET_YEAR = '"+ drpYearCorpColl.SelectedValue +"' "
                    + " ORDER BY TMP.AREA_NAME ASC ";


            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "TO_Corp_Coll_Acv_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TO Monthly Corporate Collection Achievement Report(" + drpMonthCorpColl.SelectedValue + "-" + drpYearCorpColl.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=left> Date Range: " + dtpToCorpAcvFromDate.DateString + " to " + dtpToCorpAcvToDate.DateString + " </h4></td> <td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td> </tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Target Month-Year</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Collection Target</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Collection Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Landing as per Trend</td>";
            strHTML = strHTML + "<td valign='middle' >MTD Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Pending</td>";
            strHTML = strHTML + "<td valign='middle' >Daily Require</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TARGET_MNT_YR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_TARGET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_ACHV"].ToString() + " </td>";
                    double dblLandingasPerTrend = System.Math.Round(((int.Parse(prow["CORP_COLL_ACHV"].ToString()) / dateDiff) * noofDaysInAMonth), 2);
                    strHTML = strHTML + " <td > '" + dblLandingasPerTrend + " </td>";

                    int intRgTarget = 0;
                    if (prow["CORP_COLL_TARGET"].ToString() == "")
                    {
                        intRgTarget = 0;
                    }
                    else
                    {
                        intRgTarget = int.Parse(prow["CORP_COLL_TARGET"].ToString());
                    }

                    int intRgAchieveed = 0;
                    if (prow["CORP_COLL_ACHV"].ToString() == "")
                    {
                        intRgAchieveed = 0;
                    }
                    else
                    {
                        intRgAchieveed = int.Parse(prow["CORP_COLL_ACHV"].ToString());
                    }


                    double dblMtdDate = intRgTarget / noofDaysInAMonth;
                    double dblMtdTarget = dblMtdDate * dateDiff;
                    double dblMtdAchv = intRgAchieveed / dblMtdTarget;

                    double dblMtdAchievement = System.Math.Round((dblMtdAchv * 100), 2);

                    strHTML = strHTML + " <td > '" + dblMtdAchievement + "% </td>";
                    //int intPending = int.Parse(prow["LIFTING_TARGET"].ToString()) - int.Parse(prow["LIFTING_ACHV"].ToString());
                    int intPending = intRgTarget - intRgAchieveed;
                    strHTML = strHTML + " <td > '" + intPending + "  </td>";
                    double dblDailyRequire = Convert.ToDouble(intPending / (noofDaysInAMonth - dateDiff));
                    strHTML = strHTML + " <td > '" + System.Math.Round(dblDailyRequire, 0) + " </td>";

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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TO_Corp_Coll_Acv_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }    
    }

    protected void btnToAgtTrxReport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpMonthAgtTrxAmt.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpYearAgtTrxAmt.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }

            DateTime dt1 = dtpToAgtTrxAmtFromDate.Date;
            DateTime dt2 = dtpToAgtTrxAmtToDate.Date;
            double dateDiff = ((dt2 - dt1).TotalDays) + 1;

            double noofDaysInAMonth = 0;
            if (drpMonthAgtTrxAmt.SelectedValue == "Jan")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Feb")
            {
                if (int.Parse(drpYearAgtTrxAmt.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = 29;
                }
                else
                {
                    noofDaysInAMonth = 28;
                }

            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Mar")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Apr")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "May")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Jun")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Jul")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Aug")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Sep")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Oct")
            {
                noofDaysInAMonth = 31;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Nov")
            {
                noofDaysInAMonth = 30;
            }
            else if (drpMonthAgtTrxAmt.SelectedValue == "Dec")
            {
                noofDaysInAMonth = 31;
            }

            else
            {
                // do nothing
            }

            //TRX_AMT_TARGET
            //previous ACTIVE_AGENT_TRXAMT_TARGET

            string strSql = "";
            strSql = " SELECT DISTINCT TMP.AREA_NAME, TMP.TM_NO, TMP.TM_NAME, TMP.TO_ACC_ID, TMP.TO_NO, TMP.TO_NAME, "
                    + " MKT.TARGET_MONTH||'-'||MKT.TARGET_YEAR TARGET_MNT_YR, TRUNC(SYSDATE-1) CURR_DATE, MKT.TRX_AMT_TARGET AGT_TRX_TARGET, "
                    + " APSNG101.FUNC_TO_AGT_TRX_ACHVMENT (TMP.TO_NO, '"+ dtpToAgtTrxAmtFromDate.DateString +"', '"+ dtpToAgtTrxAmtToDate.DateString +"' ) AGT_TRX_AMT_ACHV  "
                    + " FROM (SELECT DISTINCT MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_ID TO_ACC_ID, ALTO.ACCNT_NO TO_NO, "
                    + " CLTO.CLINT_NAME TO_NAME FROM MANAGE_AREA MA, MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, "
                    + " MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO WHERE MA.AREA_ID = MTA.AREA_ID(+) "        //  and  ALTO.ACCNT_NO = '019177050931'      "
                    + " AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) "
                    + " AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)) TMP, MANAGE_KPI_TARGET MKT "
                    + " WHERE TMP.TO_ACC_ID = MKT.TO_ACCNT_ID(+) AND MKT.TARGET_MONTH = '"+ drpMonthAgtTrxAmt.SelectedValue +"' AND MKT.TARGET_YEAR = '"+ drpYearAgtTrxAmt.SelectedValue +"' "
                    + " ORDER BY TMP.AREA_NAME ASC " ;

            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "TO_Agt_Trx_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TO Monthly Agent Transaction Achievement Report(" + drpMonthAgtTrxAmt.SelectedValue + "-" + drpYearAgtTrxAmt.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=left> Date Range: " + dtpToAgtTrxAmtFromDate.DateString + " to " + dtpToAgtTrxAmtToDate.DateString + " </h4></td> <td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td> </tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Target Month-Year</td>";
            strHTML = strHTML + "<td valign='middle' > Transaction Target</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Transaction Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Landing as per Trend</td>";
            strHTML = strHTML + "<td valign='middle' >MTD Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Pending</td>";
            strHTML = strHTML + "<td valign='middle' >Daily Require</td>";
            strHTML = strHTML + "</tr>";

            int count = 0;

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    count = count + 1;
                    if (count == 22)
                    {
                        string str = "";
                    }

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TARGET_MNT_YR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_TRX_TARGET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_TRX_AMT_ACHV"].ToString() + " </td>";
                    double dblLandingasPerTrend = System.Math.Round(((double.Parse(prow["AGT_TRX_AMT_ACHV"].ToString()) / dateDiff) * noofDaysInAMonth), 2);
                    strHTML = strHTML + " <td > '" + dblLandingasPerTrend + " </td>";

                    int intRgTarget = int.Parse(prow["AGT_TRX_TARGET"].ToString());
                    double intRgAchieveed = double.Parse(prow["AGT_TRX_AMT_ACHV"].ToString());

                    double dblMtdDate = intRgTarget / noofDaysInAMonth;
                    double dblMtdTarget = dblMtdDate * dateDiff;
                    double dblMtdAchv = intRgAchieveed / dblMtdTarget;

                    double dblMtdAchievement = System.Math.Round((dblMtdAchv * 100), 2);

                    strHTML = strHTML + " <td > '" + dblMtdAchievement + "% </td>";
                    double intPending = double.Parse(prow["AGT_TRX_TARGET"].ToString()) - double.Parse(prow["AGT_TRX_AMT_ACHV"].ToString());
                    strHTML = strHTML + " <td > '" + intPending + " </td>";
                    double dblDailyRequire = Convert.ToDouble(intPending / (noofDaysInAMonth - dateDiff));
                    strHTML = strHTML + " <td > '" + System.Math.Round(dblDailyRequire, 0) + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    count = count + 1;
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TO_Agt_Trx_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnTmToDisHierarchy_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT TMDIS.DISTRIBUTOR_NO,CLD.CLINT_NAME DIS_NAME,ALTO.ACCNT_NO TO_NO,CLTO.CLINT_NAME TO_NAME, ALTM.ACCNT_NO TM_NO,CLTM.CLINT_NAME TM_NAME,MA.AREA_NAME, MR.REGION_NAME FROM (SELECT DISTINCT THA.DEL_ACCNT_NO DISTRIBUTOR_NO,  ALD.ACCNT_ID DIS_ACC_ID FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD   WHERE THA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.ACCNT_STATE = 'A') TMDIS,ACCOUNT_LIST ALD,CLIENT_LIST CLD,    MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO,CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO,     ACCOUNT_LIST ALTM,CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR       WHERE TMDIS.DISTRIBUTOR_NO=ALD.ACCNT_NO(+) AND ALD.CLINT_ID=CLD.CLINT_ID(+) AND TMDIS.DIS_ACC_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) AND ALTO.CLINT_ID=CLTO.CLINT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID (+)  AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND ALTM.CLINT_ID=CLTM.CLINT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY ALTO.ACCNT_NO DESC ";

            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "TM_TO_Dis_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TM-TO-Distributor Hierarchy Report</h4></td></tr>";
            strHTML = strHTML + "</table>";




            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor No </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>"; 
            strHTML = strHTML + "<td valign='middle' >TO No </td>";
            strHTML = strHTML + "<td valign='middle' >TO NAME </td>";
            strHTML = strHTML + "<td valign='middle' >TM No</td>";
            strHTML = strHTML + "<td valign='middle' >TM NAME </td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Region</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + "</td>";


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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TM_TO_Dis_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }

    //protected void btnTmToDisHierarchy_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string strSql = "";
    //        strSql = " SELECT DISTINCT TMDIS.DISTRIBUTOR_NO, ALTO.ACCNT_NO TO_NO, ALTM.ACCNT_NO TM_NO, "
    //                + " MA.AREA_NAME, MR.REGION_NAME FROM (SELECT DISTINCT THA.DEL_ACCNT_NO DISTRIBUTOR_NO, "
    //                + " ALD.ACCNT_ID DIS_ACC_ID FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD "
    //                + " WHERE THA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.ACCNT_STATE = 'A') TMDIS, "
    //                + " MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTHTO, "
    //                + " ACCOUNT_LIST ALTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR "
    //                + " WHERE TMDIS.DIS_ACC_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) "
    //                + " AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID (+) "
    //                + " AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) "
    //                + " AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY ALTO.ACCNT_NO DESC ";

    //        string strHTML = "", fileName = "";
    //        DataSet dtsAccount = new DataSet();
    //        fileName = "TM_TO_Dis_Rpt";
    //        //------------------------------------------Report File xl processing   -------------------------------------

    //        dtsAccount = objServiceHandler.ExecuteQuery(strSql);

    //        strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
    //        strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
    //        strHTML = strHTML + "</table>";
    //        strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
    //        strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
    //        strHTML = strHTML + "</table>";
    //        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
    //        strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TM-TO-Distributor Hierarchy Report</h4></td></tr>";
    //        strHTML = strHTML + "</table>";

            


    //        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
    //        strHTML = strHTML + "<tr>";

    //        strHTML = strHTML + "<td valign='middle' >Sl</td>";
    //        strHTML = strHTML + "<td valign='middle' >Distributor No </td>";
    //        strHTML = strHTML + "<td valign='middle' >TO No </td>";
    //        strHTML = strHTML + "<td valign='middle' >TM No</td>";
    //        strHTML = strHTML + "<td valign='middle' >Area</td>";
    //        strHTML = strHTML + "<td valign='middle' >Region</td>";
    //        strHTML = strHTML + "</tr>";

    //        if (dtsAccount.Tables[0].Rows.Count > 0)
    //        {
    //            int SerialNo = 1;
    //            foreach (DataRow prow in dtsAccount.Tables[0].Rows)
    //            {
    //                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
    //                strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NO"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + "</td>";
                    

    //                strHTML = strHTML + " </tr> ";
    //                SerialNo = SerialNo + 1;

    //            }
    //        }

    //        strHTML = strHTML + "<tr>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " </tr>";
    //        strHTML = strHTML + " </table>";

    //        SaveAuditInfo("Preview", "TM_TO_Dis_Rpt");
    //        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
    //        lblMsg.ForeColor = Color.White;
    //        lblMsg.Text = "Report Generated Successfully...";



    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //    }

    //}

    protected void btnTmToAgtReportDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME,  ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, "
                    + " THA.DEL_ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, THA.SA_ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, "
                    + " TEMP.AGT_NO, TEMP.REQUEST_ID,  TEMP.REQUEST_PARTY, TEMP.RECEPENT_PARTY, ARREC.RANK_TITEL RECIPIENT_RANK, TEMP.TRANSACTION_DATE, "
                    + " TEMP.TRANSACTION_AMOUNT, TEMP.SERVICE_CODE FROM (SELECT DISTINCT ALA.ACCNT_NO AGT_NO, TM.REQUEST_ID, TM.REQUEST_PARTY, "
                    + " TM.RECEPENT_PARTY, TM.TRANSACTION_DATE, TM.TRANSACTION_AMOUNT, TM.SERVICE_CODE FROM TEMP_MIS_TRANSACTIONS_REPORT TM, "
                    + " ACCOUNT_LIST ALA WHERE TM.SERVICE_CODE IN ('CN', 'CCT', 'SW', 'BD', 'FM', 'UBP', 'UBPW', 'UBPDSP') "
                    + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' "
                    + " AND ((ALA.ACCNT_NO = TM.REQUEST_PARTY||1) OR (ALA.ACCNT_NO =  TM.RECEPENT_PARTY)) AND ALA.ACCNT_RANK_ID = '120519000000000005' "
                    + " ORDER BY ALA.ACCNT_NO, TM.TRANSACTION_DATE ASC ) TEMP, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDIS, "
                    + " CLIENT_LIST CLDIS, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE, ACCOUNT_LIST ALREC, ACCOUNT_RANK ARREC, "
                    + " MANAGE_TERRITORY_HIERARCHY MTHDIS, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO, "
                    + " ACCOUNT_LIST ALTM, CLIENT_LIST CLTM WHERE TEMP.AGT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALDIS.ACCNT_NO(+) "
                    + " AND ALDIS.CLINT_ID = CLDIS.CLINT_ID(+) AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO(+) AND ALDSE.CLINT_ID = CLDSE.CLINT_ID(+) "
                    + " AND TEMP.RECEPENT_PARTY = ALREC.ACCNT_NO(+) AND ALREC.ACCNT_RANK_ID = ARREC.ACCNT_RANK_ID(+) "
                    + " AND ALDIS.ACCNT_ID = MTHDIS.ACCNT_ID(+) AND MTHDIS.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) "
                    + " AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "TM_TO_Agt_Details_Trx_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Transaction Details Report(TM-TO wise)</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpTmToAgtFromDate.DateString + "' To '" + dtpTmToAgtToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Request Party</td>";
            strHTML = strHTML + "<td valign='middle' >Receipent Party</td>";
            strHTML = strHTML + "<td valign='middle' >Receipent Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Service</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY"].ToString() + " </td>";
                    
                    if (prow["RECIPIENT_RANK"].ToString() == "Previous Year Adj")
                    {
                        strHTML = strHTML + " <td > '</td>";
                    }
                    else if (prow["RECIPIENT_RANK"].ToString() == "Utility Bill Pay")
                    {
                        strHTML = strHTML + " <td > '</td>";
                    }
                    else
                    {
                        strHTML = strHTML + " <td > '" + prow["RECIPIENT_RANK"].ToString() + "</td>";
                    }

                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_DATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + "</td>";
                    
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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TM_TO_Agt_Details_Trx_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnDis_other_Click(object sender, EventArgs e)
    {
        try
        {
            double dblDisBal = 0;
            double dblrecpBal = 0;

            string strSql = "";
            strSql = " SELECT DISTINCT ALD.ACCNT_NO DIS_NO, 'DISTRIBUTOR' DIS_RANK, CLD.CLINT_NAME DIS_NAME, "
                    + " CABD.CAS_ACCNT_BALANCE DIS_BAL,  ALR.ACCNT_NO RECP_NO, ARR.RANK_TITEL RECP_RANK, CABR.CAS_ACCNT_BALANCE REC_BAL "
                    + " FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD, CLIENT_LIST CLD, ACCOUNT_LIST ALR, ACCOUNT_RANK ARR, "
                    + " BDMIT_ERP_101.CAS_ACCOUNT_LIST CALD, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CABD, BDMIT_ERP_101.CAS_ACCOUNT_LIST CALR, "
                    + " BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CABR  WHERE TM.SERVICE_CODE = 'FM' "
                    + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisBalFromDate.DateString + "' AND '" + dtpDisBalToDate.DateString + "' "
                    + " AND TM.REQUEST_PARTY||1 = ALD.ACCNT_NO AND ALD.ACCNT_RANK_ID = '120519000000000003' AND ALD.CLINT_ID = CLD.CLINT_ID "
                    + " AND TM.RECEPENT_PARTY = ALR.ACCNT_NO AND ALR.ACCNT_RANK_ID = ARR.ACCNT_RANK_ID AND ALD.ACCNT_NO = CALD.CAS_ACC_NO "
                    + " AND CALD.CAS_ACC_ID = CABD.CAS_ACC_ID AND ALR.ACCNT_NO = CALR.CAS_ACC_NO AND CALR.CAS_ACC_ID = CABR.CAS_ACC_ID ORDER BY ALD.ACCNT_NO ASC";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Dis_Dse_bal_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>FM wise Distributor and other Rank Balance Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisBalFromDate.DateString + "' To '" + dtpDisBalToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Balance</td>";
            strHTML = strHTML + "<td valign='middle' >Reciepent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Reciepent Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Reciepent Balance</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_RANK"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    
                    dblDisBal = Convert.ToDouble(prow["DIS_BAL"].ToString());
                    dblDisBal = System.Math.Round(dblDisBal, 2);
                    strHTML = strHTML + " <td > '" + dblDisBal + "</td>";

                    strHTML = strHTML + " <td > '" + prow["RECP_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RECP_RANK"].ToString() + " </td>";

                    dblrecpBal = Convert.ToDouble(prow["REC_BAL"].ToString());
                    dblrecpBal = System.Math.Round(dblrecpBal, 2);
                    strHTML = strHTML + " <td > '" + dblrecpBal + " </td>";
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
            
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Dis_Dse_bal_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnDis_DSE_Click(object sender, EventArgs e)
    {
        try
        {
            double dblDisBal = 0;
            double dblrecpBal = 0;

            string strSql = "";
            strSql = " SELECT DISTINCT ALD.ACCNT_NO DIS_NO, 'DISTRIBUTOR' DIS_RANK, CLD.CLINT_NAME DIS_NAME, CABD.CAS_ACCNT_BALANCE DIS_BAL, "
                    + " ALDSE.ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, CABDSE.CAS_ACCNT_BALANCE DSE_BAL "
                    + " FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD, ACCOUNT_LIST ALDSE, CLIENT_LIST CLD, "
                    + " BDMIT_ERP_101.CAS_ACCOUNT_LIST CALD, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CABD, CLIENT_LIST CLDSE, "
                    + " BDMIT_ERP_101.CAS_ACCOUNT_LIST CALDSE, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CABDSE "
                    + " WHERE TM.SERVICE_CODE = 'FM' AND TM.REQUEST_PARTY||1 = ALD.ACCNT_NO AND ALD.ACCNT_RANK_ID = '120519000000000003' "
                    + " AND TM.RECEPENT_PARTY = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' "
                    + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisBalFromDate.DateString + "' AND '" + dtpDisBalToDate.DateString + "' AND ALD.CLINT_ID = CLD.CLINT_ID "
                    + " AND ALD.ACCNT_NO = CALD.CAS_ACC_NO AND CALD.CAS_ACC_ID = CABD.CAS_ACC_ID AND ALDSE.CLINT_ID = CLDSE.CLINT_ID "
                    + " AND ALDSE.ACCNT_NO = CALDSE.CAS_ACC_NO AND CALDSE.CAS_ACC_ID = CABDSE.CAS_ACC_ID ORDER BY ALD.ACCNT_NO ASC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Dis_Dse_bal_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>FM wise Distributor and DSE Balance Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisBalFromDate.DateString + "' To '" + dtpDisBalToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Balance</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Name</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Balance</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_RANK"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";

                    dblDisBal = Convert.ToDouble(prow["DIS_BAL"].ToString());
                    dblDisBal = System.Math.Round(dblDisBal, 2);
                    strHTML = strHTML + " <td > '" + dblDisBal + "</td>";

                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + "</td>";
                    dblrecpBal = Convert.ToDouble(prow["DSE_BAL"].ToString());
                    dblrecpBal = System.Math.Round(dblrecpBal, 2);
                    strHTML = strHTML + " <td > '" + dblrecpBal + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Dis_Dse_bal_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnDisTrxSumm_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = dtpTrxSummFromDate.Date;
            DateTime dt2 = dtpTrxSummToDate.Date;
            double dateCount = (dt2 - dt1).TotalDays;
            //if (dateCount >= 2)
            //{
            //    lblMsg.Text = "Maximium Date Range is 2 days";
            //    return;
            //}

            string strSql = "";
            //strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
            //        + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, TMD.DISTRIBUTOR_ACC_ID, TMD.DISTRIBUTOR_NO,  "
            //        + " TMD.DIS_NAME, TMD.DIS_ADDR, TMD.DIS_THANA, TMD.DIS_DISTRICT, SUM(VW.CN_AMOUNT) SUM_CN, "
            //        + " SUM(VW.CCT_AMOUNT) SUM_CCT, SUM(VW.RG_COUNT) SUM_RG, SUM(VW.BD_AMOUNT) SUM_BD, SUM(VW.UBP_AMOUNT) SUM_UBP, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW ( TMD.DISTRIBUTOR_NO, '" + dtpTrxSummFromDate.DateString + "', '" + dtpTrxSummToDate.DateString + "' ) DIS_LIFT_AMT, "
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_REFUND_NEW( TMD.DISTRIBUTOR_NO, '" + dtpTrxSummFromDate.DateString + "', '" + dtpTrxSummToDate.DateString + "' )  DIS_RFND_AMT, "
            //        + " APSNG101.FUNC_DISWISE_CORP_COLL_AMT ( TMD.DISTRIBUTOR_NO, '" + dtpTrxSummFromDate.DateString + "', '" + dtpTrxSummToDate.DateString + "' ) DIS_CORP_COLL "
            //        + " FROM (SELECT DISTINCT ALD.ACCNT_ID DISTRIBUTOR_ACC_ID, ALD.ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME, "
            //        + " CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT "
            //        + " FROM ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD "
            //        + " WHERE ALD.ACCNT_RANK_ID = '120519000000000003' AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) "
            //        + " AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) ) TMD, VW_DISTRIBUTION_REPORT2 VW, MANAGE_TERRITORY_HIERARCHY MTHD, "
            //        + " ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, "
            //        + " MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR WHERE TMD.DISTRIBUTOR_NO = VW.DIS_NO(+) "
            //        + " AND TMD.DISTRIBUTOR_ACC_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) "
            //        + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) "
            //        + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) "
            //        + " AND MA.REGION_ID = MR.REGION_ID(+) AND TRUNC(VW.TRANSACTION_DATE) BETWEEN '" + dtpTrxSummFromDate.DateString + "' AND '" + dtpTrxSummToDate.DateString + "' "
            //        + " GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, TMD.DISTRIBUTOR_ACC_ID, "
            //        + " TMD.DISTRIBUTOR_NO, TMD.DIS_NAME, TMD.DIS_ADDR, TMD.DIS_THANA, TMD.DIS_DISTRICT ";

            strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, TMD.DISTRIBUTOR_ACC_ID, TMD.DISTRIBUTOR_NO, TMD.DIS_NAME, TMD.DIS_ADDR, TMD.DIS_THANA, TMD.DIS_DISTRICT, SUM(VW.CN_AMOUNT) SUM_CN, SUM(VW.CCT_AMOUNT) SUM_CCT, SUM(VW.RG_COUNT) SUM_RG, SUM(VW.BD_AMOUNT) SUM_BD, 0 SUM_UBP, 0 DIS_LIFT_AMT, APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_REFUND_NEW( TMD.DISTRIBUTOR_NO, '" + dtpTrxSummFromDate.DateString + "', '" + dtpTrxSummToDate.DateString + "' )  DIS_RFND_AMT, 0 DIS_CORP_COLL FROM (SELECT DISTINCT ALD.ACCNT_ID DISTRIBUTOR_ACC_ID, ALD.ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT FROM ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE ALD.ACCNT_RANK_ID IN ('120519000000000003', '180128000000000006') AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) ) TMD, VW_DISTRIBUTION_REPORT2 VW, MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR WHERE TMD.DISTRIBUTOR_NO = VW.DIS_NO(+)  AND TMD.DISTRIBUTOR_ACC_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+)  AND MA.REGION_ID = MR.REGION_ID(+) AND TRUNC(VW.TRANSACTION_DATE) BETWEEN '" + dtpTrxSummFromDate.DateString + "' AND '" + dtpTrxSummToDate.DateString + "' GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, TMD.DISTRIBUTOR_ACC_ID, TMD.DISTRIBUTOR_NO, TMD.DIS_NAME, TMD.DIS_ADDR, TMD.DIS_THANA, TMD.DIS_DISTRICT "
            + " UNION ALL"
            + " SELECT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, TEMP.DISTRIBUTOR_ACC_ID, TEMP.DIS_ACC_NO DISTRIBUTOR_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT, 0 SUM_CN, 0 SUM_CCT, 0 SUM_RG, 0 SUM_BD, 0 SUM_UBP, 0 DIS_LIFT_AMT, 0 DIS_RFND_AMT, SUM(TEMP.TRANSACTION_AMOUNT) DIS_CORP_COLL FROM ( SELECT DISTINCT ALDIS.ACCNT_ID DISTRIBUTOR_ACC_ID, THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE TMIS.SERVICE_CODE = 'FM' AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpTrxSummFromDate.DateString + "' AND '" + dtpTrxSummToDate.DateString + "' AND TMIS.REQUEST_PARTY||'1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002', '181219000000000002', '190519000000000003') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO  AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) ORDER BY THA.DEL_ACCNT_NO ASC) TEMP, MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR WHERE TEMP.DISTRIBUTOR_ACC_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+)  AND MA.REGION_ID = MR.REGION_ID(+) GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO, CLTM.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, TEMP.DISTRIBUTOR_ACC_ID, TEMP.DIS_ACC_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT"
            + " UNION ALL"
            + " SELECT MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO TM_NO, CLT.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, THLA.DEL_ACCNT_ID DISTRIBUTOR_ACC_ID, THLA.DEL_ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, 0 SUM_CN, 0 SUM_CCT, 0 SUM_RG, 0 SUM_BD, SUM(TRANSACTION_AMOUNT) SUM_UBP, 0 DIS_LIFT_AMT, 0 DIS_RFND_AMT, 0 DIS_CORP_COLL FROM TEMP_MIS_TRANSACTIONS_REPORT TM, SERVICE_REQUEST SR, UTILITY_TRANSACTION UT, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, TEMP_HIERARCHY_LIST_ALL THLA, MANAGE_TERRITORY_HIERARCHY MTH, MANAGE_TERRITORY_HIERARCHY MTHD, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, ACCOUNT_LIST ALT, CLIENT_LIST CLT, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO WHERE SERVICE_CODE IN ('UBPW', 'UWZP', 'UBP', 'UBPDSP','UBPREB','UBPKG') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpTrxSummFromDate.DateString + "' AND '" + dtpTrxSummToDate.DateString + "' AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND CAT.CAS_TRAN_STATUS = 'A' AND TM.REQUEST_ID = SR.REQUEST_ID AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' AND THLA.A_ACCNT_NO(+) = UT.SOURCE_ACC_NO AND THLA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THLA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO, CLT.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, THLA.DEL_ACCNT_ID, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME"
			+ " UNION ALL "
            + " SELECT MR.REGION_NAME,MA.AREA_NAME,ALT.ACCNT_NO TM_NO,CLT.CLINT_NAME TM_NAME,ALTO.ACCNT_NO TO_NO,CLTO.CLINT_NAME TO_NAME,THLA.DEL_ACCNT_ID DISTRIBUTOR_ACC_ID,THLA.DEL_ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME,CLD.CLINT_ADDRESS1 DIS_ADDR,MTD.THANA_NAME DIS_THANA,MDD.DISTRICT_NAME DIS_DISTRICT,0 SUM_CN,0 SUM_CCT,0 SUM_RG,0 SUM_BD, SUM (TRANSACTION_AMOUNT) SUM_UBP,0 DIS_LIFT_AMT,0 DIS_RFND_AMT,0 DIS_CORP_COLL FROM TEMP_MIS_TRANSACTIONS_REPORT TM,SERVICE_REQUEST SR,UTILITY_TRANSACTION UT, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT,TEMP_HIERARCHY_LIST_ALL THLA,MANAGE_TERRITORY_HIERARCHY MTH,MANAGE_TERRITORY_HIERARCHY MTHD, MANAGE_TERRITORY_AREA MTA,MANAGE_AREA MA,MANAGE_REGION MR,ACCOUNT_LIST ALD,CLIENT_LIST CLD,MANAGE_THANA MTD,MANAGE_DISTRICT MDD,ACCOUNT_LIST ALT, CLIENT_LIST CLT,ACCOUNT_LIST ALTO,CLIENT_LIST CLTO WHERE SERVICE_CODE IN ('UBPDP') AND TRUNC (TM.TRANSACTION_DATE) BETWEEN '" + dtpTrxSummFromDate.DateString + "' AND '" + dtpTrxSummToDate.DateString + "' AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND CAT.CAS_TRAN_STATUS = 'A' AND TM.REQUEST_ID = SR.REQUEST_ID AND SR.REQUEST_ID = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' AND THLA.A_ACCNT_NO(+) = SUBSTR(SR.REQUEST_PARTY,4) || '1' AND THLA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THLA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO, CLT.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, THLA.DEL_ACCNT_ID, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME "
            + " UNION ALL "
            + "SELECT MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO TM_NO, CLT.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, THLA.DEL_ACCNT_ID DISTRIBUTOR_ACC_ID, THLA.DEL_ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, 0 SUM_CN, 0 SUM_CCT, 0 SUM_RG, 0 SUM_BD, SUM(TRANSACTION_AMOUNT) SUM_UBP, 0 DIS_LIFT_AMT, 0 DIS_RFND_AMT, 0 DIS_CORP_COLL FROM TEMP_MIS_TRANSACTIONS_REPORT TM, SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, TEMP_HIERARCHY_LIST_ALL THLA, MANAGE_TERRITORY_HIERARCHY MTH,  MANAGE_TERRITORY_HIERARCHY MTHD, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, ACCOUNT_LIST ALD,   CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, ACCOUNT_LIST ALT, CLIENT_LIST CLT, ACCOUNT_LIST ALTO,   CLIENT_LIST CLTO WHERE SERVICE_CODE IN ('UBPDPPM') AND TRUNC(TM.TRANSACTION_DATE)    BETWEEN '" + dtpTrxSummFromDate.DateString + "' AND '" + dtpTrxSummToDate.DateString + "'     AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND CAT.CAS_TRAN_STATUS = 'A'    AND TM.REQUEST_ID = SR.REQUEST_ID AND  THLA.A_ACCNT_NO(+) = TM.REQUEST_PARTY||'1'     AND THLA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = MTHD.ACCNT_ID(+)     AND MTHD.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+)     AND THLA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+)     AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+)     AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)     AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)  GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO, CLT.CLINT_NAME,     ALTO.ACCNT_NO, CLTO.CLINT_NAME, THLA.DEL_ACCNT_ID, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1,  MTD.THANA_NAME, MDD.DISTRICT_NAME"
            +" UNION ALL "
            + " SELECT MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO TM_NO, CLT.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, ALD.ACCNT_ID DISTRIBUTOR_ACC_ID, ALD.ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, 0 SUM_CN, 0 SUM_CCT, 0 SUM_RG, 0 SUM_BD, 0 SUM_UBP, SUM(LIFTING_AMOUNT) DIS_LIFT_AMT, 0 DIS_RFND_AMT, 0 DIS_CORP_COLL FROM (SELECT ALD.ACCNT_ID, ALD.ACCNT_NO, SUM(CAS_TRAN_AMT) LIFTING_AMOUNT FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, ACCOUNT_LIST ALD WHERE ALD.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND ALD.ACCNT_RANK_ID IN ('120519000000000003', '180128000000000006') AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpTrxSummFromDate.DateString + "' AND '" + dtpTrxSummToDate.DateString + "' AND CAT.CAS_TRAN_TYPE = 'C' AND CAT.ACCESS_CODE = 'DLF' GROUP BY ALD.ACCNT_ID, ALD.ACCNT_NO) TEMP, (SELECT DISTINCT DEL_ACCNT_ID, DEL_ACCNT_NO FROM TEMP_HIERARCHY_LIST_ALL) THLA, MANAGE_TERRITORY_HIERARCHY MTH, MANAGE_TERRITORY_HIERARCHY MTHD, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, ACCOUNT_LIST ALT, CLIENT_LIST CLT, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO WHERE TEMP.ACCNT_NO = THLA.DEL_ACCNT_NO AND THLA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THLA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO, CLT.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME,  ALD.ACCNT_ID, ALD.ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "TM_TO_Dis_Trx_Rpt";
            string strDate = dtpTrxSummFromDate.DateString + " To " + dtpTrxSummToDate.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Transaction Report(TM-TO wise)('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >No of Registration</td>";
            strHTML = strHTML + "<td valign='middle' >CashIn Amount</td>";
            strHTML = strHTML + "<td valign='middle' >CashOut Amount</td>";
            strHTML = strHTML + "<td valign='middle' >BD Amount</td>";
            strHTML = strHTML + "<td valign='middle' >UBP Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Lifting Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Refund Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Corp. Coll. Amount</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                //string strSQL1;

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    //strSQL1 = objServiceHandler.GetCorpCollAmount(prow["DISTRIBUTOR_NO"].ToString(), dtpTrxSummFromDate.DateString, dtpTrxSummToDate.DateString);

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_RG"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["SUM_CN"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_CCT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_BD"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["SUM_UBP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_LIFT_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_RFND_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_CORP_COLL"].ToString() + "</td>";

                    //strHTML = strHTML + " <td > '" + strSQL1 + "</td>";


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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TM_TO_Dis_Trx_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    #region TM_OM_REPORT BY CHAMAK 23/9/2021

    protected void btnTmOmV2Report_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = tmOmV2FrmDate.Date;
            DateTime dt2 = tmOmV2ToDate.Date;
            double dateCount = (dt2 - dt1).TotalDays;
            //if (dateCount >= 2)
            //{
            //    lblMsg.Text = "Maximium Date Range is 2 days";
            //    return;
            //}

            string strSql = "SELECT MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO TM_NO, CLT.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, THLA.DEL_ACCNT_ID DISTRIBUTOR_ACC_ID, THLA.DEL_ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT,SUM(TRANSACTION_AMOUNT) SUM_TOTAL,SUM(DECODE(SERVICE_CODE,'UBPW',TRANSACTION_AMOUNT,0))  SUM_UBPW,SUM(DECODE(SERVICE_CODE,'UWZP',TRANSACTION_AMOUNT,0))  SUM_UWZP,SUM(DECODE(SERVICE_CODE,'UBP',TRANSACTION_AMOUNT,0))   SUM_UBP,SUM(DECODE(SERVICE_CODE,'UBPDSP',TRANSACTION_AMOUNT,0))SUM_UBPDSP,SUM(DECODE(SERVICE_CODE,'UBPREB',TRANSACTION_AMOUNT,0))SUM_UBPREB,SUM(DECODE(SERVICE_CODE,'UBPKG',TRANSACTION_AMOUNT,0)) SUM_UBPKG,SUM(DECODE(SERVICE_CODE,'UBPDP',TRANSACTION_AMOUNT,0)) SUM_UBPDP,SUM(DECODE(SERVICE_CODE,'UBPDPPM',TRANSACTION_AMOUNT,0)) SUM_UBPDPPM FROM TEMP_MIS_TRANSACTIONS_REPORT TM, SERVICE_REQUEST SR, UTILITY_TRANSACTION UT, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, TEMP_HIERARCHY_LIST_ALL THLA, MANAGE_TERRITORY_HIERARCHY MTH, MANAGE_TERRITORY_HIERARCHY MTHD, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, ACCOUNT_LIST ALT, CLIENT_LIST CLT, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO WHERE SERVICE_CODE IN ('UBPW', 'UWZP', 'UBP', 'UBPDSP','UBPREB','UBPKG') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + tmOmV2FrmDate.DateString + "' AND '" + tmOmV2ToDate.DateString + "' AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND CAT.CAS_TRAN_STATUS = 'A' AND TM.REQUEST_ID = SR.REQUEST_ID AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' AND THLA.A_ACCNT_NO(+) = UT.SOURCE_ACC_NO AND THLA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THLA.DEL_ACCNT_ID = ALD.ACCNT_ID(+)  AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO, CLT.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, THLA.DEL_ACCNT_ID, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME  UNION ALL(SELECT MR.REGION_NAME,MA.AREA_NAME,ALT.ACCNT_NO TM_NO,CLT.CLINT_NAME TM_NAME,ALTO.ACCNT_NO TO_NO,CLTO.CLINT_NAME TO_NAME,THLA.DEL_ACCNT_ID DISTRIBUTOR_ACC_ID,THLA.DEL_ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME,CLD.CLINT_ADDRESS1 DIS_ADDR,MTD.THANA_NAME DIS_THANA,MDD.DISTRICT_NAME DIS_DISTRICT,SUM(TRANSACTION_AMOUNT) SUM_TOTAL,SUM(DECODE(SERVICE_CODE,'UBPW',TRANSACTION_AMOUNT,0))  SUM_UBPW,SUM(DECODE(SERVICE_CODE,'UWZP',TRANSACTION_AMOUNT,0))  SUM_UWZP,SUM(DECODE(SERVICE_CODE,'UBP',TRANSACTION_AMOUNT,0))   SUM_UBP,SUM(DECODE(SERVICE_CODE,'UBPDSP',TRANSACTION_AMOUNT,0))SUM_UBPDSP,SUM(DECODE(SERVICE_CODE,'UBPREB',TRANSACTION_AMOUNT,0))SUM_UBPREB,SUM(DECODE(SERVICE_CODE,'UBPKG',TRANSACTION_AMOUNT,0)) SUM_UBPKG,SUM(DECODE(SERVICE_CODE,'UBPDP',TRANSACTION_AMOUNT,0)) SUM_UBPDP,SUM(DECODE(SERVICE_CODE,'UBPDPPM',TRANSACTION_AMOUNT,0)) SUM_UBPDPPM FROM TEMP_MIS_TRANSACTIONS_REPORT TM,SERVICE_REQUEST SR,UTILITY_TRANSACTION UT, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT,TEMP_HIERARCHY_LIST_ALL THLA,MANAGE_TERRITORY_HIERARCHY MTH,MANAGE_TERRITORY_HIERARCHY MTHD, MANAGE_TERRITORY_AREA MTA,MANAGE_AREA MA,MANAGE_REGION MR,ACCOUNT_LIST ALD,CLIENT_LIST CLD,MANAGE_THANA MTD,MANAGE_DISTRICT MDD,ACCOUNT_LIST ALT, CLIENT_LIST CLT,ACCOUNT_LIST ALTO,CLIENT_LIST CLTO WHERE SERVICE_CODE IN ('UBPDP') AND TRUNC (TM.TRANSACTION_DATE) BETWEEN '" + tmOmV2FrmDate.DateString + "' AND '" + tmOmV2ToDate.DateString + "' AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND CAT.CAS_TRAN_STATUS = 'A' AND TM.REQUEST_ID = SR.REQUEST_ID AND SR.REQUEST_ID = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' AND THLA.A_ACCNT_NO(+) = SUBSTR(SR.REQUEST_PARTY,4) || '1' AND THLA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) AND THLA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO, CLT.CLINT_NAME, ALTO.ACCNT_NO, CLTO.CLINT_NAME, THLA.DEL_ACCNT_ID, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME) UNION ALL SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO TM_NO, CLT.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, THLA.DEL_ACCNT_ID DISTRIBUTOR_ACC_ID, THLA.DEL_ACCNT_NO DISTRIBUTOR_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA,MDD.DISTRICT_NAME DIS_DISTRICT, SUM(TRANSACTION_AMOUNT) SUM_TOTAL,SUM(DECODE(SERVICE_CODE,'UBPW',TRANSACTION_AMOUNT,0))  SUM_UBPW,SUM(DECODE(SERVICE_CODE,'UWZP',TRANSACTION_AMOUNT,0))  SUM_UWZP,SUM(DECODE(SERVICE_CODE,'UBP',TRANSACTION_AMOUNT,0))   SUM_UBP,SUM(DECODE(SERVICE_CODE,'UBPDSP',TRANSACTION_AMOUNT,0))SUM_UBPDSP,SUM(DECODE(SERVICE_CODE,'UBPREB',TRANSACTION_AMOUNT,0))SUM_UBPREB,SUM(DECODE(SERVICE_CODE,'UBPKG',TRANSACTION_AMOUNT,0)) SUM_UBPKG,SUM(DECODE(SERVICE_CODE,'UBPDP',TRANSACTION_AMOUNT,0)) SUM_UBPDP,SUM(DECODE(SERVICE_CODE,'UBPDPPM',TRANSACTION_AMOUNT,0)) SUM_UBPDPPM FROM TEMP_MIS_TRANSACTIONS_REPORT TM, SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, TEMP_HIERARCHY_LIST_ALL THLA, MANAGE_TERRITORY_HIERARCHY MTH,  MANAGE_TERRITORY_HIERARCHY MTHD, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR, ACCOUNT_LIST ALD,   CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, ACCOUNT_LIST ALT, CLIENT_LIST CLT, ACCOUNT_LIST ALTO,   CLIENT_LIST CLTO WHERE SERVICE_CODE IN ('UBPDPPM') AND TRUNC(TM.TRANSACTION_DATE)    BETWEEN '" + tmOmV2FrmDate.DateString + "' AND '" + tmOmV2ToDate.DateString + "'     AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND CAT.CAS_TRAN_STATUS = 'A'    AND TM.REQUEST_ID = SR.REQUEST_ID AND  THLA.A_ACCNT_NO(+) = TM.REQUEST_PARTY||'1'     AND THLA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = MTHD.ACCNT_ID(+)     AND MTHD.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+)     AND THLA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+)     AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+)     AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)     AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)  GROUP BY MR.REGION_NAME, MA.AREA_NAME, ALT.ACCNT_NO, CLT.CLINT_NAME,     ALTO.ACCNT_NO, CLTO.CLINT_NAME, THLA.DEL_ACCNT_ID, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1,  MTD.THANA_NAME, MDD.DISTRICT_NAME";
            

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "TM_OM_BILL_PAYMENT_REPORT";
            string strDate = dtpTrxSummFromDate.DateString + " To " + dtpTrxSummToDate.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=22 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=22 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TM OM BILL PAYMENT REPORT ('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Acc No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Wasa</td>";
            strHTML = strHTML + "<td valign='middle' >WestZone</td>";
            strHTML = strHTML + "<td valign='middle' >UBP</td>";
            strHTML = strHTML + "<td valign='middle' >Desco Prepaid</td>";
            strHTML = strHTML + "<td valign='middle' >REB</td>";
            strHTML = strHTML + "<td valign='middle' >KGDCL</td>";
            strHTML = strHTML + "<td valign='middle' >Dpdc Prepaid</td>";
            strHTML = strHTML + "<td valign='middle' >Middleware</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                //string strSQL1;

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    //strSQL1 = objServiceHandler.GetCorpCollAmount(prow["DISTRIBUTOR_NO"].ToString(), dtpTrxSummFromDate.DateString, dtpTrxSummToDate.DateString);

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_ACC_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_TOTAL"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["SUM_UBPW"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_UWZP"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_UBP"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["SUM_UBPDSP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_UBPREB"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_UBPKG"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_UBPDP"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["SUM_UBPDPPM"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > '" + strSQL1 + "</td>";


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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "TM_OM_BILL_PAYMENT_REPORT_V2");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    protected void btnDisDSEBal_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpRank.SelectedValue == "00")
            {
                lblMsg.Text = "Select Rank";
                return;
            }

            // for distriubutor
            else if (drpRank.SelectedValue == "DIS")
            {
                string strSql = "";
                strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
                        + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, TM.DIS_NO, TM.DIS_NAME, TM.DIS_ADDRESS, TM.DIS_THANA, "
                        + " TM.DIS_DISTRICT, TM.DIS_RANK, TM.DIS_CREATION_DATE, TM.ACCNT_STATE, TM.DIS_BAL FROM  "
                        + " (SELECT DISTINCT AB.ACCNT_NO DIS_NO, AB.CLINT_NAME DIS_NAME, AB.CLINT_ADDRESS1 DIS_ADDRESS, "
                        + " AB.THANA_NAME DIS_THANA, AB.DISTRICT_NAME DIS_DISTRICT, AB.RANK_TITEL DIS_RANK, "
                        + " AB.CREATION_DATE DIS_CREATION_DATE, AB.ACCNT_STATE, AB.BALANCE DIS_BAL FROM VW_ALL_ACCCOUNT_BALANCE AB, "
                        + " THIRD_PARTY_AGENT_LIST TPA WHERE RANK_TITEL='MBL Distributor'  AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT) TM, "
                        + " ACCOUNT_LIST ALD, MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, "
                        + " MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, "
                        + " MANAGE_AREA MA, MANAGE_REGION MR WHERE TM.DIS_NO = ALD.ACCNT_NO(+) AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) "
                        + " AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) "
                        + " AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) "
                        + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) "
                        + " AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY TM.DIS_NO ASC";


                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "DisBal_Rpt";

                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Balance Report(TM-TO wise)</h2></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Region Name</td>";
                strHTML = strHTML + "<td valign='middle' >Area Name </td>";
                strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
                strHTML = strHTML + "<td valign='middle' >TM Name</td>";
                strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
                strHTML = strHTML + "<td valign='middle' >TO Name</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
                strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
                strHTML = strHTML + "<td valign='middle' >Disributor Rank</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Reg. Date</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Account State</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Balance</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ADDRESS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_RANK"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_CREATION_DATE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ACCNT_STATE"].ToString() + "</td>";
                        double dblBalance = Convert.ToDouble(prow["DIS_BAL"].ToString());
                        dblBalance = System.Math.Round(dblBalance, 2);
                        strHTML = strHTML + " <td > '" + dblBalance + "</td>";


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
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "DisBal_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";

            }
            // for dse
            else if (drpRank.SelectedValue == "DSE")
            {
                string strSql = "";
                strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
                        + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, "
                        + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, TM.DSE_NO, TM.DSE_NAME, TM.DSE_ADDRESS, "
                        + " TM.DSE_THANA, TM.DSE_DISTRICT, TM.DSE_CREATE_DATE, TM.DSE_RANK, TM.ACCNT_STATE, TM.BALANCE DSE_BALANCE FROM "
                        + " ( SELECT DISTINCT AB.ACCNT_NO DSE_NO, AB.CLINT_NAME DSE_NAME, AB.CLINT_ADDRESS1 DSE_ADDRESS, "
                        + " AB.THANA_NAME DSE_THANA, AB.DISTRICT_NAME DSE_DISTRICT, AB.CREATION_DATE DSE_CREATE_DATE, "
                        + " AB.RANK_TITEL DSE_RANK, AB.ACCNT_STATE, AB.BALANCE FROM VW_ALL_ACCCOUNT_BALANCE AB,THIRD_PARTY_AGENT_LIST TPA "
                        + " WHERE RANK_TITEL='MBL DSE' AND TPA.THIRD_PARTY_AGENT_ID=AB.UISC_AGENT AND AB.ACCNT_STATE IN ('A' ) ) TM, "
                        + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, "
                        + " MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO, "
                        + " ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR "
                        + " WHERE TM.DSE_NO = THA.SA_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) "
                        + " AND CLD.THANA_ID = MTD.THANA_ID (+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) "
                        + " AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) "
                        + " AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) "
                        + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) "
                        + " AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY TM.DSE_NO ";


                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "DSEBal_Rpt";

                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>DSE Balance Report(TM-TO wise)</h2></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Region Name</td>";
                strHTML = strHTML + "<td valign='middle' >Area Name </td>";
                strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
                strHTML = strHTML + "<td valign='middle' >TM Name</td>";
                strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
                strHTML = strHTML + "<td valign='middle' >TO Name</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
                strHTML = strHTML + "<td valign='middle' >Distributor District</td>";


                strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
                strHTML = strHTML + "<td valign='middle' >DSE Name</td>";
                strHTML = strHTML + "<td valign='middle' >DSE Address</td>";
                strHTML = strHTML + "<td valign='middle' >DSE Thana</td>";

                strHTML = strHTML + "<td valign='middle' >DSE District</td>";
                strHTML = strHTML + "<td valign='middle' >DSE Reg. Date </td>";
                strHTML = strHTML + "<td valign='middle' >DSE Rank</td>";
                strHTML = strHTML + "<td valign='middle' >DSE State</td>";
                strHTML = strHTML + "<td valign='middle' >DSE Balance</td>";


                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_ADDRESS"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_THANA"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_CREATE_DATE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_RANK"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ACCNT_STATE"].ToString() + "</td>";
                        double dblBalance = Convert.ToDouble(prow["DSE_BALANCE"].ToString());
                        dblBalance = System.Math.Round(dblBalance, 2);
                        strHTML = strHTML + " <td > '" + dblBalance + "</td>";


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
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "DSEBal_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";



            }
            else
            {
                // do nothing
            }

        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnBrnchCnCct_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT TM.REQUEST_ID, TM.TRANSACTION_DATE, TM.SERVICE_CODE, TM.TRANSACTION_AMOUNT, "
                    + " TM.REQUEST_PARTY, TM.RECEPENT_PARTY, CL.CLINT_NAME, TM.AGENT_COMMISSION FROM "
                    + " TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL, CLIENT_LIST CL "
                    + " WHERE ((TM.REQUEST_PARTY||1 = AL.ACCNT_NO) OR (TM.RECEPENT_PARTY = AL.ACCNT_NO)) AND AL.ACCNT_RANK_ID = '120519000000000002' "
                    + " AND TM.SERVICE_CODE IN ('CN', 'CCT') AND TRUNC(TM.TRANSACTION_DATE) "
                    + " BETWEEN '" + dtpBrnchCnCctFromDate.DateString + "' AND '" + dtpBrnchCnCctToDate.DateString + "' "
                    + " AND AL.CLINT_ID = CL.CLINT_ID ORDER BY TM.TRANSACTION_DATE ASC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";
            double dblCommission = 0;
            DataSet dtsAccount = new DataSet();
            fileName = "Branch_Trx_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Branch wise Transaction Report </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBrnchCnCctFromDate.DateString + "' To '" + dtpBrnchCnCctToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Branch Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Branch Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Service Code </td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date </td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Commission</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_COMMISSION"].ToString() + " </td>";

                    dblCommission = dblCommission + Convert.ToDouble(prow["AGENT_COMMISSION"].ToString());
                    
                    //strHTML = strHTML + " <td > '" + prow["VERIFIED"].ToString() + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            dblCommission = System.Math.Round(dblCommission, 2);
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + dblCommission + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Branch_Trx_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnDpsPending_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
                    + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, "
                    + " MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, THA.A_ACCNT_NO AGT_NO, "
                    + " CLA.CLINT_NAME AGT_NAME, TM.CUST_MOBILE_NO, TM.DPS_ACC, TM.CUST_NAME, TM.DPS_OPEN_DATE, "
                    + " TM.DPS_DEPO_STATUS, TM.PAYMENT_STATUS, TM.DPS_PAY_DATE, TM.DPS_INSTALL_AMT, TM.REF_AGT_WALLET "
                    + " FROM ( SELECT FS.MOBILE_NO CUST_MOBILE_NO, FD.DEPO_ACC_ID DPS_ACC, CL.CLINT_NAME CUST_NAME,  "
                    + " TRUNC (FD.OPEN_DATE) DPS_OPEN_DATE, FD.DEPO_STATUS DPS_DEPO_STATUS, FDS.PAYMENT_STAT PAYMENT_STATUS, "
                    + " FDS.PAYMENT_DAY DPS_PAY_DATE, FDS.DPS_AMT DPS_INSTALL_AMT, FD.FAVOUR REF_AGT_WALLET "
                    + " FROM BDMIT_ERP_101.FN_DEPO_SCHEDULE FDS, BDMIT_ERP_101.FN_DEPOSIT FD, BDMIT_ERP_101.FN_SOURCE FS, "
                    + " APSNG101.ACCOUNT_LIST AL, APSNG101.CLIENT_LIST CL WHERE FS.FN_SOURCE_ID = FD.FN_SOURCE_ID "
                    + " AND FD.FN_DEPO_ACC_ID = FDS.FN_DEPO_ACC_ID AND FS.MOBILE_NO || 1 = AL.ACCNT_NO  "
                    + " AND AL.CLINT_ID = CL.CLINT_ID AND TRUNC (FDS.PAYDATE) BETWEEN '" + dtpPendingFromDate.DateString + "' AND '" + dtpPendingToDate.DateString + "' "
                    + " AND FDS.PAYMENT_STAT = 'N' AND FDS.PAYMENT_DAY IS NULL ORDER BY FS.MOBILE_NO ASC ) TM, "
                    + " TEMP_HIERARCHY_LIST_ALL THA, APSNG101.ACCOUNT_LIST ALA, APSNG101.CLIENT_LIST CLA, "
                    + " APSNG101.ACCOUNT_LIST ALD, APSNG101.CLIENT_LIST CLD, APSNG101.MANAGE_TERRITORY_HIERARCHY MTHD, "
                    + " APSNG101.ACCOUNT_LIST ALTO, APSNG101.CLIENT_LIST CLTO, APSNG101.MANAGE_TERRITORY_HIERARCHY MTHTO, "
                    + " APSNG101.ACCOUNT_LIST ALTM, APSNG101.CLIENT_LIST CLTM, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, "
                    + " MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR WHERE TM.REF_AGT_WALLET = THA.A_ACCNT_NO(+) "
                    + " AND THA.A_ACCNT_NO = ALA.ACCNT_NO(+) AND ALA.CLINT_ID = CLA.CLINT_ID(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) "
                    + " AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) "
                    + " AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) "
                    + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) "
                    + " AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) "
                    + " AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "DPSPending_Trx_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=21 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=21 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=21 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TM-TO wise DPS Pending Status Report </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=21 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpPendingFromDate.DateString + "' To '" + dtpPendingToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name</td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Mobile</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Name</td>";
            strHTML = strHTML + "<td valign='middle' >DPS Account</td>";
            strHTML = strHTML + "<td valign='middle' >DPS Open Date</td>";
            strHTML = strHTML + "<td valign='middle' >Depositor Status</td>";
            strHTML = strHTML + "<td valign='middle' >Payment Status</td>";
            strHTML = strHTML + "<td valign='middle' >DPS Payment Date </td>";
            strHTML = strHTML + "<td valign='middle' >Installment Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";


                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_MOBILE_NO"].ToString() + " </td>";


                    strHTML = strHTML + " <td > '" + prow["CUST_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_ACC"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["DPS_OPEN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["DPS_OPEN_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_DEPO_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["PAYMENT_STATUS"].ToString() + " </td>";


                    strHTML = strHTML + " <td > '" + prow["DPS_PAY_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_INSTALL_AMT"].ToString() + " </td>";

                    //strHTML = strHTML + " <td > '" + prow["TRANSACTION_DATE"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["AGENT_COMMISSION"].ToString() + " </td>";

                    //strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ACTIVATION_DATE"].ToString())) + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["VERIFIED"].ToString() + "</td>";

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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "DPSPending_Trx_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }


    protected void btnDpsBalance_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
                    + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, "
                    + " MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, THA.A_ACCNT_NO AGT_NO,  CLA.CLINT_NAME AGT_NAME, "
                    + " TM.CUST_MOBILE_NO, TM.DPS_ACC, TM.CUST_NAME, TM.DPS_OPEN_DATE,  TM.DPS_DEPO_STATUS, TM.DPS_INSTALL_AMT, "
                    + " TM.REF_AGT_WALLET  FROM ( SELECT DISTINCT FS.MOBILE_NO CUST_MOBILE_NO, FD.DEPO_ACC_ID DPS_ACC, "
                    + " CL.CLINT_NAME CUST_NAME,   TRUNC (FD.OPEN_DATE) DPS_OPEN_DATE, FD.DEPO_STATUS DPS_DEPO_STATUS, "
                    + " FDS.DPS_AMT DPS_INSTALL_AMT, FD.FAVOUR REF_AGT_WALLET FROM BDMIT_ERP_101.FN_DEPO_SCHEDULE FDS, "
                    + " BDMIT_ERP_101.FN_DEPOSIT FD, BDMIT_ERP_101.FN_SOURCE FS, APSNG101.ACCOUNT_LIST AL, APSNG101.CLIENT_LIST CL  "
                    + " WHERE FS.FN_SOURCE_ID = FD.FN_SOURCE_ID  AND FD.FN_DEPO_ACC_ID = FDS.FN_DEPO_ACC_ID "
                    + " AND FS.MOBILE_NO || 1 = AL.ACCNT_NO   AND AL.CLINT_ID = CL.CLINT_ID "
                    + " AND TRUNC (FD.OPEN_DATE) BETWEEN '" + dtpDpsBalFromDate.DateString + "' AND '" + dtpDpsBalToDate.DateString + "' "
                    + " AND FDS.PAYMENT_DAY IS NULL ORDER BY FS.MOBILE_NO ASC ) TM,  TEMP_HIERARCHY_LIST_ALL THA, "
                    + " APSNG101.ACCOUNT_LIST ALA, APSNG101.CLIENT_LIST CLA,  APSNG101.ACCOUNT_LIST ALD, APSNG101.CLIENT_LIST CLD, "
                    + " APSNG101.MANAGE_TERRITORY_HIERARCHY MTHD,  APSNG101.ACCOUNT_LIST ALTO, APSNG101.CLIENT_LIST CLTO, "
                    + " APSNG101.MANAGE_TERRITORY_HIERARCHY MTHTO,  APSNG101.ACCOUNT_LIST ALTM, APSNG101.CLIENT_LIST CLTM, "
                    + " MANAGE_THANA MTD, MANAGE_DISTRICT MDD,  MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR "
                    + " WHERE TM.REF_AGT_WALLET = THA.A_ACCNT_NO(+) AND THA.A_ACCNT_NO = ALA.ACCNT_NO(+) AND ALA.CLINT_ID = CLA.CLINT_ID(+) "
                    + " AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) "
                    + " AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) "
                    + " AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) "
                    + " AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+)  AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) "
                    + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) "
                    + " AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY TM.DPS_OPEN_DATE ASC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "DpsBal_Rpt";

            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>TM-TO and Opening Date wise DPS Balance Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDpsBalFromDate.DateString + "' To '" + dtpDpsBalToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";

            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";


            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Reference Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            
            strHTML = strHTML + "<td valign='middle' >Customer Mobile No</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer DPS Account</td>";
            
            strHTML = strHTML + "<td valign='middle' >DPS Opening Date</td>";
            strHTML = strHTML + "<td valign='middle' >DPS Account Status</td>";
            strHTML = strHTML + "<td valign='middle' >DPS Installment Amount</td>";


            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CUST_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_ACC"].ToString() + "</td>";

                    //strHTML = strHTML + " <td > '" + prow["DPS_OPEN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["DPS_OPEN_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_DEPO_STATUS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_INSTALL_AMT"].ToString() + "</td>";                    
                    
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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "DpsBal_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnDpdcDsUssd_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT ALD.ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, "
                    + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, COUNT( TEMP.REQUEST_ID) UBP_COUNT, "
                    + " SUM(TEMP.TOTAL_BILL_AMOUNT) UBP_AMOUNT, SUM(TEMP.THIRDPARTY_COM_AMOUNT) UBP_DIS_COMM "
                    + " FROM  ( SELECT DISTINCT THA.DEL_ACCNT_NO, TMIS.THIRDPARTY_COM_AMOUNT, TM.SOURCE_ACC_NO, "
                    + " TM.RANK_TITEL, TM.CLINT_NAME, TM.CLINT_ADDRESS1, TM.PAYER_MOBILE_NO, TM.LOCATION_ID, "
                    + " TM.ACCOUNT_NUMBER, TM.BILL_NUMBER, TM.TOTAL_DPDC_AMOUNT, TM.VAT_AMOUNT, TM.BILL_MONTH, "
                    + " TM.BILL_YEAR,  TM.SERVICE, TM.REQUEST_ID, TM.OWNER_CODE,  TM.TRANSA_DATE, TM.TOTAL_BILL_AMOUNT, "
                    + " TM.NET_DPDC_AMOUNT, TM.RESPONSE_MSG_BP, TM.RESPONSE_STATUS_BP, TM.REQUEST_PARTY_TYPE "
                    + " FROM ( SELECT DISTINCT UT.SOURCE_ACC_NO, AR.RANK_TITEL, CL.CLINT_NAME, CL.CLINT_ADDRESS1, "
                    + " UT.PAYER_MOBILE_NO, UT.LOCATION_ID, UT.ACCOUNT_NUMBER, BILL_NUMBER, UT.TOTAL_DPDC_AMOUNT, "
                    + " UT.VAT_AMOUNT, UT.BILL_MONTH, UT.BILL_YEAR,  UT.SERVICE, CAT.REQUEST_ID, UT.OWNER_CODE, "
                    + " UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT, UT.NET_DPDC_AMOUNT, UT.RESPONSE_MSG_BP, "
                    + " UT.RESPONSE_STATUS_BP, UT.REQUEST_PARTY_TYPE FROM APSNG101.UTILITY_TRANSACTION UT, "
                    + " APSNG101.SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, ACCOUNT_LIST AL, "
                    + " ACCOUNT_RANK AR,CLIENT_LIST CL  WHERE UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
                    + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND UT.SERVICE IN ('UBP') "
                    + " AND CAT.CAS_TRAN_STATUS = 'A' AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpBpDisCommFromDate.DateString + "' AND '" + dtpBpDisCommToDate.DateString + "' "
                    + " AND UT.SOURCE_ACC_NO = AL.ACCNT_NO(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) "
                    + " AND AL.CLINT_ID = CL.CLINT_ID ) TM, TEMP_MIS_TRANSACTIONS_REPORT TMIS, TEMP_HIERARCHY_LIST_ALL THA "
                    + " WHERE TM.REQUEST_PARTY_TYPE NOT IN ('MCOM_GATEWAY', 'WAP') AND TM.SERVICE IN ('UBP') "
                    + " AND TM.OWNER_CODE IN ('DS', 'DPDC') AND TM.REQUEST_ID = TMIS.REQUEST_ID AND TM.SOURCE_ACC_NO = THA.A_ACCNT_NO ) TEMP, "
                    + " ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE TEMP.DEL_ACCNT_NO = ALD.ACCNT_NO(+) "
                    + " AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) "
                    + " GROUP BY ALD.ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Dis_USSD_Comm_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Commission (DPDC/DESCO through USSD) Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBpDisCommFromDate.DateString + "' To '" + dtpBpDisCommToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Total Billpay Count </td>";
            strHTML = strHTML + "<td valign='middle' >Total Billpay Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_DIS_COMM"].ToString() + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Dis_USSD_Comm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnWSAppsWap_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT ALD.ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, "
                    + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, COUNT( TEMP.REQUEST_ID) UBP_COUNT, "
                    + " SUM(TEMP.TOTAL_BILL_AMOUNT) UBP_AMOUNT, SUM(TEMP.THIRDPARTY_COM_AMOUNT) UBP_DIS_COMM "
                    + " FROM  ( SELECT DISTINCT THA.DEL_ACCNT_NO, TMIS.THIRDPARTY_COM_AMOUNT, TM.SOURCE_ACC_NO, "
                    + " TM.RANK_TITEL, TM.CLINT_NAME, TM.CLINT_ADDRESS1, TM.PAYER_MOBILE_NO, TM.LOCATION_ID, "
                    + " TM.ACCOUNT_NUMBER, TM.BILL_NUMBER, TM.TOTAL_DPDC_AMOUNT, TM.VAT_AMOUNT, TM.BILL_MONTH, "
                    + " TM.BILL_YEAR,  TM.SERVICE, TM.REQUEST_ID, TM.OWNER_CODE,  TM.TRANSA_DATE, TM.TOTAL_BILL_AMOUNT, "
                    + " TM.NET_DPDC_AMOUNT, TM.RESPONSE_MSG_BP, TM.RESPONSE_STATUS_BP, TM.REQUEST_PARTY_TYPE FROM "
                    + " ( SELECT DISTINCT UT.SOURCE_ACC_NO, AR.RANK_TITEL, CL.CLINT_NAME, CL.CLINT_ADDRESS1, "
                    + " UT.PAYER_MOBILE_NO, UT.LOCATION_ID, UT.ACCOUNT_NUMBER, BILL_NUMBER, UT.TOTAL_DPDC_AMOUNT, "
                    + " UT.VAT_AMOUNT, UT.BILL_MONTH, UT.BILL_YEAR,  UT.SERVICE, CAT.REQUEST_ID, UT.OWNER_CODE,  UT.TRANSA_DATE, "
                    + " UT.TOTAL_BILL_AMOUNT, UT.NET_DPDC_AMOUNT, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.REQUEST_PARTY_TYPE "
                    + " FROM APSNG101.UTILITY_TRANSACTION UT,  APSNG101.SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, "
                    + " ACCOUNT_LIST AL, ACCOUNT_RANK AR,CLIENT_LIST CL  WHERE UT.RESPONSE_STATUS_BP = '000' "
                    + " AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' "
                    + " AND UT.SERVICE IN ('UBPW') AND CAT.CAS_TRAN_STATUS = 'A' AND TRUNC(UT.TRANSA_DATE) "
                    + " BETWEEN '" + dtpBpDisCommFromDate.DateString + "' AND '" + dtpBpDisCommToDate.DateString + "' AND UT.SOURCE_ACC_NO = AL.ACCNT_NO(+) "
                    + " AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) AND AL.CLINT_ID = CL.CLINT_ID ) TM, TEMP_MIS_TRANSACTIONS_REPORT TMIS, "
                    + " TEMP_HIERARCHY_LIST_ALL THA WHERE TM.REQUEST_PARTY_TYPE IN ('MCOM_GATEWAY', 'WAP') AND TM.SERVICE IN ('UBPW') "
                    + " AND TM.OWNER_CODE IN ('WS') AND TM.REQUEST_ID = TMIS.REQUEST_ID AND TM.SOURCE_ACC_NO = THA.A_ACCNT_NO ) TEMP, "
                    + " ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE TEMP.DEL_ACCNT_NO = ALD.ACCNT_NO(+) "
                    + " AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) "
                    + " GROUP BY ALD.ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Dis_USSD_Comm_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Commission (WASA through APPS/WAP) Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBpDisCommFromDate.DateString + "' To '" + dtpBpDisCommToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Total Billpay Count </td>";
            strHTML = strHTML + "<td valign='middle' >Total Billpay Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_DIS_COMM"].ToString() + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Dis_USSD_Comm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnCustTRxCount_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            if (rbAllDis.SelectedValue == "AllDistributor")
            {
                strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME , ALTM.ACCNT_NO TM_NO, ALTO.ACCNT_NO TO_NO, "
                    + " TM1.DIS_ID, TM1.DIS_NO, TM1.DSE_NO, TM1.AGT_NO, TM1.CUST_NO, TM1.ACTIVATION_DATE, "
                    + " TM1.COMMISSION_DISBURSE, TM1.VERIFIED, TM1.TRX_MONTH_COUNT FROM ( SELECT DISTINCT "
                    + " THA.DEL_ACCNT_ID DIS_ID, THA.DEL_ACCNT_NO DIS_NO, THA.SA_ACCNT_NO DSE_NO, "
                    + " ALA.ACCNT_NO AGT_NO, ASD.CUSTOMER_MOBILE_NO, ALC.ACCNT_NO CUST_NO, ASD.ACTIVATION_DATE, "
                    + " ASD.COMMISSION_DISBURSE, ASD.VERIFIED, "
                    + " APSNG101.FUNC_CUSTOMER_TRX_COUNT_TMWISE (ALC.ACCNT_NO, '" + dtp1stMonthTrxFromDate.DateString + "', '" + dtp1stMonthTrxToDate.DateString + "') AS TRX_MONTH_COUNT "
                    + " FROM ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALA, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALC "
                    + " WHERE ASD.BANK_CODE = 'MBL' AND ASD.COMMISSION_DISBURSE IN ('V', 'Y') AND ASD.VERIFIED IN ('V','Y') "
                    + " AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpCustRegFDate.DateString + "' AND '" + dtpCustRegToDate.DateString + "' AND "
                    + " ALA.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND ALA.ACCNT_NO = THA.A_ACCNT_NO AND "
                    + " ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN ) TM1, MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTHTO, "
                    + " ACCOUNT_LIST ALTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR WHERE "
                    + " TM1.DIS_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) "
                    + " AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) "
                    + " AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY TM1.CUST_NO ASC ";
            }

            else
            {
                if (txtDisCusWallet.Text == "")
                {
                    lblMsg.Text = "Insert Distributor Wallet";
                    return;
                }

                strSql = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME , ALTM.ACCNT_NO TM_NO, ALTO.ACCNT_NO TO_NO, "
                    + " TM1.DIS_ID, TM1.DIS_NO, TM1.DSE_NO, TM1.AGT_NO, TM1.CUST_NO, TM1.ACTIVATION_DATE, "
                    + " TM1.COMMISSION_DISBURSE, TM1.VERIFIED, TM1.TRX_MONTH_COUNT FROM ( SELECT DISTINCT "
                    + " THA.DEL_ACCNT_ID DIS_ID, THA.DEL_ACCNT_NO DIS_NO, THA.SA_ACCNT_NO DSE_NO, "
                    + " ALA.ACCNT_NO AGT_NO, ASD.CUSTOMER_MOBILE_NO, ALC.ACCNT_NO CUST_NO, ASD.ACTIVATION_DATE, "
                    + " ASD.COMMISSION_DISBURSE, ASD.VERIFIED, "
                    + " APSNG101.FUNC_CUSTOMER_TRX_COUNT_TMWISE (ALC.ACCNT_NO, '" + dtp1stMonthTrxFromDate.DateString + "', '" + dtp1stMonthTrxToDate.DateString + "') AS TRX_MONTH_COUNT "
                    + " FROM ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALA, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALC "
                    + " WHERE ASD.BANK_CODE = 'MBL' AND ASD.COMMISSION_DISBURSE IN ('V', 'Y') AND ASD.VERIFIED IN ('V','Y') "
                    + " AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpCustRegFDate.DateString + "' AND '" + dtpCustRegToDate.DateString + "' AND "
                    + " ALA.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND ALA.ACCNT_NO = THA.A_ACCNT_NO AND "
                    + " ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND THA.DEL_ACCNT_NO = '" + txtDisCusWallet.Text.Trim() + "' "
                    + " ) TM1, MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTHTO, "
                    + " ACCOUNT_LIST ALTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR WHERE "
                    + " TM1.DIS_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) "
                    + " AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) "
                    + " AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY TM1.CUST_NO ASC ";
            }     
            

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Cust_Trx_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Customer Transaction Count Report(Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Customer Registration Date Range( '" + dtpCustRegFDate.DateString + "' and '" + dtpCustRegToDate.DateString + "' )</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Transaction Count Date Range( '" + dtp1stMonthTrxFromDate.DateString + "' and '" + dtp1stMonthTrxToDate.DateString + "' )</h2></td></tr>";
            strHTML = strHTML + "</table>";



            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name</td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Activation Date</td>";
            strHTML = strHTML + "<td valign='middle' >Verified</td>";
            strHTML = strHTML + "<td valign='middle' >Commission Disbursed</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Count</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACTIVATION_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["VERIFIED"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["COMMISSION_DISBURSE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_MONTH_COUNT"].ToString() + "</td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";
            SaveAuditInfo("Preview", "Cust_Trx_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnD2dAgentWiseRpt_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT ALD.ACCNT_NO, CLD.CLINT_NAME, MTD.THANA_NAME, MDD.DISTRICT_NAME,  "
                + " APSNG101.FUNC_COUNT_REG( ALD.ACCNT_NO ,'" + dtpAgetWiseFromDate.DateString + "', '" + dtpAgetWiseToDate.DateString + "') REG_COUNT, "
                + " APSNG101.FUNC_AGT_CUST_APRV_COUNT( ALD.ACCNT_MSISDN ,'" + dtpAgetWiseFromDate.DateString + "', '" + dtpAgetWiseToDate.DateString + "') APPROVE_COUNT, "
                + " APSNG101.FUNC_AGT_CUST_REG_COMM_AMOUNT( ALD.ACCNT_MSISDN ,'" + dtpAgetWiseFromDate.DateString + "', '" + dtpAgetWiseToDate.DateString + "') REG_COMM_AMOUNT, "
                + " APSNG101.FUNC_AGT_CN_COUNT( ALD.ACCNT_MSISDN ,'" + dtpAgetWiseFromDate.DateString + "', '" + dtpAgetWiseToDate.DateString + "') AGT_CN_COUNT, "
                + " APSNG101.FUNC_AGT_CN_AMOUNT ( ALD.ACCNT_MSISDN ,'" + dtpAgetWiseFromDate.DateString + "', '" + dtpAgetWiseToDate.DateString + "') AGT_CN_AMOUNT "
                + " FROM ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD "
                + " WHERE ALD.ACCNT_RANK_ID = '141105000000000001' AND ALD.ACCNT_STATE = 'A' AND ALD.CLINT_ID = CLD.CLINT_ID "
                + " AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID ORDER BY ALD.ACCNT_NO ASC";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "D2DAgt_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>D2D Agent Transaction Report Report(Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            //strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>D2D Agent Transaction Report Date Range( '" + dtpAgetWiseFromDate.DateString + "' and '" + dtpAgetWiseToDate.DateString + "' )</h2></td></tr>";
            strHTML = strHTML + "</table>";
            
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent District </td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Registered</td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Approved</td>";
            strHTML = strHTML + "<td valign='middle' >Total Registration Commission</td>";
            strHTML = strHTML + "<td valign='middle' >Total Cashin Count</td>";
            strHTML = strHTML + "<td valign='middle' >Total Cashin Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REG_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["APPROVE_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REG_COMM_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_CN_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_CN_AMOUNT"].ToString() + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";
            SaveAuditInfo("Preview", "D2DAgt_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void btnD2DAgentPerfRpt_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            //strSql = " SELECT DISTINCT ALD.ACCNT_NO, ALD.ACCNT_ID, CLD.CLINT_ID, CLD.CLINT_NAME, "
            //    + " ASD.CUSTOMER_MOBILE_NO, ASD.ACTIVATION_DATE, CLC.VERIFIED_DATE, "
            //    + " APSNG101.FUNC_CUSTOMER_CN_COUNT (ALC.ACCNT_NO ) CN_COUNT, CAB.CAS_ACCNT_BALANCE "
            //    + " FROM ACCOUNT_LIST ALD, CLIENT_LIST CLD, ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALC, "
            //    + " CLIENT_LIST CLC, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB "
            //    + " WHERE ALD.ACCNT_RANK_ID = '141105000000000001' AND ALD.ACCNT_STATE = 'A' "
            //    + " AND ALD.CLINT_ID = CLD.CLINT_ID AND ALD.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO "
            //    + " AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '"+ dtpD2DAgentPerfRptFromDate.DateString +"' AND '"+ dtpD2DAgentPerfRptToDate.DateString +"' "
            //    + " AND ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND ALC.CLINT_ID = CLC.CLINT_ID "
            //    + " AND ALC.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID ORDER BY ALD.ACCNT_NO ";

            strSql = " SELECT DISTINCT ALD.ACCNT_NO, ALD.ACCNT_ID, CLD.CLINT_ID, CLD.CLINT_NAME, ASD.CUSTOMER_MOBILE_NO,"
                    + " MD.DISTRICT_NAME, ASD.ACTIVATION_DATE, CLC.VERIFIED_DATE, APSNG101.FUNC_CUSTOMER_CN_COUNT"
                    + " (ALC.ACCNT_NO ) CN_COUNT, CAB.CAS_ACCNT_BALANCE FROM ACCOUNT_LIST ALD, CLIENT_LIST CLD,"
                    + " ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALC,  CLIENT_LIST CLC, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,"
                    + " BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, MANAGE_THANA MT, MANAGE_DISTRICT MD"
                    + " WHERE ALD.ACCNT_RANK_ID = '141105000000000001' AND ALD.ACCNT_STATE = 'A' AND ALD.CLINT_ID = CLD.CLINT_ID"
                    + " AND ALD.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND TRUNC(ASD.ACTIVATION_DATE)"
                    + " BETWEEN '" + dtpD2DAgentPerfRptFromDate.DateString + "' AND '" + dtpD2DAgentPerfRptToDate.DateString + "'"
                    + " AND ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND ALC.CLINT_ID = CLC.CLINT_ID"
                    + " AND ALC.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID AND CLD.THANA_ID = MT.THANA_ID"
                    + " AND MT.DISTRICT_ID = MD.DISTRICT_ID ORDER BY ALD.ACCNT_NO ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "D2DAgt_Perf_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>D2D Agent Performance Report Date Range( '" + dtpD2DAgentPerfRptFromDate.DateString + "' and '" + dtpD2DAgentPerfRptToDate.DateString + "' )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Mobile</td>";
            strHTML = strHTML + "<td valign='middle' >Agent District</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
            strHTML = strHTML + "<td valign='middle' >Verification Date</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Cashin Count</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Balance</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUSTOMER_MOBILE_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACTIVATION_DATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["VERIFIED_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CN_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_ACCNT_BALANCE"].ToString() + "</td>";
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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";
			
            SaveAuditInfo("Preview", "D2DAgt_Perf_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
