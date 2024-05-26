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

public partial class COMI_DISP_frmT_CUSTINFOImport2 : System.Web.UI.Page
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
    protected void btnImportSub_Click(object sender, EventArgs e)
    {        
        //ltlHide.Text = "";
        
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
            //strReturn1 = objServiceHandler.BlankCustInfoTable();

            if (strReturn1 == "")
            {

                //#####################################
                //string strProcessDate;
                string strSQL;
                SqlConnection conn;
                //#############################
                //strProcessDate = ddlPendingFile.SelectedItem.Text;
                //strProcessDate = strProcessDate.Substring(strProcessDate.IndexOf('>') + 1);
                //strProcessDate = String.Format("{0:yyyy-MM-dd}", DateTime.Parse(strProcessDate));
                //-------------------------------------
                //conn = new SqlConnection("Server=192.168.11.142;Database=ststest2;User ID=arena;Password=arena;Pooling=false;Connect Timeout=105;");

                conn = new SqlConnection("Server=192.168.11.142;Database=STSProd;User ID=arena;Password=arena;Pooling=false;Connect Timeout=105;");
                conn.Open();

                //------------------------------------------
                //strSQL = "SELECT * FROM T_CUSTINFO where KUNNR<='100003';";

                strSQL = "SELECT * FROM T_CUSTINFO ORDER BY KUNNR";
                //strSQL = "SELECT * FROM T_CUSTINFO WHERE KUNNR BETWEEN 'R071771'AND 'R071774'";



                //strSQL = "SELECT * FROM VW_QualifiedList WHERE RETAILEREASYLOADNUMBER IS NOT NULL";
                DataSet dstCommission = new DataSet();
                SqlCommand cmdCommisssion = new SqlCommand(strSQL, conn);
                cmdCommisssion.CommandTimeout = 5000;
                SqlDataAdapter adpCommission = new SqlDataAdapter(cmdCommisssion);
                //SqlDataAdapter adpCommission = new SqlDataAdapter(new SqlCommand(strSQL, conn));
                //cmdCommisssion.CommandTimeout = 5000;
                SqlCommandBuilder cmbCommission = new SqlCommandBuilder(adpCommission);
                adpCommission.Fill(dstCommission, "T_CUSTINFO");

                //grvCommission.DataSource = dstCommission;
                //#####################################
                //string strReturn = objServiceHandler.ImportCustInfoData(dstCommission, "", strIP, strHostName + " [" + System.Environment.UserName + "]");
                //lblMessage.Text = strReturn;
             
            }
            //divImage.Visible = false;
        }
        catch (Exception ex)
        {
            // error message
            lblMessage.Text = "Data import failed. [" + ex.Message.ToString() + "]";
        }

        //ltlHide.Text = "<script type=\"text/javascript\">HideProgressBar();</script>";

    }

    //override protected void OnInit(EventArgs e)
    //{
    //    btnImportSub.Attributes.Add("onclick", "javascript:" +
    //              btnImportSub.ClientID + ".disabled=true;" +
    //              this.GetPostBackEventReference(btnImportSub));
    //    InitializeComponent();
    //    base.OnInit(e);
    //}

    //private void InitializeComponent()
    //{
    //    this.btnImportSub.Click +=
    //            new System.EventHandler(this.btnImportSub_Click);
    //    this.Load += new System.EventHandler(this.Page_Load);
    //}
}