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

public partial class USSD_frmCustomerList : System.Web.UI.Page 
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
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
    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        if (ddlCustomerType.SelectedValue == "0")
        {
            GenerateCustomerListReport();
        }
        else if (ddlCustomerType.SelectedValue == "1")
        {
            GenerateCustomerValueReport();
        }
    }
    private void GenerateCustomerValueReport()
    {
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        DataSet oDS = new DataSet();
        string strHTML = "", strSql = "";
        string filename = "Customer_Details";

        strSql = " SELECT AL.ACCNT_NO,CL.CLINT_NAME,AR.RANK_TITEL,CL.CREATION_DATE,MD.DISTRICT_NAME,"
               + " CL.CLINT_ADDRESS1,CL.CLINT_ADDRESS2, ASD.AGENT_MOBILE_NO,CLA.CLINT_NAME AGENT_NAME "
               + ",CLA.CLINT_ADDRESS1,CLA.CLINT_ADDRESS2 AGENT_ADDRESS,MDA.DISTRICT_NAME AGENT_DISTRICT "
               + " FROM SERVICE_REQUEST SR,ACCOUNT_LIST AL,ACCOUNT_RANK AR,CLIENT_LIST CL,"
               + " MANAGE_THANA MT,MANAGE_DISTRICT MD,ACCOUNT_SERIAL_DETAIL ASD ,CLIENT_LIST CLA,"
               + " MANAGE_THANA MTA,MANAGE_DISTRICT MDA WHERE SR.REQUEST_PARTY=AL.ACCNT_MSISDN "
               + " AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.CLINT_ID=CL.CLINT_ID "
               + " AND SR.RECEIPENT_PARTY='AIRTEL_USSD' AND AL.ACCNT_STATE='A' AND AR.GRADE='4' "
               + " AND CL.THANA_ID=MT.THANA_ID(+) AND MD.DISTRICT_ID(+)=MT.DISTRICT_ID AND "
               + " AL.ACCNT_MSISDN=ASD.CUSTOMER_MOBILE_NO(+) AND ASD.AGENT_MOBILE_NO=CLA.CLINT_MOBILE "
               + " AND CLA.THANA_ID=MTA.THANA_ID(+) AND MDA.DISTRICT_ID(+)=MTA.DISTRICT_ID AND "
               + " UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2))='REG' "
               + " AND ACCNT_NO IN (SELECT CAL.CAS_ACC_NO FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,"
               + " BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE CAL.CAS_ACC_ID=CAT.CAS_ACC_ID "
               + " AND CAL.CAS_ACC_NO IS NOT NULL AND TO_CHAR(TO_DATE(CAT.CAS_TRAN_DATE,'dd-mm-YYYY'))"
               + " BETWEEN TO_DATE('" + dptFromDate.DateString + "') AND TO_DATE('" + dtpToDate.DateString + "'))";

        oDS = objServiceHandler.GetDataSet(strSql);

        SaveAuditInfo("View", "Customer Value");
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none'><h2 align=center>Customer Details</h2></td></tr>";
        strHTML = strHTML + " </table>";

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td align='center'><b>SN</b></td>";
        strHTML = strHTML + "<td align='center'><b>Customer Account No</b></td>";
        strHTML = strHTML + "<td align='center'><b>Customer Name</b></td>";
        strHTML = strHTML + "<td align='center'><b>Present Address</b></td>";
        strHTML = strHTML + "<td align='middle'><b>Permanent Address</b></td>";
        strHTML = strHTML + "<td align='center'><b>Creation Date</b></td>";
        strHTML = strHTML + "<td align='center'><b>District Name</b></td>";

        strHTML = strHTML + "<td align='center'><b>Agent Mobile No</b></td>";
        strHTML = strHTML + "<td align='center'><b>Agent Name</b></td>";
        strHTML = strHTML + "<td align='middle'><b>Agent Address</b></td>";
        strHTML = strHTML + "<td align='middle'><b>Agent District</b></td>";
        strHTML = strHTML + "</tr>";

        //-------------------------------------------------
        if (oDS.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 0;
            foreach (DataRow prow in oDS.Tables["Table1"].Rows)
            {
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > " + prow["ACCNT_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_ADDRESS1"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_ADDRESS2"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CREATION_DATE"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["DISTRICT_NAME"].ToString() + "</td>";

                strHTML = strHTML + " <td > '" + prow["AGENT_MOBILE_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["AGENT_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["AGENT_ADDRESS"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["AGENT_DISTRICT"].ToString() + "</td>";

                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;
            }
        }
        //-------------------------------------------------

        strHTML = strHTML + "<tr>";
        strHTML = strHTML + " <td > " + "" + "</td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + "</tr>";

        strHTML = strHTML + " </table>";
        clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
    }
    private void GenerateCustomerListReport()
    {
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        DataSet oDS = new DataSet();
        string strHTML = "", strSql = "";
        string filename = "Customer_Details";

        strSql = " SELECT AL.ACCNT_NO,CL.CLINT_NAME,AR.RANK_TITEL,CL.CREATION_DATE,MD.DISTRICT_NAME,"
               + " CL.CLINT_ADDRESS1,CL.CLINT_ADDRESS2,ASD.AGENT_MOBILE_NO,CLA.CLINT_NAME AGENT_NAME ,"
               + " CLA.CLINT_ADDRESS1,CLA.CLINT_ADDRESS2 AGENT_ADDRESS,MDA.DISTRICT_NAME AGENT_DISTRICT "
               + " FROM SERVICE_REQUEST SR,ACCOUNT_LIST AL,ACCOUNT_RANK AR,CLIENT_LIST CL ,"
               + " MANAGE_THANA MT,MANAGE_DISTRICT MD,ACCOUNT_SERIAL_DETAIL ASD,CLIENT_LIST CLA,"
               + " MANAGE_THANA MTA,MANAGE_DISTRICT MDA WHERE SR.REQUEST_PARTY=AL.ACCNT_MSISDN AND "
               + " AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.CLINT_ID=CL.CLINT_ID "
               + " AND SR.RECEIPENT_PARTY='AIRTEL_USSD' AND AL.ACCNT_STATE='A' AND AR.GRADE='4' "
               + " AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2))='REG'"
               + " AND CL.THANA_ID=MT.THANA_ID(+) AND MD.DISTRICT_ID(+)=MT.DISTRICT_ID "
               + " AND AL.ACCNT_MSISDN=ASD.CUSTOMER_MOBILE_NO(+) AND ASD.AGENT_MOBILE_NO=CLA.CLINT_MOBILE "
               + " AND CLA.THANA_ID=MTA.THANA_ID(+) AND MDA.DISTRICT_ID(+)=MTA.DISTRICT_ID "
               + " AND TO_CHAR(TO_DATE(CL.CREATION_DATE,'dd-mm-YYYY')) BETWEEN TO_DATE('" + dptFromDate.DateString+ "') AND TO_DATE('" + dtpToDate.DateString + "')";


        oDS = objServiceHandler.GetDataSet(strSql);
        SaveAuditInfo("View","Agent List Value");
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<TR><td COLSPAN=11 align=center style='border-right:none'><h2 align=center>Customer Details</h2></td></TR>";
        strHTML = strHTML + " </table>";

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td align='center'><b>SN</b></td>";
        strHTML = strHTML + "<td align='center'><b>Customer Account No</b></td>";
        strHTML = strHTML + "<td align='center'><b>Customer Name</b></td>";
        strHTML = strHTML + "<td align='center'><b>Present Address</b></td>";
        strHTML = strHTML + "<td align='middle'><b>Permanent Address</b></td>";
        strHTML = strHTML + "<td align='center'><b>Creation Date</b></td>";
        strHTML = strHTML + "<td align='center'><b>District Name</b></td>";
        
        strHTML = strHTML + "<td align='center'><b>Agent Mobile No</b></td>";
        strHTML = strHTML + "<td align='center'><b>Agent Name</b></td>";
        strHTML = strHTML + "<td align='middle'><b>Agent Address</b></td>";
        strHTML = strHTML + "<td align='middle'><b>Agent District</b></td>";
        strHTML = strHTML + "</tr>";

        //-------------------------------------------------
        if (oDS.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 0;
            foreach (DataRow prow in oDS.Tables["Table1"].Rows)
            {
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > " + prow["ACCNT_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_ADDRESS1"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_ADDRESS2"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CREATION_DATE"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["DISTRICT_NAME"].ToString() + "</td>";

                strHTML = strHTML + " <td > '" + prow["AGENT_MOBILE_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["AGENT_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["AGENT_ADDRESS"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["AGENT_DISTRICT"].ToString() + "</td>";
                                
                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;
            }
        }
        //-------------------------------------------------
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + " <td > " + "" + "</td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + "</tr>";

        strHTML = strHTML + " </table>";
        clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
    }
    private void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
