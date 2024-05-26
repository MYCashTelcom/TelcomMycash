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
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using System.Text;

public partial class UBP_frmDescoBulkBillUpload : System.Web.UI.Page
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

    protected void gdscoBulkBillUp_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveSecurityInfo("Update", "DPDC_BILL_UPLOAD");
    }

    protected void SaveSecurityInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

 
    /******** DPDC BILL Upload By Talha *********/
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload2.HasFile)
            {
                //------------------- ReNaming  excel Sheet name ----------------------------------
                string strGetExtensionName = "", strFileName = "", strNewFileName = "";
                strFileName = Path.GetFileName(FileUpload2.PostedFile.FileName); //FileUpload1.PostedFile.FileName;
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

                //------------------ upload excel file in folder and database ----------------
                FileUpload2.PostedFile.SaveAs(Server.MapPath("~/App_Data") + System.IO.Path.DirectorySeparatorChar + strNewFileName);

                DataTable dt = ReadExcelFile(Server.MapPath("~/App_Data") + System.IO.Path.DirectorySeparatorChar + strNewFileName);

                //------------------ row insert status -----------------
                DataTable dtInsertStatus = new DataTable();
                dtInsertStatus.Columns.Add("ACCOUNT_NUMBER");
                dtInsertStatus.Columns.Add("BILL_TYPE");
                dtInsertStatus.Columns.Add("ACCOUNT_STATUS");
                dtInsertStatus.Columns.Add("REMARK");
                dtInsertStatus.Columns.Add("PURPOSE");
                dtInsertStatus.Columns.Add("MESSAGE");
                if (dt.Rows.Count <= 100)
                {

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow pRow in dt.Rows)
                        {
                            string strAgentAccntNo = pRow[0].ToString();
                            string strBillType = pRow[1].ToString();
                            string strAccountStatus = pRow[2].ToString();
                            string strRemark = pRow[3].ToString();
                            string strPurpose = pRow[4].ToString();
                            strRemark = RemoveSpecialCharacters(strRemark);

                            //string strMessage = pRow[6].ToString();

                            //string strSql = "SELECT COUNT(*) FROM APSNG101.ACCOUNT_LIST WHERE ACCNT_RANK_ID = '200203000000000001' AND ACCNT_NO = '" + strAgentAccntNo + "'";
                            // string strIsExist = objServiceHandler.ReturnString(strSql);
                            //string strIsExist = "1";
                            //if (strIsExist.Equals("1"))
                            //{
                            try
                            {
                                //DateTime enteredDate = DateTime.ParseExact(strRemark, "M/d/yyyy h:mm:ss tt", CultureInfo.GetCultureInfo("en-US"));
                                //string transTime = enteredDate.ToString("MM/dd/yy hh:mm:ss tt");

                                //string strSqlCheck = "SELECT COUNT(*) FROM APSNG101.EDOTCO_UTILITY_BILL_PAY WHERE ACCOUNT_NUMBER = '" + strAgentAccntNo + "' AND LOCATION_ID = '" + strLocationId + "' AND BILL_TYPE = '" + "DPDC" + "' AND ACCOUNT_STATUS = '" + "A" + "' AND REMARK = '" + strRemark + "' AND PURPOSE = '" + "P" + "'";
                                //string strCheckDuplicate = "0";
                                //string strCheckDuplicate = objServiceHandler.ReturnString(strSqlCheck);
                                // if (Convert.ToInt32(strCheckDuplicate) > 0)
                                // {
                                //     dtInsertStatus.Rows.Add(strAgentAccntNo, strLocationId, "DPDC", "A", strRemark, "P", "Duplicate");
                                //     SaveAuditInfo("Failed", "Attach sheet has some duplicate records");
                                // }
                                // else
                                // {
                                string strResult = objServiceHandler.InsertDESCOBulkBillUpload(strAgentAccntNo, "DESCO", "A", strRemark, "P");
                                //string strResult = "Successfull";
                                if (strResult.Equals("Successfull"))
                                {
                                    dtInsertStatus.Rows.Add(strAgentAccntNo, strBillType, "A", strRemark, "P", "Success");
                                    SaveAuditInfo("Success", "Successfully Inserted Desco Bills: " + strNewFileName);
                                    lblMessage.Text = "Successfully Uploaded the bills. Please see upload status!";
                                }
                                else
                                {
                                    dtInsertStatus.Rows.Add(strAgentAccntNo, strBillType, "A", strRemark, "P", "Failed");
                                    SaveAuditInfo("Failed", "Failed To Insert DESCO Bills- ex:DB Query Failed : " + strNewFileName);


                                }
                                // }
                            }
                            catch (Exception ex)
                            {
                                dtInsertStatus.Rows.Add(strAgentAccntNo, strBillType, "A", strRemark, "P", "ex:Failed");
                                ex.Message.ToString();
                                SaveAuditInfo("Failed", "Failed To Insert DESCO Bills- ex:Failed");

                            }
                            //}
                            //else
                            //{
                            //    dtInsertStatus.Rows.Add(strAgentAccntNo, strLocationId, "DPDC", "A", strRemark, "P", "Agent Number Wrong");
                            //    SaveAuditInfo("Failed", "Failed To Insert DPDC Bills Due To Wrong Agent Number");
                            //}
                        }
                    }

                    else
                    {
                        lblMessage.Text = "Attached sheet does not have any records.";
                        SaveAuditInfo("Failed", "Attached DESCO bill sheet does not have any records");
                    }
                }
                else
                {
                    lblMessage.Text = "Attached sheet has too many records. Must be less than 100 records";
                    SaveAuditInfo("Failed", "Attached DESCO bill sheet has too many records- ex:More than 100 records");
                }

                Session["dtInsertStatus"] = dtInsertStatus;
                FileUpload2.Attributes.Clear();
            }
            else
            {
                lblMessage.Text = "Please choose a file to upload.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "File (" + FileUpload2.PostedFile.FileName + ") could not be uploaded successfully. [" + ex.Message.ToString() + "]";
        }
    }
    //Removing Special Character By Talha
    public static string RemoveSpecialCharacters(string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public DataTable ReadExcelFile(string strFileName)
    {
        DataTable dtBillDetail = new DataTable();
        dtBillDetail.Columns.Add("ACCOUNT_NUMBER");
        dtBillDetail.Columns.Add("BILL_TYPE");
        dtBillDetail.Columns.Add("ACCOUNT_STATUS");
        dtBillDetail.Columns.Add("REMARK");
        dtBillDetail.Columns.Add("PURPOSE");

        //############################## checking file extension for connection string start here #################
        string strGetExtensionName = "";
        strGetExtensionName = strFileName.Substring(strFileName.Length - 4);
        string ConnectionString = "";
        try
        {
            if (strGetExtensionName == ".xls")
            {
                ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + strFileName + "; Extended Properties=\"Excel 8.0;HDR=Yes\";";
            }
            else if (strGetExtensionName == "xlsx")
            {
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=\"Excel 12.0;HDR=Yes\";";
            }

            //################################ connection string for excel sheet ############################### 
            OleDbConnection conExcell = new OleDbConnection(ConnectionString);
            conExcell.Open();
            DataTable dts = conExcell.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string strSheetName = dts.Rows[0][2].ToString();

            string CommandText = "select * from [" + strSheetName + "]";
            DataSet dsBillDetail = new DataSet();
            OleDbCommand OLEDBcmd = new OleDbCommand(CommandText, conExcell);
            OleDbDataAdapter oOrdersDataAdapter1 = new OleDbDataAdapter(OLEDBcmd);

            oOrdersDataAdapter1.Fill(dsBillDetail, strSheetName);
            conExcell.Close();
            //#########################################################

            foreach (DataRow pRow in dsBillDetail.Tables[strSheetName].Rows)
            {
                string strAgentAccntNo = pRow[0].ToString();

                string strBillType = pRow[1].ToString();
                string strAccountStatus = pRow[2].ToString();
                string strRemark = pRow[3].ToString();
                string strPurpose = pRow[4].ToString();
                dtBillDetail.Rows.Add(strAgentAccntNo, strBillType, strAccountStatus, strRemark, strPurpose);

                //if (strAgentAccntNo.StartsWith("01"))
                //{
                //    dtBillDetail.Rows.Add(strAgentAccntNo, strLocationId, strBillType, strAccountStatus, strRemark, strPurpose);
                //}
            }
            return dtBillDetail;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return dtBillDetail;
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtInsertStatus = Session["dtInsertStatus"] as DataTable;
            if (dtInsertStatus.Rows.Count > 0 || !dtInsertStatus.Equals("null"))
            {
                //GenerateInsertDetailReport();
                gdscoBulkBillUp.DataSourceID = string.Empty;
                ViewState["Paging"] = dtInsertStatus;
                gdscoBulkBillUp.DataSource = dtInsertStatus;
                gdscoBulkBillUp.DataBind();
            }
            else
            {
                gdscoBulkBillUp.DataSource = null;
                gdscoBulkBillUp.DataBind();
                lblMessage.Text = "Attached sheet does not have any records.";
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
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
        gdscoBulkBillUp.PageIndex = e.NewPageIndex;
        gdscoBulkBillUp.DataSource = ViewState["Paging"];
        gdscoBulkBillUp.DataBind();
    }

    /********SortGridView Added By Mithu * ********/
    private void SortGridView(string sortExpression, string direction)
    {
        //  You can cache the DataTable for improving performance
        dynamic dt = ViewState["Paging"];
        DataTable dtsort = dt;
        DataView dv = new DataView(dtsort);
        dv.Sort = sortExpression + direction;

        gdscoBulkBillUp.DataSource = dv;
        gdscoBulkBillUp.DataBind();
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

    protected void gdscoBulkBillUp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblAutoID = (Label)gdscoBulkBillUp.Rows[e.RowIndex].FindControl("lblAutoID");
        string AutoID = lblAutoID.Text;
        //DeleteDPDCAccoutRow(AutoID);
    }

   


    protected void gdscoBulkBillUp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lblEditAutoID = (Label)gdscoBulkBillUp.Rows[e.RowIndex].FindControl("lblEditAutoID");
        string AutoID = lblEditAutoID.Text;
        TextBox txtEditAccountID = (TextBox)gdscoBulkBillUp.Rows[e.RowIndex].FindControl("txtEditAccountID");
        string REBAccountID = txtEditAccountID.Text;
        TextBox txtEditRemarkse = (TextBox)gdscoBulkBillUp.Rows[e.RowIndex].FindControl("txtEditRemarkse");
        string Remarks = txtEditRemarkse.Text;
        //TextBox txtEditPurpose = (TextBox)gdvBulkBillPay.Rows[e.RowIndex].FindControl("txtEditPurpose");
        //string Purpose = txtEditPurpose.Text;
        int REBAccountIDLength = REBAccountID.Length;
        int REBRemarksLength = Remarks.Length;
        if (REBAccountIDLength > 17)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('REB Account ID maximum digit will not greater than 17 ??')", true);
        }

        else if (REBRemarksLength > 100)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Remarks maximum character will not greater than 100 ??')", true);
        }

        else
        {
            //UpdateREBAccoutRow(AutoID, REBAccountID, Remarks);
        }

    }

    /********UpdateREBAccoutRow Added By Mithu * ********/

    //public void UpdateREBAccoutRow(string AutoID, string REBAccountID, string Remarks)
    //{
    //    try
    //    {

    //        clsServiceHandler obj = new clsServiceHandler();
    //        string Update = obj.UpdateREBAccoutRow(AutoID, REBAccountID, Remarks);
    //        SaveSecurityInfo("Update", "Reb_BILL_PAY");
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data update successfully !!!')", true);
    //        //LoadREBAccountsDetails(Session["Purpose"].ToString());
    //       Response.Redirect("frmRebBulkBillPay.aspx");
    //    }

    //    catch (Exception)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data is not updated??')", true);
    //        //LoadREBAccountsDetails(Session["Purpose"].ToString());
    //        Response.Redirect("frmRebBulkBillPay.aspx");
    //    }

    //    //LoadREBAccountsDetails(Session["Purpose"].ToString());

    //}
    protected void gdscoBulkBillUp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdscoBulkBillUp.EditIndex = e.NewEditIndex;
        //LoadREBAccountsDetails(Session["Purpose"].ToString());
    }


    protected void gdscoBulkBillUp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdscoBulkBillUp.EditIndex = -1;
        //LoadREBAccountsDetails(Session["Purpose"].ToString());
    }

    //protected void btnInquiry_Click(object sender, EventArgs e)
    //{

    //    Session["Purpose"] = "I";
    //    LoadREBAccountsDetails(Session["Purpose"].ToString());

    //}

    //protected void btnPayment_Click(object sender, EventArgs e)
    //{
    //    Session["Purpose"] = "P";
    //    LoadREBAccountsDetails(Session["Purpose"].ToString());


    //}


    

    
}
