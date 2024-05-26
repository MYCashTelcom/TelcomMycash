using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UBP_frmDPDCBulkBillUpload1 : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Purpose"] = "I";
        /*if (!IsPostBack)
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
        }*/
    }

    protected void gdvBulkBillPay_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveSecurityInfo("Update", "EDOTCO_BILL_PAY");
    }

    protected void SaveSecurityInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        //objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    /********Save REB Bulk Bill Pay By Md. Shafiul Azam * ********/
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SaveBulkDPDCAccount();
    }

    public void SaveBulkDPDCAccount()
    {
        Label1.Text = "";
        try
        {
            var AccountId = txtaccountid.Text;
            string Remarks = txtremarks.Text;
            var AccountIds = AccountId.Split(',')
                                  .Select(x => x.Trim())
                                  .Where(x => !string.IsNullOrWhiteSpace(x) && x.All(char.IsDigit))
                                  .ToArray();
            int CountSucces = 0;
            int CountUnsucces = 0;
            //int delimiters = AccountId.Count(x => x == ',');
            int DPDC_AccountIDLength = AccountIds.Length;
            int DPDC_RemarksLength = Remarks.Length;
            if (DPDC_AccountIDLength > 100)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Maximum DPDC Account ID Will not greater than 100 ??')", true);
            }

            else if (DPDC_RemarksLength > 100)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Remarks maximum character will not greater than 100 ??')", true);
            }

            else
            {
                foreach (var AccountIdss in AccountIds)
                {

                    int AccountIdLength = AccountIdss.Length;
                    if (AccountIdLength > 17)
                    {
                        CountUnsucces++;
                        lblunsuccesprocess.Text = "Unsuccess" + " " + CountUnsucces.ToString();
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account Id Must be less then 14')", true);
                    }
                    else
                    {

                        CountSucces++;
                        string Parameters = "";
                        string ParametersInq = "";
                        if (RadioButton1.Checked)
                        {
                            Parameters = "I";
                            lblsuccesprocess.Text = "Success" + " " + CountSucces.ToString();
                            clsServiceHandler obj = new clsServiceHandler();
                            string Save = obj.SaveBulkDPDCAccount(AccountIdss, Remarks, Parameters);
                            txtaccountid.Text = "";
                            txtremarks.Text = "";
                            SaveSecurityInfo("Insert", "DPDC_BILL_PAY");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Save Successfully !!!')", true);

                        }
                        else if (RadioButton2.Checked)
                        {
                            Parameters = "P";
                            ParametersInq = "I";
                            lblsuccesprocess.Text = "Success" + " " + CountSucces.ToString();
                            clsServiceHandler obj = new clsServiceHandler();
                            string Save = obj.SaveBulkDPDCAccount(AccountIdss, Remarks, Parameters);
                            // string SaveInq = obj.SaveBulkDescoAccount(AccountIdss, Remarks, ParametersInq);
                            txtaccountid.Text = "";
                            txtremarks.Text = "";
                            SaveSecurityInfo("Insert", "DPDC_BILL_PAY");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Save Successfully !!!')", true);

                        }
                        else
                        {
                            Label1.Text = "Insert Unsuccessful Please Check RadioButton  ";
                        }


                    }

                }
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data is not Saved??')", true);
        }
        LoadDPDCAccountsDetails(Session["Purpose"].ToString());

    }

    /********LoadREBAccountsDetails Added By Mithu * ********/
    private void LoadDPDCAccountsDetails(string purpose, string AccntID = "")
    {
        if (!txtRebID.Text.Equals(""))
        {
            AccntID = txtRebID.Text.ToString();
        }
        clsServiceHandler obj = new clsServiceHandler();
        //Ei jaigai Edotco Utility Billpay er record show kora lagbe
        DataTable dt = obj.GetDPDCAccountsDetails(purpose, AccntID);
        if (dt.Rows.Count > 0)
        {
            gdvBulkBillPay.DataSourceID = string.Empty;
            ViewState["Paging"] = dt;
            gdvBulkBillPay.DataSource = dt;
            gdvBulkBillPay.DataBind();
        }

        else
        {
            gdvBulkBillPay.DataSource = null;
            gdvBulkBillPay.DataBind();
        }


    }

    /********Gridsorting Added By Mithu * ********/
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

    /********Gridpaging Added By Mithu * ********/
    protected void Gridpaging(object sender, GridViewPageEventArgs e)
    {
        gdvBulkBillPay.PageIndex = e.NewPageIndex;
        gdvBulkBillPay.DataSource = ViewState["Paging"];
        gdvBulkBillPay.DataBind();
    }

    /********SortGridView Added By Mithu * ********/
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

    /********CurrentSortDirection Added By Mithu * ********/
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

    protected void gdvBulkBillPay_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblAutoID = (Label)gdvBulkBillPay.Rows[e.RowIndex].FindControl("lblAutoID");
        string AutoID = lblAutoID.Text;
        DeleteDPDCAccoutRow(AutoID);
    }

    
    public void DeleteDPDCAccoutRow(string AutoID)
    {
        try
        {
            clsServiceHandler obj = new clsServiceHandler();
            string Delete = obj.DeleteDescoAccoutRow(AutoID);
            SaveSecurityInfo("Delete", "DPDC_BILL_PAY");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data delete successfully !!!')", true);
        }

        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data is not deleted??')", true);
        }

        LoadDPDCAccountsDetails(Session["Purpose"].ToString());

    }


    protected void gdvBulkBillPay_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblEditAutoID = (Label)gdvBulkBillPay.Rows[e.RowIndex].FindControl("lblEditAutoID");
        string AutoID = lblEditAutoID.Text;
        TextBox txtEditAccountID = (TextBox)gdvBulkBillPay.Rows[e.RowIndex].FindControl("txtEditAccountID");
        string DPDCAccountID = txtEditAccountID.Text;
        TextBox txtEditRemarkse = (TextBox)gdvBulkBillPay.Rows[e.RowIndex].FindControl("txtEditRemarkse");
        string Remarks = txtEditRemarkse.Text;
        //TextBox txtEditPurpose = (TextBox)gdvBulkBillPay.Rows[e.RowIndex].FindControl("txtEditPurpose");
        //string Purpose = txtEditPurpose.Text;
        int DPDCAccountIDLength = DPDCAccountID.Length;
        int REBRemarksLength = Remarks.Length;
        if (DPDCAccountIDLength > 17)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('REB Account ID maximum digit will not greater than 17 ??')", true);
        }

        else if (REBRemarksLength > 100)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Remarks maximum character will not greater than 100 ??')", true);
        }
        //else if (Purpose != "I" && Purpose != "P" )
        //{
        //   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Purpose Should be I or P')", true);

        //}
        //else if (Purpose != "P")
        //{
        //   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Purpose Should be I or P')", true);

        //}
        else
        {
            UpdateDPDCAccoutRow(AutoID, DPDCAccountID, Remarks);
        }

    }

    /********UpdateREBAccoutRow Added By Mithu * ********/

    public void UpdateDPDCAccoutRow(string AutoID, string REBAccountID, string Remarks)
    {
        try
        {

            clsServiceHandler obj = new clsServiceHandler();
            string Update = obj.UpdateDescoAccoutRow(AutoID, REBAccountID, Remarks);
            SaveSecurityInfo("Update", "DPDC_BILL_PAY");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data update successfully !!!')", true);
            //LoadREBAccountsDetails(Session["Purpose"].ToString());
            Response.Redirect("frmRebBulkBillPay.aspx");
        }

        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data is not updated??')", true);
            //LoadREBAccountsDetails(Session["Purpose"].ToString());
            Response.Redirect("frmRebBulkBillPay.aspx");
        }

        LoadDPDCAccountsDetails(Session["Purpose"].ToString());

    }
    protected void gdvBulkBillPay_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvBulkBillPay.EditIndex = e.NewEditIndex;
        LoadDPDCAccountsDetails(Session["Purpose"].ToString());
    }


    protected void gdvBulkBillPay_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvBulkBillPay.EditIndex = -1;
        LoadDPDCAccountsDetails(Session["Purpose"].ToString());
    }

    protected void btnInquiry_Click(object sender, EventArgs e)
    {
        Session["Purpose"] = "I";
        LoadDPDCAccountsDetails(Session["Purpose"].ToString());
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        Session["Purpose"] = "P";
        LoadDPDCAccountsDetails(Session["Purpose"].ToString());
    }
}