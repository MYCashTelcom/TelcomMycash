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

public partial class COMMON_frmAccountRankHierarchyPaywell : System.Web.UI.Page
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadAccountInfo();
    }

    private void LoadAccountInfo()
    {
        try
        {
            txtSearchAccount.Enabled = false;
            hdfRankId.Value = "";
            hdfTerritoryRankId.Value = "";
            string strSql = "";
            strSql = " SELECT DISTINCT ALA.ACCNT_ID,  ALA.ACCNT_NO, ALA.ACCNT_MSISDN, ARA.ACCNT_RANK_ID, ARA.RANK_TITEL, SP.SERVICE_PKG_NAME, CLA.CLINT_NAME, "
                     + " ALH.ACCNT_NO||'('||CLH.CLINT_NAME||','||CLH.CLINT_ADDRESS1||')' HIERARCHY_NAME_ADDRESS, AH.UPDATED_BY UPBY_INFO   "
                     + " FROM ACCOUNT_LIST ALA, ACCOUNT_RANK ARA, SERVICE_PACKAGE SP, CLIENT_LIST CLA, ACCOUNT_HIERARCHY AH, ACCOUNT_LIST ALH, CLIENT_LIST CLH "
                     + " WHERE ALA.ACCNT_NO = '" + txtSearchAccount.Text.Trim() + "' AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID(+) AND ALA.SERVICE_PKG_ID = SP.SERVICE_PKG_ID(+) "
                     + " AND ALA.CLINT_ID = CLA.CLINT_ID(+) AND ALA.ACCNT_ID = AH.ACCNT_ID(+) AND AH.HIERARCHY_ACCNT_ID = ALH.ACCNT_ID(+) AND ALH.CLINT_ID = CLH.CLINT_ID(+) ";

            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            string strSearchAccountRankId = "";
            if (oDataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow prow in oDataSet.Tables[0].Rows)
                {
                    strSearchAccountRankId = prow["ACCNT_RANK_ID"].ToString();
                }
            }

            dtvSearchAccInfo.Visible = true;
            dtvSearchAccInfo.DataSource = oDataSet;
            dtvSearchAccInfo.DataBind();

            string strAccountRankId = strSearchAccountRankId;
            hdfRankId.Value = strSearchAccountRankId;
            

            LoadHierarchyUpperRankInfo(strAccountRankId);

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadHierarchyUpperRankInfo(string strAccountRankId)
    {
        try
        {
            lblParent.Visible = true;
            txtParentAccount.Visible = true;
            btnParentSearch.Visible = true;
            drpParent.Visible = true;
            btnParentInfo.Visible = true;
            btnHieraychySave.Visible = true;

            // load upper hierarchy account info to the dropdownlist
            // if account rank is PAYWELL AGENT LOAD PAYWELL DSE INFO
            if (strAccountRankId == "161215000000000004" )
            {
                drpParent.Items.Clear();
                string strSql = "";
                strSql = " SELECT DISTINCT AL.ACCNT_NO, AL.ACCNT_NO||'('||CL.CLINT_NAME || ', ' || CL.CLINT_ADDRESS1 ||')' UPPER_HIERARCHY_INFO "
                         + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE  AL.ACCNT_RANK_ID = '161215000000000003' AND AL.CLINT_ID = CL.CLINT_ID ";
                DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
                drpParent.DataSource = oDataSet;
                drpParent.DataBind();
            }

            // if account rank is PAYWELL AGENT LOAD PAYWELL DSE INFO
            else if (strAccountRankId == "161215000000000003" )
            {
                drpParent.Items.Clear();
                string strSql = "";
                strSql = " SELECT DISTINCT AL.ACCNT_NO, AL.ACCNT_NO||'('||CL.CLINT_NAME || ', ' || CL.CLINT_ADDRESS1 ||')' UPPER_HIERARCHY_INFO "
                         + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE  AL.ACCNT_RANK_ID = '161215000000000002' AND AL.CLINT_ID = CL.CLINT_ID ";
                DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
                drpParent.DataSource = oDataSet;
                drpParent.DataBind();
            }

            // if account rank is PAYWELL AGENT LOAD PAYWELL DSE INFO
            else if (strAccountRankId == "161215000000000002")
            {
                drpParent.Items.Clear();
                string strSql = "";
                strSql = " SELECT DISTINCT AL.ACCNT_NO, AL.ACCNT_NO||'('||CL.CLINT_NAME || ', ' || CL.CLINT_ADDRESS1 ||')' UPPER_HIERARCHY_INFO "
                         + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE  AL.ACCNT_RANK_ID = '161215000000000001' AND AL.CLINT_ID = CL.CLINT_ID ";
                DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
                drpParent.DataSource = oDataSet;
                drpParent.DataBind();
            }

            // for other condition
            else
            {
                drpParent.Items.Clear();
                lblMessage.Text = "No Upper Hierarchy Account found";
                return;
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnParentSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            dtvParentInfo.Visible = false;
            string strSearchParentAccountRankId = "";
            string strSearchParentTerritoryRankId = "";


            // getiing parent rankid 
            string strSqlSearch = " SELECT AL.ACCNT_RANK_ID, AL.TERRITORY_RANK_ID  FROM ACCOUNT_LIST AL "
                            + " WHERE AL.ACCNT_NO = '" + drpParent.SelectedValue + "'";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSqlSearch);
            if (oDataSet.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in oDataSet.Tables[0].Rows)
                {
                    strSearchParentAccountRankId = prow["ACCNT_RANK_ID"].ToString();
                }

                // selecting serached parent item in the drop down list
                drpParent.Items.Clear();
                string strSql = "";
                strSql = "  SELECT DISTINCT AL.ACCNT_NO, AL.ACCNT_NO||'('||CL.CLINT_NAME || ', ' || CL.CLINT_ADDRESS1 ||')' UPPER_HIERARCHY_INFO  "
                         + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL WHERE  AL.ACCNT_RANK_ID = '" + strSearchParentAccountRankId + "' AND AL.CLINT_ID = CL.CLINT_ID ";
                DataSet oSet = objServiceHandler.ExecuteQuery(strSql);
                drpParent.DataSource = oSet;
                drpParent.DataBind();
                try
                {
                    drpParent.SelectedValue = txtParentAccount.Text.Trim();
                    btnParentSearch.Enabled = false;
                    txtParentAccount.Enabled = false;
                }
                catch (Exception exception)
                {
                    // if Searched Parent item is not in the list
                    exception.Message.ToString();
                    drpParent.Items.Clear();
                    dtvParentInfo.Visible = false;
                    lblMessage.Text = "Searched Parent item is not in the list";
                }
            }

            else
            {
                lblMessage.Text = "No Data found in the parent list";
            }

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnParentInfo_Click(object sender, EventArgs e)
    {
        try
        {
            dtvParentInfo.Visible = true;
            string strSql = "";
            strSql = " SELECT DISTINCT ALA.ACCNT_ID,  ALA.ACCNT_NO, ALA.ACCNT_MSISDN, ARA.ACCNT_RANK_ID, ARA.RANK_TITEL, SP.SERVICE_PKG_NAME, CLA.CLINT_NAME, "
                     + " ALH.ACCNT_NO||'('||CLH.CLINT_NAME||','||CLH.CLINT_ADDRESS1||')' HIERARCHY_NAME_ADDRESS, AH.UPDATED_BY UPBY_INFO   "
                     + " FROM ACCOUNT_LIST ALA, ACCOUNT_RANK ARA, SERVICE_PACKAGE SP, CLIENT_LIST CLA, ACCOUNT_HIERARCHY AH, ACCOUNT_LIST ALH, CLIENT_LIST CLH "
                     + " WHERE ALA.ACCNT_NO = '" + drpParent.SelectedValue + "' AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID(+) AND ALA.SERVICE_PKG_ID = SP.SERVICE_PKG_ID(+) "
                     + " AND ALA.CLINT_ID = CLA.CLINT_ID(+) AND ALA.ACCNT_ID = AH.ACCNT_ID(+) AND AH.HIERARCHY_ACCNT_ID = ALH.ACCNT_ID(+) AND ALH.CLINT_ID = CLH.CLINT_ID(+) ";

            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            dtvParentInfo.DataSource = oDataSet;
            dtvParentInfo.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnHieraychySave_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpParent.SelectedValue == "")
            {
                lblMessage.Text = " Select a Parent";
                return;
            }
            else
            {
                #region old code and conditions

                //checking territory officer should be tagged with territory mamager
                //string strGetTerritoryrankId = objServiceHandler.GettingTerritoryRankId(drpParent.SelectedValue);
                //if (strGetTerritoryrankId == "150121000000000002")
                //{
                //    // checking To has a TM
                //    string strTOHierarchyIfExist = objServiceHandler.TOHierarchyIdIfExist(drpParent.SelectedValue);
                //    if (strTOHierarchyIfExist != "")
                //    {
                //        SaveHierarchy();
                //    }
                //    else
                //    {
                //        // could not save
                //        lblMessage.Text = "No TM is tagged with this TO";
                //        return;
                //    }


                //}
                //else
                //{
                //    SaveHierarchy();
                //}

                #endregion

                SaveHierarchy();
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void SaveHierarchy()
    {
        try
        {
            string strAccountId = objServiceHandler.GetAccountIdByMobileNo(txtSearchAccount.Text.Trim());
            string strParentAccountid = objServiceHandler.GetAccountIdByMobileNo(drpParent.SelectedValue);
            string strSysUserId = Session["UserID"].ToString();
            if (strSysUserId == "")
            {
                lblMessage.Text = "Login Again";
                return;
            }

            // condition if same hierarchy is saved then update the hierarchy, other wise add hierarchy  
            string strAccountidIfExistinhierarchyTable = objServiceHandler.TerritoryAccountIdIfExist(strAccountId);
            if (strAccountidIfExistinhierarchyTable != "")
            {
                // update hierarchy 
                // GET Accounthierarchyid(primary key)  
                string strAccountHierarchyId = objServiceHandler.GettingAccountHierarchyID(strAccountId);
                string strUpdateSuccMsg = objServiceHandler.UpdateAccountHierarchyPaywell(strParentAccountid,
                    Session["UserLoginName"].ToString(), strAccountId, strAccountHierarchyId);
                if (strUpdateSuccMsg == "Successfull.")
                {
                    lblMessage.Text = "Saved Successfylly";
                }
                else
                {
                    lblMessage.Text = "Save Failed";
                }
            }

            else
            {
                // add hierarchy
                string strAddSuccMsg = objServiceHandler.AddToAccountHierarchyPaywell(strAccountId, strParentAccountid, Session["UserLoginName"].ToString());
                if (strAddSuccMsg == "Successfull.")
                {
                    lblMessage.Text = "Saved Successfylly";
                }
                else
                {
                    lblMessage.Text = "Save Failed";
                }

            }

            LoadAccountInfo();

        }
        catch (Exception exception)
        {
            lblMessage.Text = exception.Message.ToString();
        }
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
}
