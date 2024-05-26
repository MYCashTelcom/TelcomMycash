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
//using MIT_MGW.Class;

public partial class MIS_frmMISRegistrationReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();   
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //try
            //{
            //    strUserName = Session["UserLoginName"].ToString();
            //    strPassword = Session["Password"].ToString();
            //}
            //catch
            //{
            //    Session.Clear();
            //    Response.Redirect("../frmSeesionExpMesage.aspx");
            //}
        }
        //try
        //{
        //    strUserName = Session["UserLoginName"].ToString();
        //    strPassword = Session["Password"].ToString();
        //}
        //catch
        //{
        //    Session.Clear();
        //    Response.Redirect("../frmSeesionExpMesage.aspx");
        //}
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        string strQuery = "", strProcedure = "", strDateRange = "", strMsg = "",strProcedureQuery="";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        // ----------------  Procedure calling and inserting data in temp table -----------------

        if (txtWallet.Text.Trim() != "")
        {
            if (rblAllDateRange.SelectedValue == "0")
            {
                strDateRange = txtWallet.Text.Trim() + "','01/Jun/2012','" + dtpTo.DateString;
            }
            else if (rblAllDateRange.SelectedValue == "1")
            {
                strDateRange = txtWallet.Text.Trim() + "','" + dptFrom.DateString + "','" + dtpTo.DateString;
            }

            strProcedure = " PKG_MIS_REPORTS.MIS_REGISTRATION_REPORTS('" + strDateRange + "')";
            strMsg = objServiceHandler.ExecuteProcedure(strProcedure);
            if (strMsg != "")
            {
                //------------------ main query start here ------------------------
                strQuery = " SELECT THR.*, AL.ACCNT_NO AGENT_WALLET,CL.CLINT_NAME AGENT_NAME,AR.RANK_TITEL AGENT_RANK,"
                         + "  CL.CLINT_ADDRESS1 AGENT_ADDRESS,ASD.SERIAL_NO AGENT_SL_NO,'" + dptFrom.DateString + "' F_DATE,'" + dtpTo.DateString + "' T_DATE "
                         + " FROM ACCOUNT_LIST AL,CLIENT_LIST CL,ACCOUNT_RANK AR,ACCOUNT_SERIAL_DETAIL ASD ,"
                         + " TEMP_HIERARCHY_REGISTRATION THR WHERE AL.CLINT_ID=CL.CLINT_ID AND AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID "
                         + " AND ASD.CUSTOMER_MOBILE_NO=AL.ACCNT_MSISDN AND AL.ACCNT_NO='" + txtWallet.Text.Trim()
                         + "' ORDER BY TEMP_HRCHY_REG_ID ASC ";

                try
                {
                    Session["ReportSQL"] = strQuery;
                    SaveAuditInfo("View", "MIS Registration Report");
                    Session["RequestForm"] = "../MIS/frmMISRegistrationReport.aspx";
                    Session["ReportFile"] = "../MIS/MIS_REGISTRATION_REPORT.rpt";
                    Response.Redirect("../COM/COM_ReportView.aspx");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
        else
        {
            if (rblAllDateRange.SelectedValue == "0")
            {
                strDateRange = "01/Jun/2012','" + dtpTo.DateString;
            }
            else if (rblAllDateRange.SelectedValue == "1")
            {
                strDateRange =dptFrom.DateString + "','" + dtpTo.DateString;
            }

            strProcedure = " PKG_MIS_REPORTS.MIS_ALL_REGISTRATION_REPORTS('" + strDateRange + "')";
            strMsg = objServiceHandler.ExecuteProcedure(strProcedure);
            if (strMsg != "")
            {
                //------------------ main query start here ------------------------
                strQuery = " SELECT THR.*,   '" + dptFrom.DateString + "' F_DATE,'" + dtpTo.DateString + "' T_DATE "
                         + " FROM TEMP_HIERARCHY_REGISTRATION THR ORDER BY TEMP_HRCHY_REG_ID ASC ";
                try
                {
                    Session["ReportSQL"] = strQuery;
                    SaveAuditInfo("View", "MIS Registration Report");
                    Session["RequestForm"] = "../MIS/frmMISRegistrationReport.aspx";
                    Session["ReportFile"] = "../MIS/MIS_ALL_REGISTRATION_REPORT.rpt";
                    Response.Redirect("../COM/COM_ReportView.aspx");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog("", strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string strSQL = "", strMsg = "", strQuery = "", strHTML = "", fileName = "";
        lblMsg.Text = "";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        try
        {
            //strSQL = "PRO_INSERT_ALL_HIERARCHY";
            //strMsg = objServiceHandler.ExecuteProcedure(strSQL);
            
            
            DataSet dtsAccntBalance = new DataSet();
            fileName = "Hierarchywise_Registration_Report";
            //------------------------------------------Report Query -------------------------------------
            strQuery = " SELECT DIS_ACCNT_NO,CLINT_NAME,DIS_BALANCE,DEL_ACCNT_NO,DEL_BALANCE,SA_ACCNT_NO,"
                     + " SA_BALANCE,A_ACCNT_NO,A_BALANCE, FUNC_COUNT_REG(A_ACCNT_NO,'" + dtpFDate.DateString + "','"
                     + dtpTDate.DateString + "')TOTAL_REG FROM VW_ALL_HIERARCHY ORDER BY DIS_ACCNT_NO,DEL_ACCNT_NO,SA_ACCNT_NO,A_ACCNT_NO";

            dtsAccntBalance = objServiceHandler.ExecuteQuery(strQuery);
            SaveAuditInfo("Report", "All Balance");
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none'><h2 align=center> Hierarchywise Registration Report </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td COLSPAN=6 align=center style='border-right:none'><h5 align=left> Date Range Between From Date " + dtpFDate.DateString + " To " + dtpTDate.DateString + " </h5></td>";
            strHTML = strHTML + " <td COLSPAN=5 align=center style='border-right:none'><h5 align=right> Print Date " + DateTime.Now.ToShortDateString() + " </h5></td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + "<td valign='middle' >SerialNo</td>";

            strHTML = strHTML + "<td valign='middle' >Distributor Wallet No</td>";
            strHTML = strHTML + "<td valign='middle' > Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Balance </td>";
            strHTML = strHTML + "<td valign='middle' >Dealer Wallet No</td>";
            strHTML = strHTML + "<td valign='middle' >Dealer Balance</td>";
            strHTML = strHTML + "<td valign='middle' >Super Agent Wallet No</td>";
            strHTML = strHTML + "<td valign='middle' >Super Agent Balance</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Balance</td>";
            strHTML = strHTML + "<td valign='middle' >No of Customer Registration</td>";
            strHTML = strHTML + "</tr>";
            if (dtsAccntBalance.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccntBalance.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_BALANCE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DEL_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DEL_BALANCE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SA_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SA_BALANCE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["A_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["A_BALANCE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_REG"].ToString() + " </td>";
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
            string str = @"D:\MPAY\TelCOM\EXPORT_REGISTRATION_RPT\" + fileName + "_" + strNow + ".xls";
            System.IO.File.WriteAllText(str, strHTML);
            //-------------------------------------------------------------------------------------------
            //-------------------------- Insert Report Info -----------------------------
            string strReportFileName = fileName + "_" + strNow + ".xls";
        
            string strMesage = objServiceHandler.SaveRegRptInfo(strReportFileName, "", dtpFDate.DateString, dtpTDate.DateString,"RegReport");

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
            lblMsg.Text = "File not Found.";
        }
    }
}
