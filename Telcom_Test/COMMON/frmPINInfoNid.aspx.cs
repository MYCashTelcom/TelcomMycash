using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class COMMON_frmPINInfoNid : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    private string strConString = ConfigurationSettings.AppSettings["dbConnectionString"];
    DataTable oDataTable = new DataTable();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
       // grvRequestList.DataSource = null;
       // grvRequestList.DataBind();


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
    protected void btnView_Click(object sender, EventArgs e)
    {

         OracleConnection conn = new OracleConnection(strConString);

        //string strSql = "SELECT CL.CLINT_NAME, AL.ACCNT_NO,AL.ACCNT_MSISDN, AR.RANK_TITEL, SP.SERVICE_PKG_NAME, CLINT_ADDRESS1,CLINT_ADDRESS2,CLIENT_DOB FROM CLIENT_IDENTIFICATION CI , CLIENT_LIST CL, ACCOUNT_LIST AL, ACCOUNT_RANK AR ,SERVICE_PACKAGE SP WHERE CI.CLIENT_ID=CL.CLINT_ID AND CL.CLINT_ID=AL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.SERVICE_PKG_ID= SP.SERVICE_PKG_ID AND CI.CLINT_IDENT_NAME='" + txtWallet.Text + "'";


        //Updated by Chamak 12.01.2022

        



        //sdsClientList.DataBind();
        //dtvClient.DataBind();


       // sdsClientList.SelectCommand = strSql;
      //  sdsClientList.DataBind();


         oDataTable = objServiceHandler.GetNidInfo(txtWallet.Text);
         ds.Tables.Add(oDataTable);

         dtvClient.DataSource = oDataTable;
      
        dtvClient.DataBind();


    }
  

    protected void dtvClient_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void dtvClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dtvClient.PageIndex = e.NewPageIndex;
        dtvClient.SelectedIndex = -1;
        Session["LastPageNumber"] = e.NewPageIndex;
      
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}