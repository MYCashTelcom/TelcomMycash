using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services.Description;

public partial class Forms_frmSubmitMSG : System.Web.UI.Page
{
    public clsServiceHandler objServiceHandler = new clsServiceHandler();
    public Service objWebService = new Service();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    public int intQuiz = 0;
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
        //if (Session["intQuiz"].ToString().Equals("0"))
        //if (intQuiz==0)
        //{
        //    ddlQuizList.Visible = false;
        //}
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
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string strReceipent;
        strReceipent = txtMSISDN.Text.ToString();
        string[] strAllMSISDN;
        string strTem = "";
        int intIndex;
        string strSender;
        string strRefNo;


        strAllMSISDN = txtMSISDN.Text.ToString().Split(';');
        //####################
        if (txtMessageSender.Text.ToString().Equals(""))
        {
            strSender = ddlAccountList.SelectedItem.Text.Substring(ddlAccountList.SelectedItem.Text.IndexOf('[') + 1);
            strSender = strSender.Substring(0, strSender.Length - 1);
        }
        else
        {
            strSender = txtMessageSender.Text.ToString();
        }
        //######################
        if (ddlMessagePurpose.SelectedIndex == 2)
        {
            strRefNo = ddlQuizList.SelectedValue;
        }
        else
        {
            strRefNo = "";
        }
        //######################
        for (intIndex = 0; intIndex < strAllMSISDN.Length; intIndex++)
        {
            strTem = AddServiceRequest(strSender, strAllMSISDN[intIndex].ToString(), txtMessage.Text, ddlAccountList.SelectedValue, ddlMessagePurpose.SelectedValue, strRefNo);
        }
        txtMessage.Text = txtMessage.Text + "\nMessage submitted successfully";

    }
    public string AddServiceRequest(string strFrom, string strTo, string strMessage, string strAccID, string strMessagePurpose,string strRefNo)
    {

        try
        {
            string connectionInfo = ConfigurationManager.AppSettings["dbConnectionString"];
            OracleConnection conn = new OracleConnection(connectionInfo);
            DataSet oDS = new DataSet();

            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TEXT,ACCNT_ID,REQUEST_PARTY_TYPE,SMSC_REFERENCE_NO FROM SERVICE_REQUEST", conn));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "SERVICE_REQUEST";

            // Insert the Data
            DataRow oOrderRow = oDS.Tables["SERVICE_REQUEST"].NewRow();
            oOrderRow["REQUEST_PARTY"] = strFrom;
            oOrderRow["RECEIPENT_PARTY"] = strTo;
            oOrderRow["REQUEST_TEXT"] = "*TXTFS*" + strMessage + "#";
            oOrderRow["ACCNT_ID"] = strAccID;
            oOrderRow["REQUEST_PARTY_TYPE"] = strMessagePurpose;
            oOrderRow["SMSC_REFERENCE_NO"] = strRefNo;
            

            oDS.Tables["SERVICE_REQUEST"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "SERVICE_REQUEST");
            
            //conn.Close();
            return "0000";
        }
        catch (Exception ex)
        {
            
            return ex.Message.ToString();
        }

    }
    protected void ddlMessagePurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMessagePurpose.SelectedIndex == 2)
        {
            ddlQuizList.Visible = true;            
        }
        else
        {
            ddlQuizList.Visible = false;            
        }
    }
    protected void ddlQuizList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connectionInfo = ConfigurationManager.AppSettings["dbConnectionString"];
        string strSql = "SELECT * FROM QUIZ_LIST WHERE QUIZ_ID='" + ddlQuizList.SelectedValue.ToString() + "'";
        OracleConnection conn = new OracleConnection(connectionInfo);
        DataSet oDS = new DataSet();
        
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        oOrdersDataAdapter.Fill(oDS, "QUIZ_LIST");

        DataRow myDataRow1 = oDS.Tables["QUIZ_LIST"].Rows.Find(ddlQuizList.SelectedValue.ToString());
        txtMessage.Text = myDataRow1["QUIZ_TEXT"].ToString();

        
    }
}
