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

public partial class TEXT_frmCommissionDisburs : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;
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

            if (txtFromDate.Text.Equals(""))
            {
                txtFromDate.Text = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1 * (dt.Day - 1)));
                txtToDate.Text = String.Format("{0:dd-MMM-yyyy}", dt);
                LoadRequestList();
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
    public void LoadRequestList()
    {
       
        String strSQL = "SELECT SR.SERVICE_TITLE,SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,"
                + "DECODE(SR.REQUEST_STAE,'P','In Que','E','Expired','Done') REQ_STATE,"
                + "SR.REQUEST_TEXT REQ_TEXT,"
                + "SR.SERVICE_REWARD,PKG_SMSGW_SERVICES.GET_VAS_ACTIVATION_STATUS(SR.REQUEST_ID) RSP_STATE"
                + " FROM ALL_SERVICE_REQUEST SR WHERE SR.SRV_REWARD_DISB_ID IS NULL AND SUBSTR(SR.SERVICE_INTERNAL_CODE,1,4)='REQF' "
                + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";
      
        sdsRequestList.SelectCommand = strSQL;
        sdsRequestList.DataBind();
        grvRequestList.DataBind();
    }
    protected void ddlAccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        String strSQL = "SELECT SR.SERVICE_TITLE,SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,"
                 + "DECODE(SR.REQUEST_STAE,'P','In Que','E','Expired','Done') REQ_STATE,"
                 + "SR.REQUEST_TEXT REQ_TEXT,"
                 + "SR.SERVICE_REWARD,PKG_SMSGW_SERVICES.GET_VAS_ACTIVATION_STATUS(SR.REQUEST_ID) RSP_STATE"
                 + " FROM ALL_SERVICE_REQUEST SR WHERE SR.SRV_REWARD_DISB_ID IS NULL AND SUBSTR(SR.SERVICE_INTERNAL_CODE,1,4)='REQF' "
                 + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        try
        {
            Session["CompanyBranch"] = "ROBI";
            Session["ReportSQL"] = strSQL;
            Session["RequestForm"] = "../TEXT/frmReconciliation.aspx";
            Session["ReportFile"] = "../TEXT/crptActiveReconcilation.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
            SaveAuditInfo("Preview", "Commission Disburse");
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
    protected void btnDisburse_Click(object sender, EventArgs e)
    {
        clsServiceHandler objSrvHandler=new clsServiceHandler();
        ArrayList arrReqID = new ArrayList();
        if (txtDisbureNoteNo.Text.ToString().Equals(""))
        {
            txtDisbureNoteNo.Text = "Note can not be empty";
            return;
            //LoadRequestList();
        }
        foreach (GridViewRow row in grvRequestList.Rows)
        {
            if (!(row.Cells[0].Text.Equals("&nbsp;")))
            {
                arrReqID.Add(row.Cells[0].Text.ToString());               
            }

        }
        string strReturn;
        strReturn = objSrvHandler.DisburseCommission(txtFromDate.Text, txtToDate.Text, arrReqID, txtDisbureNoteNo.Text);
        LoadRequestList();
        SaveAuditInfo("Insert", "Commission Disburse");
    }
}
