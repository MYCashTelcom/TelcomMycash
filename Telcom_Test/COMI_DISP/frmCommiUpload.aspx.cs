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


public partial class Forms_frmCommiUpload : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    DateTime dt=DateTime.Now;
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
            txtFromDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(-1));
            txtToDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(1));
            lblMessage.Text = "";
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
            try
            {
                // SaveAs method of PostedFile property used
                // to save the file at specified rooted path
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/App_Data") + System.IO.Path.DirectorySeparatorChar + FileUpload1.PostedFile.FileName);

                // success message
                //Label1.Text = "File " + FileUpload1.PostedFile.FileName + " uploaded successfully.";
                objServiceHandler.AddCommissionFile(FileUpload1.PostedFile.FileName, "", strIP, strHostName + " [" + System.Environment.UserName+"]",txtFromDate.Text,txtToDate.Text);
                sdsBulkAccountFile.DataBind();
                gdvBulkAccountFile.DataBind();
                sdsPendingBFile.DataBind();
                ddlPendingFile.DataBind();
               
            }
            catch (Exception ex)
            {
                // error message
                lblMessage.Text = "File (" + FileUpload1.PostedFile.FileName + ") could not be successfully. [" +ex.Message.ToString()+"]";
            }
        }
        else
        {
            // warning message
            lblMessage.Text = "Please choose a file to upload.";
        }
    }
    protected void btnExecuted_Click(object sender, EventArgs e)
    {
        //string strHostName = Dns.GetHostName();
        //IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        //IPAddress[] IPaddr = ipEntry.AddressList;
        //string strIP = "";

        //foreach (IPAddress IP in IPaddr)
        //{
        //    string strAddressFamily = IP.AddressFamily.ToString();
        //    if (strAddressFamily.Equals("InterNetwork"))
        //    {
        //        strIP = IP.ToString();
        //        break;
        //    }
        //}
        //lblMessage.Text = "";
        //string strReturn = objServiceHandler.ExecuteBulkAccountFile(ddlPendingAccount.SelectedValue, Server.MapPath("~/App_Data") + System.IO.Path.DirectorySeparatorChar + ddlPendingFile.SelectedItem.ToString(), "", strIP, strHostName + " [" + System.Environment.UserName + "]");
        //lblMessage.Text = strReturn;
        //sdsBulkAccountFile.DataBind();
        //gdvBulkAccountFile.DataBind();
        //sdsPendingBFile.DataBind();
        //ddlPendingFile.DataBind();
        //ddlPendingAccount.DataBind();
    }
    protected void btnExpClinetList_Click(object sender, EventArgs e)
    {
        //clsGridExport.Export("Customers.xls", this.gdvBulkAccountFile);       
        clsGridExport.ExportClientList();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.

    }
    protected void btnExpAccList_Click(object sender, EventArgs e)
    {
       
    }
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
        string strReturn = objServiceHandler.LoadCommissionData(ddlPendingFile.SelectedValue, Server.MapPath("~/App_Data") + System.IO.Path.DirectorySeparatorChar + ddlPendingFile.SelectedItem.ToString(), "", strIP, strHostName + " [" + System.Environment.UserName + "]");
        lblMessage.Text = strReturn;
        sdsBulkAccountFile.DataBind();
        gdvBulkAccountFile.DataBind();
        sdsPendingBFile.DataBind();
        ddlPendingFile.DataBind();

    }

    protected void btnBroadcast_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string strReturn = objServiceHandler.BroadCastCommission(ddlLoadedFile.SelectedValue,"ALL","5000");
        lblMessage.Text = strReturn;        
    }
}
