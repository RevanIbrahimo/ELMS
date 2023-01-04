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
    class PhoneDAL
    {
        public static DataSet SelectPhoneByID(int? ID)
        {
            string sql = null;
            if (ID == null)
                sql = $@"SELECT ID,
                               OWNER_TYPE,
                               OWNER_ID,
                               PHONE_DESCRIPTION_ID,
                               PHONE_PREFIX_ID,
                               PHONE_NUMBER,
                               NOTE,
                               IS_SEND_SMS,
                               ORDER_ID,
                               IS_CHANGE,
                               USED_USER_ID
                          FROM ELMS_USER_TEMP.PHONE_TEMP ORDER BY ORDER_ID";
            else
                sql = $@"SELECT ID,
                               OWNER_TYPE,
                               OWNER_ID,
                               PHONE_DESCRIPTION_ID,
                               PHONE_PREFIX_ID,
                               PHONE_NUMBER,
                               NOTE,
                               IS_SEND_SMS,
                               ORDER_ID,
                               IS_CHANGE,
                               USED_USER_ID
                      FROM ELMS_USER_TEMP.PHONE_TEMP
                      WHERE ID = {ID}";

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
                GlobalProcedures.LogWrite("Telefon açılmadı.", sql, GlobalVariables.V_UserName, "PhoneDAL", "SelectPhoneByID", exx);
                return null;
            }
        }

        public static DataTable SelectPhoneByOwnerID(int ownerID, PhoneOwnerEnum ownerType, int? id = null)
        {
            string sql = $@"SELECT P.ID,
                                   P.OWNER_TYPE,
                                   P.OWNER_ID,
                                   P.PHONE_DESCRIPTION_ID,
                                   PD.NAME DESCRIPTION_NAME,
                                   P.PHONE_PREFIX_ID,
                                   P.PHONE_NUMBER,
                                   P.NOTE,
                                   P.IS_SEND_SMS,
                                   P.ORDER_ID
                              FROM ELMS_USER_TEMP.PHONE_TEMP P, ELMS_USER.PHONE_DESCRIPTIONS PD
                             WHERE P.PHONE_DESCRIPTION_ID = PD.ID 
                               AND P.IS_CHANGE != {(int)ChangeTypeEnum.Delete}    
                               AND P.OWNER_ID = {ownerID} 
                               AND P.OWNER_TYPE = {(int)ownerType}{(id.HasValue ? $@" AND P.ID = {id}" : null)}";

            try
            {
                using (OracleDataAdapter da = new OracleDataAdapter(sql, GlobalFunctions.GetConnectionString()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Telefon açılmadı.", sql, GlobalVariables.V_UserName, "PhoneDAL", "SelectPhoneByOwnerID", exx);
                return null;
            }
        }

        public static void InsertPhone(Phone phone)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.PHONE_TEMP(OWNER_TYPE,
                                                                                           OWNER_ID,
                                                                                           PHONE_DESCRIPTION_ID,
                                                                                           PHONE_PREFIX_ID,
                                                                                           PHONE_NUMBER,
                                                                                           NOTE,
                                                                                           IS_SEND_SMS,
                                                                                           IS_CHANGE,
                                                                                           USED_USER_ID)
                                                     VALUES(:inOWNERTYPE,
                                                            :inOWNERID,
                                                            :inPHONEDESCRIPTIONID,
                                                            :inPHONEPREFIXID,
                                                            :inPHONENUMBER,
                                                            :inNOTE,
                                                            :inISSENDSMS,
                                                            :inISCHANGE,
                                                            :inUSEDUSERID)";
                        command.Parameters.Add(new OracleParameter("inOWNERTYPE", phone.OWNER_TYPE));
                        command.Parameters.Add(new OracleParameter("inOWNERID", phone.OWNER_ID));
                        command.Parameters.Add(new OracleParameter("inPHONEDESCRIPTIONID", phone.PHONE_DESCRIPTION_ID));
                        command.Parameters.Add(new OracleParameter("inPHONEPREFIXID", phone.PHONE_PREFIX_ID));
                        command.Parameters.Add(new OracleParameter("inPHONENUMBER", phone.PHONE_NUMBER));
                        command.Parameters.Add(new OracleParameter("inNOTE", phone.NOTE));
                        command.Parameters.Add(new OracleParameter("inISSENDSMS", phone.IS_SEND_SMS));
                        command.Parameters.Add(new OracleParameter("inISCHANGE", phone.IS_CHANGE));
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
                    GlobalProcedures.LogWrite("Telefon temp cədvələ daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "PhoneDAL", "InsertPhone", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void UpdatePhone(Phone phone)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.PHONE_TEMP SET OWNER_TYPE = :inOWNERTYPE,
                                                                                           OWNER_ID = :inOWNERID,
                                                                                           PHONE_DESCRIPTION_ID = :inPHONEDESCRIPTIONID,
                                                                                           PHONE_PREFIX_ID = :inPHONEPREFIXID,
                                                                                           PHONE_NUMBER = :inPHONENUMBER,
                                                                                           NOTE = :inNOTE,
                                                                                           IS_SEND_SMS = :inISSENDSMS,
                                                                                           IS_CHANGE = :inISCHANGE,
                                                                                           USED_USER_ID = :inUSEDUSERID
                                                            WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inOWNERTYPE", phone.OWNER_TYPE));
                        command.Parameters.Add(new OracleParameter("inOWNERID", phone.OWNER_ID));
                        command.Parameters.Add(new OracleParameter("inPHONEDESCRIPTIONID", phone.PHONE_DESCRIPTION_ID));
                        command.Parameters.Add(new OracleParameter("inPHONEPREFIXID", phone.PHONE_PREFIX_ID));
                        command.Parameters.Add(new OracleParameter("inPHONENUMBER", phone.PHONE_NUMBER));
                        command.Parameters.Add(new OracleParameter("inNOTE", phone.NOTE));
                        command.Parameters.Add(new OracleParameter("inISSENDSMS", phone.IS_SEND_SMS));
                        command.Parameters.Add(new OracleParameter("inISCHANGE", phone.IS_CHANGE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", phone.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Telefon temp cədvəldə dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "PhoneDAL", "UpdatePhone", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeletePhone(int phoneID, int? ownerID, PhoneOwnerEnum phoneOwner)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.PHONE_TEMP SET IS_CHANGE = {(int)ChangeTypeEnum.Delete}
                                                        WHERE OWNER_TYPE = :inOWNERTYPE
                                                          AND OWNER_ID = :inOWNERID
                                                          AND ID = :inID";
                        command.Parameters.Add(new OracleParameter("inOWNERTYPE", (int)phoneOwner));
                        command.Parameters.Add(new OracleParameter("inOWNERID", ownerID));
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
                    GlobalProcedures.LogWrite("Telefon temp cədvəldən silinmədi.", commandSql, GlobalVariables.V_UserName, "PhoneDAL", "DeletePhone", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeletePhoneTemp(OracleTransaction tran, PhoneOwnerEnum phoneOwner)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"DELETE FROM ELMS_USER_TEMP.PHONE_TEMP 
                                                        WHERE OWNER_TYPE = :inOWNERTYPE
                                                          AND USED_USER_ID = :inUSEDUSERID";
            command.Parameters.Add(new OracleParameter("inOWNERTYPE", (int)phoneOwner));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

       
        public static Int32 InsertRelativePhone(OracleTransaction tran, Phone phone)
        {
            Int32 id = 0;
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.PHONE_TEMP(OWNER_TYPE,
                                                                                           OWNER_ID,
                                                                                           PHONE_DESCRIPTION_ID,
                                                                                           PHONE_PREFIX_ID,
                                                                                           PHONE_NUMBER,
                                                                                           IS_SEND_SMS,
                                                                                           IS_CHANGE,
                                                                                           USED_USER_ID)
                                                     VALUES(:inOWNERTYPE,
                                                            :inOWNERID,
                                                            :inPHONEDESCRIPTIONID,
                                                            :inPHONEPREFIXID,
                                                            :inPHONENUMBER,
                                                            :inISSENDSMS,
                                                            :inISCHANGE,
                                                            :inUSEDUSERID) RETURNING ID INTO :outID";
            command.Parameters.Add(new OracleParameter("inOWNERTYPE", phone.OWNER_TYPE));
            command.Parameters.Add(new OracleParameter("inOWNERID", phone.OWNER_ID));
            command.Parameters.Add(new OracleParameter("inPHONEDESCRIPTIONID", phone.PHONE_DESCRIPTION_ID));
            command.Parameters.Add(new OracleParameter("inPHONEPREFIXID", phone.PHONE_PREFIX_ID));
            command.Parameters.Add(new OracleParameter("inPHONENUMBER", phone.PHONE_NUMBER));
            command.Parameters.Add(new OracleParameter("inISSENDSMS", phone.IS_SEND_SMS));
            command.Parameters.Add(new OracleParameter("inISCHANGE", phone.IS_CHANGE));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("outID", OracleDbType.Int32, ParameterDirection.Output));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            id = Convert.ToInt32(command.Parameters["outID"].Value.ToString());

            command.Dispose();

            return id;
        }


        public static void UpdateRelativePhone(OracleTransaction tran, Phone phone)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"UPDATE ELMS_USER_TEMP.PHONE_TEMP SET OWNER_TYPE = :inOWNERTYPE,
                                                                                           OWNER_ID = :inOWNERID,
                                                                                           PHONE_DESCRIPTION_ID = :inPHONEDESCRIPTIONID,
                                                                                           PHONE_PREFIX_ID = :inPHONEPREFIXID,
                                                                                           PHONE_NUMBER = :inPHONENUMBER,
                                                                                           IS_SEND_SMS = :inISSENDSMS,
                                                                                           IS_CHANGE = :inISCHANGE,
                                                                                           USED_USER_ID = :inUSEDUSERID
                                                            WHERE ID = :inID";
            command.Parameters.Add(new OracleParameter("inOWNERTYPE", phone.OWNER_TYPE));
            command.Parameters.Add(new OracleParameter("inOWNERID", phone.OWNER_ID));
            command.Parameters.Add(new OracleParameter("inPHONEDESCRIPTIONID", phone.PHONE_DESCRIPTION_ID));
            command.Parameters.Add(new OracleParameter("inPHONEPREFIXID", phone.PHONE_PREFIX_ID));
            command.Parameters.Add(new OracleParameter("inPHONENUMBER", phone.PHONE_NUMBER));
            command.Parameters.Add(new OracleParameter("inISSENDSMS", phone.IS_SEND_SMS));
            command.Parameters.Add(new OracleParameter("inISCHANGE", phone.IS_CHANGE));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inID", phone.ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
