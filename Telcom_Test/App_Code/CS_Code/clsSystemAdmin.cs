//using System;
//using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.Configuration;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using System.Data.OracleClient;
//using System.Globalization;
//using System.Web.SessionState;
//using MiT_License;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Web.SessionState;
using System.Web.Configuration;
using MiT_License;
using System.Collections;

/// <summary>
/// Summary description for clsSystemAdmin
/// </summary>
public class clsSystemAdmin
{
    private OracleTransaction dbTransaction = null;
    private OracleConnection conn;
    private MiT_License.clsDBConnectionReadWrite objLicense = new MiT_License.clsDBConnectionReadWrite();
    //private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    private string strConString = ConfigurationSettings.AppSettings["dbConnectionString"];
    //ConfigurationSettings.AppSettings["dbConnectionString"];


    public clsSystemAdmin()
    {
        //
        // TODO: Add constructor logic here
        //
    }
  
    
     public string SetConnectionString()
     {
         /*
         string strError = objLicense.CheckLicense();
         if (strError.Equals(""))
         {
             string strConString = "";
             string strProvider = "DataProtectionConfigurationProvider";
             strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
            // ############## Change conenction string in connectionStrings################
             var configuration = WebConfigurationManager.OpenWebConfiguration("~");
             var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
             section.ConnectionStrings["oracleConString"].ConnectionString = strConString;
             section.SectionInformation.ProtectSection(strProvider);
            // #############  Change conenction string in AppSetting #######################
             configuration.AppSettings.Settings["dbConnectionString"].Value = strConString;
             configuration.AppSettings.SectionInformation.ProtectSection(strProvider);
             configuration.Save();
         }
         return strError;
         */
         return "";
     }
   
