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
using System.Drawing;

public partial class Forms_frmQuerySubmitStatus : System.Web.UI.Page
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
                    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-30));
                    // txtFromDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-120));
                    // txtToDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
                    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
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
        //String strSQL = "SELECT SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,DECODE(SR.REQUEST_STAE,'P','In Que','E','Expired','Done') REQ_STATE,"
        //           + " DECODE(INSTR((REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'*CPIN1')),'PIN'),0, "
        //           + " REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'*CPIN1'),SUBSTR(SR.REQUEST_TEXT,0,5)||'...') REQ_TEXT, "
        //           + " SR.SERVICE_COST,RSP.RESPONSE_TIME,RSP.RESPONSE_ID,DECODE(RSP.RESPONSE_STAE,'D','Replied','P','In Que',NULL, "
        //           + " DECODE(UPPER(SR.REQUEST_STAE),'E','Canceled',DECODE(UPPER(SR.REQUEST_TEXT),'Y','N/A','Waitting')), "
        //           + " DECODE(UPPER(SR.REQUEST_STAE),'E','Canceled','Replied')) RSP_STATE,DECODE(INSTR(RSP.RESPONSE_MESSAGE,'OTP'),0, "
        //           + " DECODE(INSTR(RSP.RESPONSE_MESSAGE,'PIN'),0,RSP.RESPONSE_MESSAGE,SUBSTR(RSP.RESPONSE_MESSAGE,0,INSTR(RSP.RESPONSE_MESSAGE,'PIN')+3)||'...'), "
        //           + " SUBSTR(RSP.RESPONSE_MESSAGE,0,INSTR(RSP.RESPONSE_MESSAGE,'OTP')+3)||'...') RESPONSE_MESSAGE "
        //           + " FROM SERVICE_REQUEST SR,SERVICE_RESPONSE RSP WHERE SR.REQUEST_ID=RSP.REQUEST_ID(+) "
        //           + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";

        string strSQL = "";
        strSQL = "SELECT REQUEST_ID,REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TIME,REQ_STATE,REQUEST_STAE, "
               + "  DECODE(INSTR(REQ_TEXT,'OTP',1),0,REQ_TEXT,SUBSTR(REQ_TEXT,0,5)||'...') REQ_TEXT,  "
               + " SERVICE_COST,RESPONSE_TIME,RESPONSE_ID,RSP_STATE,RESPONSE_STAE,RESPONSE_MESSAGE  FROM VW_REQUEST_MONITOR3 SR WHERE "
               + " SR.REQUEST_TIME BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND SR.REQUEST_PARTY LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtServiceCode.Text.Equals(""))
        {
            strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQ_TEXT,1,INSTR(SR.REQ_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        }
       strSQL=strSQL+ " ORDER BY SR.REQUEST_ID DESC,SR.REQUEST_PARTY";

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
        SaveAuditInfo("View", "Query Request Status");
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
}
