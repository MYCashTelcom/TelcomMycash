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

public partial class COMMON_frmInsertFormSerial : System.Web.UI.Page 
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        string strSQL = "", strStatus = "", strCustMobNo = "", strAgentMobNo = "";
        lblMessage.Text = "";
        strSQL = " SELECT * FROM ACCOUNT_SERIAL_DETAIL WHERE SERIAL_NO='" + txtWallet.Text.ToString() + "'";
        DataSet oDS = new DataSet();
        clsServiceHandler objservicerHndlr = new clsServiceHandler();
        //-------------------------- Getting ACCOUNT_SERIAL_DETAIL information -----------------
        oDS = objservicerHndlr.GetDataSet(strSQL);
        if (oDS.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow pRow in oDS.Tables[0].Rows)
            {
                strStatus = pRow["STATUS"].ToString();
                strCustMobNo = pRow["CUSTOMER_MOBILE_NO"].ToString();
                strAgentMobNo = pRow["AGENT_MOBILE_NO"].ToString();
            }
        }
        else
        {
            lblMessage.Text = "This serial no is not exist.";
            return;
        }
        if (strStatus != "U")
        {
            if (strCustMobNo == "" && strAgentMobNo == "")
            {
                flsFormSerialNo.Visible = true;
                lblMessage.Text = "This serial number is Unused.";
                dtvShowData.Visible = false;
            }
            else
            {
                lblMessage.Text = "Customer and Agent mobile are exist.";
                flsFormSerialNo.Visible = false;
                dtvShowData.Visible = false;
            }
        }
        else
        {
            lblMessage.Text = "This serial number is used.";
            flsFormSerialNo.Visible = false;
            dtvShowData.Visible = false;
            return;
        }
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
        string strMsg = "", strPackageID = "", strCustomerMobile = ""; 
        clsServiceHandler objservicerHndlr = new clsServiceHandler();
        //------------------- Checking package ID ---------------------------
        strPackageID = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "SERVICE_PKG_ID", "ACCNT_MSISDN", txtAgentMobileNo.Text.ToString());
        if (txtCustMobNo.Text.ToString().Length.Equals(14) && txtAgentMobileNo.Text.ToString().Length.Equals(14))
        {
            //--------------------- Checking customer mobile number -----------------------
            strCustomerMobile = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_SERIAL_DETAIL", "CUSTOMER_MOBILE_NO", "CUSTOMER_MOBILE_NO", txtCustMobNo.Text.ToString());
            if (strCustomerMobile == "")
            {
                if (txtCustMobNo.Text.ToString() == txtAgentMobileNo.Text.ToString())
                {
                    lblMessage.Text = "Customer and Agent mobile number are same.";
                    return;
                }
                else if ((objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_MSISDN", "ACCNT_MSISDN", txtCustMobNo.Text.ToString())) == "")
                {
                    lblMessage.Text = "This customer is not exist.";
                    return;
                }
                else if ((objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_MSISDN", "ACCNT_MSISDN", txtAgentMobileNo.Text.ToString())) == "")
                {
                    lblMessage.Text = "This agent is not exist.";
                    return;
                }  
                else
                {
                    //---------------------- Checking package ID ------------------------------------
                    if ((strPackageID == "1209270001") || (strPackageID == "1211220002") || (strPackageID == "1205190001") || (strPackageID == "1205190002"))
                    {
                        strMsg = objservicerHndlr.UpdateAccntSerialDetail(txtCustMobNo.Text.Trim(), txtAgentMobileNo.Text.ToString(), txtWallet.Text.ToString());
                        SaveAuditInfo("Insert", "Customer Mobile=" + txtCustMobNo.Text.Trim() + ",Agent Mobile=" + txtAgentMobileNo.Text.ToString() + ",Form SL=" + txtWallet.Text.ToString());
                        lblMessage.Text = strMsg;
                        ShowData(txtWallet.Text.ToString());
                        ClearControl();
                    }
                    else
                    {
                        lblMessage.Text = " This agent is not authorize to update form serial number.";
                        return;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Customer mobile number is already taged.";
            }
        }
        else
        {
            lblMessage.Text = "Please insert correct mobile number.";
            return;
        }
    }
    //--------------- Shaow data in detail view ---------------------------
    private void ShowData(string strSLNo)
    {
        string strSQL = "";
        strSQL = " SELECT ASD.AGENT_MOBILE_NO ,CL.CLINT_NAME,ASD.CUSTOMER_MOBILE_NO,AL.ACCNT_NO,AR.RANK_TITEL,"
               + " ASD.SERIAL_NO,SP.SERVICE_PKG_NAME FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL,"
               + "  ACCOUNT_RANK AR,SERVICE_PACKAGE SP WHERE AL.CLINT_ID=CL.CLINT_ID AND ASD.CUSTOMER_MOBILE_NO=AL.ACCNT_MSISDN "
               + " AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND ASD.SERIAL_NO='" + strSLNo + "'";
        try
        {
            sdsShowData.SelectCommand = strSQL;
            sdsShowData.DataBind();
            dtvShowData.DataBind();
            if (dtvShowData.Rows.Count > 0)
            {
                dtvShowData.Visible = true;
            }
            else
            {
                dtvShowData.Visible = false;
            }
        }
        catch(Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void ClearControl()
    { 
        txtCustMobNo.Text = "";
        txtAgentMobileNo.Text = "";
    }
}
