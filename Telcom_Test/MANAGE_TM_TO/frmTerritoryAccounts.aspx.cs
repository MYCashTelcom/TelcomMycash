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

public partial class MANAGE_TM_TO_frmTerritoryAccountsAdd : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

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
            LoadRank();
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

    private void LoadRank()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT TERRITORY_RANK_ID, TERRITORY_RANK_NAME FROM MANAGE_TERRITORY_RANK WHERE TERRITORY_RANK_STATUS = 'A'";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            drpTrRank.DataSource = oDataSet;
            drpTrRank.DataBind();
            drpTrRank.Items.Insert(0, new ListItem("Select Rank"));
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string strAccount = txtAccount.Text.Trim();
            string strTerrirankId = drpTrRank.SelectedValue;

            string strAccountRankId = objServiceHandler.GettingAccountRankId(strAccount);
            if (strAccountRankId == "120519000000000006")
            {
                if (drpTrRank.SelectedIndex == 0)
                {
                    lblMsg.Text = "Select Rank";
                    return;
                }

                else
                {
                    string strUpdateSuccMsg = objServiceHandler.UpdateTerritoryRank(strAccount, strTerrirankId);
                    if (strUpdateSuccMsg == "Successfull.")
                    {
                        lblMsg.Text = "Rank Saved Successfully";
                        txtAccount.Text = "";
                        drpTrRank.SelectedIndex = 0;
                        SaveAuditInfo("Update", strAccount + ", TerrRank:" + drpTrRank.SelectedItem);
                    }
                    else
                    {
                        lblMsg.Text = strUpdateSuccMsg;
                        return;
                    }   
                }
                
            }
            else
            {
                lblMsg.Text = "Not a Customer Wallet";
                return;
            }
        }
        catch (Exception exception )
        {
            exception.Message.ToString();
        }
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridData();
    }

    private void LoadGridData()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT AL.ACCNT_NO, MTR.TERRITORY_RANK_ID, MTR.TERRITORY_RANK_NAME, CL.CLINT_NAME "
                     + " FROM MANAGE_TERRITORY_RANK MTR, ACCOUNT_LIST AL, CLIENT_LIST CL WHERE "
                     + " AL.TERRITORY_RANK_ID = MTR.TERRITORY_RANK_ID AND AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_NO = '" +
                     txtSearchAccount.Text.Trim() + "'";
            DataSet oSet = objServiceHandler.ExecuteQuery(strSql);
            grvTerr.DataSource = oSet;
            grvTerr.DataBind();

            if (grvTerr.Rows.Count == 0)
            {
                lblMsg.Text = "This Wallet is not a TO/TM Account";
                return;
            }

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void grvTerr_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grvTerr.EditIndex = e.NewEditIndex;
            LoadGridData();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grvTerr_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblAccNo = (Label)grvTerr.Rows[e.RowIndex].FindControl("Label1");
            string strAccNo = lblAccNo.Text;
            DropDownList drpRankId = (DropDownList)grvTerr.Rows[e.RowIndex].FindControl("DropDownList2");
            string strRankId = drpRankId.SelectedValue;

            string strUpdateSuccMsg = objServiceHandler.UpdateTerritoryRank(strAccNo, strRankId);
            if (strUpdateSuccMsg == "Successfull.")
            {
                lblMsg.Text = "Saved Successfully";
                grvTerr.EditIndex = -1;
                LoadGridData();
                SaveAuditInfo("Update", strAccNo + ", TerrRank:" + drpRankId.SelectedItem);
            }
            else
            {
                lblMsg.Text = strUpdateSuccMsg;
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grvTerr_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grvTerr.EditIndex = -1;
            LoadGridData();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
