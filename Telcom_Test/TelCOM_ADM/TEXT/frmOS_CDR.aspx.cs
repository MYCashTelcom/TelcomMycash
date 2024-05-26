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

public partial class TEXT_frmOS_CDR : System.Web.UI.Page
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
        if (txtFromDate.Text.Equals(""))
        {
            txtFromDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(-1));
            txtToDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(1));
            LoadRequestList();
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
        //String strSQL = "SELECT SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,"
        //        + "DECODE(SR.REQUEST_STAE,'P','In Que','Processwed') REQ_STATE,"
        //        + "SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',2)+1,LENGTH(SR.REQUEST_TEXT)-INSTR(SR.REQUEST_TEXT,'*',2)-1) REQ_TEXT,"
        //        + "SR.SERVICE_COST,RSP.RESPONSE_TIME,DECODE(RSP.RESPONSE_STAE,'P','In Que',NULL,'Waiting','Replied') RSP_STATE"
        //        + " FROM SERVICE_REQUEST SR,SERVICE_RESPONSE RSP WHERE SR.REQUEST_ID=RSP.REQUEST_ID(+) "
        //        + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";

        String strSQL = "SELECT S.CDR_TYPE, S.A_PARTY, S.B_PARTY,S.TIME_STAMP, S.DURATION, S.COST,S.TEXT, S.CDR_FILE"
                + " FROM SERVICE_CDR_FO_PARTY S WHERE S.TIME_STAMP BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND S.A_PARTY LIKE '%" + txtRequestParty.Text + "%'";
        }
        //if (!txtServiceCode.Text.Equals(""))
        //{
        //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        //}
        //AND "
        //+ " SR.ACCNT_ID='" + ddlAccountList.SelectedValue + "' ";

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
        SaveAuditInfo("Search", "Other System CDRr");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
