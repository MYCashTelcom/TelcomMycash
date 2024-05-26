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

public partial class Forms_frmContentTextEdit : System.Web.UI.Page
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
    protected void ddlContGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadContentList();
    }
    public void LoadContentList()
    {
        string strSQL = "SELECT CONTENT_ID, CONTENT_DETAIL_ID, CONTENT_TEXT_FILE, CONTENT_WIDTH, COTENT_CODE ,"
                      + "CONTENT_HEIGHT FROM CONTENT_DETAIL WHERE CONTENT_ID IN (SELECT CONTENT_ID FROM CONTENT_LIST WHERE CONTENT_GRP_ID='" + ddlContGroup.SelectedValue + "')";

        sdsContentDetail.SelectCommand = strSQL;
        sdsContentDetail.DataBind();
        grvContentDetail.DataBind();
    }
}
