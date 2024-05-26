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
using System.Drawing;

public partial class TEXT_frmRequestMonitoring : System.Web.UI.Page 
{    
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            if (dptFromDate.DateString != "")
            {
                dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-3));
                dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(2));
                LoadRequestList();
            }
        }
    }
    public void LoadRequestList()
    {       
         //strSQL = "SELECT SR.REQUEST_ID,SR.REQUEST_PARTY,SR.RECEIPENT_PARTY,SR.REQUEST_TIME,DECODE(SR.REQUEST_STAE,'P','In Que','E','Expired','Done') REQ_STATE,"
         //           + " DECODE(INSTR((REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'*CPIN1')),'PIN'),0, "
         //           + " REPLACE(REPLACE(SUBSTR(SR.REQUEST_TEXT,INSTR(SR.REQUEST_TEXT,'*',1)+1),'#',''),'*CPIN1'),SUBSTR(SR.REQUEST_TEXT,0,5)||'...') REQ_TEXT, "
         //           + " SR.SERVICE_COST,RSP.RESPONSE_TIME,RSP.RESPONSE_ID,DECODE(RSP.RESPONSE_STAE,'D','Replied','P','In Que',NULL, "
         //           + " DECODE(UPPER(SR.REQUEST_STAE),'E','Canceled',DECODE(UPPER(SR.REQUEST_TEXT),'Y','N/A','Waitting')), "
         //           + " DECODE(UPPER(SR.REQUEST_STAE),'E','Canceled','Replied')) RSP_STATE,DECODE(INSTR(RSP.RESPONSE_MESSAGE,'OTP'),0, "
         //           + " DECODE(INSTR(RSP.RESPONSE_MESSAGE,'PIN'),0,RSP.RESPONSE_MESSAGE,SUBSTR(RSP.RESPONSE_MESSAGE,0,INSTR(RSP.RESPONSE_MESSAGE,'PIN')+3)||'...'), "
         //           + " SUBSTR(RSP.RESPONSE_MESSAGE,0,INSTR(RSP.RESPONSE_MESSAGE,'OTP')+3)||'...') RESPONSE_MESSAGE "
         //           + " FROM SERVICE_REQUEST SR,SERVICE_RESPONSE RSP WHERE SR.REQUEST_ID=RSP.REQUEST_ID(+) "
         //           + " AND SR.REQUEST_TIME BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";

         string strSQL="";

         strSQL = "SELECT REQUEST_ID,REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TIME,REQ_STATE,REQUEST_STAE, "
               + "  DECODE(INSTR(REQ_TEXT,'OTP',1),0,REQ_TEXT,SUBSTR(REQ_TEXT,0,5)||'...') REQ_TEXT,  "
                + " SERVICE_COST,RESPONSE_TIME,RESPONSE_ID,RSP_STATE,RESPONSE_STAE,RESPONSE_MESSAGE  FROM VW_REQUEST_MONITOR3 SR WHERE "
                + " SR.REQUEST_TIME BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
          //strSQL = "SELECT REQUEST_ID,REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TIME,REQ_STATE,REQUEST_STAE, "
          //        + "  DECODE(INSTR(REQ_TEXT,'OTP',1),0,REQ_TEXT,SUBSTR(REQ_TEXT,0,5)||'...') REQ_TEXT,  "
          //         + " SERVICE_COST,RESPONSE_TIME,RESPONSE_ID,RSP_STATE,RESPONSE_STAE,RESPONSE_MESSAGE  FROM VW_REQUEST_MONITOR3 SR WHERE "
          //         + " TRUNC(SR.REQUEST_TIME) BETWEEN '01-DEC-2019' AND '11-DEC-2019' ";

        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND SR.REQUEST_PARTY LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtServiceCode.Text.Equals(""))
        {
            strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQ_TEXT,1,INSTR(SR.REQ_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        }
        strSQL = strSQL + " ORDER BY SR.REQUEST_ID DESC";

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
        SaveAuditInfo("View", "Query Request Status");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {       
    }
    protected void grvRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //------------ Requist Cell --------------
            if (e.Row.Cells[4].Text.ToString() != "Done")
            {                
                e.Row.BackColor = Color.FromArgb(Convert.ToInt32("FF8888", 16));
            }
            //----------- Response Cell -----------
            else if (e.Row.Cells[7].Text.ToString() != "Replied" && e.Row.Cells[7].Text.ToString() != "Waiting")
            {
               // e.Row.BackColor = Color.LightYellow;//Cells[10]
                e.Row.BackColor = Color.FromArgb(Convert.ToInt32("FFFF00", 16));
            }
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        LoadRequestList();
    }
}
