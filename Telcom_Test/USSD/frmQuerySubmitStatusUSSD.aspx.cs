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

public partial class Forms_frmQuerySubmitStatusUSSD : System.Web.UI.Page
{
    //MODIFIED BY KOWSHIK START HERE(29-AUG-2012)
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;
        if (!IsPostBack)
        {
            if (dptFromDate.DateString != "")
            {
                dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-120));
                dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
                //txtFromDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-120));
                //txtToDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
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
        string strSQL = " SELECT SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,"
                + " DECODE(SR.REQUEST_STAE,'P','In Que','E','Expired','Done') REQ_STATE,"
            //+ "REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'TXTFS*') REQ_TEXT,"
                + " DECODE(INSTR((REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'TXTFS*')),'PIN'),0, "
                + " REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'TXTFS*'),SUBSTR(SR.REQUEST_TEXT,0,37)||'...') REQ_TEXT,"
                + " SR.SERVICE_COST,RSP.RESPONSE_TIME,RSP.RESPONSE_ID,"
                + " DECODE(RSP.RESPONSE_STAE,'D','Replied','P','In Que',NULL,DECODE(UPPER(SR.REQUEST_STAE),'E','Canceled',DECODE(UPPER(SR.REQUEST_TEXT),'Y','N/A','Waiting')),"
                + " DECODE(UPPER(SR.REQUEST_STAE),'E','Canceled','Replied')) RSP_STATE,"
            //+ "DECODE(RSP.RESPONSE_MESSAGE,null,'',SUBSTR(RSP.RESPONSE_MESSAGE,1,1000)||DECODE(INSTR(SR.REQUEST_TEXT,'TXTFS'),2,'','')) RESPONSE_MESSAGE "
                + " DECODE(INSTR(DECODE(RSP.RESPONSE_MESSAGE,null,'', SUBSTR(RSP.RESPONSE_MESSAGE,1,1000)||DECODE(INSTR(SR.REQUEST_TEXT,'TXTFS'),2,'','')),'token'),0, "
                + " DECODE(INSTR(DECODE(RSP.RESPONSE_MESSAGE,null,'', SUBSTR(RSP.RESPONSE_MESSAGE,1,1000)||DECODE(INSTR(SR.REQUEST_TEXT,'TXTFS'),2,'','')),'PIN'),0, "
                + " DECODE(RSP.RESPONSE_MESSAGE,null,'', SUBSTR(RSP.RESPONSE_MESSAGE,1,1000)||DECODE(INSTR(SR.REQUEST_TEXT,'TXTFS'),2,'','')), "
                + " SUBSTR(DECODE(RSP.RESPONSE_MESSAGE,null,'', SUBSTR(RSP.RESPONSE_MESSAGE,1,1000)||DECODE(INSTR(SR.REQUEST_TEXT,'TXTFS'), "
                + " 2,'','')),0,30)||'...') ,SUBSTR(DECODE(RSP.RESPONSE_MESSAGE,null,'', SUBSTR(RSP.RESPONSE_MESSAGE,1,1000)||"
                + " DECODE(INSTR(SR.REQUEST_TEXT,'TXTFS'),2,'','')),0,71)||'...'|| SUBSTR(DECODE(RSP.RESPONSE_MESSAGE,null,'', "
                + " SUBSTR(RSP.RESPONSE_MESSAGE,1,1000)||DECODE(INSTR(SR.REQUEST_TEXT,'TXTFS'),2,'','')),79,155)) RESPONSE_MESSAGE"
                + " FROM SERVICE_REQUEST SR,SERVICE_RESPONSE RSP WHERE SR.REQUEST_ID=RSP.REQUEST_ID(+) "
                + " AND SR.RECEIPENT_PARTY='AIRTEL_USSD' AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        //AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2))<>'REG'
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND SR.REQUEST_PARTY LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtServiceCode.Text.Equals(""))
        {
            strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        }
        strSQL = strSQL + " ORDER BY SR.REQUEST_TIME DESC ";
        try
        {
            sdsRequestList.SelectCommand = strSQL;
            sdsRequestList.DataBind();
            grvRequestList.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ddlAccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    //MODIFIED BY Bushra 6/3/13
    protected void btnExport_Click(object sender, EventArgs e)
    {
       // string strRequest = objServiceHandler.GetQueryResult(txtFromDate.Text, txtToDate.Text, txtRequestParty.Text, txtServiceCode.Text);
        string strSQL = "";
        int intCountGrd = grvRequestList.Rows.Count;
        try
        {
            string strHTML = "";
            string filename = "USSD_Query_Status";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none' expan=true><h2 align=center> USSD Query Status </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + "<td valign='middle' >Request ID</td>";
            strHTML = strHTML + "<td valign='middle' >Request Party</td>";
            strHTML = strHTML + "<td valign='middle' > Recipent Party </td>";
            strHTML = strHTML + "<td valign='middle' >Request Time</td>";
            strHTML = strHTML + "<td valign='middle' >Request State </td>";
            strHTML = strHTML + "<td valign='middle' >Request Message</td>";
            strHTML = strHTML + "<td valign='middle' >Response ID</td>";
            strHTML = strHTML + "<td valign='middle' >Response State</td>";
            strHTML = strHTML + "<td valign='middle' >Response Time</td>";
            strHTML = strHTML + "<td valign='middle' >Response Message</td>";
            strHTML = strHTML + "</tr>";

            for (int intCnt = 0; intCnt < intCountGrd; intCnt++)
            {
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >'" + grvRequestList.Rows[intCnt].Cells[0].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >'" + grvRequestList.Rows[intCnt].Cells[1].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' > " + grvRequestList.Rows[intCnt].Cells[2].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[3].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[4].Text + " </td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[5].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >'" + grvRequestList.Rows[intCnt].Cells[6].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[7].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[8].Text + "</td>";
                strHTML = strHTML + "<td valign='middle' >" + grvRequestList.Rows[intCnt].Cells[9].Text + "</td>";
                strHTML = strHTML + "</tr>";
            }
            strHTML = strHTML + "</table>";
            clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
