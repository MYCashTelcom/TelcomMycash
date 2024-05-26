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

public partial class MANAGE_TM_TO_frmTerritoryAreaNew : System.Web.UI.Page
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


            LoadArea();
            LoadAreaThanaforModify();
            LoadAreaList();
            LoadAreaforAddtoThana();
            LoadThanaAddtoArea();
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

    private void LoadAreaforAddtoThana()
    {
        try
        {
            string strSql = " SELECT AREA_ID, AREA_NAME FROM MANAGE_AREA ORDER BY AREA_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            drpAddTerrArea.DataSource = oDataSet;
            drpAddTerrArea.DataBind();
            drpAddTerrArea.Items.Insert(0, new ListItem("Select Area"));
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadThanaAddtoArea()
    {
        try
        {
            string strSql = " SELECT THANA_ID, THANA_NAME FROM MANAGE_THANA WHERE TAGGED_WITH_TERRI_AREA = 'NT'";

            DataSet oData = objServiceHandler.ExecuteQuery(strSql);
            drpAddThanawt.DataSource = oData;
            drpAddThanawt.DataBind();
            drpAddThanawt.Items.Insert(0, new ListItem("Select Thana"));
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
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


    protected void btnAddArea_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAreaName.Text.Trim() == "")
            {
                lblMsg.Text = "Territory Area Name Could not be Null";
                return;
            }

            string strAreaName = txtAreaName.Text.Trim();
            // checking territory area name already exist
            string strTerritorAreaNameAlreadyExist = objServiceHandler.TerritoryAreaNameIfExist(strAreaName);
            if (strTerritorAreaNameAlreadyExist != "")
            {
                lblMsg.Text = "Territory Area Name " + strAreaName + " Already Exist";
                return;
            }

            else
            {
                string strAddSuccMsg = objServiceHandler.AddToTerritoryArea(strAreaName);
                if (strAddSuccMsg == "Successfull.")
                {
                    lblMsg.Text = "Saved Successfully";
                    SaveAuditInfo("Add", "Add Area");
                    txtAreaName.Text = "";
                    LoadAreaList();
                }
                else
                {
                    lblMsg.Text = strAddSuccMsg;
                }
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grvEduIns_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvEduIns.PageIndex = e.NewPageIndex;
            LoadAreaList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grvEduIns_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grvEduIns.EditIndex = -1;
            LoadAreaList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grvEduIns_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strId = grvEduIns.DataKeys[e.RowIndex].Values[0].ToString();
            // checking area id if exist in the manage thana table
            string strAreaidifExist = objServiceHandler.AreaIdIdIfExist(strId);
            if (strAreaidifExist != "")
            {
                lblMsg.Text = "Thana is Tagged with this Area";
                return;
            }
            else
            {
                string strDeleteSuccMsg = objServiceHandler.DeleteFromManageArea(strId);
                if (strDeleteSuccMsg == "Successful.")
                {
                    lblMsg.Text = "Deleted Successfully";
                    LoadAreaList();
                    SaveAuditInfo("Delete", "Territory Area");
                }
                else
                {
                    lblMsg.Text = strDeleteSuccMsg;
                }
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grvEduIns_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grvEduIns.EditIndex = e.NewEditIndex;
            LoadAreaList();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void grvEduIns_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblAreaId = (Label)grvEduIns.Rows[e.RowIndex].FindControl("Label1");
            string strAreaId = lblAreaId.Text;
            TextBox txtAreaName = (TextBox)grvEduIns.Rows[e.RowIndex].FindControl("TextBox2");
            string strAreaName = txtAreaName.Text;

            string strUpdateArea = objServiceHandler.UpdateTerritoryArea(strAreaId, strAreaName);
            if (strUpdateArea == "Successfull.")
            {
                lblMsg.Text = "Saved Successfully";
                LoadAreaList();
                SaveAuditInfo("Update", "Territory Area");
            }
            else
            {
                lblMsg.Text = strUpdateArea;
            }

            grvEduIns.EditIndex = -1;
            LoadAreaList();
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

    private void LoadAreaList()
    {
        try
        {
            string strSql = " SELECT AREA_ID, AREA_NAME FROM MANAGE_AREA ORDER BY AREA_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvEduIns.DataSource = oDataSet;
            grvEduIns.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void grvArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvArea.PageIndex = e.NewPageIndex;
            LoadAreawithThana();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadArea()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT AREA_ID, AREA_NAME FROM MANAGE_AREA ORDER BY AREA_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            drpArea.DataSource = oDataSet;
            drpArea.DataBind();
            drpArea.Items.Insert(0, new ListItem("Select Area"));

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadAreawithThana()
    {
        try
        {
            lblMsg.Text = "";
            string strSql = "";
            strSql = " SELECT MA.AREA_ID, MA.AREA_NAME, MT.THANA_ID, MT.THANA_NAME "
                     + " FROM  MANAGE_THANA MT, MANAGE_AREA MA WHERE MA.AREA_ID = MT.AREA_ID "
                     + " AND MA.AREA_ID = '" + drpArea.SelectedValue + "'";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvArea.DataSource = oDataSet;
            grvArea.DataBind();

            if (grvArea.Rows.Count == 0)
            {
                lblMsg.Text = "No Thana is Tagged with this Area";
            }

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }




    protected void drpArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAreawithThana();
    }

    protected void btnAreaThanaSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpAreaM.SelectedIndex == 0 || drpThanaM.SelectedIndex == 0)
            {
                lblMsg.Text = "Area or Thana not Selected";
                return;
            }

            else
            {
                string strUpdateSuccessfulMsg = objServiceHandler.UpdateThanaTaggedWithArea(drpAreaM.SelectedValue,
                    drpThanaM.SelectedValue);
                if (strUpdateSuccessfulMsg == "Successfull.")
                {
                    lblMsg.Text = "Thana Tagged with Area Successfully";
                    drpAreaM.SelectedIndex = 0;
                    drpThanaM.SelectedIndex = 0;
                    SaveAuditInfo("Tagging", "Area: " + drpAreaM.SelectedItem + ", Thana: " + drpThanaM.SelectedItem);
                }
                else
                {
                    lblMsg.Text = "Unsuccessful";
                    return;
                }
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadAreaThanaforModify()
    {
        try
        {
            string strSqlArea, strSqlThana;

            strSqlArea = " SELECT AREA_ID, AREA_NAME FROM MANAGE_AREA ORDER BY AREA_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSqlArea);
            drpAreaM.DataSource = oDataSet;
            drpAreaM.DataBind();
            drpAreaM.Items.Insert(0, new ListItem("Select Area"));

            strSqlThana = " SELECT THANA_ID, THANA_NAME FROM MANAGE_THANA";
            DataSet oData = objServiceHandler.ExecuteQuery(strSqlThana);
            drpThanaM.DataSource = oData;
            drpThanaM.DataBind();
            drpThanaM.Items.Insert(0, new ListItem("Select Thana"));

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnAddThwtArea_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpAddTerrArea.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Area";
                return;
            }
            if (drpAddThanawt.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Thana";
                return;
            }

            string strUpdateSuccessfulMsg = objServiceHandler.UpdateThanaTaggedWithAreaNew(drpAddTerrArea.SelectedValue,
                    drpAddThanawt.SelectedValue);
            if (strUpdateSuccessfulMsg == "Successfull.")
            {
                lblMsg.Text = "Thana Tagged with Area Successfully";
                drpAddTerrArea.SelectedIndex = 0;
                drpAddThanawt.SelectedIndex = 0;
                SaveAuditInfo("Tagging", "Area: " + drpAddTerrArea.SelectedItem + ", Thana: " + drpAddThanawt.SelectedItem);
            }
            else
            {
                lblMsg.Text = "Unsuccessful";
                return;
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
