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

public partial class COMMON_frmOperatorServCom : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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
    protected void dtvOperatorServCom_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        DropDownList ddlChannel = (DropDownList)dtvOperatorServCom.FindControl("ddlChannelName");
        e.Values["OPARETOR_NAME"] = ddlChannel.SelectedItem.ToString();
        e.Values["CHANNEL_TYPE_ID"] = ddlChannel.SelectedValue.ToString();
        e.Values["OPARETOR_CODE"] = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("CHANNEL_TYPE", "CHANNEL_TYPE", "CHANNEL_TYPE_ID", ddlChannel.SelectedValue.ToString());
    }
    protected void gdvOperatorServComi_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddlOperator = (DropDownList)gdvOperatorServComi.Rows[e.RowIndex].FindControl("ddlOpertrName");        
        e.NewValues["OPARETOR_NAME"] = ddlOperator.SelectedItem.ToString();
        e.NewValues["CHANNEL_TYPE_ID"] = ddlOperator.SelectedValue.ToString();        
        e.NewValues["OPARETOR_CODE"] = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("CHANNEL_TYPE", "CHANNEL_TYPE", "CHANNEL_TYPE_ID", ddlOperator.SelectedValue.ToString());
    }
}
