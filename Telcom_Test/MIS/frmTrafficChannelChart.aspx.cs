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
using System.Web.UI.DataVisualization.Charting;
using System.Data.OracleClient;

public partial class frmTrafficChannelChart : System.Web.UI.Page
{
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                DateTime dt = DateTime.Now;
                if (dptFromDate.DateString != "")
                {
                    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(-15));
                    // txtFromDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(-120));
                    // txtToDate.Text = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddMinutes(5));
                    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(1));
                    LoadRequestList();
                   
                }
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strSql;
        strSql = "SELECT TO_CHAR(REQUEST_TIME,'DD-Mon-YY')REQ_DATE,"
              + " DECODE(RECEIPENT_PARTY,'16225','SMS','GP_USSD','GP_USSD','BANGLALINK_USSD','BANGLALINK_USSD','AIRTEL_USSD','AIRTEL_USSD','+8809678016225','IVR','MiT_USSD','MiT_USSD','ROBI_USSD','ROBI_USSD','WAP') REQ_CHANNEL,COUNT(*) TOTAL_REQ "
              + " FROM SERVICE_REQUEST WHERE  REQUEST_PARTY_TYPE<>'BDC' "
              + " AND TO_CHAR(TO_DATE(REQUEST_TIME,'dd/Mon/yyyy HH24:MI:SS')) BETWEEN  TO_DATE('" + dptFromDate.DateString + "','dd/Mon/yyyy HH24:MI:SS') AND TO_DATE('" + dtpToDate.DateString + "','dd/Mon/yyyy HH24:MI:SS')"
              + " GROUP BY DECODE(RECEIPENT_PARTY,'16225','SMS','GP_USSD','GP_USSD','BANGLALINK_USSD','BANGLALINK_USSD','AIRTEL_USSD','AIRTEL_USSD','+8809678016225','IVR','MiT_USSD','MiT_USSD','ROBI_USSD','ROBI_USSD','WAP'),TO_CHAR(REQUEST_TIME,'DD-Mon-YY')"
              + " ORDER BY TO_CHAR(REQUEST_TIME,'DD-Mon-YY'),REQ_CHANNEL";
        //###########################################
        clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        DataSet myDataSet = new DataSet();
        string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
        //myDataSet = objSysAdmin.ExecuteQuery(strSql);

        //// Adds rows in the DataSet
        //myDataAdapter.Fill(myDataSet, "Query");
        // Create a database connection object using the connection string    
        OracleConnection myConnection = new OracleConnection(strConString);

        // Create a database command on the connection using query    
        OracleCommand myCommand = new OracleCommand(strSql, myConnection);

        // Open the connection    
        myCommand.Connection.Open();

        // Create a database reader    
        OracleDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        Chart1.Width = 1105;
        Chart1.Height = 505;
        Chart1.Titles.Add("Traffic Channel Performance");
        Chart1.DataBindCrossTable(myReader, "REQ_CHANNEL", "REQ_DATE", "TOTAL_REQ", "Label=TOTAL_REQ");

    }
    public void LoadRequestList()
    {
        string strSql;
        strSql = "SELECT TO_CHAR(REQUEST_TIME,'DD-Mon-YY')REQ_DATE,"
              + " DECODE(RECEIPENT_PARTY,'16225','SMS','GP_USSD','GP_USSD','BANGLALINK_USSD','BANGLALINK_USSD','AIRTEL_USSD','AIRTEL_USSD','+8809678016225','IVR','MiT_USSD','MiT_USSD','ROBI_USSD','ROBI_USSD','WAP') REQ_CHANNEL,COUNT(*) TOTAL_REQ "
              + " FROM SERVICE_REQUEST WHERE  REQUEST_PARTY_TYPE<>'BDC' "
              + " AND TO_CHAR(TO_DATE(REQUEST_TIME,'dd/Mon/yyyy HH24:MI:SS')) BETWEEN  TO_DATE('" + dptFromDate.DateString + "','dd/Mon/yyyy HH24:MI:SS') AND TO_DATE('" + dtpToDate.DateString + "','dd/Mon/yyyy HH24:MI:SS')"
              + " GROUP BY DECODE(RECEIPENT_PARTY,'16225','SMS','GP_USSD','GP_USSD','BANGLALINK_USSD','BANGLALINK_USSD','AIRTEL_USSD','AIRTEL_USSD','+8809678016225','IVR','MiT_USSD','MiT_USSD','ROBI_USSD','ROBI_USSD','WAP'),TO_CHAR(REQUEST_TIME,'DD-Mon-YY')"
              + " ORDER BY TO_CHAR(REQUEST_TIME,'DD-Mon-YY'),REQ_CHANNEL";
        //###########################################
        clsSystemAdmin objSysAdmin = new clsSystemAdmin();
        DataSet myDataSet = new DataSet();
        string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
        //myDataSet = objSysAdmin.ExecuteQuery(strSql);

        //// Adds rows in the DataSet
        //myDataAdapter.Fill(myDataSet, "Query");
        // Create a database connection object using the connection string    
        OracleConnection myConnection = new OracleConnection(strConString);

        // Create a database command on the connection using query    
        OracleCommand myCommand = new OracleCommand(strSql, myConnection);

        // Open the connection    
        myCommand.Connection.Open();

        // Create a database reader    
        OracleDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        Chart1.Width = 1105;
        Chart1.Height = 505;
        Chart1.Titles.Add("Traffic Channel Performance");
        Chart1.DataBindCrossTable(myReader, "REQ_CHANNEL", "REQ_DATE", "TOTAL_REQ", "Label=TOTAL_REQ");

    }
}
