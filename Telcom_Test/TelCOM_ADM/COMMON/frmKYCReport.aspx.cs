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

public partial class COMMON_frmKYCReport : System.Web.UI.Page
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string strSql = "";
        try
        {
            string strAcntMsindn="+88"+txtWalletID.Text.Substring(0,txtWalletID.Text.Length-1) ;
            //strSql = " SELECT CL.CLINT_NAME,CL.CLINT_MOBILE,CL.CLINT_FATHER_NAME,CL.CLINT_MOTHER_NAME ,CL.CLIENT_DOB,CL.OCCUPATION,CL.WORK_EDU_BUSINESS,CL.PUR_OF_TRAN,"
            //      + " CL.CLIENT_OFFIC_ADDRESS,CL.CLINT_ADDRESS1 PRESENT_ADDRESS,CL.CLINT_ADDRESS2 PERMANET_ADDRESS,BA.BANK_NAME,BA.BANK_BR_NAME,BA.BANK_ACCNT_NO,"
            //      + " IDS.IDNTIFCTION_NAME,CI.CLINT_IDENT_NAME,IINF.INTRODCR_NAME,IINF.INTRODCR_MOBILE,IINF.INTRODCR_ADDRESS,IINF.INTRODCR_OCCUPATION,NI.NOMNE_NAME,"
            //      + " NI.NOMNE_MOBILE,NI.RELATION,NI.PERCENTAGE"
            //      + " FROM CLIENT_LIST CL,ACCOUNT_LIST AL,BANK_ACCOUNT BA,CLIENT_IDENTIFICATION CI,IDENTIFICATION_SETUP IDS,INTRODUCER_INFO IINF,NOMINEE_INFO NI"
            //      + " WHERE  AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_ID=BA.CLIENT_ID(+) AND CL.CLINT_ID=CI.CLIENT_ID(+) AND CI.IDNTIFCTION_ID=IDS.IDNTIFCTION_ID(+) AND CL.CLINT_ID=IINF.CLIENT_ID(+)"
            //      + " AND CL.CLINT_ID=NI.NOMNE_ID(+)  AND AL.ACCNT_NO='" + txtWalletID.Text.ToString() + "'";

            //strSql = "  SELECT DISTINCT ASD.SERIAL_NO,SP.SERVICE_PKG_NAME,CL.CLINT_NAME,CL.HUSBAND_NAME,CL.CLINT_MOBILE,CL.CLINT_FATHER_NAME,CL.CLINT_MOTHER_NAME ,CL.CLIENT_DOB,CL.OCCUPATION,CL.WORK_EDU_BUSINESS,CL.PUR_OF_TRAN,"
            //       + " CL.CLIENT_OFFIC_ADDRESS,CL.CLINT_ADDRESS1 PRESENT_ADDRESS,CL.CLINT_ADDRESS2 PERMANET_ADDRESS,CL.THANA_ID,BA.BANK_NAME,BA.BANK_BR_NAME, BA.BANK_ACCNT_NO,"
            //       + " IDS.IDNTIFCTION_NAME,CI.CLINT_IDENT_NAME,IINF.INTRODCR_NAME,IINF.INTRODCR_MOBILE,IINF.INTRODCR_ADDRESS,IINF.INTRODCR_OCCUPATION,NI.NOMNE_NAME, NI.NOMNE_MOBILE,NI.RELATION,NI.PERCENTAGE,"
            //       + " ASD.CUSTOMER_MOBILE_NO,ASD.AGENT_MOBILE_NO,MTC.THANA_ID , MTC.THANA_NAME AS ClientThana,MTC.DISTRICT_ID,MDC.DISTRICT_ID,MDC.DISTRICT_NAME AS ClientDisName,"
            //       + " ALS.ACCNT_MSISDN ,ALS.CLINT_ID AS AClientID,CLS.CLINT_ID AS AClientID, CLS.CLINT_NAME AS AName,CLS.THANA_ID,MT.THANA_ID, MT.THANA_NAME AS AgentThanaName,MT.DISTRICT_ID,"
            //       + " MD.DISTRICT_ID,MD.DISTRICT_NAME AS AgentDisNAme"
            //       + " FROM CLIENT_LIST CL,ACCOUNT_LIST AL,BANK_ACCOUNT BA,CLIENT_IDENTIFICATION CI,IDENTIFICATION_SETUP IDS,INTRODUCER_INFO IINF,NOMINEE_INFO NI,"
            //       + " ACCOUNT_SERIAL_DETAIL ASD,MANAGE_THANA MTC,MANAGE_DISTRICT MDC ,ACCOUNT_LIST ALS ,CLIENT_LIST CLS,MANAGE_THANA MT ,MANAGE_DISTRICT MD,SERVICE_PACKAGE SP "
            //       + " WHERE  AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_ID=BA.CLIENT_ID(+) AND CL.CLINT_ID=CI.CLIENT_ID(+) AND CI.IDNTIFCTION_ID=IDS.IDNTIFCTION_ID(+) AND CL.CLINT_ID=IINF.CLIENT_ID(+)"
            //       + " AND CL.CLINT_ID=NI.CLIENT_ID(+)   AND CL.CLINT_MOBILE=ASD.CUSTOMER_MOBILE_NO(+) AND  CL.THANA_ID=MTC.THANA_ID(+)"
            //       + " AND  MTC.DISTRICT_ID=MDC.DISTRICT_ID(+) AND ASD.AGENT_MOBILE_NO=ALS.ACCNT_MSISDN(+) AND ALS.CLINT_ID=CLS.CLINT_ID(+)"
            //       + " AND CLS.THANA_ID=MT.THANA_ID(+) AND MT.DISTRICT_ID=MD.DISTRICT_ID(+) AND AL.SERVICE_PKG_ID=SP.SERVICE_PKG_ID"
            //       + "  AND AL.ACCNT_NO='" + txtWalletID.Text.ToString() + "'";


            strSql = " SELECT DISTINCT ASD.SERIAL_NO, SP.SERVICE_PKG_NAME, CL.CLINT_NAME, CL.HUSBAND_NAME,  "
                    + " CL.CLINT_MOBILE, CL.CLINT_FATHER_NAME, CL.CLINT_MOTHER_NAME, CL.CLIENT_DOB, CL.OCCUPATION, "
                    + " CL.WORK_EDU_BUSINESS, CL.PUR_OF_TRAN, CL.CLIENT_OFFIC_ADDRESS, CL.CLINT_ADDRESS1 PRESENT_ADDRESS, "
                    + " CL.CLINT_ADDRESS2 PERMANET_ADDRESS,  CL.THANA_ID, BA.BANK_NAME, BA.BANK_BR_NAME, BA.BANK_ACCNT_NO, BA.REMARKS, "
                    + " IDS.IDNTIFCTION_NAME, CI.CLINT_IDENT_NAME, IINF.INTRODCR_NAME, IINF.INTRODCR_MOBILE, "
                    + " IINF.INTRODCR_ADDRESS, IINF.INTRODCR_OCCUPATION, NI.NOMNE_NAME, NI.NOMNE_MOBILE, NI.RELATION, "
                    + " NI.PERCENTAGE, ASD.CUSTOMER_MOBILE_NO, ASD.AGENT_MOBILE_NO, MTC.THANA_ID, "
                    + " MTC.THANA_NAME AS ClientThana, MTC.DISTRICT_ID, MDC.DISTRICT_ID, MDC.DISTRICT_NAME AS ClientDisName, "
                    + " ALS.ACCNT_MSISDN, ALS.CLINT_ID AS AClientID, CLS.CLINT_ID AS AClientID, CLS.CLINT_NAME AS AName, "
                    + " CLS.THANA_ID, MT.THANA_ID, MT.THANA_NAME AS AgentThanaName, MT.DISTRICT_ID, MD.DISTRICT_ID, "
                    + " MD.DISTRICT_NAME AS AgentDisNAme, CBB.BRANCH_NAME BANK_BRANCH_NAME FROM   CLIENT_LIST CL, "
                    + " ACCOUNT_LIST AL, BANK_ACCOUNT BA, CLIENT_IDENTIFICATION CI, IDENTIFICATION_SETUP IDS, INTRODUCER_INFO IINF, "
                    + " NOMINEE_INFO NI, ACCOUNT_SERIAL_DETAIL ASD, MANAGE_THANA MTC, MANAGE_DISTRICT MDC, ACCOUNT_LIST ALS, "
                    + " CLIENT_LIST CLS, MANAGE_THANA MT, MANAGE_DISTRICT MD, SERVICE_PACKAGE SP, CM_BANK_BRANCH CBB "
                    + " WHERE AL.CLINT_ID = CL.CLINT_ID AND CL.CLINT_ID = BA.CLIENT_ID(+) AND CL.CLINT_ID = CI.CLIENT_ID(+) "
                    + " AND CI.IDNTIFCTION_ID = IDS.IDNTIFCTION_ID(+) AND CL.CLINT_ID = IINF.CLIENT_ID(+) AND CL.CLINT_ID = NI.CLIENT_ID(+) "
                    + " AND CL.CLINT_MOBILE = ASD.CUSTOMER_MOBILE_NO(+) AND CL.THANA_ID = MTC.THANA_ID(+) AND MTC.DISTRICT_ID = MDC.DISTRICT_ID(+) "
                    + " AND ASD.AGENT_MOBILE_NO = ALS.ACCNT_MSISDN(+) AND ALS.CLINT_ID = CLS.CLINT_ID(+) AND CLS.THANA_ID = MT.THANA_ID(+) "
                    + " AND MT.DISTRICT_ID = MD.DISTRICT_ID(+) AND AL.SERVICE_PKG_ID = SP.SERVICE_PKG_ID AND AL.ACCNT_NO='" + txtWalletID.Text.ToString() + "'"
                    + " AND ASD.BANK_BRANCH_ID = CBB.BANK_BRNCH_ID(+) ";



            
            Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmKYCReport.aspx";
            Session["ReportFile"] = "../COMMON/crptKYCReport.rpt";
            SaveAuditInfo("View", "KYC Report");
            Response.Redirect("../COM/COM_ReportView.aspx");
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
