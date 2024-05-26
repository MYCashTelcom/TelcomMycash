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

public partial class Forms_frmAccountActivation : System.Web.UI.Page
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
            lblMessage.Text = "";
            MultiView1.ActiveViewIndex = 0;
        }
        sdsClientAccount.SelectCommand = null;
        sdsClientAccount.DataBind();
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
    protected void btnActivation_Click(object sender, EventArgs e)
    {
        string strReturn;
        string strMessage1="";
        DataSet dsAccDetail;
        clsServiceHandler objSrvHandler = new clsServiceHandler();
        try
        {
            strReturn = objSrvHandler.UpdateAccountState(ddlAccountMsisdnList.SelectedItem.ToString().Trim(), "A");
            dsAccDetail = objSrvHandler.GetAccPIN_Detail(ddlAccountMsisdnList.SelectedItem.ToString().Trim());

            strMessage1 = "Your account has been activated successfully.\n";
            //--------------------------------------------------------------
            DataRow pRow = dsAccDetail.Tables[0].Rows[0];
            strMessage1 = strMessage1 + "Your PIN is " + pRow["ACCNT_PIN"] + "\n";
            strMessage1 = strMessage1 + ". Please store your PIN in hidden place.\n";
            //strMessage1 = strMessage1 + "PIN2 :" + pRow["ACCNT_PIN2"] + "\n";
            //strMessage1 = strMessage1 + "In the next SMS you will receive secret question answer";
            
            //objSrvHandler.ForwardMessage(ddlAccountMsisdnList.SelectedItem.ToString().Trim(), "16225", strMessage1, ddlAccountMsisdnList.SelectedValue.ToString().Trim(), "BDC", "");
            objSrvHandler.ForwardFlashMessage(ddlAccountMsisdnList.SelectedItem.ToString().Trim(), "16225", strMessage1, ddlAccountMsisdnList.SelectedValue.ToString().Trim(), "BDC", "");
            //--------------------------------------------------------------------------------  
            //foreach (DataRow qRow in dsAccDetail.Tables[0].Rows)
            //{
            //    strMessage2 = strMessage2 + qRow["ACCNT_SEC_QUES_DESC"] + " Ans." + qRow["ACCNT_SEC_ANSWER"] + "\n";

            //}
            //objSrvHandler.ForwardMessage("Admin", ddlAccountMsisdnList.SelectedItem.ToString().Trim(), strMessage2, ddlAccountMsisdnList.SelectedValue.ToString().Trim(), "BDC", "");
            SaveAuditInfo("Activate", "Account Activation");
            lblMessage.Text = "Account activated successfully";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
        txtSearchAccountMSISDN.Text = "";
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    //PURPOSE: ADDING SEARCHING OPTION TO THIS FORM
    //MODIFIED BY KOWSHIK START HERE(19,31-July-2012)
    protected void btnSearchAccountMSISDN_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string strAccMSISDN = "";
        strAccMSISDN = txtSearchAccountMSISDN.Text.ToString();        
        ddlAccountMsisdnList.ClearSelection();
        try
        {
            ddlAccountMsisdnList.Items.FindByText(strAccMSISDN).Selected = true;
        }
        catch (Exception exx)
        {
            lblMessage.Text = exx.Message.ToString();
            lblMessage.Text = "This Account is Already Activated.";
        }        
    }
    override protected void OnInit(EventArgs e)
    {
        btnSearchAccountMSISDN.Attributes.Add("onclick", "javascript:" +
                  btnSearchAccountMSISDN.ClientID + ".disabled=true;" +
                  this.GetPostBackEventReference(btnSearchAccountMSISDN));
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {
        this.btnSearchAccountMSISDN.Click +=
                new System.EventHandler(this.btnSearchAccountMSISDN_Click);
        this.Load += new System.EventHandler(this.Page_Load);
    }   
    protected void btnRefreshData_Click(object sender, EventArgs e)
    {
        ShowGridData();
    }
    private void ShowGridData()
    {
        try
        {
            string strsql = "SELECT * FROM ACCOUNT_LIST WHERE (ACCNT_STATE ='I') ORDER BY ACCNT_ID DESC";
            sdsClientAccount.SelectCommand = strsql;
            sdsClientAccount.DataBind();
        }
        catch (Exception ee)
        {
            ee.Message.ToString();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMessage.Text = "";
        ShowGridData();
    }
    //MODIFIED BY KOWSHIK END HERE(19,31-July-2012)
}
