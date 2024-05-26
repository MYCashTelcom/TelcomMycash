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

public partial class COMMON_frmTopupReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    private string strUserName = string.Empty;
    private string strPassword = string.Empty;
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
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        string strSql = "", strAdditionalSQL = "";
        if (ddlOwnerCode.SelectedValue == "All")
        {
            strAdditionalSQL = " ";
        }
        else
        {
            strAdditionalSQL = " AND OWNER_CODE='" + ddlOwnerCode.SelectedValue.ToString() + "'";
        }

        if (rbtnAllDateRange.SelectedValue == "0")
        {
            if (rbtnSelectState.SelectedValue == "S")
            {                
                strSql = "  SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE,"
                      + " SSL_VRG_UNIQUE_ID,TRAN_AMOUNT,  REQUEST_STATUS,SUCCESSFUL_STATUS,OPERATOR_CODE,SSL_CREATE_RECHAGE_STATUS, "
                      + " SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,  SSL_INT_MESSAGE,SSL_FINAL_STATUS,"
                      + " SSL_FINAL_MESSAGE,REVERSE_STATUS,OWNER_CODE FROM VW_TOPUP_TRANSACTION WHERE  REVERSE_STATUS='N' AND RESUBMIT_STATUS='N' "
                      + " AND  ALL_FINAL_STATUS='S' " + strAdditionalSQL + " AND  SSL_FINAL_STATUS IS NOT NULL  ORDER BY TRAN_DATE DESC ";
            }
            else if (rbtnSelectState.SelectedValue == "U")
            {               
                strSql = " SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE, "
                     + " SSL_VRG_UNIQUE_ID,TRAN_AMOUNT,REQUEST_STATUS, SUCCESSFUL_STATUS,OPERATOR_CODE,"
                     + " SSL_CREATE_RECHAGE_STATUS, SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,SSL_INT_MESSAGE,SSL_FINAL_STATUS,"
                     + " SSL_FINAL_MESSAGE,REVERSE_STATUS,OWNER_CODE  FROM VW_TOPUP_TRANSACTION WHERE  REVERSE_STATUS='N' AND "
                     + " RESUBMIT_STATUS='N'  AND   ALL_FINAL_STATUS='F' " + strAdditionalSQL + " AND SSL_FINAL_STATUS  IS NOT NULL "
                     + " ORDER BY TRAN_DATE DESC ";

            }
            else if (rbtnSelectState.SelectedValue == "R")
            {
                strSql = " SELECT TOPUP_TRAN_ID, REQUEST_ID, TRAN_DATE, SOURCE_ACCNT_NO, SUBSCRIBER_MOBILE_NO, SUBSCRIBER_TYPE, SSL_VRG_UNIQUE_ID, TRAN_AMOUNT, REQUEST_STATUS, SUCCESSFUL_STATUS, OPERATOR_CODE, SSL_CREATE_RECHAGE_STATUS, SSL_CREATE_MESSAGE, SSL_INT_RECHAGE_STATUS, SSL_INT_MESSAGE, SSL_FINAL_STATUS, SSL_FINAL_MESSAGE, DECODE(REVERSE_STATUS,'R','Reversed') REVERSE_STATUS, REVERSE_SMS_STATUS, OWNER_CODE, (SELECT MAX(CAS_TRAN_DATE) FROM BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION WHERE REQUEST_ID = TT.REQUEST_ID AND ROWNUM = 1) CAS_TRAN_DATE FROM VW_TOPUP_TRANSACTION TT WHERE REVERSE_STATUS='R' " + strAdditionalSQL + " ORDER BY TRAN_DATE DESC ";
               
            }
            else if (rbtnSelectState.SelectedValue == "RS")
            {
                strSql = " SELECT * FROM VW_TOPUP_TRANSACTION WHERE RESUBMIT_STATUS ='Y' " + strAdditionalSQL + " ORDER BY TRAN_DATE DESC ";              
            }
        }
        else if (rbtnAllDateRange.SelectedValue == "1")
        {
            if (rbtnSelectState.SelectedValue == "S")
            {
                if (ddlOwnerCode.SelectedValue.ToString() == "BL")
                {
                    strSql = "  SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE,"
                           + " SSL_VRG_UNIQUE_ID,TRAN_AMOUNT,  REQUEST_STATUS,SUCCESSFUL_STATUS,OPERATOR_CODE,SSL_CREATE_RECHAGE_STATUS, "
                           + " SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,  SSL_INT_MESSAGE,SSL_FINAL_STATUS,SSL_FINAL_MESSAGE,"
                           + " REVERSE_STATUS,OWNER_CODE FROM VW_TOPUP_TRANSACTION WHERE  REVERSE_STATUS='N' AND RESUBMIT_STATUS='N' "
                           + " AND SSL_CREATE_MESSAGE LIKE '%Successful%' " + strAdditionalSQL + " AND TRUNC(TRAN_DATE) "
                           + " BETWEEN TO_DATE('" + dptFromDate.DateString + "','dd/mm/yyyy ') AND  TO_DATE('" + dtpToDate.DateString
                           + "','dd/mm/yyyy') ORDER BY TRAN_DATE DESC ";
                }
                else
                {
                    strSql = "  SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE,"
                           + " SSL_VRG_UNIQUE_ID,TRAN_AMOUNT,  REQUEST_STATUS,SUCCESSFUL_STATUS,OPERATOR_CODE,SSL_CREATE_RECHAGE_STATUS, "
                           + " SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,  SSL_INT_MESSAGE,SSL_FINAL_STATUS,SSL_FINAL_MESSAGE,"
                           + " REVERSE_STATUS,OWNER_CODE FROM VW_TOPUP_TRANSACTION WHERE  REVERSE_STATUS='N' AND RESUBMIT_STATUS='N' "
                           + " AND  ALL_FINAL_STATUS='S' AND  SSL_FINAL_STATUS IS NOT NULL " + strAdditionalSQL + " AND TRUNC(TRAN_DATE) "
                           + " BETWEEN TO_DATE('" + dptFromDate.DateString + "','dd/mm/yyyy ') AND  TO_DATE('" + dtpToDate.DateString
                           + "','dd/mm/yyyy') ORDER BY TRAN_DATE DESC ";
                }
            }
            else if (rbtnSelectState.SelectedValue == "U")
            {
               strSql = " SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE, "
                      + " SSL_VRG_UNIQUE_ID,TRAN_AMOUNT,REQUEST_STATUS, SUCCESSFUL_STATUS,OPERATOR_CODE,"
                      + " SSL_CREATE_RECHAGE_STATUS, SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,SSL_INT_MESSAGE,SSL_FINAL_STATUS,"
                      + " SSL_FINAL_MESSAGE,REVERSE_STATUS,OWNER_CODE  FROM VW_TOPUP_TRANSACTION WHERE  REVERSE_STATUS='N' AND "
                      + " RESUBMIT_STATUS='N'  AND   ALL_FINAL_STATUS='F' AND SSL_FINAL_STATUS  IS NOT NULL " + strAdditionalSQL + " "
                      + " AND TRUNC(TRAN_DATE) BETWEEN TO_DATE('" + dptFromDate.DateString + " ','dd/mm/yyyy') AND TO_DATE('" + dtpToDate.DateString
                      + "','dd/mm/yyyy') ORDER BY TRAN_DATE DESC ";               
            }
            else if (rbtnSelectState.SelectedValue == "R")
            {
                //strSql = " SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE,"
                //       + " SSL_VRG_UNIQUE_ID,TRAN_AMOUNT,REQUEST_STATUS,SUCCESSFUL_STATUS,OPERATOR_CODE,"
                //       + " SSL_CREATE_RECHAGE_STATUS, SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,"
                //       + " SSL_INT_MESSAGE,SSL_FINAL_STATUS,SSL_FINAL_MESSAGE,REVERSE_STATUS, REVERSE_SMS_STATUS,OWNER_CODE "
                //       + " FROM VW_TOPUP_TRANSACTION WHERE REVERSE_STATUS='R' " + strAdditionalSQL + " AND  TRUNC(TRAN_DATE) BETWEEN TO_DATE('" + dptFromDate.DateString
                //       + " ','dd/mm/yyyy') AND TO_DATE('" + dtpToDate.DateString + "','dd/mm/yyyy') ORDER BY TRAN_DATE DESC";

                strSql = " SELECT TOPUP_TRAN_ID, REQUEST_ID, TRAN_DATE, SOURCE_ACCNT_NO, SUBSCRIBER_MOBILE_NO, SUBSCRIBER_TYPE, SSL_VRG_UNIQUE_ID, TRAN_AMOUNT, REQUEST_STATUS, SUCCESSFUL_STATUS, OPERATOR_CODE, SSL_CREATE_RECHAGE_STATUS, SSL_CREATE_MESSAGE, SSL_INT_RECHAGE_STATUS, SSL_INT_MESSAGE, SSL_FINAL_STATUS, SSL_FINAL_MESSAGE, DECODE(REVERSE_STATUS,'R','Reversed') REVERSE_STATUS, REVERSE_SMS_STATUS, OWNER_CODE, (SELECT MAX(CAS_TRAN_DATE) FROM BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION WHERE REQUEST_ID = TT.REQUEST_ID) CAS_TRAN_DATE FROM VW_TOPUP_TRANSACTION TT WHERE REVERSE_STATUS='R' " + strAdditionalSQL + " AND  TRUNC(TRAN_DATE) BETWEEN TO_DATE('" + dptFromDate.DateString + " ','dd/mm/yyyy') AND TO_DATE('" + dtpToDate.DateString + "','dd/mm/yyyy') ORDER BY CAS_TRAN_DATE DESC ";                   
            }
            else if (rbtnSelectState.SelectedValue == "RS")
            {
                strSql = " SELECT * FROM VW_TOPUP_TRANSACTION WHERE RESUBMIT_STATUS ='Y' " + strAdditionalSQL + " "
                       + " AND  TRUNC(TRAN_DATE) BETWEEN TO_DATE('" + dptFromDate.DateString + "','dd/mm/yyyy') "
                       + " AND TO_DATE('" + dtpToDate.DateString + "','dd/mm/yyyy') ORDER BY TRAN_DATE DESC ";               
            }
        }
        string strHTML = "";
        DataSet oDS = new DataSet();
        decimal dmlTotalAmt = 0;
        if (!strSql.Equals(""))
        {
            oDS = objServiceHandler.ExecuteQuery(strSql); 
            if (rbtnSelectState.SelectedValue == "S")
            {
                string filename = "TopupSuccessfullReport";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none'><h2 align=center>Successfull Topup Report </h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=9 align= style='border-right:none'><h5 align=center>Date Between From Date  " + dptFromDate.DateString + "  To Date  " + dtpToDate.DateString + "  </h5></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Serial No</td>";
                strHTML = strHTML + "<td valign='middle' >Topup TranID</td>";
                strHTML = strHTML + "<td valign='middle' >Requist ID</td>";
                strHTML = strHTML + "<td valign='middle' >Sender</td>";
                strHTML = strHTML + "<td valign='middle' >Receiver</td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "<td valign='middle' >SSL Unique ID</td>";
                strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
                strHTML = strHTML + "<td valign='middle' >Response Code</td>";
                strHTML = strHTML + "</tr>";
                if (oDS.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in oDS.Tables["Table1"].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOPUP_TRAN_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUBSCRIBER_MOBILE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TRAN_DATE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > " + prow["TRAN_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SSL_VRG_UNIQUE_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SSL_FINAL_STATUS"].ToString() + " </td>"; 
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                        dmlTotalAmt = dmlTotalAmt + Convert.ToDecimal(prow["TRAN_AMOUNT"].ToString());
                    }
                }
                object sumObject;
                sumObject = oDS.Tables["Table1"].Compute("Sum(TRAN_AMOUNT)", "");
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + "</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + "</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " +Convert.ToString(dmlTotalAmt) + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
            }
            else if (rbtnSelectState.SelectedValue == "U")
            {
                string filename = "TopupUnSuccessfullReport";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none'><h2 align=center>Unsuccessfull Topup Report </h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=10 align= style='border-right:none'><h5 align=center>Date Between From Date " + dptFromDate.DateString + "  To Date " + dtpToDate.DateString + "  </h5></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Serial No</td>";
                strHTML = strHTML + "<td valign='middle' >Topup TranID</td>";
                strHTML = strHTML + "<td valign='middle' >Requist ID</td>";
                strHTML = strHTML + "<td valign='middle' >Sender</td>";
                strHTML = strHTML + "<td valign='middle' >Receiver</td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Final Message</td>";
                strHTML = strHTML + "<td valign='middle' >SSL Unique ID</td>";
                strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
                strHTML = strHTML + "<td valign='middle' >Response Code</td>";
                strHTML = strHTML + "</tr>";
                if (oDS.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in oDS.Tables["Table1"].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOPUP_TRAN_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUBSCRIBER_MOBILE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TRAN_DATE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > " + prow["TRAN_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["SSL_FINAL_MESSAGE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SSL_VRG_UNIQUE_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SSL_FINAL_STATUS"].ToString() + " </td>"; 
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                        dmlTotalAmt = dmlTotalAmt + Convert.ToDecimal(prow["TRAN_AMOUNT"].ToString());
                    }
                }
                object sumObject;
                sumObject = oDS.Tables["Table1"].Compute("Sum(TRAN_AMOUNT)", "");
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + "</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + "</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + Convert.ToString(dmlTotalAmt) + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
            }
            else if (rbtnSelectState.SelectedValue == "R")
            {
                string filename = "TopupReversedReport";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none'><h2 align=center>Reversed Topup Report </h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=13 align= style='border-right:none'><h5 align=center>Date Between From Date  " + dptFromDate.DateString + "  To Date  " + dtpToDate.DateString + "  </h5></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Serial No</td>";
                strHTML = strHTML + "<td valign='middle' >Topup TranID</td>";
                strHTML = strHTML + "<td valign='middle' >Requist ID</td>";
                strHTML = strHTML + "<td valign='middle' >Sender</td>";
                strHTML = strHTML + "<td valign='middle' >Receiver</td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Final Message</td>";
                strHTML = strHTML + "<td valign='middle' >SSL Unique ID</td>";
                strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
                strHTML = strHTML + "<td valign='middle' >Response Code</td>";
                strHTML = strHTML + "<td valign='middle' >Reverse Status</td>";
                strHTML = strHTML + "<td valign='middle' >Reverse Date</td>";
                strHTML = strHTML + "</tr>";
                if (oDS.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in oDS.Tables["Table1"].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOPUP_TRAN_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUBSCRIBER_MOBILE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRAN_DATE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > " + prow["TRAN_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["SSL_FINAL_MESSAGE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SSL_VRG_UNIQUE_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SSL_FINAL_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REVERSE_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CAS_TRAN_DATE"].ToString() + " </td>"; 
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                        dmlTotalAmt = dmlTotalAmt + Convert.ToDecimal(prow["TRAN_AMOUNT"].ToString());
                    }
                }
                object sumObject;
                sumObject = oDS.Tables["Table1"].Compute("Sum(TRAN_AMOUNT)", "");
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + "</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + Convert.ToString(dmlTotalAmt) + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
            }
            else if (rbtnSelectState.SelectedValue == "RS")
            {
                string filename = "TopupResubmitReport";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none'><h2 align=center>Resubmit Topup Report </h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=10 align= style='border-right:none'><h5 align=center>Date Between From Date  " + dptFromDate.DateString + "  To Date " + dtpToDate.DateString + "  </h5></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Serial No</td>";
                strHTML = strHTML + "<td valign='middle' >Topup TranID</td>";
                strHTML = strHTML + "<td valign='middle' >Requist ID</td>";
                strHTML = strHTML + "<td valign='middle' >Sender</td>";
                strHTML = strHTML + "<td valign='middle' >Receiver</td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Final Message</td>";
                strHTML = strHTML + "<td valign='middle' >SSL Unique ID</td>";
                strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
                strHTML = strHTML + "<td valign='middle' >Response Code</td>";
                strHTML = strHTML + "</tr>";
                if (oDS.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in oDS.Tables["Table1"].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TOPUP_TRAN_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SUBSCRIBER_MOBILE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["TRAN_DATE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > " + prow["TRAN_AMOUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["SSL_FINAL_MESSAGE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SSL_VRG_UNIQUE_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["SSL_FINAL_STATUS"].ToString() + " </td>"; 
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;
                        dmlTotalAmt = dmlTotalAmt + Convert.ToDecimal(prow["TRAN_AMOUNT"].ToString());
                    }
                }
                object sumObject;
                sumObject = oDS.Tables["Table1"].Compute("Sum(TRAN_AMOUNT)", "");
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + "</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + Convert.ToString(dmlTotalAmt) + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
            }
        }
    }
    protected void rbtnSelectState_SelectedIndexChanged(object sender, EventArgs e)
    {   
    }
    protected void btnTopUpAccStm_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string strSqlPreBal = "";

            strSql = " SELECT   DISTINCT CT.CAS_ACC_ID, C.CAS_ACC_NO, CL.CLINT_NAME CI_CLIENT_NAME, CL.CLINT_ADDRESS1 CI_PRE_ADDRESS_1, "
                     + " CL.CLINT_ADDRESS2 CI_PRE_ADDRESS_2, CT.CAS_TRAN_DATE, ROUND (NVL (DECODE (CT.CAS_TRAN_TYPE, 'D', CAS_TRAN_AMT, ''), 0), 4) AS DEBIT, "
                     + " '" + dtpAccFDate.DateString + "' F_DATE, '" + dtpAccToDate.DateString + "' T_DATE, ROUND (DECODE (CT.CAS_TRAN_TYPE, 'C', CAS_TRAN_AMT, ''), 4) AS CREDIT, "
                     + " CT.CAS_TRAN_TYPE, (UPPER (ASR.ACCESS_CODE) || '  ' || CT.CAS_TRAN_DESC) AS CAS_TRAN_DESC,COA.ACC_TYPE, CT.CAS_TRAN_STATUS, CT.REQUEST_ID  "
                     + " FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST C, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CT, APSNG101.ALL_SERVICE_REQUEST ASR, "
                     + " APSNG101.CLIENT_BANK_ACCOUNT CBA, BDMIT_ERP_101.CAS_ACCOUNT_TYPE AT, BDMIT_ERP_101.GL_CHART_OF_ACC COA, APSNG101.ACCOUNT_LIST AL, APSNG101.CLIENT_LIST CL "
                     + " WHERE CT.CAS_ACC_ID = C.CAS_ACC_ID AND C.CAS_ACC_NO = CBA.CLINT_BANK_ACC_LOGIN AND CBA.ACCNT_ID = AL.ACCNT_ID "
                     + " AND AL.CLINT_ID = CL.CLINT_ID AND C.CAS_ACC_NO = '017491762731' AND CT.REQUEST_ID = ASR.REQUEST_ID(+) "
                     + " AND C.CAS_ACC_TYPE_ID = AT.CAS_ACC_TYPE_ID AND AT.ACC_INT_ID = COA.ACC_INT_ID "
                     + " AND TO_CHAR (TO_DATE (CT.CAS_TRAN_DATE, 'dd/mm/yyyy HH24:MI:SS')) BETWEEN TO_DATE('" + dtpAccFDate.DateString + "') AND  TO_DATE('" + dtpAccToDate.DateString + "')"
                     + " ORDER BY CT.CAS_TRAN_DATE ASC";

            strSqlPreBal = " SELECT ROUND (BDMIT_ERP_101.MBL_PRE_BAL ('017491762731', '" + dtpAccFDate.DateString + "'), 4) PRE_BAL FROM DUAL ";
            string strPreviousBalance = objServiceHandler.GetRankCount(strSqlPreBal);

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "TopupAccStm_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Top Up Account Statemant Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpAccFDate.DateString + "' To '" + dtpAccToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";

            strHTML = strHTML + "</tr>";

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
            strHTML = strHTML + "<td valign='middle' >Previous Balance: '" + strPreviousBalance + "' </td>";
            strHTML = strHTML + " </tr>";

            
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Account Id</td>";
            strHTML = strHTML + "<td valign='middle' >Account NO</td>";
            strHTML = strHTML + "<td valign='middle' >Client Name</td>";
            strHTML = strHTML + "<td valign='middle' >Pre Addr 1</td>";
            strHTML = strHTML + "<td valign='middle' >Pre Addr 2</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >From Date</td>";
            strHTML = strHTML + "<td valign='middle' >To Date</td>";
            strHTML = strHTML + "<td valign='middle' >Debit Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Credit Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Trx Type</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Transaction Description</td>";
            strHTML = strHTML + "<td valign='middle' >Account Type</td>";
            strHTML = strHTML + "<td valign='middle' >Cash Trx Status</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_ACC_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_ACC_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CI_CLIENT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CI_PRE_ADDRESS_1"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CI_PRE_ADDRESS_2"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["F_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["T_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DEBIT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CREDIT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_TYPE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_DESC"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACC_TYPE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
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

            SaveAuditInfo("Preview", "TopupAccStm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



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
