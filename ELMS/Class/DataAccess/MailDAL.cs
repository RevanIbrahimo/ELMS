﻿using ELMS.Class.Tables;
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
    class MailDAL
    {
        public static DataSet SelectMailByID(int? ID)
        {
            string sql = null;
            if (ID == null)
                sql = $@"SELECT ID,
                               OWNER_TYPE,
                               OWNER_ID,
                               MAIL,
                               NOTE,
                               IS_SEND_SMS,
                               ORDER_ID,
                               IS_CHANGE,
                               USED_USER_ID
                          FROM ELMS_USER_TEMP.MAILS_TEMP ORDER BY ORDER_ID";
            else
                sql = $@"SELECT ID,
                               OWNER_TYPE,
                               OWNER_ID,
                               MAIL,
                               NOTE,
                               IS_SEND_SMS,
                               ORDER_ID,
                               IS_CHANGE,
                               USED_USER_ID
                      FROM ELMS_USER_TEMP.MAILS_TEMP
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
                GlobalProcedures.LogWrite("Telefon açılmadı...", sql, GlobalVariables.V_UserName, "MailDAL", "SelectMailByID", exx);
                return null;
            }
        }

        public static DataTable SelectMailByOwnerID(int? ownerID, MailOwnerEnum ownerType, int? id = null)
        {
            string sql = $@"SELECT M.ID,
                                   M.OWNER_TYPE,
                                   M.OWNER_ID,
                                   M.MAIL,
                                   M.NOTE,
                                   M.IS_SEND,
                                   M.ORDER_ID
                              FROM ELMS_USER_TEMP.MAILS_TEMP M
                             WHERE M.IS_CHANGE != {(int)ChangeTypeEnum.Delete}    
                               AND M.OWNER_ID = {ownerID} 
                               AND M.OWNER_TYPE = {(int)ownerType}{(id.HasValue ? $@" AND M.ID = {id}" : null)}";

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
                GlobalProcedures.LogWrite("Telefon açılmadı..", sql, GlobalVariables.V_UserName, "MailDAL", "SelectMailByOwnerID", exx);
                return null;
            }
        }

        public static void InsertMail(Mail phone)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.MAILS_TEMP(OWNER_TYPE,
                                                                                           OWNER_ID,
                                                                                           MAIL,
                                                                                           NOTE,
                                                                                           IS_SEND_SMS,
                                                                                           IS_CHANGE,
                                                                                           USED_USER_ID)
                                                     VALUES(:inOWNERTYPE,
                                                            :inOWNERID,
                                                            :inMAIL,
                                                            :inNOTE,
                                                            :inISSENDSMS,
                                                            :inISCHANGE,
                                                            :inUSEDUSERID)";
                        command.Parameters.Add(new OracleParameter("inOWNERTYPE", phone.OWNER_TYPE));
                        command.Parameters.Add(new OracleParameter("inOWNERID", phone.OWNER_ID));
                        command.Parameters.Add(new OracleParameter("inMAIL", phone.MAIL));
                        command.Parameters.Add(new OracleParameter("inNOTE", phone.NOTE));
                        command.Parameters.Add(new OracleParameter("inISSENDSMS", phone.IS_SEND));
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
                    GlobalProcedures.LogWrite("Telefon temp cədvələ daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "MailDAL", "InsertMail", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void UpdateMail(Mail phone)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.MAILS_TEMP SET OWNER_TYPE = :inOWNERTYPE,
                                                                                           OWNER_ID = :inOWNERID,
                                                                                           MAIL = :inMAIL,
                                                                                           NOTE = :inNOTE,
                                                                                           IS_SEND_SMS = :inISSENDSMS,
                                                                                           IS_CHANGE = :inISCHANGE,
                                                                                           USED_USER_ID = :inUSEDUSERID
                                                            WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inOWNERTYPE", phone.OWNER_TYPE));
                        command.Parameters.Add(new OracleParameter("inOWNERID", phone.OWNER_ID));
                        command.Parameters.Add(new OracleParameter("inMAIL", phone.MAIL));
                        command.Parameters.Add(new OracleParameter("inNOTE", phone.NOTE));
                        command.Parameters.Add(new OracleParameter("inISSENDSMS", phone.IS_SEND));
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
                    GlobalProcedures.LogWrite("Telefon temp cədvəldə dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "MailDAL", "UpdateMail", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeleteMail(int phoneID, int ownerID, MailOwnerEnum phoneOwner)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.MAILS_TEMP SET IS_CHANGE = {(int)ChangeTypeEnum.Delete}
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
                    GlobalProcedures.LogWrite("Telefon temp cədvəldən silinmədi.", commandSql, GlobalVariables.V_UserName, "MailDAL", "DeleteMail", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeleteMailTemp(OracleTransaction tran, MailOwnerEnum phoneOwner)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"DELETE FROM ELMS_USER_TEMP.MAILS_TEMP 
                                                        WHERE OWNER_TYPE = :inOWNERTYPE
                                                          AND USED_USER_ID = :inUSEDUSERID";
            command.Parameters.Add(new OracleParameter("inOWNERTYPE", (int)phoneOwner));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
