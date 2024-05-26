using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;


public partial class MIS_frmMBL_Various_MIS_Report_3 : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            PopulateDDForAccountRank();

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


            LoadCustCountDrp();
            DateTime dt = DateTime.Now;
            dtpDisPerIFDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt.AddDays(-1));
            DateTime dt2 = DateTime.Now;
            dtpDisPerIToDate.DateString = String.Format("{0:dd-MMM-yyyy}", dt2.AddDays(-1));
        }

        if (ddlDRank.SelectedValue == "120519000000000003")
        {
            txtDWalletId.Enabled = true;
        }

        else if (ddlDRank.SelectedValue == "120519000000000004")
        {
            txtDWalletId.Enabled = true;
        }

        else if (ddlDRank.SelectedValue == "120519000000000005")
        {
            txtDWalletId.Enabled = true;
        }

        else
        {
            // do nothing
        }

        LoadDistributorTextBox();


        if (rbtAllDis.SelectedValue == "AllDistributor")
        {
            ddlDRank.Enabled = false;
            txtDWalletId.Enabled = false;
            btnDWCL.Enabled = false;
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

    private void LoadCustCountDrp()
    {
        string strSqlD = "";
        strSqlD = " SELECT ACCNT_RANK_ID, RANK_TITEL  FROM ACCOUNT_RANK AR WHERE ACCNT_RANK_ID IN ('120519000000000003','120519000000000004','120519000000000005')";
        sdsRank.SelectCommand = strSqlD;
        sdsRank.DataBind();
        ddlDRank.DataBind();
        ddlDRank.Items.Insert(0, new ListItem("Select Rank"));

    }

    private void LoadDistributorTextBox()
    {
        if (ddlDistributor.SelectedValue == "1")
        {
            txtDisWallet.Visible = false;
            lblDisWallet.Visible = false;
        }

        else
        {
            txtDisWallet.Visible = true;
            lblDisWallet.Visible = true;
        }
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    #region Regitration Datewise Customer Registration And Verification List

    protected void btnDWCL_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            if (ddlDRank.SelectedItem.Text == "Select Rank")
            {
                lblMsg.Text = "Select Rank";
            }

            // FOR DISTRIBUTOR
            else if (ddlDRank.SelectedValue == "120519000000000003")
            {
                //--DISTINCT
                strSql = " SELECT  THA.DEL_ACCNT_NO DIS_NO, THA.A_ACCNT_NO AGENT_NO, ASD.CUSTOMER_MOBILE_NO CUSTOMER_MOBILE_NO, TRUNC(ASD.ACTIVATION_DATE) ACTIVATION_DATE, "
                         + " ASD.VERIFIED VERIFIED FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_SERIAL_DETAIL ASD WHERE THA.DEL_ACCNT_NO = '" + txtDWalletId.Text + "' "
                         + " AND THA.A_ACCNT_NO = SUBSTR(ASD.AGENT_MOBILE_NO, 4, 14)||'1' AND TRUNC(ASD.ACTIVATION_DATE) "
                         + " BETWEEN '" + dtpDisFrDate.DateString + "' AND '" + dtpDisToDate.DateString + "' ORDER BY ACTIVATION_DATE ASC ";

                txtDWalletId.Text = "";

                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "DIS_wise_Cust_Reg_N_VER_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor wise Customer Registration and Verification Report</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisFrDate.DateString + "' To '" + dtpDisToDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Agent</td>";
                strHTML = strHTML + "<td valign='middle' >Booth </td>";
                strHTML = strHTML + "<td valign='middle' >Customer </td>";
                strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
                strHTML = strHTML + "<td valign='middle' >Verified</td>";
                strHTML = strHTML + "</tr>";


                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUSTOMER_MOBILE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ACTIVATION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["VERIFIED"].ToString() + "</td>";

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

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "DIS_wise_Cust_Reg_N_VER_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";

            }

            // FOR DSE
            else if (ddlDRank.SelectedValue == "120519000000000004")
            {
                //DISTINCT
                strSql = " SELECT  THA.SA_ACCNT_NO DSE_NO, THA.A_ACCNT_NO AGENT_NO, ASD.CUSTOMER_MOBILE_NO, ASD.ACTIVATION_DATE, ASD.VERIFIED "
                         + " FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_SERIAL_DETAIL ASD WHERE THA.SA_ACCNT_NO = '" + txtDWalletId.Text + "' "
                         + " AND THA.A_ACCNT_NO = SUBSTR(ASD.AGENT_MOBILE_NO, 4, 14)||'1'  "
                         + " AND TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpDisFrDate.DateString + "' AND '" + dtpDisToDate.DateString + "' ORDER BY ASD.ACTIVATION_DATE ASC ";

                txtDWalletId.Text = "";

                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "DSE_wise_Cust_Reg_N_VER_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>DSE wise Customer Registration and Verification Report</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisFrDate.DateString + "' To '" + dtpDisToDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Sub Agent</td>";
                strHTML = strHTML + "<td valign='middle' >Booth </td>";
                strHTML = strHTML + "<td valign='middle' >Customer </td>";
                strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
                strHTML = strHTML + "<td valign='middle' >Verified</td>";
                strHTML = strHTML + "</tr>";


                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUSTOMER_MOBILE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ACTIVATION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["VERIFIED"].ToString() + "</td>";

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

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "DSE_wise_Cust_Reg_N_VER_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";
            }

            // FOR AQGENT
            else if (ddlDRank.SelectedValue == "120519000000000005")
            {
                //DISTINCT
                strSql = " SELECT  AL.ACCNT_NO AGENT_NO, ASD.CUSTOMER_MOBILE_NO, ASD.ACTIVATION_DATE ACTIVATION_DATE, ASD.VERIFIED VERIFIED "
                         + " FROM ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST AL WHERE AL.ACCNT_MSISDN = ASD.AGENT_MOBILE_NO "
                         + " AND AL.ACCNT_NO LIKE ('%" + txtDWalletId.Text + "%') AND "
                         + "TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpDisFrDate.DateString + "' AND '" + dtpDisToDate.DateString + "' " + " ORDER BY   ASD.ACTIVATION_DATE ASC ";



                txtDWalletId.Text = "";

                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "Agent_wise_Cust_Reg_N_VER_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent wise Customer Registration and Verification Report</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisFrDate.DateString + "' To '" + dtpDisToDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Booth </td>";
                strHTML = strHTML + "<td valign='middle' >Customer </td>";
                strHTML = strHTML + "<td valign='middle' >Registration Date</td>";
                strHTML = strHTML + "<td valign='middle' >Verified</td>";
                strHTML = strHTML + "</tr>";


                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CUSTOMER_MOBILE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ACTIVATION_DATE"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["VERIFIED"].ToString() + "</td>";

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

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "Agent_wise_Cust_Reg_N_VER_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";
            }
            txtDWalletId.Text = "";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    #region business collection report 3

    protected void btnBusColl3_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            #region old query

            //strSql = " SELECT THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, MTDIS.THANA_NAME DIS_THANA, "
            //         + " MDDIS.DISTRICT_NAME DIS_DISTRICT, SUM(DISTINCT TMIS.TRANSACTION_AMOUNT) SUM_TRX FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, "
            //         + " ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, "
            //         + " MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE TMIS.SERVICE_CODE = 'FM' AND TRUNC(TMIS.TRANSACTION_DATE) "
            //         + " BETWEEN '" + dtpBCLFDate.DateString + "' AND '" + dtpBCLToDate.DateString + "' AND TMIS.REQUEST_PARTY||'1' = ALDSE.ACCNT_NO "
            //         + " AND ALDSE.ACCNT_RANK_ID = '120519000000000004' AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND "
            //         + " ALCOR.ACCNT_RANK_ID = '140917000000000004' AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO  AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO "
            //         + " AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+)  AND MTDIS.THANA_ID = MDDIS.DISTRICT_ID(+) "
            //         + " GROUP BY THA.DEL_ACCNT_NO, CLDIS.CLINT_NAME, CLDIS.CLINT_ADDRESS1, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME ";

            #endregion

            //strSql = " SELECT DISTINCT TEMP.DIS_ACC_NO DISTRIBUTOR_NO, TEMP.DIS_NAME DISTRIBUTOR_NAME, TEMP.DIS_ADDR DISTRIBUTOR_ADDRESS, "
            //         + " TEMP.DIS_THANA DISTRIBUTOR_THANA, TEMP.DIS_DISTRICT DISTRIBUTOR_DISTRICT, SUM(TEMP.TRANSACTION_AMOUNT) SUM_TRX_AMT FROM "
            //         + " ( SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, "
            //         + " MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID "
            //         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, TEMP_HIERARCHY_LIST_ALL THA, "
            //         + " ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE TMIS.SERVICE_CODE = 'FM' "
            //         + " AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBCLFDate.DateString + "' AND '" + dtpBCLToDate.DateString + "' "
            //         + " AND TMIS.REQUEST_PARTY||'1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' "
            //         + " AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID = '140917000000000004' AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO  "
            //         + " AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+)  AND MTDIS.THANA_ID = MDDIS.DISTRICT_ID(+) "
            //         + " ORDER BY THA.DEL_ACCNT_NO  ASC) TEMP GROUP BY TEMP.DIS_ACC_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT ";


            //strSql = " SELECT DISTINCT TEMP.DIS_ACC_NO DISTRIBUTOR_NO, TEMP.DIS_NAME DISTRIBUTOR_NAME, TEMP.DIS_ADDR DISTRIBUTOR_ADDRESS, "
            //         + " TEMP.DIS_THANA DISTRIBUTOR_THANA, TEMP.DIS_DISTRICT DISTRIBUTOR_DISTRICT, SUM(TEMP.TRANSACTION_AMOUNT) SUM_TRX_AMT FROM "
            //         + " ( SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, "
            //         + " MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID "
            //         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, TEMP_HIERARCHY_LIST_ALL THA, "
            //         + " ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE TMIS.SERVICE_CODE = 'FM' "
            //         + " AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBCLFDate.DateString + "' AND '" + dtpBCLToDate.DateString + "' "
            //         + " AND TMIS.REQUEST_PARTY||'1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' "
            //         + " AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002','160306000000000001') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO  "
            //         + " AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+)  AND MTDIS.THANA_ID = MDDIS.DISTRICT_ID(+) "
            //         + " ORDER BY THA.DEL_ACCNT_NO  ASC) TEMP GROUP BY TEMP.DIS_ACC_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT ";


            //strSql = " SELECT DISTINCT TEMP.DIS_ACC_NO DISTRIBUTOR_NO, TEMP.DIS_NAME DISTRIBUTOR_NAME, TEMP.DIS_ADDR DISTRIBUTOR_ADDRESS, "
            //         + " TEMP.DIS_THANA DISTRIBUTOR_THANA, TEMP.DIS_DISTRICT DISTRIBUTOR_DISTRICT, SUM(TEMP.TRANSACTION_AMOUNT) SUM_TRX_AMT FROM "
            //        + " ( SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, "
            //         + " MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID "
            //         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, TEMP_HIERARCHY_LIST_ALL THA, "
            //         + " ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE TMIS.SERVICE_CODE = 'FM' "
            //         + " AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBCLFDate.DateString + "' AND '" + dtpBCLToDate.DateString + "' "
            //        + " AND TMIS.REQUEST_PARTY||'1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' "
            //         + " AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO  "
            //         + " AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+)  AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) "
            //        + " ORDER BY THA.DEL_ACCNT_NO  ASC) TEMP GROUP BY TEMP.DIS_ACC_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT ";

            //new --- strSql = " SELECT DISTINCT TEMP.DIS_ACC_NO DISTRIBUTOR_NO, TEMP.DIS_NAME DISTRIBUTOR_NAME, TEMP.DIS_ADDR DISTRIBUTOR_ADDRESS, "
            //         + " TEMP.DIS_THANA DISTRIBUTOR_THANA, TEMP.DIS_DISTRICT DISTRIBUTOR_DISTRICT, SUM(TEMP.TRANSACTION_AMOUNT) SUM_TRX_AMT FROM "
            //         + " ( SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, "
            //         + " MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID "
            //         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, TEMP_HIERARCHY_LIST_ALL THA, "
            //         + " ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE TMIS.SERVICE_CODE = 'FM' "
            //         + " AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBCLFDate.DateString + "' AND '" + dtpBCLToDate.DateString + "' "
            //         + " AND TMIS.REQUEST_PARTY||'1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004', '180128000000000007', '180305000000000004') "
            //         + " AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002', '180416000000000001', '180416000000000002') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO  "
            //         + " AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+)  AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) "
            //         + " ORDER BY THA.DEL_ACCNT_NO  ASC) TEMP GROUP BY TEMP.DIS_ACC_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT ";


            // updated by Md.Jahirul Islam
            // Date Jan-09-2022
            strSql = " SELECT DISTINCT TEMP.DIS_ACC_NO DISTRIBUTOR_NO, TEMP.DIS_NAME DISTRIBUTOR_NAME, TEMP.DIS_ADDR DISTRIBUTOR_ADDRESS, "
                     + " TEMP.DIS_THANA DISTRIBUTOR_THANA, TEMP.DIS_DISTRICT DISTRIBUTOR_DISTRICT, SUM(TEMP.TRANSACTION_AMOUNT) SUM_TRX_AMT FROM "
                     + " ( SELECT DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, "
                     + " MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID "
                     + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, TEMP_HIERARCHY_LIST_ALL THA, "
                     + " ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE TMIS.SERVICE_CODE = 'FM' "
                     + " AND TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBCLFDate.DateString + "' AND '" + dtpBCLToDate.DateString + "' "
                     + " AND TMIS.REQUEST_PARTY||'1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004', '180128000000000007', '180305000000000004','200813000000000002','200813000000000003','210930000000000001','140917000000000002') "
                     + " AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002', '180416000000000001', '180416000000000002','200813000000000002','200813000000000003','210930000000000001','140917000000000002') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO  "
                     + " AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+)  AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID(+) "
                     + " ORDER BY THA.DEL_ACCNT_NO  ASC) TEMP GROUP BY TEMP.DIS_ACC_NO, TEMP.DIS_NAME, TEMP.DIS_ADDR, TEMP.DIS_THANA, TEMP.DIS_DISTRICT ";



            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            double dblTotalSumAmount = 0;
            double dblTotalSumCommission = 0;
            double dblTotalSumVat = 0;
            double dblTotalSumAmountAfterVat = 0;
            double dblTotalSumAit = 0;
            double dblTotalSumAfterAit = 0;
            double dblTotalSumDistributorCommission = 0;
            double dblTotalSumMblCommission = 0;

            DataSet dtsAccount = new DataSet();
            fileName = "Bus_coll_Rpt3";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Business Collection Report 3</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBCLFDate.DateString + "' To '" + dtpBCLToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Distributor Account No </td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Address </td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >MBL MYCash Distributor District </td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount Sum</td>";
            strHTML = strHTML + "<td valign='middle' > Total Commission 0.20% </td>";
            strHTML = strHTML + "<td valign='middle' >VAT 15% </td>";
            strHTML = strHTML + "<td valign='middle' >Amount after VAT </td>";
            strHTML = strHTML + "<td valign='middle' >AIT 10% </td>";
            strHTML = strHTML + "<td valign='middle' >Amount after AIT</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commision Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Rate</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    double dblToalAmt = Convert.ToDouble(prow["SUM_TRX_AMT"]);
                    double dblTotalCommsssion = dblToalAmt * 0.0020;
                    dblTotalCommsssion = System.Math.Round(dblTotalCommsssion, 2);
                    double dblTotalIncusiveAmt = (dblTotalCommsssion * 100) / 115;
                    dblTotalIncusiveAmt = System.Math.Round(dblTotalIncusiveAmt, 2);
                    double dblVAT = dblTotalCommsssion - dblTotalIncusiveAmt;
                    dblVAT = System.Math.Round(dblVAT, 2);
                    double dblAmtAfterVAT = dblTotalCommsssion - dblVAT;
                    dblAmtAfterVAT = System.Math.Round(dblAmtAfterVAT, 2);
                    double dblAIT = dblAmtAfterVAT * 0.1;
                    dblAIT = System.Math.Round(dblAIT, 2);
                    double dblAmtAfterAIT = dblAmtAfterVAT - dblAIT;
                    dblAmtAfterAIT = System.Math.Round(dblAmtAfterAIT, 2);
                    string disCommRate = "60%";
                    double dblDistributorCommission = dblAmtAfterAIT * 0.6;
                    dblDistributorCommission = System.Math.Round(dblDistributorCommission, 2);
                    string mblCommRate = "40%";
                    double dblMBLCommission = dblAmtAfterAIT * 0.4;
                    dblMBLCommission = System.Math.Round(dblMBLCommission, 2);

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_ADDRESS"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTOR_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SUM_TRX_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + dblTotalCommsssion + "</td>";
                    strHTML = strHTML + " <td > '" + dblVAT + "</td>";
                    strHTML = strHTML + " <td > '" + dblAmtAfterVAT + "</td>";
                    strHTML = strHTML + " <td > '" + dblAIT + "</td>";
                    strHTML = strHTML + " <td > '" + dblAmtAfterAIT + "</td>";
                    strHTML = strHTML + " <td > '" + disCommRate + "</td>";
                    strHTML = strHTML + " <td > '" + dblDistributorCommission + "</td>";
                    strHTML = strHTML + " <td > '" + mblCommRate + "</td>";
                    strHTML = strHTML + " <td > '" + dblMBLCommission + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    dblTotalSumAmount = dblTotalSumAmount + Convert.ToDouble(prow["SUM_TRX_AMT"].ToString());
                    dblTotalSumCommission = dblTotalSumCommission + dblTotalCommsssion;
                    dblTotalSumVat = dblTotalSumVat + dblVAT;
                    dblTotalSumAmountAfterVat = dblTotalSumAmountAfterVat + dblAmtAfterVAT;
                    dblTotalSumAit = dblTotalSumAit + dblAIT;
                    dblTotalSumAfterAit = dblTotalSumAfterAit + dblAmtAfterAIT;
                    dblTotalSumDistributorCommission = dblTotalSumDistributorCommission + dblDistributorCommission;
                    dblTotalSumMblCommission = dblTotalSumMblCommission + dblMBLCommission;

                }
            }

            dblTotalSumAmount = System.Math.Round(dblTotalSumAmount, 2);
            dblTotalSumCommission = System.Math.Round(dblTotalSumCommission, 2);
            dblTotalSumVat = System.Math.Round(dblTotalSumVat, 2);
            dblTotalSumAmountAfterVat = System.Math.Round(dblTotalSumAmountAfterVat, 2);
            dblTotalSumAit = System.Math.Round(dblTotalSumAit, 2);
            dblTotalSumAfterAit = System.Math.Round(dblTotalSumAfterAit, 2);
            dblTotalSumDistributorCommission = System.Math.Round(dblTotalSumDistributorCommission, 2);
            dblTotalSumMblCommission = System.Math.Round(dblTotalSumMblCommission, 2);

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + dblTotalSumAmount.ToString() + " </td>";
            strHTML = strHTML + " <td > " + dblTotalSumCommission.ToString() + " </td>";
            strHTML = strHTML + " <td > " + dblTotalSumVat.ToString() + " </td>";
            strHTML = strHTML + " <td > " + dblTotalSumAmountAfterVat.ToString() + " </td>";
            strHTML = strHTML + " <td > " + dblTotalSumAit.ToString() + " </td>";
            strHTML = strHTML + " <td > " + dblTotalSumAfterAit.ToString() + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + dblTotalSumDistributorCommission.ToString() + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + dblTotalSumMblCommission.ToString() + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Bus_coll_Rpt3");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    #region All operator sms

    protected void btnAllOprSMS_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            int TotalSMSCount = 0;

            if (drpAllOpr.SelectedValue == "0")
            {
                lblMsg.Text = "Select an Operator";
                return;
            }

            else if (drpAllOpr.SelectedValue == "1")
            {
                strSql = " SELECT DISTINCT UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) SER_CODE, "
                     + " COUNT(SRR.REQUEST_ID ) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE TRUNC(SR.REQUEST_TIME) "
                     + " BETWEEN '" + dtpSMSAllFD.DateString + "' AND '" + dtpSMSAllToD.DateString + "' AND SR.REQUEST_ID = SRR.REQUEST_ID "
                     + " AND SRR.RESPONSE_STAE = 'D' AND SUBSTR(SRR.RECEIPENT_PARTY, 1,6) = '+88017'  "
                     + " GROUP BY UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) "
                     + " ORDER BY SER_CODE ASC ";

            }

            else if (drpAllOpr.SelectedValue == "2")
            {
                strSql = " SELECT DISTINCT UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) SER_CODE, "
                     + " COUNT(SRR.REQUEST_ID ) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE TRUNC(SR.REQUEST_TIME) "
                     + " BETWEEN '" + dtpSMSAllFD.DateString + "' AND '" + dtpSMSAllToD.DateString + "' AND SR.REQUEST_ID = SRR.REQUEST_ID "
                     + " AND SRR.RESPONSE_STAE = 'D' AND SUBSTR(SRR.RECEIPENT_PARTY, 1,6) = '+88019'  "
                     + " GROUP BY UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) "
                     + " ORDER BY SER_CODE ASC ";
            }

            else if (drpAllOpr.SelectedValue == "3")
            {
                strSql = " SELECT DISTINCT UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) SER_CODE, "
                     + " COUNT(SRR.REQUEST_ID ) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE TRUNC(SR.REQUEST_TIME) "
                     + " BETWEEN '" + dtpSMSAllFD.DateString + "' AND '" + dtpSMSAllToD.DateString + "' AND SR.REQUEST_ID = SRR.REQUEST_ID "
                     + " AND SRR.RESPONSE_STAE = 'D' AND SUBSTR(SRR.RECEIPENT_PARTY, 1,6) = '+88018'  "
                     + " GROUP BY UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) "
                     + " ORDER BY SER_CODE ASC ";
            }

            else if (drpAllOpr.SelectedValue == "4")
            {
                strSql = " SELECT DISTINCT UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) SER_CODE, "
                     + " COUNT(SRR.REQUEST_ID ) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE TRUNC(SR.REQUEST_TIME) "
                     + " BETWEEN '" + dtpSMSAllFD.DateString + "' AND '" + dtpSMSAllToD.DateString + "' AND SR.REQUEST_ID = SRR.REQUEST_ID "
                     + " AND SRR.RESPONSE_STAE = 'D' AND SUBSTR(SRR.RECEIPENT_PARTY, 1,6) = '+88016'  "
                     + " GROUP BY UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) "
                     + " ORDER BY SER_CODE ASC ";
            }

            else if (drpAllOpr.SelectedValue == "5")
            {
                strSql = " SELECT DISTINCT UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) SER_CODE, "
                     + " COUNT(SRR.REQUEST_ID ) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE TRUNC(SR.REQUEST_TIME) "
                     + " BETWEEN '" + dtpSMSAllFD.DateString + "' AND '" + dtpSMSAllToD.DateString + "' AND SR.REQUEST_ID = SRR.REQUEST_ID "
                     + " AND SRR.RESPONSE_STAE = 'D' AND SUBSTR(SRR.RECEIPENT_PARTY, 1,6) = '+88015'  "
                     + " GROUP BY UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) "
                     + " ORDER BY SER_CODE ASC ";
            }

            else if (drpAllOpr.SelectedValue == "6")
            {
                strSql = " SELECT DISTINCT UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) SER_CODE, "
                     + " COUNT(SRR.REQUEST_ID ) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE TRUNC(SR.REQUEST_TIME) "
                     + " BETWEEN '" + dtpSMSAllFD.DateString + "' AND '" + dtpSMSAllToD.DateString + "' AND SR.REQUEST_ID = SRR.REQUEST_ID "
                     + " AND SRR.RESPONSE_STAE = 'D' AND SUBSTR(SRR.RECEIPENT_PARTY, 1,6) = '+88011'  "
                     + " GROUP BY UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) "
                     + " ORDER BY SER_CODE ASC ";
            }

            else if (drpAllOpr.SelectedValue == "7")
            {
                strSql = " SELECT DISTINCT UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) SER_CODE, "
                     + " COUNT(SRR.REQUEST_ID ) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE TRUNC(SR.REQUEST_TIME) "
                     + " BETWEEN '" + dtpSMSAllFD.DateString + "' AND '" + dtpSMSAllToD.DateString + "' AND SR.REQUEST_ID = SRR.REQUEST_ID "
                     + " AND SRR.RESPONSE_STAE = 'D' GROUP BY UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) "
                     + " ORDER BY SER_CODE ASC ";
            }

            else
            {
                // do nothing
            }

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "AllSERWiseSMSCount_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Operator and All Service Code wise SMS Count</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpSMSAllFD.DateString + "' To '" + dtpSMSAllToD.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Service Code</td>";
            strHTML = strHTML + "<td valign='middle' >Respose Count</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SER_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["COUNT_SMS"].ToString() + " </td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    TotalSMSCount = TotalSMSCount + Convert.ToInt32(prow["COUNT_SMS"].ToString());

                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "Total" + " </td>";
            strHTML = strHTML + " <td > " + TotalSMSCount + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "AllSERWiseSMSCount_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    #region operator and service wise response

    protected void btnOprASerW_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string strCount = "";

            if (drpOperator.SelectedValue == "0" || drpService.SelectedValue == "0")
            {
                lblMsg.Text = "Select Operator/Service Code";
                return;
            }

            else if (drpOperator.SelectedValue != "0" && drpService.SelectedValue != "18")
            {
                strSql = " SELECT COUNT(SRR.REQUEST_ID) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE "
                     + " TRUNC(SR.REQUEST_TIME) BETWEEN '" + dtpOPRandSrCFD.DateString + "' AND '" + dtpOPRandSrCToD.DateString + "' AND "
                     + " SR.REQUEST_ID = SRR.REQUEST_ID AND SRR.RESPONSE_STAE = 'D' AND SUBSTR(SRR.RECEIPENT_PARTY, 1,6) = '" + drpOperator.SelectedValue + "' "
                     + " AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) = '" + drpService.SelectedItem.ToString() + "' ";

            }

            else if ((drpOperator.SelectedValue != "0" && drpService.SelectedValue == "18"))
            {
                strSql = " SELECT COUNT(SRR.REQUEST_ID) COUNT_SMS FROM SERVICE_REQUEST SR, SERVICE_RESPONSE SRR WHERE "
                     + " TRUNC(SR.REQUEST_TIME) BETWEEN '" + dtpOPRandSrCFD.DateString + "' AND '" + dtpOPRandSrCToD.DateString + "' AND "
                     + " SR.REQUEST_ID = SRR.REQUEST_ID AND SRR.RESPONSE_STAE = 'D' AND SUBSTR(SRR.RECEIPENT_PARTY, 1,6) = '" + drpOperator.SelectedValue + "' "
                     + " AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) NOT IN ('REG', 'MTP', 'OTP', 'RG', 'CCT', 'FM', 'CN', "
                     + " 'SW', 'IOTP', 'CPIN1', 'BI', 'QT', 'BD', 'FT', 'TXTFS', 'MP', 'MYHL' ) ";
            }

            strCount = objServiceHandler.GetRankCount(strSql);
            lblResult.Text = strCount;

        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    #region Customer trx count

    protected void btnCustTrx_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            //strSql = " SELECT DISTINCT AL.ACCNT_NO, AR.RANK_TITEL, ASD.ACTIVATION_DATE , CL.CLINT_NAME, CL.CLINT_ADDRESS1, MT.THANA_NAME, MD.DISTRICT_NAME, "
            //         + " APSNG101.PKG_MIS_REPORTS.FUNC_CUSTOMER_TRX_COUNT(AL.ACCNT_NO, '" + dtpCustTrxFDate.DateString + "', '" + dtpCustTrxToDate.DateString + "' ) CUST_TRX_COUNT "
            //         + " FROM ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD, CLIENT_LIST CL, ACCOUNT_RANK AR, MANAGE_THANA MT, MANAGE_DISTRICT MD "
            //         + " WHERE AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AR.ACCNT_RANK_ID IN ('120519000000000006', '130914000000000001') "
            //         + " AND TRUNC( ASD.ACTIVATION_DATE) BETWEEN '" + dtpCustTrxFDate.DateString + "' AND '" + dtpCustTrxToDate.DateString + "' AND AL.CLINT_ID = CL.CLINT_ID "
            //         + " AND CL.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID ORDER BY ASD.ACTIVATION_DATE DESC ";


            strSql = " SELECT DISTINCT AL.ACCNT_NO, AR.RANK_TITEL, ASD.ACTIVATION_DATE, CL.CLINT_NAME, CL.CLINT_ADDRESS1, MT.THANA_NAME, MD.DISTRICT_NAME, "
                    + " DECODE (CAB.CAS_ACCNT_BALANCE, NULL, '0', CAB.CAS_ACCNT_BALANCE) BALANCE, APSNG101.PKG_MIS_REPORTS.FUNC_CUSTOMER_TRX_COUNT(AL.ACCNT_NO, '" + dtpCustTrxFDate.DateString + "', '" + dtpCustTrxToDate.DateString + "' ) CUST_TRX_COUNT "
                    + " FROM   ACCOUNT_LIST AL, ACCOUNT_SERIAL_DETAIL ASD, CLIENT_LIST CL, ACCOUNT_RANK AR, MANAGE_THANA MT, MANAGE_DISTRICT MD, "
                    + " BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB WHERE AL.ACCNT_MSISDN = ASD.CUSTOMER_MOBILE_NO AND "
                    + " AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AR.ACCNT_RANK_ID IN ('120519000000000006', '130914000000000001') "
                    + " AND TRUNC (ASD.ACTIVATION_DATE) BETWEEN '" + dtpCustTrxFDate.DateString + "' AND '" + dtpCustTrxToDate.DateString + "' "
                    + " AND AL.CLINT_ID = CL.CLINT_ID AND CL.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID AND AL.ACCNT_NO = CAL.CAS_ACC_NO AND "
                    + " CAL.CAS_ACC_ID = CAB.CAS_ACC_ID ORDER BY   ASD.ACTIVATION_DATE DESC ";


            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Cust_trx_count_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Customer Transaction Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpCustTrxFDate.DateString + "' To '" + dtpCustTrxToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Wallet Id</td>";
            strHTML = strHTML + "<td valign='middle' >Rank </td>";
            strHTML = strHTML + "<td valign='middle' >Reg. Date </td>";
            strHTML = strHTML + "<td valign='middle' >Name</td>";
            strHTML = strHTML + "<td valign='middle' >Address</td>";
            strHTML = strHTML + "<td valign='middle' >Thana</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >Last Date Balance</td>";
            strHTML = strHTML + "<td valign='middle' >Trx Count</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCNT_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ACTIVATION_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_ADDRESS1"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["THANA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BALANCE"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["CUST_TRX_COUNT"].ToString() + "</td>";
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

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Cust_trx_count_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    #region agent transaction activity report

    protected void btnAgAct_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            string strDeleteSql = "";
            string strDeleteAgtActivityTbl = "";
            string strSqlForXlWrite = "";
            string distriButorWallet = txtDisWallet.Text.Trim();
            string strTrxNo = txtTrxCount.Text.Trim();
            string strTrxAmount = txtAmonut.Text.Trim();
            int intNoOfAgent = Convert.ToInt32(txtNoOfAgent.Text);

            if (ddlDistributor.SelectedValue == "1")
            {
                // delete data from table TEMP_SA_ACCNT_NO_LIST
                strDeleteSql = " DELETE FROM TEMP_SA_ACCNT_NO_LIST";
                DataSet ods = objServiceHandler.ExecuteQuery(strDeleteSql);

                // load data into table TEMP_SA_ACCNT_NO_LIST where dse have 10(as example) agent
                objServiceHandler.ExeProForTblTempSAAccList(intNoOfAgent);

                #region old query 1

                //strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, MTD.THANA_NAME DIS_THANA, "
                //         + " MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, THA.A_ACCNT_NO AGENT_NO, CLA.CLINT_NAME AGENT_NAME, "
                //         + " ARA.RANK_TITEL AGENT_RANK, ARA.PARTY_TYPE AGENT_TYPE, CABA.CAS_ACCNT_BALANCE AGENT_BALANCE, "
                //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_TRX_AMT ( ALA.ACCNT_NO, '" + dtpAgActFDate.DateString + "', '" + dtpAgActToDate.DateString + "', '" + intTrxNo + "','" + intTrxAmount + "' ) TRX_AMOUNT, " 
                //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_TRX_CNT ( ALA.ACCNT_NO, '" + dtpAgActFDate.DateString + "', '" + dtpAgActToDate.DateString + "', '" + intTrxNo + "','" + intTrxAmount + "' ) TRX_COUNT  "
                //         + " FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, "
                //         + " ACCOUNT_LIST ALA, CLIENT_LIST CLA, ACCOUNT_RANK ARA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CALA, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CABA "
                //         + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID "
                //         + " AND MTD.DISTRICT_ID = MDD.DISTRICT_ID AND THA.A_ACCNT_NO = ALA.ACCNT_NO AND ALA.CLINT_ID = CLA.CLINT_ID "
                //         + " AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID AND ALA.ACCNT_NO = CALA.CAS_ACC_NO(+) AND CALA.CAS_ACC_ID = CABA.CAS_ACC_ID(+) "
                //         + " ORDER BY THA.DEL_ACCNT_NO ASC ";

                #endregion

                #region old query 2

                //strSql = " SELECT THA.DEL_ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, "
                //         + " THA.A_ACCNT_NO AGT_NO, CLAGT.CLINT_NAME AGT_NAME, ARAGT.RANK_TITEL AGT_RANK, ARAGT.PARTY_TYPE AGT_TYPE, CABAG.CAS_ACCNT_BALANCE AGT_BALANCE, "
                //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_TRX_CNT (ALAGT.ACCNT_NO, '" + dtpAgActFDate.DateString + "', '" + dtpAgActToDate.DateString + "','" + intTrxNo + "','" + intTrxAmount + "')TRX_COUNT, " 
                //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_TRX_AMT (ALAGT.ACCNT_NO,'" + dtpAgActFDate.DateString + "', '" + dtpAgActToDate.DateString + "','" + intTrxNo + "','" + intTrxAmount + "')TRX_AMOUNT "
                //         + " FROM TEMP_HIERARCHY_LIST_ALL THA, TEMP_SA_ACCNT_NO_LIST TEMP, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, "
                //         + " MANAGE_DISTRICT MDDIS, ACCOUNT_LIST ALAGT, CLIENT_LIST CLAGT, ACCOUNT_RANK ARAGT, BDMIT_ERP_101.CAS_ACCOUNT_LIST CALAG, "
                //         + " BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CABAG WHERE THA.SA_ACCNT_NO = TEMP.TEMP_SA_ACCNT_NO AND THA.DEL_ACCNT_NO = ALDIS.ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID  "
                //         + " AND CLDIS.THANA_ID = MTDIS.THANA_ID AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID AND THA.A_ACCNT_NO = ALAGT.ACCNT_NO "
                //         + " AND ALAGT.CLINT_ID = CLAGT.CLINT_ID AND ALAGT.ACCNT_RANK_ID = ARAGT.ACCNT_RANK_ID AND ALAGT.ACCNT_NO = CALAG.CAS_ACC_NO(+) "
                //         + " AND CALAG.CAS_ACC_ID = CABAG.CAS_ACC_ID(+) ";

                #endregion

                // delete data from table TEMP_AGT_ACTIVITY_RPT 
                strDeleteAgtActivityTbl = " DELETE FROM TEMP_AGT_ACTIVITY_RPT";
                DataSet ods1 = objServiceHandler.ExecuteQuery(strDeleteAgtActivityTbl);

                //// load data into table TEMP_AGT_ACTIVITY_RPT as per format with condition of agent trx count, amount and date range
                string strSqlAgtAcRpt = "GEN_AGT_ACTV_RPT('" + dtpAgActFDate.DateString + "','" + dtpAgActToDate.DateString + "', " + strTrxNo + "," + strTrxAmount + ")";
                string strMSG = objServiceHandler.ExecuteProcedure(strSqlAgtAcRpt);

                if (strMSG == "Successfull.")
                {
                    strSqlForXlWrite = " SELECT DIS_NO, DIS_NAME, DIS_THANA, DIS_DISTRICT, DSE_NO, AGT_NO, AGT_NAME, "
                    + " AGT_RANK, AGT_TYPE, DECODE( AGT_BALANCE, '', 0, AGT_BALANCE) AGT_BALANCE, TRX_COUNT, "
                    + " TRX_AMOUNT FROM TEMP_AGT_ACTIVITY_RPT ORDER BY DSE_NO ASC";

                }

            }
            else
            {
                strDeleteSql = " DELETE FROM TEMP_SA_ACCNT_NO_LIST";
                DataSet ods = objServiceHandler.ExecuteQuery(strDeleteSql);

                objServiceHandler.ExeProForTblTempSAAccListWithDistriButor(intNoOfAgent, distriButorWallet);

                strDeleteAgtActivityTbl = " DELETE FROM TEMP_AGT_ACTIVITY_RPT";
                DataSet ods1 = objServiceHandler.ExecuteQuery(strDeleteAgtActivityTbl);

                //// load data into table TEMP_AGT_ACTIVITY_RPT as per format with condition of agent trx count, amount and date range
                string strSqlAgtAcRpt = "GEN_AGT_ACTV_RPT('" + dtpAgActFDate.DateString + "','" + dtpAgActToDate.DateString + "', " + strTrxNo + "," + strTrxAmount + ")";
                string strMSG = objServiceHandler.ExecuteProcedure(strSqlAgtAcRpt);

                if (strMSG == "Successfull.")
                {
                    strSqlForXlWrite = " SELECT DIS_NO, DIS_NAME, DIS_THANA, DIS_DISTRICT, DSE_NO, AGT_NO, AGT_NAME, "
                    + " AGT_RANK, AGT_TYPE, DECODE( AGT_BALANCE, '', 0, AGT_BALANCE) AGT_BALANCE, TRX_COUNT, "
                    + " TRX_AMOUNT FROM TEMP_AGT_ACTIVITY_RPT ORDER BY DSE_NO ASC";
                }

                #region old query 1

                //strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, MTD.THANA_NAME DIS_THANA, "
                //         + " MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, THA.A_ACCNT_NO AGENT_NO, CLA.CLINT_NAME AGENT_NAME, "
                //         + " ARA.RANK_TITEL AGENT_RANK, ARA.PARTY_TYPE AGENT_TYPE, CABA.CAS_ACCNT_BALANCE AGENT_BALANCE, "
                //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_TRX_AMT ( ALA.ACCNT_NO, '" + dtpAgActFDate.DateString + "', '" + dtpAgActToDate.DateString + "', '" + intTrxNo + "','" + intTrxAmount + "' ) TRX_AMOUNT, "
                //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_TRX_CNT ( ALA.ACCNT_NO, '" + dtpAgActFDate.DateString + "', '" + dtpAgActToDate.DateString + "', '" + intTrxNo + "','" + intTrxAmount + "' ) TRX_COUNT "
                //         + " FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, "
                //         + " ACCOUNT_LIST ALA, CLIENT_LIST CLA, ACCOUNT_RANK ARA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CALA, "
                //         + " BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CABA WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID "
                //         + " AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID AND THA.A_ACCNT_NO = ALA.ACCNT_NO "
                //         + " AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_RANK_ID = ARA.ACCNT_RANK_ID AND ALA.ACCNT_NO = CALA.CAS_ACC_NO(+)  "
                //         + " AND CALA.CAS_ACC_ID = CABA.CAS_ACC_ID(+) AND THA.DEL_ACCNT_NO = '" + txtDisWallet.Text.Trim() + "' ORDER BY THA.DEL_ACCNT_NO ASC ";

                #endregion

                #region old query 2

                //strSql = " SELECT THA.DEL_ACCNT_NO DIS_NO, CLDIS.CLINT_NAME DIS_NAME, MTDIS.THANA_NAME, MDDIS.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, "
                //         + " THA.A_ACCNT_NO AGT_NO, CLAGT.CLINT_NAME AGT_NAME, ARAGT.RANK_TITEL AGT_RANK, ARAGT.PARTY_TYPE AGT_TYPE, CABAG.CAS_ACCNT_BALANCE AGT_BALANCE, "
                //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_TRX_CNT (ALAGT.ACCNT_NO, '" + dtpAgActFDate.DateString + "', '" + dtpAgActToDate.DateString + "','" + intTrxNo + "','" + intTrxAmount + "')TRX_COUNT, "
                //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_TRX_AMT (ALAGT.ACCNT_NO,'" + dtpAgActFDate.DateString + "', '" + dtpAgActToDate.DateString + "','" + intTrxNo + "','" + intTrxAmount + "')TRX_AMOUNT "
                //         + " FROM TEMP_HIERARCHY_LIST_ALL THA, TEMP_SA_ACCNT_NO_LIST TEMP, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, "
                //         + " MANAGE_DISTRICT MDDIS, ACCOUNT_LIST ALAGT, CLIENT_LIST CLAGT, ACCOUNT_RANK ARAGT, BDMIT_ERP_101.CAS_ACCOUNT_LIST CALAG, "
                //         + " BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CABAG WHERE THA.SA_ACCNT_NO = TEMP.TEMP_SA_ACCNT_NO AND THA.DEL_ACCNT_NO = ALDIS.ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID  "
                //         + " AND CLDIS.THANA_ID = MTDIS.THANA_ID AND MTDIS.DISTRICT_ID = MDDIS.DISTRICT_ID AND THA.A_ACCNT_NO = ALAGT.ACCNT_NO "
                //         + " AND ALAGT.CLINT_ID = CLAGT.CLINT_ID AND ALAGT.ACCNT_RANK_ID = ARAGT.ACCNT_RANK_ID AND ALAGT.ACCNT_NO = CALAG.CAS_ACC_NO(+) "
                //         + " AND CALAG.CAS_ACC_ID = CABAG.CAS_ACC_ID(+) ";

                #endregion

            }

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Agt_Trx_Acti_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSqlForXlWrite);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Transaction Activity Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=13 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpAgActFDate.DateString + "' To '" + dtpAgActToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Type</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Balance</td>";
            strHTML = strHTML + "<td valign='middle' >No of Trx</td>";
            strHTML = strHTML + "<td valign='middle' >Amount of Trx</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_RANK"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_TYPE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_BALANCE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_COUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    //if (SerialNo == 13500)
                    //{
                    //    lblMsg.Text = "";
                    //}

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

            SaveAuditInfo("Preview", "Agt_Trx_Acti_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    #region agent trx activity report

    protected void btnAgtActv2_Click(object sender, EventArgs e)
    {
        try
        {
            int intNoOfAgent = Convert.ToInt32(txtAgtNoOfAgt2.Text);
            string strDeleteSql = "";
            string strSql = "";
            // delete data from table TEMP_SA_ACCNT_NO_LIST
            strDeleteSql = " DELETE FROM TEMP_SA_ACCNT_NO_LIST";
            DataSet ods = objServiceHandler.ExecuteQuery(strDeleteSql);

            // load data into table TEMP_SA_ACCNT_NO_LIST where dse have 10(as example) agent
            objServiceHandler.ExeProForTblTempSAAccList(intNoOfAgent);

            strSql = " SELECT DISTINCT ALD.ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, MTD.THANA_NAME DIS_THANA, "
                     + " MDD.DISTRICT_NAME DIS_DISTRICT, TMP.TEMP_SA_ACCNT_NO DSE_NO, TMP.NO_OF_A_ACCNT_NO AGENT_COUNT "
                     + " FROM TEMP_SA_ACCNT_NO_LIST TMP, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, "
                     + " MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE THA.SA_ACCNT_NO = TMP.TEMP_SA_ACCNT_NO AND "
                     + " THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID "
                     + " AND MTD.DISTRICT_ID = MDD.DISTRICT_ID ORDER BY  DSE_NO ASC ";

            txtAgtNoOfAgt2.Text = "";
            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Agt_Trx_Acti_Rpt2";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent Transaction Activity Report 2</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=7 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Print Date: '" + DateTime.Now.ToString("d") + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >DSE Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Active Agent</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AGENT_COUNT"].ToString() + "</td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Agt_Trx_Acti_Rpt2");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";



        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    #region business collection report 3 without distributor


    protected void btnDWCLAllDis_Click(object sender, EventArgs e)
    {
        try
        {
            //  for all distributor
            if (rbtAllDis.SelectedValue == "AllDistributor")
            {
                string strSqlA = "";
                strSqlA = " SELECT DISTINCT MR.REGION_NAME, MA.AREA_NAME, ALTM.ACCNT_NO TM_NO, CLTM.CLINT_NAME TM_NAME, "
                        + " ALTO.ACCNT_NO TO_NO, CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, "
                        + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT,  TMPA.AGT_NO, 'MBL AGENT' AGT_RANK, "
                        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG(TMPA.AGT_NO, '" + dtpDisFrDate.DateString + "',  '" + dtpDisToDate.DateString + "' ) RG_COUNT, "
                        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_VRFD(TMPA.AGT_NO, '" + dtpDisFrDate.DateString + "',  '" + dtpDisToDate.DateString + "' ) VRF_COUNT, "
                        + " APSNG101.PKG_MIS_REPORTS.FUNC_AGENT_WISE_CUST_VRF_CN(TMPA.AGT_NO, '" + dtpDisFrDate.DateString + "',  '" + dtpDisToDate.DateString + "' ) CN_COUNT "
                        + " FROM (SELECT DISTINCT ALA.ACCNT_NO AGT_NO FROM ACCOUNT_SERIAL_DETAIL ASD, ACCOUNT_LIST ALA "
                        + " WHERE TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpDisFrDate.DateString + "' AND '" + dtpDisToDate.DateString + "' "
                        + " AND ASD.BANK_CODE = 'MBL' AND ASD.AGENT_MOBILE_NO = ALA.ACCNT_MSISDN AND ALA.ACCNT_RANK_ID = '120519000000000005') TMPA, "
                        + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, "
                        + " MANAGE_TERRITORY_HIERARCHY MTHD, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTHTO, "
                        + " ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, MANAGE_REGION MR "
                        + " WHERE TMPA.AGT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) "
                        + " AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND ALD.ACCNT_ID = MTHD.ACCNT_ID(+) "
                        + " AND MTHD.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) "
                        + " AND MTHD.HIERARCHY_ACCNT_ID = MTHTO.ACCNT_ID(+) AND MTHTO.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) "
                        + " AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) "
                        + " AND MA.REGION_ID = MR.REGION_ID(+) ORDER BY THA.DEL_ACCNT_NO ASC ";


                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "Agent_wise_Cust_Reg_N_VER_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSqlA);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent wise Customer Registration and Verification Report</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisFrDate.DateString + "' To '" + dtpDisToDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Region </td>";
                strHTML = strHTML + "<td valign='middle' >Area </td>";
                strHTML = strHTML + "<td valign='middle' >TM No</td>";
                strHTML = strHTML + "<td valign='middle' >TM Name</td>";

                strHTML = strHTML + "<td valign='middle' >TO No </td>";
                strHTML = strHTML + "<td valign='middle' >TO Name </td>";
                strHTML = strHTML + "<td valign='middle' >Distributor No</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
                strHTML = strHTML + "<td valign='middle' >Distributor Thana </td>";
                strHTML = strHTML + "<td valign='middle' >Distributor District </td>";

                strHTML = strHTML + "<td valign='middle' >Agent NO</td>";
                strHTML = strHTML + "<td valign='middle' >Agent Rank</td>";
                strHTML = strHTML + "<td valign='middle' >Registration Count</td>";
                strHTML = strHTML + "<td valign='middle' >Verification Count</td>";
                strHTML = strHTML + "<td valign='middle' >CashIn Count</td>";


                strHTML = strHTML + "</tr>";


                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";



                        strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";



                        strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGT_RANK"].ToString() + "</td>";


                        strHTML = strHTML + " <td > '" + prow["RG_COUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["VRF_COUNT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CN_COUNT"].ToString() + "</td>";


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

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                SaveAuditInfo("Preview", "Agent_wise_Cust_Reg_N_VER_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";



            }

            else
            {
                // do nothing
                lblMsg.Text = "Please Select All Distributor";
                return;
            }


        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }



    protected void btnBus3WtDis_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            //strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, TM.* FROM TEMP_HIERARCHY_LIST_ALL THA, "
            //         + " ( SELECT TMIS.REQUEST_ID REQ_ID, TMIS.TRANSACTION_DATE TRX_DATE, TMIS.SERVICE_CODE SER_CODE, "
            //         + " ALDSE.ACCNT_NO DSE_NO, ALCORP.ACCNT_NO CORP_NO, TMIS.TRANSACTION_AMOUNT TRX_AMT "
            //         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCORP "
            //         + " WHERE TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBus3WtDisFDate.DateString + "' "
            //         + " AND '" + dtpBus3WtDisToDate.DateString + "' AND TMIS.SERVICE_CODE = 'FM' AND "
            //         + " TMIS.REQUEST_PARTY||1 = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' AND "
            //         + " TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO AND ALCORP.ACCNT_RANK_ID = '140917000000000004'  ) TM "
            //         + " WHERE TM.DSE_NO = THA.SA_ACCNT_NO(+) ORDER BY TM.TRX_DATE ASC ";


            //strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, TM.* FROM TEMP_HIERARCHY_LIST_ALL THA, "
            //         + " ( SELECT TMIS.REQUEST_ID REQ_ID, TMIS.TRANSACTION_DATE TRX_DATE, TMIS.SERVICE_CODE SER_CODE, "
            //         + " ALDSE.ACCNT_NO DSE_NO, ALCORP.ACCNT_NO CORP_NO, TMIS.TRANSACTION_AMOUNT TRX_AMT "
            //         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCORP "
            //         + " WHERE TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBus3WtDisFDate.DateString + "' "
            //         + " AND '" + dtpBus3WtDisToDate.DateString + "' AND TMIS.SERVICE_CODE = 'FM' AND "
            //         + " TMIS.REQUEST_PARTY||1 = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' AND "
            //         + " TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO AND ALCORP.ACCNT_RANK_ID IN ('140917000000000004','160306000000000002','160306000000000001')  ) TM "
            //         + " WHERE TM.DSE_NO = THA.SA_ACCNT_NO(+) ORDER BY TM.TRX_DATE ASC ";

            //strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, TM.* FROM TEMP_HIERARCHY_LIST_ALL THA, "
            //         + " ( SELECT TMIS.REQUEST_ID REQ_ID, TMIS.TRANSACTION_DATE TRX_DATE, TMIS.SERVICE_CODE SER_CODE, "
            //         + " ALDSE.ACCNT_NO DSE_NO, ALCORP.ACCNT_NO CORP_NO, TMIS.TRANSACTION_AMOUNT TRX_AMT "
            //         + " FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCORP "
            //         + " WHERE TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBus3WtDisFDate.DateString + "' "
            //         + " AND '" + dtpBus3WtDisToDate.DateString + "' AND TMIS.SERVICE_CODE = 'FM' AND "
            //         + " TMIS.REQUEST_PARTY||1 = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID = '120519000000000004' AND "
            //         + " TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO AND ALCORP.ACCNT_RANK_ID IN ('140917000000000004','160306000000000002')  ) TM "
            //         + " WHERE TM.DSE_NO = THA.SA_ACCNT_NO(+) ORDER BY TM.TRX_DATE ASC ";

            //strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, ALT.ACCNT_NO TO_NO, CLT.CLINT_NAME TO_NAME,"
            //        + " TM.* FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_TERRITORY_HIERARCHY MTH, ACCOUNT_LIST ALT,"
            //        + " CLIENT_LIST CLT, ( SELECT TMIS.REQUEST_ID REQ_ID, TMIS.TRANSACTION_DATE TRX_DATE, TMIS.SERVICE_CODE SER_CODE, ALDSE.ACCNT_NO DSE_NO,"
            //        + " ALCORP.ACCNT_NO CORP_NO, TMIS.TRANSACTION_AMOUNT TRX_AMT FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCORP"
            //        + " WHERE TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBus3WtDisFDate.DateString + "' AND '" + dtpBus3WtDisToDate.DateString + "'"
            //        + " AND TMIS.SERVICE_CODE = 'FM' AND  TMIS.REQUEST_PARTY||1 = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004','180128000000000007','180305000000000004')"
            //        + " AND  TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO AND ALCORP.ACCNT_RANK_ID IN ('140917000000000004','160306000000000002','180416000000000001','180416000000000002')) TM"
            //        + " WHERE TM.DSE_NO = THA.SA_ACCNT_NO(+) AND THA.DEL_ACCNT_ID = ALD.ACCNT_ID AND ALD.CLINT_ID = CLD.CLINT_ID AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID(+)"
            //        + " AND MTH.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) ORDER BY TM.TRX_DATE ASC  ";

            strSql = "SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, MA.AREA_NAME, ALT.ACCNT_NO TO_NO, CLT.CLINT_NAME TO_NAME,TM.* FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_TERRITORY_HIERARCHY MTH, MANAGE_TERRITORY_HIERARCHY MTH1, ACCOUNT_LIST ALT, CLIENT_LIST CLT, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, (SELECT TMIS.REQUEST_ID REQ_ID, TMIS.TRANSACTION_DATE TRX_DATE, TMIS.SERVICE_CODE SER_CODE, ALDSE.ACCNT_NO DSE_NO,ALCORP.ACCNT_NO CORP_NO, TMIS.TRANSACTION_AMOUNT TRX_AMT FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCORP WHERE TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + dtpBus3WtDisFDate.DateString + "' AND '" + dtpBus3WtDisToDate.DateString + "' AND TMIS.SERVICE_CODE = 'FM' AND  TMIS.REQUEST_PARTY||1 = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004','180128000000000007','180305000000000004') AND TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO AND ALCORP.ACCNT_RANK_ID IN ('140917000000000004','160306000000000002','180416000000000001','180416000000000002')) TM WHERE TM.DSE_NO = THA.SA_ACCNT_NO(+) AND THA.DEL_ACCNT_ID = ALD.ACCNT_ID AND ALD.CLINT_ID = CLD.CLINT_ID AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND ALT.ACCNT_ID = MTH1.ACCNT_ID(+) AND MTH1.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) ORDER BY TM.TRX_DATE ASC";


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Bus_Coll3_Dtl_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Business Collection Report 3 (Details)</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBus3WtDisFDate.DateString + "' To '" + dtpBus3WtDisToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO No</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date </td>";
            strHTML = strHTML + "<td valign='middle' >Service Code</td>";
            strHTML = strHTML + "<td valign='middle' >DSE No</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate/Corporate Sub Agent No</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQ_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRX_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SER_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Bus_Coll3_Dtl_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion


    #region ProvitaReport
    //added by chamak
    protected void btnProvita_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            //strSql = "SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, MA.AREA_NAME, ALT.ACCNT_NO TO_NO, CLT.CLINT_NAME TO_NAME,TM.* FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_TERRITORY_HIERARCHY MTH, MANAGE_TERRITORY_HIERARCHY MTH1, ACCOUNT_LIST ALT, CLIENT_LIST CLT, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA, (SELECT TMIS.REQUEST_ID REQ_ID, TMIS.TRANSACTION_DATE TRX_DATE, TMIS.SERVICE_CODE SER_CODE, ALDSE.ACCNT_NO DSE_NO,ALCORP.ACCNT_NO CORP_NO, TMIS.TRANSACTION_AMOUNT TRX_AMT FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCORP WHERE TRUNC(TMIS.TRANSACTION_DATE) BETWEEN '" + fromDateProvita.DateString + "' AND '" + toDateProvita.DateString + "' AND TMIS.SERVICE_CODE = 'FM' AND  TMIS.REQUEST_PARTY||1 = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004') AND TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO AND ALCORP.ACCNT_RANK_ID IN ('190519000000000003')) TM WHERE TM.DSE_NO = THA.SA_ACCNT_NO(+) AND THA.DEL_ACCNT_ID = ALD.ACCNT_ID AND ALD.CLINT_ID = CLD.CLINT_ID AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND ALT.ACCNT_ID = MTH1.ACCNT_ID(+)AND MTH1.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) ORDER BY TM.TRX_DATE ASc";

            strSql = "SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, MDD.DISTRICT_NAME AREA_NAME, ALT.ACCNT_NO TO_NO, CLT.CLINT_NAME TO_NAME,TM.* FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_TERRITORY_HIERARCHY MTH, MANAGE_TERRITORY_HIERARCHY MTH1, ACCOUNT_LIST ALT, CLIENT_LIST CLT, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA,MANAGE_THANA MTD, MANAGE_DISTRICT MDD, (SELECT TMIS.REQUEST_ID REQ_ID, TMIS.TRANSACTION_DATE TRX_DATE, TMIS.SERVICE_CODE SER_CODE, ALDSE.ACCNT_NO DSE_NO,ALCORP.ACCNT_NO CORP_NO, TMIS.TRANSACTION_AMOUNT TRX_AMT FROM TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCORP WHERE TRUNC(TMIS.TRANSACTION_DATE)BETWEEN '" + fromDateProvita.DateString + "' AND '" + toDateProvita.DateString + "' AND TMIS.SERVICE_CODE = 'FM' AND  TMIS.REQUEST_PARTY||1 = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004') AND TMIS.RECEPENT_PARTY = ALCORP.ACCNT_NO AND ALCORP.ACCNT_RANK_ID IN ('190519000000000003')) TM WHERE TM.DSE_NO = THA.SA_ACCNT_NO(+) AND THA.DEL_ACCNT_ID = ALD.ACCNT_ID AND ALD.CLINT_ID = CLD.CLINT_ID AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID(+) AND MTH.HIERARCHY_ACCNT_ID = ALT.ACCNT_ID(+) AND ALT.CLINT_ID = CLT.CLINT_ID(+) AND ALT.ACCNT_ID = MTH1.ACCNT_ID(+)AND MTH1.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) AND CLD.THANA_ID = MTD.THANA_ID(+)ORDER BY TM.TRX_DATE ASc";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Provita_Bus_Coll_Dtl_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Provita Business Collection Report (Details)</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + fromDateProvita.DateString + "' To '" + toDateProvita.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Area Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO No</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date </td>";
            strHTML = strHTML + "<td valign='middle' >Service Code</td>";
            strHTML = strHTML + "<td valign='middle' >DSE No</td>";
            strHTML = strHTML + "<td valign='middle' >Corporate/Corporate Sub Agent No</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQ_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRX_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SER_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Provita Report");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    #endregion

    #region distributor wise agent performance report

    protected void btnDisPer_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            double dblTotalRgCount = 0;
            double dblCommRgCount = 0;
            double dblCasInCount = 0;
            double dblCasInAmount = 0;
            double dblCasOutCount = 0;
            double dblCasOutAmonut = 0;
            double dblBdCount = 0;
            double dblBdAmount = 0;
            double dblBpCount = 0;
            double dblBpAmount = 0;

            DateTime dt1 = dtpDisPerIFDate.Date;
            DateTime dt2 = dtpDisPerIToDate.Date;
            double dateCount = (dt2 - dt1).TotalDays;

            if (dateCount <= 30)
            {

                strSql = " SELECT DISTINCT ALD.ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, MTD.THANA_NAME DIS_THANA, "
                         + " MDD.DISTRICT_NAME DIS_DISTRICT, THA.SA_ACCNT_NO DSE_NO, THA.A_ACCNT_NO AGENT_NO, CLA.CLINT_NAME AGENT_NAME, "
                         + " DECODE(CAB.CAS_ACCNT_BALANCE, NULL , '0', CAB.CAS_ACCNT_BALANCE) AGT_BAL, "
                         + " PKG_MIS_REPORTS.FUNC_AGENT_WISE_TOT_CUST_RG (THA.A_ACCNT_NO, TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')),TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')))NO_OF_REG, "
                         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_VERI_RG_CNT (THA.A_ACCNT_NO, TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')) ) COMMI_RG_COUNT, "
                         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (THA.A_ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')),'CN')NO_OF_CASIN, "
                         + " PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (THA.A_ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')),'CN')CASIN_AMT, "
                         + " PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOTAL_SRV_RECV (THA.A_ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')),'CCT,SW')NO_OF_CASOUT, "
                         + " PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_RCV (THA.A_ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')),'CCT,SW')CASOUT_AMT, "
                         + " PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_SRV_INITITE (THA.A_ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')),'BD')NO_OF_BD, "
                         + " PKG_MIS_REPORTS.FUNC_AGNT_WISE_TOT_AMT_SRV_INT (THA.A_ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')),'BD')BD_AMT, "
                         + " PKG_MIS_REPORTS.FUNC_NO_OF_DIS_LIFTING_NEW (ALD.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "'))) COUNT_LIFTING, "
                         + " PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_LIFTING_NEW (ALD.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "'))) AMOUNT_LIFTING, "
                         + " PKG_MIS_REPORTS.FUNC_NO_OF_DIS_REFUND_NEW (ALD.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "'))) COUNT_REFUND, "
                         + " PKG_MIS_REPORTS.FUNC_AMT_OF_DIS_REFUND_NEW (ALD.ACCNT_NO,TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "'))) AMOUNT_REFUND, "
                         + " PKG_MIS_REPORTS.FUNC_AGT_BP_CNT (THA.A_ACCNT_NO, TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')))NO_OF_BP, "
                         + " PKG_MIS_REPORTS.FUNC_AGT_BP_AMT (THA.A_ACCNT_NO, TO_CHAR(TO_DATE('" + dtpDisPerIFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisPerIToDate.DateString + "')))AMOUNT_BP "
                         + " FROM ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALA, "
                         + " CLIENT_LIST CLA, BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.CAS_ACCOUNT_BALANCE CAB WHERE "
                         + " ALD.ACCNT_NO = '" + txtDisWall.Text.Trim() + "' AND  ALD.ACCNT_RANK_ID = '120519000000000003' AND ALD.ACCNT_STATE = 'A' AND ALD.CLINT_ID = CLD.CLINT_ID "
                         + " AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID AND ALD.ACCNT_NO = THA.DEL_ACCNT_NO AND THA.A_ACCNT_NO = ALA.ACCNT_NO "
                         + " AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_NO = CAL.CAS_ACC_NO AND CAL.CAS_ACC_ID = CAB.CAS_ACC_ID ";

                string fileName = "", strHTML = "";
                DataSet dtsAccount = new DataSet();
                fileName = "Dis_per_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor(Idividual) Wise Agent Performance Report</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisPerIFDate.DateString + "' To '" + dtpDisPerIToDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                        strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Wallet: '" + prow["DIS_NO"].ToString() + " </h2></td></tr>";
                        strHTML = strHTML + "</table>";
                        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                        strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Name: '" + prow["DIS_NAME"].ToString() + " </h2></td></tr>";
                        strHTML = strHTML + "</table>";
                        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                        strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor Address: '" + prow["DIS_NAME"].ToString() + ", " + prow["DIS_THANA"].ToString() + "  </h2></td></tr>";
                        strHTML = strHTML + "</table>";

                        if (SerialNo == 1)
                        {
                            break;
                        }

                    }
                }

                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >District</td>";
                strHTML = strHTML + "<td valign='middle' >DSE Wallet </td>";
                strHTML = strHTML + "<td valign='middle' >Agent Wallet </td>";
                strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
                strHTML = strHTML + "<td valign='middle' >Agent Balance</td>";
                strHTML = strHTML + "<td valign='middle' >Customer Registration Count</td>";
                strHTML = strHTML + "<td valign='middle' >Registration Commission Count(Cash In + Verified)</td>";
                strHTML = strHTML + "<td valign='middle' >Number of Cash-In</td>";
                strHTML = strHTML + "<td valign='middle' >Total Cash-In Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Number of Cash-Out</td>";
                strHTML = strHTML + "<td valign='middle' >Total Cash-Out Amount </td>";
                strHTML = strHTML + "<td valign='middle' >Number of Bank Deposit</td>";
                strHTML = strHTML + "<td valign='middle' >Total Bank Deposit Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Number of Bill Payment</td>";
                strHTML = strHTML + "<td valign='middle' >Total Bill Payment Amount</td>";
                strHTML = strHTML + "</tr>";


                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["DSE_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NO"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AGT_BAL"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["NO_OF_REG"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["COMMI_RG_COUNT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["NO_OF_CASIN"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CASIN_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["NO_OF_CASOUT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["CASOUT_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["NO_OF_BD"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["BD_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["NO_OF_BP"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["AMOUNT_BP"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblTotalRgCount = dblTotalRgCount + Convert.ToDouble(prow["NO_OF_REG"].ToString());
                        dblCommRgCount = dblCommRgCount + Convert.ToDouble(prow["COMMI_RG_COUNT"].ToString());
                        dblCasInCount = dblCasInCount + Convert.ToDouble(prow["NO_OF_CASIN"].ToString());
                        dblCasInAmount = dblCasInAmount + Convert.ToDouble(prow["CASIN_AMT"].ToString());
                        dblCasOutCount = dblCasOutCount + Convert.ToDouble(prow["NO_OF_CASOUT"].ToString());
                        dblCasOutAmonut = dblCasOutAmonut + Convert.ToDouble(prow["CASOUT_AMT"].ToString());
                        dblBdCount = dblBdCount + Convert.ToDouble(prow["NO_OF_BD"].ToString());
                        dblBdAmount = dblBdAmount + Convert.ToDouble(prow["BD_AMT"].ToString());
                        dblBpCount = dblBpCount + Convert.ToDouble(prow["NO_OF_BP"].ToString());
                        dblBpAmount = dblBpAmount + Convert.ToDouble(prow["AMOUNT_BP"].ToString());

                    }
                }

                dblTotalRgCount = System.Math.Round(dblTotalRgCount, 0);
                dblCommRgCount = System.Math.Round(dblCommRgCount, 0);
                dblCasInCount = System.Math.Round(dblCasInCount, 0);
                dblCasInAmount = System.Math.Round(dblCasInAmount, 0);
                dblCasOutCount = System.Math.Round(dblCasOutCount, 0);
                dblCasOutAmonut = System.Math.Round(dblCasOutAmonut, 0);
                dblBdCount = System.Math.Round(dblBdCount, 0);
                dblBdAmount = System.Math.Round(dblBdAmount, 0);
                dblBpCount = System.Math.Round(dblBpCount, 0);
                dblBpAmount = System.Math.Round(dblBpAmount, 0);


                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Total:" + " </td>";
                strHTML = strHTML + " <td > " + dblTotalRgCount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblCommRgCount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblCasInCount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblCasInAmount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblCasOutCount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblCasOutAmonut.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblBdCount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblBdAmount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblBpCount.ToString() + " </td>";
                strHTML = strHTML + " <td > " + dblBpAmount.ToString() + " </td>";

                strHTML = strHTML + " </tr>";



                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                        strHTML = strHTML + "<tr><td COLSPAN=16 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Total Number of Lifting: '" + prow["COUNT_LIFTING"].ToString() + ", Lifting Amount: '" + prow["AMOUNT_LIFTING"].ToString() + ", Total Number of Refund : '" + prow["COUNT_REFUND"].ToString() + ", Refund Amount : '" + prow["AMOUNT_REFUND"].ToString() + "  </h2></td></tr>";
                        strHTML = strHTML + "</table>";
                        if (SerialNo == 1)
                        {
                            break;
                        }

                    }
                }


                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";


            }

            else
            {
                lblMsg.Text = "Maximum Date Range is 30";
                return;
            }


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    protected void btnDisComm_Click(object sender, EventArgs e)
    {
        try
        {
            string strGetDistributorInfo = objServiceHandler.GetDistributorInfo(txtDisCommWallet.Text.Trim());

            string strSql = "";
            //strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DEL_ACC, TM.SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT, "
            //         + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '1' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
            //         + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim()  + "' AND THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' "
            //         + " AND TM.SERVICE_CODE = 'CN' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO, TM.SERVICE_CODE  "
            //         + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, 'CCT,SW' SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT,  "
            //         + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '2' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
            //         + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.RECEPENT_PARTY "
            //         + " AND TM.SERVICE_CODE IN ('CCT','SW') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO "
            //         + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, TM.SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT,  "
            //         + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '3' REC_COUNT  FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
            //         + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' "
            //         + " AND TM.SERVICE_CODE = 'BD' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO, TM.SERVICE_CODE "
            //         + " UNION SELECT DISTINCT CL_NAME, 'CORP TRX' SERVICE_CODE, COUNT(REQ_ID) COUNT_TRX, SUM(TR_AMT) TRX_AMT, SUM(TRD_COMM) TRX_COMM_AMT, '4' REC_COUNT FROM  "
            //         + " ( SELECT DISTINCT CLC.CLINT_NAME CL_NAME, THA.DEL_ACCNT_NO, THA.SA_ACCNT_NO, TM.REQUEST_ID REQ_ID, TM.TRANSACTION_DATE, TM.SERVICE_CODE, TM.TRANSACTION_AMOUNT TR_AMT, "
            //         + " TM.REQUEST_PARTY, TM.RECEPENT_PARTY, TM.SERVICE_FEE, TM.NET_COMMISSION, TM.AIT, TM.SERVICE_VAT_TAX, TM.POOL_ACCOUNT, TM.AGENT_COMMISSION, TM.NOTE, TM.THIRDPARTY_COM_AMOUNT TRD_COMM, "
            //         + " TM.VENDOR_COMMISSION, TM.CHANNEL_COMMISSION, TM.AGENT_OPT_COMMISSION, BANK_COMMISSION FROM TEMP_HIERARCHY_LIST_ALL THA, TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD, ACCOUNT_RANK ARD, ACCOUNT_LIST ALC, "
            //         + " ACCOUNT_RANK ARC, CLIENT_LIST CLC WHERE THA.DEL_ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.SA_ACCNT_NO = TM.REQUEST_PARTY||'1' AND TM.SERVICE_CODE = 'FM' "
            //         + " AND TM.REQUEST_PARTY||'1' = ALD.ACCNT_NO AND ALD.ACCNT_RANK_ID = ARD.ACCNT_RANK_ID AND ARD.ACCNT_RANK_ID = '120519000000000004' "
            //         + " AND TM.RECEPENT_PARTY = ALC.ACCNT_NO AND ALC.ACCNT_RANK_ID = ARC.ACCNT_RANK_ID AND ARC.ACCNT_RANK_ID = '140917000000000004' "
            //         + " AND ALC.CLINT_ID = CLC.CLINT_ID AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'   ) TEMP GROUP BY CL_NAME "
            //         + " UNION SELECT DIS_NO, 'RG' TRX_TYPE, SUM (RG_COUNT) RG_APRV_COUNT, SUM(REG),  SUM (TOTAL_RG_COMMISSION) TOTAL_RG_COMMISSION, '5' REC_COUNT "
            //         + " FROM VW_DISTRIBUTION_REPORT2 VDR, (SELECT TRAN2.DEL_ACCNT_NO DIS_ACC_NO, TRAN2.REG_CNT REG_COUNT, '0' REG  FROM (SELECT TRAN.DEL_ACCNT_NO,SUM(CT) REG_CNT  FROM   (SELECT THL.DEL_ACCNT_NO,ASD.AGENT_MOBILE_NO, "
            //         + " COUNT (ASD.CUSTOMER_MOBILE_NO) CT FROM ACCOUNT_SERIAL_DETAIL ASD,   TEMP_HIERARCHY_LIST_ALL THL WHERE TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'  AND SUBSTR(('+88'||THL.A_ACCNT_NO),0,14)=ASD.AGENT_MOBILE_NO "
            //         + " GROUP BY ASD.AGENT_MOBILE_NO,THL.DEL_ACCNT_NO) TRAN   GROUP BY TRAN.DEL_ACCNT_NO) TRAN2, CLIENT_LIST CL ,MANAGE_THANA MT, MANAGE_DISTRICT MD "
            //         + " WHERE TRAN2.DEL_ACCNT_NO= SUBSTR(CL.CLINT_MOBILE,4,14)||'1'  AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) ) TEMP "
            //         + " WHERE VDR.TRANSACTION_DATE BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'  AND DIS_NO = TEMP.DIS_ACC_NO(+) AND DIS_NO = '" + txtDisCommWallet.Text.Trim() + "' "
            //         + " GROUP BY DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, REG_COUNT ORDER BY REC_COUNT ASC";

            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DEL_ACC, TM.SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT, "
                     + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '1' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
                     + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' "
                     + " AND TM.SERVICE_CODE = 'CN' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO, TM.SERVICE_CODE  "
                     + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, 'CCT,SW' SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT,  "
                     + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '2' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
                     + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.RECEPENT_PARTY "
                     + " AND TM.SERVICE_CODE IN ('CCT','SW') AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO "
                     + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, TM.SERVICE_CODE, COUNT(TM.REQUEST_ID) COUNT_TRX, SUM(TM.TRANSACTION_AMOUNT) TRX_AMT,  "
                     + " SUM(TM.THIRDPARTY_COM_AMOUNT) TRX_COMM_AMT, '3' REC_COUNT  FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, TEMP_MIS_TRANSACTIONS_REPORT TM "
                     + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.A_ACCNT_NO = TM.REQUEST_PARTY||'1' "
                     + " AND TM.SERVICE_CODE = 'BD' AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' GROUP BY THA.DEL_ACCNT_NO, TM.SERVICE_CODE "
                     + " UNION SELECT DISTINCT CL_NAME, 'CORP TRX' SERVICE_CODE, COUNT(REQ_ID) COUNT_TRX, SUM(TR_AMT) TRX_AMT, SUM(TRD_COMM) TRX_COMM_AMT, '4' REC_COUNT FROM  "
                     + " ( SELECT DISTINCT CLC.CLINT_NAME CL_NAME, THA.DEL_ACCNT_NO, THA.SA_ACCNT_NO, TM.REQUEST_ID REQ_ID, TM.TRANSACTION_DATE, TM.SERVICE_CODE, TM.TRANSACTION_AMOUNT TR_AMT, "
                     + " TM.REQUEST_PARTY, TM.RECEPENT_PARTY, TM.SERVICE_FEE, TM.NET_COMMISSION, TM.AIT, TM.SERVICE_VAT_TAX, TM.POOL_ACCOUNT, TM.AGENT_COMMISSION, TM.NOTE, TM.THIRDPARTY_COM_AMOUNT TRD_COMM, "
                     + " TM.VENDOR_COMMISSION, TM.CHANNEL_COMMISSION, TM.AGENT_OPT_COMMISSION, BANK_COMMISSION FROM TEMP_HIERARCHY_LIST_ALL THA, TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST ALD, ACCOUNT_RANK ARD, ACCOUNT_LIST ALC, "
                     + " ACCOUNT_RANK ARC, CLIENT_LIST CLC WHERE THA.DEL_ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND THA.SA_ACCNT_NO = TM.REQUEST_PARTY||'1' AND TM.SERVICE_CODE = 'FM' "
                     + " AND TM.REQUEST_PARTY||'1' = ALD.ACCNT_NO AND ALD.ACCNT_RANK_ID = ARD.ACCNT_RANK_ID AND ARD.ACCNT_RANK_ID = '120519000000000004' "
                     + " AND TM.RECEPENT_PARTY = ALC.ACCNT_NO AND ALC.ACCNT_RANK_ID = ARC.ACCNT_RANK_ID AND ARC.ACCNT_RANK_ID = '140917000000000004' "
                     + " AND ALC.CLINT_ID = CLC.CLINT_ID AND TRUNC(TM.TRANSACTION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'   ) TEMP GROUP BY CL_NAME "
                     + " UNION SELECT DIS_NO, 'RG' TRX_TYPE, SUM (RG_COUNT) RG_APRV_COUNT, SUM(REG),  SUM (TOTAL_RG_COMMISSION) TOTAL_RG_COMMISSION, '5' REC_COUNT "
                     + " FROM VW_DISTRIBUTION_REPORT2 VDR, (SELECT TRAN2.DEL_ACCNT_NO DIS_ACC_NO, TRAN2.REG_CNT REG_COUNT, '0' REG  FROM (SELECT TRAN.DEL_ACCNT_NO,SUM(CT) REG_CNT  FROM   (SELECT THL.DEL_ACCNT_NO,ASD.AGENT_MOBILE_NO, "
                     + " COUNT (ASD.CUSTOMER_MOBILE_NO) CT FROM ACCOUNT_SERIAL_DETAIL ASD,   TEMP_HIERARCHY_LIST_ALL THL WHERE TRUNC(ASD.ACTIVATION_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'  AND SUBSTR(('+88'||THL.A_ACCNT_NO),0,14)=ASD.AGENT_MOBILE_NO "
                     + " GROUP BY ASD.AGENT_MOBILE_NO,THL.DEL_ACCNT_NO) TRAN   GROUP BY TRAN.DEL_ACCNT_NO) TRAN2, CLIENT_LIST CL ,MANAGE_THANA MT, MANAGE_DISTRICT MD "
                     + " WHERE TRAN2.DEL_ACCNT_NO= SUBSTR(CL.CLINT_MOBILE,4,14)||'1'  AND CL.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) ) TEMP "
                     + " WHERE VDR.TRANSACTION_DATE BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "'  AND DIS_NO = TEMP.DIS_ACC_NO(+) AND DIS_NO = '" + txtDisCommWallet.Text.Trim() + "' "
                     + " GROUP BY DIS_NO, RANK_TITEL, DISTRIBUTOR_ADD, DISTRIBUTOR_NAME, REG_COUNT "
                     + " UNION SELECT DISTINCT THA.DEL_ACCNT_NO, 'BP' SER_CODE, COUNT(DISTINCT UT.REQUEST_ID) TRX_CNT, SUM(UT.TOTAL_BILL_AMOUNT) TOT_AMT, "
                     + " SUM(CAT.CAS_TRAN_AMT) TOT_COMM_AMT, '6' REC_COUNT FROM TEMP_HIERARCHY_LIST_ALL THA, UTILITY_TRANSACTION UT, SERVICE_REQUEST SR, "
                     + " BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE THA.DEL_ACCNT_NO = '" + txtDisCommWallet.Text.Trim() + "' AND UT.SOURCE_ACC_NO = THA.A_ACCNT_NO "
                     + " AND UT.STAKEHOLDER_ID = 'MBLBANK' AND UT.SERVICE = 'UBP' AND UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
                     + " AND CAT.REQUEST_ID = SR.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'TDRCOM' AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpDisCommFDate.DateString + "' AND '" + dtpDisCommToDate.DateString + "' "
                     + " GROUP BY THA.DEL_ACCNT_NO "
                     + " ORDER BY REC_COUNT ASC ";



            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Dis_Comm_Rpt";

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor(Individual) Commission Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor : '" + strGetDistributorInfo + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=5 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisCommFDate.DateString + "' To '" + dtpDisCommToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Type</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Count</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount </td>";
            strHTML = strHTML + "<td valign='middle' >Commission</td>";
            strHTML = strHTML + "</tr>";




            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;

                double dblCnCommAmt = 0;
                double dblCctCommAmt = 0;
                double dblBdCommAmt = 0;
                double dblBpCommAmt = 0;
                double total1 = 0;


                double dblTotalSumAmount = 0;
                double dblTotalSumCommission = 0;
                double dblTotalSumVat = 0;
                double dblTotalSumAmountAfterVat = 0;
                double dblTotalSumAit = 0;
                double dblTotalSumAfterAit = 0;
                double dblTotalSumDistributorCommission = 0;
                double dblTotalSumMblCommission = 0;
                double total2 = 0;

                double total3 = 0;
                double total4 = 0;


                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Transaction Commission Details</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";



                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "CN" && prow["REC_COUNT"].ToString() == "1")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblCnCommAmt = Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());
                        total1 = dblCnCommAmt;
                    }
                }
                total1 = total1;


                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "CCT,SW" && prow["REC_COUNT"].ToString() == "2")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblCctCommAmt = Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());
                        total1 = total1 + dblCctCommAmt;
                    }
                }
                total1 = total1;


                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "BD" && prow["REC_COUNT"].ToString() == "3")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblBdCommAmt = Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());
                        total1 = total1 + dblBdCommAmt;
                    }
                }
                total1 = total1;






                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "BP" && prow["REC_COUNT"].ToString() == "6")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        dblBpCommAmt = Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());
                        total1 = total1 + dblBpCommAmt;
                    }
                }
                total1 = total1;




                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Total: </td>";
                strHTML = strHTML + " <td >  '" + total1 + "' </td>";
                strHTML = strHTML + " </tr>";

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Corporate Transaction Commission Details</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";


                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "CORP TRX" && prow["REC_COUNT"].ToString() == "4")
                    {
                        double dblToalAmt = Convert.ToDouble(prow["TRX_AMT"]);
                        double dblTotalCommsssion = dblToalAmt * 0.0025;
                        dblTotalCommsssion = System.Math.Round(dblTotalCommsssion, 2);
                        double dblTotalIncusiveAmt = (dblTotalCommsssion * 100) / 115;
                        dblTotalIncusiveAmt = System.Math.Round(dblTotalIncusiveAmt, 2);
                        double dblVAT = dblTotalCommsssion - dblTotalIncusiveAmt;
                        dblVAT = System.Math.Round(dblVAT, 2);
                        double dblAmtAfterVAT = dblTotalCommsssion - dblVAT;
                        dblAmtAfterVAT = System.Math.Round(dblAmtAfterVAT, 2);
                        double dblAIT = dblAmtAfterVAT * 0.1;
                        dblAIT = System.Math.Round(dblAIT, 2);
                        double dblAmtAfterAIT = dblAmtAfterVAT - dblAIT;
                        dblAmtAfterAIT = System.Math.Round(dblAmtAfterAIT, 2);
                        string disCommRate = "60%";
                        double dblDistributorCommission = dblAmtAfterAIT * 0.6;
                        dblDistributorCommission = System.Math.Round(dblDistributorCommission, 2);
                        string mblCommRate = "40%";
                        double dblMBLCommission = dblAmtAfterAIT * 0.4;
                        dblMBLCommission = System.Math.Round(dblMBLCommission, 2);



                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["DEL_ACC"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + dblDistributorCommission + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        total2 = total2 + dblDistributorCommission;
                    }
                }

                total2 = total2;

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Total: </td>";
                strHTML = strHTML + " <td >'" + total2 + "</td>";
                strHTML = strHTML + " </tr>";


                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >KYC Updated Commission Details</td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " </tr>";


                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    if (prow["SERVICE_CODE"].ToString() == "RG" && prow["REC_COUNT"].ToString() == "5")
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["SERVICE_CODE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["COUNT_TRX"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_AMT"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["TRX_COMM_AMT"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                        total3 = total3 + Convert.ToDouble(prow["TRX_COMM_AMT"].ToString());

                    }
                }

                total3 = total3;

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + "<td valign='middle' >Total: </td>";
                strHTML = strHTML + " <td >'" + total3 + "</td>";
                strHTML = strHTML + " </tr>";

                total4 = total1 + total2 + total3;


                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";
                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "Total Commission:" + " </td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Transaction Commission:" + " </td>";
                strHTML = strHTML + " <td >'" + total1 + "</td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";


                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Corporate Commission:" + " </td>";
                strHTML = strHTML + " <td >'" + total2 + "</td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "KYC Update Commission:" + " </td>";
                strHTML = strHTML + " <td >'" + total3 + "</td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                strHTML = strHTML + " <table>";
                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "Grand Total: " + " </td>";
                strHTML = strHTML + " <td >'" + total4 + "</td>";
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            //SaveAuditInfo("Preview", "Dis_Comm_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #region bill payment

    private void PopulateDDForAccountRank()
    {
        string strSQL = "SELECT ACCNT_RANK_ID, RANK_TITEL FROM ACCOUNT_RANK WHERE ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006', '170422000000000003', '161215000000000004', '180128000000000008') ORDER BY RANK_TITEL";
        DataSet oDS = new DataSet();

        oDS = objServiceHandler.ExecuteQuery(strSQL);

        ddlAccountRank.DataSource = oDS;
        ddlAccountRank.DataValueField = "ACCNT_RANK_ID";
        ddlAccountRank.DataTextField = "RANK_TITEL";
        ddlAccountRank.DataBind();
        ddlAccountRank.Items.Insert(0, new ListItem("--All Rank--", ""));
    }

    protected void btnBp_Click(object sender, EventArgs e)
    {
        try
        {
            double dblTotalBillAmount = 0;
            string strSql = "";

            //strSql = " SELECT DISTINCT UT.SOURCE_ACC_NO, AR.RANK_TITEL, CL.CLINT_NAME, CL.CLINT_ADDRESS1, UT.PAYER_MOBILE_NO, UT.LOCATION_ID, "
            //         + " UT.ACCOUNT_NUMBER,BILL_NUMBER, UT.TOTAL_DPDC_AMOUNT, UT.VAT_AMOUNT, UT.BILL_MONTH, UT.BILL_YEAR, "
            //         + " UT.SERVICE, UT.REQUEST_ID, UT.OWNER_CODE,  UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT, UT.NET_DPDC_AMOUNT, "
            //         + " UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.REQUEST_PARTY_TYPE FROM APSNG101.UTILITY_TRANSACTION UT, "
            //         + " APSNG101.SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, ACCOUNT_LIST AL, "
            //         + " ACCOUNT_RANK AR,CLIENT_LIST CL WHERE UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
            //         + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' "
            //         + " AND UT.SERVICE IN ('UBP', 'UBPW') AND CAT.CAS_TRAN_STATUS = 'A' AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpBpFromDate.DateString + "' AND '" + dtpBpToDate.DateString + "' "
            //         + " AND UT.SOURCE_ACC_NO = AL.ACCNT_NO(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) AND AL.CLINT_ID = CL.CLINT_ID ";

            string strSqlSub = "";

            if (ddlAccountRank.SelectedIndex > 0 && ddlBillType.SelectedIndex > 0)
            {
                strSqlSub = " AND T.ACCNT_RANK_ID = '" + ddlAccountRank.SelectedValue + "' AND T.OWNER_CODE = '" + ddlBillType.SelectedValue + "'";
            }
            else if (ddlAccountRank.SelectedIndex > 0)
            {
                strSqlSub = " AND T.ACCNT_RANK_ID = '" + ddlAccountRank.SelectedValue + "'";
            }
            else if (ddlBillType.SelectedIndex > 0)
            {
                strSqlSub = " AND T.OWNER_CODE = '" + ddlBillType.SelectedValue + "'";
            }

            strSql = " SELECT CLD.CLINT_NAME DIS_NAME, THA.DEL_ACCNT_NO DIS_NO, T.SOURCE_ACC_NO, T.RANK_TITEL, T.CLINT_NAME, T.CLINT_ADDRESS1, MT.THANA_NAME, MD.DISTRICT_NAME, T.PAYER_MOBILE_NO, T.LOCATION_ID, T.ACCOUNT_NUMBER, T.BILL_NUMBER, T.TOTAL_DPDC_AMOUNT, T.VAT_AMOUNT, T.BILL_MONTH, T.BILL_YEAR, T.SERVICE,  T.REQUEST_ID, T.OWNER_CODE, T.TRANSA_DATE, T.TOTAL_BILL_AMOUNT, T.NET_DPDC_AMOUNT, T.RESPONSE_MSG_BP,  T.RESPONSE_STATUS_BP, T.REQUEST_PARTY_TYPE FROM (SELECT DISTINCT UT.SOURCE_ACC_NO, AR.RANK_TITEL, AR.ACCNT_RANK_ID, CL.CLINT_NAME, CL.CLINT_ADDRESS1, UT.PAYER_MOBILE_NO, UT.LOCATION_ID, UT.ACCOUNT_NUMBER,BILL_NUMBER, UT.TOTAL_DPDC_AMOUNT, UT.VAT_AMOUNT, UT.BILL_MONTH,  UT.BILL_YEAR, UT.SERVICE, UT.REQUEST_ID, UT.OWNER_CODE,  UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT,  UT.NET_DPDC_AMOUNT, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.REQUEST_PARTY_TYPE, CL.CLINT_ID,  CL.THANA_ID FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR,  BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, ACCOUNT_LIST AL, ACCOUNT_RANK AR,CLIENT_LIST CL  WHERE UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO  AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT'  AND UT.SERVICE IN ('UBP', 'UBPW', 'UWZP', 'UBPKG','UBPREB') AND CAT.CAS_TRAN_STATUS = 'A' AND UT.SOURCE_ACC_NO = AL.ACCNT_NO(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) AND AL.CLINT_ID = CL.CLINT_ID) T, MANAGE_THANA MT, MANAGE_DISTRICT MD, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD WHERE T.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID(+) AND T.SOURCE_ACC_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_ID = ALD.ACCNT_ID(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND TRUNC(T.TRANSA_DATE) BETWEEN '" + dtpBpFromDate.DateString + "' AND '" + dtpBpToDate.DateString + "' " + strSqlSub;

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "BP_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=24 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=24 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=24 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Bill Payment Report</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=24 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBpFromDate.DateString + "' To '" + dtpBpToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account Name</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account Address</td>";

            strHTML = strHTML + "<td valign='middle' >Source Account Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account District</td>";

            strHTML = strHTML + "<td valign='middle' >Payer Mobile No</td>";
            strHTML = strHTML + "<td valign='middle' >Location Id</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Bill No </td>";
            strHTML = strHTML + "<td valign='middle' >Total DPDC Amount</td>";
            strHTML = strHTML + "<td valign='middle' >VAT</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Month</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Year</td>";
            strHTML = strHTML + "<td valign='middle' >Service </td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Total Bill Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Net DPDC Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Response Message</td>";
            strHTML = strHTML + "<td valign='middle' >Response Status</td>";
            strHTML = strHTML + "<td valign='middle' >BP Request Party Type</td>";


            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACC_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RANK_TITEL"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CLINT_ADDRESS1"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["THANA_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + "</td>";


                    strHTML = strHTML + " <td > '" + prow["PAYER_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["LOCATION_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["ACCOUNT_NUMBER"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_NUMBER"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_DPDC_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["VAT_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_MONTH"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_YEAR"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SERVICE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["TRANSA_DATE"].ToString())) + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_BILL_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["NET_DPDC_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_MSG_BP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_STATUS_BP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY_TYPE"].ToString() + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    dblTotalBillAmount = dblTotalBillAmount + Convert.ToDouble(prow["TOTAL_BILL_AMOUNT"].ToString());
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
            //strHTML = strHTML + " <td >'" + dblTotalBillAmount + "</td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "Total Bill Amount" + " </td>";
            strHTML = strHTML + " <td > '" + dblTotalBillAmount + "</td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "BP_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    #endregion

    protected void btnBpRptA_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpBpRank.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Rank";
                return;
            }

            if (drpBpType.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Bill Type";
                return;
            }

            string strSql = "";
            //strSql = " SELECT DISTINCT UT.SOURCE_ACC_NO AGT_NO, CL.CLINT_NAME AGT_NAME, CL.CLINT_ADDRESS1 AGT_ADDR, "
            //         + " ALD.ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, "
            //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_CNT(AL.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpRandBtFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpRandBtToDate.DateString + "')), '" + drpBpType.SelectedValue + "' ) BP_CNT, "
            //         + " APSNG101.PKG_MIS_REPORTS.FUNC_AGT_BP_AMT(AL.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpRandBtFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpRandBtToDate.DateString + "')), '" + drpBpType.SelectedValue + "' ) BP_AMT "
            //         + " FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT, "
            //         + " ACCOUNT_LIST AL, ACCOUNT_RANK AR,CLIENT_LIST CL, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD "
            //         + " WHERE UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
            //         + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'FRTAMT' AND UT.STAKEHOLDER_ID = 'MBLBANK' "
            //         + " AND UT.SERVICE = 'UBP' AND CAT.CAS_TRAN_STATUS = 'A' AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpRandBtFDate.DateString + "' AND '" + dtpRandBtToDate.DateString + "' "
            //         + " AND UT.SOURCE_ACC_NO = AL.ACCNT_NO(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) AND AL.CLINT_ID = CL.CLINT_ID "
            //         + " AND UT.OWNER_CODE = '" + drpBpType.SelectedValue + "' AND AL.ACCNT_RANK_ID IN ('" + drpBpRank.SelectedValue + "') "
            //         + " AND AL.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) ";

            //strSql = " SELECT DISTINCT UT.SOURCE_ACC_NO AGT_NO, CL.CLINT_NAME AGT_NAME, CL.CLINT_ADDRESS1 AGT_ADDR, "
            //         + " ALD.ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, "
            //         + " APSNG101.FUNC_AGT_BP_CNT_NEW(AL.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpRandBtFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpRandBtToDate.DateString + "')), '" + drpBpType.SelectedValue + "' ) BP_CNT, "
            //         + " APSNG101.FUNC_AGT_BP_AMT_NEW(AL.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpRandBtFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpRandBtToDate.DateString + "')), '" + drpBpType.SelectedValue + "' ) BP_AMT "
            //         + " FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR, "
            //         + " ACCOUNT_LIST AL, ACCOUNT_RANK AR,CLIENT_LIST CL, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD "
            //         + " WHERE UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
            //         + " AND UT.SERVICE IN ('UBP', 'UBPW', 'UWZP') AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpRandBtFDate.DateString + "' AND '" + dtpRandBtToDate.DateString + "' "
            //         + " AND UT.SOURCE_ACC_NO = AL.ACCNT_NO(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) AND AL.CLINT_ID = CL.CLINT_ID "
            //         + " AND UT.OWNER_CODE = '" + drpBpType.SelectedValue + "' AND AL.ACCNT_RANK_ID IN ('" + drpBpRank.SelectedValue + "') "
            //         + " AND AL.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) AND ALD.CLINT_ID = CLD.CLINT_ID(+) ";

            //strSql = " SELECT DISTINCT UT.SOURCE_ACC_NO AGT_NO, CL.CLINT_NAME AGT_NAME, CL.CLINT_ADDRESS1 AGT_ADDR,"
            //        + " ALD.ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, MT.THANA_NAME, MD.DISTRICT_NAME, "
            //        + " APSNG101.FUNC_AGT_BP_CNT_NEW(AL.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpRandBtFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpRandBtToDate.DateString + "')), '" + drpBpType.SelectedValue + "' ) BP_CNT, "
            //        + " APSNG101.FUNC_AGT_BP_AMT_NEW(AL.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpRandBtFDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpRandBtToDate.DateString + "')), '" + drpBpType.SelectedValue + "' ) BP_AMT "
            //        + " FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR,  ACCOUNT_LIST AL, ACCOUNT_RANK AR,CLIENT_LIST CL, "
            //        + " TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MT, MANAGE_DISTRICT MD "
            //        + " WHERE UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO  AND UT.SERVICE IN ('UBP', 'UBPW', 'UWZP') "
            //        + " AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpRandBtFDate.DateString + "' AND '" + dtpRandBtToDate.DateString + "' AND UT.SOURCE_ACC_NO = AL.ACCNT_NO(+) "
            //        + " AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) AND AL.CLINT_ID = CL.CLINT_ID AND UT.OWNER_CODE = '" + drpBpType.SelectedValue + "' "
            //        + " AND AL.ACCNT_RANK_ID IN ('" + drpBpRank.SelectedValue + "') AND AL.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) "
            //        + " AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CL.THANA_ID = MT.THANA_ID AND MT.DISTRICT_ID = MD.DISTRICT_ID ";

            strSql = " SELECT DISTINCT UT.SOURCE_ACC_NO AGT_NO, CL.CLINT_NAME AGT_NAME, CL.CLINT_ADDRESS1 AGT_ADDR, MD.DISTRICT_NAME,"
                    + " ALT.ACCNT_NO TO_ACCNT_NO, ALD.ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, MT.THANA_NAME,"
                    + " APSNG101.FUNC_AGT_BP_CNT_NEW(AL.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpRandBtFDate.DateString + "')),"
                    + " TO_CHAR(TO_DATE('" + dtpRandBtToDate.DateString + "')), '" + drpBpType.SelectedValue + "' ) BP_CNT,"
                    + " APSNG101.FUNC_AGT_BP_AMT_NEW(AL.ACCNT_NO, TO_CHAR(TO_DATE('" + dtpRandBtFDate.DateString + "')),"
                    + " TO_CHAR(TO_DATE('" + dtpRandBtToDate.DateString + "')), '" + drpBpType.SelectedValue + "' ) BP_AMT"
                    + " FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR,  ACCOUNT_LIST AL, ACCOUNT_RANK AR,"
                    + " CLIENT_LIST CL,  TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MT, MANAGE_DISTRICT MD,"
                    + " MANAGE_TERRITORY_HIERARCHY MTH, ACCOUNT_LIST ALT WHERE UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO"
                    + " AND THA.DEL_ACCNT_ID = MTH.ACCNT_ID AND ALT.ACCNT_ID = MTH.HIERARCHY_ACCNT_ID AND UT.SERVICE IN ('UBP', 'UBPW', 'UWZP')"
                    + " AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpRandBtFDate.DateString + "' AND '" + dtpRandBtToDate.DateString + "'"
                    + " AND UT.SOURCE_ACC_NO = AL.ACCNT_NO(+) AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID(+) AND AL.CLINT_ID = CL.CLINT_ID AND"
                    + " UT.OWNER_CODE = '" + drpBpType.SelectedValue + "' AND AL.ACCNT_RANK_ID IN ('120519000000000005') AND AL.ACCNT_NO = THA.A_ACCNT_NO(+) AND"
                    + " THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+)  AND ALD.CLINT_ID = CLD.CLINT_ID(+) AND CL.THANA_ID = MT.THANA_ID"
                    + " AND MT.DISTRICT_ID = MD.DISTRICT_ID ";

            double dblTrxCount = 0;
            double dblTrxAmount = 0;


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "BP_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Agent wise Bill Payment Report('" + drpBpType.SelectedItem.Text + "')</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpRandBtFDate.DateString + "' To '" + dtpRandBtToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Agent No</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Name</td>";
            strHTML = strHTML + "<td valign='middle' >Agent Address</td>";
            strHTML = strHTML + "<td valign='middle' >Agent District</td>";
            strHTML = strHTML + "<td valign='middle' >TO Wallet No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Pay Count</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Pay Amount </td>";


            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AGT_ADDR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BP_CNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BP_AMT"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    dblTrxCount = dblTrxCount + Convert.ToDouble(prow["BP_CNT"].ToString());
                    dblTrxAmount = dblTrxAmount + Convert.ToDouble(prow["BP_AMT"].ToString());

                }
            }

            dblTrxCount = System.Math.Round(dblTrxCount, 0);
            dblTrxAmount = System.Math.Round(dblTrxAmount, 0);

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "Total: " + " </td>";
            strHTML = strHTML + " <td >'" + dblTrxCount + "</td>";
            strHTML = strHTML + " <td >'" + dblTrxAmount + "</td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "BP_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    protected void btnDisWapRpt_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            double dblBpCount = 0;
            double dblBpAmount = 0;
            strSql = " SELECT DISTINCT THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, CLD.CLINT_ADDRESS1 DIS_ADDR, "
                     + " MTD.THANA_NAME DIS_THANA, MDD.DISTRICT_NAME DIS_DISTRICT, "
                     + " APSNG101.PKG_MIS_REPORTS.FUNC_DIS_WAP_APPS_BP_CNT (THA.DEL_ACCNT_NO, TO_CHAR(TO_DATE('" + dtpDisWapFromDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisWapToDate.DateString + "')) ) BP_COUNT, "
                     + " APSNG101.PKG_MIS_REPORTS.FUNC_DIS_WAP_APPS_BP_AMT (THA.DEL_ACCNT_NO, TO_CHAR(TO_DATE('" + dtpDisWapFromDate.DateString + "')), TO_CHAR(TO_DATE('" + dtpDisWapToDate.DateString + "')) ) BP_AMT  "
                     + " FROM TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD "
                     + " WHERE THA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.ACCNT_STATE = 'A' AND ALD.CLINT_ID = CLD.CLINT_ID "
                     + " AND CLD.THANA_ID = MTD.THANA_ID(+) AND MTD.DISTRICT_ID = MDD.DISTRICT_ID(+) ORDER BY THA.DEL_ACCNT_NO ASC ";

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Dis_BP_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Distributor wise Bill Payment Report Through WAP and APPS( Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDisWapFromDate.DateString + "' To '" + dtpDisWapToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Address</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Thana</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Pay Count</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Pay Amount </td>";


            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_ADDR"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_THANA"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_DISTRICT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BP_COUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["BP_AMT"].ToString() + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    dblBpCount = dblBpCount + Convert.ToDouble(prow["BP_COUNT"].ToString());
                    dblBpAmount = dblBpAmount + Convert.ToDouble(prow["BP_AMT"].ToString());

                }
            }

            dblBpCount = System.Math.Round(dblBpCount, 0);
            dblBpAmount = System.Math.Round(dblBpAmount, 0);

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "Total: " + " </td>";
            strHTML = strHTML + " <td >'" + dblBpCount + "</td>";
            strHTML = strHTML + " <td >'" + dblBpAmount + "</td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "BP_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";


        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }


    //Updated by Md. Jahirul Islam 
    // Date Jan-18-2022
    protected void btnBpVarShow_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            if (drpType.SelectedValue == "0")
            {
                lblMsg.Text = "Select Type";
                return;
            }
            // for failed
            else if (drpType.SelectedValue == "1")
            {
                strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, SR.REQUEST_ID, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.LOCATION_ID, "
                        + " UT.OWNER_CODE, UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT,  UT.SOURCE_ACC_NO, UT.PAYER_MOBILE_NO, "
                        + " MAX(CAT.CAS_TRAN_DATE) REVERSE_DATE, UT.RESPONSE_STATUS_BP, "
                        + " CASE WHEN UT.RESPONSE_STATUS_BP = '707' THEN 'FAILED DUE TO NULL RESPONSE(OUR END)' "
                        + " WHEN UT.RESPONSE_STATUS_BP = '999' THEN 'TRANSACTION FAILED(SSL END)' END AS RESPONSE_STATUS_BP_CODE, "
                        + " UT.RESPONSE_MSG_BP, (SELECT SUBSTR( DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))),13) FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND AL.ACCNT_NO=UT.SOURCE_ACC_NO) DISTRIBUTER_NAME FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR, "
                        + " BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT  WHERE  UT.SERVICE IN ('UBP','UBPREB','UBPW') "
                        + " AND UT.REVERSE_STATUS = 'R' AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND SR.REQUEST_ID = CAT.REQUEST_ID "
                        + " AND CAT.CAS_TRAN_STATUS = 'R' AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBpVarFDate.DateString + "' AND '" + dtpBpVarToDate.DateString + "' "
                        + " AND UT.RESPONSE_STATUS_BP IN ('999', '707') GROUP BY UT.UTILITY_TRAN_ID, UT.REQUEST_ID, SR.REQUEST_ID, "
                        + " UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.OWNER_CODE, UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT, UT.RESPONSE_LOG, "
                        + " UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.SOURCE_ACC_NO, UT.PAYER_MOBILE_NO, UT.LOCATION_ID ORDER BY UT.OWNER_CODE, UT.TRANSA_DATE ASC ";
            }
            // for reverse
            else if (drpType.SelectedValue == "2")
            {
                strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, SR.REQUEST_ID, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.LOCATION_ID, UT.OWNER_CODE, "
                        + " UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT,  UT.SOURCE_ACC_NO, UT.PAYER_MOBILE_NO, MAX(CAT.CAS_TRAN_DATE) REVERSE_DATE, "
                        + " UT.RESPONSE_STATUS_BP, CASE WHEN UT.RESPONSE_STATUS_BP = '789' THEN 'MANUAL REVERSE' "
                        + " WHEN UT.RESPONSE_STATUS_BP = '804' THEN 'AUTO REVERSE' END AS RESPONSE_STATUS_BP_CODE, UT.RESPONSE_MSG_BP,(SELECT SUBSTR(DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))),13) FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND AL.ACCNT_NO=UT.SOURCE_ACC_NO) DISTRIBUTER_NAME "
                        + " FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT "
                        + " WHERE UT.SERVICE IN ('UBP','UBPREB','UBPW') AND UT.REVERSE_STATUS = 'R' AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID "
                        + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_STATUS = 'R' AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBpVarFDate.DateString + "' AND '" + dtpBpVarToDate.DateString + "' "
                        + " AND UT.RESPONSE_STATUS_BP IN ('789', '804') GROUP BY UT.UTILITY_TRAN_ID, UT.REQUEST_ID, SR.REQUEST_ID, "
                        + " UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.OWNER_CODE, UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT, UT.RESPONSE_LOG, "
                        + " UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.SOURCE_ACC_NO, UT.PAYER_MOBILE_NO, UT.LOCATION_ID ORDER BY UT.OWNER_CODE, UT.TRANSA_DATE ASC ";
            }

            // for cancel api reverse
            else if (drpType.SelectedValue == "3")
            {
                strSql = " SELECT   DISTINCT UT.UTILITY_TRAN_ID, SR.REQUEST_ID, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.LOCATION_ID, UT.OWNER_CODE, "
                        + " UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT, UT.SOURCE_ACC_NO, UT.PAYER_MOBILE_NO, MAX(CAT.CAS_TRAN_DATE) REVERSE_DATE, "
                        + " UT.RESPONSE_STATUS_BP, CASE WHEN UT.RESPONSE_STATUS_BP = '789' THEN 'MANUAL REVERSE'  WHEN UT.RESPONSE_STATUS_BP = '804' "
                        + " THEN 'AUTO REVERSE' END AS RESPONSE_STATUS_BP_CODE, UT.RESPONSE_MSG_BP,(SELECT SUBSTR(DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))),13) FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND AL.ACCNT_NO=UT.SOURCE_ACC_NO) DISTRIBUTER_NAME FROM   APSNG101.UTILITY_TRANSACTION UT , "
                        + " APSNG101.SERVICE_REQUEST SR,BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT WHERE UT.CHECK_STATUS = 'Y' "
                        + " AND UT.CANCLE_RESPONSE IS NOT NULL  AND UT.REVERSE_STATUS = 'R' "
                        + " AND UT.TOTAL_BILL_AMOUNT IS NOT NULL AND UT.CANCLE_RESPONSE = '804' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO "
                        + " AND SR.REQUEST_ID = CAT.REQUEST_ID AND UT.SERVICE IN ('UBP','UBPREB','UBPW') AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBpVarFDate.DateString + "' AND '" + dtpBpVarToDate.DateString + "' "
                        + " GROUP BY UT.UTILITY_TRAN_ID, UT.REQUEST_ID, SR.REQUEST_ID,  UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.OWNER_CODE, "
                        + " UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT, UT.RESPONSE_LOG,  UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, UT.SOURCE_ACC_NO, "
                        + " UT.PAYER_MOBILE_NO, UT.LOCATION_ID ORDER BY UT.OWNER_CODE, UT.TRANSA_DATE ASC ";

            }



                // for others
            else if (drpType.SelectedValue == "4")
            {
                strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, SR.REQUEST_ID, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.LOCATION_ID, UT.OWNER_CODE, "
                        + " UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT,  UT.SOURCE_ACC_NO, UT.PAYER_MOBILE_NO, MAX(CAT.CAS_TRAN_DATE) REVERSE_DATE, "
                        + " UT.RESPONSE_STATUS_BP, CASE WHEN UT.RESPONSE_STATUS_BP = '801' THEN 'ALREADY PAID' "
                        + " WHEN UT.RESPONSE_STATUS_BP = '102' THEN 'TRY AFTER 5 MINIUTE' WHEN UT.RESPONSE_STATUS_BP = '901' THEN 'INVALID BILL NO' END AS RESPONSE_STATUS_BP_CODE, "
                        + " UT.RESPONSE_MSG_BP,(SELECT SUBSTR(DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))),13) FROM ACCOUNT_LIST AL ,CLIENT_LIST CL ,SERVICE_PACKAGE SP,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND AL.ACCNT_NO=UT.SOURCE_ACC_NO) DISTRIBUTER_NAME FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT "
                        + " WHERE UT.SERVICE IN ('UBP','UBPREB','UBPW') AND UT.REVERSE_STATUS = 'R' "
                        + " AND SR.SMSC_REFERENCE_NO = UT.REQUEST_ID AND SR.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_STATUS = 'R' "
                        + " AND TRUNC(CAT.CAS_TRAN_DATE) BETWEEN '" + dtpBpVarFDate.DateString + "' AND '" + dtpBpVarToDate.DateString + "' "
                        + " AND UT.RESPONSE_STATUS_BP IN ('801', '102', '901') GROUP BY UT.UTILITY_TRAN_ID, UT.REQUEST_ID, SR.REQUEST_ID, UT.ACCOUNT_NUMBER, "
                        + " UT.BILL_NUMBER, UT.OWNER_CODE, UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT, UT.RESPONSE_LOG, UT.RESPONSE_MSG_BP, UT.RESPONSE_STATUS_BP, "
                        + " UT.SOURCE_ACC_NO, UT.PAYER_MOBILE_NO, UT.LOCATION_ID ORDER BY UT.OWNER_CODE, UT.TRANSA_DATE ASC ";
            }



            else
            {
                // do nothing
            }

            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "BP_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);


            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            //strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Bill Payment " + drpType.SelectedItem.Text + " Report ( Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Bill Payment " + drpType.SelectedItem.Text + " Report ( Print Date: " + DateTime.Now + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=15 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBpVarFDate.DateString + "' To '" + dtpBpVarToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Id</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Bill No</td>";
            strHTML = strHTML + "<td valign='middle' >Location</td>";
            strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Amount </td>";
            strHTML = strHTML + "<td valign='middle' >Source Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Payer Mobile No</td>";
            strHTML = strHTML + "<td valign='middle' >Reverse Date</td>";
            strHTML = strHTML + "<td valign='middle' >Response Status Code</td>";
            strHTML = strHTML + "<td valign='middle' >Response Status </td>";
            strHTML = strHTML + "<td valign='middle' >Response Message</td>";
            strHTML = strHTML + "<td valign='middle' >Distributer Name</td>";



            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UTILITY_TRAN_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCOUNT_NUMBER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_NUMBER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["LOCATION_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSA_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_BILL_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACC_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["PAYER_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REVERSE_DATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_STATUS_BP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_STATUS_BP_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_MSG_BP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DISTRIBUTER_NAME"].ToString() + " </td>";




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


            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "BP_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }



    protected void btnResponse801_Click(object sender, EventArgs e)
    {
        try
        {
            // for Response Status 801, 111
            string strSql = "";
            strSql = " SELECT DISTINCT UT.UTILITY_TRAN_ID, SR.REQUEST_ID, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, "
                    + " UT.LOCATION_ID, UT.OWNER_CODE, UT.TRANSA_DATE, UT.TOTAL_BILL_AMOUNT,  UT.SOURCE_ACC_NO, "
                    + " CLA.CLINT_NAME AGENT_NAME, THA.DEL_ACCNT_NO DIS_NO, CLD.CLINT_NAME DIS_NAME, "
                    + " UT.PAYER_MOBILE_NO, ' ' REVERSE_DATE, UT.RESPONSE_STATUS  RESPONSE_STATUS_BP, "
                    + " CASE WHEN UT.RESPONSE_STATUS = '801' THEN 'ALREADY PAID OR INPROCESS' "
                    + " WHEN UT.RESPONSE_STATUS = '111' THEN ' Bill In Process ' END AS RESPONSE_STATUS_BP_CODE, "
                    + " UT.RESPONSE_MSG  RESPONSE_MSG_BP FROM APSNG101.UTILITY_TRANSACTION UT, APSNG101.SERVICE_REQUEST SR, "
                    + " ACCOUNT_LIST ALA, CLIENT_LIST CLA, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALD, CLIENT_LIST CLD "
                    + " WHERE UT.STAKEHOLDER_ID = 'MBLBANK' AND UT.SERVICE = 'UBP' AND SR.REQUEST_ID = UT.REQUEST_ID "
                    + " AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpResponseFromDate.DateString + "' AND '" + dtpResponseToDate.DateString + "' "
                    + " AND UT.RESPONSE_STATUS IN ('801', '111') AND UT.SOURCE_ACC_NO = ALA.ACCNT_NO(+) "
                    + " AND ALA.CLINT_ID = CLA.CLINT_ID AND ALA.ACCNT_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_NO = ALD.ACCNT_NO(+) "
                    + " AND ALD.CLINT_ID = CLD.CLINT_ID(+) ORDER BY UT.OWNER_CODE, UT.TRANSA_DATE ASC ";


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "BP_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Bill Payment Report(Response Status 801, 111) ( Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=18 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBpVarFDate.DateString + "' To '" + dtpBpVarToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Id</td>";
            strHTML = strHTML + "<td valign='middle' >Request Id</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Bill No</td>";
            strHTML = strHTML + "<td valign='middle' >Location</td>";
            strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Amount </td>";
            strHTML = strHTML + "<td valign='middle' >Source Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor No</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Payer Mobile No</td>";
            strHTML = strHTML + "<td valign='middle' >Reverse Date</td>";
            strHTML = strHTML + "<td valign='middle' >Response Status Code</td>";
            strHTML = strHTML + "<td valign='middle' >Response Status </td>";
            strHTML = strHTML + "<td valign='middle' >Response Message</td>";


            strHTML = strHTML + "</tr>";


            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UTILITY_TRAN_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCOUNT_NUMBER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["BILL_NUMBER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["LOCATION_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRANSA_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_BILL_AMOUNT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACC_NO"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["AGENT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DIS_NAME"].ToString() + "</td>";

                    strHTML = strHTML + " <td > '" + prow["PAYER_MOBILE_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REVERSE_DATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_STATUS_BP"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_STATUS_BP_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_MSG_BP"].ToString() + " </td>";

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

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "BP_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnDuplicateBillCount_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            strSql = " SELECT UT.SOURCE_ACC_NO, CAL.CAS_ACC_NO, UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, UT.OWNER_CODE,"
                    + " TOTAL_BILL_AMOUNT, COUNT(BILL_NUMBER) COUNT1 FROM UTILITY_TRANSACTION UT, SERVICE_REQUEST SR,"
                    + " BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL, BDMIT_ERP_101.VW_CAS_ACCOUNT_TRANSACTION CAT"
                    + " WHERE CAL.CAS_ACC_ID = CAT.CAS_ACC_ID AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO AND"
                    + " SR.REQUEST_ID = CAT.REQUEST_ID AND UT.OWNER_CODE IN ('DPDC', 'DS', 'WZ', 'WS') AND CAS_TRAN_STATUS = 'A' AND"
                    + " CAL.CAS_ACC_NO IN ('00000000000060', '00000000000061', '00000000000062', '00000000000064')"
                    + " AND REVERSE_STATUS = 'N' AND TRUNC(TRANSA_DATE) BETWEEN '" + BCDatePickerFrom.DateString + "'"
                    + " AND '" + BCDatePickerTo.DateString + "' GROUP BY UT.SOURCE_ACC_NO, CAL.CAS_ACC_NO,"
                    + " UT.ACCOUNT_NUMBER, UT.BILL_NUMBER, TOTAL_BILL_AMOUNT, UT.OWNER_CODE"
                    + " HAVING COUNT(BILL_NUMBER) > 1 ORDER BY UT.OWNER_CODE ";


            string fileName = "", strHTML = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Dup_Bill_Count_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Duplicate Bill Count ( Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + BCDatePickerFrom.DateString + "' To '" + BCDatePickerTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Source Account</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Number</td>";
            strHTML = strHTML + "<td valign='middle' >Owner Code</td>";
            strHTML = strHTML + "<td valign='middle' >Bill Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Count</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["SOURCE_ACC_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCOUNT_NUMBER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["OWNER_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_BILL_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["COUNT1"].ToString() + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Dup_Bill_Count_Rpt");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnDSPrepaidMeterBillCollReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";
            //strSql = " SELECT UT.SOURCE_ACC_NO INITIATOR_WALLET, CL.CLINT_NAME INITIATOR_NAME, AR.RANK_TITEL INITIATOR_RANK, UT.BILL_NUMBER METER_ID, UT.ACCOUNT_NUMBER, UT.TRANSA_DATE DATE_TIME, UT.ENERGY_COST, UT.METER_RENT, UT.DEMAND_CHARGE, UT.REBATE, UT.PFC, UT.RESPONSE_MSG, UT.VAT_AMOUNT, UT.TOTAL_BILL_AMOUNT, UT.REQUEST_PARTY_TYPE, DECODE(UT.RESPONSE_STATUS_BP,'000','Success','Failed') RESPONSE_STATUS_BP FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL WHERE UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID = CL.CLINT_ID AND UT.OWNER_CODE = 'DSP' AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpDSPrepaidBillCollReportFrom.DateString + "' AND '" + dtpDSPrepaidBillCollReportTo.DateString + "' ";

            strSql = " SELECT MA.AREA_NAME, MTM.HIERARCHY_ACCNT_ID TM_ACCNT_ID, CLTM.CLINT_NAME TM_NAME,  MTO.HIERARCHY_ACCNT_ID TO_ACCNT_ID, CLTO.CLINT_NAME TO_NAME, THA.DEL_ACCNT_ID, UT.SOURCE_ACC_NO INITIATOR_WALLET, CL.CLINT_NAME INITIATOR_NAME, AR.RANK_TITEL INITIATOR_RANK, UT.BILL_NUMBER METER_ID, UT.ACCOUNT_NUMBER, UT.TRANSA_DATE DATE_TIME, UT.ENERGY_COST, UT.METER_RENT, UT.DEMAND_CHARGE, UT.REBATE, UT.PFC, UT.RESPONSE_MSG, UT.VAT_AMOUNT, UT.TOTAL_BILL_AMOUNT, UT.REQUEST_PARTY_TYPE, TM.BANK_COMMISSION MBL_COMM FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, ACCOUNT_RANK AR, CLIENT_LIST CL, SERVICE_REQUEST SR, TEMP_MIS_TRANSACTIONS_REPORT TM, TEMP_HIERARCHY_LIST_ALL THA, MANAGE_TERRITORY_HIERARCHY MTO, ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_TERRITORY_HIERARCHY MTM, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_AREA MTA, MANAGE_AREA MA WHERE UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AL.CLINT_ID = CL.CLINT_ID AND UT.OWNER_CODE = 'DSP' AND UT.RESPONSE_STATUS_BP = '000' AND UT.REQUEST_ID = SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID = TM.REQUEST_ID AND UT.SOURCE_ACC_NO = THA.A_ACCNT_NO(+) AND THA.DEL_ACCNT_ID = MTO.ACCNT_ID(+) AND MTO.HIERARCHY_ACCNT_ID = ALTO.ACCNT_ID(+) AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MTO.HIERARCHY_ACCNT_ID = MTM.ACCNT_ID(+) AND MTM.HIERARCHY_ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND MTM.HIERARCHY_ACCNT_ID = MTA.ACCNT_ID(+) AND MTA.AREA_ID = MA.AREA_ID(+) AND TRUNC(UT.TRANSA_DATE) BETWEEN '" + dtpDSPrepaidBillCollReportFrom.DateString + "' AND '" + dtpDSPrepaidBillCollReportTo.DateString + "' ORDER BY TRANSA_DATE";

            string fileName = "", strHTML = "", strTitle = "";
            DataSet dtsAccount = new DataSet();
            strTitle = "Desco Prepaid Meter Bill Collection";
            fileName = "DS_Prepaid_Bill_Collection_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpDSPrepaidBillCollReportFrom.DateString + "' To '" + dtpDSPrepaidBillCollReportTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >Initiator Wallet Id</td>";
            strHTML = strHTML + "<td valign='middle' >Initiator Name</td>";
            strHTML = strHTML + "<td valign='middle' >Initiator Rank</td>";
            strHTML = strHTML + "<td valign='middle' >Desco Meter Id</td>";
            strHTML = strHTML + "<td valign='middle' >Desco Account No</td>";
            strHTML = strHTML + "<td valign='middle' >Date & Time</td>";
            strHTML = strHTML + "<td valign='middle' >Energy Cost</td>";
            strHTML = strHTML + "<td valign='middle' >Meter Rent</td>";
            strHTML = strHTML + "<td valign='middle' >Demand Charge</td>";
            strHTML = strHTML + "<td valign='middle' >Rebate</td>";
            strHTML = strHTML + "<td valign='middle' >PFC</td>";
            strHTML = strHTML + "<td valign='middle' >Desco Amount Summary</td>";
            strHTML = strHTML + "<td valign='middle' >VAT</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Channel Name</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Status</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["INITIATOR_WALLET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["INITIATOR_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["INITIATOR_RANK"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["METER_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACCOUNT_NUMBER"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DATE_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ENERGY_COST"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["METER_RENT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DEMAND_CHARGE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REBATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["PFC"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["RESPONSE_MSG"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["VAT_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TOTAL_BILL_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_PARTY_TYPE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["MBL_COMM"].ToString() + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnBCNliReport_Click(object sender, EventArgs e)
    {
        double totalTransactionAmount = 0;
        double slabeRate = 0;
        double totalCommission = 0;
        double vatAmount = 0;
        double amountAfterVat = 0;
        double distributorAmount = 0;
        double aitAmount = 0;
        double amountAfterAIT = 0;
        double distributorCommission = 0;
        double mblCommission = 0;

        double gTotalTransactionAmount = 0;
        double gTotalCommission = 0;
        double gVatAmount = 0;
        double gAmountAfterVat = 0;
        double gDistributorAmount = 0;
        double gAitAmount = 0;
        double gAmountAfterAIT = 0;
        double gDistributorCommission = 0;
        double gMblCommission = 0;

        try
        {
            string strSql = " SELECT TO_CHAR(TM.REQUEST_TIME) REQUEST_TIME, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, MDD.DISTRICT_NAME, SUM(TRANSACTION_AMOUNT) TRANSACTION_AMOUNT, CASE WHEN (SELECT SUM(TMS.TRANSACTION_AMOUNT) FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT_A TMS WHERE AL.ACCNT_NO = TMS.RECEPENT_PARTY AND AL.ACCNT_RANK_ID = '181219000000000002' AND TRUNC(TMS.TRANSACTION_DATE) = TM.REQUEST_TIME) BETWEEN 1 AND 15000000 THEN '0.19' WHEN (SELECT SUM(TMS.TRANSACTION_AMOUNT) FROM ACCOUNT_LIST AL, TEMP_MIS_TRANSACTIONS_REPORT_A TMS WHERE AL.ACCNT_NO = TMS.RECEPENT_PARTY AND AL.ACCNT_RANK_ID = '181219000000000002' AND TRUNC(TMS.TRANSACTION_DATE) = TM.REQUEST_TIME) BETWEEN 15000001 AND 35000000 THEN '0.165' ELSE '0.145' END SLABE_RATE FROM (SELECT TRUNC(NLI.REQUEST_TIME) REQUEST_TIME, TM.REQUEST_PARTY, SUM(TM.TRANSACTION_AMOUNT) TRANSACTION_AMOUNT FROM NLI_FUND_COLLECTION NLI, TEMP_MIS_TRANSACTIONS_REPORT_A TM, ACCOUNT_LIST AL WHERE TRUNC(REQUEST_TIME) BETWEEN '" + dtpBCNliFrom.DateString + "' AND '" + dtpBCNliTo.DateString + "' AND NLI.REQUEST_ID = TM.REQUEST_ID AND NLI.DESTINATION_ACCNT_NO = AL.ACCNT_NO AND AL.ACCNT_RANK_ID = '181219000000000002' AND TM.SERVICE_CODE = 'FM' GROUP BY TRUNC(NLI.REQUEST_TIME), TM.REQUEST_PARTY ORDER BY NLI.REQUEST_ID) TM, (SELECT DISTINCT DEL_ACCNT_NO, SA_ACCNT_NO FROM TEMP_HIERARCHY_LIST_ALL) THLA, ACCOUNT_LIST ALD, CLIENT_LIST CLD, MANAGE_THANA MTD, MANAGE_DISTRICT MDD WHERE TM.REQUEST_PARTY || '1' = THLA.SA_ACCNT_NO AND THLA.DEL_ACCNT_NO = ALD.ACCNT_NO AND ALD.CLINT_ID = CLD.CLINT_ID AND CLD.THANA_ID = MTD.THANA_ID AND MTD.DISTRICT_ID = MDD.DISTRICT_ID GROUP BY TM.REQUEST_TIME, THLA.DEL_ACCNT_NO, CLD.CLINT_NAME, MDD.DISTRICT_NAME ORDER BY TM.REQUEST_TIME ";


            string fileName = "", strHTML = "", strTitle = "";
            DataSet dtsAccount = new DataSet();
            strTitle = "Business Collection Report (NLI)";
            fileName = "Business_Collection_NLI_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBCNliFrom.DateString + "' To '" + dtpBCNliTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Slabe Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission</td>";
            strHTML = strHTML + "<td valign='middle' >VAT 15%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After VAT</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Amount</td>";
            strHTML = strHTML + "<td valign='middle' >AIT 10%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After AIT</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Rate</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    totalTransactionAmount = Convert.ToDouble(prow["TRANSACTION_AMOUNT"]);
                    slabeRate = Convert.ToDouble(prow["SLABE_RATE"]);
                    totalCommission = totalTransactionAmount * slabeRate / 100;
                    vatAmount = totalCommission * 15 / 115;
                    amountAfterVat = totalCommission - vatAmount;
                    distributorAmount = amountAfterVat * 63 / 100;
                    aitAmount = distributorAmount * 10 / 100;
                    amountAfterAIT = distributorAmount - aitAmount;
                    distributorCommission = amountAfterAIT;
                    mblCommission = amountAfterVat * 37 / 100;

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["REQUEST_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DEL_ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["SLABE_RATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + totalCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + vatAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterVat.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 63% </td>";
                    strHTML = strHTML + " <td > " + distributorAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + aitAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterAIT.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + distributorCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 37% </td>";
                    strHTML = strHTML + " <td > " + mblCommission.ToString("N2") + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    gTotalTransactionAmount = gTotalTransactionAmount + totalTransactionAmount;
                    gTotalCommission = gTotalCommission + totalCommission;
                    gVatAmount = gVatAmount + vatAmount;
                    gAmountAfterVat = gAmountAfterVat + amountAfterVat;
                    gDistributorAmount = gDistributorAmount + distributorAmount;
                    gAitAmount = gAitAmount + aitAmount;
                    gAmountAfterAIT = gAmountAfterAIT + amountAfterAIT;
                    gDistributorCommission = gDistributorCommission + distributorCommission;
                    gMblCommission = gMblCommission + mblCommission;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gTotalTransactionAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gTotalCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gVatAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAmountAfterVat.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gDistributorAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAitAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAmountAfterAIT.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gDistributorCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gMblCommission.ToString("N2") + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnBillPayComReport_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = "";

            strSql = " SELECT TO_CHAR(TM.TRANSACTION_DATE) TRANSACTION_DATE, SL.SERVICE_TITLE SERVICE_CODE, COUNT(TM.REQUEST_ID) TOTAL_TRANSACTION, SUM(TM.TRANSACTION_AMOUNT) TOTAL_AMOUNT, SUM(TM.BANK_COMMISSION) BANK_COMMISSION FROM TEMP_MIS_TRANSACTIONS_REPORT TM, SERVICE_LIST SL WHERE TM.SERVICE_CODE = SL.SERVICE_ACCESS_CODE AND TM.TRANSACTION_DATE BETWEEN '" + dtpBPCFromDate.DateString + "' AND '" + dtpBPCToDate.DateString + "' AND TM.SERVICE_CODE IN ('UBPDSP','UBPDP','UBP','UWZP','UBPW','UBPREB','UBPKG') GROUP BY TM.TRANSACTION_DATE, SL.SERVICE_TITLE ORDER BY TRANSACTION_DATE";

            string fileName = "", strHTML = "", strTitle = "";
            DataSet dtsAccount = new DataSet();
            strTitle = "Utility Bill Pay Commission Report";
            fileName = "UB_Bill_Pay_Commission_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=6 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpBPCFromDate.DateString + "' To '" + dtpBPCToDate.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Service Type</td>";
            strHTML = strHTML + "<td valign='middle' >Total Transaction</td>";
            strHTML = strHTML + "<td valign='middle' >Total Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TRANSACTION_DATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["SERVICE_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_TRANSACTION"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_AMOUNT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["BANK_COMMISSION"].ToString() + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                }
            }

            //strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";

            //strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #region
    //BY Md.Jahirul Islam 
    //    24 MAY 2022

    protected void btnPILILiReport_Click(object sender, EventArgs e)
    {
        double totalTransactionAmount = 0;
        double slabeRate = 0;
        double totalCommission = 0;
        double vatAmount = 0;
        double amountAfterVat = 0;
        double distributorAmount = 0;
        double aitAmount = 0;
        double amountAfterAIT = 0;
        double distributorCommission = 0;
        double mblCommission = 0;

        double gTotalTransactionAmount = 0;
        double gTotalCommission = 0;
        double gVatAmount = 0;
        double gAmountAfterVat = 0;
        double gDistributorAmount = 0;
        double gAitAmount = 0;
        double gAmountAfterAIT = 0;
        double gDistributorCommission = 0;
        double gMblCommission = 0;

        try
        {




            string strSql = "SELECT DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))) DST_ACCNT_N0,CL.CLINT_NAME, SUM(PC.AMOUNT)TOTAL_SUM FROM APSNG101.PILIL_FUND_COLLECTION PC,ACCOUNT_LIST AL ,CLIENT_LIST CL ,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE AL.ACCNT_NO = PC.SOURCE_ACCNT_NO AND CL.CLINT_ID=AL.CLINT_ID  AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND TRUNC(PC.TXN_DATE) BETWEEN '" + PILILDatePicker1.DateString + "' AND '" + PILILDatePicker2.DateString + "' GROUP BY SOURCE_ACCNT_NO , ACCNT_NO,CLINT_NAME,HIERARCHY_ACCNT_ID,SHORT_CODE";


            string fileName = "", strHTML = "", strTitle = "";
            DataSet dtsAccount = new DataSet();
            strTitle = "Business Collection Report (Prime Islami Life Ins)";
            fileName = "Business_Collection_PILIL_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + PILILDatePicker1.DateString + "' To '" + PILILDatePicker2.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            //strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            //strHTML = strHTML + "<td valign='middle' >Slabe Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission .20%</td>";
            strHTML = strHTML + "<td valign='middle' >VAT 15%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After VAT</td>";
            strHTML = strHTML + "<td valign='middle' >AIT 10%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After AIT</td>";

            strHTML = strHTML + "<td valign='middle' >Distributor Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Rate</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    totalTransactionAmount = Convert.ToDouble(prow["TOTAL_SUM"]);
                    slabeRate = Convert.ToDouble(.19);
                    totalCommission = totalTransactionAmount * slabeRate / 100;
                    vatAmount = totalCommission * 15 / 115;
                    amountAfterVat = totalCommission - vatAmount;
                    //distributorAmount = amountAfterVat * 81 / 100;
                    aitAmount = amountAfterVat * 10 / 100;
                    amountAfterAIT = amountAfterVat - aitAmount;
                    distributorCommission = amountAfterAIT * 81 / 100;
                    mblCommission = amountAfterAIT * 19 / 100;

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["REQUEST_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DST_ACCNT_N0"].ToString().Substring(1, 12) + "</td>";
                    strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_SUM"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["SLABE_RATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + totalCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + vatAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterVat.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + aitAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterAIT.ToString("N2") + "</td>";

                    strHTML = strHTML + " <td > 81% </td>";
                    strHTML = strHTML + " <td > " + distributorCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 19% </td>";
                    strHTML = strHTML + " <td > " + mblCommission.ToString("N2") + "</td>";


                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    gTotalTransactionAmount = gTotalTransactionAmount + totalTransactionAmount;
                    //gTotalCommission = gTotalCommission + totalCommission;
                    gVatAmount = gVatAmount + vatAmount;
                    gAmountAfterVat = gAmountAfterVat + amountAfterVat;
                    // gDistributorAmount = gDistributorAmount + distributorAmount;
                    gAitAmount = gAitAmount + aitAmount;
                    gAmountAfterAIT = gAmountAfterAIT + amountAfterAIT;
                    gDistributorCommission = gDistributorCommission + distributorCommission;
                    gMblCommission = gMblCommission + mblCommission;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gTotalTransactionAmount.ToString("N2") + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + gTotalCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gVatAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAmountAfterVat.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAitAmount.ToString("N2") + " </td>";

            strHTML = strHTML + " <td > " + gAmountAfterAIT.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gDistributorCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gMblCommission.ToString("N2") + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion

    #region Partex Cable and Furniture Md.Jahirul Islam

    protected void btnPartexCableReport_Click(object sender, EventArgs e)
    {
        double totalTransactionAmount = 0;
        double slabeRate = 0;
        double totalCommission = 0;
        double vatAmount = 0;
        double amountAfterVat = 0;
        double distributorAmount = 0;
        double aitAmount = 0;
        double amountAfterAIT = 0;
        double distributorCommission = 0;
        double mblCommission = 0;

        double gTotalTransactionAmount = 0;
        double gTotalCommission = 0;
        double gVatAmount = 0;
        double gAmountAfterVat = 0;
        double gDistributorAmount = 0;
        double gAitAmount = 0;
        double gAmountAfterAIT = 0;
        double gDistributorCommission = 0;
        double gMblCommission = 0;

        try
        {
            string strSql = "SELECT DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))) DST_ACCNT_N0,CL.CLINT_NAME, SUM(PC.AMOUNT)TOTAL_SUM FROM APSNG101.PARTEX_FUND_COLLECTION PC,ACCOUNT_LIST AL ,CLIENT_LIST CL ,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE AL.ACCNT_NO = PC.SOURCE_ACCNT_NO AND CL.CLINT_ID=AL.CLINT_ID  AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND TRUNC(PC.REQUEST_TIME) BETWEEN '" + prCableGMDatePicker1.DateString + "' AND '" + prCableGMDatePicker2.DateString + "' AND PC.DESTINATION_ACCNT_NO LIKE '0110100%' GROUP BY SOURCE_ACCNT_NO , ACCNT_NO,CLINT_NAME,HIERARCHY_ACCNT_ID,SHORT_CODE";

           

            string fileName = "", strHTML = "", strTitle = "";
            DataSet dtsAccount = new DataSet();
            strTitle = "Business Collection Report (PARTEX CABLES LIMITED)";
            fileName = "Business_Collection_Partex_Report_Rpt (PARTEX CABLES LIMITED)";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + prCableGMDatePicker1.DateString + "' To '" + prCableGMDatePicker2.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            //strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            //strHTML = strHTML + "<td valign='middle' >Slabe Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission .20%</td>";
            strHTML = strHTML + "<td valign='middle' >VAT 15%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After VAT</td>";
            strHTML = strHTML + "<td valign='middle' >AIT 10%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After AIT</td>";

            strHTML = strHTML + "<td valign='middle' >Distributor Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Rate</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    totalTransactionAmount = Convert.ToDouble(prow["TOTAL_SUM"]);
                    slabeRate = Convert.ToDouble(.19);
                    totalCommission = totalTransactionAmount * slabeRate / 100;
                    vatAmount = totalCommission * 15 / 115;
                    amountAfterVat = totalCommission - vatAmount;
                    //distributorAmount = amountAfterVat * 81 / 100;
                    aitAmount = amountAfterVat * 10 / 100;
                    amountAfterAIT = amountAfterVat - aitAmount;
                    distributorCommission = amountAfterAIT * 67.25 / 100;
                    mblCommission = amountAfterAIT * 32.28 / 100;

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["REQUEST_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["DST_ACCNT_N0"].ToString().Substring(1, 12) + "</td>";
                    strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_SUM"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["SLABE_RATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + totalCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + vatAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterVat.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + aitAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterAIT.ToString("N2") + "</td>";

                    strHTML = strHTML + " <td > 67% </td>";
                    strHTML = strHTML + " <td > " + distributorCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 32% </td>";
                    strHTML = strHTML + " <td > " + mblCommission.ToString("N2") + "</td>";


                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    gTotalTransactionAmount = gTotalTransactionAmount + totalTransactionAmount;
                    //gTotalCommission = gTotalCommission + totalCommission;
                    gVatAmount = gVatAmount + vatAmount;
                    gAmountAfterVat = gAmountAfterVat + amountAfterVat;
                    // gDistributorAmount = gDistributorAmount + distributorAmount;
                    gAitAmount = gAitAmount + aitAmount;
                    gAmountAfterAIT = gAmountAfterAIT + amountAfterAIT;
                    gDistributorCommission = gDistributorCommission + distributorCommission;
                    gMblCommission = gMblCommission + mblCommission;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gTotalTransactionAmount.ToString("N2") + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + gTotalCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gVatAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAmountAfterVat.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAitAmount.ToString("N2") + " </td>";

            strHTML = strHTML + " <td > " + gAmountAfterAIT.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gDistributorCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gMblCommission.ToString("N2") + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnPartexFurnitureReport_Click(object sender, EventArgs e)
    {
        double totalTransactionAmount = 0;
        double slabeRate = 0;
        double totalCommission = 0;
        double vatAmount = 0;
        double amountAfterVat = 0;
        double distributorAmount = 0;
        double aitAmount = 0;
        double amountAfterAIT = 0;
        double distributorCommission = 0;
        double mblCommission = 0;

        double gTotalTransactionAmount = 0;
        double gTotalCommission = 0;
        double gVatAmount = 0;
        double gAmountAfterVat = 0;
        double gDistributorAmount = 0;
        double gAitAmount = 0;
        double gAmountAfterAIT = 0;
        double gDistributorCommission = 0;
        double gMblCommission = 0;

        try
        {

            string strSql = "SELECT DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))) DST_ACCNT_N0,CL.CLINT_NAME, SUM(PC.AMOUNT)TOTAL_SUM FROM APSNG101.PARTEX_FUND_COLLECTION PC,ACCOUNT_LIST AL ,CLIENT_LIST CL ,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE AL.ACCNT_NO = PC.SOURCE_ACCNT_NO AND CL.CLINT_ID=AL.CLINT_ID  AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND TRUNC(PC.REQUEST_TIME) BETWEEN '" + prFurGMDatePicker1.DateString + "' AND '" + prFurGMDatePicker2.DateString + "' AND PC.DESTINATION_ACCNT_NO LIKE '0110101%' GROUP BY SOURCE_ACCNT_NO , ACCNT_NO,CLINT_NAME,HIERARCHY_ACCNT_ID,SHORT_CODE";




            string fileName = "", strHTML = "", strTitle = "";
            DataSet dtsAccount = new DataSet();
            strTitle = "Business Collection Report (PARTEX FURNITURE LIMITED)";
            fileName = "Business_Collection_Partex_Report_Rpt (PARTEX FURNITURE LIMITED)";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + prFurGMDatePicker1.DateString + "' To '" + prFurGMDatePicker2.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            //strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            //strHTML = strHTML + "<td valign='middle' >Slabe Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission .20%</td>";
            strHTML = strHTML + "<td valign='middle' >VAT 15%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After VAT</td>";
            strHTML = strHTML + "<td valign='middle' >AIT 10%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After AIT</td>";

            strHTML = strHTML + "<td valign='middle' >Distributor Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Rate</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    totalTransactionAmount = Convert.ToDouble(prow["TOTAL_SUM"]);
                    slabeRate = Convert.ToDouble(.19);
                    totalCommission = totalTransactionAmount * slabeRate / 100;
                    vatAmount = totalCommission * 15 / 115;
                    amountAfterVat = totalCommission - vatAmount;
                    //distributorAmount = amountAfterVat * 81 / 100;
                    aitAmount = amountAfterVat * 10 / 100;
                    amountAfterAIT = amountAfterVat - aitAmount;
                    distributorCommission = amountAfterAIT * 67.25 / 100;
                    mblCommission = amountAfterAIT * 32.28 / 100;

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["REQUEST_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["DST_ACCNT_N0"].ToString().Substring(1, 12) + "</td>";
                    strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_SUM"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["TRANSACTION_AMOUNT"].ToString() + "</td>";
                    //strHTML = strHTML + " <td > " + prow["SLABE_RATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + totalCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + vatAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterVat.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + aitAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterAIT.ToString("N2") + "</td>";

                    strHTML = strHTML + " <td > 67% </td>";
                    strHTML = strHTML + " <td > " + distributorCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 32% </td>";
                    strHTML = strHTML + " <td > " + mblCommission.ToString("N2") + "</td>";


                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    gTotalTransactionAmount = gTotalTransactionAmount + totalTransactionAmount;
                    //gTotalCommission = gTotalCommission + totalCommission;
                    gVatAmount = gVatAmount + vatAmount;
                    gAmountAfterVat = gAmountAfterVat + amountAfterVat;
                    // gDistributorAmount = gDistributorAmount + distributorAmount;
                    gAitAmount = gAitAmount + aitAmount;
                    gAmountAfterAIT = gAmountAfterAIT + amountAfterAIT;
                    gDistributorCommission = gDistributorCommission + distributorCommission;
                    gMblCommission = gMblCommission + mblCommission;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gTotalTransactionAmount.ToString("N2") + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + gTotalCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gVatAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAmountAfterVat.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAitAmount.ToString("N2") + " </td>";

            strHTML = strHTML + " <td > " + gAmountAfterAIT.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";

            strHTML = strHTML + " <td > " + gDistributorCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gMblCommission.ToString("N2") + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    #region Business Collection Rupali

    protected void btnBCRliReport_Click(object sender, EventArgs e)
    {
        double totalTransactionAmount = 0;
        double slabeRate = 0;
        double totalCommission = 0;
        double vatAmount = 0;
        double amountAfterVat = 0;
        double distributorAmount = 0;
        double aitAmount = 0;
        double amountAfterAIT = 0;
        double distributorCommission = 0;
        double mblCommission = 0;

        double gTotalTransactionAmount = 0;
        double gTotalCommission = 0;
        double gVatAmount = 0;
        double gAmountAfterVat = 0;
        double gDistributorAmount = 0;
        double gAitAmount = 0;
        double gAmountAfterAIT = 0;
        double gDistributorCommission = 0;
        double gMblCommission = 0;

        try
        {
            string strSql = "Select T1.DEL_ACCNT_NO,t1.CLINT_NAME,T1.TOTAL_SUM,MD.DISTRICT_NAME  from (SELECT SUBSTR(DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))),1,12) DEL_ACCNT_NO,CL.CLINT_NAME, SUM(PC.AMOUNT)TOTAL_SUM FROM APSNG101.RLIL_FUND_COLLECTION PC,ACCOUNT_LIST AL ,CLIENT_LIST CL ,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE AL.ACCNT_NO = PC.SOURCE_ACCNT_NO AND CL.CLINT_ID=AL.CLINT_ID  AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND  TRUNC(PC.REQUEST_TIME) BETWEEN '" + GMDatePicker1.DateString + "' AND '" + GMDatePicker2.DateString + "' GROUP BY SOURCE_ACCNT_NO , ACCNT_NO,CLINT_NAME,HIERARCHY_ACCNT_ID,SHORT_CODE ) t1,ACCOUNT_LIST DAL,CLIENT_LIST DCL,MANAGE_THANA MT,MANAGE_DISTRICT MD WHERE DAL.ACCNT_NO=t1.DEL_ACCNT_NO AND DAL.CLINT_ID=DCL.CLINT_ID AND MT.THANA_ID = DCL.THANA_ID AND MT.DISTRICT_ID = MD.DISTRICT_ID";
            //string strSql = "SELECT DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))) DST_ACCNT_N0,CL.CLINT_NAME, SUM(PC.AMOUNT)TOTAL_SUM FROM APSNG101.RLIL_FUND_COLLECTION PC,ACCOUNT_LIST AL ,CLIENT_LIST CL ,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE AL.ACCNT_NO = PC.SOURCE_ACCNT_NO AND CL.CLINT_ID=AL.CLINT_ID  AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND TRUNC(REQUEST_TIME) BETWEEN '" + GMDatePicker1.DateString + "' AND '" + GMDatePicker2.DateString + "' GROUP BY SOURCE_ACCNT_NO , ACCNT_NO,CLINT_NAME,HIERARCHY_ACCNT_ID,SHORT_CODE";

            string fileName = "", strHTML = "", strTitle = "";
            DataSet dtsAccount = new DataSet();
            strTitle = "Business Collection Report (RLIL)";
            fileName = "Business_Collection_RLIL_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + GMDatePicker1.DateString + "' To '" + GMDatePicker2.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            //strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Slabe Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission</td>";
            strHTML = strHTML + "<td valign='middle' >VAT 15%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After VAT</td>";
            
            strHTML = strHTML + "<td valign='middle' >AIT 10%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After AIT</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Amount</td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Rate</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    totalTransactionAmount = Convert.ToDouble(prow["TOTAL_SUM"]);
                    if(totalTransactionAmount<= 15000000)
                    {
                        slabeRate = 0.00195;
                    }
                    else if (totalTransactionAmount <= 35000000)
                    {
                        slabeRate = 0.00175;
                    }
                    else if (totalTransactionAmount > 35000000)
                    {
                        slabeRate = 0.00150;
                    }
                    totalCommission = totalTransactionAmount * slabeRate;
                    vatAmount = totalCommission * 15 / 115;
                    amountAfterVat = totalCommission - vatAmount;

                    
                    aitAmount = amountAfterVat * 10 / 100;
                   
                    amountAfterAIT = amountAfterVat - aitAmount;
                    distributorAmount = amountAfterAIT * 81 / 100;
                   // distributorCommission = amountAfterAIT;
                    mblCommission = amountAfterAIT * 19 / 100;

                   

                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                   // strHTML = strHTML + " <td > " + prow["REQUEST_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DEL_ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_SUM"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + slabeRate.ToString() + "</td>";
                    strHTML = strHTML + " <td > " + totalCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + vatAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterVat.ToString("N2") + "</td>";
                   
                    strHTML = strHTML + " <td > " + aitAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterAIT.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 81% </td>";
                    strHTML = strHTML + " <td > " + distributorAmount.ToString("N2") + "</td>";
                    //strHTML = strHTML + " <td > " + distributorCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 19% </td>";
                    strHTML = strHTML + " <td > " + mblCommission.ToString("N2") + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    gTotalTransactionAmount = gTotalTransactionAmount + totalTransactionAmount;
                    gTotalCommission = gTotalCommission + totalCommission;
                    gVatAmount = gVatAmount + vatAmount;
                    gAmountAfterVat = gAmountAfterVat + amountAfterVat;
                    gDistributorAmount = gDistributorAmount + distributorAmount;
                    gAitAmount = gAitAmount + aitAmount;
                    gAmountAfterAIT = gAmountAfterAIT + amountAfterAIT;
                    gDistributorCommission = gDistributorCommission + distributorCommission;
                    gMblCommission = gMblCommission + mblCommission;
                }
            }

            strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gTotalTransactionAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gTotalCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gVatAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAmountAfterVat.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAitAmount.ToString("N2") + " </td>";
           
            strHTML = strHTML + " <td > " + gAmountAfterAIT.ToString("N2") + " </td>";
            
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gDistributorAmount.ToString("N2") + " </td>";
            
           
            //strHTML = strHTML + " <td > " + gDistributorCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gMblCommission.ToString("N2") + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    #region Business Collection Pran

    protected void btnBCPranReport_Click(object sender, EventArgs e)
    {
        double totalTransactionAmount = 0;
        double slabeRate = 0;
        double totalCommission = 0;
        double vatAmount = 0;
        double amountAfterVat = 0;
        double distributorAmount = 0;
        double aitAmount = 0;
        double amountAfterAIT = 0;
        double distributorCommission = 0;
        double mblCommission = 0;

        double gTotalTransactionAmount = 0;
        double gTotalCommission = 0;
        double gVatAmount = 0;
        double gAmountAfterVat = 0;
        double gDistributorAmount = 0;
        double gAitAmount = 0;
        double gAmountAfterAIT = 0;
        double gDistributorCommission = 0;
        double gMblCommission = 0;

        try
        {
            string strSql = "Select T1.DEL_ACCNT_NO,t1.CLINT_NAME,T1.TOTAL_SUM,MD.DISTRICT_NAME  from (SELECT SUBSTR(DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))),1,12) DEL_ACCNT_NO,CL.CLINT_NAME, SUM(PC.AMOUNT)TOTAL_SUM FROM APSNG101.PRAN_FUND_COLLECTION PC,ACCOUNT_LIST AL ,CLIENT_LIST CL ,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE AL.ACCNT_NO = PC.SOURCE_ACCNT_NO AND CL.CLINT_ID=AL.CLINT_ID  AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND  TRUNC(PC.REQUEST_TIME) BETWEEN '" + dtPranFrom.DateString + "' AND '" + dtPranTo.DateString + "' GROUP BY SOURCE_ACCNT_NO , ACCNT_NO,CLINT_NAME,HIERARCHY_ACCNT_ID,SHORT_CODE ) t1,ACCOUNT_LIST DAL,CLIENT_LIST DCL,MANAGE_THANA MT,MANAGE_DISTRICT MD WHERE DAL.ACCNT_NO=t1.DEL_ACCNT_NO AND DAL.CLINT_ID=DCL.CLINT_ID AND MT.THANA_ID = DCL.THANA_ID AND MT.DISTRICT_ID = MD.DISTRICT_ID";
            //string strSql = "SELECT DECODE(HIERARCHY_ACCNT_ID,NULL,'',DECODE(SHORT_CODE,'D','Distributor',CLIENT_NAME(AH.HIERARCHY_ACCNT_ID))) DST_ACCNT_N0,CL.CLINT_NAME, SUM(PC.AMOUNT)TOTAL_SUM FROM APSNG101.RLIL_FUND_COLLECTION PC,ACCOUNT_LIST AL ,CLIENT_LIST CL ,ACCOUNT_RANK AR,ACCOUNT_HIERARCHY AH WHERE AL.ACCNT_NO = PC.SOURCE_ACCNT_NO AND CL.CLINT_ID=AL.CLINT_ID  AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  AH.ACCNT_ID(+)=AL.ACCNT_ID  AND TRUNC(REQUEST_TIME) BETWEEN '" + GMDatePicker1.DateString + "' AND '" + GMDatePicker2.DateString + "' GROUP BY SOURCE_ACCNT_NO , ACCNT_NO,CLINT_NAME,HIERARCHY_ACCNT_ID,SHORT_CODE";

            string fileName = "", strHTML = "", strTitle = "";
            DataSet dtsAccount = new DataSet();
            strTitle = "Business Collection Report (PRAN)";
            fileName = "Business_Collection_PRAN_Rpt";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>" + strTitle + "</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=17 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtPranFrom.DateString + "' To '" + dtPranTo.DateString + "'</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            //strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Wallet</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Name</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor District</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >Slabe Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Total Commission</td>";
            strHTML = strHTML + "<td valign='middle' >VAT 15%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After VAT</td>";

            strHTML = strHTML + "<td valign='middle' >AIT 10%</td>";
            strHTML = strHTML + "<td valign='middle' >Amount After AIT</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Rate</td>";
            strHTML = strHTML + "<td valign='middle' >Distributor Amount</td>";
            //strHTML = strHTML + "<td valign='middle' >Distributor Commission</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Rate</td>";
            strHTML = strHTML + "<td valign='middle' >MBL Commission</td>";

            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    totalTransactionAmount = Convert.ToDouble(prow["TOTAL_SUM"]);
                    if (totalTransactionAmount <= 15000000)
                    {
                        slabeRate = 0.00195;
                    }
                    else if (totalTransactionAmount <= 35000000)
                    {
                        slabeRate = 0.00175;
                    }
                    else if (totalTransactionAmount > 35000000)
                    {
                        slabeRate = 0.00150;
                    }
                    totalCommission = totalTransactionAmount * slabeRate;
                    vatAmount = totalCommission * 15 / 115;
                    amountAfterVat = totalCommission - vatAmount;


                    aitAmount = amountAfterVat * 10 / 100;

                    amountAfterAIT = amountAfterVat - aitAmount;
                    distributorAmount = amountAfterAIT * 81 / 100;
                    // distributorCommission = amountAfterAIT;
                    mblCommission = amountAfterAIT * 19 / 100;



                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    // strHTML = strHTML + " <td > " + prow["REQUEST_TIME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DEL_ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["DISTRICT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + prow["TOTAL_SUM"].ToString() + "</td>";
                    strHTML = strHTML + " <td > " + slabeRate.ToString() + "</td>";
                    strHTML = strHTML + " <td > " + totalCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + vatAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterVat.ToString("N2") + "</td>";

                    strHTML = strHTML + " <td > " + aitAmount.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > " + amountAfterAIT.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 81% </td>";
                    strHTML = strHTML + " <td > " + distributorAmount.ToString("N2") + "</td>";
                    //strHTML = strHTML + " <td > " + distributorCommission.ToString("N2") + "</td>";
                    strHTML = strHTML + " <td > 19% </td>";
                    strHTML = strHTML + " <td > " + mblCommission.ToString("N2") + "</td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                    gTotalTransactionAmount = gTotalTransactionAmount + totalTransactionAmount;
                    gTotalCommission = gTotalCommission + totalCommission;
                    gVatAmount = gVatAmount + vatAmount;
                    gAmountAfterVat = gAmountAfterVat + amountAfterVat;
                    gDistributorAmount = gDistributorAmount + distributorAmount;
                    gAitAmount = gAitAmount + aitAmount;
                    gAmountAfterAIT = gAmountAfterAIT + amountAfterAIT;
                    gDistributorCommission = gDistributorCommission + distributorCommission;
                    gMblCommission = gMblCommission + mblCommission;
                }
            }

            strHTML = strHTML + "<tr>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gTotalTransactionAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gTotalCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gVatAmount.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAmountAfterVat.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + gAitAmount.ToString("N2") + " </td>";

            strHTML = strHTML + " <td > " + gAmountAfterAIT.ToString("N2") + " </td>";

            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gDistributorAmount.ToString("N2") + " </td>";


            //strHTML = strHTML + " <td > " + gDistributorCommission.ToString("N2") + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + gMblCommission.ToString("N2") + " </td>";

            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion



}
