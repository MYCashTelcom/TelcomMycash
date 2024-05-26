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

public partial class Forms_frmMngCilentList2 : System.Web.UI.Page
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
        }
        if (MultiView1.ActiveViewIndex == 1)
        {
            lblMsg1.Text = "";
            MultiView1.ActiveViewIndex = 1;
        }
        else
        {
            lblMsg.Text = "";
            MultiView1.ActiveViewIndex = 0;
            string s1 = "";
            s1 = Request["__EVENTTARGET"];

            if (Page.IsPostBack)
            {
                if (Session["strQry"] != null && s1 != "" && s1 != "FormView1$CancelUpdateButton") // not for 1st time, new search & cancel
                {
                    string strQ = Session["strQry"].ToString();
                    SqlDataSource1.SelectCommand = strQ;
                    SqlDataSource1.DataBind();
                }
            }
        }
        LoadNull();
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

    private void LoadNull()
    {
        string strSqlBank = "";
        sdsBankAcc.SelectCommand = strSqlBank;
        sdsBankAcc.DataBind();

        string strSqlIden = "";
        sdsCIden.SelectCommand = strSqlIden;
        sdsCIden.DataBind();

        string strSqlIntro = "";
        sdsIntroInfo.SelectCommand = strSqlIntro;
        sdsIntroInfo.DataBind();

        string strSqlNominee = "";
        sdsNomiInfo.SelectCommand = strSqlNominee;
        sdsNomiInfo.DataBind(); 
    }
    protected void btnClinetList_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnNewClient_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //PURPOSE:CHANGE QUERY
        //MODIFIED BY KOWSHIK START HERE(19-JULY-2012)(8-AUG-2012)(4,5,6-SEP-2012)
        lblMsg.Text = "";
        string strSql = "";
        string strSqlCondition="";
        if (txtName.Text.ToString().Trim() != "") 
        {
           strSqlCondition = " AND (UPPER(CLINT_NAME) LIKE UPPER('%" + txtName.Text.ToString().Trim() + "%')) ORDER BY CLINT_ID ASC";            
        }
        else if(txtMobileNumber.Text!="")
        {
            strSqlCondition = " AND CLINT_MOBILE LIKE '%" + txtMobileNumber.Text.Trim() + "%' ORDER BY CLINT_ID ASC"; 
        }
        strSql = "SELECT * FROM CLIENT_LIST,CM_SYSTEM_USERS CM WHERE (CM.SYS_USR_LOGIN_NAME IS NULL OR CM.SYS_USR_LOGIN_NAME='" + strUserName.Trim() + "') " + strSqlCondition;
        try
        {
            Session["strQry"] = "";
            Session["strQry"] = strSql;
            SqlDataSource1.SelectCommand = strSql;
            SqlDataSource1.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "KYC Update");
        lblMsg.Text = "Information Saved.";
    }
    protected void dtvNewClient_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        lblMsg1.Text = "Information Saved.";
    }
    protected void dtvBankAccount_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {


    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {  
            string strSqlBank = "";
            strSqlBank = "SELECT * FROM BANK_ACCOUNT WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsBankAcc.SelectCommand = strSqlBank;
            sdsBankAcc.DataBind();

            string strSqlIden = "";
            strSqlIden = "SELECT * FROM CLIENT_IDENTIFICATION WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsCIden.SelectCommand = strSqlIden;
            sdsCIden.DataBind();

            string strSqlIntro = "";
            strSqlIntro = "SELECT * FROM INTRODUCER_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsIntroInfo.SelectCommand = strSqlIntro;
            sdsIntroInfo.DataBind();

            string strSqlNominee = "";
            strSqlNominee = "SELECT * FROM NOMINEE_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsNomiInfo.SelectCommand = strSqlNominee;
            sdsNomiInfo.DataBind();           
        }     
    }
    protected void dtvClientIdentification_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlIden = "";
            strSqlIden = "SELECT * FROM CLIENT_IDENTIFICATION WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsCIden.SelectCommand = strSqlIden;
            sdsCIden.DataBind();
            SaveAuditInfo("Insert", "KYC Update");
        }
    }
    protected void dtvIntroducerInfo_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlIntro = "";
            strSqlIntro = "SELECT * FROM INTRODUCER_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsIntroInfo.SelectCommand = strSqlIntro;
            sdsIntroInfo.DataBind();
            SaveAuditInfo("Insert", "KYC Update");
        }
    }
    protected void dtvNomineeInfo_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlNominee = "";
            strSqlNominee = "SELECT * FROM NOMINEE_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsNomiInfo.SelectCommand = strSqlNominee;
            sdsNomiInfo.DataBind();
            SaveAuditInfo("Insert", "KYC Update");
        }
    }
    protected void dtvBankAccount_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlBank = "";
            strSqlBank = "SELECT * FROM BANK_ACCOUNT WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsBankAcc.SelectCommand = strSqlBank;
            sdsBankAcc.DataBind();
            SaveAuditInfo("Insert", "KYC Update");
        }
    }
    protected void grdBankAccount_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlBank = "";
            strSqlBank = "SELECT * FROM BANK_ACCOUNT WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsBankAcc.SelectCommand = strSqlBank;
            sdsBankAcc.DataBind();
        }
    }
    
    protected void grdBankAccount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlBank = "";
            strSqlBank = "SELECT * FROM BANK_ACCOUNT WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsBankAcc.SelectCommand = strSqlBank;
            sdsBankAcc.DataBind();
        }
    }
    protected void grdClientIdentification_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlIden = "";
            strSqlIden = "SELECT * FROM CLIENT_IDENTIFICATION WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsCIden.SelectCommand = strSqlIden;
            sdsCIden.DataBind();
        }
    }
    protected void grdClientIdentification_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlIden = "";
            strSqlIden = "SELECT * FROM CLIENT_IDENTIFICATION WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsCIden.SelectCommand = strSqlIden;
            sdsCIden.DataBind();
        }
    }
    protected void grdIntroducerInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlIntro = "";
            strSqlIntro = "SELECT * FROM INTRODUCER_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsIntroInfo.SelectCommand = strSqlIntro;
            sdsIntroInfo.DataBind();
        }
    }
    protected void grdIntroducerInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlIntro = "";
            strSqlIntro = "SELECT * FROM INTRODUCER_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsIntroInfo.SelectCommand = strSqlIntro;
            sdsIntroInfo.DataBind();
        }
    }
    protected void grdNomineeInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlNominee = "";
            strSqlNominee = "SELECT * FROM NOMINEE_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsNomiInfo.SelectCommand = strSqlNominee;
            sdsNomiInfo.DataBind();
        }
    }
    protected void grdNomineeInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlNominee = "";
            strSqlNominee = "SELECT * FROM NOMINEE_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsNomiInfo.SelectCommand = strSqlNominee;
            sdsNomiInfo.DataBind();
        }
    }
    protected void grdBankAccount_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlBank = "";
            strSqlBank = "SELECT * FROM BANK_ACCOUNT WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsBankAcc.SelectCommand = strSqlBank;
            sdsBankAcc.DataBind();
            SaveAuditInfo("Update", "KYC Update");
        }
    }
    protected void grdClientIdentification_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlIden = "";
            strSqlIden = "SELECT * FROM CLIENT_IDENTIFICATION WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsCIden.SelectCommand = strSqlIden;
            sdsCIden.DataBind();
            SaveAuditInfo("Update", "KYC Update");
        }
    }
    protected void grdIntroducerInfo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlIntro = "";
            strSqlIntro = "SELECT * FROM INTRODUCER_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsIntroInfo.SelectCommand = strSqlIntro;
            sdsIntroInfo.DataBind();
            SaveAuditInfo("Update", "KYC Update");
        }
    }
    protected void grdNomineeInfo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        if (frmVwClientID != null)
        {
            string strSqlNominee = "";
            strSqlNominee = "SELECT * FROM NOMINEE_INFO WHERE CLIENT_ID='" + frmVwClientID.Text.ToString() + "'";
            sdsNomiInfo.SelectCommand = strSqlNominee;
            sdsNomiInfo.DataBind();
            SaveAuditInfo("Update", "KYC Update");
        }
    }
  
    protected void dtvBankAccount_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");

        e.Values["CLIENT_ID"] = frmVwClientID.Text;
    }
    protected void dtvClientIdentification_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        e.Values["CLIENT_ID"] = frmVwClientID.Text;
    }
    protected void dtvIntroducerInfo_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        e.Values["CLIENT_ID"] = frmVwClientID.Text;
    }
    protected void dtvNomineeInfo_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        TextBox frmVwClientID = (TextBox)FormView1.FindControl("frmVwClientID");
        e.Values["CLIENT_ID"] = frmVwClientID.Text;
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlPOT = (DropDownList)FormView1.FindControl("ddlPurOfTran");
        e.NewValues["PUR_OF_TRAN"] = ddlPOT.SelectedValue.ToString();
        e.NewValues["SYS_USR_LOGIN_NAME"] = strUserName.Trim();
    }
    //MODIFIED BY KOWSHIK END HERE(19-JULY-2012)(8,30-AUG-2012)(4,5,6-SEP-2012)    
}
