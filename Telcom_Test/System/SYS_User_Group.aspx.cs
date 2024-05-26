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

public partial class System_SYS_User_Group : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void dtvSysUsrGroup_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Insert", "System User Group");
    }
    protected void gdvSysUsrGroup_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        SaveAuditInfo("Delete", "System User Group");
    }
    protected void gdvSysUsrGroup_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SaveAuditInfo("Update", "System User Group");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }    
}
