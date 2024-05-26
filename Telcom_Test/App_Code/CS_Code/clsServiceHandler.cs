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
using System.Net;
using System.Collections;
using System.Data.OleDb;


public class clsServiceHandler
{
    private string strCDR_Destination;
    //private int intCDR_Timeinterval;
    private OracleConnection conn;
    private Int64 int64CountRequest;
    private string strConStatus;
    //private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    private string strConString = ConfigurationSettings.AppSettings["dbConnectionString"];

    public clsServiceHandler()
    {
        strCDR_Destination = ""; // System.Windows.Forms.Application.StartupPath; 
        //intCDR_Timeinterval = 15;
        int64CountRequest = 1;
        strConStatus = "";
    }
    public void ConnectDB()
    {
        //OracleConnection conn = new OracleConnection("Provider=Microsoft.Jet.Oracle.4.0;Data Source=APSNG101DB.mdb");
        //OracleConnection
        conn = new OracleConnection(strConString);
        conn.Open();
        strConStatus = "Connected";
    }
    public void ReplyError(string strMSIDN, string strLanguID, string strReqID)
    {
        //OracleConnection
        conn = new OracleConnection(strConString);
        string strMsg;
        try
        {
            conn.Open();
            OracleCommand myCMD = new OracleCommand("pkg_apsng_service.add_error_reply", conn);
            myCMD.CommandType = CommandType.StoredProcedure;
            //myCMD.CommandType=CommandType.Text;

            myCMD.Parameters.Add(new OracleParameter("inMsisdn", OracleType.VarChar)).Value = strMSIDN;
            myCMD.Parameters.Add(new OracleParameter("inMessage", OracleType.VarChar)).Value = "WMC";
            myCMD.Parameters.Add(new OracleParameter("inLanguId", OracleType.VarChar)).Value = strLanguID;
            myCMD.Parameters.Add(new OracleParameter("inReqID", OracleType.VarChar)).Value = strReqID;
            myCMD.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
        }

    }
    public void DisconnectDB()
    {
        conn.Close();
    }
    public void AddServiceRequest(string strFrom, string strTo, string strTime, string strMessage)
    {

        //OracleConnection conn = new OracleConnection("Provider=Microsoft.Jet.Oracle.4.0;Data Source=APSNG101DB.mdb");
        //OracleConnection 
        conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        // Create the DataTable "Orders" in the Dataset and the OrdersDataAdapter
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TIME,REQUEST_STAE,REQUEST_TEXT,ERROR_ID FROM SERVICE_REQUEST", conn));
        //oOrdersDataAdapter.RowUpdated += new OracleRowUpdatedEventHandler(OrdersDataAdapter_OnRowUpdate);
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
        DataTable pTable = oDS.Tables["Table"];
        pTable.TableName = "SERVICE_REQUEST";

        // Insert the Data
        DataRow oOrderRow = oDS.Tables["SERVICE_REQUEST"].NewRow();
        //oOrderRow["SH_TRAN_ID"] = GetTransactionID();
        oOrderRow["REQUEST_PARTY"] = strFrom;
        oOrderRow["RECEIPENT_PARTY"] = strTo;
        //oOrderRow["SERVICE_CODE"] = strServiceCode;
        oOrderRow["REQUEST_TIME"] = strTime;
        oOrderRow["REQUEST_STAE"] = "P";
        oOrderRow["REQUEST_TEXT"] = strMessage;

        if (CheckService(strMessage) == false)
        {
            oOrderRow["ERROR_ID"] = "APSNG101001";
        }
        oDS.Tables["SERVICE_REQUEST"].Rows.Add(oOrderRow);
        oOrdersDataAdapter.Update(oDS, "SERVICE_REQUEST");
        //conn.Close();

    }
    public string ImportRefSetupID(DataSet dtsCommission, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strSQL = "";
        //string strSQL1 = "";
        //string strFileID = "";
        int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //OracleDataAdapter oOrdersDataAdapter;
        DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //OracleTransaction dbTransaction2 = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();

        //------------------------------------------
        OracleCmd = new OracleCommand();
        OracleCmd.Connection = conn; //Active Connection
        OracleCmd.Transaction = dbTransaction;

        //strSQL1 = "DELETE FROM T_CUSTOMER_INFO";
        OracleCmd.CommandText = "DELETE FROM T_CUSTOMER_INFO";
        OracleCmd.ExecuteNonQuery();

        strSQL = "SELECT COMPLIANCE_SETUPID,REFILL_SETUPID FROM COMI_REFERANCE_SETUPID";

        DataSet dstCommission = new DataSet();
        OracleDataAdapter adpCommission = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbCommission = new OracleCommandBuilder(adpCommission);
        adpCommission.FillSchema(dstCommission, SchemaType.Source);
        DataTable dtpCommission = dstCommission.Tables["Table"];
        dtpCommission.TableName = "COMI_REFERANCE_SETUPID";
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        //try
        //{
        //-------------------------------------------------------------            
        try
        {
            foreach (DataRow pRow in dtsCommission.Tables["T_REFSETUPID"].Rows)
            {
                //---------------------------------------------------------------
                dtrRow = dstCommission.Tables["COMI_REFERANCE_SETUPID"].NewRow();


                dtrRow["COMPLIANCE_SETUPID"] = pRow["COMMISSIONSETUPID"];
                dtrRow["REFILL_SETUPID"] = pRow["REFCOMSETUPID"];

                dstCommission.Tables["COMI_REFERANCE_SETUPID"].Rows.Add(dtrRow);
                adpCommission.Update(dstCommission, "COMI_REFERANCE_SETUPID");


                intCount++;


            }
            dbTransaction.Commit();
            strMsg = " Referance SetupID Import Successfully !!";

        }
        catch (Exception ex)
        {
            strMsg = ex.Message.ToString();
        }
        conn.Close();
        return strMsg;
    }
    public string BlankCustInfoTable()
    {
        string strSql;

        strSql = "Delete FROM T_CUSTOMER_INFO";

        try
        {
            // OracleConnection 
            conn = new OracleConnection(strConString);
            OracleTransaction dbTransaction = null;
            conn.Open();
            dbTransaction = conn.BeginTransaction();

            OracleCommand olcmd = new OracleCommand(strSql, conn, dbTransaction);
            olcmd.Connection = conn;
            olcmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
    public string ForwardMessage(string strFrom, string strTo, string strMessage, string strAccID, string strMessagePurpose, string strRefNo)
    {

        try
        {
            string connectionInfo = ConfigurationManager.AppSettings["dbConnectionString"];
            //OracleConnection
            conn = new OracleConnection(connectionInfo);
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
    public string ForwardFlashMessage(string strFrom, string strTo, string strMessage, string strAccID, string strMessagePurpose, string strRefNo)
    {

        try
        {
            string connectionInfo = ConfigurationManager.AppSettings["dbConnectionString"];
            //OracleConnection
            conn = new OracleConnection(connectionInfo);
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
            oOrderRow["REQUEST_TEXT"] = "*TXTFM*" + strMessage + "#";
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
    public void AddBulkSubsFile(string strFileName, string strUserID, string strMachineIP, string strMachineName)
    {
        //OracleConnection
        conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT BULK_ACCNT_FILE,UPLOAD_SYS_USR_ID,UPLOAD_SYSTEM_IP,UPLOAD_SYSTEM_NAME FROM BULK_ACCOUNT_CREATION", conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
        DataTable pTable = oDS.Tables["Table"];
        pTable.TableName = "BULK_ACCOUNT_CREATION";

        // Insert the Data
        DataRow oOrderRow = oDS.Tables["BULK_ACCOUNT_CREATION"].NewRow();

        oOrderRow["BULK_ACCNT_FILE"] = strFileName;
        oOrderRow["UPLOAD_SYS_USR_ID"] = strUserID;
        oOrderRow["UPLOAD_SYSTEM_IP"] = strMachineIP;
        oOrderRow["UPLOAD_SYSTEM_NAME"] = strMachineName;

        oDS.Tables["BULK_ACCOUNT_CREATION"].Rows.Add(oOrderRow);
        oOrdersDataAdapter.Update(oDS, "BULK_ACCOUNT_CREATION");

    }
    public void AddCommissionFile(string strFileName, string strUserID, string strMachineIP, string strMachineName, string strFDate, string strTDate)
    {

        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT COMI_MASTER_NAME,COMI_FILE_NAME,COMI_STRAT_DATE,COMI_END_DATE FROM COMMISSION_MASTER", conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
        DataTable pTable = oDS.Tables["Table"];
        pTable.TableName = "COMMISSION_MASTER";

        // Insert the Data
        DataRow oOrderRow = oDS.Tables["COMMISSION_MASTER"].NewRow();

        oOrderRow["COMI_MASTER_NAME"] = strFileName;
        oOrderRow["COMI_FILE_NAME"] = strFileName;
        oOrderRow["COMI_STRAT_DATE"] = DateTime.Parse(strFDate);
        oOrderRow["COMI_END_DATE"] = DateTime.Parse(strTDate);

        oDS.Tables["COMMISSION_MASTER"].Rows.Add(oOrderRow);
        oOrdersDataAdapter.Update(oDS, "COMMISSION_MASTER");

    }
    public string LoadCommissionData(string strFileID, string strFileName, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strSQL = "";
        int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        OracleDataAdapter oOrdersDataAdapter;
        DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        strSQL = "SELECT COMMISSION_MONTH, COMMISSION_AMOUNT, RETAILER_MSISDN, COMI_TYPE_CODE, BROADCAST_COUNT,COMI_MASTER_ID FROM COMMISSION_DATA";
        DataSet dstClientList = new DataSet();
        OracleDataAdapter adpClientList = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbClientList = new OracleCommandBuilder(adpClientList);
        adpClientList.FillSchema(dstClientList, SchemaType.Source);
        DataTable dtpClientList = dstClientList.Tables["Table"];
        dtpClientList.TableName = "COMMISSION_DATA";
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {
            string ConnectionString = "Provider=Microsoft.Jet.Oracle.4.0; Data Source=" + strFileName + "; Extended Properties=\"Excel 8.0;HDR=Yes\";";
            string CommandText = "select * from [Sheet1$]";
            OracleConnection conExcell = new OracleConnection(ConnectionString);
            DataSet dtsBAccount = new DataSet();
            oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(CommandText, conExcell));
            oOrdersDataAdapter.Fill(dtsBAccount, "Sheet1");
            conExcell.Close();
            //-------------------------------------------------------------
            //-------------------------------------------------------------            
            try
            {
                foreach (DataRow pRow in dtsBAccount.Tables["Sheet1"].Rows)
                {
                    //if (intCount > 0)
                    //{

                    //---------------------------------------------------------------
                    dtrRow = dstClientList.Tables["COMMISSION_DATA"].NewRow();

                    dtrRow["RETAILER_MSISDN"] = pRow[0].ToString();
                    dtrRow["COMMISSION_MONTH"] = DateTime.Parse(pRow[1].ToString());
                    dtrRow["COMI_TYPE_CODE"] = pRow[2].ToString();
                    dtrRow["COMMISSION_AMOUNT"] = pRow[3].ToString();
                    dtrRow["COMI_MASTER_ID"] = strFileID;

                    dstClientList.Tables["COMMISSION_DATA"].Rows.Add(dtrRow);
                    adpClientList.Update(dstClientList, "COMMISSION_DATA");

                    //}
                    intCount++;

                }
                //---------------------------------------------------------------
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE COMMISSION_MASTER SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',LOADED_TO_DB='Y',"
                                     + "LOADED_SUMMARY='" + intCount + " commission data has been loaded.' WHERE COMI_MASTER_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //---------------------------------------------------
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
                dbTransaction = conn.BeginTransaction();
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE COMMISSION_MASTER SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',LOADED_TO_DB='Y',"
                                     + "LOADED_SUMMARY='" + intCount + " commission data has been loaded.' WHERE COMI_MASTER_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }
    public string ImportCommissionData(string strComiMasterID, DataSet dtsCommission, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strSQL = "";
        //string strFileID = "";
        int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //OracleDataAdapter oOrdersDataAdapter;
        DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        strSQL = "SELECT COMI_MASTER_ID, SERIALNO, MSISDNNO, PRODUCTCODE, PRODUCTNAME,DISTRIBUTORCODE, DISTRIBUTORNAME, DISTRIBUTORAMOUNT,"
               + "DISTRIBUTORERSNUMBER, RSPCODE, RSPNAME, RSPAMOUNT, RSPEASYLOADNUMBER, DSRCODE,DSRNAME, DSRAMOUNT, RETAILERCODE, "
               + "RETAILERNAME, RETAILERAMOUNT, RETAILEREASYLOADNUMBER,REGIONCODE, REGIONNAME, ZONECODE,ZONENAME, ACTIVATIONDATE, RCPSDATE, "
               + "LIFTATDIST, LIFTATRSP, LIFTATRETAILER, PAPERRCVATCDM, PAPERRCVATDIST, PAPERRCVATRSP,PROCESSDATE, COMMISSIONTYPE FROM COMMISSION_DATA";
        DataSet dstCommission = new DataSet();
        OracleDataAdapter adpCommission = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbCommission = new OracleCommandBuilder(adpCommission);
        adpCommission.FillSchema(dstCommission, SchemaType.Source);
        DataTable dtpCommission = dstCommission.Tables["Table"];
        dtpCommission.TableName = "COMMISSION_DATA";
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {
            //-------------------------------------------------------------            
            try
            {
                foreach (DataRow pRow in dtsCommission.Tables["VW_QualifiedList"].Rows)
                {
                    //---------------------------------------------------------------
                    dtrRow = dstCommission.Tables["COMMISSION_DATA"].NewRow();

                    dtrRow["COMI_MASTER_ID"] = strComiMasterID;
                    dtrRow["SERIALNO"] = pRow["COMI_MASTER_ID"];
                    dtrRow["MSISDNNO"] = pRow["MSISDNNO"];
                    dtrRow["PRODUCTCODE"] = pRow["PRODUCTCODE"];
                    dtrRow["PRODUCTNAME"] = pRow["PRODUCTNAME"];
                    dtrRow["DISTRIBUTORCODE"] = pRow["DISTRIBUTORCODE"];
                    dtrRow["DISTRIBUTORNAME"] = pRow["DISTRIBUTORNAME"];
                    dtrRow["DISTRIBUTORAMOUNT"] = pRow["DISTRIBUTORAMOUNT"];
                    dtrRow["DISTRIBUTORERSNUMBER"] = pRow["DISTRIBUTORERSNUMBER"];
                    dtrRow["RSPCODE"] = pRow["RSPCODE"];
                    dtrRow["RSPNAME"] = pRow["RSPNAME"];
                    dtrRow["RSPAMOUNT"] = pRow["RSPAMOUNT"];
                    dtrRow["RSPEASYLOADNUMBER"] = pRow["RSPEASYLOADNUMBER"];
                    dtrRow["DSRCODE"] = pRow["DSRCODE"];
                    dtrRow["DSRNAME"] = pRow["DSRNAME"];
                    dtrRow["DSRAMOUNT"] = pRow["DSRAMOUNT"];
                    dtrRow["RETAILERCODE"] = pRow["RETAILERCODE"];
                    dtrRow["RETAILERNAME"] = pRow["RETAILERNAME"];
                    dtrRow["RETAILERAMOUNT"] = pRow["RETAILERAMOUNT"];
                    dtrRow["RETAILEREASYLOADNUMBER"] = pRow["RETAILEREASYLOADNUMBER"];
                    dtrRow["REGIONCODE"] = pRow["REGIONCODE"];
                    dtrRow["REGIONNAME"] = pRow["REGIONNAME"];
                    dtrRow["ZONECODE"] = pRow["ZONECODE"];
                    dtrRow["ZONENAME"] = pRow["ZONENAME"];
                    dtrRow["ACTIVATIONDATE"] = pRow["ACTIVATIONDATE"];
                    dtrRow["RCPSDATE"] = pRow["RCPSDATE"];
                    dtrRow["LIFTATDIST"] = pRow["LIFTATDIST"];
                    dtrRow["LIFTATRSP"] = pRow["LIFTATRSP"];
                    dtrRow["LIFTATRETAILER"] = pRow["LIFTATRETAILER"];
                    dtrRow["PAPERRCVATCDM"] = pRow["PAPERRCVATCDM"];
                    dtrRow["PAPERRCVATDIST"] = pRow["PAPERRCVATDIST"];
                    dtrRow["PAPERRCVATRSP"] = pRow["PAPERRCVATRSP"];
                    dtrRow["PROCESSDATE"] = pRow["PROCESSDATE"];
                    dtrRow["COMMISSIONTYPE"] = pRow["COMMISSIONTYPE"];

                    dstCommission.Tables["COMMISSION_DATA"].Rows.Add(dtrRow);
                    adpCommission.Update(dstCommission, "COMMISSION_DATA");

                    //}
                    intCount++;

                }
                //---------------------------------------------------------------
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE COMMISSION_MASTER SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',LOADED_TO_DB='Y',"
                                     + "LOADED_SUMMARY='" + intCount + " commission data has been loaded.' WHERE COMI_MASTER_ID='" + strComiMasterID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //---------------------------------------------------
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
                dbTransaction = conn.BeginTransaction();
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE COMMISSION_MASTER SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',LOADED_TO_DB='Y',"
                                     + "LOADED_SUMMARY='" + intCount + " commission data has been loaded.' WHERE COMI_MASTER_ID='" + strComiMasterID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }
    // added by bushra 28/5/13
    //public string Getrefresh(string strTOPUP_TRAN_ID)
    //{

    //    string updateString = @"UPDATE TOPUP_TRANSACTION SET SSL_FINAL_STATUS ='',SSL_FINAL_MESSAGE='' WHERE TOPUP_TRAN_ID='" + strTOPUP_TRAN_ID + "' ";
    //    OracleConnection conn = new OracleConnection(strConString);
    //    string strReturn = "";

    //    try
    //    {
    //        conn.Open();
    //        OracleCommand cmd = new OracleCommand(updateString);
    //        cmd.Connection = conn;
    //        cmd.ExecuteNonQuery();
    //        conn.Close();
    //        strReturn = "Refresh";
    //    }
    //    catch (Exception ex)
    //    {
    //        strReturn = ex.Message.ToString();
    //    }
    //    return strReturn;
    //}

    // added by bushra 28/5/13

    //updated by : Aminul
    //Date: 9/Jan/2014
    //update note : Only Where OWNER_CODE=SSL  the refresh process will work 

    public string Getrefresh(string strTOPUP_TRAN_ID, string strOWNER_CODE)
    {

        string updateString = @"UPDATE TOPUP_TRANSACTION SET SSL_FINAL_STATUS ='',SSL_FINAL_MESSAGE='' WHERE TOPUP_TRAN_ID='" + strTOPUP_TRAN_ID + "' AND OWNER_CODE = 'SSL'";
        OracleConnection conn = new OracleConnection(strConString);
        string strReturn = "";

        if (strOWNER_CODE == "SSL")
        {
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(updateString);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                strReturn = "Refresh";
            }
            catch (Exception ex)
            {
                strReturn = ex.Message.ToString();
            }
            return strReturn;
        }
        else
        {
            strReturn = "Only the requests from SSL can be refreshed";
            return strReturn;
        }


    }

    public string ExecuteRobiChannelFile(string strFileID, string strFileName, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strClientID = "";
        string strAccountID = "";
        string strSQL = "";
        int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //DataSet oDS;
        OracleDataAdapter oOrdersDataAdapter;
        DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        strSQL = "SELECT CLINT_NAME,CLINT_PASS,CLINT_ADDRESS1,CLINT_ADDRESS2,CLINT_TOWN,CLINT_POSTCODE,CLINT_CITY,"
              + "COUNTRY_ID,CLINT_CONTACT_F_NAME,CLINT_CONTACT_M_NAME,CLINT_CONTACT_L_NAME,CLINT_JOB_TITLE, CLINT_CONTACT_EMAIL,CLINT_LAND_LINE,"
              + "CLINT_MOBILE,CLINT_FAX,CLINT_TITLE,CLINT_M_NAME,CLINT_L_NAME,CLINT_CONTACT_TITLE,CLINT_GENDER,CLINT_N_ID,CLINT_PASSPORT_NO,"
              + "CLI_ZONE_ID,CLINET_RSP_CODE,CLIENT_RSP_NAME,DISTRIBUTOR_NAME,DISTRIBUTOR_CODE,DISTRIBUTOR_ZONE_ID,OWNER_NAME,OWNER_MOBILE,OWNER_NID FROM CLIENT_LIST";
        DataSet dstClientList = new DataSet();
        OracleDataAdapter adpClientList = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbClientList = new OracleCommandBuilder(adpClientList);
        adpClientList.FillSchema(dstClientList, SchemaType.Source);
        DataTable dtpClientList = dstClientList.Tables["Table"];
        dtpClientList.TableName = "CLIENT_LIST";
        //----------------------------
        System.Random RandNum = new System.Random();
        //###################### Create Distributors List ##############################
        //----------------------------
        try
        {
            string ConnectionString = "Provider=Microsoft.Jet.Oracle.4.0; Data Source=" + strFileName + "; Extended Properties=\"Excel 8.0;HDR=Yes\";";
            string CommandText = "select DISTINCT [Region Name],[DISTRIBUTOR-ID],[Distributor Name],[Distributor Master ERS Number] from [Sheet1$]";
            OracleConnection conExcell = new OracleConnection(ConnectionString);
            DataSet dtsBAccount = new DataSet();
            oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(CommandText, conExcell));
            oOrdersDataAdapter.Fill(dtsBAccount, "Sheet1");
            conExcell.Close();
            //-------------------------------------------------------------
            //-------------------------------------------------------------            
            try
            {
                foreach (DataRow pRow in dtsBAccount.Tables["Sheet1"].Rows)
                {
                    if (!pRow[1].ToString().Equals(""))
                    {
                        //-------------------- Get new Clinet ID--------------------------------- 
                        OracleCmd = new OracleCommand();
                        OracleCmd.Connection = conn; //Active Connection
                        OracleCmd.Transaction = dbTransaction;
                        //---------------------------------------------------------------
                        OracleCmd.Parameters.Add("ClientID", OracleType.VarChar, 20).Direction = ParameterDirection.ReturnValue;
                        OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_id(0)"; // Stored Procedure to Call
                        OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                        OracleCmd.ExecuteNonQuery();
                        strClientID = OracleCmd.Parameters["ClientID"].Value.ToString();
                        //---------------------------------------------------------------
                        OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_acc_id(0)"; // Stored Procedure to Call
                        OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                        OracleCmd.ExecuteNonQuery();
                        strAccountID = OracleCmd.Parameters["ClientID"].Value.ToString();

                        //---------------------------------------------------------------------
                        dtrRow = dstClientList.Tables["CLIENT_LIST"].NewRow();

                        dtrRow["CLINT_TITLE"] = "Mr";
                        dtrRow["CLINT_NAME"] = pRow[2].ToString() + " [DIS]";
                        dtrRow["CLINT_PASS"] = "1234";
                        dtrRow["CLINT_ADDRESS1"] = "Unknown";
                        dtrRow["CLINT_ADDRESS2"] = "Unknown";
                        dtrRow["CLINT_TOWN"] = "Unknown";
                        dtrRow["CLINT_POSTCODE"] = "Unknown";
                        dtrRow["CLINT_CITY"] = "Unknown";
                        dtrRow["COUNTRY_ID"] = "0021";

                        dtrRow["CLINT_CONTACT_TITLE"] = "";
                        dtrRow["CLINT_CONTACT_F_NAME"] = "";
                        dtrRow["CLINT_CONTACT_M_NAME"] = "";
                        dtrRow["CLINT_CONTACT_L_NAME"] = "";
                        dtrRow["CLINT_JOB_TITLE"] = "";
                        dtrRow["CLINT_CONTACT_EMAIL"] = "";
                        dtrRow["CLINT_LAND_LINE"] = "";
                        dtrRow["CLINT_MOBILE"] = "";
                        dtrRow["CLINT_FAX"] = "";

                        dtrRow["CLINT_GENDER"] = "M";
                        dtrRow["CLINT_N_ID"] = pRow[1].ToString();
                        dtrRow["CLINT_PASSPORT_NO"] = "";

                        dtrRow["CLI_ZONE_ID"] = "101006000003";
                        dtrRow["DISTRIBUTOR_ZONE_ID"] = pRow[0].ToString(); ;


                        dstClientList.Tables["CLIENT_LIST"].Rows.Add(dtrRow);
                        adpClientList.Update(dstClientList, "CLIENT_LIST");
                        intCount++;
                        if (AddAccount(ref conn, ref dbTransaction, strClientID, "0905040001", pRow[3].ToString(), pRow[1].ToString(), "A").Equals(""))
                        {
                        }
                        //################# FOR RSP #############################
                        CommandText = "select DISTINCT [Region Name],[DISTRIBUTOR-ID],[RSP Code],[Name of RSP],[RSP Master ERS Number],[Area] from [Sheet1$] WHERE [DISTRIBUTOR-ID]=" + pRow[1].ToString() + "";
                        conExcell = new OracleConnection(ConnectionString);
                        DataSet dtsRSPAccount = new DataSet();
                        OracleDataAdapter rspDataAdapter = new OracleDataAdapter(new OracleCommand(CommandText, conExcell));
                        rspDataAdapter.Fill(dtsRSPAccount, "Sheet1");
                        conExcell.Close();
                        //---------------------------------------------------------------
                        OracleCmd = new OracleCommand();
                        OracleCmd.Connection = conn; //Active Connection
                        OracleCmd.Transaction = dbTransaction;
                        //###########################
                        OracleCmd.Parameters.Add("RSP_D", OracleType.VarChar, 20).Direction = ParameterDirection.ReturnValue;
                        OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_id(0)"; // Stored Procedure to Call
                        OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                        OracleCmd.ExecuteNonQuery();
                        strClientID = OracleCmd.Parameters["RSP_D"].Value.ToString();
                        //---------------------------------------------------------------
                        foreach (DataRow qRow in dtsRSPAccount.Tables["Sheet1"].Rows)
                        {
                            if (!qRow[1].ToString().Equals(""))
                            {
                                dtrRow = dstClientList.Tables["CLIENT_LIST"].NewRow();

                                dtrRow["CLINT_TITLE"] = "Mr";
                                dtrRow["CLINT_NAME"] = qRow[3].ToString() + " [RSP]";
                                dtrRow["CLINT_PASS"] = "1234";
                                dtrRow["CLINT_ADDRESS1"] = "Unknown";
                                dtrRow["CLINT_ADDRESS2"] = "Unknown";
                                dtrRow["CLINT_TOWN"] = "Unknown";
                                dtrRow["CLINT_POSTCODE"] = "Unknown";
                                dtrRow["CLINT_CITY"] = "Unknown";
                                dtrRow["COUNTRY_ID"] = "0021";

                                dtrRow["CLINT_GENDER"] = "M";
                                dtrRow["CLINT_N_ID"] = qRow[2].ToString();

                                dtrRow["CLI_ZONE_ID"] = "101006000003";
                                dtrRow["DISTRIBUTOR_ZONE_ID"] = qRow[0].ToString();
                                dtrRow["CLI_ZONE_ID"] = qRow[5].ToString();
                                dtrRow["DISTRIBUTOR_CODE"] = qRow[1].ToString();

                                dstClientList.Tables["CLIENT_LIST"].Rows.Add(dtrRow);
                                adpClientList.Update(dstClientList, "CLIENT_LIST");
                                intCount++;
                                if (AddAccount(ref conn, ref dbTransaction, strClientID, "0905040001", qRow[4].ToString(), qRow[2].ToString(), "A").Equals(""))
                                {
                                }
                                //################## FOR POS ############################
                                CommandText = "select DISTINCT [Region Name],[DISTRIBUTOR-ID],[RSP Code],[SIM POS Code],[Name of SIM POS],[EasyLoad Number],[Area] from [Sheet1$] WHERE [RSP Code]=" + qRow[2].ToString() + "";
                                conExcell = new OracleConnection(ConnectionString);
                                DataSet dtsPOSAccount = new DataSet();
                                OracleDataAdapter rspPOSAdapter = new OracleDataAdapter(new OracleCommand(CommandText, conExcell));
                                rspPOSAdapter.Fill(dtsPOSAccount, "Sheet1");
                                conExcell.Close();
                                //---------------------------------------------------------------
                                OracleCmd = new OracleCommand();
                                OracleCmd.Connection = conn; //Active Connection
                                OracleCmd.Transaction = dbTransaction;
                                //###########################
                                OracleCmd.Parameters.Add("RSP_D", OracleType.VarChar, 20).Direction = ParameterDirection.ReturnValue;
                                OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_id(0)"; // Stored Procedure to Call
                                OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                                OracleCmd.ExecuteNonQuery();
                                strClientID = OracleCmd.Parameters["RSP_D"].Value.ToString();
                                //---------------------------------------------------------------
                                foreach (DataRow rRow in dtsPOSAccount.Tables["Sheet1"].Rows)
                                {
                                    if (!rRow[1].ToString().Equals(""))
                                    {
                                        dtrRow = dstClientList.Tables["CLIENT_LIST"].NewRow();
                                        dtrRow["CLINT_TITLE"] = "Mr";
                                        dtrRow["CLINT_NAME"] = rRow[4].ToString() + " [POS]";
                                        dtrRow["CLINT_PASS"] = "1234";
                                        dtrRow["CLINT_ADDRESS1"] = "Unknown";
                                        dtrRow["CLINT_ADDRESS2"] = "Unknown";
                                        dtrRow["CLINT_TOWN"] = "Unknown";
                                        dtrRow["CLINT_POSTCODE"] = "Unknown";
                                        dtrRow["CLINT_CITY"] = "Unknown";
                                        dtrRow["COUNTRY_ID"] = "0021";
                                        dtrRow["CLINT_GENDER"] = "M";
                                        dtrRow["CLINT_N_ID"] = rRow[3].ToString();

                                        dtrRow["CLI_ZONE_ID"] = "101006000003";
                                        dtrRow["DISTRIBUTOR_ZONE_ID"] = rRow[0].ToString();
                                        dtrRow["CLI_ZONE_ID"] = rRow[6].ToString();
                                        dtrRow["CLINET_RSP_CODE"] = rRow[2].ToString();
                                        dtrRow["DISTRIBUTOR_CODE"] = rRow[1].ToString();

                                        dstClientList.Tables["CLIENT_LIST"].Rows.Add(dtrRow);
                                        adpClientList.Update(dstClientList, "CLIENT_LIST");
                                        intCount++;
                                        if (AddAccount(ref conn, ref dbTransaction, strClientID, "0905040001", rRow[5].ToString(), rRow[3].ToString(), "A").Equals(""))
                                        {
                                        }
                                    }
                                }
                            }
                        }
                        //#################################                        
                    }
                }
                //---------------------------------------------------------------
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE BULK_ACCOUNT_CREATION SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',BULK_ACCNT_CRE_STATUS='E',"
                                     + "BULK_ACCNT_RESULT='" + intCount + " new channels have been created.' WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //---------------------------------------------------
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
                dbTransaction = conn.BeginTransaction();
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE BULK_ACCOUNT_CREATION SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',BULK_ACCNT_CRE_STATUS='E',"
                                     + "BULK_ACCNT_RESULT='" + strMsg + "' WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }

    public string ExecuteRobiBulkSubFile(string strFileID, string strFileName, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strClientID = "";
        string strAccountID = "";
        //string strPIN1 = "";
        //string strPIN2 = "";
        //string strAns1 = "";
        //string strAns2 = "";
        //string strAns3 = "";
        //string strAns4 = "";
        string strSQL = "";
        int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //DataSet oDS;
        OracleDataAdapter oOrdersDataAdapter;
        DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        strSQL = "SELECT CLINT_NAME,CLINT_PASS,CLINT_ADDRESS1,CLINT_ADDRESS2,CLINT_TOWN,CLINT_POSTCODE,CLINT_CITY,"
             + "COUNTRY_ID,CLINT_CONTACT_F_NAME,CLINT_CONTACT_M_NAME,CLINT_CONTACT_L_NAME,CLINT_JOB_TITLE, CLINT_CONTACT_EMAIL,CLINT_LAND_LINE,"
             + "CLINT_MOBILE,CLINT_FAX,CLINT_TITLE,CLINT_M_NAME,CLINT_L_NAME,CLINT_CONTACT_TITLE,CLINT_GENDER,CLINT_N_ID,CLINT_PASSPORT_NO,"
             + "CLI_ZONE_ID,CLINET_RSP_CODE,CLIENT_RSP_NAME,DISTRIBUTOR_NAME,DISTRIBUTOR_CODE,DISTRIBUTOR_ZONE_ID,OWNER_NAME,OWNER_MOBILE,OWNER_NID FROM CLIENT_LIST";
        DataSet dstClientList = new DataSet();
        OracleDataAdapter adpClientList = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbClientList = new OracleCommandBuilder(adpClientList);
        adpClientList.FillSchema(dstClientList, SchemaType.Source);
        DataTable dtpClientList = dstClientList.Tables["Table"];
        dtpClientList.TableName = "CLIENT_LIST";
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {
            string ConnectionString = "Provider=Microsoft.Jet.Oracle.4.0; Data Source=" + strFileName + "; Extended Properties=\"Excel 8.0;HDR=Yes\";";
            string CommandText = "select * from [Sheet1$]";
            OracleConnection conExcell = new OracleConnection(ConnectionString);
            DataSet dtsBAccount = new DataSet();
            oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(CommandText, conExcell));
            oOrdersDataAdapter.Fill(dtsBAccount, "Sheet1");
            conExcell.Close();
            //-------------------------------------------------------------
            //-------------------------------------------------------------            
            try
            {
                foreach (DataRow pRow in dtsBAccount.Tables["Sheet1"].Rows)
                {
                    if (pRow[0].ToString().Equals(""))
                    {
                        break;
                    }
                    //-------------------- Get new Clinet ID--------------------------------- 
                    OracleCmd = new OracleCommand();
                    OracleCmd.Connection = conn; //Active Connection
                    OracleCmd.Transaction = dbTransaction;
                    //---------------------------------------------------------------
                    OracleCmd.Parameters.Add("ClientID", OracleType.VarChar, 20).Direction = ParameterDirection.ReturnValue;
                    OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_id(0)"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    strClientID = OracleCmd.Parameters["ClientID"].Value.ToString();
                    //---------------------------------------------------------------
                    OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_acc_id(0)"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    strAccountID = OracleCmd.Parameters["ClientID"].Value.ToString();
                    //---------------------------------------------------------------------
                    dtrRow = dstClientList.Tables["CLIENT_LIST"].NewRow();

                    dtrRow["CLINT_TITLE"] = "Mr";
                    dtrRow["CLINT_NAME"] = pRow[2].ToString();
                    dtrRow["CLINT_PASS"] = "1234";
                    dtrRow["CLINT_ADDRESS1"] = pRow[4].ToString();
                    dtrRow["CLINT_TOWN"] = pRow[8].ToString();
                    dtrRow["CLINT_POSTCODE"] = pRow[12].ToString();
                    dtrRow["CLINT_CITY"] = pRow[10].ToString();
                    dtrRow["COUNTRY_ID"] = "0021";

                    dtrRow["CLI_ZONE_ID"] = "101006000003";
                    dtrRow["DISTRIBUTOR_ZONE_ID"] = "101006000002";
                    //dtrRow["CLI_ZONE_ID"] = rRow[6].ToString();
                    //dtrRow["CLINT_JOB_TITLE"] = pRow[14].ToString();
                    //dtrRow["CLINT_CONTACT_EMAIL"] = pRow[15].ToString();
                    //dtrRow["CLINT_LAND_LINE"] = pRow[16].ToString();
                    dtrRow["CLINT_MOBILE"] = pRow[15].ToString();
                    dtrRow["CLINT_CONTACT_F_NAME"] = pRow[16].ToString();

                    dtrRow["CLINT_GENDER"] = "M";
                    //dtrRow["CLINT_N_ID"] = pRow[20].ToString();
                    //dtrRow["CLINT_PASSPORT_NO"] = pRow[21].ToString();

                    dstClientList.Tables["CLIENT_LIST"].Rows.Add(dtrRow);
                    adpClientList.Update(dstClientList, "CLIENT_LIST");
                    intCount++;
                    if (AddAccount(ref conn, ref dbTransaction, strClientID, "0905040001", pRow[14].ToString(), pRow[0].ToString(), "A").Equals(""))
                    {
                    }
                }
                //---------------------------------------------------------------
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE BULK_ACCOUNT_CREATION SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',BULK_ACCNT_CRE_STATUS='E',"
                                     + "BULK_ACCNT_RESULT='" + intCount + " new clients has been created.' WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //---------------------------------------------------
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
                dbTransaction = conn.BeginTransaction();
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE BULK_ACCOUNT_CREATION SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',BULK_ACCNT_CRE_STATUS='E',"
                                     + "BULK_ACCNT_RESULT='" + strMsg + "' WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }
    public string AddAccount(ref OracleConnection conn, ref OracleTransaction dbTransaction, string strClientID, string strPackageID, string strMSISDN, string strAccCode, string strState)
    {
        string updateString;
        updateString = @"INSERT INTO ACCOUNT_LIST(CLINT_ID,SERVICE_PKG_ID,ACCNT_MSISDN,ACCNT_NO,ACCNT_STATE) "
                     + "VALUES('" + strClientID.Trim() + "','" + strPackageID + "','+880" + strMSISDN + "','" + strAccCode + "','" + strState + "')";

        string strReturn = "";
        try
        {
            OracleCommand cmd = new OracleCommand(updateString);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            strReturn = ex.Message.ToString() + strAccCode;
        }
        return strReturn;
    }
    public string ExecuteBulkSubFile(string strFileID, string strFileName, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strClientID = "";
        string strAccountID = "";
        string strPIN1 = "";
        string strPIN2 = "";
        string strAns1 = "";
        string strAns2 = "";
        string strAns3 = "";
        string strAns4 = "";
        string strSQL = "";
        int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //DataSet oDS;
        OracleDataAdapter oOrdersDataAdapter;
        DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        strSQL = "SELECT CLINT_NAME,CLINT_PASS,CLINT_ADDRESS1,CLINT_ADDRESS2,CLINT_TOWN,CLINT_POSTCODE,CLINT_CITY,"
              + "COUNTRY_ID,CLINT_CONTACT_F_NAME,CLINT_CONTACT_M_NAME,CLINT_CONTACT_L_NAME,CLINT_JOB_TITLE, CLINT_CONTACT_EMAIL,CLINT_LAND_LINE,"
              + "CLINT_MOBILE,CLINT_FAX,CLINT_TITLE,CLINT_M_NAME,CLINT_L_NAME,CLINT_CONTACT_TITLE,CLINT_GENDER,CLINT_N_ID,CLINT_PASSPORT_NO FROM CLIENT_LIST";
        DataSet dstClientList = new DataSet();
        OracleDataAdapter adpClientList = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbClientList = new OracleCommandBuilder(adpClientList);
        adpClientList.FillSchema(dstClientList, SchemaType.Source);
        DataTable dtpClientList = dstClientList.Tables["Table"];
        dtpClientList.TableName = "CLIENT_LIST";
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {
            string ConnectionString = "Provider=Microsoft.Jet.Oracle.4.0; Data Source=" + strFileName + "; Extended Properties=\"Excel 8.0;HDR=Yes\";";
            string CommandText = "select * from [Sheet1$]";
            OracleConnection conExcell = new OracleConnection(ConnectionString);
            DataSet dtsBAccount = new DataSet();
            oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(CommandText, conExcell));
            oOrdersDataAdapter.Fill(dtsBAccount, "Sheet1");
            conExcell.Close();
            //-------------------------------------------------------------
            //-------------------------------------------------------------            
            try
            {
                foreach (DataRow pRow in dtsBAccount.Tables["Sheet1"].Rows)
                {
                    //if (intCount > 0)
                    //{
                    //-------------------- Get new Clinet ID--------------------------------- 
                    OracleCmd = new OracleCommand();
                    OracleCmd.Connection = conn; //Active Connection
                    OracleCmd.Transaction = dbTransaction;
                    //---------------------------------------------------------------
                    OracleCmd.Parameters.Add("ClientID", OracleType.VarChar, 20).Direction = ParameterDirection.ReturnValue;
                    OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_id(0)"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    strClientID = OracleCmd.Parameters["ClientID"].Value.ToString();
                    //---------------------------------------------------------------
                    OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_acc_id(0)"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    strAccountID = OracleCmd.Parameters["ClientID"].Value.ToString();
                    //---------------------------------------------------------------------------------
                    strPIN1 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    strPIN2 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    strAns1 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    strAns2 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    strAns3 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    strAns4 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    //---------------------------------------------------------------------
                    dtrRow = dstClientList.Tables["CLIENT_LIST"].NewRow();

                    dtrRow["CLINT_TITLE"] = pRow[0].ToString();
                    dtrRow["CLINT_NAME"] = pRow[1].ToString();
                    dtrRow["CLINT_M_NAME"] = pRow[2].ToString();
                    dtrRow["CLINT_L_NAME"] = pRow[3].ToString();
                    dtrRow["CLINT_PASS"] = strPIN1;
                    dtrRow["CLINT_ADDRESS1"] = pRow[4].ToString();
                    dtrRow["CLINT_ADDRESS2"] = pRow[5].ToString();
                    dtrRow["CLINT_TOWN"] = pRow[6].ToString();
                    dtrRow["CLINT_POSTCODE"] = pRow[7].ToString();
                    dtrRow["CLINT_CITY"] = pRow[8].ToString();
                    dtrRow["COUNTRY_ID"] = pRow[9].ToString();

                    dtrRow["CLINT_CONTACT_TITLE"] = pRow[10].ToString();
                    dtrRow["CLINT_CONTACT_F_NAME"] = pRow[11].ToString();
                    dtrRow["CLINT_CONTACT_M_NAME"] = pRow[12].ToString();
                    dtrRow["CLINT_CONTACT_L_NAME"] = pRow[13].ToString();
                    dtrRow["CLINT_JOB_TITLE"] = pRow[14].ToString();
                    dtrRow["CLINT_CONTACT_EMAIL"] = pRow[15].ToString();
                    dtrRow["CLINT_LAND_LINE"] = pRow[16].ToString();
                    dtrRow["CLINT_MOBILE"] = pRow[17].ToString();
                    dtrRow["CLINT_FAX"] = pRow[18].ToString();

                    dtrRow["CLINT_GENDER"] = pRow[19].ToString();
                    dtrRow["CLINT_N_ID"] = pRow[20].ToString();
                    dtrRow["CLINT_PASSPORT_NO"] = pRow[21].ToString();

                    dstClientList.Tables["CLIENT_LIST"].Rows.Add(dtrRow);
                    adpClientList.Update(dstClientList, "CLIENT_LIST");

                    //}
                    intCount++;

                }
                //---------------------------------------------------------------
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE BULK_ACCOUNT_CREATION SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',BULK_ACCNT_CRE_STATUS='E',"
                                     + "BULK_ACCNT_RESULT='" + intCount + " new clients has been created.' WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //---------------------------------------------------
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
                dbTransaction = conn.BeginTransaction();
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE BULK_ACCOUNT_CREATION SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',BULK_ACCNT_CRE_STATUS='E',"
                                     + "BULK_ACCNT_RESULT='" + strMsg + "' WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }
    public string ExecuteBulkAccountFile(string strFileID, string strFileName, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        //string strClientID = "";
        string strAccountID = "";
        string strPIN1 = "";
        string strPIN2 = "";
        string strAns1 = "";
        //string strAns2 = "";
        //string strAns3 = "";
        //string strAns4 = "";
        string strSQL = "";
        int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //DataSet oDS;
        OracleDataAdapter oOrdersDataAdapter;
        DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        //-------------------------------------------
        strSQL = "SELECT ACCNT_SEC_QUES_ID,ACCNT_SEC_QUES_DESC,ACCNT_SEC_QUES_SLNO FROM ACCOUNT_SEC_QUESTION";
        DataSet dtsSecQuestion = new DataSet();
        OracleDataAdapter adpSecQuestion = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbSecQuestion = new OracleCommandBuilder(adpSecQuestion);
        adpSecQuestion.Fill(dtsSecQuestion, "ACCOUNT_SEC_QUESTION");
        //DataTable dtpSecQuestion = dtsSecQuestion.Tables["Table"];
        //dtpSecQuestion.TableName = "ACCOUNT_SEC_QUESTION";
        //-------------------------------------------
        strSQL = "SELECT CLINT_ID,ACCNT_NO,ACCNT_PIN,ACCNT_PIN_POLICY,ACCNT_CHARGE_TYPE,ACCNT_STATE,ACCNT_BALANCE,"
                + "SERVICE_PKG_ID,ACCNT_MSISDN,ACCNT_PIN2 FROM ACCOUNT_LIST";
        DataSet dstClientList = new DataSet();
        OracleDataAdapter adpClientList = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbClientList = new OracleCommandBuilder(adpClientList);
        adpClientList.FillSchema(dstClientList, SchemaType.Source);
        DataTable dtpClientList = dstClientList.Tables["Table"];
        dtpClientList.TableName = "ACCOUNT_LIST";
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {
            string ConnectionString = "Provider=Microsoft.Jet.Oracle.4.0; Data Source=" + strFileName + "; Extended Properties=\"Excel 8.0;HDR=Yes\";";
            string CommandText = "select * from [Sheet1$]";
            OracleConnection conExcell = new OracleConnection(ConnectionString);
            DataSet dtsBAccount = new DataSet();
            oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(CommandText, conExcell));
            oOrdersDataAdapter.Fill(dtsBAccount, "Sheet1");
            conExcell.Close();
            //-------------------------------------------------------------
            //-------------------------------------------------------------            
            try
            {
                foreach (DataRow pRow in dtsBAccount.Tables["Sheet1"].Rows)
                {
                    //-------------------- Get new Clinet ID--------------------------------- 
                    OracleCmd = new OracleCommand();
                    OracleCmd.Connection = conn; //Active Connection
                    OracleCmd.Transaction = dbTransaction;
                    //---------------------------------------------------------------
                    OracleCmd.Parameters.Add("ClientID", OracleType.VarChar, 20).Direction = ParameterDirection.ReturnValue;
                    OracleCmd.CommandText = "PKG_APSNG_COMMON.get_new_client_acc_id(0)"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.StoredProcedure; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    strAccountID = OracleCmd.Parameters["ClientID"].Value.ToString();
                    //---------------------------------------------------------------------------------
                    strPIN1 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    strPIN2 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());

                    //---------------------------------------------------------------------
                    dtrRow = dstClientList.Tables["ACCOUNT_LIST"].NewRow();

                    dtrRow["CLINT_ID"] = pRow[0].ToString();
                    dtrRow["ACCNT_NO"] = pRow[2].ToString();
                    dtrRow["ACCNT_PIN"] = strPIN1;
                    dtrRow["ACCNT_PIN2"] = strPIN2;
                    dtrRow["ACCNT_PIN_POLICY"] = "S";
                    dtrRow["ACCNT_CHARGE_TYPE"] = pRow[3].ToString();
                    dtrRow["ACCNT_STATE"] = "A";
                    dtrRow["ACCNT_BALANCE"] = pRow[4].ToString();
                    //dtrRow["ACCNT_EXPIRY_DATE"] = pRow[6].ToString();
                    dtrRow["SERVICE_PKG_ID"] = pRow[5].ToString();
                    dtrRow["ACCNT_MSISDN"] = pRow[6].ToString();

                    dstClientList.Tables["ACCOUNT_LIST"].Rows.Add(dtrRow);
                    adpClientList.Update(dstClientList, "ACCOUNT_LIST");

                    //----------------------------------------------------------
                    foreach (DataRow pSecQuest in dtsSecQuestion.Tables["ACCOUNT_SEC_QUESTION"].Rows)
                    {
                        strAns1 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                        OracleCmd.CommandText = "INSERT INTO ACCOUNT_SEC_ANSWER(ACCNT_SEC_QUES_ID,ACCNT_ID,ACCNT_SEC_ANSWER) VALUES('" + pSecQuest["ACCNT_SEC_QUES_ID"].ToString() + "','" + strAccountID.Trim() + "','" + strAns1 + "')"; // Stored Procedure to Call
                        OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                        OracleCmd.ExecuteNonQuery();
                    }
                    //------------------------------------------------------------
                    //}
                    OracleCmd.CommandText = "INSERT INTO ACCOUNT_LIST_ONLY_NEW(ACCNT_ID) VALUES('" + strAccountID.Trim() + "')"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    intCount++;


                }
                //---------------------------------------------------------------
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE BULK_ACCOUNT_CREATION SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',BULK_ACCNT_CRE_STATUS='E',"
                                     + "BULK_ACCNT_RESULT='" + intCount + " new accouunts have been created.' WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //---------------------------------------------------
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
                dbTransaction = conn.BeginTransaction();
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE BULK_ACCOUNT_CREATION SET EXCUTE_SYS_USR_ID='" + strUserID + "',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                     + "EXECUTE_SYSTEM_NAME='" + strMachineName + "',BULK_ACCNT_CRE_STATUS='E',"
                                     + "BULK_ACCNT_RESULT='" + strMsg + "' WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' "; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                dbTransaction.Commit();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }
    public string ResetPIN(string strAccntID, string strMSISDN, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strPIN1 = "";
        string strPIN2 = "";
        string strAns1 = "";
        string strSQL = "";
        //int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //DataSet oDS;
        //OracleDataAdapter oOrdersDataAdapter;
        //DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        //-------------------------------------------
        strSQL = "SELECT ACCNT_SEC_QUES_ID,ACCNT_SEC_QUES_DESC,ACCNT_SEC_QUES_SLNO FROM ACCOUNT_SEC_QUESTION";
        DataSet dtsSecQuestion = new DataSet();
        OracleDataAdapter adpSecQuestion = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbSecQuestion = new OracleCommandBuilder(adpSecQuestion);
        adpSecQuestion.Fill(dtsSecQuestion, "ACCOUNT_SEC_QUESTION");
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {

            //-------------------------------------------------------------            
            try
            {
                //-------------------- Get new Clinet ID--------------------------------- 
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                //---------------------------------------------------------------------------------
                strPIN1 = String.Format("{0:0000}", RandNum.Next(1111, 9999).ToString());
                strPIN2 = String.Format("{0:0000}", RandNum.Next(1111, 9999).ToString());
                //---------------------------------------------------------------------
                OracleCmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_PIN='" + strPIN1 + "',ACCNT_PIN2='" + strPIN2 + "' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //-----------------------------------------------------------------------
                OracleCmd.CommandText = "DELETE FROM ACCOUNT_SEC_ANSWER WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //----------------------------------------------------------
                //strMsg = "Your New PIN and Answer are as follows";
                strMsg = "Your New PIN is as follows";
                strMsg = strMsg + "\nPIN=" + strPIN1;
                //strMsg = strMsg + "\nPIN2=" + strPIN2;
                //----------------------------------------------------------
                foreach (DataRow pSecQuest in dtsSecQuestion.Tables["ACCOUNT_SEC_QUESTION"].Rows)
                {
                    strAns1 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    OracleCmd.CommandText = "INSERT INTO ACCOUNT_SEC_ANSWER(ACCNT_SEC_QUES_ID,ACCNT_ID,ACCNT_SEC_ANSWER) VALUES('" + pSecQuest["ACCNT_SEC_QUES_ID"].ToString() + "','" + strAccntID.Trim() + "','" + strAns1 + "')"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    //strMsg = strMsg + "\nQ. " + pSecQuest["ACCNT_SEC_QUES_DESC"].ToString() + ". A. " + strAns1;
                }
                //--------------------------------------------------------------- 
                strMsg = ForwardMessage(strMSISDN, "1234", strMsg, strAccntID, "BDC", "");
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }
    public string ResetPIN(string strAccntID, string strMSISDN, string strUserID, string strMachineIP, string strMachineName, string strPIN1)
    {
        string strMsg = "";
        string strPIN2 = "";
        string strAns1 = "";
        string strSQL = "";
        //int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //DataSet oDS;
        //OracleDataAdapter oOrdersDataAdapter;
        //DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        //-------------------------------------------
        strSQL = "SELECT ACCNT_SEC_QUES_ID,ACCNT_SEC_QUES_DESC,ACCNT_SEC_QUES_SLNO FROM ACCOUNT_SEC_QUESTION";
        DataSet dtsSecQuestion = new DataSet();
        OracleDataAdapter adpSecQuestion = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbSecQuestion = new OracleCommandBuilder(adpSecQuestion);
        adpSecQuestion.Fill(dtsSecQuestion, "ACCOUNT_SEC_QUESTION");
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {

            //-------------------------------------------------------------            
            try
            {
                //-------------------- Get new Clinet ID--------------------------------- 
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                //---------------------------------------------------------------------------------                
                strPIN2 = String.Format("{0:0000}", RandNum.Next(1111, 9999).ToString());
                //---------------------------------------------------------------------
                //OracleCmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_PIN='" + strPIN1 + "',ACCNT_PIN2='" + strPIN2 + "' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_PIN='" + strPIN1 + "' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //-----------------------------------------------------------------------
                OracleCmd.CommandText = "DELETE FROM ACCOUNT_SEC_ANSWER WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //----------------------------------------------------------
                //strMsg = "Your New PIN and Answer are as follows";
                strMsg = "Your New PIN is as follows";
                strMsg = strMsg + "\n " + strPIN1 + ". Please remember this PIN & Delete this SMS. Thank you. MYCash";


                //strMsg = strMsg + "\nPIN2=" + strPIN2;
                //----------------------------------------------------------
                foreach (DataRow pSecQuest in dtsSecQuestion.Tables["ACCOUNT_SEC_QUESTION"].Rows)
                {
                    strAns1 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    OracleCmd.CommandText = "INSERT INTO ACCOUNT_SEC_ANSWER(ACCNT_SEC_QUES_ID,ACCNT_ID,ACCNT_SEC_ANSWER) VALUES('" + pSecQuest["ACCNT_SEC_QUES_ID"].ToString() + "','" + strAccntID.Trim() + "','" + strAns1 + "')"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    //strMsg = strMsg + "\nQ. " + pSecQuest["ACCNT_SEC_QUES_DESC"].ToString() + ". A. " + strAns1;
                }
                //--------------------------------------------------------------- 
                strMsg = ForwardMessage(strMSISDN, "1234", strMsg, strAccntID, "BDC", "");
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }


    public string KYCVerifyed(string strAccntID, string strToken, string strMSISDN, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strPIN1 = "";
        string strPIN2 = "";
        string strAns1 = "";
        //string strToken = "";
        string strSQL = "";
        //int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //DataSet oDS;
        //OracleDataAdapter oOrdersDataAdapter;
        //DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        //-------------------------------------------
        //#######################################################
        //strSQL = "SELECT ACCNT_SEC_QUES_ID,ACCNT_SEC_QUES_DESC,ACCNT_SEC_QUES_SLNO FROM ACCOUNT_SEC_QUESTION";
        //DataSet dtsSecQuestion = new DataSet();
        //OracleDataAdapter adpSecQuestion = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        //OracleCommandBuilder cmbSecQuestion = new OracleCommandBuilder(adpSecQuestion);
        //adpSecQuestion.Fill(dtsSecQuestion, "ACCOUNT_SEC_QUESTION");
        //#########################################################
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {

            //-------------------------------------------------------------            
            try
            {
                //-------------------- Get new Clinet ID--------------------------------- 
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                //---------------------------------------------------------------------------------
                strPIN1 = String.Format("{0:0000}", RandNum.Next(1111, 9999).ToString());
                strPIN2 = String.Format("{0:0000}", RandNum.Next(1111, 9999).ToString());
                //---------------------------------------------------------------------
                OracleCmd.CommandText = "UPDATE ACCOUNT_LIST_ONLY_NEW SET ACCNT_UPLOAD_STATE = 'V' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //-----------------------------------------------------------------------
                //OracleCmd.CommandText = "DELETE FROM ACCOUNT_SEC_ANSWER WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                //OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                //OracleCmd.ExecuteNonQuery();
                //----------------------------------------------------------
                //---------------------------------------------------------------------
                //OracleCmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_STATE = 'A',ACCNT_RANK_ID='120519000000000006' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                //OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                //OracleCmd.ExecuteNonQuery();

                //strMsg = "Your New PIN and Answer are as follows";
                //strMsg = "Your account has been verifyed. To activate your account please reply \"AC " + strToken + "\". Thank You MPAY.";

                //strMsg = "Your account has been verifyed. To activate your account please reply \"AC\". Thank You MPAY.";

                //Your MYCash account has been activated on 22-Apr-14 5:30 PM.MYCash
                strMsg = "Your MYCash account has been activated on " + String.Format("{0:dd-MMM-yy h:mm tt}", DateTime.Now) + " . MYCash";


                //strMsg = strMsg + "\nPIN2=" + strPIN2;
                //#####################################################################
                //----------------------------------------------------------
                //foreach (DataRow pSecQuest in dtsSecQuestion.Tables["ACCOUNT_SEC_QUESTION"].Rows)
                //{
                //    strAns1 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                //    OracleCmd.CommandText = "INSERT INTO ACCOUNT_SEC_ANSWER(ACCNT_SEC_QUES_ID,ACCNT_ID,ACCNT_SEC_ANSWER) VALUES('" + pSecQuest["ACCNT_SEC_QUES_ID"].ToString() + "','" + strAccntID.Trim() + "','" + strAns1 + "')"; // Stored Procedure to Call
                //    OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                //    OracleCmd.ExecuteNonQuery();
                //    //strMsg = strMsg + "\nQ. " + pSecQuest["ACCNT_SEC_QUES_DESC"].ToString() + ". A. " + strAns1;
                //}
                //--------------------------------------------------------------- 
                //###################################################################
                strMsg = ForwardMessage(strMSISDN, "16225", strMsg, strAccntID, "BDC", "");
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

    }
    //------------------- Update Client List Verification Information ------------------------------------
    public string UpdateClientListForVerification(string strVerifiedBy, string strUpdateTime, string strMobileNo)
    {
        OracleConnection conn;
        OracleCommand OLEDBCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OLEDBCmd = new OracleCommand();
            OLEDBCmd.Connection = conn;//Active Connection
            OLEDBCmd.Transaction = dbTransaction;

            OLEDBCmd.CommandText = " UPDATE CLIENT_LIST SET VERIFIED_BY='" + strVerifiedBy + "', VERIFIED_DATE=TO_DATE('" + strUpdateTime + "','dd/mm/yyyy HH24:MI:SS') WHERE CLINT_MOBILE='" + strMobileNo + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
    }

    public string ShowAccountActivationMSISDN(string strMSISDN)
    {
        string strAccMSISDNReturn = "";
        string strSql = " SELECT ACCNT_MSISDN FROM ACCOUNT_LIST WHERE ACCNT_NO='" + strMSISDN + "'";
        try
        {
            // OracleConnection
            conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strAccMSISDNReturn = dr["ACCNT_MSISDN"].ToString();
                }
                return strAccMSISDNReturn;
                conn.Close();
            }
            else
            {
                return strAccMSISDNReturn;
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            strAccMSISDNReturn = ex.Message.ToString();
            return strAccMSISDNReturn;
        }

    }

    public string GenSLStatusUpdate(string strValue)
    {
        string updateString;

        updateString = @" update ACCOUNT_SERIAL_MASTER set STATUS='A' where ACCNT_SL_MSTR_ID='" + strValue + "'";

        OracleConnection conn = new OracleConnection(strConString);

        string strReturn = "";
        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(updateString);
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


    public DataSet CheckStatus(string strValue)
    {
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        // Create the DataTable "Orders" in the Dataset and the OrdersDataAdapter
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT STATUS FROM ACCOUNT_SERIAL_MASTER where ACCNT_SL_MSTR_ID='" + strValue + "'", conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.Fill(oDS, "ACCOUNT_SERIAL_MASTER");
        return oDS;
    }

    public string GenSLNo(string strValue, Int64 intSLNo)
    {
        string strSql = "";
        strSql = "SELECT ACCNT_SL_MSTR_ID,SERIAL_NO,STATUS FROM ACCOUNT_SERIAL_DETAIL";
        try
        {
            DataRow oOrderRow;
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "ACCOUNT_SERIAL_DETAIL");
            oOrderRow = oDS.Tables["ACCOUNT_SERIAL_DETAIL"].NewRow();


            oOrderRow["ACCNT_SL_MSTR_ID"] = strValue;
            oOrderRow["SERIAL_NO"] = intSLNo;
            oOrderRow["STATUS"] = "A";
            oDS.Tables["ACCOUNT_SERIAL_DETAIL"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "ACCOUNT_SERIAL_DETAIL");
            return "Saved successfully";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

    //public string GenSLNo(Int64 intSLNo)
    //{
    //    string strSql = "";
    //    strSql = "SELECT SERIAL_NO FROM ACCNT_SERIAL_NO";
    //    try
    //    {
    //        DataRow oOrderRow;
    //        OracleConnection conn = new OracleConnection(strConString);
    //        DataSet oDS = new DataSet();
    //        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
    //        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
    //        oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
    //        oOrdersDataAdapter.Fill(oDS, "ACCNT_SERIAL_NO");
    //        oOrderRow = oDS.Tables["ACCNT_SERIAL_NO"].NewRow();

    //        //oOrderRow["CHQ_REG_ID"] = strChqRegID;
    //        oOrderRow["SERIAL_NO"] = intSLNo;
    //        oDS.Tables["ACCNT_SERIAL_NO"].Rows.Add(oOrderRow);
    //        oOrdersDataAdapter.Update(oDS, "ACCNT_SERIAL_NO");
    //        return "Saved successfully";
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message.ToString();
    //    }
    //}

    public DataSet ReadServiceRequest()
    {
        string strMsg;
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT * FROM SERVICE_REQUEST WHERE REQUEST_STAE='P'", conn));
            oOrdersDataAdapter.Fill(oDS, "SERVICE_REQUEST");
            return oDS;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return null;
        }

    }
    public DataSet ReadResponceRequest()
    {
        //OracleConnection conn = new OracleConnection("Provider=Microsoft.Jet.Oracle.4.0;Data Source=APSNG101DB.mdb");
        string strMsg;
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT * FROM SERVICE_REQUEST WHERE REQUEST_STAE='P'", conn));
            oOrdersDataAdapter.Fill(oDS, "SERVICE_REQUEST");
            return oDS;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return null;
        }
        //return null;

    }
    public void WriteCDR(string strCDR_ID, string strReference, string strCDR_Type, string strFrom, string strTom, string strTime, string strServiceCode, string strMessage)
    {
        string strTemCDR_Path;
        string strData;
        string strDelimiter;
        strDelimiter = ";";

        strData = strCDR_ID + strDelimiter + strReference + strDelimiter + strCDR_Type + strDelimiter + strFrom + strDelimiter + strTom + strDelimiter + strTime + strDelimiter + strServiceCode + strDelimiter + strMessage + strDelimiter;
        strTemCDR_Path = strCDR_Destination + "\\test_cdr.txt";
        //FileInfo fiCDR = new FileInfo(strTemCDR_Path);
        //FileStream fsCDR = fiCDR.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //StreamWriter w = fiCDR.CreateText();

        //StreamWriter w = File.AppendText(strTemCDR_Path);
        //w.WriteLine(strData);
        //w.Close();
    }
    public DataSet LoadPromotionList()
    {
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT * FROM PROMOTION_LIST ", conn));
        oOrdersDataAdapter.Fill(oDS, "PROMOTION_LIST");
        return oDS;
    }
    public Boolean CheckService(string strService)
    {
        string strServiceList;
        strServiceList = "MGOGO";
        if (strServiceList.IndexOf(strService) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public string GetTransactionID()
    {
        string strTemID;
        strTemID = DateTime.Now.ToShortDateString() + int64CountRequest;
        int64CountRequest = int64CountRequest + 1;
        return strTemID;
    }
    public DataSet GetAccPIN_Detail(string strMSISDN)
    {
        string strMsg;
        string strSql;
        strSql = "SELECT AL.ACCNT_PIN,AL.ACCNT_PIN2,QS.ACCNT_SEC_QUES_SLNO,QS.ACCNT_SEC_QUES_DESC,ANS.ACCNT_SEC_ANSWER FROM ACCOUNT_LIST AL,ACCOUNT_SEC_ANSWER ANS,"
             + "ACCOUNT_SEC_QUESTION QS WHERE AL.ACCNT_MSISDN='" + strMSISDN + "' AND ANS.ACCNT_ID=AL.ACCNT_ID AND ANS.ACCNT_SEC_QUES_ID=QS.ACCNT_SEC_QUES_ID";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "ACCOUNT_LIST");
            return oDS;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return null;
        }

    }
    public DataSet GetAccountDetail(string strAccountNo)
    {
        string strMsg;
        string strSql;
        strSql = "SELECT * from ACCOUNT_LIST WHERE ACCNT_NO='" + strAccountNo + "'";
        //strSql = "SELECT * FROM ACCOUNT_LIST_ONLY_NEW AN, ACCOUNT_LIST AL WHERE AN.ACCNT_ID=AL.ACCNT_ID AND AL.ACCNT_MSISDN='" + strAccountNo + "'";

        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "ACCOUNT_LIST");
            return oDS;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return null;
        }

    }

    public DataSet GetAccountDetailKYC(string strAccountNo)
    {
        string strMsg;
        string strSql;
        //strSql = "SELECT * from ACCOUNT_LIST WHERE ACCNT_NO='" + strAccountNo + "'";
        strSql = "SELECT * FROM ACCOUNT_LIST_ONLY_NEW AN, ACCOUNT_LIST AL WHERE AN.ACCNT_ID=AL.ACCNT_ID AND AL.ACCNT_MSISDN='" + strAccountNo + "'";

        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "ACCOUNT_LIST_ONLY_NEW");
            return oDS;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return null;
        }

    }
    //THIS METHOD IS ADDED BY KOWSHIK(05-SEP-2012)
    public string ShowAccountVerificationInformation(string strWalletID)
    {
        string strSql = " SELECT ACCNT_NO FROM ACCOUNT_LIST WHERE ACCNT_NO='" + strWalletID + "'";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                return "";
            }
            else
            {
                return "Wallet ID is Not Existe.";

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();

        }
    }
    //THIS METHOD IS ADDED BY KOWSHIK(05-SEP-2012)
    public string ShowAccountVerificationState(string strWalletID)
    {
        string strAccMSISDNReturn = "";
        string strSql = " SELECT ACCNT_STATE FROM ACCOUNT_LIST WHERE ACCNT_NO='" + strWalletID + "'";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strAccMSISDNReturn = dr["ACCNT_STATE"].ToString();
                }
                return strAccMSISDNReturn;
            }
            else
            {
                return strAccMSISDNReturn;

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }

    public string UpdateAccountState(string strMSISDN, string strState)
    {
        string updateString;
        updateString = @"UPDATE ACCOUNT_LIST SET ACCNT_STATE='" + strState + "' WHERE ACCNT_MSISDN='" + strMSISDN + "'";

        string strReturn = "";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(updateString);
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
    public string GetDisburseID()
    {
        string strOutput = "";
        string strQuery = "";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();

            strQuery = "SELECT TRIM(pkg_apsng_primary_key.get_new_srv_reward_id(0)) DISBURSE_ID FROM DUAL";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strQuery, conn));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "DUAL");

            if (oDS.Tables["DUAL"].Rows.Count > 0)
            {
                DataRow myDataRow1 = oDS.Tables["DUAL"].Rows[0];
                strOutput = myDataRow1["DISBURSE_ID"].ToString();

            }
            oOrdersDataAdapter.Dispose();
            //#######################################
        }
        catch (Exception ex)
        {
            strOutput = "Err:" + ex.Message.ToString();
        }
        return strOutput;
    }
    public string DisburseCommission(string strFromDate, string strToDate, ArrayList arrRequestID, string strDisburseNoteNo)
    {
        string strMsg = "";
        string strDisburseID = "";
        //int intCount = 0;
        string strSQL = "";
        //------------------------------------------
        strDisburseID = GetDisburseID();
        //------------------------------------------
        OracleConnection conn;
        //OracleDataAdapter oOrdersDataAdapter;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();

        try
        {
            //------------------------------------------
            strSQL = "INSERT INTO SERVICE_REWARD_DISBURSE(REWARD_DISB_TITLE,REWARD_DISB_FDATE,REWARD_DISB_TDATE) "
                   + "VALUES('" + strDisburseNoteNo + "','" + strFromDate + "','" + strToDate + "')";
            //---------------------------------------------------------------
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn; //Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandText = strSQL;
            OracleCmd.CommandType = CommandType.Text; //Setup Command Type
            OracleCmd.ExecuteNonQuery();
            //---------------------------------------------------
            foreach (string strReqID in arrRequestID)
            {
                //---------------------------------------------------------------
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                OracleCmd.CommandText = "UPDATE SERVICE_REQUEST SET SRV_REWARD_DISB_ID='" + strDisburseID + "' WHERE REQUEST_ID='" + strReqID + "'"; // Stored Procedure to Call
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //---------------------------------------------------
            }
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strMsg = ex.Message.ToString();
        }
        conn.Close();
        return strMsg;
    }
    public string BroadCastCommission(string strMasterID, string strChannelType, string strAmtLimit)
    {
        string strMsg;
        double dblCount = 0;
        int intCount = 0;
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            string strSql;

            if (strChannelType.Equals("ALL"))
            {
                strSql = "SELECT AL.ACCNT_MSISDN,AL.ACCNT_NO,CM.COMI_END_DATE "
                              + "FROM COMMISSION_MASTER CM,VW_COMMISION_DATA CD,ACCOUNT_LIST AL,CLIENT_LIST CL "
                              + "WHERE CM.COMI_MASTER_ID='" + strMasterID + "' AND CM.COMI_MASTER_ID=CD.COMI_MASTER_ID "
                              + "AND CL.CLINT_ID=AL.CLINT_ID AND AL.ACCNT_NO=CD.C_CODE "
                              + "AND CD.COM_AMOUNT<" + strAmtLimit;
            }
            else
            {
                strSql = "SELECT AL.ACCNT_MSISDN,AL.ACCNT_NO,CM.COMI_END_DATE "
                              + "FROM COMMISSION_MASTER CM,VW_COMMISION_DATA CD,ACCOUNT_LIST AL,CLIENT_LIST CL "
                              + "WHERE CM.COMI_MASTER_ID='" + strMasterID + "' AND CM.COMI_MASTER_ID=CD.COMI_MASTER_ID "
                              + "AND CL.CLINT_ID=AL.CLINT_ID AND AL.ACCNT_NO=CD.C_CODE "
                              + "AND CD.CHANNEL_TYPE='" + strChannelType + "' AND CD.COM_AMOUNT<" + strAmtLimit;
            }
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "SERVICE_REQUEST");

            if (oDS.Tables["SERVICE_REQUEST"].Rows.Count > 0)
            {
                foreach (DataRow prow in oDS.Tables["SERVICE_REQUEST"].Rows)
                {
                    string strRetailer = prow["ACCNT_MSISDN"].ToString();
                    string strMonth = String.Format("{0:MMyy}", DateTime.Parse(prow["COMI_END_DATE"].ToString()));
                    strMsg = "*COMB*" + prow["ACCNT_NO"].ToString() + "*" + strMasterID + "#";
                    AddServiceRequest(strRetailer, "8360", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), strMsg);
                    dblCount++;
                    intCount++;
                    if (intCount > 500)
                    {
                        System.Threading.Thread.Sleep(60000);
                        intCount = 0;
                    }
                }

            }
            return dblCount.ToString() + " messages have been broadcasted.";
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }

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

    #region PhotoSignature ADDED BY KOWSHIK(10-SEP-2012)

    public DataSet GetPhotoSignature(string strClientID)
    {
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT * FROM CR_CLIENTPHOTO_SIG WHERE CLIENT_ID='" + strClientID + "'", conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.Fill(oDS, "CR_CLIENTPHOTO_SIG");
        return oDS;
    }




    public DataSet GetPhotoSignatureCompany(string strClientID)
    {
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT * FROM CR_CLIENTPHOTO_SIG WHERE CS_STRUCT_ID='" + strClientID + "'", conn));
        OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
        oOrdersDataAdapter.Fill(oDS, "CR_CLIENTPHOTO_SIG");
        return oDS;
    }


    public string InsertPhotoSignature(string clientID, string clientPicName, string signatureName, string strKycName, string strStructureID)
    {
        try
        {
            string strSql = "SELECT  CLIENT_ID, CLIENT_PIC,SIGNATURE,CS_STRUCT_ID, KYC_FORM_PIC FROM CR_CLIENTPHOTO_SIG ";
            DataRow oOrderRow;
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oDbAdapter.Fill(oDs, "CR_CLIENTPHOTO_SIG");
            oOrderRow = oDs.Tables["CR_CLIENTPHOTO_SIG"].NewRow();
            oOrderRow["CLIENT_ID"] = clientID;
            oOrderRow["CLIENT_PIC"] = clientPicName;
            oOrderRow["SIGNATURE"] = signatureName;
            oOrderRow["KYC_FORM_PIC"] = strKycName;
            oOrderRow["CS_STRUCT_ID"] = strStructureID;
            oDs.Tables["CR_CLIENTPHOTO_SIG"].Rows.Add(oOrderRow);
            oDbAdapter.Update(oDs, "CR_CLIENTPHOTO_SIG");
        }
        catch (Exception ex)
        {
            return null;
        }
        return "Success";
    }


    public string UpdatePhotoSignature(string clientID, string clientPicName, string signatureName, string strKycFormName)
    {
        try
        {
            string strSql = "";
            strSql = "SELECT  PHOTOSIG_ID,CLIENT_ID, CLIENT_PIC,SIGNATURE, KYC_FORM_PIC FROM CR_CLIENTPHOTO_SIG  where CLIENT_ID='" + clientID + "' ";
            DataRow oOrderRow;
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDs = new DataSet();
            OracleDataAdapter oDbAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            OracleCommandBuilder oDbCmdBuilder = new OracleCommandBuilder(oDbAdapter);
            oDbAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oDbAdapter.Fill(oDs, "CR_CLIENTPHOTO_SIG");
            if (!clientID.Equals(""))
            {
                string strPhotoID = oDs.Tables[0].Rows[0]["PHOTOSIG_ID"].ToString();
                oOrderRow = oDs.Tables["CR_CLIENTPHOTO_SIG"].Rows.Find(strPhotoID);
                oOrderRow["CLIENT_PIC"] = clientPicName;
                oOrderRow["SIGNATURE"] = signatureName;
                oOrderRow["KYC_FORM_PIC"] = strKycFormName;
            }
            oDbAdapter.Update(oDs, "CR_CLIENTPHOTO_SIG");
            return "Update SuccessFully";
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }

    }
    
    
    
    
    
    #endregion
    #region Account Hierarchy Added by Kowshik(11-Sep-2012)
    public string ReturnGrade(string strAccNo)
    {
        string strGradeValue = "", strRemarks = "";
        int intCount;
        try
        {
            string strSql = "SELECT (AR.GRADE-1) GRADE,AR.REMARKS FROM ACCOUNT_LIST AL,ACCOUNT_RANK AR WHERE AR.ACCNT_RANK_ID=AL.ACCNT_RANK_ID AND AL.ACCNT_NO='" + strAccNo + "'";
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strGradeValue = dr["GRADE"].ToString();
                    strRemarks = dr["REMARKS"].ToString();
                }
                return strGradeValue + "," + strRemarks;
            }
            else
            {
                return "";
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }


    }
    public bool ReturnHierarchyCountValue(string strAccRankID)
    {
        bool blnHasRow = false;
        string strSql = "SELECT ACCNT_RANK_ID FROM  ACCOUNT_LIST WHERE ACCNT_RANK_ID='" + strAccRankID + "'";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                blnHasRow = true;
                return blnHasRow;
            }
            else
            {
                return blnHasRow;
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return blnHasRow;
        }
    }
    public bool ReturnIntValue(string strgrade, string strRemarks)
    {
        bool blnHirCunt;
        string strAccRankID = ReturnAccountRankID(strgrade, strRemarks);
        blnHirCunt = ReturnHierarchyCountValue(strAccRankID);

        return blnHirCunt;
    }
    public string ReturnAccountRankID(string strGrade, string strRemarks)
    {
        string strSql = "SELECT ACCNT_RANK_ID FROM ACCOUNT_RANK WHERE GRADE ='" + strGrade + "' AND REMARKS='" + strRemarks + "' AND ACCNT_RANK_ID NOT IN(120618000000000001,120618000000000002,120618000000000003,120618000000000004)";
        string strReturnColumnValue = "";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["ACCNT_RANK_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }
    public string ReturnOneColumnValueByTwoColumnValue(string strTableName, string strInquiryColumnName, string strColumnOne, string strColumnValueOne, string strColumnNameTwo, string strColumnValueTwo)
    {
        string strSql = "SELECT " + strInquiryColumnName + " FROM " + strTableName + " WHERE " + strColumnOne + " ='" + strColumnValueOne + "' AND " + strColumnNameTwo + "='" + strColumnValueTwo + "'";
        string strReturnColumnValue = "";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["" + strInquiryColumnName + ""].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }
    //Author :Kowshik
    //Date :25-May-2014

    public string ReturnOneColumnValue(string strMSISDN)
    {
        string strSql = " SELECT distinct REQUEST_ID FROM ACCOUNT_SERIAL_DETAIL WHERE CUSTOMER_MOBILE_NO ='" + strMSISDN + "'";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["REQUEST_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }            
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();          
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    //Author :Kowshik
    //Date :25-May-2014
    public string ReturnOneColumnValueByAnotherColumnValue(string strTableName, string strColumnOne, string strColumnNameTwo, string strColumnValueTwo)
    {
        string strSql = "SELECT " + strColumnOne + " FROM " + strTableName + " WHERE " + strColumnNameTwo + " ='" + strColumnValueTwo + "'";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["" + strColumnOne + ""].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public string UpdateHierarchyInfo(string strAccID, string strHirchyID, string strUpdatedBy)
    {
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        string strSql = "";
        strSql = @"UPDATE ACCOUNT_HIERARCHY SET HIERARCHY_ACCNT_ID='" + strHirchyID + "', UPDATED_BY ='" + strUpdatedBy + "' WHERE ACCNT_ID='" + strAccID + "'";

        try
        {
            OracleCommand cmd = new OracleCommand(strSql);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            return "Information Successfully Updated.";
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }
    }
	
	public string UpdateHierarchyInfo(string strAccID, string strHirchyID, string strUpdatedBy, string strParentCommission)
    {
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        string strSql = "";
        strSql = @"UPDATE ACCOUNT_HIERARCHY SET HIERARCHY_ACCNT_ID='" + strHirchyID + "', UPDATED_BY ='" + strUpdatedBy + "', MERCHANT_COMMISSION = '" + strParentCommission + "' WHERE ACCNT_ID='" + strAccID + "'";

        try
        {
            OracleCommand cmd = new OracleCommand(strSql);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            return "Information Successfully Updated.";
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }
    }
	
    public string SaveHierarchyInfo(string strAccID, string strHirchyID, string strUpdatedBy)
    {
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        string strSql = "";
        strSql = @"INSERT INTO ACCOUNT_HIERARCHY(ACCNT_ID,HIERARCHY_ACCNT_ID,UPDATED_BY)"
                + " VALUES('" + strAccID.Trim() + "','" + strHirchyID + "','" + strUpdatedBy + "')";
        try
        {
            OracleCommand cmd = new OracleCommand(strSql);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            return "Information Successfully Saved.";
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }
    }
	
	public string SaveHierarchyInfo(string strAccID, string strHirchyID, string strUpdatedBy, string strParentCommission)
    {
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        string strSql = "";
        strSql = @"INSERT INTO ACCOUNT_HIERARCHY(ACCNT_ID,HIERARCHY_ACCNT_ID,UPDATED_BY,MERCHANT_COMMISSION)"
                + " VALUES('" + strAccID.Trim() + "','" + strHirchyID + "','" + strUpdatedBy + "','" + strParentCommission + "')";
        try
        {
            OracleCommand cmd = new OracleCommand(strSql);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            return "Information Successfully Saved.";
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }
    }
    #endregion
    #region KYC UPDATE  related method(kowshik)

    public bool SaveBankAccount(string strClientID, string strBnkName, string strBnkBranch, string strAccNo, string strRemarks)
    {
        bool blnBank = false;
        string strSql = "";
        DataRow oOrderRow;
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleTransaction dbTransaction;
        conn.Open();
        dbTransaction = conn.BeginTransaction();

        strSql = "SELECT CLIENT_ID,BANK_NAME,BANK_BR_NAME,BANK_ACCNT_NO,REMARKS FROM BANK_ACCOUNT";
        try
        {
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);

            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "BANK_ACCOUNT";
            oOrderRow = oDS.Tables["BANK_ACCOUNT"].NewRow();

            oOrderRow["CLIENT_ID"] = strClientID;
            oOrderRow["BANK_NAME"] = strBnkName;
            oOrderRow["BANK_BR_NAME"] = strBnkBranch;
            oOrderRow["BANK_ACCNT_NO"] = strAccNo;
            oOrderRow["REMARKS"] = strRemarks;

            oDS.Tables["BANK_ACCOUNT"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "BANK_ACCOUNT");

            dbTransaction.Commit();
            blnBank = true;
            return blnBank;
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return blnBank;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public bool SaveClientIdentification(string strClientID, string strIdenName, string strIdenID, string strRemarks)
    {
        bool blnIden = false;
        string strSql = "";
        DataRow oOrderRow;
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleTransaction dbTransaction;
        conn.Open();
        dbTransaction = conn.BeginTransaction();

        strSql = "SELECT CLIENT_ID,CLINT_IDENT_NAME,IDNTIFCTION_ID,REMARKS FROM CLIENT_IDENTIFICATION";
        try
        {
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);

            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "CLIENT_IDENTIFICATION";
            oOrderRow = oDS.Tables["CLIENT_IDENTIFICATION"].NewRow();

            oOrderRow["CLIENT_ID"] = strClientID;
            oOrderRow["CLINT_IDENT_NAME"] = strIdenName;
            oOrderRow["IDNTIFCTION_ID"] = strIdenID;
            oOrderRow["REMARKS"] = strRemarks;

            oDS.Tables["CLIENT_IDENTIFICATION"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "CLIENT_IDENTIFICATION");

            dbTransaction.Commit();
            blnIden = true;
            return blnIden;
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return blnIden;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public bool SaveIntroducerInfo(string strClientID, string strIntroName, string strIntroMobile, string strIntroAdds, string strIntroOccu, string strIntroRemarks)
    {
        bool blnIntro = false;
        string strSql = "";
        DataRow oOrderRow;
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleTransaction dbTransaction;
        conn.Open();
        dbTransaction = conn.BeginTransaction();

        strSql = "SELECT CLIENT_ID,INTRODCR_NAME,INTRODCR_MOBILE,INTRODCR_ADDRESS,INTRODCR_OCCUPATION,REMARKS FROM INTRODUCER_INFO";
        try
        {
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);

            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "INTRODUCER_INFO";
            oOrderRow = oDS.Tables["INTRODUCER_INFO"].NewRow();

            oOrderRow["CLIENT_ID"] = strClientID;
            oOrderRow["INTRODCR_NAME"] = strIntroName;
            oOrderRow["INTRODCR_MOBILE"] = strIntroMobile;
            oOrderRow["INTRODCR_ADDRESS"] = strIntroAdds;
            oOrderRow["INTRODCR_OCCUPATION"] = strIntroOccu;
            oOrderRow["REMARKS"] = strIntroRemarks;
            oDS.Tables["INTRODUCER_INFO"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "INTRODUCER_INFO");

            dbTransaction.Commit();
            blnIntro = true;
            return blnIntro;
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return blnIntro;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public bool SaveNomineeInfo(string strClientID, string strNomName, string strNomMobile, string strRelation, decimal dcmPercent, string strRemarks)
    {
        bool blnNomnee = false;
        string strSql = "";
        DataRow oOrderRow;
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        OracleTransaction dbTransaction;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        strSql = "SELECT CLIENT_ID,NOMNE_NAME,NOMNE_MOBILE,RELATION,PERCENTAGE,REMARKS FROM NOMINEE_INFO";
        try
        {
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);

            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "NOMINEE_INFO";
            oOrderRow = oDS.Tables["NOMINEE_INFO"].NewRow();

            oOrderRow["CLIENT_ID"] = strClientID;
            oOrderRow["NOMNE_NAME"] = strNomName;
            oOrderRow["NOMNE_MOBILE"] = strNomMobile;
            oOrderRow["RELATION"] = strRelation;
            oOrderRow["PERCENTAGE"] = dcmPercent;
            oOrderRow["REMARKS"] = strRemarks;

            oDS.Tables["NOMINEE_INFO"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "NOMINEE_INFO");

            dbTransaction.Commit();
            blnNomnee = true;
            return blnNomnee;
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return blnNomnee;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public bool UpdateClientList(string strClientName, string strFathersName, string strMothersName, string strDOB,
                                string strOccupation, string strWEB, string strPurOfTran, string strUISCAgent,
                                string strOfcAdss, string strPreAdss, string strPerAdss, string strThana, string strClientID,
                                string strUpdatedBy, string strUpdateDate, string strReqPartyType, string strUpdateMessage, string strHusbandName, string strIncompleteKYC)
    {

        string strSQL = "";
        bool blnClientUpdate = false;
        OracleConnection conn;
        OracleCommand OLEDBCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        OLEDBCmd = new OracleCommand();
        OLEDBCmd.Connection = conn;
        OLEDBCmd.Transaction = dbTransaction;

        if (strUpdateMessage == "")
        {
            strSQL = " UPDATE CLIENT_LIST SET CLINT_NAME = '" + strClientName + "', "
                                                                   + " CLINT_ADDRESS1 = '" + strPreAdss + "', "
                                                                   + " CLINT_ADDRESS2 = '" + strPerAdss + "', "
                                                                   + " CLINT_FATHER_NAME = '" + strFathersName + "', "
                                                                   + " CLINT_MOTHER_NAME = '" + strMothersName + "', "
                                                                    + " CLIENT_DOB = TO_DATE('"+ strDOB +"', 'dd/mm/yyyy') , "
                                                                   + " CLIENT_OFFIC_ADDRESS = '" + strOfcAdss + "', "
                                                                   + " OCCUPATION = '" + strOccupation + "', "
                                                                   + " WORK_EDU_BUSINESS='" + strWEB + "', "
                                                                   + " PUR_OF_TRAN = '" + strPurOfTran + "', "
                                                                   + " UISC_AGENT = '" + strUISCAgent + "', "
                                                                   + " THANA_ID='" + strThana + "', "
                                                                   + " KYC_UPDATED_BY ='" + strUpdatedBy + "', "
                                                                   + " UPDATE_DATE=TO_DATE('" + strUpdateDate + "','dd/mm/yyyy HH24:MI:SS'),"
                                                                   + " HUSBAND_NAME='" + strHusbandName + "', "
                                                                   + " REQUEST_PARTY_TYPE='" + strReqPartyType + "', "
                                                                   + " INCOMPLETE_KYC='" + strIncompleteKYC + "',"
                                                                   + " VERIFIED_BY = '" + strUpdatedBy + "'"
                                                                   + " WHERE (CLINT_ID = '" + strClientID + "') ";
        }
        else
        {
            strSQL = " UPDATE CLIENT_LIST SET CLINT_NAME = '" + strClientName + "', "
                                                          + " CLINT_ADDRESS1 = '" + strPreAdss + "', "
                                                          + " CLINT_ADDRESS2 = '" + strPerAdss + "', "
                                                          + " CLINT_FATHER_NAME = '" + strFathersName + "', "
                                                          + " CLINT_MOTHER_NAME = '" + strMothersName + "', "
                                                           + " CLIENT_DOB = TO_DATE('"+ strDOB +"', 'dd/mm/yyyy') , "
                                                          + " CLIENT_OFFIC_ADDRESS = '" + strOfcAdss + "', "
                                                          + " OCCUPATION = '" + strOccupation + "', "
                                                          + " WORK_EDU_BUSINESS='" + strWEB + "', "
                                                          + " PUR_OF_TRAN = '" + strPurOfTran + "', "
                                                          + " UISC_AGENT = '" + strUISCAgent + "', "
                                                          + " HUSBAND_NAME='" + strHusbandName + "', "
                                                          + " THANA_ID='" + strThana + "', "
                                                          + " INCOMPLETE_KYC='" + strIncompleteKYC + "', "
                                                          + " VERIFIED_BY = '" + strUpdatedBy + "'"
                                                          + " WHERE (CLINT_ID = '" + strClientID + "') ";
        }
        try
        {
            OLEDBCmd.CommandText = strSQL;

            OLEDBCmd.CommandType = CommandType.Text;//Setup Command Type
            OLEDBCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            blnClientUpdate = true;
            return blnClientUpdate;
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return blnClientUpdate;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    //public bool UpdateClientList(string strClientName, string strFathersName, string strMothersName, string strDOB,
    //                             string strOccupation, string strWEB, string strPurOfTran, string strUISCAgent,
    //                             string strOfcAdss, string strPreAdss, string strPerAdss, string strThana, string strClientID,
    //                             string strUpdatedBy, string strUpdateDate, string strReqPartyType, string strUpdateMessage)
    //{

    //    string strSQL = "";
    //    bool blnClientUpdate = false;
    //    OracleConnection conn;
    //    OracleCommand OLEDBCmd;
    //    OracleTransaction dbTransaction = null;
    //    //-------------------------------------
    //    conn = new OracleConnection(strConString);
    //    conn.Open();
    //    dbTransaction = conn.BeginTransaction();
    //    OLEDBCmd = new OracleCommand();
    //    OLEDBCmd.Connection = conn;
    //    OLEDBCmd.Transaction = dbTransaction;

    //    if (strUpdateMessage == "")
    //    {
    //        strSQL = " UPDATE CLIENT_LIST SET CLINT_NAME = '" + strClientName + "', "
    //                                                               + " CLINT_ADDRESS1 = '" + strPreAdss + "', "
    //                                                               + " CLINT_ADDRESS2 = '" + strPerAdss + "', "
    //                                                               + " CLINT_FATHER_NAME = '" + strFathersName + "', "
    //                                                               + " CLINT_MOTHER_NAME = '" + strMothersName + "', "
    //                                                               + " CLIENT_DOB = '" + strDOB + "', "
    //                                                               + " CLIENT_OFFIC_ADDRESS = '" + strOfcAdss + "', "
    //                                                               + " OCCUPATION = '" + strOccupation + "', "
    //                                                               + " WORK_EDU_BUSINESS='" + strWEB + "', "
    //                                                               + " PUR_OF_TRAN = '" + strPurOfTran + "', "
    //                                                               + " UISC_AGENT = '" + strUISCAgent + "', "
    //                                                               + " THANA_ID='" + strThana + "', "
    //                                                               + " KYC_UPDATED_BY ='" + strUpdatedBy + "', "
    //                                                               + " UPDATE_DATE=TO_DATE('" + strUpdateDate + "','dd/mm/yyyy HH24:MI:SS'),"
    //                                                               + " REQUEST_PARTY_TYPE='" + strReqPartyType + "' "
    //                                                               + " WHERE (CLINT_ID = '" + strClientID + "') ";
    //    }
    //    else
    //    {
    //        strSQL = " UPDATE CLIENT_LIST SET CLINT_NAME = '" + strClientName + "', "
    //                                                      + " CLINT_ADDRESS1 = '" + strPreAdss + "', "
    //                                                      + " CLINT_ADDRESS2 = '" + strPerAdss + "', "
    //                                                      + " CLINT_FATHER_NAME = '" + strFathersName + "', "
    //                                                      + " CLINT_MOTHER_NAME = '" + strMothersName + "', "
    //                                                      + " CLIENT_DOB = '" + strDOB + "', "
    //                                                      + " CLIENT_OFFIC_ADDRESS = '" + strOfcAdss + "', "
    //                                                      + " OCCUPATION = '" + strOccupation + "', "
    //                                                      + " WORK_EDU_BUSINESS='" + strWEB + "', "
    //                                                      + " PUR_OF_TRAN = '" + strPurOfTran + "', "
    //                                                      + " UISC_AGENT = '" + strUISCAgent + "', "
    //                                                      + " THANA_ID='" + strThana + "' "
    //                                                      + " WHERE (CLINT_ID = '" + strClientID + "') ";
    //    }
    //    try
    //    {
    //        OLEDBCmd.CommandText = strSQL;

    //        OLEDBCmd.CommandType = CommandType.Text;//Setup Command Type
    //        OLEDBCmd.ExecuteNonQuery();
    //        dbTransaction.Commit();
    //        blnClientUpdate = true;
    //        return blnClientUpdate;
    //    }
    //    catch (Exception ex)
    //    {
    //        return blnClientUpdate;
    //    }
    //}
    //Author : Kowshik
    //Date   : 07-April-2015.
    //Purposer:Check CN Amount taka 25
    public string GetCNStatus(string strWallet)
    {
        string strSql = "";
        strSql = " SELECT COUNT(*) COUNT  FROM BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT,SERVICE_REQUEST SR,BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL "
               + " WHERE CAT.REQUEST_ID=SR.REQUEST_ID  AND UPPER(SUBSTR (SR.REQUEST_TEXT,2,INSTR (SR.REQUEST_TEXT, '*', 2) - 2)) IN ('CN')"
               + " AND CAS_TRAN_PURPOSE_CODE='TOTAMT' AND CAT.CAS_ACC_ID=CAL.CAS_ACC_ID AND CAS_ACC_NO='" + strWallet + "' AND CAT.CAS_TRAN_AMT>=25 ";
        string strReturnColumnValue = "";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["COUNT"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }
    public DataSet GetClientList(string strSearchField)
    {
        string strMsg = "", strSql = "";
        strSql = strSearchField;
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CLIENT_LIST");
            return oDS;
        }
        catch (Exception e)
        {
            strMsg = e.Message.ToString();
            return null;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }


    //public DataSet GetClientList(string strSearchField)
    //{
    //    string strMsg = "", strSql = "";
    //    strSql = " SELECT * FROM CLIENT_LIST " + strSearchField + " ORDER BY CLINT_ID ASC";
    //    try
    //    {
    //        OracleConnection conn = new OracleConnection(strConString);
    //        DataSet oDS = new DataSet();
    //        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
    //        oOrdersDataAdapter.Fill(oDS, "CLIENT_LIST");
    //        return oDS;
    //    }
    //    catch (Exception e)
    //    {
    //        strMsg = e.Message.ToString();
    //        return null;
    //    }
    //}
    public DataSet GetTableValue(string strTblName, string strColumName, string strColumnValue)
    {
        string strMsg = "", strSql = "";
        strSql = " SELECT * FROM " + strTblName + " WHERE " + strColumName + "= '" + strColumnValue + "' ";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, " " + strTblName + " ");
            return oDS;
        }
        catch (Exception e)
        {
            strMsg = e.Message.ToString();
            return null;
        }
    }
    #endregion
    #region KYC Verification(Account RG Commission) Kowshik(18-NOV-2012)
    public string ExecAccRGComm(string strWalletID, string strAccntID, string strRequistID)
    {
        OracleConnection conn = new OracleConnection(strConString);
        string strSql = "", strStatus = "", strCustMob = "", strAgntMob = "", strComDibse = "", strMesg2="";
        string strAgentPackageID = "", strm = "", strAgentRankeID = "";
        strSql = "SELECT DISTINCT STATUS,CUSTOMER_MOBILE_NO,AGENT_MOBILE_NO,COMMISSION_DISBURSE FROM ACCOUNT_SERIAL_DETAIL WHERE CUSTOMER_MOBILE_NO='" + strWalletID + "'";
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strStatus = dr["STATUS"].ToString();
                    strCustMob = dr["CUSTOMER_MOBILE_NO"].ToString();
                    strAgntMob = dr["AGENT_MOBILE_NO"].ToString();
                    strComDibse = dr["COMMISSION_DISBURSE"].ToString();
                }
            }
            conn.Close();
            //########### Distributor and agent package checking for commission ###################
            //------------------Checking Agent Rank for commission ----------------------------------
            strAgentRankeID = ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "ACCNT_RANK_ID", "ACCNT_MSISDN", strAgntMob);
            //if (strAgentRankeID == "120519000000000005" || strAgentRankeID == "130922000000000004" || strAgentRankeID == "140410000000000004")
            //{
            strm = ExeAccRGComm(strStatus, strCustMob, strAgntMob, strComDibse, strAccntID, strRequistID);
            //string strMesg = UpdateAccountList(strAccntID);
            // }
            //------------------ End Checking Agent Rank for commission ----------------------------------
            //------------------------ Update Account List Information Here ---------------------
            //else
            //{
            strMesg2 = UpdateAccountList(strAccntID);
            // }
            //-------------------------End Update Account List Information Here------------------
            return strm;
        }
        catch (Exception ex)
        {
            return "No Data Found " + ex.Message.ToString();
            conn.Close();
        }
        finally
        {
            conn.Dispose();
            conn = null;
        }
    }
    //Author : Kowshik
    //Date : 19-Jun-2014
    //Update Account List during Account Verification.
    public string UpdateAccountList(string strAccntID)
    {
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = conn.CreateCommand();
            dbTransaction = conn.BeginTransaction();
            cmd.Transaction = dbTransaction;
            cmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_STATE = 'A',ACCNT_RANK_ID='120519000000000006' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
            cmd.CommandType = CommandType.Text; //Setup Command Type
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch
        {
            dbTransaction.Rollback();
            return "Unsuccess.";
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    //Author: Kowshik
    //Date  :05-July-2014.
    //Purposer : Update Account Serial Detail Table Verified=V.
    public string ExeAccRGComm(string strStatus, string strCustMob, string strAgntMob, string strComDibse, string strAccntID, string strRequistID)
    {
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();

        if (strStatus == "U" && strCustMob != "" && strAgntMob != "")//&& strComDibse == "N"
        {
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                dbTransaction = conn.BeginTransaction();
                cmd.Transaction = dbTransaction;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " UPDATE ACCOUNT_SERIAL_DETAIL SET VERIFIED='V' WHERE CUSTOMER_MOBILE_NO='" + strCustMob + "' AND AGENT_MOBILE_NO='" + strAgntMob + "' AND BANK_CODE='MBL' ";//AND REQUEST_ID='" + strRequistID + "'
                // cmd.CommandText = " PKG_MB_NATIVE_TRANSACTION.ACCOUNT_RG_COMMISSION ('" + strCustMob.Substring(3, 11) + "1" + "')";
                cmd.ExecuteNonQuery();
                //---------------------Commented by Kowshik -19-Jun-2014-----------------------
                //cmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_STATE = 'A',ACCNT_RANK_ID='120519000000000006' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                //cmd.CommandType = CommandType.Text; //Setup Command Type
                //cmd.ExecuteNonQuery();
                //-------------------------------------------------------------------------------
                dbTransaction.Commit();
                conn.Close();
                return "Successfull.";
            }
            catch
            {
                return "Unsuccess.";
            }
        }
        else
        {
            return "UnSuccessfull.";
        }
    }
    //Author: Kowshik
    //Date  :01-July-2014.
    //Purposer : Update Account Serial Detail Table Verified=V.
    public string UpdateVerifiedStatus(string strCustMob)
    {

        //-------------------------------------
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.Transaction = dbTransaction;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " UPDATE ACCOUNT_SERIAL_DETAIL SET VERIFIED='V' WHERE CUSTOMER_MOBILE_NO='" + strCustMob + "' AND BANK_CODE='MBL' ";
            // cmd.CommandText = " PKG_MB_NATIVE_TRANSACTION.ACCOUNT_RG_COMMISSION ('" + strCustMob.Substring(3, 11) + "1" + "')";
            cmd.ExecuteNonQuery();
            //---------------------Commented by Kowshik -19-Jun-2014-----------------------
            //cmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_STATE = 'A',ACCNT_RANK_ID='120519000000000006' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
            //cmd.CommandType = CommandType.Text; //Setup Command Type
            //cmd.ExecuteNonQuery();
            //-------------------------------------------------------------------------------
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch
        {
            return "Unsuccess.";
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
    #endregion

    #region Replace MobileNo form related method(bushra)
    public string SearchWallet(string strWalletID)
    {
        string strFilter = "";

        string strClntID = "";

        string strResult = "";
        //########### for Client  ID #########################
        strFilter = "SELECT * FROM ACCOUNT_LIST WHERE ACCNT_NO='" + strWalletID + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(strFilter, conn);
        DataSet oDS = new DataSet();
        OracleDataReader dr;
        dr = cmd.ExecuteReader();


        if (dr.HasRows)
        {
            while (dr.Read())
            {
                strClntID = dr["CLINT_ID"].ToString();
            }
        }
        //####################################################

        return strClntID;
    }
    public string SearchOldMobileNmbr(string strOldNumber, string strNewNmbr)
    {
        string strFilter = "";
        string strGetOldnumber = "";
        string strFilterNew = "";
        string strGetNewNumber = "";
        //##################### For New Number ##############################
        strFilterNew = "SELECT * FROM ACCOUNT_LIST WHERE ACCNT_NO='" + strNewNmbr + "' ";
        OracleConnection conn1 = new OracleConnection(strConString);
        conn1.Open();
        OracleCommand cmd1 = new OracleCommand(strFilterNew, conn1);
        DataSet oDS1 = new DataSet();
        OracleDataReader dr1;
        dr1 = cmd1.ExecuteReader();
        if (dr1.HasRows)
        {
            while (dr1.Read())
            {
                strGetNewNumber = dr1["ACCNT_NO"].ToString();
            }
        }

        //####################################################################

        //############# for old Number ##################################
        if (strNewNmbr == strGetNewNumber)
        {
            string strResult = "Your New Number Already Registrated!";
            return strResult;
        }
        else
        {
            strFilter = "SELECT * FROM ACCOUNT_LIST WHERE ACCNT_NO='" + strOldNumber + "' ";
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strFilter, conn);
            DataSet oDS = new DataSet();
            OracleDataReader dr;
            dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strGetOldnumber = dr["ACCNT_NO"].ToString();
                }
            }
            //################################################################
            return strGetOldnumber;
        }
    }
    public string WALLET_MODIFY(string strOldNumber, string strNewNumber)
    {
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();

        if (strOldNumber != "" && strNewNumber != "")
        {
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                dbTransaction = conn.BeginTransaction();
                cmd.Transaction = dbTransaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = " WALLET_MODIFY('" + strOldNumber + "','" + strNewNumber + "')";
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                conn.Close();
                return "Successfull.";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                //return "Unsuccess.";
            }
        }
        else
        {
            return "Already Commission Disbursed.";
        }
    }
    #endregion
    #region Modify Form Serial related method(bushra,Kowshik)
    public string GetFromSerial(string strSerialNo)
    {
        string strFilter = "", strGetSerailNo = "", strResult = "", strActiveDate = "", strComDisbrs = "";
        string strStutus = "", strCustomerNo = "", strAgentPhNo = "", strRequestID = "";
        if (!strSerialNo.Equals(""))
        {
            //########### for Client  ID #########################
            strFilter = "SELECT * FROM ACCOUNT_SERIAL_DETAIL WHERE SERIAL_NO='" + strSerialNo + "' ";
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strFilter, conn);
            DataSet oDS = new DataSet();
            OracleDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strGetSerailNo = dr["SERIAL_NO"].ToString();
                    strStutus = dr["STATUS"].ToString();
                    strCustomerNo = dr["CUSTOMER_MOBILE_NO"].ToString();
                    strAgentPhNo = dr["AGENT_MOBILE_NO"].ToString();
                    strActiveDate = Convert.ToString(dr["ACTIVATION_DATE"]);
                    strRequestID = dr["REQUEST_ID"].ToString();
                    strComDisbrs = dr["COMMISSION_DISBURSE"].ToString();
                }
            }
            //####################################################
        }
        return strGetSerailNo + "*" + strStutus + "*" + strCustomerNo + "*" + strAgentPhNo + "*" + strActiveDate + "*" + strRequestID + "*" + strComDisbrs;
    }
    public string UpdateFrmSerialBoth(string strCustomerMbl, string strOldSerialNo, string strStatus, string strAgentPhNo,
                                      string strAvalblSerl, string strActiveDate, string strRequestID, string strComDisbrs)
    {
        //################ for new serial update #####################
        string updateString = @"UPDATE ACCOUNT_SERIAL_DETAIL SET STATUS='" + strStatus + "',"
                            + " CUSTOMER_MOBILE_NO='" + strCustomerMbl + "',AGENT_MOBILE_NO='" + strAgentPhNo + "',"
                            + " ACTIVATION_DATE= TO_DATE('" + strActiveDate + "','dd/mm/yyyy HH24:MI:SS'),"
                            + " REQUEST_ID='" + strRequestID + "', COMMISSION_DISBURSE='" + strComDisbrs + "'"
                            + " WHERE SERIAL_NO ='" + strAvalblSerl + "'";
        OracleConnection conn = new OracleConnection(strConString);
        string strReturn = "";
        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(updateString);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            strReturn = ex.Message.ToString();
        }
        //############### for Old serial update ##########################
        if (Convert.ToDouble(strOldSerialNo) < 200000000)
        {
            string updateStringOld = @"UPDATE ACCOUNT_SERIAL_DETAIL SET ACTIVATION_DATE ='',STATUS='" + "A" + "',"
                                   + " CUSTOMER_MOBILE_NO='',AGENT_MOBILE_NO='', REQUEST_ID='', "
                                   + " COMMISSION_DISBURSE='N' WHERE SERIAL_NO ='" + strOldSerialNo + "'";
            OracleConnection conn1 = new OracleConnection(strConString);
            try
            {
                conn1.Open();
                OracleCommand cmd1 = new OracleCommand(updateStringOld);
                cmd1.Connection = conn1;
                cmd1.ExecuteNonQuery();
                conn1.Close();
            }
            catch (Exception ex)
            {
                strReturn = ex.Message.ToString();
            }
        }
        return strReturn;
    }
    public string UpdateFrmSerial(string strCustomerMbl, string strfrmSerial)
    {
        string updateString = @"UPDATE ACCOUNT_SERIAL_DETAIL SET SERIAL_NO ='" + strfrmSerial + "'  WHERE CUSTOMER_MOBILE_NO ='" + strCustomerMbl + "'";
        OracleConnection conn = new OracleConnection(strConString);
        string strReturn = "";

        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(updateString);
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
    #endregion

    #region Delete Account Hierarchy Related Method(Kowshik(11-Dec-2012))
    public string DeleteAccountHierarchy(string strAccntID)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn; //Active Connection
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = "DELETE FROM ACCOUNT_HIERARCHY WHERE ACCNT_ID='" + strAccntID.Trim() + "'";
            OracleCmd.CommandType = CommandType.Text; //Setup Command Type
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfully deleted from Account Hierarchy.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
    }
    #endregion

    #region sms resend related method(kowshik)
    public string UpdateResponseState(string strResponseID)
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

            OracleCmd.CommandText = "UPDATE SERVICE_RESPONSE SET RESPONSE_STAE='P',RESPONSE_TRY_COUNT='0' WHERE RESPONSE_ID='" + strResponseID + "'";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
    }
    #endregion

    #region  account delete related method(kowshik(13-12-12))
    public string DeleteSingleAccount(string strMobileNo)
    {
        OracleConnection conn;
        OracleCommand OracleCmd = new OracleCommand();
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = conn.CreateCommand();

            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandType = CommandType.StoredProcedure;
            OracleCmd.CommandText = " ACCOUNT_DELETE_SINGLE ('" + strMobileNo + "')";

            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
    }
    #endregion
    #region execute procedure(kowshik)
    public string ExecuteProcedure(string strProcedure)
    {
        OracleConnection conn;
        OracleCommand OracleCmd = new OracleCommand();
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = conn.CreateCommand();

            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandType = CommandType.StoredProcedure;
            OracleCmd.CommandText = strProcedure;

            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
    }
    #endregion

    #region (kowshik)
    public DataSet ReturnDataSet(string strSQLQuery)
    {
        string strReturnValue = "";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQLQuery, conn));
            oOrdersDataAdapter.Fill(oDS, "Table1");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    #endregion

    #region Manual Service Handler(kowshik)
    public string SaveServiceRequest(string strSender, string strReceipent, string strMsg, string AccID)
    {
        string strSQL = "";
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;
        DataRow oOrderRow;
        OracleDataAdapter oOrdersDataAdapter;
        DataSet oDS = new DataSet();
        //---------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //-------------------------------------
        //OracleCmd = new OracleCommand();
        //OracleCmd.Connection = conn; //Active Connection
        //OracleCmd.Transaction = dbTransaction;
        //----------------------------------
        strSQL = "SELECT REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TEXT,REQUEST_PARTY_TYPE,ACCNT_ID,"
                + " REQUEST_OTP_AUTHEN,REQUEST_SQA_AUTHEN,REQUEST_OTP_GENERTATED FROM SERVICE_REQUEST";

        try
        {
            OracleCmd = new OracleCommand(strSQL, conn, dbTransaction);
            oOrdersDataAdapter = new OracleDataAdapter(OracleCmd);

            //OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            // oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn,dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "SERVICE_REQUEST");
            oOrderRow = oDS.Tables["SERVICE_REQUEST"].NewRow();

            oOrderRow["REQUEST_PARTY"] = strSender;
            oOrderRow["RECEIPENT_PARTY"] = strReceipent;
            oOrderRow["REQUEST_TEXT"] = strMsg;
            oOrderRow["ACCNT_ID"] = AccID;
            oOrderRow["REQUEST_PARTY_TYPE"] = "SMS";

            oOrderRow["REQUEST_OTP_AUTHEN"] = "N";
            oOrderRow["REQUEST_SQA_AUTHEN"] = "N";
            oOrderRow["REQUEST_OTP_GENERTATED"] = "N";

            oDS.Tables["SERVICE_REQUEST"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "SERVICE_REQUEST");

            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return ex.Message.ToString();
        }
    }
    #endregion

    // added by bushra 12/3/13
    public string ChangeTopuptrnaID(string strTOPUP_TRAN_ID)
    {
        string strChangeTopupID = "";
        string strTopupTransID = "SELECT SUBSTR(TOPUP_TRAN_ID,1,7)+1|| SUBSTR(TOPUP_TRAN_ID,8,18)NEW_TOPUP_TRAN_ID FROM TOPUP_TRANSACTION WHERE TOPUP_TRAN_ID='" + strTOPUP_TRAN_ID + "'";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(strTopupTransID, conn);
        OracleDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            strChangeTopupID = dr["NEW_TOPUP_TRAN_ID"].ToString();

        }
        return strChangeTopupID;

    }
    // added by bushra 12/3/13
    public bool ReSendTopupRequest(string strTopupTransID, string strREQUEST_ID, string strTRAN_DATE, string strREQUEST_STATUS, string strSUCCESSFUL_STATUS, string strSSL_CREATE_RECHAGE_STATUS, string strSSL_CREATE_MESSAGE, string strSSL_INT_RECHAGE_STATUS, string strSSL_INT_MESSAGE, string strSSL_FINAL_STATUS, string strSSL_FINAL_MESSAGE)
    {
        string strSQL = "";

        DateTime strdatatime = DateTime.Now;
        string strDate = String.Format("{0:dd-MM-yyyy HH:mm:ss}", strdatatime);
        bool blnClientUpdate = false;
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        // string strSql = "";
        try
        {
            strSQL = " UPDATE TOPUP_TRANSACTION SET TOPUP_TRAN_ID = '" + strTopupTransID + "', "
                                                         + " TRAN_DATE = TO_DATE(\'" + strDate + "\',\'dd/mm/yyyy HH24:MI:SS\'), "
                                                         + " REQUEST_STATUS = 'P', "
                                                         + " SUCCESSFUL_STATUS = 'N', "
                                                         + " SSL_CREATE_RECHAGE_STATUS = '', "
                                                         + " SSL_CREATE_MESSAGE = '', "
                                                         + " SSL_INT_RECHAGE_STATUS = '', "
                                                         + " SSL_INT_MESSAGE='', "
                                                         + " SSL_FINAL_STATUS = '',"
                                                         + " SSL_FINAL_MESSAGE='' WHERE REQUEST_ID='" + strREQUEST_ID + "' ";
            OracleCommand cmd = new OracleCommand(strSQL);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            blnClientUpdate = true;
            return blnClientUpdate;
        }
        catch (Exception ex)
        {
            return blnClientUpdate;
        }


    }

    // added by bushra 18/3/13
    public DataSet ExecuteQuery(string strSQL)
    {
        try
        {
          
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            conn.Open();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn));
            conn.Close();
            oOrdersDataAdapter.Fill(oDS, "Table1");
            return oDS;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return null;
        }
    }
    public DataSet ExecuteQuery_2(string strSQL)
    {
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            conn.Open();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn));
            conn.Close();
            oOrdersDataAdapter.Fill(oDS, "Table1");
            return oDS;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return null;
        }
    }
    #region ACCOUNT TRANSFER RELATED METHOD(KOWSHIK(31-MAR-2013))
    public string UpdateHierarchy(string strNewAccntID, string strAccntID)
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

