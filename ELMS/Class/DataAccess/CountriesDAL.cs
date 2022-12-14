using ELMS.Class.Tables;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.DataAccess
{
    class CountriesDAL
    {
        public static DataSet SelectCountriesByID(int? countryID)
        {
            string sql = null;
            if (countryID == null)
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.ALPHA3CODE,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.COUNTRY C
                            ORDER BY C.ORDER_ID";
            else
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.ALPHA3CODE,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.COUNTRY C 
                           WHERE C.ID = {countryID}";

            try
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(sql, GlobalFunctions.GetConnectionString()))
                {
                    DataSet dsAdapter = new DataSet();
                    adapter.Fill(dsAdapter);
                    return dsAdapter;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Ölkə açılmadı.", sql, GlobalVariables.V_UserName, "CountriesDAL", "SelectCountriesByID", exx);
                return null;
            }
        }

        public static void InsertCountries(Countries countries)
        {
            string commandSql = null;
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        transaction = connection.BeginTransaction();
                        command.Transaction = transaction;
                        command.CommandText = $@"INSERT INTO ELMS_USER.COUNTRY(NAME,
                                                                               NOTE,
                                                                               ALPHA3CODE,
                                                                               INSERT_USER)
                                                    VALUES(:inNAME,
                                                           :inNOTE,
                                                           :inALPHA3CODE,
                                                           :inINSERT_USER)";
                        command.Parameters.Add(new OracleParameter("inNAME", countries.NAME));
                        command.Parameters.Add(new OracleParameter("inNOTE", countries.NOTE));
                        command.Parameters.Add(new OracleParameter("inALPHA3CODE", countries.ALPHA3CODE));
                        command.Parameters.Add(new OracleParameter("inINSERT_USER", GlobalVariables.V_UserID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Ölkə bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "CountriesDAL", "InsertCountries", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }


        public static void UpdateCountries(Countries countries)
        {
            string commandSql = null;
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        transaction = connection.BeginTransaction();
                        command.Transaction = transaction;
                        command.CommandText = $@"UPDATE ELMS_USER.COUNTRY SET NAME = :inNAME,
                                                                                  NOTE = :inNOTE,
                                                                                  ALPHA3CODE = :inALPHA3CODE,
                                                                                  USED_USER_ID = :inUSEDUSERID,
                                                                                  ORDER_ID = :inORDERID,
                                                                                  UPDATE_USER = :inUPDATEUSER,
                                                                                  UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inNAME", countries.NAME));
                        command.Parameters.Add(new OracleParameter("inNOTE", countries.NOTE));
                        command.Parameters.Add(new OracleParameter("inALPHA3CODE", countries.ALPHA3CODE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", countries.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inORDERID", countries.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", countries.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Ölkə bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "CountriesDAL", "UpdateCountries", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static DataTable SelectViewData(int? ID)
        {
            string s = $@"SELECT C.ID,
                                 C.NAME,
                                 C.ALPHA3CODE,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.COUNTRY C
                           WHERE C.ID = {ID}";

            try
            {
                using (OracleDataAdapter da = new OracleDataAdapter(s, GlobalFunctions.GetConnectionString()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Ölkə məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CountriesDAL", "SelectViewData", exx);
                return null;
            }
        }



        public static void DeleteCountries(int professionID)
        {
            string commandSql = null;
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        transaction = connection.BeginTransaction();
                        command.Transaction = transaction;
                        command.CommandText = $@"DELETE FROM ELMS_USER.COUNTRY WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", professionID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Ölkə bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "CountriesDAL", "DeleteCountries", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }
    }
}
