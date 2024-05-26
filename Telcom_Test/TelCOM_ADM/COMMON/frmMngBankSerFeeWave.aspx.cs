using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class COMMON_frmMngBankSerFeeWave : System.Web.UI.Page   
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

    protected void dtvBankSerFee_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Save", "Service Type");
    }
    protected void dtvBankSerFee_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        //DropDownList ddlSrcRank = (DropDownList)dtvBankSerFee.FindControl("ddlSuorceRank");
        //e.Values["ACCNT_RANK_ID_SOURCE"] = ddlSrcRank.SelectedValue.ToString();


        //DropDownList ddlDestRank = (DropDownList)dtvBankSerFee.FindControl("ddlDestRank");
        //e.Values["ACCNT_RANK_ID_DEST"] = ddlDestRank.SelectedValue.ToString();


        //DropDownList ddlSerList = (DropDownList)dtvBankSerFee.FindControl("ddlSerList");
        //e.Values["SERVICE_ID"] = ddlSerList.SelectedValue.ToString();


        DropDownList ddlChnlName = (DropDownList)dtvBankSerFee.FindControl("ddlChnlName");
        e.Values["CHANNEL_TYPE_ID"] = ddlChnlName.SelectedValue.ToString();

        DropDownList ddlResticted = (DropDownList)dtvBankSerFee.FindControl("ddltranAllowed");
        e.Values["TRAN_ALLOWED"] = ddlResticted.SelectedValue.ToString();


        DropDownList ddlHierarchy = (DropDownList)dtvBankSerFee.FindControl("ddlHierarchy");
        e.Values["HIERARCHY_ALLOWED"] = ddlHierarchy.SelectedValue.ToString();

    }

    protected void gdvBankSerFeeWave_SelectedIndexChanged(object sender, EventArgs e)
    {
        SaveAuditInfo("Delete", "Service Type");

    }
    
    protected void gdvBankSerFeeWave_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Service Type"); 
    }
    protected void gdvBankSerFeeWave_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //DropDownList ddlSrcRank = (DropDownList)gdvBankSerFeeWave.Rows[e.RowIndex].FindControl("ddlAccRankSource");
        //e.NewValues["ACCNT_RANK_ID_SOURCE"] = ddlSrcRank.SelectedValue.ToString();

        //DropDownList ddlDestRank = (DropDownList)gdvBankSerFeeWave.Rows[e.RowIndex].FindControl("ddlAccRankDest");
        //e.NewValues["ACCNT_RANK_ID_DEST"] = ddlDestRank.SelectedValue.ToString();


        DropDownList ddlChnlName = (DropDownList)gdvBankSerFeeWave.Rows[e.RowIndex].FindControl("ddlChannelName");
        e.NewValues["CHANNEL_TYPE_ID"] = ddlChnlName.SelectedValue.ToString();

        DropDownList ddlResticted = (DropDownList)gdvBankSerFeeWave.Rows[e.RowIndex].FindControl("DropDownList8");
        e.NewValues["TRAN_ALLOWED"] = ddlResticted.SelectedValue.ToString();

        DropDownList ddlHierarchyLevel = (DropDownList)gdvBankSerFeeWave.Rows[e.RowIndex].FindControl("ddlHierarchyLevel");
        e.NewValues["HIERARCHY_ALLOWED"] = ddlHierarchyLevel.SelectedValue.ToString();


    }
    
    
    
    
    
    
    
    
    
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    clsServiceHandler objSerHandlr = new clsServiceHandler();
    //    DataSet oDS = new DataSet();
    //    string strSQL = "";
    //    string strHTML = "";
    //    string filename = "Bank_Fee_Wave";

    //    if (rblSelectType.SelectedValue == "0")
    //    {
    //        strSQL = " SELECT BSFW.WAVE_AMOUNT ,BSFW.SERVICE_ID,BSFW.CHANNEL_TYPE_ID,BSFW.CMP_BRANCH_ID,BSFW.BANK_CODE,BSFW.TRAN_ALLOWED,BSFW.ACCNT_RANK_ID_SOURCE,BSFW.ACCNT_RANK_ID_DEST , CMB.CMP_BRANCH_ID, CMB.CMP_BRANCH_NAME ,AR.ACCNT_RANK_ID,AR.RANK_TITEL AS SOURCE,ARA.ACCNT_RANK_ID,ARA.RANK_TITEL AS DESTINATION,BL.BANK_NAME,BL.BANK_INTERNAL_CODE,SL.SERVICE_ID, SL.SERVICE_TITLE,CT.CHANNEL_TYPE_ID,CT.CHANNEL_TYPE FROM BANK_SERVICE_FEE_WAVE BSFW ,CM_CMP_BRANCH CMB ,ACCOUNT_RANK AR,BANK_LIST BL,SERVICE_LIST SL,CHANNEL_TYPE CT,ACCOUNT_RANK ARA WHERE BANK_STATUS='A' AND BSFW.SERVICE_ID=SL.SERVICE_ID AND BSFW.CHANNEL_TYPE_ID= CT.CHANNEL_TYPE_ID AND  BSFW.CMP_BRANCH_ID=CMB.CMP_BRANCH_ID AND BSFW.ACCNT_RANK_ID_SOURCE=AR.ACCNT_RANK_ID AND BSFW.ACCNT_RANK_ID_DEST=ARA.ACCNT_RANK_ID AND BL.BANK_INTERNAL_CODE =BSFW.BANK_CODE ORDER BY  SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE ";
    //    }
    //    else if (rblSelectType.SelectedValue == "1")
    //    {

    //        strSQL = " SELECT BSFW.WAVE_AMOUNT ,BSFW.SERVICE_ID,BSFW.CHANNEL_TYPE_ID,BSFW.CMP_BRANCH_ID,BSFW.BANK_CODE,BSFW.TRAN_ALLOWED,BSFW.ACCNT_RANK_ID_SOURCE,BSFW.ACCNT_RANK_ID_DEST , CMB.CMP_BRANCH_ID, CMB.CMP_BRANCH_NAME,AR.ACCNT_RANK_ID,AR.RANK_TITEL AS SOURCE,ARA.ACCNT_RANK_ID,ARA.RANK_TITEL AS DESTINATION,BL.BANK_NAME,BL.BANK_INTERNAL_CODE, SL.SERVICE_ID, SL.SERVICE_TITLE,CT.CHANNEL_TYPE_ID,CT.CHANNEL_TYPE FROM BANK_SERVICE_FEE_WAVE BSFW ,CM_CMP_BRANCH CMB ,ACCOUNT_RANK AR,BANK_LIST BL,SERVICE_LIST SL,CHANNEL_TYPE CT,ACCOUNT_RANK ARA WHERE BANK_STATUS='A' AND BSFW.SERVICE_ID=SL.SERVICE_ID AND BSFW.CHANNEL_TYPE_ID= CT.CHANNEL_TYPE_ID AND  BSFW.CMP_BRANCH_ID=CMB.CMP_BRANCH_ID AND  BSFW.ACCNT_RANK_ID_SOURCE=AR.ACCNT_RANK_ID AND BSFW.ACCNT_RANK_ID_DEST=ARA.ACCNT_RANK_ID AND BL.BANK_INTERNAL_CODE =BSFW.BANK_CODE AND(CMB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString() + "') AND (BSFW.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') ORDER BY  SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE ";
    //    }
    //    else if (rblSelectType.SelectedValue == "2")
    //    {
    //        strSQL = " SELECT BSFW.WAVE_AMOUNT ,BSFW.SERVICE_ID,BSFW.CHANNEL_TYPE_ID,BSFW.CMP_BRANCH_ID,BSFW.BANK_CODE,BSFW.TRAN_ALLOWED,BSFW.ACCNT_RANK_ID_SOURCE,BSFW.ACCNT_RANK_ID_DEST , CMB.CMP_BRANCH_ID, CMB.CMP_BRANCH_NAME , AR.ACCNT_RANK_ID,AR.RANK_TITEL AS SOURCE,ARA.ACCNT_RANK_ID,ARA.RANK_TITEL AS DESTINATION,BL.BANK_NAME,BL.BANK_INTERNAL_CODE, SL.SERVICE_ID, SL.SERVICE_TITLE,CT.CHANNEL_TYPE_ID,CT.CHANNEL_TYPE FROM BANK_SERVICE_FEE_WAVE BSFW ,CM_CMP_BRANCH CMB ,ACCOUNT_RANK AR,BANK_LIST BL,SERVICE_LIST SL,CHANNEL_TYPE CT,ACCOUNT_RANK ARA  WHERE BANK_STATUS='A' AND BSFW.SERVICE_ID=SL.SERVICE_ID AND BSFW.CHANNEL_TYPE_ID= CT.CHANNEL_TYPE_ID AND  BSFW.CMP_BRANCH_ID=CMB.CMP_BRANCH_ID AND BSFW.ACCNT_RANK_ID_SOURCE=AR.ACCNT_RANK_ID  AND BSFW.ACCNT_RANK_ID_DEST=ARA.ACCNT_RANK_ID AND BL.BANK_INTERNAL_CODE =BSFW.BANK_CODE AND(BSFW.SERVICE_ID='" + ddlService.SelectedValue.ToString() + "') AND(CMB.CMP_BRANCH_ID='" + ddlBranch.SelectedValue.ToString() + "') AND (BSFW.BANK_CODE='" + ddlBankList.SelectedValue.ToString() + "') ORDER BY  SL.SERVICE_TITLE,AR.RANK_TITEL,CT.CHANNEL_TYPE ";
    //    }

    //    SaveAuditInfo("Report", "Bank fee Wave Report");

    //    oDS = objSerHandlr.ExecuteQuery(strSQL);
    //    SaveAuditInfo("Report", "Bank fee Wave Report");

    //    strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
    //    strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none'><h2 align=center> Bank fee Wave(" + rblSelectType.SelectedItem.ToString() + ") </h2></td></tr>";
    //    strHTML = strHTML + "</table>";
    //    strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
    //    strHTML = strHTML + "<tr>";

    //    strHTML = strHTML + "<td valign='middle' >SerialNo</td>";

    //    strHTML = strHTML + "<td valign='middle' >Branch</td>";
    //    strHTML = strHTML + "<td valign='middle' >  Bank Name  </td>";
    //    strHTML = strHTML + "<td valign='middle' >Service </td>";
    //    //strHTML = strHTML + "<td valign='middle' >Account Rank</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Channel Name</td>";



    //    strHTML = strHTML + "<td valign='middle' >Source Bank</td>";
    //    strHTML = strHTML + "<td valign='middle' >Destination Bank</td>";
    //    strHTML = strHTML + "<td valign='middle' >Wave Amount </td>";
    //    strHTML = strHTML + "<td valign='middle' >Channel Name</td>";
    //    strHTML = strHTML + "<td valign='middle' >Transaction Allowed</td>";

    //    //strHTML = strHTML + "<td valign='middle' >Vat and Tax(%)</td>";
    //    //strHTML = strHTML + "<td valign='middle' >AIT(%)</td>";
    //    //strHTML = strHTML + "<td valign='middle' > Fees Paid by bank(%)</td>";
    //    //strHTML = strHTML + "<td valign='middle' >Fees Paid by Customer(%) </td>";

    //    //strHTML = strHTML + "<td valign='middle' >Fees Paid by Agent(%)</td>";
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

    //                strHTML = strHTML + " <td > " + prow["SOURCE"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > " + prow["DESTINATION"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > '" + prow["WAVE_AMOUNT"].ToString() + " </td>";
    //                strHTML = strHTML + " <td > '" + prow["CHANNEL_TYPE"].ToString() + " </td>";

    //                //strHTML = strHTML + " <td > " + prow["MIN_FEE"].ToString() + "</td>";
    //                //strHTML = strHTML + " <td > " + prow["VAT_TAX"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["AIT"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_BANK"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_CUSTOMER"].ToString() + " </td>";

    //                //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY_AGENT"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > '" + prow["BANK_COMM_AMOUNT"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > '" + prow["AGENT_COMM_AMOUNT"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["POOL_ADJUSTMENT"].ToString() + "</td>";
    //                //strHTML = strHTML + " <td > " + prow["VENDOR_COMMISSION"].ToString() + " </td>";

    //                //strHTML = strHTML + " <td > " + prow["THIRD_PARTY_COMMISSION"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["CHANNEL_COMMISSION"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["TAX_PAID_BY"].ToString() + " </td>";
    //                //strHTML = strHTML + " <td > " + prow["FEES_PAID_BY"].ToString() + " </td>";

    //                if (prow["TRAN_ALLOWED"].ToString() == "Y")
    //                {
    //                    strHTML = strHTML + " <td > '" + "Yes" + " </td>";
    //                }
    //                else if (prow["TRAN_ALLOWED"].ToString() == "N")
    //                {
    //                    strHTML = strHTML + " <td > '" + "No" + " </td>";
    //                }

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
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
    //        //strHTML = strHTML + " <td > " + "" + " </td>";
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
