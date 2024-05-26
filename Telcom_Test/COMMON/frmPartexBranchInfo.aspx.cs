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
using System.Threading;

public partial class COMMON_frmPartexBranchInfo : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objsrvsHndlr = new clsServiceHandler();
    DataSet dsData;
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                GetData();
            }
            catch (Exception exxx)
            {

            }

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetData();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strBranchName = txtBranchName.Text.Trim();
        
        string strAddress = txtAddress.Text.Trim();
        string strCode = txtBranchCode.Text.Trim();
       
        string strEmpName = txtEmployeeName.Text.Trim();
        string strEmpMobile = txtEmployeeMobile.Text.Trim();

        string brType = ddlBrType.Text.ToString();
       

        try 
        {
            if (String.IsNullOrEmpty(strBranchName))
            {
                lblError.Text = "Office Name Needed";
                return;
            }
            if (strEmpMobile.Length != 11)
            {
                lblError.Text = "Incharge Mobile No Must be 11 Digit";
                return;
            }
            if (strCode.Length > 4 || strCode.Length < 3)
            {
                lblError.Text = "Office Code Needs To Be 3 or 4 Digits";
                return;
            }
            else
            {
               
                string leadsMob = "01713367384";
                string conMob = "01708462427";
                string mobileNo = "";
                string branchCode = "";
                string CasAccTypeId = "";
                if (strCode.Length == 3)
                {
                    branchCode += "0";
                }
                branchCode += strCode;
                var isExist = objsrvsHndlr.GetPartexBranchInfoByCode(branchCode, brType);
                if (!String.IsNullOrEmpty(isExist))
                {
                    lblError.Text = "Office Code Already Exist";
                    return;
                }
                if (brType == "FURNITURE")
                {
                    mobileNo = "0110101" + branchCode;
                    CasAccTypeId = "22042601002002";
                    leadsMob = "01713367384";
                    conMob = "01708462427";
                   
                }
                else
                {
                    leadsMob = "01708808125";
                    conMob = "01770790287";                
                    mobileNo = "0110100" + branchCode;
                    CasAccTypeId = "22042601002001";
                }

                string getSerial = objsrvsHndlr.GetAccountSerial();
                string insertServiceHandler = "*RG*" + mobileNo + "*" + getSerial + "#";

                DateTime date = DateTime.Now;
                string strDataTime = "";
                string strSMSC_ID = "";
                strDataTime = date.ToString("yyMMddHHmmss");
                strSMSC_ID = "AP" + strDataTime;
               
                string insertSrvsRes = objsrvsHndlr.InsertPartexBranchServiceHandler(insertServiceHandler, strSMSC_ID);
                string columnOne = "REQUEST_ID";
                string table = "SERVICE_REQUEST";
                string columnTwo = "SMSC_REFERENCE_NO";
                string valueTwo = strSMSC_ID;

                string requestId = objsrvsHndlr.ReturnOneColumnValueByAnotherColumnValue(table, columnOne, columnTwo, valueTwo);            
      
                string response = "";
                for (int i = 0; i < 100; i++)
                {
                    string res = objsrvsHndlr.ReturnOneColumnValueByAnotherColumnValue("SERVICE_RESPONSE", "RESPONSE_MESSAGE", "REQUEST_ID", requestId);

                    if (res == "")
                    {
                        Thread.Sleep(2000);
                    }
                    else if (res.Contains("successful") || res.Contains("Congratulations"))
                    {
                        string insertRes = objsrvsHndlr.InsertIntoPartexBranch(strBranchName, branchCode, strAddress, leadsMob, strEmpName, strEmpMobile, conMob, brType);

                        string insertbulkTable = objsrvsHndlr.InsertTmpBulkServiceHandler(mobileNo, strBranchName, "10/01/1990", DateTime.Now.ToString("dd/MM/yyyy"), "PARTEX "+ brType);
                                               

                        response = "OK";
                        break;
                    }
                    else
                    {
                        response = res;
                    }


                }
                if (response != "OK")
                {
                    lblError.Text = response;
                    return;
                }

                string strTableName = "ACCOUNT_LIST";
                string strColumnOne = "ACCNT_ID";
                string strColumnNameTwo = "ACCNT_NO";
                string strColumnValueTwo = mobileNo + "1";
                string inAccntList = "";
                for (int i = 0; i < 60; i++)
                {
                    string accountRes = objsrvsHndlr.ReturnOneColumnValueByAnotherColumnValue(strTableName, strColumnOne, strColumnNameTwo, strColumnValueTwo);
                    if (accountRes != "")
                    {
                        inAccntList = "Y";                      
                        string verify = objsrvsHndlr.VerifyPartexAccount();
                        string updateAccountList = objsrvsHndlr.UpdateAccountListPartexBranchInfo("A", "2204260001", "220426000000000002", mobileNo + "1");
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                string updateCasAccountList = objsrvsHndlr.UpdateCasAccountListPartexBranchInfo(CasAccTypeId, mobileNo + "1");
                string updateCasAccountList2 = objsrvsHndlr.UpdateCasAccountListPartexBranchInfo(CasAccTypeId, mobileNo + "2");
                string updateClientBankOne = objsrvsHndlr.UpdateClientBankAccPartexBranchInfo(conMob + "1", mobileNo + "1");
                string updateClientBankTwo = objsrvsHndlr.UpdateClientBankAccPartexBranchInfo(conMob + "2", mobileNo + "2");
                string UpdatePartexWallet = objsrvsHndlr.UpdatePartexBranchWalletNo(branchCode, brType, mobileNo + "1");
            
              
                

               
                }
                lblMessage.Text = "Information Successfully Saved.";
                txtBranchCode.Text = "";
                txtAddress.Text = "";
                txtBranchName.Text = "";
                //txtLeadsMobile.Text = "";
                //txtConMobile.Text = "";
                txtEmployeeMobile.Text = "";
                txtEmployeeName.Text = "";        
                GetData();
        }
        catch (Exception ex)
        {

            lblError.Text = ex.Message;
        }
    }

    protected void gdvBranchList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvBranchList.PageIndex = e.NewPageIndex;
        GetData();
    }
    protected void gdvBranchList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvBranchList.EditIndex = e.NewEditIndex;
    }
    protected void gdvBranchList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtNamee = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtBranchName") as TextBox;
        //TextBox txtcodee = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtBranchCode") as TextBox;
        TextBox txtaddresss = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtBranchAddress") as TextBox;

        //TextBox txtLead = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtlblLeadsMobile") as TextBox;
        TextBox txtEmpNamee = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtEmployeeName") as TextBox;
        TextBox txtEmpMobilee = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtEmployeeMobile") as TextBox;
        //TextBox txtCon = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtlblConMobile") as TextBox;
        //TextBox txtWallett = gdvBranchList.Rows[e.RowIndex].FindControl("gvtxtWallet") as TextBox;
        TextBox tstatus = gdvBranchList.Rows[e.RowIndex].FindControl("txtStatus") as TextBox;

        Label lblId = gdvBranchList.Rows[e.RowIndex].FindControl("lblBranchId") as Label;
        string name = txtNamee.Text.Trim();
        
        //string code = txtcodee.Text.Trim();
        string address = txtaddresss.Text.Trim();
        string branchId = lblId.Text.Trim();

        //string Lead = txtLead.Text.Trim();
        string empName = txtEmpNamee.Text.Trim();
        string empMobile = txtEmpMobilee.Text.Trim();
        //string Con = txtCon.Text.Trim();
       // string wallet = txtWallett.Text.Trim();
        string status = tstatus.Text.Trim();
        //txtSearch.Text = branchId;
        string updateRes = objsrvsHndlr.UpdatePartexBranchInfo(branchId, name, address, empName, empMobile, status);
        lblMessage.Text = updateRes;
        gdvBranchList.EditIndex = -1;
        
        GetData();

    }
    protected void gdvBranchList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvBranchList.EditIndex = -1;

    }
    protected void gdvCustomerEmailAddressList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gdvBranchList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gdvBranchList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void GetData()
    {
        string all = txtSearch.Text.Trim();
        string strQry = "SELECT * FROM PARTEX_BRANCH_INFO";
        if (all != "")
        {
            strQry = "SELECT * FROM PARTEX_BRANCH_INFO WHERE BR_NAME='" + all + "' ORDER BY PARTEX_BRANCH_INFO_ID DESC";
        }
        else
        {
            strQry = "SELECT * FROM PARTEX_BRANCH_INFO ORDER BY PARTEX_BRANCH_INFO_ID DESC";
        }
        DataSet dt = objsrvsHndlr.ExecuteQuery(strQry);
        gdvBranchList.DataSource = dt;
        gdvBranchList.DataBind();
    }


    



}