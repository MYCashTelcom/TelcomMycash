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
using System.Data.SqlClient;
using System.Net;
using System.Data.OracleClient;

public partial class frmCOMI_RFR_SETUP_IMPORT : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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
    protected void btnImportSub_Click(object sender, EventArgs e)
    {        
        ltlHide.Text = "";
                
        string strHostName = Dns.GetHostName();
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] IPaddr = ipEntry.AddressList;
        string strIP = "";

        foreach (IPAddress IP in IPaddr)
        {
            string strAddressFamily = IP.AddressFamily.ToString();
            if (strAddressFamily.Equals("InterNetwork"))
            {
                strIP = IP.ToString();
                break;
            }
        }
        //lblMessage.Text = "Test Message";
        lblMessage.Text = "";
        try
        {
            string strReturn1 = "";
            strReturn1 = objServiceHandler.BlankCustInfoTable();

            if (strReturn1 == "")
            {

                //#####################################
                //string strProcessDate;
                string strSQL;
                SqlConnection conn;
                //#############################
                //conn = new SqlConnection("Server=192.168.11.142;Database=ststest2;User ID=arena;Password=arena;Pooling=false;Connect Timeout=105;");
                conn = new SqlConnection("Server=192.168.11.142;Database=STSProd;User ID=arena;Password=arena;Pooling=false;Connect Timeout=105;");
                conn.Open();
                strSQL = "SELECT * FROM T_REFSETUPID";
           

                DataSet dstCommission = new DataSet();
                SqlCommand cmdCommisssion = new SqlCommand(strSQL, conn);
                cmdCommisssion.CommandTimeout = 5000;
                SqlDataAdapter adpCommission = new SqlDataAdapter(cmdCommisssion);
                SqlCommandBuilder cmbCommission = new SqlCommandBuilder(adpCommission);
                adpCommission.Fill(dstCommission, "T_REFSETUPID");

                //#####################################
                string strReturn = objServiceHandler.ImportRefSetupID(dstCommission, "", strIP, strHostName + " [" + System.Environment.UserName + "]");
                lblMessage.Text = strReturn;
              
            }
            SaveAuditInfo("Import", "Referance SetupID Import");
        }
        catch (Exception ex)
        {
            // error message
            lblMessage.Text = "Data import failed. [" + ex.Message.ToString() + "]";
        }

        ltlHide.Text = "<script type=\"text/javascript\">HideProgressBar();</script>";

    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    override protected void OnInit(EventArgs e)
    {
        btnImportSub.Attributes.Add("onclick", "javascript:" +
                  btnImportSub.ClientID + ".disabled=true;" +
                  this.GetPostBackEventReference(btnImportSub));
        //InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {
        this.btnImportSub.Click +=
                new System.EventHandler(this.btnImportSub_Click);
        this.Load += new System.EventHandler(this.Page_Load);
    }
}