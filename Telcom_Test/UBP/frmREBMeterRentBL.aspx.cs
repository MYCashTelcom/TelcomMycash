using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UBP_frmREBMeterRentBL : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    //clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsServiceHandler obj = new clsServiceHandler();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
        if (!IsPostBack)
        {
            try
            {

                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
                //LoadREBAccountsDetails(Session["Purpose"].ToString());
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
    }

    protected void gdvBulkBillPay_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveSecurityInfo("Update", "Reb_Meter_BL");
    }
    protected void SaveSecurityInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveREBAccountBL();
    }
     public void SaveREBAccountBL()
    {
         try
         {
             var smsAccNumber = txtSMSAccNoAdd.Text;
             var mrent = txtMRENTAdd.Text;
             if(smsAccNumber!="" && mrent!="")
             {
                 //int checkDuplicate = obj.CheckDuplicateRebMRBL(smsAccNumber, mrent);
                 string strSqlCheck = "SELECT COUNT(*) FROM APSNG101.UBP_REB_METERRENT_BL WHERE SMS_ACC_NO = '" + smsAccNumber + "'";
                 string strCheckDuplicate = "0";
                 strCheckDuplicate = obj.ReturnString(strSqlCheck);
                 if (strCheckDuplicate == "0")
                 {
                     string saveData = obj.SaveRebMRBL(smsAccNumber, mrent);
                     if(saveData=="Successfull")
                     {
                         Label1.Text = "";
                         Label2.Text = "Successfully inserted data";
                         SaveSecurityInfo("Added", "Reb_Meter_Rent_BL_Add");
                         txtMRENTAdd.Text = "";
                         txtSMSAccNoAdd.Text = "";
                     }
                     else
                     {
                         Label2.Text = "";
                         Label1.Text = "Failed to insert data";
                         txtMRENTAdd.Text = "";
                         txtSMSAccNoAdd.Text = "";

                     }
                 }
                 else
                 {
                     Label2.Text = "";
                     Label1.Text = "This account number already exists";
                     txtMRENTAdd.Text = "";
                     txtSMSAccNoAdd.Text = "";
                 }
                 
             }
             else
             {
                 Label2.Text = "";
                 Label1.Text = "Please insert data";
                 txtMRENTAdd.Text = "";
                 txtSMSAccNoAdd.Text = "";

             }
             
         }
         catch
         {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data is not Saved.')", true);
         }
        
    }
     protected void btnSearch_Click(object sender, EventArgs e)
     {
         LoadREBAccountsDetails();
     }
     private void LoadREBAccountsDetails()
     {

         var smsAccNoSearch = txtsmsAccNoSearch.Text;
             if(smsAccNoSearch!=null)
             {
                 DataTable dt = obj.GetREBBLAccountsDetails(smsAccNoSearch);
                 if (dt.Rows.Count > 0)
                 {
                     gdvBulkBillPay.DataSourceID = string.Empty;
                     ViewState["Paging"] = dt;
                     gdvBulkBillPay.DataSource = dt;
                     gdvBulkBillPay.DataBind();
                     Label2.Text = "";
                 }

                 else
                 {
                     gdvBulkBillPay.DataSource = null;
                     gdvBulkBillPay.DataBind();
                     Label2.Text = "";
                     Label1.Text = "No data found!";
                 }
             }
             else
             {
                 Label2.Text = "";
                 Label1.Text = "Please insert data";

             }       
     }
     protected void Gridsorting(object sender, GridViewSortEventArgs e)
     {
         string ColumnTosort = e.SortExpression;

         if (CurrentSortDirection == SortDirection.Ascending)
         {
             CurrentSortDirection = SortDirection.Descending;
             SortGridView(ColumnTosort, DESCENDING);
         }
         else
         {
             CurrentSortDirection = SortDirection.Ascending;
             SortGridView(ColumnTosort, ASCENDING);
         }

     }
     protected void Gridpaging(object sender, GridViewPageEventArgs e)
     {
         gdvBulkBillPay.PageIndex = e.NewPageIndex;
         gdvBulkBillPay.DataSource = ViewState["Paging"];
         gdvBulkBillPay.DataBind();
     }
     private void SortGridView(string sortExpression, string direction)
     {
         //  You can cache the DataTable for improving performance
         dynamic dt = ViewState["Paging"];
         DataTable dtsort = dt;
         DataView dv = new DataView(dtsort);
         dv.Sort = sortExpression + direction;

         gdvBulkBillPay.DataSource = dv;
         gdvBulkBillPay.DataBind();
     }
     public SortDirection CurrentSortDirection
     {
         get
         {
             if (ViewState["sortDirection"] == null)
             {
                 ViewState["sortDirection"] = SortDirection.Ascending;
             }

             return (SortDirection)ViewState["sortDirection"];
         }
         set
         {
             ViewState["sortDirection"] = value;

         }
     }

     protected void gdvBulkBillPay_RowUpdating(object sender, GridViewUpdateEventArgs e)
     {
         Label lblEditAutoID = (Label)gdvBulkBillPay.Rows[e.RowIndex].FindControl("lblEditAutoID");
         string AutoID = lblEditAutoID.Text;
         TextBox txtEditAccountID = (TextBox)gdvBulkBillPay.Rows[e.RowIndex].FindControl("txtEditAccountID");
         string REBAccountID = txtEditAccountID.Text;
         TextBox txtMrent = (TextBox)gdvBulkBillPay.Rows[e.RowIndex].FindControl("txtMrent");
         string Remarks = txtMrent.Text;
         int REBAccountIDLength = REBAccountID.Length;
         int REBRemarksLength = Remarks.Length;
         if (REBAccountIDLength > 17)
         {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('REB Account ID maximum digit will not greater than 17 ??')", true);
         }

         //else if (REBRemarksLength > 100)
         //{
         //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Remarks maximum character will not greater than 100 ??')", true);
         //}
         else
         {
             UpdateREBAccoutRow(AutoID, REBAccountID, Remarks);
         }

     }

     public void UpdateREBAccoutRow(string AutoID, string REBAccountID, string Remarks)
     {
         try
         {

             clsServiceHandler obj = new clsServiceHandler();
             string Update = obj.UpdateREBBLAccoutRow(AutoID, REBAccountID, Remarks);
             SaveSecurityInfo("Update", "Reb_Meter_Rent_BL_Update");
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data update successfully !!!')", true);
             LoadREBAccountsDetails();
             Label1.Text = "";
             Label2.Text = "Successfully updated!";
             //Response.Redirect("frmREBMeterRentBL.aspx");
         }

         catch (Exception)
         {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data is not updated??')", true);
             //LoadREBAccountsDetails(Session["Purpose"].ToString());
             Response.Redirect("frmREBMeterRentBL.aspx");
         }

         LoadREBAccountsDetails();

     }
     protected void gdvBulkBillPay_RowEditing(object sender, GridViewEditEventArgs e)
     {
         gdvBulkBillPay.EditIndex = e.NewEditIndex;
         LoadREBAccountsDetails();
     }


     protected void gdvBulkBillPay_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
     {
         gdvBulkBillPay.EditIndex = -1;
         LoadREBAccountsDetails();
     }
     
}