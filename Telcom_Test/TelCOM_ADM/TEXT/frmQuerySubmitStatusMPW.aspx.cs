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

public partial class TEXT_frmQuerySubmitStatusMPW : System.Web.UI.Page
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
                DateTime dt = DateTime.Now;
                if (dptFromDate.DateString != "")
                {
                    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-15));
                    // txtFromDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-120));
                    // txtToDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
                    //dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
                    LoadRequestList();
                }
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

    public void LoadRequestList()
    {
        //String strSQL = " SELECT SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,DECODE(SR.REQUEST_STAE,'P','In Que','E','Expired','Done') REQ_STATE,"
        //              + " DECODE(INSTR((REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'*CPIN1')),'PIN'),0, "
        //              + " REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'*CPIN1'),SUBSTR(SR.REQUEST_TEXT,0,5)||'...') REQ_TEXT, "
        //              + " SR.SERVICE_COST,RSP.RESPONSE_TIME,RSP.RESPONSE_ID,DECODE(RSP.RESPONSE_STAE,'D','Replied','P','In Que',NULL, "
        //              + " DECODE(UPPER(SR.REQUEST_STAE),'E','Canceled',DECODE(UPPER(SR.REQUEST_TEXT),'Y','N/A','Waitting')), "
        //              + " DECODE(UPPER(SR.REQUEST_STAE),'E','Canceled','Replied')) RSP_STATE,DECODE(INSTR(RSP.RESPONSE_MESSAGE,'OTP'),0, "
        //              + " DECODE(INSTR(RSP.RESPONSE_MESSAGE,'PIN'),0,RSP.RESPONSE_MESSAGE,SUBSTR(RSP.RESPONSE_MESSAGE,0,INSTR(RSP.RESPONSE_MESSAGE,'PIN')+3)||'...'), "
        //              + " SUBSTR(RSP.RESPONSE_MESSAGE,0,INSTR(RSP.RESPONSE_MESSAGE,'OTP')+3)||'...') RESPONSE_MESSAGE "
        //              + " FROM SERVICE_REQUEST SR,SERVICE_RESPONSE RSP WHERE SR.REQUEST_ID=RSP.REQUEST_ID(+) "
        //              + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";

        string strSQL = "";
        strSQL = " SELECT DISTINCT SR.REQUEST_ID, SR.REQUEST_PARTY, SR.RECEIPENT_PARTY, SR.REQUEST_TIME, SR.REQ_STATE, SR.REQUEST_STAE, "
               + " DECODE(INSTR(SR.REQ_TEXT,'OTP',1),0,SR.REQ_TEXT,SUBSTR(SR.REQ_TEXT,0,5)||'...') REQ_TEXT,  "
               + " SR.SERVICE_COST, SR.RESPONSE_TIME, SR.RESPONSE_ID, SR.RSP_STATE, SR.RESPONSE_STAE, SR.RESPONSE_MESSAGE, SR.CCT_OWNER_CODE "
               + " FROM VW_REQUEST_MONITOR3 SR, ACCOUNT_LIST AL WHERE  SR.REQUEST_PARTY = AL.ACCNT_MSISDN  AND (AL.ACCNT_RANK_ID IN ('161215000000000002', '161215000000000003', '161215000000000004') OR (SR.CCT_OWNER_CODE = 'PW')) AND"
               + " SR.REQUEST_TIME BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND SR.REQUEST_PARTY LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtServiceCode.Text.Equals(""))
        {
            strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQ_TEXT,1,INSTR(SR.REQ_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        }
        strSQL = strSQL + " ORDER BY SR.REQUEST_ID DESC,SR.REQUEST_PARTY";

        sdsRequestList.SelectCommand = strSQL;
        sdsRequestList.DataBind();
        grvRequestList.DataBind();
    }
    protected void ddlAccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadRequestList();
        SaveAuditInfo("View", "Query Request Status Manual");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void grvRequestList_Sorting(object sender, GridViewSortEventArgs e)
    {
        LoadRequestList();
    }
    protected void grvRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //------------ Requist Cell --------------
        //        {
        //            if (e.Row.Cells[4].Text.ToString() != "Done")
        //            {                     
        //                e.Row.BackColor = Color.FromArgb(Convert.ToInt32("FF8888", 16));
        //            }
        //            //----------- Response Cell -----------
        //            else if (e.Row.Cells[7].Text.ToString() != "Replied" && e.Row.Cells[7].Text.ToString() != "Waiting")
        //            {
        //                e.Row.BackColor = Color.FromArgb(Convert.ToInt32("FFFF00", 16));
        //                //e.Row.BackColor = Color.LightYellow;//Cells[10]
        //            }
        //        }
        //    }
        //}
        //catch(Exception ex)
        //{
        //    ex.Message.ToString();
        //}
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int intCountGrd = grvRequestList.Rows.Count;
        try
        {
            string strHTML = "";
            string filename = "Query_Request_Status";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none' expan=true><h2 align=center> Query Request Status </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + "<td valign='middle' >Request ID</td>";
            strHTML = strHTML + "<td valign='middle' >Request Party</td>";
            // strHTML = strHTML + "<td valign='middle' >Receipent Party</td>";
            strHTML = strHTML + "<td valign='middle' >Request Time</td>";
            // strHTML = strHTML + "<td valign='middle' >Request State</td>";

            strHTML = strHTML + "<td valign='middle' >Request Message</td>";

            // strHTML = strHTML + "<td valign='middle' >Response ID</td>";
            //  strHTML = strHTML + "<td valign='middle' >Response State</td>";
            //  strHTML = strHTML + "<td valign='middle' >Response Time</td>";
            strHTML = strHTML + "<td valign='middle' >Response Message</td>";
            strHTML = strHTML + "</tr>";
            for (int intCnt = 0; intCnt < intCountGrd; intCnt++)
            {
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >'" + grvRequestList.Rows[intCnt].Cells[0].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >'" + grvRequestList.Rows[intCnt].Cells[1].Text + "</td>";
                // strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[2].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[3].Text + "</td>";
                //  strHTML = strHTML + "<td valign='middle' >'" + grvRequestList.Rows[intCnt].Cells[4].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[5].Text + "</td>";
                //  strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[6].Text + "</td>";
                //  strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[7].Text + "</td>";
                //  strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[8].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[9].Text + "</td>";
                strHTML = strHTML + "</tr>";
            }
            strHTML = strHTML + "</table>";
            clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void grvRequestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string strAdditionalQuery = "", strRequestID = "", strSQL = "", strAddReqID = "", str = "";

        strAdditionalQuery = " CT.REQUEST_ID='" + grvRequestList.Rows[grvRequestList.SelectedIndex].Cells[0].Text + "' ORDER BY CT.CAS_TRAN_DATE DESC";
        // strAddReqID = " CT.REQUEST_ID='" + txtRequistID.Text.Trim() + "' ORDER BY CT.REQUEST_ID DESC";
        strSQL = " SELECT CT.REQUEST_ID,CT.CAS_ACC_ID,C.CAS_ACC_NO,CI.CI_CLIENT_NAME,CI.CI_PRE_ADDRESS_1,CI.CI_PRE_ADDRESS_2,CI.CI_PER_ADDRESS_1,CI.CI_PER_ADDRESS_2,CT.CAS_TRAN_DATE,Round(NVL(DECODE(CT.CAS_TRAN_TYPE,'D', CAS_TRAN_AMT,''),0),4) AS DEBIT,'" + dptFromDate.DateString + " ' F_DATE,'" + dtpToDate.DateString + " ' T_DATE,"
                         + " Round(NVL(DECODE(CT.CAS_TRAN_TYPE, 'C', CAS_TRAN_AMT, ''),0),4)AS CREDIT,CT.CAS_TRAN_TYPE,(UPPER(ASR.ACCESS_CODE)||'  '|| CT.CAS_TRAN_DESC) AS CAS_TRAN_DESC "
                         + " FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST C,BDMIT_ERP_101.CR_CLIENT_INFO CI,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CT,APSNG101.ALL_SERVICE_REQUEST ASR "
                         + " WHERE CI.CI_CLIENT_ID=C.CI_CLINET_ID AND CT.CAS_ACC_ID = C.CAS_ACC_ID AND "
                         + "  CT.REQUEST_ID =ASR.REQUEST_ID(+) AND " + strAdditionalQuery;

        //strRequestID = " SELECT DISTINCT CT.REQUEST_ID FROM CAS_ACCOUNT_LIST C,"
        //              + " CAS_ACCOUNT_TRANSACTION CT,CR_CLIENT_INFO CI,APSNG101.ALL_SERVICE_REQUEST ASR WHERE CI.CI_CLIENT_ID=C.CI_CLINET_ID AND "
        //             + " CT.CAS_ACC_ID = C.CAS_ACC_ID AND CT.REQUEST_ID =ASR.REQUEST_ID(+) AND " + strAddReqID;


        //try
        //{
        //    sdsBankTrans.SelectCommand = strSQL;
        //    sdsBankTrans.DataBind();
        //    grvRequestList.DataBind();

        //    sdsRequestID.SelectCommand = strRequestID;
        //    sdsRequestID.DataBind();
        //    ddlReQuestID.DataBind();
        //    if (grvRequestList.Rows.Count > 0)
        //    {
        //        lblRequestID.Visible = true;
        //        ddlReQuestID.Visible = true;
        //        btnView.Visible = true;
        //        grvIndividualRequestList.DataSource = null;
        //    }
        //}
        //catch (Exception exx)
        //{
        //    exx.Message.ToString();
        //}

        Session["CompanyBranch"] = "";
        Session["ReportSQL"] = strSQL;
        Session["RequestForm"] = "../TEXT/frmQuerySubmitStatusM.aspx";
        Session["ReportFile"] = "../TEXT/crptQuery_Submit_Status_Manual.rpt";
        Response.Redirect("../COM/COM_ReportView.aspx");


    }
}
