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
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objservicerHndlr = new clsServiceHandler();
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
        lblMsg.Text = "";
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
        //############ delete account id from account hierarchy table ################
        lblMsg.Text = "";
        string strSql = "";
        if (txtWalletID.Text != "")
        {
            string strAccID = "", strMsg = "", strProcedure = "", strAccIDExists = "";
            string strAccName = "", strAccRankID = "", strAccRankTitle = "", strMobileNo = "";
            strAccID = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_ID",
                                                                        "ACCNT_NO", txtWalletID.Text.Trim());
            if (strAccID != "")
            {
                strAccIDExists = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_HIERARCHY", "ACCNT_ID",
                                                                        "ACCNT_ID", strAccID);
                if (strAccIDExists != "")
                {
                    // ########### insert data into  temp table ####################
                    strProcedure = "GEN_HIERARCHY_LIST('" + txtWalletID.Text.ToString().Trim() + "')";
                    strMsg = objservicerHndlr.ExecuteProcedure(strProcedure);
                    if (strMsg != "")
                    {
                        strMobileNo = "+88" + (txtWalletID.Text.ToString()).Substring(0, 11);
                        strAccName = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("CLIENT_LIST", "CLINT_NAME",
                                                                        "CLINT_MOBILE", strMobileNo);

                        strAccRankID = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_RANK_ID",
                                                                        "ACCNT_ID", strAccID);

                        strAccRankTitle = objservicerHndlr.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_RANK", "RANK_TITEL",
                                                                       "ACCNT_RANK_ID", strAccRankID);

                        strSql = " SELECT RANK_TITEL,CLINT_NAME,ACCNT_ID,HIERARCHY_ACCNT_ID,ACCNT_NO,WORK_EDU_BUSINESS,"
                               + " CLIENT_OFFIC_ADDRESS, GRADE,SHORT_CODE,CAS_ACC_ID,'" + strAccName + "' NAME,'" + strAccRankTitle + "' TITLE,"
                               + "'" + txtWalletID.Text.Trim() + "' WALLET_NO  FROM TEMP_HIERARCHY_LIST";

                        try
                        {
                            Session["ReportSQL"] = strSql;
                            Session["RequestForm"] = "../COMMON/frmAccountHierarchyListReport.aspx";
                            Session["ReportFile"] = "../COMMON/ACC_HIERARCHY_LIST.rpt";
                            Response.Redirect("../COM/COM_ReportView.aspx");
                            SaveAuditInfo("View", "Account Hierarchy Report");
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Error In Procedure.";
                    }

                }
                else
                {
                    lblMsg.Text = "This acount is not in account Hierarchy.";
                }
            }
            else
            {
                lblMsg.Text = "This account is not registered.";
            }
        }
        else
        {

            if (rblRank.SelectedValue == "0")
            {
                strSql = " SELECT * FROM CLIENT_LIST CL, ACCOUNT_RANK AR,ACCOUNT_LIST AL,ACCOUNT_HIERARCHY AH "
                  + " WHERE CL.CLINT_ID=AL.CLINT_ID AND AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID AND AL.ACCNT_ID=AH.ACCNT_ID";

                Session["ReportFile"] = "../COMMON/ACC_HIERARCHY_LIST_GROUPWISE_ALL.rpt";

            }
            else if (rblRank.SelectedValue == "1")
            {
                strSql = " SELECT * FROM CLIENT_LIST CL, ACCOUNT_RANK AR,ACCOUNT_LIST AL,ACCOUNT_HIERARCHY AH "
                       + " WHERE CL.CLINT_ID=AL.CLINT_ID AND AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID AND AL.ACCNT_ID=AH.ACCNT_ID AND AR.SHORT_CODE='D'";
                Session["ReportFile"] = "../COMMON/ACC_HIERARCHY_LIST_GROUPWISE.rpt";
            }
            else if (rblRank.SelectedValue == "2")
            {
                strSql = "SELECT * FROM CLIENT_LIST CL,ACCOUNT_RANK AR,ACCOUNT_LIST AL,ACCOUNT_HIERARCHY AH "
                                + "WHERE  CL.CLINT_ID=AL.CLINT_ID AND AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID AND AL.ACCNT_ID=AH.ACCNT_ID AND AR.SHORT_CODE='SD' ";
                Session["ReportFile"] = "../COMMON/ACC_HIERARCHY_LIST_GROUPWISE.rpt";
            }
            else if (rblRank.SelectedValue == "3")
            {
                strSql = "SELECT * FROM CLIENT_LIST CL, ACCOUNT_RANK AR,ACCOUNT_LIST AL,ACCOUNT_HIERARCHY AH "
                      + " WHERE CL.CLINT_ID=AL.CLINT_ID AND AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID AND AL.ACCNT_ID=AH.ACCNT_ID AND AR.SHORT_CODE='SA' ";
                Session["ReportFile"] = "../COMMON/ACC_HIERARCHY_LIST_GROUPWISE.rpt";
            }
            else if (rblRank.SelectedValue == "4")
            {
                strSql = "SELECT * FROM CLIENT_LIST CL, ACCOUNT_RANK AR,ACCOUNT_LIST AL,ACCOUNT_HIERARCHY AH "
                         + " WHERE CL.CLINT_ID=AL.CLINT_ID AND AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID AND AL.ACCNT_ID=AH.ACCNT_ID AND AR.SHORT_CODE='A'";
                Session["ReportFile"] = "../COMMON/ACC_HIERARCHY_LIST_GROUPWISE.rpt";
            }

            try
            {
                Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMMON/frmAccountHierarchyListReport.aspx";
                Response.Redirect("../COM/COM_ReportView.aspx");
                SaveAuditInfo("View", "Account Hierarchy Report");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        //#######################################################################
    }

    private void SaveAuditInfo(string strOperationType, string strRemarks) 
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
