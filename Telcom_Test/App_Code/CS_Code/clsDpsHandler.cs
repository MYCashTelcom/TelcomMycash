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


public class clsDpsHandler
{
    private string strCDR_Destination;
    //private int intCDR_Timeinterval;
    private OracleConnection conn;
    private Int64 int64CountRequest;
    private string strConStatus;
    //private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    private string strConString = ConfigurationSettings.AppSettings["dbConnectionString"];

    public clsDpsHandler()
    {
        strCDR_Destination = ""; // System.Windows.Forms.Application.StartupPath; 
        //intCDR_Timeinterval = 15;
        int64CountRequest = 1;
        strConStatus = "";
    }

    public DataTable GetDetails(DateTime Todate, DateTime Fromdate, string accNo)
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

          
            OracleCmd.CommandText = "SELECT AL.REQUEST_ID,ABA.RESPONSE_1,DECODE(ABA.RESPONSE_3,'1','SUCCESS','FAILED')STATUS,REQUEST_PARTY,ABA.CBS_ACC_NO,ABA.TRAN_AMOUNT,ABA.SRV_FEE,CT.CAS_TRAN_DATE,CBI.BRANCH_NAME FROM APSNG101.ALL_SERVICE_REQUEST AL,BDMIT_ERP_101.CAS_ACCOUNT_TRANSACTION CT, APSNG101.ABS_CBS_TRANSACTION ABA,APSNG101.CBS_BRANCH_INFO CBI WHERE AL.ACCESS_CODE='BD' AND AL.REQUEST_ID=CT.REQUEST_ID  AND ABA.CBS_BRANCH=CBI.BRANCH_CODE AND AL.REQUEST_ID=ABA.REQUEST_ID AND CAS_TRAN_PURPOSE_CODE='FRTAMT'AND REQUEST_PARTY='" + accNo + "' AND CT.CAS_TRAN_DATE BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";

       
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


    public DataTable GetDetailsDps(string subQuery,string subQueryTime,string searchValue)
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

            

            OracleCmd.CommandText = "SELECT CD.REQUEST_ID,CT.CAS_ACC_NO,CD.DPS_REF_CODE,CD.CAS_TRAN_DATE,CD.CAS_TRAN_AMT,CD.RESPONSE_MESSAGE FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CT, BDMIT_ERP_101.CAS_DPS_TRANSACTION CD, APSNG101.SERVICE_REQUEST SR WHERE CT.CAS_ACC_ID=CD.CAS_ACC_ID AND CD.REQUEST_ID=SR.REQUEST_ID AND CD.RESPONSE_STATUS='1'AND CD.RESPONSE_MESSAGE='Success' AND  CD.TRAN_TYPE='" + searchValue + "'" + subQuery + "" + subQueryTime + "";

            
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

