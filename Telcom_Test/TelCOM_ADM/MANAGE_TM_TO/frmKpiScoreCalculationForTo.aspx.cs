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

public partial class MANAGE_TM_TO_frmKpiScoreCalculationForTo : System.Web.UI.Page
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
            //divCalculation.Visible = false;
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtToAccNo.Text == "")
            {
                lblMsg.Text = "Enter TO Account No";
                return;
            }
            if (drpMonth.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpYear.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }

            bench1.Text = "";
            target1.Text = "";
            achieved1.Text = "";
            txtMAcvCAcq.Text = "";
            //bench2.Text = "";
            //target2.Text = "";
            //achieved2.Text = "";
            //txtMAcvDpsAcq.Text = "";
            bench3.Text = "";
            target3.Text = "";
            achieved3.Text = "";
            txtMAcvTrxAmt.Text = "";
            bench4.Text = "";
            target4.Text = "";
            achieved4.Text = "";
            txtMAcvActAgt.Text = "";
            bench5.Text = "";
            target5.Text = "";
            achieved5.Text = "";
            txtMAcvLftRfd.Text = "";
            bench6.Text = "";
            target6.Text = "";
            achieved6.Text = "";
            //txtMAcvComp.Text = "";
            bench7.Text = "";
            target7.Text = "";
            achieved7.Text = "";
            //txtMAcvVisi.Text = "";
            txtMAcvLfting.Text = "";
            txtResult.Text = "";

            bench6.Text = "";
            target6.Text = "";
            achieved6.Text = "";
            txtMAcvUtBll.Text = "";
          


            divCalculation.Visible = true;
            lblMsg.Text = "";

            // FOR BENCHMARK VALUES
            string strSqlBenchmark = "";
            strSqlBenchmark = "SELECT MANAGE_KPI_PARAMETERS_ID, PARAMETER_NAME, BENCHMARK  FROM MANAGE_KPI_PARAMETERS WHERE  KPI_AREA='U' AND STATS='A'";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSqlBenchmark);
            if (oDataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in oDataSet.Tables[0].Rows)
                {
                    if (prow["MANAGE_KPI_PARAMETERS_ID"].ToString() == "21070200000003")
                    {
                        bench1.Text = prow["BENCHMARK"].ToString();
                    }
                    //else if (prow["MANAGE_KPI_PARAMETERS_ID"].ToString() == "15121000000002")
                    //{
                    //    bench2.Text = prow["BENCHMARK"].ToString();
                    //}
                    else if (prow["MANAGE_KPI_PARAMETERS_ID"].ToString() == "21070200000005")
                    {
                        bench3.Text = prow["BENCHMARK"].ToString();
                    }
                    else if (prow["MANAGE_KPI_PARAMETERS_ID"].ToString() == "21070200000006")
                    {
                        bench4.Text = prow["BENCHMARK"].ToString();
                    }
                    else if (prow["MANAGE_KPI_PARAMETERS_ID"].ToString() == "21070200000004")
                    {
                        bench5.Text = prow["BENCHMARK"].ToString();
                    }
                    else if (prow["MANAGE_KPI_PARAMETERS_ID"].ToString() == "21070200000002")
                    {
                        bench6.Text = prow["BENCHMARK"].ToString();
                    }
                    else if (prow["MANAGE_KPI_PARAMETERS_ID"].ToString() == "21070200000001")
                    {
                        bench7.Text = prow["BENCHMARK"].ToString();
                    }
                  
                }

                
                // FOR TARGET VALUES

                string dtpFromDate = "";
                string dtpToDate = "";

                if (drpMonth.SelectedValue == "Jan")
                {
                    dtpFromDate = "01/Jan/";
                    dtpToDate = "31/Jan/";
                }
                else if (drpMonth.SelectedValue == "Feb")
                {
                    if ((Convert.ToInt32(drpYear.SelectedValue) % 4) == 0)
                    {
                        dtpFromDate = "01/Feb/";
                        dtpToDate = "29/Feb/";
                    }
                    else
                    {
                        dtpFromDate = "01/Feb/";
                        dtpToDate = "28/Feb/";
                    }
                }
                else if (drpMonth.SelectedValue == "Mar")
                {
                    dtpFromDate = "01/Mar/";
                    dtpToDate = "31/Mar/";
                }
                else if (drpMonth.SelectedValue == "Apr")
                {
                    dtpFromDate = "01/Apr/";
                    dtpToDate = "30/Apr/";
                }
                else if (drpMonth.SelectedValue == "May")
                {
                    dtpFromDate = "01/May/";
                    dtpToDate = "31/May/";
                }
                else if (drpMonth.SelectedValue == "Jun")
                {
                    dtpFromDate = "01/Jun/";
                    dtpToDate = "30/Jun/";
                }
                else if (drpMonth.SelectedValue == "Jul")
                {
                    dtpFromDate = "01/Jul/";
                    dtpToDate = "31/Jul/";
                }
                else if (drpMonth.SelectedValue == "Aug")
                {
                    dtpFromDate = "01/Aug/";
                    dtpToDate = "31/Aug/";
                }
                else if (drpMonth.SelectedValue == "Sep")
                {
                    dtpFromDate = "01/Sep/";
                    dtpToDate = "30/Sep/";
                }
                else if (drpMonth.SelectedValue == "Oct")
                {
                    dtpFromDate = "01/Oct/";
                    dtpToDate = "31/Oct/";
                }
                else if (drpMonth.SelectedValue == "Nov")
                {
                    dtpFromDate = "01/Nov/";
                    dtpToDate = "30/Nov/";
                }
                else if (drpMonth.SelectedValue == "Dec")
                {
                    dtpFromDate = "01/Dec/";
                    dtpToDate = "31/Dec/";
                }

                else
                {
                    // do nothing
                }

                dtpFromDate = dtpFromDate + drpYear.SelectedValue;
                dtpToDate = dtpToDate + drpYear.SelectedValue;

                // for test ing date range
                //dtpFromDate = "01/sep/2014";
                //hdfFromDate.Value = dtpFromDate;
                //dtpToDate = "20/Dec/2015";
                //hdfToDate.Value = dtpToDate;


                string strSqlTarget = "";
                strSqlTarget = " SELECT AL.ACCNT_ID, AL.ACCNT_NO, MK.KPI_TARGET_ID, MK.TO_ACCNT_ID, MK.CUST_ACQU_TARGET,  MK.DPS_ACC_ACQU_TARGET, MK.TRX_AMT_TARGET, MK.ACTIVE_AGENTNO_TARGET, MK.ACTIVE_AGENT_TRXAMT_TARGET, MK.CORP_COLLECTION_TARGET, MK.COMPLIANCE_TARGET, MK.VISIBILITY_TARGET, MK.TARGET_YEAR, MK.TARGET_MONTH,LIFTING_AMOUNT_TARGET,UTILITY_AMOUNT_TARGET FROM ACCOUNT_LIST AL, MANAGE_KPI_TARGET MK WHERE AL.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "'  AND AL.ACCNT_RANK_ID = '120519000000000006' AND AL.TERRITORY_RANK_ID = '150121000000000002'  AND AL.ACCNT_ID = MK.TO_ACCNT_ID AND MK.TARGET_MONTH = '" + drpMonth.SelectedValue + "' AND MK.TARGET_YEAR = '" + drpYear.SelectedValue + "'";
                
                DataSet oDataSetTar = objServiceHandler.ExecuteQuery(strSqlTarget);
                if (oDataSetTar.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow prow in oDataSetTar.Tables[0].Rows)
                    {
                        target1.Text = prow["CUST_ACQU_TARGET"].ToString();
                       // target2.Text = prow["DPS_ACC_ACQU_TARGET"].ToString();
                        target3.Text = prow["TRX_AMT_TARGET"].ToString();
                        string strActAgtNo = prow["ACTIVE_AGENTNO_TARGET"].ToString();
                        hdfActiveAgent.Value = strActAgtNo;
                        string strEachActAgtTrxAmt = prow["ACTIVE_AGENT_TRXAMT_TARGET"].ToString();
                        hdfAgtTrx.Value = strEachActAgtTrxAmt;
                        string strTarget4 = strActAgtNo + " Agents with Trx. Amt. of BDT " + strEachActAgtTrxAmt;
                        target4.Text = strTarget4;
                        target5.Text = prow["CORP_COLLECTION_TARGET"].ToString();
                        target6.Text = prow["LIFTING_AMOUNT_TARGET"].ToString();
                        target7.Text = prow["UTILITY_AMOUNT_TARGET"].ToString();
                       
                        //string strSqlComplianceTarget = " SELECT   COUNT (CLCS.CLINT_ID) CNT_COMPLIANCE FROM   ACCOUNT_LIST ALTO, "
                        //    + " MANAGE_TERRITORY_HIERARCHY MTH, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALCS, "
                        //    + " CLIENT_LIST CLCS WHERE ALTO.ACCNT_NO = '"+txtToAccNo.Text.Trim()  +"' AND ALTO.ACCNT_RANK_ID = '120519000000000006' "
                        //    + " AND ALTO.TERRITORY_RANK_ID = '150121000000000002' AND ALTO.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID "
                        //    + " AND MTH.ACCNT_ID = THA.DEL_ACCNT_ID AND '+88' || SUBSTR (THA.A_ACCNT_NO, 1, 11) = ASD.AGENT_MOBILE_NO "
                        //    + " AND ASD.VERIFIED IN ('Y', 'V') AND ASD.BANK_CODE = 'MBL' AND ASD.CUSTOMER_MOBILE_NO = ALCS.ACCNT_MSISDN "
                        //    + " AND ALCS.CLINT_ID = CLCS.CLINT_ID AND CLCS.VERIFIED_BY IS NOT NULL "
                        //    + " AND TRUNC (CLCS.VERIFIED_DATE ) BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' ";
                        //DataSet oDataSetComplianceTarget = objServiceHandler.ExecuteQuery(strSqlComplianceTarget);
                        //if (oDataSetComplianceTarget.Tables[0].Rows.Count > 0)
                        //{
                        //    foreach (DataRow prow1 in oDataSetComplianceTarget.Tables[0].Rows)
                        //    {
                        //        target6.Text = prow1["CNT_COMPLIANCE"].ToString();
                        //    }
                        //}

                       // target7.Text = prow["VISIBILITY_TARGET"].ToString();

                    }
                }

                
                // for achieved marks
                               
                //// for customer acquisition
                // condition: count of customer verification as per verification date

                string strsqlCAcq = "";
                strsqlCAcq = " SELECT DISTINCT COUNT(ALC.ACCNT_ID ) CNT_RG FROM ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTH, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_SERIAL_DETAIL ASD,  ACCOUNT_LIST ALC, CLIENT_LIST CLC WHERE ALTO.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "'   AND  ALTO.ACCNT_RANK_ID = '120519000000000006' AND ALTO.TERRITORY_RANK_ID = '150121000000000002'  AND ALTO.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID AND MTH.ACCNT_ID = THA.DEL_ACCNT_ID  AND '+88'||SUBSTR(THA.A_ACCNT_NO, 1,11) = ASD.AGENT_MOBILE_NO AND ASD.VERIFIED IN ( 'Y', 'V')  AND ASD.BANK_CODE = 'MBL' AND ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND ALC.CLINT_ID =  CLC.CLINT_ID AND CLC.VERIFIED_BY IS NOT NULL  AND TRUNC(CLC.VERIFIED_DATE) BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' "; 

                DataSet oDataSetCAcq = objServiceHandler.ExecuteQuery(strsqlCAcq);
                if (oDataSetCAcq.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow prow in oDataSetCAcq.Tables[0].Rows)
                    {
                        if (prow["CNT_RG"].ToString() == "")
                        {
                            achieved1.Text = Convert.ToString(0);
                        }
                        else
                        {
                            achieved1.Text = prow["CNT_RG"].ToString();
                        }

                        //target6.Text = achieved1.Text;

                    }
                }

                // for dps account acquisition
                // condition: dps account holder should be verified account holder

                //string strSqlDpsAcq = "";
                //strSqlDpsAcq = " SELECT DISTINCT COUNT(FD.DEPO_ACC_ID) CNT_DPS FROM BDMIT_ERP_101.FN_DEPOSIT FD, "
                //               + " BDMIT_ERP_101.FN_SOURCE FS, TEMP_HIERARCHY_LIST_ALL THA, MANAGE_TERRITORY_HIERARCHY MTH, "
                //               + " ACCOUNT_LIST AL, ACCOUNT_LIST ALC, ACCOUNT_SERIAL_DETAIL ASD "
                //               + " WHERE TRUNC(FD.ACTIVE_DATE) BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' "
                //               + " AND FD.DEPO_STATUS = 'A' AND FD.FAVOUR IS NOT NULL AND THA.A_ACCNT_NO = FD.FAVOUR "
                //               + " AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID AND MTH.HIERARCHY_ACCNT_ID = AL.ACCNT_ID "
                //               + " AND AL.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "' AND FD.FN_SOURCE_ID = FS.FN_SOURCE_ID "
                //               + " AND FS.MOBILE_NO||1 = ALC.ACCNT_NO AND ALC.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO "
                //               + " AND ASD.VERIFIED = 'Y' AND ASD.BANK_CODE = 'MBL' ";

                //DataSet oDataSetDps = objServiceHandler.ExecuteQuery(strSqlDpsAcq);
                //if (oDataSetDps.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow prow in oDataSetDps.Tables[0].Rows)
                //    {
                //        if (prow["CNT_DPS"].ToString() == "")
                //        {
                //            achieved2.Text = Convert.ToString(0);
                //        }
                //        else
                //        {
                //            achieved2.Text = prow["CNT_DPS"].ToString();    
                //        }
                //    }
                //}

                // for total transaction amount
                string strSqlTrx = "";
                //strSqlTrx = " SELECT DISTINCT SUM(TMIS.TRANSACTION_AMOUNT) SUM_TRX FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_HIERARCHY MTH, "
                //            + " TEMP_HIERARCHY_LIST_ALL THA, TEMP_MIS_TRANSACTIONS_REPORT TMIS WHERE TRUNC(TMIS.TRANSACTION_DATE) "
                //            + " BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' AND AL.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "' "
                //            + " AND AL.ACCNT_RANK_ID = '120519000000000006' AND AL.TERRITORY_RANK_ID = '150121000000000002' "
                //            + " AND AL.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID AND MTH.ACCNT_ID = THA.DEL_ACCNT_ID "
                //            + " AND ((SUBSTR(THA.A_ACCNT_NO, 1, 11) = TMIS.REQUEST_PARTY) OR (THA.A_ACCNT_NO = TMIS.RECEPENT_PARTY)) "
                //            + " AND TMIS.SERVICE_CODE IN ('CN', 'CCT', 'SW') ";

                strSqlTrx = " SELECT DISTINCT SUM(TMIS.TRANSACTION_AMOUNT) SUM_TRX FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_HIERARCHY MTH, "
                            + " TEMP_HIERARCHY_LIST_ALL THA, TEMP_MIS_TRANSACTIONS_REPORT TMIS WHERE TRUNC(TMIS.TRANSACTION_DATE) "
                            + " BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' AND AL.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "' "
                            + " AND AL.ACCNT_RANK_ID = '120519000000000006' AND AL.TERRITORY_RANK_ID = '150121000000000002' "
                            + " AND AL.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID AND MTH.ACCNT_ID = THA.DEL_ACCNT_ID "
                            + " AND ((SUBSTR(THA.A_ACCNT_NO, 1, 11) = TMIS.REQUEST_PARTY) OR (THA.A_ACCNT_NO = TMIS.RECEPENT_PARTY)) "
                            + " AND TMIS.SERVICE_CODE IN ('CN', 'CCT', 'SW',  'BD') ";



                DataSet oDataSetTrxAmtTO = objServiceHandler.ExecuteQuery(strSqlTrx);
                if (oDataSetTrxAmtTO.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow prow in oDataSetTrxAmtTO.Tables[0].Rows)
                    {
                        if (prow["SUM_TRX"].ToString() == "")
                        {
                            achieved3.Text = Convert.ToString(0);
                        }
                        else
                        {
                            achieved3.Text = prow["SUM_TRX"].ToString();
                        }
                        
                    }
                }

                
                // for active agent transaction(cn, cct, sw, ubp, bd)

                string strSqlActAgtTrx = "";
                strSqlActAgtTrx = " SELECT TM_F.AGT_ACC, TM_F.TRX_SUM FROM  ( SELECT DISTINCT TM.A_ACCNT_NO AGT_ACC, "
                                  + " SUM(TM.TRANSACTION_AMOUNT) TRX_SUM FROM ( SELECT DISTINCT AL.ACCNT_NO ACCNT_NO, "
                                  + " THA.DEL_ACCNT_NO DEL_ACCNT_NO, THA.A_ACCNT_NO A_ACCNT_NO, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID "
                                  + " FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_HIERARCHY MTH, TEMP_HIERARCHY_LIST_ALL THA, "
                                  + " TEMP_MIS_TRANSACTIONS_REPORT TMIS WHERE AL.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "' "
                                  + " AND AL.ACCNT_RANK_ID = '120519000000000006' AND AL.TERRITORY_RANK_ID = '150121000000000002' "
                                  + " AND AL.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID AND "
                                  + " TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' "
                                  + " AND TMIS.SERVICE_CODE IN ('CN', 'CCT', 'SW', 'UBP', 'BD') AND ((SUBSTR(THA.A_ACCNT_NO, 1, 11) = TMIS.REQUEST_PARTY) "
                                  + " OR (THA.A_ACCNT_NO = TMIS.RECEPENT_PARTY)) ) TM GROUP BY TM.A_ACCNT_NO ) TM_F "
                                  + " WHERE TM_F.TRX_SUM >= '" + hdfAgtTrx.Value + "' ";

                DataSet oDataSetActAgt = objServiceHandler.ExecuteQuery(strSqlActAgtTrx);
                int intActAgtCount = oDataSetActAgt.Tables[0].Rows.Count;
                achieved4.Text = intActAgtCount + " Agents with Trx. Amt. of BDT " + hdfAgtTrx.Value;


                // for copporate collection
                string strSqlLftRfd = "";
               

                strSqlLftRfd = " SELECT SUM(TM.TRANSACTION_AMOUNT) SUM_DLF_DRF_AMT  FROM ( SELECT DISTINCT TMIS.REQUEST_ID, TMIS.TRANSACTION_DATE, "
                                + " TMIS.SERVICE_CODE, TMIS.TRANSACTION_AMOUNT, TMIS.REQUEST_PARTY, TMIS.RECEPENT_PARTY FROM ACCOUNT_LIST ALTO, "
                                + " MANAGE_TERRITORY_HIERARCHY MTH,  ACCOUNT_LIST ALDIS, TEMP_HIERARCHY_LIST_ALL THA, TEMP_MIS_TRANSACTIONS_REPORT TMIS, "
                                + " ACCOUNT_LIST ALCORP WHERE ALTO.TERRITORY_RANK_ID = '150121000000000002' AND ALTO.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "' "
                                + " AND ALTO.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID AND MTH.ACCNT_ID = ALDIS.ACCNT_ID AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO  "
                                + " AND THA.SA_ACCNT_NO = TMIS.REQUEST_PARTY||'1' AND TMIS.SERVICE_CODE = 'FM' "
                                + " AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' "
                                + " AND TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO  AND ALCORP.ACCNT_RANK_ID = '140917000000000004') TM ";

                DataSet oDataSetLr = objServiceHandler.ExecuteQuery(strSqlLftRfd);
                if (oDataSetLr.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow prow in oDataSetLr.Tables[0].Rows)
                    {
                        if (prow["SUM_DLF_DRF_AMT"].ToString() == "")
                        {
                            achieved5.Text = Convert.ToString(0);
                        }
                        else
                        {
                            achieved5.Text = prow["SUM_DLF_DRF_AMT"].ToString();
                        }
                    }
                }
                // for lifting amount
                string strSqllft = "";


                strSqllft = "SELECT SUM (LIFTING) SUM_TRX_LFT FROM (SELECT DISTINCT  ALTO.ACCNT_NO TO_NO,  CLTO.CLINT_NAME TO_NAME,  'CASH IN' MARKET_TYPE, CAB.CAS_ACCNT_BALANCE DIS_BALANCE, CAB.UPTO_DATE BAL_DATE,  APSNG101.PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW(TMDIS.DISTRIBUTOR_NO,'" + dtpFromDate + "','" + dtpToDate + "') LIFTING FROM (SELECT DISTINCT THA.DEL_ACCNT_NO DISTRIBUTOR_NO, ALD.ACCNT_ID DIS_ACC_ID  FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD WHERE THA.DEL_ACCNT_ID = ALD.ACCNT_ID(+)  AND ALD.ACCNT_STATE IN ('A')   AND ALD.ACCNT_RANK_ID IN ('120519000000000003') ) TMDIS,  MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTHTO, MANAGE_TERRITORY_AREA MTA,  CLIENT_LIST CLTO, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB   WHERE ALTO.ACCNT_NO ='" + txtToAccNo.Text.Trim() + "' AND TMDIS.DIS_ACC_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+)     AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND TMDIS.DISTRIBUTOR_NO = CAL.CAS_ACC_NO(+)  AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID(+) ORDER BY ALTO.ACCNT_NO DESC) ";



                DataSet oDataSetLftTO = objServiceHandler.ExecuteQuery(strSqllft);
                if (oDataSetLftTO.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow prow in oDataSetLftTO.Tables[0].Rows)
                    {
                        if (prow["SUM_TRX_LFT"].ToString() == "")
                        {
                            achieved6.Text = Convert.ToString(0);
                        }
                        else
                        {
                            achieved6.Text = prow["SUM_TRX_LFT"].ToString();
                        }

                    }
                }

                // FOR UTILITY AMOUNT TRANSACTION
                string strSqlUti = "";

                strSqlTrx = " SELECT DISTINCT NVL(SUM(TMIS.TRANSACTION_AMOUNT),0) SUM_TRX_UT FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_HIERARCHY MTH, "
                            + " TEMP_HIERARCHY_LIST_ALL THA, TEMP_MIS_TRANSACTIONS_REPORT TMIS WHERE TRUNC(TMIS.TRANSACTION_DATE) "
                            + " BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' AND AL.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "' "
                            + " AND AL.ACCNT_RANK_ID = '120519000000000006' AND AL.TERRITORY_RANK_ID = '150121000000000002' "
                            + " AND AL.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID AND MTH.ACCNT_ID = THA.DEL_ACCNT_ID "
                            + " AND ((SUBSTR(THA.A_ACCNT_NO, 1, 11) = TMIS.REQUEST_PARTY) OR (THA.A_ACCNT_NO = TMIS.RECEPENT_PARTY)) "
                            + " AND TMIS.SERVICE_CODE ='UBP'  ";



                DataSet oDataSetUtiTO = objServiceHandler.ExecuteQuery(strSqlTrx);
                if (oDataSetUtiTO.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow prow in oDataSetUtiTO.Tables[0].Rows)
                    {
                        if (prow["SUM_TRX_UT"].ToString() == "")
                        {
                            achieved7.Text = Convert.ToString(0);
                        }
                        else
                        {
                            achieved7.Text = prow["SUM_TRX_UT"].ToString();
                        }

                    }
                }

                // for compliance
                //Compliance Issue:
                //Total number of Verified customer in a particular month divided by 
                //Total number of Registered customer in a particular month will be considered as compliance 
                //ratio for Achieved mark where the Total count of Registered  customer will be shown automatically in the “Target Given” menu 
                //and the number of Verified customer will be shown automatically in the “If Achieved” menu. 

                //string strSqlCompliance = "";
                //strSqlCompliance = " SELECT COUNT(CLCS.CLINT_ID) CNT_COMPLIANCE FROM ACCOUNT_LIST ALTO, MANAGE_TERRITORY_HIERARCHY MTH, "
                //                    + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALCS, CLIENT_LIST CLCS "
                //                    + " WHERE ALTO.ACCNT_NO = '" + txtToAccNo.Text.Trim() + "' AND  ALTO.ACCNT_RANK_ID = '120519000000000006' "
                //                    + " AND ALTO.TERRITORY_RANK_ID = '150121000000000002' AND ALTO.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID AND "
                //                    + " MTH.ACCNT_ID = THA.DEL_ACCNT_ID AND '+88'||SUBSTR(THA.A_ACCNT_NO, 1, 11) = ASD.AGENT_MOBILE_NO "
                //                    + " AND ASD.VERIFIED IN( 'Y', 'V') AND ASD.BANK_CODE = 'MBL' AND ASD.CUSTOMER_MOBILE_NO = ALCS.ACCNT_MSISDN "
                //                    + " AND ALCS.CLINT_ID = CLCS.CLINT_ID AND CLCS.VERIFIED_BY IS NOT NULL  "
                //                    + " AND TRUNC(CLCS.VERIFIED_DATE) BETWEEN '" + dtpFromDate + "' AND '" + dtpToDate + "' ";

                
                //DataSet oDataSetCompliance = objServiceHandler.ExecuteQuery(strSqlCompliance);
                //if (oDataSetCompliance.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow prow in oDataSetCompliance.Tables[0].Rows)
                //    {
                //        if (prow["CNT_COMPLIANCE"].ToString() == "")
                //        {
                //            achieved6.Text = Convert.ToString(0);
                //        }
                //        else
                //        {
                //            achieved6.Text = prow["CNT_COMPLIANCE"].ToString();
                //        }
                //    }
                //}

                // for visibility
                achieved7.Text = "0";


                // ---------------for final calculation---------------------------------------
                
                // for customer acquisition/registration 
                int intCustAchived = Convert.ToInt32(achieved1.Text);
                
                ////
                //if (intCustAchived < 50)
                //{
                //    intCustAchived = 0;
                //}
                ////
                int intTargetCustAchieved = Convert.ToInt32(target1.Text);
                int intBenchmark1 = Convert.ToInt32(bench1.Text);
                double dblCustAchievedMark = (double)intCustAchived / (double)intTargetCustAchieved;
                dblCustAchievedMark = dblCustAchievedMark * (double)intBenchmark1;
                dblCustAchievedMark = System.Math.Round(dblCustAchievedMark, 2);
                string strResult1 = Convert.ToString(dblCustAchievedMark);
                // if customer registration exceeds more than 200% 
                // then TO will get x1.5 marks
                if (Convert.ToDouble(strResult1) > Convert.ToDouble(intBenchmark1*1.5))
                {
                    strResult1 = Convert.ToString(intBenchmark1*1.5);
                }
                
                txtMAcvCAcq.Text = strResult1;

                // for dps account acquisition
                //int intDpsAchieved = Convert.ToInt32(achieved2.Text);
                //int intTargetDpsAchieved = Convert.ToInt32(target2.Text);
                //int intBenchmark2 = Convert.ToInt32(bench2.Text);
                //double dblDpsAchievedMark = (double)intDpsAchieved / (double)intTargetDpsAchieved;
                //dblDpsAchievedMark = dblDpsAchievedMark * (double)intBenchmark2;
                //dblDpsAchievedMark = System.Math.Round(dblDpsAchievedMark, 2);
                //string strResult2 = Convert.ToString(dblDpsAchievedMark);
                //// if mydps account acquisition exceeds more than 200% 
                //// then TO will get 200% marks
                //if (Convert.ToDouble(strResult2) > Convert.ToDouble(intBenchmark2*2))
                //{
                //    strResult2 = Convert.ToString(intBenchmark2*2);
                //}
                //txtMAcvDpsAcq.Text = strResult2;

                // for transaction amount  
                int intTrxAmountAchieved = Convert.ToInt32(achieved3.Text);
                int intTargetTrxAmount = Convert.ToInt32(target3.Text);
                int intBenchmark3 = Convert.ToInt32(bench3.Text);
                double dblTransactionAmount = (double)intTrxAmountAchieved / (double)intTargetTrxAmount;
                dblTransactionAmount = dblTransactionAmount * (double)intBenchmark3;
                dblTransactionAmount = System.Math.Round(dblTransactionAmount, 2);
                string strResult3 = Convert.ToString(dblTransactionAmount);
                // if transaction amount exceeds more than 200% 
                // then TO will get *1.5 marks
                if (Convert.ToDouble(strResult3) > Convert.ToDouble(intBenchmark3 * 1.5))
                {
                    strResult3 = Convert.ToString(intBenchmark3 * 1.5);
                }
                txtMAcvTrxAmt.Text = strResult3;
                
                // for active agent
                // calculation formula:
                // target: if 50 agent with transaction amount more than 2000
                // achieved: if 40 agent with transaction amount more than 2000 and benchmark = 20
                // then: (40/50)*20 = 16
                // more than 79% of the benchmark value would be consider 

                
                int intActiveAgentCount = intActAgtCount;
                int intTargetActiveAgent = Convert.ToInt32(hdfActiveAgent.Value);
                int intBenchmark4 = Convert.ToInt32(bench4.Text);
                double dbl79PercentTrx = Convert.ToDouble(intBenchmark4*.79);
                double dblActAgentTrx = (double) intActiveAgentCount/(double) intTargetActiveAgent;
                dblActAgentTrx = dblActAgentTrx*(double) intBenchmark4;
                dblActAgentTrx = System.Math.Round(dblActAgentTrx, 2);
                string strResult4 = Convert.ToString(dblActAgentTrx);
                //if (Convert.ToDouble(strResult4) > Convert.ToDouble(intBenchmark4))
                //{
                //    strResult4 = Convert.ToString(intBenchmark4);
                //}

                if (Convert.ToDouble(strResult4) < dbl79PercentTrx)
                {
                    strResult4 = Convert.ToString(0);
                }

                else if (Convert.ToDouble(strResult4) >= dbl79PercentTrx && Convert.ToDouble(strResult4) < Convert.ToDouble(intBenchmark4))
                {
                    strResult4 = strResult4;
                }

                else if (Convert.ToDouble(strResult4) > Convert.ToDouble(intBenchmark4))
                {
                    strResult4 = Convert.ToString(intBenchmark4);
                }

                txtMAcvActAgt.Text = strResult4;

                // for CORPORATE COLLECTION
                int intAchievedLftRfd = Convert.ToInt32(achieved5.Text);
                int intTargetLftRfd = Convert.ToInt32(target5.Text);
                int intBenchmark5 = Convert.ToInt32(bench5.Text);
                double dblLiftingrefund = (double) intAchievedLftRfd/(double) intTargetLftRfd;
                dblLiftingrefund = dblLiftingrefund*(double) intBenchmark5;
                dblLiftingrefund = System.Math.Round(dblLiftingrefund, 2);
                string strResult5 = Convert.ToString(dblLiftingrefund);
                // if corporate collection exceeds more than 200% 
                // then TO will get *1.5 marks
                if (Convert.ToDouble(strResult5) > Convert.ToDouble(intBenchmark5*1.5))
                {
                    strResult5 = Convert.ToString(intBenchmark5*1.5);
                }
                txtMAcvLftRfd.Text = strResult5;

                //FOR LIFTING
                int intAchievedLft = Convert.ToInt32(achieved6.Text);
                int intTargetLft = Convert.ToInt32(target6.Text);
                int intBenchmark6 = Convert.ToInt32(bench6.Text);
                double dblLifting = (double)intAchievedLft / (double)intTargetLft;
                dblLifting = dblLifting * (double)intBenchmark6;
                dblLifting = System.Math.Round(dblLifting, 2);
                string strResult6 = Convert.ToString(dblLifting);
                // if corporate collection exceeds more than 200% 
                // then TO will get *1.5 marks
                if (Convert.ToDouble(strResult6) > Convert.ToDouble(intBenchmark6 * 1.5))
                {
                    strResult6 = Convert.ToString(intBenchmark6 * 1.5);
                }
                txtMAcvLfting.Text = strResult6;

                //FOR UTILITY
                int intAchievedUt = Convert.ToInt32(achieved7.Text);
                int intTargetUt = Convert.ToInt32(target7.Text);
                int intBenchmark7 = Convert.ToInt32(bench7.Text);
                double dblUtility = (double)intAchievedUt / (double)intTargetUt;
                dblUtility = dblUtility * (double)intBenchmark7;
                dblLifting = System.Math.Round(dblUtility, 2);
                string strResult7 = Convert.ToString(dblUtility);
                // if corporate collection exceeds more than 200% 
                // then TO will get *1.5 marks
                if (Convert.ToDouble(strResult7) > Convert.ToDouble(intBenchmark7 * 1.5))
                {
                    strResult7 = Convert.ToString(intBenchmark7 * 1.5);
                }
                txtMAcvUtBll.Text = strResult7;

                // for compliance
                //int intAchievedCompliance = Convert.ToInt32(achieved6.Text);
                //int intTargetCompliance = Convert.ToInt32(target6.Text);
                //int intBenchmark6 = Convert.ToInt32(bench6.Text);
                //double dbl69PctBenchmark = Convert.ToDouble(intBenchmark6*.69);
                //double dblComplianceCount = (double) intAchievedCompliance/(double) intTargetCompliance;
                //dblComplianceCount = dblComplianceCount*(double) intBenchmark6;
                //dblComplianceCount = System.Math.Round(dblComplianceCount, 2);
                //string strResult6 = Convert.ToString(dblComplianceCount);

                //// for undefined number NAN
                //if (intAchievedCompliance == 0 && intTargetCompliance == 0)
                //{
                //    strResult6 = "0";
                //}

                //if (Convert.ToDouble(strResult6) > Convert.ToDouble(intBenchmark6))
                //{
                //    strResult6 = Convert.ToString(intBenchmark6);
                //}
                //if (Convert.ToDouble(strResult6) <= dbl69PctBenchmark)
                //{
                //    strResult6 = "0";
                //}
                //txtMAcvComp.Text = strResult6;

                // if customer reg 50 or 


                //if (intCustAchived < 50)
                //{
                //    strResult6 = "0";
                //}

                //else
                //{
                //    // for undefined number NAN
                //    if (intAchievedCompliance == 0 && intTargetCompliance == 0)
                //    {
                //        strResult6 = "0";
                //    }

                //    if (Convert.ToDouble(strResult6) > Convert.ToDouble(intBenchmark6))
                //    {
                //        strResult6 = Convert.ToString(intBenchmark6);
                //    }
                //    if (Convert.ToDouble(strResult6) <= dbl69PctBenchmark)
                //    {
                //        strResult6 = "0";
                //    }
                //}

                
                //txtMAcvComp.Text = strResult6;



                //// for visibility
                //string strResult7 = "0";
                //txtMAcvVisi.Text = strResult7;


                // for final result summation
                double dblResult1 = Convert.ToDouble(strResult1);
              //  double dblresult2 = Convert.ToDouble(strResult2);
                double dblresult3 = Convert.ToDouble(strResult3);
                double dblresult4 = Convert.ToDouble(strResult4);
                double dblresult5 = Convert.ToDouble(strResult5);
                double dblResult6 = Convert.ToDouble(strResult6);
                double dblResult7 = Convert.ToDouble(strResult7);

                //double finalGrade = dblResult1 + dblresult2 + dblresult3 + dblresult4 + dblresult5 + dblResult6 +
                //                    dblResult7;

                double finalGrade = dblResult1 + dblresult3 + dblresult4 + dblresult5 + dblResult6 + dblResult7;

                txtResult.Text = Convert.ToString(finalGrade);
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
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
