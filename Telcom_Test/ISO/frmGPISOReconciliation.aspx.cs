using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public partial class ISO_frmGPISOReconciliation : System.Web.UI.Page
{
    private clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private clsServiceHandler objServiceHandler = new clsServiceHandler();
    private string strUserName = string.Empty;
    private string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GridLoad();
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        #region old
        //string strId = "";
        //string strText = "";
        //strId = txtReqID.Text.Trim();
        
        //string[] lines = Regex.Split(strId, "\n");
        //var myArray1 = "";

        //foreach (var line in lines)
        //{
        //    string strUpdatedReqId = "0000000000" + line.TrimEnd();
        //    string strNewReqId = strUpdatedReqId.Substring(strUpdatedReqId.Length - 13);
        //    if (strNewReqId.Contains(",") == false)
        //    {
        //        strNewReqId = strUpdatedReqId.Substring(strUpdatedReqId.Length - 12); 
        //    }
        //    myArray1 = strNewReqId.ToString();
        //    strText = strText +"'"+ myArray1;
        //}

        //string strTextFinal = strText;
        //int textLength = strTextFinal.Length;
        //string strTextFinal1 = strTextFinal.Substring(1, textLength-1);
        //lblMsg.Text = strTextFinal1;
        #endregion

        GridLoad();
    }

    private void GridLoad()
    {
        string strId = "";
        string strText = "";
        strId = txtReqID.Text.Trim();

        string[] lines = Regex.Split(strId, ",");
        var myArray1 = "";

        foreach (var line in lines)
        {
            string strUpdatedReqId = "000000" + line.TrimStart();
            string strUpdatedReqId1 = strUpdatedReqId.TrimEnd();
            string strNewReqId = strUpdatedReqId1.Substring(strUpdatedReqId1.Length - 12);
            //if (strNewReqId.Contains(",") == false)
            //{
            //    strNewReqId = strUpdatedReqId.Substring(strUpdatedReqId.Length - 12);
            //}
            myArray1 = strNewReqId.ToString();
            strText = strText + myArray1 + "','";
        }

        string strTextFinal = strText;
        int textLength = strTextFinal.Length;
        string strTextFinal1 = strTextFinal.Substring(0, textLength - 3);
        //lblMsg.Text = strTextFinal1;


        try
        {
            string strSql = "";

            strSql = " SELECT R.ISO_CLIENT_REQ_ID,R.CS_ID,R.ISO_RESPONSE_CODE,R.DIFF ,R.CS_RES_CODE, "
                     + " R.CS_DIFF ,RB.ISO_ROLLBACK_REF_ID RollbackID,RB.ISO_RESPONSE_CODE RB_RES_CODE, "
                     + " R.REQ_TIME,R.REQ_TIME_CS, DATEDIFFINSEC(TO_DATE(SUBSTR(RB.ISO_REQUEST_LOG,1,INSTR(RB.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'), "
                     + " TO_DATE(SUBSTR(RB.ISO_RESPONSE_LOG,1,INSTR(RB.ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS RB_DIFF, "
                     + " TO_DATE(SUBSTR(RB.ISO_REQUEST_LOG,1,INSTR(RB.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') AS REQ_TIME_RB , "
                     + " R.ISO_REQUEST_LOG,R.ISO_RESPONSE_LOG,R.CS_REQUEST_LOG,R.CS_RESPONSE_LOG,RB.ISO_REQUEST_LOG RB_REQ_LOG,RB.ISO_RESPONSE_LOG RB_RES_LOG "
                     + " FROM (SELECT  M.ISO_CLIENT_REQ_ID,CS.ISO_CHECK_STATUS_REF_ID CS_ID,M.ISO_RESPONSE_CODE,M.DIFF ,CS.ISO_RESPONSE_CODE CS_RES_CODE, "
                     + " DATEDIFFINSEC(TO_DATE(SUBSTR(CS.ISO_REQUEST_LOG,1,INSTR(CS.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'), "
                     + " TO_DATE(SUBSTR(CS.ISO_RESPONSE_LOG,1,INSTR(CS.ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS CS_DIFF ,M.REQ_TIME, "
                     + " TO_DATE(SUBSTR(CS.ISO_REQUEST_LOG,1,INSTR(CS.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') as REQ_TIME_CS, "
                     + " M.ISO_REQUEST_LOG,M.ISO_RESPONSE_LOG, CS.ISO_REQUEST_LOG CS_REQUEST_LOG,CS.ISO_RESPONSE_LOG CS_RESPONSE_LOG "
                     + " FROM (SELECT ISO_CLIENT_REQ_ID,ISO_RESPONSE_CODE,DATEDIFFINSEC(TO_DATE(SUBSTR(ISO_REQUEST_LOG,1,INSTR(ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'), "
                     + " TO_DATE(SUBSTR(ISO_RESPONSE_LOG,1,INSTR(ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS DIFF,TO_DATE(SUBSTR(ISO_REQUEST_LOG,1,INSTR(ISO_REQUEST_LOG,'M:')) ,'MM/DD/YYYY HH:MI:SS AM')as  REQ_TIME ,ISO_REQUEST_LOG,ISO_RESPONSE_LOG "
                     + " FROM ISO_REQUEST WHERE ISO_CLIENT_REQ_ID  IN('" + strTextFinal1 + "')) M,ISO_REQUEST CS WHERE M.ISO_CLIENT_REQ_ID=CS.ISO_CHECK_STATUS_REF_ID(+) ) "
                     + " R ,ISO_REQUEST RB WHERE  R.ISO_CLIENT_REQ_ID=RB.ISO_ROLLBACK_REF_ID(+) ";
           

            sdsStatus.SelectCommand = strSql;
            sdsStatus.DataBind();
            gdvStatus.DataBind();
            txtReqID.Enabled = false;

            DataTable dt = (DataTable)gdvStatus.DataSource;
            int rowCount = dt.Rows.Count;

            if (rowCount == 0)
            {
                lblMsg.Text = "No Data Found..";
            }

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }


        //txtReqID.Text = "";
    }

    private void GridLoadReport()
    {
        gdvStatus.Visible = false;
        string strId = "";
        string strText = "";
        strId = txtReqID.Text.Trim();

        string[] lines = Regex.Split(strId, ",");
        var myArray1 = "";

        foreach (var line in lines)
        {
            string strUpdatedReqId = "000000" + line.TrimStart();
            string strUpdatedReqId1 = strUpdatedReqId.TrimEnd();
            string strNewReqId = strUpdatedReqId1.Substring(strUpdatedReqId1.Length - 12);
            myArray1 = strNewReqId.ToString();
            strText = strText + myArray1 + "','";
        }

        string strTextFinal = strText;
        int textLength = strTextFinal.Length;
        string strTextFinal1 = strTextFinal.Substring(0, textLength - 3);
        
        try
        {
            string strSql = "";

            //strSql = " SELECT R.ISO_CLIENT_REQ_ID ISO_CLIENT_REQ_ID,R.CS_ID CS_ID,R.ISO_RESPONSE_CODE ISO_RESPONSE_CODE,R.DIFF DIFF,R.CS_RES_CODE CS_RES_CODE, "
            //         + " R.CS_DIFF CS_DIFF,RB.ISO_ROLLBACK_REF_ID ROLLBACKID,RB.ISO_RESPONSE_CODE RB_RES_CODE, "
            //         + " R.REQ_TIME REQ_TIME,R.REQ_TIME_CS REQ_TIME_CS, DATEDIFFINSEC(TO_DATE(SUBSTR(RB.ISO_REQUEST_LOG,1,INSTR(RB.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') TIME_DIFFERENCE, "
            //         + " TO_DATE(SUBSTR(RB.ISO_RESPONSE_LOG,1,INSTR(RB.ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS RB_DIFF, "
            //         + " TO_DATE(SUBSTR(RB.ISO_REQUEST_LOG,1,INSTR(RB.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') AS REQ_TIME_RB , "
            //         + " R.ISO_REQUEST_LOG ISO_REQUEST_LOG,R.ISO_RESPONSE_LOG ISO_RESPONSE_LOG,R.CS_REQUEST_LOG CS_REQUEST_LOG,R.CS_RESPONSE_LOG CS_RESPONSE_LOG,RB.ISO_REQUEST_LOG RB_REQ_LOG, RB.ISO_RESPONSE_LOG RB_RES_LOG "
            //         + " FROM (SELECT  M.ISO_CLIENT_REQ_ID,CS.ISO_CHECK_STATUS_REF_ID CS_ID,M.ISO_RESPONSE_CODE,M.DIFF ,CS.ISO_RESPONSE_CODE CS_RES_CODE, "
            //         + " DATEDIFFINSEC(TO_DATE(SUBSTR(CS.ISO_REQUEST_LOG,1,INSTR(CS.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'), "
            //         + " TO_DATE(SUBSTR(CS.ISO_RESPONSE_LOG,1,INSTR(CS.ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS CS_DIFF ,M.REQ_TIME, "
            //         + " TO_DATE(SUBSTR(CS.ISO_REQUEST_LOG,1,INSTR(CS.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') as REQ_TIME_CS, "
            //         + " M.ISO_REQUEST_LOG,M.ISO_RESPONSE_LOG, CS.ISO_REQUEST_LOG CS_REQUEST_LOG,CS.ISO_RESPONSE_LOG CS_RESPONSE_LOG "
            //         + " FROM (SELECT ISO_CLIENT_REQ_ID,ISO_RESPONSE_CODE,DATEDIFFINSEC(TO_DATE(SUBSTR(ISO_REQUEST_LOG,1,INSTR(ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'), "
            //         + " TO_DATE(SUBSTR(ISO_RESPONSE_LOG,1,INSTR(ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS DIFF,TO_DATE(SUBSTR(ISO_REQUEST_LOG,1,INSTR(ISO_REQUEST_LOG,'M:')) ,'MM/DD/YYYY HH:MI:SS AM')as  REQ_TIME ,ISO_REQUEST_LOG,ISO_RESPONSE_LOG "
            //         + " FROM ISO_REQUEST WHERE ISO_CLIENT_REQ_ID  IN('" + strTextFinal1 + "')) M,ISO_REQUEST CS WHERE M.ISO_CLIENT_REQ_ID=CS.ISO_CHECK_STATUS_REF_ID(+) ) "
            //         + " R ,ISO_REQUEST RB WHERE  R.ISO_CLIENT_REQ_ID=RB.ISO_ROLLBACK_REF_ID(+) ";


            strSql = " SELECT R.ISO_CLIENT_REQ_ID,R.CS_ID,R.ISO_RESPONSE_CODE,R.DIFF ,R.CS_RES_CODE, "
                     + " R.CS_DIFF ,RB.ISO_ROLLBACK_REF_ID RollbackID,RB.ISO_RESPONSE_CODE RB_RES_CODE, "
                     + " R.REQ_TIME,R.REQ_TIME_CS, DATEDIFFINSEC(TO_DATE(SUBSTR(RB.ISO_REQUEST_LOG,1,INSTR(RB.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'), "
                     + " TO_DATE(SUBSTR(RB.ISO_RESPONSE_LOG,1,INSTR(RB.ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS RB_DIFF, "
                     + " TO_DATE(SUBSTR(RB.ISO_REQUEST_LOG,1,INSTR(RB.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') AS REQ_TIME_RB , "
                     + " R.ISO_REQUEST_LOG,R.ISO_RESPONSE_LOG,R.CS_REQUEST_LOG,R.CS_RESPONSE_LOG,RB.ISO_REQUEST_LOG RB_REQ_LOG,RB.ISO_RESPONSE_LOG RB_RES_LOG "
                     + " FROM (SELECT  M.ISO_CLIENT_REQ_ID,CS.ISO_CHECK_STATUS_REF_ID CS_ID,M.ISO_RESPONSE_CODE,M.DIFF ,CS.ISO_RESPONSE_CODE CS_RES_CODE, "
                     + " DATEDIFFINSEC(TO_DATE(SUBSTR(CS.ISO_REQUEST_LOG,1,INSTR(CS.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'), "
                     + " TO_DATE(SUBSTR(CS.ISO_RESPONSE_LOG,1,INSTR(CS.ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS CS_DIFF ,M.REQ_TIME, "
                     + " TO_DATE(SUBSTR(CS.ISO_REQUEST_LOG,1,INSTR(CS.ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM') as REQ_TIME_CS, "
                     + " M.ISO_REQUEST_LOG,M.ISO_RESPONSE_LOG, CS.ISO_REQUEST_LOG CS_REQUEST_LOG,CS.ISO_RESPONSE_LOG CS_RESPONSE_LOG "
                     + " FROM (SELECT ISO_CLIENT_REQ_ID,ISO_RESPONSE_CODE,DATEDIFFINSEC(TO_DATE(SUBSTR(ISO_REQUEST_LOG,1,INSTR(ISO_REQUEST_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM'), "
                     + " TO_DATE(SUBSTR(ISO_RESPONSE_LOG,1,INSTR(ISO_RESPONSE_LOG,'M:')),'MM/DD/YYYY HH:MI:SS AM')) AS DIFF,TO_DATE(SUBSTR(ISO_REQUEST_LOG,1,INSTR(ISO_REQUEST_LOG,'M:')) ,'MM/DD/YYYY HH:MI:SS AM')as  REQ_TIME ,ISO_REQUEST_LOG,ISO_RESPONSE_LOG "
                     + " FROM ISO_REQUEST WHERE ISO_CLIENT_REQ_ID  IN('" + strTextFinal1 + "')) M,ISO_REQUEST CS WHERE M.ISO_CLIENT_REQ_ID=CS.ISO_CHECK_STATUS_REF_ID(+) ) "
                     + " R ,ISO_REQUEST RB WHERE  R.ISO_CLIENT_REQ_ID=RB.ISO_ROLLBACK_REF_ID(+) ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "ISO_RECONCILIATION";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>ISO Reconciliation Details Information</h3></td></tr>";
            strHTML = strHTML + "</table>";
            
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >ISO Client Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >ISO Response Code </td>";
            strHTML = strHTML + "<td valign='middle' >Request Time </td>";
            strHTML = strHTML + "<td valign='middle' >Time Difference(In Second)</td>";
            strHTML = strHTML + "<td valign='middle' >Check Status Id</td>";
            strHTML = strHTML + "<td valign='middle' >Check Status Request Time </td>";
            strHTML = strHTML + "<td valign='middle' >Check Status Response Code</td>";
            strHTML = strHTML + "<td valign='middle' >Check Status Time Difference</td>";
            strHTML = strHTML + "<td valign='middle' >Roll Back Id</td>";
            strHTML = strHTML + "<td valign='middle' >Roll Back Request Time</td>";
            strHTML = strHTML + "<td valign='middle' >Roll Back Response Code</td>";
            strHTML = strHTML + "<td valign='middle' >Roll Back Time Difference(In Second)</td>";
            strHTML = strHTML + "<td valign='middle' >ISO Request Log</td>";
            strHTML = strHTML + "<td valign='middle' >ISO Response Log</td>";
            strHTML = strHTML + "<td valign='middle' >Check Status Request Log</td>";
            strHTML = strHTML + "<td valign='middle' >Check Status Response Log</td>";
            strHTML = strHTML + "<td valign='middle' >Roll Back Request Log</td>";
            strHTML = strHTML + "<td valign='middle' >Roll Back Response Log</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ISO_CLIENT_REQ_ID"].ToString() + " </td>"; //
                    strHTML = strHTML + " <td > '" + prow["ISO_RESPONSE_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQ_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIFF"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CS_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQ_TIME_CS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CS_RES_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CS_DIFF"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ROLLBACKID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQ_TIME_RB"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RB_RES_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RB_DIFF"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ISO_REQUEST_LOG"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ISO_RESPONSE_LOG"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CS_REQUEST_LOG"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CS_RESPONSE_LOG"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RB_REQ_LOG"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RB_RES_LOG"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Business_Collection_Rpt1");
            txtReqID.Text = "";
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }

        //txtReqID.Text = "";
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        GridLoadReport();
    }

    protected void btnSample_Click(object sender, EventArgs e)
    {
        string strHTML = "";
        string filename = "Sheet1";
        
        strHTML = strHTML + "080900009188, 080900005432, 080900003967, 80700001942, 080600019693," + System.Environment.NewLine;
        strHTML = strHTML + "080600011172, 080600012360, 080600010645, 080500008176, 080500011878";
        
        clsGridExport.ExportToMSExcel12(filename, "txt", strHTML, "landscape");   
        
    }
}
