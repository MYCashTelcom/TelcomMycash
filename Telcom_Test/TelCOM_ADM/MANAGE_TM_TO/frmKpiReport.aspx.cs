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
using System.Drawing;

public partial class MANAGE_TM_TO_frmKpiReport : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();

              
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
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

    protected void btnToDpsReport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpMonthToUt.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpYearToUt.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }

           

            string noofDaysInAMonth = "";
            if (drpMonthToUt.SelectedValue == "Jan")
            {
                noofDaysInAMonth = "31/Jan/";
            }
            else if (drpMonthToUt.SelectedValue == "Feb")
            {
                if (int.Parse(drpYearToUt.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = "29/Feb/";
                }
                else
                {
                    noofDaysInAMonth = "28/Feb/";
                }

            }
            else if (drpMonthToUt.SelectedValue == "Mar")
            {
                noofDaysInAMonth = "31/Mar/";
            }
            else if (drpMonthToUt.SelectedValue == "Apr")
            {
                noofDaysInAMonth = "30/Apr/";
            }
            else if (drpMonthToUt.SelectedValue == "May")
            {
                noofDaysInAMonth = "31/May/";
            }
            else if (drpMonthToUt.SelectedValue == "Jun")
            {
                noofDaysInAMonth = "30/Jun/";
            }
            else if (drpMonthToUt.SelectedValue == "Jul")
            {
                noofDaysInAMonth = "31/Jul/";
            }
            else if (drpMonthToUt.SelectedValue == "Aug")
            {
                noofDaysInAMonth = "31/Aug/";
            }
            else if (drpMonthToUt.SelectedValue == "Sep")
            {
                noofDaysInAMonth = "30/Sep/";
            }
            else if (drpMonthToUt.SelectedValue == "Oct")
            {
                noofDaysInAMonth = "31/Oct/";
            }
            else if (drpMonthToUt.SelectedValue == "Nov")
            {
                noofDaysInAMonth = "30/Nov/";
            }
            else if (drpMonthToUt.SelectedValue == "Dec")
            {
                noofDaysInAMonth = "31/Dec/";
            }

            else
            {
                // do nothing
            }

            string formDt = "1/" + drpMonthToUt.Text.ToString()+"/"+ drpYearToUt.Text.ToString();
            string toDt = noofDaysInAMonth + drpYearToUt.Text.ToString();



            string strSql = "";
            strSql = " SELECT rownum, MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO, MKT.CUST_ACQU_TARGET, FUNC_GET_CUS_REG_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "')  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, FUNC_GET_ACTV_AGNT_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "') ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET,FUNC_GET_LIFTING_ACHIV_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "') LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET, FUNC_GET_CORPORT_COLL_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "') CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,  FUNC_GET_AGNT_TRX_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "') TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,FUNC_GET_UTILITY_TRX_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "') UTILITY_AMOUNT_ACHIVE FROM MANAGE_AREA MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT   WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)  AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthToUt.Text.ToString() + "' AND MKT.TARGET_YEAR='" + drpYearToUt.Text.ToString() + "'AND UTILITY_AMOUNT_TARGET>0";


            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Kpi Report TO(utility)";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Kpi Report TO(utility)(" + drpMonthToUt.SelectedValue + "-" + drpYearToUt.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=left> Date Range: " + formDt + " to " + toDt + " </h4></td> <td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td> </tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name </td>";
            strHTML = strHTML + "<td valign='middle' >TM Wallet </td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Target Month-Year</td>";
            strHTML = strHTML + "<td valign='middle' >MyDps Target</td>";
            strHTML = strHTML + "<td valign='middle' >MyDps Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Landing as per Trend</td>";
            strHTML = strHTML + "<td valign='middle' >MTD Achievement</td>";
            strHTML = strHTML + "<td valign='middle' >Pending</td>";
            strHTML = strHTML + "<td valign='middle' >Daily Require</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TARGET_MNT_YR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_TARGET"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_ACQ"].ToString() + " </td>";                   
                   

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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Kpi Report tm - utility");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";





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



}