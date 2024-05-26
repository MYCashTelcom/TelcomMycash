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

public partial class COMMON_frmTst : System.Web.UI.Page
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


            LoadName();
          
            LoadNameList();
           
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
        
        MainView.ActiveViewIndex = 0;
    }
    


    protected void btnAddName_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text.Trim() == "")
            {
                lblMsg.Text = " Name Could not be Null";
                return;
            }

            string strName = txtName.Text.Trim();
            // checking territory area name already exist
            string strNameAlreadyExist = objServiceHandler.AddTstInstituteInfo(strName);
            if (strNameAlreadyExist != "")
            {
                lblMsg.Text = " Name " + strName + " Already Exist";
                return;
            }

            else
            {
                string strAddSuccMsg = objServiceHandler.AddTstInstituteInfo(strName);
                if (strAddSuccMsg == "Successfull.")
                {
                    lblMsg.Text = "Saved Successfully";
                    SaveAuditInfo("Add", "Add Name");
                    txtName.Text = "";
                    LoadNameList();
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
            LoadNameList();
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
            LoadNameList();
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
            LoadNameList();
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
            Label lblId = (Label)grvEduIns.Rows[e.RowIndex].FindControl("Label1");
            string strId = lblId.Text;
            TextBox txtName = (TextBox)grvEduIns.Rows[e.RowIndex].FindControl("TextBox2");
            string strName = txtName.Text;

            string strUpdateName = objServiceHandler.UpdateTstInstituteInfo(strId, strName);
            if (strUpdateName == "Successfull.")
            {
                lblMsg.Text = "Saved Successfully";
                LoadNameList();
                SaveAuditInfo("Update", "Name");
            }
            else
            {
                lblMsg.Text = strUpdateName;
            }

            grvEduIns.EditIndex = -1;
            LoadNameList();
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

    private void LoadNameList()
    {
        try
        {
            string strSql = " SELECT TEST_EDU_INS_PK_ID, TEST_EDU_INS_NAME FROM TEST_EDUCATIONAL_INSTITUTE ORDER BY TEST_EDU_INS_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvEduIns.DataSource = oDataSet;
            grvEduIns.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

   

    private void LoadName()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT AREA_ID, AREA_NAME FROM MANAGE_AREA ORDER BY AREA_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

}
