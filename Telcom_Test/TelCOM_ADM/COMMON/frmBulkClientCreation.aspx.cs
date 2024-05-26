using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;


public partial class Forms_frmBulkClinetCreation : System.Web.UI.Page
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
            lblMessage.Text = "";
        }
        lblMessage.Text = "";
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string  strHostName = Dns.GetHostName ();
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress [] IPaddr = ipEntry.AddressList;
        string strIP="";

        foreach (IPAddress IP in IPaddr)
        {
            string strAddressFamily = IP.AddressFamily.ToString();
            if (strAddressFamily.Equals("InterNetwork"))
            {
                strIP = IP.ToString();
                break;
            }
        }
        if (FileUpload1.HasFile)
        {

            //------------------- ReNaming  excel Sheet name ----------------------------------                    
            string strGetExtensionName = "", strFileName = "", strNewFileName = "",strMsg="";
            strFileName = FileUpload1.PostedFile.FileName;
            strGetExtensionName = strFileName.Substring(strFileName.Length - 4);
            if (strGetExtensionName == ".xls")
            {
                strNewFileName = (strFileName.Replace(".xls", "")) + "_" + string.Format("{0:ddmmyy_hhmmss}", DateTime.Now);
                strNewFileName = strNewFileName + ".xls";
            }
            else if (strGetExtensionName == "xlsx")
            {
                strNewFileName = (strFileName.Replace(".xlsx", "")) + "_" + string.Format("{0:ddmmyy_hhmmss}", DateTime.Now);
                strNewFileName = strNewFileName + ".xlsx";
            }
            //---------------------------------------------------------------------------------
            try
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/App_Data") + System.IO.Path.DirectorySeparatorChar + strNewFileName);

                strMsg=objServiceHandler.AddBulkSubsFile(strNewFileName, "", strIP, strHostName + " [" + System.Environment.UserName + "]", "");
                sdsBulkAccountFile.DataBind();
                gdvBulkAccountFile.DataBind();
                sdsPendingBFile.DataBind();
                ddlPendingFile.DataBind();
                ddlPendingAccount.DataBind();
                SaveAuditInfo("Upload", "Bulk File Upload");
            }
            catch (Exception ex)
            {               
                lblMessage.Text = "File (" + FileUpload1.PostedFile.FileName + ") could not be successfully. [" +ex.Message.ToString()+"]";
            }
        }
        else
        {          
            lblMessage.Text = "Please choose a file to upload.";
        }
    }
    //---------------------- Register Executed file ----------------------
    protected void btnExecuted_Click(object sender, EventArgs e)
    {
        string strSQL="";
        strSQL = "PRO_BULK_SUBMIT_NEW('" + ddlPendingAccount.SelectedValue + "')";
        string strMsg12 = objServiceHandler.ExecuteProcedure(strSQL);
        sdsExecutedFile.DataBind();
        ddlPendingAccount.DataBind();
        
        strSQL = "PRO_BULK_REGISTRATION('" + ddlPendingAccount.SelectedValue + "')";
        string strMsg123 = objServiceHandler.ExecuteProcedure(strSQL); 
        lblMessage.Text = "Successfull.";
        SaveAuditInfo("Upload", "Bulk Account Registration");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    //---------------------- Execute   Excel file ----------------------
    protected void btnCreateSub_Click(object sender, EventArgs e)
    {
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
        lblMessage.Text = "";        
        string strMsg="";
        string strFileID = ddlPendingFile.SelectedValue;
        string strReturn = objServiceHandler.ExecuteBulkFile(strFileID, Server.MapPath("~/App_Data") + System.IO.Path.DirectorySeparatorChar + ddlPendingFile.SelectedItem.ToString(), "", strIP, strHostName + " [" + System.Environment.UserName + "]");
        strMsg = objServiceHandler.UpdateBulkAccStatus(strFileID);
        lblMessage.Text = strReturn;
        lblMessage.Text = strMsg;
        sdsBulkAccountFile.DataBind();
        gdvBulkAccountFile.DataBind();
        sdsPendingBFile.DataBind();
        ddlPendingFile.DataBind();
        ddlPendingAccount.DataBind();
        SaveAuditInfo("Create", "Bulk Channel Execution");
        btnCreateSub.Enabled = true;
    }
    protected void btnSampleData_Click(object sender, EventArgs e)
    {
        string strHTML = "";
        string filename = "Sheet1";
        strHTML = strHTML + "<table border=\"1\" width=\"20%\">";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td valign='left' > Customer Mobile No(11 digit)</td>";
        strHTML = strHTML + "<td valign='left' > Form Sl No</td>";
        strHTML = strHTML + "<td valign='left'> Agent No(11 digit)</td>";
        strHTML = strHTML + "</tr>";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td valign='left' >'01917884781</td>";
        strHTML = strHTML + "<td valign='left' >201</td>";
        strHTML = strHTML + "<td valign='left' >'01713125905</td>";
        strHTML = strHTML + "</tr>";
        strHTML = strHTML + "</table>";
        clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape", "");
    }
}