    public DataTable GetDetailsDps(DateTime Fromdate, DateTime Todate, string strType, string strReqID)
    {
        OracleConnection conn;
        OracleCommand OracleCmd;
        OracleTransaction dbTransaction;


        conn = new OracleConnection(strConString);
        conn.Open();
        dbTransaction = conn.BeginTransaction();
        DataTable dt = new DataTable();
        string subQuery = "";
        string subQueryTime = "";
        string subQueryTime2 = "";
        try
        {
            OracleCmd = new OracleCommand();
            OracleCmd.Connection = conn;//Active Connection
            OracleCmd.Transaction = dbTransaction;
            if (strReqID != null && !String.IsNullOrEmpty(strReqID))
            {
                subQuery = "AND CD.REQUEST_ID='" + strReqID + "' ";

            }
            if (strType == "BDCP")
            {
                subQueryTime = "AND CD.TRANSA_DATE BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";
            }
            else if (strType == "BDLA" || strType == "WD")
            {
                subQueryTime2 = "AND CD.CAS_TRAN_DATE BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";
            }
            else
            {
                subQueryTime = "AND CD.TRANSA_DATE BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";
                subQueryTime2 = "AND CD.CAS_TRAN_DATE BETWEEN TO_DATE('" + Fromdate + "','MM/DD/YYYY HH:MI:SS PM') AND TO_DATE('" + Todate + "','MM/DD/YYYY HH:MI:SS PM')";

            }

            if (strType == "BDCP")
            {
                OracleCmd.CommandText = "SELECT CD.REQUEST_ID,CD.SOURCE_ACCNT_NO CAS_ACC_NO,CD.CARD_NUMBER DPS_REF_CODE,CD.TRANSA_DATE CAS_TRAN_DATE,CD.TRAN_AMOUNT CAS_TRAN_AMT,CD.RESPONSE_MSG_BP RESPONSE_MESSAGE,'BDCP' TRAN_TYPE FROM APSNG101.CARD_TRANSACTION CD, APSNG101.SERVICE_REQUEST SR WHERE CD.REQUEST_ID=SR.REQUEST_ID AND CD.RESPONSE_STATUS_BP='000'AND CD.DEBIT_CREDIT_SUCC_STATUS='Y' " + subQuery + "" + subQueryTime + "";
            }
            else if (strType == "BDLA" || strType == "WD")
            {
                OracleCmd.CommandText = "SELECT CD.REQUEST_ID,CT.CAS_ACC_NO,CD.DPS_REF_CODE,CD.CAS_TRAN_DATE,CD.CAS_TRAN_AMT,CD.RESPONSE_MESSAGE,CD.TRAN_TYPE FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CT, BDMIT_ERP_101.CAS_DPS_TRANSACTION CD, APSNG101.SERVICE_REQUEST SR WHERE CT.CAS_ACC_ID=CD.CAS_ACC_ID AND CD.REQUEST_ID=SR.REQUEST_ID AND CD.RESPONSE_STATUS='1'AND CD.RESPONSE_MESSAGE='Success' AND  CD.TRAN_TYPE='" + strType + "'" + subQuery + "" + subQueryTime2 + "";
            }
            else if ((strType == "ALL"))
            {
                OracleCmd.CommandText = "SELECT CD.REQUEST_ID,CT.CAS_ACC_NO,CD.DPS_REF_CODE,CD.CAS_TRAN_DATE,CD.CAS_TRAN_AMT,CD.RESPONSE_MESSAGE,CD.TRAN_TYPE  FROM BDMIT_ERP_101.CAS_ACCOUNT_LIST CT, BDMIT_ERP_101.CAS_DPS_TRANSACTION CD, APSNG101.SERVICE_REQUEST SR WHERE CT.CAS_ACC_ID=CD.CAS_ACC_ID AND CD.REQUEST_ID=SR.REQUEST_ID AND CD.RESPONSE_STATUS='1'AND CD.RESPONSE_MESSAGE='Success' AND  CD.TRAN_TYPE IN ('BDLA','WD')" + subQuery + "" + subQueryTime2 + " UNION SELECT CD.REQUEST_ID,CD.SOURCE_ACCNT_NO CAS_ACC_NO,CD.CARD_NUMBER DPS_REF_CODE,CD.TRANSA_DATE CAS_TRAN_DATE,CD.TRAN_AMOUNT CAS_TRAN_AMT,CD.RESPONSE_MSG_BP RESPONSE_MESSAGE ,'BDCP' TRAN_TYPE FROM APSNG101.CARD_TRANSACTION CD, APSNG101.SERVICE_REQUEST SR WHERE CD.REQUEST_ID=SR.REQUEST_ID AND CD.RESPONSE_STATUS_BP='000'AND CD.DEBIT_CREDIT_SUCC_STATUS='Y' " + subQuery + "" + subQueryTime + "";

            }

            //            SELECT CD.REQUEST_ID,CD.SOURCE_ACCNT_NO CAS_ACC_NO,CD.CARD_NUMBER DPS_REF_CODE,CD.TRANSA_DATE CAS_TRAN_DATE,
            //CD.TRAN_AMOUNT CAS_TRAN_AMT,CD.RESPONSE_MSG_BP RESPONSE_MESSAGE 
            //FROM APSNG101.CARD_TRANSACTION CD, APSNG101.SERVICE_REQUEST SR 
            //WHERE CD.REQUEST_ID=SR.REQUEST_ID AND CD.RESPONSE_STATUS_BP='000'AND CD.DEBIT_CREDIT_SUCC_STATUS='Y' ;

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

}