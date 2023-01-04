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

        public static DataTable SelectViewDataByID(int? ID)
        {
            string s = $@"SELECT CC.ID,
                                   P.NAME POSITION,
                                   CC.PLACE_NAME,
                                   CC.SALARY,
                                   CC.NOAVAILABLE,
                                   CC.NOTE,
                                   CC.USED_USER_ID
                              FROM ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP CC,
                                   ELMS_USER.PROFESSION P
                             WHERE CC.PROFESSION_ID = P.ID 
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
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CustomerWorkDAL", "SelectViewData", exx);
                return null;
            }
        }


        public static DataTable SelectViewDataAll(int? ID)
        {
            string s = $@"SELECT CC.ID,
                                   P.NAME POSITION,
                                   CC.PLACE_NAME,
                                   CC.SALARY,
                                   CC.NOAVAILABLE,
                                   CC.NOTE,
                                   CC.USED_USER_ID
                              FROM ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP CC,
                                   ELMS_USER.PROFESSION P
                             WHERE CC.PROFESSION_ID = P.ID 
                                   AND CC.IS_CHANGE <> {(int)ChangeTypeEnum.Delete} {(ID.HasValue ? $@" AND CC.CUSTOMER_ID = {ID}" : null)}
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
                        command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP(PROFESSION_ID,
                                                                                                PLACE_NAME,
                                                                                                SALARY,
                                                                                                NOAVAILABLE,
                                                                                                NOTE,
                                                                                                CUSTOMER_ID)
                                                    VALUES(:inPROFESSION_ID,
                                                           :inPLACE_NAME,
                                                           :inSALARY,
                                                           :inNOAVAILABLE,
                                                           :inNOTE,
                                                           :inCUSTOMER_ID)";
                        command.Parameters.Add(new OracleParameter("inPROFESSION_ID", customer.PROFESSION_ID));
                        command.Parameters.Add(new OracleParameter("inPLACE_NAME", customer.PLACE_NAME));
                        command.Parameters.Add(new OracleParameter("inSALARY", customer.SALARY));
                        command.Parameters.Add(new OracleParameter("inNOAVAILABLE", customer.NOAVAILABLE));
                        command.Parameters.Add(new OracleParameter("inNOTE", customer.NOTE));
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.CUSTOMER_WORKPLACE_TEMP SET PROFESSION_ID = :inPROFESSION_ID,
                                                                                                PLACE_NAME = :inPLACE_NAME,
                                                                                                SALARY = :inSALARY,
                                                                                                NOAVAILABLE = :inNOAVAILABLE,
                                                                                                NOTE = :inNOTE,
                                                                                                USED_USER_ID = :inUSEDUSERID,
                                                                                                IS_CHANGE = :inISCHANGE
                                                            WHERE CUSTOMER_ID = :inCUSTOMER_ID AND ID = :inID";

                        command.Parameters.Add(new OracleParameter("inPROFESSION_ID", customer.PROFESSION_ID));
                        command.Parameters.Add(new OracleParameter("inPLACE_NAME", customer.PLACE_NAME));
                        command.Parameters.Add(new OracleParameter("inSALARY", customer.SALARY));
                        command.Parameters.Add(new OracleParameter("inNOAVAILABLE", customer.NOAVAILABLE));
                        command.Parameters.Add(new OracleParameter("inNOTE", customer.NOTE));
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
                    GlobalProcedures.LogWrite("İş yeri bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "CustomerWorkDAL", "UpdateCustomerWork", exx);
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
