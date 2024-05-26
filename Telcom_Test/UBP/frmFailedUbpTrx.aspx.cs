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

public partial class UBP_frmFailedUbpTrx : System.Web.UI.Page
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
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }

            LoadBillerType();

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

    private void LoadBillerType()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT BILLPAY_TYPE_ID, BILLPAY_TYPE_NAME, BILLPAY_TYPE_SHORT_CODE "
                    + " FROM BILLPAY_TYPE ORDER BY BILLPAY_TYPE_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            drpBillerType.DataSource = oDataSet;
            drpBillerType.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        LoadBillList();
    }

    private void LoadBillList()
    {
        try
        {
            lblMsg.Text = "";
            string strSql = "";
            strSql = " SELECT UTILITY_TRAN_ID, STAKEHOLDER_ID, LOCATION_ID, ACCOUNT_NUMBER, BILL_NUMBER, "
                     + " TOTAL_DPDC_AMOUNT, VAT_AMOUNT, BILL_MONTH, BILL_YEAR, REQUEST_ID, OWNER_CODE, "
                     + " TRANSACTION_STATUS, TRANSA_DATE, FINAL_STATUS, SOURCE_ACC_NO, BILL_STATUS,  RESPONSE_STATUS, "
                     + " REVERSE_STATUS, REQUEST_LOG, RESPONSE_LOG, REQUEST_LOG_BP, RESPONSE_LOG_BP, "
                     + " RESPONSE_MSG_BP, RESPONSE_STATUS_BP, REQUEST_PARTY_TYPE, PAYER_MOBILE_NO FROM UTILITY_TRANSACTION UT "
                     + " WHERE UT.STAKEHOLDER_ID = 'MBLBANK' AND UT.SERVICE = 'UBP' AND UT.OWNER_CODE = '" + drpBillerType.SelectedValue + "' "
                     + " AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpFrDate.DateString + "' AND '" + dtpToDate.DateString + "' "
                     + " AND RESPONSE_STATUS_BP IS NULL AND UT.BILL_NUMBER IS NOT NULL ORDER BY UTILITY_TRAN_ID DESC ";

            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvNoResponseList.DataSource = oDataSet;
            grvNoResponseList.DataBind();

            if (grvNoResponseList.Rows.Count == 0)
            {
                lblMsg.Text = "No Data found";
            }

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadBillDetails(string strutlId)
    {
        try
        {
            lblMsg.Text = "";
            string strSql = "";
            strSql = " SELECT UTILITY_TRAN_ID, STAKEHOLDER_ID, LOCATION_ID, ACCOUNT_NUMBER, BILL_NUMBER, "
                     + " TOTAL_DPDC_AMOUNT, VAT_AMOUNT, BILL_MONTH, BILL_YEAR, REQUEST_ID, OWNER_CODE, "
                     + " TRANSACTION_STATUS, TRANSA_DATE, FINAL_STATUS, SOURCE_ACC_NO, BILL_STATUS,  RESPONSE_STATUS, "
                     + " REVERSE_STATUS, REQUEST_LOG, RESPONSE_LOG, REQUEST_LOG_BP, RESPONSE_LOG_BP, "
                     + " RESPONSE_MSG_BP, RESPONSE_STATUS_BP, REQUEST_PARTY_TYPE, PAYER_MOBILE_NO FROM UTILITY_TRANSACTION UT "
                     + " WHERE UT.STAKEHOLDER_ID = 'MBLBANK' AND UT.SERVICE = 'UBP' AND UT.OWNER_CODE = '" + drpBillerType.SelectedValue + "' "
                     + " AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpFrDate.DateString + "' AND '" + dtpToDate.DateString + "' "
                     + " AND RESPONSE_STATUS_BP IS NULL AND UT.BILL_NUMBER IS NOT NULL AND UTILITY_TRAN_ID = '" + strutlId + "' ORDER BY UTILITY_TRAN_ID ASC ";

            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            dtvBillDtls.DataSource = oDataSet;
            dtvBillDtls.DataBind();

            if (dtvBillDtls.Rows.Count == 0)
            {
                lblMsg.Text = "No Data found";
            }

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    protected void grvNoResponseList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvNoResponseList.PageIndex = e.NewPageIndex;
            LoadBillList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void lnkButtonDetails_Click(object sender, EventArgs e)
    {
        try
        {
            dtvBillDtls.Visible = true;

            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;

            Label lblBillPayId = (Label)Grow.FindControl("Label1");
            string strBillPayId = lblBillPayId.Text.ToString();
            LoadBillDetails(strBillPayId);
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }


    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        LoadExportData();
    }

    private void LoadExportData()
    {
        try
        {
            lblMsg.Text = "";
            string strSql = "";
            strSql = " SELECT UTILITY_TRAN_ID, STAKEHOLDER_ID, LOCATION_ID, ACCOUNT_NUMBER, BILL_NUMBER, "
                     + " TOTAL_DPDC_AMOUNT, VAT_AMOUNT, BILL_MONTH, BILL_YEAR, REQUEST_ID, OWNER_CODE, "
                     + " TRANSACTION_STATUS, TRANSA_DATE, FINAL_STATUS, SOURCE_ACC_NO, BILL_STATUS,  RESPONSE_STATUS, "
                     + " REVERSE_STATUS, REQUEST_LOG, RESPONSE_LOG, REQUEST_LOG_BP, RESPONSE_LOG_BP, "
                     + " RESPONSE_MSG_BP, RESPONSE_STATUS_BP, REQUEST_PARTY_TYPE, PAYER_MOBILE_NO FROM UTILITY_TRANSACTION UT "
                     + " WHERE UT.STAKEHOLDER_ID = 'MBLBANK' AND UT.SERVICE = 'UBP' AND UT.OWNER_CODE = '" + drpBillerType.SelectedValue + "' "
                     + " AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpFrDate.DateString + "' AND '" + dtpToDate.DateString + "' "
                     + " AND RESPONSE_STATUS_BP IS NULL AND UT.BILL_NUMBER IS NOT NULL ORDER BY UTILITY_TRAN_ID ASC ";

            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Bill Pay Transaction Reverse Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpFrDate.DateString + "' To '" + dtpToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Stake Holder</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Account No </td>";
            strHTML = strHTML + "<td valign='middle' >Bill No</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Id </td>";
            strHTML = strHTML + "<td valign='middle' >Source Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Response Status</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["STAKEHOLDER_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACCOUNT_NUMBER"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_NUMBER"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACC_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_DPDC_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSA_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_MSG_BP"].ToString() + "</td>";
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

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
