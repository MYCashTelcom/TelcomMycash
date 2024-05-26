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

public partial class Forms_frmContentHistory : System.Web.UI.Page
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
            LoadContentHistory();
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
    public void LoadContentHistory()
    {
        String strSQL = "SELECT CG.CONTENT_GRP_NAME,CL.CONTENT_TITLE,CUH.CONTENT_USER_MSISDN,CUH.CONTENT_USED_TIME,CUH.CONTENT_USER_HANDSET,"
                      + "CUH.CONTENT_PRICE_DEDUCTED,CUH.CONTENT_BILLING_INFO FROM CONTENT_USED_HISTORY CUH, CONTENT_LIST CL,"
                      + "CONTENT_GROUP CG WHERE CUH.CONTENT_ID=CL.CONTENT_ID AND CG.CONTENT_GRP_ID=CL.CONTENT_GRP_ID "
                      + " AND CUH.CONTENT_USED_TIME "
                      + "BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";

        sdsContHistory.SelectCommand = strSQL;
        sdsContHistory.DataBind();
        grvContentDLHistory.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadContentHistory();
    }
}
