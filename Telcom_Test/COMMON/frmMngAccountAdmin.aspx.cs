using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls.TableRow;

public partial class Forms_frmMngAccount : System.Web.UI.Page
{
    private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();    
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objSrvHndlr = new clsServiceHandler();
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
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
            LoadSearchResult();        
    }
    public void LoadSearchResult()
    {        

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
            }
            else if (txtMSISDN.Text.ToString().Trim() != "")
            {
                strAddSQL = " A.ACCNT_MSISDN='" + txtMSISDN.Text.ToString().Trim() + "' ";
            }

            strSql = " SELECT A.CLINT_ID, ACCNT_ID, ACCNT_NO, ACCNT_MSISDN, CLINT_NAME,A.STATE_NOTE, "
                   + " A.SERVICE_PKG_ID, SERVICE_PKG_NAME, ACCNT_STATE, ACCNT_RANK_ID "
                   + " FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S "
                   + " WHERE A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID AND " + strAddSQL;

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
                    lblMsg.Text = "Sorry, no data found.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //#####################  Kowshik Start #########################
        LoadSearchResult();
        //#################### Kowshik End  ############################# 
    }
    protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
      //  sdsClientAccount.InsertParameters["ACCNT_MSISDN"].DefaultValue=        
    }

    public string searchEasyLoadNo(string strEasyNo)
    {        
        string DepoID = "";       
        clsAccountHandler objAcc = new clsAccountHandler();
        DataSet ods = new DataSet();
        ods = objAcc.GetDuplecateID(strEasyNo);
        foreach (DataRow row in ods.Tables["ACCOUNT_LIST"].Rows)
        {
            DepoID = row["ACCNT_MSISDN"].ToString();
            if (DepoID == strEasyNo)
            {                
                
            }
        }    
        return null;
    }      
   
    protected void gdvSearch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //############## insert audit info #####################
        string strSrvcPkg = "", strWalletID = "", strAccRank = "", strStatus = "", strRemarks = "",strPreRank="";

        GridViewRow row = (GridViewRow)gdvSearch.Rows[e.RowIndex];
        DropDownList ddlSrvcPkg = (DropDownList)row.FindControl("ddlEIPackage");
        strSrvcPkg = ddlSrvcPkg.SelectedItem.ToString();

        Label lblWalletID = (Label)row.FindControl("Label2");
        strWalletID = lblWalletID.Text.ToString();

        DropDownList ddlAccRank = (DropDownList)row.FindControl("ddlAccntrankID");
        strAccRank = ddlAccRank.SelectedItem.ToString();

        //DropDownList ddlAccRankPre = (DropDownList)row.FindControl("ddlAccntRankName");
        //strPreRank = ddlAccRankPre.SelectedValue.ToString();
        
        DropDownList ddlStatus = (DropDownList)row.FindControl("DropDownList1");
        strStatus = ddlStatus.SelectedItem.ToString();

       
        //##################################################


        //---------- chaecking balance for rank changing
        //string strFisBalance = objSrvHndlr.GetAccountBalance(strWalletID);
        //double dblFisBalance = Convert.ToDouble(strFisBalance);
        //if (dblFisBalance > 1)
        //{
        //    lblMsg.Text = "You are not allowed to change the rank due to sufficient balance.";
        //    e.Cancel = true;
        //    return;
        //}


        
        //################# Updating  Cash Accnt Type ID ######################
        
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


        strRemarks = strSrvcPkg + "," + strWalletID + ",New Rank" + strAccRank + ",Pre Rank=" + strPreRank+"," + strStatus + "," + strCashAccntTypeID;
        SaveAuditInfo("Update", strRemarks);
    }
}
