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
    public class BranchDAL
    {
        public static DataSet SelectBranchByID(int? ID)
        {
            string sql = null;
            if (ID == null)
                sql = "SELECT ID,NAME,LEADING_NAME,ADDRESS,PHONE,NOTE,USED_USER_ID,ORDER_ID FROM ELMS_USER.BRANCH ORDER BY ORDER_ID";
            else
                sql = $@"SELECT ID,NAME,LEADING_NAME,ADDRESS,PHONE,NOTE,USED_USER_ID,ORDER_ID FROM ELMS_USER.BRANCH WHERE ID = {ID}";

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
                GlobalProcedures.LogWrite("Filial açılmadı.", sql, GlobalVariables.V_UserName, "BranchDAL", "SelectBranchByID", exx);
                return null;
            }
        }

        public static void InsertBranch(Branch branch)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER.BRANCH(NAME,
                                                                                LEADING_NAME,
                                                                                ADDRESS,
                                                                                PHONE,
                                                                                NOTE,
                                                                                INSERT_USER)
                                                    VALUES(:inNAME,
                                                           :inLEADING_NAME,
                                                           :inADDRESS,
                                                           :inPHONE,
                                                           :inNOTE,
                                                           :inINSERTUSER)";
                        command.Parameters.Add(new OracleParameter("inNAME", branch.NAME));
                        command.Parameters.Add(new OracleParameter("inLEADING_NAME", branch.LEADING_NAME));
                        command.Parameters.Add(new OracleParameter("inADDRESS", branch.ADDRESS));
                        command.Parameters.Add(new OracleParameter("inPHONE", branch.PHONE));
                        command.Parameters.Add(new OracleParameter("inNOTE", branch.NOTE));
                        command.Parameters.Add(new OracleParameter("inINSERTUSER", GlobalVariables.V_UserID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Filial bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "BranchDAL", "InsertBranch", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void UpdateBranch(Branch branch)
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
                        command.CommandText = $@"UPDATE ELMS_USER.BRANCH SET NAME = :inNAME,
                                                                                  LEADING_NAME = :inLEADING_NAME,
                                                                                  ADDRESS = :inADDRESS,
                                                                                  PHONE = :inPHONE,
                                                                                  NOTE = :inNOTE,
                                                                                  USED_USER_ID = :inUSEDUSERID,
                                                                                  ORDER_ID = :inORDERID,
                                                                                  UPDATE_USER = :inUPDATEUSER,
                                                                                  UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inNAME", branch.NAME));
                        command.Parameters.Add(new OracleParameter("inLEADING_NAME", branch.LEADING_NAME));
                        command.Parameters.Add(new OracleParameter("inADDRESS", branch.ADDRESS));
                        command.Parameters.Add(new OracleParameter("inPHONE", branch.PHONE));
                        command.Parameters.Add(new OracleParameter("inNOTE", branch.NOTE));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", branch.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inORDERID", branch.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", branch.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Filial bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "BranchDAL", "UpdateBranch", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeleteBranch(int branchID)
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
                        command.CommandText = $@"DELETE FROM ELMS_USER.BRANCH WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", branchID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Filial bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "BranchDAL", "DeleteBranch", exx);
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
