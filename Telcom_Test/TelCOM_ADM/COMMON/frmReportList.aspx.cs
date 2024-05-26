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

public partial class Forms_frmReportList : System.Web.UI.Page
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
        string strVal;
        // Get the currently selected row using the SelectedRow property.
        GridViewRow row = GridView1.SelectedRow;

        // Display the company name from the selected row.
        // In this example, the third column (index 2) contains
        // the company name.
        Session["ReportFile"] = row.Cells[3].Text;
        strVal = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
        strVal = row.Cells[3].Text;
        Response.Redirect("frmReportView.aspx");
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
