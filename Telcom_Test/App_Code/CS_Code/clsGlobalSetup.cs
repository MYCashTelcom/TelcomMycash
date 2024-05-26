using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for clsGlobalSetup
/// </summary>
public class clsGlobalSetup
{
    //private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    private string strConString = ConfigurationSettings.AppSettings["dbConnectionString"];
	public clsGlobalSetup()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet GetChildAccount(string strParentAcc)
    {
        string strSql;
        strSql = "SELECT ACC_ID,ACC_PREFIX,ACC_NAME,ACC_LEVEL  FROM GL_CHART_OF_ACC WHERE PARENT_CODE='" + strParentAcc + "' ORDER BY ACC_ID";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "GL_CHART_OF_ACC");
            return oDS;
        }
        catch  (Exception e)
        {
             e.Message.ToString();
             return null;
        }
    }
    public string GetCompanyName()
    {
        string strSql = "SELECT * FROM CM_COMPANY";
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDs = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.Fill(oDs, "CM_COMPANY");

        DataRow dRow = oDs.Tables["CM_COMPANY"].Rows[0];
        return dRow["COMPANY_NAME"].ToString();
    }
    // Added By Bushra
    public DataSet GetSolnNameAndCpyRtInfo()
    {
        string strSql = "SELECT * FROM CM_SYSTEM_INFO";
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDs = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        
        oOrdersDataAdapter.Fill(oDs, "CM_SYSTEM_INFO");

        //DataRow dRow = oDs.Tables["CM_SYSTEM_INFO"].Rows[0];
        return oDs;

    }
    public string GetWelComeMsg()
    {
        string strSql = "SELECT * FROM CM_SYSTEM_INFO";
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDs = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.Fill(oDs, "CM_SYSTEM_INFO");

        DataRow dRow = oDs.Tables["CM_SYSTEM_INFO"].Rows[0];
        return dRow["WELCOME_MSG"].ToString();

    }
    public string GetSlnNAmeAfterLgn()
    {
        string strSql = "SELECT * FROM CM_SYSTEM_INFO";
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDs = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.Fill(oDs, "CM_SYSTEM_INFO");

        DataRow dRow = oDs.Tables["CM_SYSTEM_INFO"].Rows[0];
        return dRow["SOLN_NAME_AFTR_LOGIN"].ToString();

    }
    public string GetTitleAfterLgn()
    {
        string strSql = "SELECT * FROM CM_SYSTEM_INFO";
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDs = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.Fill(oDs, "CM_SYSTEM_INFO");

        DataRow dRow = oDs.Tables["CM_SYSTEM_INFO"].Rows[0];
        return dRow["TITLE_AFTR_LOGIN"].ToString();

    }
}
