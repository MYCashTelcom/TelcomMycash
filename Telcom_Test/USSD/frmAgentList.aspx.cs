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

public partial class USSD_frmAgentList : System.Web.UI.Page
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
        if (ddlAgentType.SelectedValue == "0")
        {
            GenerateAgentListReport();
        }
        else if (ddlAgentType.SelectedValue == "1")
        {
            GenerateAgentListValueReport();
        }
    }

    private void GenerateAgentListValueReport()
    {
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        DataSet oDS = new DataSet();
        string strHTML = "", strSql = "";
        string filename = "Agent_Valued";

        strSql = " SELECT AL.ACCNT_NO,CL.CLINT_NAME,AR.RANK_TITEL,CL.CREATION_DATE,MD.DISTRICT_NAME,CL.CLINT_ADDRESS1,"
               + " CL.CLINT_ADDRESS2 FROM SERVICE_REQUEST SR,ACCOUNT_LIST AL,ACCOUNT_RANK AR,CLIENT_LIST CL,"
               + " MANAGE_THANA MT,MANAGE_DISTRICT MD WHERE SR.REQUEST_PARTY=AL.ACCNT_MSISDN AND "
               + " AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.CLINT_ID=CL.CLINT_ID AND SR.RECEIPENT_PARTY"
               + " ='AIRTEL_USSD' AND AL.ACCNT_STATE='A' AND AR.GRADE='3' AND CL.THANA_ID=MT.THANA_ID(+) "
               + " AND MD.DISTRICT_ID(+)=MT.DISTRICT_ID AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR "
               + "(SR.REQUEST_TEXT, '*', 2) - 2))='REG' AND ACCNT_NO IN(SELECT CAL.CAS_ACC_NO FROM "
               + "BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT "
               + " WHERE CAL.CAS_ACC_ID=CAT.CAS_ACC_ID AND CAL.CAS_ACC_NO IS NOT NULL AND "
               + "TO_CHAR(TO_DATE(CAT.CAS_TRAN_DATE,'dd-mm-YYYY')) BETWEEN TO_DATE('"+dptFromDate.DateString+"') AND TO_DATE('"+dtpToDate.DateString+"'))";

        oDS = objServiceHandler.GetDataSet(strSql);
        SaveAuditInfo("View","Agent List Value");
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<TR><td COLSPAN=7 align=center style='border-right:none'><h2 align=center>Agent Valued</h2></td></TR>";
        strHTML = strHTML + " </table>";

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td align='center'><b>SN</b></td>";
        strHTML = strHTML + "<td align='center'><b>Agent Account No</b></td>";
        strHTML = strHTML + "<td align='center'><b>Agent Name</b></td>";
        strHTML = strHTML + "<td align='center'><b>Creation Date</b></td>";
        strHTML = strHTML + "<td align='center'><b>District Name</b></td>";
        strHTML = strHTML + "<td align='center'><b>Present Address</b></td>";
        strHTML = strHTML + "<td align='middle'><b>Permanent Address</b></td>";
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
                strHTML = strHTML + " <td > " + prow["CREATION_DATE"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["DISTRICT_NAME"].ToString() + "</td>";
                strHTML = strHTML + " <td > " + prow["CLINT_ADDRESS1"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_ADDRESS2"].ToString() + " </td>";
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

        strHTML = strHTML + "</tr>";

        strHTML = strHTML + " </table>";
        clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
    }

    private void GenerateAgentListReport()
    {
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        DataSet oDS = new DataSet();
        string strHTML = "", strSql="";
        string filename = "MPay_Agent";

        strSql = " SELECT SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TEXT,AL.ACCNT_NO,CL.CLINT_NAME,CL.CREATION_DATE, "
               + " MD.DISTRICT_NAME,CL.CLINT_ADDRESS1,CL.CLINT_ADDRESS2 FROM SERVICE_REQUEST SR,ACCOUNT_LIST AL,"
               + " CLIENT_LIST CL,MANAGE_THANA MT,MANAGE_DISTRICT MD,ACCOUNT_RANK AR WHERE SR.RECEIPENT_PARTY='AIRTEL_USSD' "
               + " AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2))='REG' AND AL.ACCNT_STATE='A' "
               + " AND CL.CLINT_MOBILE=SR.REQUEST_PARTY AND AL.CLINT_ID=CL.CLINT_ID AND AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID"
               + " AND CL.THANA_ID=MT.THANA_ID(+) AND MD.DISTRICT_ID(+)=MT.DISTRICT_ID "
               + " AND TO_CHAR(TO_DATE(CL.CREATION_DATE,'dd-mm-YYYY')) BETWEEN TO_DATE('"+dptFromDate.DateString+"') AND TO_DATE('"+dtpToDate.DateString+"') AND AR.GRADE='3' ";

        oDS = objServiceHandler.GetDataSet(strSql);
        SaveAuditInfo("View", "Agent List");
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<TR><td COLSPAN=7 align=center style='border-right:none'><h2 align=center>Agent List</h2></td></TR>";
        strHTML = strHTML + " </table>";

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td align='center'><b>SN</b></td>";
        strHTML = strHTML + "<td align='center'><b>Agent Account No</b></td>";
        strHTML = strHTML + "<td align='center'><b>Agent Name</b></td>";
        strHTML = strHTML + "<td align='center'><b>Creation Date</b></td>";
        strHTML = strHTML + "<td align='center'><b>District Name</b></td>";
        strHTML = strHTML + "<td align='center'><b>Present Address</b></td>";
        strHTML = strHTML + "<td align='middle'><b>Permanent Address</b></td>";
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
                strHTML = strHTML + " <td > " + prow["CREATION_DATE"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["DISTRICT_NAME"].ToString() + "</td>";
                strHTML = strHTML + " <td > " + prow["CLINT_ADDRESS1"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_ADDRESS2"].ToString() + " </td>";
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

        strHTML = strHTML + "</tr>";

        strHTML = strHTML + " </table>";
        clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
    }
    private void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
       // objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
