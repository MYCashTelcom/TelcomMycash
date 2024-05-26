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

public partial class TEXT_frmReconciliation : System.Web.UI.Page
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
        DateTime dt = DateTime.Now;
        if (txtFromDate.DateString.Equals(""))
        {
            txtFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(-1 * (dt.Day - 1)));
            txtToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);
            //LoadRequestList();
        }
        //
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
        String strSQL = "";
        //String strSQL = "SELECT SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,"
        //        + "DECODE(SR.REQUEST_STAE,'P','In Que','Processwed') REQ_STATE,"
        //        + "SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',2)+1,LENGTH(SR.REQUEST_TEXT)-INSTR(SR.REQUEST_TEXT,'*',2)-1) REQ_TEXT,"
        //        + "SR.SERVICE_COST,RSP.RESPONSE_TIME,DECODE(RSP.RESPONSE_STAE,'P','In Que',NULL,'Waiting','Replied') RSP_STATE"
        //        + " FROM SERVICE_REQUEST SR,SERVICE_RESPONSE RSP WHERE SR.REQUEST_ID=RSP.REQUEST_ID(+) "
        //        + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";

         strSQL = " SELECT SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,"
                + " DECODE(SR.REQUEST_STAE,'P','In Que','Done') REQ_STATE,"
                + " SR.REQUEST_TEXT REQ_TEXT,"
                + " SR.SERVICE_COST,PKG_SMSGW_SERVICES.GET_VAS_ACTIVATION_STATUS(SR.REQUEST_ID) RSP_STATE"
                + " FROM ALL_SERVICE_REQUEST SR WHERE SUBSTR(SR.SERVICE_INTERNAL_CODE,1,4)='REQF' "
                + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + txtFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND SR.REQUEST_PARTY LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtServiceCode.Text.Equals(""))
        {
            strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        }
        //AND "
        //+ " SR.ACCNT_ID='" + ddlAccountList.SelectedValue + "' ";

        //sdsRequestList.SelectCommand = strSQL;
        //sdsRequestList.DataBind();
        //grvRequestList.DataBind();
    }
    protected void ddlAccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //string strFilter = "";
            if (rdbreportchoose.SelectedValue.ToString() == "QR")
            {

                String strSQL = "SELECT SUBSTR(REQ_TEXT,LENGTH(REQ_TEXT)-11,10) CUS_MSISDN, SR.SERVICE_TITLE,SR.REQUEST_TIME,AL.DISTRIBUTORCODE,"
                                + " AL.DISTRIBUTORNAME,AL.RSPCODE,AL.RSPNAME,AL.RSPEASYLOADNUMBER,AL.ACCNT_NO,"
                                + " AL.RETAILER_NAME,AL.ACCNT_MSISDN,SSR.RETAILER_REWARD RETAILER_AMOUNT,AL.DSRCODE,AL.DSRNAME,SSR.DSR_REWARD DSR_AMOUNT ,"
                                + " SR.RSP_SUCESS FROM ALL_FORWARD_REQ_RECONCIL SR,ACCOUNT_LIST AL, SERVICE_RATE SSR WHERE SR.ACCNT_NO=AL.ACCNT_NO "
                                + " AND SSR.SERVICE_ID=SR.SERVICE_ID AND SR.RSP_SUCESS=1 AND SR.SERVICE_REWARD<>0 AND SR.REQUEST_TIME "
                                + " BETWEEN TO_DATE('" + txtFromDate.DateString + "','dd/mm/yyyy HH24:MI:SS')AND TO_DATE('" + txtToDate.DateString + "','dd/mm/yyyy HH24:MI:SS')";

                Session["CompanyBranch"] = "ROBI";
                Session["ReportSQL"] = strSQL;
                //Session["RequestForm"] = "../TEXT/frmReconciliation.aspx";
                Session["RequestForm"] = "../TEXT/frmVASQualified.aspx";
                Session["ReportFile"] = "../TEXT/crptVASQualified.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }

            if (rdbreportchoose.SelectedValue.ToString() == "DR")
            {

                String strSQL = "SELECT SUBSTR(REQ_TEXT,LENGTH(REQ_TEXT)-11,10) CUS_MSISDN, SR.SERVICE_TITLE,SR.REQUEST_TIME,AL.DISTRIBUTORCODE,"
                                + " AL.DISTRIBUTORNAME,AL.RSPCODE,AL.RSPNAME,AL.RSPEASYLOADNUMBER,AL.ACCNT_NO,AL.RETAILER_NAME,AL.ACCNT_MSISDN,"
                                + " SSR.RETAILER_REWARD RETAILER_AMOUNT,AL.DSRCODE,AL.DSRNAME,SSR.DSR_REWARD DSR_AMOUNT ,SR.RSP_SUCESS "
                                + " FROM ALL_FORWARD_REQ_RECONCIL SR,ACCOUNT_LIST AL, SERVICE_RATE SSR WHERE SR.ACCNT_NO=AL.ACCNT_NO "
                                + " AND SSR.SERVICE_ID=SR.SERVICE_ID AND SR.RSP_FAILED=1 AND SR.REQUEST_TIME "
                                + " BETWEEN TO_DATE('" + txtFromDate.DateString + "','dd/mm/yyyy HH24:MI:SS')AND TO_DATE('" + txtToDate.DateString + "','dd/mm/yyyy HH24:MI:SS')";



                Session["CompanyBranch"] = "ROBI";
                Session["ReportSQL"] = strSQL;
                //Session["RequestForm"] = "../TEXT/frmReconciliation.aspx";
                Session["RequestForm"] = "../TEXT/frmVASQualified.aspx";
                Session["ReportFile"] = "../TEXT/crptVASDisQualified.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }

            if (rdbreportchoose.SelectedValue.ToString() == "WR")
            {

                String strSQL = "SELECT SUBSTR(REQ_TEXT,LENGTH(REQ_TEXT)-11,10) CUS_MSISDN, SR.SERVICE_TITLE,SR.REQUEST_TIME,AL.DISTRIBUTORCODE,"
                                + " AL.DISTRIBUTORNAME,AL.RSPCODE,AL.RSPNAME,AL.RSPEASYLOADNUMBER,AL.ACCNT_NO,AL.RETAILER_NAME,AL.ACCNT_MSISDN,"
                                + " AL.DSRCODE,AL.DSRNAME,SR.RSP_Waiting FROM ALL_FORWARD_REQ_RECONCIL SR,ACCOUNT_LIST AL WHERE SR.ACCNT_NO=AL.ACCNT_NO AND SR.RSP_Waiting=1"
                                + " AND SR.REQUEST_TIME "
                                + " BETWEEN TO_DATE('" + txtFromDate.DateString + "','dd/mm/yyyy HH24:MI:SS')AND TO_DATE('" + txtToDate.DateString + "','dd/mm/yyyy HH24:MI:SS')";

                Session["CompanyBranch"] = "ROBI";
                Session["ReportSQL"] = strSQL;
                //Session["RequestForm"] = "../TEXT/frmReconciliation.aspx";
                Session["RequestForm"] = "../TEXT/frmVASQualified.aspx";
                Session["ReportFile"] = "../TEXT/crptVASWaitingList.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            SaveAuditInfo("Preview", "VAS Qualified and Disqualified");
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
    protected void rdbcheck_CheckedChanged(object sender, EventArgs e)
    {

    }
}
