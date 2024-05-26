using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Forms_frmSQL_Terminal : System.Web.UI.Page
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
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        string strType=txtSQL.Text.ToUpper();

        if (strType.IndexOf("SELECT")==0)
        {
            ExecuteQuerySQL();
        }
        else if (strType.IndexOf("DESC") == 0)
        {
            ExecuteQuerySQL();
        }
        else if (strType.IndexOf("INSERT") == 0)
        {
        
        }
        else
        {
            txtSQL.Text = txtSQL.Text + '\n' + "Unknown SQL command";
        }
    }
    public void ExecuteNonQuerySQL(string strCommandType)
    {
        string connectionString = ConfigurationManager.AppSettings["dbConnectionString"];
        String strSQL = txtSQL.Text;

        
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Transaction = sqlTransaction;

        try
        {
            sqlCommand.CommandText = strSQL;
            sqlCommand.ExecuteNonQuery();
            
            sqlTransaction.Commit();          
        }

        catch(Exception ex)
        {
          sqlTransaction.Rollback();
          txtSQL.Text = txtSQL.Text + '\n' + ex.Message.ToString();
        }

        finally
        {
          sqlConnection.Close();
        }
        
    }
    public void ExecuteQuerySQL()
    {
        string strCon = ConfigurationManager.AppSettings["dbConnectionString"];
        String strSQL = txtSQL.Text;

        try
        {
            sdsSQL.ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings["oracleConString"].ProviderName;
            sdsSQL.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["oracleConString"].ConnectionString;
            sdsSQL.SelectCommand = strSQL;
            //sdsSQL.Select(strSQL.ToUpper());
            sdsSQL.DataBind();
            gdvOutput.DataSource = sdsSQL;
            gdvOutput.DataBind();
        }
        catch (Exception ex)
        {
            txtSQL.Text = txtSQL.Text + '\n' + ex.Message.ToString();
        }
    }
}
