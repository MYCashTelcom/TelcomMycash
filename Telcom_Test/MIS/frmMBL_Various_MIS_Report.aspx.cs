using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MIS_frmMBL_Various_MIS_Report : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{       

        //if (rbtnRank.SelectedValue == "1")
            if (checkBoxRank.Checked == false)
            {
                EnableRank();
                //btnInterimMISReport.Enabled = true;
                //btnInterimMISReport.Attributes.Add("OnClick", "this.disabled = false; _doPostBack('btnInterimMISReport',''); return true;");

                DateTime dt = DateTime.Now;
                DateTime dt1 = DateTime.Now;
                              
                //try
                //{
                    dtpInterim.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));
                    dtpDisPerformance.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));
                    dtpAgentPerformance.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));
                    dtpAgentCommFstMonth.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));
                    dtpAgentComm2ndMonth.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));
                    //dtpAgentAqui.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));

                //    strUserName = Session["UserLoginName"].ToString();
                //    strPassword = Session["Password"].ToString();
                //}
                //catch
                //{
                //    Session.Clear();
                //    Response.Redirect("../frmSeesionExpMesage.aspx");
                //}
            }

            //else if (rbtnRank.SelectedValue== "2")
            else if (checkBoxRank.Checked == true)    
            {
                DisableRank();
                //btnInterimMISReport.Enabled = true;
                //btnInterimMISReport.Attributes.Add("OnClick", "this.disabled = false; _doPostBack('btnInterimMISReport',''); return true;");

                DateTime dt = DateTime.Now;
                DateTime dt1 = DateTime.Now;
                //try
                //{
                    dtpInterim.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));
                    dtpAgentPerformance.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));
                    dtpAgentCommFstMonth.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));
                    dtpAgentComm2ndMonth.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));
                    //dtpAgentAqui.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1));
                    strUserName = Session["UserLoginName"].ToString();
                    strPassword = Session["Password"].ToString();
                //}
                //catch
                //{
                //    Session.Clear();
                //    Response.Redirect("../frmSeesionExpMesage.aspx");
                //}
            }

            else
            {
                ///
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
        //}
    }

    #region Various Fund Transfer Report 

    private void EnableRank()
    {
        ddlSourceRank.Enabled = true;
        ddlDestinationRank.Enabled = true;
    }

    private void DisableRank()
    {
        ddlSourceRank.Enabled = false;
        ddlDestinationRank.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        string strQuery = "", strProcedure = "", strDateRange = "", strMsg = "";
        double transactionAmount = 0;
        double transactionFee = 0;
        double totalFee = 0;

        clsServiceHandler objServiceHandler = new clsServiceHandler();

        #region Daily customer Cash in

        //Daily Customer Cash In Report At Agent Point

        if (ddlServiceCode.SelectedValue == "CN" && ddlSourceRank.SelectedItem.Text == "MBL Agent" && ddlDestinationRank.SelectedItem.Text == "MBL Customer")
        {
            //strProcedure = " PKG_MIS_REPORTS.MIS_TRANSACTIONS_REPORT_CN('" + ddlServiceCode.SelectedValue + "','" + dtpFDate.DateString + "','" + dtpTDate.DateString + "')";
            //strMsg = objServiceHandler.ExecuteProcedure(strProcedure);

            //if (strMsg != "")
            //{

            //------------------ main query start here ------------------------
            strDateRange = " AND TO_CHAR(TO_DATE(TRANSACTION_DATE,'DD/MM/YYYY')) BETWEEN  TO_DATE('" + dtpFDate.DateString + "') AND TO_DATE('" + dtpTDate.DateString + "') ";

            try
            {

                //strQuery = " SELECT DISTINCT TO_CHAR(CAT.CAS_TRAN_DATE, 'DD/MON/YYYY MI:HH24:SS') TRAN_DATE,SR.REQUEST_ID, AL.ACCNT_NO AGENT_ACCOUNT,SUBSTR (SR.REQUEST_TEXT, LENGTH (SR.REQUEST_TEXT) - 12, 11)CUS_ACCOUNT, "
                //     + " SR.REQUEST_TEXT, PKG_MIS_REPORTS.FUNC_TRAN_AMOUNT (SR.REQUEST_ID,'CN','" + dtpFDate.DateString + "','" + dtpTDate.DateString + "','D')CN_TRAN_AMNT, "
                //     + " PKG_MIS_REPORTS.FUNC_TRAN_SRV_FEE(SR.REQUEST_ID)CN_SRV_FEE FROM ACCOUNT_LIST AL,BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,"
                //     + " BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, SERVICE_REQUEST SR WHERE AL.ACCNT_NO=CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID=CAT.CAS_ACC_ID "
                //     + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND AL.ACCNT_RANK_ID='120519000000000005' AND  UPPER(SUBSTR (SR.REQUEST_TEXT, 2, INSTR (SR.REQUEST_TEXT, '*', 2) - 2))='CN' "
                //     + " AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "'";

                //  strQuery = " SELECT DISTINCT TRUNC(CAT.CAS_TRAN_DATE) TRAN_DATE,SR.REQUEST_ID, AL.ACCNT_NO AGENT_ACCOUNT,SUBSTR (SR.REQUEST_TEXT, LENGTH (SR.REQUEST_TEXT) - 12, 11)CUS_ACCOUNT, "

                // PACKAGE APSNG101.PKG_MIS_REPORTS 
                // PROCEDURE PRO_MIS_REPORT_FINAL;
                // --EXEC PKG_MIS_REPORTS.PRO_MIS_REPORT_FINAL;

                strQuery = " SELECT TO_CHAR(TRANSACTION_DATE, 'DD/MON/YYYY')TRANSACTION_DATE, REQUEST_ID,REQUEST_PARTY,SUBSTR(RECEPENT_PARTY,1,11)RECEPENT_PARTY," +
                           " TRANSACTION_AMOUNT,SERVICE_FEE FROM TEMP_MIS_TRANSACTIONS_REPORT " +
                           " WHERE SERVICE_CODE='CN' AND TRUNC(TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "'";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Cust_CashIN_Rpt_At_Agent_Point";
                //------------------------------------------Report Query -------------------------------------

                SaveAuditInfo("View", "MIS Daily Customer Cash In Report");
                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"70%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h2 align=center> Mercantile Bank Limited </h2></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"0\" width=\"70%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h2 align=center> MYCash </h3></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"0\" width=\"70%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h3 align=center> Daily Cash-In (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ") </h3></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h3 align=center> Customer Cash-In at Agent point </h3></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >Agent MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Customer MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Txn Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Fee</td>";

                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_FEE"].ToString() + "</td>";

                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                        transactionFee = transactionFee + Convert.ToDouble(prow["SERVICE_FEE"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + transactionFee.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";

            }

            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
            // }

        }
        #endregion

        #region daily customer cashout

        // Daily Customer Cash Out Report At Agent Point  

        else if (ddlServiceCode.SelectedValue == "CCT" && ddlSourceRank.SelectedItem.Text == "MBL Customer" && ddlDestinationRank.SelectedItem.Text == "MBL Agent")
        {
            //strQuery = " SELECT DISTINCT TO_CHAR(CAT.CAS_TRAN_DATE, 'DD/MON/YYYY MI:HH24:SS')TRAN_DATE,SR.REQUEST_ID,SUBSTR (SR.REQUEST_TEXT, LENGTH (SR.REQUEST_TEXT) - 12, 11)AGENT_ACC, "
            //         + " SUBSTR(SR.REQUEST_PARTY,4,11)CUST_ACC, SR.REQUEST_TEXT, PKG_MIS_REPORTS.FUNC_TRAN_AMOUNT (SR.REQUEST_ID,'CCT','"+dtpFDate.DateString+"','"+dtpTDate.DateString+"','D')CCT_TRAN_AMNT, "
            //         + " PKG_MIS_REPORTS.FUNC_TRAN_SRV_FEE(CAT.REQUEST_ID)CCT_SRV_FEE FROM SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT,ACCOUNT_LIST AL "
            //         + " WHERE UPPER(SUBSTR (SR.REQUEST_TEXT, 2, INSTR (SR.REQUEST_TEXT, '*', 2) - 2))='CCT' AND SR.REQUEST_ID=CAT.REQUEST_ID "
            //         + " AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '"+dtpFDate.DateString+"' AND '"+dtpTDate.DateString+"' AND SUBSTR (SR.REQUEST_TEXT, LENGTH (SR.REQUEST_TEXT) - 12, 12)=AL.ACCNT_NO "
            //         + " AND AL.ACCNT_RANK_ID='120519000000000005' ";

            strQuery = " SELECT TO_CHAR(TRANSACTION_DATE, 'DD/MON/YYYY')TRANSACTION_DATE,REQUEST_ID,REQUEST_PARTY,SUBSTR(RECEPENT_PARTY,1,11)RECEPENT_PARTY,TRANSACTION_AMOUNT,SERVICE_FEE "
                      + " FROM TEMP_MIS_TRANSACTIONS_REPORT WHERE SERVICE_CODE='CCT' AND TRUNC(TRANSACTION_DATE) "
                      + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "'";
            try
            {

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccntBalance = new DataSet();
                SaveAuditInfo("View", "MIS Daily Customer Cash Out Report At Agent Point");
                fileName = "Daily_Cust_CashOUT_Rpt_At_Agent_Point";
                //------------------------------------------Report Query -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"70%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h2 align=center> Mercantile Bank Limited </h2></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"0\" width=\"70%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h2 align=center> MYCash </h3></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"0\" width=\"70%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h3 align=center> Daily Cash-Out (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ") </h3></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h3 align=center> Customer Cash-Out at Agent point </h3></td></tr>";
                strHTML = strHTML + "</table>";

                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >SerialNo</td>";
                strHTML = strHTML + "<td valign='middle' > Transaction Date </td>";
                strHTML = strHTML + "<td valign='middle' >Transaction ID</td>";
                strHTML = strHTML + "<td valign='middle' >Agent MyCash Account No. </td>";
                strHTML = strHTML + "<td valign='middle' >Customer MyCash Account No</td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Fee</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td valign='right' > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td valign='right' > '" + prow["SERVICE_FEE"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                        transactionFee = transactionFee + Convert.ToDouble(prow["SERVICE_FEE"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + transactionFee.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
        #endregion

        #region customer to customer fund transfer

        //Daily Customer to Customer Fund Transfer Report 

        else if (ddlServiceCode.SelectedValue == "FT" && ddlSourceRank.SelectedItem.Text == "MBL Customer" && ddlDestinationRank.SelectedItem.Text == "MBL Customer")
        {
            try
            {

                //strProcedure = " PKG_MIS_REPORTS.MIS_TRANSACTIONS_REPORT_FT('" + ddlServiceCode.SelectedValue + "','" + dtpFDate.DateString + "','" + dtpTDate.DateString + "')";
                //strMsg = objServiceHandler.ExecuteProcedure(strProcedure);
                //string strServiceName = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("SERVICE_LIST", "SERVICE_TITLE", "SERVICE_ACCESS_CODE", ddlServiceCode.SelectedValue.ToString());

                //strQuery = " SELECT DISTINCT TM.*,'" + strServiceName + "' REPORT_NAME,'" + dtpFDate.DateString
                //        + "' F_DATE,'" + dtpTDate.DateString + "' T_DATE FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL, ACCOUNT_RANK AR, ACCOUNT_LIST ALD, ACCOUNT_RANK ARD"
                //        + " WHERE TM.RECEPENT_PARTY = ALD.ACCNT_NO AND ALD.ACCNT_RANK_ID = ARD.ACCNT_RANK_ID AND ARD.ACCNT_RANK_ID = '120519000000000006' AND "
                //        + " TM.REQUEST_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AR.ACCNT_RANK_ID = '120519000000000006'"
                //        + " AND SERVICE_CODE='" + ddlServiceCode.SelectedValue.ToString() + "'  AND TO_CHAR(TO_DATE(TM.TRANSACTION_DATE,'DD/MM/YYYY')) "
                //        + " BETWEEN  TO_DATE('" + dtpFDate.DateString + "') AND TO_DATE('" + dtpTDate.DateString + "')";

                strQuery = " SELECT TO_CHAR(TRANSACTION_DATE, 'DD/MON/YYYY')TRANSACTION_DATE,REQUEST_ID,REQUEST_PARTY,SUBSTR(RECEPENT_PARTY,1,11)RECEPENT_PARTY,"
                          + " TRANSACTION_AMOUNT,SERVICE_FEE FROM TEMP_MIS_TRANSACTIONS_REPORT "
                          + " WHERE SERVICE_CODE='FT' AND TRUNC(TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "'";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("View", "Daily Customer to Customer Fund Transfer Report");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Customer_To_Customer_Trx_Report";
                //------------------------------------------Report Query -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h3 align=center>Daily Send Money (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none; font-size:18px;font-weight:bold;'><h2 align=center> Customer Send Money </h2></td></tr>";
                strHTML = strHTML + "</table>";
                //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                //strHTML = strHTML + "<tr>";
                //strHTML = strHTML + " <td COLSPAN=6 align=center style='border-right:none'><h5 align=left> From Date: " + dtpFDate.DateString + " To Date: " + dtpTDate.DateString + " </h5></td>";
                //strHTML = strHTML + " <td COLSPAN=5 align=center style='border-right:none'><h5 align=left> Print Date " + DateTime.Now.ToShortDateString() + " </h5></td>";
                //strHTML = strHTML + " </tr>";
                //strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >Sender MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Receiver MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Txn Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Fee</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_FEE"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                        totalFee = totalFee + Convert.ToDouble(prow["SERVICE_FEE"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + totalFee.ToString() + " </td>";

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }
        #endregion


        #region micro dse to micro agent gemini Fund Transfer
        // Daily fund transfer from Branch to Distributor

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "Micro Super Agent/DSE" && ddlDestinationRank.SelectedItem.Text == "Micro POS Agent/Gemini")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,DIS_ACCNT_NO BR_REQUEST_PARTY,CL.CLINT_NAME BR_NAME, "
                //         + " DEL_ACCNT_NO RECEPENT_PARTY_DIS, CL1.CLINT_NAME DIS_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE FROM TEMP_HIERARCHY_LIST_ALL THA," 
                //         + " TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, ACCOUNT_LIST AL1, CLIENT_LIST CL1 "
                //         + " WHERE THA.DIS_ACCNT_NO=MIS.REQUEST_PARTY||1 AND THA.DIS_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID "
                //         + " AND THA.DEL_ACCNT_NO=MIS.RECEPENT_PARTY AND THA.DEL_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //         + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) " 
                //         + " BETWEEN '"+dtpFDate.DateString+"' AND '"+dtpTDate.DateString+"' ORDER BY TRANSACTION_DATE DESC ";

                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO BR_REQUEST_PARTY,"
                         + " CLA.CLINT_NAME BR_NAME, AL1.ACCNT_NO RECEPENT_PARTY_DIS,CLB.CLINT_NAME DIS_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                         + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                         + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                         + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Fund Transfer from Micro DSE to Micro Agent gemini");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Branch_To_DIS_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Micro Dse's Fund Transfer to Micro Agent Gemini  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Micro DSE's Fund Transfer to Micro Agent Gemini</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_DIS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }

        #endregion      
            
        #region micro agent gemini to micro dse Fund Transfer
        // Daily fund transfer from Branch to Distributor

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "Micro POS Agent/Gemini" && ddlDestinationRank.SelectedItem.Text == "Micro Super Agent/DSE")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,DIS_ACCNT_NO BR_REQUEST_PARTY,CL.CLINT_NAME BR_NAME, "
                //         + " DEL_ACCNT_NO RECEPENT_PARTY_DIS, CL1.CLINT_NAME DIS_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE FROM TEMP_HIERARCHY_LIST_ALL THA," 
                //         + " TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, ACCOUNT_LIST AL1, CLIENT_LIST CL1 "
                //         + " WHERE THA.DIS_ACCNT_NO=MIS.REQUEST_PARTY||1 AND THA.DIS_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID "
                //         + " AND THA.DEL_ACCNT_NO=MIS.RECEPENT_PARTY AND THA.DEL_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //         + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) " 
                //         + " BETWEEN '"+dtpFDate.DateString+"' AND '"+dtpTDate.DateString+"' ORDER BY TRANSACTION_DATE DESC ";

                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO BR_REQUEST_PARTY,"
                         + " CLA.CLINT_NAME BR_NAME, AL1.ACCNT_NO RECEPENT_PARTY_DIS,CLB.CLINT_NAME DIS_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                         + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                         + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                         + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Fund Transfer from Micro DSE to Micro Agent gemini");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Branch_To_DIS_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Micro Agent Gemini's Fund Transfer to Micro DSE  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Micro Agent Gemini's Fund Transfer to Micro DSE</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_DIS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }

        #endregion          
            
            
            
            
            
        #region Branch to Distributor Fund Transfer
        // Daily fund transfer from Branch to Distributor

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Branch" && ddlDestinationRank.SelectedItem.Text == "MBL Distributor")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,DIS_ACCNT_NO BR_REQUEST_PARTY,CL.CLINT_NAME BR_NAME, "
                //         + " DEL_ACCNT_NO RECEPENT_PARTY_DIS, CL1.CLINT_NAME DIS_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE FROM TEMP_HIERARCHY_LIST_ALL THA," 
                //         + " TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, ACCOUNT_LIST AL1, CLIENT_LIST CL1 "
                //         + " WHERE THA.DIS_ACCNT_NO=MIS.REQUEST_PARTY||1 AND THA.DIS_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID "
                //         + " AND THA.DEL_ACCNT_NO=MIS.RECEPENT_PARTY AND THA.DEL_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //         + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) " 
                //         + " BETWEEN '"+dtpFDate.DateString+"' AND '"+dtpTDate.DateString+"' ORDER BY TRANSACTION_DATE DESC ";

                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO BR_REQUEST_PARTY,"
                         + " CLA.CLINT_NAME BR_NAME, AL1.ACCNT_NO RECEPENT_PARTY_DIS,CLB.CLINT_NAME DIS_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                         + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                         + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                         + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";
                
                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Fund Transfer from Branch to Distributor");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Branch_To_DIS_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Branch's Fund Transfer to Distributor  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Branch's Fund Transfer to Distributor</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_DIS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }

        #endregion  

        #region Fund Transfer from Branch to DSE
        // Daily Fund manegement from Branch to DSE

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Branch" && ddlDestinationRank.SelectedItem.Text == "MBL DSE")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,DIS_ACCNT_NO BR_REQUEST_PARTY,CL.CLINT_NAME BRANCH_NAME, "
                //     + " SA_ACCNT_NO RECEPENT_PARTY_DSE, CL1.CLINT_NAME DSE_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE  FROM TEMP_HIERARCHY_LIST_ALL THA,"
                //     + " TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, ACCOUNT_LIST AL1, CLIENT_LIST CL1 "
                //     + " WHERE THA.DIS_ACCNT_NO=MIS.REQUEST_PARTY||1 AND THA.DIS_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID "
                //     + " AND THA.SA_ACCNT_NO=MIS.RECEPENT_PARTY AND THA.SA_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //     + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //     + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";
                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO BR_REQUEST_PARTY"
                        + " ,CLA.CLINT_NAME BRANCH_NAME,AL1.ACCNT_NO RECEPENT_PARTY_DSE,CLB.CLINT_NAME DSE_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                        + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                        + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                        + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                        + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Fund Transfer from Branch to DSE");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Branch_To_DSE_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Branch's Fund Transfer to DSE  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Branch's Fund Transfer to DSE</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BRANCH_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_DSE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }
        # endregion 

        #region fund transfer from Branch to Agent
        // Daily Fund Management from Branch to Agent

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Branch" && ddlDestinationRank.SelectedItem.Text == "MBL Agent")
        {
            try
            {

                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,DIS_ACCNT_NO BR_REQUEST_PARTY,CL.CLINT_NAME BRANCH_NAME, "
                //         + " A_ACCNT_NO RECEPENT_PARTY_AGENT, CL1.CLINT_NAME AGENT_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE FROM TEMP_HIERARCHY_LIST_ALL THA," 
                //         + " TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, ACCOUNT_LIST AL1, CLIENT_LIST CL1" 
                //         + " WHERE THA.DIS_ACCNT_NO=MIS.REQUEST_PARTY||1 AND THA.DIS_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID " 
                //         + " AND THA.A_ACCNT_NO=MIS.RECEPENT_PARTY AND THA.A_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //         + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //         + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";

                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO BR_REQUEST_PARTY"
                         + " ,CLA.CLINT_NAME BRANCH_NAME,AL1.ACCNT_NO RECEPENT_PARTY_AGENT,CLB.CLINT_NAME AGENT_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                         + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                         + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                         + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Fund Transfer from Branch to Agent");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Branch_To_Agent_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Branch's Fund Transfer to Agent  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Branch's Fund Transfer to Agent</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BRANCH_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_AGENT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }
        #endregion 

        #region Daily distributor to branch fund transfer
        // Daily distributor fund transfer to branch 

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Distributor" && ddlDestinationRank.SelectedItem.Text == "MBL Branch")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,DEL_ACCNT_NO DIS_REQUEST_PARTY,CL.CLINT_NAME DIS_NAME, "
                //         + " DIS_ACCNT_NO RECEPENT_PARTY_BR, CL1.CLINT_NAME BR_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE FROM TEMP_HIERARCHY_LIST_ALL THA," 
                //         + " TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, ACCOUNT_LIST AL1, CLIENT_LIST CL1 " 
                //         + " WHERE THA.DEL_ACCNT_NO=MIS.REQUEST_PARTY||1 AND THA.DEL_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID " 
                //         + " AND THA.DIS_ACCNT_NO=MIS.RECEPENT_PARTY AND THA.DIS_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //         + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //         + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";
               
                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO DIS_REQUEST_PARTY "
                      + ",CLA.CLINT_NAME DIS_NAME,AL1.ACCNT_NO RECEPENT_PARTY_BR,CLB.CLINT_NAME BR_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                      + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB "
                      + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID "
                      + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                      + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Fund Transfer from Distributor to Branch");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Dsitributor_To_Branch_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Distributor's Fund Transfer to Branch  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Distributor's Fund Transfer to Branch</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_BR"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }
        #endregion 

        #region daily distributor to dse fund transfer
        //Daily Distributor to DSE Fund Management Report

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Distributor" && ddlDestinationRank.SelectedItem.Text == "MBL DSE")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,DEL_ACCNT_NO REQUEST_PARTY,CL.CLINT_NAME DIS_NAME, "
                //           + " SA_ACCNT_NO RECEPENT_PARTY_DSE, CL1.CLINT_NAME DSE_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE "
                //           + " FROM TEMP_HIERARCHY_LIST_ALL THA,TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, "
                //           + " ACCOUNT_LIST AL1, CLIENT_LIST CL1 WHERE THA.DEL_ACCNT_NO=MIS.REQUEST_PARTY||1 AND "
                //           + " THA.DEL_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND THA.SA_ACCNT_NO=MIS.RECEPENT_PARTY "
                //           + " AND THA.SA_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //           + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //           + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' "
                //           + " ORDER BY TRANSACTION_DATE DESC";

                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO REQUEST_PARTY "
                       + ",CLA.CLINT_NAME DIS_NAME,AL1.ACCNT_NO RECEPENT_PARTY_DSE,CLB.CLINT_NAME DSE_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                       + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB "
                       + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID "
                       + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                       + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";


                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Distributor to DSE Fund Transfer");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Distributor_To_DSE_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Distributor's Fund Transfer to DSE  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Distributor's Fund Transfer to DSE</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor's Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE's Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_DSE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        #endregion

        #region Daily Distributor to Agent Fund transfer
        // Daily Distributor to Agent Fund Transfer

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Distributor" && ddlDestinationRank.SelectedItem.Text == "MBL Agent")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,DEL_ACCNT_NO DIS_REQUEST_PARTY,CL.CLINT_NAME DIS_NAME, "
                //     + " A_ACCNT_NO RECEPENT_PARTY_AGENT, CL1.CLINT_NAME AGENT_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE "
                //     + " FROM TEMP_HIERARCHY_LIST_ALL THA,TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, "
                //     + " ACCOUNT_LIST AL1, CLIENT_LIST CL1 WHERE THA.DEL_ACCNT_NO=MIS.REQUEST_PARTY||1 AND "
                //     + " THA.DEL_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND THA.A_ACCNT_NO=MIS.RECEPENT_PARTY "
                //     + " AND THA.A_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //     + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //     + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";


                strQuery =" SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO DIS_REQUEST_PARTY"
                        + ",CLA.CLINT_NAME DIS_NAME,AL1.ACCNT_NO RECEPENT_PARTY_AGENT,CLB.CLINT_NAME AGENT_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                        + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                        + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                        + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                        + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Distributor to Agent Fund Transfer");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Distributor_To_Agent_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Distributor's Fund Transfer to Agent  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Distributor's Fund Transfer to Agent</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_AGENT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }            
        }
        #endregion 

        #region daily DSE to Branch Fund Transfer
        // Daily DSE to Branch Fund Transfer

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL DSE" && ddlDestinationRank.SelectedItem.Text == "MBL Branch")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID, SA_ACCNT_NO DSE_REQUEST_PARTY,CL.CLINT_NAME DSE_NAME, "
                //         + " DIS_ACCNT_NO RECEPENT_PARTY_BR, CL1.CLINT_NAME BR_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE "
                //         + " FROM TEMP_HIERARCHY_LIST_ALL THA,TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, "
                //         + " ACCOUNT_LIST AL1, CLIENT_LIST CL1 WHERE THA.SA_ACCNT_NO=MIS.REQUEST_PARTY||1 AND "
                //         + " THA.SA_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND THA.DIS_ACCNT_NO=MIS.RECEPENT_PARTY "
                //         + " AND THA.DIS_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //         + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //         + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";

                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO DSE_REQUEST_PARTY"
                         + " ,CLA.CLINT_NAME DSE_NAME,AL1.ACCNT_NO RECEPENT_PARTY_BR,CLB.CLINT_NAME BR_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                         + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                         + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                         + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";


                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily DSE to Branch Fund Transfer");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_DSE_To_Branch_Trx_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily DSE Fund Transfer to Branch  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>DSE Fund Transfer to Branch</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_BR"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BR_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");


            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }        
        #endregion 

        # region daily dse to distributor fund transfer

        //Daily DSE to Distributor Fund Management Report
        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL DSE" && ddlDestinationRank.SelectedItem.Text == "MBL Distributor")
        {
            try
            {              
               
                //strQuery = " SELECT   DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID, SA_ACCNT_NO REQUEST_PARTY,CL.CLINT_NAME DSE_NAME, "
                //         + " DEL_ACCNT_NO RECEPENT_PARTY_DIS, CL1.CLINT_NAME DIS_NAME, TRANSACTION_AMOUNT, SERVICE_CODE FROM   TEMP_HIERARCHY_LIST_ALL THA, "
                //         + " TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL, CLIENT_LIST CL, ACCOUNT_LIST AL1, CLIENT_LIST CL1 WHERE   THA.SA_ACCNT_NO = MIS.REQUEST_PARTY ||1 "
                //         + " AND THA.SA_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND THA.DEL_ACCNT_NO = MIS.RECEPENT_PARTY "
                //         + " AND THA.DEL_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID AND SERVICE_CODE = 'FM' "
                //         + " AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' "
                //         + " ORDER BY   TRANSACTION_DATE DESC";

                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO REQUEST_PARTY"
                       + " ,CLA.CLINT_NAME DSE_NAME,AL1.ACCNT_NO RECEPENT_PARTY_DIS,CLB.CLINT_NAME DIS_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                       + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                       + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                       + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                       + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("View", "Daily DSE to Distributor Fund Transfer Report");

                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_DSE_To_Distributor_Trx_Report";
                //------------------------------------------Report Query -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily DSE's Fund Transfer to Distributor (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>DSE's Fund Transfer to Distributor</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date</td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE's Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor's Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_DIS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }
        #endregion

        #region Daily DSE to Agent Fund Transfer
        // Daily Dse to Agent Fund transfer

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL DSE" && ddlDestinationRank.SelectedItem.Text == "MBL Agent")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,SA_ACCNT_NO DSE_REQUEST_PARTY,CL.CLINT_NAME DSE_NAME,  "
                //         + " A_ACCNT_NO RECEPENT_PARTY_AGENT, CL1.CLINT_NAME AGENT_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE "
                //         + " FROM TEMP_HIERARCHY_LIST_ALL THA,TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, "
                //         + " ACCOUNT_LIST AL1, CLIENT_LIST CL1 WHERE THA.SA_ACCNT_NO=MIS.REQUEST_PARTY||1 AND "
                //         + " THA.SA_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND THA.A_ACCNT_NO=MIS.RECEPENT_PARTY "
                //         + " AND THA.A_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //         + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //         + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";

				//strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO DSE_REQUEST_PARTY"
                //         + " ,CLA.CLINT_NAME DSE_NAME,AL1.ACCNT_NO RECEPENT_PARTY_AGENT,CLB.CLINT_NAME AGENT_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                //         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                //         + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                //         + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                //         + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                //strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID, CLTM.CLINT_NAME TM_NAME,"
                //        + " CLT.CLINT_NAME TO_NAME, CLD1.CLINT_NAME DIS_NAME, AL.ACCNT_NO DSE_REQUEST_PARTY, CLA.CLINT_NAME"
                //        + " DSE_NAME,AL1.ACCNT_NO RECEPENT_PARTY_AGENT,CLB.CLINT_NAME AGENT_NAME, MDA.DISTRICT_NAME,"
                //        + " TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE FROM TEMP_MIS_TRANSACTIONS_REPORT TMR, ACCOUNT_LIST AL,"
                //        + " ACCOUNT_LIST AL1, CLIENT_LIST CLA, CLIENT_LIST CLB, TEMP_HIERARCHY_LIST_ALL DEL1,"
                //        + " MANAGE_TERRITORY_HIERARCHY TO1, ACCOUNT_LIST ALT, CLIENT_LIST CLT, ACCOUNT_LIST ALD1, CLIENT_LIST CLD1,"
                //        + " MANAGE_TERRITORY_HIERARCHY TM1, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_THANA MTA,"
                //        + " MANAGE_DISTRICT MDA WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+)"
                //        + " AND CLA.CLINT_ID(+)=AL.CLINT_ID AND DEL1.SA_ACCNT_NO(+) = AL.ACCNT_NO AND DEL1.DEL_ACCNT_ID = TO1.ACCNT_ID(+)"
                //        + " AND TO1.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+)"
                //        + " AND DEL1.DEL_ACCNT_ID = ALD1.ACCNT_ID(+) AND ALD1.CLINT_ID =  CLD1.CLINT_ID(+)"
                //        + " AND TO1.HIERARCHY_ACCNT_ID = TM1.ACCNT_ID(+) AND TM1.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+)"
                //        + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND CLB.THANA_ID = MTA.THANA_ID AND MTA.DISTRICT_ID = MDA.DISTRICT_ID"
                //        + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "')"
                //        + " AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+)"
                //        + " AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                //        + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "'"
                //        + " AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";
				
				strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID, CLTM.CLINT_NAME TM_NAME,"
                        + " CLT.CLINT_NAME TO_NAME, CLD1.CLINT_NAME DIS_NAME, ALD1.ACCNT_NO DIS_WALLET, AL.ACCNT_NO DSE_REQUEST_PARTY, CLA.CLINT_NAME DSE_NAME,"
                        + " AL1.ACCNT_NO RECEPENT_PARTY_AGENT,CLB.CLINT_NAME AGENT_NAME, MA.AREA_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE"
                        + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR, ACCOUNT_LIST AL, ACCOUNT_LIST AL1, CLIENT_LIST CLA, CLIENT_LIST CLB,"
                        + " TEMP_HIERARCHY_LIST_ALL DEL1, MANAGE_TERRITORY_HIERARCHY TO1, ACCOUNT_LIST ALT, CLIENT_LIST CLT, ACCOUNT_LIST ALD1,"
                        + " CLIENT_LIST CLD1, MANAGE_TERRITORY_HIERARCHY TM1, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA,"
                        + " MANAGE_AREA MA WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                        + " AND DEL1.SA_ACCNT_NO(+) = AL.ACCNT_NO AND DEL1.DEL_ACCNT_ID = TO1.ACCNT_ID(+) AND TO1.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+)"
                        + " AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND DEL1.DEL_ACCNT_ID = ALD1.ACCNT_ID(+) AND ALD1.CLINT_ID =  CLD1.CLINT_ID(+)"
                        + " AND TO1.HIERARCHY_ACCNT_ID = TM1.ACCNT_ID(+) AND TM1.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+)"
                        + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND TM1.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+)"
                        + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+)"
                        + " AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "') AND CLB.CLINT_ID(+)=AL1.CLINT_ID"
                        + " AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "'"
                        + " ORDER BY TRANSACTION_DATE DESC ";


                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("View", "Daily DSE to Agent Fund Transfer Report");

                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_DSE_To_Agent_Trx_Report";
                //------------------------------------------Report Query -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily DSE's Fund Transfer to Agent (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>DSE's Fund Transfer to Agent</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date</td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >TM Name</td>";
                strHTML = strHTML + "<td valign='middle' >TO Name</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
				strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent Name</td>";
                strHTML = strHTML + "<td valign='middle' >Area</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
						strHTML = strHTML + " <td > '" + prow["DIS_WALLET"].ToString() + " </td>";
						strHTML = strHTML + " <td > '" + prow["DSE_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_AGENT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
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
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }   
        }
        #endregion

        #region Daily Agent to Branch Fund Transfer
        // Daily Agent to Branch Fund Transfer

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Agent" && ddlDestinationRank.SelectedItem.Text == "MBL Branch")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,A_ACCNT_NO AGENT_REQUEST_PARTY,CL.CLINT_NAME AGENT_NAME, "
                //           + " DIS_ACCNT_NO RECEPENT_PARTY_BR, CL1.CLINT_NAME BRANCH_NAME, TRANSACTION_AMOUNT,SERVICE_CODE "
                //           + " FROM TEMP_HIERARCHY_LIST_ALL THA,TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, "
                //           + " ACCOUNT_LIST AL1, CLIENT_LIST CL1 WHERE THA.A_ACCNT_NO=MIS.REQUEST_PARTY||1 AND "
                //           + " THA.A_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND THA.DIS_ACCNT_NO=MIS.RECEPENT_PARTY "
                //           + " AND THA.DIS_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //           + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //           + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";
                
               strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO AGENT_REQUEST_PARTY"
                        + " ,CLA.CLINT_NAME AGENT_NAME,AL1.ACCNT_NO RECEPENT_PARTY_BR,CLB.CLINT_NAME BRANCH_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                        + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                        + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') "
                        + " AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "') AND CLB.CLINT_ID(+)=AL1.CLINT_ID"
                        + " AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("View", "Daily Agent to Branch Fund Transfer Report");

                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Agent_To_Branch_Trx_Report";
                //------------------------------------------Report Query -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Agent Fund Transfer to Branch (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Agent Fund Transfer to Branch </h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date</td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Branch Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_BR"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["BRANCH_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }        
        #endregion 

        #region daily Agent Fund Transfer to Distributor
        // Daily Agent to Distributor Fund Transfer

        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Agent" && ddlDestinationRank.SelectedItem.Text == "MBL Distributor")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,A_ACCNT_NO AGENT_REQUEST_PARTY,CL.CLINT_NAME AGENT_NAME, "
                //      + " DEL_ACCNT_NO RECEPENT_PARTY_DIS, CL1.CLINT_NAME DIS_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE  "
                //      + " FROM TEMP_HIERARCHY_LIST_ALL THA,TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, "
                //      + " ACCOUNT_LIST AL1, CLIENT_LIST CL1 WHERE THA.A_ACCNT_NO=MIS.REQUEST_PARTY||1 AND "
                //      + " THA.A_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND THA.DEL_ACCNT_NO=MIS.RECEPENT_PARTY "
                //      + " AND THA.DEL_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //      + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //      + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";

               strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO AGENT_REQUEST_PARTY"
                        + " ,CLA.CLINT_NAME AGENT_NAME,AL1.ACCNT_NO RECEPENT_PARTY_DIS,CLB.CLINT_NAME DIS_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                        + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                        + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                        + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                        + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("View", "Daily Agent to Distributor Fund Transfer Report");

                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Agent_To_Distributor_Trx_Report";
                //------------------------------------------Report Query -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Agent Fund Transfer to Distributor (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Agent Fund Transfer to Distrtibutor </h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date</td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_DIS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }            
        }        
        #endregion 

        #region daily agent fund transfer to DSE
        // Daily agent fund transfer to DSE
        else if (ddlServiceCode.SelectedValue == "FM" && ddlSourceRank.SelectedItem.Text == "MBL Agent" && ddlDestinationRank.SelectedItem.Text == "MBL DSE")
        {
            try
            {
                //strQuery = " SELECT DISTINCT TRUNC (TRANSACTION_DATE) TRANSACTION_DATE, REQUEST_ID ,A_ACCNT_NO AGENT_REQUEST_PARTY,CL.CLINT_NAME AGENT_NAME, "
                //         + " SA_ACCNT_NO RECEPENT_PARTY_DSE, CL1.CLINT_NAME DSE_NAME,  TRANSACTION_AMOUNT,SERVICE_CODE "
                //         + " FROM TEMP_HIERARCHY_LIST_ALL THA,TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL,  CLIENT_LIST CL, "
                //         + " ACCOUNT_LIST AL1, CLIENT_LIST CL1 WHERE THA.A_ACCNT_NO=MIS.REQUEST_PARTY||1 AND "
                //         + " THA.A_ACCNT_NO = AL.ACCNT_NO AND AL.CLINT_ID = CL.CLINT_ID AND THA.SA_ACCNT_NO=MIS.RECEPENT_PARTY "
                //         + " AND THA.SA_ACCNT_NO = AL1.ACCNT_NO AND AL1.CLINT_ID = CL1.CLINT_ID "
                //         + " AND SERVICE_CODE='FM' AND TRUNC (TRANSACTION_DATE) "
                //         + " BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC ";

                strQuery = " SELECT DISTINCT TRUNC (TMR.TRANSACTION_DATE) TRANSACTION_DATE, TMR.REQUEST_ID ,AL.ACCNT_NO AGENT_REQUEST_PARTY"
                       + " ,CLA.CLINT_NAME AGENT_NAME,AL1.ACCNT_NO RECEPENT_PARTY_DSE,CLB.CLINT_NAME DSE_NAME, TMR.TRANSACTION_AMOUNT,TMR.SERVICE_CODE "
                       + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMR ,ACCOUNT_LIST AL,ACCOUNT_LIST AL1,CLIENT_LIST CLA,CLIENT_LIST CLB"
                       + " WHERE SERVICE_CODE='FM' AND REQUEST_PARTY||'1'=AL.ACCNT_NO(+) AND CLA.CLINT_ID(+)=AL.CLINT_ID"
                       + " AND AL.ACCNT_RANK_ID IN('" + ddlSourceRank.SelectedValue.ToString() + "') AND TMR.RECEPENT_PARTY=AL1.ACCNT_NO(+) AND AL1.ACCNT_RANK_ID IN('" + ddlDestinationRank.SelectedValue.ToString() + "')"
                       + " AND CLB.CLINT_ID(+)=AL1.CLINT_ID AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' ORDER BY TRANSACTION_DATE DESC";
                                
                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("View", "Daily Agent to DSE Fund Transfer Report");

                DataSet dtsAccntBalance = new DataSet();
                fileName = "Daily_Agent_To_DSE_Trx_Report";
                //------------------------------------------Report Query -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Agent Fund Transfer to DSE (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Agent Fund Transfer to DSE</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date</td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash Agent Name</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >MYCash DSE Name</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["RECEPENT_PARTY_DSE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }
        #endregion 

        #region Daily Bank Deposit Report new

        else if (checkBoxRank.Checked == true && ddlServiceCode.SelectedValue == "BD")
        {
            string strSQL = "";
            
            try
            {
                // 02.06.2014

                //strSQL = " SELECT DISTINCT TRUNC(CAT.CAS_TRAN_DATE) TRAN_DATE,CAT.REQUEST_ID,SUBSTR(AL.ACCNT_NO,1,11)SENDER_ACC , "
                //         + " SUBSTR (SR.REQUEST_TEXT, LENGTH (SR.REQUEST_TEXT) - 29, 15)MSS_AC_NO,"
                //         + " SUBSTR (SR.REQUEST_TEXT, LENGTH (SR.REQUEST_TEXT) - 13, 11) DEPOSITOR_MOB_NO,"
                //         + " PKG_MIS_REPORTS.FUNC_TRAN_AMOUNT (CAT.REQUEST_ID,'BD','" + dtpBankDFDate.DateString + "','" + dtpBankDTDate.DateString + "','D')BD_TRAN_AMNT, "
                //         + " PKG_MIS_REPORTS.FUNC_TRAN_SRV_FEE(CAT.REQUEST_ID)CN_SRV_FEE "
                //         + " FROM ACCOUNT_LIST AL,BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, SERVICE_REQUEST SR "
                //         + " WHERE AL.ACCNT_NO=CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID=CAT.CAS_ACC_ID "
                //         + " AND CAT.REQUEST_ID=SR.REQUEST_ID AND AL.ACCNT_MSISDN=SR.REQUEST_PARTY "
                //         + " AND  UPPER(SUBSTR (SR.REQUEST_TEXT, 2, INSTR (SR.REQUEST_TEXT, '*', 2) - 2))='BD' "
                //         + "AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '"+dtpBankDFDate.DateString+"' AND '"+dtpBankDTDate.DateString+"' ";

                // dtpFDate = 25/may/2014  or 01/feb/2014

                strSQL = " SELECT DISTINCT TRUNC(TRANSACTION_DATE) TRANSACTION_DATE,MIS.REQUEST_PARTY,MIS.REQUEST_ID,TRANSACTION_AMOUNT,DPS_REF_CODE MSS_ACC_NO,SERVICE_FEE, "
                       + " PKG_MIS_REPORTS.FUNC_GET_DEPOSITORS_MOB(REQUEST_TEXT) DEPO_MOB_NO, REQUEST_TEXT, "
                       + " DECODE (CAS_ISO_REQ_STATUS,'O', 'Offline','S', 'OnLine','F', 'Failed','E', 'Expired',CAS_ISO_REQ_STATUS) TRAN_STATUS "
                       + " FROM TEMP_MIS_TRANSACTIONS_REPORT MIS,BDMIT_ERP_101.CAS_DPS_TRANSACTION DPS,SERVICE_REQUEST SR "
                       + " WHERE SERVICE_CODE='BD' AND MIS.REQUEST_ID=DPS.REQUEST_ID AND MIS.REQUEST_ID=SR.REQUEST_ID "
                       + " AND DPS_OWNER='MBL' AND CAS_DPS_RESUBMIT='N' AND TRUNC(TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "'"
                       + " ORDER BY TRAN_STATUS DESC";


                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Daily Bank Deposit Report");
                DataSet dtsAccount = new DataSet();
                fileName = "Daily_Bank_Deposit_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSQL);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MyCash </h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Bank Deposit(" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                //strHTML = strHTML + "<tr>";
                //strHTML = strHTML + " <td COLSPAN=6 align=center style='border-right:none'><h5 align=left> From Date: " + dtpBankDFDate.DateString + " To Date: " + dtpBankDTDate.DateString + " </h5></td>";
                //strHTML = strHTML + " <td COLSPAN=5 align=center style='border-right:none'><h5 align=left> Print Date " + DateTime.Now.ToShortDateString() + " </h5></td>";
                //strHTML = strHTML + " </tr>";
                //strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Bank Deposit</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date</td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >Sender MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Depositor Mobile No.</td>";
                strHTML = strHTML + "<td valign='middle' >MSS A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Type (Offline/Online)</td>";
                strHTML = strHTML + "<td valign='middle' >Txn Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Fee</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSACTION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DEPO_MOB_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["MSS_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRAN_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_FEE"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                        transactionFee = transactionFee + Convert.ToDouble(prow["SERVICE_FEE"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + transactionFee.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message.ToString();
            }
        }
        
    #endregion

        #region Daily Top Up Report New

        else if (checkBoxRank.Checked == true && ddlServiceCode.SelectedValue == "MTP")
        {
            
            try
            {
                //strQuery = " SELECT DISTINCT  TOPUP_TRAN_ID, REQUEST_ID, TRUNC(TRAN_DATE) TRAN_DATE, SOURCE_ACCNT_NO, SUBSCRIBER_MOBILE_NO, SUBSCRIBER_TYPE, "
                //     + " SSL_VRG_UNIQUE_ID, TRAN_AMOUNT,OWNER_CODE FROM TOPUP_TRANSACTION WHERE SSL_FINAL_STATUS IS NOT NULL "
                //     + " AND REVERSE_STATUS='N' AND RESUBMIT_STATUS='N' AND SSL_FINAL_STATUS IN ('900','200','300')"
                //     + " AND TRUNC(TRAN_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "'";


                strQuery = " SELECT DISTINCT TOPUP_TRAN_ID, MTP.REQUEST_ID, TRUNC(TRAN_DATE) TRANSAC_DATE, SOURCE_ACCNT_NO, SUBSCRIBER_MOBILE_NO, "
                         + " SUBSCRIBER_TYPE, SSL_VRG_UNIQUE_ID, TRAN_AMOUNT, OWNER_CODE FROM   TEMP_MIS_TRANSACTIONS_REPORT MIS, "
                         + " BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, TOPUP_TRANSACTION MTP WHERE   SERVICE_CODE IN ('MTP') "
                         + " AND MIS.REQUEST_ID = CAT.REQUEST_ID AND CAT.REQUEST_ID = MTP.REQUEST_ID AND CAS_TRAN_STATUS = 'A' "
                         + " AND TRUNC (TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "'";


                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "Daily_TopUp_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strQuery);
                SaveAuditInfo("Preview", "Daily Top-Up Report");

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MyCash </h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Top-Up(" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Top-Up</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >TXN Date </td>";
                strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
                strHTML = strHTML + "<td valign='middle' >Customer MYCash A/C No. (Initiator)</td>";
                strHTML = strHTML + "<td valign='middle' >Beneficiary Mobile No.</td>";
                strHTML = strHTML + "<td valign='middle' >Txn Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}",Convert.ToDateTime(prow["TRANSAC_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUBSCRIBER_MOBILE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRAN_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        transactionAmount = transactionAmount + Convert.ToDouble(prow["TRAN_AMOUNT"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            }
            catch (Exception exception)
            {
                lblMsg.Text = exception.Message.ToString();
            }
        }
#endregion
		
		#region Account Registration Report by Agent 

        else if (ddlServiceCode.SelectedValue == "RG")
        {
            try
            {
                strQuery = " SELECT ALS.ACCNT_NO AGENT_ACCNT, ALD.ACCNT_NO CUS_ACCNT, TM.SERVICE_FEE, TM.NET_COMMISSION, TM.AIT, TM.SERVICE_VAT_TAX, TM.POOL_ACCOUNT, TM.AGENT_COMMISSION, TM.THIRDPARTY_COM_AMOUNT, TM.VENDOR_COMMISSION, TM.CHANNEL_COMMISSION, TM.AGENT_OPT_COMMISSION, TM.BANK_COMMISSION FROM ACCOUNT_LIST ALS, TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD WHERE ALS.ACCNT_NO = TM.REQUEST_PARTY || '1' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN '" + dtpFDate.DateString + "' AND '" + dtpTDate.DateString + "' AND TM.SERVICE_CODE = 'RG' AND ALS.ACCNT_RANK_ID IN ('" + ddlSourceRank.SelectedValue.ToString() + "') AND ALD.ACCNT_NO = TM.RECEPENT_PARTY || '1' AND ALD.ACCNT_RANK_ID IN ('" + ddlDestinationRank.SelectedValue.ToString() + "') ORDER BY TM.TRANSACTION_DATE DESC ";

                string strSQL = "", strHTML = "", fileName = "";
                lblMsg.Text = "";

                SaveAuditInfo("Preview", "Account Registration Report");
                DataSet dtsAccntBalance = new DataSet();
                fileName = "Account_Registration_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=14 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=14 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=14 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Customer Account Registration Report  (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=14 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Customer Account Registration Report</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Agent Account</td>";
                strHTML = strHTML + "<td valign='middle' >Customer Account</td>";
                strHTML = strHTML + "<td valign='middle' >Service Fee</td>";
                strHTML = strHTML + "<td valign='middle' >Net Commission</td>";
                strHTML = strHTML + "<td valign='middle' >AIT</td>";
                strHTML = strHTML + "<td valign='middle' >Service_VAT_TAX</td>";
                strHTML = strHTML + "<td valign='middle' >Pool Account</td>";
                strHTML = strHTML + "<td valign='middle' >Agent Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Third Party Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Vendor Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Channel Commssion</td>";
                strHTML = strHTML + "<td valign='middle' >Agent_Opt_Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Bank Commission</td>";
                
                strHTML = strHTML + "</tr>";

                if (dtsAccntBalance.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                    {

                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_ACCNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUS_ACCNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_FEE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["NET_COMMISSION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AIT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_VAT_TAX"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["POOL_ACCOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_COMMISSION"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["THIRDPARTY_COM_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["VENDOR_COMMISSION"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CHANNEL_COMMISSION"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_OPT_COMMISSION"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["BANK_COMMISSION"].ToString() + "</td>";

                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        //transactionAmount = transactionAmount + Convert.ToDouble(prow["TRANSACTION_AMOUNT"].ToString());
                    }
                }

                //strHTML = strHTML + "<tr>";
                //strHTML = strHTML + " <td > " + "" + " </td>";
                //strHTML = strHTML + " <td > " + "" + " </td>";
                //strHTML = strHTML + " <td > " + "" + " </td>";
                //strHTML = strHTML + " <td > " + "" + " </td>";
                //strHTML = strHTML + " <td > " + "" + " </td>";
                //strHTML = strHTML + " <td > " + "" + " </td>";
                //strHTML = strHTML + " <td > " + "Total:" + " </td>";
                //strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
                //strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }
        #endregion
		
        else
        {
            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "You Have Selected Wrong Source/Destination Rank or Service List...";
        }
    }
    #endregion

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    #region Daily DPS Account Open

    // Daily DPS Account Opening Report
    protected void btnAccOpenReport_Click(object sender, EventArgs e)
    {
        string strSQL = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();

        try
        {
            strSQL =   " SELECT DISTINCT TO_CHAR( ACTIVE_DATE,'DD/Mon/YYYY') TRAN_DATE,FN_DEPO_ACC_ID TRAN_ID,"
                     + " DECODE(AL.CAS_ACC_NO,NULL,CAL.CAS_ACC_NO,AL.CAS_ACC_NO)CAS_ACC_NO, ACCOUNT_NO,TERM||' '||TERM_IN TENURE,DEPO_AMT "
                     + " FROM BDMIT_ERP_101.FN_DEPOSIT FD, BDMIT_ERP_101.CAS_ACCOUNT_LIST AL,BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL "
                     + " WHERE FD.PRIMARY_ACC_ID=AL.CAS_ACC_ID(+) AND FD.SECONDARY_ACC_ID=CAL.CAS_ACC_ID(+) AND DEPO_STATUS='A' "
                     + " AND LOAN_TYPE_ID='1402040000000007' AND FD.ACTIVE_DATE BETWEEN TO_DATE('" + dtpDate.DateString + "') AND TO_DATE('" + dtpToAODate.DateString + "') "
                     + " AND ACCOUNT_NO IS NOT NULL";

            //+ " AND LOAN_TYPE_ID='1402040000000007' AND FD.ACTIVE_DATE BETWEEN '" + dtpDate.DateString + "' AND '" + dtpToAODate.DateString + "' "
            // dtpFDate = 1/NOV/2013, 1/feb/2013

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Daily_DPS_Account_Opening_Report";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSQL);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily MYDpS A/C opening (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td COLSPAN=6 align=center style='border-right:none'><h5 align=left> Date: " + dtpDate.DateString + " </h5></td>";
            //strHTML = strHTML + " <td COLSPAN=5 align=center style='border-right:none'><h5 align=left> Print Date " + DateTime.Now.ToShortDateString() + " </h5></td>";
            //strHTML = strHTML + " </tr>";
            //strHTML = strHTML + "</table>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>MYDpS</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >TXN Date</td>";
            strHTML = strHTML + "<td valign='middle' >TXN ID</td>";
            strHTML = strHTML + "<td valign='middle' >MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >MYDpS A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Tenure</td>";
            strHTML = strHTML + "<td valign='middle' >Installment Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRAN_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRAN_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_ACC_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACCOUNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TENURE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DEPO_AMT"].ToString() + "</td>";
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

            //btnAccOpenReport.Enabled = true;
            //btnAccOpenReport.Attributes.Add("OnClientClick", "this.Enabled = true; this.value = 'Submitted...';_doPostBack('btnAccOpenReport', '');");
            //btnAccOpenReport.Attributes.Add("OnClientClick", "javascript:enableButton()");
            SaveAuditInfo("Preview", "Daily DPS Account Opening Report");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        }
        catch (Exception exception)
        {
            lblMsg.Text = exception.Message.ToString();
        }
    }

    #endregion

    #region Customer KYC Pending Status Report

    protected void btnKYCRPT_Click(object sender, EventArgs e)
    {
        string strSQL = "", strMsg = "", strQuery = "", strHTML = "", fileName = "";
        lblMsg.Text = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        try
        {
            strSQL = "PRO_INSERT_ALL_HIERARCHY";
            strMsg = objServiceHandler.ExecuteProcedure(strSQL);

            DataSet dtsAccntBalance = new DataSet();
            fileName = "KYC_Pending_Status";

            //------------------------------------------Report Query -------------------------------------

            strQuery = " SELECT DEL_ACCNT_NO,AR.RANK_TITEL,CL.CLINT_NAME,MD.DISTRICT_NAME,THL.SA_ACCNT_NO,THL.A_ACCNT_NO,"
                     + " CLA.CLINT_NAME AGENT_NAME,CLA.CLINT_ADDRESS1 AGENT_ADDRESS,ALC.ACCNT_NO CUST_ACC_NO,CLC.CREATION_DATE,"
                     + " CLC.KYC_UPDATED_BY FROM TEMP_HIERARCHY_LIST_ALL THL,ACCOUNT_LIST AL,ACCOUNT_RANK AR,"
                     + " CLIENT_LIST CL,MANAGE_THANA MT,MANAGE_DISTRICT MD,ACCOUNT_LIST ALA,CLIENT_LIST CLA,"
                     + " ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST ALC, CLIENT_LIST CLC"
                     + " WHERE THL.DEL_ACCNT_ID=AL.ACCNT_ID  AND AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID"
                     + " AND AL.CLINT_ID=CL.CLINT_ID AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) AND"
                     + " ALA.ACCNT_ID=THL.A_ACCNT_ID AND ALA.CLINT_ID=CLA.CLINT_ID AND ASD.AGENT_MOBILE_NO='+88'||SUBSTR(THL.A_ACCNT_NO,1,11) "
                     + " AND ALC.ACCNT_MSISDN=ASD.CUSTOMER_MOBILE_NO AND ALC.CLINT_ID=CLC.CLINT_ID "
                     + " AND TO_CHAR(TO_DATE(CLC.CREATION_DATE,'DD/MM/YYYY')) BETWEEN TO_DATE('" + dtpKYCFDate.DateString
                     + "') AND TO_DATE('" + dtpKYCToDate.DateString + "') AND CLC.KYC_UPDATED_BY IS NULL ";

            dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
            SaveAuditInfo("Report", "Customer KYC Pending Status");

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center> Mercantile Bank Limited </h3></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center> MYCash </h3></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center> Daily Customer KYC Approval Pending status (Date) </h3></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center> Customer KYC Approval Pending status </h4></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td COLSPAN=5 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h5 align=left> Date Range Between From Date: " + dtpKYCFDate.DateString + " To Date: " + dtpKYCToDate.DateString + " </h5></td>";
            strHTML = strHTML + " <td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h5 align=right> Print Date " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h5></td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"80%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' > SerialNo</td>";
            strHTML = strHTML + "<td valign='middle' > Distributor MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' > Distributor Type </td>";
            strHTML = strHTML + "<td valign='middle' > Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' > District </td>";
            strHTML = strHTML + "<td valign='middle' > DSE MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' > Agent MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' > Agent Name </td>";
            strHTML = strHTML + "<td valign='middle' > Agent Address </td>";
            strHTML = strHTML + "<td valign='middle' > Customer MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' > Customer Registration Date </td>";

            strHTML = strHTML + "</tr>";
            if (dtsAccntBalance.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DEL_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SA_ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["A_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CREATION_DATE"].ToString() + " </td>";
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
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    #region pending/idle customer status report

    protected void btnIdleCust_Click(object sender, EventArgs e)
    {
        string strQuery = "", fileName = "", strHTML = "";

        try
        {
            strQuery = " SELECT DISTINCT AL.ACCNT_NO CUST_ACC_NO,TRUNC(ACTIVATION_DATE)REG_DATE,CL.CLINT_NAME CUST_NAME,CL.CLINT_ADDRESS1 CUST_ADD, "
                      + " SUBSTR(AGENT_MOBILE_NO,4,11) AGNT_ACCNT,CLI.CLINT_NAME AGNT_NAME,MT.THANA_NAME AGNT_THANA,MD.DISTRICT_NAME AGNT_DISTRICT, "
                      + " SA_ACCNT_NO DSE_ACCNT,DEL_ACCNT_NO DIS_ACCNT,CLD.CLINT_NAME DIS_NAME,MDD.DISTRICT_NAME DISTRIBUTOR_DISTRICT "
                      + " FROM ACCOUNT_LIST AL ,ACCOUNT_SERIAL_DETAIL ASD,CLIENT_LIST CL,ACCOUNT_LIST ALI,CLIENT_LIST CLI,MANAGE_THANA MT,MANAGE_DISTRICT MD, "
                      + " TEMP_HIERARCHY_LIST_ALL THL,ACCOUNT_LIST ALD,CLIENT_LIST CLD,MANAGE_THANA MTD,MANAGE_DISTRICT MDD "
                      + " WHERE   AL.ACCNT_MSISDN =ASD.CUSTOMER_MOBILE_NO AND AL.CLINT_ID=CL.CLINT_ID "
                      + " AND ASD.AGENT_MOBILE_NO=ALI.ACCNT_MSISDN AND ALI.CLINT_ID=CLI.CLINT_ID "
                      + " AND CLI.THANA_ID=MT.THANA_ID AND MT.DISTRICT_ID=MD.DISTRICT_ID AND ALI.ACCNT_ID=THL.A_ACCNT_ID(+) "
                      + " AND THL.DEL_ACCNT_ID=ALD.ACCNT_ID(+) AND ALD.CLINT_ID=CLD.CLINT_ID(+) AND CLD.THANA_ID=MTD.THANA_ID(+) "
                      + " AND MTD.DISTRICT_ID=MDD.DISTRICT_ID(+) AND AL.ACCNT_STATE='I' AND AL.ACCNT_RANK_ID='130914000000000001' "
                      + " AND TO_CHAR(TO_DATE( ACTIVATION_DATE, 'DD/MM/YYYY')) BETWEEN TO_DATE('" + dtpCusFDate.DateString + "') AND TO_DATE('" + dtpCusTDate.DateString + "') "
                      + " ORDER BY TRUNC(ACTIVATION_DATE),AL.ACCNT_NO";

            DataSet dtsAccntBalance = new DataSet();
            clsServiceHandler objServiceHandler = new clsServiceHandler();
            fileName = "Idle_Customer_Status_Report";

            dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
            SaveAuditInfo("Report", "Idle/Pending Customer Status Report");

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center> Mercantile Bank Limited </h3></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center> MYCash </h3></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center> Customer Account Status (pending/idle) Report (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h3></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center> Customer Account Status Report </h3></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"1\" width=\"80%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >SlNo</td>";
            strHTML = strHTML + "<td valign='middle' > Customer MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' > Customer Name </td>";
            strHTML = strHTML + "<td valign='middle' > Customer Address </td>";
            strHTML = strHTML + "<td valign='middle' > Agent MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' > Agent Name </td>";
            strHTML = strHTML + "<td valign='middle' > Thana </td>";
            strHTML = strHTML + "<td valign='middle' > DSE MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' > Distributor MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' > Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' > District </td>";

            strHTML = strHTML + "</tr>";
            if (dtsAccntBalance.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_ADD"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGNT_ACCNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGNT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGNT_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_ACCNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ACCNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_DISTRICT"].ToString() + "</td>";
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

            SaveAuditInfo("Report", "pending/idle customer status report");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }
        catch (Exception exception)
        {

            lblMsg.Text = exception.Message.ToString();
        }
    }
    #endregion

    # region Daily Agent Balance
    protected void btnAgentBalance_Click(object sender, EventArgs e)
    {
        string strQuery = "";
        double transactionAmount = 0;

        //strQuery = " SELECT DISTINCT AL.ACCNT_NO AGENT_ACC_NO, AL.ACCNT_RANK_ID , ACCOUNT_BALANCE(AL.ACCNT_NO) ACC_BALANCE, "
        //         + " (CASE WHEN AL.ACCNT_RANK_ID ='120519000000000005' THEN 'MBL_AGENT' "
        //         + " WHEN AL.ACCNT_RANK_ID ='130922000000000004' THEN 'ROBI_AGENT' "
        //         + " WHEN AL.ACCNT_RANK_ID ='140410000000000004' THEN 'GP_AGENT' "
        //         + " END) AS AGENT_PARTY, SUBSTR(AL.ACCNT_NO, 1,3) OPERATOR_NAME "
        //         + " FROM ACCOUNT_LIST AL, TEMP_HIERARCHY_LIST_ALL THA "
        //         + " WHERE THA.A_ACCNT_NO = AL.ACCNT_NO "
        //         + " AND AL.ACCNT_RANK_ID IN ('120519000000000005', '130922000000000004','140410000000000004') "
        //         + " AND THA.A_ACCNT_NO IS NOT NULL AND AL.ACCNT_NO IS NOT NULL"
        //         + " ORDER BY AGENT_PARTY DESC";

        strQuery = " SELECT DISTINCT ACCNT_NO,PARTY_TYPE,CAS_ACCNT_BALANCE, SUBSTR (AL.ACCNT_NO, 1, 3) OPERATOR_NAME "
                 + " FROM ACCOUNT_LIST AL,ACCOUNT_RANK AR,CLIENT_BANK_ACCOUNT CBA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,"
                 + " BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB WHERE  AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID "
                 + " AND AL.ACCNT_NO=CBA.CLINT_BANK_ACC_NO AND CBA.CLINT_BANK_ACC_LOGIN=CAL.CAS_ACC_NO "
                 + " AND CAL.CAS_ACC_ID=CAB.CAS_ACC_ID AND AL.ACCNT_RANK_ID IN ('120519000000000005','130922000000000004')"
                 + " ORDER BY CAS_ACCNT_BALANCE DESC";

        string strHTML = "", fileName = "";
        lblMsg.Text = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        DataSet dtsAccount = new DataSet();
        fileName = "Daily_Agent_Balance_Report";
        //------------------------------------------Report File xl processing   -------------------------------------

        dtsAccount = objServiceHandler.ExecuteQuery(strQuery);
        SaveAuditInfo("Preview", "Daily Agent Balance Report");

        strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
        strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
        strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MYCash </h3></td></tr>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily Agent Balance (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Agent Balance</h2></td></tr>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td valign='middle' >Sl</td>";
        strHTML = strHTML + "<td valign='middle' >Agent MYCash A/C No. </td>";
        strHTML = strHTML + "<td valign='middle' >Operator Name of Agent's Mobile No.</td>";
        strHTML = strHTML + "<td valign='middle' >Agent Type (1st Party or 3rd Party)</td>";
        strHTML = strHTML + "<td valign='middle' >Balance</td>";
        strHTML = strHTML + "</tr>";

        if (dtsAccount.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in dtsAccount.Tables[0].Rows)
            {
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["OPERATOR_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["PARTY_TYPE"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["CAS_ACCNT_BALANCE"].ToString() + " </td>";
                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;

                transactionAmount = transactionAmount + Convert.ToDouble(prow["CAS_ACCNT_BALANCE"].ToString());
            }
        }

        strHTML = strHTML + "<tr>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "Total:" + " </td>";
        strHTML = strHTML + " <td > " + transactionAmount.ToString() + " </td>";
        strHTML = strHTML + " </tr>";
        strHTML = strHTML + " </table>";

        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");


    }
    #endregion


    // 2(LoadChildAccount, LoadAffair) method for fis gl account ending balance(for using inside interim mis report)
    // data for corporate distributor ho and corporate agent ending balance

    private void LoadChildAccount(DataTable myDataTable, string strAccID, ref double dblOpenBal, ref double dblCurBal, ref double dblDebit, ref double dblCredit, ref double dblEndBalance)
    {
        clsServiceHandler objGeneralLedger = new clsServiceHandler();
        DataSet dtsServiceReq = new DataSet();
        DataSet dtsAccOpenBal = new DataSet();
        DataSet dtsAccBal = new DataSet();
        double dblLocal_OpenBal = 0;
        double dblLocal_CurBal = 0;
        double dblLocal_Debit = 0;
        double dblLocal_Credit = 0;
        double dblTmpDebit = 0;
        double dblTmpCredit = 0;
        double dblTemAmount = 0;
        double dblLocal_EndBal = 0;

        string strGlAccId = "";
        string strGlEndingBalance = "";
        string strGlCorpAgentEndingBalance = "";



        ///##################
        if (ddlCmpBranchList.SelectedIndex < 1)
        {
            dtsServiceReq = objGeneralLedger.GetChildAccount("All", ddlCmpBranchList.SelectedValue.ToString(), strAccID);
        }
        else
        {
            if (chkAllAccount.Checked)
            {
                dtsServiceReq = objGeneralLedger.GetChildAccount("All", ddlCmpBranchList.SelectedValue.ToString(), strAccID);
            }
            else
            {
                dtsServiceReq = objGeneralLedger.GetChildAccount("Bon", "'" + ddlCmpBranchList.Items[0].Value.ToString() + "','" + ddlCmpBranchList.SelectedValue + "'", strAccID);
            }
        }

        ///##################
        try
        {
            foreach (DataRow pRow in dtsServiceReq.Tables["GL_CHART_OF_ACC"].Rows)
            {
                //-----------------------------------------
                if (pRow["IS_ACC"].ToString().Equals("Y"))
                {
                    DataRow dataRow = myDataTable.NewRow();
                    dataRow[0] = pRow["ACC_NAME"].ToString();
                    string strRow0 = Convert.ToString(dataRow[0]);
                    dataRow[1] = pRow["ACC_ID"].ToString();
                    string strRow1 = Convert.ToString(dataRow[1]);
                    strGlAccId = strRow1;
                    if (strRow1 == "205060005")
                    {

                    }

                    if (strRow1 == "205060015")
                    {

                    }

                    //-----------------------------------------
                    dataRow[2] = String.Format("{0:0,0.00}", 0);
                    string strRow2 = Convert.ToString(dataRow[2]);
                    dataRow[3] = String.Format("{0:0,0.00}", 0);
                    string strRow3 = Convert.ToString(dataRow[3]);
                    dataRow[4] = String.Format("{0:0,0.00}", 0);
                    string strRow4 = Convert.ToString(dataRow[4]);
                    dataRow[5] = String.Format("{0:0,0.00}", 0);
                    string strRow5 = Convert.ToString(dataRow[5]);
                    dataRow[6] = String.Format("{0:0,0.00}", 0);
                    string strRow6 = Convert.ToString(dataRow[6]);
                    dataRow[7] = pRow["ACC_LEVEL"].ToString();
                    string strRow7 = Convert.ToString(dataRow[7]);
                    dataRow[8] = pRow["IS_ACC"].ToString();
                    string strRow8 = Convert.ToString(dataRow[8]);




                    //------------------------------------------
                    myDataTable.Rows.Add(dataRow);
                    if (ddlCmpBranchList.SelectedIndex < 1)
                    {
                        dtsAccOpenBal = objGeneralLedger.GetAccOpeningBalance("A", ddlCmpBranchList.SelectedValue.ToString(), ddlAccYear.SelectedValue.ToString(), pRow["ACC_ID"].ToString());
                        dtsAccBal = objGeneralLedger.GetAccTranAmount("A", ddlCmpBranchList.SelectedValue.ToString(), pRow["ACC_ID"].ToString(), dtpAsOff.DateString, ddlAccYear.SelectedValue.ToString());
                    }
                    else
                    {
                        dtsAccOpenBal = objGeneralLedger.GetAccOpeningBalance("", ddlCmpBranchList.SelectedValue.ToString(), ddlAccYear.SelectedValue.ToString(), pRow["ACC_ID"].ToString());
                        dtsAccBal = objGeneralLedger.GetAccTranAmount("", ddlCmpBranchList.SelectedValue.ToString(), pRow["ACC_ID"].ToString(), dtpAsOff.DateString, ddlAccYear.SelectedValue.ToString());
                    }
                    foreach (DataRow bRow in dtsAccOpenBal.Tables["OPENING_BALANCE"].Rows)
                    {
                        dblOpenBal = dblOpenBal + double.Parse(bRow["OPENING_BAL"].ToString());
                        dblCurBal = dblCurBal + double.Parse(bRow["CURRENT_BAL"].ToString());
                        //----------------------------------
                        dataRow[2] = String.Format("{0:0,0.00}", double.Parse(bRow["OPENING_BAL"].ToString()));
                        dataRow[3] = String.Format("{0:0,0.00}", double.Parse(bRow["CURRENT_BAL"].ToString()));
                        //------------------
                        dblTmpCredit = 0;
                        dblTmpDebit = 0;
                        //----------------------------------------
                        foreach (DataRow cRow in dtsAccBal.Tables["DAILY_TRAN"].Rows)
                        {
                            dblDebit = dblDebit + double.Parse(cRow["Debit"].ToString());
                            dblCredit = dblCredit + double.Parse(cRow["Credit"].ToString());

                            dataRow[4] = String.Format("{0:0,0.00}", cRow["Debit"]);
                            dataRow[5] = String.Format("{0:0,0.00}", cRow["Credit"]);

                            dblTmpCredit = double.Parse(cRow["Credit"].ToString());
                            dblTmpDebit = double.Parse(cRow["Debit"].ToString());
                        }
                        if (bRow["ACC_TYPE"].ToString().Equals("L"))
                        {
                            dblTemAmount = (double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpCredit - dblTmpDebit);
                            dblEndBalance = dblEndBalance + dblTemAmount;
                            if (double.Parse(bRow["ACC_LIMIT"].ToString()) <= 0)
                            {
                                dataRow[6] = String.Format("{0:0,0.00}", double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpCredit - dblTmpDebit);
                                if (strGlAccId == "205060015")
                                {
                                    strGlCorpAgentEndingBalance = dataRow[6].ToString();
                                    HDFAgentEndBal.Value = strGlCorpAgentEndingBalance; 
                                }
                            }
                            else
                            {
                                if (double.Parse(bRow["ACC_LIMIT"].ToString()) <= dblTemAmount)
                                {
                                    dblTemAmount = (double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpCredit - dblTmpDebit) - double.Parse(bRow["ACC_LIMIT"].ToString());
                                    dblEndBalance = dblEndBalance + (dblTemAmount * -1);
                                    dataRow[6] = String.Format("{0:0,0.00}", dblEndBalance);
                                }
                                else
                                {
                                    dataRow[6] = String.Format("{0:0,0.00}", double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpCredit - dblTmpDebit);
                                }
                            }
                        }
                        else if (bRow["ACC_TYPE"].ToString().Equals("R"))
                        {
                            dblTemAmount = (double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpCredit - dblTmpDebit);
                            dblEndBalance = dblEndBalance + dblTemAmount;
                            if (double.Parse(bRow["ACC_LIMIT"].ToString()) <= 0)
                            {
                                dataRow[6] = String.Format("{0:0,0.00}", double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpCredit - dblTmpDebit);
                            }
                            else
                            {
                                if (double.Parse(bRow["ACC_LIMIT"].ToString()) <= dblTemAmount)
                                {
                                    dblTemAmount = (double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpCredit - dblTmpDebit) - double.Parse(bRow["ACC_LIMIT"].ToString());
                                    dblEndBalance = dblEndBalance + (dblTemAmount * -1);
                                    dataRow[6] = String.Format("{0:0,0.00}", dblEndBalance);
                                }
                                else
                                {
                                    dataRow[6] = String.Format("{0:0,0.00}", double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpCredit - dblTmpDebit);
                                }
                            }
                        }
                        else
                        {
                            dblEndBalance = dblEndBalance + double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpDebit - dblTmpCredit;
                            dataRow[6] = String.Format("{0:0,0.00}", double.Parse(bRow["CURRENT_BAL"].ToString()) + dblTmpDebit - dblTmpCredit);
                        }

                    }

                }
                else
                {
                    dblLocal_OpenBal = 0;
                    dblLocal_CurBal = 0;
                    dblLocal_Debit = 0;
                    dblLocal_Credit = 0;
                    dblLocal_EndBal = 0;
                    DataRow dataRow = myDataTable.NewRow();
                    dataRow[0] = pRow["ACC_NAME"].ToString();
                    dataRow[1] = pRow["ACC_ID"].ToString();
                    myDataTable.Rows.Add(dataRow);
                    LoadChildAccount(myDataTable, pRow["ACC_ID"].ToString(), ref dblLocal_OpenBal, ref dblLocal_CurBal, ref dblLocal_Debit, ref dblLocal_Credit, ref dblLocal_EndBal);
                    dataRow[2] = String.Format("{0:0,0.00}", dblLocal_OpenBal);
                    dataRow[3] = String.Format("{0:0,0.00}", dblLocal_CurBal);
                    dataRow[4] = String.Format("{0:0,0.00}", dblLocal_Debit);
                    dataRow[5] = String.Format("{0:0,0.00}", dblLocal_Credit);
                    dataRow[6] = String.Format("{0:0,0.00}", dblLocal_EndBal);
                    dataRow[7] = pRow["ACC_LEVEL"].ToString();
                    dataRow[8] = pRow["IS_ACC"].ToString();
                    //------------------------------------------
                    //------------------------------------------------------
                    dblOpenBal = dblOpenBal + dblLocal_OpenBal;
                    dblCurBal = dblCurBal + dblLocal_CurBal;
                    dblDebit = dblDebit + dblLocal_Debit;
                    dblCredit = dblCredit + dblLocal_Credit;
                    dblEndBalance = dblEndBalance + dblLocal_EndBal;
                }

                if (strGlAccId == "205060005")
                {
                    strGlEndingBalance = Convert.ToString(dblEndBalance);
                    HDFDistributorEndBal.Value = strGlEndingBalance; 
                }

                //if (strGlAccId == "205060015")
                //{
                //    strGlEndingBalance = strGlCorpAgentEndingBalance;
                //}

            }
        }
        catch (NullReferenceException ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void LoadAffair()
    {
        clsServiceHandler objGeneralLedger = new clsServiceHandler();
        //Step 1: C# Code to Create ASP.Net DataTable
        // Initialize a DataTable
        DataTable myDataTable = new DataTable();
        // Initialize DataColumn
        DataColumn myDataColumn = new DataColumn();

        //// 1| Add and Create a first DataColumn
        //myDataTable.Columns.Add(myDataColumn);
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Account Name";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Account Code";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        // 2| initialize a new instance of DataColumn to add another column with different properties.
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Year Begining Balance";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //3| --------------------------------------------------------------
        // 2| initialize a new instance of DataColumn to add another column with different properties.
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Cutoff Balance";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //3| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Debit";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //4| -------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Credit";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //5| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Ending Balance";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //6| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Account Level";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);
        //7| --------------------------------------------------------------
        myDataColumn = new DataColumn();
        myDataColumn.ColumnName = "Group Type";
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataTable.Columns.Add(myDataColumn);

        //Step 2: C# Code to Add New Rows to ASP.Net DataTable
        // create a new row using NewRow() function of DataTable.
        // dataRow object will inherit the schema of myDataTable to create a new rowte
        double dblOpenBal = 0;
        double dblCurBal = 0;
        double dblDebit = 0;
        double dblCredit = 0;
        double dblEndBalance = 0;
        string strBranchType = "All";

        DataSet dtsServiceReq = new DataSet();
        ///##################
        if (ddlType.SelectedValue.ToString() == "AL")
        {

            if (ddlCmpBranchList.SelectedIndex < 1)
            {
                dtsServiceReq = objGeneralLedger.GetAssetSAccount("All", ddlCmpBranchList.SelectedValue.ToString(), "1", "");
            }
            else
            {
                if (chkAllAccount.Checked)
                {
                    dtsServiceReq = objGeneralLedger.GetAssetSAccount("All", "'" + ddlCmpBranchList.Items[0].Value.ToString() + "','" + ddlCmpBranchList.SelectedValue + "'", "1", "");
                }
                else
                {
                    dtsServiceReq = objGeneralLedger.GetAssetSAccount("Bon", "'" + ddlCmpBranchList.Items[0].Value.ToString() + "','" + ddlCmpBranchList.SelectedValue + "'", "1", "");
                }
            }
        }
        else
        {
            if (ddlCmpBranchList.SelectedIndex < 1)
            {
                dtsServiceReq = objGeneralLedger.GetIncomeSAccount("All", ddlCmpBranchList.SelectedValue.ToString(), "1", "");
            }
            else
            {
                if (chkAllAccount.Checked)
                {
                    dtsServiceReq = objGeneralLedger.GetIncomeSAccount("All", "'" + ddlCmpBranchList.Items[0].Value.ToString() + "','" + ddlCmpBranchList.SelectedValue + "'", "1", "");
                }
                else
                {
                    dtsServiceReq = objGeneralLedger.GetIncomeSAccount("Bon", "'" + ddlCmpBranchList.Items[0].Value.ToString() + "','" + ddlCmpBranchList.SelectedValue + "'", "1", "");
                }
            }
        }
        ///##################
        try
        {
            foreach (DataRow pRow in dtsServiceReq.Tables["GL_CHART_OF_ACC"].Rows)
            {
                dblOpenBal = 0;
                dblCurBal = 0;
                dblDebit = 0;
                dblCredit = 0;
                dblEndBalance = 0;
                DataRow dataRow = myDataTable.NewRow();
                dataRow[0] = pRow["ACC_NAME"].ToString();
                string strRow0 = Convert.ToString(dataRow[0]);
                dataRow[1] = pRow["ACC_ID"].ToString();
                string strRow1 = Convert.ToString(dataRow[1]);
                if (strRow1 == "205060015")
                {

                }
                myDataTable.Rows.Add(dataRow);
                LoadChildAccount(myDataTable, pRow["ACC_ID"].ToString(), ref dblOpenBal, ref dblCurBal, ref dblDebit, ref dblCredit, ref dblEndBalance);
                dataRow[2] = String.Format("{0:0,0.00}", dblOpenBal);
                string strRow2 = Convert.ToString(dataRow[2]);
                dataRow[3] = String.Format("{0:0,0.00}", dblCurBal);
                string strRow3 = Convert.ToString(dataRow[3]);
                dataRow[4] = String.Format("{0:0,0.00}", dblDebit);
                string strRow4 = Convert.ToString(dataRow[4]);
                dataRow[5] = String.Format("{0:0,0.00}", dblCredit);
                string strRow5 = Convert.ToString(dataRow[5]);
                dataRow[6] = String.Format("{0:0,0.00}", dblEndBalance);
                string strRow6 = Convert.ToString(dataRow[6]);
                dataRow[7] = pRow["ACC_LEVEL"].ToString();
                string strRow7 = Convert.ToString(dataRow[7]);
                dataRow[8] = pRow["IS_ACC"].ToString();
                string strRow8 = Convert.ToString(dataRow[8]);
                //------------------------------------------------------


            }
        }
        catch (NullReferenceException ex)
        {
            Response.Write(ex.Message.ToString());
        }
        // add new data row to the data table.
        //Step 3: C# Code to bind ASP.Net GridView to DataTable
        //gdvOpenBal.DataSource = myDataTable;
        //gdvOpenBal.DataBind();
    }

    private void LoadMonthlySalaryDisburseAmount()
    {
        try
        {
            hdfSalDisbAmt.Value = "";
            string strSql = " SELECT SUM(TOTAL_ACCOUNT) SUM_TOTAL_ACCOUNT, SUM(CAS_PRS_AMOUNT) SUM_CAS_PRS_AMOUNT  FROM "
                            + " ( SELECT CSI.CAS_PRM_ID, COUNT(CSI.CAS_PRM_ID) TOTAL_ACCOUNT,SUM(CSI.CAS_PRS_AMOUNT) CAS_PRS_AMOUNT, "
                            + " TRUNC(ADD_MONTHS(SYSDATE, -1), 'MM') AS F_DATE, TRUNC(LAST_DAY(ADD_MONTHS(SYSDATE, -1))) T_DATE , "
                            + " TRUNC(CSI.CAS_PRS_DISBURSE_DATE) CAS_PRS_DISBURSE_DATE, CSI.CMP_BRANCH_ID,CM.CAS_PRM_UPLOAD_FILE_NAME "
                            + " FROM BDMIT_ERP_101.CAS_PR_SALARY_ACCOUNT_INFO CSI,BDMIT_ERP_101.CAS_PR_MONTH_CREATION CM "
                            + " WHERE CSI.CAS_PRS_DISBURSE_STATUS='Y' AND CSI.CAS_PRM_ID=CM.CAS_PRM_ID "
                            + " AND  TRUNC(CSI.CAS_PRS_DISBURSE_DATE) BETWEEN TRUNC(ADD_MONTHS(SYSDATE, -1), 'MM') AND TRUNC(LAST_DAY(ADD_MONTHS(SYSDATE, -1))) "
                            + " GROUP BY CSI.CAS_PRM_ID,TRUNC(CSI.CAS_PRS_DISBURSE_DATE),CSI.CMP_BRANCH_ID,CM.CAS_PRM_UPLOAD_FILE_NAME "
                            + " ORDER BY TRUNC(CSI.CAS_PRS_DISBURSE_DATE) )TM ";

            DataSet ods = new DataSet();
            clsServiceHandler objServiceHandler = new clsServiceHandler();
            ods = objServiceHandler.ExecuteQuery(strSql);
            if (ods.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in ods.Tables[0].Rows)
                {
                    hdfSalDisbAmt.Value = prow["SUM_CAS_PRS_AMOUNT"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    
    #region interim mis report
    protected void btnInterimMISReport_Click(object sender, EventArgs e)
    {
        //AvoidMultipleSubmit(btnInterimMISReport);
        //btnInterimMISReport.Enabled = false;
        //btnInterimMISReport.Attributes.Add("OnClientClick", "if(Page_ClientValidate('ValidatePage')){this.disabled = true;}" + ClientScript.GetPostBackEventReference(btnInterimMISReport, null) + ";");

        // data for loading corporate distributor and agent ending balance
        hdfSalDisbAmt.Value = "";
        LoadAffair();
        LoadMonthlySalaryDisburseAmount();
        
        
        //data from view interim mis report 
        try
        {
            string strQuery = "";
            double sumOfNumber = 0;
            double sumOfAmount = 0;
            strQuery = " SELECT * FROM APSNG101.VW_INTERIM_MIS_REPORT";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";
            clsServiceHandler objServiceHandler = new clsServiceHandler();
            DataSet dtsAccount = new DataSet();
            fileName = "Daily_MIS_Interim_Report";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strQuery);
            
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MYCash </h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily MIS Report (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' ></td>";
            strHTML = strHTML + "<td valign='middle' >Number</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "</tr>";
            double countTotal = 0;
            double amount = 0;

            //btnInterimMISReport.Enabled = true;
            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    //if (prow["PARTICULARS"].ToString() != "Particulars")
                    //{
                    //    strHTML = strHTML + " <td valign='middle' style='font-size:14px;font-weight:bold;'> " + prow["PARTICULARS"].ToString() + " </td>";
                    //}

                    if (prow["COUNT_TOTAL"].ToString() == "")
                    {
                        countTotal = 0;
                    }
                    else
                    {
                        countTotal = Convert.ToDouble(prow["COUNT_TOTAL"]);
                    }
                    if (prow["AMOUNT"].ToString() == "")
                    {
                        amount = 0;
                    }
                    else
                    {
                        amount = Convert.ToDouble(prow["AMOUNT"]);
                    }
                    if (prow["PARTICULARS"].ToString() != "Total:" && prow["PARTICULARS"].ToString() != "Particulars")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > " + prow["PARTICULARS"].ToString() + " </td>";
                        if (prow["COUNT_TOTAL"].ToString() != "0")
                        {
                            strHTML = strHTML + " <td align='right'> '" + prow["COUNT_TOTAL"].ToString() + " </td>";
                        }
                        else
                        {
                            strHTML = strHTML + " <td > </td>";
                        }
                        if (prow["AMOUNT"].ToString() != "0")
                        {
                            strHTML = strHTML + " <td align='right'> '" + prow["AMOUNT"].ToString() + " </td>";
                        }
                        else
                        {
                            strHTML = strHTML + " <td > </td>";
                        }
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        sumOfNumber = sumOfNumber + Convert.ToDouble(countTotal);
                        sumOfAmount = sumOfAmount + Convert.ToDouble(amount);
                    }
                    else
                    {
                        strHTML = strHTML + " <tr>";
                        strHTML = strHTML + " <td > " + "" + " </td>";
                        strHTML = strHTML + " <td valign='middle' style='font-size:14px;font-weight:bold;'> " + prow["PARTICULARS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + sumOfNumber.ToString() + " </td>";
                        strHTML = strHTML + " <td > " + sumOfAmount.ToString() + " </td>";
                        strHTML = strHTML + " </tr>";
                        sumOfNumber = 0;
                        sumOfAmount = 0;
                    }
                    
                }

                

            }

            strHTML = strHTML + " </table>";

            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + " <tr>";
            strHTML = strHTML + " <td COLSPAN=4 > " + "DEPOSIT CORPORATE HO(DISTRIBUTOR): " + HDFDistributorEndBal.Value.ToString() + " </td>";
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + " <tr>";
            strHTML = strHTML + " <td COLSPAN=4 > " + "DEPOSIT CORPORATE AGENT: " + HDFAgentEndBal.Value.ToString() + " </td>";
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + " <tr>";
            strHTML = strHTML + " <td COLSPAN=4 > " + "Last Month's Salary Disbursement Amount: " + hdfSalDisbAmt.Value.ToString() + " </td>";
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + " </table>"; 


            try
            {
                btnInterimMISReport.Enabled = true;
                //btnInterimMISReport.Attributes.Add("OnClientClick", "this.enabled = true; _doPostBack('btnInterimMISReport',''); return true;");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }

            
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape"); 
        }

        catch (Exception exception)
        {
            lblMsg.Text = exception.Message;
        }
        
    }
    #endregion

    # region distributor wise agent list

    protected void btnDisAgent_Click(object sender, EventArgs e)
    {
        try
        {
            string strQuery = "";

            strQuery = " SELECT DEL_ACCNT_NO DIS_ACCONT,CL.CLINT_NAME DIS_NAME ,SA_ACCNT_NO DSE_ACCONT,CLD.CLINT_NAME DSE_NAME,A_ACCNT_NO , "
                     + " CLA.CLINT_NAME AGENT_NAME, PARTY_TYPE,CLA.CLINT_ADDRESS1 AGENT_ADD,THANA_NAME AGENT_THANA,DISTRICT_NAME AGENT_DIS "
                     + " FROM TEMP_HIERARCHY_LIST_ALL HL,ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_LIST ALD,CLIENT_LIST CLD,ACCOUNT_LIST ALA, "
                     + " CLIENT_LIST CLA,ACCOUNT_RANK AR, MANAGE_THANA MT,MANAGE_DISTRICT MD WHERE HL.DEL_ACCNT_ID=AL.ACCNT_ID "
                     + " AND AL.CLINT_ID=CL.CLINT_ID AND HL.SA_ACCNT_ID=ALD.ACCNT_ID AND ALD.CLINT_ID=CLD.CLINT_ID AND HL.A_ACCNT_ID=ALA.ACCNT_ID "
                     + " AND ALA.CLINT_ID=CLA.CLINT_ID AND ALA.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND CLA.THANA_ID=MT.THANA_ID(+) "
                     + " AND MT.DISTRICT_ID=MD.DISTRICT_ID(+)";


            try
            {
                Session["ReportSQL"] = strQuery;
                SaveAuditInfo("View", "Distributor wise Agent list");
                Session["RequestForm"] = "../MIS/frmMBL_Various_MIS_Report.aspx";
                Session["ReportFile"] = "../MIS/AgentList_DistributorWise_REPORT.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }




        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }


    }

    #endregion

    #region Distributor Performance Report

    protected void btnDisPerformance_Click(object sender, EventArgs e)
    {
        string strQuery = "";

        try
        {
            strQuery = "SELECT * FROM VW_DISTRIBUTOR_PERFORMANCE";
            
            #region for excel report

            //string strHTML = "", fileName = "";
            //lblMsg.Text = "";
            //DateTime dt1 = DateTime.Now;

            //double noOfDseasonToday = 0;
            //double noOfAgentAsonToday = 0;
            //double totalDepositDistrbutorAsonToday = 0;
            //double totalDepositDseAsOnToday = 0;
            //double totalDepositAgentAsOnToday = 0;
            //double noOfCustomerRegPlsAuthoLm = 0;
            //double noOfCustomerRegPlsAuthoToday = 0;
            //double noOfCusPendingToday = 0;
            //double noOfCashInToday = 0;
            //double noOfCashOutToday = 0;
            //double noOfCashInLm = 0;
            //double noOfCashOutLm = 0;
            //double totalAmtCustCashInToday = 0;
            //double totalAmtCustCashOutToday = 0;
            //double totalAmtCustCashInLm = 0;
            //double totalAmtCustCashOutLm = 0;
            //double noOfAgentPerformTransactionToday = 0;
            //double noOfAgentPerformRegToday = 0;
            //double avgNoCashInLweek = 0;
            //double avgNoOfCAshOutLweek = 0;



            //clsServiceHandler objServiceHandler = new clsServiceHandler();
            //DataSet dtsAccount = new DataSet();
            //fileName = "Daily_Distributor_Performance_Report";
            ////------------------------------------------Report File xl processing   -------------------------------------

            //dtsAccount = objServiceHandler.ExecuteQuery(strQuery);
            //SaveAuditInfo("Preview", "Daily Distributor Performance Report");

            //strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=25 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
            //strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=25 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h2 align=center> MYCash </h3></td></tr>";
            //strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=25 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor's Performance Report(" + String.Format("{0:dd-MMM-yyyy}", dtpDisPerformance.DateString = String.Format("{0:dd-MMM-yyyy}", dt1.AddDays(-1))) + ")</h2></td></tr>";
            //strHTML = strHTML + "</table>";
            //strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + " <td COLSPAN=25 align=right style='border-right:none;font-size:12px;font-weight:bold;'> Print Date "  + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) +  " </td>";
            //strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + "<td valign='middle' >Sl</td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor's MYCash A/C No. </td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            //strHTML = strHTML + "<td valign='middle' >District</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of DSE (as on today)</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of Agent (as on today)</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of DSE (as on today)</td>";
            //strHTML = strHTML + "<td valign='middle' >Total Deposit (Distributor) as on today</td>";
            //strHTML = strHTML + "<td valign='middle' >Total Deposit (DSE) as on today</td>";
            //strHTML = strHTML + "<td valign='middle' >Total Deposit (Agent) as on today</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of Customer (Registered + Authorized) last one month</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of Customer (Registered + Authorized) as on today</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of Customer (Pending only) as on today</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of Customer Cash-In (only Today)</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of Customer Cash-Out (only Today)</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of Customer Cash-In (last one Month)</td>";
            //strHTML = strHTML + "<td valign='middle' >No. of Customer Cash-Out (last one Month)</td>";
            //strHTML = strHTML + "<td valign='middle' >Total Amount Customer Cash-In (only Today)</td>";
            //strHTML = strHTML + "<td valign='middle' >Total Amount Customer Cash-Out (only Today)</td>";
            //strHTML = strHTML + "<td valign='middle' >Total Amount Customer Cash-In (last one month)</td>";
            //strHTML = strHTML + "<td valign='middle' >Total Amount Customer Cash-Out (last one month)</td>";
            //strHTML = strHTML + "<td valign='middle' >No. Of Agents perform transaction (only today)</td>";
            //strHTML = strHTML + "<td valign='middle' >Average no. Of  Cash-In (Last 7 days)</td>";
            //strHTML = strHTML + "<td valign='middle' >Average no. of Cash-Out (Last 7 days)</td>";
            //strHTML = strHTML + "</tr>";

            //if (dtsAccount.Tables[0].Rows.Count > 0)
            //{
            //    int SerialNo = 1;
            //    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
            //    {
            //        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
            //        strHTML = strHTML + " <td > '" + prow["DIS_ACCOUNT"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["DIS_ADDRESS"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TOTAL_AGENT"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TOTAL_DSE"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["DIS_BAL"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["DSE_BAL"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["AGENT_BAL"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TMONTH_CUSTOMER"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TODAY_CUSTOMER"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TODAY_PCUSTOMER"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TODAY_CASIN"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TODAY_CASHOUT"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TMONTH_CASIN"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TMONTH_CASHOUT"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TODAY_CASIN_AMT"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TODAY_CASHOUT_AMT"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TMONTH_CASIN_AMT"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TMONTH_CASHOUT_AMT"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TODAY_NO_OF_AGENT_PERFORM"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["TODAY_NO_OF_AGENT_PERFORM_RG"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["AVARG_CASIN_LASTWEEK"].ToString() + " </td>";
            //        strHTML = strHTML + " <td > '" + prow["AVARG_CASOUT_LASTWEEK"].ToString() + " </td>";
            //        strHTML = strHTML + " </tr> ";

            //        SerialNo = SerialNo + 1;

            //        noOfDseasonToday = noOfDseasonToday + Convert.ToDouble(prow["TOTAL_DSE"].ToString());
            //        noOfAgentAsonToday = noOfAgentAsonToday + Convert.ToDouble(prow["TOTAL_AGENT"].ToString());
            //        totalDepositDistrbutorAsonToday = totalDepositDistrbutorAsonToday + Convert.ToDouble(prow["DIS_BAL"].ToString());
            //        totalDepositDseAsOnToday = totalDepositDseAsOnToday + Convert.ToDouble(prow["DSE_BAL"].ToString());
            //        totalDepositAgentAsOnToday = totalDepositAgentAsOnToday + Convert.ToDouble(prow["AGENT_BAL"].ToString());
            //        noOfCustomerRegPlsAuthoLm = noOfCustomerRegPlsAuthoLm + Convert.ToDouble(prow["TMONTH_CUSTOMER"].ToString());
            //        noOfCustomerRegPlsAuthoToday = noOfCustomerRegPlsAuthoToday + Convert.ToDouble(prow["TODAY_CUSTOMER"].ToString());
            //        noOfCusPendingToday = noOfCusPendingToday + Convert.ToDouble(prow["TODAY_PCUSTOMER"].ToString());
            //        noOfCashInToday = noOfCashInToday + Convert.ToDouble(prow["TODAY_CASIN"].ToString());
            //        noOfCashOutToday = noOfCashOutToday + Convert.ToDouble(prow["TODAY_CASHOUT"].ToString());
            //        noOfCashInLm = noOfCashInLm + Convert.ToDouble(prow["TMONTH_CASIN"].ToString());
            //        noOfCashOutLm = noOfCashOutLm + Convert.ToDouble(prow["TMONTH_CASHOUT"].ToString());
            //        totalAmtCustCashInToday = totalAmtCustCashInToday + Convert.ToDouble(prow["TODAY_CASIN_AMT"].ToString());
            //        totalAmtCustCashOutToday = totalAmtCustCashOutToday + Convert.ToDouble(prow["TODAY_CASHOUT_AMT"].ToString());
            //        totalAmtCustCashInLm = totalAmtCustCashInLm + Convert.ToDouble(prow["TMONTH_CASIN_AMT"].ToString());
            //        totalAmtCustCashOutLm = totalAmtCustCashOutLm + Convert.ToDouble(prow["TMONTH_CASHOUT_AMT"].ToString());
            //        noOfAgentPerformTransactionToday = noOfAgentPerformTransactionToday + Convert.ToDouble(prow["TODAY_NO_OF_AGENT_PERFORM"].ToString());
            //        noOfAgentPerformRegToday = noOfAgentPerformRegToday + Convert.ToDouble(prow["TODAY_NO_OF_AGENT_PERFORM_RG"].ToString());
            //        avgNoCashInLweek = avgNoCashInLweek + Convert.ToDouble(prow["AVARG_CASIN_LASTWEEK"].ToString());
            //        avgNoOfCAshOutLweek = avgNoOfCAshOutLweek + Convert.ToDouble(prow["AVARG_CASOUT_LASTWEEK"].ToString());
            //    }
            //}

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "Total:" + " </td>";
            //strHTML = strHTML + " <td > " + noOfDseasonToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfAgentAsonToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + totalDepositDistrbutorAsonToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + totalDepositDseAsOnToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + totalDepositAgentAsOnToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfCustomerRegPlsAuthoLm.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfCustomerRegPlsAuthoToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfCusPendingToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfCashInToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfCashOutToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfCashInLm.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfCashOutLm.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + totalAmtCustCashInToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + totalAmtCustCashOutToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + totalAmtCustCashInLm.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + totalAmtCustCashOutLm.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfAgentPerformTransactionToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + noOfAgentPerformRegToday.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + avgNoCashInLweek.ToString() + " </td>";
            //strHTML = strHTML + " <td > " + avgNoOfCAshOutLweek.ToString() + " </td>";


            //strHTML = strHTML + " </tr>";
            //strHTML = strHTML + " </table>";

            //clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

#endregion

            try
            {
                Session["ReportSQL"] = strQuery;
                SaveAuditInfo("View", "Distributor Performance Report");
                Session["RequestForm"] = "../MIS/frmMBL_Various_MIS_Report.aspx";
                Session["ReportFile"] = "../MIS/Distributor_Performance_Report.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }
        catch (Exception exception)
        {

            exception.Message.ToString();
        }

        

    }

    #endregion

    #region agent performance Report

    protected void btnAgentPerformanceRpt_Click(object sender, EventArgs e)
    {
        string strQuery = "";
        try
        {
            strQuery = "SELECT * FROM VW_AGENT_PERFORMANCE";

            try
            {
                Session["ReportSQL"] = strQuery;
                SaveAuditInfo("View", "Agent Performance Report");
                Session["RequestForm"] = "../MIS/frmMBL_Various_MIS_Report.aspx";
                Session["ReportFile"] = "../MIS/Agent_Performance_Report.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        

    }

    #endregion

    #region agent commission reoprt as per customer 1st month transaction

    protected void btnAgentCommFM_Click(object sender, EventArgs e)
    {
        string strQuery = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        try
        {
            #region old query
            //strQuery = " SELECT DISTINCT SUBSTR(ASD.CUSTOMER_MOBILE_NO, 4, 14)||1 CUST_ACC_NO, AL.ACCNT_NO , ASD.ACTIVATION_DATE CUST_REG_DATE, "
            //     + " TRUNC(CL.CREATION_DATE) CUST_VERIFY_DATE, SUBSTR(ASD.AGENT_MOBILE_NO, 4, 14)||1 AGENT_ACC_NO, "
            //     + " CASE WHEN AL1.ACCNT_STATE = 'I' THEN 'INACTIVE' WHEN AL1.ACCNT_STATE = 'A' THEN 'ACTIVE' END) AGENT_STATUS, "
            //     + " THA.DEL_ACCNT_NO DIS_ACC_NO, CL1.CLINT_NAME DIS_NAME, "
            //     + " APSNG101.PKG_MIS_REPORTS.FUNC_COUNT_CUST_TRAN_FST_MONTH(AL.ACCNT_NO) COUNT_CUST_TRAN_FMONTH "
            //     + " FROM ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST AL, CLIENT_LIST CL, ACCOUNT_LIST AL1, ACCOUNT_LIST AL2, CLIENT_LIST CL1,"
            //     + " TEMP_HIERARCHY_LIST_ALL THA  WHERE SUBSTR(ASD.CUSTOMER_MOBILE_NO, 4, 14)||1 = AL.ACCNT_NO(+) "
            //     + " AND AL.CLINT_ID = CL.CLINT_ID(+) AND SUBSTR(ASD.AGENT_MOBILE_NO, 4, 14)||1 = AL1.ACCNT_NO(+) AND "
            //     + " THA.A_ACCNT_NO(+) = AL1.ACCNT_NO AND THA.DEL_ACCNT_NO = AL2.ACCNT_NO(+) AND AL2.CLINT_ID = CL1.CLINT_ID(+) AND "
            //     + " ASD.CUSTOMER_MOBILE_NO IS NOT NULL AND ASD.AGENT_MOBILE_NO IS NOT NULL";
            #endregion

            strQuery = " SELECT DISTINCT AL.ACCNT_NO CUST_ACC_NO,TRUNC(ACTIVATION_DATE)REG_DATE,TRUNC(CL.VERIFIED_DATE)VERIFIED_DATE, " //NVL(TRUNC(CL.VERIFIED_DATE), '') VERIFIED_DATE,
                     + " SUBSTR(AGENT_MOBILE_NO,4,11)||1 AGENT_ACC_NO, ALI.ACCNT_STATE AGENT_STATUS, HL.DEL_ACCNT_NO DIS_ACC_NO, "
                     + " CLD.CLINT_NAME DIS_NAME, PKG_MIS_REPORTS.FUNC_COUNT_CUST_TRAN_FST_MONTH(AL.ACCNT_NO) COUNT_TRX "
                     + " FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_LIST ALI,TEMP_HIERARCHY_LIST_ALL HL,"
                     + " ACCOUNT_LIST ALD,CLIENT_LIST CLD WHERE TRUNC(ADD_MONTHS(SYSDATE,-1))-1 =TRUNC(ACTIVATION_DATE) "
                     + " AND CUSTOMER_MOBILE_NO=AL.ACCNT_MSISDN AND AL.CLINT_ID=CL.CLINT_ID AND AGENT_MOBILE_NO=ALI.ACCNT_MSISDN "
                     + " AND ALI.ACCNT_NO=HL.A_ACCNT_NO(+) AND HL.DEL_ACCNT_NO=ALD.ACCNT_NO AND ALD.CLINT_ID=CLD.CLINT_ID "
                     + " AND AL.ACCNT_RANK_ID='120519000000000006' ORDER BY REG_DATE";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Daily_Agent_Comm_AsPer_CustTrx(1stm)_Report";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strQuery);
            SaveAuditInfo("Preview", "Daily Agent Comm(1m) Report");

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 valign=center style='border-right:none;font-size:14px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 valign=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MyCash </h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 valign=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Customer Transaction wise Agent commission report (1st month txn )(" + DateTime.Today.ToLongDateString() + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Customer MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
            strHTML = strHTML + "<td valign='middle' >A/C Verification Date</td>";
            strHTML = strHTML + "<td valign='middle' >Agent MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Status</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C Name</td>";
            strHTML = strHTML + "<td valign='middle' >1st Month No. of Trx Count(CN, CCT, FT, MTP, MP, BD)</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["VERIFIED_DATE"].ToString() != "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["REG_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1; 
                    }

                    else if (prow["VERIFIED_DATE"].ToString() == "")
                    {
                        //strHTML = strHTML + " <td ></td>";
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["REG_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td >  </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1; 
                    }

                    else
                    {
                        
                    }
                    
                    
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

            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
        
    }
    #endregion 

    #region agent commission reoprt as per customer 2nd month transaction
    protected void btnAgentConn2ndMonth_Click(object sender, EventArgs e)
    {
        string strQuery = "";

        try
        {
            #region old query

            //strQuery = " SELECT DISTINCT SUBSTR(ASD.CUSTOMER_MOBILE_NO, 4, 14)||1 CUST_ACC_NO, AL.ACCNT_NO , ASD.ACTIVATION_DATE CUST_REG_DATE, "
            //     + " TRUNC(CL.CREATION_DATE) CUST_VERIFY_DATE, SUBSTR(ASD.AGENT_MOBILE_NO, 4, 14)||1 AGENT_ACC_NO, "
            //     + " (CASE WHEN AL1.ACCNT_STATE = 'I' THEN 'INACTIVE' WHEN AL1.ACCNT_STATE = 'A' THEN 'ACTIVE' END) AGENT_STATUS,"
            //     + " THA.DEL_ACCNT_NO DIS_ACC_NO, CL1.CLINT_NAME DIS_NAME, "
            //     + " APSNG101.PKG_MIS_REPORTS.FUNC_COUNT_CUST_TRAN_SCND_MNTH(AL.ACCNT_NO) COUNT_CUST_TRAN_LMONTH "
            //     + " FROM ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST AL, CLIENT_LIST CL, ACCOUNT_LIST AL1, ACCOUNT_LIST AL2, CLIENT_LIST CL1,"
            //     + " TEMP_HIERARCHY_LIST_ALL THA  WHERE SUBSTR(ASD.CUSTOMER_MOBILE_NO, 4, 14)||1 = AL.ACCNT_NO(+) "
            //     + " AND AL.CLINT_ID = CL.CLINT_ID(+) AND SUBSTR(ASD.AGENT_MOBILE_NO, 4, 14)||1 = AL1.ACCNT_NO(+) AND "
            //     + " THA.A_ACCNT_NO(+) = AL1.ACCNT_NO AND THA.DEL_ACCNT_NO = AL2.ACCNT_NO(+) AND AL2.CLINT_ID = CL1.CLINT_ID(+) AND "
            //     + " ASD.CUSTOMER_MOBILE_NO IS NOT NULL AND ASD.AGENT_MOBILE_NO IS NOT NULL";


            //strQuery = " SELECT DISTINCT AL.ACCNT_NO CUST_ACC_NO,TRUNC(ACTIVATION_DATE) REG_DATE,TRUNC(CL.VERIFIED_DATE)VERIFIED_DATE, "
            //           + " SUBSTR(AGENT_MOBILE_NO,4,11)||1 AGENT_ACC_NO, ALI.ACCNT_STATE AGENT_STATUS, HL.DEL_ACCNT_NO DIS_ACC_NO, "
            //           + " CLD.CLINT_NAME DIS_NAME, PKG_MIS_REPORTS.FUNC_COUNT_CUST_TRAN_SCND_MNTH(AL.ACCNT_NO) COUNT_TRX "
            //           + " FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_LIST ALI, "
            //           + " TEMP_HIERARCHY_LIST_ALL HL,ACCOUNT_LIST ALD,CLIENT_LIST CLD WHERE  "
            //           + " TRUNC(ACTIVATION_DATE) = TRUNC(ADD_MONTHS(SYSDATE,-2))-1 AND CUSTOMER_MOBILE_NO=AL.ACCNT_MSISDN "
            //           + " AND AL.CLINT_ID=CL.CLINT_ID AND AGENT_MOBILE_NO=ALI.ACCNT_MSISDN AND ALI.ACCNT_NO=HL.A_ACCNT_NO(+) "
            //           + " AND HL.DEL_ACCNT_NO=ALD.ACCNT_NO AND ALD.CLINT_ID=CLD.CLINT_ID AND AL.ACCNT_RANK_ID='120519000000000006' "
            //           + " ORDER BY REG_DATE";

            #endregion

            strQuery = " SELECT DISTINCT AL.ACCNT_NO CUST_ACC_NO,TRUNC(ACTIVATION_DATE) REG_DATE,TRUNC(CL.VERIFIED_DATE)VERIFIED_DATE, "
                       + " SUBSTR(AGENT_MOBILE_NO,4,11)||1 AGENT_ACC_NO, ALI.ACCNT_STATE AGENT_STATUS, HL.DEL_ACCNT_NO DIS_ACC_NO, "
                       + " CLD.CLINT_NAME DIS_NAME, PKG_MIS_REPORTS.FUNC_COUNT_CUST_TRAN_FST_MONTH(AL.ACCNT_NO) COUNT_TRX "
                       + " FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_LIST ALI, "
                       + " TEMP_HIERARCHY_LIST_ALL HL,ACCOUNT_LIST ALD,CLIENT_LIST CLD WHERE  "
                       + " TRUNC(ACTIVATION_DATE) = TRUNC(ADD_MONTHS(SYSDATE,-2))-1 AND CUSTOMER_MOBILE_NO=AL.ACCNT_MSISDN "
                       + " AND AL.CLINT_ID=CL.CLINT_ID AND AGENT_MOBILE_NO=ALI.ACCNT_MSISDN AND ALI.ACCNT_NO=HL.A_ACCNT_NO(+) "
                       + " AND HL.DEL_ACCNT_NO=ALD.ACCNT_NO AND ALD.CLINT_ID=CLD.CLINT_ID AND AL.ACCNT_RANK_ID='120519000000000006' "
                       + " ORDER BY REG_DATE";

            clsServiceHandler objServiceHandler = new clsServiceHandler();

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Daily_Agent_Comm_AsPer_CustTrx(2ndm)_Report";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strQuery);
            SaveAuditInfo("Preview", "Daily Agent Comm(2ndM) Report");

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 valign=center style='border-right:none;font-size:14px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 valign=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MyCash </h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 valign=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Customer Transaction wise Agent commission report (2nd month txn )(" + DateTime.Today.ToLongDateString() + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Customer MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
            strHTML = strHTML + "<td valign='middle' >A/C Verification Date</td>";
            strHTML = strHTML + "<td valign='middle' >Agent MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Status</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C Name</td>";
            strHTML = strHTML + "<td valign='middle' >2nd Month No. of Trx Count(CN, CCT, FT, MTP, MP, BD)</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                //int SerialNo = 1;
                //foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                //{
                //    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                //    strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                //    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["REG_DATE"].ToString())) + " </td>";
                //    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                //    //strHTML = strHTML + " <td > '" + prow["VERIFIED_DATE"].ToString() + " </td>";
                //    strHTML = strHTML + " <td > '" + prow["AGENT_ACC_NO"].ToString() + " </td>";
                //    strHTML = strHTML + " <td > '" + prow["AGENT_STATUS"].ToString() + " </td>";
                //    strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                //    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                //    strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                //    strHTML = strHTML + " </tr> ";
                //    SerialNo = SerialNo + 1;

                //}

                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["VERIFIED_DATE"].ToString() != "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["REG_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }

                    else if (prow["VERIFIED_DATE"].ToString() == "")
                    {
                        //strHTML = strHTML + " <td ></td>";
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["REG_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td >  </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }

                    else
                    {

                    }


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

            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    #endregion 

    #region Daily Agent Aquisition Report
        protected void btnAgentAquisiotionRpt_Click(object sender, EventArgs e)
        {
            string strQuery = "";

            try
            {
                strQuery = "SELECT DISTINCT DEL_ACCNT_NO,'3RD_PARTY',CL.CLINT_NAME DIS_NAME,DISTRICT_NAME DIS_DISTRICT,A_ACCNT_NO AGENT_ACC, "
                         + " CLI.CLINT_NAME AGENT_NAME, CLI.CLINT_ADDRESS1 AGENT_ADD,TRUNC(CLI.VERIFIED_DATE) VERIFICATION_DATE "
                         + " FROM ACCOUNT_LIST AL,TEMP_HIERARCHY_LIST_ALL HL,ACCOUNT_LIST ALI,CLIENT_LIST CL,MANAGE_THANA MT, "
                         + " MANAGE_DISTRICT MD,CLIENT_LIST CLI WHERE AL.ACCNT_RANK_ID='120519000000000005' "
                         + " AND AL.ACCNT_ID=HL.A_ACCNT_ID(+) AND HL.DEL_ACCNT_NO=ALI.ACCNT_NO(+) AND ALI.CLINT_ID=CL.CLINT_ID(+)"
                         + " AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) AND AL.ACCNT_ID=CLI.CLINT_ID(+) "
                         + " AND TRUNC(CLI.VERIFIED_DATE) BETWEEN '" + dtpAgentAquiFromDate.DateString + "' AND '" + dtpAgentAquiToDate.DateString + "' "
                         //+ " AND TRUNC(CLI.VERIFIED_DATE) BETWEEN '1-Jul-2014' AND '30-Jul-2014' "
                         + " ORDER BY VERIFICATION_DATE,DIS_NAME";

                Session["ReportSQL"] = strQuery;
                SaveAuditInfo("View", "Daily Agent Aquisition Report");
                Session["RequestForm"] = "../MIS/frmMBL_Various_MIS_Report.aspx";
                Session["ReportFile"] = "../MIS/Agent_Aqui_Report.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    #endregion

    #region Customer A/C Approve and KYC Update Details

        protected void btnCustAprvNKyc_Click(object sender, EventArgs e)
        {
            string strSql = "";
            try
            {
                #region queryold
                //strSql = " SELECT DISTINCT AL.ACCNT_NO CUSTOMER_ACC_NO, ASD.ACTIVATION_DATE REG_DATE, TRUNC(CL.VERIFIED_DATE) VERIFY_DATE, "
                //       + " CL.UPDATE_DATE KYC_UPDATE_DATE, ASD.AGENT_MOBILE_NO, THA.A_ACCNT_NO AGENT_ACC_NO, THA.DEL_ACCNT_NO DIS_ACC_NO,  "
                //       + " CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRS, MT.THANA_NAME DIS_THANA, MD.DISTRICT_NAME DIS_DISTRICT,  "
                //       + " CL.VERIFIED_BY VERIFIED_BY, CL.KYC_UPDATED_BY KYC_UPDATED_BY, ALKY.ACCNT_NO KYC_ACC_NO, AR.RANK_TITEL RANK_TITEL, CLKY.CLINT_NAME KYC_AC_NAME, "
                //       + " CLKY.CLINT_ADDRESS1 KYC_ADDRS, MTKY.THANA_NAME KYC_THANA, MDKY.DISTRICT_NAME KYC_DISTRICT "
                //       + " FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD, CLIENT_LIST CL, TEMP_HIERARCHY_LIST_ALL THA, "
                //       + " ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MT, MANAGE_DISTRICT MD, ACCOUNT_LIST ALKY, CLIENT_LIST CLKY,"
                //       + " MANAGE_THANA MTKY, MANAGE_DISTRICT MDKY, ACCOUNT_RANK AR WHERE  "
                //       + " AL.ACCNT_NO(+) = SUBSTR(ASD.CUSTOMER_MOBILE_NO, 4.14)||1 AND AL.CLINT_ID = CL.CLINT_ID(+) "
                //       + " AND SUBSTR(ASD.AGENT_MOBILE_NO, 4, 14)||1 = THA.A_ACCNT_NO  AND THA.A_ACCNT_NO = ALD.ACCNT_NO(+) AND "
                //       + " ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID(+) AND "
                //       + " CL.KYC_UPDATED_BY = ALKY.ACCNT_ID(+) AND ALKY.CLINT_ID = CLKY.CLINT_ID(+) AND "
                //       + " CLKY.THANA_ID = MTKY.THANA_ID(+) AND MTKY.DISTRICT_ID = MDKY.DISTRICT_ID(+) "
                //       + " AND ALKY.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) "
                //       + " AND TRUNC(CL.VERIFIED_DATE) BETWEEN '" + dtpCustNKycFDate.DateString + "' AND '" + dtpCustNKycFDate.DateString + "' "
                //       + " AND AL.ACCNT_STATE = 'A' AND ASD.BANK_CODE = 'MBL' AND TRUNC(CL.VERIFIED_DATE) IS NOT NULL"
                //       + " ORDER BY ASD.ACTIVATION_DATE DESC";
                #endregion

                strSql = " SELECT DISTINCT AL.ACCNT_NO CUSTOMER_ACC_NO, ASD.ACTIVATION_DATE REG_DATE, TRUNC(CL.VERIFIED_DATE) VERIFY_DATE, "
                         + " CL.UPDATE_DATE KYC_UPDATE_DATE, ASD.AGENT_MOBILE_NO, THA.A_ACCNT_NO AGENT_ACC_NO, THA.DEL_ACCNT_NO DIS_ACC_NO,  "
                         + " CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDRS, MT.THANA_NAME DIS_THANA, MD.DISTRICT_NAME DIS_DISTRICT,  "
                         + " CL.VERIFIED_BY VERIFIED_BY, CL.KYC_UPDATED_BY KYC_UPDATED_BY, ALKY.ACCNT_NO KYC_ACC_NO, AR.RANK_TITEL RANK_TITEL, "
                         + " CLKY.CLINT_NAME KYC_AC_NAME, CLKY.CLINT_ADDRESS1 KYC_ADDRS, MTKY.THANA_NAME KYC_THANA, MDKY.DISTRICT_NAME KYC_DISTRICT  "
                         + " FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD, CLIENT_LIST CL, TEMP_HIERARCHY_LIST_ALL THA, "
                         + " ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MT, MANAGE_DISTRICT MD, ACCOUNT_LIST ALKY, CLIENT_LIST CLKY, "
                         + " MANAGE_THANA MTKY, MANAGE_DISTRICT MDKY, ACCOUNT_RANK AR "
                         + " WHERE  AL.ACCNT_NO(+) = SUBSTR(ASD.CUSTOMER_MOBILE_NO, 4.14)||1 AND AL.CLINT_ID = CL.CLINT_ID(+) "
                         + " AND AL.ACCNT_RANK_ID = '120519000000000006' AND SUBSTR(ASD.AGENT_MOBILE_NO, 4, 14)||1 = THA.A_ACCNT_NO  "
                         + " AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CLD.THANA_ID = MT.THANA_ID(+) "
                         + " AND MT.DISTRICT_ID = MD.DISTRICT_ID(+) AND CL.KYC_UPDATED_BY = ALKY.ACCNT_ID(+) AND "
                         + " ALKY.CLINT_ID = CLKY.CLINT_ID(+) AND CLKY.THANA_ID = MTKY.THANA_ID(+) AND MTKY.DISTRICT_ID = MDKY.DISTRICT_ID(+) "
                         + " AND ALKY.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) AND TRUNC(CL.VERIFIED_DATE) "
                         + " BETWEEN '" + dtpCustNKycFDate.DateString + "' AND '" + dtpCustNKycToDate.DateString + "'  "
                         + " AND AL.ACCNT_STATE = 'A' AND ASD.BANK_CODE = 'MBL' AND TRUNC(CL.VERIFIED_DATE) IS NOT NULL "
                         + " ORDER BY ASD.ACTIVATION_DATE DESC";




                string strHTML = "", fileName = "";
                lblMsg.Text = "";
                clsServiceHandler objServiceHandler = new clsServiceHandler();
                DataSet dtsAccount = new DataSet();
                fileName = "Cust_AC_Aprv_and_KYC_Upd_Det_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MYCash </h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Customer A/C Approve and KYC Update Details Report (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Verification Date Range: '"+dtpCustNKycFDate.DateString+"' To '"+dtpCustNKycToDate.DateString+"'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Customer MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
                strHTML = strHTML + "<td valign='middle' >Verification Date</td>";
                strHTML = strHTML + "<td valign='middle' >KYC Update Date</td>";
                strHTML = strHTML + "<td valign='middle' >Agent MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Thana</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
                strHTML = strHTML + "<td valign='middle' >KYC Update by MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Updated by A/C Rank</td>";
                strHTML = strHTML + "<td valign='middle' >Updated by A/C Name</td>";
                strHTML = strHTML + "<td valign='middle' >Updated by A/C Address</td>";
                strHTML = strHTML + "<td valign='middle' >Updated by A/C Thana</td>";
                strHTML = strHTML + "<td valign='middle' >Updated by A/C District</td>";
                strHTML = strHTML + "</tr>";


                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        string strNullValue = "";

                        if (prow["KYC_UPDATE_DATE"].ToString() == "" || prow["KYC_UPDATED_BY"].ToString() == ""
                            || prow["KYC_ACC_NO"].ToString() == "" || prow["RANK_TITEL"].ToString() == "" ||
                            prow["KYC_AC_NAME"].ToString() == "" || prow["KYC_ADDRS"].ToString() == "" ||
                            prow["KYC_THANA"].ToString() == "" || prow["KYC_DISTRICT"].ToString() == "")
                        {
                            //strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                            strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["CUSTOMER_ACC_NO"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["REG_DATE"].ToString())) + " </td>";
                            strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFY_DATE"].ToString())) + " </td>";
                            strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                            strHTML = strHTML + " <td > '" + prow["AGENT_ACC_NO"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_ADDRS"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                            strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                            strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                            strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                            strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                            strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                            strHTML = strHTML + " </tr> ";
                            SerialNo = SerialNo + 1;
                        }
                        else
                        {
                            strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["CUSTOMER_ACC_NO"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["REG_DATE"].ToString())) + " </td>";
                            strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFY_DATE"].ToString())) + " </td>";
                            strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["KYC_UPDATE_DATE"].ToString())) + " </td>";
                            strHTML = strHTML + " <td > '" + prow["AGENT_ACC_NO"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_ADDRS"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + prow["KYC_ACC_NO"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["KYC_AC_NAME"].ToString() + " </td>";
                            strHTML = strHTML + " <td > '" + prow["KYC_ADDRS"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["KYC_THANA"].ToString() + "</td>";
                            strHTML = strHTML + " <td > '" + prow["KYC_DISTRICT"].ToString() + " </td>";
                            strHTML = strHTML + " </tr> ";
                            SerialNo = SerialNo + 1;
                        }

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

                SaveAuditInfo("Preview", "Cust_AC_Aprv_and_KYC_Upd_Det_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    #endregion

    #region Customer A/C Approve and KYC Update Summary Report
    
        protected void btnCustAprvNKycSummary_Click(object sender, EventArgs e)
        {
            string strSql = "";
            try
            {
                strSql = " SELECT DISTINCT THL.DEL_ACCNT_NO DIS_ACC_NO,CL.CLINT_NAME DIST_NAME ,MD.DISTRICT_NAME DIS_DISTRICT_NAME, "
                       + " MT.THANA_NAME DIST_THANA_NAME, CL.CLINT_ADDRESS1 DIST_ADDRESS, "
                       + " PKG_MIS_REPORTS.FUNC_COUNT_TOTAL_CUST_VERIFIED(THL.DEL_ACCNT_ID) TOTAL_CUST_APPROVED, "
                       + " PKG_MIS_REPORTS.FUNC_COUNT_TOTAL_CUST_UPDATE(THL.DEL_ACCNT_ID) TOTAL_CUST_UPDATE "
                       + " FROM  TEMP_HIERARCHY_LIST_ALL THL,ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT,MANAGE_DISTRICT MD "
                       + " WHERE THL.DEL_ACCNT_ID=AL.ACCNT_ID AND CL.CLINT_ID=AL.CLINT_ID AND CL.THANA_ID=MT.THANA_ID(+) "
                       + " AND MD.DISTRICT_ID(+)=MT.DISTRICT_ID ";


                clsServiceHandler objServiceHandler = new clsServiceHandler();
                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "Cust_AC_Aprv_and_KYC_Upd_Summary_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Customer A/C Approve and KYC Update Summary Report (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Thana</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
                strHTML = strHTML + "<td valign='middle' >Total Customer Approved</td>";
                //strHTML = strHTML + "<td valign='middle' >Total mobicash KYC approve & update</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIST_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIST_THANA_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIST_ADDRESS"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CUST_APPROVED"].ToString() + " </td>";
                        //strHTML = strHTML + " <td > '" + prow["TOTAL_CUST_UPDATE"].ToString() + " </td>";
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
                //strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "Cust_AC_Apv_n_KYC_Upd_Sum_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
                
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }

            
        }
    #endregion

        #region interim mis report with date Jubayer
        protected void btnInterimMISReportWD_Click(object sender, EventArgs e)
        {
            //AvoidMultipleSubmit(btnInterimMISReport);
            //btnInterimMISReport.Enabled = false;
            //btnInterimMISReport.Attributes.Add("OnClientClick", "if(Page_ClientValidate('ValidatePage')){this.disabled = true;}" + ClientScript.GetPostBackEventReference(btnInterimMISReport, null) + ";");

            // data for loading corporate distributor and agent ending balance
            hdfSalDisbAmt.Value = "";
            LoadAffair();
            LoadMonthlySalaryDisburseAmount();


            //data from view interim mis report 
            try
            {
                string strQuery = "";
                double sumOfNumber = 0;
                double sumOfAmount = 0;
                string vDate = vDateP.DateString;
                strQuery = "SELECT 'Agent' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 1     ROW_COUNT, 1     RECORD_TYPE FROM DUAL UNION SELECT 'Particulars' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 2     ROW_COUNT, 1     RECORD_TYPE FROM DUAL UNION SELECT 'No. of 3rd party active agent (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0     AMOUNT, 3     ROW_COUNT, 1     RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '120519000000000005' AND ACCNT_STATE = 'A' UNION SELECT 'No. of 3rd party inactive agent (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0     AMOUNT, 4     ROW_COUNT, 1     RECORD_TYPE FROM ACCOUNT_LIST AL WHERE     ACCNT_RANK_ID = '120519000000000005' AND ACCNT_STATE IN ('E', 'L', 'I') UNION SELECT 'No. of GP active agent (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0     AMOUNT, 5     ROW_COUNT, 1     RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '140410000000000004' AND ACCNT_STATE = 'A' UNION SELECT 'No. of Robi active agent (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0     AMOUNT, 6     ROW_COUNT, 1     RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '130922000000000004' AND ACCNT_STATE = 'A' UNION SELECT 'No. of Paywel active agent (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0     AMOUNT, 7     ROW_COUNT, 1     RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '161215000000000004' AND ACCNT_STATE = 'A' UNION SELECT 'No. of PBazar active agent (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0     AMOUNT, 8     ROW_COUNT, 1     RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '180128000000000008' AND ACCNT_STATE = 'A' UNION SELECT 'No. of MICRO_POS active agent (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0     AMOUNT, 9     ROW_COUNT, 1     RECORD_TYPE FROM ACCOUNT_LIST AL WHERE     ACCNT_RANK_ID IN ('170422000000000003', '170718000000000001', '170422000000000004') AND ACCNT_STATE = 'A' UNION SELECT 'Total:' PARTICULARS, TO_NUMBER ('') COUNT_TOTAL, TO_NUMBER ('') AMOUNT, 10     ROW_COUNT, 1      RECORD_TYPE FROM DUAL UNION SELECT 'Newly added agent (3rd Party) (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 11     ROW_COUNT, 2      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '120519000000000005' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Newly added agent (3rd Party) (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 12     ROW_COUNT, 2      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '120519000000000005' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Newly added agent (GP agent) (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 13     ROW_COUNT, 2      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '140410000000000004' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Newly added agent (Robi agent) (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 14     ROW_COUNT, 2      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '130922000000000004' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Newly added agent (Paywel agent) (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 15     ROW_COUNT, 2      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '161215000000000004' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Newly added agent (PBazar agent) (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 16     ROW_COUNT, 2      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '161215000000000004' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Total:' PARTICULARS, TO_NUMBER ('') COUNT_TOTAL, TO_NUMBER ('') AMOUNT, 17     ROW_COUNT, 2      RECORD_TYPE FROM DUAL UNION SELECT 'Particulars' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 18     ROW_COUNT, 3      RECORD_TYPE FROM DUAL UNION SELECT 'Distribution channel' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 19     ROW_COUNT, 3      RECORD_TYPE FROM DUAL UNION SELECT 'No. of MYCash Active Distributor (Till today)' PARTICULARS, COUNT (*), 0      AMOUNT, 20     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '120519000000000003' AND ACCNT_STATE = 'A' UNION SELECT 'No. of Robi Active Distributor (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 21     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '130922000000000002' AND ACCNT_STATE = 'A' UNION SELECT 'No. of Paywel Active Distributor (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 22     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '161215000000000002' AND ACCNT_STATE = 'A' UNION SELECT 'No. of PBazar Active Distributor/Sub Distributor (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 23     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE     ACCNT_RANK_ID IN ('180128000000000005', '180128000000000006') AND ACCNT_STATE = 'A' UNION SELECT 'No. of MYCash Active DSE (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 24     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '120519000000000004' AND ACCNT_STATE = 'A' UNION SELECT 'No. of Robi Active DSE (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 25     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '130922000000000003' AND ACCNT_STATE = 'A' UNION SELECT 'No. of Paywel Active DSE (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 26     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '161215000000000003' AND ACCNT_STATE = 'A' UNION SELECT 'No. of PBazar Active DSE (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 27     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '180128000000000007' AND ACCNT_STATE = 'A' UNION SELECT 'Total:' PARTICULARS, TO_NUMBER ('') COUNT_TOTAL, TO_NUMBER ('') AMOUNT, 28     ROW_COUNT, 3      RECORD_TYPE FROM DUAL UNION SELECT 'Newly added MYCash Distributor (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 29     ROW_COUNT, 4      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '120519000000000003' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Newly added Robi Distributor (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 30     ROW_COUNT, 4      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '130922000000000002' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Newly addedMYCash DSE (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 31     ROW_COUNT, 4      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '120519000000000004' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Newly addedRobi DSE (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 32     ROW_COUNT, 4      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     ACCNT_RANK_ID = '130922000000000003' AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Total:' PARTICULARS, TO_NUMBER ('') COUNT_TOTAL, TO_NUMBER ('') AMOUNT, 33     ROW_COUNT, 4      RECORD_TYPE FROM DUAL UNION SELECT 'No. of Merchant (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 34     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '131205000000000001' AND ACCNT_STATE = 'A' UNION SELECT 'No. of PBazar Sub Merchant (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 35     ROW_COUNT, 3      RECORD_TYPE FROM ACCOUNT_LIST AL WHERE ACCNT_RANK_ID = '180128000000000003' AND ACCNT_STATE = 'A' UNION SELECT 'Customer' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 36     ROW_COUNT, 5      RECORD_TYPE FROM DUAL UNION SELECT 'Particulars' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 37     ROW_COUNT, 5      RECORD_TYPE FROM DUAL UNION SELECT 'Total Customer (till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 38     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN '01-Jan-2010' AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID IN ('120519000000000006', '130914000000000001') UNION SELECT 'Total Customer (Registered only) (till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 39     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN '01-Jan-2010' AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID IN ('130914000000000001') UNION SELECT 'Total Customer (Registered + Verified ) (till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 40     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN '01-Jan-2010' AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID IN ('120519000000000006') AND AL.ACCNT_STATE = 'A' UNION SELECT 'Total Customer (Verified + InActive) (till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 41     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND TRUNC (ACTIVATION_DATE) BETWEEN '01-Jan-2010' AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID IN ('120519000000000006') AND AL.ACCNT_STATE IN ('L', 'I', 'E') UNION SELECT 'Total Customer (Registered only) (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 42     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO  AND TRUNC (ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Total Customer (Verified only) (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 43     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST           AL, CLIENT_LIST            CL, CM_SYSTEM_USERS        CSU, ACCOUNT_SERIAL_DETAIL  ASD, SERVICE_PACKAGE        SP WHERE     CL.CLINT_ID = AL.CLINT_ID AND (   CSU.ACCNT_ID = CL.VERIFIED_BY OR CSU.ACCNT_ID = CL.KYC_UPDATED_BY) AND AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND SP.SERVICE_PKG_ID = AL.SERVICE_PKG_ID AND (   TO_CHAR (TO_DATE (CL.VERIFIED_DATE, 'DD/MM/YYYY')) BETWEEN TRUNC ( TO_DATE('" + vDate + "','DD-MM-YYYY') ) AND TRUNC ( TO_DATE('" + vDate + "','DD-MM-YYYY') ) OR TO_CHAR (TO_DATE (CL.UPDATE_DATE, 'DD/MM/YYYY')) BETWEEN TRUNC ( TO_DATE('" + vDate + "','DD-MM-YYYY') ) AND TRUNC ( TO_DATE('" + vDate + "','DD-MM-YYYY') )) UNION SELECT 'Total Customer A/C KYC Update (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, 0      AMOUNT, 44     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE     AL.CLINT_ID = CL.CLINT_ID AND ACCNT_RANK_ID IN ('120519000000000006', '130914000000000001') AND TRUNC (CL.UPDATE_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Total Customer (Verified) through GP Agent (till today)' PARTICULARS, COUNT (DISTINCT CUSTOMER_MOBILE_NO) COUNT_TOTAL, 0      AMOUNT, 45     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND BANK_CODE = 'MBL' AND COMMISSION_DISBURSE = 'Y' AND VERIFIED = 'Y' AND AL.ACCNT_RANK_ID = '140410000000000004' AND TRUNC (ACTIVATION_DATE) BETWEEN '1-Jan-2010' AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT DISTINCT 'Total Customer (Registered + Verified) through Robi Agent (till today)' PARTICULARS, COUNT (CUSTOMER_MOBILE_NO) COUNT_TOTAL, 0 AMOUNT, 46 ROW_COUNT, 5 RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '130922000000000004' AND TRUNC (ACTIVATION_DATE) BETWEEN '1-Jan-2010' AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Total Customer (Registered) through GP Agent (till today)' PARTICULARS, COUNT (DISTINCT ASD.CUSTOMER_MOBILE_NO) COUNT_TOTAL, 0      AMOUNT, 47     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '140410000000000004' AND BANK_CODE = 'GP' AND TRUNC (ASD.ACTIVATION_DATE) BETWEEN '1-Jan-2010' AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT DISTINCT 'Total Customer (Registered) through Robi Agent (till today)' PARTICULARS, COUNT (ASD.CUSTOMER_MOBILE_NO) COUNT_TOTAL, 0      AMOUNT, 48     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '130922000000000004' AND COMMISSION_DISBURSE = 'N' AND TRUNC (ASD.ACTIVATION_DATE) BETWEEN '1-Jan-2010' AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT DISTINCT 'Total Customer (Verified only)  through GP Agent (today)' PARTICULARS, COUNT (ASD.CUSTOMER_MOBILE_NO) COUNT_TOTAL, 0      AMOUNT, 49     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '140410000000000004' AND COMMISSION_DISBURSE = 'Y' AND TRUNC (ASD.ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT DISTINCT 'Total Customer (Verified only)  through Robi Agent (today)' PARTICULARS, COUNT (ASD.CUSTOMER_MOBILE_NO) COUNT_TOTAL, 0      AMOUNT, 50     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '130922000000000004' AND COMMISSION_DISBURSE = 'Y' AND TRUNC (ASD.ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT DISTINCT 'Total Customer (Registered) through GP Agent (today)' PARTICULARS, COUNT (ASD.CUSTOMER_MOBILE_NO) COUNT_TOTAL, 0      AMOUNT, 51     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '140410000000000004' AND COMMISSION_DISBURSE = 'N' AND TRUNC (ASD.ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT DISTINCT 'Total Customer (Registered) through Robi Agent (today)' PARTICULARS, COUNT (ASD.CUSTOMER_MOBILE_NO) COUNT_TOTAL, 0      AMOUNT, 52     ROW_COUNT, 5      RECORD_TYPE FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD WHERE     AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO AND AL.ACCNT_RANK_ID = '130922000000000004' AND COMMISSION_DISBURSE = 'N' AND TRUNC (ASD.ACTIVATION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT DISTINCT 'Total Active MyDPS Account Count(till today)' PARTICULARS, COUNT (DEPO_ACC_ID) COUNT_TOTAL, 0      AMOUNT, 53     ROW_COUNT, 5      RECORD_TYPE FROM BDMIT_ERP_101.FN_DEPOSIT WHERE DEPO_STATUS = 'A' UNION SELECT DISTINCT 'Total MyDPS Account Open (today)' PARTICULARS, COUNT (DEPO_ACC_ID) COUNT_TOTAL, 0      AMOUNT, 54     ROW_COUNT, 5      RECORD_TYPE FROM BDMIT_ERP_101.FN_DEPOSIT WHERE     TRUNC (OPEN_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND DEPO_STATUS = 'A' UNION SELECT 'Transaction Summary' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 55     ROW_COUNT, 6      RECORD_TYPE FROM DUAL UNION SELECT 'Particulars' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 56     ROW_COUNT, 6      RECORD_TYPE FROM DUAL UNION SELECT 'PERTEX CABLES LIMITED(till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (AMOUNT), 57     ROW_COUNT, 6      RECORD_TYPE FROM PARTEX_FUND_COLLECTION WHERE     SERVICE_TYPE = 'FM' AND DESTINATION_ACCNT_NO LIKE '0110100%' AND TRUNC (REQUEST_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'PARTEX FURNITURE INDUSTRIES LTD(till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (AMOUNT), 57     ROW_COUNT, 6      RECORD_TYPE FROM PARTEX_FUND_COLLECTION WHERE     SERVICE_TYPE = 'FM' AND DESTINATION_ACCNT_NO LIKE '0110101%' AND TRUNC (REQUEST_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'RUPALI LIFE INSURANCE' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (AMOUNT), 57     ROW_COUNT, 6      RECORD_TYPE FROM RLIL_FUND_COLLECTION WHERE     SERVICE_TYPE = 'FM' AND TRUNC (REQUEST_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'PRIME ISLAMI LIFE INSURANCE LTD(till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (AMOUNT), 57     ROW_COUNT, 6      RECORD_TYPE FROM PILIL_FUND_COLLECTION WHERE     SERVICE_TYPE = 'FM' AND TRUNC (TXN_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Cash In from 3rd Party Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 57 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE = 'CN' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID = '120519000000000005' UNION SELECT 'No. of Cash Out from 3rd Party Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 58 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST                  AL, TEMP_MIS_TRANSACTIONS_REPORT  MIS, ACCOUNT_LIST                  ALI WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND RECEPENT_PARTY = ALI.ACCNT_NO AND SERVICE_CODE IN ('CCT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ALI.ACCNT_RANK_ID = '120519000000000005' UNION SELECT 'No. of Salary Withdrawal  from 3rd Party Agent (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 59 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST                  AL, TEMP_MIS_TRANSACTIONS_REPORT  MIS, ACCOUNT_LIST                  ALI WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND RECEPENT_PARTY = ALI.ACCNT_NO AND SERVICE_CODE IN ('SW') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ALI.ACCNT_RANK_ID = '120519000000000005' UNION SELECT 'No. of Salary Withdrawal  from MBL Branch (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 60 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST                  AL, TEMP_MIS_TRANSACTIONS_REPORT  MIS, ACCOUNT_LIST                  ALI WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND RECEPENT_PARTY = ALI.ACCNT_NO AND SERVICE_CODE IN ('SW') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ALI.ACCNT_RANK_ID = '120519000000000002' UNION SELECT 'Business Collection(till today) ' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 61                           ROW_COUNT, 6                            RECORD_TYPE FROM (  SELECT DISTINCT THA.DEL_ACCNT_NO            DIS_ACC_NO, CLDIS.CLINT_NAME            DIS_NAME, CLDIS.CLINT_ADDRESS1        DIS_ADDR, MTDIS.THANA_NAME            DIS_THANA, MDDIS.DISTRICT_NAME         DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT     TRANSACTION_AMOUNT, TMIS.REQUEST_ID FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST                ALDSE, ACCOUNT_LIST                ALCOR, TEMP_HIERARCHY_LIST_ALL     THA, ACCOUNT_LIST                ALDIS, CLIENT_LIST                 CLDIS, MANAGE_THANA                MTDIS, MANAGE_DISTRICT             MDDIS WHERE     TMIS.SERVICE_CODE = 'FM' AND TRUNC (TMIS.TRANSACTION_DATE) BETWEEN TRUNC ( TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC ( TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TMIS.REQUEST_PARTY || '1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004', '180128000000000007') AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002', '180416000000000001') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.THANA_ID = MDDIS.DISTRICT_ID(+) ORDER BY THA.DEL_ACCNT_NO ASC) TEMP UNION SELECT 'Business Collection(National life insurance)(till today) ' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 62                           ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL WHERE     TM.SERVICE_CODE = 'FM' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TM.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('181219000000000002') UNION SELECT 'Business Collection(Provita )(till today) ', COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 63                           ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL WHERE     TM.SERVICE_CODE = 'FM' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TM.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('190519000000000001') UNION SELECT 'Business Collection(Maxis Dse)(till today) ' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 63                           ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL WHERE     TM.SERVICE_CODE = 'FM' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TM.REQUEST_PARTY || '1' = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('200813000000000003') UNION SELECT 'No. of Cash In from GP Agent point (Till today in this month)' PARTICULARS, COUNT (*), SUM (TRANSACTION_AMOUNT)     AMOUNT, 63                           ROW_COUNT, 6                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('CN') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID = '140410000000000004' UNION SELECT 'No. of Cash Out from GP Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 63                           ROW_COUNT, 6                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE = 'CCT' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND AL.ACCNT_RANK_ID = '140514000000000001' UNION SELECT 'No. of Salary Withdrawal from GP Agent (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 64 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE = 'SW' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND AL.ACCNT_RANK_ID = '140514000000000001' UNION SELECT 'No. of Cash In from Robi Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 65                           ROW_COUNT, 6                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('CN') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID = '130922000000000004' UNION SELECT 'No. of Cash In from Paywel Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 66 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('CN') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID = '161215000000000004' UNION SELECT 'No. of Cash In from PBazar Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 67 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('CN') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID = '180128000000000008' UNION SELECT 'No. of Cash Out from Robi Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 68 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE = 'CCT' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND AL.ACCNT_RANK_ID = '130922000000000004' UNION SELECT 'No. of Cash Out from Paywel Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 69 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE = 'CCT' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND AL.ACCNT_RANK_ID = '161215000000000004' UNION SELECT 'No. of Cash Out from PBazar Agent point (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 70 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE = 'CCT' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND AL.ACCNT_RANK_ID = '180128000000000008' UNION SELECT 'No. of Salary Withdrawal from Robi Agent (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 71 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE = 'SW' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND AL.ACCNT_RANK_ID = '130922000000000004' UNION SELECT 'No. of Bank Deposit from 3rd Party Agent and Micro POS Agent (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 72 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('BD') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID IN ('120519000000000005', '170422000000000003', '170422000000000004') UNION SELECT 'No. of Bank Deposit by MYCash Customer (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 73 ROW_COUNT, 6 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('BD') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID = '120519000000000006' UNION SELECT 'No. of Send Money (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 74                           ROW_COUNT, 6                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('FT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND ACCNT_RANK_ID = '120519000000000006' UNION SELECT 'No. of Top-Up (Till today in this month)' PARTICULARS, COUNT (DISTINCT TOPUP_TRAN_ID), SUM (TRAN_AMOUNT)     AMOUNT, 75                    ROW_COUNT, 6                     RECORD_TYPE FROM TOPUP_TRANSACTION WHERE     ALL_FINAL_STATUS = 'S' AND TRUNC (TRAN_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) UNION SELECT 'No. of Balance Query (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 76                           ROW_COUNT, 6                            RECORD_TYPE FROM SERVICE_REQUEST SR, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     SR.REQUEST_ID = MIS.REQUEST_ID(+) AND UPPER ( SUBSTR (SR.REQUEST_TEXT, 2, INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) = 'BI' AND TRUNC (REQUEST_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) UNION SELECT 'No. of Bill Payment (Till today in this month)(MBL Agent and Customer)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT) AMOUNT, 77 ROW_COUNT, 6 RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBP', 'UBPW') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') UNION SELECT 'No. of Bill Payment WestZone (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 78                          ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UWZP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') UNION SELECT 'No. of Bill Payment Paywel (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 79                          ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBP', 'UBPW', 'UWZP', 'UBPKG', 'UBPREB', 'UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = '161215000000000004' UNION SELECT 'No. of Bill Payment PBazar (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 80                          ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBP', 'UBPW', 'UWZP', 'UBPKG', 'UBPREB', 'UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = '180128000000000008' UNION SELECT 'No. of Bill Payment  Micro POS Agent (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 81                          ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBP', 'UBPW') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('170422000000000003', '170422000000000004', '170718000000000001') UNION SELECT 'No. of Bill Payment DPDC Prepaid Meter Bill Collection (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (BILL_AMOUNT) AMOUNT, 82 ROW_COUNT, 6 RECORD_TYPE FROM DPDC_PREPAID_BILL_COL_DETAIL WHERE     EXECUTE_STATUS = 'Success' AND TRUNC (EXECUTE_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Bill Payment DPDC Middleware Bill Collection (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (CAT.CAS_TRAN_AMT) AMOUNT, 82 ROW_COUNT, 6 RECORD_TYPE FROM UTILITY_MIDDLEWARE_TRANSACTION         UMT, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION  CAT WHERE     UMT.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'TOTAMT' AND UMT.IS_REVERSED = 'N' AND UMT.RESPONSE_CODE = '0001' AND UMT.RESPONSE_MSG = 'Success' AND UMT.BILL_TYPE = 'DPDC' AND TRUNC (UMT.RESPONSE_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Bill Payment DESCO Middleware Bill Collection (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (CAT.CAS_TRAN_AMT) AMOUNT, 82 ROW_COUNT, 6 RECORD_TYPE FROM UTILITY_MIDDLEWARE_TRANSACTION         UMT, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION  CAT WHERE     UMT.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'TOTAMT' AND UMT.IS_REVERSED = 'N' AND UMT.RESPONSE_CODE = '0001' AND UMT.RESPONSE_MSG = 'Success' AND UMT.BILL_TYPE = 'UBPDSPM' AND TRUNC (UMT.RESPONSE_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Bill Payment DESCO Middleware Bill Collection (Till today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (CAT.CAS_TRAN_AMT) AMOUNT, 82 ROW_COUNT, 6 RECORD_TYPE FROM UTILITY_MIDDLEWARE_TRANSACTION         UMT, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION  CAT WHERE     UMT.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'TOTAMT' AND UMT.IS_REVERSED = 'N' AND UMT.RESPONSE_CODE = '0001' AND UMT.RESPONSE_MSG = 'Success' AND UMT.BILL_TYPE = 'UBPDSPM' AND TRUNC (UMT.RESPONSE_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Bill Payment DESCO Prepaid Meter Bill Collection (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT) AMOUNT, 83 ROW_COUNT, 6 RECORD_TYPE FROM UTILITY_TRANSACTION UT WHERE     SERVICE IN ('UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) UNION SELECT 'No. of Bill Payment BREB Bill Collection (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT) AMOUNT, 83 ROW_COUNT, 6 RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBPREB') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006', '210111000000000001') UNION SELECT 'No. of Bill Payment KGDCL (Till today in this month)(MBL Agent and Customer)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT) AMOUNT, 81 ROW_COUNT, 6 RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBPKG') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') UNION SELECT 'No. of Statement Query (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 82                           ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     SERVICE_CODE IN ('QT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Merchant Payment (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 83                           ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL WHERE     SERVICE_CODE IN ('MP') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID NOT IN ('180128000000000003') AND AL.ACCNT_NO <> '018859971001' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Merchant Payment OM (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 84                           ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL WHERE     SERVICE_CODE IN ('MP') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND (   AL.ACCNT_RANK_ID IN ('180128000000000003') OR AL.ACCNT_NO = '018859971001') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Institute Payment (Till today in this month)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 85                           ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     SERVICE_CODE IN ('UIFPS') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  , 'month') AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Total:' PARTICULARS, TO_NUMBER ('') COUNT_TOTAL, TO_NUMBER ('') AMOUNT, 86     ROW_COUNT, 6      RECORD_TYPE FROM DUAL UNION SELECT 'No. of Cash In from 3rd Party Agent point (today)' PARTICULARS, COUNT (*), SUM (TRANSACTION_AMOUNT)     AMOUNT, 87                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE = 'CN' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND ACCNT_RANK_ID = '120519000000000005' UNION SELECT 'No. of Cash Out from 3rd Party Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 88                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('CCT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND AL.ACCNT_RANK_ID = '120519000000000005' UNION SELECT 'No. of Salary Withdrawal  from 3rd Party Agent (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 89                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('SW') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND AL.ACCNT_RANK_ID = '120519000000000005' UNION SELECT 'No. of Salary Withdrawal  from MBL Branch (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 90                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('SW') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND AL.ACCNT_RANK_ID = '120519000000000002' UNION SELECT 'No. of Cash In from GP Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 91                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('CN') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND ACCNT_RANK_ID = '140410000000000004' UNION SELECT 'No. of Cash Out from GP Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 92                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('CCT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND AL.ACCNT_RANK_ID = '140514000000000001' UNION SELECT 'No. of Salary Withdrawal from GP Agent (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 93                           ROW_COUNT, 6                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE = 'SW' AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND AL.ACCNT_RANK_ID = '140514000000000001' UNION SELECT 'No. of Cash In from Robi Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 94                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('CN') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND ACCNT_RANK_ID = '130922000000000004' UNION SELECT 'No. of Cash In from Paywel Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 95                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('CN') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND ACCNT_RANK_ID = '161215000000000004' UNION SELECT 'No. of Cash In from PBazar Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 96                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('CN') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND ACCNT_RANK_ID = '180128000000000008' UNION SELECT 'No. of Cash Out from Robi Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 97                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('CCT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND AL.ACCNT_RANK_ID = '130922000000000004' UNION SELECT 'No. of Cash Out from Paywell Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 98                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('CCT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND AL.ACCNT_RANK_ID = '161215000000000004' UNION SELECT 'No. of Cash Out from PBazar Agent point (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 99                           ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('CCT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND AL.ACCNT_RANK_ID = '180128000000000008' UNION SELECT 'No. of Salary Withdrawal from Robi Agent (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 100                          ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.RECEPENT_PARTY AND SERVICE_CODE IN ('SW') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND AL.ACCNT_RANK_ID = '130922000000000004' UNION SELECT 'No. of Bank Deposit from 3rd Party Agent AND Micro Pos Agent (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT) AMOUNT, 101 ROW_COUNT, 7 RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('BD') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND ACCNT_RANK_ID IN ('120519000000000005', '170422000000000003') UNION SELECT 'No. of Bank Deposit by MYCash Customer (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 102                          ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('BD') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND ACCNT_RANK_ID = '120519000000000006' UNION SELECT 'No. of Send Money (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 103                          ROW_COUNT, 7                            RECORD_TYPE FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     AL.ACCNT_NO = MIS.REQUEST_PARTY || 1 AND SERVICE_CODE IN ('FT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND ACCNT_RANK_ID = '120519000000000006' UNION SELECT 'No. of Top-Up (today)' PARTICULARS, COUNT (DISTINCT TOPUP_TRAN_ID) COUNT_TOTAL, SUM (TRAN_AMOUNT)     AMOUNT, 104                   ROW_COUNT, 7                     RECORD_TYPE FROM TOPUP_TRANSACTION WHERE     ALL_FINAL_STATUS = 'S' AND TRUNC (TRAN_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) UNION SELECT 'No. of Business Collection (Today)' PARTICULARS, COUNT (DISTINCT REQUEST_ID) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 105                          ROW_COUNT, 7                            RECORD_TYPE FROM (  SELECT DISTINCT THA.DEL_ACCNT_NO            DIS_ACC_NO, CLDIS.CLINT_NAME            DIS_NAME, CLDIS.CLINT_ADDRESS1        DIS_ADDR, MTDIS.THANA_NAME            DIS_THANA, MDDIS.DISTRICT_NAME         DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT     TRANSACTION_AMOUNT, TMIS.REQUEST_ID FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST                ALDSE, ACCOUNT_LIST                ALCOR, TEMP_HIERARCHY_LIST_ALL     THA, ACCOUNT_LIST                ALDIS, CLIENT_LIST                 CLDIS, MANAGE_THANA                MTDIS, MANAGE_DISTRICT             MDDIS WHERE     TMIS.SERVICE_CODE = 'FM' AND TRUNC (TMIS.TRANSACTION_DATE) BETWEEN TRUNC ( TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC ( TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TMIS.REQUEST_PARTY || '1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004', '180128000000000007') AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002', '180416000000000001') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.THANA_ID = MDDIS.DISTRICT_ID(+) ORDER BY THA.DEL_ACCNT_NO ASC) TEMP UNION SELECT 'Business Collection(National life insurance)(today) ' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 106                          ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL WHERE     TM.SERVICE_CODE = 'FM' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TM.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('181219000000000002') UNION SELECT 'Business Collection(Provita)(today) ' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 106                          ROW_COUNT, 6                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL WHERE     TM.SERVICE_CODE = 'FM' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TM.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('190519000000000001') UNION SELECT 'No. of Balance Query (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 106                          ROW_COUNT, 7                            RECORD_TYPE FROM SERVICE_REQUEST SR, TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     SR.REQUEST_ID = MIS.REQUEST_ID(+) AND UPPER ( SUBSTR (SR.REQUEST_TEXT, 2, INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) = 'BI' AND TRUNC (REQUEST_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) UNION SELECT 'No. of Statement Query (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 107                          ROW_COUNT, 7                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     SERVICE_CODE IN ('QT') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) UNION SELECT 'No. of Bill Payment (Today)(Mbl Agent and Customer)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 108                         ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBP', 'UBPW') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') UNION SELECT 'No. of Bill Payment WestZone (Today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 109                         ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UWZP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') UNION SELECT 'No. of Bill Payment Paywel (Today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 110                         ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBP', 'UBPW', 'UWZP', 'UBPKG', 'UBPREB', 'UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = '161215000000000004' UNION SELECT 'No. of Bill Payment PBazar (Today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 111                         ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBP', 'UBPW', 'UWZP', 'UBPKG', 'UBPREB', 'UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = '180128000000000008' UNION SELECT 'No. of Bank Bill Payment  Micro POS Agent (Today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 112                         ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBP', 'UBPW', 'UWZP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('170422000000000003', '170422000000000004', '170718000000000001') UNION SELECT 'No. of Bill Payment DPDC Prepaid Meter Bill Collection (Today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (BILL_AMOUNT)     AMOUNT, 112                   ROW_COUNT, 6                     RECORD_TYPE FROM DPDC_PREPAID_BILL_COL_DETAIL WHERE     EXECUTE_STATUS = 'Success' AND TRUNC (EXECUTE_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Bill Payment DPDC Middleware Bill Collection (Today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (CAT.CAS_TRAN_AMT)     AMOUNT, 112                        ROW_COUNT, 6                          RECORD_TYPE FROM UTILITY_MIDDLEWARE_TRANSACTION         UMT, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION  CAT WHERE     UMT.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'TOTAMT' AND UMT.IS_REVERSED = 'N' AND UMT.RESPONSE_CODE = '0001' AND UMT.RESPONSE_MSG = 'Success' AND TRUNC (UMT.RESPONSE_TIME) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Bill Payment DESCO Prepaid Meter Bill Collection (Today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 113                         ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT WHERE     SERVICE IN ('UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Bill Payment BREB Bill Collection (Today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 113                         ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBPREB') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') UNION SELECT 'No. of Bill Payment KGDCL (today)(MBL Agent and Customer)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TOTAL_BILL_AMOUNT)     AMOUNT, 113                         ROW_COUNT, 6                           RECORD_TYPE FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL WHERE     SERVICE IN ('UBPKG') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') UNION SELECT 'No. of Merchant Payment (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 113                          ROW_COUNT, 7                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL WHERE     SERVICE_CODE IN ('MP') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID NOT IN ('180128000000000003') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Merchant Payment OM (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 114                          ROW_COUNT, 7                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT MIS, ACCOUNT_LIST AL WHERE     SERVICE_CODE IN ('MP') AND MIS.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('180128000000000003') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'No. of Institute Payment (today)' PARTICULARS, COUNT (*) COUNT_TOTAL, SUM (TRANSACTION_AMOUNT)     AMOUNT, 115                          ROW_COUNT, 7                            RECORD_TYPE FROM TEMP_MIS_TRANSACTIONS_REPORT MIS WHERE     SERVICE_CODE IN ('UIFPS') AND TRUNC (TRANSACTION_DATE) BETWEEN TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')) AND TRUNC (TO_DATE('" + vDate + "','DD-MM-YYYY')  ) UNION SELECT 'Total:' PARTICULARS, TO_NUMBER ('') COUNT_TOTAL, TO_NUMBER ('') AMOUNT, 116     ROW_COUNT, 7       RECORD_TYPE FROM DUAL UNION SELECT 'Deposit' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 117     ROW_COUNT, 8       RECORD_TYPE FROM DUAL UNION SELECT 'Particulars' PARTICULARS, TO_NUMBER ('0') COUNT_TOTAL, TO_NUMBER ('0') AMOUNT, 118     ROW_COUNT, 8       RECORD_TYPE FROM DUAL UNION SELECT 'MYCash Customer Deposit (till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205053000') AMOUNT, 119 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'MYCash Distributor (till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205051000') AMOUNT, 120 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'MYCash DSE (till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205052000') AMOUNT, 121 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'MYCash Agent (till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205052500') AMOUNT, 122 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'MBL Branch (till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205050500') AMOUNT, 123 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'Robi Distributor (till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205051100') AMOUNT, 124 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'Robi DSE (till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205052100') AMOUNT, 125 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'Robi Agent (till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205052600') AMOUNT, 126 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'GP Mother Account (Till today)' PARTICULARS, 0 COUNT_TOTAL, BDMIT_ERP_101.FUNC_GET_GL_ACCOUNT_BALANCE ('205053505') AMOUNT, 127 ROW_COUNT, 8 RECORD_TYPE FROM DUAL UNION SELECT 'MyDPS Account Balance(today)' PARTICULARS, 0 COUNT_TOTAL, CAB.CAS_ACCNT_BALANCE + 3300  AMOUNT, 128     ROW_COUNT, 8       RECORD_TYPE FROM BDMIT_ERP_101.CAS_ACCOUNT_BALANCE  CAB, BDMIT_ERP_101.CAS_ACCOUNT_LIST     CAL WHERE     CAL.CAS_ACC_ID = CAB.CAS_ACC_ID AND CAL.CAS_ACC_NO = '00000000000045' UNION SELECT 'Total:' PARTICULARS, TO_NUMBER ('') COUNT_TOTAL, TO_NUMBER ('') AMOUNT, 130     ROW_COUNT, 8       RECORD_TYPE FROM DUAL ORDER BY ROW_COUNT";


                string strHTML = "", fileName = "";
                lblMsg.Text = "";
                clsServiceHandler objServiceHandler = new clsServiceHandler();
                DataSet dtsAccount = new DataSet();
                fileName = "Daily_MIS_Interim_Report_By_Date";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strQuery);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MYCash </h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily MIS Report By Date (" + vDate + ")</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' ></td>";
                strHTML = strHTML + "<td valign='middle' >Number</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "</tr>";
                double countTotal = 0;
                double amount = 0;

                //btnInterimMISReport.Enabled = true;
                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        //if (prow["PARTICULARS"].ToString() != "Particulars")
                        //{
                        //    strHTML = strHTML + " <td valign='middle' style='font-size:14px;font-weight:bold;'> " + prow["PARTICULARS"].ToString() + " </td>";
                        //}

                        if (prow["COUNT_TOTAL"].ToString() == "")
                        {
                            countTotal = 0;
                        }
                        else
                        {
                            countTotal = Convert.ToDouble(prow["COUNT_TOTAL"]);
                        }
                        if (prow["AMOUNT"].ToString() == "")
                        {
                            amount = 0;
                        }
                        else
                        {
                            amount = Convert.ToDouble(prow["AMOUNT"]);
                        }
                        if (prow["PARTICULARS"].ToString() != "Total:" && prow["PARTICULARS"].ToString() != "Particulars")
                        {
                            strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                            strHTML = strHTML + " <td > " + prow["PARTICULARS"].ToString() + " </td>";
                            if (prow["COUNT_TOTAL"].ToString() != "0")
                            {
                                strHTML = strHTML + " <td align='right'> '" + prow["COUNT_TOTAL"].ToString() + " </td>";
                            }
                            else
                            {
                                strHTML = strHTML + " <td > </td>";
                            }
                            if (prow["AMOUNT"].ToString() != "0")
                            {
                                strHTML = strHTML + " <td align='right'> '" + prow["AMOUNT"].ToString() + " </td>";
                            }
                            else
                            {
                                strHTML = strHTML + " <td > </td>";
                            }
                            strHTML = strHTML + " </tr> ";
                            SerialNo = SerialNo + 1;

                            sumOfNumber = sumOfNumber + Convert.ToDouble(countTotal);
                            sumOfAmount = sumOfAmount + Convert.ToDouble(amount);
                        }
                        else
                        {
                            strHTML = strHTML + " <tr>";
                            strHTML = strHTML + " <td > " + "" + " </td>";
                            strHTML = strHTML + " <td valign='middle' style='font-size:14px;font-weight:bold;'> " + prow["PARTICULARS"].ToString() + " </td>";
                            strHTML = strHTML + " <td > " + sumOfNumber.ToString() + " </td>";
                            strHTML = strHTML + " <td > " + sumOfAmount.ToString() + " </td>";
                            strHTML = strHTML + " </tr>";
                            sumOfNumber = 0;
                            sumOfAmount = 0;
                        }

                    }



                }

                strHTML = strHTML + " </table>";

                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + " <tr>";
                strHTML = strHTML + " <td COLSPAN=4 > " + "DEPOSIT CORPORATE HO(DISTRIBUTOR): " + HDFDistributorEndBal.Value.ToString() + " </td>";
                strHTML = strHTML + " </tr>";

                strHTML = strHTML + " <tr>";
                strHTML = strHTML + " <td COLSPAN=4 > " + "DEPOSIT CORPORATE AGENT: " + HDFAgentEndBal.Value.ToString() + " </td>";
                strHTML = strHTML + " </tr>";

                strHTML = strHTML + " <tr>";
                strHTML = strHTML + " <td COLSPAN=4 > " + "Last Month's Salary Disbursement Amount: " + hdfSalDisbAmt.Value.ToString() + " </td>";
                strHTML = strHTML + " </tr>";

                strHTML = strHTML + " </table>";


                try
                {
                    btnInterimMISReport.Enabled = true;
                    //btnInterimMISReport.Attributes.Add("OnClientClick", "this.enabled = true; _doPostBack('btnInterimMISReport',''); return true;");
                }
                catch (Exception exception)
                {
                    exception.Message.ToString();
                }


                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
            }

            catch (Exception exception)
            {
                lblMsg.Text = exception.Message;
            }

        }
        #endregion
}
