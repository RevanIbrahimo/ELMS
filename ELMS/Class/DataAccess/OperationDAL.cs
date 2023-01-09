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
    class OperationDAL
    {




        public static DataTable SelectOperationData(int? ID)
        {
            string s = $@"SELECT OO.ID,
                               OO.OPERATION_TYPE_ID TYPE_ID,
                               OO.NOTE OPERATION_NOTE,
                               OT.NAME OPERATION_NAME,
                               SU.FULL_NAME USER_NAME,
                               OO.USER_ID,
                               OO.INSERT_DATE
                          FROM ELMS_USER.ORDER_OPERATION OO,
                               ELMS_USER.OPERATION_TYPE OT,
                               ELMS_USER.SYSTEM_USER SU
                          WHERE OO.USER_ID = SU.ID    
                               AND OO.OPERATION_TYPE_ID = OT.ID {(ID.HasValue ? $@" AND OO.ID = {ID}" : null)}
                        ORDER BY OO.ID";

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
                GlobalProcedures.LogWrite("Əməliyyatların məlumatları açılmadı.", s, GlobalVariables.V_UserName, "OperationDAL", "SelectOperationData", exx);
                return null;
            }
        }


        public static void InsertOrderOperation(OrderOperation order)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER.ORDER_OPERATION(ORDER_ID,                               
                                                            OPERATION_TYPE_ID)
                                                    VALUES(:inORDER_ID,
                                                           :inOPERATION_TYPE_ID)";
                        command.Parameters.Add(new OracleParameter("inORDER_ID", order.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inOPERATION_TYPE_ID", order.OPERATION_ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Müraciət məlumatları təsdiq edilmədi", commandSql, GlobalVariables.V_UserName, "OperationDAL", "InsertOrderOperation", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static Int32 InsertOrderOperation(OracleTransaction tran, OrderOperation order)
        {
            Int32 id = 0;
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER.ORDER_OPERATION(ORDER_ID,                               
                                                                           OPERATION_TYPE_ID,
                                                                           INSERT_USER)
                                                    VALUES(:inORDER_ID,
                                                           :inOPERATION_TYPE_ID,
                                                           :inINSERTUSER) RETURNING ID INTO :outID";
            command.Parameters.Add(new OracleParameter("inORDER_ID", order.ORDER_ID));
            command.Parameters.Add(new OracleParameter("inOPERATION_TYPE_ID", order.OPERATION_ID));
            command.Parameters.Add(new OracleParameter("inINSERTUSER", order.INSERT_USER));
            command.Parameters.Add(new OracleParameter("outID", OracleDbType.Int32, ParameterDirection.Output));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            id = Convert.ToInt32(command.Parameters["outID"].Value.ToString());

            command.Dispose();

            return id;
        }

        public static void UpdateOrderOperation(OrderOperation order)
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
                        command.CommandText = $@"UPDATE ELMS_USER.ORDER_OPERATION SET ORDER_ID = :inORDER_ID,
                                                                                           OPERATION_TYPE_ID = :inOPERATION_TYPE_ID,
                                                                                           NOTE = :inNOTE
                                                            WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inORDER_ID", order.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inOPERATION_TYPE_ID", order.OPERATION_ID));
                        command.Parameters.Add(new OracleParameter("inNOTE", order.NOTE));
                        command.Parameters.Add(new OracleParameter("inID", order.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Müraciətin vəziyyəti dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "OperationDAL", "UpdateOrderOperation", exx);
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
