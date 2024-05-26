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
using System.Drawing;

public partial class COMMON_frmTopupRequestReSubmit : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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
                    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(-1));
                    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);
                    LoadRequestList();
                }
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch(Exception ex)
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

        strSQL = " SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,"
               + " SUBSCRIBER_TYPE,TRAN_AMOUNT,REQUEST_STATUS,SUCCESSFUL_STATUS,OPERATOR_CODE,"
               + " SSL_CREATE_RECHAGE_STATUS,SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,SSL_INT_MESSAGE,"
               + " SSL_FINAL_STATUS,SSL_FINAL_MESSAGE,REVERSE_STATUS,OWNER_CODE  FROM TOPUP_TRANSACTION WHERE "
               + " REVERSE_STATUS='N' AND RESUBMIT_STATUS='N' AND ALL_FINAL_STATUS='F' AND "   //SSL_FINAL_STATUS NOT IN ('900','200')
               + " SSL_FINAL_STATUS IS NOT NULL AND TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
               + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString
               + "\',\'dd/mm/yyyy HH24:MI:SS\') ";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND SOURCE_ACCNT_NO LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtSubscriberNo.Text.Equals(""))
        {
            strSQL = strSQL + " AND SUBSCRIBER_MOBILE_NO LIKE '%" + txtSubscriberNo.Text + "%'";
        }
        strSQL = strSQL + " ORDER BY TRAN_DATE DESC";
        try
        {
            sdsRequest.SelectCommand = strSQL;
            sdsRequest.DataBind();
            gdvRequest.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e) // Resubmit
    {
         List<string> lstGetValue = new List<string>();
        //Get the button that raised the event
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        for (int intCnt = 0; intCnt < gvr.Controls.Count; intCnt++)
        {
            int intCountRow = gvr.RowIndex;
            lstGetValue.Add(gdvRequest.Rows[intCountRow].Cells[intCnt].Text);
        }
        string strTOPUP_TRAN_ID = lstGetValue[0].ToString();
        string strREQUEST_ID = lstGetValue[1].ToString();
        string strTRAN_DATE = lstGetValue[2].ToString();
        string strSourceWallet = lstGetValue[3].ToString();
        string strSubscriberNo = lstGetValue[4].ToString();
        string strAmount = lstGetValue[6].ToString();
        string strREQUEST_STATUS = lstGetValue[7].ToString();
        string strSUCCESSFUL_STATUS = lstGetValue[8].ToString();
        string strSSL_CREATE_RECHAGE_STATUS = lstGetValue[10].ToString();
        string strSSL_CREATE_MESSAGE = lstGetValue[11].ToString();
        string strSSL_INT_RECHAGE_STATUS = lstGetValue[12].ToString();
        string strSSL_INT_MESSAGE = lstGetValue[13].ToString();
        string strSSL_FINAL_STATUS = lstGetValue[14].ToString();
        string strSSL_FINAL_MESSAGE = lstGetValue[15].ToString();
        //// Change topup trans ID 
       // string strTopupTransID = objServiceHandler.ChangeTopuptrnaID(strTOPUP_TRAN_ID);
        //string strTopupTransID = "SELECT SUBSTR(TOPUP_TRAN_ID,1,7)+1|| SUBSTR(TOPUP_TRAN_ID,8,18)NEW_TOPUP_TRAN_ID FROM TOPUP_TRANSACTION WHERE TOPUP_TRAN_ID='"+strTOPUP_TRAN_ID+"'";
       // bool strUpdateRequest = objServiceHandler.ReSendTopupRequest(strTopupTransID, strREQUEST_ID, strTRAN_DATE, strREQUEST_STATUS, strSUCCESSFUL_STATUS, strSSL_CREATE_RECHAGE_STATUS, strSSL_CREATE_MESSAGE, strSSL_INT_RECHAGE_STATUS, strSSL_INT_MESSAGE, strSSL_FINAL_STATUS, strSSL_FINAL_MESSAGE);
        string strSQLProce = "", strMsgPro = "";
        strSQLProce = " PKG_USSD_SERVICE.TOPUP_TRANSACTION_RESUBMIT('" + strTOPUP_TRAN_ID + "') ";
        strMsgPro = objServiceHandler.ExecuteProcedure(strSQLProce);
        LoadRequestList();
        string strReversedNote = objServiceHandler.ResubmitNote(strTOPUP_TRAN_ID);// resubmit note 
        if (!strReversedNote.Equals(""))
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Resubmit Successfully";
        }        
        LoadRequestList();
        string strRemarks = "Resubmit :" + strReversedNote + ",Trans_ID :" + strTOPUP_TRAN_ID + ", Request_ID :" + strREQUEST_ID + ",Subscriber_No:" + strSubscriberNo + ",Amount:" + strAmount;
        SaveAuditInfo("Resubmit", strRemarks);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT TOPUP_TRAN_ID, REQUEST_ID,TRAN_DATE,SOURCE_ACCNT_NO,SUBSCRIBER_MOBILE_NO,"
               + " SUBSCRIBER_TYPE,TRAN_AMOUNT,REQUEST_STATUS,SUCCESSFUL_STATUS,OPERATOR_CODE,"
               + " SSL_CREATE_RECHAGE_STATUS,SSL_CREATE_MESSAGE,SSL_INT_RECHAGE_STATUS,SSL_INT_MESSAGE,"
               + " SSL_FINAL_STATUS,SSL_FINAL_MESSAGE,REVERSE_STATUS,OWNER_CODE  FROM TOPUP_TRANSACTION WHERE "
               + " REVERSE_STATUS='N' AND RESUBMIT_STATUS='N' AND ALL_FINAL_STATUS='F' AND "   //SSL_FINAL_STATUS NOT IN ('900','200')
               + " SSL_FINAL_STATUS IS NOT NULL AND TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
               + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString
               + "\',\'dd/mm/yyyy HH24:MI:SS\') ";

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Resubmit_Topup_Rpt";
            string strDate = dptFromDate.DateString + " To " + dptFromDate.DateString;
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none' expan=true><h2 align=center> Topup Failed Status </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction ID</td>";
            strHTML = strHTML + "<td valign='middle' >Request ID</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Source Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Subscriber Mobile Number</td>";
            strHTML = strHTML + "<td valign='middle' >Subscriber Type</td>";

            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";

            strHTML = strHTML + "<td valign='middle' >Create Message</td>";
            strHTML = strHTML + "<td valign='middle' >Initiate Message</td>";
            strHTML = strHTML + "<td valign='middle' >Final Message</td>";
            strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOPUP_TRAN_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUBSCRIBER_MOBILE_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SUBSCRIBER_TYPE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRAN_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SSL_CREATE_MESSAGE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SSL_INT_MESSAGE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SSL_FINAL_STATUS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Resubmit_Topup_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void gdvRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvRequest.PageIndex = e.NewPageIndex;
        gdvRequest.SelectedIndex = -1;
        LoadRequestList();
    }
    protected void Button2_Click(object sender, EventArgs e) // Reversed
    {
        List<string> lstGetValue = new List<string>();

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        for (int intCnt = 0; intCnt < gvr.Controls.Count; intCnt++)
        {
            int intCountRow = gvr.RowIndex;
            lstGetValue.Add(gdvRequest.Rows[intCountRow].Cells[intCnt].Text);
        }
        string strTOPUP_TRAN_ID = lstGetValue[0].ToString();
        string strREQUEST_ID = lstGetValue[1].ToString();
        string strTRAN_DATE = lstGetValue[2].ToString();
        string strSourceWallet=lstGetValue[3].ToString();
        string strSubscriberNo=lstGetValue[4].ToString();
        string strAmount=lstGetValue[6].ToString();
        string strREQUEST_STATUS = lstGetValue[7].ToString();
        string strSUCCESSFUL_STATUS = lstGetValue[8].ToString();
        string strSSL_CREATE_RECHAGE_STATUS	=lstGetValue[10].ToString();
        string strSSL_CREATE_MESSAGE = lstGetValue[11].ToString();
        string strSSL_INT_RECHAGE_STATUS = lstGetValue[12].ToString();
        string strSSL_INT_MESSAGE = lstGetValue[13].ToString();
        string strSSL_FINAL_STATUS = lstGetValue[14].ToString();
        string strSSL_FINAL_MESSAGE = lstGetValue[15].ToString();

        string strSQLProce = "", strMsgPro = "", strOutput="P_MSG";
        strSQLProce = " PKG_USSD_SERVICE.TOPUP_TRANSACTION_REVERSE('" + strREQUEST_ID + "') ";
        strMsgPro = objServiceHandler.ExecuteProcedure(strSQLProce);
        LoadRequestList();
        string strReversedNote = objServiceHandler.ReversedNote(strTOPUP_TRAN_ID);//reverse note 
        lblMsg.Visible = true;
        lblMsg.Text = strReversedNote;
        string strRemarks = "Result:" + strReversedNote + ",Trans_ID :" + strTOPUP_TRAN_ID + ", Request_ID :" + strREQUEST_ID + ",Subscriber_No:" + strSubscriberNo + ",Amount:" + strAmount;
        SaveAuditInfo("Reverse", strRemarks);
    }   
    protected void gdvRequest_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LoadRequestList();
    }
    protected void gdvRequest_RowEditing(object sender, GridViewEditEventArgs e)
    {
        LoadRequestList();
    }
    protected void gdvRequest_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        LoadRequestList();
        //--------update note -------------
        string strTopupTrnasId = gdvRequest.Rows[0].Cells[0].Text.ToString();
        string strRequest_ID = gdvRequest.Rows[0].Cells[1].Text.ToString();
        string strOldvalue = e.OldValues["SUBSCRIBER_TYPE"].ToString();
        string strNewValue = e.NewValues["SUBSCRIBER_TYPE"].ToString();
        objServiceHandler.UpdateTopupNote(strOldvalue,strNewValue,strTopupTrnasId);        
        string strUpdate = "Update: " + strOldvalue + "-to-" + strNewValue +",TopupTrans_ID:"+strTopupTrnasId+",Request_ID:"+strRequest_ID;
        SaveAuditInfo("Update",strUpdate);       
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvRequest_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e) // Refresh
    {
        List<string> lstGetValue = new List<string>();

        //Get the button that raised the event
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        for (int intCnt = 0; intCnt < gvr.Controls.Count; intCnt++)
        {
            int intCountRow = gvr.RowIndex;
            lstGetValue.Add(gdvRequest.Rows[intCountRow].Cells[intCnt].Text);
            
        }
        string strTOPUP_TRAN_ID = lstGetValue[0].ToString();

        string strOWNER_CODE = lstGetValue[17].ToString();
        string strRefresh = objServiceHandler.Getrefresh(strTOPUP_TRAN_ID, strOWNER_CODE);       
        lblMsg.Visible = true;
        lblMsg.Text = strRefresh;
        LoadRequestList();
    }
    protected void gdvRequest_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        gdvRequest.PageIndex = e.NewSelectedIndex;
        gdvRequest.DataBind();
    }
}
