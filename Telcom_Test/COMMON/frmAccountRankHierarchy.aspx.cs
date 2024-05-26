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

public partial class COMMON_frmAccountRankHierarchy : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    public delegate void delAccRank(string strAccountID,string strAccHirchyID,string strUpdatedBy);
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
         string strSql1="";
         lblMessage.Text = "";         
         ddlShowHierarchyList.Items.Clear();
         dtvParentInfo.Visible = false;
         btnSave.Visible = false;
         ddlShowHierarchyList.Visible = false;
         lblParent.Visible = false;
         btnView.Visible = false;
         txtWalletSearch.Text = "";
         try
         {
             strSql1 = " SELECT AL.ACCNT_MSISDN ,CL.CLINT_NAME,SP.SERVICE_PKG_NAME ,AR.RANK_TITEL,DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))) CLIENT_RANK_NAME,AH.HIERARCHY_ACCNT_ID,AH.UPDATED_BY"
                      + " FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH"
                      + " WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND "
                      + " AH.ACCNT_ID(+)=AL.ACCNT_ID  AND AL.ACCNT_NO='" + txtWalletAccountNo.Text.Trim().ToString() + "'";
             sdsWalletInfo.SelectCommand = strSql1;
             sdsWalletInfo.DataBind();
             dtvAccInfo.DataBind();
             if (dtvAccInfo.Rows.Count > 0)
             {
                 lblWalletID.Visible = true;
                 txtWalletSearch.Visible = true;
                 btnViewSelectedValue.Visible = true;
                 dtvAccInfo.Visible = true;
                 ddlShowHierarchyList.Visible = true;                
                 lblParent.Visible = true;
                 btnView.Visible = true;
                 string strRankID = "";
                 strRankID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_RANK_ID", "ACCNT_MSISDN", "+88" + (txtWalletAccountNo.Text.Trim()).Substring(0, 11));
                 if ((strRankID != "120519000000000006") && strRankID != "120712000000000005" && strRankID != "120712000000000010")
                 {
                     ShowDataInDropDownList();
                 }
                 else
                 {
                     lblMessage.Text = "This Account is Customer.";
                 }
             }
             else
             {
                 lblMessage.Text = "This wallet is Not Registered.";
             }
             
         }
         catch (Exception ex)
         {
             lblMessage.Text = "Err." + ex.Message.ToString();
         }
       
    }
    private void ShowDataInDropDownList()
    {
        bool blnReturn = false;
        string strSql = "", strGradeRemarksValue = "", strRankID = "", strRemarks = "";
        strGradeRemarksValue = objServiceHandler.ReturnGrade(txtWalletAccountNo.Text.Trim().ToString());
        string[] strArrayGradeRemarks = strGradeRemarksValue.Split(',');
        int intGradeValue = Convert.ToInt32(strArrayGradeRemarks[0].ToString());
        strRemarks = strArrayGradeRemarks[1].ToString();
        if (intGradeValue >= 0)
        {
            strRankID = objServiceHandler.ReturnAccountRankID(Convert.ToString(intGradeValue), strRemarks);
            blnReturn = objServiceHandler.ReturnHierarchyCountValue(strRankID);
            if (blnReturn == true)
            {
                strSql = " SELECT AL.ACCNT_MSISDN ,AL.ACCNT_MSISDN|| '  ('||CL.CLINT_NAME || ')' ACCNT_NAME FROM ACCOUNT_LIST AL,CLIENT_LIST CL WHERE AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_RANK_ID='" + strRankID + "'";
                sdsParent.SelectCommand = strSql;
                sdsParent.DataBind();
            }
            else
            {
                lblMessage.Text = "This Account has no Parent.";
            }
           
        }
        else
        {
            ddlShowHierarchyList.Items.Clear();
            ddlShowHierarchyList.Items.Add("N/A");
        }

       /* #region  Show Data In DropDown
        string strSql = "", strGradeRemarksValue = "", strRankID = "", strRemarks = "";

        bool blnForLoop = false;
        int i;
        try
        {
            strGradeRemarksValue = objServiceHandler.ReturnGrade(txtWalletAccountNo.Text.Trim().ToString());
            string[] strArrayGradeRemarks = strGradeRemarksValue.Split(',');
            int intGradeValue = Convert.ToInt32(strArrayGradeRemarks[0].ToString());
            strRemarks = strArrayGradeRemarks[1].ToString();
            if (intGradeValue >= 0)
            {
                for (i = intGradeValue; i >= 0; i--)
                {
                    blnForLoop = objServiceHandler.ReturnIntValue(Convert.ToString(i), strRemarks);
                    if (blnForLoop == true)
                        break;
                }
                if (i >= 0)
                {
                    ddlShowHierarchyList.Items.Clear();
                    try
                    {
                        strRankID = objServiceHandler.ReturnAccountRankID(Convert.ToString(i), strRemarks);
                        strSql = "SELECT ACCNT_MSISDN FROM ACCOUNT_LIST WHERE ACCNT_RANK_ID='" + strRankID + "'";
                        sdsParent.SelectCommand = strSql;
                        sdsParent.DataBind();
                    }
                    catch (Exception exx)
                    {
                        lblMessage.Text = exx.Message.ToString();
                    }
                }
            }
            else
            {
                ddlShowHierarchyList.Items.Clear();
                ddlShowHierarchyList.Items.Add("N/A");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        #endregion*/
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string strAccountIDExist = "", strAccIDCheck = "";
        delAccRank objDelegate = new delAccRank(SaveHierarchyInformation);
        string strAccID = "", strAccHierarchyID = "", strUpdateBy = "";
        strAccountIDExist = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", txtWalletAccountNo.Text.Trim().ToString());
        strAccIDCheck = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_HIERARCHY", "ACCNT_ID", "ACCNT_ID", strAccountIDExist);
        if (strAccIDCheck == "")
        {
            string strAccIDExist = "", strAccountIDCheck = "";
            strAccIDExist = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_MSISDN", ddlShowHierarchyList.SelectedValue.ToString());
            strAccountIDCheck = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_HIERARCHY", "ACCNT_ID", "ACCNT_ID", strAccIDExist);           
            if (strAccountIDCheck != "")
            {   
                strAccID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", txtWalletAccountNo.Text.Trim().ToString());
                strAccHierarchyID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_MSISDN", ddlShowHierarchyList.SelectedValue.ToString());
                strUpdateBy = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("CLIENT_LIST", "CLINT_NAME", "CLINT_MOBILE", ddlShowHierarchyList.SelectedValue.ToString());               
                objDelegate(strAccID, strAccHierarchyID, strUpdateBy);
                ShowUpdatedInformation();
                SaveAuditInfo("Insert", "Account Rank Hierarchy,Account_No=" + txtWalletAccountNo.Text.Trim().ToString() + ",Hierarchy Account  Mobile="+ddlShowHierarchyList.SelectedValue.ToString()+"");
            }
            else
            {
                if (ddlShowHierarchyList.SelectedValue.ToString() == "N/A")
                {
                    strAccID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", txtWalletAccountNo.Text.Trim().ToString());
                    strAccHierarchyID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_RANK", "ACCNT_RANK_ID", "RANK_TITEL", ddlShowHierarchyList.SelectedValue.ToString());
                    string strMobileNo = "";
                    strMobileNo = "+88"+(txtWalletAccountNo.Text.ToString()).Substring(0, 11);
                    strUpdateBy = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("CLIENT_LIST", "CLINT_NAME", "CLINT_MOBILE", strMobileNo);
                    objDelegate(strAccID, strAccHierarchyID, strUpdateBy);
                    ShowUpdatedInformation();
                    SaveAuditInfo("Insert", "Account Rank Hierarchy,Account_No=" + txtWalletAccountNo.Text.Trim().ToString() + ",Hierarchy Account  Mobile=" + ddlShowHierarchyList.SelectedValue.ToString() + "");
                }
                else
                {
                    lblMessage.Text = "The Selected Parent is not Bounded.";
                }
            }
        }
        else
        {
            btnSave.Visible = false;
            lblMessage.Text = "This Wallet is Already Bounded.";
            UpdateHierarchy();
            ShowUpdatedInformation();                       
        }
    }
    private void ShowUpdatedInformation()
    {
        try
        {
            string strSql1 = " SELECT AL.ACCNT_MSISDN ,CL.CLINT_NAME,SP.SERVICE_PKG_NAME ,AR.RANK_TITEL,CLIENT_RANK(AH.HIERARCHY_ACCNT_ID) CLIENT_RANK_NAME,AH.HIERARCHY_ACCNT_ID,AH.UPDATED_BY"
                      + " FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH"
                      + " WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND "
                      + " AH.ACCNT_ID(+)=AL.ACCNT_ID  AND AL.ACCNT_NO='" + txtWalletAccountNo.Text.Trim().ToString() + "'";
            sdsWalletInfo.SelectCommand = strSql1;
            sdsWalletInfo.DataBind();
            dtvAccInfo.DataBind();
            dtvAccInfo.Visible = true;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void UpdateHierarchy()
    {
        string strAccID = "", strAccHierarchyID = "", strUpdateBy = "";
        if (ddlShowHierarchyList.SelectedValue.Equals("N/A"))
        {
            strAccID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", txtWalletAccountNo.Text.Trim().ToString());
            strAccHierarchyID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_RANK", "ACCNT_RANK_ID", "RANK_TITEL", ddlShowHierarchyList.SelectedValue.ToString());
            strUpdateBy = "N/A";
            UpdateParent(strAccID, strAccHierarchyID, strUpdateBy);
            SaveAuditInfo("Update", "Account Rank Hierarchy,Account_No=" + txtWalletAccountNo.Text.Trim().ToString() + ",Hierarchy Account  Mobile=" + ddlShowHierarchyList.SelectedValue.ToString() + "");
        }
        else
        {
            string strAccIDExist = "", strAccountIDCheck = "";
            strAccIDExist = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_MSISDN", ddlShowHierarchyList.SelectedValue.ToString());
            strAccountIDCheck = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_HIERARCHY", "ACCNT_ID", "ACCNT_ID", strAccIDExist);
            if (strAccountIDCheck != "")
            {
                strAccID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", txtWalletAccountNo.Text.Trim().ToString());
                strAccHierarchyID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_MSISDN", ddlShowHierarchyList.SelectedValue.ToString());
                strUpdateBy = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("CLIENT_LIST", "CLINT_NAME", "CLINT_MOBILE", ddlShowHierarchyList.SelectedValue.ToString());
                UpdateParent(strAccID, strAccHierarchyID, strUpdateBy);
                SaveAuditInfo("Update", "Account Rank Hierarchy,Account_No=" + txtWalletAccountNo.Text.Trim().ToString() + ",Hierarchy Account  Mobile=" + ddlShowHierarchyList.SelectedValue.ToString() + "");
            }
            else
            {
                lblMessage.Text = "The Selected Parent is not Bounded.";
            }
        }
    }
    private void UpdateParent(string strAccID, string strAccHierarchyID, string strUpdateBy)
    {
        if (strAccID != "" && strAccHierarchyID != "")
        {
            string strMsg = objServiceHandler.UpdateHierarchyInfo(strAccID, strAccHierarchyID, strUpdateBy);
            lblMessage.Text = strMsg;
        }
    }
    private void SaveHierarchyInformation(string strAccID,string  strAccHierarchyID,string strUpdateBy) 
    {
        if (strAccID != "" && strAccHierarchyID != "" )
        {
            string strMsg = objServiceHandler.SaveHierarchyInfo(strAccID, strAccHierarchyID, strUpdateBy);
            lblMessage.Text = strMsg;
        }
        else
        {
            lblMessage.Text = "All Necessary Information are not Provided Correctly.";
        }
    }
    override protected void OnInit(EventArgs e)
    {
        btnSearch.Attributes.Add("onclick", "javascript:" +
                  btnSearch.ClientID + ".disabled=true;" +
                  this.GetPostBackEventReference(btnSearch));
        InitializeComponent();
        base.OnInit(e);
    }
    private void InitializeComponent()
    {
        this.btnSearch.Click +=
                new System.EventHandler(this.btnSearch_Click);
        this.Load += new System.EventHandler(this.Page_Load);
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        try
        {
        string strSqlData = "";
        strSqlData = " SELECT AL.ACCNT_MSISDN ,CL.CLINT_NAME,SP.SERVICE_PKG_NAME ,AR.RANK_TITEL FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR "
                   + "  WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.ACCNT_MSISDN='" + ddlShowHierarchyList.SelectedValue.ToString()+"'";
       
            sdsParentInfo.SelectCommand = strSqlData;
            sdsParentInfo.DataBind();
            dtvParentInfo.DataBind();
            if (dtvParentInfo.Rows.Count > 0)
            {
                dtvParentInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnViewSelectedValue_Click(object sender, EventArgs e)
    {
        if (txtWalletSearch.Text.Trim() != "")
        {
            try
            {
            string strWalletID = "", strMobileNo = "";
            strWalletID = txtWalletSearch.Text.Trim();
            strMobileNo = "+88" + strWalletID.Substring(0, 11);
            ddlShowHierarchyList.ClearSelection();
            
                ddlShowHierarchyList.Items.FindByValue(strMobileNo).Selected = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "This number is not available in the list.";
            }
        }
        else
        {
            lblMessage.Text = "Please Insert a Wallet ID.";
        }
    }
}
