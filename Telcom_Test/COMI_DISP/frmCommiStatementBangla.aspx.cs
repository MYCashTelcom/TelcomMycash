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
using System.IO;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class COMI_DISP_frmCommiStatementBangla : System.Web.UI.Page
{

    clsAccountHandler objAccount = new clsAccountHandler();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    DateTime dt = DateTime.Now;
    string strResult = "";
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

            zonecode();
            txtFromDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1 * (dt.Day - 1)));
            txtToDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt);
            Session["Progress Msg"] = "";
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
    
     
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        
    }
    public void RSPCode()
    {
           }


    protected void Button3_Click(object sender, EventArgs e)
    {  //string strSQL = "";
      
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

        string strSQL = "";
        
        Session["CompanyBranch"] = "ROBI";
        Session["ReportSQL"] = strSQL;
        Session["RequestForm"] = "../COMI_DISP/frmCommiStatementBangla.aspx";
        Session["ReportFile"] = "../COMI_DISP/COMISSION_STATEMNT_BNG.rpt";
        Response.Redirect("../COM/COM_ReportView.aspx");


    }
   
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        distributerCode();
        rspcode();
        lblResult.Text = "";
       
    }

    protected void ddlDistributer_SelectedIndexChanged(object sender, EventArgs e)
    {
        rspcode();
        lblResult.Text = "";
        
    }
  
    protected void btnGenerateFile_Click(object sender, EventArgs e)
    {
       

    }
    public string CreateRange(string RSPCode,string RSPDCODE,string strFromDate,string strToDate)
    {
        Session["Progress Msg"] = ""; 
        lblResult.Text = "";
        string strReportFile = "";
        
        try
        {
            string strDirectory = String.Format("{0:ddMMMyyyy}", System.DateTime.Today);
            //################### Creat directory yyyymmdd_234 ######################
            Directory.CreateDirectory(Server.MapPath("~/Region/" + "" + rdblbcrtype.SelectedItem.ToString().Trim() + " " + strDirectory + "/" + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + ddlDistributer.SelectedItem.ToString() + ""));

           // Directory.CreateDirectory(Server.MapPath("~/Region/" + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + ddlDistributer.SelectedItem.ToString() + ""));


            string fileName = string.Empty;
            DataSet ds = new DataSet();
            fileName = RSPCode;
            fileName = fileName + ".pdf";
            
            fileName = RSPCode;
            fileName = fileName + ".pdf";

            string fullPath = Server.MapPath("~/Region/" + "" + rdblbcrtype.SelectedItem.ToString().Trim() + " " + strDirectory +"/" + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + ddlDistributer.SelectedItem.ToString() + "" + "/" + fileName);
           // string fullPath = Server.MapPath("~/Region/" + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + ddlDistributer.SelectedItem.ToString() + "" + "/" + fileName);
            strReportFile = "";
            //CreatePDF(report, fullPath);
            ReportDocument objCReport = new ReportDocument();
            DataSet dtsReport = new DataSet();
            dtsReport = objAccount.GetDataInfo(RSPDCODE, strFromDate, strToDate, rdblbcrtype);
            //int countreportRow=dtsReport.Tables[0].Rows.Count;
            if (rdblbcrtype.SelectedValue.ToString() == "CO")
            {
                strReportFile = Server.MapPath("../COMI_DISP/COMISSION_STATEMNT_BNG.rpt");
            }
            if (rdblbcrtype.SelectedValue.ToString() == "RE")
            {
                strReportFile = Server.MapPath("../COMI_DISP/COMISSION_STATEMNT_BNG_RE.rpt");
            }
            if (rdblbcrtype.SelectedValue.ToString() == "US")
            {
                strReportFile = Server.MapPath("../COMI_DISP/COMISSION_STATEMNT_BNG_REFILL.rpt");
            }
            if (rdblbcrtype.SelectedValue.ToString() == "BO")
            {
                strReportFile = Server.MapPath("../COMI_DISP/COMISSION_STATEMNT_BNG_BOTH.rpt");
            }
            objCReport.Load(strReportFile);
            objCReport.SetDataSource(dtsReport.Tables[0]);
            objCReport.ExportToDisk(ExportFormatType.PortableDocFormat, fullPath);
        }
        catch (Exception )
        {
            //Response.Write(ex.Message.ToString() + "_1_  " + strReportFile);
            return strResult = "";
        }
        Session["Progress Msg"] = "Report Generate Successfully";
        return strResult = "Report Generate Successfully";
    }
    private void CreatePDF(ReportDocument report, string filePath)
    {
        CrystalDecisions.Shared.DiskFileDestinationOptions diskOpts = new CrystalDecisions.Shared.DiskFileDestinationOptions();
        diskOpts.DiskFileName = filePath;
        report.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
        report.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
        report.ExportOptions.DestinationOptions = diskOpts;
        report.Export();
    }

    protected void distributerCode()
    {
        DataSet ds = objAccount.GetDistributer(ddlZone.SelectedValue.ToString());
        ddlDistributer.DataSource = ds;
        ddlDistributer.DataTextField = "DISTRIBUTORNAME";
        ddlDistributer.DataValueField = "DISTRIBUTORCODE";
        ddlDistributer.DataBind();
 
    }
    protected void rspcode()
    {
        DataSet ds = objAccount.GetRSPCode(ddlDistributer.SelectedValue.ToString());
        ddlRSPCode.DataSource = ds;
        ddlRSPCode.DataTextField = "RSPNAME";
        ddlRSPCode.DataValueField = "RSPCODE";
        ddlRSPCode.DataBind();
 
    }
    protected void zonecode() 
    {
        DataSet ds = objAccount.GetZoneCode();
        ddlZone.DataSource = ds;
        ddlZone.DataTextField = "REGIONNAME";
        ddlZone.DataValueField = "REGIONCODE";
        ddlZone.DataBind();

    
    }
    protected void ddlRSPCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResult.Text = "";
    }
    protected void btnGeneratezoneFile_Click(object sender, EventArgs e)
    {
       
    }
    public string CreateDistributerRange(string RSPNAME, string RSPDCODE,string DistributerName,string strFromDate,string strToDate)
    {
        
        string strReportFile="";
        try
        {
            string strDirectory = String.Format("{0:ddMMMyyyy}", System.DateTime.Today);
            //################### Creat directory yyyymmdd_234 ######################
            //Directory.CreateDirectory(Server.MapPath("~/Region/" + ""+ rdblbcrtype.SelectedItem.Text.Trim()+ " /" + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + DistributerName + ""));
           // Directory.CreateDirectory(Server.MapPath("~/Region/" + ""+ ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + DistributerName + ""));
            Directory.CreateDirectory(Server.MapPath("~/Region/" + "" + rdblbcrtype.SelectedItem.ToString() + " " + strDirectory + "/" + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + DistributerName + ""));

            string fileName = string.Empty;
            DataSet ds = new DataSet();
            fileName = RSPNAME;
            fileName = fileName + ".pdf";

            
            fileName = RSPNAME;
            fileName = fileName + ".pdf";

           // string fullPath = Server.MapPath("~/Region/" + ""+rdblbcrtype.SelectedItem.Text.Trim( + "/"  + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + DistributerName + "" + "/" + fileName);
            //string fullPath = Server.MapPath("~/Region/" +"" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + DistributerName + "" + "/" + fileName);
            string fullPath = Server.MapPath("~/Region/" + "" + rdblbcrtype.SelectedItem.ToString() + " " + strDirectory + "/" + "" + ddlZone.SelectedItem.ToString() + " " + strDirectory + "/" + "" + DistributerName + "" + "/" + fileName);

            //CreatePDF(report, fullPath);
            ReportDocument objCReport = new ReportDocument();
            DataSet dtsReport = new DataSet();
            dtsReport = objAccount.GetDataInfo(RSPDCODE,strFromDate,strToDate,rdblbcrtype);
            //########################Checking for Type################################
            
            if (rdblbcrtype.SelectedValue.ToString() == "CO")
            {
                strReportFile = Server.MapPath("../COMI_DISP/COMISSION_STATEMNT_BNG.rpt");
            }
            if (rdblbcrtype.SelectedValue.ToString() == "RE")
            {
                strReportFile = Server.MapPath("../COMI_DISP/COMISSION_STATEMNT_BNG_RE.rpt");
            }
            if (rdblbcrtype.SelectedValue.ToString() == "US")
            {
                strReportFile = Server.MapPath("../COMI_DISP/COMISSION_STATEMNT_BNG_REFILL.rpt");
            }
            if (rdblbcrtype.SelectedValue.ToString() == "BO")
            {
                strReportFile = Server.MapPath("../COMI_DISP/COMISSION_STATEMNT_BNG_BOTH.rpt");
            }

            objCReport.Load(strReportFile);
            objCReport.SetDataSource(dtsReport.Tables[0]);
            objCReport.ExportToDisk(ExportFormatType.PortableDocFormat, fullPath);
        }
        catch (Exception)
        {

            //Response.Write(ex.Message.ToString() + "_2_  " + strReportFile);
            return strResult = "";
        }
        Session["Progress Msg"] = "Report Generate Successfully";
        return strResult = "Report Generate Successfully";
        
    }
    

    protected void btnZone_Click(object sender, EventArgs e)
      
    {

        
        if (rdblbcrOption.SelectedValue.ToString() == "RG")
        {
            //calling function
            
            ZoneWise();
            
           
        }
        if(rdblbcrOption.SelectedValue.ToString() == "DS")
        {
            //calling function
         
            DistributerWise();
        }
        if (rdblbcrOption.SelectedValue.ToString() == "RS")
        {
           //calling function
           
            RSPWise();
        }

        //#####################Login Audit################################
        //string IPAddress = Request.ServerVariables["remote_addr"];
        //string Technology = Request.Browser.Browser + Request.Browser.Version;
        //string IPTechnology = IPAddress + "-" + Technology;
        //string strPageName=objSysAdmin.GetCurrentPageName();
        //string strUser =Session["UserID"].ToString() ;
        //objSysAdmin.AddAuditLog("", "Report", IPTechnology,strPageName,"kk");
        SaveAuditInfo("Preview", "Commission Statement Bangla");
       
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    // ZoneWiseFuction
    public void ZoneWise()
    {
       
        DataSet ds = objAccount.GetDistributer(ddlZone.SelectedValue.ToString());
        foreach (DataRow rowheader in ds.Tables[0].Rows)
        {
            DataSet dss = objAccount.GetRSPCode(rowheader["DISTRIBUTORCODE"].ToString());
            foreach (DataRow hrowHeader in dss.Tables[0].Rows)
            {
                CreateDistributerRange(hrowHeader["RSPNAME"].ToString(), hrowHeader["RSPCODE"].ToString(), rowheader["DISTRIBUTORNAME"].ToString(), txtFromDate.DateString, txtToDate.DateString);
            }

        }
        if (!strResult.Equals(""))
        {
            lblResult.Text = strResult;
        }
       
        lblResult.Text = strResult;
    }
    //DistributerWiseFuction
    public void DistributerWise()
    {
        DataSet ds = objAccount.GetRSPCode(ddlDistributer.SelectedValue.ToString());
        foreach (DataRow rheader in ds.Tables[0].Rows)
        {
            CreateRange(rheader["RSPNAME"].ToString(), rheader["RSPCODE"].ToString(), txtFromDate.DateString, txtToDate.DateString);
        }
        if (!strResult.Equals(""))
        {
            lblResult.Text = strResult;
        }
    }
    //RSPWiseFunction
    public void RSPWise()
    {
        DataSet ds = objAccount.GetRSPCodeSing(ddlRSPCode.SelectedValue.ToString());
        foreach (DataRow rheader in ds.Tables[0].Rows)
        {
            CreateRange(rheader["RSPNAME"].ToString(), rheader["RSPCODE"].ToString(), txtFromDate.DateString, txtToDate.DateString);
        }
        if (!strResult.Equals(""))
        {
            lblResult.Text = strResult;
        }
    }

    protected void btnRSP_Click(object sender, EventArgs e)
    {
       
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResult.Text = "";
    }
    protected void rdblbcrOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblbcrOption.SelectedValue == "RG")
        {
            ddlDistributer.Enabled = false;
            ddlRSPCode.Enabled = false;
        }
        else if (rdblbcrOption.SelectedValue == "DS")
        {
            ddlDistributer.Enabled = true;
            ddlRSPCode.Enabled = false;
        }
        else if (rdblbcrOption.SelectedValue == "RS")
        {
            ddlDistributer.Enabled = true;
            ddlRSPCode.Enabled = true;
        }
    }
    protected void btnLoadData_Click(object sender, EventArgs e)
    {
        distributerCode();
        rspcode();
    }

    override protected void OnInit(EventArgs e)
    {
        btnZone.Attributes.Add("onclick", "javascript:" +
                  btnZone.ClientID + ".disabled=true;" +
                  this.GetPostBackEventReference(btnZone));
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {
        this.btnZone.Click +=
                new System.EventHandler(this.btnZone_Click);
        this.Load += new System.EventHandler(this.Page_Load);
    }
}
