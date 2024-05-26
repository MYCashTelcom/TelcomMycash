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

public partial class Forms_frmDpdcPosList : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;

    clsServiceHandler obj = new clsServiceHandler();
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
        //DateTime dt = DateTime.Now;
        //if (dptFromDate.DateString!= "")
        //{
        //    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(-6)); 
        //    //txtFromDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt.AddHours(-6));
        //    //txtToDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt.AddHours(6));  
        //    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(6)); 
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {    
    
        //Insert INTO DPDC_PREPAID_METER_POS_LIST (AAMRA_USER_ID,DPDC_POS_ID) VALUES ('','');
        string result = "";

        //if (txtPosList.Text != "" && txtAccountNo.Text != "")
            if (txtAccountNo.Text != "" && txtPosList.Text != "")
        {


             //result = obj.DPDC_Pos_Insert(txtPosList.Text, txtAccountNo.Text);
            result = obj.DPDC_Pos_Insert(txtAccountNo.Text, txtPosList.Text);
             lblMsg.Text = result;
        }


        
            

    }	
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    
}
