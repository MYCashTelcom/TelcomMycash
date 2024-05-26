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

public partial class Forms_frmMngGroupSMS_Submit : System.Web.UI.Page
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
    protected void dlvServiceType_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        DateTime dt = DateTime.UtcNow;
        sdsGroupSMS.InsertParameters[1].DefaultValue = ddlAccountList.SelectedValue;
        sdsGroupSMS.InsertParameters[2].DefaultValue = ddlGroupList.SelectedValue;
        sdsGroupSMS.InsertParameters[5].DefaultValue = String.Format("{0:yyMMddHHmmss}", dt);
    }
    protected void dlvServiceType_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        //e.NewValues["ACCNT_GROUP_ID"] = ((DropDownList)((FormView)sender).FindControl("ddlAccountList")).SelectedValue;
        ////e.NewValues["Cityid"] = ((DropDownList)((FormView)sender).FindControl("DropDownList4")).SelectedValue
    }
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        DetailsView frmV = (DetailsView)ddl.NamingContainer;

        if (frmV.DataItem != null)
        {
            string strCity = ddl.SelectedValue.ToString();
            DropDownList ddlSubdivision = (DropDownList)frmV.FindControl("ddlAccountList");
            ddl.ClearSelection();
            ListItem lm = ddl.Items.FindByValue(strCity);

            if (lm == null)

            { ddl.SelectedIndex = 0; }

            else

            { lm.Selected = true; }
        }
    }
}
