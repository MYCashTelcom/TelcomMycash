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
using Coolite.Ext.Web;

public partial class frmLogin : System.Web.UI.Page
{
    clsGlobalSetup objGs = new clsGlobalSetup();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!objSysAdmin.SetConnectionString().Equals(""))
            {
                Response.Write("Lisence is not installed");
            }
            else
            {
                DataSet oSS = new DataSet();
                oSS = objGs.GetSolnNameAndCpyRtInfo();

                if (oSS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow sRow in oSS.Tables[0].Rows)
                    {
                        //string SolnName = objGs.GetSolnNameAndCpyRtInfo();
                        string SolnName = sRow["SOLN_NAME_BFR_LOGIN"].ToString();
                        string CpyRtInfo = sRow["COPYRIGHT_INFO"].ToString();
                        string TitleBeforeLogin = sRow["TITLE_BFR_LOGIN"].ToString();

                        //header.InnerHtml = "<h1>" + SolnName + "</h1>";
                        Panel4.Html = "<div class='message'>&nbsp;&nbsp;</div><div id='header'><h1>" + SolnName + "</h1></div>";
                        CopyRightMsg.InnerHtml = "<h1>" + CpyRtInfo + "</h1>";
                        Title = TitleBeforeLogin; //Md.Asaduzzaman
                    }
                }
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

    protected void Button1_Click(object sender, AjaxEventArgs e)
    {
        // Do some Authentication...

        // Then user send to application
        Response.Redirect("Desktop.aspx");
    }
    protected void LoadUser()
    {
        //this.cmbUser.Items.Insert(0, new Coolite.Ext.Web.ListItem("None", "0"));
    }
    protected void btnLogin_Click(object sender, AjaxEventArgs e)
    {
        //this.Window1.Hide();
        pnlLogin.Title = "Login";
        //----------------------
        string username = this.txtUsername.Text;
        string password = this.txtPassword.Text;
        clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        clsGlobalSetup objGS = new clsGlobalSetup();
        DataSet dtsLoginUser = new DataSet();
        dtsLoginUser = objSysAdmin.LoginWithUserName(username, password);

        if (dtsLoginUser.Tables["CM_SYSTEM_USERS"].Rows.Count == 1)
        {
            foreach (DataRow prow in dtsLoginUser.Tables["CM_SYSTEM_USERS"].Rows)
            {
                Session["CompanyBranch"] = "N/A";
                Session["Branch_Type"] = prow["CMP_BRANCH_TYPE_ID"].ToString();
                Session["Branch_ID"] = prow["CMP_BRANCH_ID"].ToString();
                Session["UserDname"] = prow["SYS_USR_DNAME"].ToString();
                Session["UserLoginName"] = prow["SYS_USR_LOGIN_NAME"].ToString();
                Session["Password"] = prow["SYS_USR_PASS"].ToString();
                Session["UserID"] = prow["SYS_USR_ID"].ToString();
                Session["GroupID"] = prow["SYS_USR_GRP_ID"].ToString();
                Session["UserEmail"] = prow["SYS_USR_EMAIL"].ToString();
                Session["CompanyName"] = objGS.GetCompanyName();
                Session["CmpanyAddress"] = "Jahangir Tower, Kawran Bazar";
                Session["AccountID"] = prow["ACCNT_ID"].ToString();
                Session.Timeout = 720;
            }
            //Audit Log
            string IPAddress = Request.ServerVariables["remote_addr"];
            string Technology = Request.Browser.Browser + Request.Browser.Version;
            string IPTechnology = IPAddress + "-" + Technology;
            objSysAdmin.AddAuditLog(Session["UserID"].ToString(), "Login", IPTechnology, objSysAdmin.GetCurrentPageName(), "Log in to Application");
            Session["UserIDD"] = Session["UserID"].ToString();
            Response.Redirect("frmWellcome.aspx");
            //Response.Redirect("frmTest.aspx");
        }
        else
        {
            pnlLogin.Title = "Login [ Please insert correct username & password ]";
        }
        
    }
    protected void btnClear_Click(object sender, AjaxEventArgs e)
    {
        pnlLogin.Title = "Login";
        this.txtUsername.Text="";
        this.txtPassword.Text="";
    }

 
}
