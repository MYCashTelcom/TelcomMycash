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

public partial class COMMON_frmAccountRankCom : System.Web.UI.Page
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
    protected void dtvAccntRankCom_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        //sdsRankCom.InsertParameters["ACCNT_RANK_ID"].DefaultValue = ddlServiceList.SelectedValue;

        sdsRankCom.InsertParameters["SERVICE_ID"].DefaultValue = ddlServiceList.SelectedValue;
    }
    protected void dtvAccntRankCom_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Insert", "Transaction Limit & Comission");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvComList_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Transaction Limit & Comission");
    }
    protected void gdvComList_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SaveAuditInfo("Delete", "Transaction Limit & Comission");
    }
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //     clsServiceHandler objSerHandlr = new clsServiceHandler();
    //    DataSet oDS = new DataSet();
    //    string strSQL = "";
    //    string strHTML = "";
    //    string filename = "Trans_Limit_Commission";

    //    if (rblSelectType.SelectedValue == "0")
    //    {
    //        strSQL = "SELECT ARC.* ,AR.RANK_TITEL,AR.ACCNT_RANK_ID,CCB.CMP_BRANCH_ID, CCB.CMP_BRANCH_NAME,BL.BANK_NAME,BL.BANK_INTERNAL_CODE,SL.SERVICE_ID, SL.SERVICE_TITLE FROM ACCOUNT_RANK_COMMISSION ARC ,ACCOUNT_RANK AR ,CM_CMP_BRANCH CCB, BANK_LIST BL,SERVICE_LIST SL WHERE AR.ACCNT_RANK_ID<>'120519000000000001' AND BL.BANK_STATUS='A' AND AR.ACCNT_RANK_ID=ARC.ACCNT_RANK_ID AND CCB.CMP_BRANCH_ID=ARC.CMP_BRANCH_ID AND BL.BANK_INTERNAL_CODE=ARC.BANK_CODE AND SL.SERVICE_ID=ARC.SERVICE_ID ORDER BY AR.ACCNT_RANK_ID,BL.BANK_NAME ,SL.SERVICE_TITLE ";
    //    }
    //    else if (rblSelectType.SelectedValue == "1")
    //    {
    //        strSQL = "SELECT ARC.* ,AR.RANK_TITEL,AR.ACCNT_RANK_ID,CCB.CMP_BRANCH_ID, CCB.CMP_BRANCH_NAME,BL.BANK_NAME,BL.BANK_INTERNAL_CODE,SL.SERVICE_ID, SL.SERVICE_TITLE FROM ACCOUNT_RANK_COMMISSION ARC ,ACCOUNT_RANK AR ,CM_CMP_BRANCH CCB, BANK_LIST BL,SERVICE_LIST SL WHERE AR.ACCNT_RANK_ID<>'120519000000000001' AND BL.BANK_STATUS='A' AND AR.ACCNT_RANK_ID=ARC.ACCNT_RANK_ID AND CCB.CMP_BRANCH_ID=ARC.CMP_BRANCH_ID AND BL.BANK_INTERNAL_CODE=ARC.BANK_CODE AND SL.SERVICE_ID=ARC.SERVICE_ID AND(CMB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString() + "') AND (BSFW.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') ORDER BY AR.ACCNT_RANK_ID,BL.BANK_NAME ,SL.SERVICE_TITLE ";

    //    }
    //    else if (rblSelectType.SelectedValue == "2")
    //    {
    //        strSQL = "SELECT ARC.* ,AR.RANK_TITEL,AR.ACCNT_RANK_ID,CCB.CMP_BRANCH_ID, CCB.CMP_BRANCH_NAME,BL.BANK_NAME,BL.BANK_INTERNAL_CODE,SL.SERVICE_ID, SL.SERVICE_TITLE FROM ACCOUNT_RANK_COMMISSION ARC ,ACCOUNT_RANK AR ,CM_CMP_BRANCH CCB, BANK_LIST BL,SERVICE_LIST SL WHERE AR.ACCNT_RANK_ID<>'120519000000000001' AND BL.BANK_STATUS='A' AND AR.ACCNT_RANK_ID=ARC.ACCNT_RANK_ID AND CCB.CMP_BRANCH_ID=ARC.CMP_BRANCH_ID AND BL.BANK_INTERNAL_CODE=ARC.BANK_CODE AND SL.SERVICE_ID=ARC.SERVICE_ID AND(BSFW.SERVICE_ID='" +ddlServiceList.SelectedValue.ToString() + "') AND(CMB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString() + "') AND (BSFW.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') ORDER BY AR.ACCNT_RANK_ID,BL.BANK_NAME ,SL.SERVICE_TITLE ";

    //    }
    //    oDS = objSerHandlr.ExecuteQuery(strSQL);

    //    SaveAuditInfo("Report", "tranasaction limite Commission");
    //    strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
    //    strHTML = strHTML + "<tr><td COLSPAN=14 align=center style='border-right:none'><h2 align=center> Transaction Limit & Commission(" + rblSelectType.SelectedItem.ToString() + ") </h2></td></tr>";
    //    strHTML = strHTML + "</table>";
    //    strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
    //    strHTML = strHTML + "<tr>";

    //    strHTML = strHTML + "<td valign='middle' >SerialNo</td>";

    //    strHTML = strHTML + "<td valign='middle' >Branch</td>";
    //    strHTML = strHTML + "<td valign='middle' >  Bank Name  </td>";
    //    strHTML = strHTML + "<td valign='middle' >Service </td>";
       


    //    strHTML = strHTML + "<td valign='middle' >Account Rank</td>";
    //    strHTML = strHTML + "<td valign='middle' >Reward  </td>";
    //    strHTML = strHTML + "<td valign='middle' >Maximum Debit Amount Per Day </td>";
    //    strHTML = strHTML + "<td valign='middle' >Maximum Debit Amount Per Month</td>";
    //    strHTML = strHTML + "<td valign='middle' >Maximum Credit Amount Per Day </td>";

    //    strHTML = strHTML + "<td valign='middle' >Maximum Credit Amount Per Month</td>";
    //    strHTML = strHTML + "<td valign='middle' >Maximum Tranasction Per Day</td>";
    //    strHTML = strHTML + "<td valign='middle' > Maximum Tranasction Per Month</td>";
    //    strHTML = strHTML + "<td valign='middle' >Transaction Direction </td>";

    //    strHTML = strHTML + "<td valign='middle' >Remarks</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Bank commission(%)</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Agent commission(%)</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Pool Adjustment(%)</td>";

    //    //strHTML = strHTML + "<td valign='middle' >Vendor commission(%) </td>";
    //    //strHTML = strHTML + "<td valign='middle' >Third party commission(%)</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Channel commission(%)</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Tax paid by</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Fees paid by(%)</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Fees include Vat/Tax(%)</td>";

    //    strHTML = strHTML + "</tr>";

    //    try
    //    {
    //        if (oDS.Tables[0].Rows.Count > 0)
    //        {
    //            int SerialNo = 1;
    //            foreach (DataRow prow in oDS.Tables[0].Rows)
    //            {

    //                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";

    //                strHTML = strHTML + " <td > " + prow["CMP_BRANCH_NAME"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > " + prow["BANK_NAME"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > " + prow["SERVICE_TITLE"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["RANK_TITEL"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["CHANNEL_TYPE"].ToString() + "</td>";

    //                strHTML = strHTML + " <td > " + prow["RANK_TITEL"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > " + prow["COMMISSION_AMT"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > '" + prow["MAX_VALUE_PER_DAY"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > '" + prow["MAX_VALUE_PER_MONTH"].ToString() + " </td>";

    //                strHTML = strHTML + " <td > " + prow["MAX_CR_VALUE_PER_DAY"].ToString() + "</td>";
    //                strHTML = strHTML + " <td > " + prow["MAX_CR_VALUE_PER_MONTH"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > " + prow["MAX_TRAN_PER_DAY"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > " + prow["MAX_TRAN_PER_MONTH"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > " + prow["TRANSACTION_DIRECTION"].ToString() + " </td>";

    //                strHTML = strHTML + " <td > " + prow["REMARKS"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > '" + prow["BANK_COMM_AMOUNT"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > '" + prow["AGENT_COMM_AMOUNT"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["POOL_ADJUSTMENT"].ToString() + "</td>";
    //                //strHTML = strHTML + " <td > " + prow["VENDOR_COMMISSION"].ToString() + " </td>";

    //                //strHTML = strHTML + " <td > " + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["CHANNEL_COMMISSION"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["TAX_PAID_BY"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY"].ToString() + " </td>";

    //                //if (prow["FEE_INCLUDE_VAT_TAX"].ToString() == "Y")
    //                //{
    //                //    strHTML = strHTML + " <td > '" + "Yes" + " </td>";
    //                //}
    //                //else if (prow["FEE_INCLUDE_VAT_TAX"].ToString() == "N")
    //                //{
    //                //    strHTML = strHTML + " <td > '" + "No" + " </td>";
    //                //}

    //                strHTML = strHTML + " </tr> ";
    //                SerialNo = SerialNo + 1;
    //            }
    //        }
    //        strHTML = strHTML + "<tr>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        strHTML = strHTML + " </tr>";

    //        strHTML = strHTML + " </table>";
    //        clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //}
}
