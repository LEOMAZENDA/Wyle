using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AcessoDados.Data
{
    public class GlobalDapper
    {
        public static string ConnectionString { get; set; }
        public static void ExecuteWithoutReturn(string sProcName, DynamicParameters param = null)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
                sqlCon.Execute(sProcName, param, commandType: CommandType.StoredProcedure);
        }

        //DatterORM.ExecuteReturnScalar<int>(_,_);
        public static T ExecuteReturnScalar<T>(string sProcName, DynamicParameters param = null)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);           
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar(sProcName, param, commandType:
                    CommandType.StoredProcedure),typeof(T));          
        }

        //DatterORM.ReturnList<aModel> <= IEnumerable<aModel>
        public static IEnumerable<T> ReturnList<T>(string sProcName, DynamicParameters param = null)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
                return sqlCon.Query<T>(sProcName, param, commandType: CommandType.StoredProcedure);
        }
    }
}
