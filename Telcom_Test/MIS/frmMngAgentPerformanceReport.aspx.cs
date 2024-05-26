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
using System.Text;

public partial class MIS_frmMngAgentPerformanceReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
 
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
    protected void btnAgntPrfRpt_Click(object sender, EventArgs e) 
    {
        string strSQL = "", strMsg = "", strQuery = "", fileName = "", strHTML="";
        //StringBuilder strHTML = new StringBuilder();
        lblMessage.Text = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();

        DataSet dtsAgntPerRpt = new DataSet(); 
        fileName = "Agent_Per_Report";
        //------------------------------------------Report Query -------------------------------------
        strQuery = " SELECT * FROM TEMP_AGENT_PER_RPT ";

        dtsAgntPerRpt = objServiceHandler.ExecuteQuery(strQuery);
        //SaveAuditInfo("Report", "All Balance");
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=28 align=center style='border-right:none'><h2 align=center> Agent Performance Report </h2></td></tr>";
        //strHTML = strHTML + "</table>";
        //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        //strHTML = strHTML + "<tr>";
        //strHTML = strHTML + " <td COLSPAN=6 align=center style='border-right:none'><h5 align=left> Date Range Between From Date " + dtpFDate.DateString + " To " + dtpTDate.DateString + " </h5></td>";
        //strHTML = strHTML + " <td COLSPAN=5 align=center style='border-right:none'><h5 align=right> Print Date " + DateTime.Now.ToShortDateString() + " </h5></td>";
        //strHTML = strHTML + " </tr>";
        //strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td valign='middle' >SerialNo</td>";
        strHTML = strHTML + "<td valign='middle' >Agent No</td>";
        strHTML = strHTML + "<td valign='middle' > Agent Name </td>";
        strHTML = strHTML + "<td valign='middle' >Agent Rank </td>";
        strHTML = strHTML + "<td valign='middle' >Agent Type</td>";
        strHTML = strHTML + "<td valign='middle' >DSE Account No</td>";
        strHTML = strHTML + "<td valign='middle' >Distributor Acc No</td>";
        strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
        strHTML = strHTML + "<td valign='middle' >District Name</td>";
        strHTML = strHTML + "<td valign='middle' >Thana Name</td>";
        strHTML = strHTML + "<td valign='middle' >Agent Balance</td>";
        strHTML = strHTML + "<td valign='middle' >No Of Reg Today</td>";
        strHTML = strHTML + "<td valign='middle' > No Of Reg This Month </td>";
        strHTML = strHTML + "<td valign='middle' >No Of Cash In Today </td>";
        strHTML = strHTML + "<td valign='middle' >Cash In Amt Today</td>";
        strHTML = strHTML + "<td valign='middle' >No Of Cash In This Month</td>";
        strHTML = strHTML + "<td valign='middle' >Cash In Amt This Month</td>";
        strHTML = strHTML + "<td valign='middle' >AMT_CN_DIS_COMM_TMONTH</td>";
        strHTML = strHTML + "<td valign='middle' >No of Cashout Today</td>";
        strHTML = strHTML + "<td valign='middle' >Cashout Amt Today</td>";
        strHTML = strHTML + "<td valign='middle' >No of Cashout This Month</td>";
        strHTML = strHTML + "<td valign='middle' >No Of Cashout Amt This Month</td>";
        strHTML = strHTML + "<td valign='middle' > AMT_CCT_DIS_COMM_TMONTH </td>";
        strHTML = strHTML + "<td valign='middle' >No Of BD Today </td>";
        strHTML = strHTML + "<td valign='middle' >BD Amt Today</td>";
        strHTML = strHTML + "<td valign='middle' >No Of BD This Month</td>";
        strHTML = strHTML + "<td valign='middle' >BD Amt This Month</td>";
        strHTML = strHTML + "<td valign='middle' >AMT_BD_DIS_COMM_TMONTH</td>";
        strHTML = strHTML + "</tr>";
        if (dtsAgntPerRpt.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in dtsAgntPerRpt.Tables[0].Rows)
            {
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["AGENT_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["AGENT_RANK"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["AGENT_TYPE"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["DSE_ACCNT"].ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["DIS_ACCNT"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["THANA_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["AGENT_BAL"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_REG_TODAY"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_REG_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_CASIN_TODAY"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_CASIN_AMT_TODAY"].ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_CASIN_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_CASIN_AMT_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["AMT_CN_DIS_COMM_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_CASOUT_TODAY"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_CASOUT_AMT_TODAY"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_CASOUT_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_CASOUT_AMT_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["AMT_CCT_DIS_COMM_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_BD_TODAY"].ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_BD_AMT_TODAY"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_BD_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NO_OF_BD_AMT_TMONTH"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["AMT_BD_DIS_COMM_TMONTH"].ToString() + " </td>";
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
        string str = @"D:\MPAY\TelCOM_ADM\MIS\EXPORT_AGENT_PER_REPORT\" + fileName + "_" + strNow + ".xls";
        System.IO.File.WriteAllText(str, strHTML);
        //-------------------------------------------------------------------------------------------
        //-------------------------- Insert Report Info -----------------------------
        string strReportFileName = fileName + "_" + strNow + ".xls";
        string strMesage = objServiceHandler.SaveAgentPerRptInfo(strReportFileName, Session["UserLoginName"].ToString());

        lblMessage.Text = "Report Generated Successfully.";
        sdsAgntPerReport.DataBind();
        gdvReportDownload.DataBind();
    }
}
