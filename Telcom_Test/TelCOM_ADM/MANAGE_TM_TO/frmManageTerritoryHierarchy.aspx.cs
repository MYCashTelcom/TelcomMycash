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

public partial class MANAGE_TM_TO_frmManageTerritoryHierarchy : System.Web.UI.Page
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
        try
        {
            txtSearchAccount.Enabled = false;
            hdfRankId.Value = "";
            hdfTerritoryRankId.Value = "";
            string strSql = "";
            strSql = " SELECT SRAL.ACCNT_ID,  SRAL.ACCNT_NO, SRAL.ACCNT_MSISDN, SRCL.CLINT_NAME, SRAR.ACCNT_RANK_ID, SRAR.RANK_TITEL, SRAL.TERRITORY_RANK_ID, MTR.TERRITORY_RANK_NAME,"
                     + " SRSP.SERVICE_PKG_NAME, HRAL.ACCNT_NO||'('||CLHR.CLINT_NAME||','||CLHR.CLINT_ADDRESS1||')' HIERARCHY_NAME_ADDRESS, "
                     + " UPCL.CLINT_NAME||', '||UPCL.CLINT_ADDRESS1 UPBY_INFO FROM ACCOUNT_LIST SRAL, CLIENT_LIST SRCL, ACCOUNT_RANK SRAR, "
                     + " SERVICE_PACKAGE SRSP, MANAGE_TERRITORY_HIERARCHY SRMTH, ACCOUNT_LIST HRAL, CLIENT_LIST CLHR, "
                     + " CM_SYSTEM_USERS UPSU, ACCOUNT_LIST UPAL, CLIENT_LIST UPCL, MANAGE_TERRITORY_RANK MTR WHERE SRAL.ACCNT_NO = '" + txtSearchAccount.Text.Trim() + "' "
                     + " AND SRAL.CLINT_ID = SRCL.CLINT_ID AND SRAL.ACCNT_RANK_ID = SRAR.ACCNT_RANK_ID AND SRAL.SERVICE_PKG_ID = SRSP.SERVICE_PKG_ID "
                     + " AND SRAL.ACCNT_ID = SRMTH.ACCNT_ID(+) AND SRMTH.HIERARCHY_ACCNT_ID = HRAL.ACCNT_ID(+) "
                     + " AND HRAL.CLINT_ID = CLHR.CLINT_ID(+) AND SRMTH.UPDATED_BY = UPSU.SYS_USR_ID(+) AND "
                     + " UPSU.ACCNT_ID = UPAL.ACCNT_ID(+) AND UPAL.CLINT_ID = UPCL.CLINT_ID(+) AND SRAL.TERRITORY_RANK_ID = MTR.TERRITORY_RANK_ID(+) ";

            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            string strSearchAccountRankId = "";
            string strSearchTerritoryRankId = "";
            if (oDataSet.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in oDataSet.Tables[0].Rows)
                {
                    strSearchAccountRankId = prow["ACCNT_RANK_ID"].ToString();
                    strSearchTerritoryRankId = prow["TERRITORY_RANK_ID"].ToString();
                }
            }

            dtvSearchAccInfo.Visible = true;
            dtvSearchAccInfo.DataSource = oDataSet;
            dtvSearchAccInfo.DataBind();

            string strAccountRankId = strSearchAccountRankId;
            string strAccountTerritoryRankId = strSearchTerritoryRankId;
            hdfRankId.Value = strSearchAccountRankId;
            hdfTerritoryRankId.Value = strSearchTerritoryRankId;


            LoadHierarchyUpperRankInfo(strAccountRankId, strAccountTerritoryRankId);

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadHierarchyUpperRankInfo(string strAccountRankId, string strAccountTerritoryRankId)
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
            // if account rank is distributor
            if (strAccountRankId == "120519000000000003" && strAccountTerritoryRankId == "")
            {
                drpParent.Items.Clear();
                string strSql = "";
                strSql = " SELECT DISTINCT AL.ACCNT_NO, AL.ACCNT_NO||'('||CL.CLINT_NAME || ', ' || CL.CLINT_ADDRESS1 ||')' UPPER_HIERARCHY_INFO "
                         + " FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_RANK MTR, CLIENT_LIST CL WHERE "
                         + " AL.TERRITORY_RANK_ID = MTR.TERRITORY_RANK_ID AND MTR.TERRITORY_RANK_ID = '150121000000000002' "
                         + " AND AL.ACCNT_RANK_ID = '120519000000000006' AND AL.CLINT_ID = CL.CLINT_ID ";
                DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
                drpParent.DataSource = oDataSet;
                drpParent.DataBind();
            }

            // if territory officer and mbl customer
            else if (strAccountRankId == "120519000000000006" && strAccountTerritoryRankId == "150121000000000002")
            {
                drpParent.Items.Clear();
                string strSql = "";
                strSql = " SELECT DISTINCT AL.ACCNT_NO, AL.ACCNT_NO||'('||CL.CLINT_NAME || ', ' || CL.CLINT_ADDRESS1 ||')' UPPER_HIERARCHY_INFO "
                         + " FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_RANK MTR, CLIENT_LIST CL WHERE "
                         + " AL.TERRITORY_RANK_ID = MTR.TERRITORY_RANK_ID AND MTR.TERRITORY_RANK_ID = '150121000000000001' "
                         + " AND AL.ACCNT_RANK_ID = '120519000000000006' AND AL.CLINT_ID = CL.CLINT_ID ";
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
            

            // getiing parent rankid and territory rank id
            string strSqlSearch = " SELECT AL.ACCNT_RANK_ID, AL.TERRITORY_RANK_ID  FROM ACCOUNT_LIST AL "
                            + " WHERE AL.ACCNT_RANK_ID = '120519000000000006' AND AL.ACCNT_NO = '"+drpParent.SelectedValue+"'";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSqlSearch);
            if (oDataSet.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in oDataSet.Tables[0].Rows)
                {
                    strSearchParentAccountRankId = prow["ACCNT_RANK_ID"].ToString();
                    strSearchParentTerritoryRankId = prow["TERRITORY_RANK_ID"].ToString();
                }

                // selecting serached parent item in the drop down list
                drpParent.Items.Clear();
                string strSql = "";
                strSql = " SELECT DISTINCT AL.ACCNT_NO, AL.ACCNT_NO||'('||CL.CLINT_NAME || ', ' || CL.CLINT_ADDRESS1 ||')' UPPER_HIERARCHY_INFO "
                         + " FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_RANK MTR, CLIENT_LIST CL WHERE "
                         + " AL.TERRITORY_RANK_ID = MTR.TERRITORY_RANK_ID AND MTR.TERRITORY_RANK_ID = '" + strSearchParentTerritoryRankId + "' "
                         + " AND AL.ACCNT_RANK_ID = '" + strSearchParentAccountRankId + "' AND AL.CLINT_ID = CL.CLINT_ID ";
                DataSet oSet = objServiceHandler.ExecuteQuery(strSql);
                drpParent.DataSource = oSet;
                drpParent.DataBind();
                try
                {
                    drpParent.SelectedValue = txtParentAccount.Text.Trim();
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
            strSql = " SELECT SRAL.ACCNT_ID,  SRAL.ACCNT_NO, SRAL.ACCNT_MSISDN, SRCL.CLINT_NAME, SRAR.ACCNT_RANK_ID, SRAR.RANK_TITEL, SRAL.TERRITORY_RANK_ID, MTR.TERRITORY_RANK_NAME,"
                     + " SRSP.SERVICE_PKG_NAME, HRAL.ACCNT_NO||'('||CLHR.CLINT_NAME||','||CLHR.CLINT_ADDRESS1||')' HIERARCHY_NAME_ADDRESS, "
                     + " UPCL.CLINT_NAME||', '||UPCL.CLINT_ADDRESS1 UPBY_INFO FROM ACCOUNT_LIST SRAL, CLIENT_LIST SRCL, ACCOUNT_RANK SRAR, "
                     + " SERVICE_PACKAGE SRSP, MANAGE_TERRITORY_HIERARCHY SRMTH, ACCOUNT_LIST HRAL, CLIENT_LIST CLHR, "
                     + " CM_SYSTEM_USERS UPSU, ACCOUNT_LIST UPAL, CLIENT_LIST UPCL, MANAGE_TERRITORY_RANK MTR WHERE SRAL.ACCNT_NO = '" + drpParent.SelectedValue + "' "
                     + " AND SRAL.CLINT_ID = SRCL.CLINT_ID AND SRAL.ACCNT_RANK_ID = SRAR.ACCNT_RANK_ID AND SRAL.SERVICE_PKG_ID = SRSP.SERVICE_PKG_ID "
                     + " AND SRAL.ACCNT_ID = SRMTH.ACCNT_ID(+) AND SRMTH.HIERARCHY_ACCNT_ID = HRAL.ACCNT_ID(+) "
                     + " AND HRAL.CLINT_ID = CLHR.CLINT_ID(+) AND SRMTH.UPDATED_BY = UPSU.SYS_USR_ID(+) AND "
                     + " UPSU.ACCNT_ID = UPAL.ACCNT_ID(+) AND UPAL.CLINT_ID = UPCL.CLINT_ID(+) AND SRAL.TERRITORY_RANK_ID = MTR.TERRITORY_RANK_ID(+)";
            
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

            string strAccountidIfExistinMTHTable = objServiceHandler.TerritoryAccountIdIfExist(strAccountId);
            if (strAccountidIfExistinMTHTable != "")
            {
                //update
                string strUpdateSuccMsg = objServiceHandler.UpdateTerritoryHierarchyByAccId(strAccountId,
                    strParentAccountid, strSysUserId);
                if (strUpdateSuccMsg == "Successfull.")
                {
                    lblMessage.Text = "Hirerarchy Saved Successfully";
                    SaveAuditInfo("Update", "Hirerarchy:" + txtSearchAccount.Text + ", Parent:" + drpParent.SelectedValue);
                }
                else
                {
                    lblMessage.Text = strUpdateSuccMsg;
                }
            }
            else
            {
                // add
                string strAddSuccmsg = objServiceHandler.AddToTerritoryHierarchy(strAccountId,
                    strParentAccountid, strSysUserId);
                if (strAddSuccmsg == "Successfull.")
                {
                    lblMessage.Text = "Hirerarchy Saved Successfully";
                    SaveAuditInfo("Add", "Hirerarchy:"+txtSearchAccount.Text+", Parent:"+drpParent.SelectedValue);
                }
                else
                {
                    lblMessage.Text = strAddSuccmsg;
                }
            }
        }
        catch (Exception exception)
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
}
