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

public partial class MIS_frmMBL_Various_MIS_Report_2 : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;
        dtpInterimDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));
        
        if (rbtOption.SelectedValue == "0")
        {
            dtpRankFDate.Enabled = false;
            dtpRankToDate.Enabled = false;
            ddlOperator.Enabled = false;
        }

        // for operator wise
        else if (rbtOption.SelectedValue == "1")
        {
            dtpRankFDate.Enabled = false;
            dtpRankToDate.Enabled = false;
            ddlOperator.Enabled = true; 
        }

        // for date range
        else if (rbtOption.SelectedValue == "2")
        {
            dtpRankFDate.Enabled = true;
            dtpRankToDate.Enabled = true;
            ddlOperator.Enabled = false;
        }

        // for operator and date range (both)
        else if (rbtOption.SelectedValue == "3")
        {
            dtpRankFDate.Enabled = true;
            dtpRankToDate.Enabled = true;
            ddlOperator.Enabled = true;
        }

        else
        {
           // 
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

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    #region distributor wise agent Registration Report
    protected void btnDisAgentReg_Click(object sender, EventArgs e)
    {
        string strSql = "";
        try
        {
            strSql =   " SELECT  DEL_ACCNT_NO DIS_ACC,CLINT_NAME DIS_NAME,CLINT_ADDRESS1 DIS_ADDRESS,"
                     + " MT.THANA_NAME DIS_THANA,MD.DISTRICT_NAME DIS_DISTRICT,COUNT (*) TOTAL "
                     + " FROM ACCOUNT_SERIAL_DETAIL ASD,TEMP_HIERARCHY_LIST_ALL HL,ACCOUNT_LIST AL, "
                     + " CLIENT_LIST CL,MANAGE_THANA MT,MANAGE_DISTRICT MD "
                     + " WHERE CUSTOMER_MOBILE_NO= '+88'||SUBSTR(A_ACCNT_NO,1,11) AND "
                     + " DEL_ACCNT_NO=AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID "
                     + " AND TRUNC(ACTIVATION_DATE) BETWEEN '" + dtpDisAgFrDate.DateString + "' AND '" + dtpDisAgToDate.DateString + "'  AND "
                     + " CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) "
                     + " GROUP BY AGENT_MOBILE_NO,DEL_ACCNT_NO,CLINT_NAME,CLINT_ADDRESS1, "
                     + " MT.THANA_NAME,MD.DISTRICT_NAME ";


          /*  strSql = " SELECT DEL_ACCNT_NO DIS_ACC, CLINT_NAME DIS_NAME, CLINT_ADDRESS1 DIS_ADDRESS, MD.DISTRICT_NAME, "
                     + " AGENT_MOBILE_NO, COUNT(*) TOTAL FROM ACCOUNT_SERIAL_DETAIL ASD,TEMP_HIERARCHY_LIST_ALL HL, "
                     + " ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT,MANAGE_DISTRICT MD "
                     + " WHERE AGENT_MOBILE_NO= '+88'||SUBSTR(A_ACCNT_NO,1,11) AND DEL_ACCNT_NO=ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID "
                     + " AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) "
                     + " AND TRUNC(ACTIVATION_DATE) BETWEEN '"+dtpDisAgFrDate.DateString+"' AND '"+dtpDisAgToDate.DateString+"' "
                     + " GROUP BY AGENT_MOBILE_NO,DEL_ACCNT_NO,CLINT_NAME,CLINT_ADDRESS1,DISTRICT_NAME "
                     + " ORDER BY TOTAL DESC";  */

            clsServiceHandler objServiceHandler = new clsServiceHandler();
            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Dis_wise_Agnt_Reg_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Distributor wise Agent Registration Report (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisAgFrDate.DateString + "' To '" + dtpDisAgToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            //strHTML = strHTML + "<td valign='middle' >Agent MyCash A/C No</td>";
            strHTML = strHTML + "<td valign='middle' >Total Count</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ACC"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > '" + prow["AGENT_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL"].ToString() + " </td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Dis_wise_Agnt_Reg_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");


        }
        catch (Exception exception)
        {
            lblMsg.Text = exception.Message.ToString();
        }
    }

    #endregion

    #region DistrictWise Sales Report

    protected void btnDistrictWiseSales_Click(object sender, EventArgs e)
    {
        string strSql = "";
        double totalRG = 0;
        double totalCNCount = 0;
        double totalCNAmount = 0;
        double totalCCTCount = 0;
        double totalCCTAmount = 0;

        try
        {
            #region Old query

            /*

            strSql = " SELECT DISTINCT MD.DISTRICT_ID, MD.DISTRICT_NAME DISTRICT_NAME, "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_GET_TOT_RG_DIST_AGENT(MD.DISTRICT_ID,'" + dtpDistrictFDate.DateString + "','" + dtpDistrictToDate.DateString + "') TOTAL_RG, "
               // + " SUM(APSNG101.PKG_MIS_REPORTS.FUNC_GET_TOT_RG_DIST_AGENT(AL.ACCNT_NO,'" + dtpDistrictFDate.DateString + "','" + dtpDistrictToDate.DateString + "')) TOTAL_RG, "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_GET_COUNT_CN_DIST_AGENT ( MD.DISTRICT_ID ,'" + dtpDistrictFDate.DateString + "','" + dtpDistrictToDate.DateString + "') TOTAL_CN, "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_GET_SUM_CN_DIST_AGENT ( MD.DISTRICT_ID ,'" + dtpDistrictFDate.DateString + "','" + dtpDistrictToDate.DateString + "') SUM_CN,"
                + " APSNG101.PKG_MIS_REPORTS.FUNC_GET_CNT_CCT_DIST_AGENT(MD.DISTRICT_ID ,'" + dtpDistrictFDate.DateString + "','" + dtpDistrictToDate.DateString + "') TOTAL_CCT, "
                + " APSNG101.PKG_MIS_REPORTS.FUNC_GET_SUM_CCT_DIST_AGENT(MD.DISTRICT_ID ,'" + dtpDistrictFDate.DateString + "','" + dtpDistrictToDate.DateString + "') SUM_CCT "
                + " FROM CLIENT_LIST CL,MANAGE_THANA MT,MANAGE_DISTRICT MD,ACCOUNT_LIST AL "
                + " WHERE CL.THANA_ID=MT.THANA_ID AND MT.DISTRICT_ID=MD.DISTRICT_ID AND CL.CLINT_ID=AL.CLINT_ID AND ACCNT_RANK_ID='120519000000000005' "
                + " GROUP BY MD.DISTRICT_ID, MD.DISTRICT_NAME ORDER BY MD.DISTRICT_NAME";

             */

            #endregion

            string strProcedure = "APSNG101.PRO_SALES_REPORT('" + dtpDistrictFDate.DateString + "','" + dtpDistrictToDate.DateString + "')";
            string strMsg = objServiceHandler.ExecuteProcedure(strProcedure);

            try
            {
                if (strMsg != "")
                {
                    strSql = " SELECT DISTRICT_NAME, NVL(SUM(TOTAL_RG_COUNT),0)TOTAL_RG_COUNT, NVL(SUM(TOTAL_CN_COUNT),0)TOTAL_CN_COUNT, "
                             + " NVL(SUM(TOTAL_CN_SUM),0)TOTAL_CN_SUM , NVL(SUM(TOTAL_CCT_COUNT),0)TOTAL_CCT_COUNT, "
                             + " NVL(SUM(TOTAL_CCT_SUM),0) TOTAL_CCT_SUM FROM TEMP_SALES_REPORT "
                             + " GROUP BY DISTRICT_NAME ORDER BY DISTRICT_NAME";
                }

                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "Dist_wise_Mycash_Sales_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>District wise MYCash Sales Report</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDistrictFDate.DateString + "' To '" + dtpDistrictToDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >District</td>";
                strHTML = strHTML + "<td valign='middle' >Registration Count </td>";
                strHTML = strHTML + "<td valign='middle' >Cash-In Count </td>";
                strHTML = strHTML + "<td valign='middle' >Cash-In Amount (In BDT)</td>";
                strHTML = strHTML + "<td valign='middle' >Cash-Out Count</td>";
                strHTML = strHTML + "<td valign='middle' >Cash-Out Amount (In BDT)</td>";
                strHTML = strHTML + "</tr>";


                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG_COUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN_COUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN_SUM"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT_COUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT_SUM"].ToString() + "</td>";

                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;


                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG_COUNT"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN_COUNT"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["TOTAL_CN_SUM"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT_COUNT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["TOTAL_CCT_SUM"].ToString());
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + totalRG.ToString() + " </td>";
                strHTML = strHTML + " <td > " + totalCNCount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + totalCNAmount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + totalCCTCount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + totalCCTAmount.ToString() + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "Dist_wise_Mycash_Sales_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            #region old code

            /*
            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Dist_wise_Mycash_Sales_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>District wise MYCash Sales Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDistrictFDate.DateString + "' To '" + dtpDistrictToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >Registration Count </td>";
            strHTML = strHTML + "<td valign='middle' >Cash-In Count </td>";
            strHTML = strHTML + "<td valign='middle' >Cash-In Amount (In BDT)</td>";
            strHTML = strHTML + "<td valign='middle' >Cash-Out Count</td>";
            strHTML = strHTML + "<td valign='middle' >Cash-Out Amount (In BDT)</td>";
            strHTML = strHTML + "</tr>";

            string strZero = "0";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["TOTAL_RG"].ToString() != "0" && prow["TOTAL_CN"].ToString() != "0" && prow["TOTAL_CCT"].ToString() != "0" )
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CN"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CCT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else if (prow["TOTAL_RG"].ToString() == "0" && prow["TOTAL_CN"].ToString() == "0" && prow["TOTAL_CCT"].ToString() == "0" )
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }


                    else if (prow["TOTAL_RG"].ToString() == "0" && prow["TOTAL_CN"].ToString() == "0")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CCT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else if (prow["TOTAL_RG"].ToString() == "0" && prow["TOTAL_CCT"].ToString() == "0")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CN"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else if (prow["TOTAL_CN"].ToString() == "0" && prow["TOTAL_CCT"].ToString() == "0")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else if (prow["TOTAL_CN"].ToString() == "0")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CCT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else if (prow["TOTAL_CCT"].ToString() == "0")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CN"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else if (prow["TOTAL_RG"].ToString() == "0")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CN"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CCT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else
                    {
                        //
                    }

                    */

            #endregion

            #region old condition 1


            /*
                    
                    if (prow["TOTAL_RG"].ToString() == "0" && prow["TOTAL_CN"].ToString() == "0"
                        && prow["SUM_CN"].ToString() == "0" && prow["TOTAL_CCT"].ToString() == "0"
                        && prow["SUM_CCT"].ToString() == "0")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());

                    }

                    else if (prow["TOTAL_CN"].ToString() == "0" && prow["TOTAL_CCT"].ToString() == "0")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else if (prow["SUM_CCT"].ToString() == "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CN"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + strZero.ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }

                    else if (prow["SUM_CN"].ToString() == "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > ' " + strZero.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CCT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());

                    }

                    else
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_RG"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CN"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOTAL_CCT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SUM_CCT"].ToString() + " </td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        totalRG = totalRG + Convert.ToDouble(prow["TOTAL_RG"].ToString());
                        totalCNCount = totalCNCount + Convert.ToDouble(prow["TOTAL_CN"].ToString());
                        totalCNAmount = totalCNAmount + Convert.ToDouble(prow["SUM_CN"].ToString());
                        totalCCTCount = totalCCTCount + Convert.ToDouble(prow["TOTAL_CCT"].ToString());
                        totalCCTAmount = totalCCTAmount + Convert.ToDouble(prow["SUM_CCT"].ToString());
                    }
                     
                     */

            #endregion
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    #region distributor Agent wise transaction performance Report
    protected void btnDisAgentPerformCount_Click(object sender, EventArgs e)
    {
        string strSql = "";

    /*    SELECT   DISTINCT
        DEL_ACCNT_NO DIS_ACCOUNT, CL.CLINT_NAME DIS_NAME, CLINT_ADDRESS1 DIS_ADDR, MT.THANA_NAME DIS_THANA, DISTRICT_NAME DIST_NAME,
        PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_CUSTOMER_RG ( DEL_ACCNT_NO, '1-JUN-2014','30-JUN-2014' ) TOTAL_CUST_RG,
        PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_AGENT_AQUI (DEL_ACCNT_NO, '1-JUN-2014','30-JUN-2014') TOTAL_AGENT_AQUISOTION,
        PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_SRV_INITITE ( DEL_ACCNT_NO, '1-JUN-2014','30-JUN-2014', 'CN' ) TOTAL_CASIN, 
        PKG_MIS_REPORTS.FUNC_DIS_WISE_TOTAL_SRV_RECV ( DEL_ACCNT_NO,  '1-JUN-2014','30-JUN-2014','CCT,SW') TOTAL_CASHOUT,
        PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_SRV_FM (DEL_ACCNT_NO,  '1-JUN-2014','30-JUN-2014') TOTAL_FM,
        PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_SRV_INITITE ( DEL_ACCNT_NO, '1-JUN-2014','30-JUN-2014', 'BD' ) TOTAL_BD_BY_AG
        FROM  TEMP_HIERARCHY_LIST_ALL HL, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD
        WHERE HL.DEL_ACCNT_ID = AL.ACCNT_ID AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) 
        AND MT.DISTRICT_ID = MD.DISTRICT_ID(+)
        --AND SA_ACCNT_NO=CALD.CAS_ACC_NO  --AND DEL_ACCNT_NO='019888025961'
        GROUP BY DEL_ACCNT_NO, CL.CLINT_NAME, CLINT_ADDRESS1, MT.THANA_NAME, DISTRICT_NAME
        ORDER BY DISTRICT_NAME;  */

        try
        {

            strSql = " SELECT DISTINCT DEL_ACCNT_NO DIS_ACCOUNT, CL.CLINT_NAME DIS_NAME, CLINT_ADDRESS1 DIS_ADDR,"
                   + " MT.THANA_NAME DIS_THANA, DISTRICT_NAME DIST_NAME, "
                   + " PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_CUSTOMER_RG ( DEL_ACCNT_NO, '" + dtpDisAgentFromDate.DateString + "','" + dtpDisAgentToDate.DateString + "' ) TOTAL_CUST_RG,"
                   + " PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_AGENT_AQUI (DEL_ACCNT_NO, '" + dtpDisAgentFromDate.DateString + "','" + dtpDisAgentToDate.DateString + "') TOTAL_AGENT_AQUISITION,"
                   + " PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_SRV_INITITE ( DEL_ACCNT_NO, '" + dtpDisAgentFromDate.DateString + "','" + dtpDisAgentToDate.DateString + "', 'CN' ) + "
                   + " PKG_MIS_REPORTS.FUNC_DIS_WISE_TOTAL_SRV_RECV ( DEL_ACCNT_NO,  '" + dtpDisAgentFromDate.DateString + "','" + dtpDisAgentToDate.DateString + "','CCT,SW') + "
                   + " PKG_MIS_REPORTS.FUNC_DIS_WISE_TOT_SRV_INITITE ( DEL_ACCNT_NO, '" + dtpDisAgentFromDate.DateString + "','" + dtpDisAgentToDate.DateString + "', 'BD' ) TOTAL_TRAN "
                   + " FROM  TEMP_HIERARCHY_LIST_ALL HL, ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD "
                   + " WHERE HL.DEL_ACCNT_ID = AL.ACCNT_ID AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) "
                   + " AND MT.DISTRICT_ID = MD.DISTRICT_ID(+) GROUP BY DEL_ACCNT_NO, CL.CLINT_NAME, CLINT_ADDRESS1, MT.THANA_NAME, DISTRICT_NAME "
                   + " ORDER BY DISTRICT_NAME";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Dist_wise_Agent_Trx_Perfrm_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Agent wise Transaction Performance Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisAgentFromDate.DateString + "' To '" + dtpDisAgentToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana (In BDT)</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Registered</td>";
            strHTML = strHTML + "<td valign='middle' >Total Agent Acquired</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction(CN,CCT,SW,BD) Count </td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ACCOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIST_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_CUST_RG"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_AGENT_AQUISITION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_TRAN"].ToString() + "</td>";

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

            SaveAuditInfo("Preview", "Dist_wise_Agent_Trx_Perfrm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }


    }
    #endregion

    #region business collection report 1

    protected void btnBusinessRpt1_Click(object sender, EventArgs e)
    {
        string strSql = "";

        try
        {
            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIST.CLINT_NAME DIST_NAME, CLDIST.CLINT_ADDRESS1 DIST_ADDRESS, "
                 + " MT.THANA_NAME DIST_THANA, MD.DISTRICT_NAME DIST_DISTRICT, TMIS.REQUEST_ID TRX_ID, TRUNC(TMIS.TRANSACTION_DATE) TRX_DATE, "
                 + " TMIS.SERVICE_CODE, TMIS.REQUEST_PARTY MBL_DSE_ACC_NO, CLDSE.CLINT_NAME DSE_NAME, "
                 + " TMIS.RECEPENT_PARTY CORP_AGENT_NO, CLCORP.CLINT_NAME CORP_AGENT_NAME, TMIS.TRANSACTION_AMOUNT TRX_AMOUNT    "
                 + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCORP, "
                 + " CLIENT_LIST CLDSE, CLIENT_LIST CLCORP, ACCOUNT_LIST ALDIST, CLIENT_LIST CLDIST, MANAGE_THANA MT, MANAGE_DISTRICT MD "
                 + " WHERE TMIS.SERVICE_CODE = 'FM' AND TMIS.REQUEST_PARTY||1 = THA.SA_ACCNT_NO AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO "
                 + " AND ALDSE.ACCNT_RANK_ID in ('120519000000000004','200813000000000003') AND ALDSE.CLINT_ID = CLDSE.CLINT_ID "
                 + " AND TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO AND ALCORP.ACCNT_RANK_ID IN('140917000000000004','210930000000000001') "
                 + " AND ALCORP.CLINT_ID = CLCORP.CLINT_ID AND THA.DEL_ACCNT_NO = ALDIST.ACCNT_NO AND ALDIST.CLINT_ID = CLDIST.CLINT_ID  "
                 + " AND CLDIST.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID(+) "
                 + " AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBusRpt1FDate.DateString + "' AND '" + dtpBusRpt1ToDate.DateString + "' "
                 + " ORDER BY TRUNC(TMIS.TRANSACTION_DATE) DESC";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Business_Collection_Rpt1";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Business Collection Report1</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBusRpt1FDate.DateString + "' To '" + dtpBusRpt1ToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Date</td>";
            strHTML = strHTML + "<td valign='middle' >Trx ID </td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor A/C No </td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Thana</td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >MBL DSE MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' >MBL DSE MYCash A/C Name </td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Agent MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Agent MYCash Name</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIST_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIST_ADDRESS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIST_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIST_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["MBL_DSE_ACC_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_AGENT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_AGENT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMOUNT"].ToString() + "</td>";

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

            SaveAuditInfo("Preview", "Business_Collection_Rpt1");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
        
    }

    #endregion

    #region business collection report 2
    
    protected void btnBussCollecRpt2_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT DISTINCT TMIS.REQUEST_ID TRX_ID, TRUNC(TMIS.TRANSACTION_DATE) TRX_DATE, TMIS.SERVICE_CODE, TMIS.TRANSACTION_AMOUNT TRX_AMOUNT, "
                   + " TMIS.REQUEST_PARTY AGENT_ACC_NO,ALAGT.ACCNT_RANK_ID, CLAGT.CLINT_NAME AGENT_NAME, TMIS.RECEPENT_PARTY D_ACC_NO, CLD.CLINT_NAME D_NAME  "
                   + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALAGT, CLIENT_LIST CLAGT,  ACCOUNT_LIST ALD, CLIENT_LIST CLD "
                   + " WHERE TMIS.SERVICE_CODE = 'FM' AND TMIS.REQUEST_PARTY||1 = ALAGT.ACCNT_NO AND ALAGT.CLINT_ID = CLAGT.CLINT_ID "
                   + " AND ALAGT.ACCNT_RANK_ID = '140917000000000004' AND TMIS.RECEPENT_PARTY = ALD.ACCNT_NO AND ALD.ACCNT_RANK_ID IN ('140917000000000002','140917000000000003') "
                   + " AND ALD.CLINT_ID = CLD.CLINT_ID AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBusCol2FDate.DateString + "' AND '" + dtpBusCol2ToDate.DateString + "' "
                   + " ORDER BY TRUNC(TMIS.TRANSACTION_DATE) DESC";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Business_Collection_Rpt2";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Business Collection Report2</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBusCol2FDate.DateString + "' To '" + dtpBusCol2ToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Date</td>";
            strHTML = strHTML + "<td valign='middle' >Trx ID </td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Agent MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Agent MYCash A/C Name</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Distributor/DSE  MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate Distributor MYCash A/C Name</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_ACC_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["D_ACC_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["D_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMOUNT"].ToString() + "</td>";

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

            SaveAuditInfo("Preview", "Business_Collection_Rpt2");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        
        catch (Exception exception)
        {
            exception.Message.ToString();
        }

        


    }

    #endregion

    #region Distributor Wise Customer Registration Count
    protected void btnDistWSCustRG_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT  TRAN2.DEL_ACCNT_NO DIS_ACC_NO,Cl.CLINT_NAME DIS_NAME,MT.THANA_NAME THANA_NAME,MD.DISTRICT_NAME DISTRICT,"
                     + " CL.CLINT_ADDRESS1 DIST_ADDRESS,TRAN2.REG_CNT REG_COUNT FROM (SELECT TRAN.DEL_ACCNT_NO,SUM(CT) REG_CNT "
                     + " FROM (SELECT THL.DEL_ACCNT_NO,ASD.AGENT_MOBILE_NO, "
                     + " COUNT (ASD.CUSTOMER_MOBILE_NO) CT FROM ACCOUNT_SERIAL_DETAIL ASD, "
                     + " TEMP_HIERARCHY_LIST_ALL THL WHERE TRUNC(ASD.ACTIVATION_DATE) "
                     + " BETWEEN '" + dtpDWCustRegFDate.DateString + "' AND '" + dtpDWCustRegToDate.DateString + "' "
                     + " AND SUBSTR(('+88'||THL.A_ACCNT_NO),0,14)=ASD.AGENT_MOBILE_NO "
                     + " GROUP BY ASD.AGENT_MOBILE_NO,THL.DEL_ACCNT_NO) TRAN "
                     + " GROUP BY TRAN.DEL_ACCNT_NO) TRAN2,CLIENT_LIST CL ,MANAGE_THANA MT,"
                     + " MANAGE_DISTRICT MD WHERE TRAN2.DEL_ACCNT_NO= SUBSTR(CL.CLINT_MOBILE,4,14)||'1' "
                     + " AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) "
                     + " ORDER BY MD.DISTRICT_NAME";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Distr_Wise_Cust_Reg_Count";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Wise Customer Registration Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDWCustRegFDate.DateString + "' To '" + dtpDWCustRegToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Thana</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >Count Of Customer Registration</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIST_ADDRESS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["THANA_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REG_COUNT"].ToString() + "</td>";
                    
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

            SaveAuditInfo("Preview", "Distr_Wise_Cust_Reg_Count_rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

#endregion

    #region Customer account approve and kyc Update Details with mobiCash Agent
    protected void btnKycUpdate_Click(object sender, EventArgs e)
    {
        string strSql = "";
        try
        {
            #region OLD QUERY
            /*

            strSql = " SELECT AL_C.ACCNT_NO CUST_NO, ASD.ACTIVATION_DATE CUST_REG_DATE, "
                     + " CL_C.VERIFIED_DATE CUST_VERIFY_DATE,  CL_C.UPDATE_DATE KYC_UPDATE_DATE, AL_AG.ACCNT_NO AGENT_NO, "
                     + " AR_AG.RANK_TITEL AGENT_RANK, THA_AG.DEL_ACCNT_NO DIS_NO, CL_DIS.CLINT_NAME DIS_NAME, "
                     + " CL_DIS.CLINT_ADDRESS1 DIS_ADDRESS, MT_DIS.THANA_NAME DIS_THANA_NAME,  "
                     + " MD_DIS.DISTRICT_NAME DIS_DISTR_NAME, AL_KYC.ACCNT_NO KYC_UPDATED_BY, "
                     + " AR_KYC.RANK_TITEL KYC_ACC_RANK, "
                     + " CL_KYC.CLINT_NAME KYC_ACC_NAME, CL_KYC.CLINT_ADDRESS1 KYC_ADDRESS,  "
                     + " MT_KYC.THANA_NAME KYC_THANA, MD_KYC.DISTRICT_NAME KYC_DIS_NAME  "
                     + " FROM ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST AL_AG, ACCOUNT_LIST AL_C, "
                     + " ACCOUNT_RANK AR_AG, CLIENT_LIST CL_C, TEMP_HIERARCHY_LIST_ALL THA_AG, "
                     + " ACCOUNT_LIST AL_DIS, CLIENT_LIST CL_DIS, MANAGE_THANA MT_DIS, "
                     + " MANAGE_DISTRICT MD_DIS, ACCOUNT_LIST AL_KYC, CLIENT_LIST CL_KYC, "
                     + " ACCOUNT_RANK AR_KYC, MANAGE_THANA MT_KYC, MANAGE_DISTRICT MD_KYC "
                     + " WHERE ASD.CUSTOMER_MOBILE_NO = '+88'||SUBSTR(AL_C.ACCNT_NO, 1,11) "
                     + " AND AL_C.CLINT_ID = CL_C.CLINT_ID AND TRUNC(CL_C.VERIFIED_DATE) "
                     + " BETWEEN '" + dtpKycFromDate.DateString + "' AND '" + dtpKycToDate.DateString + "' "
                     + " AND ASD.BANK_CODE IN ('MBL', 'GP') AND ASD.AGENT_MOBILE_NO = '+88'||SUBSTR(AL_AG.ACCNT_NO, 1,11) "
                     + " AND AL_AG.ACCNT_RANK_ID = AR_AG.ACCNT_RANK_ID AND AR_AG.ACCNT_RANK_ID IN('120519000000000005','140410000000000004')"
                     + " AND AL_AG.ACCNT_NO = THA_AG.A_ACCNT_NO(+) AND THA_AG.DEL_ACCNT_NO = AL_DIS.ACCNT_NO(+) "
                     + " AND AL_DIS.CLINT_ID = CL_DIS.CLINT_ID(+) AND CL_DIS.THANA_ID = MT_DIS.THANA_ID(+) AND MT_DIS.DISTRICT_ID = MD_DIS.DISTRICT_ID(+) "
                     + " AND CL_C.KYC_UPDATED_BY = AL_KYC.ACCNT_ID(+) AND AL_KYC.CLINT_ID = CL_KYC.CLINT_ID(+) "
                     + " AND AL_KYC.ACCNT_RANK_ID = AR_KYC.ACCNT_RANK_ID(+) AND CL_KYC.THANA_ID = MT_KYC.THANA_ID(+) "
                     + " AND MT_KYC.DISTRICT_ID = MD_KYC.DISTRICT_ID(+) ORDER BY CL_C.VERIFIED_DATE, AR_AG.RANK_TITEL ";
             */

#endregion

            strSql = " SELECT DISTINCT CAT_AG.REQUEST_ID REQ_ID, AL_CUST.ACCNT_NO CUST_ACC_NO, "
                     + " TRUNC(CL_CUST.CREATION_DATE) CUST_REG_DATE, TRUNC(CL_CUST.VERIFIED_DATE) CUST_VERIFY_DATE, "
                     + " TRUNC(CL_CUST.UPDATE_DATE) KYC_UPDATE_DATE, CL_CUST.KYC_UPDATED_BY KYC_UP_ACC_ID, "
                     + " TMIS.REQUEST_PARTY||1 AG_ACC_NO, AR_AG.RANK_TITEL AGENT_RANK, TRUNC(CAT_AG.CAS_TRAN_DATE) AG_COMM_DATE, "
                     + " THA.DEL_ACCNT_NO DIS_ACC_NO, CL_DIS.CLINT_NAME DIS_NAME, CL_DIS.CLINT_ADDRESS1 DIS_ADDR, "
                     + " MT_DIS.THANA_NAME DIS_THANA_NAME, MD_DIS.DISTRICT_NAME DIS_DISTR_NAME, "
                     + " AL_KYC.ACCNT_NO KYC_ACC_NO, AR_KYC.RANK_TITEL KYC_RANK,"
                     + " CL_KYC.CLINT_NAME KYC_NAME, CL_KYC.CLINT_ADDRESS1 KYC_ADDR, "
                     + " MT_KYC.THANA_NAME KYC_THANA_NAME, MD_KYC.DISTRICT_NAME KYC_DISTR_NAME "
                     + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT_AG, "
                     + " ACCOUNT_LIST AL_CUST, CLIENT_LIST CL_CUST, TEMP_HIERARCHY_LIST_ALL THA, "
                     + " ACCOUNT_LIST AL_AG, ACCOUNT_LIST AL_DIS, CLIENT_LIST CL_DIS,"
                     + " MANAGE_THANA MT_DIS, MANAGE_DISTRICT MD_DIS, ACCOUNT_LIST AL_KYC, "
                     + " CLIENT_LIST CL_KYC, ACCOUNT_RANK AR_KYC, MANAGE_THANA MT_KYC, MANAGE_DISTRICT MD_KYC, ACCOUNT_RANK AR_AG "
                     + " WHERE TRUNC(CAT_AG.CAS_TRAN_DATE) BETWEEN '" + dtpKycFromDate.DateString + "' AND '" + dtpKycToDate.DateString + "' "
                     + " AND TMIS.SERVICE_CODE = 'RG' AND CAT_AG.REQUEST_ID = TMIS.REQUEST_ID  "
                     + " AND CAT_AG.CAS_TRAN_PURPOSE_CODE = 'AGNCOM' "
                     + " AND (TMIS.RECEPENT_PARTY||1) = AL_CUST.ACCNT_NO AND AL_CUST.CLINT_ID = CL_CUST.CLINT_ID "
                     + " AND (TMIS.REQUEST_PARTY||1) = AL_AG.ACCNT_NO AND AL_AG.ACCNT_NO = THA.A_ACCNT_NO(+) "
                     + " AND AL_AG.ACCNT_RANK_ID IN('120519000000000005', '140410000000000004') AND AL_AG.ACCNT_RANK_ID = AR_AG.ACCNT_RANK_ID"
                     + " AND THA.DEL_ACCNT_NO = AL_DIS.ACCNT_NO(+) AND AL_DIS.CLINT_ID = CL_DIS.CLINT_ID(+) "
                     + " AND CL_DIS.THANA_ID = MT_DIS.THANA_ID(+) AND  MT_DIS.DISTRICT_ID = MD_DIS.DISTRICT_ID(+) "
                     + " AND CL_CUST.KYC_UPDATED_BY = AL_KYC.ACCNT_ID(+) AND AL_KYC.CLINT_ID = CL_KYC.CLINT_ID(+) "
                     + " AND CL_KYC.THANA_ID = MT_KYC.THANA_ID(+) AND  MT_KYC.DISTRICT_ID = MD_KYC.DISTRICT_ID(+) "
                     + " AND AL_KYC.ACCNT_RANK_ID = AR_KYC.ACCNT_RANK_ID ORDER BY CUST_REG_DATE";
            

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "CUST_APRV_AND_KYC_RPT";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Customer Account Approve and KYC Update(MBL Agent & MobiCash) Details Report </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=20 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpKycFromDate.DateString + "' To '" + dtpKycToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Trx Id</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Registration Date </td>";
            strHTML = strHTML + "<td valign='middle' >Customer Verification Date </td>";
            strHTML = strHTML + "<td valign='middle' >KYC Update Date</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Account Number</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Commission Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account Number</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >KYC Update By</td>";
            strHTML = strHTML + "<td valign='middle' >Rank of KYC Account</td>";
            strHTML = strHTML + "<td valign='middle' >Name of KYC Update Account Holder</td>";
            strHTML = strHTML + "<td valign='middle' >Address of KYC Update Account</td>";
            strHTML = strHTML + "<td valign='middle' >Thana of KYC Update A/C </td>";
            strHTML = strHTML + "<td valign='middle' >District of KYC Update A/C </td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {

                    string strNullValue = "";

                    if (prow["DIS_ACC_NO"].ToString() == "" || prow["DIS_NAME"].ToString() == "" ||
                        prow["DIS_ADDR"].ToString() == "" || prow["DIS_THANA_NAME"].ToString() == "" ||
                        prow["DIS_DISTR_NAME"].ToString() == "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REQ_ID"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CUST_REG_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CUST_VERIFY_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["KYC_UPDATE_DATE"].ToString())) + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AG_ACC_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_RANK"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["AG_COMM_DATE"].ToString())) + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_ACC_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_RANK"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_ADDR"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_THANA_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_DISTR_NAME"].ToString() + "</td>";

                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }


                    else if (prow["KYC_UPDATE_DATE"].ToString() == "" ||
                        prow["KYC_ACC_NO"].ToString() == "" || prow["KYC_NAME"].ToString() == "" ||
                        prow["KYC_RANK"].ToString() == "" || prow["KYC_ADDR"].ToString() == "" ||
                        prow["KYC_THANA_NAME"].ToString() == "" || prow["KYC_DISTR_NAME"].ToString() == "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REQ_ID"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CUST_REG_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CUST_VERIFY_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AG_ACC_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_RANK"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["AG_COMM_DATE"].ToString())) + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_THANA_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTR_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";

                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }

                    else if (prow["DIS_ACC_NO"].ToString() == "" || prow["DIS_NAME"].ToString() == "" ||
                        prow["DIS_ADDR"].ToString() == "" || prow["DIS_THANA_NAME"].ToString() == "" ||
                        prow["DIS_DISTR_NAME"].ToString() == "" || prow["KYC_UPDATE_DATE"].ToString() == "" ||
                        prow["KYC_ACC_NO"].ToString() == "" || prow["KYC_NAME"].ToString() == "" ||
                        prow["KYC_RANK"].ToString() == "" || prow["KYC_ADDR"].ToString() == "" ||
                        prow["KYC_THANA_NAME"].ToString() == "" || prow["KYC_DISTR_NAME"].ToString() == "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REQ_ID"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CUST_REG_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CUST_VERIFY_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AG_ACC_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_RANK"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["AG_COMM_DATE"].ToString())) + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";

                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }

                     

                    else
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REQ_ID"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CUST_REG_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CUST_VERIFY_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["KYC_UPDATE_DATE"].ToString())) + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AG_ACC_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_RANK"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["AG_COMM_DATE"].ToString())) + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_THANA_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTR_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_ACC_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_RANK"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_ADDR"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_THANA_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["KYC_DISTR_NAME"].ToString() + "</td>";

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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Distr_Wise_Cust_Reg_Count_rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
#endregion

    #region rank wise member count

    protected void btnRankOpr_Click(object sender, EventArgs e)
    {
        string strVaule = "";
        try
        {
            if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = AllMBLBranch();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = AllMBLDistributor();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = AllMBLDSE();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                strVaule = AllRank();
                //strVaule = AllMBLDSE();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = AllMBLCustomer();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000002" )
            {
                //strVaule = GpOperatorMBLBranch();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = GpOperatorMBLDistributor();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = GpOperatorMBLDSE();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = GpOperatorMBLAgent();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = GpOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = RobiOperatorMBLBranch();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = RobiOperatorMBLDistributor();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = RobiOperatorMBLDSE();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = RobiOperatorMBLAgent();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = RobiOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = RobiOperatorMBLBranch();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = RobiOperatorMBLDistributor();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = RobiOperatorMBLDSE();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = RobiOperatorMBLAgent();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = RobiOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = ATOperatorMBLBranch();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = ATOperatorMBLDistributor();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = ATOperatorMBLDSE();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = ATOperatorMBLAgent();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = ATOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = TTOperatorMBLBranch();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = TTOperatorMBLDistributor();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = TTOperatorMBLDSE();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = TTOperatorMBLAgent();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = TTOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            



            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = CCOperatorMBLBranch();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = CCOperatorMBLDistributor();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = CCOperatorMBLDSE();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = CCOperatorMBLAgent();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserank();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = MBLBrachDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = MBLDistributorDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = MBLDSEDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = MBLAgentDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWise();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }




            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }
            
            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOption();
                lblResult.Text = strVaule;
            }

            else
            {
                //
            }


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    public string AllRank()
    {
        string strSql = "";
        strSql = "SELECT COUNT(*) COUNT FROM ACCOUNT_LIST AL WHERE AL.ACCNT_RANK_ID = '"+ddlSourceRank.SelectedValue+"'";
        string count = objServiceHandler.GetRankCount(strSql);
        return count;
    }

    public string OperatorWiserank()
    {
        string strSql = "";
        strSql = "SELECT COUNT(*) COUNT FROM ACCOUNT_LIST AL WHERE SUBSTR(AL.ACCNT_NO, 1, 3) = '"+ddlOperator.SelectedValue+"' AND "
            + " AL.ACCNT_RANK_ID = '" + ddlSourceRank.SelectedValue + "'";
        string count = objServiceHandler.GetRankCount(strSql);
        return count;
    }

    public string MBLRankDateRangeWise()
    {
        string strSql = "";//120519000000000006
        strSql = " SELECT COUNT(*) COUNT FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE "
                 + " AL.ACCNT_RANK_ID = '" + ddlSourceRank.SelectedValue + "' AND AL.CLINT_ID = CL.CLINT_ID AND "
                 + " TRUNC(CL.CREATION_DATE) BETWEEN '" + dtpRankFDate.DateString + "' AND '" + dtpRankToDate.DateString + "' ";
        string count = objServiceHandler.GetRankCount(strSql);
        return count;
    }

    public string BothOption()
    {
        string strSql = "";
        strSql = " SELECT COUNT(*) FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID "
                 + " AND SUBSTR(AL.ACCNT_NO, 1, 3) = '" + ddlOperator.SelectedValue + "' "
                 + " AND TRUNC(CL.CREATION_DATE) BETWEEN '" + dtpRankFDate.DateString + "' AND '" + dtpRankToDate.DateString + "' "
                 + " AND AL.ACCNT_RANK_ID = '" + ddlSourceRank.SelectedValue + "'";

        string count = objServiceHandler.GetRankCount(strSql);
        return count;
    }

    protected void btnRankWiseList_Click(object sender, EventArgs e)
    {
        string strVaule = "";
        try
        {
            if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = AllMBLBranch();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = AllMBLDistributor();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = AllMBLDSE();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                strVaule = AllRankList();
                //strVaule = AllMBLDSE();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = AllMBLCustomer();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "0" && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = AllMBLMerchant();
                strVaule = AllRankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = GpOperatorMBLBranch();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = GpOperatorMBLDistributor();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = GpOperatorMBLDSE();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = GpOperatorMBLAgent();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = GpOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "017"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = GpOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }




            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = RobiOperatorMBLBranch();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = RobiOperatorMBLDistributor();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = RobiOperatorMBLDSE();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = RobiOperatorMBLAgent();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = RobiOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "018"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = RobiOperatorMBLBranch();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = RobiOperatorMBLDistributor();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = RobiOperatorMBLDSE();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = RobiOperatorMBLAgent();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = RobiOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "019"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = RobiOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = ATOperatorMBLBranch();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = ATOperatorMBLDistributor();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = ATOperatorMBLDSE();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = ATOperatorMBLAgent();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = ATOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "016"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = ATOperatorMBLMerChant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = TTOperatorMBLBranch();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = TTOperatorMBLDistributor();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = TTOperatorMBLDSE();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = TTOperatorMBLAgent();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = TTOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }





            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "015"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = TTOperatorMBLMerchant();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = CCOperatorMBLBranch();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = CCOperatorMBLDistributor();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = CCOperatorMBLDSE();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = CCOperatorMBLAgent();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }



            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "1" && ddlOperator.SelectedValue == "011"
                && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = CCOperatorMBLCustomer();
                strVaule = OperatorWiserankList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000002")
            {
                //strVaule = MBLBrachDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000003")
            {
                //strVaule = MBLDistributorDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000004")
            {
                //strVaule = MBLDSEDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000005")
            {
                //strVaule = MBLAgentDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "120519000000000006")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "131205000000000001")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "141105000000000001")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "130922000000000004")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "130922000000000002")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "130922000000000003")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "140410000000000004")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "2" && ddlSourceRank.SelectedValue == "130914000000000001")
            {
                //strVaule = MBLCustomerDateRange();
                strVaule = MBLRankDateRangeWiseList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }




            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "017")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "018")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "019")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "016")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "015")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000002"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000003"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000004"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000005"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "120519000000000006"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "131205000000000001"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "141105000000000001"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000004"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000002"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130922000000000003"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "140410000000000004"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else if (rbtOption.SelectedValue == "3" && ddlSourceRank.SelectedValue == "130914000000000001"
                && ddlOperator.SelectedValue == "011")
            {
                strVaule = BothOptionList();
                lblResult.Text = strVaule;
            }

            else
            {
                //
            }


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    public string AllRankList()
    {
        string strSql = "";
        strSql = "SELECT SUBSTR(AL.ACCNT_NO, 1, 11) LIST FROM ACCOUNT_LIST AL WHERE AL.ACCNT_RANK_ID = '" + ddlSourceRank.SelectedValue + "'";
        
        //string count = objServiceHandler.GetRankCount(strSql);
        //return count;

        string strHTML = "", fileName = "";
        lblMsg.Text = "";

        DataSet dtsAccount = new DataSet();
        fileName = "List_Rpt";
        //------------------------------------------Report File xl processing   -------------------------------------

        dtsAccount = objServiceHandler.ExecuteQuery(strSql);

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";

        strHTML = strHTML + "<td valign='middle' >Sl</td>";
        strHTML = strHTML + "<td valign='middle' >Mobile No</td>";
        strHTML = strHTML + "</tr>";

        if (dtsAccount.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in dtsAccount.Tables[0].Rows)
            {
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["LIST"].ToString() + " </td>";
                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;

            }

        }

        strHTML = strHTML + "<tr>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " </tr>";
        strHTML = strHTML + " </table>";

        SaveAuditInfo("Preview", "List_Rpt");
        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        lblMsg.ForeColor = Color.White;
        lblMsg.Text = "Report Generated Successfully...";

        return "";

    }

    public string OperatorWiserankList()
    {
        string strSql = "";
        strSql = "SELECT SUBSTR(AL.ACCNT_NO, 1, 11) LIST FROM ACCOUNT_LIST AL WHERE SUBSTR(AL.ACCNT_NO, 1, 3) = '" + ddlOperator.SelectedValue + "' AND "
            + " AL.ACCNT_RANK_ID = '" + ddlSourceRank.SelectedValue + "'";
        //string count = objServiceHandler.GetRankCount(strSql);
        //return count;

        string strHTML = "", fileName = "";
        lblMsg.Text = "";

        DataSet dtsAccount = new DataSet();
        fileName = "List_Rpt";
        //------------------------------------------Report File xl processing   -------------------------------------

        dtsAccount = objServiceHandler.ExecuteQuery(strSql);

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";

        strHTML = strHTML + "<td valign='middle' >Sl</td>";
        strHTML = strHTML + "<td valign='middle' >Mobile No</td>";
        strHTML = strHTML + "</tr>";

        if (dtsAccount.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in dtsAccount.Tables[0].Rows)
            {
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["LIST"].ToString() + " </td>";
                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;

            }

        }

        strHTML = strHTML + "<tr>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " </tr>";
        strHTML = strHTML + " </table>";

        SaveAuditInfo("Preview", "List_Rpt");
        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        lblMsg.ForeColor = Color.White;
        lblMsg.Text = "Report Generated Successfully...";

        return "";
    }

    public string MBLRankDateRangeWiseList()
    {
        string strSql = "";//120519000000000006
        strSql = " SELECT SUBSTR(AL.ACCNT_NO, 1, 11) LIST, CL.CREATION_DATE CREATION_DATE FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE "
                 + " AL.ACCNT_RANK_ID = '" + ddlSourceRank.SelectedValue + "' AND AL.CLINT_ID = CL.CLINT_ID AND "
                 + " TRUNC(CL.CREATION_DATE) BETWEEN '" + dtpRankFDate.DateString + "' AND '" + dtpRankToDate.DateString + "' ";
        //string count = objServiceHandler.GetRankCount(strSql);
        //return count;

        string strHTML = "", fileName = "";
        lblMsg.Text = "";

        DataSet dtsAccount = new DataSet();
        fileName = "List_Rpt";
        //------------------------------------------Report File xl processing   -------------------------------------

        dtsAccount = objServiceHandler.ExecuteQuery(strSql);

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";

        strHTML = strHTML + "<td valign='middle' >Sl</td>";
        strHTML = strHTML + "<td valign='middle' >Mobile No</td>";
        strHTML = strHTML + "<td valign='middle' >Create Date</td>";
        strHTML = strHTML + "</tr>";

        if (dtsAccount.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in dtsAccount.Tables[0].Rows)
            {
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["LIST"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["CREATION_DATE"].ToString() + " </td>";
                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;

            }

        }

        strHTML = strHTML + "<tr>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " </tr>";
        strHTML = strHTML + " </table>";

        SaveAuditInfo("Preview", "List_Rpt");
        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        lblMsg.ForeColor = Color.White;
        lblMsg.Text = "Report Generated Successfully...";

        return "";
    }

    public string BothOptionList()
    {
        string strSql = "";
        strSql = " SELECT SUBSTR(AL.ACCNT_NO, 1, 11) LIST FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID "
                 + " AND SUBSTR(AL.ACCNT_NO, 1, 3) = '" + ddlOperator.SelectedValue + "' "
                 + " AND TRUNC(CL.CREATION_DATE) BETWEEN '" + dtpRankFDate.DateString + "' AND '" + dtpRankToDate.DateString + "' "
                 + " AND AL.ACCNT_RANK_ID = '" + ddlSourceRank.SelectedValue + "'";

        //string count = objServiceHandler.GetRankCount(strSql);
        //return count;

        string strHTML = "", fileName = "";
        lblMsg.Text = "";

        DataSet dtsAccount = new DataSet();
        fileName = "List_Rpt";
        //------------------------------------------Report File xl processing   -------------------------------------

        dtsAccount = objServiceHandler.ExecuteQuery(strSql);

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";

        strHTML = strHTML + "<td valign='middle' >Sl</td>";
        strHTML = strHTML + "<td valign='middle' >Mobile No</td>";
        strHTML = strHTML + "</tr>";

        if (dtsAccount.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in dtsAccount.Tables[0].Rows)
            {
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["LIST"].ToString() + " </td>";
                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;

            }

        }

        strHTML = strHTML + "<tr>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " </tr>";
        strHTML = strHTML + " </table>";

        SaveAuditInfo("Preview", "List_Rpt");
        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

        lblMsg.ForeColor = Color.White;
        lblMsg.Text = "Report Generated Successfully...";

        return "";
    }


    #endregion

    #region D2D Agent Performance Report
    protected void btnD2dRpt_Click(object sender, EventArgs e)
    {
        
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CL_DIS.CLINT_NAME DIS_NAME, CL_DIS.CLINT_ADDRESS1 DIS_ADDR, "
                     + " MT_DIS.THANA_NAME DIS_THANA, MD_DIS.DISTRICT_NAME DIS_DISTRICT,  "
                     + " ALD2D.ACCNT_NO D2D_AGENT_NO, CLD2D.CLINT_NAME D2D_NAME, CLD2D.CLINT_ADDRESS1 D2D_ADDR, "
                     + " MTD2D.THANA_NAME D2D_THANA, MD_D2D.DISTRICT_NAME D2D_DISTRICT , "
                     + " SUBSTR(ASD_CUST.CUSTOMER_MOBILE_NO, 4, 14)||'1' CUST_NO, ASD_CUST.ACTIVATION_DATE ACTIVATION_DATE, "
                     + " CL_CUST.CREATION_DATE CREATION_DATE, CL_CUST.VERIFIED_DATE VERIFIED_DATE,  "
                     + " PKG_MIS_REPORTS.FUNC_D2D_CUST_TRX_COUNT (AL_CUST.ACCNT_NO) CUST_TRX_COUNT,"
                     + " PKG_MIS_REPORTS.FUNC_D2D_CUST_CN_AMT (AL_CUST.ACCNT_NO) CUST_CN_AMOUNT, "
                     + " PKG_MIS_REPORTS.FUNC_D2D_CUST_TRX_AMT (AL_CUST.ACCNT_NO) CUST_TRX_AMOUNT "
                     + " FROM ACCOUNT_LIST ALD2D, CLIENT_LIST CLD2D, MANAGE_THANA MTD2D, MANAGE_DISTRICT MD_D2D, "
                     + " ACCOUNT_SERIAL_DETAIL ASD_CUST, ACCOUNT_LIST AL_CUST, CLIENT_LIST CL_CUST, "
                     + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST AL_DIS, CLIENT_LIST CL_DIS, MANAGE_THANA MT_DIS, MANAGE_DISTRICT MD_DIS "
                     + " WHERE TRUNC(CL_CUST.VERIFIED_DATE) BETWEEN '"+dtpD2DFDate.DateString+"' AND '"+dtpD2DToDate.DateString+"' "
                     + " AND ALD2D.ACCNT_RANK_ID = '141105000000000001' AND ALD2D.CLINT_ID = CLD2D.CLINT_ID "
                     + " AND CLD2D.THANA_ID = MTD2D.THANA_ID(+) AND MTD2D.DISTRICT_ID = MD_D2D.DISTRICT_ID(+) "
                     + " AND ALD2D.ACCNT_NO = SUBSTR(ASD_CUST.AGENT_MOBILE_NO, 4, 14)||'1'  "
                     + " AND SUBSTR(ASD_CUST.CUSTOMER_MOBILE_NO, 4, 14)||'1' = AL_CUST.ACCNT_NO AND AL_CUST.CLINT_ID = CL_CUST.CLINT_ID "
                     + " AND ALD2D.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = AL_DIS.ACCNT_NO(+)  "
                     + " AND AL_DIS.CLINT_ID = CL_DIS.CLINT_ID(+) AND CL_DIS.THANA_ID = MT_DIS.THANA_ID(+) "
                     + " AND MT_DIS.DISTRICT_ID = MD_DIS.DISTRICT_ID(+) ORDER BY TRUNC(CL_CUST.VERIFIED_DATE) ASC";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "D2D_Performance_RPT";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>D2D Agent Performance Report(Based on Verify Date) </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpD2DFDate.DateString + "' To '" + dtpD2DToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distribtor MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent Address</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent Thana</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent District</td>";
            strHTML = strHTML + "<td valign='middle' >Customer MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' >Customer Registration Date</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Verification Date</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Count (Any txn except MYCash Help Line))</td>";
            strHTML = strHTML + "<td valign='middle' >Cash In Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount (Cash-In, Cash Out, Salary Out, Top Up, Merchant Payment, Bank Deposit)</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {

                    string strNullValue = "";

                    if (prow["DIS_NO"].ToString() == "" || prow["DIS_NAME"].ToString() == "" ||
                        prow["DIS_ADDR"].ToString() == "" || prow["DIS_THANA"].ToString() == "" ||
                        prow["DIS_DISTRICT"].ToString() == "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_AGENT_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_ADDR"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_THANA"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CREATION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_TRX_COUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_CN_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_TRX_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }



                    else
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_AGENT_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_ADDR"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_THANA"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CREATION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_TRX_COUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_CN_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_TRX_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "D2D_Performance_RPT");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    #endregion
    
    #region InterimTopup report 
    protected void btnInterim_Click(object sender, EventArgs e)
    {
        try
        {
            string strQuery = "";
            double sumOfNumber = 0;
            double sumOfAmount = 0;
            strQuery = " SELECT * FROM APSNG101.VW_INTERIM_TOPUP_REPORT ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";
            clsServiceHandler objServiceHandler = new clsServiceHandler();
            DataSet dtsAccount = new DataSet();
            fileName = "Daily_TopUp_Interim_Report";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strQuery);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h2 align=center> MYCash </h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=4 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Daily TopUP Report (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
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
            try
            {
                btnInterim.Enabled = true;
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

    #region Distribution Channel Balance

    protected void btnDisChaBalance_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            #region old query
            /*
            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DISTR_NO, AR_DIS.RANK_TITEL DISTRI_RANK, CL_DIS.CLINT_NAME DISTR_NAME, "
                     + " CL_DIS.CLINT_ADDRESS1 DISTRI_ADDRESS, MT_DIS.THANA_NAME DISTR_THANA, MD_DIS.DISTRICT_NAME DISTRI_DISTRICT, "
                     + " APSNG101.GET_FIS_BALANCE_BY_DATE (THA.DEL_ACCNT_NO, '" + dtpDistribution.DateString + "') DIS_DAY_BALANCE,  "
                     + " THA.SA_ACCNT_NO DSE_NO, AR_DSE.RANK_TITEL DSE_RANK, "
                     + " APSNG101.GET_FIS_BALANCE_BY_DATE (THA.SA_ACCNT_NO, '" + dtpDistribution.DateString + "') DSE_DAY_BALANCE, "
                     + " THA.A_ACCNT_NO AGENT_NO, AR_AG.RANK_TITEL AG_RANK, CL_AG.CLINT_NAME AG_NAME,"
                     + " APSNG101.GET_FIS_BALANCE_BY_DATE (THA.A_ACCNT_NO, '" + dtpDistribution.DateString + "') AG_DAY_BALANCE "
                     + " FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST AL_DIS, ACCOUNT_RANK AR_DIS, CLIENT_LIST CL_DIS, "
                     + " MANAGE_THANA MT_DIS, MANAGE_DISTRICT MD_DIS, ACCOUNT_LIST AL_DSE, ACCOUNT_RANK AR_DSE, ACCOUNT_LIST AL_AG, "
                     + " ACCOUNT_RANK AR_AG, CLIENT_LIST CL_AG WHERE THA.DEL_ACCNT_NO = AL_DIS.ACCNT_NO  AND AL_DIS.ACCNT_RANK_ID = AR_DIS.ACCNT_RANK_ID "
                     + " AND AL_DIS.ACCNT_RANK_ID IN ('120519000000000003') AND AL_DIS.CLINT_ID = CL_DIS.CLINT_ID  "
                     + " AND CL_DIS.THANA_ID = MT_DIS.THANA_ID(+) AND MT_DIS.DISTRICT_ID = MD_DIS.DISTRICT_ID(+)  "
                     + " AND THA.SA_ACCNT_NO = AL_DSE.ACCNT_NO AND AL_DSE.ACCNT_RANK_ID = AR_DSE.ACCNT_RANK_ID  "
                     + " AND AR_DSE.ACCNT_RANK_ID IN ('120519000000000004') AND THA.A_ACCNT_NO = AL_AG.ACCNT_NO AND  "
                     + " AL_AG.ACCNT_RANK_ID = AR_AG.ACCNT_RANK_ID AND AR_AG.ACCNT_RANK_ID IN ('120519000000000005') "
                     + " AND AL_AG.CLINT_ID = CL_AG.CLINT_ID ";
             */
            #endregion


            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DISTR_NO, AR_DIS.RANK_TITEL DISTRI_RANK, CL_DIS.CLINT_NAME DISTR_NAME, "
                     + " CL_DIS.CLINT_ADDRESS1 DISTRI_ADDRESS, MT_DIS.THANA_NAME DISTR_THANA, MD_DIS.DISTRICT_NAME DISTRI_DISTRICT, "
                     + " APSNG101.GET_FIS_BALANCE_BY_DATE (THA.DEL_ACCNT_NO, TO_CHAR(TO_DATE('14/JAN/2015'))) DIS_DAY_BALANCE,  "
                     + " THA.SA_ACCNT_NO DSE_NO, AR_DSE.RANK_TITEL DSE_RANK, "
                     + " APSNG101.GET_FIS_BALANCE_BY_DATE (THA.SA_ACCNT_NO, TO_CHAR(TO_DATE('14/JAN/2015'))) DSE_DAY_BALANCE "
                     + " FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST AL_DIS, ACCOUNT_RANK AR_DIS, CLIENT_LIST CL_DIS, "
                     + " MANAGE_THANA MT_DIS, MANAGE_DISTRICT MD_DIS, ACCOUNT_LIST AL_DSE, ACCOUNT_RANK AR_DSE "
                     + " WHERE THA.DEL_ACCNT_NO = AL_DIS.ACCNT_NO  AND AL_DIS.ACCNT_RANK_ID = AR_DIS.ACCNT_RANK_ID "
                     + " AND AL_DIS.ACCNT_RANK_ID IN ('120519000000000003') AND AL_DIS.CLINT_ID = CL_DIS.CLINT_ID  "
                     + " AND CL_DIS.THANA_ID = MT_DIS.THANA_ID(+) AND MT_DIS.DISTRICT_ID = MD_DIS.DISTRICT_ID(+)  "
                     + " AND THA.SA_ACCNT_NO = AL_DSE.ACCNT_NO AND AL_DSE.ACCNT_RANK_ID = AR_DSE.ACCNT_RANK_ID  "
                     + " AND AR_DSE.ACCNT_RANK_ID IN ('120519000000000004') ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Distribution_Channel_Balance_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distribution Channel Balance Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Balance Date : '" + dtpDistribution.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Rank </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Balance</td>";
            strHTML = strHTML + "<td valign='middle' >DSE MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Rank </td>";
            strHTML = strHTML + "<td valign='middle' >DSE Balance</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTR_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRI_RANK"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTR_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRI_ADDRESS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTR_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRI_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DAY_BALANCE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_RANK"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_DAY_BALANCE"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    //break;
                    //if (dtsAccount.Tables[0].Rows.Count == 10)
                    //{
                    //    break;
                    //}
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

            SaveAuditInfo("Preview", "Distribution_Channel_Balance_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    #endregion

    #region Distributor lifting and refund report

    protected void btnDisLiftNRefndOld_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            double noOfLift = 0;
            double amountOfLifting = 0;
            double noOfRefund = 0;
            double amountOfRefund = 0;

            strSql = " SELECT AL.ACCNT_NO DIS_NO, AR.RANK_TITEL DIS_RANK, CL.CLINT_NAME DIS_NAME, CL.CLINT_ADDRESS1 DIS_ADDRESS, "
                     + " MT.THANA_NAME DIS_THANA, MD.DISTRICT_NAME DIS_DISTRICT, "
                     + " PKG_MIS_REPORTS.FUNC_NO_OF_DIS_LIFTING (AL.ACCNT_NO,'" + dtpDisLiftFD.DateString + "','" + dtpDisLiftToD.DateString + "') COUNT_LIFTING, "
                     + " PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING (AL.ACCNT_NO,'" + dtpDisLiftFD.DateString + "','" + dtpDisLiftToD.DateString + "') AMOUNT_LIFTING, "
                     + " PKG_MIS_REPORTS.FUNC_NO_OF_DIS_REFUND (AL.ACCNT_NO,'" + dtpDisLiftFD.DateString + "','" + dtpDisLiftToD.DateString + "') COUNT_REFUND, "
                     + " PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_REFUND (AL.ACCNT_NO,'" + dtpDisLiftFD.DateString + "','" + dtpDisLiftToD.DateString + "') AMOUNT_REFUND "
                     + " FROM APSNG101.ACCOUNT_LIST AL, APSNG101.ACCOUNT_RANK AR, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD "
                     + " WHERE AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_RANK_ID IN ('120519000000000003', '140917000000000002', '140514000000000001', '130922000000000001') "
                     + " AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID(+) ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Distr_Lifting_And_Refund_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Lifting and Refund Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisLiftFD.DateString + "' To '" + dtpDisLiftToD.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Business A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Rank </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >No. of lifting</td>";
            strHTML = strHTML + "<td valign='middle' >Total Lifting Amount</td>";
            strHTML = strHTML + "<td valign='middle' >No. of Refund</td>";
            strHTML = strHTML + "<td valign='middle' >Total Refund Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                string strBusinessNo = "01616225100";

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + strBusinessNo + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_RANK"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDRESS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["COUNT_LIFTING"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AMOUNT_LIFTING"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["COUNT_REFUND"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AMOUNT_REFUND"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    noOfLift = noOfLift + Convert.ToDouble(prow["COUNT_LIFTING"].ToString());
                    amountOfLifting = amountOfLifting + Convert.ToDouble(prow["AMOUNT_LIFTING"].ToString());
                    noOfRefund = noOfRefund + Convert.ToDouble(prow["COUNT_REFUND"].ToString());
                    amountOfRefund = amountOfRefund + Convert.ToDouble(prow["AMOUNT_REFUND"].ToString());
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
            strHTML = strHTML + " <td > " + "Total:" + " </td>";
            strHTML = strHTML + " <td > " + noOfLift.ToString() + " </td>";
            strHTML = strHTML + " <td > " + amountOfLifting.ToString() + " </td>";
            strHTML = strHTML + " <td > " + noOfRefund.ToString() + " </td>";
            strHTML = strHTML + " <td > " + amountOfRefund.ToString() + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Distr_Lifting_And_Refund_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }

    }


    protected void btnDisLiftNRfnd_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            double noOfLift = 0;
            double amountOfLifting = 0;
            double noOfRefund = 0;
            double amountOfRefund = 0;

            strSql = "SELECT AL.ACCNT_NO DIS_NO, AR.RANK_TITEL DIS_RANK, CL.CLINT_NAME DIS_NAME, CL.CLINT_ADDRESS1 DIS_ADDRESS, MT.THANA_NAME DIS_THANA, MD.DISTRICT_NAME DIS_DISTRICT, NVL(TO_CHAR(TB.CAS_TRAN_DATE,'MM/DD/YYYY'),'NA') LIFTING_DATE, NVL(TB.CAS_TRAN_AMT,0)  AMOUNT_LIFTING,  NVL(TO_CHAR(TBL.CAS_TRAN_DATE,'MM/DD/YYYY'),'NA') REFUND_DATE, NVL(TBL.CAS_TRAN_AMT,0) AMOUNT_REFUND  FROM APSNG101.ACCOUNT_LIST AL, APSNG101.ACCOUNT_RANK AR, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD, (SELECT CAS_TRAN_DATE, CAS_TRAN_AMT , AL.ACCNT_NO FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT, APSNG101.ACCOUNT_LIST AL WHERE AL.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND  TRUNC(CAT.CAS_TRAN_DATE) BETWEEN  TRUNC(TO_DATE('" + dtpDisLiftFD.DateString + "'))  AND  TRUNC(TO_DATE('" + dtpDisLiftToD.DateString + "')) AND CAT.CAS_TRAN_TYPE = 'C' AND CAT.ACCESS_CODE = 'DLF') TB, (SELECT CAS_TRAN_DATE, CAS_TRAN_AMT,AL.ACCNT_NO FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT, APSNG101.ACCOUNT_LIST AL WHERE  AL.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN  TRUNC(TO_DATE('" + dtpDisLiftFD.DateString + "'))  AND  TRUNC(TO_DATE('" + dtpDisLiftToD.DateString + "')) AND CAT.CAS_TRAN_TYPE = 'D' AND CAT.ACCESS_CODE = 'DRF') TBL WHERE AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.ACCNT_NO =TB.ACCNT_NO AND AL.ACCNT_NO =TBL.ACCNT_NO (+)  AND (AL.ACCNT_RANK_ID IN     ('120519000000000003', '140917000000000002', '140514000000000001',           '130922000000000001') OR RANK_TITEL LIKE '%DIS%' OR RANK_TITEL LIKE '%Dis%'  OR RANK_TITEL LIKE '%dis%') AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID(+)";

            // strSql = "  SELECT AL.ACCNT_NO DIS_NO, AR.RANK_TITEL DIS_RANK, CL.CLINT_NAME DIS_NAME, CL.CLINT_ADDRESS1 DIS_ADDRESS,  MT.THANA_NAME DIS_THANA, MD.DISTRICT_NAME DIS_DISTRICT, PKG_MIS_REPORTS.FUNC_NO_OF_DIS_LIFTING_NEW (AL.ACCNT_NO,'" + dtpDisLiftFD.DateString + "','" + dtpDisLiftToD.DateString + "') COUNT_LIFTING,  PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW (AL.ACCNT_NO,'" + dtpDisLiftFD.DateString + "','" + dtpDisLiftToD.DateString + "') AMOUNT_LIFTING,  PKG_MIS_REPORTS.FUNC_NO_OF_DIS_REFUND_NEW (AL.ACCNT_NO,'" + dtpDisLiftFD.DateString + "','" + dtpDisLiftToD.DateString + "') COUNT_REFUND,  PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_REFUND_NEW (AL.ACCNT_NO,'" + dtpDisLiftFD.DateString + "','" + dtpDisLiftToD.DateString + "') AMOUNT_REFUND  FROM APSNG101.ACCOUNT_LIST AL, APSNG101.ACCOUNT_RANK AR, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD WHERE AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND (AL.ACCNT_RANK_ID IN     ('120519000000000003', '140917000000000002', '140514000000000001', '130922000000000001') OR RANK_TITEL LIKE '%DIS%' OR RANK_TITEL LIKE '%Dis%'  OR RANK_TITEL LIKE '%dis%') AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID(+)";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Distr_Lifting_And_Refund_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Lifting and Refund Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisLiftFD.DateString + "' To '" + dtpDisLiftToD.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Business A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Rank </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            // strHTML = strHTML + "<td valign='middle' >No. of lifting</td>";

            strHTML = strHTML + "<td valign='middle' >Lifting Date</td>";
            strHTML = strHTML + "<td valign='middle' >Total Lifting Amount</td>";
            // strHTML = strHTML + "<td valign='middle' >No. of Refund</td>";

            strHTML = strHTML + "<td valign='middle' >Refund Date</td>";
            strHTML = strHTML + "<td valign='middle' >Total Refund Amount</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                string strBusinessNo = "01616225100";

                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + strBusinessNo + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_RANK"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDRESS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    // strHTML = strHTML + " <td > '" + prow["COUNT_LIFTING"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["LIFTING_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AMOUNT_LIFTING"].ToString() + " </td>";
                    // strHTML = strHTML + " <td > '" + prow["COUNT_REFUND"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REFUND_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AMOUNT_REFUND"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    // noOfLift = noOfLift + Convert.ToDouble(prow["COUNT_LIFTING"].ToString());
                    amountOfLifting = amountOfLifting + Convert.ToDouble(prow["AMOUNT_LIFTING"].ToString());
                    // noOfRefund = noOfRefund + Convert.ToDouble(prow["COUNT_REFUND"].ToString());
                    amountOfRefund = amountOfRefund + Convert.ToDouble(prow["AMOUNT_REFUND"].ToString());
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
            strHTML = strHTML + " <td > " + "Total:" + " </td>";
            // strHTML = strHTML + " <td > " + noOfLift.ToString() + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + amountOfLifting.ToString() + " </td>";
            // strHTML = strHTML + " <td > " + noOfRefund.ToString() + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + amountOfRefund.ToString() + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Distr_Lifting_And_Refund_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }

    }
    #endregion

    #region Rule Checker
    protected void btnRuleCheck_Click(object sender, EventArgs e)
    {
        string strSql = "";
        try
        {
            if (txtRCFRAmt.Text == "" || txtRCTOAmt.Text == "" || ddlRCServiceList.SelectedValue == "0")
            {
                lblMsg.Text = "Enter Amount. or Select Service.";
                lblMsg.ForeColor = Color.Red;
            }

            else if (ddlRCServiceList.SelectedValue == "1" && txtRCFRAmt.Text != "" && txtRCTOAmt.Text != "")
            {
                strSql = " SELECT TMIS.RECEPENT_PARTY ACC_NO, COUNT(DISTINCT TMIS.REQUEST_ID) COUNT_TRX, SUM(TMIS.TRANSACTION_AMOUNT) TRX_SUM FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS "
                         + " WHERE TMIS.SERVICE_CODE IN('" + ddlRCServiceList.SelectedItem.Text + "') AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpRuleFromDate.DateString + "' AND '" + dtpRuleToDate.DateString + "' "
                         + " GROUP BY TMIS.RECEPENT_PARTY HAVING SUM(TMIS.TRANSACTION_AMOUNT) BETWEEN '" + Convert.ToDouble(txtRCFRAmt.Text.Trim()) + "' AND '" + Convert.ToDouble(txtRCTOAmt.Text.Trim()) + "' ";

                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "Transaction_Rule_Checker_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Transaction Rule Checker Report(CN)</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpRuleFromDate.DateString + "' To '" + dtpRuleToDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Total Count</td>";
                strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_SUM"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "Transaction_Rule_Checker_Report");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";

                
            }

            else if (ddlRCServiceList.SelectedValue == "2" && txtRCFRAmt.Text != "" && txtRCTOAmt.Text != "")
            {
                strSql = " SELECT TMIS.REQUEST_PARTY ACC_NO, COUNT(DISTINCT TMIS.REQUEST_ID) COUNT_TRX, SUM(TMIS.TRANSACTION_AMOUNT) TRX_SUM FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS "
                    + " WHERE TMIS.SERVICE_CODE IN('CCT', 'SW') AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpRuleFromDate.DateString + "' AND '" + dtpRuleToDate.DateString + "' "
                    + " GROUP BY TMIS.REQUEST_PARTY HAVING SUM(TMIS.TRANSACTION_AMOUNT) BETWEEN '" + Convert.ToDouble(txtRCFRAmt.Text.Trim()) + "' AND '" + Convert.ToDouble(txtRCTOAmt.Text.Trim()) + "' ";

                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "Transaction_Rule_Checker_Report";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Transaction Rule Checker Report(CCT, SW)</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpRuleFromDate.DateString + "' To '" + dtpRuleToDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >A/C No.</td>";
                strHTML = strHTML + "<td valign='middle' >Total Count</td>";
                strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
                strHTML = strHTML + "</tr>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;

                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ACC_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_SUM"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "Transaction_Rule_Checker_Report");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";

            }

            else if (ddlRCServiceList.SelectedValue == "3" && txtRCFRAmt.Text != "" && txtRCTOAmt.Text != "")
            {
                strSql = " SELECT TMIS.REQUEST_PARTY REQ_P, TMIS.RECEPENT_PARTY REC_P, COUNT(DISTINCT TMIS.REQUEST_ID) COUNT_TRX, SUM(TMIS.TRANSACTION_AMOUNT) TRX_SUM"
                   + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS WHERE TMIS.SERVICE_CODE = 'FT' AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpRuleFromDate.DateString + "' AND '" + dtpRuleToDate.DateString + "' "
                   + " GROUP BY TMIS.REQUEST_PARTY, TMIS.RECEPENT_PARTY HAVING SUM(TMIS.TRANSACTION_AMOUNT) BETWEEN '" + Convert.ToDouble(txtRCFRAmt.Text.Trim()) + "' AND '" + Convert.ToDouble(txtRCTOAmt.Text.Trim()) + "' ";

               string strHTML = "", fileName = "";
               lblMsg.Text = "";

               DataSet dtsAccount = new DataSet();
               fileName = "Transaction_Rule_Checker_Report";
               //------------------------------------------Report File xl processing   -------------------------------------

               dtsAccount = objServiceHandler.ExecuteQuery(strSql);

               strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
               strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
               strHTML = strHTML + "</table>";
               strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
               strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
               strHTML = strHTML + "</table>";
               strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
               strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Transaction Rule Checker Report(FT)</h2></td></tr>";
               strHTML = strHTML + "</table>";
               strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
               strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpRuleFromDate.DateString + "' To '" + dtpRuleToDate.DateString + "'</h2></td></tr>";
               strHTML = strHTML + "</table>";
               strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
               strHTML = strHTML + "<tr>";

               strHTML = strHTML + "<td valign='middle' >Sl</td>";
               strHTML = strHTML + "<td valign='middle' >Request A/C No.</td>";
               strHTML = strHTML + "<td valign='middle' >Recipient A/C No.</td>";
               strHTML = strHTML + "<td valign='middle' >Total Count</td>";
               strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
               strHTML = strHTML + "</tr>";

               if (dtsAccount.Tables[0].Rows.Count > 0)
               {
                   int SerialNo = 1;

                   foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                   {
                       strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                       strHTML = strHTML + " <td > '" + prow["REQ_P"].ToString() + " </td>";
                       strHTML = strHTML + " <td > '" + prow["REC_P"].ToString() + " </td>";
                       strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                       strHTML = strHTML + " <td > '" + prow["TRX_SUM"].ToString() + "</td>";
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

               SaveAuditInfo("Preview", "Transaction_Rule_Checker_Report");
               clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

               lblMsg.ForeColor = Color.White;
               lblMsg.Text = "Report Generated Successfully..."; 
            }

            else
            {
                lblMsg.Text = "Select a Service..";
            }

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    public void ClearRuleChecker()
    {
        dtpRuleFromDate.Date = DateTime.Now;
        dtpRuleToDate.Date = DateTime.Now;
        txtRCFRAmt.Text = "";
        txtRCTOAmt.Text = "";
        ddlRCServiceList.SelectedIndex = -1;
    }

    protected void btnRuleCheckClear_Click(object sender, EventArgs e)
    {
        ClearRuleChecker();
    }


    #endregion

    #region D2D Agent Performance Report(Based on Registration Date)
    protected void btnD2DRegRpt_Click(object sender, EventArgs e)
    {
        string strSql = "";
        try
        {
            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CL_DIS.CLINT_NAME DIS_NAME, CL_DIS.CLINT_ADDRESS1 DIS_ADDR, "
                     + " MT_DIS.THANA_NAME DIS_THANA, MD_DIS.DISTRICT_NAME DIS_DISTRICT,  ALD2D.ACCNT_NO D2D_AGENT_NO, CLD2D.CLINT_NAME D2D_NAME, "
                     + " CLD2D.CLINT_ADDRESS1 D2D_ADDR,  MTD2D.THANA_NAME D2D_THANA, MD_D2D.DISTRICT_NAME D2D_DISTRICT , "
                     + " SUBSTR(ASD_CUST.CUSTOMER_MOBILE_NO, 4, 14)||'1' CUST_NO, AR_CUST.RANK_TITEL CUST_RANK, ASD_CUST.ACTIVATION_DATE ACTIVATION_DATE, "
                     + " CL_CUST.CREATION_DATE CREATION_DATE, CL_CUST.VERIFIED_DATE VERIFIED_DATE,  "
                     + " PKG_MIS_REPORTS.FUNC_D2D_CUST_TRX_COUNT (AL_CUST.ACCNT_NO) CUST_TRX_COUNT, "
                     + " PKG_MIS_REPORTS.FUNC_D2D_CUST_CN_AMT (AL_CUST.ACCNT_NO) CUST_CN_AMOUNT,  "
                     + " PKG_MIS_REPORTS.FUNC_D2D_CUST_TRX_AMT (AL_CUST.ACCNT_NO) CUST_TRX_AMOUNT "
                     + " FROM ACCOUNT_LIST ALD2D, CLIENT_LIST CLD2D, MANAGE_THANA MTD2D, MANAGE_DISTRICT MD_D2D, "
                     + " ACCOUNT_SERIAL_DETAIL ASD_CUST, ACCOUNT_LIST AL_CUST, CLIENT_LIST CL_CUST, ACCOUNT_RANK AR_CUST, "
                     + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST AL_DIS, CLIENT_LIST CL_DIS, MANAGE_THANA MT_DIS, MANAGE_DISTRICT MD_DIS "
                     + " WHERE TRUNC(ASD_CUST.ACTIVATION_DATE) BETWEEN '" + dtpD2DRegFDate.DateString + "' AND '" + dtpD2DRegToDate.DateString + "' "
                     + " AND ALD2D.ACCNT_RANK_ID = '141105000000000001' AND ALD2D.CLINT_ID = CLD2D.CLINT_ID  "
                     + " AND CLD2D.THANA_ID = MTD2D.THANA_ID(+) AND MTD2D.DISTRICT_ID = MD_D2D.DISTRICT_ID(+)  "
                     + " AND ALD2D.ACCNT_NO = SUBSTR(ASD_CUST.AGENT_MOBILE_NO, 4, 14)||'1'  "
                     + " AND SUBSTR(ASD_CUST.CUSTOMER_MOBILE_NO, 4, 14)||'1' = AL_CUST.ACCNT_NO AND AL_CUST.CLINT_ID = CL_CUST.CLINT_ID "
                     + " AND AL_CUST.ACCNT_RANK_ID = AR_CUST.ACCNT_RANK_ID AND AR_CUST.ACCNT_RANK_ID IN ('130914000000000001', '120519000000000006') "
                     + " AND ALD2D.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = AL_DIS.ACCNT_NO(+)   "
                     + " AND AL_DIS.CLINT_ID = CL_DIS.CLINT_ID(+) AND CL_DIS.THANA_ID = MT_DIS.THANA_ID(+) "
                     + " AND MT_DIS.DISTRICT_ID = MD_DIS.DISTRICT_ID(+) ORDER BY TRUNC(CL_CUST.VERIFIED_DATE) ASC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "D2D_Perf_RPT_RG";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>D2D Agent Performance Report(Based on Registration Date) </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpD2DFDate.DateString + "' To '" + dtpD2DToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distribtor MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent MYCash A/C No.</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent Address</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent Thana</td>";
            strHTML = strHTML + "<td valign='middle' >D2D Agent District</td>";
            strHTML = strHTML + "<td valign='middle' >Customer MYCash A/C No. </td>";
            strHTML = strHTML + "<td valign='middle' >Customer Rank </td>";
            strHTML = strHTML + "<td valign='middle' >Customer Registration Date</td>";
            strHTML = strHTML + "<td valign='middle' >Customer Verification Date</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Count (Any txn except MYCash Help Line))</td>";
            strHTML = strHTML + "<td valign='middle' >Cash In Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount (Cash-In, Cash Out, Salary Out, Top Up, Merchant Payment, Bank Deposit)</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {

                    string strNullValue = "";

                    if (prow["DIS_NO"].ToString() == "" || prow["DIS_NAME"].ToString() == "" ||
                        prow["DIS_ADDR"].ToString() == "" || prow["DIS_THANA"].ToString() == "" ||
                        prow["DIS_DISTRICT"].ToString() == "")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + " </td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + strNullValue + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_AGENT_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_ADDR"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_THANA"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_RANK"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CREATION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_TRX_COUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_CN_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_TRX_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                    }



                    else
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_AGENT_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_ADDR"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_THANA"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["D2D_DISTRICT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_RANK"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["CREATION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["VERIFIED_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_TRX_COUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_CN_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["CUST_TRX_AMOUNT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

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
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "D2D_Perf_RPT_Reg");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    #endregion
    
}
