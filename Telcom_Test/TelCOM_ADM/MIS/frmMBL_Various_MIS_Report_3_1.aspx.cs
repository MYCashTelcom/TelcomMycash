using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

public partial class MIS_frmMBL_Various_MIS_Report_3_1 : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();

                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                var PrevMonthFirstDate = month.AddMonths(-1);
                var PrevMonthLastDate = month.AddDays(-1);

                dtpARIFrDate.DateString = String.Format("{0:dd-MMM-yyyy}", PrevMonthFirstDate);
                dtpARIToDate.DateString = String.Format("{0:dd-MMM-yyyy}", PrevMonthLastDate);

                dtpDRIFrDate.DateString = String.Format("{0:dd-MMM-yyyy}", PrevMonthFirstDate);
                dtpDRIToDate.DateString = String.Format("{0:dd-MMM-yyyy}", PrevMonthLastDate);


                dtpARIFrDate.Enabled = false;
                dtpARIToDate.Enabled = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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

    protected void btnDisComm_Click(object sender, EventArgs e)
    {
        try
        {
            string strGetDistributorInfo = objServiceHandler.GetDistributorInfo(txtDisCommWallet.Text.Trim());

            string strSql = "";
            //strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DEL_ACC, TM.SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT, "
            //         + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '1' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
            //         + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim()  + "' AND THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' "
            //         + " AND TM.SERVICE_CODE = 'CN' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO, TM.SERVICE_CODE  "
            //         + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, 'CCT,SW' SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT,  "
            //         + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '2' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
            //         + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.RECEPENT_PARTY "
            //         + " AND TM.SERVICE_CODE IN ('CCT','SW') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO "
            //         + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, TM.SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT,  "
            //         + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '3' REC_COUNT  FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
            //         + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' "
            //         + " AND TM.SERVICE_CODE = 'BD' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO, TM.SERVICE_CODE "
            //         + " UNION SELECT DISTINCT CL_NAME, 'CORP TRX' SERVICE_CODE, COUNT(REQ_ID) COUNT_TRX, SUM(TR_AMT) TRX_AMT, SUM(TRD_COMM) TRX_COMM_AMT, '4' REC_COUNT FROM  "
            //         + " ( SELECT DISTINCT CLC.CLINT_NAME CL_NAME, THA.DEL_ACCNT_NO, THA.SA_ACCNT_NO, TM.REQUEST_ID REQ_ID, TM.TRANSACTION_DATE, TM.SERVICE_CODE, TM.TRANSACTION_AMOUNT TR_AMT, "
            //         + " TM.REQUEST_PARTY, TM.RECEPENT_PARTY, TM.SERVICE_FEE, TM.NET_COMMISSION, TM.AIT, TM.SERVICE_VAT_TAX, TM.POOL_ACCOUNT, TM.AGENT_COMMISSION, TM.NOTE, TM.THIRDPARTY_COM_AMOUNT TRD_COMM, "
            //         + " TM.VENDOR_COMMISSION, TM.CHANNEL_COMMISSION, TM.AGENT_OPT_COMMISSION, BANK_COMMISSION FROM TEMP_HIERARCHY_LIST_ALL THA, TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD, ACCOUNT_RANK ARD, ACCOUNT_LIST ALC, "
            //         + " ACCOUNT_RANK ARC, CLIENT_LIST CLC WHERE THA.DEL_ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.SA_ACCNT_NO = TM.REQUEST_PARTY||'1' AND TM.SERVICE_CODE = 'FM' "
            //         + " AND TM.REQUEST_PARTY||'1' = ALD.ACCNT_NO AND ALD.ACCNT_RANK_ID = ARD.ACCNT_RANK_ID AND ARD.ACCNT_RANK_ID = '120519000000000004' "
            //         + " AND TM.RECEPENT_PARTY = ALC.ACCNT_NO AND ALC.ACCNT_RANK_ID = ARC.ACCNT_RANK_ID AND ARC.ACCNT_RANK_ID = '140917000000000004' "
            //         + " AND ALC.CLINT_ID = CLC.CLINT_ID AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'   ) TEMP GROUP BY CL_NAME "
            //         + " UNION SELECT DIS_NO, 'RG' TRX_TYPE, SUM (RG_COUNT) RG_APRV_COUNT, SUM(REG),  SUM (TOTAL_RG_COMMISSION) TOTAL_RG_COMMISSION, '5' REC_COUNT "
            //         + " FROM VW_DISTRIBUTION_REPORT2 VDR, (SELECT TRAN2.DEL_ACCNT_NO DIS_ACC_NO, TRAN2.REG_CNT REG_COUNT, '0' REG  FROM (SELECT TRAN.DEL_ACCNT_NO,SUM(CT) REG_CNT  FROM   (SELECT THL.DEL_ACCNT_NO,ASD.AGENT_MOBILE_NO, "
            //         + " COUNT (ASD.CUSTOMER_MOBILE_NO) CT FROM ACCOUNT_SERIAL_DETAIL ASD,   TEMP_HIERARCHY_LIST_ALL THL WHERE TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'  AND SUBSTR(('+88'||THL.A_ACCNT_NO),0,14)=ASD.AGENT_MOBILE_NO "
            //         + " GROUP BY ASD.AGENT_MOBILE_NO,THL.DEL_ACCNT_NO) TRAN   GROUP BY TRAN.DEL_ACCNT_NO) TRAN2, CLIENT_LIST CL ,MANAGE_THANA MT, MANAGE_DISTRICT MD "
            //         + " WHERE TRAN2.DEL_ACCNT_NO= SUBSTR(CL.CLINT_MOBILE,4,14)||'1'  AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) ) TEMP "
            //         + " WHERE VDR.TRANSACTION_DATE BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'  AND DIS_NO = TEMP.DIS_ACC_NO(+) AND DIS_NO = '" + txtDisCommWallet.Text.Trim() + "' "
            //         + " GROUP BY DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, REG_COUNT ORDER BY REC_COUNT ASC";

            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DEL_ACC, TM.SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT, "
                     + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '1' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
                     + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' "
                     + " AND TM.SERVICE_CODE = 'CN' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO, TM.SERVICE_CODE  "
                     + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, 'CCT,SW' SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT,  "
                     + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '2' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
                     + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.RECEPENT_PARTY "
                     + " AND TM.SERVICE_CODE IN ('CCT','SW') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO "
                     + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, TM.SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT,  "
                     + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '3' REC_COUNT  FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
                     + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' "
                     + " AND TM.SERVICE_CODE = 'BD' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO, TM.SERVICE_CODE "
                     + " UNION SELECT DISTINCT CL_NAME, 'CORP TRX' SERVICE_CODE, COUNT(REQ_ID) COUNT_TRX, SUM(TR_AMT) TRX_AMT, SUM(TRD_COMM) TRX_COMM_AMT, '4' REC_COUNT FROM  "
                     + " ( SELECT DISTINCT CLC.CLINT_NAME CL_NAME, THA.DEL_ACCNT_NO, THA.SA_ACCNT_NO, TM.REQUEST_ID REQ_ID, TM.TRANSACTION_DATE, TM.SERVICE_CODE, TM.TRANSACTION_AMOUNT TR_AMT, "
                     + " TM.REQUEST_PARTY, TM.RECEPENT_PARTY, TM.SERVICE_FEE, TM.NET_COMMISSION, TM.AIT, TM.SERVICE_VAT_TAX, TM.POOL_ACCOUNT, TM.AGENT_COMMISSION, TM.NOTE, TM.THIRDPARTY_COM_AMOUNT TRD_COMM, "
                     + " TM.VENDOR_COMMISSION, TM.CHANNEL_COMMISSION, TM.AGENT_OPT_COMMISSION, BANK_COMMISSION FROM TEMP_HIERARCHY_LIST_ALL THA, TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD, ACCOUNT_RANK ARD, ACCOUNT_LIST ALC, "
                     + " ACCOUNT_RANK ARC, CLIENT_LIST CLC WHERE THA.DEL_ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.SA_ACCNT_NO = TM.REQUEST_PARTY||'1' AND TM.SERVICE_CODE = 'FM' "
                     + " AND TM.REQUEST_PARTY||'1' = ALD.ACCNT_NO AND ALD.ACCNT_RANK_ID = ARD.ACCNT_RANK_ID AND ARD.ACCNT_RANK_ID = '120519000000000004' "
                     + " AND TM.RECEPENT_PARTY = ALC.ACCNT_NO AND ALC.ACCNT_RANK_ID = ARC.ACCNT_RANK_ID AND ARC.ACCNT_RANK_ID = '140917000000000004' "
                     + " AND ALC.CLINT_ID = CLC.CLINT_ID AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'   ) TEMP GROUP BY CL_NAME "
                     + " UNION SELECT DIS_NO, 'RG' TRX_TYPE, SUM (RG_COUNT) RG_APRV_COUNT, SUM(REG),  SUM (TOTAL_RG_COMMISSION) TOTAL_RG_COMMISSION, '5' REC_COUNT "
                     + " FROM VW_DISTRIBUTION_REPORT2 VDR, (SELECT TRAN2.DEL_ACCNT_NO DIS_ACC_NO, TRAN2.REG_CNT REG_COUNT, '0' REG  FROM (SELECT TRAN.DEL_ACCNT_NO,SUM(CT) REG_CNT  FROM   (SELECT THL.DEL_ACCNT_NO,ASD.AGENT_MOBILE_NO, "
                     + " COUNT (ASD.CUSTOMER_MOBILE_NO) CT FROM ACCOUNT_SERIAL_DETAIL ASD,   TEMP_HIERARCHY_LIST_ALL THL WHERE TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'  AND SUBSTR(('+88'||THL.A_ACCNT_NO),0,14)=ASD.AGENT_MOBILE_NO "
                     + " GROUP BY ASD.AGENT_MOBILE_NO,THL.DEL_ACCNT_NO) TRAN   GROUP BY TRAN.DEL_ACCNT_NO) TRAN2, CLIENT_LIST CL ,MANAGE_THANA MT, MANAGE_DISTRICT MD "
                     + " WHERE TRAN2.DEL_ACCNT_NO= SUBSTR(CL.CLINT_MOBILE,4,14)||'1'  AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) ) TEMP "
                     + " WHERE VDR.TRANSACTION_DATE BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'  AND DIS_NO = TEMP.DIS_ACC_NO(+) AND DIS_NO = '" + txtDisCommWallet.Text.Trim() + "' "
                     + " GROUP BY DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, REG_COUNT "
                     + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, 'BP' SER_CODE, COUNT(DISTINCT UT.REQUEST_ID) TRX_CNT, SUM(UT.TOTAL_BILL_AMOUNT) TOT_AMT, "
                     + " SUM(CAT.CAS_TRAN_AMT) TOT_COMM_AMT, '6' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, UTILITY_TRANSACTION UT, SERVICE_REQUEST SR, "
                     + " BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE THA.DEL_ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND UT.SOURCE_ACC_NO = THA.A_ACCNT_NO "
                     + " AND UT.STAKEHOLDER_ID = 'MBLBANK' AND UT.SERVICE = 'UBP' AND UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
                     + " AND CAT.REQUEST_ID = SR.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'TDRCOM' AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' "
                     + " GROUP BY THA.DEL_ACCNT_NO "
                     + " ORDER BY REC_COUNT ASC ";



            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Dis_Comm_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor(Individual) Commission Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor : '" + strGetDistributorInfo + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisCommFDate.DateString + "' To '" + dtpDisCommToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Type</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Count</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount </td>";
            strHTML = strHTML + "<td valign='middle' >Commission</td>";
            strHTML = strHTML + "</tr>";




            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;

                double dblCnCommAmt = 0;
                double dblCctCommAmt = 0;
                double dblBdCommAmt = 0;
                double dblBpCommAmt = 0;
                double total1 = 0;


                double dblTotalSumAmount = 0;
                double dblTotalSumCommission = 0;
                double dblTotalSumVat = 0;
                double dblTotalSumAmountAfterVat = 0;
                double dblTotalSumAit = 0;
                double dblTotalSumAfterAit = 0;
                double dblTotalSumDistributorCommission = 0;
                double dblTotalSumMblCommission = 0;
                double total2 = 0;

                double total3 = 0;
                double total4 = 0;


                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Commission Details</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";



                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "CN" && prow["REC_COUNT"].ToString() == "1")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblCnCommAmt = Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());
                        total1 = dblCnCommAmt;
                    }
                }
                total1 = total1;


                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "CCT,SW" && prow["REC_COUNT"].ToString() == "2")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblCctCommAmt = Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());
                        total1 = total1 + dblCctCommAmt;
                    }
                }
                total1 = total1;


                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "BD" && prow["REC_COUNT"].ToString() == "3")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblBdCommAmt = Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());
                        total1 = total1 + dblBdCommAmt;
                    }
                }
                total1 = total1;






                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "BP" && prow["REC_COUNT"].ToString() == "6")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblBpCommAmt = Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());
                        total1 = total1 + dblBpCommAmt;
                    }
                }
                total1 = total1;




                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Total: </td>";
                strHTML = strHTML + " <td >  '" + total1 + "' </td>";
                strHTML = strHTML + " </tr>";

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Corporate Transaction Commission Details</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";


                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "CORP TRX" && prow["REC_COUNT"].ToString() == "4")
                    {
                        double dblToalAmt = Convert.ToDouble(prow["TRX_AMT"]);
                        double dblTotalCommsssion = dblToalAmt * 0.0025;
                        dblTotalCommsssion = System.Math.Round(dblTotalCommsssion, 2);
                        double dblTotalIncusiveAmt = (dblTotalCommsssion * 100) / 115;
                        dblTotalIncusiveAmt = System.Math.Round(dblTotalIncusiveAmt, 2);
                        double dblVAT = dblTotalCommsssion - dblTotalIncusiveAmt;
                        dblVAT = System.Math.Round(dblVAT, 2);
                        double dblAmtAfterVAT = dblTotalCommsssion - dblVAT;
                        dblAmtAfterVAT = System.Math.Round(dblAmtAfterVAT, 2);
                        double dblAIT = dblAmtAfterVAT * 0.1;
                        dblAIT = System.Math.Round(dblAIT, 2);
                        double dblAmtAfterAIT = dblAmtAfterVAT - dblAIT;
                        dblAmtAfterAIT = System.Math.Round(dblAmtAfterAIT, 2);
                        string disCommRate = "60%";
                        double dblDistributorCommission = dblAmtAfterAIT * 0.6;
                        dblDistributorCommission = System.Math.Round(dblDistributorCommission, 2);
                        string mblCommRate = "40%";
                        double dblMBLCommission = dblAmtAfterAIT * 0.4;
                        dblMBLCommission = System.Math.Round(dblMBLCommission, 2);



                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DEL_ACC"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + dblDistributorCommission + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        total2 = total2 + dblDistributorCommission;
                    }
                }

                total2 = total2;

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Total: </td>";
                strHTML = strHTML + " <td >'" + total2 + "</td>";
                strHTML = strHTML + " </tr>";


                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >KYC Updated Commission Details</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";


                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "RG" && prow["REC_COUNT"].ToString() == "5")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        total3 = total3 + Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());

                    }
                }

                total3 = total3;

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Total: </td>";
                strHTML = strHTML + " <td >'" + total3 + "</td>";
                strHTML = strHTML + " </tr>";

                total4 = total1 + total2 + total3;


                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "Total Commission:" + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Transaction Commission:" + " </td>";
                strHTML = strHTML + " <td >'" + total1 + "</td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";


                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Corporate Commission:" + " </td>";
                strHTML = strHTML + " <td >'" + total2 + "</td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "KYC Update Commission:" + " </td>";
                strHTML = strHTML + " <td >'" + total3 + "</td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Grand Total: " + " </td>";
                strHTML = strHTML + " <td >'" + total4 + "</td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

            }

            strHTML = strHTML + "<tr>";
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

    protected void btnDisDCReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            double totalAmount = 0;

            if (ddlStatus.SelectedValue == "S")
            {
                strSql = " SELECT DISTINCT U.UTILITY_TRAN_ID, CAT.REQUEST_ID, U.ACCOUNT_NUMBER, U.BILL_NUMBER,"
                        + " U.BILL_MONTH, U.BILL_YEAR, SERVICE, U.OWNER_CODE, U.TRANSACTION_STATUS, U.SOURCE_ACC_NO, "
                        + " U.FINAL_STATUS, U.TRANSA_DATE, U.TOTAL_BILL_AMOUNT, U.NET_DPDC_AMOUNT,"
                        + " U.RESPONSE_MSG, U.RESPONSE_LOG, U.RESPONSE_LOG_BP, U.RESPONSE_MSG_BP,"
                        + " U.RESPONSE_STATUS_BP, U.REQUEST_PARTY_TYPE, U.PAYER_MOBILE_NO,"
                        + " U.DEBIT_CREDIT_SUCC_STATUS, U.DEBIT_CREDIT_SUCC_MSG FROM UTILITY_TRANSACTION U,"
                        + " SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT"
                        + " WHERE DEBIT_CREDIT_SUCC_STATUS = 'Y' AND DEBIT_CREDIT_SUCC_MSG = 'SUCCESS'"
                        + " AND SERVICE = '" + ddlOrganization.SelectedValue + "' AND TRUNC(U.TRANSA_DATE)"
                        + " BETWEEN '" + dtpDisDCFrDate.DateString + "' AND '" + dtpDisDCToDate.DateString + "'"
                        + " AND U.REQUEST_ID = SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID = CAT.REQUEST_ID"
                        + " AND CAT.CAS_TRAN_STATUS = 'A' ORDER BY U.TRANSA_DATE DESC";
            }
            else if (ddlStatus.SelectedValue == "F")
            {
                strSql = " SELECT DISTINCT UTILITY_TRAN_ID, '' REQUEST_ID, ACCOUNT_NUMBER, BILL_NUMBER, BILL_MONTH,"
                        + " BILL_YEAR, SERVICE, OWNER_CODE, TRANSACTION_STATUS, FINAL_STATUS, TRANSA_DATE, SOURCE_ACC_NO, "
                        + " TOTAL_BILL_AMOUNT, NET_DPDC_AMOUNT, RESPONSE_MSG, RESPONSE_LOG, RESPONSE_LOG_BP,"
                        + " RESPONSE_MSG_BP, RESPONSE_STATUS_BP, REQUEST_PARTY_TYPE, PAYER_MOBILE_NO,"
                        + " DEBIT_CREDIT_SUCC_STATUS, DEBIT_CREDIT_SUCC_MSG FROM UTILITY_TRANSACTION U"
                        + " WHERE DEBIT_CREDIT_SUCC_STATUS = 'N' AND DEBIT_CREDIT_SUCC_MSG IS NULL"
                        + " AND SERVICE = '" + ddlOrganization.SelectedValue + "' AND TRUNC(U.TRANSA_DATE)"
                        + " BETWEEN '" + dtpDisDCFrDate.DateString + "' AND '" + dtpDisDCToDate.DateString + "'"
                        + " ORDER BY U.TRANSA_DATE DESC";
            }

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Utility_Bill_Pay";
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            string stReportHeading = ddlOrganization.SelectedItem.Text + " " + ddlStatus.SelectedItem.Text + " Report";

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>'" + stReportHeading + "'( Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisDCFrDate.DateString + "' To '" + dtpDisDCToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Utility Transaction Id</td>";
            strHTML = strHTML + "<td valign='middle' >Fis Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Number</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Month</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Year</td>";
            strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Status</td>";
            strHTML = strHTML + "<td valign='middle' >Final Status</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date </td>";
            strHTML = strHTML + "<td valign='middle' >Net Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Total Bill Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Initial Response Message</td>";
            strHTML = strHTML + "<td valign='middle' >Response Log</td>";
            strHTML = strHTML + "<td valign='middle' >Response Log BP</td>";
            strHTML = strHTML + "<td valign='middle' >Response Message BP</td>";
            strHTML = strHTML + "<td valign='middle' >Response Status BP</td>";
            strHTML = strHTML + "<td valign='middle' >Channel Name</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account Number</td>";
            strHTML = strHTML + "<td valign='middle' >Payer Mobile Number</td>";
            strHTML = strHTML + "<td valign='middle' >Debit Credit Successful Status</td>";
            strHTML = strHTML + "<td valign='middle' >Debit Credit Successful Message</td>";


            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UTILITY_TRAN_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCOUNT_NUMBER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_NUMBER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_YEAR"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_STATUS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["FINAL_STATUS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSA_DATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NET_DPDC_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_BILL_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_MSG"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_LOG"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_LOG_BP"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_MSG_BP"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_STATUS_BP"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY_TYPE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACC_NO"].ToString() + "</td>";                    
                    strHTML = strHTML + " <td > '" + prow["PAYER_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DEBIT_CREDIT_SUCC_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DEBIT_CREDIT_SUCC_MSG"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    //totalAmount = totalAmount + Convert.ToDouble(prow["TOTAL_BILL_AMOUNT"].ToString());
                }
            }

            totalAmount = System.Math.Round(totalAmount, 0);

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
            //strHTML = strHTML + " <td > " + "Total: " + " </td>";
            //strHTML = strHTML + " <td >'" + totalAmount + "</td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";
            strHTML = strHTML + " <td >'" + "" + "</td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Utility_Bill_Pay");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
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

    protected void btnDCSReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            double totalAmount = 0;
            double totalCommission = 0;
            double totalSMSCost = 0;
            double totalCommissionWithoutSMSCost = 0;
            double totalSRLCommision = 0;
            double totalMBLCommision = 0;

            //strSql = " SELECT THA.DEL_ACCNT_NO, '3RD_PARTY' PARTY_TYPE, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, "
            //        + " MDD.DISTRICT_NAME, COUNT(TM.REQUEST_ID) COUNT_BP, SUM(TM.TRANSACTION_AMOUNT) AMOUNT_BP, "
            //        + " SUM(TM.BANK_COMMISSION) BANK_COMMI_BP FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL, "
            //        + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD "
            //        + " WHERE TM.SERVICE_CODE = 'UWZP' AND TM.REQUEST_PARTY||1 = AL.ACCNT_NO AND AL.ACCNT_NO = THA.A_ACCNT_NO "
            //        + " AND AL.ACCNT_RANK_ID = '120519000000000005' AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND "
            //        + " ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID AND "
            //        + " TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDCSFrDate.DateString + "' AND '" + dtpDCSToDate.DateString + "' "
            //        + " GROUP BY THA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME "
            //        + " ORDER BY THA.DEL_ACCNT_NO ASC";

            if (ddlChannelType.SelectedValue == "1")
            {
                //strSql = " SELECT THA.DEL_ACCNT_NO, '3RD_PARTY' PARTY_TYPE, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, "
                //    + " MDD.DISTRICT_NAME, COUNT(TM.REQUEST_ID) COUNT_BP, SUM(TM.TRANSACTION_AMOUNT) AMOUNT_BP, "
                //    + " SUM(TM.BANK_COMMISSION) BANK_COMMI_BP FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL, "
                //    + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, "
                //    + " SERVICE_REQUEST SR, UTILITY_TRANSACTION UT "
                //    + " WHERE TM.SERVICE_CODE = 'UWZP' AND TM.REQUEST_PARTY||1 = AL.ACCNT_NO AND AL.ACCNT_NO = THA.A_ACCNT_NO "
                //    + " AND AL.ACCNT_RANK_ID IN ('120519000000000005', '161215000000000004', '170422000000000003', '120519000000000006') AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND "
                //    + " ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID AND "
                //    + " TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDCSFrDate.DateString + "' AND '" + dtpDCSToDate.DateString + "' "
                //    + " AND SR.REQUEST_ID = TM.REQUEST_ID AND SR.REQUEST_PARTY_TYPE IN ('WAP', 'MCOM_GATEWAY') "
                //    + " AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' "
                //    + " GROUP BY THA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME "
                //    + " ORDER BY THA.DEL_ACCNT_NO ASC";

                strSql = " SELECT AL.ACCNT_NO, AR.RANK_TITEL, MD.DISTRICT_NAME, CL.CLINT_NAME, COUNT(TM.REQUEST_ID) COUNT_BP,"
                        + " SUM(TM.TRANSACTION_AMOUNT) AMOUNT_BP, SUM(TM.BANK_COMMISSION) BANK_COMMI_BP FROM TEMP_MIS_TRANSACTIONS_REPORT_A TM,"
                        + " ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD, VW_SERVICE_REQUEST SR,"
                        + " VW_UTILITY_TRANSACTION UT WHERE TM.SERVICE_CODE = 'UWZP' AND TM.REQUEST_PARTY||1 = AL.ACCNT_NO"
                        + " AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID"
                        + " AND MT.DISTRICT_ID = MD.DISTRICT_ID"
                        + " AND AL.ACCNT_RANK_ID IN ('120519000000000005','161215000000000004','170422000000000003','120519000000000006','180128000000000008')"
                        + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDCSFrDate.DateString + "' AND '" + dtpDCSToDate.DateString + "'"
                        + " AND SR.REQUEST_ID = TM.REQUEST_ID AND SR.REQUEST_PARTY_TYPE IN ('WAP', 'MCOM_GATEWAY')"
                        + " AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' GROUP BY AL.ACCNT_NO, AR.RANK_TITEL,"
                        + " MD.DISTRICT_NAME, CL.CLINT_NAME ORDER BY AL.ACCNT_NO";
            }
            else if (ddlChannelType.SelectedValue == "2")
            {
                //strSql = " SELECT THA.DEL_ACCNT_NO, '3RD_PARTY' PARTY_TYPE, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, "
                //    + " MDD.DISTRICT_NAME, COUNT(TM.REQUEST_ID) COUNT_BP, SUM(TM.TRANSACTION_AMOUNT) AMOUNT_BP, "
                //    + " SUM(TM.BANK_COMMISSION) BANK_COMMI_BP FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL, "
                //    + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, "
                //    + " SERVICE_REQUEST SR, UTILITY_TRANSACTION UT "
                //    + " WHERE TM.SERVICE_CODE = 'UWZP' AND TM.REQUEST_PARTY||1 = AL.ACCNT_NO AND AL.ACCNT_NO = THA.A_ACCNT_NO "
                //    + " AND AL.ACCNT_RANK_ID IN ('120519000000000005', '161215000000000004', '170422000000000003', '120519000000000006') AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND "
                //    + " ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID AND "
                //    + " TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDCSFrDate.DateString + "' AND '" + dtpDCSToDate.DateString + "' "
                //    + " AND SR.REQUEST_ID = TM.REQUEST_ID AND SR.REQUEST_PARTY_TYPE IN ('AIRTEL_USSD', 'BANGLALINK_USSD', 'ROBI_USSD') "
                //    + " AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' "
                //    + " GROUP BY THA.DEL_ACCNT_NO, CLD.CLINT_NAME, CLD.CLINT_ADDRESS1, MTD.THANA_NAME, MDD.DISTRICT_NAME "
                //    + " ORDER BY THA.DEL_ACCNT_NO ASC";

                strSql = " SELECT AL.ACCNT_NO, AR.RANK_TITEL, MD.DISTRICT_NAME, CL.CLINT_NAME, COUNT(TM.REQUEST_ID) COUNT_BP,"
                        + " SUM(TM.TRANSACTION_AMOUNT) AMOUNT_BP, SUM(TM.BANK_COMMISSION) BANK_COMMI_BP FROM TEMP_MIS_TRANSACTIONS_REPORT_A TM,"
                        + " ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD, VW_SERVICE_REQUEST SR,"
                        + " VW_UTILITY_TRANSACTION UT WHERE TM.SERVICE_CODE = 'UWZP' AND TM.REQUEST_PARTY||1 = AL.ACCNT_NO"
                        + " AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID"
                        + " AND MT.DISTRICT_ID = MD.DISTRICT_ID"
                        + " AND AL.ACCNT_RANK_ID IN ('120519000000000005','161215000000000004','170422000000000003','120519000000000006','180128000000000008')"
                        + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDCSFrDate.DateString + "' AND '" + dtpDCSToDate.DateString + "'"
                        + " AND SR.REQUEST_ID = TM.REQUEST_ID AND SR.REQUEST_PARTY_TYPE IN ('AIRTEL_USSD', 'BANGLALINK_USSD', 'ROBI_USSD')"
                        + " AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' GROUP BY AL.ACCNT_NO, AR.RANK_TITEL,"
                        + " MD.DISTRICT_NAME, CL.CLINT_NAME ORDER BY AL.ACCNT_NO";
            } 
            

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Bill_Pay_Commission";
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Commission Statement WEST ZONE('" + ddlChannelType.SelectedItem.Text + "')</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDCSFrDate.DateString + "' To '" + dtpDCSToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Number</td>";
            strHTML = strHTML + "<td valign='middle' >Account Rank</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >Account Name</td>";
            strHTML = strHTML + "<td valign='middle' >Total No of BIll Pay</td>";
            strHTML = strHTML + "<td valign='middle' >Total BIll Pay Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission (00000000000019)</td>";
            strHTML = strHTML + "<td valign='middle' >SMS Cost(0.50)</td>";
            strHTML = strHTML + "<td valign='middle' >Amount after SMS Cost</td>";
            strHTML = strHTML + "<td valign='middle' >SRL Commision Rate(50%)</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission(50%)</td>";

            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    double smsCost = Convert.ToDouble(prow["COUNT_BP"]) * 0.5;
                    double commissionWithoutSMSCost = Convert.ToDouble(prow["BANK_COMMI_BP"]) - smsCost;
                    double commision = commissionWithoutSMSCost / 2;

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["COUNT_BP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + Convert.ToDouble(prow["AMOUNT_BP"]).ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > '" + Convert.ToDouble(prow["BANK_COMMI_BP"]).ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > '" + smsCost.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > '" + commissionWithoutSMSCost.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > '" + commision.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > '" + commision.ToString("N2") + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    totalAmount = totalAmount + Convert.ToDouble(prow["AMOUNT_BP"].ToString());
                    totalSMSCost = totalSMSCost + smsCost;
                    totalCommissionWithoutSMSCost = totalCommissionWithoutSMSCost + commissionWithoutSMSCost;
                    totalCommission = totalCommission + Convert.ToDouble(prow["BANK_COMMI_BP"].ToString());
                    totalSRLCommision = totalSRLCommision + commision;
                    totalMBLCommision = totalMBLCommision + commision;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "Total" + " </td>";
            strHTML = strHTML + " <td > " + totalAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + totalCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + totalSMSCost.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + totalCommissionWithoutSMSCost.ToString("N2") + " </td>";
            strHTML = strHTML + " <td >'" + totalSRLCommision.ToString("N2") + "</td>";
            strHTML = strHTML + " <td >'" + totalMBLCommision.ToString("N2") + "</td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Utility_Bill_Pay");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    protected void btnARISearch_Click(object sender, EventArgs e)
    {
        string strSql = "";
        string strSqlD = "";

        double dblCnCount = 0;
        double dblCnAmount = 0;
        double dblCctCount = 0;
        double dblCctAmount = 0;

        try
        {
            #region old query

            //strSql = "SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO,"
            //        + " CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME,"
            //        + " THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRESS,"
            //        + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO,"
            //        + " CLDSE.CLINT_NAME DSE_NAME, TM_AG.AGENT_NO, TM_AG.AGENT_NAME, TM_AG.AGT_ADDRESS,"
            //        + " TM_AG.AGENT_THANA, TM_AG.AGENT_DISTRICT, TM_AG.ID_CARD_NO, TM_AG.ID_TYPE,"
            //        + " TM_AG.AGT_VRFY_DATE, TM_AG.AGENT_MOBILE_NO, TM_AG.AGENT_BALANCE, TM_AG.AGT_RANK,"
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,"
            //        + " TO_CHAR(TO_DATE('" + dtpARIFrDate.DateString + "')),TO_CHAR(TO_DATE("
            //        + " '" + dtpARIToDate.DateString + "')),'CN')NO_OF_CASIN,"
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,"
            //        + " TO_CHAR(TO_DATE('" + dtpARIFrDate.DateString + "')),TO_CHAR(TO_DATE("
            //        + " '" + dtpARIToDate.DateString + "')),'CN')CASIN_AMT,"
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (TM_AG.AGENT_NO,"
            //        + " TO_CHAR(TO_DATE('" + dtpARIFrDate.DateString + "')),TO_CHAR(TO_DATE("
            //        + " '" + dtpARIToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT,"
            //        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (TM_AG.AGENT_NO,"
            //        + " TO_CHAR(TO_DATE('" + dtpARIFrDate.DateString + "')), TO_CHAR(TO_DATE("
            //        + " '" + dtpARIToDate.DateString + "')),'CCT,SW')CASOUT_AMT"
            //        + " FROM (SELECT DISTINCT ALA.ACCNT_ID AGENT_ACC_ID, ALA.ACCNT_NO AGENT_NO,"
            //        + " CLA.CLINT_NAME AGENT_NAME, CLA.CLINT_ADDRESS1 AGT_ADDRESS, MTA.THANA_NAME AGENT_THANA,"
            //        + " MDA.DISTRICT_NAME AGENT_DISTRICT, CID.CLINT_IDENT_NAME ID_CARD_NO,"
            //        + " IDS.IDNTIFCTION_NAME ID_TYPE, CLA.VERIFIED_DATE AGT_VRFY_DATE, ALA.ACCNT_MSISDN"
            //        + " AGENT_MOBILE_NO, CAB.CAS_ACCNT_BALANCE  AGENT_BALANCE, ARA.RANK_TITEL AGT_RANK"
            //        + " FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,"
            //        + " BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB,"
            //        + " ACCOUNT_RANK ARA, MANAGE_THANA MTA, MANAGE_DISTRICT MDA, CLIENT_IDENTIFICATION CID,"
            //        + " IDENTIFICATION_SETUP IDS WHERE ALA.CLINT_ID = CLA.CLINT_ID(+) AND"
            //        + " ALA.ACCNT_RANK_ID = '120519000000000005' AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID(+)"
            //        + " AND ALA.ACCNT_NO = CAL.CAS_ACC_NO(+) AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID(+) AND"
            //        + " CAL.CAS_ACC_ID = CAT.CAS_ACC_ID(+) AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpARIFrDate.DateString + "'"
            //        + " AND '" + dtpARIToDate.DateString + "' AND CLA.THANA_ID = MTA.THANA_ID(+) AND MTA.DISTRICT_ID ="
            //        + " MDA.DISTRICT_ID(+) AND CLA.CLINT_ID = CID.CLIENT_ID(+) AND CID.IDNTIFCTION_ID ="
            //        + " IDS.IDNTIFCTION_ID(+) AND CAT.ACCESS_CODE IN ('CN', 'CCT', 'SW') ORDER BY"
            //        + " ALA.ACCNT_NO ASC) TM_AG, TEMP_HIERARCHY_LIST_ALL THA,  CLIENT_LIST CLD,"
            //        + " ACCOUNT_LIST ALD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, MANAGE_TERRITORY_HIERARCHY MTHD,"
            //        + " ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO, ACCOUNT_LIST ALTM,"
            //        + " CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR,"
            //        + " ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE WHERE TM_AG.AGENT_NO = THA.A_ACCNT_NO(+)"
            //        + " AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND"
            //        + " CLD.THANA_ID = MTD.THANA_ID(+)  AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND"
            //        + " ALD.ACCNT_ID = MTHD.ACCNT_ID(+) AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+)"
            //        + " AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND ALTO.ACCNT_ID = MTHTO.ACCNT_ID(+) AND"
            //        + " MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+)  AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)"
            //        + " AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID ="
            //        + " MR.REGION_ID(+) AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO(+) AND ALDSE.CLINT_ID ="
            //        + " CLDSE.CLINT_ID(+)  ORDER BY ALTO.ACCNT_NO DESC ";

            #endregion 

            strSqlD = objServiceHandler.ExecuteProcedure("APSNG101.PRO_AGENT_TRX_REPORT_BBANK");
            if (strSqlD != "Successfull.")
            {
                lblMsg.Text = "Report Generation Failed";
                return;
            }

            Thread.Sleep(5000);

            strSql = " SELECT DISTINCT AGT_NO, AGT_ACC_NO, AGT_NAME, AGT_ADDR, AGT_ACC_MSISDN, "
                    + " AGT_THANA_NAME, AGT_DISTRICT, DIS_NO, DIS_NAME, DIS_DISTRICT, DSE_NO, "
                    + " CN_COUNT, CN_AMOUNT, CCT_COUNT, CCT_AMOUNT FROM TEMP_AGENT_TRX_RPT_BANK ";


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Agent_Related_Information";
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MBL Agent Transaction Report of Previous Month For Bangladesh Bank (Monthly Report) Print Date : " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpARIFrDate.DateString + "' To '" + dtpARIToDate.DateString + "'</h2></td></tr>";
            //strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account Number</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Account Number</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Address</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Agent District</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Mobile Number</td>";
            strHTML = strHTML + "<td valign='middle' >Number of Cashin</td>";
            strHTML = strHTML + "<td valign='middle' >Total Cashin Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Number of Cashout</td>";
            strHTML = strHTML + "<td valign='middle' >Total Cashout Amount</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_ADDR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_THANA_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_ACC_MSISDN"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_AMOUNT"].ToString() + "</td>";


                    dblCnCount = dblCnCount + double.Parse(prow["CN_COUNT"].ToString());
                    dblCnAmount = dblCnAmount + double.Parse(prow["CN_AMOUNT"].ToString());
                    dblCctCount = dblCctCount + double.Parse(prow["CCT_COUNT"].ToString());
                    dblCctAmount = dblCctAmount + double.Parse(prow["CCT_AMOUNT"].ToString());

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
            strHTML = strHTML + " <td > " + "Total: " + " </td>";
            strHTML = strHTML + " <td > " + dblCnCount + " </td>";
            strHTML = strHTML + " <td > " + dblCnAmount + " </td>";
            strHTML = strHTML + " <td > " + dblCctCount + " </td>";
            strHTML = strHTML + " <td > " + dblCctAmount + " </td>";
            
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Utility_Bill_Pay");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnDRISearch_Click(object sender, EventArgs e)
    {
        string strSql = "";
        try
        {

            double dblCnCount = 0;
            double dblCnAmount = 0;
            double dblCctCount = 0;
            double dblCctAmount = 0;


            #region old query

            //strSql = "SELECT DISTINCT TM_D.DIS_NO, TM_D.DISTRIBUTOR_NAME, TM_D.DISTRIBUTOR_ADD, MTD.THANA_NAME DIS_THANA,"
            //        + " MDD.DISTRICT_NAME DIS_DISTRICT, ALD.ACCNT_MSISDN DIS_MOBILE_NO, TM_D.RANK_TITEL,"
            //        + " CID.CLINT_IDENT_NAME ID_TYPE_NO, IDS.IDNTIFCTION_NAME ID_TYPE, TM_D.CN_COUNT, TM_D.CN_AMOUNT,"
            //        + " TM_D.CCT_COUNT, TM_D.CCT_AMOUNT, COUNT(THA.A_ACCNT_NO) COUNT_AGT FROM ( SELECT DISTINCT DIS_NO,"
            //        + " DISTRIBUTOR_NAME, DISTRIBUTOR_ADD, RANK_TITEL, SUM(CN_COUNT) CN_COUNT, SUM(CN_AMOUNT) CN_AMOUNT,"
            //        + " SUM(CCT_COUNT) CCT_COUNT, SUM(CCT_AMOUNT) CCT_AMOUNT  FROM VW_DISTRIBUTION_REPORT2 VW WHERE"
            //        + " TRUNC(VW.TRANSACTION_DATE) BETWEEN '" + dtpDRIFrDate.DateString + "' AND '" + dtpDRIToDate.DateString + "'"
            //        + " GROUP BY DIS_NO, DISTRIBUTOR_NAME, DISTRIBUTOR_ADD, RANK_TITEL ORDER BY DIS_NO ASC)  TM_D,"
            //        + " ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, CLIENT_IDENTIFICATION CID,"
            //        + " IDENTIFICATION_SETUP IDS, TEMP_HIERARCHY_LIST_ALL THA WHERE TM_D.DIS_NO = ALD.ACCNT_NO AND"
            //        + " ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID"
            //        + " AND CLD.CLINT_ID = CID.CLIENT_ID AND CID.IDNTIFCTION_ID = IDS.IDNTIFCTION_ID AND TM_D.DIS_NO ="
            //        + " THA.DEL_ACCNT_NO GROUP BY TM_D.DIS_NO, TM_D.DISTRIBUTOR_NAME, TM_D.DISTRIBUTOR_ADD, MTD.THANA_NAME,"
            //        + " MDD.DISTRICT_NAME, ALD.ACCNT_MSISDN, TM_D.RANK_TITEL, CID.CLINT_IDENT_NAME, IDS.IDNTIFCTION_NAME,"
            //        + " TM_D.CN_COUNT, TM_D.CN_AMOUNT, TM_D.CCT_COUNT, TM_D.CCT_AMOUNT ORDER BY TM_D.DIS_NO ASC";

            #endregion

            strSql = " SELECT DISTINCT T2.DIS_NO, T2.DIS_CLINT_ID, T2.DISTRIBUTOR_NAME, T2.DISTRIBUTOR_ADD, "
                    + " T2.DISTRICT_NAME, T2.DIS_MOBILE_NO, T2.CN_COUNT, T2.CN_AMOUNT, T2.CCT_COUNT, T2.CCT_AMOUNT, "
                    + " T2.AGT_COUNT FROM ( SELECT DISTINCT T1.DIS_NO, CLD.CLINT_ID DIS_CLINT_ID, T1.DISTRIBUTOR_NAME, "
                    + " T1.DISTRIBUTOR_ADD, T1.DISTRICT_NAME, ALD.ACCNT_MSISDN DIS_MOBILE_NO, T1.CN_COUNT, T1.CN_AMOUNT, "
                    + " T1.CCT_COUNT, T1.CCT_AMOUNT, COUNT(THAD.A_ACCNT_NO) AGT_COUNT FROM ( SELECT DISTINCT DIS_NO, "
                    + " DISTRIBUTOR_NAME, DISTRIBUTOR_ADD, DISTRICT_NAME, SUM (CN_COUNT) CN_COUNT,  SUM (CN_AMOUNT) CN_AMOUNT, "
                    + " SUM (CCT_COUNT) CCT_COUNT,   SUM (CCT_AMOUNT) CCT_AMOUNT  FROM VW_DISTRIBUTION_REPORT2 VDR, "
                    + " (SELECT TRAN2.DEL_ACCNT_NO DIS_ACC_NO, TRAN2.REG_CNT REG_COUNT, MD.DISTRICT_NAME "
                    + " FROM (SELECT TRAN.DEL_ACCNT_NO,SUM(CT) REG_CNT  FROM   (SELECT THL.DEL_ACCNT_NO,ASD.AGENT_MOBILE_NO, "
                    + " COUNT (ASD.CUSTOMER_MOBILE_NO) CT FROM ACCOUNT_SERIAL_DETAIL ASD,   TEMP_HIERARCHY_LIST_ALL THL "
                    + " WHERE TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpDRIFrDate.DateString + "' AND '" + dtpDRIToDate.DateString + "' "
                    + " AND SUBSTR(('+88'||THL.A_ACCNT_NO),0,14)=ASD.AGENT_MOBILE_NO GROUP BY ASD.AGENT_MOBILE_NO,THL.DEL_ACCNT_NO) TRAN "
                    + " GROUP BY TRAN.DEL_ACCNT_NO) TRAN2, CLIENT_LIST CL ,MANAGE_THANA MT, MANAGE_DISTRICT MD "
                    + " WHERE TRAN2.DEL_ACCNT_NO= SUBSTR(CL.CLINT_MOBILE,4,14)||'1'  AND CL.THANA_ID=MT.THANA_ID(+) "
                    + " AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) ) TEMP WHERE VDR.TRANSACTION_DATE "
                    + " BETWEEN '" + dtpDRIFrDate.DateString + "' AND '" + dtpDRIToDate.DateString + "' AND DIS_NO = TEMP.DIS_ACC_NO(+) "
                    + " GROUP BY DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, REG_COUNT, DISTRICT_NAME "
                    + " ORDER BY DIS_NO ) T1, TEMP_HIERARCHY_LIST_ALL THAD, ACCOUNT_LIST ALD, CLIENT_LIST CLD "
                    + " WHERE T1.DIS_NO = THAD.DEL_ACCNT_NO AND THAD.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID "
                    + " GROUP BY T1.DIS_NO, CLD.CLINT_ID, T1.DISTRIBUTOR_NAME, T1.DISTRIBUTOR_ADD, T1.DISTRICT_NAME, ALD.ACCNT_MSISDN, "
                    + " T1.CN_COUNT, T1.CN_AMOUNT, T1.CCT_COUNT, T1.CCT_AMOUNT ) T2 ORDER BY T2.DIS_NO ASC ";


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Distributor_Related_Information";
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Related Information For Bangladesh Bank (Monthly Report) Print Date : " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDRIFrDate.DateString + "' To '" + dtpDRIToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account Number</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Mobile Number</td>";
            strHTML = strHTML + "<td valign='middle' >Number of Cashin</td>";
            strHTML = strHTML + "<td valign='middle' >Total Cashin Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Number of Cashout</td>";
            strHTML = strHTML + "<td valign='middle' >Total Cashout Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Count</td>";

            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_ADD"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_COUNT"].ToString() + "</td>";

                    dblCnCount = dblCnCount + double.Parse(prow["CN_COUNT"].ToString());
                    dblCnAmount = dblCnAmount + double.Parse(prow["CN_AMOUNT"].ToString());
                    dblCctCount = dblCctCount + double.Parse(prow["CCT_COUNT"].ToString());
                    dblCctAmount = dblCctAmount + double.Parse(prow["CCT_AMOUNT"].ToString());

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
            strHTML = strHTML + " <td > " + "Total: " + " </td>";
            strHTML = strHTML + " <td >'" + dblCnCount + "</td>";
            strHTML = strHTML + " <td >'" + dblCnAmount + "</td>";
            strHTML = strHTML + " <td >'" + dblCctCount + "</td>";
            strHTML = strHTML + " <td >'" + dblCctAmount + "</td>";
            
            strHTML = strHTML + " <td >'" + "" + "</td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Utility_Bill_Pay");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    protected void btnDisList_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = objServiceHandler.ExecuteProcedure("APSNG101.PRO_DIST_ACC_INFO");
            if (strSql == "Successfull.")
            {
                Thread.Sleep(5000);

                string strSqlD = "";
                strSqlD = "SELECT DIST_ACC_NO, ID_CARD_NO, ID_CARD_TYPE FROM TEMP_DISTR_ACC_INFO";

                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "DistributorInfo_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSqlD);

                //strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                //strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                //strHTML = strHTML + "</table>";
                //strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                //strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                //strHTML = strHTML + "</table>";
                //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                //strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Customer A/C Approve and KYC Update Summary Report (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                //strHTML = strHTML + "</table>";
                //strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor ID Card No</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor ID Card Type</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ID_CARD_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ID_CARD_TYPE"].ToString() + " </td>";
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

                SaveAuditInfo("Preview", "DistributorInfo_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }

        }

        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    protected void btnAgtInfo_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT TMP.AGT_NO, ALA.ACCNT_NO, CLA.CLINT_NAME AGT_NAME, "
                    + " CID.CLINT_IDENT_NAME ID_CARD_NO, IDS.IDNTIFCTION_NAME ID_CARD_TYPE "
                    + " FROM ( SELECT DISTINCT TM1.REQUEST_PARTY||1 AGT_NO FROM TEMP_MIS_TRANSACTIONS_REPORT TM1 "
                    + " WHERE TM1.SERVICE_CODE IN ('CN') AND TRUNC(TM1.TRANSACTION_DATE) "
                    + " BETWEEN '" + dtpARIFrDate.DateString + "' AND '" + dtpARIToDate.DateString + "' "
                    + " UNION SELECT DISTINCT TM2.RECEPENT_PARTY FROM TEMP_MIS_TRANSACTIONS_REPORT TM2 "
                    + " WHERE TM2.SERVICE_CODE IN ('CCT', 'SW') AND TRUNC(TM2.TRANSACTION_DATE) "
                    + " BETWEEN '" + dtpARIFrDate.DateString + "' AND '" + dtpARIToDate.DateString + "' ) TMP, "
                    + " ACCOUNT_LIST ALA, CLIENT_LIST CLA, CLIENT_IDENTIFICATION CID, IDENTIFICATION_SETUP IDS "
                    + " WHERE TMP.AGT_NO = ALA.ACCNT_NO AND ALA.ACCNT_RANK_ID IN ('120519000000000005','180128000000000008') "
                    + " AND ALA.CLINT_ID = CLA.CLINT_ID AND CLA.CLINT_ID = CID.CLIENT_ID "
                    + " AND CID.IDNTIFCTION_ID = IDS.IDNTIFCTION_ID ORDER BY TMP.AGT_NO ASC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "AgentInfo_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            //strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            //strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            //strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Customer A/C Approve and KYC Update Summary Report (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            //strHTML = strHTML + "</table>";
            //strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Agent MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent ID Card No</td>";
            strHTML = strHTML + "<td valign='middle' >Agent ID Card Type</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ID_CARD_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ID_CARD_TYPE"].ToString() + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "AgentInfo_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");




        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }

    }


    protected void btnARISearchOld_Click(object sender, EventArgs e)
    {
        string strSql = "";
        string strSqlD = "";

        double dblCnCount = 0;
        double dblCnAmount = 0;
        double dblCctCount = 0;
        double dblCctAmount = 0;

        try
        {
            

            strSql = " SELECT DISTINCT AGT_NO, AGT_ACC_NO, AGT_NAME, AGT_ADDR, AGT_ACC_MSISDN, "
                    + " AGT_THANA_NAME, AGT_DISTRICT, DIS_NO, DIS_NAME, DIS_DISTRICT, DSE_NO, "
                    + " CN_COUNT, CN_AMOUNT, CCT_COUNT, CCT_AMOUNT FROM TEMP_AGENT_TRX_RPT_BANK ";


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Agent_Related_Information";
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MBL Agent Transaction Report of Previous Month For Bangladesh Bank (Monthly Report) Print Date : " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=23 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpARIFrDate.DateString + "' To '" + dtpARIToDate.DateString + "'</h2></td></tr>";
            //strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account Number</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Account Number</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Address</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Agent District</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Mobile Number</td>";
            strHTML = strHTML + "<td valign='middle' >Number of Cashin</td>";
            strHTML = strHTML + "<td valign='middle' >Total Cashin Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Number of Cashout</td>";
            strHTML = strHTML + "<td valign='middle' >Total Cashout Amount</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_ADDR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_THANA_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_ACC_MSISDN"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_AMOUNT"].ToString() + "</td>";


                    dblCnCount = dblCnCount + double.Parse(prow["CN_COUNT"].ToString());
                    dblCnAmount = dblCnAmount + double.Parse(prow["CN_AMOUNT"].ToString());
                    dblCctCount = dblCctCount + double.Parse(prow["CCT_COUNT"].ToString());
                    dblCctAmount = dblCctAmount + double.Parse(prow["CCT_AMOUNT"].ToString());

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
            strHTML = strHTML + " <td > " + "Total: " + " </td>";
            strHTML = strHTML + " <td > " + dblCnCount + " </td>";
            strHTML = strHTML + " <td > " + dblCnAmount + " </td>";
            strHTML = strHTML + " <td > " + dblCctCount + " </td>";
            strHTML = strHTML + " <td > " + dblCctAmount + " </td>";

            strHTML = strHTML + " </tr>";

            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Utility_Bill_Pay");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnDCCReport_Click(object sender, EventArgs e)
    {
        // Date Convert For First Month
        string dtpfmFromDate = "";
        string dtpfmToDate = "";

        if (ddlFirstMonth.SelectedValue == "Jan")
        {
            dtpfmFromDate = "01/Jan/";
            dtpfmToDate = "31/Jan/";
        }
        else if (ddlFirstMonth.SelectedValue == "Feb")
        {
            if ((Convert.ToInt32(ddlFirstYear.SelectedValue) % 4) == 0)
            {
                dtpfmFromDate = "01/Feb/";
                dtpfmToDate = "29/Feb/";
            }
            else
            {
                dtpfmFromDate = "01/Feb/";
                dtpfmToDate = "28/Feb/";
            }
        }
        else if (ddlFirstMonth.SelectedValue == "Mar")
        {
            dtpfmFromDate = "01/Mar/";
            dtpfmToDate = "31/Mar/";
        }
        else if (ddlFirstMonth.SelectedValue == "Apr")
        {
            dtpfmFromDate = "01/Apr/";
            dtpfmToDate = "30/Apr/";
        }
        else if (ddlFirstMonth.SelectedValue == "May")
        {
            dtpfmFromDate = "01/May/";
            dtpfmToDate = "31/May/";
        }
        else if (ddlFirstMonth.SelectedValue == "Jun")
        {
            dtpfmFromDate = "01/Jun/";
            dtpfmToDate = "30/Jun/";
        }
        else if (ddlFirstMonth.SelectedValue == "Jul")
        {
            dtpfmFromDate = "01/Jul/";
            dtpfmToDate = "31/Jul/";
        }
        else if (ddlFirstMonth.SelectedValue == "Aug")
        {
            dtpfmFromDate = "01/Aug/";
            dtpfmToDate = "31/Aug/";
        }
        else if (ddlFirstMonth.SelectedValue == "Sep")
        {
            dtpfmFromDate = "01/Sep/";
            dtpfmToDate = "30/Sep/";
        }
        else if (ddlFirstMonth.SelectedValue == "Oct")
        {
            dtpfmFromDate = "01/Oct/";
            dtpfmToDate = "31/Oct/";
        }
        else if (ddlFirstMonth.SelectedValue == "Nov")
        {
            dtpfmFromDate = "01/Nov/";
            dtpfmToDate = "30/Nov/";
        }
        else if (ddlFirstMonth.SelectedValue == "Dec")
        {
            dtpfmFromDate = "01/Dec/";
            dtpfmToDate = "31/Dec/";
        }
        else
        {
            // do nothing
        }

        //Date Convert For Second Month
        string dtpsmFromDate = "";
        string dtpsmToDate = "";

        if (ddlSecondMonth.SelectedValue == "Jan")
        {
            dtpsmFromDate = "01/Jan/";
            dtpsmToDate = "31/Jan/";
        }
        else if (ddlSecondMonth.SelectedValue == "Feb")
        {
            if ((Convert.ToInt32(ddlSecondYear.SelectedValue) % 4) == 0)
            {
                dtpsmFromDate = "01/Feb/";
                dtpsmToDate = "29/Feb/";
            }
            else
            {
                dtpsmFromDate = "01/Feb/";
                dtpsmToDate = "28/Feb/";
            }
        }
        else if (ddlSecondMonth.SelectedValue == "Mar")
        {
            dtpsmFromDate = "01/Mar/";
            dtpsmToDate = "31/Mar/";
        }
        else if (ddlSecondMonth.SelectedValue == "Apr")
        {
            dtpsmFromDate = "01/Apr/";
            dtpsmToDate = "30/Apr/";
        }
        else if (ddlSecondMonth.SelectedValue == "May")
        {
            dtpsmFromDate = "01/May/";
            dtpsmToDate = "31/May/";
        }
        else if (ddlSecondMonth.SelectedValue == "Jun")
        {
            dtpsmFromDate = "01/Jun/";
            dtpsmToDate = "30/Jun/";
        }
        else if (ddlSecondMonth.SelectedValue == "Jul")
        {
            dtpsmFromDate = "01/Jul/";
            dtpsmToDate = "31/Jul/";
        }
        else if (ddlSecondMonth.SelectedValue == "Aug")
        {
            dtpsmFromDate = "01/Aug/";
            dtpsmToDate = "31/Aug/";
        }
        else if (ddlSecondMonth.SelectedValue == "Sep")
        {
            dtpsmFromDate = "01/Sep/";
            dtpsmToDate = "30/Sep/";
        }
        else if (ddlSecondMonth.SelectedValue == "Oct")
        {
            dtpsmFromDate = "01/Oct/";
            dtpsmToDate = "31/Oct/";
        }
        else if (ddlSecondMonth.SelectedValue == "Nov")
        {
            dtpsmFromDate = "01/Nov/";
            dtpsmToDate = "30/Nov/";
        }
        else if (ddlSecondMonth.SelectedValue == "Dec")
        {
            dtpsmFromDate = "01/Dec/";
            dtpsmToDate = "31/Dec/";
        }
        else
        {
            // do nothing
        }

        //Date Convert For Third Month
        string dtptmFromDate = "";
        string dtptmToDate = "";

        if (ddlThirdMonth.SelectedValue == "Jan")
        {
            dtptmFromDate = "01/Jan/";
            dtptmToDate = "31/Jan/";
        }
        else if (ddlThirdMonth.SelectedValue == "Feb")
        {
            if ((Convert.ToInt32(ddlThirdYear.SelectedValue) % 4) == 0)
            {
                dtptmFromDate = "01/Feb/";
                dtptmToDate = "29/Feb/";
            }
            else
            {
                dtptmFromDate = "01/Feb/";
                dtptmToDate = "28/Feb/";
            }
        }
        else if (ddlThirdMonth.SelectedValue == "Mar")
        {
            dtptmFromDate = "01/Mar/";
            dtptmToDate = "31/Mar/";
        }
        else if (ddlThirdMonth.SelectedValue == "Apr")
        {
            dtptmFromDate = "01/Apr/";
            dtptmToDate = "30/Apr/";
        }
        else if (ddlThirdMonth.SelectedValue == "May")
        {
            dtptmFromDate = "01/May/";
            dtptmToDate = "31/May/";
        }
        else if (ddlThirdMonth.SelectedValue == "Jun")
        {
            dtptmFromDate = "01/Jun/";
            dtptmToDate = "30/Jun/";
        }
        else if (ddlThirdMonth.SelectedValue == "Jul")
        {
            dtptmFromDate = "01/Jul/";
            dtptmToDate = "31/Jul/";
        }
        else if (ddlThirdMonth.SelectedValue == "Aug")
        {
            dtptmFromDate = "01/Aug/";
            dtptmToDate = "31/Aug/";
        }
        else if (ddlThirdMonth.SelectedValue == "Sep")
        {
            dtptmFromDate = "01/Sep/";
            dtptmToDate = "30/Sep/";
        }
        else if (ddlThirdMonth.SelectedValue == "Oct")
        {
            dtptmFromDate = "01/Oct/";
            dtptmToDate = "31/Oct/";
        }
        else if (ddlThirdMonth.SelectedValue == "Nov")
        {
            dtptmFromDate = "01/Nov/";
            dtptmToDate = "30/Nov/";
        }
        else if (ddlThirdMonth.SelectedValue == "Dec")
        {
            dtptmFromDate = "01/Dec/";
            dtptmToDate = "31/Dec/";
        }
        else
        {
            // do nothing
        }

        dtpfmFromDate = dtpfmFromDate + ddlFirstYear.SelectedValue;
        dtpfmToDate = dtpfmToDate + ddlFirstYear.SelectedValue;
        dtpsmFromDate = dtpsmFromDate + ddlSecondYear.SelectedValue;
        dtpsmToDate = dtpsmToDate + ddlSecondYear.SelectedValue;
        dtptmFromDate = dtptmFromDate + ddlThirdYear.SelectedValue;
        dtptmToDate = dtptmToDate + ddlThirdYear.SelectedValue;

        string strSql = "";
        try
        {
            strSql = "SELECT  T.AS1, T.AS2, T.AS3, T.AS4, T.AS5, T.AS6, T.REGION_NAME, T.AREA_NAME, T.TM_NO,"
                    + " T.TM_NAME, T.TO_NO, T.TO_NAME, T.DIS_NO, T.DIS_STATE, T.DIS_NAME, T.DIS_ADDR,"
                    + " T.DIS_THANA, T.DIS_DISTRICT,"

                    + " APSNG101.FUNC_DISTRIBUTOR_DTWISE_CNSUM (T.DIS_NO, to_date('" + dtpfmFromDate + "'),"
                    + " to_date('" + dtpfmToDate + "')) CN_SUM_AMT_1ST_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_CCT_SUM (T.DIS_NO, to_date('" + dtpfmFromDate + "'),"
                    + " to_date('" + dtpfmToDate + "')) CCT_SUM_AMT_1ST_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_BD_SUM (T.DIS_NO, to_date('" + dtpfmFromDate + "'),"
                    + " to_date('" + dtpfmToDate + "')) BD_SUM_AMT_1ST_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_UBP_SUM (T.DIS_NO, to_date('" + dtpfmFromDate + "'),"
                    + " to_date('" + dtpfmToDate + "')) UBP_SUM_AMT_1ST_MONTH,"
                    + " APSNG101.FNC_DIST_DTWISE_CRP_COLL_SUM (T.DIS_NO, to_date('" + dtpfmFromDate + "'),"
                    + " to_date('" + dtpfmToDate + "')) CORP_COLL_SUM_AMT_1ST_MONTH,"

                    + " APSNG101.FUNC_DISTRIBUTOR_DTWISE_CNSUM (T.DIS_NO, to_date('" + dtpsmFromDate + "'),"
                    + " to_date('" + dtpsmToDate + "')) CN_SUM_AMT_2ND_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_CCT_SUM (T.DIS_NO, to_date('" + dtpsmFromDate + "'),"
                    + " to_date('" + dtpsmToDate + "')) CCT_SUM_AMT_2ND_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_BD_SUM (T.DIS_NO, to_date('" + dtpsmFromDate + "'),"
                    + " to_date('" + dtpsmToDate + "')) BD_SUM_AMT_2ND_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_UBP_SUM (T.DIS_NO, to_date('" + dtpsmFromDate + "'),"
                    + " to_date('" + dtpsmToDate + "')) UBP_SUM_AMT_2ND_MONTH,"
                    + " APSNG101.FNC_DIST_DTWISE_CRP_COLL_SUM (T.DIS_NO, to_date('" + dtpsmFromDate + "'),"
                    + " to_date('" + dtpsmToDate + "')) CORP_COLL_SUM_AMT_2ND_MONTH,"

                    + " APSNG101.FUNC_DISTRIBUTOR_DTWISE_CNSUM (T.DIS_NO, to_date('" + dtptmFromDate + "'),"
                    + " to_date('" + dtptmToDate + "')) CN_SUM_AMT_3RD_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_CCT_SUM (T.DIS_NO, to_date('" + dtptmFromDate + "'),"
                    + " to_date('" + dtptmToDate + "')) CCT_SUM_AMT_3RD_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_BD_SUM (T.DIS_NO, to_date('" + dtptmFromDate + "'),"
                    + " to_date('" + dtptmToDate + "')) BD_SUM_AMT_3RD_MONTH,"
                    + " APSNG101.FNC_DISTRIBUTOR_DTWISE_UBP_SUM (T.DIS_NO, to_date('" + dtptmFromDate + "'),"
                    + " to_date('" + dtptmToDate + "')) UBP_SUM_AMT_3RD_MONTH,"
                    + " APSNG101.FNC_DIST_DTWISE_CRP_COLL_SUM (T.DIS_NO, to_date('" + dtptmFromDate + "'),"
                    + " to_date('" + dtptmToDate + "')) CORP_COLL_AMT_3RD_MONTH"

                    + " FROM ( SELECT DISTINCT '" + dtpfmFromDate + "' AS1, '" + dtpfmToDate + "'"
                    + " AS2, '" + dtpsmFromDate + "' AS3, '" + dtpsmToDate + "' AS4,"
                    + " '" + dtptmFromDate + "' AS5, '" + dtptmToDate + "' AS6, MR.REGION_NAME,"
                    + " MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO,"
                    + " CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, ALD.ACCNT_STATE DIS_STATE,"
                    + " CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA,"
                    + " MDD.DISTRICT_NAME DIS_DISTRICT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD,"
                    + " CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, MANAGE_TERRITORY_HIERARCHY MTH,"
                    + " ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTM, ACCOUNT_LIST ALTM,"
                    + " CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA,"
                    + " MANAGE_REGION MR WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.ACCNT_STATE = 'A' AND"
                    + " ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+) AND"
                    + " MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALD.ACCNT_ID = MTH.ACCNT_ID(+) AND"
                    + " MTH.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND"
                    + " ALTO.ACCNT_ID = MTHTM.ACCNT_ID(+) AND MTHTM.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND"
                    + " ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND"
                    + " MTA.AREA_ID = MA.AREA_ID(+) AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY"
                    + " THA.DEL_ACCNT_NO ASC) T";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Distributor_Commission_Comparison";
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=34 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=34 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Commision Comparison Report Print Date : " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=34 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpARIFrDate.DateString + "' To '" + dtpARIToDate.DateString + "'</h2></td></tr>";
            //strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Region</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >TM Number</td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Number</td>";
            strHTML = strHTML + "<td valign='middle' >Territory Officer  Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor State</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Company Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >First Month From Date</td>";
            strHTML = strHTML + "<td valign='middle' >First Month To Date</td>";
            strHTML = strHTML + "<td valign='middle' >Cash In Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Out Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Bank Deposite Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Utility Bill Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Collection Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Second Month From Date</td>";
            strHTML = strHTML + "<td valign='middle' >Second Month To Date</td>";
            strHTML = strHTML + "<td valign='middle' >Cash In Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Out Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Bank Deposite Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Utility Bill Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Collection Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Third Month From Date</td>";
            strHTML = strHTML + "<td valign='middle' >Third Month To Date</td>";
            strHTML = strHTML + "<td valign='middle' >Cash In Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Out Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Bank Deposite Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Utility Bill Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Collection Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_STATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AS1"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AS2"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_SUM_AMT_1ST_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_SUM_AMT_1ST_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BD_SUM_AMT_1ST_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_SUM_AMT_1ST_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_SUM_AMT_1ST_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AS3"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AS4"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_SUM_AMT_2ND_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_SUM_AMT_2ND_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BD_SUM_AMT_2ND_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_SUM_AMT_2ND_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_SUM_AMT_2ND_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AS5"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AS6"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_SUM_AMT_3RD_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_SUM_AMT_3RD_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BD_SUM_AMT_3RD_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_SUM_AMT_3RD_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_AMT_3RD_MONTH"].ToString() + "</td>";


                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";

            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Distributor_Commission_Comparison");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


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
                    + " ACCOUNT_LIST ALA WHERE TM.SERVICE_CODE IN ('CN', 'CCT', 'SW', 'BD', 'FM', 'UBP', 'UBPW') "
                    + " AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpTmToAgtFromDate.DateString + "' AND '" + dtpTmToAgtToDate.DateString + "' "
                    + " AND ((ALA.ACCNT_NO = TM.REQUEST_PARTY||1) OR (ALA.ACCNT_NO =  TM.RECEPENT_PARTY)) AND ALA.ACCNT_RANK_ID IN ('170422000000000003', '170422000000000004') "
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
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Microtech Agent(Gemini, Standard) Transaction Details Report</h2></td></tr>";
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

            strSql = "  SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME , ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, ALTO.ACCNT_NO TO_NO,  "
                    + " CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRESS, "
                    + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, "
                    + " TM_AG.AGENT_NO AGT_NO, TM_AG.AGT_RANK, TM_AG.AGT_VRFY_DATE AGT_VRFY_DATE, TM_AG.AGENT_BALANCE AGT_BALANCE, "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG(TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_RG,  "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')NO_OF_CASIN, "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CN')CASIN_AMT,  "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT, "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'CCT,SW')CASOUT_AMT, "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')NO_OF_BD,  "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (TM_AG.AGENT_NO,TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "')),'BD')BD_AMT,  "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_CNT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) NO_OF_BP,  "
                    + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_AMT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) BP_AMOUNT, "
                    + " APSNG101.FUNC_AGT_FM_TRX_AMT (TM_AG.AGENT_NO, TO_CHAR(TO_DATE('" + dtpTmToAgtFromDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpTmToAgtToDate.DateString + "'))) FM_AMT "
                    + " FROM (SELECT DISTINCT ALA.ACCNT_ID AGENT_ACC_ID, ALA.ACCNT_NO AGENT_NO, CLA.VERIFIED_DATE AGT_VRFY_DATE, CAB.CAS_ACCNT_BALANCE  AGENT_BALANCE, ARA.RANK_TITEL AGT_RANK "
                    + " FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB, ACCOUNT_RANK ARA "
                    + " WHERE ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID IN ('170422000000000003', '170422000000000004') AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID  AND ALA.ACCNT_NO = CAL.CAS_ACC_NO "
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
            strHTML = strHTML + "<tr><td COLSPAN=29 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Microtech Agent(Gemini, Standard) Transaction Performance Report('" + strDate + "'  )</h2></td></tr>";
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
                    + " WHERE ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID IN ('170422000000000003', '170422000000000004') AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID  AND ALA.ACCNT_NO = CAL.CAS_ACC_NO "
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

    protected void btnInsPaymentDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT TM.REQUEST_PARTY, AR.RANK_TITEL, TM.RECEPENT_PARTY, EI.EDU_INS_NAME,"
                    + " TM.TRANSACTION_AMOUNT, TM.SERVICE_FEE, TM.POOL_ACCOUNT, TM.CHANNEL_COMMISSION,"
                    + " DECODE(SUBSTR(TM.REQUEST_PARTY, 1,3),'019', 'Banglalink', '017', 'Grameenphone','018',"
                    + " 'Robi', '016', 'Airtel', '015', 'Teletalk') OPERATOR_TYPE, TM.BANK_COMMISSION, TM.AIT,"
                    + " TM.SERVICE_VAT_TAX, TM.AGENT_COMMISSION, TM.VENDOR_COMMISSION FROM"
                    + " TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL, ACCOUNT_RANK AR,"
                    + " EDUCATIONAL_INSTITUTE EI WHERE SERVICE_CODE = 'UIFPS' AND"
                    + " TM.REQUEST_PARTY || '1' = AL.ACCNT_NO  AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND"
                    + " TM.RECEPENT_PARTY = EI.ACCOUNT_NO AND TRUNC(TRANSACTION_DATE)"
                    + " BETWEEN '" + dtInsPaymentFrom.DateString + "' AND '" + dtInsPaymentTo.DateString + "' ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Institute_Payment_Rpt";
            string strDate = dtInsPaymentFrom.DateString + " To " + dtInsPaymentTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Institute Fees Payment Transaction Detail ('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Initiator's Wallet No</td>";
            strHTML = strHTML + "<td valign='middle' >Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Inst Wallet No</td>";
            strHTML = strHTML + "<td valign='middle' >Inst. Name</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Service Charge</td>";
            strHTML = strHTML + "<td valign='middle' >Realise Comm. Expenses For Cash In</td>";
            strHTML = strHTML + "<td valign='middle' >Telco Commision Account</td>";
            strHTML = strHTML + "<td valign='middle' >Request Telco Party</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission Account</td>";
            strHTML = strHTML + "<td valign='middle' >AIT Account</td>";
            strHTML = strHTML + "<td valign='middle' >Service VAT</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Accomission</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["EDU_INS_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE_FEE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["POOL_ACCOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CHANNEL_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["OPERATOR_TYPE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BANK_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AIT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE_VAT_TAX"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["VENDOR_COMMISSION"].ToString() + " </td>";
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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Institute_Payment_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	
	protected void btnOMBalanceDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT TO_CHAR(TRAN_DATE) TRAN_DATE, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000002', TRAN_DATE) OM_MOTHER_MERCHANT, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000003', TRAN_DATE) OM_SUB_MERCHANT, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000005', TRAN_DATE) OM_MASTER_DISTRIBUTOR, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000006', TRAN_DATE) OM_DISTRIBUTOR, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000007', TRAN_DATE) OM_DSE, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000008', TRAN_DATE) OM_AGENT, GET_TOTAL_CUST_BAL_BY_AGENT('180128000000000008', TRAN_DATE) OM_CUSTOMER FROM (SELECT TRUNC(CAT.CAS_TRAN_DATE) TRAN_DATE FROM ACCOUNT_LIST AL, ACCOUNT_RANK AR, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE AL.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_RANK_ID IN ('180128000000000002', '180128000000000003', '180128000000000006', '180128000000000007', '180128000000000008') AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtBalanceFrom.DateString + "' AND '" + dtBalanceTo.DateString + "' GROUP BY TRUNC(CAT.CAS_TRAN_DATE)) ORDER BY TRAN_DATE ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "RankWiseOMBalance_Rpt";
            string strDate = dtBalanceFrom.DateString + " To " + dtBalanceTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Rank Wise OM Balance Detail ('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >OM Mother Merchant</td>";
            strHTML = strHTML + "<td valign='middle' >OM Sub Merchant</td>";
            strHTML = strHTML + "<td valign='middle' >PBazar Master Distributor</td>";
            strHTML = strHTML + "<td valign='middle' >PBazar Distributor</td>";
            strHTML = strHTML + "<td valign='middle' >PBazar DSE</td>";
            strHTML = strHTML + "<td valign='middle' >PBazar Agent</td>";
            strHTML = strHTML + "<td valign='middle' >PBazar Customer</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["OM_MOTHER_MERCHANT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["OM_SUB_MERCHANT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["OM_MASTER_DISTRIBUTOR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["OM_DISTRIBUTOR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["OM_DSE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["OM_AGENT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["OM_CUSTOMER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '"  + (Convert.ToDecimal(prow["OM_MOTHER_MERCHANT"]) 
                                                    + Convert.ToDecimal(prow["OM_SUB_MERCHANT"]) 
                                                    + Convert.ToDecimal(prow["OM_MASTER_DISTRIBUTOR"]) 
                                                    + Convert.ToDecimal(prow["OM_DISTRIBUTOR"])
                                                    + Convert.ToDecimal(prow["OM_DSE"])
                                                    + Convert.ToDecimal(prow["OM_AGENT"])
                                                    + Convert.ToDecimal(prow["OM_CUSTOMER"])).ToString()
                                                    + "</td>";
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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "RankWiseOMBalance_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	
	protected void btnOmTranDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            //strSql = " SELECT DISTINCT TRUNC(SR.REQUEST_TIME) TRAN_DATE, MN.REQUEST_ID, SUBSTR(SR.REQUEST_PARTY,4,11) || '1' CUSTOMER_NO, ALS.ACCNT_NO SUB_MERCHANT, CLS.CLINT_NAME SUB_MERCHANT_NAME, SUM(DECODE(CAT.CAS_TRAN_PURPOSE_CODE,'TOTAMT',CAS_TRAN_AMT,0)) - SUM(DECODE(CAT.CAS_TRAN_PURPOSE_CODE,'DRMERCOM',CAS_TRAN_AMT,0)) CR, SUM(DECODE(CAT.CAS_TRAN_PURPOSE_CODE,'DRMERCOM',CAS_TRAN_AMT,0)) DR, ALM.ACCNT_NO MASTER_MERCHANT, SUM(MN.AMOUNT) / 2 M_MERCHANT_AMOUNT, SUM(DECODE(CAT.CAS_TRAN_PURPOSE_CODE,'DRMERCOM',CAS_TRAN_AMT,0)) - SUM(MN.AMOUNT) / 2 SERVICE_CHARGE FROM MERCHANT_NOTIFICATION MN, ACCOUNT_HIERARCHY AH, ACCOUNT_LIST ALM, ACCOUNT_LIST ALS, CLIENT_LIST CLS, SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE MN.SUB_MERCHANT_ACCNT_ID = AH.ACCNT_ID AND AH.HIERARCHY_ACCNT_ID = ALM.ACCNT_ID AND MN.SUB_MERCHANT_ACCNT_ID = ALS.ACCNT_ID AND ALS.CLINT_ID = CLS.CLINT_ID AND MN.REQUEST_ID = SR.REQUEST_ID AND MN.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE IN ('TOTAMT','DRMERCOM') AND TRUNC(SR.REQUEST_TIME) BETWEEN '" + dtOtdFrom.DateString + "' AND '" + dtOtdTo.DateString + "' GROUP BY TRUNC(SR.REQUEST_TIME), MN.REQUEST_ID, SR.REQUEST_PARTY, ALS.ACCNT_NO, CLS.CLINT_NAME, ALM.ACCNT_NO ORDER BY TRUNC(SR.REQUEST_TIME) ";

            strSql = " SELECT DISTINCT TRUNC (SR.REQUEST_TIME) TRAN_DATE, MN.REQUEST_ID, SUBSTR (SR.REQUEST_PARTY, 4, 11) || '1' CUSTOMER_NO, ALS.ACCNT_NO SUB_MERCHANT, CLS.CLINT_NAME SUB_MERCHANT_NAME, CLS.CLINT_ADDRESS1 SUB_MERCHANT_ADDRESS, MTS.THANA_NAME SUB_MERCHANT_THANA, MDS.DISTRICT_NAME, SUM (DECODE (CAT.CAS_TRAN_PURPOSE_CODE, 'TOTAMT', CAS_TRAN_AMT, 0)) - SUM (DECODE (CAT.CAS_TRAN_PURPOSE_CODE, 'DRMERCOM', CAS_TRAN_AMT, 0)) CR, SUM (DECODE (CAT.CAS_TRAN_PURPOSE_CODE, 'DRMERCOM', CAS_TRAN_AMT, 0)) DR, ALM.ACCNT_NO MASTER_MERCHANT,SUM (MN.AMOUNT) / 2 M_MERCHANT_AMOUNT, SUM (DECODE (CAT.CAS_TRAN_PURPOSE_CODE, 'DRMERCOM', CAS_TRAN_AMT, 0))- SUM (MN.AMOUNT) / 2 SERVICE_CHARGE FROM MERCHANT_NOTIFICATION MN, ACCOUNT_HIERARCHY AH,ACCOUNT_LIST ALM,ACCOUNT_LIST ALS,CLIENT_LIST CLS,MANAGE_THANA MTS,MANAGE_DISTRICT MDS,SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE MN.SUB_MERCHANT_ACCNT_ID = AH.ACCNT_ID AND AH.HIERARCHY_ACCNT_ID = ALM.ACCNT_ID AND MN.SUB_MERCHANT_ACCNT_ID = ALS.ACCNT_ID AND ALS.CLINT_ID = CLS.CLINT_ID AND CLS.THANA_ID = MTS.THANA_ID(+) AND MTS.DISTRICT_ID = MDS.DISTRICT_ID(+) AND MN.REQUEST_ID = SR.REQUEST_ID AND MN.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE IN ('TOTAMT', 'DRMERCOM') AND TRUNC (SR.REQUEST_TIME) BETWEEN '" + dtOtdFrom.DateString + "' AND '" + dtOtdTo.DateString + "' GROUP BY TRUNC (SR.REQUEST_TIME), MN.REQUEST_ID, SR.REQUEST_PARTY, ALS.ACCNT_NO, CLS.CLINT_NAME, CLS.CLINT_ADDRESS1, MTS.THANA_NAME, MDS.DISTRICT_NAME, ALM.ACCNT_NO ORDER BY TRUNC (SR.REQUEST_TIME) ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";
            double strCreditAmount = 0;
            double strDebitAmount = 0;
            double strCreditBalance = 0;
            double strServiceCharge = 0;
            double strMblCommission = 0;

            DataSet dtsAccount = new DataSet();
            fileName = "OM_Transaction_Detail_Rpt";
            string strDate = dtOtdFrom.DateString + " To " + dtOtdTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>OM Transaction Detail ('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Name</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Address</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Merchant District</td>";
            strHTML = strHTML + "<td valign='middle' >Credit</td>";
            strHTML = strHTML + "<td valign='middle' >Debit</td>";
            strHTML = strHTML + "<td valign='middle' >Master Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Credit Balance</td>";
            strHTML = strHTML + "<td valign='middle' >Service Charge</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUSTOMER_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT_ADDRESS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DR"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MASTER_MERCHANT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["M_MERCHANT_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE_CHARGE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + (Convert.ToDouble(prow["SERVICE_CHARGE"]) * 1 / 100).ToString() + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    strCreditAmount = strCreditAmount + Convert.ToDouble(prow["CR"]);
                    strDebitAmount = strDebitAmount + Convert.ToDouble(prow["DR"]);
                    strCreditBalance = strCreditBalance + Convert.ToDouble(prow["M_MERCHANT_AMOUNT"]);
                    strServiceCharge = strServiceCharge + Convert.ToDouble(prow["SERVICE_CHARGE"]);
                    strMblCommission = strMblCommission + (Convert.ToDouble(prow["SERVICE_CHARGE"]) * 1 / 100);
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
            strHTML = strHTML + " <td > " + strCreditAmount + " </td>";
            strHTML = strHTML + " <td > " + strDebitAmount + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + strCreditBalance + " </td>";
            strHTML = strHTML + " <td > " + strServiceCharge + " </td>";
            strHTML = strHTML + " <td > " + strMblCommission + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "OM_Transaction_Detail_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnOmCommission_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            //strSql = " SELECT TRUNC(SR.REQUEST_TIME) TRAN_DATE, ALM.ACCNT_NO MASTER_MERCHANT, ALS.ACCNT_NO SUB_MERCHANT, CLS.CLINT_NAME SUB_MERCHANT_NAME, SUM(MN.AMOUNT) AMOUNT FROM MERCHANT_NOTIFICATION MN, ACCOUNT_HIERARCHY AH, ACCOUNT_LIST ALM, ACCOUNT_LIST ALS, CLIENT_LIST CLS, SERVICE_REQUEST SR WHERE MN.SUB_MERCHANT_ACCNT_ID = AH.ACCNT_ID AND AH.HIERARCHY_ACCNT_ID = ALM.ACCNT_ID AND MN.SUB_MERCHANT_ACCNT_ID = ALS.ACCNT_ID AND ALS.CLINT_ID = CLS.CLINT_ID AND MN.REQUEST_ID = SR.REQUEST_ID AND TRUNC(SR.REQUEST_TIME) BETWEEN '" + dtOmcFrom.DateString + "' AND '" + dtOmcTo.DateString + "' GROUP BY TRUNC(SR.REQUEST_TIME), ALM.ACCNT_NO, ALS.ACCNT_NO, CLS.CLINT_NAME ";

            //strSql = " SELECT ALM.ACCNT_NO MASTER_MERCHANT, ALS.ACCNT_NO SUB_MERCHANT, CLS.CLINT_NAME SUB_MERCHANT_NAME, AH.MERCHANT_COMMISSION FROM MERCHANT_NOTIFICATION MN, ACCOUNT_HIERARCHY AH, ACCOUNT_LIST ALM, ACCOUNT_LIST ALS, CLIENT_LIST CLS, SERVICE_REQUEST SR WHERE MN.SUB_MERCHANT_ACCNT_ID = AH.ACCNT_ID AND AH.HIERARCHY_ACCNT_ID = ALM.ACCNT_ID AND MN.SUB_MERCHANT_ACCNT_ID = ALS.ACCNT_ID AND ALS.CLINT_ID = CLS.CLINT_ID AND MN.REQUEST_ID = SR.REQUEST_ID GROUP BY ALM.ACCNT_NO, ALS.ACCNT_NO, CLS.CLINT_NAME, AH.MERCHANT_COMMISSION ";
			
			strSql = " SELECT ALM.ACCNT_NO MASTER_MERCHANT, ALS.ACCNT_NO SUB_MERCHANT, CLS.CLINT_NAME SUB_MERCHANT_NAME, NVL(AH.MERCHANT_COMMISSION, 'NOT ASSIGN') MERCHANT_COMMISSION FROM ACCOUNT_LIST ALS, ACCOUNT_HIERARCHY AH, CLIENT_LIST CLS, ACCOUNT_RANK AR, ACCOUNT_LIST ALM WHERE ALS.ACCNT_ID = AH.ACCNT_ID(+) AND ALS.ACCNT_RANK_ID = '180128000000000003' AND ALS.CLINT_ID = CLS.CLINT_ID AND ALS.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AR.HIERARCHY = ALM.ACCNT_RANK_ID ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "OM_Merchant_Commission_Rpt";
            //string strDate = dtOmcFrom.DateString + " To " + dtOmcTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>OM Merchant Commission ('" + DateTime.Now.ToShortDateString() + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Master Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Name</td>";
            strHTML = strHTML + "<td valign='middle' >Merchant Commission</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["MASTER_MERCHANT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["MERCHANT_COMMISSION"].ToString() + "</td>";

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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "OM_Merchant_Commission_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnPBazarCusRegReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT ALA.ACCNT_NO AGENT_WALLET, CLA.CLINT_NAME AGENT_WALLET_NAME, ALC.ACCNT_NO CUSTOMER_WALLET, CLC.CLINT_NAME CUSTOMER_NAME, TO_CHAR(CLC.CREATION_DATE) REG_DATE, DECODE(ASD.VERIFIED, 'V', 'Y', 'N', 'N') VERIFIED FROM ACCOUNT_LIST ALA, CLIENT_LIST CLA, ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALC, CLIENT_LIST CLC WHERE ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND ALC.CLINT_ID = CLC.CLINT_ID AND ALA.ACCNT_RANK_ID = '180128000000000008' AND TRUNC(CLC.CREATION_DATE) BETWEEN '" + dtPcRegReportFrom.DateString + "' AND '" + dtPcRegReportTo.DateString + "' ORDER BY TO_CHAR(CLC.CREATION_DATE)";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "PBazar_Customer_Reg_Rpt";
            string strDate = dtPcRegReportFrom.DateString + " To " + dtPcRegReportTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>OM Customer Registration Report ('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Name</td>";
			strHTML = strHTML + "<td valign='middle' >Is Verified</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REG_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_WALLET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_WALLET_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUSTOMER_WALLET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUSTOMER_NAME"].ToString() + "</td>";
					strHTML = strHTML + " <td > '" + prow["VERIFIED"].ToString() + "</td>";

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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "PBazar_Customer_Reg_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	
	protected void btnPBazarAccountHierarchy_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT THA.DEL_ACCNT_NO DISTRIBUTOR, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRESS, MDD.DISTRICT_NAME, ALD.ACCNT_STATE DIS_STATUS, THA.SA_ACCNT_NO DSE, CLDSE.CLINT_NAME DSE_NAME, ALDSE.ACCNT_STATE DSE_STATUS, THA.A_ACCNT_NO AGENT, CLA.CLINT_NAME AGENT_NAME, CLA.CLINT_ADDRESS1 AGENT_ADDRESS, ALA.ACCNT_STATE A_STATUS FROM ACCOUNT_LIST AL, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE, ACCOUNT_LIST ALA, CLIENT_LIST CLA WHERE AL.ACCNT_RANK_ID = '180128000000000006' AND AL.ACCNT_NO = THA.DEL_ACCNT_NO AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO AND ALDSE.CLINT_ID = CLDSE.CLINT_ID AND THA.A_ACCNT_NO = ALA.ACCNT_NO AND ALA.CLINT_ID = CLA.CLINT_ID ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "PBazar_Hierarchy_Rpt";
            string strDate = dtPcRegReportFrom.DateString + " To " + dtPcRegReportTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>PBazar Account Hierarchy Report ('" + DateTime.Now.ToShortDateString() + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Distributor</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Status</td>";
            strHTML = strHTML + "<td valign='middle' >DSE</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Name</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Status</td>";
            strHTML = strHTML + "<td valign='middle' >Agent</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Address</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Status</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_ADDRESS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["A_STATUS"].ToString() + " </td>";

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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "PBazar_Hierarchy_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	
	protected void btnRankWalletWiseBalance_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT AL.ACCNT_NO, CL.CLINT_NAME, NVL(BDMIT_ERP_101.GET_FIS_BALANCE(AL.ACCNT_NO),0) CURRENT_BALANCE FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = '" + ddlAccountRankList.SelectedValue + "' GROUP BY AL.ACCNT_NO, CL.CLINT_NAME ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Balance_With_Wallet_Rpt";
            //string strDate = dtRWBalDetailFrom.DateString + " To " + dtRWBalDetailTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>"+ ddlAccountRankList.SelectedItem.Text +" Balance Details ('" + DateTime.Now.ToShortDateString() + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Wallet Id</td>";
            strHTML = strHTML + "<td valign='middle' >Account Name</td>";
            strHTML = strHTML + "<td valign='middle' >Current Balance</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["CURRENT_BALANCE"].ToString() + "</td>";

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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "PBazar_Balance_With_Wallet_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	
	protected void btnPBazarSummaryReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT AR.RANK_TITEL, COUNT(*) NUMBER1, 0 AMOUNT FROM ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_RANK_ID = '180128000000000006' AND TRUNC(CL.CREATION_DATE) < TRUNC(SYSDATE) GROUP BY AR.RANK_TITEL UNION ALL SELECT AR.RANK_TITEL, COUNT(*) NUMBER1, 0 AMOUNT FROM ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL  WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_RANK_ID = '180128000000000007' AND TRUNC(CL.CREATION_DATE) < TRUNC(SYSDATE) GROUP BY AR.RANK_TITEL UNION ALL SELECT AR.RANK_TITEL, COUNT(*) NUMBER1, 0 AMOUNT FROM ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_RANK_ID = '180128000000000008' AND TRUNC(CL.CREATION_DATE) < TRUNC(SYSDATE) GROUP BY AR.RANK_TITEL UNION ALL SELECT 'Total Customer', COUNT(*) NUMBERS, 0 AMOUNT FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALC, ACCOUNT_RANK AR, CLIENT_LIST CL WHERE ALC.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '180128000000000008' AND TRUNC(CL.CREATION_DATE) < TRUNC(SYSDATE) AND ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND AR.ACCNT_RANK_ID = ALC.ACCNT_RANK_ID UNION ALL SELECT 'Total Non-Verified Customer', COUNT(*) NUMBERS, 0 AMOUNT FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALC, ACCOUNT_RANK AR, CLIENT_LIST CL WHERE ALC.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '180128000000000008' AND TRUNC(CL.CREATION_DATE) < TRUNC(SYSDATE) AND ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND AR.ACCNT_RANK_ID = ALC.ACCNT_RANK_ID AND CL.VERIFIED_DATE IS NULL UNION ALL SELECT 'Total Verified Customer', COUNT(*) NUMBERS, 0 AMOUNT FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALC, ACCOUNT_RANK AR, CLIENT_LIST CL WHERE ALC.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '180128000000000008' AND TRUNC(CL.CREATION_DATE) < TRUNC(SYSDATE) AND ASD.CUSTOMER_MOBILE_NO = ALC.ACCNT_MSISDN AND AR.ACCNT_RANK_ID = ALC.ACCNT_RANK_ID AND CL.VERIFIED_DATE IS NOT NULL UNION ALL SELECT AR.RANK_TITEL, COUNT(*) NUMBER1, 0 AMOUNT FROM ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_RANK_ID = '180128000000000002' AND TRUNC(CL.CREATION_DATE) < TRUNC(SYSDATE) GROUP BY AR.RANK_TITEL UNION ALL SELECT AR.RANK_TITEL, COUNT(*) NUMBER1, 0 AMOUNT FROM ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_RANK_ID = '180128000000000003' AND TRUNC(CL.CREATION_DATE) < TRUNC(SYSDATE) GROUP BY AR.RANK_TITEL UNION ALL SELECT 'TRANSACTION', 0, 0 FROM DUAL UNION ALL SELECT 'No of Cash In', COUNT(*) NUMBERS, SUM(TRANSACTION_AMOUNT) AMOUNT FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.ACCNT_RANK_ID = '180128000000000008' AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND SERVICE_CODE = 'CN' AND TM.TRANSACTION_DATE BETWEEN '" + dtPBazarSumReportFr.DateString + "' AND '" + dtPBazarSumReportTo.DateString + "' UNION ALL SELECT 'No of Cash Out', COUNT(*) NUMBERS, SUM(TRANSACTION_AMOUNT) AMOUNT FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.ACCNT_RANK_ID = '180128000000000008' AND AL.ACCNT_NO = TM.RECEPENT_PARTY AND SERVICE_CODE = 'CCT' AND TM.TRANSACTION_DATE BETWEEN '" + dtPBazarSumReportFr.DateString + "' AND '" + dtPBazarSumReportTo.DateString + "' UNION ALL SELECT 'Merchant Payment', COUNT(*) NUMBERS, SUM(TRANSACTION_AMOUNT) AMOUNT FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.ACCNT_RANK_ID = '180128000000000003' AND AL.ACCNT_NO = TM.RECEPENT_PARTY AND SERVICE_CODE = 'MP' AND TM.TRANSACTION_DATE BETWEEN '" + dtPBazarSumReportFr.DateString + "' AND '" + dtPBazarSumReportTo.DateString + "' UNION ALL SELECT 'No of Utility Bill Payment', COUNT(*) NUMBERS, SUM(TRANSACTION_AMOUNT) AMOUNT FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.ACCNT_RANK_ID = '180128000000000008' AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND SERVICE_CODE IN ('UBP','UBI','UWZP','UBPW','UWZI') AND TM.TRANSACTION_DATE BETWEEN '" + dtPBazarSumReportFr.DateString + "' AND '" + dtPBazarSumReportTo.DateString + "' UNION ALL SELECT 'No of Business Collection', COUNT ( * ) NUMBERS, SUM (TRANSACTION_AMOUNT) AMOUNT FROM   ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.ACCNT_RANK_ID = '180416000000000001' AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND SERVICE_CODE IN ('FM') AND TM.TRANSACTION_DATE BETWEEN '" + dtPBazarSumReportFr.DateString + "' AND '" + dtPBazarSumReportTo.DateString + "' UNION ALL SELECT 'DEPOSIT', 0, 0 FROM DUAL UNION ALL SELECT 'OM Mother Merchant', 0, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000002', TRUNC(SYSDATE-1)) AMOUNT FROM DUAL UNION ALL SELECT 'OM Sub Merchant', 0, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000003', TRUNC(SYSDATE-1)) AMOUNT FROM DUAL UNION ALL SELECT 'PBazar Master Distributor', 0, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000005', TRUNC(SYSDATE-1)) AMOUNT FROM DUAL UNION ALL SELECT 'PBazar Distributor', 0, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000006', TRUNC(SYSDATE-1)) AMOUNT FROM DUAL UNION ALL SELECT 'PBazar DSE', 0, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000007', TRUNC(SYSDATE-1)) AMOUNT FROM DUAL UNION ALL SELECT 'PBazar Agent', 0, GET_RANK_WISE_BALANCE_BY_DATE('180128000000000008', TRUNC(SYSDATE-1)) AMOUNT FROM DUAL UNION ALL SELECT 'PBazar Customer', 0, GET_TOTAL_CUST_BAL_BY_AGENT('180128000000000008', TRUNC(SYSDATE-1)) AMOUNT FROM DUAL ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "PBazar_Summary_Rpt";
            string strDate = dtPcRegReportFrom.DateString + " To " + dtPcRegReportTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>PBAZAR SUMMARY Report ('" + DateTime.Now.ToShortDateString() + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Rank Title</td>";
            strHTML = strHTML + "<td valign='middle' >Number</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["NUMBER1"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AMOUNT"].ToString() + " </td>";

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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "PBazar_Summary_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	
	protected void btnPBazarTranReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT TRANSACTION_DATE, DIS_NO, DIS_NAME, DSE_NO, DSE_NAME, AGENT_NO, AGENT_NAME, SUM(TOTAL_CN) TOTAL_CN, SUM(CN_AMOUNT) CN_AMOUNT, SUM(TOTAL_CCT) TOTAL_CCT, SUM(CCT_AMOUNT) CCT_AMOUNT FROM (SELECT TRUNC(TM.TRANSACTION_DATE) TRANSACTION_DATE, THA.DEL_ACCNT_NO DIS_NO, CLSD.CLINT_NAME DIS_NAME, THA.SA_ACCNT_NO DSE_NO, CLDSE.CLINT_NAME DSE_NAME, THA.A_ACCNT_NO AGENT_NO, CLA.CLINT_NAME AGENT_NAME, DECODE(TM.SERVICE_CODE, 'CN', COUNT(*),0) TOTAL_CN, DECODE(TM.SERVICE_CODE, 'CN', SUM(TM.TRANSACTION_AMOUNT ),0) CN_AMOUNT, DECODE(TM.SERVICE_CODE, 'CCT', COUNT(*),0) TOTAL_CCT, DECODE(TM.SERVICE_CODE, 'CCT', SUM(TM.TRANSACTION_AMOUNT ),0) CCT_AMOUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALSD, CLIENT_LIST CLSD, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE, ACCOUNT_LIST ALA, CLIENT_LIST CLA, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE THA.DEL_ACCNT_ID = ALSD.ACCNT_ID AND ALSD.ACCNT_RANK_ID = '180128000000000006' AND ALSD.CLINT_ID = CLSD.CLINT_ID AND THA.SA_ACCNT_ID = ALDSE.ACCNT_ID AND ALDSE.CLINT_ID = CLDSE.CLINT_ID AND THA.A_ACCNT_ID = ALA.ACCNT_ID AND ALA.CLINT_ID = CLA.CLINT_ID AND (THA.A_ACCNT_NO = TM.REQUEST_PARTY || '1' OR THA.A_ACCNT_NO = TM.RECEPENT_PARTY) GROUP BY TM.TRANSACTION_DATE, THA.DEL_ACCNT_NO, CLSD.CLINT_NAME, THA.SA_ACCNT_NO, CLDSE.CLINT_NAME, THA.A_ACCNT_NO, CLA.CLINT_NAME, TM.SERVICE_CODE) WHERE TRUNC(TRANSACTION_DATE) BETWEEN '" + dtPBazarTranReportFrom.DateString + "' AND '" + dtPBazarTranReportTo.DateString + "' AND (TOTAL_CN > 0 OR TOTAL_CCT > 0) GROUP BY TRANSACTION_DATE, DIS_NO, DIS_NAME, DSE_NO, DSE_NAME, DSE_NAME, AGENT_NO, AGENT_NAME ORDER BY TRANSACTION_DATE ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "PBazar_Transaction_Rpt";
            string strDate = dtPBazarTranReportFrom.DateString + " To " + dtPBazarTranReportTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>PBazar Transaction Report ('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >DSE No</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent No</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Number of Cash In</td>";
            strHTML = strHTML + "<td valign='middle' >Cash In Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Number of Cash Out</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Out Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CN_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_AMOUNT"].ToString() + " </td>";

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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "PBazar_Transaction_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnKYCUpdatedReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            //strSql = " SELECT CLD.CLINT_NAME, AL.ACCNT_NO, COUNT(*) COUNT1 FROM ACCOUNT_LIST AL, CLIENT_LIST CL, CLIENT_IDENTIFICATION CI, CLIENT_LIST CLD WHERE AL.ACCNT_NO IN ('017533999261','018197040771','018202054411','018278980271','018303629341','018358081741','018358606671','018422343251','018514743771','018593572541','018654423551','018674593951','018782569001','018859678401','018864466681') AND AL.ACCNT_ID = CL.KYC_UPDATED_BY AND CL.CLINT_ID = CI.CLIENT_ID AND AL.CLINT_ID = CLD.CLINT_ID AND CL.CLINT_NAME IS NOT NULL AND CL.CLINT_FATHER_NAME IS NOT NULL AND CL.CLINT_MOTHER_NAME IS NOT NULL AND CL.CLINT_ADDRESS1 IS NOT NULL AND CL.THANA_ID IS NOT NULL AND CL.CLINT_MOBILE IS NOT NULL AND CI.CLINT_IDENT_NAME IS NOT NULL GROUP BY CLD.CLINT_NAME, AL.ACCNT_NO ";

            strSql = " SELECT CLD.CLINT_NAME DISTRIBUTOR_NAME, AL.ACCNT_NO DISTRIBUTOR_NO, ALC.ACCNT_NO CLIENT_NO, CL.CLINT_NAME, CL.CLINT_ADDRESS1, CL.UPDATE_DATE FROM ACCOUNT_LIST AL, CLIENT_LIST CL, CLIENT_IDENTIFICATION CI, CLIENT_LIST CLD, ACCOUNT_LIST ALC WHERE AL.ACCNT_NO IN ('017533999261','018197040771','018202054411','018278980271','018303629341','018358081741','018358606671','018422343251','018514743771','018593572541','018654423551','018674593951','018782569001','018859678401','018864466681') AND AL.ACCNT_ID = CL.KYC_UPDATED_BY AND CL.CLINT_ID = CI.CLIENT_ID AND AL.CLINT_ID = CLD.CLINT_ID AND CL.CLINT_ID = ALC.CLINT_ID AND CL.CLINT_NAME IS NOT NULL AND (CL.CLINT_FATHER_NAME IS NOT NULL OR CL.HUSBAND_NAME IS NOT NULL) AND CL.CLINT_MOTHER_NAME IS NOT NULL AND CL.CLINT_ADDRESS1 IS NOT NULL AND CL.THANA_ID IS NOT NULL AND CL.CLINT_MOBILE IS NOT NULL AND CI.CLINT_IDENT_NAME IS NOT NULL AND TRUNC(CL.UPDATE_DATE) BETWEEN '" + dtKYCUpdateFrom.DateString + "' AND '" + dtKYCUpdateTo.DateString + "' ORDER BY AL.ACCNT_NO ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";
            int total = 0;

            DataSet dtsAccount = new DataSet();
            fileName = "PBazar_KYC_Updated_Rpt";
            string strDate = dtKYCUpdateFrom.DateString + " To " + dtKYCUpdateTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>PBazar KYC Updated Report ('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Name</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Address</td>";
            strHTML = strHTML + "<td valign='middle' >Number of KYC Updated</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLIENT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_ADDRESS1"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["UPDATE_DATE"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                    //total = total + Convert.ToInt32(prow["COUNT1"]);
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "Total" + " </td>";
            //strHTML = strHTML + " <td > '" + total + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "PBazar_KYC_Updated_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	protected void btnOMTranSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT ALM.ACCNT_NO MM, ALSM.ACCNT_NO SM, CLSM.CLINT_NAME SM_NAME, AH.MERCHANT_COMMISSION SM_COMM, COUNT(TM.REQUEST_ID) TOTAL_TRANS, SUM(TM.TRANSACTION_AMOUNT) TRANS_AMOUNT FROM ACCOUNT_LIST ALM, ACCOUNT_RANK ARM, ACCOUNT_RANK ARSM, ACCOUNT_LIST ALSM, CLIENT_LIST CLSM, ACCOUNT_HIERARCHY AH, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE ALM.ACCNT_RANK_ID = ARM.ACCNT_RANK_ID AND ARM.ACCNT_RANK_ID = '180128000000000002' AND ARM.ACCNT_RANK_ID = ARSM.HIERARCHY AND ARSM.ACCNT_RANK_ID = ALSM.ACCNT_RANK_ID AND ALSM.CLINT_ID = CLSM.CLINT_ID AND ALSM.ACCNT_ID = AH.ACCNT_ID AND ALSM.ACCNT_NO = TM.RECEPENT_PARTY AND TM.TRANSACTION_DATE BETWEEN '" + dtOMTSRFrom.DateString + "' AND '" + dtOMTSRTo.DateString + "' GROUP BY ALM.ACCNT_NO, ALSM.ACCNT_NO, CLSM.CLINT_NAME, AH.MERCHANT_COMMISSION ORDER BY ALSM.ACCNT_NO ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";
            int totalWallet = 0;
            int totalAmount = 0;

            DataSet dtsAccount = new DataSet();
            fileName = "OM_Transaction_Summary";
            string strDate = dtOMTSRFrom.DateString + " To " + dtOMTSRTo.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>OM Transaction Summary ('" + strDate + "'  )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Master Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Name</td>";
            strHTML = strHTML + "<td valign='middle' >Merchant Commission(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Count of Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Sum of Total Transaction Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["MM"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SM"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["SM_COMM"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_TRANS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TRANS_AMOUNT"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                    totalWallet = totalWallet + Convert.ToInt32(prow["TOTAL_TRANS"]);
                    totalAmount = totalAmount + Convert.ToInt32(prow["TRANS_AMOUNT"]);
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + totalWallet + " </td>";
            strHTML = strHTML + " <td > " + totalAmount + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "OM_Transaction_Summary");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	
	protected void btnPrepaidBillPayComm_Click(object sender, EventArgs e)
    {
        // try
        // {
            // string strSql = "";
            // string strWalletId = "";
            // string strWalletName = "";

            // if (ddlReportType.SelectedValue.Equals("Distributor"))
            // {
                // //strSql = " SELECT DEL_ACCNT_NO DISTRIBUTOR_NUMBER, CLINT_NAME DISTRIBUTOR_NAME, DISTRICT_NAME DISTRICT, SUM(NUMBER_OF_BILL) TOTAL_BILL, SUM(TOTAMT) TOTAL_AMOUNT, SUM(FEEBYI) FEES, SUM(VATBYI) VAT, SUM(TAXBYI) TAX, SUM(BNKCOM) BANK_COMMISSION, SUM(AGNCOM) AGENT_COMMISSION, SUM(TDRCOM) THIRD_PARTY_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 1/3,4)) MBL_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 2/3,4)) AAMRA_COMMISSION FROM (SELECT THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, MDD.DISTRICT_NAME, NUMBER_OF_BILL, SUM(T1.TOTAMT) TOTAMT, SUM(T1.FEEBYI) FEEBYI, SUM(T1.VATBYI) VATBYI, SUM(T1.TAXBYI) TAXBYI, SUM(T1.BNKCOM) BNKCOM, SUM(T1.AGNCOM) AGNCOM, SUM(T1.TDRCOM) TDRCOM FROM (SELECT SUBSTR(SR.REQUEST_PARTY,4) || '1' REQUEST_PARTY, COUNT(CAT.REQUEST_ID) NUMBER_OF_BILL, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'TOTAMT', SUM(CAS_TRAN_AMT)) TOTAMT, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'FEEBYI', SUM(CAS_TRAN_AMT)) FEEBYI, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'VATBYI', SUM(CAS_TRAN_AMT)) VATBYI, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'TAXBYI', SUM(CAS_TRAN_AMT)) TAXBYI, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'BNKCOM', SUM(CAS_TRAN_AMT)) BNKCOM, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'AGNCOM', SUM(CAS_TRAN_AMT)) AGNCOM, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'TDRCOM', SUM(CAS_TRAN_AMT)) TDRCOM FROM SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE ) T1, TEMP_HIERARCHY_LIST_ALL THLA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE T1.REQUEST_PARTY= THLA.A_ACCNT_NO AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID GROUP BY THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, MDD.DISTRICT_NAME, NUMBER_OF_BILL) GROUP BY DEL_ACCNT_NO, CLINT_NAME, DISTRICT_NAME ";

                // strSql = "SELECT TO_CHAR(REQUEST_TIME) REQUEST_TIME, DEL_ACCNT_NO DISTRIBUTOR_NUMBER,CLINT_NAME DISTRIBUTOR_NAME,DISTRICT_NAME DISTRICT,SUM (NUMBER_OF_BILL) TOTAL_BILL,SUM (TOTAMT) TOTAL_AMOUNT,SUM (FEEBYI) FEES,SUM (VATBYI) VAT,SUM (TAXBYI) TAX,SUM (BNKCOM) BANK_COMMISSION,SUM (AGNCOM) AGENT_COMMISSION,SUM (TDRCOM) THIRD_PARTY_COMMISSION,TO_CHAR (ROUND (SUM (BNKCOM) * 1 / 3, 4)) MBL_COMMISSION,TO_CHAR (ROUND (SUM (BNKCOM) * 2 / 3, 4)) AAMRA_COMMISSION FROM (SELECT REQUEST_TIME, THLA.DEL_ACCNT_NO,CLD.CLINT_NAME,MDD.DISTRICT_NAME,NUMBER_OF_BILL,SUM (T1.TOTAMT) TOTAMT,SUM (T1.FEEBYI) FEEBYI,SUM (T1.VATBYI) VATBYI,SUM (T1.TAXBYI) TAXBYI,SUM (T1.BNKCOM) BNKCOM,SUM (T1.AGNCOM) AGNCOM,SUM (T1.TDRCOM) TDRCOM FROM (SELECT TRUNC(SR.REQUEST_TIME) REQUEST_TIME, SUBSTR (SR.REQUEST_PARTY, 4) || '1' REQUEST_PARTY,COUNT (CAT.REQUEST_ID) NUMBER_OF_BILL,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TOTAMT',SUM (CAS_TRAN_AMT)) TOTAMT,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'FEEBYI',SUM (CAS_TRAN_AMT))FEEBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'VATBYI',SUM (CAS_TRAN_AMT))VATBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TAXBYI',SUM (CAS_TRAN_AMT)) TAXBYI, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'BNKCOM',SUM (CAS_TRAN_AMT)) BNKCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'AGNCOM',SUM (CAS_TRAN_AMT)) AGNCOM, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TDRCOM',SUM (CAS_TRAN_AMT)) TDRCOM FROM SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE ('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC (CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY TRUNC(REQUEST_TIME), SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE) T1, TEMP_HIERARCHY_LIST_ALL THLA,ACCOUNT_LIST ALD,CLIENT_LIST CLD,MANAGE_THANA MTD,MANAGE_DISTRICT MDD WHERE T1.REQUEST_PARTY = THLA.A_ACCNT_NO AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID GROUP BY REQUEST_TIME, THLA.DEL_ACCNT_NO,CLD.CLINT_NAME,MDD.DISTRICT_NAME,NUMBER_OF_BILL) GROUP BY REQUEST_TIME, DEL_ACCNT_NO, CLINT_NAME, DISTRICT_NAME ORDER BY REQUEST_TIME";

                // strWalletId = "Distributor Wallet Id";
                // strWalletName = "Distributor Name";
            // }
            // else
            // {
                // //strSql = " SELECT DISTRIBUTOR_NUMBER, CLA.CLINT_NAME DISTRIBUTOR_NAME, MDA.DISTRICT_NAME DISTRICT, TOTAL_BILL, TOTAL_AMOUNT, FEES, VAT, TAX, BANK_COMMISSION, AGENT_COMMISSION, THIRD_PARTY_COMMISSION, MBL_COMMISSION, AAMRA_COMMISSION FROM (SELECT REQUEST_PARTY DISTRIBUTOR_NUMBER, NUMBER_OF_BILL TOTAL_BILL, SUM(TOTAMT) TOTAL_AMOUNT, SUM(FEEBYI) FEES, SUM(VATBYI) VAT, SUM(TAXBYI) TAX, SUM(BNKCOM) BANK_COMMISSION, SUM(AGNCOM) AGENT_COMMISSION, SUM(TDRCOM) THIRD_PARTY_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 1/3,4)) MBL_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 2/3,4)) AAMRA_COMMISSION FROM (SELECT SUBSTR (SR.REQUEST_PARTY, 4) || '1' REQUEST_PARTY,COUNT (CAT.REQUEST_ID) NUMBER_OF_BILL, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TOTAMT',SUM (CAS_TRAN_AMT))TOTAMT,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'FEEBYI',SUM (CAS_TRAN_AMT))FEEBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'VATBYI',SUM (CAS_TRAN_AMT))VATBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TAXBYI',SUM (CAS_TRAN_AMT))TAXBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'BNKCOM',SUM (CAS_TRAN_AMT))BNKCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'AGNCOM',SUM (CAS_TRAN_AMT))AGNCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TDRCOM',SUM (CAS_TRAN_AMT))TDRCOM FROM SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE ('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC (CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE) GROUP BY REQUEST_PARTY, NUMBER_OF_BILL) T1, ACCOUNT_LIST ALA, CLIENT_LIST CLA, MANAGE_THANA MTA, MANAGE_DISTRICT MDA WHERE T1.DISTRIBUTOR_NUMBER = ALA.ACCNT_NO AND ALA.CLINT_ID = CLA.CLINT_ID AND CLA.THANA_ID = MTA.THANA_ID AND MTA.DISTRICT_ID = MDA.DISTRICT_ID ";

                // strSql = " SELECT TO_CHAR(REQUEST_TIME) REQUEST_TIME, DISTRIBUTOR_NUMBER, CLA.CLINT_NAME DISTRIBUTOR_NAME, MDA.DISTRICT_NAME DISTRICT, TOTAL_BILL, TOTAL_AMOUNT, FEES, VAT, TAX, BANK_COMMISSION, AGENT_COMMISSION, THIRD_PARTY_COMMISSION, MBL_COMMISSION, AAMRA_COMMISSION FROM (SELECT REQUEST_TIME, REQUEST_PARTY DISTRIBUTOR_NUMBER, NUMBER_OF_BILL TOTAL_BILL, SUM(TOTAMT) TOTAL_AMOUNT, SUM(FEEBYI) FEES, SUM(VATBYI) VAT, SUM(TAXBYI) TAX, SUM(BNKCOM) BANK_COMMISSION, SUM(AGNCOM) AGENT_COMMISSION, SUM(TDRCOM) THIRD_PARTY_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 1/3,4)) MBL_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 2/3,4)) AAMRA_COMMISSION FROM (SELECT TRUNC(SR.REQUEST_TIME) REQUEST_TIME, SUBSTR (SR.REQUEST_PARTY, 4) || '1' REQUEST_PARTY,COUNT (CAT.REQUEST_ID) NUMBER_OF_BILL, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TOTAMT', SUM (CAS_TRAN_AMT))TOTAMT,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'FEEBYI',SUM (CAS_TRAN_AMT))FEEBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'VATBYI',SUM (CAS_TRAN_AMT))VATBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TAXBYI',SUM (CAS_TRAN_AMT))TAXBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'BNKCOM',SUM (CAS_TRAN_AMT))BNKCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'AGNCOM',SUM (CAS_TRAN_AMT))AGNCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TDRCOM',SUM (CAS_TRAN_AMT))TDRCOM FROM SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE ('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC (CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY TRUNC(SR.REQUEST_TIME), SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE) GROUP BY REQUEST_TIME, REQUEST_PARTY, NUMBER_OF_BILL) T1, ACCOUNT_LIST ALA, CLIENT_LIST CLA, MANAGE_THANA MTA, MANAGE_DISTRICT MDA WHERE T1.DISTRIBUTOR_NUMBER = ALA.ACCNT_NO AND ALA.CLINT_ID = CLA.CLINT_ID AND CLA.THANA_ID = MTA.THANA_ID AND MTA.DISTRICT_ID = MDA.DISTRICT_ID ORDER BY REQUEST_TIME ";

                // strWalletId = "Agent Wallet Id";
                // strWalletName = "Agent Name";
            // }

            // string strHTML = "", fileName = "";
            // lblMsg.Text = "";
            // int totalBill = 0;
            // double totalAmount = 0;
            // double fees = 0;
            // double vat = 0;
            // double tax = 0;
            // double bankCommission = 0;
            // double agentCommission = 0;
            // double thirdPartyCommission = 0;
            // double mblCommission = 0;
            // double aamraCommission = 0;

            // DataSet dtsAccount = new DataSet();
            // fileName = "Prepaid_Bill_Pay_Commission";
            // string strDate = dtpBPCPrepaidFrom.DateString + " To " + dtpBPCPrepaidTo.DateString;
            // //------------------------------------------Report File xl processing   -------------------------------------

            // dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            // strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            // strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            // strHTML = strHTML + "</table>";
            // strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            // strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            // strHTML = strHTML + "</table>";
            // strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            // strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MBL and Aamra Bill Pay Commission Report(Prepaid)</h2></td></tr>";
            // strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center> Date Range: " + strDate + "</h2></td></tr>";
            // strHTML = strHTML + "</table>";

            // strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            // strHTML = strHTML + "<tr>";

            // strHTML = strHTML + "<td valign='middle' >Sl</td>";
            // strHTML = strHTML + "<td valign='middle' >Execute Date</td>";
            // strHTML = strHTML + "<td valign='middle' >" + strWalletId + "</td>";
            // strHTML = strHTML + "<td valign='middle' >" + strWalletName + "</td>";
            // strHTML = strHTML + "<td valign='middle' >District</td>";
            // strHTML = strHTML + "<td valign='middle' >Total Bill</td>";
            // strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
            // strHTML = strHTML + "<td valign='middle' >Fees</td>";
            // strHTML = strHTML + "<td valign='middle' >VAT</td>";
            // strHTML = strHTML + "<td valign='middle' >TAX</td>";
            // strHTML = strHTML + "<td valign='middle' >Bank Commission</td>";
            // strHTML = strHTML + "<td valign='middle' >Agent Commission</td>";
            // strHTML = strHTML + "<td valign='middle' >Third Party Commission</td>";
            // strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";
            // strHTML = strHTML + "<td valign='middle' >Aamra Commission</td>";

            // strHTML = strHTML + "</tr>";

            // if (dtsAccount.Tables[0].Rows.Count > 0)
            // {
                // int SerialNo = 1;
                // foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                // {
                    // strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    // strHTML = strHTML + " <td > '" + prow["REQUEST_TIME"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NUMBER"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NAME"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > '" + prow["DISTRICT"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["TOTAL_BILL"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["TOTAL_AMOUNT"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["FEES"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["VAT"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["TAX"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["BANK_COMMISSION"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["AGENT_COMMISSION"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["MBL_COMMISSION"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > " + prow["AAMRA_COMMISSION"].ToString() + " </td>";

                    // strHTML = strHTML + " </tr> ";
                    // SerialNo = SerialNo + 1;
                    // totalBill = totalBill + Convert.ToInt32(prow["TOTAL_BILL"]);
                    // totalAmount = totalAmount + Convert.ToDouble(prow["TOTAL_AMOUNT"]);
                    // fees = fees + Convert.ToDouble(prow["FEES"]);
                    // vat = vat + Convert.ToDouble(prow["VAT"]);
                    // tax = tax + Convert.ToDouble(prow["TAX"]);
                    // bankCommission = bankCommission + Convert.ToDouble(prow["BANK_COMMISSION"]);
                    // agentCommission = agentCommission + Convert.ToDouble(prow["AGENT_COMMISSION"]);
                    // thirdPartyCommission = thirdPartyCommission + Convert.ToDouble(prow["THIRD_PARTY_COMMISSION"]);
                    // mblCommission = mblCommission + Convert.ToDouble(prow["MBL_COMMISSION"]);
                    // aamraCommission = aamraCommission + Convert.ToDouble(prow["AAMRA_COMMISSION"]);
                // }
            // }

            // strHTML = strHTML + "<tr>";
            // strHTML = strHTML + " <td > " + "" + " </td>";
            // strHTML = strHTML + " <td > " + "" + " </td>";
            // strHTML = strHTML + " <td > " + "" + " </td>";
            // strHTML = strHTML + " <td > " + "" + " </td>";
            // strHTML = strHTML + " <td > " + "" + " </td>";
            // strHTML = strHTML + " <td > " + totalBill + " </td>";
            // strHTML = strHTML + " <td > " + totalAmount + " </td>";
            // strHTML = strHTML + " <td > " + fees + " </td>";
            // strHTML = strHTML + " <td > " + vat + " </td>";
            // strHTML = strHTML + " <td > " + tax + " </td>";
            // strHTML = strHTML + " <td > " + bankCommission + " </td>";
            // strHTML = strHTML + " <td > " + agentCommission + " </td>";
            // strHTML = strHTML + " <td > " + thirdPartyCommission + " </td>";
            // strHTML = strHTML + " <td > " + mblCommission + " </td>";
            // strHTML = strHTML + " <td > " + aamraCommission + " </td>";
            // strHTML = strHTML + " </tr>";
            // strHTML = strHTML + " </table>";

            // SaveAuditInfo("Preview", fileName);
            // clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            // lblMsg.ForeColor = Color.White;
            // lblMsg.Text = "Report Generated Successfully...";
        // }
        // catch (Exception ex)
        // {
            // ex.Message.ToString();
        // }
		
		try
        {
            string strSql = "";
            string strWalletId = "";
            string strWalletName = "";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";
            int totalBill = 0;
            double totalAmount = 0;
            double fees = 0;
            double vat = 0;
            double tax = 0;
            double bankCommission = 0;
            double agentCommission = 0;
            double thirdPartyCommission = 0;
            double mblCommission = 0;
            double aamraCommission = 0;

            if (ddlReportType.SelectedValue.Equals("Distributor"))
            {
                //strSql = " SELECT DEL_ACCNT_NO DISTRIBUTOR_NUMBER, CLINT_NAME DISTRIBUTOR_NAME, DISTRICT_NAME DISTRICT, SUM(NUMBER_OF_BILL) TOTAL_BILL, SUM(TOTAMT) TOTAL_AMOUNT, SUM(FEEBYI) FEES, SUM(VATBYI) VAT, SUM(TAXBYI) TAX, SUM(BNKCOM) BANK_COMMISSION, SUM(AGNCOM) AGENT_COMMISSION, SUM(TDRCOM) THIRD_PARTY_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 1/3,4)) MBL_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 2/3,4)) AAMRA_COMMISSION FROM (SELECT THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, MDD.DISTRICT_NAME, NUMBER_OF_BILL, SUM(T1.TOTAMT) TOTAMT, SUM(T1.FEEBYI) FEEBYI, SUM(T1.VATBYI) VATBYI, SUM(T1.TAXBYI) TAXBYI, SUM(T1.BNKCOM) BNKCOM, SUM(T1.AGNCOM) AGNCOM, SUM(T1.TDRCOM) TDRCOM FROM (SELECT SUBSTR(SR.REQUEST_PARTY,4) || '1' REQUEST_PARTY, COUNT(CAT.REQUEST_ID) NUMBER_OF_BILL, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'TOTAMT', SUM(CAS_TRAN_AMT)) TOTAMT, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'FEEBYI', SUM(CAS_TRAN_AMT)) FEEBYI, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'VATBYI', SUM(CAS_TRAN_AMT)) VATBYI, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'TAXBYI', SUM(CAS_TRAN_AMT)) TAXBYI, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'BNKCOM', SUM(CAS_TRAN_AMT)) BNKCOM, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'AGNCOM', SUM(CAS_TRAN_AMT)) AGNCOM, DECODE(CAT.CAS_TRAN_PURPOSE_CODE, 'TDRCOM', SUM(CAS_TRAN_AMT)) TDRCOM FROM SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE ) T1, TEMP_HIERARCHY_LIST_ALL THLA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE T1.REQUEST_PARTY= THLA.A_ACCNT_NO AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID GROUP BY THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, MDD.DISTRICT_NAME, NUMBER_OF_BILL) GROUP BY DEL_ACCNT_NO, CLINT_NAME, DISTRICT_NAME ";

                strSql = "SELECT TO_CHAR(REQUEST_TIME) REQUEST_TIME, DEL_ACCNT_NO DISTRIBUTOR_NUMBER,CLINT_NAME DISTRIBUTOR_NAME,DISTRICT_NAME DISTRICT,SUM (NUMBER_OF_BILL) TOTAL_BILL,SUM (TOTAMT) TOTAL_AMOUNT,SUM (FEEBYI) FEES,SUM (VATBYI) VAT,SUM (TAXBYI) TAX,SUM (BNKCOM) BANK_COMMISSION,SUM (AGNCOM) AGENT_COMMISSION,SUM (TDRCOM) THIRD_PARTY_COMMISSION,TO_CHAR (ROUND (SUM (BNKCOM) * 1 / 3, 4)) MBL_COMMISSION,TO_CHAR (ROUND (SUM (BNKCOM) * 2 / 3, 4)) AAMRA_COMMISSION FROM (SELECT REQUEST_TIME, THLA.DEL_ACCNT_NO,CLD.CLINT_NAME,MDD.DISTRICT_NAME,NUMBER_OF_BILL,SUM (T1.TOTAMT) TOTAMT,SUM (T1.FEEBYI) FEEBYI,SUM (T1.VATBYI) VATBYI,SUM (T1.TAXBYI) TAXBYI,SUM (T1.BNKCOM) BNKCOM,SUM (T1.AGNCOM) AGNCOM,SUM (T1.TDRCOM) TDRCOM FROM (SELECT TRUNC(SR.REQUEST_TIME) REQUEST_TIME, SUBSTR (SR.REQUEST_PARTY, 4) || '1' REQUEST_PARTY,COUNT (CAT.REQUEST_ID) NUMBER_OF_BILL,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TOTAMT',SUM (CAS_TRAN_AMT)) TOTAMT,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'FEEBYI',SUM (CAS_TRAN_AMT))FEEBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'VATBYI',SUM (CAS_TRAN_AMT))VATBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TAXBYI',SUM (CAS_TRAN_AMT)) TAXBYI, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'BNKCOM',SUM (CAS_TRAN_AMT)) BNKCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'AGNCOM',SUM (CAS_TRAN_AMT)) AGNCOM, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TDRCOM',SUM (CAS_TRAN_AMT)) TDRCOM FROM VW_SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE ('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC (CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY TRUNC(REQUEST_TIME), SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE) T1, TEMP_HIERARCHY_LIST_ALL THLA,ACCOUNT_LIST ALD,CLIENT_LIST CLD,MANAGE_THANA MTD,MANAGE_DISTRICT MDD WHERE T1.REQUEST_PARTY = THLA.A_ACCNT_NO AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID GROUP BY REQUEST_TIME, THLA.DEL_ACCNT_NO,CLD.CLINT_NAME,MDD.DISTRICT_NAME,NUMBER_OF_BILL) GROUP BY REQUEST_TIME, DEL_ACCNT_NO, CLINT_NAME, DISTRICT_NAME ORDER BY REQUEST_TIME";

                strWalletId = "Distributor Wallet Id";
                strWalletName = "Distributor Name";

                DataSet dtsAccount = new DataSet();
                fileName = "Prepaid_Bill_Pay_Commission";
                string strDate = dtpBPCPrepaidFrom.DateString + " To " + dtpBPCPrepaidTo.DateString;
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MBL and Aamra Bill Pay Commission Report(Prepaid)</h2></td></tr>";
                strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center> Date Range: " + strDate + "</h2></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Execute Date</td>";
                strHTML = strHTML + "<td valign='middle' >" + strWalletId + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + strWalletName + "</td>";
                strHTML = strHTML + "<td valign='middle' >District</td>";
                strHTML = strHTML + "<td valign='middle' >Total Bill</td>";
                strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Fees</td>";
                strHTML = strHTML + "<td valign='middle' >VAT</td>";
                strHTML = strHTML + "<td valign='middle' >TAX</td>";
                strHTML = strHTML + "<td valign='middle' >Bank Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Agent Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Third Party Commission</td>";
                strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Aamra Commission</td>";

                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_TIME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NUMBER"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TOTAL_BILL"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TOTAL_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["FEES"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["VAT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TAX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["BANK_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["AGENT_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["MBL_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["AAMRA_COMMISSION"].ToString() + " </td>";

                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                        totalBill = totalBill + Convert.ToInt32(prow["TOTAL_BILL"]);
                        totalAmount = totalAmount + Convert.ToDouble(prow["TOTAL_AMOUNT"]);
                        fees = fees + Convert.ToDouble(prow["FEES"]);
                        vat = vat + Convert.ToDouble(prow["VAT"]);
                        tax = tax + Convert.ToDouble(prow["TAX"]);
                        bankCommission = bankCommission + Convert.ToDouble(prow["BANK_COMMISSION"]);
                        agentCommission = agentCommission + Convert.ToDouble(prow["AGENT_COMMISSION"]);
                        thirdPartyCommission = thirdPartyCommission + Convert.ToDouble(prow["THIRD_PARTY_COMMISSION"]);
                        mblCommission = mblCommission + Convert.ToDouble(prow["MBL_COMMISSION"]);
                        aamraCommission = aamraCommission + Convert.ToDouble(prow["AAMRA_COMMISSION"]);
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + totalBill + " </td>";
                strHTML = strHTML + " <td > " + totalAmount + " </td>";
                strHTML = strHTML + " <td > " + fees + " </td>";
                strHTML = strHTML + " <td > " + vat + " </td>";
                strHTML = strHTML + " <td > " + tax + " </td>";
                strHTML = strHTML + " <td > " + bankCommission + " </td>";
                strHTML = strHTML + " <td > " + agentCommission + " </td>";
                strHTML = strHTML + " <td > " + thirdPartyCommission + " </td>";
                strHTML = strHTML + " <td > " + mblCommission + " </td>";
                strHTML = strHTML + " <td > " + aamraCommission + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

            }
            else
            {
                //strSql = " SELECT DISTRIBUTOR_NUMBER, CLA.CLINT_NAME DISTRIBUTOR_NAME, MDA.DISTRICT_NAME DISTRICT, TOTAL_BILL, TOTAL_AMOUNT, FEES, VAT, TAX, BANK_COMMISSION, AGENT_COMMISSION, THIRD_PARTY_COMMISSION, MBL_COMMISSION, AAMRA_COMMISSION FROM (SELECT REQUEST_PARTY DISTRIBUTOR_NUMBER, NUMBER_OF_BILL TOTAL_BILL, SUM(TOTAMT) TOTAL_AMOUNT, SUM(FEEBYI) FEES, SUM(VATBYI) VAT, SUM(TAXBYI) TAX, SUM(BNKCOM) BANK_COMMISSION, SUM(AGNCOM) AGENT_COMMISSION, SUM(TDRCOM) THIRD_PARTY_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 1/3,4)) MBL_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 2/3,4)) AAMRA_COMMISSION FROM (SELECT SUBSTR (SR.REQUEST_PARTY, 4) || '1' REQUEST_PARTY,COUNT (CAT.REQUEST_ID) NUMBER_OF_BILL, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TOTAMT',SUM (CAS_TRAN_AMT))TOTAMT,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'FEEBYI',SUM (CAS_TRAN_AMT))FEEBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'VATBYI',SUM (CAS_TRAN_AMT))VATBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TAXBYI',SUM (CAS_TRAN_AMT))TAXBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'BNKCOM',SUM (CAS_TRAN_AMT))BNKCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'AGNCOM',SUM (CAS_TRAN_AMT))AGNCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TDRCOM',SUM (CAS_TRAN_AMT))TDRCOM FROM SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE ('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC (CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE) GROUP BY REQUEST_PARTY, NUMBER_OF_BILL) T1, ACCOUNT_LIST ALA, CLIENT_LIST CLA, MANAGE_THANA MTA, MANAGE_DISTRICT MDA WHERE T1.DISTRIBUTOR_NUMBER = ALA.ACCNT_NO AND ALA.CLINT_ID = CLA.CLINT_ID AND CLA.THANA_ID = MTA.THANA_ID AND MTA.DISTRICT_ID = MDA.DISTRICT_ID ";

                //strSql = " SELECT TO_CHAR(REQUEST_TIME) REQUEST_TIME, DISTRIBUTOR_NUMBER, CLA.CLINT_NAME DISTRIBUTOR_NAME, MDA.DISTRICT_NAME DISTRICT, TOTAL_BILL, TOTAL_AMOUNT, FEES, VAT, TAX, BANK_COMMISSION, AGENT_COMMISSION, THIRD_PARTY_COMMISSION, MBL_COMMISSION, AAMRA_COMMISSION FROM (SELECT REQUEST_TIME, REQUEST_PARTY DISTRIBUTOR_NUMBER, NUMBER_OF_BILL TOTAL_BILL, SUM(TOTAMT) TOTAL_AMOUNT, SUM(FEEBYI) FEES, SUM(VATBYI) VAT, SUM(TAXBYI) TAX, SUM(BNKCOM) BANK_COMMISSION, SUM(AGNCOM) AGENT_COMMISSION, SUM(TDRCOM) THIRD_PARTY_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 1/3,4)) MBL_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 2/3,4)) AAMRA_COMMISSION FROM (SELECT TRUNC(SR.REQUEST_TIME) REQUEST_TIME, SUBSTR (SR.REQUEST_PARTY, 4) || '1' REQUEST_PARTY,COUNT (CAT.REQUEST_ID) NUMBER_OF_BILL, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TOTAMT', SUM (CAS_TRAN_AMT))TOTAMT,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'FEEBYI',SUM (CAS_TRAN_AMT))FEEBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'VATBYI',SUM (CAS_TRAN_AMT))VATBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TAXBYI',SUM (CAS_TRAN_AMT))TAXBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'BNKCOM',SUM (CAS_TRAN_AMT))BNKCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'AGNCOM',SUM (CAS_TRAN_AMT))AGNCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TDRCOM',SUM (CAS_TRAN_AMT))TDRCOM FROM SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE ('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC (CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY TRUNC(SR.REQUEST_TIME), SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE) GROUP BY REQUEST_TIME, REQUEST_PARTY, NUMBER_OF_BILL) T1, ACCOUNT_LIST ALA, CLIENT_LIST CLA, MANAGE_THANA MTA, MANAGE_DISTRICT MDA WHERE T1.DISTRIBUTOR_NUMBER = ALA.ACCNT_NO AND ALA.CLINT_ID = CLA.CLINT_ID AND CLA.THANA_ID = MTA.THANA_ID AND MTA.DISTRICT_ID = MDA.DISTRICT_ID ORDER BY REQUEST_TIME ";

                strSql = " SELECT TO_CHAR(REQUEST_TIME) REQUEST_TIME, THLA.DIS_ACCNT_NO, CLD.CLINT_NAME DIS_NAME, DISTRIBUTOR_NUMBER, CLA.CLINT_NAME DISTRIBUTOR_NAME, MDA.DISTRICT_NAME DISTRICT, TOTAL_BILL, TOTAL_AMOUNT, FEES, VAT, TAX, BANK_COMMISSION, AGENT_COMMISSION, THIRD_PARTY_COMMISSION, MBL_COMMISSION, AAMRA_COMMISSION FROM (SELECT REQUEST_TIME, REQUEST_PARTY DISTRIBUTOR_NUMBER, NUMBER_OF_BILL TOTAL_BILL, SUM(TOTAMT) TOTAL_AMOUNT, SUM(FEEBYI) FEES, SUM(VATBYI) VAT, SUM(TAXBYI) TAX, SUM(BNKCOM) BANK_COMMISSION, SUM(AGNCOM) AGENT_COMMISSION, SUM(TDRCOM) THIRD_PARTY_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 1/3,4)) MBL_COMMISSION, TO_CHAR(ROUND(SUM(BNKCOM) * 2/3,4)) AAMRA_COMMISSION FROM (SELECT TRUNC(SR.REQUEST_TIME) REQUEST_TIME, SUBSTR (SR.REQUEST_PARTY, 4) || '1' REQUEST_PARTY,COUNT (CAT.REQUEST_ID) NUMBER_OF_BILL, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TOTAMT', SUM (CAS_TRAN_AMT))TOTAMT,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'FEEBYI',SUM (CAS_TRAN_AMT))FEEBYI, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'VATBYI',SUM (CAS_TRAN_AMT))VATBYI,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TAXBYI',SUM (CAS_TRAN_AMT))TAXBYI, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'BNKCOM',SUM (CAS_TRAN_AMT))BNKCOM,DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'AGNCOM',SUM (CAS_TRAN_AMT))AGNCOM, DECODE (CAT.CAS_TRAN_PURPOSE_CODE,'TDRCOM',SUM (CAS_TRAN_AMT))TDRCOM FROM VW_SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_TEXT LIKE ('*UBPDP*%') AND SR.REQUEST_ID = CAT.REQUEST_ID AND TRUNC (CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBPCPrepaidFrom.DateString + "' AND '" + dtpBPCPrepaidTo.DateString + "' GROUP BY TRUNC(SR.REQUEST_TIME), SR.REQUEST_PARTY, CAT.CAS_TRAN_PURPOSE_CODE) GROUP BY REQUEST_TIME, REQUEST_PARTY, NUMBER_OF_BILL) T1, ACCOUNT_LIST ALA, CLIENT_LIST CLA, MANAGE_THANA MTA, MANAGE_DISTRICT MDA, TEMP_HIERARCHY_LIST_ALL THLA, ACCOUNT_LIST ALD, CLIENT_LIST CLD WHERE T1.DISTRIBUTOR_NUMBER = ALA.ACCNT_NO AND ALA.CLINT_ID = CLA.CLINT_ID AND CLA.THANA_ID = MTA.THANA_ID AND MTA.DISTRICT_ID = MDA.DISTRICT_ID AND THLA.A_ACCNT_NO = T1.DISTRIBUTOR_NUMBER AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID ORDER BY REQUEST_TIME, ALD.ACCNT_NO ";

                strWalletId = "Agent Wallet Id";
                strWalletName = "Agent Name";

                DataSet dtsAccount = new DataSet();
                fileName = "Prepaid_Bill_Pay_Commission";
                string strDate = dtpBPCPrepaidFrom.DateString + " To " + dtpBPCPrepaidTo.DateString;
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>MBL and Aamra Bill Pay Commission Report(Prepaid)</h2></td></tr>";
                strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center> Date Range: " + strDate + "</h2></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Execute Date</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Wallet Id</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >" + strWalletId + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + strWalletName + "</td>";
                strHTML = strHTML + "<td valign='middle' >District</td>";
                strHTML = strHTML + "<td valign='middle' >Total Bill</td>";
                strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Fees</td>";
                strHTML = strHTML + "<td valign='middle' >VAT</td>";
                strHTML = strHTML + "<td valign='middle' >TAX</td>";
                strHTML = strHTML + "<td valign='middle' >Bank Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Agent Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Third Party Commission</td>";
                strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Aamra Commission</td>";

                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_TIME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ACCNT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NUMBER"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TOTAL_BILL"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TOTAL_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["FEES"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["VAT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TAX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["BANK_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["AGENT_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["MBL_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["AAMRA_COMMISSION"].ToString() + " </td>";

                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                        totalBill = totalBill + Convert.ToInt32(prow["TOTAL_BILL"]);
                        totalAmount = totalAmount + Convert.ToDouble(prow["TOTAL_AMOUNT"]);
                        fees = fees + Convert.ToDouble(prow["FEES"]);
                        vat = vat + Convert.ToDouble(prow["VAT"]);
                        tax = tax + Convert.ToDouble(prow["TAX"]);
                        bankCommission = bankCommission + Convert.ToDouble(prow["BANK_COMMISSION"]);
                        agentCommission = agentCommission + Convert.ToDouble(prow["AGENT_COMMISSION"]);
                        thirdPartyCommission = thirdPartyCommission + Convert.ToDouble(prow["THIRD_PARTY_COMMISSION"]);
                        mblCommission = mblCommission + Convert.ToDouble(prow["MBL_COMMISSION"]);
                        aamraCommission = aamraCommission + Convert.ToDouble(prow["AAMRA_COMMISSION"]);
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
                strHTML = strHTML + " <td > " + totalBill + " </td>";
                strHTML = strHTML + " <td > " + totalAmount + " </td>";
                strHTML = strHTML + " <td > " + fees + " </td>";
                strHTML = strHTML + " <td > " + vat + " </td>";
                strHTML = strHTML + " <td > " + tax + " </td>";
                strHTML = strHTML + " <td > " + bankCommission + " </td>";
                strHTML = strHTML + " <td > " + agentCommission + " </td>";
                strHTML = strHTML + " <td > " + thirdPartyCommission + " </td>";
                strHTML = strHTML + " <td > " + mblCommission + " </td>";
                strHTML = strHTML + " <td > " + aamraCommission + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
            }

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
	
	protected void btnDCSPBazarReport_Click(object sender, EventArgs e)
    {
        try
        {
            int totalBill = 0;
            double totalAmount = 0;
            double totalCommission = 0;
            double totalSMSCost = 0;
            double totalCommissionWithoutSMSCost = 0;
            string strSql = "";

			//strSql = " SELECT AL.ACCNT_NO, AR.RANK_TITEL, MD.DISTRICT_NAME, CL.CLINT_NAME, COUNT(TM.REQUEST_ID) COUNT_BP, SUM(TM.TRANSACTION_AMOUNT) AMOUNT_BP, SUM(TM.THIRDPARTY_COM_AMOUNT) TOTAL_COMM, (COUNT(TM.REQUEST_ID) * 0.5) SMS_COST, SUM(TM.THIRDPARTY_COM_AMOUNT) - (COUNT(TM.REQUEST_ID) * 0.5) AMT_WITUT_SMS_COST FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD,SERVICE_REQUEST SR, UTILITY_TRANSACTION UT WHERE TM.SERVICE_CODE = 'UWZP' AND TM.REQUEST_PARTY||1 = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND MT.DISTRICT_ID = MD.DISTRICT_ID AND AL.ACCNT_RANK_ID IN ('180128000000000008') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDCSFrDate.DateString + "' AND '" + dtpDCSToDate.DateString + "' AND SR.REQUEST_ID = TM.REQUEST_ID AND SR.REQUEST_PARTY_TYPE IN ('AIRTEL_USSD', 'BANGLALINK_USSD', 'ROBI_USSD', 'WAP', 'MCOM_GATEWAY') AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' GROUP BY AL.ACCNT_NO, AR.RANK_TITEL, MD.DISTRICT_NAME, CL.CLINT_NAME ORDER BY AL.ACCNT_NO ";
			
			if (ddlChannelType.SelectedValue == "1")
            {
                strSql = " SELECT AL.ACCNT_NO, AR.RANK_TITEL, MD.DISTRICT_NAME, CL.CLINT_NAME, COUNT(TM.REQUEST_ID) COUNT_BP, SUM(TM.TRANSACTION_AMOUNT) AMOUNT_BP, SUM(TM.THIRDPARTY_COM_AMOUNT) TOTAL_COMM, (COUNT(TM.REQUEST_ID) * 0.5) SMS_COST, SUM(TM.THIRDPARTY_COM_AMOUNT) - (COUNT(TM.REQUEST_ID) * 0.5) AMT_WITUT_SMS_COST FROM TEMP_MIS_TRANSACTIONS_REPORT_A TM, ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD, VW_SERVICE_REQUEST SR, VW_UTILITY_TRANSACTION UT WHERE TM.SERVICE_CODE = 'UWZP' AND TM.REQUEST_PARTY||1 = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND MT.DISTRICT_ID = MD.DISTRICT_ID AND AL.ACCNT_RANK_ID IN ('180128000000000008') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDCSFrDate.DateString + "' AND '" + dtpDCSToDate.DateString + "' AND SR.REQUEST_ID = TM.REQUEST_ID AND SR.REQUEST_PARTY_TYPE IN ('WAP', 'MCOM_GATEWAY') AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' GROUP BY AL.ACCNT_NO, AR.RANK_TITEL, MD.DISTRICT_NAME, CL.CLINT_NAME ORDER BY AL.ACCNT_NO ";
            }
            else if (ddlChannelType.SelectedValue == "2")
            {
                strSql = " SELECT AL.ACCNT_NO, AR.RANK_TITEL, MD.DISTRICT_NAME, CL.CLINT_NAME, COUNT(TM.REQUEST_ID) COUNT_BP, SUM(TM.TRANSACTION_AMOUNT) AMOUNT_BP, SUM(TM.THIRDPARTY_COM_AMOUNT) TOTAL_COMM, (COUNT(TM.REQUEST_ID) * 0.5) SMS_COST, SUM(TM.THIRDPARTY_COM_AMOUNT) - (COUNT(TM.REQUEST_ID) * 0.5) AMT_WITUT_SMS_COST FROM TEMP_MIS_TRANSACTIONS_REPORT_A TM, ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD, VW_SERVICE_REQUEST SR, VW_UTILITY_TRANSACTION UT WHERE TM.SERVICE_CODE = 'UWZP' AND TM.REQUEST_PARTY||1 = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND MT.DISTRICT_ID = MD.DISTRICT_ID AND AL.ACCNT_RANK_ID IN ('180128000000000008') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDCSFrDate.DateString + "' AND '" + dtpDCSToDate.DateString + "' AND SR.REQUEST_ID = TM.REQUEST_ID AND SR.REQUEST_PARTY_TYPE IN ('AIRTEL_USSD', 'BANGLALINK_USSD', 'ROBI_USSD') AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND UT.RESPONSE_STATUS_BP = '000' GROUP BY AL.ACCNT_NO, AR.RANK_TITEL, MD.DISTRICT_NAME, CL.CLINT_NAME ORDER BY AL.ACCNT_NO ";
            }

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "PBazar_Bill_Pay_Commission";
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Commission Statement WEST ZONE(AIRTEL_USSD, BANGLALINK_USSD, ROBI_USSD, WAP, MCOM_GATEWAY)</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDCSFrDate.DateString + "' To '" + dtpDCSToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Number</td>";
            strHTML = strHTML + "<td valign='middle' >Account Rank</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >Account Name</td>";
            strHTML = strHTML + "<td valign='middle' >Total No of BIll Pay</td>";
            strHTML = strHTML + "<td valign='middle' >Total BIll Pay Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Total Third Party Commission</td>";
            strHTML = strHTML + "<td valign='middle' >SMS Cost(0.50)</td>";
            strHTML = strHTML + "<td valign='middle' >Amount after SMS Cost</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["COUNT_BP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + Convert.ToDouble(prow["AMOUNT_BP"]).ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > '" + Convert.ToDouble(prow["TOTAL_COMM"]).ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > '" + Convert.ToDouble(prow["SMS_COST"]).ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > '" + Convert.ToDouble(prow["AMT_WITUT_SMS_COST"]).ToString("N2") + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    totalBill = totalBill + Convert.ToInt32(prow["COUNT_BP"]);
                    totalAmount = totalAmount + Convert.ToDouble(prow["AMOUNT_BP"]);
                    totalCommission = totalCommission + Convert.ToDouble(prow["TOTAL_COMM"]);
                    totalSMSCost = totalSMSCost + Convert.ToDouble(prow["SMS_COST"]);
                    totalCommissionWithoutSMSCost = totalCommissionWithoutSMSCost + Convert.ToDouble(prow["AMT_WITUT_SMS_COST"]);
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "Total" + " </td>";
            strHTML = strHTML + " <td > " + totalBill.ToString() + " </td>";
            strHTML = strHTML + " <td > " + totalAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + totalCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + totalSMSCost.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + totalCommissionWithoutSMSCost.ToString("N2") + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnAICAStatus_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            if (ddlCASearch.SelectedItem.Value == "C")
            {
                strSql = " SELECT RANK_TITEL,AREA,CLINT_GENDER,ACCNT_STATE, SUM(TOTAL) TOTAL, (SUM(TOTAL) - SUM(ACTIVE_STATUS)) ACTIVE, SUM(ACTIVE_STATUS) INACTIVE FROM (SELECT RANK_TITEL, AREA, CLINT_GENDER, ACCNT_STATE,COUNT(ACCNT_NO) TOTAL,DECODE(CLINT_GENDER,'M',COUNT(CLINT_GENDER)) MALE, DECODE(CLINT_GENDER,'F',COUNT(CLINT_GENDER)) FEMALE, DECODE(ACTIVE_STATUS,'0',COUNT(ACCNT_NO)) ACTIVE_STATUS FROM (SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Urban' AREA, (SELECT COUNT(*) FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN (TO_DATE('" + dtpCADetailTo.DateString + "')-(TO_CHAR(LAST_DAY('" + dtpCADetailTo.DateString + "'),'DD') - 1)) AND '" + dtpCADetailTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND AL.ACCNT_RANK_ID IN ('120519000000000006') AND MT.THANA_NAME LIKE ('%Sadar%') AND AL.ACCNT_STATE = 'A' AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "' UNION ALL SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, (SELECT COUNT(*) FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN (TO_DATE('" + dtpCADetailTo.DateString + "')-(TO_CHAR(LAST_DAY('" + dtpCADetailTo.DateString + "'),'DD') - 1)) AND '" + dtpCADetailTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND AL.ACCNT_RANK_ID IN ('120519000000000006') AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND AL.ACCNT_STATE = 'A' AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "' UNION ALL SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, (SELECT COUNT(*) FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN (TO_DATE('" + dtpCADetailTo.DateString + "')-(TO_CHAR(LAST_DAY('" + dtpCADetailTo.DateString + "'),'DD') - 1)) AND '" + dtpCADetailTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND AL.ACCNT_RANK_ID IN ('120519000000000006') AND CL.THANA_ID IS NULL AND AL.ACCNT_STATE = 'A' AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "') GROUP BY RANK_TITEL,AREA,ACCNT_STATE,CLINT_GENDER,ACTIVE_STATUS) GROUP BY RANK_TITEL,AREA,CLINT_GENDER,ACCNT_STATE ORDER BY RANK_TITEL,AREA,CLINT_GENDER ";

                fileName = "Active_Inactive_Customer_Detail";
                strTitle = "Active / Inactive Customer Detail";
            }
            else if (ddlCASearch.SelectedItem.Value == "A")
            {
                strSql = " SELECT RANK_TITEL,AREA,CLINT_GENDER,ACCNT_STATE, SUM(TOTAL) TOTAL, (SUM(TOTAL) - SUM(ACTIVE_STATUS)) ACTIVE, SUM(ACTIVE_STATUS) INACTIVE FROM (SELECT RANK_TITEL, AREA, CLINT_GENDER, ACCNT_STATE,COUNT(ACCNT_NO) TOTAL,DECODE(CLINT_GENDER,'M',COUNT(CLINT_GENDER)) MALE,DECODE(CLINT_GENDER,'F',COUNT(CLINT_GENDER)) FEMALE, DECODE(ACTIVE_STATUS,'0',COUNT(ACCNT_NO)) ACTIVE_STATUS FROM (SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Urban' AREA, (SELECT COUNT(*) FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120712000000000004', '120712000000000009', '130922000000000004', '140917000000000004', '180128000000000008', '180305000000000005', '180416000000000001', '180416000000000002', '140410000000000004', '170422000000000003', '170422000000000004', '170718000000000001') AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "' UNION ALL SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, (SELECT COUNT(*) FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120712000000000004', '120712000000000009', '130922000000000004', '140917000000000004', '180128000000000008', '180305000000000005', '180416000000000001', '180416000000000002', '140410000000000004', '170422000000000003', '170422000000000004', '170718000000000001') AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "' UNION ALL SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, (SELECT COUNT(*) FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120712000000000004', '120712000000000009', '130922000000000004', '140917000000000004', '180128000000000008', '180305000000000005', '180416000000000001', '180416000000000002', '140410000000000004', '170422000000000003', '170422000000000004', '170718000000000001') AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpCADetailFrom.DateString + "' AND '" + dtpCADetailTo.DateString + "') GROUP BY RANK_TITEL,AREA,ACCNT_STATE,CLINT_GENDER,ACTIVE_STATUS) GROUP BY RANK_TITEL, AREA, CLINT_GENDER, ACCNT_STATE ORDER BY RANK_TITEL, AREA, CLINT_GENDER ";

                fileName = "Active_Inactive_Agent_Detail";
                strTitle = "Active / Inactive Agent Detail";
            }

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpCADetailFrom.DateString + "' To '" + dtpCADetailTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Rank Title</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Account State</td>";
            strHTML = strHTML + "<td valign='middle' >Total</td>";
            strHTML = strHTML + "<td valign='middle' >Active</td>";
            strHTML = strHTML + "<td valign='middle' >Inactive</td>";
            
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_STATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACTIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["INACTIVE"].ToString() + "</td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnAICB_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT RANK_TITEL,AREA,DECODE(CLINT_GENDER,'M','Male','F','Female','Others') CLINT_GENDER,DECODE(ACCNT_STATE,'A','Active','L','Locked','Inactive') ACCNT_STATE, SUM(TOTAL) TOTAL, (SUM(TOTAL) - SUM(ACTIVE_STATUS)) ACTIVE, SUM(ACTIVE_STATUS) INACTIVE, SUM(BALANCE) BALANCE FROM (SELECT RANK_TITEL, AREA, CLINT_GENDER, ACCNT_STATE,COUNT(ACCNT_NO) TOTAL,DECODE(CLINT_GENDER,'M',COUNT(CLINT_GENDER)) MALE, DECODE(CLINT_GENDER,'F',COUNT(CLINT_GENDER)) FEMALE, DECODE(ACTIVE_STATUS,'0',COUNT(ACCNT_NO)) ACTIVE_STATUS, SUM(BALANCE) BALANCE FROM (SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Urban' AREA, GET_FIS_BALANCE_BY_DATE(AL.ACCNT_NO,'" + dtpAICBTo.DateString + "') BALANCE, (SELECT COUNT(*) FROM BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN (TO_DATE('" + dtpAICBTo.DateString + "')-30) AND '" + dtpAICBTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND MT.THANA_NAME LIKE ('%Sadar%') AND AL.ACCNT_RANK_ID IN ('120519000000000006') AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpAICBFrom.DateString + "' AND '" + dtpAICBTo.DateString + "' UNION ALL SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, GET_FIS_BALANCE_BY_DATE(AL.ACCNT_NO,'" + dtpAICBTo.DateString + "') BALANCE, (SELECT COUNT(*) FROM BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN '" + dtpAICBFrom.DateString + "' AND '" + dtpAICBTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND AL.ACCNT_RANK_ID IN ('120519000000000006') AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpAICBFrom.DateString + "' AND '" + dtpAICBTo.DateString + "' UNION ALL SELECT AR.RANK_TITEL, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, GET_FIS_BALANCE_BY_DATE(AL.ACCNT_NO,'" + dtpAICBTo.DateString + "') BALANCE, (SELECT COUNT(*) FROM BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION WHERE CAS_ACC_ID = (SELECT CAS_ACC_ID FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST WHERE CAS_ACC_NO = AL.ACCNT_NO) AND TRUNC(CAS_TRAN_DATE) BETWEEN '" + dtpAICBFrom.DateString + "' AND '" + dtpAICBTo.DateString + "') ACTIVE_STATUS FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, ACCOUNT_RANK AR, ACCOUNT_SERIAL_DETAIL ASD WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND CL.THANA_ID IS NULL AND AL.ACCNT_RANK_ID IN ('120519000000000006') AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpAICBFrom.DateString + "' AND '" + dtpAICBTo.DateString + "') GROUP BY RANK_TITEL, AREA, ACCNT_STATE, CLINT_GENDER, ACTIVE_STATUS) GROUP BY RANK_TITEL,AREA,CLINT_GENDER,ACCNT_STATE ORDER BY RANK_TITEL,AREA,CLINT_GENDER ";

            fileName = "Active_Inactive_Customer_Balance";
            strTitle = "Active / Inactive Customer Balance";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpAICBFrom.DateString + "' To '" + dtpAICBTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Rank Title</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Account State</td>";
            strHTML = strHTML + "<td valign='middle' >Total</td>";
            strHTML = strHTML + "<td valign='middle' >Active</td>";
            strHTML = strHTML + "<td valign='middle' >Inactive</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_STATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACTIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["INACTIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BALANCE"].ToString() + "</td>";

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
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnCashInReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT SERVICE_CODE, AREA, DECODE(CLINT_GENDER,'M','Male','F','Female','Others') CLINT_GENDER, SUM(TOTAL_TRANSACTION) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT SERVICE_CODE, AREA, CLINT_GENDER, COUNT(REQUEST_ID) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Urban' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE = 'CN' AND MT.THANA_NAME LIKE ('%Sadar%') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpCashInFrom.DateString + "' AND '" + dtpCashInTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000005','140410000000000004','180128000000000008') UNION ALL SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE = 'CN' AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpCashInFrom.DateString + "' AND '" + dtpCashInTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000005','140410000000000004','180128000000000008') UNION ALL SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID IS NULL AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE = 'CN' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpCashInFrom.DateString + "' AND '" + dtpCashInTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000005','140410000000000004','180128000000000008')) GROUP BY SERVICE_CODE, REQUEST_ID, CLINT_GENDER, AREA) GROUP BY SERVICE_CODE, AREA, CLINT_GENDER ORDER BY SERVICE_CODE, AREA, CLINT_GENDER ";

            fileName = "Cashin_Report";
            strTitle = "Cash In Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpCashInFrom.DateString + "' To '" + dtpCashInTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnCashOutReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT SERVICE_CODE, AREA, DECODE(CLINT_GENDER,'M','Male','F','Female','Others') CLINT_GENDER, SUM(TOTAL_TRANSACTION) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT SERVICE_CODE, AREA, CLINT_GENDER, COUNT(REQUEST_ID) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Urban' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+)         AND AL.ACCNT_NO = TM.RECEPENT_PARTY AND TM.SERVICE_CODE IN ('CCT','SW') AND MT.THANA_NAME LIKE ('%Sadar%') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpCashOutFrom.DateString + "' AND '" + dtpCashOutTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000002', '180128000000000008', '130922000000000004') UNION ALL SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE, CL.CLINT_GENDER, 'Rural' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_NO = TM.RECEPENT_PARTY AND TM.SERVICE_CODE IN ('CCT','SW') AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpCashOutFrom.DateString + "' AND '" + dtpCashOutTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000005',  '120519000000000002', '180128000000000008', '130922000000000004') UNION ALL SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID IS NULL AND AL.ACCNT_NO = TM.RECEPENT_PARTY AND TM.SERVICE_CODE IN ('CCT','SW') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpCashOutFrom.DateString + "' AND '" + dtpCashOutTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000002', '180128000000000008', '130922000000000004')) GROUP BY SERVICE_CODE, REQUEST_ID, CLINT_GENDER, AREA) GROUP BY SERVICE_CODE, AREA, CLINT_GENDER ORDER BY SERVICE_CODE, AREA, CLINT_GENDER ";

            fileName = "Cashout_Report";
            strTitle = "Cash Out Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpCashOutFrom.DateString + "' To '" + dtpCashOutTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnBankDeposit_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT SERVICE_CODE, AREA, DECODE(CLINT_GENDER,'M','Male','Female') CLINT_GENDER, SUM(TOTAL_TRANSACTION) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT SERVICE_CODE, AREA, CLINT_GENDER, COUNT(REQUEST_ID) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Urban' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE IN ('BD') AND MT.THANA_NAME LIKE ('%Sadar%') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpBdFrom.DateString + "' AND '" + dtpBdTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000006') UNION ALL SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE IN ('BD') AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpBdFrom.DateString + "' AND '" + dtpBdTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000006') UNION ALL SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, TEMP_MIS_TRANSACTIONS_REPORT TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID IS NULL AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE IN ('BD') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpBdFrom.DateString + "' AND '" + dtpBdTo.DateString + "' AND AL.ACCNT_RANK_ID IN ('120519000000000006')) GROUP BY SERVICE_CODE, REQUEST_ID, CLINT_GENDER, AREA) GROUP BY SERVICE_CODE, AREA, CLINT_GENDER ORDER BY SERVICE_CODE, AREA, CLINT_GENDER ";

            fileName = "Bank_Deposit_Report";
            strTitle = "Bank Deposit Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBdFrom.DateString + "' To '" + dtpBdTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnSalaryDisburse_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT 'SD', AREA, DECODE(CLINT_GENDER,'M','Male','Female') CLINT_GENDER, SUM(TOTAL_TRANSACTION) TOTAL_TRANSACTION, SUM(CAS_PRS_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT 'SD', AREA, CLINT_GENDER, COUNT(CAS_PRS_ACCNT_ID) TOTAL_TRANSACTION, SUM(CAS_PRS_AMOUNT) CAS_PRS_AMOUNT FROM (SELECT 'SD', TM.CAS_PRS_ACCNT_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Urban' AREA, TM.CAS_PRS_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, BDMIT_ERP_101.CAS_PR_SALARY_ACCOUNT_INFO TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND  SUBSTR(AL.ACCNT_NO,1,11) || '2' = TM.CAS_PRS_ACC_NO AND MT.THANA_NAME LIKE ('%Sadar%') AND TRUNC(TM.CAS_PRS_DISBURSE_DATE) BETWEEN '" + dtpSdFrom.DateString + "' AND '" + dtpSdTo.DateString + "' UNION ALL SELECT 'SD', TM.CAS_PRS_ACCNT_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.CAS_PRS_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, BDMIT_ERP_101.CAS_PR_SALARY_ACCOUNT_INFO TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND SUBSTR(AL.ACCNT_NO,1,11) || '2' = TM.CAS_PRS_ACC_NO AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC(TM.CAS_PRS_DISBURSE_DATE) BETWEEN '" + dtpSdFrom.DateString + "' AND '" + dtpSdTo.DateString + "' UNION ALL SELECT 'SD', TM.CAS_PRS_ACCNT_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.CAS_PRS_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, BDMIT_ERP_101.CAS_PR_SALARY_ACCOUNT_INFO TM WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID IS NULL AND SUBSTR(AL.ACCNT_NO,1,11) || '2' = TM.CAS_PRS_ACC_NO  AND TRUNC(TM.CAS_PRS_DISBURSE_DATE) BETWEEN '" + dtpSdFrom.DateString + "' AND '" + dtpSdTo.DateString + "') GROUP BY 'SD', CAS_PRS_ACCNT_ID, CLINT_GENDER, AREA) GROUP BY 'SD', AREA, CLINT_GENDER ORDER BY 'SD', AREA, CLINT_GENDER ";

            fileName = "Bank_Deposit_Report";
            strTitle = "Bank Deposit Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpSdFrom.DateString + "' To '" + dtpSdTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnMerchantPayment_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT 'MP' SERVICE, AREA, DECODE(CLINT_GENDER,'M','Male','Female') CLINT_GENDER, SUM(TOTAL_TRANSACTION) TOTAL_TRANSACTION, SUM(AMOUNT) TRANSACTION_AMOUNT FROM (SELECT COUNT (*) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) AMOUNT,'Urban' AREA, CL.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND SERVICE_CODE IN ('MP') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND MT.THANA_NAME LIKE ('%Sadar%') AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpMpFrom.DateString + "' AND '" + dtpMpTo.DateString + "' GROUP BY CLINT_GENDER UNION ALL SELECT COUNT (*) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) AMOUNT,'Rural' AREA, CL.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND SERVICE_CODE IN ('MP') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpMpFrom.DateString + "' AND '" + dtpMpTo.DateString + "' GROUP BY CLINT_GENDER UNION ALL SELECT COUNT (*) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) AMOUNT,'Rural' AREA, CL.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND SERVICE_CODE IN ('MP') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND CL.THANA_ID IS NULL AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpMpFrom.DateString + "' AND '" + dtpMpTo.DateString + "' GROUP BY CLINT_GENDER) GROUP BY AREA, CLINT_GENDER ";

            fileName = "Merchant_Payment_Report";
            strTitle = "Merchant Payment Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBdFrom.DateString + "' To '" + dtpBdTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnMobileTopup_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT SERVICE_CODE, AREA, DECODE(CLINT_GENDER,'M','Male','Female') CLINT_GENDER, SUM(TOTAL_TRANSACTION) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT SERVICE_CODE, AREA, CLINT_GENDER, COUNT(REQUEST_ID) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Urban' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_MIS_TRANSACTIONS_REPORT TM, TOPUP_TRANSACTION TT WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE IN ('MTP') AND MT.THANA_NAME LIKE ('%Sadar%') AND TM.REQUEST_ID = TT.REQUEST_ID AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '"+dtpMtpFrom.DateString+"' AND '"+dtpMtpTo.DateString+"' AND TT.ALL_FINAL_STATUS = 'S' UNION ALL SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_MIS_TRANSACTIONS_REPORT TM, TOPUP_TRANSACTION TT WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE IN ('MTP') AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TM.REQUEST_ID = TT.REQUEST_ID AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '"+dtpMtpFrom.DateString+"' AND '"+dtpMtpTo.DateString+"' AND TT.ALL_FINAL_STATUS = 'S' UNION ALL SELECT TM.SERVICE_CODE, TM.REQUEST_ID, AL.ACCNT_NO, AL.ACCNT_STATE,CL.CLINT_GENDER, 'Rural' AREA, TM.TRANSACTION_AMOUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL, TEMP_MIS_TRANSACTIONS_REPORT TM, TOPUP_TRANSACTION TT WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID IS NULL AND AL.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TM.SERVICE_CODE IN ('MTP') AND TM.REQUEST_ID = TT.REQUEST_ID AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '"+dtpMtpFrom.DateString+"' AND '"+dtpMtpTo.DateString+"' AND TT.ALL_FINAL_STATUS = 'S' ) GROUP BY SERVICE_CODE, REQUEST_ID, CLINT_GENDER, AREA) GROUP BY SERVICE_CODE, AREA, CLINT_GENDER ORDER BY SERVICE_CODE, AREA, CLINT_GENDER ";

            fileName = "Mobile_Topup_Report";
            strTitle = "Mobile Topup Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBdFrom.DateString + "' To '" + dtpBdTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnBusinessCollection_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT 'FM' SERVICE, AREA, CLINT_GENDER,COUNT(REQUEST_ID) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM (SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO,CLDIS.CLINT_NAME DIS_NAME,CLDIS.CLINT_ADDRESS1 DIS_ADDR,MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT,TMIS.REQUEST_ID,'Urban' AREA, CLCOR.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE,ACCOUNT_LIST ALCOR,TEMP_HIERARCHY_LIST_ALL THA,ACCOUNT_LIST ALDIS,CLIENT_LIST CLDIS,MANAGE_THANA MTDIS,MANAGE_DISTRICT MDDIS, CLIENT_LIST CLCOR, MANAGE_THANA MTCOR WHERE TMIS.SERVICE_CODE = 'FM' AND TRUNC (TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBcFrom.DateString + "' AND '" + dtpBcTo.DateString + "' AND ALCOR.CLINT_ID = CLCOR.CLINT_ID(+) AND TMIS.REQUEST_PARTY || '1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004','180128000000000007') AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002','180416000000000001','181219000000000002','190519000000000003') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLCOR.THANA_ID = MTCOR.THANA_ID AND MTCOR.THANA_NAME LIKE ('%Sadar%') AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) UNION ALL SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO,CLDIS.CLINT_NAME DIS_NAME,CLDIS.CLINT_ADDRESS1 DIS_ADDR,MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT,TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT,TMIS.REQUEST_ID, 'Rural' AREA, CLCOR.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE,ACCOUNT_LIST ALCOR,TEMP_HIERARCHY_LIST_ALL THA,ACCOUNT_LIST ALDIS,CLIENT_LIST CLDIS,MANAGE_THANA MTDIS,MANAGE_DISTRICT MDDIS, CLIENT_LIST CLCOR, MANAGE_THANA MTCOR WHERE  TMIS.SERVICE_CODE = 'FM' AND TRUNC (TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBcFrom.DateString + "' AND '" + dtpBcTo.DateString + "' AND ALCOR.CLINT_ID = CLCOR.CLINT_ID(+) AND TMIS.REQUEST_PARTY || '1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004','180128000000000007') AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002','180416000000000001','181219000000000002','190519000000000003') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLCOR.THANA_ID = MTCOR.THANA_ID AND MTCOR.THANA_NAME NOT LIKE ('%Sadar%') AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) UNION ALL SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO,CLDIS.CLINT_NAME DIS_NAME,CLDIS.CLINT_ADDRESS1 DIS_ADDR,MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT,TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT,TMIS.REQUEST_ID, 'Rural' AREA, CLCOR.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE,ACCOUNT_LIST ALCOR,TEMP_HIERARCHY_LIST_ALL THA,ACCOUNT_LIST ALDIS,CLIENT_LIST CLDIS,MANAGE_THANA MTDIS,MANAGE_DISTRICT MDDIS, CLIENT_LIST CLCOR WHERE TMIS.SERVICE_CODE = 'FM' AND TRUNC (TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBcFrom.DateString + "' AND '" + dtpBcTo.DateString + "' AND ALCOR.CLINT_ID = CLCOR.CLINT_ID(+) AND TMIS.REQUEST_PARTY || '1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004','180128000000000007') AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002','180416000000000001','181219000000000002','190519000000000003') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLCOR.THANA_ID IS NULL AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+)) GROUP BY AREA, CLINT_GENDER ";

            fileName = "Business_Collection_Report";
            strTitle = "Business Collection Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBcFrom.DateString + "' To '" + dtpBcTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnUtilityTransaction_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT 'UBP' SERVICE, AREA, CLINT_GENDER, SUM(TOTAL_TRANSACTION) TOTAL_TRANSACTION, SUM(AMOUNT) TRANSACTION_AMOUNT FROM (SELECT COUNT(*) TOTAL_TRANSACTION,SUM (TOTAL_BILL_AMOUNT) AMOUNT,'Urban' AREA, CL.CLINT_GENDER FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT WHERE SERVICE IN ('UBP', 'UBPW', 'UWZP','UBPKG','UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND TRUNC (TRANSA_DATE) BETWEEN '" + dtpUtFrom.DateString + "' AND '" + dtpUtTo.DateString + "' AND MT.THANA_NAME LIKE ('%Sadar%') AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006','120519000000000005', '120519000000000006','161215000000000004','180128000000000008') GROUP BY CL.CLINT_GENDER UNION ALL SELECT COUNT(*) TOTAL_TRANSACTION,SUM (TOTAL_BILL_AMOUNT) AMOUNT,'Rural' AREA, CL.CLINT_GENDER FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT WHERE SERVICE IN ('UBP', 'UBPW', 'UWZP','UBPKG','UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND TRUNC (TRANSA_DATE) BETWEEN '" + dtpUtFrom.DateString + "' AND '" + dtpUtTo.DateString + "' AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006','120519000000000005', '120519000000000006','161215000000000004','180128000000000008') GROUP BY CL.CLINT_GENDER UNION ALL SELECT COUNT(*) TOTAL_TRANSACTION,SUM (BILL_AMOUNT) AMOUNT,'Urban' AREA, CL.CLINT_GENDER FROM DPDC_PREPAID_BILL_COL_DETAIL DL, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT WHERE EXECUTE_STATUS = 'Success' AND DL.AGENT_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND TRUNC (EXECUTE_DATE) BETWEEN '" + dtpUtFrom.DateString + "' AND '" + dtpUtTo.DateString + "' AND MT.THANA_NAME LIKE ('%Sadar%') GROUP BY CL.CLINT_GENDER UNION ALL SELECT COUNT(*) TOTAL_TRANSACTION,SUM (BILL_AMOUNT) AMOUNT,'Rural' AREA, CL.CLINT_GENDER FROM DPDC_PREPAID_BILL_COL_DETAIL DL, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT WHERE EXECUTE_STATUS = 'Success' AND DL.AGENT_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID AND TRUNC (EXECUTE_DATE) BETWEEN '" + dtpUtFrom.DateString + "' AND '" + dtpUtTo.DateString + "' AND MT.THANA_NAME NOT LIKE ('%Sadar%') GROUP BY CL.CLINT_GENDER) GROUP BY AREA, CLINT_GENDER ";

            fileName = "Utility_Transaction_Report";
            strTitle = "Utility Transaction Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpUtFrom.DateString + "' To '" + dtpUtTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnFundTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";

            strSql = " SELECT 'FT' SERVICE, AREA, DECODE(CLINT_GENDER,'M','Male','Female') CLINT_GENDER, SUM(TOTAL_TRANSACTION) TOTAL_TRANSACTION, SUM(AMOUNT) TRANSACTION_AMOUNT FROM (SELECT COUNT (*) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) AMOUNT,'Urban' AREA, CL.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND SERVICE_CODE IN ('FT') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND MT.THANA_NAME LIKE ('%Sadar%') AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFtFrom.DateString + "' AND '" + dtpFtTo.DateString + "' GROUP BY CLINT_GENDER UNION ALL SELECT COUNT (*) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) AMOUNT,'Rural' AREA, CL.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND SERVICE_CODE IN ('FT') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND MT.THANA_NAME NOT LIKE ('%Sadar%') AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFtFrom.DateString + "' AND '" + dtpFtTo.DateString + "' GROUP BY CLINT_GENDER UNION ALL SELECT COUNT (*) TOTAL_TRANSACTION, SUM(TRANSACTION_AMOUNT) AMOUNT,'Rural' AREA, CL.CLINT_GENDER FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND SERVICE_CODE IN ('FT') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND CL.THANA_ID IS NULL AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFtFrom.DateString + "' AND '" + dtpFtTo.DateString + "' GROUP BY CLINT_GENDER) GROUP BY AREA, CLINT_GENDER ";

            fileName = "Fund_Transfer_Report";
            strTitle = "Fund Transfer Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpFtFrom.DateString + "' To '" + dtpFtTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >Gender</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + " </td>";

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

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
	
	protected void btnSubMerchantFMTranReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string fileName = "", strHTML = "", strTitle = "";
            double totalAmount = 0;

            strSql = " SELECT DISTINCT TO_CHAR(TM.TRANSACTION_DATE) TRANSACTION_DATE, THLA.DEL_ACCNT_NO DISTRIBUTOR_WALLET, CLD.CLINT_NAME DISTRIBUTOR_NAME, TM.RECEPENT_PARTY DSE_WALLET, CLDSE.CLINT_NAME DSE_NAME, ALS.ACCNT_NO SUB_MERCHANT_WALLET, CLS.CLINT_NAME SUB_MERCHANT_NAME, CLS.CLINT_ADDRESS1 SUB_MERCHANT_ADDRESS, MDS.DISTRICT_NAME MERCHANT_DISTRICT, TM.TRANSACTION_AMOUNT FM_AMOUNT FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALS, CLIENT_LIST CLS, MANAGE_THANA MTS, MANAGE_DISTRICT MDS, ACCOUNT_LIST ALDSE, CLIENT_LIST CLDSE, TEMP_HIERARCHY_LIST_ALL THLA, ACCOUNT_LIST ALD, CLIENT_LIST CLD WHERE TM.REQUEST_PARTY || '1' = ALS.ACCNT_NO AND ALS.ACCNT_RANK_ID = '180128000000000003' AND TM.SERVICE_CODE = 'FM' AND ALS.CLINT_ID = CLS.CLINT_ID AND CLS.THANA_ID = MTS.THANA_ID AND MTS.DISTRICT_ID = MDS.DISTRICT_ID AND TM.RECEPENT_PARTY = ALDSE.ACCNT_NO AND ALDSE.CLINT_ID = CLDSE.CLINT_ID AND TM.RECEPENT_PARTY = THLA.SA_ACCNT_NO AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND TM.TRANSACTION_DATE BETWEEN '" + dtpSMFMTDRFrom.DateString + "' AND '" + dtpSMFMTDRTo.DateString + "' ORDER BY TRANSACTION_DATE ";

            fileName = "OM_Sub_Merchant_FM_Transfer_Report";
            strTitle = "Sub Merchant FM Transaction Detail Report";

            DataSet dtsAccount = new DataSet();
            //------------------------------------------Report File xl processing   -------------------------------------
            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h4></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpSMFMTDRFrom.DateString + "' To '" + dtpSMFMTDRTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Name</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Name</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant Address</td>";
            strHTML = strHTML + "<td valign='middle' >Sub Merchant District</td>";
            strHTML = strHTML + "<td valign='middle' >FM Amount</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSACTION_DATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_WALLET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_WALLET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT_WALLET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUB_MERCHANT_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MERCHANT_DISTRICT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["FM_AMOUNT"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                    totalAmount = totalAmount + Convert.ToDouble(prow["FM_AMOUNT"].ToString());
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
            strHTML = strHTML + " <td > " + totalAmount.ToString() + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
