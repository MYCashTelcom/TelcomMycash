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
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Data.OracleClient;

public partial class COMMON_frmABStoMYCashReport : System.Web.UI.Page
{




    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    private string strLoginName = string.Empty;
    private string strPassword = string.Empty;
    //private string strClientID = string.Empty;
    DateTime dt = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (!IsPostBack)
        {
            dtpFrom.Date = dt;
            dtpTo.Date = dt;  
            gdvAccountDetail.Visible = false;
           
            btnExport.Visible = false;
            try
            {
                //strClientID = Session["ClientID"].ToString();
                strLoginName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch
            {
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
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
        
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        gdvAccountDetail.Visible = true;
       
       
        btnExport.Visible = true;


        string frmDateIn = dtpFrom.DateString.ToString();

        DateTime dateTime10 = dtpFrom.Date;
        string frmDate = dateTime10.ToString("MM/dd/yyyy hh:mm:ss tt");


        string toDateIn = dtpTo.DateString.ToString();

        DateTime dateTime11 = dtpTo.Date;
        string toDate = dateTime11.ToString("MM/dd/yyyy hh:mm:ss tt");
        string frmTT = frmDate.Substring(frmDate.Length - 2);
        string toTT = toDate.Substring(toDate.Length - 2);
        string frmFormat = "'MM/dd/yyyy hh:mi:ss " + frmTT;
        string toFormat = "'MM/dd/yyyy hh:mi:ss " + toTT;


        DataSet dtsAccount = new DataSet();
        string strQry = "SELECT MAD.ACCOUNT_NO MYCASH_WALLET,MAD.ABS_CLIENT_ACCOUNT_NO ABS_ACCOUNT_NO,MAD.CLIENT_NAME NAME,MAD.CLIENT_NID,MAD.CREATION_DATE,MAD.IS_TAGGED,ABS_AGENT_ACCOUNT_NO FROM MYCASH_ABS_DETAIL MAD WHERE MAD.CREATION_DATE >= TO_DATE('" + frmDate + "'," + frmFormat + "') AND MAD.CREATION_DATE <= TO_DATE('" + toDate + "'," + toFormat + "') ORDER BY MAD.CREATION_DATE DESC";
        dtsAccount = objServiceHandler.ExecuteQuery(strQry);
        gdvAccountDetail.DataSource = dtsAccount;
        gdvAccountDetail.DataBind();
    }
   
    protected void btnExport_Click(object sender, EventArgs e)
    {
       
        ExportReport();
        
    }
    private void ExportReport()
    {
        try
        {

            string fileName = "", strHTML = "";
            //int totalAmount = 0;
            DataTable dtsAccount = new DataTable();
            fileName = "ABS to MYCash Report";
            //------------------------------------------Report File xl processing   -------------------------------------
            string frmDateIn = dtpFrom.DateString.ToString();

            DateTime dateTime10 = dtpFrom.Date;
            string frmDate = dateTime10.ToString("MM/dd/yyyy hh:mm:ss tt");


            string toDateIn = dtpTo.DateString.ToString();

            DateTime dateTime11 = dtpTo.Date;
            string toDate = dateTime11.ToString("MM/dd/yyyy hh:mm:ss tt");
            string frmTT = frmDate.Substring(frmDate.Length - 2);
            string toTT = toDate.Substring(toDate.Length - 2);
            string frmFormat = "'MM/dd/yyyy hh:mi:ss " + frmTT;
            string toFormat = "'MM/dd/yyyy hh:mi:ss " + toTT;



            string strQry = "SELECT MAD.ACCOUNT_NO MYCASH_WALLET,MAD.ABS_CLIENT_ACCOUNT_NO ABS_ACCOUNT_NO,MAD.CLIENT_NAME NAME,MAD.CLIENT_NID,MAD.CREATION_DATE,MAD.IS_TAGGED,ABS_AGENT_ACCOUNT_NO FROM MYCASH_ABS_DETAIL MAD WHERE MAD.CREATION_DATE >= TO_DATE('" + frmDate + "'," + frmFormat + "') AND MAD.CREATION_DATE <= TO_DATE('" + toDate + "'," + toFormat + "') ORDER BY MAD.CREATION_DATE DESC";
            dtsAccount = objServiceHandler.ExecuteQueryV2(strQry);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>ABS to MYCash Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            //strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>From: '" + dateFrom + "' To : '" + dateTo + "'</h2></td></tr>";
            //strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' > SL</td>";
            strHTML = strHTML + "<td valign='middle' > MYCash Wallet No.</td>";
            strHTML = strHTML + "<td valign='middle' > ABS Account No</td>";

            strHTML = strHTML + "<td valign='middle' >Client Name</td>";
            strHTML = strHTML + "<td valign='middle' >Client NID No</td>";
            strHTML = strHTML + "<td valign='middle' > Creation Date</td>";
            strHTML = strHTML + "<td valign='middle' > ABS Registered By Agent</td>";
            strHTML = strHTML + "<td valign='middle' >Tag Status</td>";
            
            
          
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Rows.Count > 0)
            {
                int SerialNo = 1;

                foreach (DataRow prow in dtsAccount.Rows)
                {
                    strHTML = strHTML + "<tr>";
                    strHTML = strHTML + " <td > " + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > \'" + prow["MYCASH_WALLET"].ToString() + "</td>";

                    string acc = "'" + prow["ABS_ACCOUNT_NO"].ToString();
                    strHTML = strHTML + " <td > " + acc + "</td>";
                    strHTML = strHTML + " <td > " + prow["NAME"].ToString() + "</td>";
                    string nid = "'" + prow["CLIENT_NID"].ToString();
                    strHTML = strHTML + " <td > " + nid + "</td>";
                    strHTML = strHTML + " <td > " + prow["CREATION_DATE"].ToString() + "</td>";
                    string agacc = "'" + prow["ABS_AGENT_ACCOUNT_NO"].ToString();
                    strHTML = strHTML + " <td > " + agacc + "</td>";
                    strHTML = strHTML + " <td > " + prow["IS_TAGGED"].ToString() + "</td>";
                    
                    
                   
                    strHTML = strHTML + "</tr>";
                    SerialNo++;

                }

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            }
        }


        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    

}