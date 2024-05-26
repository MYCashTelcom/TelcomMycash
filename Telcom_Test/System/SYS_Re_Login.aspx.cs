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
using Ext.Net;
using System.Xml;

public partial class System_SYS_Re_Login : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsGlobalSetup objGS = new clsGlobalSetup();
    string SessionId = string.Empty;
    string Flag;
    static int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Ext.DirectEvents)
        //{
        //    if (!this.IsPostBack)
        //    {
        //    }
        //}
    }
    protected void Button1_Click(object sender, DirectEventArgs e)
    {
        Response.Redirect("Desktop.aspx");
    }
    protected void LoadUser()
    {
    }

    //########################################################################
    //Developer: Md. Asaduzzaman Dated: 28-Jan-2014
    //Checking Lock and Number of Failure click
    //#######################################################################
    protected void btnLogin_Click(object sender, DirectEventArgs e)
    {

        pnlLogin.Title = "Login";
        //----------------------
        string username = this.txtUsername.Text;
        string password = this.txtPassword.Text;

        // Checking Username is valid 
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


        //this.Window1.Hide();
        //pnlLogin.Title = "Login";
        ////----------------------
        //string username = this.txtUsername.Text;
        //string password = this.txtPassword.Text;
        //clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        //clsGlobalSetup objGS = new clsGlobalSetup();
        //DataSet dtsLoginUser = new DataSet();
        //dtsLoginUser = objSysAdmin.LoginWithUserName(username, password);

        //if (dtsLoginUser.Tables["CM_SYSTEM_USERS"].Rows.Count == 1)
        //{
        //    foreach (DataRow prow in dtsLoginUser.Tables["CM_SYSTEM_USERS"].Rows)
        //    {
        //        Session["CompanyBranch"] = "N/A";
        //        Session["Branch_Type"] = prow["CMP_BRANCH_TYPE_ID"].ToString();
        //        Session["Branch_ID"] = prow["CMP_BRANCH_ID"].ToString();
        //        Session["UserDname"] = prow["SYS_USR_DNAME"].ToString();
        //        Session["UserLoginName"] = prow["SYS_USR_LOGIN_NAME"].ToString();
        //        Session["Password"] = prow["SYS_USR_PASS"].ToString();
        //        Session["UserID"] = prow["SYS_USR_ID"].ToString();
        //        Session["GroupID"] = prow["SYS_USR_GRP_ID"].ToString();
        //        Session["UserEmail"] = prow["SYS_USR_EMAIL"].ToString();
        //        Session["CompanyName"] = objGS.GetCompanyName();
        //        Session["CmpanyAddress"] = "Jahangir Tower, Kawran Bazar";
        //        objSysAdmin.SetSeessionData(prow["CMP_BRANCH_ID"].ToString());
        //        Session["ClientID"] = objSysAdmin.GetClientID(username, password);
        //    }
        //    //Audit Log
        //    //#########################################################
        //    string IPAddress = Request.ServerVariables["remote_addr"];
        //    string Technology = Request.Browser.Browser + Request.Browser.Version;
        //    string IPTechnology = IPAddress + "-" + Technology;
        //    objSysAdmin.AddAuditLog(Session["UserID"].ToString(), "Login", IPTechnology, objSysAdmin.GetCurrentPageName(), "Log in to Application");
        //    //##########################################################
        //    setSessionInfo();
        //    pnlLogin.Title = "Login [ Login Successfull ]";
        //    this.txtPassword.Text = "";
        //}
        //else
        //{
        //    pnlLogin.Title = "Login [ Please insert correct username & password ]";
        //}
    }
    
    protected void btnClear_Click(object sender, DirectEventArgs e)
    {
        pnlLogin.Title = "Login";
        this.txtUsername.Text = "";
        this.txtPassword.Text = "";
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
                Session["CompanyName"] = objGS.GetCompanyName();
                Session["CmpanyAddress"] = "Jahangir Tower, Kawran Bazar";
                objSysAdmin.SetSeessionData(prow["CMP_BRANCH_ID"].ToString());
                Session["ClientID"] = objSysAdmin.GetClientID(username, password);

                Session["SessionOut"] = objSysAdmin.GetSessionTimeOut();

                //Checking PasswordExpiry
                string dtNow = DateTime.Now.ToString();
                string dtNw = String.Format("{0:MMM-dd-yyyy}", dtNow);
                string ExpireDt = prow["PASSWORD_EXPIRED_DATE"].ToString();
                ExpireDt = String.Format("{0:MMM-dd-yyyy}", prow["PASSWORD_EXPIRED_DATE"].ToString());
                if (Convert.ToDateTime(dtNw) >= Convert.ToDateTime(ExpireDt))
                {
                    pnlLogin.Title = "Login [Your Password Expired,Please Reset]";
                    //btnReset.Visible = true;
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
					pnlLogin.Title = "Login Successfully]";
                    //Response.Redirect("frmWellcome.aspx");

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
            }
        }
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
