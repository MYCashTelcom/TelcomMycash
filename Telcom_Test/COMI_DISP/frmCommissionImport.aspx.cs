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
using System.Net;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class TEXT_frmCommissionImport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    DateTime dt = DateTime.Now;
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
            txtProcessDate.Text = String.Format("{0:dd-MMM-yyyy}", dt);
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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        string strSQL;
        string strProcessDate;
        SqlConnection conn;
        //SqlDataAdapter oOrdersDataAdapter;
        //-------------------------------------
        strProcessDate = txtProcessDate.Text;
        strProcessDate = String.Format("{0:yyyy-MM-dd}", DateTime.Parse(strProcessDate));
        //-------------------------------------
        conn = new SqlConnection("Server=192.168.11.142;Database=ststest2;User ID=arena;Password=arena;");
        conn.Open();

        //------------------------------------------
        strSQL = "SELECT * FROM VW_QualifiedList WHERE ProcessDate BETWEEN '" + txtProcessDate.Text + " 00:00:00'"
               + "AND '" + strProcessDate + " 23:59:59'";
        DataSet dstCommission = new DataSet();
        SqlDataAdapter adpCommission = new SqlDataAdapter(new SqlCommand(strSQL, conn));
        SqlCommandBuilder cmbCommission = new SqlCommandBuilder(adpCommission);
        adpCommission.Fill(dstCommission, "VW_QualifiedList");

        grvCommission.DataSource = dstCommission;
        grvCommission.DataBind();
        SaveAuditInfo("View", "View Commission");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strHostName = Dns.GetHostName();
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] IPaddr = ipEntry.AddressList;
    }
}
