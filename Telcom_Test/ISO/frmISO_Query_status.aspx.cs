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

public partial class COM_frmISO_Query_status : System.Web.UI.Page

{  
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
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
                //strUserName = Session["UserLoginName"].ToString();
                //strPassword = Session["Password"].ToString();
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
    public void LoadRequestList()
    {
        string strSQL = "";
        strSQL = " SELECT distinct CDT.*,IR.ISO_RESPONSE_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,APSNG101.ISO_REQUEST IR WHERE "
                      + " CDT.CAS_DPS_ID=IR.ISO_CLIENT_REQ_ID AND DPS_OWNER='MBL' AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" 
                      + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" 
                      + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') ";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtSubscriberNo.Text.Equals(""))
        {
            strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
        }
        //if (!txtServiceCode.Text.Equals(""))
        //{
        //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        //}
        strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";

        sdsStatus.SelectCommand = strSQL;
        sdsStatus.DataBind();
        gdvstatus.DataBind();
        if (gdvstatus.Rows.Count > 0)
        {
 
        }
    }
    //public void LoadRequestList()
    //{
    //    string strSQL = "SELECT * FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION WHERE DPS_OWNER='MBL' AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
    //    if (!txtRequestParty.Text.Equals(""))
    //    {
    //        strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
    //    }
    //    if (!txtSubscriberNo.Text.Equals(""))
    //    {
    //        strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
    //    }
    //    //if (!txtServiceCode.Text.Equals(""))
    //    //{
    //    //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
    //    //}
    //    strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";

    //    sdsStatus.SelectCommand = strSQL;
    //    sdsStatus.DataBind();
    //    gdvstatus.DataBind();

    //}
    protected void gdvstatus_PageIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
}
