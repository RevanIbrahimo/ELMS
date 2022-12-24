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
    public class UserDAL
    {
        public static DataSet SelectUserByID(int? ID)
        {
            string sql = $@"SELECT * FROM ELMS_USER.SYSTEM_USER {(ID.HasValue ? $@"WHERE ID = {ID}" : null)} ORDER BY FULL_NAME";

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
                GlobalProcedures.LogWrite("İstifadəçinin məlumatları açılmadı.", sql, GlobalVariables.V_UserName, "UserDAL", "SelectUserByID", exx);
                return null;
            }
        }


        public static DataSet SelectUserByGroupID(int? GroupID)
        {
            string sql = $@"SELECT FULL_NAME,
                                 ID,
                                 USED_USER_ID,
                                 SEX_ID,
                                 SESSION_ID
                            FROM ELMS_USER.SYSTEM_USER
                           WHERE IS_ACTIVE = 1 AND GROUP_ID = {GroupID}
                        ORDER BY FULL_NAME";
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
                GlobalProcedures.LogWrite("Qrupa daxil olan istifadəçilərin siyahısı cədvələ yüklənmədi.", sql, GlobalVariables.V_UserName, "UserDAL", "SelectUserByGroupID", exx);
                return null;
            }
        }

        public static Int32 InsertUser(OracleTransaction tran, Users user)
        {
            Int32 id = 0;
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER.SYSTEM_USER(BRANCH_ID,
                                                                          FULL_NAME,
                                                                          LOGIN_NAME,
                                                                          PASSWORD,
                                                                          BIRTHDAY,
                                                                          SEX_ID,
                                                                          ADDRESS,
                                                                          EMAIL,
                                                                          NOTE,
                                                                          GROUP_ID,
                                                                          DOCTOR_ID,
                                                                          INSERT_USER)
                                                    VALUES(:inBRANCHID,
                                                           :inFULLNAME,
                                                           :inLOGINNAME,
                                                           :inPASSWORD, 
                                                           :inBIRTHDAY,
                                                           :inSEXID,                                                          
                                                           :inADDRESS,
                                                           :inEMAIL,                                                         
                                                           :inNOTE,
                                                           :inGROUPID,
                                                           :inDOCTORID,
                                                           :inINSERTUSER) RETURNING ID INTO :outID";
            
            command.Parameters.Add(new OracleParameter("inBRANCHID", user.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inFULLNAME", user.FULL_NAME));
            command.Parameters.Add(new OracleParameter("inLOGINNAME", user.LOGIN_NAME));
            command.Parameters.Add(new OracleParameter("inPASSWORD", user.PASSWORD));            
            command.Parameters.Add(new OracleParameter("inBIRTHDAY", user.BIRTHDAY));
            command.Parameters.Add(new OracleParameter("inSEXID", user.SEX_ID));            
            command.Parameters.Add(new OracleParameter("inADDRESS", user.ADDRESS));
            command.Parameters.Add(new OracleParameter("inEMAIL", user.EMAIL));
            command.Parameters.Add(new OracleParameter("inNOTE", user.NOTE));
            command.Parameters.Add(new OracleParameter("inGROUPID", user.GROUP_ID));
            command.Parameters.Add(new OracleParameter("inDOCTORID", user.DOCTOR_ID));
            command.Parameters.Add(new OracleParameter("inINSERTUSER", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("outID", OracleDbType.Int32, ParameterDirection.Output));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            id = Convert.ToInt32(command.Parameters["outID"].Value.ToString());

            command.Dispose();

            return id;
        }

        public static void UpdateUser(OracleTransaction tran, Users user)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"UPDATE ELMS_USER.SYSTEM_USER SET BRANCH_ID = :inBRANCHID,
                                                                          FULL_NAME = :inFULLNAME,
                                                                          LOGIN_NAME = :inLOGINNAME,
                                                                          PASSWORD = :inPASSWORD, 
                                                                          BIRTHDAY = :inBIRTHDAY,
                                                                          SEX_ID = :inSEXID,    
                                                                          ADDRESS = :inADDRESS,
                                                                          EMAIL = :inEMAIL,    
                                                                          NOTE = :inNOTE,
                                                                          GROUP_ID = :inGROUPID,
                                                                          DOCTOR_ID = :inDOCTORID,
                                                                          UPDATE_USER = :inUPDATEUSER,
                                                                          UPDATE_DATE = SYSDATE
                                        WHERE ID = :inID";

            command.Parameters.Add(new OracleParameter("inBRANCHID", user.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inFULLNAME", user.FULL_NAME));
            command.Parameters.Add(new OracleParameter("inLOGINNAME", user.LOGIN_NAME));
            command.Parameters.Add(new OracleParameter("inPASSWORD", user.PASSWORD));
            command.Parameters.Add(new OracleParameter("inBIRTHDAY", user.BIRTHDAY));
            command.Parameters.Add(new OracleParameter("inSEXID", user.SEX_ID));
            command.Parameters.Add(new OracleParameter("inADDRESS", user.ADDRESS));
            command.Parameters.Add(new OracleParameter("inEMAIL", user.EMAIL));
            command.Parameters.Add(new OracleParameter("inNOTE", user.NOTE));
            command.Parameters.Add(new OracleParameter("inGROUPID", user.GROUP_ID));
            command.Parameters.Add(new OracleParameter("inDOCTORID", user.DOCTOR_ID));
            command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inID", user.ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();

            command.Dispose();
        }


        public static void InsertUsers(Users users)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER.SYSTEM_USER(FULL_NAME,
                                                            LOGIN_NAME,
                                                            PASSWORD,
                                                            EMAIL,
                                                            NOTE,
                                                            INSERT_USER)
                                                    VALUES(:inFULL_NAME,
                                                           :inLOGIN_NAME,
                                                           :inPASSWORD,
                                                           :inEMAIL,
                                                           :inNOTE,
                                                           :inINSERT_USER)";
                        command.Parameters.Add(new OracleParameter("inFULL_NAME", users.FULL_NAME));
                        command.Parameters.Add(new OracleParameter("inLOGIN_NAME", users.LOGIN_NAME));
                        command.Parameters.Add(new OracleParameter("inPASSWORD", users.PASSWORD));
                        command.Parameters.Add(new OracleParameter("inEMAIL", users.EMAIL));
                        command.Parameters.Add(new OracleParameter("inNOTE", users.NOTE));
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
                    GlobalProcedures.LogWrite("İstifadəçi bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "UsersDAL", "InsertUsers", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }


        public static void UpdateUsers(Users users)
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
                        command.CommandText = $@"UPDATE ELMS_USER.SYSTEM_USER SET FULL_NAME = :inFULL_NAME,
                                                                                  LOGIN_NAME = :inLOGIN_NAME,
                                                                                  PASSWORD = :inPASSWORD,
                                                                                  EMAIL = :inEMAIL,
                                                                                  IS_ACTIVE = :inIS_ACTIVE,
                                                                                  NOTE = :inNOTE,
                                                                                  USED_USER_ID = :inUSEDUSERID,
                                                                                  UPDATE_USER = :inUPDATEUSER,
                                                                                  UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inFULL_NAME", users.FULL_NAME));
                        command.Parameters.Add(new OracleParameter("inLOGIN_NAME", users.LOGIN_NAME));
                        command.Parameters.Add(new OracleParameter("inPASSWORD", users.PASSWORD));
                        command.Parameters.Add(new OracleParameter("inEMAIL", users.EMAIL));
                        command.Parameters.Add(new OracleParameter("inIS_ACTIVE", users.IS_ACTIVE));
                        command.Parameters.Add(new OracleParameter("inNOTE", users.NOTE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", users.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", users.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("İstifadəçi bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "UsersDAL", "UpdateUsers", exx);
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
