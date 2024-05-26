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
//using  
using System.Net;
using System.Threading;
using System.Collections.Generic;
using Ext.Net;
using System.Xml;
using System.Drawing.Imaging;
public partial class frmLogin : System.Web.UI.Page
{
    clsGlobalSetup objGs = new clsGlobalSetup();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string SessionId = string.Empty;
    string Flag;
    static int count = 0;
    protected void Page_Load(object sender, EventArgs e)
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

        //if (Session["LoginError"] != null)
        //{
        //    pnlLogin.Title = Session["LoginError"].ToString();
        //}        
    }
    public string GetComputerName(string clientIP)
    {
        try
        {
            var hostEntry = Dns.GetHostEntry(clientIP);
            return hostEntry.HostName;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
    protected void Button1_Click(object sender, DirectEventArgs  e)
    {
        // Do some Authentication...

        // Then user send to application
        Response.Redirect("Desktop.aspx");
    }
    protected void LoadUser()
    {
        //this.cmbUser.Items.Insert(0, new Coolite.Ext.Web.ListItem("None", "0"));
    }
    protected void btnLogin_Click(object sender, DirectEventArgs e) 
    
    {
        pnlLogin.Title = "Login";
        //----------------------
        string username = this.txtUsername.Text;
        string password = this.txtPassword.Text;
		
		string captcha = this.txtTuring.Text;

        if (captcha != Session["CaptchaImageText"].ToString())
        {
            pnlLogin.Title = "Captcha does not matched.";
            return;
        }

        bool usernameContain = username.All(char.IsLetterOrDigit);
        if (!usernameContain)
        {
            pnlLogin.Title = "Username must not contain any symbol";
            return;
        }

        try
        {
            string strSysUsrId = objSysAdmin.getUserId(username);
            AccessToSystem(username, password, strSysUsrId);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            pnlLogin.Title = "Invalid username or password";
        }
    }
    private void AccessToSystem(string username, string password, string SysUsrId)
    {
        DataSet dtsLoginUser = new DataSet();
        string SessionId = objSysAdmin.GetSessionID(username);
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
                Session["CompanyName"] = objGs.GetCompanyName();
                Session["CmpanyAddress"] = "Jahangir Tower, Kawran Bazar";
                Session["AccountID"] = prow["ACCNT_ID"].ToString();
                string str = Session["AccountID"].ToString();

                Session["SessionOut"] = objSysAdmin.GetSessionTimeOut();

                //Checking PasswordExpiry
                string dtNow = DateTime.Now.ToString();
                string dtNw = String.Format("{0:MMM-dd-yyyy}", dtNow);
                string ExpireDt = prow["PASSWORD_EXPIRED_DATE"].ToString();
                ExpireDt = String.Format("{0:MMM-dd-yyyy}", prow["PASSWORD_EXPIRED_DATE"].ToString());
                if (Convert.ToDateTime(dtNw) >= Convert.ToDateTime(ExpireDt))
                {
                    pnlLogin.Title = "Login [Your Password Expired,Please Reset]";
                    btnReset.Visible = true;
                    return;
                    //Session["redirect"] = "yes";
                }
                else
                {

                    //#########################################################
                    string IPAddress = Request.ServerVariables["remote_addr"];
                    string Technology = Request.Browser.Browser + Request.Browser.Version;
                    string IPTechnology = IPAddress + "-" + Technology;
                    objSysAdmin.AddAuditLog(Session["UserID"].ToString(), "Login", IPTechnology, objSysAdmin.GetCurrentPageName(), "Log in to Application");
                    //##########################################################
                    //Update click on failure
                    int NumOfClick = 0;
                    objSysAdmin.UpdateFailureClick(SysUsrId, NumOfClick);
                    //Update LockInfo
                    objSysAdmin.UpdateLocked(SysUsrId, "UL"); //UnLocked

                    //Check User Session
                    setSessionInfo();

                    Response.Redirect("frmWellcome.aspx");

                }
            }
        }
        else
        {
            DataSet dsClick = objSysAdmin.GetFailureClick(username);
            if (dsClick.Tables[0].Rows.Count > 0)
            {
                string SysUserId = dsClick.Tables[0].Rows[0]["SYS_USR_ID"].ToString();
                int NumOfClick = Int32.Parse(dsClick.Tables[0].Rows[0]["CLICK_FAILURE"].ToString());
                //checking Lock
                DataSet dsLock = objSysAdmin.GetSysUsersInfo(SysUserId);
                if (dsLock.Tables[0].Rows[0]["LOCKED_STATUS"].ToString() == "L")
                {
                    this.btnLogin.Enabled = false;
                    pnlLogin.Title = "Login [Sorry Your Account is locked !!!]";
                }
                else
                {
                    DataSet dsClickLogIn = objSysAdmin.GetPasswordPolicyInfo();
                    int ClickLogIn = int.Parse(dsClickLogIn.Tables[0].Rows[0]["PP_PASS_LOGON_MAX_TIMES"].ToString());
                    NumOfClick = NumOfClick + 1;

                    if (NumOfClick == ClickLogIn)
                    {
                        objSysAdmin.UpdateFailureClick(SysUserId, NumOfClick); //Update click on failure
                        objSysAdmin.UpdateLocked(SysUserId, "L"); //Locked
                        pnlLogin.Title = "Login [ Sorry Your Account is locked !!! ]";
                        btnLogin.Enabled = false;
                    }
                    else
                    {
                        objSysAdmin.UpdateFailureClick(SysUserId, NumOfClick); //Update click on failure
                        pnlLogin.Title = "Login [Wrong Password(Attempt-" + NumOfClick + ")]";
                    }
                }
            }
            else
            {
                pnlLogin.Title = "Login [ Please insert correct username & password ]";
                //Session["LoginError"] = "Login [ Please insert correct username & password ]";
                //Response.Redirect("frmLogin.aspx");
            }
        }
    }
    protected void btnClear_Click(object sender, DirectEventArgs e) 
    {
        pnlLogin.Title = "Login";
        this.txtUsername.Text = "";
        this.txtPassword.Text = "";
        this.txtTuring.Text = "";
    }

    protected void btnReset_Click(object sender, DirectEventArgs e)
    {
        Response.Redirect("~/RESET_PASSWORD.aspx");
    }

    protected void btnNewReg_Click(object sender, DirectEventArgs e)
    {
        Response.Redirect("~/Sys_Company_Reg.aspx");
    }

    public void setSessionInfo()
    {
        try
        {
            string ip = "";
            string country = "";
            string region = "";
            string city = "";
            string timezone = "";

            string browser_sess = HttpContext.Current.Session.SessionID;
            Random rnd = new Random();
            int a = rnd.Next(100000, 999999);
            string sess_id = "TC" + a.ToString() + Session["UserID"].ToString();


            string WebBrowserName = "";
            try
            {
                WebBrowserName = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
            }
            catch (Exception ex)
            {
                WebBrowserName = "";
                ex.Message.ToString();
            }

            try
            {

                //  string ip = Server.HtmlEncode(Request.UserHostAddress);

                XmlDocument doc = new XmlDocument();

                string getdetails = "http://www.freegeoip.net/xml/";// +ip;
                //string getdetails = "https://ipstack.com/";

                doc.Load(getdetails);
                XmlNodeList nodeLstIP = doc.GetElementsByTagName("IP");
                XmlNodeList nodeLstCountry = doc.GetElementsByTagName("CountryName");
                XmlNodeList nodeLstRegion = doc.GetElementsByTagName("RegionName");
                XmlNodeList nodeLstCity = doc.GetElementsByTagName("City");
                XmlNodeList nodeLstTimeZone = doc.GetElementsByTagName("TimeZone");

                ip = nodeLstIP[0].InnerText;
                country = nodeLstCountry[0].InnerText;
                region = nodeLstRegion[0].InnerText;
                city = nodeLstCity[0].InnerText;
                timezone = nodeLstTimeZone[0].InnerText;
                //    string location = nodeLstCity[0].InnerText;
            }
            catch (Exception ex)
            {
                ip = "";
                country = "";
                region = "";
                city = "";
                timezone = "";
                ex.Message.ToString();
            }

            ip = Request.ServerVariables["remote_addr"];
            string strResult = objSysAdmin.ExpierSessAcctWise(Session["UserID"].ToString());
            strResult = objSysAdmin.InsertSessInfo(Session["UserID"].ToString(), Session["UserLoginName"].ToString(), sess_id, WebBrowserName, ip, city, region, timezone, browser_sess);

            if (strResult == "Successfull.")
            {
                Session["Sess_ID"] = sess_id;
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            Session.Clear();
            Response.Redirect("../frmSeesionExpMesage.aspx");
        }
    }
}
