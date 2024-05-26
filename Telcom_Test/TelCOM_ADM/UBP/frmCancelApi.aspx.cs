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

public partial class UBP_frmCancelApi : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
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
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }

            //LoadBillerType();

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

    private void LoadGrid()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.BILL_MONTH, UT.BILL_YEAR, UT.TOTAL_BILL_AMOUNT,"
                     + " UT.SERVICE, SR.REQUEST_ID, UT.OWNER_CODE, UT.SOURCE_ACC_NO, UT.TRANSA_DATE, UT.REVERSE_STATUS, "
                     + " UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.PAYER_MOBILE_NO, UT.CHECK_STATUS, UT.CANCLE_RESPONSE "
                     + " FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT "
                     + " WHERE UT.CHECK_STATUS = 'Y' AND UT.CANCLE_RESPONSE IS NOT NULL AND UT.RESPONSE_STATUS_BP IS NULL "
                     + " AND UT.REVERSE_STATUS = 'N' AND UT.TOTAL_BILL_AMOUNT IS NOT NULL AND UT.CANCLE_RESPONSE <> '804' "
                     + " AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID = CAT.REQUEST_ID AND UT.SERVICE = 'UBP' "
                     + " AND UT.STAKEHOLDER_ID = 'MBLBANK' AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpFrDate.DateString +
                     "' AND " + " '" + dtpToDate.DateString + "' ORDER BY UT.TRANSA_DATE ASC";

            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            grvCancelApiList.DataSource = oDataSet;
            grvCancelApiList.DataBind();

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void grvCancelApiList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvCancelApiList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void lnkButtonReverse_Click(object sender, EventArgs e)
    {
        try
        {
            
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;

            Label lblUtlTrxidId = (Label)Grow.FindControl("Label1");
            string strUtlTrxid = lblUtlTrxidId.Text.ToString();

            string strSuccmsg = objServiceHandler.UpdateBillTrxIdCancelToReverse(strUtlTrxid);
            if (strSuccmsg == "Successfull.")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Bill payment successfully reversed');", true);
                LoadGrid();
            }


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }


    }


}
