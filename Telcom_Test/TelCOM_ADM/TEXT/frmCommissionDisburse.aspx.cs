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

public partial class TEXT_frmCommissionDisburse : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
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
            sdsDisburseList.DataBind();
            GridView1.DataBind();
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strVal;
        // Get the currently selected row using the SelectedRow property.
        GridViewRow row = GridView1.SelectedRow;

        // Display the company name from the selected row.
        // In this example, the third column (index 2) contains
        // the company name.
        //Session["ReportFile"] = row.Cells[3].Text;
        //strVal = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
        //strVal = row.Cells[3].Text;
        //Response.Redirect("frmReportView.aspx");
        //String strSQL = "SELECT SR.CLINT_NAME,CLINT_ADDRESS1,SR.CLINT_ADDRESS2,SR.SERVICE_TITLE,SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,"
        //              + " SR.REQUEST_TIME,SR.SERVICE_REWARD,SR.REQ_TEXT,SR.SERVICE_COST,SR.ACCESS_CODE,"
        //              + " SUM(SR.REQ_IN_QUE) REQ_IN_QUE,SUM(SR.REQ_DONE) REQ_DONE,SUM(SR.RSP_SUCESS) RSP_SUCESS,"
        //              + " SUM(SR.RSP_FAILED)RSP_FAILED,SUM(SR.RSP_Waiting) RSP_Waiting,"
        //              + " SUM(SR.SERVICE_REWARD*SR.RSP_SUCESS) COMISSION,'" + GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text + "' FROM_DATE,'" + GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text + "' TODATE "
        //              + " FROM ALL_FORWARD_REQ_RECONCIL SR WHERE SR.SRV_REWARD_DISB_ID='" + GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text + "'"
        //              + " GROUP BY SR.CLINT_NAME,CLINT_ADDRESS1,SR.CLINT_ADDRESS2,SR.SERVICE_TITLE,SR.REQUEST_ID,SR.REQUEST_PARTY,"
        //              + " SR.RECEIPENT_PARTY,SR.REQUEST_TIME,SR.SERVICE_REWARD,SR.REQ_TEXT,SR.SERVICE_COST,SR.ACCESS_CODE";

        String strSQL = "SELECT SR.CLINT_NAME,CLINT_ADDRESS1,SR.CLINT_ADDRESS2,SR.SERVICE_TITLE,SR.REQUEST_PARTY,"
                     + " SUM(SR.REQ_IN_QUE) REQ_IN_QUE,SUM(SR.REQ_DONE) REQ_DONE,SUM(SR.REQ_EXPIRED ) REQ_EXPIRED,SUM(SR.RSP_SUCESS) RSP_SUCESS,"
                     + " SUM(SR.RSP_FAILED)RSP_FAILED,SUM(SR.RSP_Waiting) RSP_Waiting,"
                     + " SUM(NVL(SR.SERVICE_REWARD,0)*SR.RSP_SUCESS) COMISSION,'" + GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text + "' FROM_DATE,'" + GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text + "' TODATE "
                     + " FROM ALL_FORWARD_REQ_RECONCIL SR WHERE SR.SRV_REWARD_DISB_ID='" + GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text + "'"
                     + " GROUP BY SR.CLINT_NAME,CLINT_ADDRESS1,SR.CLINT_ADDRESS2,SR.SERVICE_TITLE,SR.REQUEST_PARTY";
        try
        {
            Session["CompanyBranch"] = "ROBI";
            Session["ReportSQL"] = strSQL;
            Session["RequestForm"] = "../TEXT/frmCommissionDisburse.aspx";
            Session["ReportFile"] = "../TEXT/crptCommissionDisburse.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
            SaveAuditInfo("Preview", "Disbursement History");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        // Get the currently selected row. Because the SelectedIndexChanging event
        // occurs before the select operation in the GridView control, the
        // SelectedRow property cannot be used. Instead, use the Rows collection
        // and the NewSelectedIndex property of the e argument passed to this 
        // event handler.
        GridViewRow row = GridView1.Rows[e.NewSelectedIndex];

        // You can cancel the select operation by using the Cancel
        // property. For this example, if the user selects a customer with 
        // the ID "ANATR", the select operation is canceled and an error message
        // is displayed.
        //if (row.Cells[1].Text == "ANATR")
        //{

        //    e.Cancel = true;
        //    MessageLabel.Text = "You cannot select " + row.Cells[2].Text + ".";

        //}

    }
}
