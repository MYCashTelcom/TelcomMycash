using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class Forms_frmMngClientAccount3 : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
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
        lblMsg.Text = "";
        MultiView1.ActiveViewIndex = 0;

        string s1 = "";
        s1 = Request["__EVENTTARGET"];

        if(Page.IsPostBack)
        {
            if (Session["strQry"] != null && s1 != "" && s1 != "FormView1$CancelUpdateButton") // not for 1st time, new search & cancel
            {
                string strQ=Session["strQry"].ToString();
                SqlDataSource1.SelectCommand = strQ;
                SqlDataSource1.DataBind();
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
    protected void btnAccountList_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnNewAccount_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        //if (txtName.Text.ToString().Trim() == "")
        //{
        //    lblMsg.Text = "Enter client name.";
        //    return;
        //}
        //else 
        //if (txtAccCode.Text.ToString().Trim() == "")
        //{
        //    lblMsg.Text = "Enter account code.";
        //    return;
        //}

        if (txtName.Text.ToString().Trim() == "" && txtAccCode.Text.ToString().Trim() == "") // when both blank
        {
            lblMsg.Text = "Enter Channel Name or Channel Code.";
            return;
        }

        //string strFilter;
        //strFilter = "";

        string strSql = "";

        if (txtName.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() == "") // only with name
        { 
            //strFilter=" OR C.CLINT_NAME LIKE '" + txtName.Text.ToString().Trim() + "%'";
            //strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE (C.CLINT_NAME LIKE '" + txtName.Text.ToString().Trim() + "%' AND A.CLINT_ID=C.CLINT_ID) ORDER BY A.ACCNT_NO ASC"; // AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID
            strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE (UPPER(C.CLINT_NAME) LIKE UPPER('%" + txtName.Text.ToString().Trim() + "%') AND A.CLINT_ID=C.CLINT_ID) ORDER BY A.ACCNT_NO ASC";
        }
        else if ((txtName.Text.ToString().Trim() == "" && txtAccCode.Text.ToString().Trim() != "") || (txtName.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() != "")) // only with account code or with both
        {
            strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE (A.ACCNT_NO='" + txtAccCode.Text.ToString().Trim() + "' AND A.CLINT_ID=C.CLINT_ID)"; // AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID
        }

        //string strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE ((A.ACCNT_NO='" + txtAccCode.Text.ToString().Trim() + "'" + strFilter + ") AND A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID)";
        //string strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE ((A.ACCNT_NO='" + txtAccCode.Text.ToString().Trim() + "' AND C.CLINT_NAME LIKE '" + txtName.Text.ToString().Trim() + "%') AND A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID)";
        
        
        //string strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE ((A.ACCNT_NO='" + txtAccCode.Text.ToString().Trim() + "') AND A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID)";
        Session["strQry"] = "";
        Session["strQry"] = strSql;
        SqlDataSource1.SelectCommand = strSql;
        SqlDataSource1.DataBind();     
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Channel Account");
    }
    protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Insert", "Channel Account");
    }
}
