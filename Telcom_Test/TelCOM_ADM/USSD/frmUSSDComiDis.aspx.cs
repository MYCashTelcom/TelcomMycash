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

public partial class COMMON_frmUSSDComiDis : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {
            Session["strSQL"] = "";
            LoadAllData();
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

    private void LoadAllData()
    {
        string strSQL = "";
        strSQL = " SELECT REQUEST_PARTY_NAME,ACCNT_NO,REQUEST_ID,REQUEST_PARTY,RECEIPENT_PARTY,"
              + " REQUEST_TEXT,SERVICE_TITLE,COLLECTED_COM_AMOUNT,CAS_TRAN_DATE,Round(AIRTEL_COMMISSION,3) AIRTEL_COMMISSION,"
              + " COMMI_DIS_MASTER_ID FROM BDMIT_ERP_101.CAS_COMMISSION_DISBURSEMENT "
              + " ORDER BY CAS_TRAN_DATE DESC ";
        try
        {
            sdsComiDis.SelectCommand = strSQL;
            sdsComiDis.DataBind();
            GridView1.DataBind();

            Session["strSQL"] = strSQL;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strSQL = "", strAdditionalQuery = "";

        if (rblType.SelectedValue == "0")
        {
            strAdditionalQuery = "  TO_CHAR(TO_DATE(CAS_TRAN_DATE)) BETWEEN TO_DATE(\'" 
                               + txtFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" 
                               + txtToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') ";

        }
        else if (rblType.SelectedValue == "1")
        {
            strAdditionalQuery = " COMMI_DIS_MASTER_ID IS NOT NULL AND TO_CHAR(TO_DATE(CAS_TRAN_DATE)) BETWEEN TO_DATE(\'" 
                + txtFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') ";
        }
        else if (rblType.SelectedValue == "2")
        {
            strAdditionalQuery = " COMMI_DIS_MASTER_ID IS NULL AND TO_CHAR(TO_DATE(CAS_TRAN_DATE)) BETWEEN TO_DATE(\'" 
                + txtFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + txtToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') ";
        }

        strSQL = " SELECT REQUEST_PARTY_NAME,ACCNT_NO,REQUEST_ID,REQUEST_PARTY,RECEIPENT_PARTY,"
              + " REQUEST_TEXT,SERVICE_TITLE,COLLECTED_COM_AMOUNT,CAS_TRAN_DATE, Round(AIRTEL_COMMISSION,3) AIRTEL_COMMISSION ,"
              + " COMMI_DIS_MASTER_ID FROM BDMIT_ERP_101.CAS_COMMISSION_DISBURSEMENT WHERE" + strAdditionalQuery
              + " ORDER BY CAS_TRAN_DATE DESC ";
       
        try
        {
            sdsComiDis.SelectCommand = strSQL;
            sdsComiDis.DataBind();
            GridView1.DataBind();

            Session["strSQL"] = strSQL;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {       
        string strHTML = "";
        string filename = "USSD Commission";

        if (Session["strSQL"].ToString() != "")
        {
            GridView1.AllowPaging = false;
            string strSQL = Session["strSQL"].ToString();
            sdsComiDis.SelectCommand = strSQL;
            sdsComiDis.DataBind();
        }
        
        GridView1.DataBind();

        int intTotalRow = GridView1.Rows.Count;
        decimal dcmTotalCommission = 0;

        if (intTotalRow > 0)
        {
            try
            {
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=9 align=center style='border-right:none'><h2 align=center> USSD Commission </h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >SerialNo</td>";
                strHTML = strHTML + "<td valign='middle' >Request Party Name</td>";
                strHTML = strHTML + "<td valign='middle' >Wallet</td>";
                strHTML = strHTML + "<td valign='middle' >  Request ID  </td>";
                strHTML = strHTML + "<td valign='middle' >Request Party</td>";
                strHTML = strHTML + "<td valign='middle' >Request Party Type</td>";
                strHTML = strHTML + "<td valign='middle' >Request Text </td>";
                strHTML = strHTML + "<td valign='middle' >Service Name</td>";
                strHTML = strHTML + "<td valign='middle' >Collected Commission Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
                strHTML = strHTML + "<td valign='middle' >Airtel Commission</td>";
                strHTML = strHTML + "<td valign='middle' >Disburse ID</td>";

                strHTML = strHTML + "</tr>";

                for (int j = 0; j < intTotalRow; j++)
                {
                    strHTML = strHTML + "<tr>";
                    strHTML = strHTML + "<td valign='middle'>" + (j + 1) + "</td>";
                    strHTML = strHTML + "<td align='left'>" + GridView1.Rows[j].Cells[0].Text + "</td>";
                    strHTML = strHTML + "<td align='right'>" + GridView1.Rows[j].Cells[1].Text + "</td>";
                    strHTML = strHTML + "<td align='right'>'" + GridView1.Rows[j].Cells[2].Text + "</td>";
                    strHTML = strHTML + "<td align='right'>'" + GridView1.Rows[j].Cells[3].Text + "</td>";
                    strHTML = strHTML + "<td align='right'>" + GridView1.Rows[j].Cells[4].Text + "</td>";
                    strHTML = strHTML + "<td align='center'>" + GridView1.Rows[j].Cells[5].Text + "</td>";
                    strHTML = strHTML + "<td align='center'>" + GridView1.Rows[j].Cells[6].Text + "</td>";
                    strHTML = strHTML + "<td align='right'>" + GridView1.Rows[j].Cells[7].Text + "</td>";
                    strHTML = strHTML + "<td align='right'>" + GridView1.Rows[j].Cells[8].Text + "</td>";
                    strHTML = strHTML + "<td align='center'>" + GridView1.Rows[j].Cells[9].Text + "</td>";
                    strHTML = strHTML + "<td align='center'>" + GridView1.Rows[j].Cells[10].Text + "</td>";

                    strHTML = strHTML + "</tr>";
                    dcmTotalCommission = dcmTotalCommission + Convert.ToDecimal(GridView1.Rows[j].Cells[9].Text.ToString());
                }
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle'></td>";
                strHTML = strHTML + "<td align='right'>" + GridView1.FooterRow.Cells[0].Text + "</td>";
                strHTML = strHTML + "<td align='right'><b>" + GridView1.FooterRow.Cells[1].Text + "</b></td>";
                strHTML = strHTML + "<td align='right'>" + GridView1.FooterRow.Cells[2].Text + "</td>";
                strHTML = strHTML + "<td align='right'>" + GridView1.FooterRow.Cells[3].Text + "</td>";
                strHTML = strHTML + "<td align='right'>" + GridView1.FooterRow.Cells[4].Text + "</td>";
                strHTML = strHTML + "<td align='right'>" + GridView1.FooterRow.Cells[5].Text + "</td>";
                strHTML = strHTML + "<td align='right'><b>" + GridView1.FooterRow.Cells[6].Text + "</b></td>";
                strHTML = strHTML + "<td align='right'>" + GridView1.FooterRow.Cells[7].Text + "</td>";
                strHTML = strHTML + "<td align='right'>" + GridView1.FooterRow.Cells[8].Text + "</td>";
                strHTML = strHTML + "<td align='right'>" + Convert.ToString(dcmTotalCommission) + "</td>";
                strHTML = strHTML + "<td align='right'>" + GridView1.FooterRow.Cells[10].Text + "</td>";

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";


                if (Session["strSQL"].ToString() != "")
                {
                    GridView1.AllowPaging = true;
                    string strSQL = Session["strSQL"].ToString();
                    sdsComiDis.SelectCommand = strSQL;
                    sdsComiDis.DataBind();
                }
                
                GridView1.DataBind();
               
                
                clsGridExport.ExportToMSExcel(filename, "msexcel", strHTML, "landscape");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        else
        {
 
        }
    }
}
