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
    class CustomerWorkDAL
    {

        public static DataTable SelectViewData(int? ID)
        {
            string s = $@"SELECT CC.ID,
                                   CC.PLACE_NAME,
                                   CC.START_DATE,
                                   CC.END_DATE,
                                   CC.POSITION,
                                   CC.USED_USER_ID
                              FROM ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP CC
                             WHERE CC.IS_CHANGE <> {(int)ChangeTypeEnum.Delete} {(ID.HasValue ? $@" AND CC.CUSTOMER_ID = {ID}" : null)}
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
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CustomerWorkDAL", "SelectViewData", exx);
                return null;
            }
        }


        public static void InsertCustomerWork(CustomerWork customer)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP(PLACE_NAME,
                                                                                                POSITION,
                                                                                                START_DATE,
                                                                                                END_DATE,
                                                                                                CUSTOMER_ID)
                                                    VALUES(:inPLACE_NAME,
                                                           :inPOSITION,
                                                           :inSTART_DATE,
                                                           :inEND_DATE,
                                                           :inCUSTOMER_ID)";
                        command.Parameters.Add(new OracleParameter("inPLACE_NAME", customer.PLACE_NAME));
                        command.Parameters.Add(new OracleParameter("inPOSITION", customer.POSITION));
                        command.Parameters.Add(new OracleParameter("inSTART_DATE", customer.START_DATE));
                        command.Parameters.Add(new OracleParameter("inEND_DATE", customer.END_DATE));
                        command.Parameters.Add(new OracleParameter("inCUSTOMER_ID", customer.CUSTOMER_ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("İş yeri bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "CustomerWorkDAL", "InsertCustomerWork", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void UpdateCustomerWork(CustomerWork customer)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP SET PLACE_NAME = :inPLACE_NAME,
                                                                                                POSITION = :inPOSITION,
                                                                                                START_DATE = :inSTART_DATE,
                                                                                                END_DATE = :inEND_DATE,
                                                                                                USED_USER_ID = :inUSEDUSERID,
                                                                                                IS_CHANGE = :inISCHANGE
                                                            WHERE CUSTOMER_ID = :inCUSTOMER_ID AND ID = :inID";

                        command.Parameters.Add(new OracleParameter("inPLACE_NAME", customer.PLACE_NAME));
                        command.Parameters.Add(new OracleParameter("inPOSITION", customer.POSITION));
                        command.Parameters.Add(new OracleParameter("inSTART_DATE", customer.START_DATE));
                        command.Parameters.Add(new OracleParameter("inEND_DATE", customer.END_DATE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inISCHANGE", customer.IS_CHANGE));
                        command.Parameters.Add(new OracleParameter("inCUSTOMER_ID", customer.CUSTOMER_ID));
                        command.Parameters.Add(new OracleParameter("inID", customer.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Sənəd bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "CustomerWorkDAL", "UpdateCustomerWork", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeleteCustomerWork(int phoneID, int ownerID)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP SET IS_CHANGE = {(int)ChangeTypeEnum.Delete}
                                                        WHERE  CUSTOMER_ID = :inCUSTOMERID
                                                          AND ID = :inID";
                        command.Parameters.Add(new OracleParameter("inCUSTOMERID", ownerID));
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
                    GlobalProcedures.LogWrite("İş yeri temp cədvəldən silinmədi.", commandSql, GlobalVariables.V_UserName, "CustomerWorkDAL", "DeleteCustomerWork", exx);
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
