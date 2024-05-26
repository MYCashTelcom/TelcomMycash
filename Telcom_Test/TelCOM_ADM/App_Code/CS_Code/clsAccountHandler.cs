using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;

/// <summary>
/// Summary description for clsAccountHandler
/// </summary>
public class clsAccountHandler
{
    public static string strConString;
    // public string strOracleCon;
   
   // private OracleTransaction dbTransaction = null;
    private OracleConnection conn;
	public clsAccountHandler()
	{
        strConString = ConfigurationSettings.AppSettings["dbConnectionString"]; 
        //strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
	}

    //zone code
     public DataSet GetZoneCode()
    {
        // OracleConnection
        conn = new OracleConnection(strConString);
        //OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT DISTINCT (REGIONCODE || '  ['||REGIONNAME || ']')REGIONNAME,REGIONCODE FROM COMMISSION_DATA ORDER BY REGIONCODE", conn));
        //OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT DISTINCT (REGIONCODE || '  ['||REGIONNAME || ']')REGIONNAME,TO_NUMBER(REGIONCODE)REGIONCODE FROM COMMISSION_DATA ORDER BY REGIONCODE ASC", conn));
        OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT DISTINCT (REGIONCODE || '  ['||decode(REGIONNAME,'Bogra','Rangpur',REGIONNAME) || ']')REGIONNAME,TO_NUMBER(REGIONCODE)REGIONCODE FROM COMMISSION_DATA ORDER BY REGIONCODE ASC", conn));
        try
        {
            DataSet oDS = new DataSet();
            oOrdersDataAdapter.Fill(oDS, "COMMISSION_DATA");
            return oDS;
        }
        catch (Exception e)
        {
            e.Message.ToString();
            return null;
        }


    }
     //Distributer 
     public DataSet GetDistributer( string Distributer)
     {
         // OracleConnection
         conn = new OracleConnection(strConString);
         //OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT DISTINCT (DISTRIBUTORCODE || '  ['|| REGIONNAME ||' ]'||' [' || DISTRIBUTORNAME||']') DISTRIBUTORNAME, (DISTRIBUTORCODE ) DISTRIBUTORCODE FROM COMMISSION_DATA  WHERE  REGIONCODE='"+Distributer+"' order by DISTRIBUTORNAME ", conn));
         OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT DISTINCT (DISTRIBUTORCODE || '  ['|| decode(REGIONNAME,'Bogra','Rangpur',REGIONNAME) ||' ]'||' [' || DISTRIBUTORNAME||']') DISTRIBUTORNAME, (DISTRIBUTORCODE ) DISTRIBUTORCODE FROM COMMISSION_DATA WHERE  REGIONCODE='" + Distributer + "' order by DISTRIBUTORNAME ", conn));

         try
         {
             DataSet oDS = new DataSet();
             oOrdersDataAdapter.Fill(oDS, "COMMISSION_DATA");
             return oDS;
         }
         catch //(Exception e)
         {
             return null;
         }


     }
     //RSP code 
     public DataSet GetRSPCode(string RSPCode)
     {
         //OracleConnection
          conn = new OracleConnection(strConString);
         OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("  SELECT DISTINCT ( replace(RSPNAME,'/',' ') || ' ['||RSPCODE ||']')RSPNAME,RSPCODE FROM COMMISSION_DATA WHERE  DISTRIBUTORCODE ='" + RSPCode + "'AND RSPCODE<>' 'order by RSPNAME ", conn));

