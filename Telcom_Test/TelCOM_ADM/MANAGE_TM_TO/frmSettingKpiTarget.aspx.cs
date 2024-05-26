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

public partial class MANAGE_TM_TO_frmSettingKpiTarget : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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
    protected void btnTOInfo_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            txtSearchTOAcc.Enabled = false;
            strSql = " SELECT SRAL.ACCNT_ID,  SRAL.ACCNT_NO, SRAL.ACCNT_MSISDN, SRCL.CLINT_NAME, SRAR.ACCNT_RANK_ID, SRAR.RANK_TITEL, SRAL.TERRITORY_RANK_ID, MTR.TERRITORY_RANK_NAME,"
                     + " SRSP.SERVICE_PKG_NAME, HRAL.ACCNT_NO||'('||CLHR.CLINT_NAME||','||CLHR.CLINT_ADDRESS1||')' HIERARCHY_NAME_ADDRESS, "
                     + " UPCL.CLINT_NAME||', '||UPCL.CLINT_ADDRESS1 UPBY_INFO FROM ACCOUNT_LIST SRAL, CLIENT_LIST SRCL, ACCOUNT_RANK SRAR, "
                     + " SERVICE_PACKAGE SRSP, MANAGE_TERRITORY_HIERARCHY SRMTH, ACCOUNT_LIST HRAL, CLIENT_LIST CLHR, "
                     + " CM_SYSTEM_USERS UPSU, ACCOUNT_LIST UPAL, CLIENT_LIST UPCL, MANAGE_TERRITORY_RANK MTR WHERE SRAL.ACCNT_NO = '" + txtSearchTOAcc.Text.Trim() + "' "
                     + " AND SRAL.CLINT_ID = SRCL.CLINT_ID AND SRAL.ACCNT_RANK_ID = SRAR.ACCNT_RANK_ID AND SRAL.SERVICE_PKG_ID = SRSP.SERVICE_PKG_ID "
                     + " AND SRAL.ACCNT_ID = SRMTH.ACCNT_ID(+) AND SRMTH.HIERARCHY_ACCNT_ID = HRAL.ACCNT_ID(+) "
                     + " AND HRAL.CLINT_ID = CLHR.CLINT_ID(+) AND SRMTH.UPDATED_BY = UPSU.SYS_USR_ID(+) AND "
                     + " UPSU.ACCNT_ID = UPAL.ACCNT_ID(+) AND UPAL.CLINT_ID = UPCL.CLINT_ID(+) AND SRAL.TERRITORY_RANK_ID = MTR.TERRITORY_RANK_ID(+) "
                     + " AND MTR.TERRITORY_RANK_ID = '150121000000000002' AND SRAR.ACCNT_RANK_ID = '120519000000000006' ";
            
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            dtvSearchAccInfo.DataSource = oDataSet;
            dtvSearchAccInfo.DataBind();

            if (dtvSearchAccInfo.Rows .Count == 0)
            {
                lblMsg.Text = "Not a TO account";
                return;
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // checking target already saved
            string strIfExistSql = " SELECT * FROM ACCOUNT_LIST AL, MANAGE_KPI_TARGET MKT WHERE AL.ACCNT_ID = MKT.TO_ACCNT_ID  AND AL.ACCNT_NO = '" + txtSearchTOAcc.Text.Trim() + "' AND MKT.TARGET_YEAR = '" + drpYear.SelectedValue + "' AND MKT.TARGET_MONTH= '"+DrpMonth.SelectedValue+"'";
            DataSet oSet = objServiceHandler.ExecuteQuery(strIfExistSql);
            if (oSet.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "TO Target Alredy Saved";
                return;
            }

            //lblMsg.Text = "";
            if (txtSearchTOAcc.Text == "")
            {
                lblMsg.Text = "Enter Correct TO No";
                return;
            }
            if (dtvSearchAccInfo.Rows.Count == 0)
            {
                lblMsg.Text = "Enter Correct TO No";
                return;
            }

            if (drpYear.SelectedValue == "0")
            {
                lblMsg.Text = "Select Year";
                return;
            }

            
            int intCustAcqTarget = Convert.ToInt32(txtCustAcqTar.Text.Trim());
           // int intDpsAcqTarget = Convert.ToInt32(txtDpsAccTar.Text.Trim());
            int intTargetAmoumt = Convert.ToInt32(txtTrxAmtTar.Text.Trim());
            int intActiveAgentno = Convert.ToInt32(txtActiveAgentNoTar.Text.Trim());
            int intAgentTrxAmount = Convert.ToInt32(txtActvAgntTrxtAmt.Text.Trim());
            int intCorporateCollectionAmount = Convert.ToInt32(txtLiftRfdTar.Text.Trim());
          //  int intComplianceTrget = Convert.ToInt32(txtComplianceTar.Text.Trim());
           // int intVisibilityTarget = Convert.ToInt32(txtVisibilityTar.Text.Trim());
            int intLiftingAmountTarget = Convert.ToInt32(txtLiftingTarget.Text.Trim());
            int intUtilityAmtTarget = Convert.ToInt32(txtUtilityBillPay.Text.Trim());

            string strUtArea = "";
            if (DrpArea.SelectedValue == "Utility Area")
            {
                strUtArea = "U";
            }
            else if (DrpArea.SelectedValue == "Non-Utility Area")
            {
                strUtArea = "NU";
                //txtUtilityBillPay.Text = "0";
                //txtUtilityBillPay.Enabled = false;
            }

            string strYear = drpYear.SelectedValue;
            string strRemarks = txtRemarks.Text.Trim();

            if (intCustAcqTarget < 50)
            {
                lblMsg.Text = "Customer Registration Count should be 50 or more";
                return;
            }

            string strGetAccId = objServiceHandler.GetAccountIdByMobileNo(txtSearchTOAcc.Text.Trim());

            //string[] monthOfYear = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //int count = 0;

            //for (int i = 0; i < monthOfYear.Length; i++ )
            //{
              //  string strMonth = monthOfYear[i].ToString();

            string strMonth =DrpMonth.SelectedValue;
                
                string strAddSuccMsg = objServiceHandler.AddToManageKPiTarget(strGetAccId, intCustAcqTarget, intTargetAmoumt, intActiveAgentno, intCorporateCollectionAmount,
                    intLiftingAmountTarget,intAgentTrxAmount, intUtilityAmtTarget, strMonth, strYear, strRemarks, strUtArea, Session["UserID"].ToString());
                if (strAddSuccMsg == "Successfull.")
                {
                    //count = count + 1;
                    lblMsg.Text = "Target Saved Successfully";
                }

                else
                {
                    lblMsg.Text = "Saving Process Failed";
                    return;
                }
                

                //if (count == 12)
                //{
                //    lblMsg.Text = "Target Saved Successfully";
                //    Clear();
                //    SaveAuditInfo("Add", "TOAccount:"+txtSearchTOAcc.Text+", Year: "+drpYear.SelectedValue);
                //}          


        }
        catch (Exception exception)
        {
            lblMsg.Text = exception.Message.ToString();
        }
    }

    private void Clear()
    {
        txtCustAcqTar.Text = "";
       // txtDpsAccTar.Text = "";
        txtTrxAmtTar.Text = "";
        txtActiveAgentNoTar.Text = "";
      //  txtAgtTrxAmtTar.Text = "";
        txtLiftRfdTar.Text = "";
       // txtComplianceTar.Text = "";
      //  txtVisibilityTar.Text = "";
        txtRemarks.Text = "";
        txtUtilityBillPay.Text = "";
        drpYear.SelectedValue = "0";
        DrpArea.SelectedValue = "0";
        txtLiftingTarget.Text = "";
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void Utarea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpArea.SelectedValue == "Non-Utility Area")
        {
            txtUtilityBillPay.Text = "0";
            txtUtilityBillPay.Enabled = false;
        }
         else if (DrpArea.SelectedValue == "Utility Area")
        {
            txtUtilityBillPay.Text = "";
            txtUtilityBillPay.Enabled = true;
        }


    }
}