            OracleCmd.CommandText = "UPDATE ACCOUNT_HIERARCHY SET HIERARCHY_ACCNT_ID='" + strNewAccntID + "' WHERE ACCNT_ID IN(" + strAccntID + ")";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
    }
    #endregion

    // BUSHRA 2/4/13
    public void UpdateTopupNote(string strOldvalue, string strNewValue, string strTransID)
    {
        string strUpdate = "Update " + strOldvalue + "-to-" + strNewValue;
        string updateString = @"UPDATE TOPUP_TRANSACTION SET UPDATE_NOTE ='" + strUpdate + "' WHERE TOPUP_TRAN_ID='" + strTransID + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        string strReturn = "";

        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(updateString);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            strReturn = ex.Message.ToString();
        }
    }

    #region bushra 2/4/13
    public string ReversedNote(string strTOPUP_TRAN_ID)
    {
        string strResult = "";
        string strSQL = " SELECT REVERSE_NOTE FROM TOPUP_TRANSACTION WHERE TOPUP_TRAN_ID='" + strTOPUP_TRAN_ID + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(strSQL, conn);
        OracleDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            strResult = dr["REVERSE_NOTE"].ToString();
        }
        return strResult;
    }
    #endregion
    #region INSERT SERIAL NO RELATED METHOD (KOWSHIK(09-APR-2013)
    public DataSet GetDataSet(string strSQL)
    {
        string strMsg;
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = null;
        try
        {
            oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn));
            oOrdersDataAdapter.Fill(oDS, "Table1");
            return oDS;
        }
        catch (Exception e)
        {
            strMsg = e.Message.ToString();
            return oDS;
        }
    }
    public string UpdateAccntSerialDetail(string strCustMobileNo, string strAgntMobNo, string strFormSLNo)
    {
        string updateString;
        updateString = @"UPDATE ACCOUNT_SERIAL_DETAIL SET STATUS='U', CUSTOMER_MOBILE_NO='" + strCustMobileNo + "',AGENT_MOBILE_NO='" + strAgntMobNo + "' WHERE SERIAL_NO='" + strFormSLNo + "'";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(updateString);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
    #endregion

    #region Two Account Open realted method
    // Author  : Kowshik
    // Date    : 30-Oct-2013.
    // Purposer: Getting Duplicate Two Account information.
    public string CheckDuplicateTwoAccount(string strAccntWallet)
    {
        string strSql = "";
        strSql = " SELECT CLINT_BANK_ACC_NO  FROM  CLIENT_BANK_ACCOUNT  WHERE  CLINT_BANK_ACC_NO='" + strAccntWallet + "'";
        OracleConnection conn = new OracleConnection(strConString);
        string strAccntWalletTwo = "";
        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strAccntWalletTwo = dr["CLINT_BANK_ACC_NO"].ToString();
                }
                return strAccntWalletTwo;
            }
            else
            {
                return strAccntWalletTwo;
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            return strAccntWalletTwo;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    // Author  : Kowshik
    // Date    : 30-Oct-2013.
    // Purposer: Getting Bank Code information.
    public string GettingBankCode(string strUserName)
    {
        string strSql = "";
        strSql = " SELECT DISTINCT BL.BANK_INTERNAL_CODE FROM CM_SYSTEM_USERS CSU,BANK_LIST BL"
               + " WHERE  CSU.CMP_BRANCH_ID=BL.CMP_BRANCH_ID AND CSU.SYS_USR_LOGIN_NAME='" + strUserName + "'";
        OracleConnection conn = new OracleConnection(strConString);
        string strBankCode = "";
        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strBankCode = dr["BANK_INTERNAL_CODE"].ToString();
                }
                return strBankCode;
            }
            else
            {
                return strBankCode;
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            return strBankCode;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    // Author  : Kowshik
    // Date    : 30-Oct-2013.
    // Purposer: Open wallet two.
    public string OpenWalletTwo(string strMobileNo, string strBankCode, string strDigit)
    {
        OracleConnection conn;
        OracleCommand OracleCmd = new OracleCommand();
        OracleTransaction dbTransaction;
        string strProcedure = "";
        strProcedure = " ADD_NEW_WALLET('" + strMobileNo + "','" + strBankCode + "','" + strDigit + "')";

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = conn.CreateCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandType = CommandType.StoredProcedure;
            OracleCmd.CommandText = strProcedure;

            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            return "Successfull.";
        }
        catch (Exception ex)
        {

            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    #endregion

    #region Change wallet admin related method
    public string UpdatePackageandRank(string strAccntID, string strRankID, string strPackgID)
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

            OracleCmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_RANK_ID='" + strRankID + "' , SERVICE_PKG_ID ='" + strPackgID + "' WHERE ACCNT_ID ='" + strAccntID + "'";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
    }

    // Author  : Kowshik
    // Date    : 31-Oct-2013.
    // Purposer: Getting Cash Account ID.
    public string GettingCashAccountID(string strRankID)
    {
        string strSql = "";
        strSql = "SELECT CAS_ACC_TYPE_ID FROM ACCOUNT_RANK WHERE  ACCNT_RANK_ID='" + strRankID + "'";
        OracleConnection conn = new OracleConnection(strConString);
        string strCasAccntID = "";
        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strCasAccntID = dr["CAS_ACC_TYPE_ID"].ToString();
                }
                return strCasAccntID;
            }
            else
            {
                return strCasAccntID;
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            return strCasAccntID;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    // Author  : Kowshik
    // Date    : 31-Oct-2013.
    // Purposer: Update Cash Account ID in Cash Acccount List table.
    public string UpdateCashAccTypeID(string strCasAccntTypeID, string strCasAccntNo)
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

            OracleCmd.CommandText = "UPDATE BDMIT_ERP_101.CAS_ACCOUNT_LIST SET CAS_ACC_TYPE_ID='" + strCasAccntTypeID + "'  WHERE CAS_ACC_NO ='" + strCasAccntNo + "'";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;

        }
    }
    // Author  : Kowshik
    // Date    : 31-Oct-2013.
    // Purposer: Getting Wallet information.
    public DataSet GetAllWalletAccount(string strAccntWallet)
    {
        string strSql = "";

        strSql = " SELECT CAS_ACC_NO FROM APSNG101.ACCOUNT_LIST AL,APSNG101.CLIENT_BANK_ACCOUNT CBA,"
               + " BDMIT_ERP_101.CAS_ACCOUNT_LIST CAL WHERE AL.ACCNT_ID=CBA.ACCNT_ID(+) AND "
               + " CLINT_BANK_ACC_NO=CAL.CAS_ACC_NO AND ACCNT_NO='" + strAccntWallet + "'";
        OracleConnection conn = new OracleConnection(strConString);
        try
        {
            conn.Open();
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "Table1");
            conn.Close();
            return oDS;
        }
        catch (Exception e)
        {
            conn.Close();
            return null;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    #endregion

    #region
    public string GetMerchantID(string strAccountNO)
    {
        OracleConnection conn = new OracleConnection(strConString);
        string strSql = "SELECT ACCNT_ID FROM ACCOUNT_LIST WHERE  SERVICE_PKG_ID='1312050001' AND ACCNT_RANK_ID='131205000000000001' AND ACCNT_NO='" + strAccountNO + "'";
        string strReturnColumnValue = "";
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["ACCNT_ID"].ToString() != "")
                    {
                        strReturnColumnValue = dr["ACCNT_ID"].ToString();
                    }
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return strReturnColumnValue;
        }
        finally
        {
            conn.Close();
        }
    }
    #endregion

    #region SMS BroadCast
    // Author   : Kowshik.
    // Date     : 18-Dec-2013.
    // Purposer : SMS Broadcasting.
    public string GetSMSBroadcastMsg(string strSMSContent, string strStatus, string strAccRankID)
    {
        string strOutput = "", strSQL = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        strSQL = "SELECT APSNG101.FUNC_SMS_BROADCAST ('" + strSMSContent + "','" + strStatus + "','" + strAccRankID + "') AS OUTPUT FROM DUAL";
        try
        {
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["OUTPUT"].ToString() != "")
                    {
                        strOutput = dr["OUTPUT"].ToString();
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
    // Author   : Kowshik.
    // Date     : 05-May-2014.
    // Purposer : SMS Broadcasting.
    public string GetPkgSMSBroadcastMsg(string strSMSContent, string strStatus, string strAccPkgID)
    {
        string strOutput = "", strSQL = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        strSQL = "SELECT APSNG101.FUNC_SMS_BROADCAST_BY_PKG ('" + strSMSContent + "','" + strStatus + "','" + strAccPkgID + "') AS OUTPUT FROM DUAL";
        try
        {
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["OUTPUT"].ToString() != "")
                    {
                        strOutput = dr["OUTPUT"].ToString();
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
    public string GetPkgSMSBroadcastMsgOnlyActive(string strSMSContent, string strStatus, string strAccPkgID)
    {
        string strOutput = "", strSQL = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        strSQL = "SELECT APSNG101.FUNC_SMS_BROADCAST_BY_PKG_TRAN ('" + strSMSContent + "','" + strStatus + "','" + strAccPkgID + "') AS OUTPUT FROM DUAL";
        try
        {
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["OUTPUT"].ToString() != "")
                    {
                        strOutput = dr["OUTPUT"].ToString();
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
    public string ResubmitNote(string strTOPUP_TRAN_ID)
    {
        string strResult = "";
        string strSQL = " SELECT RESUBMIT_NOTE FROM TOPUP_TRANSACTION WHERE TOPUP_TRAN_ID='" + strTOPUP_TRAN_ID + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(strSQL, conn);
        OracleDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            strResult = dr["RESUBMIT_NOTE"].ToString();

        }
        conn.Close();
        return strResult;

    }


    #region Account Serial Creation methods
    // Author   : Kowshik.
    // Date     : 14-May-2014.
    // Purposer : Create Account Serial No.
    public string CreateAccSLNo(string strMasterID, string strStartSLNo, string strEndSLNo)
    {
        string strOutput = "", strSQL = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        strSQL = "SELECT APSNG101.FUNC_CREATE_FORM_SLNO ('" + strMasterID + "','" + strStartSLNo + "','" + strEndSLNo + "') AS OUTPUT FROM DUAL";
        try
        {
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["OUTPUT"].ToString() != "")
                    {
                        strOutput = dr["OUTPUT"].ToString();
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
    // Author   : Sajib
    // Date     : 14-May-2014.
    // Purposer : Create Account Serial No.
    public string CreateAccSLNo(string strMasterID, string strStartSLNo, string strEndSLNo, string strBnk)
    {
        string strOutput = "", strSQL = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        strSQL = "SELECT APSNG101.FUNC_CREATE_FORM_SLNO ('" + strMasterID + "','" + strStartSLNo + "','" + strEndSLNo + "','" + strBnk + "') AS OUTPUT FROM DUAL";
        try
        {
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["OUTPUT"].ToString() != "")
                    {
                        strOutput = dr["OUTPUT"].ToString();
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
    public string SaveRegRptInfo(string strReportFileName, string strUserLoginName, string strFDate, string strTDate, string strReportType, string AccountNO)
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
            strSql = "SELECT REPORT_NAME, CREATED_BY, REG_FROM_DATE,REG_TO_DATE,REPORT_TYPE,ACCOUNT_NO FROM REG_REPORT_INFO ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "REG_REPORT_INFO";
            oOrderRow = oDS.Tables["REG_REPORT_INFO"].NewRow();

            oOrderRow["REPORT_NAME"] = strReportFileName;
            oOrderRow["CREATED_BY"] = strUserLoginName;
            oOrderRow["REG_FROM_DATE"] = DateTime.Parse(strFDate);
            oOrderRow["REG_TO_DATE"] = DateTime.Parse(strTDate);
            oOrderRow["REPORT_TYPE"] = strReportType.ToString();
            oOrderRow["ACCOUNT_NO"] = AccountNO.ToString();

            oDS.Tables["REG_REPORT_INFO"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "REG_REPORT_INFO");

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

    public string SaveRegRptInfo(string strReportFileName, string strUserLoginName, string strFDate, string strTDate, string strReportType)
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
            strSql = "SELECT REPORT_NAME, CREATED_BY, REG_FROM_DATE,REG_TO_DATE,REPORT_TYPE FROM REG_REPORT_INFO ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "REG_REPORT_INFO";
            oOrderRow = oDS.Tables["REG_REPORT_INFO"].NewRow();

            oOrderRow["REPORT_NAME"] = strReportFileName;
            oOrderRow["CREATED_BY"] = strUserLoginName;
            oOrderRow["REG_FROM_DATE"] = DateTime.Parse(strFDate);
            oOrderRow["REG_TO_DATE"] = DateTime.Parse(strTDate);
            oOrderRow["REPORT_TYPE"] = strReportType.ToString();
            // oOrderRow["ACCOUNT_NO"] = strReportType.ToString();

            oDS.Tables["REG_REPORT_INFO"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "REG_REPORT_INFO");

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
    //Author: Sajib.
    //Date  : 17-Jun-2014.
    //Purposer: Update Customer Rank To Agent Rank by Territory Manager.
    public string UpdateCustomerToAgent(string strWalletID, string strAccRank, string strSrvcPkg)
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
            cmd.CommandText = "UPDATE APSNG101.ACCOUNT_LIST SET ACCNT_RANK_ID='120519000000000005', SERVICE_PKG_ID='1205190002' WHERE ACCNT_NO='" + strWalletID + "' AND ACCNT_RANK_ID ='" + strAccRank + "' AND SERVICE_PKG_ID = '" + strSrvcPkg + "'";
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
    #region ISO Related Method
    public string IfExistBankAndServiceCode(string strBankId, string strServiceId)
    {
        string strSql = "";
        strSql = " SELECT ISO_PRO_CODE_ID FROM ISO_PROCESSING_CODE WHERE BANK_ID = '" + strBankId + "' AND SERVICE_ID = '" + strServiceId + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["ISO_PRO_CODE_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }
    //Sajib(03.09.2014)
    // Data save to iso processing code table
    public string SaveToIsoProcessingCode(string strBankId, string strServiceId, string strProcessingCode, string strRemarks)
    {
        string strSql;
        string strIsoProcessingCodeId = "";
        OracleConnection conn = new OracleConnection(strConString);
        DataRow oOrderRow;
        OracleTransaction dbTransaction;
        DataSet oDS = new DataSet();
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            strSql = "SELECT BANK_ID, SERVICE_ID, PROCESSING_CODE, REMARKS FROM ISO_PROCESSING_CODE ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "ISO_PROCESSING_CODE";
            oOrderRow = oDS.Tables["ISO_PROCESSING_CODE"].NewRow();

            oOrderRow["BANK_ID"] = strBankId;
            oOrderRow["SERVICE_ID"] = strServiceId;
            oOrderRow["PROCESSING_CODE"] = strProcessingCode;
            oOrderRow["REMARKS"] = strRemarks;

            oDS.Tables["ISO_PROCESSING_CODE"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "ISO_PROCESSING_CODE");
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
    //Sajib(03.09.2014)
    // Data search from iso processing code table
    public string FindIsoProcessingCodeID(string strBankId, string strServiceId, string strProcessingCode, string strRemarks)
    {
        string strSql = "";
        strSql = " SELECT ISO_PRO_CODE_ID FROM ISO_PROCESSING_CODE WHERE BANK_ID = '" + strBankId + "' AND SERVICE_ID = '" + strServiceId + "' AND PROCESSING_CODE = '" + strProcessingCode + "' AND REMARKS = '" + strRemarks + "'  ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["ISO_PRO_CODE_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }    
    public DataSet GetOfflineData(string strSql)
    {
        try
        {
            DataSet oDS = new DataSet();
            OracleConnection conn = new OracleConnection(strConString);
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "BDMIT_ERP_101.CAS_DPS_TRANSACTION");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
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
        try
        {
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT * FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION WHERE CAS_DPS_ID='" + strTranID + "'", conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            oOrdersDataAdapter.Fill(oDS, "BDMIT_ERP_101.CAS_DPS_TRANSACTION");

            DataRow myDataRow1 = oDS.Tables["BDMIT_ERP_101.CAS_DPS_TRANSACTION"].Rows.Find(strTranID);
            myDataRow1["CAS_ISO_REQ_DESPATCH"] = strStatus;
            myDataRow1["CAS_ISO_DESPATHCH_DATE"] = strDate;
            myDataRow1["CAS_ISO_REQ_STATUS"] = "S";

            oOrdersDataAdapter.Update(oDS, "BDMIT_ERP_101.CAS_DPS_TRANSACTION");
            oOrdersDataAdapter.Dispose();
            dbTransaction.Commit();          
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    public string UpdateISOProcessingCode(string strPrcId, string strPrcCode, string strRemarks)
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

            OracleCmd.CommandText = "UPDATE ISO_PROCESSING_CODE SET PROCESSING_CODE='" + strPrcCode + "' , REMARKS ='" + strRemarks + "' WHERE ISO_PRO_CODE_ID ='" + strPrcId + "'";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
    }
    #endregion
    // Added by sajib
    // date: 25/08/2014
    // purpose: for manage service testing
    public string AddServiceRequestForOTP(string strRequestParty, string strRecepentParty, string strRequestState, string strReqText, string strReqPartyType)
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
            strSql = "SELECT REQUEST_PARTY, RECEIPENT_PARTY, REQUEST_STAE, REQUEST_TEXT, REQUEST_PARTY_TYPE, REQUEST_OTP_AUTHEN, REQUEST_SQA_AUTHEN, REQUEST_OTP_GENERTATED FROM SERVICE_REQUEST ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "SERVICE_REQUEST";
            oOrderRow = oDS.Tables["SERVICE_REQUEST"].NewRow();

            oOrderRow["REQUEST_PARTY"] = strRequestParty;
            oOrderRow["RECEIPENT_PARTY"] = strRecepentParty;
            oOrderRow["REQUEST_STAE"] = strRequestState;
            oOrderRow["REQUEST_TEXT"] = strReqText;
            oOrderRow["REQUEST_PARTY_TYPE"] = strReqPartyType;

            oOrderRow["REQUEST_OTP_AUTHEN"] = "Y";
            oOrderRow["REQUEST_SQA_AUTHEN"] = "Y";
            oOrderRow["REQUEST_OTP_GENERTATED"] = "Y";

            oDS.Tables["SERVICE_REQUEST"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "SERVICE_REQUEST");

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
    // Added by sajib
    // date: 25/08/2014
    // purpose: for manage service testing
    public string SaveServiceReqInfoForVariousFund(string strRequestParty, string strRecepentParty, string strRequestState, string strReqText, string strReqPartyType)
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
            strSql = "SELECT REQUEST_PARTY, RECEIPENT_PARTY, REQUEST_STAE, REQUEST_TEXT, REQUEST_PARTY_TYPE, REQUEST_OTP_AUTHEN, REQUEST_SQA_AUTHEN, REQUEST_OTP_GENERTATED FROM SERVICE_REQUEST ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "SERVICE_REQUEST";
            oOrderRow = oDS.Tables["SERVICE_REQUEST"].NewRow();

            oOrderRow["REQUEST_PARTY"] = strRequestParty;
            oOrderRow["RECEIPENT_PARTY"] = strRecepentParty;
            oOrderRow["REQUEST_STAE"] = strRequestState;
            oOrderRow["REQUEST_TEXT"] = strReqText;
            oOrderRow["REQUEST_PARTY_TYPE"] = strReqPartyType;

            oOrderRow["REQUEST_OTP_AUTHEN"] = "Y";
            oOrderRow["REQUEST_SQA_AUTHEN"] = "Y";
            oOrderRow["REQUEST_OTP_GENERTATED"] = "Y";

            oDS.Tables["SERVICE_REQUEST"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "SERVICE_REQUEST");

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

    public string GetRankCount(string strSqlQuery)
    {
        string strCount = "";
        OracleConnection conn = new OracleConnection(strConString);

        try
        {
            string strGetValue = strSqlQuery;
            OracleCommand showResult = new OracleCommand(strGetValue, conn);
            conn.Open();
            strCount = showResult.ExecuteOracleScalar().ToString();
            conn.Close();
            return strCount;
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
    // Author   : Kowshik.
    // Date     : 27-Jan-2015.
    // Purposer : Insert Report Infor in Agent Performance Table.
    public string SaveAgentPerRptInfo(string strReportFileName, string strUserLoginName)
    {
        string strSql = "";
        OracleConnection conn = new OracleConnection(strConString);
        DataRow oOrderRow;
        OracleTransaction dbTransaction;
        DataSet oDS = new DataSet();
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            strSql = "SELECT REPORT_NAME, CREATED_BY, REPORT_TYPE FROM AGNT_PERF_RPT ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "AGNT_PERF_RPT";
            oOrderRow = oDS.Tables["AGNT_PERF_RPT"].NewRow();

            oOrderRow["REPORT_NAME"] = strReportFileName;
            oOrderRow["CREATED_BY"] = strUserLoginName;
            oOrderRow["REPORT_TYPE"] = "";

            oDS.Tables["AGNT_PERF_RPT"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "AGNT_PERF_RPT");

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

    //--- for kyc update with 13 or 17 digit of national id
    #region kyc update with 13 or 17 digit of national id

    public string ClientIdIfExist(string strClientId)
    {
        string strSql = "";

        strSql = " SELECT CLIENT_ID FROM CR_CLIENTPHOTO_SIG WHERE CLIENT_ID = '" + strClientId + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["CLIENT_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }
    
    public string GetIdentityTypeName(string strClientId)
    {
        string strSql = "";
        strSql = " SELECT IDS.IDNTIFCTION_NAME IDNTIFCTION_NAME FROM CLIENT_IDENTIFICATION CI, IDENTIFICATION_SETUP IDS WHERE "
                + " CI.CLIENT_ID = '" + strClientId + "' AND CI.IDNTIFCTION_ID = IDS.IDNTIFCTION_ID";
        string strReturnColumnValue = "";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["IDNTIFCTION_NAME"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }

    public string GetIdentityNo(string strClientId)
    {
        string strSql = "";
        string strReturnColumnValue = "";
        strSql = " SELECT CLINT_IDENT_NAME FROM CLIENT_IDENTIFICATION WHERE CLIENT_ID = '" + strClientId + "'";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["CLINT_IDENT_NAME"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }

    public string UpdateClientIdentification(string strClientID, string strIdenName, string strIdenID, string strRemarks)
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

            OracleCmd.CommandText = "UPDATE CLIENT_IDENTIFICATION SET CLINT_IDENT_NAME = '" + strIdenName + "', IDNTIFCTION_ID = '" + strIdenID + "', REMARKS = '" + strRemarks + "'  WHERE CLIENT_ID ='" + strClientID + "'";
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
    #endregion

    #region for agent activity report

    public void ExeProForTblTempSAAccList(int strAgentCount)
    {
        //OracleConnection
        conn = new OracleConnection(strConString);
        string strMsg;
        try
        {
            conn.Open();
            OracleCommand myCMD = new OracleCommand("APSNG101.GEN_DSE_LIST('" + strAgentCount + "')", conn);
            myCMD.CommandType = CommandType.StoredProcedure;
            //myCMD.CommandType=CommandType.Text;
            myCMD.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
        }

    }

    public void ExeProForTblTempSAAccListWithDistriButor(int intAgentNo, string strDisWallet)
    {
        //OracleConnection
        conn = new OracleConnection(strConString);
        string strMsg;
        try
        {
            conn.Open();
            OracleCommand myCMD = new OracleCommand("APSNG101.GEN_DSE_LIST_WITH_DISTRI('" + intAgentNo + "', '" + strDisWallet + "')", conn);
            myCMD.CommandType = CommandType.StoredProcedure;
            //myCMD.CommandType=CommandType.Text;
            myCMD.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
        }

    }

    public void ExeProGenAgentActivityReport(string strSql)
    {
        //OracleConnection
        conn = new OracleConnection(strConString);
        string strMsg;
        try
        {
            conn.Open();
            OracleCommand myCMD = new OracleCommand(strSql, conn);
            myCMD.CommandType = CommandType.StoredProcedure;
            myCMD.CommandType = CommandType.Text;

            //myCMD.Parameters.Add(new OracleParameter("inFDate", OracleType.VarChar)).Value = dtpFromDate;
            //myCMD.Parameters.Add(new OracleParameter("inToDate", OracleType.VarChar)).Value = dtpToDate;
            //myCMD.Parameters.Add(new OracleParameter("inTrxNo", OracleType.VarChar)).Value = intAgtTtrxNo;
            //myCMD.Parameters.Add(new OracleParameter("inTrxAmt", OracleType.VarChar)).Value = intAgtTRxAmount;
            myCMD.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
        }
    }
    #endregion

    #region Upload Bulk File

    //Author:  Kowshik.
    //Date  :  13-July-2015.
    //Purposer:Save Bulk Channel File Upload. 
    public string  AddBulkSubsFile(string strFileName, string strUserID, string strMachineIP, string strMachineName, string abc)
    {
        OracleConnection conn = new OracleConnection(strConString);
        DataSet oDS = new DataSet();
        conn.Open();
        OracleTransaction dbTransaction = null;
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT BULK_ACCNT_FILE,UPLOAD_SYS_USR_ID,UPLOAD_SYSTEM_IP,UPLOAD_SYSTEM_NAME FROM BULK_ACCOUNT_CREATION", conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "BULK_ACCOUNT_CREATION";

            // Insert the Data
            DataRow oOrderRow = oDS.Tables["BULK_ACCOUNT_CREATION"].NewRow();

            oOrderRow["BULK_ACCNT_FILE"] = strFileName;
            oOrderRow["UPLOAD_SYS_USR_ID"] = strUserID;
            oOrderRow["UPLOAD_SYSTEM_IP"] = strMachineIP;
            oOrderRow["UPLOAD_SYSTEM_NAME"] = strMachineName;

            oDS.Tables["BULK_ACCOUNT_CREATION"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "BULK_ACCOUNT_CREATION");
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
           return  ex.Message.ToString();           
        }
        finally
        {
            conn.Close();
        }
    }
    // Author : Kowshik
    //Date    : 14-July-2015
    //Purposer: update status. 
    public string UpdateBulkAccStatus(string strValue)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE BULK_ACCOUNT_CREATION SET BULK_ACCNT_CRE_STATUS='E' WHERE BULK_ACCNT_CRE_ID='" + strValue + "'";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }
    //Author:  Kowshik.
    //Date  :  13-July-2015.
    //Purposer:Save Bulk Channel File Upload. 
    public string ExecuteBulkFile(string strFileID, string strFileName, string strUserID, string strMachineIP, string strMachineName)
    {
        string strMsg = "";
        string strSQL = "";
        string strAccNo = "", strCasPRSAccountID = "";
        int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        OracleDataAdapter oOrdersDataAdapter;
        DataRow dtrRow;
        OracleCommand OLEDBCmd;
        OracleTransaction dbTransaction = null;
        DataSet dsDuplicateAccnt = new DataSet();

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();

        string strGetExtensionName = "";
        strGetExtensionName = strFileName.Substring(strFileName.Length - 4);
        string ConnectionString = "";
        try
        {
            if (strGetExtensionName == ".xls")
            {
                ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + strFileName + "; Extended Properties=\"Excel 8.0;HDR=Yes\";";
            }
            else if (strGetExtensionName == "xlsx")
            {
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=\"Excel 12.0;HDR=YES\";";
            }
            //################################ connection string for excel sheet ############################### 
            OleDbConnection conExcell = new OleDbConnection(ConnectionString);
            conExcell.Open();
            string CommandText = "select * from [Sheet1$]";
            DataSet dtsBAccount = new DataSet();
            OleDbCommand OLEDBcmd = new OleDbCommand(CommandText, conExcell);
            OleDbDataAdapter oOrdersDataAdapter1 = new OleDbDataAdapter(OLEDBcmd);

            oOrdersDataAdapter1.Fill(dtsBAccount, "Sheet1");
            conExcell.Close();


            foreach (DataRow pRow in dtsBAccount.Tables["Sheet1"].Rows)
            {
                //-------------------------------------------
                OracleConnection connNew;
                OracleDataAdapter oOrdersDataAdapterNew;
                DataRow dtrRowNew;
                OracleCommand OLEDBCmdNew;
                OracleTransaction dbTransactionNew = null;
                DataSet dsDuplicateAccntNew = new DataSet();
                //#########################################
                connNew = new OracleConnection(strConString);
                connNew.Open();
                dbTransactionNew = connNew.BeginTransaction();
                //##########################################
                string strSQLQuery = "";
                strSQLQuery = "SELECT BULK_ACCNT_CRE_ID,BULK_ACC_NO,BULK_ACC_FORM_SL_NO,BULK_ACC_AGNT_NO FROM BULK_ACCOUNT_DETAIL";
                DataSet dstClientListNew = new DataSet();
                OracleDataAdapter adpClientListNew = new OracleDataAdapter(new OracleCommand(strSQLQuery, connNew, dbTransactionNew));
                OracleCommandBuilder cmbClientListNew = new OracleCommandBuilder(adpClientListNew);
                adpClientListNew.FillSchema(dstClientListNew, SchemaType.Source);
                DataTable dtpClientListNew = dstClientListNew.Tables["Table"];
                dtpClientListNew.TableName = "BULK_ACCOUNT_DETAIL";
                ///###################################################################

                if (pRow[0].ToString().Equals(""))
                {
                   // break;
                }
                //####################### Account checking in the system ###########################
                else
                {
                    OLEDBCmdNew = new OracleCommand();
                    OLEDBCmdNew.Connection = connNew; //Active Connection
                    OLEDBCmdNew.Transaction = dbTransactionNew;
                    //################################# Get new Client ID  ####################################
                    OLEDBCmdNew.Parameters.Add("ClientID", OracleType.VarChar, 20).Direction = ParameterDirection.ReturnValue;

                    //##################################################################
                    OLEDBCmdNew.CommandText = "PKG_APSNG_COMMON.GET_NEW_BLK_ACC_DTL_ID(0)"; // Stored Procedure to Call
                    OLEDBCmdNew.CommandType = CommandType.StoredProcedure; //Setup Command Type
                    OLEDBCmdNew.ExecuteNonQuery();
                    strCasPRSAccountID = OLEDBCmdNew.Parameters["ClientID"].Value.ToString();
                    //########################################################################
                    dtrRow = dstClientListNew.Tables["BULK_ACCOUNT_DETAIL"].NewRow();

                    //######################### Data insertion start here ###########################
                    dtrRow["BULK_ACCNT_CRE_ID"] = strFileID;

                    dtrRow["BULK_ACC_NO"] = pRow[0].ToString().Replace("'", "");
                    dtrRow["BULK_ACC_FORM_SL_NO"] = pRow[1].ToString().Replace("'", "");
                    dtrRow["BULK_ACC_AGNT_NO"] = pRow[2].ToString().Replace("'", "");

                    dstClientListNew.Tables["BULK_ACCOUNT_DETAIL"].Rows.Add(dtrRow);
                    adpClientListNew.Update(dstClientListNew, "BULK_ACCOUNT_DETAIL");
                    intCount++;
                    dbTransactionNew.Commit();
                    connNew.Close();
                }
            }
            //  dbTransaction = conn.BeginTransaction();
            OLEDBCmd = new OracleCommand();
            OLEDBCmd.Connection = conn; //Active Connection
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = " UPDATE BULK_ACCOUNT_CREATION SET BULK_ACCNT_CRE_STATUS='E',EXECUTE_SYSTEM_IP='" + strMachineIP + "',"
                                  + " EXECUTE_SYSTEM_NAME='" + strMachineName + "'  WHERE BULK_ACCNT_CRE_ID='" + strFileID + "' ";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception e)
        {
            dbTransaction.Rollback();
            strMsg = e.Message.ToString();
            conn.Close();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
        }
    }
    #endregion

    public string GetDistributorInfo(string strAccountNO)
    {
        OracleConnection conn = new OracleConnection(strConString);
        string strSql = " SELECT (AL.ACCNT_NO||', '|| CL.CLINT_NAME||',' || CL.CLINT_ADDRESS1 || ', ' || MT.THANA_NAME ||', ' || MD.DISTRICT_NAME) INFO_DTL "
            + " FROM ACCOUNT_LIST AL, CLIENT_LIST CL, MANAGE_THANA MT, MANAGE_DISTRICT MD WHERE "
            + " AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_NO = '" + strAccountNO + "' AND CL.THANA_ID = MT.THANA_ID(+) AND MT.DISTRICT_ID = MD.DISTRICT_ID(+)  ";
        string strReturnColumnValue = "";
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["INFO_DTL"].ToString() != "")
                    {
                        strReturnColumnValue = dr["INFO_DTL"].ToString();
                    }
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return strReturnColumnValue;
        }
        finally
        {
            conn.Close();
        }
    }


    #region for fis gl account balance

    public DataSet GetAssetSAccount(string strBranchType, string strBranchID, string strAccLevel, string strParentCode)
    {
        string strSql = "";
        if (!strAccLevel.Equals(""))
        {
            strSql = "WHERE ACC_LEVEL='" + strAccLevel + "'";
        }
        if (!strParentCode.Equals(""))
        {
            if (strSql.Equals(""))
            {
                strSql = "WHERE PARENT_CODE='" + strParentCode + "'";
            }
            else
            {
                strSql = strSql + "AND PARENT_CODE='" + strParentCode + "'";
            }
        }
        if (!strBranchType.Equals("All"))
        {
            if (strSql.Equals(""))
            {
                strSql = "WHERE CMP_BRANCH_ID  IN (" + strBranchID + ")";
            }
            else
            {
                strSql = strSql + " AND CMP_BRANCH_ID IN (" + strBranchID + ")";
            }
        }
        //change: Add ACC_INT_ID ID
        strSql = "SELECT ACC_INT_ID,ACC_ID,ACC_PREFIX,ACC_NAME,ACC_LEVEL,PARENT_CODE,IS_ACC,ACC_TYPE,IS_BANK FROM BDMIT_ERP_101.GL_CHART_OF_ACC " + strSql + " ORDER BY ACC_ID DESC";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "GL_CHART_OF_ACC");
            return oDS;
        }
        catch (Exception e)
        {
            strSql = e.Message.ToString();
            return null;
        }
    }


    public DataSet GetIncomeSAccount(string strBranchType, string strBranchID, string strAccLevel, string strParentCode)
    {
        string strSql = "";
        if (!strAccLevel.Equals(""))
        {
            strSql = "WHERE ACC_LEVEL='" + strAccLevel + "'";
        }
        if (!strParentCode.Equals(""))
        {
            if (strSql.Equals(""))
            {
                strSql = "WHERE ACC_ID IN ('300000000','400000000') AND PARENT_CODE='" + strParentCode + "'";
            }
            else
            {
                strSql = strSql + "AND ACC_ID IN ('300000000','400000000') AND PARENT_CODE='" + strParentCode + "'";
            }
        }
        if (!strBranchType.Equals("All"))
        {
            if (strSql.Equals(""))
            {
                strSql = "WHERE ACC_ID IN ('300000000','400000000') AND CMP_BRANCH_ID  IN (" + strBranchID + ")";
            }
            else
            {
                strSql = strSql + " AND ACC_ID IN ('300000000','400000000') AND CMP_BRANCH_ID IN (" + strBranchID + ")";
            }
        }
        //change: Add ACC_INT_ID ID
        strSql = "SELECT ACC_INT_ID,ACC_ID,ACC_PREFIX,ACC_NAME,ACC_LEVEL,PARENT_CODE,IS_ACC,ACC_TYPE,IS_BANK FROM BDMIT_ERP_101.GL_CHART_OF_ACC " + strSql + " ORDER BY ACC_ID";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "GL_CHART_OF_ACC");
            return oDS;
        }
        catch (Exception e)
        {
            strSql = e.Message.ToString();
            return null;
        }
    }

    public DataSet GetChildAccount(string strBranchType, string strBranchID, string strParentAcc)
    {
        string strSql;
        string strBranch = "";
        if (!strBranchType.Equals("All"))
        {
            strBranch = " AND CMP_BRANCH_ID IN (" + strBranchID + ")";
        }
        strSql = "SELECT ACC_INT_ID,ACC_ID,ACC_PREFIX,ACC_NAME,ACC_LEVEL,IS_ACC,ACC_TYPE  FROM BDMIT_ERP_101.GL_CHART_OF_ACC WHERE PARENT_CODE='" + strParentAcc + "' " + strBranch + " ORDER BY ACC_ID";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "GL_CHART_OF_ACC");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
    }




    public DataSet GetAccOpeningBalance(string strBranchType, string strBranchID, string strYearID, string strAccountNo)
    {
        string strSql = "";

        if (strBranchType.Equals("A"))
        {
            strSql = "SELECT G.GL_ACC_YEAR_ID,G.ACC_ID,G.ACC_TYPE,SUM(NVL(G.OPENING_BAL,0)) OPENING_BAL, SUM(NVL(G.CURRENT_BAL,0)) CURRENT_BAL, SUM(NVL(G.ACC_LIMIT,0)) ACC_LIMIT "
               + "FROM BDMIT_ERP_101.VW_GL_OPENING_BALANCE G WHERE G.GL_ACC_YEAR_ID='" + strYearID + "' AND G.ACC_ID='" + strAccountNo + "' "
               + "GROUP BY G.GL_ACC_YEAR_ID,G.ACC_ID,G.ACC_TYPE";
        }
        else
        {
            strSql = "SELECT G.GL_ACC_YEAR_ID,G.ACC_ID,G.ACC_TYPE,SUM(NVL(G.OPENING_BAL,0)) OPENING_BAL, SUM(NVL(G.CURRENT_BAL,0)) CURRENT_BAL, SUM(NVL(G.ACC_LIMIT,0)) ACC_LIMIT "
               + "FROM BDMIT_ERP_101.VW_GL_OPENING_BALANCE G WHERE G.CMP_BRANCH_ID='" + strBranchID + "' AND G.GL_ACC_YEAR_ID='" + strYearID + "' AND G.ACC_ID='" + strAccountNo + "' "
               + "GROUP BY G.GL_ACC_YEAR_ID,G.ACC_ID,G.ACC_TYPE";

        }
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "OPENING_BALANCE");
            return oDS;
        }
        catch (Exception e)
        {
            strSql = e.Message.ToString();
            return null;
        }
    }


    public DataSet GetAccTranAmount(string strBranchType, string strBranchID, string strAccountNo, string strAsoffDate, string strAccYearID)
    {
        string strSql = "";

        if (strBranchType.Equals("A"))
        {
            strSql = "SELECT DISTINCT D.ACC_ID,SUM(DECODE(D.TRAN_TYPE,'D',D.TRAN_AMT,0)) DEBIT, SUM(DECODE(D.TRAN_TYPE,'C',D.TRAN_AMT,0)) CREDIT "
                  + "FROM BDMIT_ERP_101.GL_DAILY_TRAN D, BDMIT_ERP_101.GL_VOUCHER_MASTER V WHERE D.VOUCHER_NO=V.VOUCHER_NO "
                  + "AND D.ACC_ID='" + strAccountNo + "' and D.TRAN_DATE<='" + strAsoffDate + "' and V.GL_ACC_YEAR_ID='" + strAccYearID + "' GROUP BY D.ACC_ID";

        }
        else
        {
            strSql = "SELECT DISTINCT G.ACC_ID,SUM(NVL(G.DEBIT,0)) DEBIT, SUM(NVL(G.CREDIT,0)) CREDIT "
                   + "FROM BDMIT_ERP_101.VW_GL_DAILY_TRAN G WHERE G.CMP_BRANCH_ID='" + strBranchID + "' AND G.ACC_ID='" + strAccountNo + "' "
                   + "and G.TRAN_DATE<='" + strAsoffDate + "' and G.GL_ACC_YEAR_ID='" + strAccYearID + "' GROUP BY G.ACC_ID";

            strSql = "SELECT DISTINCT D.ACC_ID,SUM(DECODE(D.TRAN_TYPE,'D',D.TRAN_AMT,0)) DEBIT, SUM(DECODE(D.TRAN_TYPE,'C',D.TRAN_AMT,0)) CREDIT "
                   + "FROM BDMIT_ERP_101.GL_DAILY_TRAN D, BDMIT_ERP_101.GL_VOUCHER_MASTER V WHERE D.VOUCHER_NO=V.VOUCHER_NO AND D.CMP_BRANCH_ID='" + strBranchID + "' "
                   + "AND D.ACC_ID='" + strAccountNo + "' and D.TRAN_DATE<='" + strAsoffDate + "' and V.GL_ACC_YEAR_ID='" + strAccYearID + "' GROUP BY D.ACC_ID";

        }

        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "DAILY_TRAN");
            return oDS;
        }
        catch (Exception e)
        {
            strSql = e.Message.ToString();
            return null;
        }
    }


    #endregion 

    #region territory informatoion

    public string GettingAccountRankId(string strAccountNo)
    {
        string strSql = "";
        strSql = " SELECT ACCNT_RANK_ID FROM ACCOUNT_LIST WHERE ACCNT_NO LIKE '%" + strAccountNo + "%'";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["ACCNT_RANK_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string UpdateTerritoryRank(string strAccountNo, string strTerritoryRankId)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE ACCOUNT_LIST SET TERRITORY_RANK_ID = '" + strTerritoryRankId + "' WHERE ACCNT_NO = '" + strAccountNo + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string AddToTerritoryRegion(string strRegionName)
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
            strSql = "SELECT REGION_NAME FROM MANAGE_REGION ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "MANAGE_REGION";
            oOrderRow = oDS.Tables["MANAGE_REGION"].NewRow();

            oOrderRow["REGION_NAME"] = strRegionName;

            oDS.Tables["MANAGE_REGION"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "MANAGE_REGION");
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
            conn = null;
        }
    }

    public string UpdateTerritoryRegion(string strRegionId, string strTerritoryRegionName)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_REGION SET REGION_NAME = '" + strTerritoryRegionName + "' WHERE REGION_ID = '" + strRegionId + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string UpdateAreaTaggedWithRegion(string strRegionId, string strAreaId)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_AREA SET REGION_ID = '" + strRegionId + "' WHERE AREA_ID = '" + strAreaId + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string RegionIdIdIfExist(string strRegionId)
    {
        string strSql = "";

        strSql = " SELECT REGION_ID FROM MANAGE_AREA WHERE REGION_ID = '" + strRegionId + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["REGION_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string DeleteFromManageRegion(string strId)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn; //Active Connection
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = "DELETE FROM MANAGE_REGION WHERE REGION_ID='" + strId + "'";
            OracleCmd.CommandType = CommandType.Text; //Setup Command Type
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            conn = null;
            return "Successful.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            conn.Close();
            conn = null;
            return "Unsuccessfull.";
        }
    }

    public string UpdateThanaTaggedWithArea(string strAreaId, string strThanaId)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_THANA SET AREA_ID = '" + strAreaId + "' WHERE THANA_ID = '" + strThanaId + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string UpdateThanaTaggedWithAreaNew(string strAreaId, string strThanaId)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_THANA SET AREA_ID = '" + strAreaId + "' , TAGGED_WITH_TERRI_AREA = 'T' WHERE THANA_ID = '" + strThanaId + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string AddToTerritoryArea(string strAreaName)
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
            strSql = "SELECT AREA_NAME FROM MANAGE_AREA ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "MANAGE_AREA";
            oOrderRow = oDS.Tables["MANAGE_AREA"].NewRow();

            oOrderRow["AREA_NAME"] = strAreaName;

            oDS.Tables["MANAGE_AREA"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "MANAGE_AREA");
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
            conn = null;
        }
    }

    public string AreaIdIdIfExist(string strAreaId)
    {
        string strSql = "";

        strSql = " SELECT AREA_ID FROM MANAGE_THANA WHERE AREA_ID = '" + strAreaId + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["AREA_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string DeleteFromManageArea(string strId)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn; //Active Connection
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = "DELETE FROM MANAGE_AREA WHERE AREA_ID='" + strId + "'";
            OracleCmd.CommandType = CommandType.Text; //Setup Command Type
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            conn = null;
            return "Successful.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            conn.Close();
            conn = null;
            return "Unsuccessfull.";
        }
    }

    public string UpdateTerritoryArea(string strAreaId, string strTerritoryAreaName)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_AREA SET AREA_NAME = '" + strTerritoryAreaName + "' WHERE AREA_ID = '" + strAreaId + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string TOHierarchyIdIfExist(string strAccountNo)
    {
        string strSql = "";
        //strSql = " SELECT ISO_PRO_CODE_ID FROM ISO_PROCESSING_CODE WHERE BANK_ID = '" + strBankId + "' AND SERVICE_ID = '" + strServiceId + "' ";
        strSql = " SELECT MT.HIERARCHY_ACCNT_ID FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_HIERARCHY MT "
                                    + " WHERE AL.TERRITORY_RANK_ID = '150121000000000002' AND AL.ACCNT_NO = '" + strAccountNo + "' "
                                    + " AND AL.ACCNT_ID = MT.ACCNT_ID ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["HIERARCHY_ACCNT_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string TerritoryAccountIdIfExist(string strAccountId)
    {
        string strSql = "";

        strSql = " SELECT ACCNT_ID FROM MANAGE_TERRITORY_HIERARCHY WHERE ACCNT_ID = '" + strAccountId + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["ACCNT_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string GettingTerritoryRankId(string strAccountNo)
    {
        string strSql = "";
        strSql = " SELECT TERRITORY_RANK_ID FROM ACCOUNT_LIST WHERE ACCNT_NO LIKE '%" + strAccountNo + "%'";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["TERRITORY_RANK_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string UpdateTerritoryHierarchyByAccId(string strAccountId, string strParentAccId, string strUserId)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_TERRITORY_HIERARCHY SET HIERARCHY_ACCNT_ID = '" + strParentAccId + "' , UPDATED_BY = '" + strUserId + "' "
                       + " , UPDATE_DATE = TO_DATE ('" + DateTime.Now + "', 'MM-DD-YYYY HH12:MI:SS AM') WHERE ACCNT_ID = '" + strAccountId + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string AddToTerritoryHierarchy(string strAccid, string strParentAccid, string strUpdatedBy)
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
            strSql = "SELECT ACCNT_ID, HIERARCHY_ACCNT_ID, UPDATED_BY, UPDATE_DATE FROM MANAGE_TERRITORY_HIERARCHY ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "MANAGE_TERRITORY_HIERARCHY";
            oOrderRow = oDS.Tables["MANAGE_TERRITORY_HIERARCHY"].NewRow();

            oOrderRow["ACCNT_ID"] = strAccid;
            oOrderRow["HIERARCHY_ACCNT_ID"] = strParentAccid;
            oOrderRow["UPDATED_BY"] = strUpdatedBy;
            oOrderRow["UPDATE_DATE"] = DateTime.Now;

            oDS.Tables["MANAGE_TERRITORY_HIERARCHY"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "MANAGE_TERRITORY_HIERARCHY");
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
            conn = null;
        }
    }

    public string TerritoryAreaIdIfExist(string strAreaId)
    {
        string strSql = "";

        strSql = " SELECT AREA_ID FROM MANAGE_TERRITORY_AREA WHERE AREA_ID = '" + strAreaId + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["AREA_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string UpdateTMTaggedWithArea(string strAreaId, string strAccId)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_TERRITORY_AREA SET ACCNT_ID = '" + strAccId + "' WHERE AREA_ID = '" + strAreaId + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string AddToManageTerritoryArea(string strAccid, string strAreaid)
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
            strSql = "SELECT AREA_ID, ACCNT_ID FROM MANAGE_TERRITORY_AREA ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "MANAGE_TERRITORY_AREA";
            oOrderRow = oDS.Tables["MANAGE_TERRITORY_AREA"].NewRow();

            oOrderRow["AREA_ID"] = strAreaid;
            oOrderRow["ACCNT_ID"] = strAccid;

            oDS.Tables["MANAGE_TERRITORY_AREA"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "MANAGE_TERRITORY_AREA");
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
            conn = null;
        }
    }

    public string AddToManageKPiTarget(string strAccId, int intCustAcqTarget,
        int intTargetAmoumt, int intActiveAgentno, int intCorpCollAmount,
                   int intLiftingTarget, int intAgentTrxAmount, int intUtilityAmtTarget, string strMonth, string strYear, string strRemarks, string strUtArea, string createdBy)
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
            strSql = " SELECT TO_ACCNT_ID,CUST_ACQU_TARGET, TRX_AMT_TARGET,ACTIVE_AGENTNO_TARGET, CORP_COLLECTION_TARGET, TARGET_YEAR, TARGET_MONTH,REMARKS,LIFTING_AMOUNT_TARGET,ACTIVE_AGENT_TRXAMT_TARGET,CREATED_BY,UT_AREA,UTILITY_AMOUNT_TARGET  FROM MANAGE_KPI_TARGET ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "MANAGE_KPI_TARGET";
            oOrderRow = oDS.Tables["MANAGE_KPI_TARGET"].NewRow();

            oOrderRow["TO_ACCNT_ID"] = strAccId;
            oOrderRow["CUST_ACQU_TARGET"] = intCustAcqTarget;
            //  oOrderRow["DPS_ACC_ACQU_TARGET"] = intDpsAcqTarget;
            oOrderRow["TRX_AMT_TARGET"] = intTargetAmoumt;
            oOrderRow["ACTIVE_AGENTNO_TARGET"] = intActiveAgentno;
            oOrderRow["ACTIVE_AGENT_TRXAMT_TARGET"] = intAgentTrxAmount;
            oOrderRow["CORP_COLLECTION_TARGET"] = intCorpCollAmount;
            // oOrderRow["COMPLIANCE_TARGET"] = intComplianceTrget;
            // oOrderRow["VISIBILITY_TARGET"] = intVisibilityTarget;
            oOrderRow["TARGET_YEAR"] = strYear;
            oOrderRow["TARGET_MONTH"] = strMonth;
            oOrderRow["LIFTING_AMOUNT_TARGET"] = intLiftingTarget;
            oOrderRow["REMARKS"] = strRemarks;
            oOrderRow["UT_AREA"] = strUtArea;
            oOrderRow["CREATED_BY"] = createdBy;
            oOrderRow["UTILITY_AMOUNT_TARGET"] = intUtilityAmtTarget;

            oDS.Tables["MANAGE_KPI_TARGET"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "MANAGE_KPI_TARGET");
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
            conn = null;
        }
    }

    public string UpdateTOIndividualMonthlyTarget(string strid, int strCustAcq,
               int strTrxAmt, int strAgtNo, int strCorpColl, int strLiftingTarget, string strRmks, int strUtilityAmt, int strAgtTrxAmt)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_KPI_TARGET MT SET MT.CUST_ACQU_TARGET = '" + strCustAcq + "',  MT.TRX_AMT_TARGET = '" + strTrxAmt + "', MT.ACTIVE_AGENTNO_TARGET = '" + strAgtNo + "',  MT.CORP_COLLECTION_TARGET = '" + strCorpColl + "',  MT.LIFTING_AMOUNT_TARGET = '" + strLiftingTarget + "',ACTIVE_AGENT_TRXAMT_TARGET='" + strAgtTrxAmt + "', MT.UTILITY_AMOUNT_TARGET='" + strUtilityAmt + "', MT.REMARKS = '" + strRmks + "' WHERE MT.KPI_TARGET_ID = '" + strid + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }


    public string TerritoryAreaNameIfExist(string strAreaName)
    {
        string strSql = "";

        strSql = " SELECT AREA_NAME FROM MANAGE_AREA WHERE AREA_NAME = '" + strAreaName + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["AREA_NAME"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string TerritoryRegionNameIfExist(string strRegionName)
    {
        string strSql = "";

        strSql = " SELECT REGION_NAME FROM MANAGE_REGION WHERE REGION_NAME = '" + strRegionName + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["REGION_NAME"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string UpdateAreaTaggedWithRegionaAdd(string strRegionId, string strAreaId)
    {
        string updateString = "", strReturn = "";
        updateString = " UPDATE MANAGE_AREA SET REGION_ID = '" + strRegionId + "', TAGGED_WITH_TERRI_REG = 'T' WHERE AREA_ID = '" + strAreaId + "' ";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCommand cmd = new OracleCommand(updateString, conn, dbTransaction);
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            strReturn = ex.Message.ToString();
            return strReturn;
        }
        finally
        {
            conn.Close();
        }
    }

    public string GetAccountIdByMobileNo(string strMobileNo)
    {
        string strAccMSISDNReturn = "";
        string strSql = " SELECT ACCNT_ID FROM ACCOUNT_LIST WHERE ACCNT_NO LIKE ('%" + strMobileNo + "%')";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strAccMSISDNReturn = dr["ACCNT_ID"].ToString();
                }
                conn.Close();
                return strAccMSISDNReturn;
            }
            else
            {
                conn.Close();
                return strAccMSISDNReturn;
            }

        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }

    public string GetComplianceTarget(string strSqlCompliance)
    {
        string strAccMSISDNReturn = "";
        string strSql = strSqlCompliance;
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strAccMSISDNReturn = dr["ACCNT_ID"].ToString();
                }
                conn.Close();
                return strAccMSISDNReturn;
            }
            else
            {
                conn.Close();
                return strAccMSISDNReturn;
            }

        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }



    #endregion


    #region 

    public string UpdateBillTrxIdCancelToReverse(string strUtlTrxId)
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

            OracleCmd.CommandText = " UPDATE UTILITY_TRANSACTION SET TRANSACTION_STATUS = 'D', FINAL_STATUS = 'N', REVERSE_STATUS = 'R', RESPONSE_MSG_BP = 'REVERSE', RESPONSE_STATUS_BP = '789', "
                                    + " CHECK_STATUS = 'Y' WHERE UTILITY_TRAN_ID = '" + strUtlTrxId + "' ";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
    }

    #endregion 

    #region
    #endregion
	/// <summary>
/// Author: Tanjil Alam; Date:13-Feb-2017; Purpose: Check is NID already tagged to another account or not.    
/// </summary>
/// <param name="strSql"></param>
/// <returns></returns>
    public DataSet CheckNID(string strSql)
    {
        try
        {
            DataSet oDS = new DataSet();
            OracleConnection conn = new OracleConnection(strConString);
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CLIENT_IDENTIFICATION");
            return oDS;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    #region bank service fee new

    public string AddBankServiceFee(string strServiceId,
        string strBankServiceFeeTitle,
        string strStartAmount,
        string strMaxAmount,
        string strBankServiceFeeAmount,
        string strVatTAx,
        string strTaxPaidBy,
        string strFeeIncludeVatTax,
        string strAccountRankId,
        string strFeePaidByBank,
        string strFeePaidByInitiator,
        string strAit,
        string strAgtCommAmount,
        string strBankCommiAmount,
        string strFeePaidBy,
        string strPoolAdjustMent,
        string strVendorCommission,
        string str3RdPartyCommission,
        string strChannelCommission,
        string strChannelTypeId,
        string strFeesPaidByAgent,
        string strMinFee,
        string strCmpBranchId,
        string strBankCode,
        string strAgentCommonOperatorCommission,
        string strAgtOperatorCommiId)
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
            strSql = " SELECT SERVICE_ID, BANK_SRV_FEE_TITLE, START_AMOUNT, MAX_AMOUNT, BANK_SRV_FEE_AMOUNT, VAT_TAX, TAX_PAID_BY, "
                    + " FEE_INCLUDE_VAT_TAX, ACCNT_RANK_ID, FEES_PAID_BY_BANK, FEES_PAID_BY_CUSTOMER, AIT, AGENT_COMM_AMOUNT, "
                    + " BANK_COMM_AMOUNT, FEES_PAID_BY, POOL_ADJUSTMENT, VENDOR_COMMISSION, THIRD_PARTY_COMMISSION, CHANNEL_COMMISSION, "
                    + " CHANNEL_TYPE_ID, FEES_PAID_BY_AGENT, MIN_FEE, CMP_BRANCH_ID, BANK_CODE, AGENT_OPERATOR_COMI, AGENT_OPARETOR_COMI_ID  FROM BANK_SERVICE_FEE ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "BANK_SERVICE_FEE";
            oOrderRow = oDS.Tables["BANK_SERVICE_FEE"].NewRow();

            oOrderRow["SERVICE_ID"] = strServiceId;
            oOrderRow["BANK_SRV_FEE_TITLE"] = strBankServiceFeeTitle;
            oOrderRow["START_AMOUNT"] = strStartAmount;

            oOrderRow["MAX_AMOUNT"] = strMaxAmount;
            oOrderRow["BANK_SRV_FEE_AMOUNT"] = strBankServiceFeeAmount;
            oOrderRow["VAT_TAX"] = strVatTAx;

            oOrderRow["TAX_PAID_BY"] = strTaxPaidBy;
            oOrderRow["FEE_INCLUDE_VAT_TAX"] = strFeeIncludeVatTax;
            oOrderRow["ACCNT_RANK_ID"] = strAccountRankId;

            oOrderRow["FEES_PAID_BY_BANK"] = strFeePaidByBank;
            oOrderRow["FEES_PAID_BY_CUSTOMER"] = strFeePaidByInitiator;
            oOrderRow["AIT"] = strAit;

            oOrderRow["AGENT_COMM_AMOUNT"] = strAgtCommAmount;
            oOrderRow["BANK_COMM_AMOUNT"] = strBankCommiAmount;
            oOrderRow["FEES_PAID_BY"] = strFeePaidBy;

            oOrderRow["POOL_ADJUSTMENT"] = strPoolAdjustMent;
            oOrderRow["VENDOR_COMMISSION"] = strVendorCommission;
            oOrderRow["THIRD_PARTY_COMMISSION"] = str3RdPartyCommission;

            oOrderRow["CHANNEL_COMMISSION"] = strChannelCommission;
            oOrderRow["CHANNEL_TYPE_ID"] = strChannelTypeId;
            oOrderRow["FEES_PAID_BY_AGENT"] = strFeesPaidByAgent;

            oOrderRow["MIN_FEE"] = strMinFee;
            oOrderRow["CMP_BRANCH_ID"] = strCmpBranchId;
            oOrderRow["BANK_CODE"] = strBankCode;

            oOrderRow["AGENT_OPERATOR_COMI"] = strAgentCommonOperatorCommission;
            oOrderRow["AGENT_OPARETOR_COMI_ID"] = strAgtOperatorCommiId;

            oDS.Tables["BANK_SERVICE_FEE"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "BANK_SERVICE_FEE");
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
            conn = null;
        }
    }

    //Added by Rokon # Date : 21-Oct-2017 # Added parameter sub service code
    public string AddBankServiceFee2(string strServiceId,
        string strBankServiceFeeTitle,
        string strStartAmount,
        string strMaxAmount,
        string strBankServiceFeeAmount,
        string strVatTAx,
        string strTaxPaidBy,
        string strFeeIncludeVatTax,
        string strAccountRankId,
        string strFeePaidByBank,
        string strFeePaidByInitiator,
        string strAit,
        string strAgtCommAmount,
        string strBankCommiAmount,
        string strFeePaidBy,
        string strPoolAdjustMent,
        string strVendorCommission,
        string str3RdPartyCommission,
        string strChannelCommission,
        string strChannelTypeId,
        string strFeesPaidByAgent,
        string strMinFee,
        string strCmpBranchId,
        string strBankCode,
        string strAgentCommonOperatorCommission,
        string strAgtOperatorCommiId,
        string strSubServiceCode)
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
            strSql = " SELECT SERVICE_ID, BANK_SRV_FEE_TITLE, START_AMOUNT, MAX_AMOUNT, BANK_SRV_FEE_AMOUNT, VAT_TAX, TAX_PAID_BY, "
                    + " FEE_INCLUDE_VAT_TAX, ACCNT_RANK_ID, FEES_PAID_BY_BANK, FEES_PAID_BY_CUSTOMER, AIT, AGENT_COMM_AMOUNT, "
                    + " BANK_COMM_AMOUNT, FEES_PAID_BY, POOL_ADJUSTMENT, VENDOR_COMMISSION, THIRD_PARTY_COMMISSION, CHANNEL_COMMISSION, "
                    + " CHANNEL_TYPE_ID, FEES_PAID_BY_AGENT, MIN_FEE, CMP_BRANCH_ID, BANK_CODE, AGENT_OPERATOR_COMI, AGENT_OPARETOR_COMI_ID, "
                    + " SUB_SERVICE_CODE FROM BANK_SERVICE_FEE ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "BANK_SERVICE_FEE";
            oOrderRow = oDS.Tables["BANK_SERVICE_FEE"].NewRow();

            oOrderRow["SERVICE_ID"] = strServiceId;
            oOrderRow["BANK_SRV_FEE_TITLE"] = strBankServiceFeeTitle;
            oOrderRow["START_AMOUNT"] = strStartAmount;

            oOrderRow["MAX_AMOUNT"] = strMaxAmount;
            oOrderRow["BANK_SRV_FEE_AMOUNT"] = strBankServiceFeeAmount;
            oOrderRow["VAT_TAX"] = strVatTAx;

            oOrderRow["TAX_PAID_BY"] = strTaxPaidBy;
            oOrderRow["FEE_INCLUDE_VAT_TAX"] = strFeeIncludeVatTax;
            oOrderRow["ACCNT_RANK_ID"] = strAccountRankId;

            oOrderRow["FEES_PAID_BY_BANK"] = strFeePaidByBank;
            oOrderRow["FEES_PAID_BY_CUSTOMER"] = strFeePaidByInitiator;
            oOrderRow["AIT"] = strAit;

            oOrderRow["AGENT_COMM_AMOUNT"] = strAgtCommAmount;
            oOrderRow["BANK_COMM_AMOUNT"] = strBankCommiAmount;
            oOrderRow["FEES_PAID_BY"] = strFeePaidBy;

            oOrderRow["POOL_ADJUSTMENT"] = strPoolAdjustMent;
            oOrderRow["VENDOR_COMMISSION"] = strVendorCommission;
            oOrderRow["THIRD_PARTY_COMMISSION"] = str3RdPartyCommission;

            oOrderRow["CHANNEL_COMMISSION"] = strChannelCommission;
            oOrderRow["CHANNEL_TYPE_ID"] = strChannelTypeId;
            oOrderRow["FEES_PAID_BY_AGENT"] = strFeesPaidByAgent;

            oOrderRow["MIN_FEE"] = strMinFee;
            oOrderRow["CMP_BRANCH_ID"] = strCmpBranchId;
            oOrderRow["BANK_CODE"] = strBankCode;

            oOrderRow["AGENT_OPERATOR_COMI"] = strAgentCommonOperatorCommission;
            oOrderRow["AGENT_OPARETOR_COMI_ID"] = strAgtOperatorCommiId;

            oOrderRow["SUB_SERVICE_CODE"] = strSubServiceCode;

            oDS.Tables["BANK_SERVICE_FEE"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "BANK_SERVICE_FEE");
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
            conn = null;
        }
    }
	
	//Added by Rokon # Date : 21-Oct-2017 # Added parameter sub service code with extra parameter
    public string AddBankServiceFee2(string strServiceId,
        string strBankServiceFeeTitle,
        string strStartAmount,
        string strMaxAmount,
        string strBankServiceFeeAmount,
        string strVatTAx,
        string strTaxPaidBy,
        string strFeeIncludeVatTax,
        string strAccountRankId,
        string strFeePaidByBank,
        string strFeePaidByInitiator,
        string strAit,
        string strAgtCommAmount,
        string strBankCommiAmount,
        string strFeePaidBy,
        string strPoolAdjustMent,
        string strVendorCommission,
        string str3RdPartyCommission,
        string strChannelCommission,
        string strChannelTypeId,
        string strFeesPaidByAgent,
        string strMinFee,
        string strCmpBranchId,
        string strBankCode,
        string strAgentCommonOperatorCommission,
        string strAgtOperatorCommiId,
        string strSubServiceCode,
        string strSubWalletNumber)
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
            strSql = " SELECT SERVICE_ID, BANK_SRV_FEE_TITLE, START_AMOUNT, MAX_AMOUNT, BANK_SRV_FEE_AMOUNT, VAT_TAX, TAX_PAID_BY, "
                    + " FEE_INCLUDE_VAT_TAX, ACCNT_RANK_ID, FEES_PAID_BY_BANK, FEES_PAID_BY_CUSTOMER, AIT, AGENT_COMM_AMOUNT, "
                    + " BANK_COMM_AMOUNT, FEES_PAID_BY, POOL_ADJUSTMENT, VENDOR_COMMISSION, THIRD_PARTY_COMMISSION, CHANNEL_COMMISSION, "
                    + " CHANNEL_TYPE_ID, FEES_PAID_BY_AGENT, MIN_FEE, CMP_BRANCH_ID, BANK_CODE, AGENT_OPERATOR_COMI, AGENT_OPARETOR_COMI_ID, "
                    + " SUB_SERVICE_CODE, SUB_WALLET_NUMBER FROM BANK_SERVICE_FEE ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "BANK_SERVICE_FEE";
            oOrderRow = oDS.Tables["BANK_SERVICE_FEE"].NewRow();

            oOrderRow["SERVICE_ID"] = strServiceId;
            oOrderRow["BANK_SRV_FEE_TITLE"] = strBankServiceFeeTitle;
            oOrderRow["START_AMOUNT"] = strStartAmount;

            oOrderRow["MAX_AMOUNT"] = strMaxAmount;
            oOrderRow["BANK_SRV_FEE_AMOUNT"] = strBankServiceFeeAmount;
            oOrderRow["VAT_TAX"] = strVatTAx;

            oOrderRow["TAX_PAID_BY"] = strTaxPaidBy;
            oOrderRow["FEE_INCLUDE_VAT_TAX"] = strFeeIncludeVatTax;
            oOrderRow["ACCNT_RANK_ID"] = strAccountRankId;

            oOrderRow["FEES_PAID_BY_BANK"] = strFeePaidByBank;
            oOrderRow["FEES_PAID_BY_CUSTOMER"] = strFeePaidByInitiator;
            oOrderRow["AIT"] = strAit;

            oOrderRow["AGENT_COMM_AMOUNT"] = strAgtCommAmount;
            oOrderRow["BANK_COMM_AMOUNT"] = strBankCommiAmount;
            oOrderRow["FEES_PAID_BY"] = strFeePaidBy;

            oOrderRow["POOL_ADJUSTMENT"] = strPoolAdjustMent;
            oOrderRow["VENDOR_COMMISSION"] = strVendorCommission;
            oOrderRow["THIRD_PARTY_COMMISSION"] = str3RdPartyCommission;

            oOrderRow["CHANNEL_COMMISSION"] = strChannelCommission;
            oOrderRow["CHANNEL_TYPE_ID"] = strChannelTypeId;
            oOrderRow["FEES_PAID_BY_AGENT"] = strFeesPaidByAgent;

            oOrderRow["MIN_FEE"] = strMinFee;
            oOrderRow["CMP_BRANCH_ID"] = strCmpBranchId;
            oOrderRow["BANK_CODE"] = strBankCode;

            oOrderRow["AGENT_OPERATOR_COMI"] = strAgentCommonOperatorCommission;
            oOrderRow["AGENT_OPARETOR_COMI_ID"] = strAgtOperatorCommiId;

            oOrderRow["SUB_SERVICE_CODE"] = strSubServiceCode;
            oOrderRow["SUB_WALLET_NUMBER"] = strSubWalletNumber;

            oDS.Tables["BANK_SERVICE_FEE"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "BANK_SERVICE_FEE");
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
            conn = null;
        }
    }

    public string UpdateServiceFeeNew(string strServiceFeeId, string strServiceId, string strServiceFeeName, string strStartAmount,
                 string strMaxAmount, string strBankSErvFeeAmount, string strMinimumFee, string strVatTax, string strAit,
                  string strFeesPaidByBank, string strFeesPaidByInitiator, string strFeesPaidByReceipent, string strBankCommission,
                   string strAgentCommission, string strPoolAdjustment, string strVendorCommission, string strThirdPartyCommission,
                   string strChannelCommission, string strCommonOperatorCommission, string strAgentOperatorCommissionId,
                 string strTaxPaidBy, string strFeesPaidBy, string strFeesIncludeVatTax)
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

            OracleCmd.CommandText = @"UPDATE BANK_SERVICE_FEE SET BANK_SRV_FEE_TITLE = '" + strServiceFeeName + "', START_AMOUNT = '" + strStartAmount + "', "
                                    + " MAX_AMOUNT = '" + strMaxAmount + "', BANK_SRV_FEE_AMOUNT = '" + strBankSErvFeeAmount + "', MIN_FEE = '" + strMinimumFee + "', "
                                    + " VAT_TAX = '" + strVatTax + "',  AIT = '" + strAit + "', FEES_PAID_BY_BANK = '" + strFeesPaidByBank + "', "
                                    + " FEES_PAID_BY_CUSTOMER = '" + strFeesPaidByInitiator + "',  FEES_PAID_BY_AGENT = '" + strFeesPaidByReceipent + "', "
                                    + " BANK_COMM_AMOUNT = '" + strBankCommission + "', AGENT_COMM_AMOUNT = '" + strAgentCommission + "', "
                                    + " POOL_ADJUSTMENT = '" + strPoolAdjustment + "', VENDOR_COMMISSION = '" + strVendorCommission + "', "
                                    + " THIRD_PARTY_COMMISSION = '" + strThirdPartyCommission + "', CHANNEL_COMMISSION = '" + strChannelCommission + "', "
                                    + " AGENT_OPERATOR_COMI = '" + strCommonOperatorCommission + "', AGENT_OPARETOR_COMI_ID = '" + strAgentOperatorCommissionId + "', "
                                    + " TAX_PAID_BY = '" + strTaxPaidBy + "', FEES_PAID_BY = '" + strFeesPaidBy + "', FEE_INCLUDE_VAT_TAX = '" + strFeesIncludeVatTax + "' "
                                    + " WHERE BANK_SRV_FEE_ID = '" + strServiceFeeId + "' AND SERVICE_ID = '" + strServiceId + "'  ";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
            conn = null;

        }
    }

    // Added By Rokon # Date : 21-Oct-2017 # Added Sub Service Code
    public string UpdateServiceFeeNew2(string strServiceFeeId, string strServiceId, string strServiceFeeName, string strStartAmount,
                 string strMaxAmount, string strBankSErvFeeAmount, string strMinimumFee, string strVatTax, string strAit,
                  string strFeesPaidByBank, string strFeesPaidByInitiator, string strFeesPaidByReceipent, string strBankCommission,
                   string strAgentCommission, string strPoolAdjustment, string strVendorCommission, string strThirdPartyCommission,
                   string strChannelCommission, string strCommonOperatorCommission, string strAgentOperatorCommissionId,
                 string strTaxPaidBy, string strFeesPaidBy, string strFeesIncludeVatTax, string strSubServiceCode)
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

            OracleCmd.CommandText = @"UPDATE BANK_SERVICE_FEE SET BANK_SRV_FEE_TITLE = '" + strServiceFeeName + "', START_AMOUNT = '" + strStartAmount + "', "
                                    + " MAX_AMOUNT = '" + strMaxAmount + "', BANK_SRV_FEE_AMOUNT = '" + strBankSErvFeeAmount + "', MIN_FEE = '" + strMinimumFee + "', "
                                    + " VAT_TAX = '" + strVatTax + "',  AIT = '" + strAit + "', FEES_PAID_BY_BANK = '" + strFeesPaidByBank + "', "
                                    + " FEES_PAID_BY_CUSTOMER = '" + strFeesPaidByInitiator + "',  FEES_PAID_BY_AGENT = '" + strFeesPaidByReceipent + "', "
                                    + " BANK_COMM_AMOUNT = '" + strBankCommission + "', AGENT_COMM_AMOUNT = '" + strAgentCommission + "', "
                                    + " POOL_ADJUSTMENT = '" + strPoolAdjustment + "', VENDOR_COMMISSION = '" + strVendorCommission + "', "
                                    + " THIRD_PARTY_COMMISSION = '" + strThirdPartyCommission + "', CHANNEL_COMMISSION = '" + strChannelCommission + "', "
                                    + " AGENT_OPERATOR_COMI = '" + strCommonOperatorCommission + "', AGENT_OPARETOR_COMI_ID = '" + strAgentOperatorCommissionId + "', "
                                    + " TAX_PAID_BY = '" + strTaxPaidBy + "', FEES_PAID_BY = '" + strFeesPaidBy + "', FEE_INCLUDE_VAT_TAX = '" + strFeesIncludeVatTax + "', "
                                    + " SUB_SERVICE_CODE = '" + strSubServiceCode + "' "
                                    + " WHERE BANK_SRV_FEE_ID = '" + strServiceFeeId + "' AND SERVICE_ID = '" + strServiceId + "'  ";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
            conn = null;

        }
    }
	
	// Added By Rokon # Date : 18-Sep-2017 # Added Sub Service Code with extra parameter
    public string UpdateServiceFeeNew2(string strServiceFeeId, string strServiceId, string strServiceFeeName, string strStartAmount,
                 string strMaxAmount, string strBankSErvFeeAmount, string strMinimumFee, string strVatTax, string strAit,
                  string strFeesPaidByBank, string strFeesPaidByInitiator, string strFeesPaidByReceipent, string strBankCommission,
                   string strAgentCommission, string strPoolAdjustment, string strVendorCommission, string strThirdPartyCommission,
                   string strChannelCommission, string strCommonOperatorCommission, string strAgentOperatorCommissionId,
                 string strTaxPaidBy, string strFeesPaidBy, string strFeesIncludeVatTax, string strSubServiceCode, string strSubWalletNumber)
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

            OracleCmd.CommandText = @"UPDATE BANK_SERVICE_FEE SET BANK_SRV_FEE_TITLE = '" + strServiceFeeName + "', START_AMOUNT = '" + strStartAmount + "', "
                                    + " MAX_AMOUNT = '" + strMaxAmount + "', BANK_SRV_FEE_AMOUNT = '" + strBankSErvFeeAmount + "', MIN_FEE = '" + strMinimumFee + "', "
                                    + " VAT_TAX = '" + strVatTax + "',  AIT = '" + strAit + "', FEES_PAID_BY_BANK = '" + strFeesPaidByBank + "', "
                                    + " FEES_PAID_BY_CUSTOMER = '" + strFeesPaidByInitiator + "',  FEES_PAID_BY_AGENT = '" + strFeesPaidByReceipent + "', "
                                    + " BANK_COMM_AMOUNT = '" + strBankCommission + "', AGENT_COMM_AMOUNT = '" + strAgentCommission + "', "
                                    + " POOL_ADJUSTMENT = '" + strPoolAdjustment + "', VENDOR_COMMISSION = '" + strVendorCommission + "', "
                                    + " THIRD_PARTY_COMMISSION = '" + strThirdPartyCommission + "', CHANNEL_COMMISSION = '" + strChannelCommission + "', "
                                    + " AGENT_OPERATOR_COMI = '" + strCommonOperatorCommission + "', AGENT_OPARETOR_COMI_ID = '" + strAgentOperatorCommissionId + "', "
                                    + " TAX_PAID_BY = '" + strTaxPaidBy + "', FEES_PAID_BY = '" + strFeesPaidBy + "', FEE_INCLUDE_VAT_TAX = '" + strFeesIncludeVatTax + "', "
                                    + " SUB_SERVICE_CODE = '" + strSubServiceCode + "', SUB_WALLET_NUMBER = '" + strSubWalletNumber + "' "
                                    + " WHERE BANK_SRV_FEE_ID = '" + strServiceFeeId + "' AND SERVICE_ID = '" + strServiceId + "'  ";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
            conn = null;

        }
    }

    public string DeleteBankserviceFeeNew(string strId)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn; //Active Connection
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = "DELETE FROM BANK_SERVICE_FEE WHERE BANK_SRV_FEE_ID = '" + strId + "'";
            OracleCmd.CommandType = CommandType.Text; //Setup Command Type
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            conn = null;
            return "Successful.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            conn.Close();
            conn = null;
            return "Unsuccessfull.";
        }
    }


    #endregion 



    #region for paywell hierarchy tagging

    public string HierarchyAccountIdIfExist(string strAccountId)
    {
        string strSql = "";

        strSql = " SELECT DISTINCT ACCNT_HIERARCHY_ID, ACCNT_ID, HIERARCHY_ACCNT_ID, UPDATED_BY "
                + " FROM ACCOUNT_HIERARCHY WHERE ACCNT_ID = '" + strAccountId + "' ";
        string strReturnColumnValue = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturnColumnValue = dr["ACCNT_ID"].ToString();
                }
                return strReturnColumnValue;
            }
            else
            {
                return strReturnColumnValue;
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }

    public string GettingAccountHierarchyID(string strAccountID)
    {
        string strSql = "";
        strSql = "SELECT DISTINCT ACCNT_HIERARCHY_ID, ACCNT_ID, HIERARCHY_ACCNT_ID, UPDATED_BY "
            + " FROM ACCOUNT_HIERARCHY WHERE ACCNT_ID = '" + strAccountID + "'";
        OracleConnection conn = new OracleConnection(strConString);
        string strCasAccntID = "";
        try
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strCasAccntID = dr["ACCNT_HIERARCHY_ID"].ToString();
                }
                return strCasAccntID;
            }
            else
            {
                return strCasAccntID;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            conn.Close();
            return strCasAccntID;
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public string UpdateAccountHierarchyPaywell(string strHierarchyAccId, string strUpdatedBy, string strAccId, string strAccountHierarchyid)
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

            OracleCmd.CommandText = @"UPDATE ACCOUNT_HIERARCHY SET HIERARCHY_ACCNT_ID = '" + strHierarchyAccId + "', UPDATED_BY = '" +
                       strUpdatedBy + "' WHERE ACCNT_ID = '" + strAccId + "' AND ACCNT_HIERARCHY_ID = '" +
                       strAccountHierarchyid + "' ";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
            conn = null;

        }
    }

    public string AddToAccountHierarchyPaywell(string strAccid, string strHierarchyAccId, string strUpdatedBy)
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
            strSql = "SELECT ACCNT_ID, HIERARCHY_ACCNT_ID, UPDATED_BY FROM ACCOUNT_HIERARCHY ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "ACCOUNT_HIERARCHY";
            oOrderRow = oDS.Tables["ACCOUNT_HIERARCHY"].NewRow();

            oOrderRow["ACCNT_ID"] = strAccid;
            oOrderRow["HIERARCHY_ACCNT_ID"] = strHierarchyAccId;
            oOrderRow["UPDATED_BY"] = strUpdatedBy;

            oDS.Tables["ACCOUNT_HIERARCHY"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "ACCOUNT_HIERARCHY");
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
            conn = null;
        }
    }

    #endregion


    public string GetAccountBalance(string strWallet)
    {
        string strAccountBalance = "";
        string strSql = " select APSNG101.GET_FIS_BALANCE ('" + strWallet + "') ACC_BALANCE from dual ";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strAccountBalance = dr["ACC_BALANCE"].ToString();
                }
                return strAccountBalance;
            }
            else
            {
                return strAccountBalance;

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }

    public string AddInstituteInfo(string strName, string strId, string strAccountNo, string strOwnerCode, string strBdMitInternalCode)
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
            strSql = " SELECT EDU_INS_NAME, EDU_INST_REF_ID, ACCOUNT_NO, STATUS, OWNER_CODE, BDMIT_INTERNAL_INS_CODE FROM EDUCATIONAL_INSTITUTE ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "EDUCATIONAL_INSTITUTE";
            oOrderRow = oDS.Tables["EDUCATIONAL_INSTITUTE"].NewRow();

            oOrderRow["EDU_INS_NAME"] = strName;
            oOrderRow["EDU_INST_REF_ID"] = strId;
            oOrderRow["ACCOUNT_NO"] = strAccountNo;
            oOrderRow["STATUS"] = "A";
            oOrderRow["OWNER_CODE"] = strOwnerCode;
            oOrderRow["BDMIT_INTERNAL_INS_CODE"] = strBdMitInternalCode;

            oDS.Tables["EDUCATIONAL_INSTITUTE"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "EDUCATIONAL_INSTITUTE");
            dbTransaction.Commit();

            return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public string AddInstitutePayment(string strInstituteName, string strPurposeName, string strPurposeCode, string strDescription, string strAmount)
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
            strSql = " SELECT EDU_INS_PK_ID, PURPOSE_CODE, AMOUNT, PURPOSE_CODE_NAME, DESCRIPTION FROM EDUCATIONAL_INSTITUTE_AMOUNT ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "EDUCATIONAL_INSTITUTE_AMOUNT";
            oOrderRow = oDS.Tables["EDUCATIONAL_INSTITUTE_AMOUNT"].NewRow();

            oOrderRow["EDU_INS_PK_ID"] = strInstituteName;
            oOrderRow["PURPOSE_CODE"] = strPurposeCode;
            oOrderRow["AMOUNT"] = strAmount;
            oOrderRow["PURPOSE_CODE_NAME"] = strPurposeName;
            oOrderRow["DESCRIPTION"] = strDescription;

            oDS.Tables["EDUCATIONAL_INSTITUTE_AMOUNT"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "EDUCATIONAL_INSTITUTE_AMOUNT");
            dbTransaction.Commit();

            return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    //--------fahim---------//
    
    public string AddTstInstituteInfo(string strName)
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
            strSql = " SELECT TEST_EDU_INS_NAME, TEST_EDU_INST_REF_ID, TEST_ACCOUNT_NO, TEST_STATUS, TEST_OWNER_CODE, TEST_BDMIT_INTERNAL_INS_CODE FROM TEST_EDUCATIONAL_INSTITUTE ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "TEST_EDUCATIONAL_INSTITUTE";
            oOrderRow = oDS.Tables["TEST_EDUCATIONAL_INSTITUTE"].NewRow();

            oOrderRow["TEST_EDU_INS_NAME"] = strName;
           

            oDS.Tables["TEST_EDUCATIONAL_INSTITUTE"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "TEST_EDUCATIONAL_INSTITUTE");
            dbTransaction.Commit();

            return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            return ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }



    public string UpdateTstInstituteInfo(string strId,string strName)
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

            OracleCmd.CommandText = @"UPDATE TEST_EDUCATIONAL_INSTITUTE SET TEST_EDU_INS_PK_ID = '" + strId + "', TEST_EDU_INS_NAME = '" + strName + "' ";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }





    /// ----------------/// 


    public string GetCorpCollAmount(string strWallet, string fromDate, string toDate)
    {
        string strAccountBalance = "";
        //string strSql = " select APSNG101.GET_FIS_BALANCE ('" + strWallet + "') ACC_BALANCE from dual ";
        string strSql = "SELECT APSNG101.FUNC_DISWISE_CORP_COLL_AMT( '" + strWallet + "', '" + fromDate + "', '" + toDate + "' ) DIS_CORP_COLL FROM DUAL ";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strAccountBalance = dr["DIS_CORP_COLL"].ToString();
                }
                return strAccountBalance;
            }
            else
            {
                return strAccountBalance;
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }

    public string UpdateInstituteAmount(string strPkId, string strPurposeName, string strPurposeCode, string strDescription, string strAmount)
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

            OracleCmd.CommandText = @"UPDATE EDUCATIONAL_INSTITUTE_AMOUNT SET PURPOSE_CODE = '" + strPurposeCode + "', AMOUNT = '" + strAmount + "', "
                                    + " PURPOSE_CODE_NAME = '" + strPurposeName + "', DESCRIPTION = '" + strDescription + "' "
                                    + " WHERE EDU_INS_AMT_ID = '" + strPkId + "' ";
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
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    string strRequestId = "";
    // Date : 20-Nov-2017; Added By Rokon; Reverse request id in CAS_ACCOUNT_TRANSACTION
    public string ReverseRequestId(string strSql)
    {
        this.strRequestId = strSql;

        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        string strSQL = "", strMsg = "";
        strSQL = " SELECT BDMIT_ERP_101.FUNC_UBP_TRANSACTION_REVERSE('" + strSql + "') OUTPUT FROM DUAL ";
        try
        {
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strMsg = dr["OUTPUT"].ToString();
                }
				
				if (strMsg.Equals("FAILED"))
                {
                    strMsg = ReverseRequestId(strRequestId);
                }
            }
        }
        catch (Exception ex)
        {
			conn.Close();
            conn.Dispose();
            conn = null;
			
            strMsg = ReverseRequestId(strRequestId);
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
        return strMsg;
    }
	
	// Date : 14-Jan-2018; Added By Rokon; Send Account PIN 2
	public string ResendAccountPIN2(string message, string accountMSISDN)
    {
        try
        {
            conn = new OracleConnection(strConString);
            DataSet oDS = new DataSet();
            // Create the DataTable "Orders" in the Dataset and the OrdersDataAdapter
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TIME,REQUEST_STAE,REQUEST_TEXT,ERROR_ID FROM SERVICE_REQUEST", conn));
            //oOrdersDataAdapter.RowUpdated += new OracleRowUpdatedEventHandler(OrdersDataAdapter_OnRowUpdate);
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);
            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "SERVICE_REQUEST";

            // Insert the Data
            DataRow oOrderRow = oDS.Tables["SERVICE_REQUEST"].NewRow();
            //oOrderRow["SH_TRAN_ID"] = GetTransactionID();
            oOrderRow["REQUEST_PARTY"] = accountMSISDN;
            oOrderRow["RECEIPENT_PARTY"] = "16225";
            //oOrderRow["SERVICE_CODE"] = strServiceCode;
            oOrderRow["REQUEST_TIME"] = DateTime.Now.ToShortDateString();
            oOrderRow["REQUEST_STAE"] = "P";
            oOrderRow["REQUEST_TEXT"] = message;

            oDS.Tables["SERVICE_REQUEST"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "SERVICE_REQUEST");

            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
	
	public string ReturnString(string strSql)
    {
        string strReturn = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturn = dr[0].ToString();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            strReturn = ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return strReturn;
    }
	
	public void CreateMerchant(string strBranch, string strBank, string strService, string strAccountRank, string strChannelType, string merchantAccountId, string strFeeName, string strStartAmount, string strMaxAmount, string strFee, string strMinimumFee, string strVatTax, string strAit, string strFeesPaidByBank, string strFeesPaidByInitiator, string strFeesPaidByReceipent, string strBankCommission, string strAgentCommission, string strPoolAdjustment, string strVendorCommission, string strThirdPartyCommission, string strChannelCommission, string strAgentOperatorCommission, string strTaxPaidBy, string strFeesPaidBy, string strFeesIncludeVatTax, string strFeesPaidByMotherMerchant)
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
            strSql = " SELECT MERCHANT_ACCNT_ID, SERVICE_ID, BANK_SRV_FEE_TITLE, START_AMOUNT, MAX_AMOUNT, BANK_SRV_FEE_AMOUNT, VAT_TAX, TAX_PAID_BY, FEE_INCLUDE_VAT_TAX, ACCNT_RANK_ID, FEES_PAID_BY_BANK, FEES_PAID_BY_CUSTOMER, AIT, AGENT_COMM_AMOUNT, BANK_COMM_AMOUNT, FEES_PAID_BY, POOL_ADJUSTMENT, VENDOR_COMMISSION, THIRD_PARTY_COMMISSION, CHANNEL_COMMISSION, CHANNEL_TYPE_ID, FEES_PAID_BY_AGENT, MIN_FEE, CMP_BRANCH_ID, BANK_CODE, AGENT_OPERATOR_COMI, FEES_PAID_BY_MM FROM MERCHANT_LIST ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "MERCHANT_LIST";
            oOrderRow = oDS.Tables["MERCHANT_LIST"].NewRow();

            oOrderRow["MERCHANT_ACCNT_ID"] = merchantAccountId;
            oOrderRow["SERVICE_ID"] = strService;
            oOrderRow["BANK_SRV_FEE_TITLE"] = strFeeName;
            oOrderRow["START_AMOUNT"] = strStartAmount;
            oOrderRow["MAX_AMOUNT"] = strMaxAmount;
            oOrderRow["BANK_SRV_FEE_AMOUNT"] = strFee;
            oOrderRow["VAT_TAX"] = strVatTax;
            oOrderRow["TAX_PAID_BY"] = strTaxPaidBy;
            oOrderRow["FEE_INCLUDE_VAT_TAX"] = strFeesIncludeVatTax;
            oOrderRow["ACCNT_RANK_ID"] = strAccountRank;
            oOrderRow["FEES_PAID_BY_BANK"] = strFeesPaidByBank;
            oOrderRow["FEES_PAID_BY_CUSTOMER"] = strFeesPaidByInitiator;
            oOrderRow["AIT"] = strAit;
            oOrderRow["AGENT_COMM_AMOUNT"] = strAgentCommission;
            oOrderRow["BANK_COMM_AMOUNT"] = strBankCommission;
            oOrderRow["FEES_PAID_BY"] = strFeesPaidBy;
            oOrderRow["POOL_ADJUSTMENT"] = strPoolAdjustment;
            oOrderRow["VENDOR_COMMISSION"] = strVendorCommission;
            oOrderRow["THIRD_PARTY_COMMISSION"] = strThirdPartyCommission;
            oOrderRow["CHANNEL_COMMISSION"] = strChannelCommission;
            oOrderRow["CHANNEL_TYPE_ID"] = strChannelType;
            oOrderRow["FEES_PAID_BY_AGENT"] = strFeesPaidByReceipent;
            oOrderRow["MIN_FEE"] = strMinimumFee;
            oOrderRow["CMP_BRANCH_ID"] = strBranch;
            oOrderRow["BANK_CODE"] = strBank;
            oOrderRow["AGENT_OPERATOR_COMI"] = strAgentOperatorCommission;
            oOrderRow["FEES_PAID_BY_MM"] = strFeesPaidByMotherMerchant;

            oDS.Tables["MERCHANT_LIST"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "MERCHANT_LIST");
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }


    public void CreateMerchant2(string strBranch, string strBank, string strService, string strAccountRank, string strSourceAccountRank, string strChannelType, string merchantAccountId, string strFeeName, string strStartAmount, string strMaxAmount, string strFee, string strMinimumFee, string strVatTax, string strAit, string strFeesPaidByBank, string strFeesPaidByInitiator, string strFeesPaidByReceipent, string strBankCommission, string strAgentCommission, string strPoolAdjustment, string strVendorCommission, string strThirdPartyCommission, string strChannelCommission, string strAgentOperatorCommission, string strTaxPaidBy, string strFeesPaidBy, string strFeesIncludeVatTax, string strFeesPaidByMotherMerchant)
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
            strSql = " SELECT MERCHANT_ACCNT_ID, SERVICE_ID, BANK_SRV_FEE_TITLE, START_AMOUNT, MAX_AMOUNT, BANK_SRV_FEE_AMOUNT, VAT_TAX, TAX_PAID_BY, FEE_INCLUDE_VAT_TAX, ACCNT_RANK_ID, SOURCE_ACCNT_RANK_ID, FEES_PAID_BY_BANK, FEES_PAID_BY_CUSTOMER, AIT, AGENT_COMM_AMOUNT, BANK_COMM_AMOUNT, FEES_PAID_BY, POOL_ADJUSTMENT, VENDOR_COMMISSION, THIRD_PARTY_COMMISSION, CHANNEL_COMMISSION, CHANNEL_TYPE_ID, FEES_PAID_BY_AGENT, MIN_FEE, CMP_BRANCH_ID, BANK_CODE, AGENT_OPERATOR_COMI, FEES_PAID_BY_MM FROM MERCHANT_LIST ";
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn, dbTransaction));
            OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
            oOrdersDataAdapter.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "MERCHANT_LIST";
            oOrderRow = oDS.Tables["MERCHANT_LIST"].NewRow();

            oOrderRow["MERCHANT_ACCNT_ID"] = merchantAccountId;
            oOrderRow["SERVICE_ID"] = strService;

            oOrderRow["BANK_SRV_FEE_TITLE"] = strFeeName;
            oOrderRow["START_AMOUNT"] = strStartAmount;
            oOrderRow["MAX_AMOUNT"] = strMaxAmount;
            oOrderRow["BANK_SRV_FEE_AMOUNT"] = strFee;
            oOrderRow["VAT_TAX"] = strVatTax;
            oOrderRow["TAX_PAID_BY"] = strTaxPaidBy;
            oOrderRow["FEE_INCLUDE_VAT_TAX"] = strFeesIncludeVatTax;
            oOrderRow["ACCNT_RANK_ID"] = strAccountRank;
            oOrderRow["SOURCE_ACCNT_RANK_ID"] = strSourceAccountRank;
            oOrderRow["FEES_PAID_BY_BANK"] = strFeesPaidByBank;
            oOrderRow["FEES_PAID_BY_CUSTOMER"] = strFeesPaidByInitiator;
            oOrderRow["AIT"] = strAit;
            oOrderRow["AGENT_COMM_AMOUNT"] = strAgentCommission;
            oOrderRow["BANK_COMM_AMOUNT"] = strBankCommission;
            oOrderRow["FEES_PAID_BY"] = strFeesPaidBy;
            oOrderRow["POOL_ADJUSTMENT"] = strPoolAdjustment;
            oOrderRow["VENDOR_COMMISSION"] = strVendorCommission;
            oOrderRow["THIRD_PARTY_COMMISSION"] = strThirdPartyCommission;
            oOrderRow["CHANNEL_COMMISSION"] = strChannelCommission;
            oOrderRow["CHANNEL_TYPE_ID"] = strChannelType;
            oOrderRow["FEES_PAID_BY_AGENT"] = strFeesPaidByReceipent;
            oOrderRow["MIN_FEE"] = strMinimumFee;
            oOrderRow["CMP_BRANCH_ID"] = strBranch;
            oOrderRow["BANK_CODE"] = strBank;
            oOrderRow["AGENT_OPERATOR_COMI"] = strAgentOperatorCommission;
            oOrderRow["FEES_PAID_BY_MM"] = strFeesPaidByMotherMerchant;

            oDS.Tables["MERCHANT_LIST"].Rows.Add(oOrderRow);
            oOrdersDataAdapter.Update(oDS, "MERCHANT_LIST");
            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public string UpdateMerchantInfo(string strMerchantId, string strStartAmount, string strMaxAmount, string strFee, string strMinimumFee, string strVatTax, string strAIT, string strFeesPaidByBank, string strFeesPaidByInitiator, string strFeesPaidByReceipent, string strBankCommission, string strAgentCommission, string strPoolAdjustment, string strVendorCommission, string strThirdPartyCommission, string strChannelCommission, string strAgentOperatorCommission, string strTaxPaidBy, string strFeesPaidby, string strFeeIncludeVatTax, string strFeesPaidByMM)
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

            OracleCmd.CommandText = @"UPDATE MERCHANT_LIST SET START_AMOUNT = '" + strStartAmount + "', MAX_AMOUNT = '" + strMaxAmount + "', BANK_SRV_FEE_AMOUNT = '" + strFee + "', VAT_TAX = '" + strVatTax + "', TAX_PAID_BY = '" + strTaxPaidBy + "', FEE_INCLUDE_VAT_TAX = '" + strFeeIncludeVatTax + "', FEES_PAID_BY_BANK = '" + strFeesPaidByBank + "', FEES_PAID_BY_CUSTOMER = '" + strFeesPaidByInitiator + "', AIT = '" + strAIT + "', AGENT_COMM_AMOUNT = '" + strAgentCommission + "', BANK_COMM_AMOUNT = '" + strBankCommission + "', FEES_PAID_BY = '" + strFeesPaidby + "', POOL_ADJUSTMENT = '" + strPoolAdjustment + "', VENDOR_COMMISSION = '" + strVendorCommission + "', THIRD_PARTY_COMMISSION = '" + strThirdPartyCommission + "', CHANNEL_COMMISSION = '" + strChannelCommission + "', FEES_PAID_BY_AGENT = '" + strFeesPaidByReceipent + "', MIN_FEE = '" + strMinimumFee + "', AGENT_OPERATOR_COMI = '" + strAgentOperatorCommission + "', FEES_PAID_BY_MM = '" + strFeesPaidByMM + "' WHERE MERCHANT_ID = '" + strMerchantId + "'";

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            return "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public string DeleteMerchant(string strMerchantId)
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

            OracleCmd.CommandText = @"DELETE MERCHANT_LIST WHERE MERCHANT_ID = '" + strMerchantId + "' ";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            return "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
	
	public string DeleteDeviceDetail(string strId)
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

            OracleCmd.CommandText = @"DELETE DEVICE_LIST WHERE DEVICE_LIST_ID = '" + strId + "' ";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            return "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

