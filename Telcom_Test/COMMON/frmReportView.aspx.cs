using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Reporting;
//using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using System.Configuration;
using System.IO;
using System.Security.Principal;

public partial class frmReportView : System.Web.UI.Page
{
    //private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    //private CrystalReport1 report = new CrystalReport1();
    clsReports objReport = new clsReports();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    ConnectionInfo info = new ConnectionInfo();
    static ReportDocument objCReport = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (IsPostBack)
        //{
        //    CrystalReportViewer1.ReportSource = Session["Rep"];
        //    CrystalReportViewer1.DataBind();
        //    CrystalReportViewer1.RefreshReport();
        //}
        //else
        //{
            
        //}

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
    private void Page_Init(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
    }
    private void ConfigureCrystalReports()
    {

        //CrystalReportViewer1.ReportSource = Server.MapPath("crptClientList.rpt");
        CrystalReportViewer1.ToolbarImagesFolderUrl="../aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer4/images";
        LoadReport();
        //CrystalReportViewer1.DisplayToolbar = true;
        //CrystalReportViewer1.HasSearchButton = true;
        //CrystalReportViewer1.HasExportButton = false;
        //CrystalReportViewer1.HasPrintButton = false;
        
    }

    private void LoadReport()
    {
        string strReportFile="";
        if (strReportFile == "")
        {
            strReportFile = "crptClientList.rpt";
        }
        strReportFile = Session["ReportFile"].ToString();
        objCReport.Load(Server.MapPath(strReportFile));
        objCReport.SetDatabaseLogon("APSNG101", "APSNG101");
        //objCReport.SetDatabaseLogon("APSNG101", "APSNG101", "APSNG101", "APSNG101");
        //objCReport.SetDataSource(objReport.GetClientList());
        CrystalReportViewer1.ReportSource = objCReport;
        CrystalReportViewer1.RefreshReport();
        // oRpt = new Customer();
        //info.UserID = objReport.strUser;
        //info.Password = objReport.strPass;
        //info.DatabaseName = objReport.strDataBase;
        ////info.ServerName = "192.168.1.1";
        //info.Type = ConnectionInfoType.SQL;
        //info.IntegratedSecurity = false;

        //TableLogOnInfos myTableLogOnInfos = CrystalReportViewer1.LogOnInfo;
        //foreach (TableLogOnInfo myLogOnInfo in myTableLogOnInfos)
        //{
        //    myLogOnInfo.ConnectionInfo = info;
        //}
        ////Session["Rep"] = objCReport;
        //string fileName = "report";
        //string filePath = @"~/tmpStorage/" + fileName;

        ////objCReport.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(filePath + ".pdf"));
        //Response.Buffer = false;  
        //Response.ClearContent();  
        //Response.ClearHeaders();

        ////objCReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Report");

    }

    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }

    protected void imbExport_Click(object sender, ImageClickEventArgs e)
    {
        ////string fileName = "report";
        ////string filePath = @"~/TempReports/" + fileName;
        //Response.Buffer = false;
        //Response.ClearContent();
        //Response.ClearHeaders();
        //objCReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Report1");

        try
        {

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            objCReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, Session["ReportFile"].ToString().Substring(4,Session["ReportFile"].ToString().Length-5-3)+"_"+DateTime.Now.ToString());
            Response.Flush();
            Response.Close();

        }
        finally
        {
        }

    }
    protected void imbPrint_Click(object sender, ImageClickEventArgs e)
    {
        objCReport.PrintToPrinter(1, true, 1,99);
    }
    protected void CrystalReportViewer1_Navigate(object source, NavigateEventArgs e)
    {
        LoadReport();
    }
    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmReportList.aspx");
    }
}
