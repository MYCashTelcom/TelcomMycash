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

public partial class COMMON_frmReplaceMobileNo : System.Web.UI.Page
{
    clsServiceHandler objservicerHndlr = new clsServiceHandler();
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
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        lblWMsg.Text = "";
        lblMsg.Text = "";
        string strSql = "", strSqlResult = "";
        if (TxtWalletID.Text != "")
        {
            //--------------------- checking lenth of wallet ------------------
            if ((TxtWalletID.Text.ToString().Length.Equals(12)))
            {
                //############### check for account and add data in grid #################
                strSqlResult = objservicerHndlr.SearchWallet(TxtWalletID.Text.ToString());
                if (!strSqlResult.Equals(""))
                {
                    strSql = " SELECT CL.CLINT_NAME,CL.CLINT_ADDRESS1,AL.ACCNT_NO,AL.ACCNT_MSISDN FROM ACCOUNT_LIST AL, CLIENT_LIST CL "
                           + " WHERE AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_NO='" + TxtWalletID.Text + "' AND AL.ACCNT_NO NOT IN '016162251001'";
                    sdsAccountInfo.SelectCommand = strSql;
                    sdsAccountInfo.DataBind();
                    gdvAccountInfo.DataBind();
                    if (gdvAccountInfo.Rows.Count > 0)
                    {
                        gdvAccountInfo.Visible = true;
                        txtOldNumber.Text = TxtWalletID.Text.ToString();
                        pnlModify.Visible = true;
                    }
                    else
                    {
                        lblWMsg.Text = " No Data Found.";
                        gdvAccountInfo.Visible = false;
                    }
                }
                else
                {
                    lblWMsg.Text = " No Data Found.";
                    gdvAccountInfo.Visible = false;
                }
            }
            else
            {
                lblWMsg.Text = "Wallet is not Correct. ";
                gdvAccountInfo.Visible = false;
            }
        }
        else
        {
            lblWMsg.Text = " Please Insert Wallet ID.";
            gdvAccountInfo.Visible = false;
        }
    }
    protected void btnReplace_Click(object sender, EventArgs e)
    {
        lblWMsg.Text = "";
        lblMsg.Text = "";
        string strSql = "";
        gdvAccountInfo.Visible = true;
        //############# checking number of digits ###############
        if (!CheckDigit().Equals(""))
        {
            return;
        }
        string strGetOldnumbr = objservicerHndlr.SearchOldMobileNmbr(txtOldNumber.Text.ToString(), txtNewNumber.Text.ToString());
        if (!strGetOldnumbr.Equals(""))
        {
            if (strGetOldnumbr == "Your New Number Already Registrated!")
            {
                lblMsg.Text = "Your New Number Already Registrated!";
            }
            else
            {
                string strTopUpTran = "";
                strTopUpTran = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("TOPUP_TRANSACTION", "SOURCE_ACCNT_NO", "SOURCE_ACCNT_NO", TxtWalletID.Text.ToString());
                if (strTopUpTran == "")
                {
                    //############# checking old and new number ##############################
                    string strStorPros = objservicerHndlr.WALLET_MODIFY(txtOldNumber.Text.ToString(), txtNewNumber.Text.ToString());
                    lblMsg.Text = strStorPros;
                    //############ Add data to Grid  #################
                    strSql = " SELECT CL.CLINT_NAME,CL.CLINT_ADDRESS1,AL.ACCNT_NO,AL.ACCNT_MSISDN FROM ACCOUNT_LIST AL,"
                           + " CLIENT_LIST CL WHERE AL.CLINT_ID=CL.CLINT_ID AND AL.ACCNT_NO='" + txtNewNumber.Text + "'";
                    sdsAccountInfo.SelectCommand = strSql;
                    sdsAccountInfo.DataBind();
                    lblWMsg.Text = "Your Wallet has successfully modified !";
                    lblWMsg.Visible = true;
                    pnlModify.Visible = false;
                    //################ audit info ######################
                    string strRemarks = "Old wallet=" + txtOldNumber.Text.Trim() + " Change to New Wallet=" + txtNewNumber.Text.Trim() + "";

                    string IPAddress = Request.ServerVariables["remote_addr"];
                    string Technology = Request.Browser.Browser + Request.Browser.Version;
                    string IPTechnology = IPAddress + "-" + Technology;
                    objSysAdmin.AddAuditLog(Session["UserID"].ToString(), "Update", IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
                    txtNewNumber.Text = "";
                }
                else
                {
                    lblMsg.Text = " This wallet has topup transaction.";
                }
            }
        }
        else
        {
            lblMsg.Text = "Your Old Mobile Number has not registered.";
        }
    }
    private string CheckDigit()
    {
        string strOldNmbr = txtOldNumber.Text; 
        string strNewNmbr=txtNewNumber.Text;
        Console.WriteLine(strOldNmbr.Length);
        Console.WriteLine(strNewNmbr.Length);
        if (strOldNmbr.Length != 12)
        {
            lblMsg.Text = "Your Old Wallet is not correct. ";           
            return "ERR";
        }
        else if(strNewNmbr.Length!=12)
        {
            lblMsg.Text = "Your New Wallet is not correct.";
            return "ERR";
        }
        return "";
    }
    //override protected void OnInit(EventArgs e)
    //{
    //    btnReplace.Attributes.Add("onclick", "javascript:" +
    //              btnReplace.ClientID + ".disabled=true;" +
    //              this.GetPostBackEventReference(btnReplace));
    //    InitializeComponent();
    //    base.OnInit(e);
    //}

    //private void InitializeComponent()
    //{
    //    this.btnReplace.Click +=
    //            new System.EventHandler(this.btnReplace_Click);
    //    this.Load += new System.EventHandler(this.Page_Load);
    //}
}
