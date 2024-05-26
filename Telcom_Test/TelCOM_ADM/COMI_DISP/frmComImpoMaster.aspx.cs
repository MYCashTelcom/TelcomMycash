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

public partial class COMI_DISP_frmComImpoMaster : System.Web.UI.Page
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
            txtFromDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddHours(-1));
            txtToDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddHours(1));
            txtProcessDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt);
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

       try
        {
            //Label1.Text = "File " + FileUpload1.PostedFile.FileName + " uploaded successfully.";
            objServiceHandler.AddCommissionFile("Commission ->" + txtProcessDate.DateString, "", strIP, strHostName + " [" + System.Environment.UserName + "]", txtFromDate.DateString, txtToDate.DateString);
            sdsBulkAccountFile.DataBind();
            gdvBulkAccountFile.DataBind();
            sdsPendingBFile.DataBind();
            ddlPendingFile.DataBind();
            lblMessage.Text = "Commission master created successfully.";

            SaveAuditInfo("Create", "Import & Broadcast");
        }
        catch (Exception ex)
        {
            // error message
            lblMessage.Text = "Commission master could not be created. [" + ex.Message.ToString() + "]";
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
        string[] strSetupId; 

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
        try
        {
            //#####################################
            string strProcessDate;
            string strSQL;
            SqlConnection conn;
            //#############################
            strProcessDate = ddlPendingFile.SelectedItem.Text;
            strProcessDate = strProcessDate.Substring(strProcessDate.IndexOf('>')+1);
            strProcessDate = String.Format("{0:yyyy-MM-dd}", DateTime.Parse(strProcessDate));
            //-------------------------------------
            //conn = new SqlConnection("Server=192.168.11.142;Database=ststest2;User ID=arena;Password=arena;Pooling=false;Connect Timeout=105;");

            conn = new SqlConnection("Server=192.168.11.142;Database=STSProd;User ID=arena;Password=arena;Pooling=false;Connect Timeout=105;");
            conn.Open();

            //------------------------------------------
            //strSQL = "SELECT * FROM VW_QualifiedList WHERE ProcessDate BETWEEN '" + String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(txtProcessFrom.Text)) + "' "
              //     + "AND '" + String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(txtProcessTo.Text))+ "'";// AND RETAILEREASYLOADNUMBER IS NOT NULL";
            //-------------------------------------------
            strSetupId = txtSetupID.Text.ToString().Split(',');
            foreach (string strSID in strSetupId)
            {
                //--------------------------------------------
                DataSet dstCommission = new DataSet();
                if (rdblbcrOption.SelectedValue.ToString() == "CO")
                {
                    strSQL = "SELECT * FROM QualifiedList WHERE SetupID=" + strSID + "";
                    //strSQL = "SELECT * FROM VW_QualifiedList WHERE SetupID=" + strSID + "";// AND RETAILEREASYLOADNUMBER IS NOT NULL";

                    //strSQL = "SELECT * FROM VW_QualifiedList WHERE RETAILEREASYLOADNUMBER IS NOT NULL";
                    
                    SqlCommand cmdCommisssion = new SqlCommand(strSQL, conn);
                    cmdCommisssion.CommandTimeout = 5000;
                    SqlDataAdapter adpCommission = new SqlDataAdapter(cmdCommisssion);
                    SqlCommandBuilder cmbCommission = new SqlCommandBuilder(adpCommission);
                    //adpCommission.Fill(dstCommission, "VW_QualifiedList");
                    adpCommission.Fill(dstCommission, "QualifiedList");

                }
                if (rdblbcrOption.SelectedValue.ToString() == "RU")
                {
                    string strSQL2 = "SELECT * FROM QualifiedList_RefillUsage WHERE SetupID=" + strSID + "";
                    SqlCommand cmdCommisssion = new SqlCommand(strSQL2, conn);
                    cmdCommisssion.CommandTimeout = 5000;
                    SqlDataAdapter adpCommission = new SqlDataAdapter(cmdCommisssion);
                    SqlCommandBuilder cmbCommission = new SqlCommandBuilder(adpCommission);
                    adpCommission.Fill(dstCommission, "QualifiedList_RefillUsage");

                }


                if (rdblbcrOption.SelectedValue.ToString() == "BO")
                {
                    strSQL = "SELECT * FROM QualifiedList WHERE SetupID=" + strSID + "";
                    //strSQL = "SELECT * FROM VW_QualifiedList WHERE SetupID=" + strSID + "";// AND RETAILEREASYLOADNUMBER IS NOT NULL";

                    //strSQL = "SELECT * FROM VW_QualifiedList WHERE RETAILEREASYLOADNUMBER IS NOT NULL";

                    SqlCommand cmdCommisssion = new SqlCommand(strSQL, conn);
                    cmdCommisssion.CommandTimeout = 5000;
                    SqlDataAdapter adpCommission = new SqlDataAdapter(cmdCommisssion);
                    SqlCommandBuilder cmbCommission = new SqlCommandBuilder(adpCommission);
                    //adpCommission.Fill(dstCommission, "VW_QualifiedList");
                    adpCommission.Fill(dstCommission, "QualifiedList");

                }


                //grvCommission.DataSource = dstCommission;
                //#####################################
               
                //string strReturn = objServiceHandler.ImportCommissionData(ddlPendingFile.SelectedValue, dstCommission, "", strIP, strHostName + " [" + System.Environment.UserName + "]", rdblbcrOption.SelectedValue);
                //lblMessage.Text = strReturn;
            }
            sdsBulkAccountFile.DataBind();
            gdvBulkAccountFile.DataBind();
            sdsPendingBFile.DataBind();
            ddlPendingFile.DataBind();
            lblMessage.Text = "Data Import successfully.";
            SaveAuditInfo("Import", "Import & Broadcast");
        }
        catch (Exception ex)
        {
            // error message
            lblMessage.Text = "Commission import failed. [" + ex.Message.ToString() + "]";
        }
        
    }

    protected void btnBroadcast_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string strReturn = "";

        
        //strReturn = objServiceHandler.UpdateBroadCastStatus(ddlLoadedFile.SelectedValue);
     

        strReturn = objServiceHandler.BroadCastCommission(ddlLoadedFile.SelectedValue,ddlChannelType.SelectedValue,txtAmountLimit.Text);
        lblMessage.Text = strReturn;
        SaveAuditInfo("Brodcast", "Import & Broadcast");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(15000);
        lblMessage.Text = "Processing completed";
    }
    protected void rdblbcrOption_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gdvBulkAccountFile_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "Import & Broadcast");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