public void ReverseTopupTransaction(string strRequestId)
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

            OracleCmd.CommandText = @"UPDATE TOPUP_TRANSACTION SET REQUEST_STATUS = 'E', SUCCESSFUL_STATUS = 'F', SSL_FINAL_STATUS = '999', SSL_FINAL_MESSAGE = 'Failed', ALL_FINAL_STATUS = 'F' WHERE REVERSE_STATUS = 'N' AND ALL_FINAL_STATUS <> 'S' AND REQUEST_ID = '" + strRequestId + "'";

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            //return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            //return "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
	
	public void GenerateQrCodeManually(string strWalletNumber)
    {
        conn = new OracleConnection(strConString);
        try
        {
            conn.Open();
            OracleCommand myCMD = new OracleCommand("PRO_GENERATE_QRCODE_EKYC", conn);
            myCMD.CommandType = CommandType.StoredProcedure;

            myCMD.Parameters.Add(new OracleParameter("vWalletNumber", OracleType.VarChar)).Value = strWalletNumber;
            myCMD.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
	
	public void UpdateClientGender(string strClientId, string strClientGender)
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

            OracleCmd.CommandText = @"UPDATE CLIENT_LIST SET CLINT_GENDER = '" + strClientGender + "' WHERE CLINT_ID = '" + strClientId + "'";

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            //return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            //return "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    // A.Salam 4.July.2019
    public void UpdateDIGITAL_KYC_INFO(string strDIGITAL_KYC_ID)
    {
        string updateString = @"UPDATE DIGITAL_KYC_INFO SET IS_UPDATE ='Y', IS_REGISTER ='Y', IS_PROCESSING = 'D' WHERE DIGITAL_KYC_ID='" + strDIGITAL_KYC_ID + "' ";



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

            OracleCmd.CommandText = updateString;

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            //return "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            //return "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn = null;
        }

    }

    public bool UpdateClientList(string strClientName, string strFathersName, string strMothersName, string strDOB,
                                string strOccupation, string strWEB, string strPurOfTran, string strUISCAgent,
                                string strOfcAdss, string strPreAdss, string strPerAdss, string strThana, string strClientID,
                                string strUpdatedBy, string strUpdateDate, string strReqPartyType, string strUpdateMessage, string strHusbandName, string strIncompleteKYC, string strPerThana, string customerArea, string strClientGender)
    {

        string strSQL = "";
        bool blnClientUpdate = false;
        OracleConnection conn;
        OracleCommand OLEDBCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        OLEDBCmd = new OracleCommand();
        OLEDBCmd.Connection = conn;
        OLEDBCmd.Transaction = dbTransaction;

        if (strUpdateMessage == "")
        {
            strSQL = " UPDATE CLIENT_LIST SET CLINT_NAME = '" + strClientName + "', "
                                                                   + " CLINT_ADDRESS1 = '" + strPreAdss + "', "
                                                                   + " CLINT_ADDRESS2 = '" + strPerAdss + "', "
                                                                   + " CLINT_FATHER_NAME = '" + strFathersName + "', "
                                                                   + " CLINT_MOTHER_NAME = '" + strMothersName + "', "
                                                                   + " CLIENT_DOB = TO_DATE('" + strDOB + "', 'dd/mm/yyyy') , "
                                                                   + " CLIENT_OFFIC_ADDRESS = '" + strOfcAdss + "', "
                                                                   + " OCCUPATION = '" + strOccupation + "', "
                                                                   + " WORK_EDU_BUSINESS='" + strWEB + "', "
                                                                   + " PUR_OF_TRAN = '" + strPurOfTran + "', "
                                                                   + " UISC_AGENT = '" + strUISCAgent + "', "
                                                                   + " THANA_ID='" + strThana + "', "
                                                                   + " KYC_UPDATED_BY ='" + strUpdatedBy + "', "
                                                                   + " UPDATE_DATE=TO_DATE('" + strUpdateDate + "','dd/mm/yyyy HH24:MI:SS'),"
                                                                   + " HUSBAND_NAME='" + strHusbandName + "', "
                                                                   + " REQUEST_PARTY_TYPE='" + strReqPartyType + "', "
                                                                   + " INCOMPLETE_KYC='" + strIncompleteKYC + "', "
                                                                   + " THANA_ID_PERMANENT='" + strPerThana + "', "
                                                                   + " CUSTOMER_AREA='" + customerArea + "', "
                                                                   + " CLINT_GENDER = '" + strClientGender + "'"
                                                                   + " WHERE (CLINT_ID = '" + strClientID + "') ";
        }
        else
        {
            strSQL = " UPDATE CLIENT_LIST SET CLINT_NAME = '" + strClientName + "', "
                                                          + " CLINT_ADDRESS1 = '" + strPreAdss + "', "
                                                          + " CLINT_ADDRESS2 = '" + strPerAdss + "', "
                                                          + " CLINT_FATHER_NAME = '" + strFathersName + "', "
                                                          + " CLINT_MOTHER_NAME = '" + strMothersName + "', "
                                                          + " CLIENT_DOB = TO_DATE('" + strDOB + "', 'dd/mm/yyyy') , "
                                                          + " CLIENT_OFFIC_ADDRESS = '" + strOfcAdss + "', "
                                                          + " OCCUPATION = '" + strOccupation + "', "
                                                          + " WORK_EDU_BUSINESS='" + strWEB + "', "
                                                          + " PUR_OF_TRAN = '" + strPurOfTran + "', "
                                                          + " UISC_AGENT = '" + strUISCAgent + "', "
                                                          + " HUSBAND_NAME='" + strHusbandName + "', "
                                                          + " THANA_ID='" + strThana + "', "
                                                          + " INCOMPLETE_KYC='" + strIncompleteKYC + "',"
                                                          + " CLINT_GENDER = '" + strClientGender + "'"
                                                          + " WHERE (CLINT_ID = '" + strClientID + "') ";
        }
        try
        {
            OLEDBCmd.CommandText = strSQL;

            OLEDBCmd.CommandType = CommandType.Text;//Setup Command Type
            OLEDBCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            blnClientUpdate = true;
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
        return blnClientUpdate;
    }
	
	public string UpdateCancelStatus(string strUserName, string strDigitalKYC, string strRemarks)
    {
        string strReturnString = "";
        string updateString = "UPDATE DIGITAL_KYC_INFO SET IS_PROCESSING = 'N', REMARKS = 'User: " + strUserName + ", Loged On: " + DateTime.Now + ",\n Remarks: " + strRemarks.ToString() + "' WHERE DIGITAL_KYC_ID = '" + strDigitalKYC + "'";

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

            OracleCmd.CommandText = updateString;

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            strReturnString = "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            strReturnString = "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
        return strReturnString;
    }
	
	public void UpdateDeleteResult(string strDigitalKYC)
    {
        string strReturnString = "";
        string updateString = "UPDATE DIGITAL_KYC_INFO SET IS_REGISTER = 'E', IS_UPDATE = 'N', IS_PROCESSING = 'C', CANCEL_DATE = SYSDATE WHERE DIGITAL_KYC_ID = '" + strDigitalKYC + "'";

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

            OracleCmd.CommandText = updateString;

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            conn.Close();
            strReturnString = "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            conn.Close();
            ex.Message.ToString();
            strReturnString = "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
	
	public string ResetAccountStatus(string strAccntID, string strMSISDN, string strUserID, string strMachineIP, string strMachineName, string strPIN1)
    {
        string strMsg = "";
        string strPIN2 = "";
        string strAns1 = "";
        string strSQL = "";
        //int intCount = 0;
        //------------------------------------------
        OracleConnection conn;
        //DataSet oDS;
        //OracleDataAdapter oOrdersDataAdapter;
        //DataRow dtrRow;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;
        //-------------------------------------
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        //------------------------------------------
        //-------------------------------------------
        strSQL = "SELECT ACCNT_SEC_QUES_ID,ACCNT_SEC_QUES_DESC,ACCNT_SEC_QUES_SLNO FROM ACCOUNT_SEC_QUESTION";
        DataSet dtsSecQuestion = new DataSet();
        OracleDataAdapter adpSecQuestion = new OracleDataAdapter(new OracleCommand(strSQL, conn, dbTransaction));
        OracleCommandBuilder cmbSecQuestion = new OracleCommandBuilder(adpSecQuestion);
        adpSecQuestion.Fill(dtsSecQuestion, "ACCOUNT_SEC_QUESTION");
        //----------------------------
        System.Random RandNum = new System.Random();
        //----------------------------
        try
        {

            //-------------------------------------------------------------            
            try
            {
                //-------------------- Get new Clinet ID--------------------------------- 
                OracleCmd = new OracleCommand();
                OracleCmd.Connection = conn; //Active Connection
                OracleCmd.Transaction = dbTransaction;
                //---------------------------------------------------------------------------------                
                strPIN2 = String.Format("{0:0000}", RandNum.Next(1111, 9999).ToString());
                //---------------------------------------------------------------------
                //OracleCmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_PIN='" + strPIN1 + "',ACCNT_PIN2='" + strPIN2 + "' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandText = "UPDATE ACCOUNT_LIST SET ACCNT_PIN='" + strPIN1 + "', ACCNT_STATE = 'A', INVALID_LOGIN_ATTEMPT_COUNT = '0' WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //-----------------------------------------------------------------------
                OracleCmd.CommandText = "DELETE FROM ACCOUNT_SEC_ANSWER WHERE ACCNT_ID='" + strAccntID.Trim() + "'"; // 
                OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                OracleCmd.ExecuteNonQuery();
                //----------------------------------------------------------
                //strMsg = "Your New PIN and Answer are as follows";
                strMsg = "Your New PIN is as follows";
                strMsg = strMsg + "\n " + strPIN1 + ". Please remember this PIN & Delete this SMS. Thank you. MYCash";


                //strMsg = strMsg + "\nPIN2=" + strPIN2;
                //----------------------------------------------------------
                foreach (DataRow pSecQuest in dtsSecQuestion.Tables["ACCOUNT_SEC_QUESTION"].Rows)
                {
                    strAns1 = String.Format("{0:00000}", RandNum.Next(00000, 99999).ToString());
                    OracleCmd.CommandText = "INSERT INTO ACCOUNT_SEC_ANSWER(ACCNT_SEC_QUES_ID,ACCNT_ID,ACCNT_SEC_ANSWER) VALUES('" + pSecQuest["ACCNT_SEC_QUES_ID"].ToString() + "','" + strAccntID.Trim() + "','" + strAns1 + "')"; // Stored Procedure to Call
                    OracleCmd.CommandType = CommandType.Text; //Setup Command Type
                    OracleCmd.ExecuteNonQuery();
                    //strMsg = strMsg + "\nQ. " + pSecQuest["ACCNT_SEC_QUES_DESC"].ToString() + ". A. " + strAns1;
                }
                //--------------------------------------------------------------- 
                strMsg = ForwardMessage(strMSISDN, "1234", strMsg, strAccntID, "BDC", "");
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                strMsg = ex.Message.ToString();
            }
            conn.Close();
            return strMsg;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message.ToString());
            strMsg = e.Message.ToString();
            return strMsg;
        }
    }

    public DataSet GetSystemAccountDetail(string strSysUserLoginName)
    {
        string strMsg;
        string strSql;
        strSql = "SELECT * from CM_SYSTEM_USERS WHERE SYS_USR_LOGIN_NAME='" + strSysUserLoginName + "'";

        OracleConnection conn = new OracleConnection(strConString);

        try
        {   
            DataSet oDS = new DataSet();
            OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
            oOrdersDataAdapter.Fill(oDS, "CM_SYSTEM_USERS");
            return oDS;
        }
        catch (Exception ex)
        {
            strMsg = ex.Message.ToString();
            return null;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }

    public string ResetSystemUserPassword(string strSysUserLoginName)
    {
        string strReturnString = "";
        string updateString = @"UPDATE CM_SYSTEM_USERS SET SYS_USR_PASS = '" + strSysUserLoginName + "', LOCKED_STATUS = 'UL', PASSWORD_EXPIRED_DATE = SYSDATE+90, CLICK_FAILURE = '0' WHERE SYS_USR_LOGIN_NAME = '" + strSysUserLoginName + "'";

        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = updateString;

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            strReturnString = "Successful";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            strReturnString = "Unsuccessful";
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
        return strReturnString;
    }

    public string UpdateNliDeviceStatus(string accountPosId, string strStatus, string strOfficeName, string strZoneName, string strCashierName, string strDesignation, string strNlicCIdNo, string strMobileNo, string strImei1, string strImei2)
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

            DataSet oDs = ExecuteQuery("SELECT NLI_INFO_ENTRY_DATE FROM ACCOUNT_POS_LIST WHERE ACCNT_POS_ID = '" + accountPosId + "'");

            if (oDs.Tables[0].Rows[0]["NLI_INFO_ENTRY_DATE"].ToString().Length < 2)
            {
                OracleCmd.CommandText = "UPDATE ACCOUNT_POS_LIST SET ACTIVE_STATUS = '" + strStatus + "', OFFICE_NAME = '" + strOfficeName + "', ZONE_NAME = '" + strZoneName + "', CASHIER_NAME = '" + strCashierName + "', DESIGNATION = '" + strDesignation + "', NLIC_CASHIER_ID_NO = '" + strNlicCIdNo + "', MOBILE_NO = '" + strMobileNo + "', IMEI_NO_1 = '" + strImei1 + "', IMEI_NO_2 = '" + strImei2 + "', NLI_INFO_ENTRY_DATE = '" + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy") + "' WHERE ACCNT_POS_ID = '" + accountPosId + "'";
            }
            else
            {
                OracleCmd.CommandText = "UPDATE ACCOUNT_POS_LIST SET ACTIVE_STATUS = '" + strStatus + "', OFFICE_NAME = '" + strOfficeName + "', ZONE_NAME = '" + strZoneName + "', CASHIER_NAME = '" + strCashierName + "', DESIGNATION = '" + strDesignation + "', NLIC_CASHIER_ID_NO = '" + strNlicCIdNo + "', MOBILE_NO = '" + strMobileNo + "', IMEI_NO_1 = '" + strImei1 + "', IMEI_NO_2 = '" + strImei2 + "' WHERE ACCNT_POS_ID = '" + accountPosId + "'";
            }

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public string Middleware_Reverse(string strRequestId)
    {

        string strReturn = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        //string strSql = " select APSNG101.GET_FIS_BALANCE ('" + strWallet + "') ACC_BALANCE from dual ";
        string strSql = "SELECT BDMIT_ERP_101.FUNC_UBP_MID_TRAN_REVERSE('" + strRequestId + "') OUTPUT FROM DUAL";
        try
        {
            //OracleConnection conn = new OracleConnection(strConString);
            //conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturn = dr["OUTPUT"].ToString();
                }
                return strReturn;
            }
            else
            {
                return strReturn;
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }


    }

    #region old_GetDetails_middleware

    //public DataTable GetDetails_Middleware(DateTime Todate, DateTime Fromdate, string strSearchText)
    //{
    //    OracleConnection conn;
    //    OracleCommand OracleCmd;
    //    OracleTransaction dbTransaction;
    //    string strAccNo = "";


    //    conn = new OracleConnection(strConString);
    //    conn.Open();
    //    dbTransaction = conn.BeginTransaction();
    //    DataTable dt = new DataTable();




    //    try
    //    {
    //        OracleCmd = new OracleCommand();
    //        OracleCmd.Connection = conn;//Active Connection
    //        OracleCmd.Transaction = dbTransaction;
    //        if (strSearchText != "")
    //        {
    //            OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE UMT.REQUEST_ID='" + strSearchText + "' AND RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT'";
    //        }
    //        else
    //        {
    //            OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT'";
    //        }
    //        // OracleCmd.CommandText = "SELECT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED FROM UTILITY_MIDDLEWARE_TRANSACTION WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";




    //        OracleCmd.CommandType = CommandType.Text;
    //        OracleCmd.ExecuteNonQuery();
    //        OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
    //        sda.Fill(dt);
    //        dbTransaction.Commit();
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //        return null;
    //    }
    //}
    # endregion

    #region new_GetDetails_middleware

    public DataTable GetDetails_Middleware(DateTime Todate, DateTime Fromdate, string strSearchText, string strUsrIdSearchText, string strPosIdSearch, string strResSearchText, string strMeterId)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;
        string strAccNo = "";
        string strReqTxt = "";
        string strUsrId = "";
        string strPosId = "";
        string strResMsg = "";
        string strMeter = "";

        if (strSearchText != "")
        {
            strReqTxt = "AND UMT.REQUEST_ID='" + strSearchText + "' ";
        }
        if (strPosIdSearch != "")
        {
            strPosId = "AND DPDC_POS_ID='" + strPosIdSearch + "' ";
        }
        if (strUsrIdSearchText != "")
        {
            strUsrId = "AND AAMRA_USER_ID='" + strUsrIdSearchText + "' ";
        }

        if (strMeterId != "")
        {
            strMeter = "AND METER_NUMBER='" + strMeterId + "' ";
        }
        if (strResSearchText != "All")
        {
            strResMsg = "AND RESPONSE_MSG='" + strResSearchText + "'";

        }
        else
        {
            strResMsg = " ";

        }





        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();




        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            //if (strSearchText !="")
            //{
            //    OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT' " + strReqTxt + strPosId + strUsrId + strResMsg;
            //}
            //else
            //{
            //    OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT'";
            //}
            // OracleCmd.CommandText = "SELECT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED FROM UTILITY_MIDDLEWARE_TRANSACTION WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";

            OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT,SR.REQUEST_TIME FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,SERVICE_REQUEST SR,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_ID=UMT.REQUEST_ID AND RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND BILL_TYPE='DPDC' AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT' " + strReqTxt + strPosId + strUsrId + strResMsg + strMeter + " ORDER BY REQUEST_ID DESC";



            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return null;
        }
    }

    #endregion


    #region new_GetDetails_middleware_DESCO

    public DataTable GetDetails_Middleware_Desco(DateTime Todate, DateTime Fromdate, string strSearchText, string strUsrIdSearchText, string strPosIdSearch, string strResSearchText, string strMeterId)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;
        string strAccNo = "";
        string strReqTxt = "";
        string strUsrId = "";
        string strPosId = "";
        string strResMsg = "";
        string strMeter = "";

        if (strSearchText != "")
        {
            strReqTxt = "AND UMT.REQUEST_ID='" + strSearchText + "' ";
        }
        if (strPosIdSearch != "")
        {
            strPosId = "AND DPDC_POS_ID='" + strPosIdSearch + "' ";
        }
        if (strUsrIdSearchText != "")
        {
            strUsrId = "AND AAMRA_USER_ID='" + strUsrIdSearchText + "' ";
        }

        if (strMeterId != "")
        {
            strMeter = "AND METER_NUMBER='" + strMeterId + "' ";
        }
        if (strResSearchText != "All")
        {
            strResMsg = "AND RESPONSE_MSG='" + strResSearchText + "'";

        }
        else
        {
            strResMsg = " ";

        }





        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();




        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            //if (strSearchText !="")
            //{
            //    OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT' " + strReqTxt + strPosId + strUsrId + strResMsg;
            //}
            //else
            //{
            //    OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT'";
            //}
            // OracleCmd.CommandText = "SELECT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED FROM UTILITY_MIDDLEWARE_TRANSACTION WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";

            //OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,TXN_ID,IS_REVERSED,CAT.CAS_TRAN_AMT,SR.REQUEST_TIME FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,SERVICE_REQUEST SR,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_ID=UMT.REQUEST_ID AND RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND BILL_TYPE='UBPDSPM' AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT' " + strReqTxt + strPosId + strUsrId + strResMsg + strMeter + "  ORDER BY REQUEST_ID DESC";

            OracleCmd.CommandText = "SELECT SUB1.AAMRA_USER_ID,SUB1.DPDC_POS_ID,SUB1.REF_NO,SUB1.REQUEST_ID,SUB1.RESPONSE_CODE,SUB1.RESPONSE_MSG,SUB1.RESPONSE_TIME,SUB1.METER_NUMBER,SUB1.BILL_TYPE,SUB1.TXN_ID,SUB1.IS_REVERSED,SUB1.CAS_TRAN_AMT,SUB1.REQUEST_TIME,TRUNC(SUB1.FEE,6) AS FEES,TRUNC(SUB1.FEE*15/115,6) AS VAT_1,TRUNC(SUB1.FEE-(SUB1.FEE*15/115),6) AS AFTER_VAT_1,TRUNC(((SUB1.FEE-(SUB1.FEE*15/115))*10/100),6) AS TAX_1,TRUNC((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100),6) AS AFTER_TAX_1,TRUNC(((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*10/100,6) AS BANK_COMM,TRUNC(((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*55/100,6) AS AGENT_COMM,TRUNC(((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*15/100,6) AS THIRD_COMM, TRUNC((((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*20/100),6) AAMRA_COM FROM (SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,TXN_ID,IS_REVERSED,CAT.CAS_TRAN_AMT,SR.REQUEST_TIME, CASE WHEN CAT.CAS_TRAN_AMT <=2000 THEN (1/100)*CAT.CAS_TRAN_AMT ELSE 20 END FEE FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,SERVICE_REQUEST SR,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_ID=UMT.REQUEST_ID AND RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND BILL_TYPE='UBPDSPM' AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT' " + strReqTxt + strPosId + strUsrId + strResMsg + strMeter + "  ORDER BY REQUEST_ID DESC) SUB1";



            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return null;
        }
    }

    #endregion


    public string SaveBulkREBAccount(string AccountIds, string Remarks)
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

            OracleCmd.CommandText = "INSERT INTO UBP_REB_BULK_BILL_PAY (UBP_REB_ACCOUNT_ID,UBP_REB_ACCOUNT_STATUS, UBP_REB_REMARKS) " +
                "VALUES('" + AccountIds + "', '" + 'A' + "', '" + Remarks + "')";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public string InsertDPDCBulkBillPayEdotco(string strAgentAccntNo, string strLocationId, string strBillType, string strAccountStatus, string strRemark, string strPurpose)
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

            OracleCmd.CommandText = "INSERT INTO APSNG101.EDOTCO_UTILITY_BILL_PAY (ACCOUNT_NUMBER, LOCATION_ID, BILL_TYPE, ACCOUNT_STATUS, REMARK, PURPOSE) " +
                "VALUES('" + strAgentAccntNo + "', '" + strLocationId + "', '" + strBillType + "', '" + strAccountStatus + "', '" + strRemark + "', '" + strPurpose + "')";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "Failed";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

   //-------Fahim--------//
    public string InsertDESCOBulkBillUpload(string strAgentAccntNo, string strBillType, string strAccountStatus, string strRemark, string strPurpose)
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

            OracleCmd.CommandText = "INSERT INTO APSNG101.EDOTCO_UTILITY_BILL_PAY (ACCOUNT_NUMBER, BILL_TYPE, ACCOUNT_STATUS, REMARK, PURPOSE) " +
                "VALUES('" + strAgentAccntNo + "', '" + strBillType + "', '" + strAccountStatus + "', '" + strRemark + "', '" + strPurpose + "')";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "Failed";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    //-------Fahim--------//

    public DataTable GetREBAccountsDetails()
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandText = "Select *From UBP_REB_BULK_BILL_PAY";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }

    public string DeleteREBAccoutRow(string AutoID)
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

            OracleCmd.CommandText = "Delete From UBP_REB_BULK_BILL_PAY Where UBP_REB_BBP_ID='" + AutoID + "'";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    //public string UpdateREBAccoutRow(string AutoID, string REBAccountID, string Remarks, string Purpose)
    //{
    //    OracleConnection conn;
    //    OracleCommand OracleCmd;
    //    OracleTransaction dbTransaction;

    //    conn = new OracleConnection(strConString);
    //    conn.Open();
    //    dbTransaction = conn.BeginTransaction();
    //    try
    //    {
    //        OracleCmd = new OracleCommand();
    //        OracleCmd.Connection = conn;//Active Connection
    //        OracleCmd.Transaction = dbTransaction;

    //        OracleCmd.CommandText = "Update  UBP_REB_BULK_BILL_PAY Set UBP_REB_ACCOUNT_ID='" + REBAccountID + "', UBP_REB_REMARKS='" + Remarks + "', UBP_REB_PURPOSE='" + Purpose + "' Where UBP_REB_BBP_ID='" + AutoID + "'";


    //        OracleCmd.CommandType = CommandType.Text;
    //        OracleCmd.ExecuteNonQuery();
    //        dbTransaction.Commit();
    //        return "Successfull.";
    //    }
    //    catch (Exception ex)
    //    {
    //        dbTransaction.Rollback();
    //        ex.Message.ToString();
    //        return "";
    //    }
    //    finally
    //    {
    //        conn.Close();
    //        conn = null;
    //    }
    //}
    public string SaveBulkREBAccount(string AccountIds, string Remarks, string Parameters)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;
        string space = "L";
        string inqOrPaid = "N";
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;

            //OracleCmd.CommandText = "INSERT INTO UBP_REB_BULK_BILL_PAY (UBP_REB_ACCOUNT_ID,UBP_REB_ACCOUNT_STATUS, UBP_REB_REMARKS,UBP_REB_PURPOSE) " +
            //"VALUES('" + AccountIds + "', '" + 'A' + "', '" + Remarks + "','" + Parameters + "')";

            if (Parameters == "I")
            {
                string strSqlCheck = "SELECT COUNT(*) FROM APSNG101.UBP_REB_BULK_BILL_PAY WHERE UBP_REB_ACCOUNT_ID='" + AccountIds + "' AND UBP_REB_PURPOSE='I'";
                string strCheckDuplicate = ReturnString(strSqlCheck);

                if (Convert.ToInt32(strCheckDuplicate) < 1)
                {

                    OracleCmd.CommandText = "INSERT INTO UBP_REB_BULK_BILL_PAY (UBP_REB_ACCOUNT_ID,UBP_REB_ACCOUNT_STATUS, UBP_REB_REMARKS,UBP_REB_PURPOSE,IS_INQUIRED,IS_PAID) " +
                       "VALUES('" + AccountIds + "', '" + 'A' + "', '" + Remarks + "','" + Parameters + "','" + inqOrPaid + "','" + space + "')";
                }
            }
            else if (Parameters == "P")
            {
                string strSqlCheck = "SELECT COUNT(*) FROM APSNG101.UBP_REB_BULK_BILL_PAY WHERE UBP_REB_ACCOUNT_ID='" + AccountIds + "' AND UBP_REB_PURPOSE='P' AND IS_PAID='N'";
                string strCheckDuplicate = ReturnString(strSqlCheck);

                if (Convert.ToInt32(strCheckDuplicate) < 1)
                {
                    OracleCmd.CommandText = "INSERT INTO UBP_REB_BULK_BILL_PAY (UBP_REB_ACCOUNT_ID,UBP_REB_ACCOUNT_STATUS, UBP_REB_REMARKS,UBP_REB_PURPOSE,IS_INQUIRED,IS_PAID) " +
                       "VALUES('" + AccountIds + "', '" + 'A' + "', '" + Remarks + "','" + Parameters + "','" + space + "','" + inqOrPaid + "')";
                }
            }


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    #region DESCO/DPDC Bulk Bill Pay added by Shuvo Islam 4/22/2024
    public string SaveBulkDescoAccount(string AccountIds, string Remarks, string Parameters)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;
        string space = "L";
        string inqOrPaid = "N";
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;

            //OracleCmd.CommandText = "INSERT INTO UBP_REB_BULK_BILL_PAY (UBP_REB_ACCOUNT_ID,UBP_REB_ACCOUNT_STATUS, UBP_REB_REMARKS,UBP_REB_PURPOSE) " +
            //"VALUES('" + AccountIds + "', '" + 'A' + "', '" + Remarks + "','" + Parameters + "')";

            if (Parameters == "I")
            {
                string strSqlCheck = "SELECT COUNT(*) FROM APSNG101.EDOTCO_UTILITY_BILL_PAY WHERE ACCOUNT_NUMBER='" + AccountIds + "' AND PURPOSE='I'";
                string strCheckDuplicate = ReturnString(strSqlCheck);

                if (Convert.ToInt32(strCheckDuplicate) < 1)
                {

                    OracleCmd.CommandText = "INSERT INTO APSNG101.EDOTCO_UTILITY_BILL_PAY (ACCOUNT_NUMBER,BILL_TYPE, ACCOUNT_STATUS, REMARK,PURPOSE,IS_INQUIRED,IS_PAID) " +
                       "VALUES('" + AccountIds + "','DS','" + 'A' + "', '" + Remarks + "','" + Parameters + "','" + inqOrPaid + "','" + space + "')";
                }
            }
            else if (Parameters == "P")
            {
                string strSqlCheck = "SELECT COUNT(*) FROM APSNG101.EDOTCO_UTILITY_BILL_PAY WHERE ACCOUNT_NUMBER='" + AccountIds + "' AND PURPOSE='P' AND IS_PAID='N'";
                string strCheckDuplicate = ReturnString(strSqlCheck);

                if (Convert.ToInt32(strCheckDuplicate) < 1)
                {
                    OracleCmd.CommandText = "INSERT INTO APSNG101.EDOTCO_UTILITY_BILL_PAY (ACCOUNT_NUMBER,BILL_TYPE,ACCOUNT_STATUS, REMARK,PURPOSE,IS_INQUIRED,IS_PAID) " +
                       "VALUES('" + AccountIds + "', 'DS','" + 'A' + "', '" + Remarks + "','" + Parameters + "','" + space + "','" + inqOrPaid + "')";
                }
            }

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public string SaveBulkDPDCAccount(string AccountIds, string Remarks, string Parameters)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;
        string space = "L";
        string inqOrPaid = "N";
        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;

            //OracleCmd.CommandText = "INSERT INTO UBP_REB_BULK_BILL_PAY (UBP_REB_ACCOUNT_ID,UBP_REB_ACCOUNT_STATUS, UBP_REB_REMARKS,UBP_REB_PURPOSE) " +
            //"VALUES('" + AccountIds + "', '" + 'A' + "', '" + Remarks + "','" + Parameters + "')";

            if (Parameters == "I")
            {
                string strSqlCheck = "SELECT COUNT(*) FROM APSNG101.EDOTCO_UTILITY_BILL_PAY WHERE ACCOUNT_NUMBER='" + AccountIds + "' AND PURPOSE='I'";
                string strCheckDuplicate = ReturnString(strSqlCheck);

                if (Convert.ToInt32(strCheckDuplicate) < 1)
                {

                    OracleCmd.CommandText = "INSERT INTO APSNG101.EDOTCO_UTILITY_BILL_PAY (ACCOUNT_NUMBER,BILL_TYPE, ACCOUNT_STATUS, REMARK,PURPOSE,IS_INQUIRED,IS_PAID) " +
                       "VALUES('" + AccountIds + "','DPDC','" + 'A' + "', '" + Remarks + "','" + Parameters + "','" + inqOrPaid + "','" + space + "')";
                }
            }
            else if (Parameters == "P")
            {
                string strSqlCheck = "SELECT COUNT(*) FROM APSNG101.EDOTCO_UTILITY_BILL_PAY WHERE ACCOUNT_NUMBER='" + AccountIds + "' AND PURPOSE='P' AND IS_PAID='N'";
                string strCheckDuplicate = ReturnString(strSqlCheck);

                if (Convert.ToInt32(strCheckDuplicate) < 1)
                {
                    OracleCmd.CommandText = "INSERT INTO APSNG101.EDOTCO_UTILITY_BILL_PAY (ACCOUNT_NUMBER,BILL_TYPE,ACCOUNT_STATUS, REMARK,PURPOSE,IS_INQUIRED,IS_PAID) " +
                       "VALUES('" + AccountIds + "', 'DPDC','" + 'A' + "', '" + Remarks + "','" + Parameters + "','" + space + "','" + inqOrPaid + "')";
                }
            }


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }
    
    public DataTable GetDescoAccountsDetails(string strPurpose, string DescoAccntID)
    {
        string subQuery = "";
        if (DescoAccntID != "")
        {
            subQuery = " AND ACCOUNT_NUMBER='" + DescoAccntID + "'";
        }
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            
            OracleCmd.CommandText = "Select *From APSNG101.EDOTCO_UTILITY_BILL_PAY WHERE PURPOSE='" + strPurpose + "'"+" AND BILL_TYPE='DS' " + subQuery;
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }
    public DataTable GetDPDCAccountsDetails(string strPurpose, string DescoAccntID)
    {
        string subQuery = "";
        if (DescoAccntID != "")
        {
            subQuery = " AND ACCOUNT_NUMBER='" + DescoAccntID + "'";
        }
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = "Select * From APSNG101.EDOTCO_UTILITY_BILL_PAY WHERE PURPOSE='" + strPurpose + "'"+" AND BILL_TYPE = 'DPDC' "+ subQuery;
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }
    public string DeleteDescoAccoutRow(string AutoID)
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

            OracleCmd.CommandText = "Delete From APSNG101.EDOTCO_UTILITY_BILL_PAY Where EDOTCO_BILL_PAY_ID='" + AutoID + "'";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public string UpdateDescoAccoutRow(string AutoID, string REBAccountID, string Remarks)
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

            OracleCmd.CommandText = "Update  APSNG101.EDOTCO_UTILITY_BILL_PAY Set ACCOUNT_NUMBER='" + REBAccountID + "', REMARK='" + Remarks + "' Where EDOTCO_BILL_PAY_ID='" + AutoID + "'";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    #endregion
    public DataTable GetREBAccountsDetails(string strPurpose, string REBAccntID)
    {
        string subQuery = "";
        if (REBAccntID != "")
        {
            subQuery = " AND UBP_REB_ACCOUNT_ID='" + REBAccntID + "'";
        }
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandText = "Select *From UBP_REB_BULK_BILL_PAY WHERE UBP_REB_PURPOSE='" + strPurpose + "'" + subQuery;
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }

    public bool UtilityTransctionBillProcess()
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        bool ret = false;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandText = "BEGIN PROCESS_TRAN_UB_BULK_INSERT('060'); END";
            OracleCmd.CommandType = CommandType.StoredProcedure;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            ret = true;
            return ret;
        }

        catch (Exception ex)
        {
            conn.Close();
            return ret;
        }
        finally
        {
            conn.Close();
        }
    }
    public DataTable GetREBAccountsRepor(DateTime Todate)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            //OracleCmd.CommandText = "SELECT BBS.UBP_REB_BBP_ID,BBS.UBP_REB_BBPS_MONTH,BBS.UBP_REB_BBPS_STATUS,BBS.UBP_REB_BBPS_REF,BBS.UBP_REB_BBPS_PURPOSE,BBS.UBP_REB_BBPS_TIME,"
            //                 +"UT.TOTAL_BILL_AMOUNT,UT.CHEQUE_REMARKS,BBP.UBP_REB_ACCOUNT_ID FROM UBP_REB_BULK_BILL_PAY_STATUS BBS,SERVICE_REQUEST SR, UTILITY_TRANSACTION UT,"
            //                +"UBP_REB_BULK_BILL_PAY BBP "
            //                +"WHERE BBP.UBP_REB_BBP_ID=BBS.UBP_REB_BBP_ID AND BBS.UBP_REB_BBPS_REF=SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID=UT.REQUEST_ID AND  UBP_REB_BBPS_TIME BETWEEN  TO_DATE('" + Fromdate + "', 'MM/DD/YYYY  HH:MI:SS AM')  AND TO_DATE('" + Todate + "', 'MM/DD/YYYY HH:MI:SS PM')";

           // OracleCmd.CommandText = "SELECT BBP.UBP_REB_ACCOUNT_ID,SR.SYSTEM_NOTE,SR.REQUEST_ID,BBS.UBP_REB_BBP_ID,BBS.UBP_REB_BBPS_MONTH,BBS.UBP_REB_BBPS_STATUS,BBS.UBP_REB_BBPS_REF,BBS.UBP_REB_BBPS_PURPOSE,BBS.UBP_REB_BBPS_TIME,UT.TOTAL_BILL_AMOUNT,SUBSTR(RESPONSE_LOG,INSTR(RESPONSE_LOG,'PAID_STATUS:')+15,INSTR(RESPONSE_LOG,'PBS_CODE:')-INSTR(RESPONSE_LOG,'PAID_STATUS:')-17) CHEQUE_REMARKS_OLD,REPLACE(SUBSTR(UT.RESPONSE_LOG,50, LENGTH(UT.RESPONSE_LOG)-357),'\"','') CHEQUE_REMARKS FROM UBP_REB_BULK_BILL_PAY_STATUS BBS,SERVICE_REQUEST SR, UTILITY_TRANSACTION UT,UBP_REB_BULK_BILL_PAY BBP WHERE BBS.UBP_REB_BBPS_REF=SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID=UT.REQUEST_ID (+) AND BBP.UBP_REB_BBP_ID=BBS.UBP_REB_BBP_ID AND BBS.UBP_REB_BBPS_MONTH=TO_CHAR(add_months(trunc(TO_DATE('" + Todate + "', 'MM/DD/YYYY HH:MI:SS PM'),'MM'),-1),'MM')            AND BBP.UBP_REB_PURPOSE='I' AND BBS.UBP_REB_BBPS_PURPOSE='I'";

            //OracleCmd.CommandText = "SELECT BBP.UBP_REB_ACCOUNT_ID,SR.SYSTEM_NOTE,SR.REQUEST_ID,BBS.UBP_REB_BBP_ID,BBS.UBP_REB_BBPS_MONTH,BBS.UBP_REB_BBPS_STATUS,BBS.UBP_REB_BBPS_REF,BBS.UBP_REB_BBPS_PURPOSE,BBS.UBP_REB_BBPS_TIME,UT.TOTAL_BILL_AMOUNT,REPLACE(SUBSTR(UT.RESPONSE_LOG,50),'\"','') CHEQUE_REMARKS FROM UBP_REB_BULK_BILL_PAY_STATUS BBS,SERVICE_REQUEST SR, UTILITY_TRANSACTION UT,UBP_REB_BULK_BILL_PAY BBP WHERE BBS.UBP_REB_BBPS_REF=SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID=UT.REQUEST_ID (+) AND BBP.UBP_REB_BBP_ID=BBS.UBP_REB_BBP_ID AND BBS.UBP_REB_BBPS_MONTH=TO_CHAR(add_months(trunc(TO_DATE('" + Todate + "', 'MM/DD/YYYY HH:MI:SS PM'),'MM'),-1),'MM')            AND BBP.UBP_REB_PURPOSE='I' AND BBS.UBP_REB_BBPS_PURPOSE='I' and BBP.UBP_REB_BBP_ID like '211012%'";

            OracleCmd.CommandText = "SELECT BBP.UBP_REB_ACCOUNT_ID,SR.SYSTEM_NOTE,SR.REQUEST_ID,BBS.UBP_REB_BBP_ID,BBS.UBP_REB_BBPS_MONTH,BBS.UBP_REB_BBPS_STATUS,BBS.UBP_REB_BBPS_REF,BBS.UBP_REB_BBPS_PURPOSE,BBS.UBP_REB_BBPS_TIME,UT.TOTAL_BILL_AMOUNT,REPLACE(SUBSTR(UT.RESPONSE_LOG,50),'\"','') CHEQUE_REMARKS FROM UBP_REB_BULK_BILL_PAY_STATUS BBS,SERVICE_REQUEST SR, UTILITY_TRANSACTION UT,UBP_REB_BULK_BILL_PAY BBP WHERE BBS.UBP_REB_BBPS_REF=SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID=UT.REQUEST_ID (+) AND BBP.UBP_REB_BBP_ID=BBS.UBP_REB_BBP_ID AND BBS.UBP_REB_BBPS_MONTH=TO_CHAR(add_months(trunc(TO_DATE('" + Todate + "', 'MM/DD/YYYY HH:MI:SS PM'),'MM'),-1),'MM')            AND BBP.UBP_REB_PURPOSE='I' AND BBS.UBP_REB_BBPS_PURPOSE='I' AND BBS.UBP_REB_BBPS_ID LIKE '22%'";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }

    public DataTable GetNidInfo(string strWalletID)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            //OracleCmd.CommandText = "SELECT BBS.UBP_REB_BBP_ID,BBS.UBP_REB_BBPS_MONTH,BBS.UBP_REB_BBPS_STATUS,BBS.UBP_REB_BBPS_REF,BBS.UBP_REB_BBPS_PURPOSE,BBS.UBP_REB_BBPS_TIME,"
            //                 +"UT.TOTAL_BILL_AMOUNT,UT.CHEQUE_REMARKS,BBP.UBP_REB_ACCOUNT_ID FROM UBP_REB_BULK_BILL_PAY_STATUS BBS,SERVICE_REQUEST SR, UTILITY_TRANSACTION UT,"
            //                +"UBP_REB_BULK_BILL_PAY BBP "
            //                +"WHERE BBP.UBP_REB_BBP_ID=BBS.UBP_REB_BBP_ID AND BBS.UBP_REB_BBPS_REF=SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID=UT.REQUEST_ID AND  UBP_REB_BBPS_TIME BETWEEN  TO_DATE('" + Fromdate + "', 'MM/DD/YYYY  HH:MI:SS AM')  AND TO_DATE('" + Todate + "', 'MM/DD/YYYY HH:MI:SS PM')";


            OracleCmd.CommandText = "SELECT CL.CLINT_NAME CLINT_NAME, AL.ACCNT_NO,AL.ACCNT_MSISDN, AR.RANK_TITEL, SP.SERVICE_PKG_NAME, CLINT_ADDRESS1,CLINT_ADDRESS2,CLIENT_DOB FROM CLIENT_IDENTIFICATION CI , CLIENT_LIST CL, ACCOUNT_LIST AL, ACCOUNT_RANK AR ,SERVICE_PACKAGE SP WHERE CI.CLIENT_ID=CL.CLINT_ID AND CL.CLINT_ID=AL.CLINT_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND AL.SERVICE_PKG_ID= SP.SERVICE_PKG_ID AND CI.CLINT_IDENT_NAME='" + strWalletID + "' UNION SELECT DKI.CLINT_NAME CLINT_NAME, AL.ACCNT_NO,AL.ACCNT_MSISDN, AR.RANK_TITEL, SP.SERVICE_PKG_NAME, DKI.CLIENT_PRE_ADDRESS CLINT_ADDRESS1,DKI.CLIENT_PER_ADDRESS CLINT_ADDRESS2,DKI.CLIENT_DOB  FROM DIGITAL_KYC_INFO DKI,ACCOUNT_LIST AL,CLIENT_LIST CL ,SERVICE_PACKAGE SP, ACCOUNT_RANK AR WHERE  CL.CLINT_NAME=CONCAT('+88', DKI.CLINT_MOBILE) AND CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID= SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  DKI.CLINT_NATIONAL_ID='" + strWalletID + "'UNION SELECT DKI.CLINT_NAME CLINT_NAME, AL.ACCNT_NO,AL.ACCNT_MSISDN, AR.RANK_TITEL, SP.SERVICE_PKG_NAME, DKI.CLIENT_PRE_ADDRESS CLINT_ADDRESS1,DKI.CLIENT_PER_ADDRESS CLINT_ADDRESS2,DKI.CLIENT_DOB  FROM DIGITAL_KYC_INFO DKI,ACCOUNT_LIST AL,CLIENT_LIST CL ,SERVICE_PACKAGE SP, ACCOUNT_RANK AR WHERE  CL.CLINT_MOBILE =CONCAT('+88', DKI.CLINT_MOBILE) AND CL.CLINT_ID=AL.CLINT_ID AND AL.SERVICE_PKG_ID= SP.SERVICE_PKG_ID AND AL.ACCNT_RANK_ID=AR.ACCNT_RANK_ID AND  DKI.CLINT_NATIONAL_ID='" + strWalletID + "'";

            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }




    







    public string UpdateREBAccoutRow(string AutoID, string REBAccountID, string Remarks)
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

            OracleCmd.CommandText = "Update  UBP_REB_BULK_BILL_PAY Set UBP_REB_ACCOUNT_ID='" + REBAccountID + "', UBP_REB_REMARKS='" + Remarks + "' Where UBP_REB_BBP_ID='" + AutoID + "'";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public DataTable GetKGDCLAccountsRepor(DateTime Todate, DateTime Fromdate)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            //OracleCmd.CommandText = "SELECT BBS.UBP_REB_BBP_ID,BBS.UBP_REB_BBPS_MONTH,BBS.UBP_REB_BBPS_STATUS,BBS.UBP_REB_BBPS_REF,BBS.UBP_REB_BBPS_PURPOSE,BBS.UBP_REB_BBPS_TIME,"
            //                 +"UT.TOTAL_BILL_AMOUNT,UT.CHEQUE_REMARKS,BBP.UBP_REB_ACCOUNT_ID FROM UBP_REB_BULK_BILL_PAY_STATUS BBS,SERVICE_REQUEST SR, UTILITY_TRANSACTION UT,"
            //                +"UBP_REB_BULK_BILL_PAY BBP "
            //                +"WHERE BBP.UBP_REB_BBP_ID=BBS.UBP_REB_BBP_ID AND BBS.UBP_REB_BBPS_REF=SR.SMSC_REFERENCE_NO AND SR.REQUEST_ID=UT.REQUEST_ID AND  UBP_REB_BBPS_TIME BETWEEN  TO_DATE('" + Fromdate + "', 'MM/DD/YYYY  HH:MI:SS AM')  AND TO_DATE('" + Todate + "', 'MM/DD/YYYY HH:MI:SS PM')";

            OracleCmd.CommandText = "SELECT UT.ACCOUNT_NUMBER UTILITY_TRAN_ID,UT.REQUEST_ID,UT.TRANSA_DATE,UT.SOURCE_ACC_NO,UT.NAME,UT.BILL_MONTH,UT.BILL_YEAR,UT.TOTAL_BILL_AMOUNT, DECODE(UT.RESPONSE_STATUS_BP,'000','SUCCESS','FAILED') TRANSACTION_STATUS FROM APSNG101.UTILITY_TRANSACTION UT WHERE UT.SERVICE='UBPKG'AND UT.OWNER_CODE='KGDCL' AND UT.RESPONSE_STATUS_BP='000' AND UT.RESPONSE_MSG_BP LIKE '%Congratulations! your payment done successfully.%' AND UT.TRANSA_DATE BETWEEN TO_DATE('" + Fromdate + "', 'MM/DD/YYYY  HH:MI:SS AM') AND TO_DATE('" + Todate + "', 'MM/DD/YYYY  HH:MI:SS AM')";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }

    #region Partext Barnch JubayerSohel

    public string InsertIntoPartexBranch(string name, string code, string address, string lead, string empName, string empMobile, string con, string brType)
    {
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        string strSql = "";
        strSql = @"INSERT INTO PARTEX_BRANCH_INFO(BR_NAME,BR_CODE,BR_ADDRESS,LEAD_RETAIL_SALES_MOBILE,INCHARGE_NAME,INCHARGE_MOBILE,BUSINESS_CONTROLLER_MOBILE,BR_TYPE)"
                + " VALUES('" + name + "','" + code + "','" + address + "','" + lead + "','" + empName + "','" + empMobile + "','" + con + "','" + brType + "')";
        try
        {
            OracleCommand cmd = new OracleCommand(strSql);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            return "Information Successfully Saved.";
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }
    }


    public string VerifyPartexAccount()
    {

        string strAccountBalance = "";
        string strSql = "SELECT PROCESS_TMP_BULK_REG_VERIFY ('200818000000000001') RESULT FROM DUAL";
        try
        {
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strAccountBalance = dr["RESULT"].ToString();
                }
                return strAccountBalance;
            }
            else
            {
                return strAccountBalance;

            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }
    }

    public string InsertTmpBulkServiceHandler(string mobileNo, string accountName, string dateOfBirth, string openingDate, string fatherName)
    {
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        string strSql = "";
        strSql = @"INSERT INTO TMP_BULK_REG(MOBILE_NUMBER,MOBILE_NUMBER_SORTED,ACCOUNT_NAME,OPENING_DATE,DATE_OF_BIRTH, FATHER_NAME, PROCESS_DTATUS)"
                + " VALUES('" + mobileNo + "','+88" + mobileNo + "','" + accountName + "','" + openingDate + "','" + dateOfBirth + "','" + fatherName + "', 'D')";
        try
        {
            OracleCommand cmd = new OracleCommand(strSql);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            return "Information Successfully Saved.";
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }
    }

    public string InsertPartexBranchServiceHandler(string reqText, string smsc)
    {
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        conn.Open();
        string strSql = "";
        strSql = @"INSERT INTO SERVICE_REQUEST(REQUEST_PARTY,RECEIPENT_PARTY,REQUEST_TEXT,SMSC_REFERENCE_NO,REQUEST_PARTY_TYPE)"
                + " VALUES('+8801616225129','MCOM_GATEWAY','" + reqText + "','" + smsc + "','MCOM_GATEWAY')";
        try
        {
            OracleCommand cmd = new OracleCommand(strSql);
            cmd.Connection = conn;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            return "Information Successfully Saved.";
        }
        catch (Exception ex)
        {
            return "Err:" + ex.Message.ToString();
        }
    }

    public string UpdatePartexBranchInfo(string branchId, string name, string address, string empName, string empMobile, string status)
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
            cmd.CommandText = @"UPDATE PARTEX_BRANCH_INFO SET BR_NAME = '" + name + "',BR_ADDRESS = '" + address + "',INCHARGE_NAME = '" + empName + "',INCHARGE_MOBILE = '" + empMobile + "',BR_STATUS = '" + status + "' WHERE PARTEX_BRANCH_INFO_ID = '" + branchId + "'";

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

    public string UpdatePartexBranchWalletNo(string branchCode, string brType, string walletNo)
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
            cmd.CommandText = @"UPDATE PARTEX_BRANCH_INFO SET WALLET_ID = '" + walletNo + "' WHERE BR_CODE = '" + branchCode + "' AND BR_TYPE='" + brType + "'";

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
    public string GetPartexBranchInfoByCode(string branchCode, string branchType)
    {
        string strResult = "";
        string strSQL = "SELECT BR_CODE FROM PARTEX_BRANCH_INFO WHERE BR_CODE='" + branchCode + "' AND BR_TYPE='" + branchType + "'";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(strSQL, conn);
        OracleDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            strResult = dr["BR_CODE"].ToString();

        }
        conn.Close();
        return strResult;

    }

    public string GetAccountSerial()
    {
        string strResult = "";
        string strSQL = "SELECT SERIAL_NO FROM ACCOUNT_SERIAL_DETAIL WHERE STATUS='A' AND ROWNUM=1";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(strSQL, conn);
        OracleDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            strResult = dr["SERIAL_NO"].ToString();

        }
        conn.Close();
        return strResult;

    }

    public string UpdateAccountListPartexBranchInfo(string state, string package, string rank, string accntNo)
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
            cmd.CommandText = @"UPDATE ACCOUNT_LIST SET ACCNT_STATE = '" + state + "',SERVICE_PKG_ID = '" + package + "',ACCNT_RANK_ID = '" + rank + "' WHERE ACCNT_NO = '" + accntNo + "'";

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

    public string UpdateCasAccountListPartexBranchInfo(string type, string accntNo)
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
            cmd.CommandText = @"UPDATE BDMIT_ERP_101.CAS_ACCOUNT_LIST SET CAS_ACC_TYPE_ID = '" + type + "' WHERE CAS_ACC_NO = '" + accntNo + "'";

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

    public string UpdateClientBankAccPartexBranchInfo(string accLogin, string accntNo)
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
            cmd.CommandText = @"UPDATE CLIENT_BANK_ACCOUNT SET CLINT_BANK_ACC_LOGIN = '" + accLogin + "' WHERE CLINT_BANK_ACC_NO = '" + accntNo + "'";

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


    public string DeletePartexBranch(string branchCode)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction = null;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn; //Active Connection
            OracleCmd.Transaction = dbTransaction;

            OracleCmd.CommandText = "DELETE FROM PARTEX_BRANCH_INFO WHERE BR_CODE='" + branchCode + "'";
            OracleCmd.CommandType = CommandType.Text; //Setup Command Type
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfully deleted";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "Unsuccessfull.";
        }
    }



    #endregion

    #region new_GetDetails_middleware_TechnoCell

    public DataTable GetDetails_Middleware_TechnoCell(DateTime Todate, DateTime Fromdate, string strSearchText, string strUsrIdSearchText, string strPosIdSearch, string strResSearchText, string strMeterId)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;
        string strAccNo = "";
        string strReqTxt = "";
        string strUsrId = "";
        string strPosId = "";
        string strResMsg = "";
        string strMeter = "";

        if (strSearchText != "")
        {
            strReqTxt = "AND UMT.REQUEST_ID='" + strSearchText + "' ";
        }
        if (strPosIdSearch != "")
        {
            strPosId = "AND DPDC_POS_ID='" + strPosIdSearch + "' ";
        }
        if (strUsrIdSearchText != "")
        {
            strUsrId = "AND AAMRA_USER_ID='" + strUsrIdSearchText + "' ";
        }

        if (strMeterId != "")
        {
            strMeter = "AND METER_NUMBER='" + strMeterId + "' ";
        }
        if (strResSearchText != "All")
        {
            strResMsg = "AND RESPONSE_MSG='" + strResSearchText + "'";

        }
        else
        {
            strResMsg = " ";

        }





        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();




        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            //if (strSearchText !="")
            //{
            //    OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT' " + strReqTxt + strPosId + strUsrId + strResMsg;
            //}
            //else
            //{
            //    OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT'";
            //}
            // OracleCmd.CommandText = "SELECT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED FROM UTILITY_MIDDLEWARE_TRANSACTION WHERE RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";

            //OracleCmd.CommandText = "SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT,SR.REQUEST_TIME FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,SERVICE_REQUEST SR,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_ID=UMT.REQUEST_ID AND RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND BILL_TYPE='UBPDPPM'  AND BILLER_CODE='TechnoCell'  AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT' " + strReqTxt + strPosId + strUsrId + strResMsg + strMeter + "  ORDER BY REQUEST_ID DESC";

            OracleCmd.CommandText = @"SELECT SUB1.AAMRA_USER_ID,SUB1.DPDC_POS_ID,SUB1.REF_NO,SUB1.REQUEST_ID,SUB1.RESPONSE_CODE,SUB1.RESPONSE_MSG,SUB1.RESPONSE_TIME,SUB1.METER_NUMBER,SUB1.BILL_TYPE,SUB1.IS_REVERSED,SUB1.CAS_TRAN_AMT,SUB1.REQUEST_TIME,TRUNC(SUB1.FEE,6) AS FEES,TRUNC(SUB1.FEE*15/115,6) AS VAT_1,TRUNC(SUB1.FEE-(SUB1.FEE*15/115),6) AS AFTER_VAT_1,TRUNC(((SUB1.FEE-(SUB1.FEE*15/115))*10/100),6) AS TAX_1,TRUNC((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100),6) AS AFTER_TAX_1,TRUNC(((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*30/100,6) AS BANK_COMM,TRUNC(((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*50/100,6) AS AGENT_COMM,TRUNC(((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*20/100,6) AS THIRD_COMM,TRUNC((((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*30/100)*1/3,6) MBL_COMM, TRUNC((((SUB1.FEE-(SUB1.FEE*15/115))-((SUB1.FEE-(SUB1.FEE*15/115))*10/100))*30/100)*2/3,6) AAMRA_COM FROM (SELECT DISTINCT AAMRA_USER_ID,DPDC_POS_ID,REF_NO,UMT.REQUEST_ID,RESPONSE_CODE,RESPONSE_MSG,RESPONSE_TIME,METER_NUMBER,BILL_TYPE,IS_REVERSED,CAT.CAS_TRAN_AMT,SR.REQUEST_TIME,( CASE WHEN CAT.CAS_TRAN_AMT>=0 AND CAT.CAS_TRAN_AMT<=400 THEN '5' WHEN CAT.CAS_TRAN_AMT>=401 AND CAT.CAS_TRAN_AMT<=1500 THEN '10' WHEN CAT.CAS_TRAN_AMT>=1501 AND CAT.CAS_TRAN_AMT<=5000 THEN '15' WHEN CAT.CAS_TRAN_AMT>=5001 THEN '25' END ) FEE FROM UTILITY_MIDDLEWARE_TRANSACTION UMT,SERVICE_REQUEST SR,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT WHERE SR.REQUEST_ID=UMT.REQUEST_ID AND RESPONSE_TIME BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM') AND CAT.REQUEST_ID=UMT.REQUEST_ID AND BILL_TYPE='UBPDPPM'  AND BILLER_CODE='TechnoCell'  AND CAT.CAS_TRAN_PURPOSE_CODE='FRTAMT' " + strReqTxt + strPosId + strUsrId + strResMsg + strMeter + "  ORDER BY REQUEST_ID DESC) SUB1";



            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return null;
        }
    }


    #endregion
    public DataTable ExecuteQueryV2(string strSQL)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandText = strSQL;
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }

    #region Bangladesh Bank Utility Report JubayerSohel 13-9-2022


    public string getBBankUtilityReportData(string dtpUtFromDateString, string dtpUtToDateString, string type, string gender)
    {

        string strReturn = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();


        try
        {
            string strQry = @"SELECT NVL(SUM(SUB1.CNT),0) CNTT,NVL(SUM(SUB1.AMOUNT),0) AMTT FROM (SELECT COUNT ( * ) CNT, SUM (TOTAL_BILL_AMOUNT) AMOUNT FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, CLIENT_LIST CL,MANAGE_THANA MT WHERE SERVICE IN ('UBP', 'UBPW') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') union all SELECT COUNT ( * ) CNT, SUM (TOTAL_BILL_AMOUNT) AMOUNT FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, CLIENT_LIST CL,MANAGE_THANA MT WHERE SERVICE IN ('UBP', 'UBPW', 'UWZP', 'UBPKG', 'UBPREB', 'UBPDSP') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND AL.ACCNT_RANK_ID = '161215000000000004' union all SELECT COUNT ( * ) CNT, SUM (TOTAL_BILL_AMOUNT) AMOUNT FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, CLIENT_LIST CL,MANAGE_THANA MT WHERE SERVICE IN ('UBPKG') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND AL.ACCNT_RANK_ID IN ('120519000000000005', '120519000000000006') union all SELECT COUNT ( * ) CNT, SUM (CAT.CAS_TRAN_AMT) AMOUNT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT, ACCOUNT_LIST AL, CLIENT_LIST CL,MANAGE_THANA MT WHERE UMT.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'TOTAMT' AND UMT.IS_REVERSED = 'N' AND UMT.RESPONSE_CODE = '0001' AND UMT.RESPONSE_MSG = 'Success' AND UMT.BILL_TYPE = 'UBPDSPM' AND UMT.AAMRA_USER_ID=AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND TRUNC (UMT.RESPONSE_TIME) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' union all SELECT COUNT ( * ) CNT, SUM (CAT.CAS_TRAN_AMT) AMOUNT FROM UTILITY_MIDDLEWARE_TRANSACTION UMT, BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CAT, ACCOUNT_LIST AL, CLIENT_LIST CL,MANAGE_THANA MT WHERE UMT.REQUEST_ID = CAT.REQUEST_ID AND CAT.CAS_TRAN_PURPOSE_CODE = 'TOTAMT' AND UMT.IS_REVERSED = 'N' AND UMT.RESPONSE_CODE = '0001' AND UMT.RESPONSE_MSG = 'Success' AND UMT.BILL_TYPE = 'DPDC' AND UMT.AAMRA_USER_ID=AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND TRUNC (UMT.RESPONSE_TIME) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' union all SELECT COUNT ( * ) CNT, SUM (BILL_AMOUNT) AMOUNT FROM DPDC_PREPAID_BILL_COL_DETAIL DPB,ACCOUNT_LIST AL, CLIENT_LIST CL,MANAGE_THANA MT WHERE EXECUTE_STATUS = 'Success' AND DPB.AGENT_ACCNT_NO=AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND TRUNC (EXECUTE_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' union all SELECT COUNT ( * ) CNT, SUM (TOTAL_BILL_AMOUNT) AMOUNT FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, CLIENT_LIST CL,MANAGE_THANA MT WHERE SERVICE IN ('UBPDSP') AND REQUEST_ID IS NOT NULL AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' union all SELECT COUNT ( * ) CNT, SUM (TOTAL_BILL_AMOUNT) AMOUNT FROM UTILITY_TRANSACTION UT, ACCOUNT_LIST AL, CLIENT_LIST CL,MANAGE_THANA MT WHERE SERVICE IN ('UBPREB') AND REQUEST_ID IS NOT NULL AND RESPONSE_STATUS_BP = '000' AND TRUNC (TRANSA_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' AND UT.SOURCE_ACC_NO = AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND AL.ACCNT_RANK_ID IN('120519000000000005','120519000000000006','210111000000000001'))SUB1";
            OracleCommand cmd = new OracleCommand(strQry, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturn = dr["CNTT"].ToString() + "*" + dr["AMTT"].ToString();
                }
                return strReturn;
            }
            else
            {
                return strReturn;
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }


    }

    public string getBBankFMReportData(string dtpUtFromDateString, string dtpUtToDateString, string type, string gender)
    {

        string strReturn = "";
        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();


        try
        {
            string strQry = @"SELECT NVL(SUM(SUB1.CNT),0) CNTT,NVL(SUM(SUB1.AMT),0) AMTT FROM (SELECT COUNT ( * ) CNT, SUM (AMOUNT) AMT FROM   PARTEX_FUND_COLLECTION PF,ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT WHERE PF.SOURCE_ACCNT_NO=AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME  " + type + " AND   SERVICE_TYPE = 'FM' AND DESTINATION_ACCNT_NO LIKE '0110100%' AND TRUNC (REQUEST_TIME) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' UNION SELECT COUNT ( * ) CNT, SUM (AMOUNT) AMT FROM   PARTEX_FUND_COLLECTION PF,ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT WHERE PF.SOURCE_ACCNT_NO=AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME  " + type + " AND   SERVICE_TYPE = 'FM' AND DESTINATION_ACCNT_NO LIKE '0110101%' AND TRUNC (REQUEST_TIME) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' UNION SELECT COUNT ( * ) CNT, SUM (AMOUNT) AMT FROM   RLIL_FUND_COLLECTION PF,ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT WHERE PF.SOURCE_ACCNT_NO=AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME  " + type + " AND   SERVICE_TYPE = 'FM' AND TRUNC (REQUEST_TIME) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' UNION SELECT COUNT ( * ) CNT, SUM (AMOUNT) AMT FROM   PILIL_FUND_COLLECTION PF,ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT WHERE PF.SOURCE_ACCNT_NO=AL.ACCNT_NO AND AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME  " + type + " AND   SERVICE_TYPE = 'FM' AND TRUNC (TXN_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' union SELECT COUNT ( * ) CNT, SUM (TRANSACTION_AMOUNT) AMT FROM   (  SELECT   DISTINCT THA.DEL_ACCNT_NO DIS_ACC_NO, CLDIS.CLINT_NAME DIS_NAME, CLDIS.CLINT_ADDRESS1 DIS_ADDR, MTDIS.THANA_NAME DIS_THANA, MDDIS.DISTRICT_NAME DIS_DISTRICT, TMIS.TRANSACTION_AMOUNT TRANSACTION_AMOUNT, TMIS.REQUEST_ID FROM   TEMP_MIS_TRANSACTIONS_REPORT TMIS, ACCOUNT_LIST ALDSE, ACCOUNT_LIST ALCOR, CLIENT_LIST CL, MANAGE_THANA MT, TEMP_HIERARCHY_LIST_ALL THA, ACCOUNT_LIST ALDIS, CLIENT_LIST CLDIS, MANAGE_THANA MTDIS, MANAGE_DISTRICT MDDIS WHERE   TMIS.SERVICE_CODE = 'FM' AND TRUNC (TMIS.TRANSACTION_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' AND TMIS.REQUEST_PARTY || '1' = ALDSE.ACCNT_NO AND ALDSE.ACCNT_RANK_ID IN ('120519000000000004', '180128000000000007') AND TMIS.RECEPENT_PARTY = ALCOR.ACCNT_NO AND ALCOR.ACCNT_RANK_ID IN ('140917000000000004', '160306000000000002', '180416000000000001') AND THA.SA_ACCNT_NO = ALDSE.ACCNT_NO AND ALDIS.ACCNT_NO = THA.DEL_ACCNT_NO AND ALDIS.CLINT_ID = CLDIS.CLINT_ID AND CLDIS.THANA_ID = MTDIS.THANA_ID(+) AND MTDIS.THANA_ID = MDDIS.DISTRICT_ID(+) AND ALCOR.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME  " + type + " ORDER BY   THA.DEL_ACCNT_NO ASC) TEMP UNION SELECT COUNT ( * ) CNT, SUM (TRANSACTION_AMOUNT) AMT FROM   TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT WHERE  AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND   TM.SERVICE_CODE = 'FM' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' AND TM.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('181219000000000002') UNION SELECT COUNT ( * ) CNT, SUM (TRANSACTION_AMOUNT) AMT FROM   TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT WHERE  AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME  " + type + " AND   TM.SERVICE_CODE = 'FM' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' AND TM.RECEPENT_PARTY = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('190519000000000001') UNION SELECT COUNT ( * ) CNT, SUM (TRANSACTION_AMOUNT) AMT FROM   TEMP_MIS_TRANSACTIONS_REPORT TM, ACCOUNT_LIST AL,CLIENT_LIST CL,MANAGE_THANA MT WHERE  AL.CLINT_ID=CL.CLINT_ID AND CL.CLINT_GENDER=" + gender + " AND MT.THANA_ID=(CASE WHEN CL.THANA_ID IS NOT NULL THEN CL.THANA_ID ELSE '121203000000000360' END) AND MT.THANA_NAME " + type + " AND   TM.SERVICE_CODE = 'FM' AND TRUNC (TM.TRANSACTION_DATE) BETWEEN '" + dtpUtFromDateString + "' AND '" + dtpUtToDateString + "' AND TM.REQUEST_PARTY || '1' = AL.ACCNT_NO AND AL.ACCNT_RANK_ID IN ('200813000000000003'))SUB1";
            OracleCommand cmd = new OracleCommand(strQry, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strReturn = dr["CNTT"].ToString() + "*" + dr["AMTT"].ToString();
                }
                return strReturn;
            }
            else
            {
                return strReturn;
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
            conn.Close();
        }


    }
    #endregion


    #region registration verification - Add By Abdul Bari 15.9.2022
   
    public string VerifyAccount(string WalletID)
    {

        clsAccountHandler objServiceHandler = new clsAccountHandler();
        var result = "";

        // hard data ClientId
        string ClientId = "WEB";


        if (ClientId != "")
        {
            string strMSISDN = "", strProMsg = "", strRequistID = "";
            string strAccountID;
            string strToken;

            if (WalletID != "")
            {
                strMSISDN = "+88" + WalletID.Substring(0, 11);
            }

            DataSet dtsAccDetail = objServiceHandler.GetAccountDetailKYC(strMSISDN);
            if (dtsAccDetail.Tables["ACCOUNT_LIST_ONLY_NEW"].Rows.Count > 0)
            {

                //-------------------   KYC Details Info ------------------------------
                DataRow dRow = dtsAccDetail.Tables["ACCOUNT_LIST_ONLY_NEW"].Rows[0];
                strAccountID = dRow["ACCNT_ID"].ToString();
                strToken = dRow["ACCCT_AC_TOKEN"].ToString();

                //-------------------   KYC Verified ------------------------------
                //string strReturn = objServiceHandler.KYCVerifyed(strAccountID, strToken, strMSISDN, "", strIP, strHostName + " [" + System.Environment.UserName + "]");

                //string strReturn = objServiceHandler.KYCVerifyed(strAccountID, strToken, strMSISDN, "", "", "" + "");

                //--------------- Update Client List table for verification -----------------------
                string strUpdateDate = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", DateTime.Now);
                string strUpdateMsg = objServiceHandler.UpdateClientListForVerification(ClientId, strUpdateDate, strMSISDN);
                //---------- checking requist id for commission --------------------------- 
                // strRequistID = objServiceHandler.ReturnOneColumnValue(strMSISDN);
                //strRequistID = objServiceHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_SERIAL_DETAIL", "REQUEST_ID", "CUSTOMER_MOBILE_NO", strMSISDN);
                //if (strRequistID != "")
                //{
                //    //------------------ execute procedure -------------------
                //    strProMsg = objServiceHandler.ExecAccRGComm(strMSISDN, strAccountID, strRequistID);
                //}
                //-------------------Account_List Status and Account_Serial_Detail verification Status--------- 
                string mobileNo = WalletID.Substring(0, 11);
                //string strMsg = objServiceHandler.UpdateAccountList(strAccountID);

                string strMsg2 = objServiceHandler.UpdateDKYC(mobileNo);
                // string strMsgStatus = objServiceHandler.UpdateVerifiedStatus(strMSISDN);
                //------------------------ Execute Commission Procedure for Distributor--------------------------
                //string strSQLProcedure = "";
                //strSQLProcedure = "PKG_MB_NATIVE_TRANSACTION.PRO_DISTRIBUTOR_REG_COMI('" + strMSISDN.ToString() + "')";
                //string strMsg12 = objServiceHandler.ExecuteProcedure(strSQLProcedure);

                //if (WalletID.Trim().Length == 12)
                //{
                //    objServiceHandler.GenerateQrCodeManually_v2(WalletID.Trim());
                //}

                result = "Verifyed successfully.";
            }
            else
            {
                result = "Insert correct wallet ID";
            }
        }
        return result;
    }


    public IDictionary<string, string> KONA_eKYC_NID_Data(string strDecrypt_CLIENT_NID, string strDecrypt_CLIENT_MobileNo)
    {
        string strSql = "SELECT * FROM DIGITAL_KYC_INFO WHERE CLINT_NATIONAL_ID ='" + strDecrypt_CLIENT_NID + "' AND CLINT_MOBILE = '" + strDecrypt_CLIENT_MobileNo + "'";

        IDictionary<string, string> data = new Dictionary<string, string>();
        data.Add("status", "Failed");

        OracleConnection conn = new OracleConnection(strConString);
        conn.Open();
        try
        {
            OracleCommand cmd = new OracleCommand(strSql, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    data.Add("nid", dr["CLINT_NATIONAL_ID"].ToString());
                    data.Add("name", dr["CLINT_NAME"].ToString());
                    data.Add("fatherName", dr["CLINT_FATHER_NAME"].ToString());
                    data.Add("motherName", dr["CLINT_MOTHER_NAME"].ToString());
                    data.Add("spouseName", dr["CLINT_HUSBAND_NAME"].ToString());
                    data.Add("dateOfBirth", dr["CLIENT_DOB"].ToString());
                    data.Add("gender", dr["CLIENT_GENDER"].ToString());
                    data.Add("presentAddress", dr["CLIENT_PRE_ADDRESS"].ToString());
                    data.Add("permanentAddress", dr["CLIENT_PER_ADDRESS"].ToString());
                    data.Add("contactNo", dr["CLINT_MOBILE"].ToString());

                    data.Add("nomineeName", dr["NOMINEE_NAME"].ToString());
                    data.Add("nomineeDob", "01-01-2000"); // must change :TODO

                    data.Add("ocrReferenceID", dr["OCR_SYSTEM_REF_ID"].ToString());
                    data.Add("faceMatchReferenceId", dr["FACE_SYSTEM_REF_ID"].ToString());
                    data.Add("nidFrontUrl", dr["KONA_NID_FRONT_URL"].ToString());
                    data.Add("nidBackUrl", dr["KONA_NID_BACK_URL"].ToString());
                    data.Add("userImageUrl", dr["KONA_USER_IMAGE_URL"].ToString());
                    data.Add("status", "Success");
                }
            }
            return data;
        }
        catch (Exception ex)
        {
            return data;
        }
        finally
        {
            conn.Close();
        }
    }

    public string UpdateUserData(string isSaved, string ekycTrackingId, string customerMobileNo, string customerNID)
    {
        string strResult = "";
        string strSql = "";
        OracleConnection conn = new OracleConnection(strConString);
        OracleTransaction dbTransaction = null;
        try
        {
            strSql = "UPDATE DIGITAL_KYC_INFO SET KONA_IS_SAVED = '" + isSaved + "', KONA_EKYC_TRACKING_ID = '" + ekycTrackingId + "' WHERE CLINT_MOBILE = '" + customerMobileNo + "' AND CLINT_NATIONAL_ID = '" + customerNID + "'";

            conn.Open();
            /// Submit Service request Table
            dbTransaction = conn.BeginTransaction();

            OracleCommand cmd = conn.CreateCommand();

            cmd.CommandText = strSql;
            cmd.Transaction = dbTransaction;
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            strResult = "Updated";
            conn.Close();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            conn = null;
        }

        return strResult;
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

    #endregion

    public string getRebBalance()
    {
        try
        {
            string strResult = "";
            string strSQL = "SELECT GET_FIS_BALANCE('000000000701') as balance FROM DUAL";
            OracleConnection conn = new OracleConnection(strConString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            OracleDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strResult = dr["balance"].ToString();

            }
            conn.Close();
            return strResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Data Deserialization
    public IDictionary<string, string> DataDeserialization(string someString)
    {
        IDictionary<string, string> pair = new Dictionary<string, string>();

        string[] stringValue = someString.Split('*');

        foreach (var item in stringValue)
        {
            var value = item.Split('~');
            pair.Add(value[0], value[1]);
        }

        return pair;
    }
    #endregion
    #region REB Meter Rent BL
    public string SaveRebMRBL(string smsAccountNo, string mrent)
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

            OracleCmd.CommandText = "INSERT INTO APSNG101.UBP_REB_METERRENT_BL (SMS_ACC_NO, MRENT)" +
                "VALUES('" + smsAccountNo + "', '" + mrent + "')";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "Failed";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    public DataTable GetREBBLAccountsDetails(string smsAccNo)
    {
        string subQuery = "";
        if (smsAccNo != "")
        {
            subQuery = "SMS_ACC_NO='" + smsAccNo + "'";
        }
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandText = "Select * From APSNG101.UBP_REB_METERRENT_BL WHERE SMS_ACC_NO='" + smsAccNo + "'";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            dbTransaction.Commit();
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }

    public string UpdateREBBLAccoutRow(string AutoID, string REBAccountID, string Remarks)
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

            OracleCmd.CommandText = "Update  APSNG101.UBP_REB_METERRENT_BL Set SMS_ACC_NO='" + REBAccountID + "', MRENT='" + Remarks + "' Where UBP_REB_MR_ID='" + AutoID + "'";


            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            dbTransaction.Commit();
            return "Successfull.";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            conn.Close();
            conn = null;
        }
    }

    #endregion

    #region testFahim
    public DataTable testLoadFahim()
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;

        conn = new OracleConnection(strConString);
        conn.Open();
        DataTable dt = new DataTable();
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            //OracleCmd.Transaction = dbTransaction;
            OracleCmd.CommandText = "SELECT DISTINCT TEST_EDU_INS_PK_ID, TEST_EDU_INS_NAME, TEST_EDU_INST_REF_ID, TEST_ACCOUNT_NO, TEST_STATUS, TEST_OWNER_CODE, TEST_BDMIT_INTERNAL_INS_CODE FROM TEST_EDUCATIONAL_INSTITUTE ORDER BY TEST_EDU_INS_PK_ID DESC";
            OracleCmd.CommandType = CommandType.Text;
            OracleCmd.ExecuteNonQuery();
            OracleDataAdapter sda = new OracleDataAdapter(OracleCmd);
            sda.Fill(dt);
            return dt;
        }

        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }
    #endregion


}
