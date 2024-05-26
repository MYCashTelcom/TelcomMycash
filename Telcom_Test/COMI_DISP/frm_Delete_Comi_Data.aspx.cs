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
using System.Data.OracleClient;


public partial class COMI_DISP_frm_Delete_Comi_Data : System.Web.UI.Page
{
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteSetupID();
        txtSetupID.Text = "";
        SaveAuditInfo("Delete", "Delete Commission Data");
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
        btnDelete.Attributes.Add("onclick", "javascript:" +
                      btnDelete.ClientID + ".disabled=true;" +
            //txtSetupID.ClientID + ".disabled=true;" +
            //txtSetupID.ClientID + ".readonly=\"readonly\";" +
                      this.GetPostBackEventReference(btnDelete));

        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {
        this.btnDelete.Click +=
                new System.EventHandler(this.btnDelete_Click);
        this.Load += new System.EventHandler(this.Page_Load);
    }

    public void DeleteSetupID()
    {

        string connectionInfo = ConfigurationManager.AppSettings["dbConnectionString"];
        OracleConnection conn = new OracleConnection(connectionInfo);
        OracleCommand cmd;

        string strSQL = "DELETE FROM COMMISSION_DATA WHERE SETUPID IN (" + txtSetupID.Text.ToString() + ")";

        string strReturn = "";
        try
        {
            conn.Open();
            cmd = new OracleCommand(strSQL, conn);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();

            lblMessage1.Text = "Delete Successfully";
            //return "Delete Successfull";
            conn.Close();
        }
        catch (Exception ex)
        {
            strReturn = ex.Message.ToString();
        }

    }


}