         try
         {
             DataSet oDS = new DataSet();
             oOrdersDataAdapter.Fill(oDS, "COMMISSION_DATA");
             return oDS;
         }
         catch //(Exception e)
         {
             return null;
         }


     }
    //get Database Information
     public DataSet GetDataInfo(string RSPCode,string strFromDate,string strToDate,RadioButtonList rdbltype)
     {
         DataSet oDS = new DataSet();
         if(rdbltype.SelectedValue.ToString()=="CO")
         {
          // OracleConnection
          conn = new OracleConnection(strConString);
         //OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT *  FROM ALL_COMMISSION_BANG WHERE RSPCODE='" + RSPCode + "' and ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "'", conn));
         
        
         string strQuery = "";

         strQuery = " select clint_name, distributorname, rspname, CB.accnt_no, clint_address1, accnt_msisdn, "
          + "comi_strat_date, comi_end_date,comi_type_name, productname,sum(retailer_amt)retailer_amt,com_oer_act, sum(activation)ACTIVATION, "
          + "SUM(elijable)elijable,clint_passport_no, rspcode, comi_master_id from ALL_COMMISSION_BANG CB, "
          + "(select distinct accnt_no from ALL_COMMISSION_BANG  where  RSPCODE='" + RSPCode + "' AND  activationdate "
          + "between '" + strFromDate + "' and '" + strToDate + "' AND commissiontype = 1 AND BOTH='N' "
          + "AND elijable<>0)T  where CB.ACCNT_NO=T.ACCNT_NO AND  RSPCODE='" + RSPCode + "' AND activationdate "
          + "between '" + strFromDate + "' and '" + strToDate + "' AND commissiontype = 1  group by clint_name, distributorname, rspname, CB.accnt_no, "
          + "clint_address1, accnt_msisdn, comi_strat_date, comi_end_date,  comi_type_name, productname, com_oer_act, elijable, "
          + "clint_passport_no, rspcode, comi_master_id";

         //strQuery = "select clint_name, distributorname, rspname, accnt_no, clint_address1, accnt_msisdn, comi_strat_date, comi_end_date, "
         //              + "comi_type_name, productname, sum(retailer_amt)retailer_amt, com_oer_act, sum(activation)ACTIVATION, SUM(ACTIVATION)-SUM(ELIJABLE)elijable, "
         //              + "clint_passport_no, rspcode, comi_master_id from ALL_COMMISSION_BANG "
         //              + "where RSPCODE='" + RSPCode + "' AND activationdate between '" + strFromDate + "' and '" + strToDate + "' AND commissiontype = 1 "
         //              + "group by clint_name, distributorname, rspname, accnt_no, clint_address1, accnt_msisdn, comi_strat_date, comi_end_date, "
         //              + "comi_type_name, productname, com_oer_act, elijable, clint_passport_no, rspcode, comi_master_id";
         OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strQuery, conn));
         // Added By Priscila (Sep 7,2011)
        
         
         try
         {
             
             oOrdersDataAdapter.Fill(oDS, "ALL_COMMISSION_BANG");
             return oDS;
         }
         catch //(Exception e)
         {
             return null;
         }
         }
         if (rdbltype.SelectedValue.ToString() == "RE")
         {
             //OracleConnection
             conn = new OracleConnection(strConString);
             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("select cr.clint_name,cr.distributorname,cr.rspname,cr.accnt_no,cr.clint_address1,cr.accnt_msisdn,cr.comi_strat_date,cr.comi_end_date,cr.comi_type_name,cr.productname,SUM(cr.retailer_amt)retailer_amt,cr.com_oer_act,sum(cr.activation)activation,cr.elijable,cr.clint_passport_no,cr.rspcode,cr.commissiontype from all_commission_refill_new cr,(select sum(activation) activation,accnt_no from  all_commission_refill_new WHERE  ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' and rspcode='" + RSPCode + "' and COMMISSIONTYPE=2 AND ACTIVATION>0  group by accnt_no)T WHERE  cr.accnt_no =T.accnt_no and cr.rspcode='" + RSPCode + "'and cr.ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' group by cr.clint_name,cr.distributorname,cr.rspname,cr.accnt_no,cr.clint_address1,cr.accnt_msisdn,cr.comi_strat_date,cr.comi_end_date,cr.comi_type_name,cr.productname,cr.com_oer_act,cr.elijable,cr.clint_passport_no,cr.rspcode,cr.commissiontype", conn));
             //OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("select cr.clint_name,cr.distributorname,cr.rspname,cr.accnt_no,cr.clint_address1,cr.accnt_msisdn,cr.comi_strat_date,cr.comi_end_date,cr.comi_type_name,cr.productname,T.retailer_amt,T.com_oer_act,sum(cr.activation)activation,cr.elijable,cr.clint_passport_no,cr.rspcode,cr.commissiontype from all_commission_refill_new cr,(select sum(activation) activation,accnt_no,sum(retailer_amt)retailer_amt,com_oer_act from  all_commission_refill_new WHERE  ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' and rspcode='" + RSPCode + "' and COMMISSIONTYPE=2 AND ACTIVATION>0  group by accnt_no,com_oer_act)T WHERE  cr.accnt_no =T.accnt_no and cr.rspcode='" + RSPCode + "' and cr.ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' group by cr.clint_name,cr.distributorname,cr.rspname,cr.accnt_no,cr.clint_address1,cr.accnt_msisdn,cr.comi_strat_date,cr.comi_end_date,cr.comi_type_name,cr.productname,T.RETAILER_AMT,T.com_oer_act,cr.elijable,cr.clint_passport_no,cr.rspcode,cr.commissiontype", conn));

             //string strsql = "select cr.clint_name,cr.distributorname,cr.rspname,cr.accnt_no,cr.clint_address1,cr.accnt_msisdn,cr.comi_strat_date,cr.comi_end_date,cr.comi_type_name,cr.productname,T.retailer_amt,T.com_oer_act,sum(cr.activation)activation,cr.elijable,cr.clint_passport_no,cr.rspcode,cr.commissiontype from all_commission_refill_new cr,(select sum(activation) activation,accnt_no,sum(retailer_amt)retailer_amt,com_oer_act from  all_commission_refill_new WHERE  ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' and rspcode='" + RSPCode + "' and COMMISSIONTYPE=2 AND ACTIVATION>0  group by accnt_no,com_oer_act)T WHERE  cr.accnt_no =T.accnt_no and cr.rspcode='" + RSPCode + "' and cr.ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' group by cr.clint_name,cr.distributorname,cr.rspname,cr.accnt_no,cr.clint_address1,cr.accnt_msisdn,cr.comi_strat_date,cr.comi_end_date,cr.comi_type_name,cr.productname,T.RETAILER_AMT,T.com_oer_act,cr.elijable,cr.clint_passport_no,cr.rspcode,cr.commissiontype";
             string strsql = "select cr.clint_name,cr.distributorname,cr.rspname,cr.accnt_no,cr.clint_address1,cr.accnt_msisdn,cr.comi_strat_date,cr.comi_end_date,cr.comi_type_name,cr.productname,SUM(cr.retailer_amt)retailer_amt,cr.com_oer_act,sum(cr.activation)activation,cr.elijable,cr.clint_passport_no,cr.rspcode,cr.commissiontype from all_commission_refill_new cr,(select sum(activation) activation,accnt_no from  all_commission_refill_new WHERE  ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' and rspcode='" + RSPCode + "' and COMMISSIONTYPE=2 AND ACTIVATION>0  group by accnt_no)T WHERE  cr.accnt_no =T.accnt_no and cr.rspcode='" + RSPCode + "'and cr.ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' group by cr.clint_name,cr.distributorname,cr.rspname,cr.accnt_no,cr.clint_address1,cr.accnt_msisdn,cr.comi_strat_date,cr.comi_end_date,cr.comi_type_name,cr.productname,cr.com_oer_act,cr.elijable,cr.clint_passport_no,cr.rspcode,cr.commissiontype";
             //string strsql = "select clint_name,distributorname,rspname,accnt_no,clint_address1,accnt_msisdn,comi_strat_date,comi_end_date,comi_type_name,productname,SUM(retailer_amt)retailer_amt,com_oer_act,sum(activation)activation,elijable,clint_passport_no,rspcode,commissiontype from all_commission_refill_new WHERE rspcode='" + RSPCode + "' and ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "' group by clint_name,distributorname,rspname,accnt_no,clint_address1,accnt_msisdn,comi_strat_date,comi_end_date,comi_type_name,productname,com_oer_act,elijable,clint_passport_no,rspcode,commissiontype";
             //and ELIGIBLE_AVAILABLE ('" + RSPCode + "','" + strFromDate + "','" + strToDate + "')<>0
             try
             {
                 oOrdersDataAdapter.Fill(oDS, "ALL_COMMISSION_REFILL_NEW");
                 return oDS;
             }
             catch //(Exception e)
             {
                 return null;
             }
         }
         if (rdbltype.SelectedValue.ToString() == "US")
         {
             //OracleConnection
             conn = new OracleConnection(strConString);
             //OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT *  FROM ALL_COMMISSION_BANG WHERE RSPCODE='" + RSPCode + "' and ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "'", conn));

            
             string strQuery = "";

             strQuery = "select clint_name, distributorname, rspname, accnt_no, clint_address1, accnt_msisdn, comi_strat_date, comi_end_date, "
                           + "comi_type_name, productname, sum(retailer_amt)retailer_amt, com_oer_act, sum(activation)ACTIVATION, elijable, "
                           + "clint_passport_no, rspcode, comi_master_id from ALL_COMMISSION_BANG_REFILL "
                           + "where RSPCODE='" + RSPCode + "' AND activationdate between '" + strFromDate + "' and '" + strToDate + "' AND (commissiontype =2 OR COMMISSIONTYPE=3) "
                           + "group by clint_name, distributorname, rspname, accnt_no, clint_address1, accnt_msisdn, comi_strat_date, comi_end_date, "
                           + "comi_type_name, productname, com_oer_act, elijable, clint_passport_no, rspcode, comi_master_id";
             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strQuery, conn));
            


             try
             {

                 oOrdersDataAdapter.Fill(oDS, "ALL_COMMISSION_BANG_REFILL");
                 return oDS;
             }
             catch //(Exception e)
             {
                 return null;
             }
         }
         if (rdbltype.SelectedValue.ToString() == "BO")
         {
             OracleConnection conn = new OracleConnection(strConString);
             //OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("SELECT *  FROM ALL_COMMISSION_BANG WHERE RSPCODE='" + RSPCode + "' and ACTIVATIONDATE between '" + strFromDate + "' and '" + strToDate + "'", conn));


             string strQuery = "";

             strQuery = " select clint_name, distributorname, rspname, CB.accnt_no, clint_address1, accnt_msisdn, "
              + "comi_strat_date, comi_end_date,comi_type_name, productname,sum(retailer_amt)retailer_amt,com_oer_act, sum(activation)ACTIVATION, "
              + "SUM(elijable)elijable,clint_passport_no, rspcode, comi_master_id from ALL_COMMISSION_BANG_BOTH CB, "
              + "(select distinct accnt_no from ALL_COMMISSION_BANG_BOTH  where  RSPCODE='" + RSPCode + "' AND  activationdate "
              + "between '" + strFromDate + "' and '" + strToDate + "' AND commissiontype = 1 AND BOTH='Y' "
              + "AND elijable<>0)T  where CB.ACCNT_NO=T.ACCNT_NO AND  RSPCODE='" + RSPCode + "' AND activationdate "
              + "between '" + strFromDate + "' and '" + strToDate + "' AND commissiontype = 1  group by clint_name, distributorname, rspname, CB.accnt_no, "
              + "clint_address1, accnt_msisdn, comi_strat_date, comi_end_date,  comi_type_name, productname, com_oer_act, elijable, "
              + "clint_passport_no, rspcode, comi_master_id";

             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strQuery, conn));
             


             try
             {

                 oOrdersDataAdapter.Fill(oDS, "ALL_COMMISSION_BANG_BOTH");
                 return oDS;
             }
             catch //(Exception e)
             {
                 return null;
             }
         }


         return oDS;
     }
     public DataSet GetRSPCodeSing(string RSPCode)
     {

         OracleConnection conn = new OracleConnection(strConString);
         OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand("  SELECT DISTINCT ( replace(RSPNAME,'/',' ') || ' ['||RSPCODE ||']')RSPNAME,RSPCODE FROM COMMISSION_DATA WHERE  RSPCODE ='" + RSPCode + "'order by RSPNAME ", conn));

         try
         {
             DataSet oDS = new DataSet();
             oOrdersDataAdapter.Fill(oDS, "COMMISSION_DATA");
             return oDS;
         }
         catch //(Exception e)
         {
             return null;
         }


     }

     /********Save MSISDN NO * ********/
     public string AddAccount( string strAccountID,string strServiceID,string strClientID, string strPosCode,   
                                 string stACCMT,string strACCStatus )
     {
         string strSql;

         if (strAccountID.Equals(""))
         {
             strSql = "SELECT CLINT_ID,ACCNT_STATE,ACCNT_NO,SERVICE_PKG_ID,ACCNT_MSISDN from ACCOUNT_LIST";
         }
         else
         {
             strSql = "SELECT ACCNT_ID,CLINT_ID,ACCNT_STATE,ACCNT_NO,SERVICE_PKG_ID,ACCNT_MSISDN from ACCOUNT_LIST  WHERE ACCNT_ID = '" + strAccountID + "'";
         }
         try
         {
             DataRow oOrderRow;
             OracleConnection conn = new OracleConnection(strConString);
             DataSet oDS = new DataSet();
             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
             OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
             oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
             oOrdersDataAdapter.Fill(oDS, "ACCOUNT_LIST");


             // Insert the Data
             if (strAccountID.Equals(""))
             {
                 oOrderRow = oDS.Tables["ACCOUNT_LIST"].NewRow();
             }
             else
             {
                 oOrderRow = oDS.Tables["ACCOUNT_LIST"].Rows.Find(strAccountID);
             }

             oOrderRow["SERVICE_PKG_ID"] = strServiceID;
             oOrderRow["CLINT_ID"] = strClientID;
             oOrderRow["ACCNT_NO"] = strPosCode;
             oOrderRow["ACCNT_MSISDN"] = stACCMT;
             oOrderRow["ACCNT_STATE"] = strACCStatus;
           
             if (strAccountID.Equals(""))
             {
                 oDS.Tables["ACCOUNT_LIST"].Rows.Add(oOrderRow);
             }
             oOrdersDataAdapter.Update(oDS, "ACCOUNT_LIST");
             return "Saved successfully";
         }
         catch (Exception ex)
         {
             return ex.Message.ToString();
         }
     }
    /********End Save  * ********/


     /********Check EasyLoad  NO * ********/
     public DataSet GetDuplecateID(string strEasyNo)
     {
         string strSQL = "";

         strSQL = " SELECT * FROM ACCOUNT_LIST WHERE ACCNT_MSISDN LIKE '%" + strEasyNo + "%'";
         try
         {
             OracleConnection conn = new OracleConnection(strConString);
             DataSet oDS = new DataSet();
             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn));
             oOrdersDataAdapter.Fill(oDS, "ACCOUNT_LIST");
             return oDS;
         }
         catch //(Exception e)
         {
             return null;
         }
     }

    /********End EasyLoad  NO * ********/

     public DataSet GetChannelInfo(string clientId)
     {
         string strSQL = "";

         //strSQL = "SELECT CL.CLINT_NAME,AL.ACCNT_NO CHANNEL_CODE,AL.ACCNT_MSISDN MSISDN "
         //           + "FROM CLIENT_LIST CL, ACCOUNT_LIST AL "
         //           + "WHERE CL.CLINT_ID=AL.CLINT_ID(+) AND CL.CLINT_ID = '" + clientId + "' ORDER BY CL.CLINT_ID DESC";

         strSQL = "SELECT CL.CLINT_NAME,CL.CLINT_MOBILE MSISDN FROM CLIENT_LIST CL "
                    + "WHERE CL.CLINT_ID = '" + clientId + "' ORDER BY CL.CLINT_ID DESC";

         try
         {
             OracleConnection conn = new OracleConnection(strConString);
             DataSet oDS = new DataSet();
             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn));
             oOrdersDataAdapter.Fill(oDS, "CHANNEL_INFO");
             return oDS;
         }
         catch //(Exception e)
         {
             return null;
         }
     }


     /********Toget Account Synonym   NO * ********/
     public DataSet GetAcc_Synonym(string strAccID)
     {
         string strSQL = "";

         strSQL = " SELECT concat(substr(AL.ACCNT_MSISDN,4,14),'1') as ACCNT_MSISDN  FROM ACCOUNT_LIST AL WHERE  AL.ACCNT_ID ='" + strAccID + "'";
         try
         {
             OracleConnection conn = new OracleConnection(strConString);
             DataSet oDS = new DataSet();
             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn));
             oOrdersDataAdapter.Fill(oDS, "ACCOUNT_LIST");
             return oDS;
         }
         catch //(Exception e)
         {
             return null;
         }
     }
     /********Toget Account Synonym   NO * ********/


     /******** Save New Client bank Acount********/

     public string AddClient(string strClBnAcID, string strClAccount, string strDlBank, string strAccSyn, string strAccNo, string strPasswd)
     {
         string strSql;

         if (strClBnAcID.Equals(""))
         {
             strSql = "SELECT  ACCNT_ID,BANK_ID,CLINT_BANK_ACC_NO,CLINT_BANK_ACC_LOGIN,CLINT_BANK_ACC_PASS FROM CLIENT_BANK_ACCOUNT";
         }
         else
         {
             strSql = "SELECT  CLINT_BANK_ACC_ID,ACCNT_ID,BANK_ID,CLINT_BANK_ACC_NO,CLINT_BANK_ACC_LOGIN,CLINT_BANK_ACC_PASS FROM CLIENT_BANK_ACCOUNT WHERE CLINT_BANK_ACC_ID = '" + strClBnAcID + "'";
         }
         try
         {
             DataRow oOrderRow;
             OracleConnection conn = new OracleConnection(strConString);
             DataSet oDS = new DataSet();
             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSql, conn));
             OracleCommandBuilder oOrdersCmdBuilder = new OracleCommandBuilder(oOrdersDataAdapter);
             oOrdersDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
             oOrdersDataAdapter.Fill(oDS, "CLIENT_BANK_ACCOUNT");


             // Insert the Data
             if (strClBnAcID.Equals(""))
             {
                 oOrderRow = oDS.Tables["CLIENT_BANK_ACCOUNT"].NewRow();
             }
             else
             {
                 oOrderRow = oDS.Tables["CLIENT_BANK_ACCOUNT"].Rows.Find(strClBnAcID);
             }

             oOrderRow["ACCNT_ID"] = strClAccount;
             oOrderRow["BANK_ID"] = strDlBank;
             oOrderRow["CLINT_BANK_ACC_NO"] = strAccSyn;
             oOrderRow["CLINT_BANK_ACC_LOGIN"] = strAccNo;
             oOrderRow["CLINT_BANK_ACC_PASS"] = strPasswd;

             if (strClBnAcID.Equals(""))
             {
                 oDS.Tables["CLIENT_BANK_ACCOUNT"].Rows.Add(oOrderRow);
             }
             oOrdersDataAdapter.Update(oDS, "CLIENT_BANK_ACCOUNT");
             return "Saved Successfully";
         }
         catch (Exception ex)
         {
             return ex.Message.ToString();
         }
     }
    /********End  New Client bank Acount********/

     public DataSet queryBankTransaction(string strAccountNo, string strFromDate, string strToDate)
     {
         string strSQL = "", strAddSql = "";

         if (strAccountNo != "")
         {
             strAddSql = " CLINT_BANK_ACC_NO='" + strAccountNo + "' AND BANK_TRAN_DATE "
                + " BETWEEN TO_DATE(\'" + strFromDate + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + strToDate + "\',\'dd/mm/yyyy HH24:MI:SS\')";
         }
         else
         {
             strAddSql = " BANK_TRAN_DATE BETWEEN TO_DATE(\'" + strFromDate + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + strToDate + "\',\'dd/mm/yyyy HH24:MI:SS\')";

         }
         strSQL = " SELECT BANK_TRAN_ID, BANK_TRAN_DESC, BANK_TRAN_DTL_ID, BANK_ACCOUNT_NO, AC_NAME, CLINT_BANK_ACC_NO,"
                + " BANK_INTERNAL_CODE, BANK_TRAN_DATE, DEBIT, CREDIT, IS_SATLEMENT_TRAN FROM ALL_BANK_TRANSACTION WHERE " + strAddSql;
         
         
         try
         {
             OracleConnection conn = new OracleConnection(strConString);
             DataSet oDS = new DataSet();
             OracleDataAdapter oOrdersDataAdapter = new OracleDataAdapter(new OracleCommand(strSQL, conn));
             oOrdersDataAdapter.Fill(oDS, "ALL_BANK_TRANSACTION");
             return oDS;
         }
         catch //(Exception e)
         {
             return null;
         }
     }
    
}
