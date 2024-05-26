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

public partial class COMMON_frmAccountSerial : System.Web.UI.Page
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
   
    protected void btnActive_Click(object sender, EventArgs e)
    {
        DataSet Status = new DataSet();
        clsServiceHandler objServiceHandeler = new clsServiceHandler();
        string St="";
        int intSLNo=0;
        int intSLNoTo=0;
        string strBank = drpBankList.SelectedValue.ToString();
        string strValue = "";
        Button b = (Button)sender;
        string id = b.CommandArgument;        
        foreach (GridViewRow row in gdvSerialList.Rows)
        {
            strValue = row.Cells[0].Text;
           // strBank = row.Cells[3].Text;            
            if (strValue.Equals(id))
            {
                intSLNo = int.Parse(row.Cells[1].Text);
                intSLNoTo = int.Parse(row.Cells[2].Text);
                Status = objServiceHandeler.CheckStatus(strValue);
                foreach (DataRow pRow in Status.Tables["ACCOUNT_SERIAL_MASTER"].Rows)
                {
                    St = pRow["STATUS"].ToString();
                }
                if (St == "A")
                {
                    lblMessage.Text = "Already Generated.";
                }
                else
                {
                    InsertDetailID(strValue, intSLNo, intSLNoTo, strBank);                    
                }
                break;
            }
        }
        SaveAuditInfo("Active", "Account Serial Creation From " + intSLNo + " To " + intSLNoTo);  
    }

    public void InsertDetailID(string strValue, int intSLNo, int intSLNoTo, string strBank)
    {
        string strMsg = "";
        //DataSet ASL = new DataSet();
        clsServiceHandler objServiceHandeler = new clsServiceHandler();
        //Button b = (Button)sender;
        //string id = b.CommandArgument;
        //string strValue = "";
        //foreach (GridViewRow row in gdvSerialList.Rows)
        //{
        //    strValue = row.Cells[0].Text;
        //    if (strValue.Equals(id))
        //    {
        //        intSLNo = int.Parse( row.Cells[1].Text);
        //        intSLNoTo = int.Parse(row.Cells[2].Text);
        //        break;
        //    }
        //}        
        // intSLNo = Int64.Parse(txtSerialFrom.Text);
        //for (int intIndex = intSLNo; intIndex <= intSLNoTo; intIndex++)
        //{
        //    objServiceHandeler.GenSLNo(strValue, intSLNo);
        //    intSLNo++;
        //}
        strMsg = objServiceHandeler.CreateAccSLNo(strValue, intSLNo.ToString(), intSLNoTo.ToString(), strBank);
        //objServiceHandeler.GenSLStatusUpdate(strValue);
        sdsClientList.DataBind();
        gdvSerialList.DataBind();
        lblMessage.Text = strMsg;
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        SaveAuditInfo("Insert", "Account Serial Creation");
    }
	protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        lblMessage.Text = "";

        clsServiceHandler objServiceHandeler = new clsServiceHandler();

        string start = e.Values[0].ToString();
        string end = e.Values[1].ToString();

        string strSql = "SELECT COUNT(*) FROM ACCOUNT_SERIAL_DETAIL WHERE SERIAL_NO BETWEEN " + start + " AND " + end + "";

        string count = objServiceHandeler.ReturnString(strSql);

        if (!count.Equals("0"))
        {
            e.Cancel = true;
            lblMessage.Text = "Duplicate serial cannot be created.";
            return;
        }
    }
}
