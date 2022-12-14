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
    public class PhonePrefixDAL
    {
        public static DataSet SelectPhoneProfixByID(int? ID)
        {
            string sql = null;
            if (ID == null)
                sql = $@"SELECT ID,PHONE_DESCRIPTION_ID,PREFIX,NOTE,USED_USER_ID,IS_CHANGE FROM ELMS_USER_TEMP.PHONE_PREFIX_TEMP ORDER BY PREFIX";
            else
                sql = $@"SELECT ID,PHONE_DESCRIPTION_ID,PREFIX,NOTE,USED_USER_ID,IS_CHANGE FROM ELMS_USER_TEMP.PHONE_PREFIX_TEMP WHERE ID = {ID}";

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
                GlobalProcedures.LogWrite("Telefon prefiksləri açılmadı.", sql, GlobalVariables.V_UserName, "PhonePrefixDAL", "SelectPhoneProfixByID", exx);
                return null;
            }
        }

        public static DataSet SelectTempPhoneProfixByDescriptionID(int descriptionID)
        {
            string sql = $@"SELECT ID,PHONE_DESCRIPTION_ID,PREFIX,NOTE,USED_USER_ID,IS_CHANGE FROM ELMS_USER_TEMP.PHONE_PREFIX_TEMP WHERE IS_CHANGE IN ({(int)ChangeTypeEnum.Default}, {(int)ChangeTypeEnum.Change}) AND PHONE_DESCRIPTION_ID = {descriptionID}";

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
                GlobalProcedures.LogWrite("Telefon təsvirinin prefiksləri açılmadı.", sql, GlobalVariables.V_UserName, "PhonePrefixDAL", "SelectPhoneProfixByDescriptionID", exx);
                return null;
            }
        }

        public static DataSet SelectPhoneProfixByDescriptionID(int descriptionID)
        {
            string sql = $@"SELECT ID,PHONE_DESCRIPTION_ID,PREFIX,NOTE FROM ELMS_USER.PHONE_PREFIX WHERE PHONE_DESCRIPTION_ID = {descriptionID}";

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
                GlobalProcedures.LogWrite("Telefon təsvirinin prefiksləri açılmadı.", sql, GlobalVariables.V_UserName, "PhonePrefixDAL", "SelectPhoneProfixByDescriptionID", exx);
                return null;
            }
        }


        public static void InsertPhonePrefix(PhonePrefix prefix)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.PHONE_PREFIX_TEMP(PHONE_DESCRIPTION_ID,
                                                                                                 PREFIX,
                                                                                                 NOTE,
                                                                                                 IS_CHANGE,
                                                                                                 USED_USER_ID)
                                                                            VALUES(:inDESCRIPTIONID,
                                                                                   :inPREFIX,
                                                                                   :inNOTE,
                                                                                   :inCHANGE,
                                                                                   :inUSEDUSERID)";
                        command.Parameters.Add(new OracleParameter("inDESCRIPTIONID", prefix.PHONE_DESCRIPTION_ID));
                        command.Parameters.Add(new OracleParameter("inPREFIX", prefix.PREFIX));
                        command.Parameters.Add(new OracleParameter("inNOTE", prefix.NOTE));
                        command.Parameters.Add(new OracleParameter("inCHANGE", (int)ChangeTypeEnum.Change));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Telefon prefiksi bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "PhonePrefixDAL", "InsertPhonePrefix", exx);                    
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void UpdatePhonePrefix(PhonePrefix prefix)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.PHONE_PREFIX_TEMP SET PHONE_DESCRIPTION_ID = :inDESCRIPTIONID,
                                                                                               PREFIX = :inPREFIX,                                                                                              
                                                                                               NOTE = :inNOTE,
                                                                                               IS_CHANGE = :inCHANGE,
                                                                                               USED_USER_ID = :inUSEDUSERID
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inDESCRIPTIONID", prefix.PHONE_DESCRIPTION_ID));
                        command.Parameters.Add(new OracleParameter("inPREFIX", prefix.PREFIX));                        
                        command.Parameters.Add(new OracleParameter("inNOTE", prefix.NOTE));
                        command.Parameters.Add(new OracleParameter("inCHANGE", (int)ChangeTypeEnum.Change));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", prefix.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inID", prefix.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Telefon prefiksi bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "PhonePrefixDAL", "UpdatePhonePrefix", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeletePhonePrefix(int prefixID)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.PHONE_PREFIX_TEMP SET IS_CHANGE = :inCHANGE WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inCHANGE", (int)ChangeTypeEnum.Delete));
                        command.Parameters.Add(new OracleParameter("inID", prefixID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Telefon prefiksi bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "PhonePrefixDAL", "DeletePhonePrefix", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeleteTempPhonePrefix(OracleTransaction tran)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"DELETE FROM ELMS_USER_TEMP.PHONE_PREFIX_TEMP WHERE USED_USER_ID = :inUSEDUSERID";
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
