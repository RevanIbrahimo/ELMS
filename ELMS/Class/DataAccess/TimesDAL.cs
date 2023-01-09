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
    class TimesDAL
    {

        public static DataSet SelectTimesByID(int? typeID)
        {
            string sql = null;
            if (!typeID.HasValue)
                sql = $@"SELECT C.ID,
                                 C.PERIOD,
                                 C.PERCENT,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.TIMES C
                            ORDER BY C.ID";
            else
                sql = $@"SELECT C.ID,
                                 C.PERIOD,
                                 C.PERCENT,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.TIMES C 
                           WHERE C.ID = {typeID}";

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
                GlobalProcedures.LogWrite("Müddətlər açılmadı.", sql, GlobalVariables.V_UserName, "TimesDAL", "SelectTimesByID", exx);
                return null;
            }
        }


        public static DataSet SelectTimes(int? timesID)
        {
            string sql = null;
            if (!timesID.HasValue)
                sql = $@"SELECT C.ID,
                                 C.PERIOD,
                                 C.PERCENT
                            FROM ELMS_USER.TIMES C
                            ORDER BY C.ORDER_ID";
            else
                sql = $@"SELECT C.ID,
                                 C.PERIOD,
                                 C.PERCENT
                            FROM ELMS_USER.TIMES C 
                           WHERE C.ID = {timesID}";

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
                GlobalProcedures.LogWrite("Mənbələr açılmadı.", sql, GlobalVariables.V_UserName, "TimesDAL", "SelectTimesByID", exx);
                return null;
            }
        }


        public static void InsertTimes(Times times)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER.TIMES(PERIOD,
                                                                             PERCENT,
                                                                             NOTE,
                                                                             INSERT_USER)
                                                    VALUES(:inPERIOD,
                                                           :inPERCENT,
                                                           :inNOTE,
                                                           :inINSERT_USER)";
                        command.Parameters.Add(new OracleParameter("inPERIOD", times.PERIOD));
                        command.Parameters.Add(new OracleParameter("inPERCENT", times.PERCENT));
                        command.Parameters.Add(new OracleParameter("inNOTE", times.NOTE));
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
                    GlobalProcedures.LogWrite("Mənbə bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "TimesDAL", "InsertTimes", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }


        public static void UpdateTimes(Times times)
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
                        command.CommandText = $@"UPDATE ELMS_USER.TIMES SET PERIOD = :inPERIOD,
                                                                                  PERCENT = :inPERCENT,
                                                                                  NOTE = :inNOTE,
                                                                                  USED_USER_ID = :inUSEDUSERID,
                                                                                  ORDER_ID = :inORDERID,
                                                                                  UPDATE_USER = :inUPDATEUSER,
                                                                                  UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inPERIOD", times.PERIOD));
                        command.Parameters.Add(new OracleParameter("inPERCENT", times.PERCENT));
                        command.Parameters.Add(new OracleParameter("inNOTE", times.NOTE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", times.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inORDERID", times.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", times.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Mənbə bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "TimesDAL", "UpdateTimes", exx);
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
                                 C.PERIOD,
                                 C.PERCENT,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.TIMES C
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
                GlobalProcedures.LogWrite("Mənbə məlumatları açılmadı.", s, GlobalVariables.V_UserName, "TimesDAL", "SelectViewData", exx);
                return null;
            }
        }



        public static void DeleteTimes(int timesID)
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
                        command.CommandText = $@"DELETE FROM ELMS_USER.TIMES WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", timesID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Mənbə bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "TimesDAL", "DeleteTimes", exx);
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
