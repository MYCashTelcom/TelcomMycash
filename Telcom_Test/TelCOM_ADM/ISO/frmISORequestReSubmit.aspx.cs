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
using System.Collections.Generic;

public partial class COM_frmISORequestReSubmit : System.Web.UI.Page
{
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objReport = new clsServiceHandler();
   // clsLogWritter objLogWriter = new clsLogWritter();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try
            {
                // strAccount_ID = Session["AccountID"].ToString();
                DateTime dt = DateTime.Now;
                //Session.Timeout = 10;
                if (dptFromDate.DateString != "")
                {

                    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(-1));
                    // txtFromDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-120));
                    // txtToDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
                    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);
                    LoadRequestList();
                }
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch (Exception ex)
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
        LoadRequestList();

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

    }
    public void LoadRequestList()
    {
        string strSQL = "";
        strSQL = " SELECT distinct CDT.*,IR.ISO_RESPONSE_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,APSNG101.ISO_REQUEST IR WHERE "
              + " CDT.CAS_DPS_ID=IR.ISO_CLIENT_REQ_ID AND DPS_OWNER='MBL' AND  CAS_ISO_REQ_STATUS='F' "
              + " AND CAS_DPS_RESUBMIT='N' AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" 
              + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" 
              + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') ";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%' ";
        }
        if (!txtSubscriberNo.Text.Equals(""))
        {
            strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%' ";
        }       
        strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC ";
        try
        {
            sdsFailed.SelectCommand = strSQL;
            sdsFailed.DataBind();
            gdvFailed.DataBind();
            if (gdvFailed.Rows.Count > 0)
            {
 
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }    
    protected void btnResubmit_Click(object sender, EventArgs e)
    {
        List<string> lstGetValue = new List<string>();

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        for (int intCnt = 0; intCnt < gvr.Controls.Count; intCnt++)
        {
            int intCountRow = gvr.RowIndex;
            lstGetValue.Add(gdvFailed.Rows[intCountRow].Cells[intCnt].Text);
        }
        string strTOPUP_TRAN_ID = lstGetValue[0].ToString();
        string strREQUEST_ID = lstGetValue[1].ToString();
        string strAccountID = lstGetValue[2].ToString();       
        string strSubscriberNo = lstGetValue[7].ToString();
        string strAmount = lstGetValue[6].ToString();      

        string strSQLProce = "", strMsgPro = "";
        strSQLProce = " PKG_ISO_SERVICE.PRO_ISO_TRAN_RESUBMIT('" + strTOPUP_TRAN_ID + "') ";
        strMsgPro = objReport.ExecuteProcedure(strSQLProce);
        string strRemarks = "Resubmit :" + strMsgPro + ",Trans_ID :" + strTOPUP_TRAN_ID + ", Request_ID :" + strREQUEST_ID + ",Subscriber_No:" + strSubscriberNo + ",Amount:" + strAmount;

        //SaveAuditInfo("Resubmit", strRemarks);
        LoadRequestList();
        lblMsg.Visible = true;
        lblMsg.Text = strMsgPro;
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvFailed_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void gdvFailed_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       // LoadRequestList();
    }
    protected void gdvFailed_PageIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
}
