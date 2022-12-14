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
    class ProductDAL
    {
        public static DataSet SelectProductByID(int? issuingID)
        {
            string sql = null;
            if (issuingID == null)
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.PRODUCT C
                            ORDER BY C.ORDER_ID";
            else
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.PRODUCT C 
                           WHERE C.ID = {issuingID}";

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
                GlobalProcedures.LogWrite("Məhsul açılmadı.", sql, GlobalVariables.V_UserName, "ProductDAL", "SelectProductByID", exx);
                return null;
            }
        }


        public static void InsertProduct(Product product)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER.PRODUCT(NAME,
                                                                                    NOTE,
                                                                                    INSERT_USER)
                                                    VALUES(:inNAME,
                                                           :inNOTE,
                                                           :inINSERT_USER)";
                        command.Parameters.Add(new OracleParameter("inNAME", product.NAME));
                        command.Parameters.Add(new OracleParameter("inNOTE", product.NOTE));
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
                    GlobalProcedures.LogWrite("Məhsul bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "ProductDAL", "InsertProduct", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }


        public static void UpdateProduct(Product product)
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
                        command.CommandText = $@"UPDATE ELMS_USER.PRODUCT SET NAME = :inNAME,
                                                                                  NOTE = :inNOTE,
                                                                                  USED_USER_ID = :inUSEDUSERID,
                                                                                  ORDER_ID = :inORDERID,
                                                                                  UPDATE_USER = :inUPDATEUSER,
                                                                                  UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inNAME", product.NAME));
                        command.Parameters.Add(new OracleParameter("inNOTE", product.NOTE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", product.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inORDERID", product.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", product.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Məhsul bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "ProductDAL", "UpdateProduct", exx);
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
                            FROM ELMS_USER.PRODUCT C
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
                GlobalProcedures.LogWrite("Məhsulun məlumatları açılmadı.", s, GlobalVariables.V_UserName, "ProductDAL", "SelectViewData", exx);
                return null;
            }
        }



        public static void DeleteProduct(int productID)
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
                        command.CommandText = $@"DELETE FROM ELMS_USER.PRODUCT WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", productID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Məhsul bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "ProductDAL", "DeleteProduct", exx);
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
