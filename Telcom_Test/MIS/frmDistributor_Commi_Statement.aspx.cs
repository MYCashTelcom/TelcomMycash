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


public partial class MIS_frmDistributor_Commi_Statement : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();   
    private string strUserName = string.Empty;
    private string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             //DateTime dt = DateTime.Now;
             //DateTime dtStartDate = new DateTime(dt.Year, dt.Month, 1);
            try
            {
                //dtpFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dtStartDate);
                //dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(-1));
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
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
        lblMsg.Text = "";
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string strSQL = "", strMsg = "", strQuery = "", strHTML = "", fileName = "";
        lblMsg.Text = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        try
        {
            DataSet dtsAccntBalance = new DataSet();
            fileName = "Distributor_Commission_Statment";

            #region old query

            //strSQL = "PRO_INSERT_ALL_HIERARCHY";
            //strMsg = objServiceHandler.ExecuteProcedure(strSQL);

            //strSQL = "PKG_MIS_REPORTS.PRO_MIS_REPORT_FINAL";
            //strMsg = objServiceHandler.ExecuteProcedure(strSQL);

            
            // AND DR.ACCNT_RANK_ID='120519000000000003'
            //------------------------------------------Report Query -------------------------------------
            //strQuery = " SELECT DIS_NO,PARTY_TYPE RANK_TITEL,DISTRIBUTOR_ADD,DISTRIBUTOR_NAME,DECODE(SERVICE_CODE,'CN',COUNT(*),0)CN_COUNT,"
            //         + " DECODE(SERVICE_CODE,'CN',SUM(TRANSACTION_AMOUNT),0)CN_AMOUNT,DECODE(SERVICE_CODE,'CN',SUM(DIS_COMI_AMNT),0)CN_COMMISSION,"
            //         + " DECODE(SERVICE_CODE,'CCT',COUNT(*),0)CCT_COUNT,DECODE(SERVICE_CODE,'CCT',SUM(TRANSACTION_AMOUNT),0)CCT_AMOUNT,"
            //         + " DECODE(SERVICE_CODE,'CCT',SUM(DIS_COMI_AMNT),0)CCT_COMMISSION,DECODE(SERVICE_CODE,'RG',COUNT(*),0)RG_COUNT,"
            //         + " DECODE(SERVICE_CODE,'RG',COUNT(*),0)RG_COUNT,DECODE(SERVICE_CODE,'RG',SUM(DIS_COMI_AMNT),0)TOTAL_RG_COMMISSION,"
            //         + " DECODE(SERVICE_CODE,'CN',SUM(DIS_COMI_AMNT),0)+DECODE(SERVICE_CODE,'CCT',SUM(DIS_COMI_AMNT),0)+DECODE(SERVICE_CODE,'RG',SUM(DIS_COMI_AMNT),0) DIS_RG_COMMISSION FROM VW_DISTRIBUTION_REPORT DR"
            //         + " WHERE SERVICE_CODE IN ('CN','CCT','RG') "
            //         + " AND DR.TRANSACTION_DATE BETWEEN '" + dtpFromDate.DateString +"' AND '" + dtpToDate.DateString + "' "
            //         + " GROUP BY DIS_NO,SERVICE_CODE,PARTY_TYPE,DISTRIBUTOR_ADD,DISTRIBUTOR_NAME ORDER BY DIS_NO ";

            // OLD QUERY : 13/10/2014
           /* strQuery = "  SELECT DIS_NO,RANK_TITEL,DISTRIBUTOR_ADD,DISTRIBUTOR_NAME,SUM(CN_COUNT)CN_COUNT,SUM(CN_AMOUNT)CN_AMOUNT,SUM(CN_COMMISSION)CN_COMMISSION,"
             + " SUM(CCT_COUNT)CCT_COUNT,SUM(CCT_AMOUNT)CCT_AMOUNT,SUM(CCT_COMMISSION)CCT_COMMISSION,SUM(RG_COUNT)RG_COUNT,"
             + " SUM(TOTAL_RG_COMMISSION)TOTAL_RG_COMMISSION,SUM(DIS_RG_COMMISSION)DIS_RG_COMMISSION "
             + " FROM VW_DISTRIBUTION_REPORT2 VDR WHERE VDR.TRANSACTION_DATE BETWEEN '" + dtpFromDate.DateString + "' AND '" + dtpToDate.DateString + "' "
             + " GROUP BY DIS_NO,RANK_TITEL,DISTRIBUTOR_ADD,DISTRIBUTOR_NAME ORDER BY DIS_NO "; */
            #endregion

            #region old query 1
            /*
            strQuery = " SELECT DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, SUM (CN_COUNT) CN_COUNT, "
                     + " SUM (CN_AMOUNT) CN_AMOUNT, SUM (CN_COMMISSION) CN_COMMISSION, SUM (CCT_COUNT) CCT_COUNT, "
                     + " SUM (CCT_AMOUNT) CCT_AMOUNT, SUM (CCT_COMMISSION) CCT_COMMISSION, SUM (RG_COUNT) RG_COUNT, "
                     + " SUM (TOTAL_RG_COMMISSION) TOTAL_RG_COMMISSION, SUM (BD_COUNT) BD_COUNT, SUM (BD_AMOUNT) BD_AMOUNT, "
                     + " SUM (BD_COMMISSION) BD_COMMISSION, SUM (DIS_RG_COMMISSION) DIS_RG_COMMISSION "
                     + " FROM VW_DISTRIBUTION_REPORT2 VDR WHERE VDR.TRANSACTION_DATE "
                     + " BETWEEN '" + dtpFromDate.DateString + "' AND '" + dtpToDate.DateString + "' "
                     + " GROUP BY DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME ORDER BY DIS_NO";
            */
            #endregion

            #region old query 2

            //strQuery = " SELECT DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, SUM (CN_COUNT) CN_COUNT, "
            //           + " SUM (CN_AMOUNT) CN_AMOUNT, SUM (CN_COMMISSION) CN_COMMISSION, SUM (CCT_COUNT) CCT_COUNT,  "
            //           + " SUM (CCT_AMOUNT) CCT_AMOUNT, SUM (CCT_COMMISSION) CCT_COMMISSION, SUM (RG_COUNT) RG_APRV_COUNT, TEMP.REG_COUNT REGIS_COUNT,  "
            //           + " SUM (TOTAL_RG_COMMISSION) TOTAL_RG_COMMISSION, SUM (BD_COUNT) BD_COUNT, SUM (BD_AMOUNT) BD_AMOUNT,  "
            //           + " SUM (BD_COMMISSION) BD_COMMISSION, SUM (DIS_RG_COMMISSION) DIS_RG_COMMISSION  FROM VW_DISTRIBUTION_REPORT2 VDR,"
            //           + " (SELECT TRAN2.DEL_ACCNT_NO DIS_ACC_NO, TRAN2.REG_CNT REG_COUNT  FROM (SELECT TRAN.DEL_ACCNT_NO,SUM(CT) REG_CNT  FROM  "
            //           + " (SELECT THL.DEL_ACCNT_NO,ASD.AGENT_MOBILE_NO, COUNT (ASD.CUSTOMER_MOBILE_NO) CT FROM ACCOUNT_SERIAL_DETAIL ASD,  "
            //           + " TEMP_HIERARCHY_LIST_ALL THL WHERE TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpFromDate.DateString + "' AND '" + dtpToDate.DateString + "' "
            //           + " AND SUBSTR(('+88'||THL.A_ACCNT_NO),0,14)=ASD.AGENT_MOBILE_NO GROUP BY ASD.AGENT_MOBILE_NO,THL.DEL_ACCNT_NO) TRAN  "
            //           + " GROUP BY TRAN.DEL_ACCNT_NO) TRAN2, CLIENT_LIST CL ,MANAGE_THANA MT, MANAGE_DISTRICT MD WHERE TRAN2.DEL_ACCNT_NO= SUBSTR(CL.CLINT_MOBILE,4,14)||'1' "
            //           + " AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) ) TEMP WHERE "
            //           + "VDR.TRANSACTION_DATE BETWEEN '" + dtpFromDate.DateString + "' AND '" + dtpToDate.DateString + "'  AND DIS_NO = TEMP.DIS_ACC_NO(+)"
            //           + " GROUP BY DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, REG_COUNT ORDER BY DIS_NO ";
            #endregion


            strQuery = " SELECT DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, DISTRICT_NAME, SUM (CN_COUNT) CN_COUNT, "
                       + " SUM (CN_AMOUNT) CN_AMOUNT, SUM (CN_COMMISSION) CN_COMMISSION, SUM (CCT_COUNT) CCT_COUNT,  "
                       + " SUM (CCT_AMOUNT) CCT_AMOUNT, SUM (CCT_COMMISSION) CCT_COMMISSION, SUM (RG_COUNT) RG_APRV_COUNT, TEMP.REG_COUNT REGIS_COUNT,  "
                       + " SUM (TOTAL_RG_COMMISSION) TOTAL_RG_COMMISSION, SUM (BD_COUNT) BD_COUNT, SUM (BD_AMOUNT) BD_AMOUNT,  "
                       + " SUM (BD_COMMISSION) BD_COMMISSION, SUM (UBP_COUNT) UBP_COUNT, SUM(UBP_AMOUNT) UBP_AMOUNT, SUM(UBP_COMMISSION) UBP_COMMISSION, "
                       + " SUM (DIS_RG_COMMISSION) DIS_RG_COMMISSION  FROM VW_DISTRIBUTION_REPORT2 VDR,"
                       + " (SELECT TRAN2.DEL_ACCNT_NO DIS_ACC_NO, TRAN2.REG_CNT REG_COUNT, MD.DISTRICT_NAME  FROM (SELECT TRAN.DEL_ACCNT_NO,SUM(CT) REG_CNT  FROM  "
                       + " (SELECT THL.DEL_ACCNT_NO,ASD.AGENT_MOBILE_NO, COUNT (ASD.CUSTOMER_MOBILE_NO) CT FROM ACCOUNT_SERIAL_DETAIL ASD,  "
                       + " TEMP_HIERARCHY_LIST_ALL THL WHERE TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpFromDate.DateString + "' AND '" + dtpToDate.DateString + "' "
                       + " AND SUBSTR(('+88'||THL.A_ACCNT_NO),0,14)=ASD.AGENT_MOBILE_NO GROUP BY ASD.AGENT_MOBILE_NO,THL.DEL_ACCNT_NO) TRAN  "
                       + " GROUP BY TRAN.DEL_ACCNT_NO) TRAN2, CLIENT_LIST CL ,MANAGE_THANA MT, MANAGE_DISTRICT MD WHERE TRAN2.DEL_ACCNT_NO= SUBSTR(CL.CLINT_MOBILE,4,14)||'1' "
                       + " AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) ) TEMP WHERE "
                       + "VDR.TRANSACTION_DATE BETWEEN '" + dtpFromDate.DateString + "' AND '" + dtpToDate.DateString + "'  AND DIS_NO = TEMP.DIS_ACC_NO(+)"
                       + " GROUP BY DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, REG_COUNT, DISTRICT_NAME ORDER BY DIS_NO ";

            dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
            SaveAuditInfo("Report", "Distributor Commission Statement");

            strHTML = strHTML + "<table border=\"0\" width=\"60%\">";
            strHTML = strHTML + "<tr><td COLSPAN=22 align=center style='border-right:none;align=center;font-size:22px;font-weight:bold;'> Mercantile Bank Limited </td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"60%\">";
            strHTML = strHTML + "<tr><td COLSPAN=22 align=center style='border-right:none;align=center;font-size:20px;font-weight:bold;'> MYCash </td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"60%\">";
            strHTML = strHTML + "<tr><td COLSPAN=22 align=center style='border-right:none;align=center;font-size:16px;font-weight:bold;'> Distributor Commission Statement </td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"60%\">";
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td COLSPAN=11 align=left style='border-right:none;align=left;font-size:12px;font-weight:bold;'> Date Range Between From Date " + dtpFromDate.DateString + " To " + dtpToDate.DateString + " </td>";
            strHTML = strHTML + " <td COLSPAN=11 align=right style='border-right:none;font-size:12px;font-weight:bold;'> Print Date " + DateTime.Now.ToShortDateString() + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + "</table>";

            #region old code 

            /*
            strHTML = strHTML + "<table border=\"1\" width=\"60%\">";
            strHTML = strHTML + "<tr style='border-right:none;font-size:12px;font-weight:bold;'>";
            strHTML = strHTML + "<td valign='middle' >SerialNo</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor MYCash Account Number</td>";
            strHTML = strHTML + "<td valign='middle' > Distributor Type </td>";
            strHTML = strHTML + "<td valign='middle' > Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Total No. of Cash In</td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Cash In amount</td>";
            strHTML = strHTML + "<td valign='middle' >Gross Distributor Commission (Cash In)</td>";
            strHTML = strHTML + "<td valign='middle' >Total No. of Cash out</td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Cash out amount</td>";
            strHTML = strHTML + "<td valign='middle' >Gross Distributor Commission (Cash Out)</td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Approved</td>";
            strHTML = strHTML + "<td valign='middle' >Total Registration Commission </td>";
            strHTML = strHTML + "<td valign='middle' >Net Distributor Commission</td>";
            strHTML = strHTML + "</tr>";
            if (dtsAccntBalance.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["RANK_TITEL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DISTRIBUTOR_ADD"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DISTRIBUTOR_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CN_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CN_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RG_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_RG_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_RG_COMMISSION"].ToString() + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";  */

#endregion

            strHTML = strHTML + "<table border=\"1\" width=\"60%\">";
            strHTML = strHTML + "<tr style='border-right:none;font-size:12px;font-weight:bold;'>";
            strHTML = strHTML + "<td valign='middle' >SerialNo</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor MYCash Account Number</td>";
            strHTML = strHTML + "<td valign='middle' > Distributor Type </td>";
            strHTML = strHTML + "<td valign='middle' > Distributor Address </td>";

            strHTML = strHTML + "<td valign='middle' > Distributor District </td>";

            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Total No. of Cash In</td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Cash In amount</td>";
            strHTML = strHTML + "<td valign='middle' >Gross Distributor Commission (Cash In)</td>";
            strHTML = strHTML + "<td valign='middle' >Total No. of Cash out</td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Cash out amount</td>";
            strHTML = strHTML + "<td valign='middle' >Gross Distributor Commission (Cash Out)</td>";
            strHTML = strHTML + "<td valign='middle' >Total No of Bank Deposit </td>";
            strHTML = strHTML + "<td valign='middle' >Total Bank Deposit Amount </td>";
            strHTML = strHTML + "<td valign='middle' >Gross Distributor Commission(Bank Deposit) </td>";

            strHTML = strHTML + "<td valign='middle' >Total No of BIll Pay </td>";
            strHTML = strHTML + "<td valign='middle' >Total BIll Pay Amount </td>";
            strHTML = strHTML + "<td valign='middle' >Gross Distributor Commission(BIll Pay) </td>";


            strHTML = strHTML + "<td valign='middle' >Total Customer Registered</td>";
            strHTML = strHTML + "<td valign='middle' >Total Customer Approved</td>";
            strHTML = strHTML + "<td valign='middle' >Total Registration Commission </td>";
            strHTML = strHTML + "<td valign='middle' >Net Distributor Commission</td>";
            strHTML = strHTML + "</tr>";
            if (dtsAccntBalance.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_ADD"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CN_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CN_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CN_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CCT_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BD_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BD_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BD_COMMISSION"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["UBP_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["UBP_COMMISSION"].ToString() + " </td>";


                    strHTML = strHTML + " <td > '" + prow["REGIS_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RG_APRV_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_RG_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_RG_COMMISSION"].ToString() + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            
            //-------------------------------------- String convert to excel File ------------------
            string strPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string strNow = DateTime.Now.ToString();
            strNow = strNow.Replace("/", "_");
            strNow = strNow.Replace(":", "");
            strNow = strNow.Replace(" ", "_");

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            // Write the rendered content to a file.
            //  string renderedGridView = sw.ToString();
            //string str = @"D:\MPAY\TelCOM\EXPORT_REGISTRATION_RPT\" + fileName + "_" + strNow + ".xls";
            //string str = @"D:\MPAY\TelCOM_ADM\EXPORT_REGISTRATION_RPT\" + fileName + "_" + strNow + ".xls";
            string str = @"D:\MPAY\TelCOM_ADM\TelCOM_ADM\EXPORT_REGISTRATION_RPT\" + fileName + "_" + strNow + ".xls";
            
            //string str = @"D:\TESTING\TelComNew\EXPORT_REGISTRATION_RPT\" + fileName + "_" + strNow + ".xls";
            System.IO.File.WriteAllText(str, strHTML);
            //-------------------------------------------------------------------------------------------
            //-------------------------- Insert Report Info -----------------------------
            string strReportFileName = fileName + "_" + strNow + ".xls";
            string strMesage = objServiceHandler.SaveRegRptInfo(strReportFileName, Session["UserLoginName"].ToString(), dtpFromDate.DateString,dtpToDate.DateString, "DistCommStatemet");

            lblMsg.Text = "Report Generated Successfully.";
            sdsRegReport.DataBind();
            gdvReportDownload.DataBind();
            btnReport.Enabled = true;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Button btndwl = (Button)sender;
            GridViewRow grdRow = (GridViewRow)btndwl.NamingContainer;
            string strFileName = "";
            strFileName = gdvReportDownload.Rows[grdRow.RowIndex].Cells[1].Text;
            string filePath = Server.MapPath("~/EXPORT_REGISTRATION_RPT") + System.IO.Path.DirectorySeparatorChar + strFileName;
            Response.TransmitFile((filePath));
            Response.ContentType = "xls/xlsx/CSV";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + strFileName + "\"");
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "File not found.";
        }
    }
}
