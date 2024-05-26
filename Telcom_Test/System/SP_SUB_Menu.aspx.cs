using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class System_SP_SUB_Menu : System.Web.UI.Page 
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void dtvSystemMenu_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Insert","System Menu Manager");
        string strSQL = "";
        strSQL = "SELECT * FROM SERVICE_PACKAGE_SUB_MENU WHERE (SERVICE_PKG_ID=:SERVICE_PKG_ID)  ORDER BY SERVICE_PKG_ID,SP_DISPLAY_ORDER ASC";
        try
        {
            sdsSPSubMenu.SelectCommand = strSQL;
            sdsSPSubMenu.DataBind();
            grvSystemMenu.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void SaveAuditInfo(string strOperationType,string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void grvSystemMenu_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SaveAuditInfo("Delete", "System Menu Manager");
    }
    protected void grvSystemMenu_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "System Menu Manager");
    }
    protected void dtvSystemMenu_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        e.Values["SERVICE_PKG_ID"] = ddlServicePackage.SelectedValue.ToString();
        clsServiceHandler objSerHdlr = new clsServiceHandler();
        string strParentCode="";
        //----------------------------------- Inserting Parent Code -------------------------------
        DropDownList ddlParentCode = (DropDownList)dtvSystemMenu.FindControl("DropDownList9");
        strParentCode = objSerHdlr.ReturnOneColumnValueByAnotherColumnValue("SERVICE_PACKAGE_SUB_MENU", "SP_DISPLAY_ORDER", "SP_MENU_ID", ddlParentCode.SelectedValue.ToString());
        e.Values["SP_MENU_PARENT_CODE"] =strParentCode.ToString();        
    }
}
