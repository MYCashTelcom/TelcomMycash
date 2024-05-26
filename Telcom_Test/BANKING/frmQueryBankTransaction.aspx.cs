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

public partial class Forms_frmQueryBankTransaction : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
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
        if (dptFromDate.DateString!= "")
        {
            dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(-6)); 
            //txtFromDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt.AddHours(-6));
            //txtToDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt.AddHours(6));  
            dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddHours(6)); 
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
    protected void CalculateTotal()
    {
        bool blnVisible;
        double dblDebit = 0;
        double dblCredit = 0;
        double dblBalance = 0;
        foreach(GridViewRow row in grvRequestList.Rows)
        {
            blnVisible = row.Visible;
            if (blnVisible == true)
            {
                dblDebit = double.Parse((row.Cells[6].Text.Equals("&nbsp;")) ? "0" :row.Cells[6].Text);
                dblCredit = double.Parse((row.Cells[7].Text.Equals("&nbsp;")) ? "0" :row.Cells[7].Text);
                dblBalance = dblBalance + dblCredit - dblDebit;
            }
        }
        if (grvRequestList.Rows.Count > 0)
        {
            if (dblBalance < 0)
            {
                grvRequestList.FooterRow.Cells[6].Text =  (dblBalance*-1).ToString();
            }
            else
            {
                grvRequestList.FooterRow.Cells[7].Text = dblBalance.ToString();
            }
            grvRequestList.FooterRow.Cells[5].Text = "Ending Balance";
        }        
    }
    public void LoadRequestList()
    {
        string strAdditionalQuery = "";
       
            if (txtAccountNo.Text != "")
            {
              strAdditionalQuery= " CLINT_BANK_ACC_NO='" + txtAccountNo.Text.ToString().Trim() + "' AND BANK_TRAN_DATE "
                               + " BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
            }
            else
            {
                strAdditionalQuery = " BANK_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
            }

            String strSQL = " SELECT BANK_TRAN_ID, BANK_TRAN_ID,ACCESS_CODE ||' '|| BANK_TRAN_DESC BANK_TRAN_DESC, BANK_TRAN_DTL_ID, BANK_ACCOUNT_NO, AC_NAME, CLINT_BANK_ACC_NO,"
                          + " BANK_INTERNAL_CODE, BANK_TRAN_DATE,Round(DEBIT,2) DEBIT, Round(CREDIT,2) CREDIT, IS_SATLEMENT_TRAN FROM ALL_BANK_TRANSACTION WHERE " + strAdditionalQuery;
            try
            {
                sdsBankTrans.SelectCommand = strSQL;
                sdsBankTrans.DataBind();
                grvRequestList.DataBind();
            }
            catch (Exception exx)
            {
                exx.Message.ToString();
            }
    }
    protected void ddlAccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        LoadRequestList();
        CalculateTotal();
        SaveAuditInfo("Search", "Transaction History");
    }	
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {  
        clsAccountHandler objAccountHandler = new clsAccountHandler();
        DataSet oDS = new DataSet();
        string strHTML = "";
        string filename = "Bank Transaction";
        string strAccountNo = txtAccountNo.Text.Trim();
        string strFromDate = dptFromDate.DateString;
        string strToDate = dtpToDate.DateString;
        oDS = objAccountHandler.queryBankTransaction(strAccountNo, strFromDate, strToDate);
        SaveAuditInfo("Report", "Transaction History");
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none'><h2 align=center> Bank Transaction </h2></td></tr>";        
        strHTML = strHTML + "</table>";
        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + "<td valign='middle' >SerialNo</td>";
        strHTML = strHTML + "<td valign='middle' >Description</td>";
        //strHTML = strHTML + "<td valign='middle' >Account No</td>";
        strHTML = strHTML + "<td valign='middle' >  Account Name  </td>";
        strHTML = strHTML + "<td valign='middle' >ACC No</td>";
        strHTML = strHTML + "<td valign='middle' >Bank Code</td>";
        strHTML = strHTML + "<td valign='middle' >Date</td>"; 
        strHTML = strHTML + "<td valign='middle' >Debit</td>";
        strHTML = strHTML + "<td valign='middle' >Credit</td>";
        strHTML = strHTML + "</tr>";       

        if (oDS.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 0;
            foreach (DataRow prow in oDS.Tables["ALL_BANK_TRANSACTION"].Rows)
            {
                string activetiondate = prow["BANK_TRAN_DATE"].ToString().Trim() == "" ? "" : String.Format("{0:dd-MMM-yyyy}", DateTime.Parse(prow["BANK_TRAN_DATE"].ToString()));

                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > " + prow["BANK_TRAN_DESC"].ToString() + " </td>";  
                //strHTML = strHTML + " <td > " + prow["BANK_ACCOUNT_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["AC_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CLINT_BANK_ACC_NO"].ToString() + " </td>";  
                strHTML = strHTML + " <td > " + prow["BANK_INTERNAL_CODE"].ToString() + "</td>";
                strHTML = strHTML + " <td > " + activetiondate + " </td>";
                strHTML = strHTML + " <td > " + prow["DEBIT"].ToString() + " </td>";
                strHTML = strHTML + " <td > " + prow["CREDIT"].ToString() + " </td>";
                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;
            }
        }
        strHTML = strHTML + "<tr>";
        strHTML = strHTML + " <td > " + "" + "</td>";
        strHTML = strHTML + " <td > " + "" + " </td>"; 
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + "" + " </td>";  
        strHTML = strHTML + " <td > " + "" + " </td>";
        strHTML = strHTML + " <td > " + grvRequestList.FooterRow.Cells[5].Text + " </td>";
        strHTML = strHTML + " <td > " + grvRequestList.FooterRow.Cells[6].Text + " </td>";
        strHTML = strHTML + " <td > " + grvRequestList.FooterRow.Cells[7].Text + " </td>";
        strHTML = strHTML + " </tr>";
        strHTML = strHTML + " </table>";
        clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
    }
}
