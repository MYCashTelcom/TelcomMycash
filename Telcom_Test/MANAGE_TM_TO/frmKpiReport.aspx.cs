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

    protected void btnTOUt_Click(object sender, EventArgs e)
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

            string formDt = "01/" + drpMonthToUt.Text.ToString()+"/"+ drpYearToUt.Text.ToString();
            string toDt = noofDaysInAMonth + drpYearToUt.Text.ToString();

            string bnchCusReg = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000003");
            string bnchUbp = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000001");
            string bnchLifting = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000002");
            string bnchCorp = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000004");
            string bnchTrx = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000005");
            string bnchActvAgnt = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000006");



            string strSql = "";
            //strSql = "SELECT * FROM KPI_TEMP_REPORT1";

            //// Updated by Md. Zubair Hossain
            //// Date : 23-Aug-2022
            strSql = "  SELECT distinct MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO TO_NO, MKT.CUST_ACQU_TARGET,NVL(FUNC_GET_CUS_REG_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, NVL(FUNC_GET_ACTV_AGNT_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET,NVL(FUNC_GET_LIFTING_ACHIV_TO_V2 (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET, NVL(FUNC_GET_CORPORT_COLL_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)+NVL(GET_RLIL_AMOUNT(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)+NVL(GET_PARTEX_AMOUNT(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,  NVL(FUNC_GET_AGNT_TRX_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,NVL(FUNC_GET_UTILITY_TRX_TO_V2(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) UTILITY_AMOUNT_ACHIVE  FROM MANAGE_AREA MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT   WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+) AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthToUt.Text.ToString() + "' AND MKT.TARGET_YEAR='" + drpYearToUt.Text.ToString() + "'AND UTILITY_AMOUNT_TARGET>0";


            // new --- strSql = " SELECT distinct MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO TO_NO, MKT.CUST_ACQU_TARGET, NVL(FUNC_GET_CUS_REG_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, NVL(FUNC_GET_ACTV_AGNT_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET, NVL(FUNC_GET_LIFTING_ACHIV_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET, NVL(FUNC_GET_CORPORT_COLL_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,  NVL(FUNC_GET_AGNT_TRX_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,NVL(FUNC_GET_UTILITY_TRX_TO_V2(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) UTILITY_AMOUNT_ACHIVE FROM MANAGE_AREA MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT   WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)  AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthToUt.Text.ToString() + "' AND MKT.TARGET_YEAR='" + drpYearToUt.Text.ToString() + "'AND UTILITY_AMOUNT_TARGET>0";

        //    strSql = "SELECT  distinct MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO TO_NO, MKT.CUST_ACQU_TARGET, 50  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, 200   ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET,10000 LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET,   100000000 CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,   50000 TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,    659986320 UTILITY_AMOUNT_ACHIVE FROM MANAGE_AREA    MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM,   MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT       WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)       AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)      AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID      AND  MKT.TARGET_MONTH='Jul' AND MKT.TARGET_YEAR='2021' AND UTILITY_AMOUNT_TARGET>0 ";


            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Kpi Report TO(utility)";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Kpi Report TO(utility)(" + drpMonthToUt.SelectedValue + "-" + drpYearToUt.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center> Date Range: " + formDt + " to " + toDt + " </h4></td>  </tr>";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td></tr>";           
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Number</td>";

            strHTML = strHTML + "<td valign='middle' >Customer Reg Target ("+bnchCusReg+")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Active Agent Target ("+bnchActvAgnt+")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Lifting Amount Target ("+bnchLifting+")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Corporate Collection Target ("+bnchCorp+")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Transaction Amount Target ("+bnchTrx+")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Utility Amount Target ("+bnchUbp+")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Total Score</td>";            
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                     
                    //registration calculation
                    string regTergt = prow["CUST_ACQU_TARGET"].ToString();
                    string regAchiv = prow["CUST_ACQU_ACHIVE"].ToString();
                    double regRatio = (Convert.ToDouble(regAchiv) * 100 )/ Convert.ToDouble(regTergt);
                    regRatio = System.Math.Round(regRatio, 2);
                    double regScore = (Convert.ToDouble(regAchiv) / Convert.ToDouble(regTergt)) * Convert.ToDouble(bnchCusReg);
                    if (regRatio > 150)
                    {
                        regScore = Convert.ToDouble(bnchCusReg) * 1.5;
                    }
                    else if (regRatio < 27)
                    {
                        regScore = 0;
                    }
                    else
                    {
                        regScore = System.Math.Round(regScore, 2);
                    }
                    // active agent calculation
                    string actvAgntTergt = prow["ACTIVE_AGENTNO_TARGET"].ToString();
                    string actvAgntAchiv = prow["ACTIVE_AGNT_ACHIVE"].ToString();
                    double actvAgntRatio = (Convert.ToDouble(actvAgntAchiv) * 100) / Convert.ToDouble(actvAgntTergt);
                    actvAgntRatio = System.Math.Round(actvAgntRatio, 2);
                    double actvAgnScore = (Convert.ToDouble(actvAgntAchiv) / Convert.ToDouble(actvAgntTergt)) * Convert.ToDouble(bnchActvAgnt);
                    if (actvAgntRatio > 150)
                    {
                        actvAgnScore = Convert.ToDouble(bnchActvAgnt) * 1.5;
                    }
                    else if (actvAgntRatio < 27)
                    {
                        actvAgnScore = 0;
                    }
                    else
                    {
                        actvAgnScore = System.Math.Round(actvAgnScore, 2);
                    }

                    //lifting calculation
                    string liftingTergt = prow["LIFTING_AMOUNT_TARGET"].ToString();
                    string liftingAchiv = prow["LIFTING_AMT_ACHIVE"].ToString();
                    double liftingRatio = (Convert.ToDouble(liftingAchiv) * 100) / Convert.ToDouble(liftingTergt);
                    liftingRatio = System.Math.Round(liftingRatio, 2);
                    double liftingScore = (Convert.ToDouble(liftingAchiv) / Convert.ToDouble(liftingTergt)) * Convert.ToDouble(bnchLifting);
                    if (liftingRatio > 150)
                    {
                        liftingScore = Convert.ToDouble(bnchLifting) * 1.5;
                    }
                    else if (liftingRatio < 27)
                    {
                        liftingScore = 0;
                    }
                    else
                    {
                        liftingScore = System.Math.Round(liftingScore, 2);
                    }

                    //corport calculation
                    string corpTergt = prow["CORP_COLLECTION_TARGET"].ToString();
                    string corpAchiv = prow["CORP_COLL_ACHIVE"].ToString();
                    double corpRatio = (Convert.ToDouble(corpAchiv) * 100) / Convert.ToDouble(corpTergt);
                    corpRatio = System.Math.Round(corpRatio, 2);
                    double corpScore = (Convert.ToDouble(corpAchiv) / Convert.ToDouble(corpTergt)) * Convert.ToDouble(bnchCorp);
                    if (corpRatio > 150)
                    {
                        corpScore = Convert.ToDouble(bnchCorp) * 1.5;
                    }
                    else if (corpRatio < 27)
                    {
                        corpScore = 0;
                    }
                    else
                    {
                        corpScore = System.Math.Round(corpScore, 2);
                    }

                    //transaction calculation
                    string trxTergt = prow["TRX_AMT_TARGET"].ToString();
                    string trxAchiv = prow["TRX_AMT_ACHIVE"].ToString();
                    double trxRatio = (Convert.ToDouble(trxAchiv) * 100) / Convert.ToDouble(trxTergt);
                    trxRatio = System.Math.Round(trxRatio, 2);
                    double trxScore = (Convert.ToDouble(trxAchiv) / Convert.ToDouble(trxTergt)) * Convert.ToDouble(bnchTrx);
                    if (trxRatio > 150)
                    {
                        trxScore = Convert.ToDouble(bnchTrx) * 1.5;
                    }
                    else if (trxRatio < 27)
                    {
                        trxScore = 0;
                    }
                    else
                    {
                        trxScore = System.Math.Round(trxScore, 2);
                    }

                    //utility calculation
                    string utTergt = prow["UTILITY_AMOUNT_TARGET"].ToString();
                    string utAchiv = prow["UTILITY_AMOUNT_ACHIVE"].ToString();
                    double utRatio = (Convert.ToDouble(utAchiv) * 100) / Convert.ToDouble(utTergt);
                    utRatio = System.Math.Round(utRatio, 2);
                    double utScore = (Convert.ToDouble(utAchiv) / Convert.ToDouble(utTergt)) * Convert.ToDouble(bnchUbp);
                    if (utRatio > 150)
                    {
                        utScore = Convert.ToDouble(bnchUbp) * 1.5;
                    }
                    else if (utRatio < 27)
                    {
                        utScore = 0;
                    }
                    else
                    {
                        utScore = System.Math.Round(utScore, 2);
                    }
                    //total score calculation
                    double totalScore = actvAgnScore + corpScore + liftingScore + regScore + trxScore + utScore;
                    if (actvAgnScore == 0 || corpScore == 0 || liftingScore == 0 || regScore == 0 || trxScore == 0 || utScore == 0)
                    {
                        totalScore = 0;
                    }
                    



                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + regRatio + " </td>";
                    strHTML = strHTML + " <td > '" + regScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["ACTIVE_AGENTNO_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACTIVE_AGNT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + actvAgntRatio + " </td>";
                    strHTML = strHTML + " <td > '" + actvAgnScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["LIFTING_AMOUNT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["LIFTING_AMT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + liftingRatio + " </td>";
                    strHTML = strHTML + " <td > '" + liftingScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CORP_COLLECTION_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + corpRatio + " </td>";
                    strHTML = strHTML + " <td > '" + corpScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["TRX_AMT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + trxRatio + " </td>";
                    strHTML = strHTML + " <td > '" + trxScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["UTILITY_AMOUNT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UTILITY_AMOUNT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + utRatio + " </td>";
                    strHTML = strHTML + " <td > '" + utScore + " </td>";

                  


                    strHTML = strHTML + " <td > '" + totalScore + " </td>";                   

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }            

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

    protected void btnTONu_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpMonthToNu.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpYearToNu.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }



            string noofDaysInAMonth = "";
            if (drpMonthToNu.SelectedValue == "Jan")
            {
                noofDaysInAMonth = "31/Jan/";
            }
            else if (drpMonthToNu.SelectedValue == "Feb")
            {
                if (int.Parse(drpYearToNu.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = "29/Feb/";
                }
                else
                {
                    noofDaysInAMonth = "28/Feb/";
                }

            }
            else if (drpMonthToNu.SelectedValue == "Mar")
            {
                noofDaysInAMonth = "31/Mar/";
            }
            else if (drpMonthToNu.SelectedValue == "Apr")
            {
                noofDaysInAMonth = "30/Apr/";
            }
            else if (drpMonthToNu.SelectedValue == "May")
            {
                noofDaysInAMonth = "31/May/";
            }
            else if (drpMonthToNu.SelectedValue == "Jun")
            {
                noofDaysInAMonth = "30/Jun/";
            }
            else if (drpMonthToNu.SelectedValue == "Jul")
            {
                noofDaysInAMonth = "31/Jul/";
            }
            else if (drpMonthToNu.SelectedValue == "Aug")
            {
                noofDaysInAMonth = "31/Aug/";
            }
            else if (drpMonthToNu.SelectedValue == "Sep")
            {
                noofDaysInAMonth = "30/Sep/";
            }
            else if (drpMonthToNu.SelectedValue == "Oct")
            {
                noofDaysInAMonth = "31/Oct/";
            }
            else if (drpMonthToNu.SelectedValue == "Nov")
            {
                noofDaysInAMonth = "30/Nov/";
            }
            else if (drpMonthToNu.SelectedValue == "Dec")
            {
                noofDaysInAMonth = "31/Dec/";
            }

            else
            {
                // do nothing
            }

            string formDt = "01/" + drpMonthToNu.Text.ToString() + "/" + drpYearToNu.Text.ToString();
            string toDt = noofDaysInAMonth + drpYearToNu.Text.ToString();

            string bnchCusReg = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000008");
            string bnchUbp = "0";
            string bnchLifting = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000007");
            string bnchCorp = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000009");
            string bnchTrx = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000010");
            string bnchActvAgnt = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000011");



            string strSql = "";
            //strSql = " SELECT distinct MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO TO_NO, MKT.CUST_ACQU_TARGET, NVL(FUNC_GET_CUS_REG_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, NVL(FUNC_GET_ACTV_AGNT_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET, NVL(FUNC_GET_LIFTING_ACHIV_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET, NVL(FUNC_GET_CORPORT_COLL_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,  NVL(FUNC_GET_AGNT_TRX_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,0 UTILITY_AMOUNT_ACHIVE FROM MANAGE_AREA MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT   WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)  AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthToNu.Text.ToString() + "' AND MKT.TARGET_YEAR='" + drpYearToNu.Text.ToString() + "'AND UTILITY_AMOUNT_TARGET=0";

                //strSql = "SELECT  distinct MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO TO_NO, MKT.CUST_ACQU_TARGET, 50  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, 200   ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET,10000 LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET,   100000000 CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,   50000 TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,    659986320 UTILITY_AMOUNT_ACHIVE FROM MANAGE_AREA    MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM,   MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT       WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)       AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)      AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID      AND  MKT.TARGET_MONTH='Dec' AND MKT.TARGET_YEAR='2021' AND UTILITY_AMOUNT_TARGET>0 ";

            //Update By : Md.Jahirul Islam
            // Date : Jan-13-2022 
            strSql = " SELECT distinct MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO TO_NO, MKT.CUST_ACQU_TARGET, NVL(FUNC_GET_CUS_REG_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, NVL(FUNC_GET_ACTV_AGNT_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET, NVL(FUNC_GET_LIFTING_ACHIV_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET, NVL(FUNC_GET_CORPORT_COLL_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,  NVL(FUNC_GET_AGNT_TRX_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,0 UTILITY_AMOUNT_ACHIVE FROM MANAGE_AREA MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT   WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)  AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthToNu.Text.ToString() + "' AND MKT.TARGET_YEAR='" + drpYearToNu.Text.ToString() + "'AND UTILITY_AMOUNT_TARGET=0";


            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Kpi Report TO(Non-utility)";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Kpi Report TO(Non-utility)(" + drpMonthToNu.SelectedValue + "-" + drpYearToNu.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center> Date Range: " + formDt + " to " + toDt + " </h4></td>  </tr>";
            strHTML = strHTML + "<tr><td COLSPAN=31 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td></tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Name</td>";
            strHTML = strHTML + "<td valign='middle' >TO Number</td>";

            strHTML = strHTML + "<td valign='middle' >Customer Reg Target (" + bnchCusReg + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Active Agent Target (" + bnchActvAgnt + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Lifting Amount Target (" + bnchLifting + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Corporate Collection Target (" + bnchCorp + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Transaction Amount Target (" + bnchTrx + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Utility Amount Target (" + bnchUbp + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Total Score</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {

                    //registration calculation
                    string regTergt = prow["CUST_ACQU_TARGET"].ToString();
                    string regAchiv = prow["CUST_ACQU_ACHIVE"].ToString();
                    double regRatio = (Convert.ToDouble(regAchiv) * 100) / Convert.ToDouble(regTergt);
                    regRatio = System.Math.Round(regRatio, 2);
                    double regScore = (Convert.ToDouble(regAchiv) / Convert.ToDouble(regTergt)) * Convert.ToDouble(bnchCusReg);
                    if (regRatio > 150)
                    {
                        regScore = Convert.ToDouble(bnchCusReg) * 1.5;
                    }
                    else if (regRatio < 27 )
                    {
                        regScore = 0;
                    }
                    else
                    {
                        regScore = System.Math.Round(regScore, 2);
                    }
                    // active agent calculation
                    string actvAgntTergt = prow["ACTIVE_AGENTNO_TARGET"].ToString();
                    string actvAgntAchiv = prow["ACTIVE_AGNT_ACHIVE"].ToString();
                    double actvAgntRatio = (Convert.ToDouble(actvAgntAchiv) * 100) / Convert.ToDouble(actvAgntTergt);
                    actvAgntRatio = System.Math.Round(actvAgntRatio, 2);
                    double actvAgnScore = (Convert.ToDouble(actvAgntAchiv) / Convert.ToDouble(actvAgntTergt)) * Convert.ToDouble(bnchActvAgnt);
                    if (actvAgntRatio > 150)
                    {
                        actvAgnScore = Convert.ToDouble(bnchActvAgnt) * 1.5;
                    }
                    else if (actvAgntRatio < 27)
                    {
                        actvAgnScore = 0;
                    }
                    else
                    {
                        actvAgnScore = System.Math.Round(actvAgnScore, 2);
                    }

                    //lifting calculation
                    string liftingTergt = prow["LIFTING_AMOUNT_TARGET"].ToString();
                    string liftingAchiv = prow["LIFTING_AMT_ACHIVE"].ToString();
                    double liftingRatio = (Convert.ToDouble(liftingAchiv) * 100) / Convert.ToDouble(liftingTergt);
                    liftingRatio = System.Math.Round(liftingRatio, 2);
                    double liftingScore = (Convert.ToDouble(liftingAchiv) / Convert.ToDouble(liftingTergt)) * Convert.ToDouble(bnchLifting);
                    if (liftingRatio > 150)
                    {
                        liftingScore = Convert.ToDouble(bnchLifting) * 1.5;
                    }
                    else if (liftingRatio < 27 )
                    {
                        liftingScore = 0;
                    }
                    else
                    {
                        liftingScore = System.Math.Round(liftingScore, 2);
                    }

                    //corport calculation
                    string corpTergt = prow["CORP_COLLECTION_TARGET"].ToString();
                    string corpAchiv = prow["CORP_COLL_ACHIVE"].ToString();
                    double corpRatio = (Convert.ToDouble(corpAchiv) * 100) / Convert.ToDouble(corpTergt);
                    corpRatio = System.Math.Round(corpRatio, 2);
                    double corpScore = (Convert.ToDouble(corpAchiv) / Convert.ToDouble(corpTergt)) * Convert.ToDouble(bnchCorp);
                    if (corpRatio > 150)
                    {
                        corpScore = Convert.ToDouble(bnchCorp) * 1.5;
                    }
                    else if (corpRatio < 27 || corpTergt=="0")
                    {
                        corpScore = 0;
                    }
                    else
                    {
                        corpScore = System.Math.Round(corpScore, 2);
                    }

                    //transaction calculation
                    string trxTergt = prow["TRX_AMT_TARGET"].ToString();
                    string trxAchiv = prow["TRX_AMT_ACHIVE"].ToString();
                    double trxRatio = (Convert.ToDouble(trxAchiv) * 100) / Convert.ToDouble(trxTergt);
                    trxRatio = System.Math.Round(trxRatio, 2);
                    double trxScore = (Convert.ToDouble(trxAchiv) / Convert.ToDouble(trxTergt)) * Convert.ToDouble(bnchTrx);
                    if (trxRatio > 150)
                    {
                        trxScore = Convert.ToDouble(bnchTrx) * 1.5;
                    }
                    else if (trxRatio < 27 )
                    {
                        trxScore = 0;
                    }
                    else
                    {
                        trxScore = System.Math.Round(trxScore, 2);
                    }

                    //utility calculation
                    string utTergt = prow["UTILITY_AMOUNT_TARGET"].ToString();
                    string utAchiv = prow["UTILITY_AMOUNT_ACHIVE"].ToString();
                    double utRatio = 0;
                    double utScore = 0;
                    
                    //total score calculation
                    double totalScore = actvAgnScore + corpScore + liftingScore + regScore + trxScore + utScore;
                    if (actvAgnScore == 0 || corpScore == 0 || liftingScore == 0 || regScore == 0 || trxScore == 0 )
                    {
                        totalScore = 0;
                    }




                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TO_NO"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + regRatio + " </td>";
                    strHTML = strHTML + " <td > '" + regScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["ACTIVE_AGENTNO_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACTIVE_AGNT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + actvAgntRatio + " </td>";
                    strHTML = strHTML + " <td > '" + actvAgnScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["LIFTING_AMOUNT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["LIFTING_AMT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + liftingRatio + " </td>";
                    strHTML = strHTML + " <td > '" + liftingScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CORP_COLLECTION_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + corpRatio + " </td>";
                    strHTML = strHTML + " <td > '" + corpScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["TRX_AMT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + trxRatio + " </td>";
                    strHTML = strHTML + " <td > '" + trxScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["UTILITY_AMOUNT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UTILITY_AMOUNT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + utRatio + " </td>";
                    strHTML = strHTML + " <td > '" + utScore + " </td>";

                    strHTML = strHTML + " <td > '" + totalScore + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            SaveAuditInfo("Preview", "Kpi Report tm - non-utility");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnTMUt_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpMonthTMut.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (drpYearTmUt.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }



            string noofDaysInAMonth = "";
            if (drpMonthTMut.SelectedValue == "Jan")
            {
                noofDaysInAMonth = "31/Jan/";
            }
            else if (drpMonthTMut.SelectedValue == "Feb")
            {
                if (int.Parse(drpYearTmUt.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = "29/Feb/";
                }
                else
                {
                    noofDaysInAMonth = "28/Feb/";
                }

            }
            else if (drpMonthTMut.SelectedValue == "Mar")
            {
                noofDaysInAMonth = "31/Mar/";
            }
            else if (drpMonthTMut.SelectedValue == "Apr")
            {
                noofDaysInAMonth = "30/Apr/";
            }
            else if (drpMonthTMut.SelectedValue == "May")
            {
                noofDaysInAMonth = "31/May/";
            }
            else if (drpMonthTMut.SelectedValue == "Jun")
            {
                noofDaysInAMonth = "30/Jun/";
            }
            else if (drpMonthTMut.SelectedValue == "Jul")
            {
                noofDaysInAMonth = "31/Jul/";
            }
            else if (drpMonthTMut.SelectedValue == "Aug")
            {
                noofDaysInAMonth = "31/Aug/";
            }
            else if (drpMonthTMut.SelectedValue == "Sep")
            {
                noofDaysInAMonth = "30/Sep/";
            }
            else if (drpMonthTMut.SelectedValue == "Oct")
            {
                noofDaysInAMonth = "31/Oct/";
            }
            else if (drpMonthTMut.SelectedValue == "Nov")
            {
                noofDaysInAMonth = "30/Nov/";
            }
            else if (drpMonthTMut.SelectedValue == "Dec")
            {
                noofDaysInAMonth = "31/Dec/";
            }

            else
            {
                // do nothing
            }

            string formDt = "01/" + drpMonthTMut.Text.ToString() + "/" + drpYearTmUt.Text.ToString();
            string toDt = noofDaysInAMonth + drpYearTmUt.Text.ToString();

            string bnchCusReg = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000003");
            string bnchUbp = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000001");
            string bnchLifting = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000002");
            string bnchCorp = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000004");
            string bnchTrx = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000005");
            string bnchActvAgnt = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000006");



            string strSql = "";
            strSql = "SELECT distinct MA.AREA_NAME,MR.REGION_NAME,T1.TM_NAME, T1.TM_NO,SUM(CUST_ACQU_TARGET) CUST_ACQU_TARGET, SUM(CUST_ACQU_ACHIVE) CUST_ACQU_ACHIVE, SUM(ACTIVE_AGENTNO_TARGET) ACTIVE_AGENTNO_TARGET, SUM(ACTIVE_AGNT_ACHIVE) ACTIVE_AGNT_ACHIVE, SUM(LIFTING_AMOUNT_TARGET) LIFTING_AMOUNT_TARGET, SUM(LIFTING_AMT_ACHIVE) LIFTING_AMT_ACHIVE,  SUM(CORP_COLLECTION_TARGET) CORP_COLLECTION_TARGET,  SUM(CORP_COLL_ACHIVE) CORP_COLL_ACHIVE, SUM(TRX_AMT_TARGET) TRX_AMT_TARGET,  SUM(TRX_AMT_ACHIVE) TRX_AMT_ACHIVE,  SUM(UTILITY_AMOUNT_TARGET) UTILITY_AMOUNT_TARGET,   SUM(UTILITY_AMOUNT_ACHIVE) UTILITY_AMOUNT_ACHIVE  FROM (SELECT DISTINCT CLTM.CLINT_NAME TM_NAME, ALTM.ACCNT_NO TM_NO, ALTM.ACCNT_ID ACCNT_ID,CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO, MKT.CUST_ACQU_TARGET, nvl(FUNC_GET_CUS_REG_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET,nvl( FUNC_GET_ACTV_AGNT_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET, nvl(FUNC_GET_LIFTING_ACHIV_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) LIFTING_AMT_ACHIVE,MKT.CORP_COLLECTION_TARGET,nvl( FUNC_GET_CORPORT_COLL_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET, nvl( FUNC_GET_AGNT_TRX_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  TRX_AMT_ACHIVE,  MKT.UTILITY_AMOUNT_TARGET, nvl(FUNC_GET_UTILITY_TRX_TO_V2(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  UTILITY_AMOUNT_ACHIVE   FROM ACCOUNT_LIST ALTM,  CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO,  MANAGE_KPI_TARGET MKT   WHERE   ALTM.CLINT_ID = CLTM.CLINT_ID(+)   AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+)  AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)  AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthTMut.Text.ToString() + "'  AND MKT.TARGET_YEAR='" + drpYearTmUt.Text.ToString() + "' )T1,  (SELECT ALTM.ACCNT_NO TM_NO FROM  ACCOUNT_LIST ALTM,  CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,   ACCOUNT_LIST ALTO, CLIENT_LIST CLTO,  MANAGE_KPI_TARGET MKT   WHERE   ALTM.CLINT_ID = CLTM.CLINT_ID(+)   AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+)  AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)  AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthTMut.Text.ToString() + "' AND MKT.TARGET_YEAR='" + drpYearTmUt.Text.ToString() + "' HAVING SUM(MKT.UTILITY_AMOUNT_TARGET)>0 GROUP BY ALTM.ACCNT_NO) T2,MANAGE_AREA MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA  WHERE T1.TM_NO=T2.TM_NO AND MA.AREA_ID = MTA.AREA_ID  AND MTA.ACCNT_ID = T1.ACCNT_ID  AND MR.REGION_ID = MA.REGION_ID    GROUP BY T1.TM_NO,TM_NAME,  MA.AREA_NAME, MR.REGION_NAME";

            //    strSql = "SELECT  distinct MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO TO_NO, MKT.CUST_ACQU_TARGET, 50  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, 200   ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET,10000 LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET,   100000000 CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,   50000 TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,    659986320 UTILITY_AMOUNT_ACHIVE FROM MANAGE_AREA    MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM,   MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT       WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)       AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)      AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID      AND  MKT.TARGET_MONTH='Jul' AND MKT.TARGET_YEAR='2021' AND UTILITY_AMOUNT_TARGET>0 ";


            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Kpi Report TM(utility)";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Kpi Report TM(utility)(" + drpMonthTMut.SelectedValue + "-" + drpYearTmUt.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center> Date Range: " + formDt + " to " + toDt + " </h4></td>  </tr>";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td></tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TM No</td>";
            

            strHTML = strHTML + "<td valign='middle' >Customer Reg Target (" + bnchCusReg + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Active Agent Target (" + bnchActvAgnt + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Lifting Amount Target (" + bnchLifting + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Corporate Collection Target (" + bnchCorp + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Transaction Amount Target (" + bnchTrx + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Utility Amount Target (" + bnchUbp + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Total Score</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {

                    //registration calculation
                    string regTergt = prow["CUST_ACQU_TARGET"].ToString();
                    string regAchiv = prow["CUST_ACQU_ACHIVE"].ToString();
                    double regRatio = (Convert.ToDouble(regAchiv) * 100) / Convert.ToDouble(regTergt);
                    regRatio = System.Math.Round(regRatio, 2);
                    double regScore = (Convert.ToDouble(regAchiv) / Convert.ToDouble(regTergt)) * Convert.ToDouble(bnchCusReg);
                    if (regRatio > 150)
                    {
                        regScore = Convert.ToDouble(bnchCusReg) * 1.5;
                    }
                    else if (regRatio < 27)
                    {
                        regScore = 0;
                    }
                    else
                    {
                        regScore = System.Math.Round(regScore, 2);
                    }
                    // active agent calculation
                    string actvAgntTergt = prow["ACTIVE_AGENTNO_TARGET"].ToString();
                    string actvAgntAchiv = prow["ACTIVE_AGNT_ACHIVE"].ToString();
                    double actvAgntRatio = (Convert.ToDouble(actvAgntAchiv) * 100) / Convert.ToDouble(actvAgntTergt);
                    actvAgntRatio = System.Math.Round(actvAgntRatio, 2);
                    double actvAgnScore = (Convert.ToDouble(actvAgntAchiv) / Convert.ToDouble(actvAgntTergt)) * Convert.ToDouble(bnchActvAgnt);
                    if (actvAgntRatio > 150)
                    {
                        actvAgnScore = Convert.ToDouble(bnchActvAgnt) * 1.5;
                    }
                    else if (actvAgntRatio < 27)
                    {
                        actvAgnScore = 0;
                    }
                    else
                    {
                        actvAgnScore = System.Math.Round(actvAgnScore, 2);
                    }

                    //lifting calculation
                    string liftingTergt = prow["LIFTING_AMOUNT_TARGET"].ToString();
                    string liftingAchiv = prow["LIFTING_AMT_ACHIVE"].ToString();
                    double liftingRatio = (Convert.ToDouble(liftingAchiv) * 100) / Convert.ToDouble(liftingTergt);
                    liftingRatio = System.Math.Round(liftingRatio, 2);
                    double liftingScore = (Convert.ToDouble(liftingAchiv) / Convert.ToDouble(liftingTergt)) * Convert.ToDouble(bnchLifting);
                    if (liftingRatio > 150)
                    {
                        liftingScore = Convert.ToDouble(bnchLifting) * 1.5;
                    }
                    else if (liftingRatio < 27)
                    {
                        liftingScore = 0;
                    }
                    else
                    {
                        liftingScore = System.Math.Round(liftingScore, 2);
                    }

                    //corport calculation
                    string corpTergt = prow["CORP_COLLECTION_TARGET"].ToString();
                    string corpAchiv = prow["CORP_COLL_ACHIVE"].ToString();
                    double corpRatio = (Convert.ToDouble(corpAchiv) * 100) / Convert.ToDouble(corpTergt);
                    corpRatio = System.Math.Round(corpRatio, 2);
                    double corpScore = (Convert.ToDouble(corpAchiv) / Convert.ToDouble(corpTergt)) * Convert.ToDouble(bnchCorp);
                    if (corpRatio > 150)
                    {
                        corpScore = Convert.ToDouble(bnchCorp) * 1.5;
                    }
                    else if (corpRatio < 27)
                    {
                        corpScore = 0;
                    }
                    else
                    {
                        corpScore = System.Math.Round(corpScore, 2);
                    }

                    //transaction calculation
                    string trxTergt = prow["TRX_AMT_TARGET"].ToString();
                    string trxAchiv = prow["TRX_AMT_ACHIVE"].ToString();
                    double trxRatio = (Convert.ToDouble(trxAchiv) * 100) / Convert.ToDouble(trxTergt);
                    trxRatio = System.Math.Round(trxRatio, 2);
                    double trxScore = (Convert.ToDouble(trxAchiv) / Convert.ToDouble(trxTergt)) * Convert.ToDouble(bnchTrx);
                    if (trxRatio > 150)
                    {
                        trxScore = Convert.ToDouble(bnchTrx) * 1.5;
                    }
                    else if (trxRatio < 27)
                    {
                        trxScore = 0;
                    }
                    else
                    {
                        trxScore = System.Math.Round(trxScore, 2);
                    }

                    //utility calculation
                    string utTergt = prow["UTILITY_AMOUNT_TARGET"].ToString();
                    string utAchiv = prow["UTILITY_AMOUNT_ACHIVE"].ToString();
                    double utRatio = (Convert.ToDouble(utAchiv) * 100) / Convert.ToDouble(utTergt);
                    utRatio = System.Math.Round(utRatio, 2);
                    double utScore = (Convert.ToDouble(utAchiv) / Convert.ToDouble(utTergt)) * Convert.ToDouble(bnchUbp);
                    if (utRatio > 150)
                    {
                        utScore = Convert.ToDouble(bnchUbp) * 1.5;
                    }
                    else if (utRatio < 27)
                    {
                        utScore = 0;
                    }
                    else
                    {
                        utScore = System.Math.Round(utScore, 2);
                    }
                    //total score calculation
                    double totalScore = actvAgnScore + corpScore + liftingScore + regScore + trxScore + utScore;
                    if (actvAgnScore == 0 || corpScore == 0 || liftingScore == 0 || regScore == 0 || trxScore == 0 || utScore == 0)
                    {
                        totalScore = 0;
                    }




                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";
                   

                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + regRatio + " </td>";
                    strHTML = strHTML + " <td > '" + regScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["ACTIVE_AGENTNO_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACTIVE_AGNT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + actvAgntRatio + " </td>";
                    strHTML = strHTML + " <td > '" + actvAgnScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["LIFTING_AMOUNT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["LIFTING_AMT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + liftingRatio + " </td>";
                    strHTML = strHTML + " <td > '" + liftingScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CORP_COLLECTION_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + corpRatio + " </td>";
                    strHTML = strHTML + " <td > '" + corpScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["TRX_AMT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + trxRatio + " </td>";
                    strHTML = strHTML + " <td > '" + trxScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["UTILITY_AMOUNT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UTILITY_AMOUNT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + utRatio + " </td>";
                    strHTML = strHTML + " <td > '" + utScore + " </td>";

                    strHTML = strHTML + " <td > '" + totalScore + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

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

    protected void btnTMNu_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (drpMonthTMNU.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Month";
                return;
            }
            if (frpYearTMNu.SelectedIndex == 0)
            {
                lblMsg.Text = "Select Year";
                return;
            }



            string noofDaysInAMonth = "";
            if (drpMonthTMNU.SelectedValue == "Jan")
            {
                noofDaysInAMonth = "31/Jan/";
            }
            else if (drpMonthTMNU.SelectedValue == "Feb")
            {
                if (int.Parse(frpYearTMNu.SelectedValue) % 4 == 0)
                {
                    noofDaysInAMonth = "29/Feb/";
                }
                else
                {
                    noofDaysInAMonth = "28/Feb/";
                }

            }
            else if (drpMonthTMNU.SelectedValue == "Mar")
            {
                noofDaysInAMonth = "31/Mar/";
            }
            else if (drpMonthTMNU.SelectedValue == "Apr")
            {
                noofDaysInAMonth = "30/Apr/";
            }
            else if (drpMonthTMNU.SelectedValue == "May")
            {
                noofDaysInAMonth = "31/May/";
            }
            else if (drpMonthTMNU.SelectedValue == "Jun")
            {
                noofDaysInAMonth = "30/Jun/";
            }
            else if (drpMonthTMNU.SelectedValue == "Jul")
            {
                noofDaysInAMonth = "31/Jul/";
            }
            else if (drpMonthTMNU.SelectedValue == "Aug")
            {
                noofDaysInAMonth = "31/Aug/";
            }
            else if (drpMonthTMNU.SelectedValue == "Sep")
            {
                noofDaysInAMonth = "30/Sep/";
            }
            else if (drpMonthTMNU.SelectedValue == "Oct")
            {
                noofDaysInAMonth = "31/Oct/";
            }
            else if (drpMonthTMNU.SelectedValue == "Nov")
            {
                noofDaysInAMonth = "30/Nov/";
            }
            else if (drpMonthTMNU.SelectedValue == "Dec")
            {
                noofDaysInAMonth = "31/Dec/";
            }

            else
            {
                // do nothing
            }

            string formDt = "01/" + drpMonthTMNU.Text.ToString() + "/" + frpYearTMNu.Text.ToString();
            string toDt = noofDaysInAMonth + frpYearTMNu.Text.ToString();

            string bnchCusReg = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000008");
            string bnchUbp = "0";
            string bnchLifting = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000007");
            string bnchCorp = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000009");
            string bnchTrx = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000010");
            string bnchActvAgnt = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("MANAGE_KPI_PARAMETERS", "BENCHMARK", "MANAGE_KPI_PARAMETERS_ID", "21070400000011");



            string strSql = "";
            strSql = " SELECT distinct MA.AREA_NAME,MR.REGION_NAME,T1.TM_NAME, T1.TM_NO,SUM(CUST_ACQU_TARGET) CUST_ACQU_TARGET, SUM(CUST_ACQU_ACHIVE) CUST_ACQU_ACHIVE, SUM(ACTIVE_AGENTNO_TARGET) ACTIVE_AGENTNO_TARGET, SUM(ACTIVE_AGNT_ACHIVE) ACTIVE_AGNT_ACHIVE, SUM(LIFTING_AMOUNT_TARGET) LIFTING_AMOUNT_TARGET, SUM(LIFTING_AMT_ACHIVE) LIFTING_AMT_ACHIVE,  SUM(CORP_COLLECTION_TARGET) CORP_COLLECTION_TARGET,  SUM(CORP_COLL_ACHIVE) CORP_COLL_ACHIVE, SUM(TRX_AMT_TARGET) TRX_AMT_TARGET,  SUM(TRX_AMT_ACHIVE) TRX_AMT_ACHIVE,  SUM(UTILITY_AMOUNT_TARGET) UTILITY_AMOUNT_TARGET,   SUM(UTILITY_AMOUNT_ACHIVE) UTILITY_AMOUNT_ACHIVE  FROM (SELECT DISTINCT CLTM.CLINT_NAME TM_NAME, ALTM.ACCNT_NO TM_NO, ALTM.ACCNT_ID ACCNT_ID,CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO, MKT.CUST_ACQU_TARGET, nvl(FUNC_GET_CUS_REG_TO(ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET,nvl( FUNC_GET_ACTV_AGNT_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET, nvl(FUNC_GET_LIFTING_ACHIV_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0) LIFTING_AMT_ACHIVE,MKT.CORP_COLLECTION_TARGET,nvl( FUNC_GET_CORPORT_COLL_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET, nvl( FUNC_GET_AGNT_TRX_TO (ALTO.ACCNT_NO,'" + formDt + "','" + toDt + "'),0)  TRX_AMT_ACHIVE,  MKT.UTILITY_AMOUNT_TARGET, 0  UTILITY_AMOUNT_ACHIVE   FROM ACCOUNT_LIST ALTM,  CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO,  MANAGE_KPI_TARGET MKT   WHERE   ALTM.CLINT_ID = CLTM.CLINT_ID(+)   AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+)  AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)  AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthTMNU.Text.ToString() + "'  AND MKT.TARGET_YEAR='" + frpYearTMNu.Text.ToString() + "' )T1,  (SELECT ALTM.ACCNT_NO TM_NO FROM  ACCOUNT_LIST ALTM,  CLIENT_LIST CLTM, MANAGE_TERRITORY_HIERARCHY MTHTM,   ACCOUNT_LIST ALTO, CLIENT_LIST CLTO,  MANAGE_KPI_TARGET MKT   WHERE   ALTM.CLINT_ID = CLTM.CLINT_ID(+)   AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+)  AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)  AND ALTO.CLINT_ID = CLTO.CLINT_ID(+)  AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID AND  MKT.TARGET_MONTH='" + drpMonthTMNU.Text.ToString() + "' AND MKT.TARGET_YEAR='" + frpYearTMNu.Text.ToString() + "' HAVING SUM(MKT.UTILITY_AMOUNT_TARGET)=0 GROUP BY ALTM.ACCNT_NO) T2,MANAGE_AREA MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA  WHERE T1.TM_NO=T2.TM_NO AND MA.AREA_ID = MTA.AREA_ID  AND MTA.ACCNT_ID = T1.ACCNT_ID  AND MR.REGION_ID = MA.REGION_ID    GROUP BY T1.TM_NO,TM_NAME,  MA.AREA_NAME, MR.REGION_NAME";

            //    strSql = "SELECT  distinct MA.AREA_NAME,MR.REGION_NAME, CLTM.CLINT_NAME TM_NAME, CLTO.CLINT_NAME TO_NAME,ALTO.ACCNT_NO TO_NO, MKT.CUST_ACQU_TARGET, 50  CUST_ACQU_ACHIVE, MKT.ACTIVE_AGENTNO_TARGET, 200   ACTIVE_AGNT_ACHIVE, MKT.LIFTING_AMOUNT_TARGET,10000 LIFTING_AMT_ACHIVE, MKT.CORP_COLLECTION_TARGET,   100000000 CORP_COLL_ACHIVE, MKT.TRX_AMT_TARGET,   50000 TRX_AMT_ACHIVE, MKT.UTILITY_AMOUNT_TARGET,    659986320 UTILITY_AMOUNT_ACHIVE FROM MANAGE_AREA    MA,MANAGE_REGION MR,  MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST ALTM, CLIENT_LIST CLTM,   MANAGE_TERRITORY_HIERARCHY MTHTM,  ACCOUNT_LIST ALTO, CLIENT_LIST CLTO, MANAGE_KPI_TARGET MKT       WHERE MA.AREA_ID = MTA.AREA_ID(+)  AND MTA.ACCNT_ID = ALTM.ACCNT_ID(+) AND ALTM.CLINT_ID = CLTM.CLINT_ID(+)       AND ALTM.ACCNT_ID = MTHTM.HIERARCHY_ACCNT_ID(+) AND MTHTM.ACCNT_ID = ALTO.ACCNT_ID(+)      AND ALTO.CLINT_ID = CLTO.CLINT_ID(+) AND MR.REGION_ID = MA.REGION_ID (+) AND MKT.TO_ACCNT_ID= ALTO.ACCNT_ID      AND  MKT.TARGET_MONTH='Jul' AND MKT.TARGET_YEAR='2021' AND UTILITY_AMOUNT_TARGET>0 ";


            string strHTML = "", fileName = "";
            DataSet dtsAccount = new DataSet();
            fileName = "Kpi Report TM(Non-utility)";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSql);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Kpi Report TM(Non-utility)(" + drpMonthTMNU.SelectedValue + "-" + frpYearTMNu.SelectedValue + " )</h2></td></tr>";
            strHTML = strHTML + "</table>";

            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=left style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center> Date Range: " + formDt + " to " + toDt + " </h4></td>  </tr>";
            strHTML = strHTML + "<tr><td COLSPAN=30 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=right> Print Date: " + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + " </h4></td></tr>";
            strHTML = strHTML + "</table>";


            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >Area</td>";
            strHTML = strHTML + "<td valign='middle' >District</td>";
            strHTML = strHTML + "<td valign='middle' >TM Name</td>";
            strHTML = strHTML + "<td valign='middle' >TM No</td>";
           
            strHTML = strHTML + "<td valign='middle' >Customer Reg Target (" + bnchCusReg + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Active Agent Target (" + bnchActvAgnt + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Lifting Amount Target (" + bnchLifting + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Corporate Collection Target (" + bnchCorp + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Transaction Amount Target (" + bnchTrx + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Utility Amount Target (" + bnchUbp + ")</td>";
            strHTML = strHTML + "<td valign='middle' >Achive</td>";
            strHTML = strHTML + "<td valign='middle' >Ratio(%)</td>";
            strHTML = strHTML + "<td valign='middle' >Score</td>";

            strHTML = strHTML + "<td valign='middle' >Total Score</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {

                    //registration calculation
                    string regTergt = prow["CUST_ACQU_TARGET"].ToString();
                    string regAchiv = prow["CUST_ACQU_ACHIVE"].ToString();
                    double regRatio = (Convert.ToDouble(regAchiv) * 100) / Convert.ToDouble(regTergt);
                    regRatio = System.Math.Round(regRatio, 2);
                    double regScore = (Convert.ToDouble(regAchiv) / Convert.ToDouble(regTergt)) * Convert.ToDouble(bnchCusReg);
                    if (regRatio > 150)
                    {
                        regScore = Convert.ToDouble(bnchCusReg) * 1.5;
                    }
                    else if (regRatio < 27)
                    {
                        regScore = 0;
                    }
                    else
                    {
                        regScore = System.Math.Round(regScore, 2);
                    }
                    // active agent calculation
                    string actvAgntTergt = prow["ACTIVE_AGENTNO_TARGET"].ToString();
                    string actvAgntAchiv = prow["ACTIVE_AGNT_ACHIVE"].ToString();
                    double actvAgntRatio = (Convert.ToDouble(actvAgntAchiv) * 100) / Convert.ToDouble(actvAgntTergt);
                    actvAgntRatio = System.Math.Round(actvAgntRatio, 2);
                    double actvAgnScore = (Convert.ToDouble(actvAgntAchiv) / Convert.ToDouble(actvAgntTergt)) * Convert.ToDouble(bnchActvAgnt);
                    if (actvAgntRatio > 150)
                    {
                        actvAgnScore = Convert.ToDouble(bnchActvAgnt) * 1.5;
                    }
                    else if (actvAgntRatio < 27)
                    {
                        actvAgnScore = 0;
                    }
                    else
                    {
                        actvAgnScore = System.Math.Round(actvAgnScore, 2);
                    }

                    //lifting calculation
                    string liftingTergt = prow["LIFTING_AMOUNT_TARGET"].ToString();
                    string liftingAchiv = prow["LIFTING_AMT_ACHIVE"].ToString();
                    double liftingRatio = (Convert.ToDouble(liftingAchiv) * 100) / Convert.ToDouble(liftingTergt);
                    liftingRatio = System.Math.Round(liftingRatio, 2);
                    double liftingScore = (Convert.ToDouble(liftingAchiv) / Convert.ToDouble(liftingTergt)) * Convert.ToDouble(bnchLifting);
                    if (liftingRatio > 150)
                    {
                        liftingScore = Convert.ToDouble(bnchLifting) * 1.5;
                    }
                    else if (liftingRatio < 27)
                    {
                        liftingScore = 0;
                    }
                    else
                    {
                        liftingScore = System.Math.Round(liftingScore, 2);
                    }

                    //corport calculation
                    string corpTergt = prow["CORP_COLLECTION_TARGET"].ToString();
                    string corpAchiv = prow["CORP_COLL_ACHIVE"].ToString();
                    double corpRatio = (Convert.ToDouble(corpAchiv) * 100) / Convert.ToDouble(corpTergt);
                    corpRatio = System.Math.Round(corpRatio, 2);
                    double corpScore = (Convert.ToDouble(corpAchiv) / Convert.ToDouble(corpTergt)) * Convert.ToDouble(bnchCorp);
                    if (corpRatio > 150)
                    {
                        corpScore = Convert.ToDouble(bnchCorp) * 1.5;
                    }
                    else if (corpRatio < 27 || corpTergt == "0")
                    {
                        corpScore = 0;
                    }
                    else
                    {
                        corpScore = System.Math.Round(corpScore, 2);
                    }

                    //transaction calculation
                    string trxTergt = prow["TRX_AMT_TARGET"].ToString();
                    string trxAchiv = prow["TRX_AMT_ACHIVE"].ToString();
                    double trxRatio = (Convert.ToDouble(trxAchiv) * 100) / Convert.ToDouble(trxTergt);
                    trxRatio = System.Math.Round(trxRatio, 2);
                    double trxScore = (Convert.ToDouble(trxAchiv) / Convert.ToDouble(trxTergt)) * Convert.ToDouble(bnchTrx);
                    if (trxRatio > 150)
                    {
                        trxScore = Convert.ToDouble(bnchTrx) * 1.5;
                    }
                    else if (trxRatio < 27)
                    {
                        trxScore = 0;
                    }
                    else
                    {
                        trxScore = System.Math.Round(trxScore, 2);
                    }

                    //utility calculation
                    string utTergt = prow["UTILITY_AMOUNT_TARGET"].ToString();
                    string utAchiv = prow["UTILITY_AMOUNT_ACHIVE"].ToString();
                    double utRatio = 0;
                    double utScore = 0;

                    //total score calculation
                    double totalScore = actvAgnScore + corpScore + liftingScore + regScore + trxScore + utScore;
                    if (actvAgnScore == 0 || corpScore == 0 || liftingScore == 0 || regScore == 0 || trxScore == 0)
                    {
                        totalScore = 0;
                    }




                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["AREA_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REGION_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NAME"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["TM_NO"].ToString() + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CUST_ACQU_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + regRatio + " </td>";
                    strHTML = strHTML + " <td > '" + regScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["ACTIVE_AGENTNO_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["ACTIVE_AGNT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + actvAgntRatio + " </td>";
                    strHTML = strHTML + " <td > '" + actvAgnScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["LIFTING_AMOUNT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["LIFTING_AMT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + liftingRatio + " </td>";
                    strHTML = strHTML + " <td > '" + liftingScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["CORP_COLLECTION_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CORP_COLL_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + corpRatio + " </td>";
                    strHTML = strHTML + " <td > '" + corpScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["TRX_AMT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["TRX_AMT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + trxRatio + " </td>";
                    strHTML = strHTML + " <td > '" + trxScore + " </td>";

                    strHTML = strHTML + " <td > '" + prow["UTILITY_AMOUNT_TARGET"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UTILITY_AMOUNT_ACHIVE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + utRatio + " </td>";
                    strHTML = strHTML + " <td > '" + utScore + " </td>";

                    strHTML = strHTML + " <td > '" + totalScore + " </td>";

                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;

                }
            }

            SaveAuditInfo("Preview", "Kpi Report tm - non-utility");
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