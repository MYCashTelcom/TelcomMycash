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
/// Summary description for clsReports
/// </summary>
public class clsReports
{
    //private string strConString = MiT_License.clsDBConnectionReadWrite.GetFisConnectionString();
    //private static string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    private static string strConString = ConfigurationSettings.AppSettings["dbConnectionString"];

    //private static string CONNECTION_STRING = MiT_License.clsDBConnectionReadWrite.GetFisConnectionString();
    private static string QUERY_STRING = "SELECT * FROM GL_CHART_OF_ACC WHERE ACC_TYPE='A' ORDER BY ACC_ID";
    private static string DATATABLE_NAME = "GL_CHART_OF_ACC";
    private static string DIRECTORY_FILE_PATH = "";

	public clsReports()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet GetReportData(string strSql)
    {
        try
        {
            DataSet oDS = new DataSet();
            OracleConnection conn = new OracleConnection(strConString);            
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "DTS_REPORT");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public static String ConvertToTaka(String str)
    {
        float fltInput;

        String strTemp1, strTemp2;
        //***********************
        str = str.Trim();
        fltInput = float.Parse(str);
        //#########################################
        if (str.IndexOf(".") > -1)
        {
            strTemp1 = str.Substring(0, str.IndexOf("."));
            strTemp2 = str.Substring(str.IndexOf(".") + 1);
        }
        else
        {
            strTemp1 = str;
            strTemp2 = "";
        }
        //*******************
        if (strTemp2.Length > 0)
        {
            if (strTemp2.Substring(0, 1).Equals("0"))
            {
                return ConvertToWord(float.Parse(strTemp1)).Trim() + " taka";
            }
            else
            {
                return ConvertToWord(float.Parse(strTemp1)).Trim() + " taka and " +
                       ConvertToWord(float.Parse(strTemp2)).Trim() + " paisa";
            }
        }
        else if (strTemp1.Length > 0)
        {
            return ConvertToWord(float.Parse(strTemp1)).Trim() + " taka ";
        }
        else
        {
            return "Nill";
        }
    }
    public static String ConvertToWord(float dblNumber)
    {
        String strInWord;

        int intResult;

        float dblReminder;
        int intPrecision = 0;
        float dblPrecision = 0;
        long lngNumber;
        lngNumber = (long)dblNumber;
        strInWord = " ";
        if (dblNumber > 999999999.99)
        {
            return "It is too big to convert in word by this class. Try with smaller one. ";
        }
        if (dblNumber == 0)
        {
            return "zero";
        }
        if (dblNumber < 0)
        {
            return "Negative number cannot be converted.";
        }
        intResult = (int)dblNumber / 10000000;
        dblNumber = dblNumber % 10000000;
        if (intResult > 0)
        {
            strInWord = strInWord + ConverTwoDigit((int)intResult) + " crore ";
        }

        intResult = (int)dblNumber / 100000;
        dblNumber = dblNumber % 100000;
        if (intResult > 0)
        {
            strInWord = strInWord + ConverTwoDigit((int)intResult) + " lac ";
        }
        intResult = (int)dblNumber / 1000;
        dblNumber = dblNumber % 1000;
        if (intResult > 0)
        {
            strInWord = strInWord + ConverTwoDigit((int)intResult) + " thousand ";
        }
        intResult = (int)dblNumber / 100;
        dblNumber = dblNumber % 100;
        if (intResult > 0)
        {
            strInWord = strInWord + ConverTwoDigit((int)intResult) + " hundred ";
        }
        intResult = (int)dblNumber / 1;
        dblNumber = dblNumber % 1;
        if (intResult > 0)
        {
            strInWord = strInWord + ConverTwoDigit((int)intResult);
        }
        //strInWord = strInWord.toUpperCase();
        return strInWord;

    }
    public static String ConverTwoDigit(int intTwoDigit)
    {
        String strTem;
        int intFirstDigit;
        int intSecondDigit;
        String[] strArrayFirst = new String[] { "", " one", " two", " three", " four", " five", " six", " seven", " eight", " nine" };
        String[] strArraySecond = new String[] { "", " ten", " twenty", " thirty", " forty", " fifty", " sixty", " seventy", " eighty", " ninety" }; ;
        String[] strArrayThird = new String[] { "", " eleven", " twelve", " thirteen", " forteen", " fifteen", " sixteen", " seventeen", " eighteen", " ninteen" }; ;
        intFirstDigit = intTwoDigit / 10;
        intSecondDigit = intTwoDigit % 10;
        if (intFirstDigit > 0 && intSecondDigit == 0)
        {
            return strArraySecond[intFirstDigit];
        }
        if (intFirstDigit == 1 && intSecondDigit > 0)
        {
            return strArrayThird[intSecondDigit];
        }
        if (intFirstDigit == 0 && intSecondDigit > 0)
        {
            return strArrayFirst[intSecondDigit];
        }
        if (intFirstDigit > 0 && intSecondDigit > 0)
        {
            return strArraySecond[intFirstDigit] + strArrayFirst[intSecondDigit];
        }
        return " ";

    }
    #region EXECUTE PROCEDURE(bushra)
    public string ExecuteProcedure(string strProcedure)
    {
        OracleConnection conn;
        OracleCommand OLEDBCmd = new OracleCommand();
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OLEDBCmd = conn.CreateCommand();

            OLEDBCmd.Connection = conn;//Active Connection
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandType = CommandType.StoredProcedure;
            OLEDBCmd.CommandText = strProcedure;

            OLEDBCmd.ExecuteNonQuery();
            dbTransaction.Commit();// commit transaction
            conn.Close();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();//rollback transaction
            ex.Message.ToString();
            return "";
        }
        conn.Close();
    }
    public string GetMbl(string strCAS_ACC_ID)
    {
        string strMBl = "";
        OracleConnection conn = new OracleConnection(strConString);
        try
        {
            conn.Open();

            string strGetBIN = "select * from BDMIT_ERP_101.CAS_ACCOUNT_LIST where CAS_ACC_ID='" + strCAS_ACC_ID + "' ";
            OracleCommand cmd = new OracleCommand(strGetBIN, conn);

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strMBl = dr["CAS_ACC_NO"].ToString();
            }
            conn.Close();
            return strMBl;
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }

    }
    public void updateDPS(string strTranID, string strStatus, string strDate)
    {
        string strconn = "";
        OracleTransaction dbTransaction = null;
        //##########################################################

        //Class.clsSSL_Topup objSSL = new MIT_MGW.Class.clsSSL_Topup();
        //string strConnString = objSSL.Getconn(strconn);
        //################################################################

        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataSet oDS = new DataSet();

        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT * FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION WHERE CAS_DPS_ID='" + strTranID + "'", conn, dbTransaction));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        oOrdersDataAdapter.Fill(oDS, "CAS_DPS_TRANSACTION");

        DataRow myDataRow1 = oDS.Tables["CAS_DPS_TRANSACTION"].Rows.Find(strTranID);
        myDataRow1["CAS_ISO_REQ_DESPATCH"] = strStatus;
        myDataRow1["CAS_ISO_DESPATHCH_DATE"] = strDate;
        myDataRow1["CAS_ISO_REQ_STATUS"] = "S";

        oOrdersDataAdapter.Update(oDS, "CAS_DPS_TRANSACTION");
        oOrdersDataAdapter.Dispose();
        dbTransaction.Commit();
        conn.Close();

    }
    public static void ExportToMSExcel(string fileName, string strType, string strContent, string strPageOrientation)
    {
        fileName = fileName + "_" + string.Format("{0:ddmmyy_hhmmss}", DateTime.Now);
        if (strType.ToLower().Equals("msexcel"))
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "application/msexcel";
            fileName = fileName + ".xls";
            HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            HttpContext.Current.Response.Write("<html>");
            HttpContext.Current.Response.Write("<head>");
            HttpContext.Current.Response.Write("<META HTTP-EQUIV=\" Content-Type\" CONTENT=\" text/html; charset=UTF-8\">");
            HttpContext.Current.Response.Write("<meta name=ProgId content=Word.Document>");
            HttpContext.Current.Response.Write("<meta name=Generator content=\"Microsoft Word 9\">");
            HttpContext.Current.Response.Write("<meta name=Originator content=\"Microsoft Word 9\">");
            HttpContext.Current.Response.Write("<style>");

            //HttpContext.Current.Response.Write("@page Section1 {size:595.45pt 841.7pt; margin:1.0in 1.25in 1.0in 1.25in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}");
            //HttpContext.Current.Response.Write("div.Section1 {page:Section1;}");
            //HttpContext.Current.Response.Write("@page Section2 {size:841.7pt 595.45pt;mso-page-orientation:" + strPageOrientation + ";margin:1.25in 1.0in 1.25in 1.0in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}");
            //HttpContext.Current.Response.Write("div.Section2 {page:Section2;}");

            //HttpContext.Current.Response.Write("@page Section1 {size:595.45pt 841.7pt; margin:1.0in 1.25in 1.0in 1.25in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}");
            //HttpContext.Current.Response.Write("div.Section1 {page:Section1;}");
            //HttpContext.Current.Response.Write("@page Section2 {size:841.7pt 595.45pt;mso-page-orientation:" + strPageOrientation + ";margin:1.25in 1.0in 1.25in 1.0in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}");
            //HttpContext.Current.Response.Write("@page Section2 {size:595.45pt 841.7pt;mso-page-orientation:" + strPageOrientation + ";margin:1.0in 1.0in 1.0in 1.0in;mso-paper-source:0;}");
            //HttpContext.Current.Response.Write("div.Section2 {page:Section2;}");
            HttpContext.Current.Response.Write("</style>");
            HttpContext.Current.Response.Write("</head>");
        }
        else
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ContentType = "application/pdf";
            //fileName = fileName + ".pdf";
            HttpContext.Current.Response.ContentType = "application/txt";
            fileName = fileName + ".txt";
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            //HttpContext.Current.Response.Write("<html>");
            //HttpContext.Current.Response.Write("<head>");
            //HttpContext.Current.Response.Write("</head>");
        }



        //HttpContext.Current.Response.Write("<body>");
        //HttpContext.Current.Response.Write("<div class=Section2>");

        HttpContext.Current.Response.Write(strContent);
        //HttpContext.Current.Response.Write("</div>");
        //HttpContext.Current.Response.Write("</body>");
        //HttpContext.Current.Response.Write("</html>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
    public void AddAuditLog(string strUsr_ID, string strOperationType, string strHost, string strModule, string strRemarks)//string strMenu_Id
    {
        // string strSql = "";
        string query;
        // DataSet oDSfull = new DataSet();
        // DataSet oDSremain = new DataSet();

        //  oDSfull = GetSysMenu();

        OracleConnection conn = new OracleConnection(strConString);
        conn.Open(); // 1. Instantiate a new command with command text only

        //  foreach (DataRow prow in oDSfull.Tables["CM_SYSTEM_MENU"].Rows)
        // {
        query = @"INSERT INTO CM_SYSTEM_AUDIT (SYS_USR_ID, OPERATION_TYPE,HOST,MODULE,REMARKS) "
               + "VALUES ('" + strUsr_ID + "', '" + strOperationType + "','" + strHost + "','" + strModule + "','" + strRemarks + "')";

        OracleCommand olcmd = new OracleCommand(query); // 2. Set the Connection property
        olcmd.Connection = conn;
        olcmd.ExecuteNonQuery();// 3. Call ExecuteNonQuery to send command

        //  }

        conn.Close();

    }
    public DataSet GetOfflineData(string strSql)
    {
        try
        {
            DataSet oDS = new DataSet();
            OracleConnection conn = new OracleConnection(strConString);
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CAS_DPS_TRANSACTION");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    #endregion
    //Author : Sajib
    //Date   : 07-Dec-2014.
    public DataSet ExecuteQuery(string strSQL)
    {
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn));
            oOrdersDataAdapter.Fill(oDS, "Table1");
            return oDS;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return null;
        }
    }
}
