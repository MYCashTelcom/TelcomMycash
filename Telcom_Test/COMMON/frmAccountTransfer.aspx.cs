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
using System.Collections.Generic;

public partial class COMMON_frmAccountTransfer : System.Web.UI.Page
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
    protected void txtOldWalletSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if ((txtOldWalletID.Text.ToString()).Length.Equals(12))
        {
            ShowOldWalletData();
        }
        else
        {
            lblMsg.Text = "Invalid Wallet ID.";
        }
    }

    private void ShowOldWalletData()
    {
        string strSQL = "", strSQLData = "";
        strSQL = " SELECT CL.CLINT_NAME,AL.ACCNT_NO,AR.RANK_TITEL,SP.SERVICE_PKG_NAME FROM ACCOUNT_LIST AL,"
              + " CLIENT_LIST CL,ACCOUNT_RANK AR,SERVICE_PACKAGE SP WHERE AL.CLINT_ID=CL.CLINT_ID AND "
              + " AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_NO='" + txtOldWalletID.Text.ToString() + "'";

        strSQLData = " SELECT ACCOUNT_LIST.ACCNT_ID,ACCOUNT_LIST.ACCNT_NO,CL.CLINT_NAME,AR.RANK_TITEL FROM ACCOUNT_LIST, ACCOUNT_HIERARCHY, "
                   + " ACCOUNT_LIST ACCOUNT_LIST_1 ,CLIENT_LIST CL,ACCOUNT_RANK AR"
                   + " WHERE ACCOUNT_LIST.ACCNT_ID = ACCOUNT_HIERARCHY.ACCNT_ID AND ACCOUNT_HIERARCHY.HIERARCHY_ACCNT_ID = ACCOUNT_LIST_1.ACCNT_ID "
                   + " AND CL.CLINT_ID=ACCOUNT_LIST.CLINT_ID AND AR.ACCNT_RANK_ID=ACCOUNT_LIST.ACCNT_RANK_ID AND ACCOUNT_LIST_1.ACCNT_NO='" + txtOldWalletID.Text.Trim() + "'";
        try
        {
            sdsOldWalletID.SelectCommand = strSQL;
            sdsOldWalletID.DataBind();
            dtvOldWalletInfo.DataBind();
            if (dtvOldWalletInfo.Rows.Count > 0)
            {
                dtvOldWalletInfo.Visible = true;
            }
            else
            {
                dtvOldWalletInfo.Visible = false;
            }

            sdsOldAccnt.SelectCommand = strSQLData;
            sdsOldAccnt.DataBind();
            gdvOldAccount.DataBind();
            if(gdvOldAccount.Rows.Count>0)
            {
                fldpreviousChild.Visible = true;
                gdvOldAccount.Visible=true;
            }
            else
            {
                fldpreviousChild.Visible = false;
                gdvOldAccount.Visible=false;
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }   
    protected void btnNewWalletSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if ((txtNewWalletID.Text.ToString()).Length.Equals(12))
        {
            ShowNewWalletData();
        }
        else
        {
            lblMsg.Text = "Invalid Wallet ID.";
        }
    }

    private void ShowNewWalletData()
    {
        string strSQL = "", strSQLData = "";
        strSQL = " SELECT CL.CLINT_NAME,AL.ACCNT_NO,AR.RANK_TITEL,SP.SERVICE_PKG_NAME FROM ACCOUNT_LIST AL,"
              + " CLIENT_LIST CL,ACCOUNT_RANK AR,SERVICE_PACKAGE SP WHERE AL.CLINT_ID=CL.CLINT_ID AND "
              + " AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_NO='" + txtNewWalletID.Text.ToString() + "'";

        strSQLData = " SELECT ACCOUNT_LIST.ACCNT_NO,CL.CLINT_NAME,AR.RANK_TITEL FROM ACCOUNT_LIST, ACCOUNT_HIERARCHY, "
                   + " ACCOUNT_LIST ACCOUNT_LIST_1,CLIENT_LIST CL,ACCOUNT_RANK AR "
                  + " WHERE ACCOUNT_LIST.ACCNT_ID = ACCOUNT_HIERARCHY.ACCNT_ID AND ACCOUNT_HIERARCHY.HIERARCHY_ACCNT_ID = ACCOUNT_LIST_1.ACCNT_ID "
                  + " AND CL.CLINT_ID=ACCOUNT_LIST.CLINT_ID AND AR.ACCNT_RANK_ID=ACCOUNT_LIST.ACCNT_RANK_ID AND ACCOUNT_LIST_1.ACCNT_NO='" + txtNewWalletID.Text.Trim() + "'";
        try
        {
            sdsNewWalletID.SelectCommand = strSQL;
            sdsNewWalletID.DataBind();
            dtvNewWalletInfo.DataBind();
            if (dtvNewWalletInfo.Rows.Count > 0)
            {
                dtvNewWalletInfo.Visible = true;
            }
            else
            {
                dtvNewWalletInfo.Visible = false;
            }
            sdsNewAccount.SelectCommand = strSQLData;
            sdsNewAccount.DataBind();
            gdvNewAccount.DataBind();
            if (gdvNewAccount.Rows.Count > 0)
            {
                fldNewChild.Visible = true;
                gdvNewAccount.Visible = true;               
            }
            else
            {
                fldNewChild.Visible = false;
                gdvNewAccount.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string strOldRankID = "", strNewRankID = "", strNewAccntID = "",strCheckAccount="";
        strOldRankID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_RANK_ID", "ACCNT_NO", txtOldWalletID.Text.Trim());
        strNewRankID=objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_RANK_ID", "ACCNT_NO", txtNewWalletID.Text.Trim());
        strNewAccntID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", txtNewWalletID.Text.Trim());
        strCheckAccount = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_HIERARCHY", "ACCNT_ID", "ACCNT_ID", strNewAccntID);        
        if (strCheckAccount == "")
        {
            lblMsg.Text = "New Parent not in hierarchy list.";
            return;
        }
        else if (txtOldWalletID.Text == txtNewWalletID.Text)
        {
            lblMsg.Text = "Previous parent and new parent are same.";
            return;
        }
        else if (strOldRankID != strNewRankID)
        {
            lblMsg.Text = "Account Rank are not Same.";
            return;
        }       
        else
        {
            string strTotalAccountID = "", strTotalAccntNo = "";
            List<string> lstTotalAccntID = new List<string>();
            List<string> lstTotalAccnt = new List<string>();
            foreach (GridViewRow grow in gdvOldAccount.Rows)
            {
                CheckBox chk;
                bool blnVisible;
                chk = (CheckBox)grow.FindControl("chkIndividual");
                blnVisible = grow.Visible;
                if (chk.Checked)
                {
                    if (blnVisible == true)
                    {
                        //strSpMessage = objAccount.UpdateSetUpStatus(grow.Cells[1].Text.ToString());
                    }
                    lstTotalAccnt.Add(grow.Cells[1].Text.ToString());
                    lstTotalAccntID.Add(objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID", "ACCNT_NO", grow.Cells[1].Text.ToString()));
                }
            }
            if (lstTotalAccntID.Count > 0)
            {
                string[] myStringArray = (string[])lstTotalAccntID.ToArray();
                strTotalAccountID = string.Join(",", myStringArray);
                string[] arryAccntNo = (string[])lstTotalAccnt.ToArray();
                strTotalAccntNo = string.Join(",", arryAccntNo);
                lblMsg.Text = objServiceHandler.UpdateHierarchy(strNewAccntID, strTotalAccountID);
                SaveAuditInfo("Transfer", "Previous Parent=" + txtOldWalletID.Text + " and child=" + strTotalAccntNo + " Transfer to New Parent=" + txtNewWalletID.Text + " and Child=" + strTotalAccntNo);
                ShowNewWalletData();
                ShowOldWalletData();
            }
            else
            {
                lblMsg.Text = "Select a account.";
                return;
            }
        }               
    }
    private void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    override protected void OnInit(EventArgs e)
    {
        btnTransfer.Attributes.Add("onclick", "javascript:" +
                  btnTransfer.ClientID + ".disabled=true;" +
                  this.GetPostBackEventReference(btnTransfer));
        //InitializeComponent();
        base.OnInit(e);
    }

    //private void InitializeComponent()
    //{
    //    this.btnTransfer.Click +=
    //            new System.EventHandler(this.btnTransfer_Click);
    //    this.Load += new System.EventHandler(this.Page_Load);
    //}   
}
