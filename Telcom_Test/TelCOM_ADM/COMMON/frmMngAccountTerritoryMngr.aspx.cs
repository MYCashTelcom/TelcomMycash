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

public partial class COMMON_frmMngAccountTerritoryMngr : System.Web.UI.Page
{   
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    DataSet oDS;

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
    protected void gdvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LoadSearchResult();
    }
    protected void gdvSearch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LoadSearchResult();
    }
    protected void gdvSearch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        LoadSearchResult();
        gdvSearch.SelectedIndex = e.NewEditIndex;
    }
    protected void gdvSearch_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        LoadSearchResult();
        int index = gdvSearch.EditIndex;
        GridViewRow grow = gdvSearch.Rows[index];
        SaveAuditInfo("Update", "Manage Wallet");
    }
    protected void gdvSearch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSrvcPkg = "", strWalletID = "", strAccRank = "", strStatus = "", strRemarks = "";

        GridViewRow row = (GridViewRow)gdvSearch.Rows[e.RowIndex];
        DropDownList ddlSrvcPkg = (DropDownList)row.FindControl("ddlEIPackage");
        strSrvcPkg = ddlSrvcPkg.SelectedItem.ToString();

        Label lblWalletID = (Label)row.FindControl("Label2");
        strWalletID = lblWalletID.Text.ToString();

        DropDownList ddlAccRank = (DropDownList)row.FindControl("ddlAccntrankID");
        strAccRank = ddlAccRank.SelectedItem.ToString();

        DropDownList ddlStatus = (DropDownList)row.FindControl("DropDownList1");
        strStatus = ddlStatus.SelectedItem.ToString();

        //##################################################

        //################# Updating  Cash Accnt Type ID ######################
        clsServiceHandler objSrvHndlr = new clsServiceHandler();
        DataSet dsAccntNo = null;
        string strAccntID = "", strAccRankID = "", strSrvcPkgID = "", strMsg = "", strAccountID = "", strCashAccntTypeID = "";
        Label lblAccntID = (Label)row.FindControl("Label1");
        strAccntID = lblAccntID.Text.ToString();

        DropDownList ddlAccRankID = (DropDownList)row.FindControl("ddlAccntrankID");
        strAccRankID = ddlAccRankID.SelectedValue.ToString();

        DropDownList ddlSrvcPkgID = (DropDownList)row.FindControl("ddlEIPackage");
        strSrvcPkgID = ddlSrvcPkgID.SelectedValue.ToString();

        strCashAccntTypeID = objSrvHndlr.GettingCashAccountID(strAccRankID);

        dsAccntNo = objSrvHndlr.GetAllWalletAccount(strWalletID);

        if (dsAccntNo.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow pRow in dsAccntNo.Tables[0].Rows)
            {
                objSrvHndlr.UpdateCashAccTypeID(strCashAccntTypeID, pRow["CAS_ACC_NO"].ToString());
            }
        }

        lblMsg.Text = "Successfull.";
        //################# Updating  Cash Accnt Type ID ######################

        //########################## Audit Log #####################################
        strRemarks = strSrvcPkg + "," + strWalletID + "," + strAccRank + "," + strStatus + "," + strCashAccntTypeID;
        SaveAuditInfo("Update", strRemarks);
        //##########################################################################
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    public void LoadSearchResult()    {
        
            string strChecking = "";
            lblMsg.Text = "";
            gdvSearch.Visible = false;
            string strSql = "", strAddSQL = "";
            if (txtName.Text.ToString().Trim() == "" && txtAccCode.Text.ToString().Trim() == "" && txtMSISDN.Text.ToString().Trim() == "")
            {
                lblMsg.Text = "Enter Client Name or wallet ID or Mobile No.";
            }
            else if (txtName.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() != "" && txtMSISDN.Text.ToString().Trim() != "")
            {
                lblMsg.Text = "Enter Client Name or wallet ID or Mobile No.";
            }
            else if (txtName.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() != "")
            {
                lblMsg.Text = "Enter Client Name or wallet ID.";
            }
            else if (txtName.Text.ToString().Trim() != "" && txtMSISDN.Text.ToString().Trim() != "")
            {
                lblMsg.Text = "Enter Client Name or Mobile No.";
            }
            else if (txtMSISDN.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() != "")
            {
                lblMsg.Text = "Enter Wallet ID or Mobile No.";
            }
            else
            {
                if (txtName.Text.ToString().Trim() != "")
                {
                    strAddSQL = " C.CLINT_NAME LIKE '" + txtName.Text.ToString().Trim() + "%' ";
                }
                else if (txtAccCode.Text.ToString().Trim() != "")
                {
                    strAddSQL = " A.ACCNT_NO='" + txtAccCode.Text.ToString().Trim() + "' ";
                    strChecking = (txtAccCode.Text.ToString().Trim()).Substring(0, 5);
                }
                else if (txtMSISDN.Text.ToString().Trim() != "")
                {
                    strAddSQL = " A.ACCNT_MSISDN='" + txtMSISDN.Text.ToString().Trim() + "' ";
                    strChecking = (txtMSISDN.Text.ToString().Trim()).Substring(0, 5);
                }

                if (strChecking != "00000" && strChecking != "+8800")
                {
                    strSql = " SELECT A.CLINT_ID, ACCNT_ID, ACCNT_NO, ACCNT_MSISDN, CLINT_NAME,A.STATE_NOTE, "
                           + " A.SERVICE_PKG_ID, SERVICE_PKG_NAME, ACCNT_STATE, ACCNT_RANK_ID "
                           + " FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S "
                           + " WHERE A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID AND " + strAddSQL;

                    //strSql = " SELECT A.CLINT_ID, ACCNT_ID, ACCNT_NO, ACCNT_MSISDN, CLINT_NAME,A.STATE_NOTE, "
                    //       + " A.SERVICE_PKG_ID, SERVICE_PKG_NAME, ACCNT_STATE, ACCNT_RANK_ID "
                    //       + " FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S "
                    //       + " WHERE A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID AND A.ACCNT_RANK_ID = '130914000000000001' AND " + strAddSQL;

                    try
                    {
                        sdsClientAccount2.SelectCommand = strSql;
                        sdsClientAccount2.DataBind();
                        gdvSearch.DataBind();
                        if (gdvSearch.Rows.Count > 0)
                        {
                            gdvSearch.Visible = true;
                            lblMsg.Text = "";
                        }
                        else
                        {
                            gdvSearch.Visible = false;
                            lblMsg.Text = "Sorry, No New Customer Data found.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = " You are not authorized to see this account.";
                    }
                }
                else
                {
                    lblMsg.Text = " You are not authorized to see this account.";
                } 
        }

        
        }    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadSearchResult();
    }
    protected void btnConvertToAgent_Click(object sender, EventArgs e)
    {
        string strSrvcPkg = "", strWalletID = "", strAccRank = "", strStatus = "", strRemarks = "" ;        
        GridViewRow row = (GridViewRow)gdvSearch.Rows[0];
        DropDownList ddlSrvcPkg1 = (DropDownList)row.FindControl("ddlEIPackage");
        strSrvcPkg = ddlSrvcPkg1.SelectedValue.ToString();

        DropDownList ddlAccRank = (DropDownList)row.FindControl("ddlAccntRankName");
        strAccRank = ddlAccRank.SelectedValue.ToString();

        Label labelWalletId = (Label) row.FindControl("Label2");
        strWalletID = labelWalletId.Text.ToString();

        clsServiceHandler objServicehandler = new clsServiceHandler();
        DataSet dts = new DataSet();

        string strAccId = "";        
        //----------------- Update Customer to Agent by Territory Manager-----------------------------
       
        strAccId = objServicehandler.UpdateCustomerToAgent(strWalletID, strAccRank, strSrvcPkg);
        //---------------------------------------------------------------------------------------------

        //---------------------- Update Cas Account Type ID --------------------------------------------
        string strAccRankID = "", strCashAccntTypeID = "";
        DataSet dsAccntNo = null;
        strAccRankID = "120519000000000005";
        strCashAccntTypeID = objServicehandler.GettingCashAccountID(strAccRankID);
        dsAccntNo = objServicehandler.GetAllWalletAccount(strWalletID);
        if (dsAccntNo.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow pRow in dsAccntNo.Tables[0].Rows)
            {
                objServicehandler.UpdateCashAccTypeID(strCashAccntTypeID, pRow["CAS_ACC_NO"].ToString());
            }
        }
        //------------------------ End of Update Cash Account Type ID-------------------------------------
        LoadSearchResult();
        strUserName = Session["UserLoginName"].ToString();
        strRemarks = strWalletID + "," + strAccRank + "," + strSrvcPkg + "," + strUserName;
        SaveAuditInfo("Update", strRemarks);
    }
    protected void gdvSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gdvSearch.SelectedRow;
    }
    protected void gdvSearch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = gdvSearch.Rows[e.NewSelectedIndex];
    }    
}
