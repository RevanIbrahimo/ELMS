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
    class KindShipDAL
    {
        public static DataSet SelectKindShipByID(int? typeID)
        {
            string sql = null;
            if (typeID == null)
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.KINDSHIP_RATE C
                            ORDER BY C.ORDER_ID";
            else
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.KINDSHIP_RATE C 
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
                GlobalProcedures.LogWrite("Qohumluq dərəcəsi açılmadı.", sql, GlobalVariables.V_UserName, "KindShipDAL", "SelectKindShipByID", exx);
                return null;
            }
        }


        public static void InsertKindShip(KindShip kindShip)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER.KINDSHIP_RATE(NAME,
                                                                                    NOTE,
                                                                                    INSERT_USER)
                                                    VALUES(:inNAME,
                                                           :inNOTE,
                                                           :inINSERT_USER)";
                        command.Parameters.Add(new OracleParameter("inNAME", kindShip.NAME));
                        command.Parameters.Add(new OracleParameter("inNOTE", kindShip.NOTE));
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
                    GlobalProcedures.LogWrite("Qohumluq dərəcəsi bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "KindShipDAL", "InsertKindShip", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }


        public static void UpdateKindShip(KindShip kindShip)
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
                        command.CommandText = $@"UPDATE ELMS_USER.KINDSHIP_RATE SET NAME = :inNAME,
                                                                                  NOTE = :inNOTE,
                                                                                  USED_USER_ID = :inUSEDUSERID,
                                                                                  ORDER_ID = :inORDERID,
                                                                                  UPDATE_USER = :inUPDATEUSER,
                                                                                  UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inNAME", kindShip.NAME));
                        command.Parameters.Add(new OracleParameter("inNOTE", kindShip.NOTE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", kindShip.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inORDERID", kindShip.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", kindShip.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Qohumluq dərəcəsi bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "KindShipDAL", "UpdateKindShip", exx);
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
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.KINDSHIP_RATE C
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
                GlobalProcedures.LogWrite("Qohumluq dərəcəsinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "KindShipDAL", "SelectViewData", exx);
                return null;
            }
        }



        public static void DeleteKindShip(int kindShipID)
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
                        command.CommandText = $@"DELETE FROM ELMS_USER.KINDSHIP_RATE WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", kindShipID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Qohumluq dərəcəsi bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "KindShipDAL", "DeleteKindShip", exx);
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
