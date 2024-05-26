using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_frmNLIPaymentDetail : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            populateGridViewForDestinationOffice();
			PopulateGridviewForServiceList();
			
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

    protected void btnNLICollectionDetail_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet oDs = GetPostingDetail();

            gdvReportDetails.DataSource = oDs;
            gdvReportDetails.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private DataSet GetPostingDetail()
    {
        DataSet oDs = new DataSet();
        try
        {
            string strSqlSubQuery = "";

            if (ddlSearchType.SelectedItem.Value.Equals("PROJECT_CODE"))
            {
                strSqlSubQuery = " AND NFC.PROJECT_CODE = '" + txtSearchText.Text.Trim() + "'";
            }
            else if (ddlSearchType.SelectedItem.Value.Equals("SOURCE_ACCNT_NO"))
            {
                strSqlSubQuery = " AND NFC.SOURCE_ACCNT_NO = '" + txtSearchText.Text.Trim() + "'";
            }
            else if (ddlSearchType.SelectedItem.Value.Equals("PREMIUM_TYPE"))
            {
                strSqlSubQuery = " AND UPPER(NAC.PREMIUM_TYPE) LIKE UPPER('%" + txtSearchText.Text.Trim() + "%') ";
            }
            else if (ddlSearchType.SelectedItem.Value.Equals("DESTINATION_ACCNT_NO"))
            {
                strSqlSubQuery = " AND NFC.DESTINATION_ACCNT_NO = '" + txtSearchText.Text.Trim() + "'";
            }
            else if (ddlSearchType.SelectedItem.Value.Equals("CLINT_NAME"))
            {
                strSqlSubQuery = " AND UPPER(CLD.CLINT_NAME) LIKE UPPER('%" + txtSearchText.Text.Trim() + "%') ";
            }
            if (!ddlOfficeList.SelectedItem.Value.Equals("All"))
            {
                strSqlSubQuery = " AND NFC.DESTINATION_ACCNT_NO = '" + ddlOfficeList.SelectedItem.Value +"'";
            }
			if (!ddlServiceList.SelectedItem.Value.Equals("All"))
            {
                strSqlSubQuery = strSqlSubQuery + " AND NFC.PROJECT_CODE = '" + ddlServiceList.SelectedItem.Value + "'";
            }
			if (!ddlServiceList.SelectedItem.Value.Equals("All"))
            {
                strSqlSubQuery = strSqlSubQuery + " AND NFC.PROJECT_CODE = '" + ddlServiceList.SelectedItem.Value + "'";
            }
			
            string strDateRange = " AND TRUNC(REQUEST_TIME) BETWEEN '" + dtpNliFrDate.DateString + "' AND '" + dtpNliToDate.DateString + "'";

            string strSql = "SELECT NFC.DESTINATION_ACCNT_NO, CLD.CLINT_NAME DESTINATION_ACCNT_NAME, NFC.NLI_FUND_COLLECTION_ID, NFC.SERVICE_TYPE, NFC.SOURCE_ACCNT_NO, CLS.CLINT_NAME SOURCE_ACCNT_NAME, NFC.AMOUNT, NFC.PROJECT_CODE, NFC.REQUEST_ID, NFC.SMSC_REFERENCE_NO, NFC.REQUEST_TIME, DECODE(NFC.STATUS,'D','Deposited','Pending') STATUS, NAC.NLI_ACCOUNTS_CODE_ID,NAC.SERVICE_NAME,NAC.PREMIUM_TYPE FROM NLI_FUND_COLLECTION NFC, NLI_ACCOUNTS_CODE NAC, ACCOUNT_LIST ALD, CLIENT_LIST CLD, ACCOUNT_LIST ALS, CLIENT_LIST CLS WHERE NFC.PROJECT_CODE = NAC.PREMIUM_CODE AND NFC.DESTINATION_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND NFC.SOURCE_ACCNT_NO = ALS.ACCNT_NO AND ALS.CLINT_ID = CLS.CLINT_ID " + strSqlSubQuery + strDateRange + " GROUP BY NFC.DESTINATION_ACCNT_NO, CLD.CLINT_NAME, NFC.NLI_FUND_COLLECTION_ID, NFC.SERVICE_TYPE, NFC.SOURCE_ACCNT_NO,CLS.CLINT_NAME, NFC.AMOUNT, NFC.PROJECT_CODE, NFC.REQUEST_ID, NFC.SMSC_REFERENCE_NO, NFC.REQUEST_TIME, DECODE(NFC.STATUS,'D','Deposited','Pending'), NAC.NLI_ACCOUNTS_CODE_ID,NAC.SERVICE_NAME,NAC.PREMIUM_TYPE ORDER BY NFC.DESTINATION_ACCNT_NO, NFC.REQUEST_TIME DESC";

            oDs = objServiceHandler.ExecuteQuery(strSql);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return oDs;
    }

    private void populateGridViewForDestinationOffice()
    {
        string strSql = "SELECT AL.ACCNT_NO, CL.CLINT_NAME FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = '181219000000000002'";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
        ddlOfficeList.DataSource = oDs;
        ddlOfficeList.DataTextField = "CLINT_NAME";
        ddlOfficeList.DataValueField = "ACCNT_NO";
        ddlOfficeList.DataBind();

        ddlOfficeList.Items.Insert(0, new ListItem("--Select Office--", "All"));
    }
	
	private void PopulateGridviewForServiceList()
    {
        string strSql = "SELECT PREMIUM_CODE,PREMIUM_TYPE FROM NLI_ACCOUNTS_CODE";
        DataSet Ods = objServiceHandler.ExecuteQuery(strSql);
        ddlServiceList.DataSource = Ods;
        ddlServiceList.DataValueField = "PREMIUM_CODE";
        ddlServiceList.DataTextField = "PREMIUM_TYPE";
        ddlServiceList.DataBind();

        ddlServiceList.Items.Insert(0, new ListItem("--Select Service--", "All"));
    }
	
    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = "", strHTML = "";
            int totalAmount = 0;
            DataSet dtsAccount = new DataSet();
            fileName = "NLI_Transaction_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = GetPostingDetail();

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>NLI Transaction Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpNliFrDate.DateString + "' To '" + dtpNliToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Destination Account</td>";
            strHTML = strHTML + "<td valign='middle' >Destination Account Name</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Id</td>";
            strHTML = strHTML + "<td valign='middle' >Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Project Code</td>";
            strHTML = strHTML + "<td valign='middle' >Service Name</td>";
            strHTML = strHTML + "<td valign='middle' >Premium Type</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account Name</td>";
            strHTML = strHTML + "<td valign='middle' >Status</td>";

            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DESTINATION_ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DESTINATION_ACCNT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["PROJECT_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["PREMIUM_TYPE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["STATUS"].ToString() + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                    totalAmount = totalAmount + Convert.ToInt32(prow["AMOUNT"]);
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "Total Amount" + " </td>";
            strHTML = strHTML + " <td > " + totalAmount.ToString() + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

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

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}