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

public partial class BANKING_frmMngBankServiceFeeReport : System.Web.UI.Page 
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
                ddlBranch.SelectedValue = Session["Branch_ID"].ToString();
                ddlBranch.DataBind();
                if (Session["Branch_Type"].Equals("A"))
                {
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlBranch.Enabled = false;
                }
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
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        clsServiceHandler objSerHandlr = new clsServiceHandler();
        DataSet oDS = new DataSet();
        string strSQL = "";
        string strHTML = "";
        string filename = "Service_Fee";

        if (rblSelectType.SelectedValue == "0")
        {
            strSQL = " SELECT CCB.CMP_BRANCH_NAME,BL.BANK_NAME, SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE, BSF.* "
                   + " FROM BANK_SERVICE_FEE BSF,SERVICE_LIST SL,CM_CMP_BRANCH CCB,BANK_LIST BL,ACCOUNT_RANK AR,CHANNEL_TYPE CT "
                   + " WHERE BSF.SERVICE_ID=SL.SERVICE_ID AND BSF.CMP_BRANCH_ID= CCB.CMP_BRANCH_ID  "
                   + " AND BL.BANK_INTERNAL_CODE=BSF.BANK_CODE AND AR.ACCNT_RANK_ID=BSF.ACCNT_RANK_ID AND BSF.CHANNEL_TYPE_ID=CT.CHANNEL_TYPE_ID "
                   + " ORDER BY CCB.CMP_BRANCH_NAME,SL.SERVICE_TITLE,AR.RANK_TITEL,CHANNEL_TYPE ";
        }
        else if (rblSelectType.SelectedValue == "1")
        {
            strSQL = " SELECT CCB.CMP_BRANCH_NAME,BL.BANK_NAME, SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE, BSF.* "
                  + " FROM BANK_SERVICE_FEE BSF,SERVICE_LIST SL,CM_CMP_BRANCH CCB,BANK_LIST BL,ACCOUNT_RANK AR,CHANNEL_TYPE CT "
                  + " WHERE BSF.SERVICE_ID=SL.SERVICE_ID AND BSF.CMP_BRANCH_ID= CCB.CMP_BRANCH_ID  "
                  + " AND BL.BANK_INTERNAL_CODE=BSF.BANK_CODE AND AR.ACCNT_RANK_ID=BSF.ACCNT_RANK_ID AND BSF.CHANNEL_TYPE_ID=CT.CHANNEL_TYPE_ID "
                  + " AND (CCB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString()
                  + "') AND (BSF.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "')"
                  + " ORDER BY CCB.CMP_BRANCH_NAME,SL.SERVICE_TITLE,AR.RANK_TITEL,CHANNEL_TYPE ";           
        }
        else if (rblSelectType.SelectedValue == "2")
        {
            strSQL = " SELECT CCB.CMP_BRANCH_NAME,BL.BANK_NAME, SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE, BSF.* "
                  + " FROM BANK_SERVICE_FEE BSF,SERVICE_LIST SL,CM_CMP_BRANCH CCB,BANK_LIST BL,ACCOUNT_RANK AR,CHANNEL_TYPE CT "
                  + " WHERE BSF.SERVICE_ID=SL.SERVICE_ID AND BSF.CMP_BRANCH_ID= CCB.CMP_BRANCH_ID  "
                  + " AND BL.BANK_INTERNAL_CODE=BSF.BANK_CODE AND AR.ACCNT_RANK_ID=BSF.ACCNT_RANK_ID AND BSF.CHANNEL_TYPE_ID=CT.CHANNEL_TYPE_ID "
                  + " AND (CCB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString()
                  + "') AND (BSF.BANK_CODE='" + ddlBankList.SelectedValue.ToString()
                  + "') AND (BSF.SERVICE_ID='" + ddlService.SelectedValue.ToString() + "')"
                  + " ORDER BY CCB.CMP_BRANCH_NAME,SL.SERVICE_TITLE,AR.RANK_TITEL,CHANNEL_TYPE ";
        }
        else if (rblSelectType.SelectedValue == "3")
        {
            strSQL = " SELECT CCB.CMP_BRANCH_NAME,BL.BANK_NAME, SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE, BSF.* "
                   + " FROM BANK_SERVICE_FEE BSF,SERVICE_LIST SL,CM_CMP_BRANCH CCB,BANK_LIST BL,ACCOUNT_RANK AR,CHANNEL_TYPE CT "
                   + " WHERE BSF.SERVICE_ID=SL.SERVICE_ID AND BSF.CMP_BRANCH_ID= CCB.CMP_BRANCH_ID  "
                   + " AND BL.BANK_INTERNAL_CODE=BSF.BANK_CODE AND AR.ACCNT_RANK_ID=BSF.ACCNT_RANK_ID AND BSF.CHANNEL_TYPE_ID=CT.CHANNEL_TYPE_ID "
                   + " AND (CCB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString()
                   + "') AND (BSF.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') AND (BSF.SERVICE_ID='" + ddlService.SelectedValue.ToString()
                   + "') AND (BSF.ACCNT_RANK_ID='" + ddlAccountRank.SelectedValue.ToString() + "')"
                   + " ORDER BY CCB.CMP_BRANCH_NAME,SL.SERVICE_TITLE,AR.RANK_TITEL,CHANNEL_TYPE ";
        }
        else if (rblSelectType.SelectedValue == "4")
        {
            strSQL = " SELECT CCB.CMP_BRANCH_NAME,BL.BANK_NAME, SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE, BSF.* "
                  + " FROM BANK_SERVICE_FEE BSF,SERVICE_LIST SL,CM_CMP_BRANCH CCB,BANK_LIST BL,ACCOUNT_RANK AR,CHANNEL_TYPE CT "
                  + " WHERE BSF.SERVICE_ID=SL.SERVICE_ID AND BSF.CMP_BRANCH_ID= CCB.CMP_BRANCH_ID  "
                  + " AND BL.BANK_INTERNAL_CODE=BSF.BANK_CODE AND AR.ACCNT_RANK_ID=BSF.ACCNT_RANK_ID AND BSF.CHANNEL_TYPE_ID=CT.CHANNEL_TYPE_ID "
                  + " AND (CCB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString()
                  + "') AND (BSF.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') AND (BSF.SERVICE_ID='" + ddlService.SelectedValue.ToString()
                  + "') AND (BSF.ACCNT_RANK_ID='" + ddlAccountRank.SelectedValue.ToString() + "') AND BSF.CHANNEL_TYPE_ID='" + ddlChannelName.SelectedValue.ToString() + "'"
                  + " ORDER BY CCB.CMP_BRANCH_NAME,SL.SERVICE_TITLE,AR.RANK_TITEL,CHANNEL_TYPE ";
        }

        oDS = objSerHandlr.ExecuteQuery(strSQL);

         SaveAuditInfo("Report", "Service Rate Report");
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=25 align=center style='border-right:none'><h2 align=center> Service Fee("+rblSelectType.SelectedItem.ToString()+") </h2></td></tr>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";

        strHTML = strHTML + "<td valign='middle' >SerialNo</td>";

        strHTML = strHTML + "<td valign='middle' >Branch</td>";
        strHTML = strHTML + "<td valign='middle' >  Bank Name  </td>";
        strHTML = strHTML + "<td valign='middle' >Service </td>";
        strHTML = strHTML + "<td valign='middle' >Account Rank</td>";
        strHTML = strHTML + "<td valign='middle' >Channel Name</td>";



        strHTML = strHTML + "<td valign='middle' >Fee Name</td>";
        strHTML = strHTML + "<td valign='middle' >  Start Amount  </td>";
        strHTML = strHTML + "<td valign='middle' >Max Amount </td>";
        strHTML = strHTML + "<td valign='middle' >Fee</td>";
        strHTML = strHTML + "<td valign='middle' >Minimum Fee</td>";

        strHTML = strHTML + "<td valign='middle' >Vat and Tax(%)</td>";
        strHTML = strHTML + "<td valign='middle' >AIT(%)</td>";
        strHTML = strHTML + "<td valign='middle' > Fees Paid by bank(%)</td>";
        strHTML = strHTML + "<td valign='middle' >Fees Paid by Customer(%) </td>";

        strHTML = strHTML + "<td valign='middle' >Fees Paid by Agent(%)</td>";
        strHTML = strHTML + "<td valign='middle' >Bank commission(%)</td>";
        strHTML = strHTML + "<td valign='middle' >Agent commission(%)</td>";
        strHTML = strHTML + "<td valign='middle' >Pool Adjustment(%)</td>";

        strHTML = strHTML + "<td valign='middle' >Vendor commission(%) </td>";
        strHTML = strHTML + "<td valign='middle' >Third party commission(%)</td>";
        strHTML = strHTML + "<td valign='middle' >Channel commission(%)</td>";
        strHTML = strHTML + "<td valign='middle' >Tax paid by</td>";
        strHTML = strHTML + "<td valign='middle' >Fees paid by(%)</td>";
        strHTML = strHTML + "<td valign='middle' >Fees include Vat/Tax(%)</td>";

        strHTML = strHTML + "</tr>";

        try
        {
            if (oDS.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in oDS.Tables[0].Rows)
                {

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";

                    strHTML = strHTML + " <td > " + prow["CMP_BRANCH_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["BANK_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["SERVICE_TITLE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["RANK_TITEL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["CHANNEL_TYPE"].ToString() + "</td>";

                    strHTML = strHTML + " <td > " + prow["BANK_SRV_FEE_TITLE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["START_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MAX_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BANK_SRV_FEE_AMOUNT"].ToString() + " </td>";

                    strHTML = strHTML + " <td > " + prow["MIN_FEE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["VAT_TAX"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["AIT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_BANK"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_CUSTOMER"].ToString() + " </td>";

                    strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_AGENT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BANK_COMM_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_COMM_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["POOL_ADJUSTMENT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["VENDOR_COMMISSION"].ToString() + " </td>";

                    strHTML = strHTML + " <td > " + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["CHANNEL_COMMISSION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TAX_PAID_BY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["FEES_PAID_BY"].ToString() + " </td>";

                    if (prow["FEE_INCLUDE_VAT_TAX"].ToString() == "Y")
                    {
                        strHTML = strHTML + " <td > '" + "Yes" + " </td>";
                    }
                    else if (prow["FEE_INCLUDE_VAT_TAX"].ToString() == "N")
                    {
                        strHTML = strHTML + " <td > '" + "No" + " </td>";
                    }

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
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + " </table>";
            clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        clsServiceHandler objSerHandlr = new clsServiceHandler();
        DataSet oDS = new DataSet();
        string strSQL = "";
        string strHTML = "";
        string filename = "Bank_Fee_Wave";

        if (rblSelectBankFeeWave.SelectedValue == "0")
        {
            strSQL = " SELECT BSFW.WAVE_AMOUNT ,BSFW.SERVICE_ID,BSFW.CHANNEL_TYPE_ID,BSFW.CMP_BRANCH_ID,BSFW.BANK_CODE,BSFW.TRAN_ALLOWED,BSFW.ACCNT_RANK_ID_SOURCE,BSFW.ACCNT_RANK_ID_DEST , CMB.CMP_BRANCH_ID, CMB.CMP_BRANCH_NAME ,AR.ACCNT_RANK_ID,AR.RANK_TITEL AS SOURCE,ARA.ACCNT_RANK_ID,ARA.RANK_TITEL AS DESTINATION,BL.BANK_NAME,BL.BANK_INTERNAL_CODE,SL.SERVICE_ID, SL.SERVICE_TITLE,CT.CHANNEL_TYPE_ID,CT.CHANNEL_TYPE FROM BANK_SERVICE_FEE_WAVE BSFW ,CM_CMP_BRANCH CMB ,ACCOUNT_RANK AR,BANK_LIST BL,SERVICE_LIST SL,CHANNEL_TYPE CT,ACCOUNT_RANK ARA WHERE BANK_STATUS='A' AND BSFW.SERVICE_ID=SL.SERVICE_ID AND BSFW.CHANNEL_TYPE_ID= CT.CHANNEL_TYPE_ID AND  BSFW.CMP_BRANCH_ID=CMB.CMP_BRANCH_ID AND BSFW.ACCNT_RANK_ID_SOURCE=AR.ACCNT_RANK_ID AND BSFW.ACCNT_RANK_ID_DEST=ARA.ACCNT_RANK_ID AND BL.BANK_INTERNAL_CODE =BSFW.BANK_CODE ORDER BY  SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE ";
        }
        else if (rblSelectBankFeeWave.SelectedValue == "1")
        {

            strSQL = " SELECT BSFW.WAVE_AMOUNT ,BSFW.SERVICE_ID,BSFW.CHANNEL_TYPE_ID,BSFW.CMP_BRANCH_ID,BSFW.BANK_CODE,BSFW.TRAN_ALLOWED,BSFW.ACCNT_RANK_ID_SOURCE,BSFW.ACCNT_RANK_ID_DEST , CMB.CMP_BRANCH_ID, CMB.CMP_BRANCH_NAME,AR.ACCNT_RANK_ID,AR.RANK_TITEL AS SOURCE,ARA.ACCNT_RANK_ID,ARA.RANK_TITEL AS DESTINATION,BL.BANK_NAME,BL.BANK_INTERNAL_CODE, SL.SERVICE_ID, SL.SERVICE_TITLE,CT.CHANNEL_TYPE_ID,CT.CHANNEL_TYPE FROM BANK_SERVICE_FEE_WAVE BSFW ,CM_CMP_BRANCH CMB ,ACCOUNT_RANK AR,BANK_LIST BL,SERVICE_LIST SL,CHANNEL_TYPE CT,ACCOUNT_RANK ARA WHERE BANK_STATUS='A' AND BSFW.SERVICE_ID=SL.SERVICE_ID AND BSFW.CHANNEL_TYPE_ID= CT.CHANNEL_TYPE_ID AND  BSFW.CMP_BRANCH_ID=CMB.CMP_BRANCH_ID AND  BSFW.ACCNT_RANK_ID_SOURCE=AR.ACCNT_RANK_ID AND BSFW.ACCNT_RANK_ID_DEST=ARA.ACCNT_RANK_ID AND BL.BANK_INTERNAL_CODE =BSFW.BANK_CODE AND(CMB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString() + "') AND (BSFW.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') ORDER BY  SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE ";
        }
        else if (rblSelectBankFeeWave.SelectedValue == "2")
        {
            strSQL = " SELECT BSFW.WAVE_AMOUNT ,BSFW.SERVICE_ID,BSFW.CHANNEL_TYPE_ID,BSFW.CMP_BRANCH_ID,BSFW.BANK_CODE,BSFW.TRAN_ALLOWED,BSFW.ACCNT_RANK_ID_SOURCE,BSFW.ACCNT_RANK_ID_DEST , CMB.CMP_BRANCH_ID, CMB.CMP_BRANCH_NAME , AR.ACCNT_RANK_ID,AR.RANK_TITEL AS SOURCE,ARA.ACCNT_RANK_ID,ARA.RANK_TITEL AS DESTINATION,BL.BANK_NAME,BL.BANK_INTERNAL_CODE, SL.SERVICE_ID, SL.SERVICE_TITLE,CT.CHANNEL_TYPE_ID,CT.CHANNEL_TYPE FROM BANK_SERVICE_FEE_WAVE BSFW ,CM_CMP_BRANCH CMB ,ACCOUNT_RANK AR,BANK_LIST BL,SERVICE_LIST SL,CHANNEL_TYPE CT,ACCOUNT_RANK ARA  WHERE BANK_STATUS='A' AND BSFW.SERVICE_ID=SL.SERVICE_ID AND BSFW.CHANNEL_TYPE_ID= CT.CHANNEL_TYPE_ID AND  BSFW.CMP_BRANCH_ID=CMB.CMP_BRANCH_ID AND BSFW.ACCNT_RANK_ID_SOURCE=AR.ACCNT_RANK_ID  AND BSFW.ACCNT_RANK_ID_DEST=ARA.ACCNT_RANK_ID AND BL.BANK_INTERNAL_CODE =BSFW.BANK_CODE AND(BSFW.SERVICE_ID='" + ddlService.SelectedValue.ToString() + "') AND(CMB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString() + "') AND (BSFW.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') ORDER BY  SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE ";
        }

        SaveAuditInfo("Report", "Bank fee Wave Report");

        oDS = objSerHandlr.ExecuteQuery(strSQL);
        SaveAuditInfo("Report", "Bank fee Wave Report");

        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none'><h2 align=center> Bank Fee Wave(" + rblSelectBankFeeWave.SelectedItem.ToString() + ") </h2></td></tr>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";

        strHTML = strHTML + "<td valign='middle' >SerialNo</td>";

        strHTML = strHTML + "<td valign='middle' >Branch</td>";
        strHTML = strHTML + "<td valign='middle' >  Bank Name  </td>";
        strHTML = strHTML + "<td valign='middle' >Service </td>";
        //strHTML = strHTML + "<td valign='middle' >Account Rank</td>";
        //strHTML = strHTML + "<td valign='middle' >Channel Name</td>";



        strHTML = strHTML + "<td valign='middle' >Source Bank</td>";
        strHTML = strHTML + "<td valign='middle' >Destination Bank</td>";
        strHTML = strHTML + "<td valign='middle' >Wave Amount </td>";
        strHTML = strHTML + "<td valign='middle' >Channel Name</td>";
        strHTML = strHTML + "<td valign='middle' >Transaction Allowed</td>";

        //strHTML = strHTML + "<td valign='middle' >Vat and Tax(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >AIT(%)</td>";
        //strHTML = strHTML + "<td valign='middle' > Fees Paid by bank(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Fees Paid by Customer(%) </td>";

        //strHTML = strHTML + "<td valign='middle' >Fees Paid by Agent(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Bank commission(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Agent commission(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Pool Adjustment(%)</td>";

        //strHTML = strHTML + "<td valign='middle' >Vendor commission(%) </td>";
        //strHTML = strHTML + "<td valign='middle' >Third party commission(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Channel commission(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Tax paid by</td>";
        //strHTML = strHTML + "<td valign='middle' >Fees paid by(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Fees include Vat/Tax(%)</td>";

        strHTML = strHTML + "</tr>";

        try
        {
            if (oDS.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in oDS.Tables[0].Rows)
                {

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";

                    strHTML = strHTML + " <td > " + prow["CMP_BRANCH_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["BANK_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["SERVICE_TITLE"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["RANK_TITEL"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["CHANNEL_TYPE"].ToString() + "</td>";

                    strHTML = strHTML + " <td > " + prow["SOURCE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["DESTINATION"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["WAVE_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CHANNEL_TYPE"].ToString() + " </td>";

                    //strHTML = strHTML + " <td > " + prow["MIN_FEE"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["VAT_TAX"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["AIT"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_BANK"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_CUSTOMER"].ToString() + " </td>";

                    //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_AGENT"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["BANK_COMM_AMOUNT"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["AGENT_COMM_AMOUNT"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["POOL_ADJUSTMENT"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["VENDOR_COMMISSION"].ToString() + " </td>";

                    //strHTML = strHTML + " <td > " + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["CHANNEL_COMMISSION"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["TAX_PAID_BY"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY"].ToString() + " </td>";

                    if (prow["TRAN_ALLOWED"].ToString() == "Y")
                    {
                        strHTML = strHTML + " <td > '" + "Yes" + " </td>";
                    }
                    else if (prow["TRAN_ALLOWED"].ToString() == "N")
                    {
                        strHTML = strHTML + " <td > '" + "No" + " </td>";
                    }

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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + " </table>";
            clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }



    protected void btnExportTransLimitComm_Click1(object sender, EventArgs e)
    {
        clsServiceHandler objSerHandlr = new clsServiceHandler();
        DataSet oDS = new DataSet();
        string strSQL = "";
        string strHTML = "";
        string filename = "Trans_Limit_Commission";

        if (rblTransactionLimitCommisssion.SelectedValue == "0")
        {
            strSQL = "SELECT ARC.* ,AR.RANK_TITEL,AR.ACCNT_RANK_ID,CCB.CMP_BRANCH_ID, CCB.CMP_BRANCH_NAME,BL.BANK_NAME,BL.BANK_INTERNAL_CODE,SL.SERVICE_ID, SL.SERVICE_TITLE FROM ACCOUNT_RANK_COMMISSION ARC ,ACCOUNT_RANK AR ,CM_CMP_BRANCH CCB, BANK_LIST BL,SERVICE_LIST SL WHERE AR.ACCNT_RANK_ID<>'120519000000000001' AND BL.BANK_STATUS='A' AND AR.ACCNT_RANK_ID=ARC.ACCNT_RANK_ID AND CCB.CMP_BRANCH_ID=ARC.CMP_BRANCH_ID AND BL.BANK_INTERNAL_CODE=ARC.BANK_CODE AND SL.SERVICE_ID=ARC.SERVICE_ID ORDER BY AR.ACCNT_RANK_ID,BL.BANK_NAME ,SL.SERVICE_TITLE ";
        }
        else if (rblTransactionLimitCommisssion.SelectedValue == "1")
        {
            strSQL = "SELECT ARC.* ,AR.RANK_TITEL,AR.ACCNT_RANK_ID,CCB.CMP_BRANCH_ID, CCB.CMP_BRANCH_NAME,BL.BANK_NAME,BL.BANK_INTERNAL_CODE,SL.SERVICE_ID, SL.SERVICE_TITLE FROM ACCOUNT_RANK_COMMISSION ARC ,ACCOUNT_RANK AR ,CM_CMP_BRANCH CCB, BANK_LIST BL,SERVICE_LIST SL WHERE AR.ACCNT_RANK_ID<>'120519000000000001' AND BL.BANK_STATUS='A' AND AR.ACCNT_RANK_ID=ARC.ACCNT_RANK_ID AND CCB.CMP_BRANCH_ID=ARC.CMP_BRANCH_ID AND BL.BANK_INTERNAL_CODE=ARC.BANK_CODE AND SL.SERVICE_ID=ARC.SERVICE_ID AND(CCB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString() + "') AND (ARC.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') ORDER BY AR.ACCNT_RANK_ID,BL.BANK_NAME ,SL.SERVICE_TITLE ";

        }
        else if (rblTransactionLimitCommisssion.SelectedValue == "2")
        {
            strSQL = "SELECT ARC.* ,AR.RANK_TITEL,AR.ACCNT_RANK_ID,CCB.CMP_BRANCH_ID, CCB.CMP_BRANCH_NAME,BL.BANK_NAME,BL.BANK_INTERNAL_CODE,SL.SERVICE_ID, SL.SERVICE_TITLE FROM ACCOUNT_RANK_COMMISSION ARC ,ACCOUNT_RANK AR ,CM_CMP_BRANCH CCB, BANK_LIST BL,SERVICE_LIST SL WHERE AR.ACCNT_RANK_ID<>'120519000000000001' AND BL.BANK_STATUS='A' AND AR.ACCNT_RANK_ID=ARC.ACCNT_RANK_ID AND CCB.CMP_BRANCH_ID=ARC.CMP_BRANCH_ID AND BL.BANK_INTERNAL_CODE=ARC.BANK_CODE AND SL.SERVICE_ID=ARC.SERVICE_ID AND(ARC.SERVICE_ID='" + ddlService.SelectedValue.ToString() + "') AND(CCB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString() + "') AND (ARC.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') ORDER BY AR.ACCNT_RANK_ID,BL.BANK_NAME ,SL.SERVICE_TITLE ";

        }
        oDS = objSerHandlr.ExecuteQuery(strSQL);

        SaveAuditInfo("Report", "tranasaction limite Commission");
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=14 align=center style='border-right:none'><h2 align=center> Transaction Limit & Commission(" + rblTransactionLimitCommisssion.SelectedItem.ToString() + ") </h2></td></tr>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";

        strHTML = strHTML + "<td valign='middle' >SerialNo</td>";

        strHTML = strHTML + "<td valign='middle' >Branch</td>";
        strHTML = strHTML + "<td valign='middle' >  Bank Name  </td>";
        strHTML = strHTML + "<td valign='middle' >Service </td>";



        strHTML = strHTML + "<td valign='middle' >Account Rank</td>";
        strHTML = strHTML + "<td valign='middle' >Reward  </td>";
        strHTML = strHTML + "<td valign='middle' >Maximum Debit Amount Per Day </td>";
        strHTML = strHTML + "<td valign='middle' >Maximum Debit Amount Per Month</td>";
        strHTML = strHTML + "<td valign='middle' >Maximum Credit Amount Per Day </td>";

        strHTML = strHTML + "<td valign='middle' >Maximum Credit Amount Per Month</td>";
        strHTML = strHTML + "<td valign='middle' >Maximum Tranasction Per Day</td>";
        strHTML = strHTML + "<td valign='middle' > Maximum Tranasction Per Month</td>";
        strHTML = strHTML + "<td valign='middle' >Transaction Direction </td>";

        strHTML = strHTML + "<td valign='middle' >Remarks</td>";
        //strHTML = strHTML + "<td valign='middle' >Bank commission(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Agent commission(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Pool Adjustment(%)</td>";

        //strHTML = strHTML + "<td valign='middle' >Vendor commission(%) </td>";
        //strHTML = strHTML + "<td valign='middle' >Third party commission(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Channel commission(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Tax paid by</td>";
        //strHTML = strHTML + "<td valign='middle' >Fees paid by(%)</td>";
        //strHTML = strHTML + "<td valign='middle' >Fees include Vat/Tax(%)</td>";

        strHTML = strHTML + "</tr>";

        try
        {
            if (oDS.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in oDS.Tables[0].Rows)
                {

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";

                    strHTML = strHTML + " <td > " + prow["CMP_BRANCH_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["BANK_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["SERVICE_TITLE"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["RANK_TITEL"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["CHANNEL_TYPE"].ToString() + "</td>";

                    strHTML = strHTML + " <td > " + prow["RANK_TITEL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["COMMISSION_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MAX_VALUE_PER_DAY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["MAX_VALUE_PER_MONTH"].ToString() + " </td>";

                    strHTML = strHTML + " <td > " + prow["MAX_CR_VALUE_PER_DAY"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["MAX_CR_VALUE_PER_MONTH"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["MAX_TRAN_PER_DAY"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["MAX_TRAN_PER_MONTH"].ToString() + " </td>";
                    strHTML = strHTML + " <td > " + prow["TRANSACTION_DIRECTION"].ToString() + " </td>";

                    strHTML = strHTML + " <td > " + prow["REMARKS"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["BANK_COMM_AMOUNT"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > '" + prow["AGENT_COMM_AMOUNT"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["POOL_ADJUSTMENT"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["VENDOR_COMMISSION"].ToString() + " </td>";

                    //strHTML = strHTML + " <td > " + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["CHANNEL_COMMISSION"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["TAX_PAID_BY"].ToString() + " </td>";
                    //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY"].ToString() + " </td>";

                    //if (prow["FEE_INCLUDE_VAT_TAX"].ToString() == "Y")
                    //{
                    //    strHTML = strHTML + " <td > '" + "Yes" + " </td>";
                    //}
                    //else if (prow["FEE_INCLUDE_VAT_TAX"].ToString() == "N")
                    //{
                    //    strHTML = strHTML + " <td > '" + "No" + " </td>";
                    //}

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
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";

            strHTML = strHTML + " </table>";
            clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
}
