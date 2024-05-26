using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

/// <summary>
/// does any kind of database opperation on Oracle
/// </summary>

public class DataBaseClassOracle
{
    OracleDataAdapter daOracle;
    OracleConnection connect = new OracleConnection(ConfigurationManager.ConnectionStrings["oracleConString"].ConnectionString);
    OracleCommand command = new OracleCommand();
    DataSet dsOracle = new DataSet();
    DataTable dtOracle = new DataTable();

    public void ConnectDataBaseToInsert(string Query)
    {

        command.CommandText = Query;
        command.Connection = connect;
        daOracle = new OracleDataAdapter(command);
        connect.Open();
        command.ExecuteNonQuery();
        connect.Close();

    }
    public void ConnectDataBaseReturnDR(string Query)
    {

        command.CommandText = Query;
        command.Connection = connect;
        daOracle = new OracleDataAdapter(command);
        connect.Open();
        command.ExecuteNonQuery();
        connect.Close();

    }
    public DataSet ConnectDataBaseReturnDS(string Query)
    {
        dsOracle = new DataSet();
      
        command.CommandText = Query;
        command.Connection = connect;
        daOracle = new OracleDataAdapter(command);
        daOracle.Fill(dsOracle);
        connect.Open();
        command.ExecuteNonQuery();
        connect.Close();
        return dsOracle;
    }
    public DataTable ConnectDataBaseReturnDT(string Query)
    {
        dtOracle = new DataTable();
        
        command.CommandText = Query;
        command.Connection = connect;
        daOracle = new OracleDataAdapter(command);
        daOracle.Fill(dtOracle);
        connect.Open();
        command.ExecuteNonQuery();
        connect.Close();
        return dtOracle;
    }
}
