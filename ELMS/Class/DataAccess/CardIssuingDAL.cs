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
    class CardIssuingDAL
    {
        public static DataSet SelectCardIssuingByID(int? typeID)
        {
            string sql = null;
            if (typeID == null)
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.CARD_ISSUING C
                            ORDER BY C.ORDER_ID";
            else
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.CARD_ISSUING C 
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
                GlobalProcedures.LogWrite("Orqanlar açılmadı.", sql, GlobalVariables.V_UserName, "CardIssuingDAL", "SelectCardIssuingByID", exx);
                return null;
            }
        }


        public static void InsertCardIssuing(CardIssuing cardIssuing)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER.CARD_ISSUING(NAME,
                                                                                    NOTE,
                                                                                    INSERT_USER)
                                                    VALUES(:inNAME,
                                                           :inNOTE,
                                                           :inINSERT_USER)";
                        command.Parameters.Add(new OracleParameter("inNAME", cardIssuing.NAME));
                        command.Parameters.Add(new OracleParameter("inNOTE", cardIssuing.NOTE));
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
                    GlobalProcedures.LogWrite("Sənəd bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "CardIssuingDAL", "InsertCardIssuing", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }


        public static void UpdateCardIssuing(CardIssuing cardIssuing)
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
                        command.CommandText = $@"UPDATE ELMS_USER.CARD_ISSUING SET NAME = :inNAME,
                                                                                  NOTE = :inNOTE,
                                                                                  USED_USER_ID = :inUSEDUSERID,
                                                                                  ORDER_ID = :inORDERID,
                                                                                  UPDATE_USER = :inUPDATEUSER,
                                                                                  UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inNAME", cardIssuing.NAME));
                        command.Parameters.Add(new OracleParameter("inNOTE", cardIssuing.NOTE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", cardIssuing.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inORDERID", cardIssuing.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", cardIssuing.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Sənəd bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "CardIssuingDAL", "UpdateCardIssuing", exx);
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
                            FROM ELMS_USER.CARD_ISSUING C
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
                GlobalProcedures.LogWrite("Orqan məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CardIssuingDAL", "SelectViewData", exx);
                return null;
            }
        }



        public static void DeleteCardIssuing(int cardIssuingID)
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
                        command.CommandText = $@"DELETE FROM ELMS_USER.CARD_ISSUING WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", cardIssuingID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Sənəd bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "CardIssuingDAL", "DeleteCardIssuing", exx);
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
