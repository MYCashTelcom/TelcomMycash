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
/// Summary description for clsSystemAdmin
/// </summary>
public class clsSystemAd
{
    //private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    private string strConString = ConfigurationSettings.AppSettings["dbConnectionString"];
    public clsSystemAd()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet GetRootMenu()
    {
        string strSql = "";
        strSql = "SELECT * FROM CM_SYSTEM_MENU WHERE SYS_MENU_TYPE='RT' ORDER BY SYS_MENU_SERIAL";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_MENU");
            return oDS;
        }
        catch (Exception e)
        {
            strSql = e.Message.ToString();
            return null;
        }
    }

    public DataSet GetSystemUsers(string strBranch)
    {
        string strSql;
        strSql = "SELECT * FROM CM_SYSTEM_USERS WHERE CMP_BRANCH_ID='" + strBranch + "' ORDER BY SYS_USR_DNAME";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch //(Exception e)
        {
            return null;
        }
    }
    public DataSet GetChildMenu(string strParentMenu,string strGroupID)
    {
        string strSql;
        strSql = "SELECT DISTINCT "
               + "M.SYS_MENU_ID, SYS_MENU_TITLE, SYS_MENU_FILE," 
               + "SYS_MENU_PARENT, CMP_BRANCH_ID, SYS_MENU_TYPE," 
               + "SYS_MENU_SERIAL "
               + "FROM CM_SYSTEM_MENU M,CM_SYSTEM_ACCESS_POLICY P "
               + "WHERE P.SYS_MENU_ID=M.SYS_MENU_ID AND M.SYS_MENU_PARENT='" + strParentMenu + "' AND P.SYS_ACCP_VIEW = 'Y' "
                + "AND M.SYS_MENU_PARENT<> M.SYS_MENU_ID AND P.SYS_USR_GRP_ID = '" + strGroupID + "' ORDER BY SYS_MENU_SERIAL";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_MENU");
            return oDS;
        }
        catch (Exception e)
        {
            e.Message.ToString();
            return null;
        }
    }


    
    // ############## START:  Code Added by MUNIR on 26-04-2010 in order to check data
    //when a user write his/her Password on the Login Form  :START ##################
    public DataSet FrmLogin(string loginid, string password)
    {

        string strSql = "SELECT SU.*,CB.CMP_BRANCH_TYPE_ID FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND SU.SYS_USR_ID = '" + loginid + "' AND SU.SYS_USR_PASS = '" + password + "'";
        //string retint;
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
            odaFrmLogin.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch //(Exception e)
        {
            return null;
        }

    }
    public DataSet LoginWithUserName(string login_name, string password)
    {

        string strSql = "SELECT SU.*,CB.CMP_BRANCH_TYPE_ID FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND SU.SYS_USR_LOGIN_NAME = '" + login_name + "' AND SU.SYS_USR_PASS = '" + password + "'";
        //string retint;
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
            odaFrmLogin.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch //(Exception e)
        {
            return null;
        }

    }
    // ############## END:  Code Added by MUNIR on 26-04-2010 in order to check data
    //when a user write his/her Password on the Login Form  :END ##################

    public DataSet GetSysMenu()
    {
        string sqlCommand = "SELECT Distinct SYS_MENU_ID "
                           + "FROM CM_SYSTEM_MENU "; 

        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter odaSysMenu = new OracleDataAdapter(new OracleCommand(sqlCommand, conn));
            odaSysMenu.Fill(oDS, "CM_SYSTEM_MENU");
            return oDS;         
        }
        catch //(Exception e)
        {
            return null;
        }
    } 

    public void AddAllMenu(string strUsr_Grp_ID)//string strMenu_Id
    {
       // string strSql = "";
        string query;
        DataSet oDSfull = new DataSet();
        DataSet oDSremain = new DataSet();

        oDSfull = GetSysMenu();
        
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open(); // 1. Instantiate a new command with command text only

        foreach (DataRow prow in oDSfull.Tables["CM_SYSTEM_MENU"].Rows)
        {
            query = @"INSERT INTO CM_SYSTEM_ACCESS_POLICY (SYS_USR_GRP_ID, SYS_MENU_ID) "
                   + "VALUES ('" + strUsr_Grp_ID + "', '" + prow["SYS_MENU_ID"].ToString() + "')";

            OracleCommand olcmd = new OracleCommand(query); // 2. Set the Connection property
            olcmd.Connection = conn;
            olcmd.ExecuteNonQuery();// 3. Call ExecuteNonQuery to send command

        }

        conn.Close();

    }




    // ############## Start:  Function Created by MUNIR on 28-04-2010 in order Count Dataset :Start ##################
    
    public DataSet CountSysMenu(string strUsr_Grp_ID)
    {
        string sqlCommand = "SELECT * FROM CM_SYSTEM_ACCESS_POLICY WHERE SYS_USR_GRP_ID = '" + strUsr_Grp_ID + "'";

        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter odaSysMenu = new OracleDataAdapter(new OracleCommand(sqlCommand, conn));
            odaSysMenu.Fill(oDS, "CM_SYSTEM_ACCESS_POLICY");
            return oDS;
        }
        catch //(Exception e)
        {
            return null;
        }
    }

    // ############## END:  Function Created by MUNIR on 28-04-2010 in order Count Dataset :END ##################

	

    public string sChangePassword(string strUserID, string strNewPass, string strName)
    {
        string strSql = "";

        if (strUserID.Equals(""))
        {
            strSql = "SELECT SYS_USR_PASS, SYS_USR_LOGIN_NAME FROM CM_SYSTEM_USERS ";


        }
        else
        {
            strSql = "SELECT SYS_USR_PASS, SYS_USR_ID, SYS_USR_LOGIN_NAME FROM CM_SYSTEM_USERS  "
                      + "WHERE SYS_USR_ID = '" +strUserID+ "'  ";
        }

        try
        {
            DataRow oOrderRow;
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oDbAdapter.Fill(oDs, "CM_SYSTEM_USERS");


            if (strUserID.Equals(""))
            {
                oOrderRow = oDs.Tables["CM_SYSTEM_USERS"].NewRow();
            }
            else
            {
                oOrderRow = oDs.Tables["CM_SYSTEM_USERS"].Rows.Find(strUserID);
            }

            oOrderRow["SYS_USR_PASS"] = strNewPass.Replace(" ", "");
            oOrderRow["SYS_USR_LOGIN_NAME"] = strName;



            if (strUserID.Equals(""))
            {
                oDs.Tables["CM_SYSTEM_USERS"].Rows.Add(oOrderRow);

            }
            oDbAdapter.Update(oDs, "CM_SYSTEM_USERS");
            return "Password Changed";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

}
