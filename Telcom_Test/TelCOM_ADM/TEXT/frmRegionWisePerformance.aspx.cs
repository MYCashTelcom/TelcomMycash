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

public partial class TEXT_frmRegionWisePerformance : System.Web.UI.Page
{
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
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
        DateTime dt = DateTime.Now;
        if (txtFromDate.Text.Equals(""))
        {
            txtFromDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(-1 * (dt.Day - 1)));
            txtToDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);
            //LoadRequestList();
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

        String strSQL = "SELECT SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,"
                + "DECODE(SR.REQUEST_STAE,'P','In Que','Done') REQ_STATE,"
                + "SR.REQUEST_TEXT REQ_TEXT,"
                + "SR.SERVICE_COST,PKG_SMSGW_SERVICES.GET_VAS_ACTIVATION_STATUS(SR.REQUEST_ID) RSP_STATE"
                + " FROM ALL_SERVICE_REQUEST SR WHERE SUBSTR(SR.SERVICE_INTERNAL_CODE,1,4)='REQF' "
                + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + txtFromDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.Text + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        //if (!txtRequestParty.Text.Equals(""))
        //{
        //    strSQL = strSQL + " AND SR.REQUEST_PARTY LIKE '%" + txtRequestParty.Text + "%'";
        //}
        //if (!txtServiceCode.Text.Equals(""))
        //{
        //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        //}
        //AND "
        //+ " SR.ACCNT_ID='" + ddlAccountList.SelectedValue + "' ";

        //sdsRequestList.SelectCommand = strSQL;
        //sdsRequestList.DataBind();
        //grvRequestList.DataBind();
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
        string strFilter = "";

        if (!chkAllZone.Checked)
        {
            strFilter = " AND CZ.CLI_ZONE_ID='" + ddlZone.SelectedValue.ToString() + "' ";
        }
        //if (!txtRequestParty.Text.ToString().Equals(""))
        //{
        //    strFilter = " AND SR.REQUEST_PARTY='" + txtRequestParty.Text.ToString().Trim() + "' ";
        //}
        String strSQL = "SELECT CZ.CLI_ZONE_TITLE,SR.SERVICE_TITLE,"
                     + " SUM(SR.REQ_IN_QUE) REQ_IN_QUE,SUM(SR.REQ_DONE) REQ_DONE,SUM(SR.REQ_EXPIRED ) REQ_EXPIRED,SUM(SR.RSP_SUCESS) RSP_SUCESS,"
                     + " SUM(SR.RSP_FAILED)RSP_FAILED,SUM(SR.RSP_Waiting) RSP_Waiting,"
                     + " SUM(NVL(SR.SERVICE_REWARD,0)*SR.RSP_SUCESS) COMISSION,'" + txtFromDate.Text + "' FROM_DATE,'" + txtToDate.Text + "' TODATE "
                     + " FROM ALL_FORWARD_REQ_RECONCIL SR,ACCOUNT_LIST AL,CLIENT_LIST CL,CLIENT_ZONE CZ WHERE AL.ACCNT_MSISDN=SR.REQUEST_PARTY AND AL.CLINT_ID=CL.CLINT_ID"
                     + " AND CL.CLI_ZONE_ID=CZ.CLI_ZONE_ID AND SR.REQUEST_TIME BETWEEN TO_DATE('" + txtFromDate.Text + "','dd/mm/yyyy HH24:MI:SS') "
                     + " AND TO_DATE('" + txtToDate.Text + "','dd/mm/yyyy HH24:MI:SS') " + strFilter
                     + " GROUP BY CZ.CLI_ZONE_TITLE,SR.SERVICE_TITLE";

        Session["CompanyBranch"] = "ROBI";
        Session["ReportSQL"] = strSQL;
        Session["RequestForm"] = "../TEXT/frmRegionWisePerformance.aspx";
        Session["ReportFile"] = "../TEXT/crptZonePerformance.rpt";
        Response.Redirect("../COM/COM_ReportView.aspx");    


    }
}