    public DataSet GetRootMenu()
    {
        conn = new OracleConnection(strConString);
        string strSql = "";
        strSql = "SELECT * FROM CM_SYSTEM_MENU WHERE SYS_MENU_TYPE='RT' ORDER BY SYS_MENU_SERIAL";
        try
        {
             
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
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public DataSet GetSystemUsers(string strBranch)
    {
        conn = new OracleConnection(strConString);
        string strSql;
        strSql = "SELECT * FROM CM_SYSTEM_USERS WHERE CMP_BRANCH_ID='" + strBranch + "' ORDER BY SYS_USR_DNAME";
        try
        {
            
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public DataSet GetChildMenu(string strParentMenu, string strGroupID)
    {
        conn = new OracleConnection(strConString);
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
            
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_MENU");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public void SetSeessionData(string strBranchId)
    {
        //string strSQL = "exec pkg_erp_admin_services.SET_BRANCH_ID('" + strBranchId + "')";
        string strSQL = "exec SET_BRANCH_ID('" + strBranchId + "')";
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand objComman = new OracleCommand(strSQL, conn, dbTransaction);
            objComman.CommandText = "SET_BRANCH_ID";
            objComman.CommandType = CommandType.StoredProcedure;
            objComman.Parameters.Add("inBRANCH_ID", OracleType.VarChar).Value = strBranchId;
            //OracleCommand objComman = new OracleCommand(strSQL);
            objComman.Connection = conn;
            objComman.ExecuteNonQuery();
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
            string strReturn = ex.Message.ToString();
        }
        
        finally
        {
            conn.Close();
            conn = null;
        }

    }

    public void AddAuditLog(string strUsr_ID, string strOperationType, string strHost, string strModule, string strRemarks)//string strMenu_Id
    {
        string query;
        //##########################################################
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();

        try
        {
            // 1. Instantiate a new command with command text only
            query = "INSERT INTO CM_SYSTEM_AUDIT (SYS_USR_ID, OPERATION_TYPE,HOST,MODULE,REMARKS) "
                   + "VALUES ('" + strUsr_ID + "', '" + strOperationType + "','" + strHost + "','" + strModule + "','" + strRemarks + "')";
            OracleCommand olcmd = new OracleCommand(query, conn, dbTransaction); // 2. Set the Connection property
            olcmd.Connection = conn;
            olcmd.ExecuteNonQuery();// 3. Call ExecuteNonQuery to send command
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
            string strReturn = ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        //########################################################
    }

    public string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string FileName = oInfo.Name;
        return FileName;
    }


    // ############## START:  Code Added by MUNIR on 26-04-2010 in order to check data
    //when a user write his/her Password on the Login Form  :START ##################
    public DataSet FrmLogin(string loginid, string password)
    {
        conn = new OracleConnection(strConString);//GET_CM_USER_PASS(SU.SYS_USR_ID,'GWTRM21943309')
        //string strSql = "SELECT SU.*,CB.CMP_BRANCH_TYPE_ID FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND SU.SYS_USR_ID = '" + loginid + "' AND SU.SYS_USR_PASS = '" + password + "'";
        string strSql = "SELECT SU.*,CB.CMP_BRANCH_TYPE_ID FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND SU.SYS_USR_ID = '" + loginid + "' AND GET_CM_USER_PASS(SU.SYS_USR_ID,'GWTRM21943309') = '" + password + "'";
        //string retint;
        try
        {
            
            DataSet oDS = new DataSet();
            OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
            odaFrmLogin.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }

    }
    //public DataSet LoginWithUserName(string login_name, string password)
    //{

    //    string strSql = "SELECT SU.*,CB.CMP_BRANCH_TYPE_ID FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND SU.SYS_USR_LOGIN_NAME = '" + login_name + "' AND SU.SYS_USR_PASS = '" + password + "'";
    //    //string retint;
    //    try
    //    {
    //        OracleConnection conn = new OracleConnection(strConString);
    //        DataSet oDS = new DataSet();
    //        OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
    //        odaFrmLogin.Fill(oDS, "CM_SYSTEM_USERS");
    //        return oDS;
    //    }
    //    catch (Exception e)
    //    {
    //        return null;
    //    }

    //}
    public DataSet LoginWithUserName(string login_name, string password)
    {
        conn = new OracleConnection(strConString);
       // string strSql = "SELECT SU.*,CB.CMP_BRANCH_TYPE_ID, CO.ADDRESS1,CB.CMP_BRANCH_NAME FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB, CM_COMPANY CO WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND CB.CMP_COMPANY_ID=CO.COMPANY_ID AND SU.SYS_USR_LOGIN_NAME = '" + login_name + "' AND SU.SYS_USR_PASS = '" + password + "' AND SU.STATUS='A'";

        string strSql = "SELECT SU.*,CB.CMP_BRANCH_TYPE_ID, CO.ADDRESS1,CB.CMP_BRANCH_NAME FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB, CM_COMPANY CO WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND CB.CMP_COMPANY_ID=CO.COMPANY_ID AND SU.SYS_USR_LOGIN_NAME = '" + login_name + "' AND GET_CM_USER_PASS(SU.SYS_USR_ID,'GWTRM21943309') = '" + password + "' AND SU.STATUS='A' AND SU.LOCKED_STATUS = 'UL'";
        try
        {            
            DataSet oDS = new DataSet();
            OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
            odaFrmLogin.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    // ############## END:  Code Added by MUNIR on 26-04-2010 in order to check data
    //when a user write his/her Password on the Login Form  :END ##################

    public DataSet GetSysMenu()
    {
        string sqlCommand = "SELECT Distinct SYS_MENU_ID "
                           + "FROM CM_SYSTEM_MENU ";
        conn = new OracleConnection(strConString);
        try
        {
            
            DataSet oDS = new DataSet();
            OracleDataAdapter odaSysMenu = new OracleDataAdapter(new OracleCommand(sqlCommand, conn));
            odaSysMenu.Fill(oDS, "CM_SYSTEM_MENU");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
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
                      + "WHERE SYS_USR_ID = '" + strUserID + "'  ";
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

    public string GetClientID(string login_name, string password)
    {
        string strSql = "", strClientID = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        strSql = " SELECT SU.*,CB.CMP_BRANCH_TYPE_ID,MC.POSCL_ID FROM CM_SYSTEM_USERS SU, "
              + "CM_CMP_BRANCH CB,CM_MULTIPLE_CLIENT MC WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID "
              + " AND MC.SYS_USR_ID=SU.SYS_USR_ID AND SU.SYS_USR_LOGIN_NAME = '" + login_name + "' AND GET_CM_USER_PASS(SU.SYS_USR_ID,'GWTRM21943309') = '" + password + "'";

        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            DataSet oDS = new DataSet();
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strClientID = dr["POSCL_ID"].ToString();
                }
            }
            return strClientID;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public void AddAllMenu(string strUsr_Grp_ID)//string strMenu_Id
    {
        string strSql = "";
        string query;
        DataSet oDSfull = new DataSet();
        DataSet oDSremain = new DataSet();

        oDSfull = GetSysMenu();

        conn = new OracleConnection(strConString);
        try
        {
            conn.Open(); // 1. Instantiate a new command with command text only
            dbTransaction = conn.BeginTransaction();


            foreach (DataRow prow in oDSfull.Tables["CM_SYSTEM_MENU"].Rows)
            {
                query = @"INSERT INTO CM_SYSTEM_ACCESS_POLICY (SYS_USR_GRP_ID, SYS_MENU_ID) "
                       + "VALUES ('" + strUsr_Grp_ID + "', '" + prow["SYS_MENU_ID"].ToString() + "')";

                OracleCommand olcmd = new OracleCommand(query, conn, dbTransaction); // 2. Set the Connection property
                olcmd.Connection = conn;
                olcmd.ExecuteNonQuery();// 3. Call ExecuteNonQuery to send command

            }
            dbTransaction.Commit();
        }
        catch (Exception)
        {
            
            
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }




    // ############## Start:  Function Created by MUNIR on 28-04-2010 in order Count Dataset :Start ##################

    public DataSet CountSysMenu(string strUsr_Grp_ID)
    {
        string sqlCommand = "SELECT * FROM CM_SYSTEM_ACCESS_POLICY WHERE SYS_USR_GRP_ID = '" + strUsr_Grp_ID + "'";
        conn = new OracleConnection(strConString);
        try
        {
           
            DataSet oDS = new DataSet();
            OracleDataAdapter odaSysMenu = new OracleDataAdapter(new OracleCommand(sqlCommand, conn));
            odaSysMenu.Fill(oDS, "CM_SYSTEM_ACCESS_POLICY");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    // ############## END:  Function Created by MUNIR on 28-04-2010 in order Count Dataset :END ##################


   
    #region Login Check with IP & Machine Name

    public bool ChkAuthorizeIP(string IP)
    {
        string strSql;

        strSql = "SELECT USER_IP_ID FROM CM_SYSTEM_USER_IP WHERE USER_IP='" + IP + "'";
        conn = new OracleConnection(strConString);
        try
        {
             
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "USER_IP");

            DataTable tbl_AD = oDS.Tables["USER_IP"];

            // already exists
            if (tbl_AD.Rows.Count > 0) { return true; }
            else { return false; }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public bool ChkAuthorizeIP(string IP, string machineName)
    {
        string strSql;

        strSql = "SELECT USER_IP_ID FROM CM_SYSTEM_USER_IP WHERE USER_IP='" + IP + "' AND MACHINE_NAME='" + machineName + "'";
        conn = new OracleConnection(strConString);
        try
        {
            
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "USER_IP");

            DataTable tbl_AD = oDS.Tables["USER_IP"];

            // already exists
            if (tbl_AD.Rows.Count > 0) { return true; }
            else { return false; }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public bool ChkAnyIPExists()
    {
        string strSql;

        strSql = "select count(USER_IP_ID) TOTAL FROM CM_SYSTEM_USER_IP";
        conn = new OracleConnection(strConString);
        try
        {
             
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "TOTAL_IP");

            DataTable tbl_AD = oDS.Tables["TOTAL_IP"];
            double totValue = Convert.ToDouble(tbl_AD.Rows[0]["TOTAL"].ToString());

            // already exists
            if (totValue > 0) { return true; }
            else { return false; }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    //asad
    public bool ChkAllaccessIPExists()
    {
        string strSql;
        bool exist = false;
        strSql = "select count(USER_IP_ID) TOTAL FROM CM_SYSTEM_USER_IP where USER_IP='*' and MACHINE_NAME='*' ";
        conn = new OracleConnection(strConString);
        try
        {

            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "TOTAL_IP");

            DataTable tbl_AD = oDS.Tables["TOTAL_IP"];
            double totValue = Convert.ToDouble(tbl_AD.Rows[0]["TOTAL"].ToString());
            // already exists
            if (totValue > 0)
            { 
                exist = true; 
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return exist;
    }

     /* 
     * Developer: Md.Asaduzzaman 
     * Dated:02-May-2013 
     */
    public string InsertUserIP(string USER_IP, string MACHINE_NAME)
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT USER_IP, MACHINE_NAME FROM CM_SYSTEM_USER_IP";
            DataRow oOrderRow;
            
            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.FillSchema(oDs, SchemaType.Source, "CM_SYSTEM_USER_IP");
            oOrderRow = oDs.Tables["CM_SYSTEM_USER_IP"].NewRow();

            oOrderRow["USER_IP"] = USER_IP;
            oOrderRow["MACHINE_NAME"] = MACHINE_NAME;

            oDs.Tables["CM_SYSTEM_USER_IP"].Rows.Add(oOrderRow);
            oDbAdapter.Update(oDs, "CM_SYSTEM_USER_IP");
           
            dbTransaction.Commit();
            //conn.Close();
            return "Success";
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

    #endregion

    public string InsertMultipleProduct(string clientId, string ProductId, string ProductDetails, string BRANCH_ID, string UserId)
    {
        conn = new OracleConnection(strConString);
        string strSql;
        strSql = "SELECT POSPL_ID, CI_CLINT_ID, PRO_DETAILS, CMP_BRANCH_ID, SYS_USR_ID FROM CM_MULTIPLE_PRODUCT ";
        try
        {
            DataRow oOrderRow;
            
            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable dtSchedule = oDS.Tables["Table"];
            dtSchedule.TableName = "CM_MULTIPLE_PRODUCT";

            oOrderRow = oDS.Tables["CM_MULTIPLE_PRODUCT"].NewRow();

            oOrderRow["POSPL_ID"] = ProductId;
            oOrderRow["CI_CLINT_ID"] = clientId;
            oOrderRow["PRO_DETAILS"] = ProductDetails;
            oOrderRow["CMP_BRANCH_ID"] = BRANCH_ID;
            oOrderRow["SYS_USR_ID"] = UserId;

            oDS.Tables["CM_MULTIPLE_PRODUCT"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "CM_MULTIPLE_PRODUCT");
            dbTransaction.Commit();
            //conn.Close();
            return "Saved Successfully.";
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

    public string InsertMultipleModule(string BRANCH_ID, string ProductId, string moduleId, string Client, string ModuleDetails)
    {
        conn = new OracleConnection(strConString);
        string strSql;
        strSql = "SELECT POSPL_ID, SUBM_ID, MOD_DETAILS, CMP_BRANCH_ID, CI_CLINT_ID FROM CM_MULTIPLE_MODULE ";
        try
        {
            DataRow oOrderRow;
            
            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable dtSchedule = oDS.Tables["Table"];
            dtSchedule.TableName = "CM_MULTIPLE_MODULE";

            oOrderRow = oDS.Tables["CM_MULTIPLE_MODULE"].NewRow();

            oOrderRow["CMP_BRANCH_ID"] = BRANCH_ID;
            oOrderRow["POSPL_ID"] = ProductId;
            oOrderRow["SUBM_ID"] = moduleId;
            oOrderRow["CI_CLINT_ID"] = Client;
            oOrderRow["MOD_DETAILS"] = ModuleDetails;        
            

            oDS.Tables["CM_MULTIPLE_MODULE"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "CM_MULTIPLE_MODULE");
            dbTransaction.Commit();
            //conn.Close();
            return "Saved Successfully.";
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
 
    //public DataSet LoginGroupUser(string userID)
    //{

    //    string strSql = " SELECT SG.SYS_USR_GRP_TITLE,SG.SYS_USR_GRP_PARENT,SG.SYS_USR_GRP_TYPE,SG.CMP_BRANCH_ID,SG.POSCL_ID,SG.SYS_USR_GRP_ID, "
    //                     + " SU.SYS_USR_ID,SU.SYS_USR_DNAME,SU.SYS_USR_LOGIN_NAME,SU.SYS_USR_PASS,SU.SYS_USR_EMAIL,CB.CMP_BRANCH_TYPE_ID "
    //                     + " FROM CM_SYSTEM_USER_GROUP SG,CM_SYSTEM_USERS SU,CM_CMP_BRANCH CB "
    //                     + " WHERE SG.SYS_USR_GRP_ID=SU.SYS_USR_GRP_ID AND CB.CMP_BRANCH_ID=SG.CMP_BRANCH_ID "
    //                     + " AND CB.CMP_BRANCH_ID=SU.CMP_BRANCH_ID AND SU.SYS_USR_ID='" + userID + "' ";
    //    try
    //    {
    //        OracleConnection conn = new OracleConnection(strConString);
    //        DataSet oDS = new DataSet();
    //        OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
    //        odaFrmLogin.Fill(oDS, "CM_SYSTEM_USERS_GROUP");
    //        return oDS;
    //    }
    //    catch (Exception e)
    //    {
    //        return null;
    //    }
    //}

    public DataSet Menu(string bId)
    {
        string strQuery = "SELECT distinct  S.SYS_MENU_ID,S.SYS_MENU_TITLE,S.SYS_MENU_PARENT, S.SYS_MENU_SERIAL, "
                         + "getParentMenu(S.SYS_MENU_PARENT,S.CMP_BRANCH_ID) ParentMenu, "
                         + "S.CMP_BRANCH_ID,S.SYS_MENU_FILE,S.SYS_MENU_TYPE,S.SYS_MENU_SERIAL "
                         + "FROM CM_SYSTEM_MENU S,CM_CMP_BRANCH C "
                         + "WHERE  S.CMP_BRANCH_ID = C.CMP_BRANCH_ID AND C.CMP_BRANCH_ID in ('" + bId + "') "
                         + "ORDER BY S.SYS_MENU_SERIAL ";


        conn = new OracleConnection(strConString);
        try
        {

            DataSet oDS = new DataSet();
            OracleDataAdapter odaData = new OracleDataAdapter(new OracleCommand(strQuery, conn));
            odaData.Fill(oDS, "CM_SYSTEM_MENU");
            return oDS;
        }
        catch (Exception ex)
        {
            return null;
        }

        finally
        {
            conn.Close();
            conn = null;
        }
    }
    public string InsertSysUser(string BRANCH_ID, string UserName, string LogName, string Pass, string Email, string Active, string Group, DateTime ExDate)
    {
        conn = new OracleConnection(strConString);
        string strSql;
        strSql = "SELECT SYS_USR_DNAME, SYS_USR_LOGIN_NAME, SYS_USR_PASS, USER_ACTIVE, SYS_USR_EMAIL, SYS_USR_GRP_ID, CMP_BRANCH_ID, PASSWORD_EXPIRED_DATE FROM CM_SYSTEM_USERS ";
        try
        {
            DataRow oOrderRow;

            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable dtSchedule = oDS.Tables["Table"];
            dtSchedule.TableName = "CM_SYSTEM_USERS";

            oOrderRow = oDS.Tables["CM_SYSTEM_USERS"].NewRow();

            oOrderRow["CMP_BRANCH_ID"] = BRANCH_ID;
            oOrderRow["SYS_USR_DNAME"] = UserName;
            oOrderRow["SYS_USR_LOGIN_NAME"] = LogName;
            oOrderRow["SYS_USR_PASS"] = Pass;
            oOrderRow["SYS_USR_EMAIL"] = Email;
            oOrderRow["USER_ACTIVE"] = Active;
            oOrderRow["SYS_USR_GRP_ID"] = Group;
            oOrderRow["PASSWORD_EXPIRED_DATE"] = ExDate;


            oDS.Tables["CM_SYSTEM_USERS"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "CM_SYSTEM_USERS");
            dbTransaction.Commit();
            //conn.Close();
            return "Saved Successfully.";
        }
        catch (Exception ex)
        {
            return "Login Name Already Exists";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }


    #region InsertCheckingForMultipleClient_Product_Module
    /*
    Developer: Md.Asaduzzaman
    Dated: 26-Jan-2014
    */
    public bool ChkMultiExists(params string[] strParam)
    {
        string flag="";
        bool Exist = false;
        DataSet oDS = new DataSet();
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "";
            flag = strParam[0].ToString();
            if (flag == "client")
            {
               string brClient =  strParam[1].ToString();
               string clClient =  strParam[2].ToString();
               string usrClient = strParam[3].ToString();
               strSql = "select CM_MULTIPLE_CLIENT_ID from CM_MULTIPLE_CLIENT where CMP_BRANCH_ID='" + brClient + "' and POSCL_ID='" + clClient + "' and  SYS_USR_ID='" + usrClient + "' ";
            }
            else if(flag == "product")
            {
                string BRproduct = strParam[1].ToString();
                string PRproduct = strParam[2].ToString();
                string USRproduct = strParam[3].ToString();
                string CLproduct = strParam[4].ToString();
                strSql = "select PRO_ID from CM_MULTIPLE_PRODUCT where CMP_BRANCH_ID='" + BRproduct + "' and POSPL_ID='" + PRproduct + "' and  SYS_USR_ID='" + USRproduct + "' and CI_CLINT_ID='" + CLproduct + "' ";
            }
            else if (flag == "module")
            {
                string BRmodule = strParam[1].ToString();
                string PRmodule = strParam[2].ToString();
                string ModRmodule = strParam[3].ToString();
                string CLmodule = strParam[4].ToString();
                strSql = "select MOD_ID  from CM_MULTIPLE_MODULE  where CMP_BRANCH_ID='" + BRmodule + "' and POSPL_ID='" + PRmodule + "' and  SUBM_ID='" + ModRmodule + "' and CI_CLINT_ID='" + CLmodule + "' ";
            }
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "Multiple");
            if (oDS.Tables["Multiple"].Rows.Count > 0)
            {
                Exist = true;
            }

            return Exist;
        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return Exist;
    }

    #endregion


    //###############################################  LogIN-Security   ####################################################
         #region LogIN-Security
    /* 
     * Developer: Md.Asaduzzaman 
     * Dated:27-Jan-2014 
     */
    public string InsertPasswordPolicy(params string[] strParm)
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT PP_PASS_LENGTH, PP_PASS_SAME_NOT_NEXT_TIMES,PP_PASS_LOGON_MAX_TIMES,PP_PASS_EXPIRE_AFTER_DAYS,PP_PASS_DESCRIPTION FROM CM_SYSTEM_PASSWORD_POLICY";
            DataRow oOrderRow;

            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.FillSchema(oDs, SchemaType.Source, "CM_SYSTEM_PASSWORD_POLICY");
            oOrderRow = oDs.Tables["CM_SYSTEM_PASSWORD_POLICY"].NewRow();

            oOrderRow["PP_PASS_LENGTH"] = Convert.ToInt32(strParm[0].ToString());
            oOrderRow["PP_PASS_SAME_NOT_NEXT_TIMES"] = Convert.ToInt32(strParm[1].ToString());
            oOrderRow["PP_PASS_LOGON_MAX_TIMES"] = Convert.ToInt32(strParm[2].ToString());
            oOrderRow["PP_PASS_EXPIRE_AFTER_DAYS"] = Convert.ToInt32(strParm[3].ToString());
            oOrderRow["PP_PASS_DESCRIPTION"] = strParm[4].ToString();


            oDs.Tables["CM_SYSTEM_PASSWORD_POLICY"].Rows.Add(oOrderRow);
            oDbAdapter.Update(oDs, "CM_SYSTEM_PASSWORD_POLICY");

            dbTransaction.Commit();
            return "Saved Successfully...";
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
    public DataSet GetPasswordPolicyInfo()
    {
        conn = new OracleConnection(strConString);
        string strSql;
        strSql = "Select * From CM_SYSTEM_PASSWORD_POLICY  ";
        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_PASSWORD_POLICY");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    public void UpdatePasswordExpiredDt(string ExpiredDate)
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT SYS_USR_ID,PASSWORD_EXPIRED_DATE FROM  CM_SYSTEM_USERS where SYS_USR_ID=(select max(SYS_USR_ID) from CM_SYSTEM_USERS) ";
            DataRow oOrderRow;
            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            DataSet dsFill =new DataSet();
            oDbAdapter.Fill(dsFill, "expired");
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oDbAdapter.Fill(oDs, "CM_SYSTEM_USERS");
            if (dsFill.Tables["expired"].Rows.Count > 0)
            {
              string useId = dsFill.Tables["expired"].Rows[0]["SYS_USR_ID"].ToString();
              oOrderRow = oDs.Tables["CM_SYSTEM_USERS"].Rows.Find(useId);
              oOrderRow["PASSWORD_EXPIRED_DATE"] = DateTime.Parse(ExpiredDate);
            }
            oDbAdapter.Update(oDs, "CM_SYSTEM_USERS");
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {

           
        }
        finally
        {
            conn.Close();
            conn = null;
        }

    }
    //OverLoaded
    public void UpdatePasswordExpiredDt(string ExpiredDate,string usrId)
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT SYS_USR_ID,PASSWORD_EXPIRED_DATE FROM  CM_SYSTEM_USERS where  SYS_USR_ID='" + usrId + "' ";
            DataRow oOrderRow;
            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            DataSet dsFill = new DataSet();
            oDbAdapter.Fill(dsFill, "expired");
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oDbAdapter.Fill(oDs, "CM_SYSTEM_USERS");
            if (dsFill.Tables["expired"].Rows.Count > 0)
            {
                string useId = dsFill.Tables["expired"].Rows[0]["SYS_USR_ID"].ToString();
                oOrderRow = oDs.Tables["CM_SYSTEM_USERS"].Rows.Find(useId);
                oOrderRow["PASSWORD_EXPIRED_DATE"] = DateTime.Parse(ExpiredDate);
            }
            oDbAdapter.Update(oDs, "CM_SYSTEM_USERS");
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {


        }
        finally
        {
            conn.Close();
            conn = null;
        }

    }
    //28-Jan-2014  ASAD
    public DataSet GetFailureClick(string LoginName)
    {
        conn = new OracleConnection(strConString);
        string strSql;
        strSql = "SELECT SYS_USR_ID,NVL(CLICK_FAILURE,0) CLICK_FAILURE FROM CM_SYSTEM_USERS WHERE SYS_USR_LOGIN_NAME='" + LoginName + "' ";
        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    public void UpdateFailureClick(string SysUsrID, int Click)
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT CLICK_FAILURE,SYS_USR_ID FROM CM_SYSTEM_USERS WHERE SYS_USR_ID='" + SysUsrID + "' ";
            DataRow oOrderRow;
            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oDbAdapter.Fill(oDs, "CM_SYSTEM_USERS");
            oOrderRow = oDs.Tables["CM_SYSTEM_USERS"].Rows.Find(SysUsrID);
            oOrderRow["CLICK_FAILURE"] = Click.ToString();
            oDbAdapter.Update(oDs, "CM_SYSTEM_USERS");
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            conn.Close();
            conn = null;
        }

    }
    public void UpdateLocked(string SysUsrID,string locking)
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT LOCKED_STATUS,SYS_USR_ID FROM CM_SYSTEM_USERS WHERE SYS_USR_ID='" + SysUsrID + "' ";
            DataRow oOrderRow;
            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oDbAdapter.Fill(oDs, "CM_SYSTEM_USERS");
            oOrderRow = oDs.Tables["CM_SYSTEM_USERS"].Rows.Find(SysUsrID);
            oOrderRow["LOCKED_STATUS"] = locking;
            oDbAdapter.Update(oDs, "CM_SYSTEM_USERS");
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            conn.Close();
            conn = null;
        }

    }
    public DataSet GetSysUsersInfo(string SysUsrID)  //GetLockInfo
    {
        conn = new OracleConnection(strConString);
        string strSql;
        strSql = "SELECT * FROM CM_SYSTEM_USERS WHERE SYS_USR_ID='" + SysUsrID + "' ";
        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    public DataSet GetSysUsrInfo(string LogInName)  //GetLockInfo
    {
        conn = new OracleConnection(strConString);
        string strSql;
        strSql = "SELECT * FROM CM_SYSTEM_USERS  WHERE SYS_USR_LOGIN_NAME='" + LogInName + "' ";
        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    //asad   30-JAN-2014
    public string GetSessionID(string logiName)  
    {

       
            if (logiName.Length < 4)
            {
                return "Invalid Length of LoginName";
            }
            else
            {
                string ReturnSessionId = string.Empty;
                logiName = logiName.Substring(0, 4);
                conn = new OracleConnection(strConString);
                string strSql;
                strSql = "SELECT GET_SESSION_ID('" + logiName + "') SESSIONID FROM DUAL ";
                try
                {
                    DataSet oDS = new DataSet();
                    OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
                    oOrdersDataAdapter.Fill(oDS, "getSession");
                    ReturnSessionId = oDS.Tables[0].Rows[0]["SESSIONID"].ToString();
                }
                catch (Exception e)
                {

                }
                finally
                {
                    conn.Close();
                    conn = null;
                }
                return ReturnSessionId;
            }
    }
    //29-Jan-2014
    //Modified by: Md. Asaduzzaman, Dated:29-Jan-2014
    public string sChangePassword(string strUserID, string strNewPass, string strPrevPass, string strName)
    {
        string strSql = "";

        if (strUserID.Equals(""))
        {
            strSql = "SELECT SYS_USR_PASS,SYS_USR_PASS_PREVIOUS, SYS_USR_LOGIN_NAME FROM CM_SYSTEM_USERS ";
        }
        else
        {
            strSql = "SELECT SYS_USR_PASS, SYS_USR_ID,SYS_USR_PASS_PREVIOUS, SYS_USR_LOGIN_NAME FROM CM_SYSTEM_USERS  "
                      + "WHERE SYS_USR_ID = '" + strUserID + "'  ";
        }
        conn = new OracleConnection(strConString);
        try
        {
            DataRow oOrderRow;

            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);

            if (strUserID.Equals(""))
            {
                oDbAdapter.FillSchema(oDs, SchemaType.Source, "CM_SYSTEM_USERS");
                oOrderRow = oDs.Tables["CM_SYSTEM_USERS"].NewRow();
            }
            else
            {
                oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                oDbAdapter.Fill(oDs, "CM_SYSTEM_USERS");
                oOrderRow = oDs.Tables["CM_SYSTEM_USERS"].Rows.Find(strUserID);
            }

            oOrderRow["SYS_USR_PASS"] = strNewPass.Replace(" ", "");
            oOrderRow["SYS_USR_LOGIN_NAME"] = strName;
            oOrderRow["SYS_USR_PASS_PREVIOUS"] = strPrevPass;



            if (strUserID.Equals(""))
            {
                oDs.Tables["CM_SYSTEM_USERS"].Rows.Add(oOrderRow);

            }
            oDbAdapter.Update(oDs, "CM_SYSTEM_USERS");
            dbTransaction.Commit();
            conn.Close();
            return "Password Changed Successfully...";

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
    //overloaded     Md. Asaduzzaman Dated:30-Jan-2014     // Decrypt the Encrypted Password.
    public DataSet LoginWithUserName(string login_name, string password,string SessionId)
    {
        conn = new OracleConnection(strConString);
        string strSql = " SELECT SU.*,CB.CMP_BRANCH_TYPE_ID, CO.ADDRESS1,CB.CMP_BRANCH_NAME FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB, CM_COMPANY CO  "
                          +"WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND CB.CMP_COMPANY_ID=CO.COMPANY_ID  "
                          +"AND SU.SYS_USR_LOGIN_NAME = '" + login_name + "' AND GET_CM_USER_PASS(SU.SYS_USR_ID,'" + SessionId + "') = '" + password + "' ";

        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
            odaFrmLogin.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    public DataSet DecryptPassword(string SessionId, string logInName, string UsrId)  
    {
        conn = new OracleConnection(strConString);
        string strSql;
        //strSql = "SELECT GET_CM_USER_PASS_PREVIOUS(SU.SYS_USR_ID,'" + SessionId + "') PREV_PASS,GET_CM_USER_PASS(SU.SYS_USR_ID,'" + SessionId + "')EXIS_PASS "
        //        +"FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB, CM_COMPANY CO "
        //        +"WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND CB.CMP_COMPANY_ID=CO.COMPANY_ID "
        //        +"AND SU.SYS_USR_LOGIN_NAME = '" + logInName + "' AND  SYS_USR_ID='" + UsrId + "'  ";

        strSql = "SELECT SYS_USR_PASS_PREVIOUS PREV_PASS,GET_CM_USER_PASS(SU.SYS_USR_ID,'GWTRM21943309') EXIS_PASS "
                + "FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB, CM_COMPANY CO "
                + "WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND CB.CMP_COMPANY_ID=CO.COMPANY_ID "
                + "AND SU.SYS_USR_LOGIN_NAME = '" + logInName + "' AND  SYS_USR_ID='" + UsrId + "'  ";

        //strSql = "SELECT SYS_USR_PASS_PREVIOUS PREV_PASS, SYS_USR_PASS EXIS_PASS "
        //        + "FROM CM_SYSTEM_USERS SU, CM_CMP_BRANCH CB, CM_COMPANY CO "
        //        + "WHERE SU.CMP_BRANCH_ID=CB.CMP_BRANCH_ID AND CB.CMP_COMPANY_ID=CO.COMPANY_ID "
        //        + "AND SU.SYS_USR_LOGIN_NAME = '" + logInName + "' AND  SYS_USR_ID='" + UsrId + "'  ";

        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    public void DeleteSessionID(string strSessID)
    {
        string strSQL = "DELETE SYS_TEMINAL_SESSION WHERE STS_SESSION_KEY='" + strSessID + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        try
        {
            conn.Open();
            OracleTransaction dbTransaction = null;
            dbTransaction = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand(strSQL);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    //saydur
    //24.3.2014
    //to get user id
    public String getUserId(String username)
    {
        String userid = null;
        conn = new OracleConnection(strConString);
        string strSql = "Select * from CM_SYSTEM_USERS where SYS_USR_LOGIN_NAME='" + username + "'";

        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
            odaFrmLogin.Fill(oDS, "CM_SYSTEM_USERS");
            userid = oDS.Tables[0].Rows[0]["SYS_USR_ID"].ToString();
        }
        catch (Exception e)
        {
            userid = null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }

        return userid;
    }


    #endregion
    //###############################################  LogIN-Security   ####################################################

    public string InsertWorkflowSetup(string branch, string mainType, string Item, string SettelType, string Deprtment, string SorPer,
                                  string SorDegistion, string DesEmpId, string DesDesEmpId, string Description)
    {
        string strSql;
        strSql = "SELECT CMP_BRANCH_ID, DESTINATION_EMP_ID, MAIN_TYPE_ID, WS_SETTLED_TYPE, SOURCE_EMP_ID, DPT_ID, WS_DESCRIPTION, ITEM_SET_CODE, SOURCE_DSG_ID, DESTINATION_DSG_ID FROM CM_WORKFLOW_SETUP ";

        conn = new OracleConnection(strConString);
        try
        {
            DataRow oOrderRow;
            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
            //######################################################
            DataTable dtSchedule = oDS.Tables["Table"];
            dtSchedule.TableName = "CM_WORKFLOW_SETUP";
            //######################################################
            oOrderRow = oDS.Tables["CM_WORKFLOW_SETUP"].NewRow();
            oOrderRow["CMP_BRANCH_ID"] = branch;
            oOrderRow["MAIN_TYPE_ID"] = mainType;
            oOrderRow["ITEM_SET_CODE"] = Item;
            oOrderRow["WS_SETTLED_TYPE"] = SettelType;
            oOrderRow["DPT_ID"] = Deprtment;
            oOrderRow["SOURCE_EMP_ID"] = SorPer;
            oOrderRow["SOURCE_DSG_ID"] = SorDegistion;
            oOrderRow["DESTINATION_EMP_ID"] = DesEmpId;
            oOrderRow["DESTINATION_DSG_ID"] = DesDesEmpId;
            oOrderRow["WS_DESCRIPTION"] = Description;
            //#########################################################
            oDS.Tables["CM_WORKFLOW_SETUP"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "CM_WORKFLOW_SETUP");
            dbTransaction.Commit();
            return "Saved Successfully.";
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

    public string InsertCountry(string Country_Name, string Country_Code, string Description) /*** added by : Niaz Morshed, 1-jun-2014, Decription : Insert Country ***/
                                
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT CM_COUNTRY_CODE, CM_COUNTRY_NAME, CM_COUNTRY_DESCRIPTION FROM CM_COUNTRY ";
            DataRow oOrderRow;
            //DataRow oOrderRow1;

            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            //oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //oDbAdapter.Fill(oDs, "HCR_TREATMENT");
            oDbAdapter.FillSchema(oDs, SchemaType.Source, "CM_COUNTRY");
            oOrderRow = oDs.Tables["CM_COUNTRY"].NewRow();

            oOrderRow["CM_COUNTRY_NAME"] = Country_Name;
            oOrderRow["CM_COUNTRY_CODE"] = Country_Code;
            oOrderRow["CM_COUNTRY_DESCRIPTION"] = Description;

            oDs.Tables["CM_COUNTRY"].Rows.Add(oOrderRow);
            oDbAdapter.Update(oDs, "CM_COUNTRY");
            dbTransaction.Commit();
            // conn.Close();
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return "Success";
    }

    public string InsertCompanyReg(string Company_Name, string Abbre_Name, string Address_1, string Addrese_2,  /*** added by : Niaz Morshed, 1-jun-2014, Decription : Insert Country ***/
                                   string country, string city, string mobile, string phone, string fax, string tin,
                                   string reg_no, string type, string parent, string soft_pkg, string user_name,
                                   string password, string con_password, string email, string describ) 
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT REG_COMPANY_NAME, REG_ABBREVIATED_NAME, REG_ADDRESS01, REG_ADDRESS02, CM_COUNTRY_ID, "
                          + "CM_CITY_ID, REG_MOBILE_NO, REG_PHONE_NO, REG_FAX_NO, REG_TIN_NO, REG_REGISTATION_NO, "
                          + "REG_COM_TYPE, CTYPE_ID, SOFT_PKG_ID, REG_LOGING_NAME, REG_PASSWORD, REG_CONFIRM_PASSWORD, "
                          + "REG_EMAIL, SYS_DATE, DESCRIPTION, COMPANY_ID FROM CM_COMPANY_REGISTRATION ";
            DataRow oOrderRow;
            //DataRow oOrderRow1;

            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            //oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //oDbAdapter.Fill(oDs, "CM_COMPANY_REGISTRATION");
            oDbAdapter.FillSchema(oDs, SchemaType.Source, "CM_COMPANY_REGISTRATION");
            oOrderRow = oDs.Tables["CM_COMPANY_REGISTRATION"].NewRow();

            oOrderRow["REG_COMPANY_NAME"] = Company_Name;
            oOrderRow["REG_ABBREVIATED_NAME"] = Abbre_Name;
            oOrderRow["REG_ADDRESS01"] = Address_1;
            oOrderRow["REG_ADDRESS02"] = Addrese_2;
            oOrderRow["CM_COUNTRY_ID"] = country;
            oOrderRow["CM_CITY_ID"] = city;
            oOrderRow["REG_MOBILE_NO"] = mobile;
            oOrderRow["REG_PHONE_NO"] = phone;
            oOrderRow["REG_FAX_NO"] = fax;
            oOrderRow["REG_TIN_NO"] = tin;
            oOrderRow["REG_REGISTATION_NO"] = reg_no;
            oOrderRow["CTYPE_ID"] = type;
            oOrderRow["COMPANY_ID"] = parent;
            oOrderRow["SOFT_PKG_ID"] = soft_pkg;
            oOrderRow["REG_LOGING_NAME"] = user_name;
            oOrderRow["REG_PASSWORD"] = password;
            oOrderRow["REG_CONFIRM_PASSWORD"] = con_password;
            oOrderRow["REG_EMAIL"] = email;
            oOrderRow["DESCRIPTION"] = describ;

            oDs.Tables["CM_COMPANY_REGISTRATION"].Rows.Add(oOrderRow);
            oDbAdapter.Update(oDs, "CM_COMPANY_REGISTRATION");
            dbTransaction.Commit();
            // conn.Close();
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return "Success";
    }

    public DataSet UserGroup(string groupId)
    {        
        string strQuery = "SELECT SYS_USR_GRP_ID FROM CM_SOFTWARE_PACKAGE WHERE SOFT_PKG_ID='" + groupId + "'";

        conn = new OracleConnection(strConString);
        try
        {

            DataSet oDS = new DataSet();
            OracleDataAdapter odaData = new OracleDataAdapter(new OracleCommand(strQuery, conn));
            odaData.Fill(oDS, "USER_G");
            return oDS;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public DataSet CountryImages()
    {
        string strQuery = "SELECT CM_COUNTRY_ID, CM_COUNTRY_NAME, COUNTRY_IAMGE, CM_COUNTRY_CODE FROM CM_COUNTRY ";

        conn = new OracleConnection(strConString);
        try
        {

            DataSet oDS = new DataSet();
            OracleDataAdapter odaData = new OracleDataAdapter(new OracleCommand(strQuery, conn));
            odaData.Fill(oDS, "CM_COUNTRY");
            return oDS;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    public void permission(ArrayList notpermit,Page currenrpage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("var permitar=new Array();");
        foreach (string val in notpermit)
        {
            sb.Append("permitar.push('" + val + "');");
        }
        sb.Append("</script>");
   //     String jscode = "<script>"
   //     +"function mainfunc() {"
   //      +   "this.hide = function hidefunc(param) {"
   //       +     " var buttons = document.getElementsByTagName('input');"
   //        +     "for (var i = 0; i < buttons.length; i++) {"
   //        +         "var button = buttons[i];"
   //         +        "if (button.getAttribute('value') == param) {"
   //         +        "    button.style.visibility = 'hidden';"
   //         +        "}"
   //          +  " }"
   //        + "}"
   //   + " }"
   //   +  "function toload() {"
   //    +   "  objmainfinc = new mainfunc();"
   //    +   "  if (permitar) {"
   //     +      "  for (var i=0; i < permitar.length; i++) {"
   //      +     "      objmainfinc.hide(permitar[i]);"
   //       +    "  }"
   //      +  " }"
            
   //   + " }"
   //+ "</script>";
        currenrpage.ClientScript.RegisterStartupScript(this.GetType(), "permitarscript", sb.ToString());
        
    }
    public ArrayList accessPermission(string userId,string pagename)
    {
        ArrayList permissions = new ArrayList();
        //string qry = "SELECT UNIQUE SP.SYS_ACCP_ID, SP.SYS_USR_GRP_ID, SP.SYS_MENU_ID, SP.SYS_ACCP_VIEW, SP.SYS_ACCP_ADD, SP.SYS_ACCP_DELETE, SP.SYS_ACCP_EDIT, SP.SYS_ACCP_PRINT FROM CM_SYSTEM_ACCESS_POLICY SP,CM_SYSTEM_MENU SM WHERE SP.SYS_USR_GRP_ID='"+groupid+"'";
        string qry = "SELECT SU.SYS_USR_DNAME, UG.SYS_USR_GRP_TITLE, SM.SYS_MENU_FILE, AP.SYS_ACCP_VIEW, AP.SYS_ACCP_ADD, AP.SYS_ACCP_DELETE, AP.SYS_ACCP_EDIT FROM CM_SYSTEM_USERS SU, CM_SYSTEM_USER_GROUP UG, CM_SYSTEM_ACCESS_POLICY AP, CM_SYSTEM_MENU SM WHERE SU.SYS_USR_GRP_ID=UG.SYS_USR_GRP_ID AND AP.SYS_USR_GRP_ID=UG.SYS_USR_GRP_ID AND SM.SYS_MENU_ID=AP.SYS_MENU_ID AND SU.SYS_USR_ID='" + userId + "' AND SM.SYS_MENU_FILE like'%" + pagename + "'";
        DataBaseClassOracle db = new DataBaseClassOracle();
        DataTable dt = db.ConnectDataBaseReturnDT(qry);
        permissions.Add(dt.Rows[0]["SYS_ACCP_ADD"].ToString() == "N" ? "add" : "");
        permissions.Add(dt.Rows[0]["SYS_ACCP_DELETE"].ToString() == "N" ? "Delete" : "");
        permissions.Add(dt.Rows[0]["SYS_ACCP_EDIT"].ToString() == "N" ? "Edit" : "");
        return permissions;
    }

    public string InsertTerminal(string terminalIp, string terminalName, string cmpBranch) /*** added by : Tanjil Alam, 24-july-2014,***/
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT TERMINAL_IP, TERMINAL_NAME, CMP_BRANCH_ID FROM CM_TERMINAL_LIST ";
            DataRow oOrderRow;
            //DataRow oOrderRow1;

            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            //oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //oDbAdapter.Fill(oDs, "HCR_TREATMENT");
            oDbAdapter.FillSchema(oDs, SchemaType.Source, "CM_TERMINAL_LIST");
            oOrderRow = oDs.Tables["CM_TERMINAL_LIST"].NewRow();

            oOrderRow["TERMINAL_IP"] = terminalIp;
            oOrderRow["TERMINAL_NAME"] = terminalName;
            oOrderRow["CMP_BRANCH_ID"] = cmpBranch;

            oDs.Tables["CM_TERMINAL_LIST"].Rows.Add(oOrderRow);
            oDbAdapter.Update(oDs, "CM_TERMINAL_LIST");
            dbTransaction.Commit();
            // conn.Close();
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return "Success";
    }

    public string InsertEmpThumb(string EmpId, string userID, string terminal, string cmpBranch) /*** added by : Tanjil Alam, 24-july-2014,***/
    {
        conn = new OracleConnection(strConString);
        try
        {
            string strSql = "SELECT EMP_ID, USERID, CMP_BRANCH_ID,TERMINAL_ID FROM HR_EMPLOYEE_THUMB ";
            DataRow oOrderRow;
            //DataRow oOrderRow1;

            conn.Open();
            dbTransaction = conn.BeginTransaction();
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            //oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //oDbAdapter.Fill(oDs, "HCR_TREATMENT");
            oDbAdapter.FillSchema(oDs, SchemaType.Source, "HR_EMPLOYEE_THUMB");
            oOrderRow = oDs.Tables["HR_EMPLOYEE_THUMB"].NewRow();

            oOrderRow["EMP_ID"] = EmpId;
            oOrderRow["USERID"] = userID;
            oOrderRow["CMP_BRANCH_ID"] = cmpBranch;
            oOrderRow["TERMINAL_ID"] = terminal;

            oDs.Tables["HR_EMPLOYEE_THUMB"].Rows.Add(oOrderRow);
            oDbAdapter.Update(oDs, "HR_EMPLOYEE_THUMB");
            dbTransaction.Commit();
            // conn.Close();
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return "Success";
    }
    public string ExecuteScript(string strScript)
    {
        string strReturn = "";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strScript);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            strReturn = ex.Message.ToString();
        }
        return strReturn;
    }

    // Checking User Session
    #region Checking_User_Session
    public string ExpierSessAcctWise(string AccID)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = " UPDATE USER_SESSION_INFO SET SESSTION_STATE = 'E' WHERE ACCNT_ID = '" + AccID + "' ";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            conn = null;
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
    }
    public string InsertSessInfo(string accID, string userID, string sessID, string BrowName, string IP, string city, string division, string timeZone, string brow_sess)
    {
        string strSql;
        OracleConnection conn = new OracleConnection(strConString);
        DataRow oOrderRow;
        OracleTransaction dbTransaction;
        DataSet oDS = new DataSet();
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            strSql = "SELECT ACCNT_ID, USER_ID, SESSION_ID,BROWSER_NAME,USER_IP,CITY,DIVISION,TIME_ZONE,BROWSER_SESSION FROM USER_SESSION_INFO ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "USER_SESSION_INFO";
            oOrderRow = oDS.Tables["USER_SESSION_INFO"].NewRow();

            oOrderRow["ACCNT_ID"] = accID;
            oOrderRow["USER_ID"] = userID;
            oOrderRow["SESSION_ID"] = sessID;
            oOrderRow["BROWSER_NAME"] = BrowName;
            oOrderRow["USER_IP"] = IP;
            oOrderRow["CITY"] = city;
            oOrderRow["DIVISION"] = division;
            oOrderRow["TIME_ZONE"] = timeZone;
            oOrderRow["BROWSER_SESSION"] = brow_sess;

            oDS.Tables["USER_SESSION_INFO"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "USER_SESSION_INFO");

            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }
    public string ExpierSess(string brow_Sess)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = " UPDATE USER_SESSION_INFO SET SESSTION_STATE = 'E', SESSSION_END_TIME= '" + DateTime.Now.ToString() + "' WHERE BROWSER_SESSION = '" + brow_Sess + "' ";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            conn = null;
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
    }
    public string GetActiveSess(string browser_sess, string Acc_id)
    {
        string strOutput = "", strSQL = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        strSQL = "SELECT SESSION_ID FROM USER_SESSION_INFO where ACCNT_ID= '" + Acc_id + "' and BROWSER_SESSION = '" + browser_sess + "' and SESSTION_STATE = 'A'";
        try
        {
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["SESSION_ID"].ToString() != "")
                    {
                        strOutput = dr["SESSION_ID"].ToString();
                    }
                }
                return strOutput;
            }
            else
            {
                return strOutput;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return strOutput;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    #endregion

    public string GetSessionTimeOut()
    {
        string strSessionTimeOut = "";
        conn = new OracleConnection(strConString);
        string strSql = "SELECT DISTINCT SESSION_TIMEOUT_MIN FROM CM_SYSTEM_INFO";

        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter odaFrmLogin = new OracleDataAdapter(new OracleCommand(strSql, conn));
            odaFrmLogin.Fill(oDS, "CM_SYSTEM_INFO");
            strSessionTimeOut = oDS.Tables[0].Rows[0]["SESSION_TIMEOUT_MIN"].ToString();
        }
        catch (Exception e)
        {
            strSessionTimeOut = null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return strSessionTimeOut;
    }


    public string UpdatePlilBranchInfo(string branchId, string name, string address, string empName, string empMobile, string status)
    {
        OracleConnection conn;
        OracleCommand cmd;
        OracleTransaction transaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        transaction = conn.BeginTransaction();
        try
        {
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE PILIL_BRANCH_INFO SET BR_NAME = '" + name + "',BR_ADDRESS = '" + address + "',EMP_NAME = '" + empName + "',EMP_MOBILE = '" + empMobile + "',BR_STATUS = '" + status + "' WHERE PILIL_BRANCH_INFO_ID = '" + branchId + "'";

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();
            return "Successfull";
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
}
