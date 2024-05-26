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

public partial class MANAGE_TM_TO_frmTerritoryRegionNew : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Tab1.CssClass = "Clicked";
            MainView.ActiveViewIndex = 0;

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


            LoadRegionList();
            LoadRegion();
            LoadAreanRegionForModify();
            LoadAreanRegionForTagg();
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

    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }

    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        Tab4.CssClass = "Initial";
        MainView.ActiveViewIndex = 2;
    }

    protected void Tab4_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab4.CssClass = "Clicked";
        MainView.ActiveViewIndex = 3;
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    protected void grdRegionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdRegionList.PageIndex = e.NewPageIndex;
            LoadRegionList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grdRegionList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdRegionList.EditIndex = -1;
            LoadRegionList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grdRegionList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strId = grdRegionList.DataKeys[e.RowIndex].Values[0].ToString();
            // checking region id if exist in manage area table
            string strREgionIdIfExist = objServiceHandler.RegionIdIdIfExist(strId);
            if (strREgionIdIfExist != "")
            {
                lblMsg.Text = "Region is tagged with Area";
                return;
            }
            else
            {
                string strDeleteSuccMsg = objServiceHandler.DeleteFromManageRegion(strId);
                if (strDeleteSuccMsg == "Successful.")
                {
                    lblMsg.Text = "Deleted Successfully";
                    LoadRegionList();
                }
                else
                {
                    lblMsg.Text = strDeleteSuccMsg;
                }
            }


            LoadRegionList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grdRegionList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdRegionList.EditIndex = e.NewEditIndex;
            LoadRegionList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grdRegionList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblRegId = (Label)grdRegionList.Rows[e.RowIndex].FindControl("Label1");
            string strRegId = lblRegId.Text;
            TextBox txtRegName = (TextBox)grdRegionList.Rows[e.RowIndex].FindControl("TextBox2");
            string strRegName = txtRegName.Text;

            string strUpdateSuccMsg = objServiceHandler.UpdateTerritoryRegion(strRegId, strRegName);
            if (strUpdateSuccMsg == "Successfull.")
            {
                lblMsg.Text = "Saved Successfully";
            }
            else
            {
                lblMsg.Text = strUpdateSuccMsg;
            }

            grdRegionList.EditIndex = -1;
            LoadRegionList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadRegionList()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT REGION_ID, REGION_NAME FROM MANAGE_REGION ORDER BY REGION_NAME ASC";
            DataSet oSet = objServiceHandler.ExecuteQuery(strSql);
            grdRegionList.DataSource = oSet;
            grdRegionList.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRegionName.Text.Trim() == "")
            {
                lblMsg.Text = "Region Name Could Not Be Null";
            }
            else
            {
                // checking territory region name already exist
                string strTerritoryRegionNameAlreadyExist = objServiceHandler.TerritoryRegionNameIfExist(txtRegionName.Text.Trim());
                if (strTerritoryRegionNameAlreadyExist != "")
                {
                    lblMsg.Text = "Region Name " + txtRegionName.Text.Trim() + " Already Exist";
                    return;
                }
                else
                {
                    string strAddToRegion = objServiceHandler.AddToTerritoryRegion(txtRegionName.Text.Trim());
                    if (strAddToRegion == "Successfull.")
                    {
                        lblMsg.Text = "Saved Successfully";
                        txtRegionName.Text = "";
                        //LoadRegion();
                        LoadRegionList();
                        SaveAuditInfo("Add", txtRegionName.Text);
                    }
                    else
                    {
                        lblMsg.Text = strAddToRegion;
                        return;
                    }
                }


            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    protected void drpRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRegionWithThana();
    }

    private void LoadRegionWithThana()
    {
        try
        {
            lblMsg.Text = "";
            string strSql = "";
            strSql = " SELECT MR.REGION_ID, MR.REGION_NAME, MA.AREA_ID, MA.AREA_NAME "
                     + " FROM MANAGE_REGION MR, MANAGE_AREA MA WHERE MA.REGION_ID = MR.REGION_ID"
                     + " AND MA.REGION_ID = '" + drpRegion.SelectedValue + "'";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvRegion.DataSource = oDataSet;
            grvRegion.DataBind();

            if (grvRegion.Rows.Count == 0)
            {
                lblMsg.Text = "No Area is tagged with this Region";
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void grvRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvRegion.PageIndex = e.NewPageIndex;
            LoadRegionWithThana();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadRegion()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT REGION_ID, REGION_NAME FROM MANAGE_REGION ORDER BY REGION_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            drpRegion.DataSource = oDataSet;
            drpRegion.DataBind();
            drpRegion.Items.Insert(0, new ListItem("Select Region"));
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadAreanRegionForModify()
    {
        try
        {
            string strSqlArea, strSqlRegion;

            strSqlRegion = " SELECT REGION_ID, REGION_NAME FROM MANAGE_REGION ORDER BY REGION_NAME ASC";
            DataSet oSet = objServiceHandler.ExecuteQuery(strSqlRegion);
            drpRegionM.DataSource = oSet;
            drpRegionM.DataBind();
            drpRegionM.Items.Insert(0, new ListItem("Select Region"));

            strSqlArea = " SELECT MA.AREA_ID, MA.AREA_NAME FROM MANAGE_AREA MA ORDER BY MA.AREA_NAME ASC";
            DataSet oSet1 = objServiceHandler.ExecuteQuery(strSqlArea);
            drpAreaM.DataSource = oSet1;
            drpAreaM.DataBind();
            drpAreaM.Items.Insert(0, new ListItem("Select Area"));

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    protected void btnRegionAreaSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpAreaM.SelectedIndex == 0 || drpRegionM.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Area/Region to Tag";
                return;
            }
            else
            {
                // tagged area with region
                string strUpdateSuccMsg = objServiceHandler.UpdateAreaTaggedWithRegion(drpRegionM.SelectedValue, drpAreaM.SelectedValue);
                if (strUpdateSuccMsg == "Successfull.")
                {
                    lblMsg.Text = "Area Tagged with Region Successfully";
                    drpAreaM.SelectedIndex = 0;
                    drpRegionM.SelectedIndex = 0;
                    SaveAuditInfo("Tagged", "Area: " + drpAreaM.SelectedItem + ", Region: " + drpRegionM.SelectedItem);
                }
                else
                {
                    lblMsg.Text = "Unsuccessful";
                }

            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    private void LoadAreanRegionForTagg()
    {
        try
        {
            string strSqlArea, strSqlRegion;

            strSqlRegion = " SELECT REGION_ID, REGION_NAME FROM MANAGE_REGION ORDER BY REGION_NAME ASC";
            DataSet oSet = objServiceHandler.ExecuteQuery(strSqlRegion);
            drpRegTagAdd.DataSource = oSet;
            drpRegTagAdd.DataBind();
            drpRegTagAdd.Items.Insert(0, new ListItem("Select Region"));

            strSqlArea = " SELECT MA.AREA_ID, MA.AREA_NAME FROM MANAGE_AREA MA WHERE TAGGED_WITH_TERRI_REG = 'NT' ORDER BY MA.AREA_NAME ASC";
            DataSet oSet1 = objServiceHandler.ExecuteQuery(strSqlArea);
            drpAreaTagAdd.DataSource = oSet1;
            drpAreaTagAdd.DataBind();
            drpAreaTagAdd.Items.Insert(0, new ListItem("Select Area"));

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnTerrRegiAreaTagg_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpAreaTagAdd.SelectedIndex == 0 || drpRegTagAdd.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Area/Region to Tag";
                return;
            }
            else
            {
                // tagged area with region
                string strUpdateSuccMsg = objServiceHandler.UpdateAreaTaggedWithRegionaAdd(drpRegTagAdd.SelectedValue, drpAreaTagAdd.SelectedValue);
                if (strUpdateSuccMsg == "Successfull.")
                {
                    lblMsg.Text = "Area Tagged with Region Successfully";
                    drpAreaTagAdd.SelectedIndex = 0;
                    drpRegTagAdd.SelectedIndex = 0;
                    SaveAuditInfo("Tagged", "Area: " + drpAreaTagAdd.SelectedItem + ", Region: " + drpRegTagAdd.SelectedItem);
                    LoadAreanRegionForTagg();
                }
                else
                {
                    lblMsg.Text = "Unsuccessful";
                }

            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

}
