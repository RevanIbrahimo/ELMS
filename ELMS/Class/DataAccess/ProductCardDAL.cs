using ELMS.Class.Tables;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ELMS.Class.Enum;

namespace ELMS.Class.DataAccess
{
    class ProductCardDAL
    {

        public static DataSet SelectProductCardByID(int? typeID)
        {
            string sql = null;
            if (typeID == null)
                sql = $@"SELECT CC.ID,
                                   P.NAME PRODUCT_NAME,
                                   CC.TOTAL,
                                   CC.PRICE,
                                   CC.PRODUCT_COUNT,
                                   CC.IMEI,
                                   CC.USED_USER_ID
                              FROM ELMS_USER_TEMP.PRODUCT_CARDS_TEMP CC,
                                   ELMS_USER.PRODUCT P
                             WHERE CC.PRODUCT_ID = P.ID
                                   AND CC.IS_CHANGE <> {(int)ChangeTypeEnum.Delete}
                        ORDER BY CC.ID";
            else
                sql = $@"SELECT CC.ID,
                                    P.NAME PRODUCT_NAME,
                                   CC.TOTAL,
                                   CC.PRICE,
                                   CC.PRODUCT_COUNT,
                                   CC.IMEI,
                                   CC.USED_USER_ID
                              FROM ELMS_USER_TEMP.PRODUCT_CARDS_TEMP CC,
                                   ELMS_USER.PRODUCT P
                             WHERE CC.PRODUCT_ID = P.ID
                                   AND CC.IS_CHANGE <> {(int)ChangeTypeEnum.Delete}
                              AND CC.ID = {typeID}";

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
                GlobalProcedures.LogWrite("Sifarişlər açılmadı.", sql, GlobalVariables.V_UserName, "ProductCardDAL", "SelectProductCardByID", exx);
                return null;
            }
        }

        public static DataTable SelectViewData(int? ID)
        {
            string s = $@"SELECT CC.ID,
                                    P.NAME PRODUCT_NAME,
                                   CC.TOTAL,
                                   CC.PRICE,
                                   CC.PRODUCT_COUNT,
                                   CC.NOTE,
                                   CC.USED_USER_ID
                              FROM ELMS_USER_TEMP.PRODUCT_CARDS_TEMP CC,
                                   ELMS_USER.PRODUCT P
                             WHERE CC.PRODUCT_ID = P.ID
                                   AND CC.IS_CHANGE <> {(int)ChangeTypeEnum.Delete} {(ID.HasValue ? $@" AND CC.ID = {ID}" : null)}
                        ORDER BY CC.ID";

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
                GlobalProcedures.LogWrite("Sifarişlər açılmadı.", s, GlobalVariables.V_UserName, "ProductCardDAL", "SelectViewData", exx);
                return null;
            }
        }

        public static DataTable SelectTotal(int? ID)
        {
            string s = $@"SELECT SUM(TOTAL) ORDER_AMOUNT FROM ELMS_USER_TEMP.PRODUCT_CARDS_TEMP";

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
                GlobalProcedures.LogWrite("Məbləğ açılmadı.", s, GlobalVariables.V_UserName, "ProductCardDAL", "SelectTotal", exx);
                return null;
            }
        }

        public static void InsertProductCard(ProductCard product)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.PRODUCT_CARDS_TEMP(PRODUCT_ID,
                                                                                                TOTAL,
                                                                                                PRICE,
                                                                                                PRODUCT_COUNT,
                                                                                                NOTE,
                                                                                                ORDER_TAB_ID)
                                                    VALUES(:inPRODUCT_ID,
                                                           :inTOTAL,
                                                           :inPRICE,
                                                           :inPRODUCT_COUNT,
                                                           :inNOTE,
                                                           :inORDER_TAB_ID)";
                        command.Parameters.Add(new OracleParameter("inPRODUCT_ID", product.PRODUCT_ID));
                        command.Parameters.Add(new OracleParameter("inTOTAL", product.TOTAL));
                        command.Parameters.Add(new OracleParameter("inPRICE", product.PRICE));
                        command.Parameters.Add(new OracleParameter("inPRODUCT_COUNT", product.PRODUCT_COUNT));
                        command.Parameters.Add(new OracleParameter("inNOTE", product.NOTE));
                        command.Parameters.Add(new OracleParameter("inORDER_TAB_ID", product.ORDER_TAB_ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Sifarişlər bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "ProductCardDAL", "InsertProductCard", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void UpdateProductCard(ProductCard product)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.PRODUCT_CARDS_TEMP SET   PRODUCT_ID = :inPRODUCT_ID,
                                                                                                TOTAL = :inTOTAL,
                                                                                                PRICE = :inPRICE,
                                                                                                PRODUCT_COUNT = :inPRODUCT_COUNT,
                                                                                                NOTE = :inNOTE,
                                                                                                USED_USER_ID = :inUSEDUSERID,
                                                                                                IS_CHANGE = :inISCHANGE
                                                            WHERE ORDER_TAB_ID = :inORDER_TAB_ID AND ID = :inID";
                        command.Parameters.Add(new OracleParameter("inPRODUCT_ID", product.PRODUCT_ID));
                        command.Parameters.Add(new OracleParameter("inTOTAL", product.TOTAL));
                        command.Parameters.Add(new OracleParameter("inPRICE", product.PRICE));
                        command.Parameters.Add(new OracleParameter("inPRODUCT_COUNT", product.PRODUCT_COUNT));
                        command.Parameters.Add(new OracleParameter("inNOTE", product.NOTE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inISCHANGE", product.IS_CHANGE));
                        command.Parameters.Add(new OracleParameter("inORDER_TAB_ID", product.ORDER_TAB_ID));
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
                    GlobalProcedures.LogWrite("Sifarişlər bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "ProductCardDAL", "UpdateProductCard", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeleteProductCard(int phoneID, int ownerID)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.PRODUCT_CARDS_TEMP SET IS_CHANGE = {(int)ChangeTypeEnum.Delete}
                                                        WHERE  ORDER_TAB_ID = :inORDER_TAB_ID
                                                          AND ID = :inID";
                        command.Parameters.Add(new OracleParameter("inORDER_TAB_ID", ownerID));
                        command.Parameters.Add(new OracleParameter("inID", phoneID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Sifarişlər temp cədvəldən silinmədi.", commandSql, GlobalVariables.V_UserName, "ProductCardDAL", "DeleteProductCard", exx);
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
