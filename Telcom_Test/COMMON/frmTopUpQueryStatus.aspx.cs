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

public partial class COMMON_frmTopUpQueryStatus : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try
            {                
                DateTime dt = DateTime.Now;
                if (dptFromDate.DateString != "")
                {
                    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-120));
                    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
                    LoadRequestList();
                }
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
    public void LoadRequestList()
    {
        string strSQL = "";
        strSQL = " SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE,"
                     + " TRAN_AMOUNT,REQUEST_STATUS,SUCCESSFUL_STATUS,SSL_VRG_UNIQUE_ID,OPERATOR_CODE,SSL_CREATE_MESSAGE,SSL_INT_MESSAGE,"
                     + " SSL_FINAL_MESSAGE,REVERSE_STATUS,REVERSE_NOTE,OWNER_CODE FROM TOPUP_TRANSACTION "
                     + " WHERE TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'"
                     + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND SOURCE_ACCNT_NO LIKE '%" + txtRequestParty.Text + "%'";
        }
        else if (!txtSubscriberNo.Text.Equals(""))
        {
            strSQL = strSQL + " AND SUBSCRIBER_MOBILE_NO LIKE '%" + txtSubscriberNo.Text + "%'";
        }
        else if (!txtOwnerCode.Text.Equals(""))
        {
            strSQL = strSQL + " AND OWNER_CODE LIKE UPPER('%" + txtOwnerCode.Text + "%')";
        }
        strSQL = strSQL + " ORDER BY TRAN_DATE DESC";
        try
        {
            sdsRequest.SelectCommand = strSQL;
            sdsRequest.DataBind();
            gdvStatus.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        clsServiceHandler objSerHandler = new clsServiceHandler();
        DataSet oDS = new DataSet();
        string strHTML = "";
        string filename = "Query_Status";
        string strSQL = "";
        strSQL = " SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,SUBSCRIBER_TYPE,"
                     + " TRAN_AMOUNT,REQUEST_STATUS,SUCCESSFUL_STATUS,SSL_VRG_UNIQUE_ID,OPERATOR_CODE,SSL_CREATE_MESSAGE,SSL_INT_MESSAGE,"
                     + " SSL_FINAL_MESSAGE,REVERSE_STATUS,REVERSE_NOTE,OWNER_CODE FROM TOPUP_TRANSACTION "
                     + " WHERE TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'"
                     + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND SOURCE_ACCNT_NO LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtSubscriberNo.Text.Equals(""))
        {
            strSQL = strSQL + " AND SUBSCRIBER_MOBILE_NO LIKE '%" + txtSubscriberNo.Text + "%'";
        }
        strSQL = strSQL + " ORDER BY TRAN_DATE DESC";
        oDS = objSerHandler.GetDataSet(strSQL);

        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none' expan=true><h2 align=center>Topup Query Status </h2></td></tr>";
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";

        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td valign='middle' >Serial No</td>";
		strHTML = strHTML + "<td valign='middle' >Transaction ID</td>";
        strHTML = strHTML + "<td valign='middle' >Request ID</td>";
        strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
        strHTML = strHTML + "<td valign='middle' >Source Wallet</td>";
        strHTML = strHTML + "<td valign='middle' >Subscriber Mobile Number</td>";
        strHTML = strHTML + "<td valign='middle' >Subscriber Type</td>";
        strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
        strHTML = strHTML + "<td valign='middle' >GUID</td>";
        strHTML = strHTML + "<td valign='middle' >Create Message</td>";
        strHTML = strHTML + "<td valign='middle' >Initiate Message</td>";
        strHTML = strHTML + "<td valign='middle' >Final Message</td>";
        strHTML = strHTML + "<td valign='middle' >Owner Code </td>";
        strHTML = strHTML + "<td valign='middle' >Reverse Status </td>";
        strHTML = strHTML + "</tr>";

        if (oDS.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in oDS.Tables["Table1"].Rows)
            {
                strHTML = strHTML + "<tr>";
                string activetiondate = prow["TRAN_DATE"].ToString().Trim() == "" ? "" : String.Format("{0:dd-MMM-yyyy}", DateTime.Parse(prow["TRAN_DATE"].ToString()));

                strHTML = strHTML + "<td valign='middle' >'" + SerialNo.ToString() + "</td>";
				strHTML = strHTML + "<td valign='middle' >'" + prow["TOPUP_TRAN_ID"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >'" + prow["REQUEST_ID"].ToString() + "</td>";
				strHTML = strHTML + "<td valign='middle' >'" + prow["TRAN_DATE"].ToString() + "</td>";
                //strHTML = strHTML + " <td > " + activetiondate + " </td>";
                strHTML = strHTML + "<td valign='middle' >'" + prow["SOURCE_ACCNT_NO"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >'" + prow["SUBSCRIBER_MOBILE_NO"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >'" + prow["SUBSCRIBER_TYPE"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >'" + prow["TRAN_AMOUNT"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + prow["SSL_VRG_UNIQUE_ID"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + prow["SSL_CREATE_MESSAGE"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + prow["SSL_INT_MESSAGE"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + prow["SSL_FINAL_MESSAGE"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + prow["OWNER_CODE"].ToString() + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + prow["REVERSE_STATUS"].ToString() + "</td>";
                SerialNo= SerialNo+1;
            }
        }
        strHTML = strHTML + "</table>";
        clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");       
    }
    protected void gdvStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {       
        LoadRequestList();           
    }
}
