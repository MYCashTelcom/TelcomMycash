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

public partial class Forms_frmQueryInvitationStatus : System.Web.UI.Page
{
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
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
            txtFromDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt.AddHours(-6));
            txtToDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt.AddHours(6));
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    public void LoadRequestList()
    {
        String strSQL = "SELECT INVS.REQUEST_ID,INVS.RECEIPENT_PARTY,INVS.REQUEST_TIME,"
	                   + "DECODE(INVS.REQ_STATE,'P','In Que','Processwed') REQ_STATE,"
                       + "INVS.REQ_TEXT,INVS.SERVICE_COST,INVS.RSP_STATE,INVR.REQUEST_TIME RESPONSE_TIME,"
	                   + "DECODE(UPPER(INVR.ACCESS_CODE),'Y','Accepted','N','Declained', 'Pending')  INV_STATE "
	                   + "FROM ALL_INVITATION_STATUS INVS, ALL_INVITATION_RESPONSE INVR WHERE INVS.RECEIPENT_PARTY=INVR.REQUEST_PARTY(+) AND "
	                   + "SUBSTR(INVS.REQUEST_ID,7,6)=INVR.REQ_NO(+) AND "
                       + " INVS.ACCNT_ID='" + ddlAccountList.SelectedValue + "' AND INVS.REQUEST_TIME "
                       + "BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";

        sdsRequestList.SelectCommand = strSQL;
        sdsRequestList.DataBind();
        grvRequestList.DataBind();
    }
    protected void ddlAccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
}
