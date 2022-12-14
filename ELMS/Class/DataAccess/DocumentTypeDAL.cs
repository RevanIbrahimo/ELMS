using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELMS.Class.Tables;

namespace ELMS.Class.DataAccess
{
    class DocumentTypeDAL
    {
        public static DataSet SelectDocumentTypeByID(int? typeID)
        {
            string sql = null;
            if (typeID == null)
                sql = $@"SELECT DT.ID,
                                 DT.NORESIDENT,
                                 DG.NAME GROUP_NAME,
                                 DT.NAME,
                                 DT.PTTRN,
                                 DT.ISPINCODE,
                                 PT.NAME PERSON_TYPE_NAME,
                                 DT.NOTE,
                                 DT.USED_USER_ID,
                                 DT.ORDER_ID
                            FROM ELMS_USER.DOCUMENT_TYPE DT,
                                 ELMS_USER.DOCUMENT_GROUP DG,
                                 ELMS_USER.PERSON_TYPE PT
                           WHERE DT.DOCUMENTGROUPID = DG.ID AND DT.PERSONTYPEID = PT.ID
                        ORDER BY DT.ORDER_ID";
            else
                sql = $@"SELECT DT.ID,
                                 DT.NORESIDENT,
                                 DG.NAME GROUP_NAME,
                                 DT.NAME,
                                 DT.PTTRN,
                                 DT.ISPINCODE,
                                 PT.NAME PERSON_TYPE_NAME,
                                 DT.NOTE,
                                 DT.USED_USER_ID,
                                 DT.ORDER_ID
                            FROM ELMS_USER.DOCUMENT_TYPE DT,
                                 ELMS_USER.DOCUMENT_GROUP DG,
                                 ELMS_USER.PERSON_TYPE PT
                           WHERE DT.DOCUMENTGROUPID = DG.ID AND DT.PERSONTYPEID = PT.ID
                              AND DT.ID = {typeID}";

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
                GlobalProcedures.LogWrite("Sənəd növü açılmadı.", sql, GlobalVariables.V_UserName, "DocumentTypeDAL", "SelectDocumentTypeByID", exx);
                return null;
            }
        }




        public static void InsertDocumentType(DocumentType documentType)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER.DOCUMENT_TYPE(NAME,
                                                                                    PTTRN,
                                                                                    PERSONTYPEID,
                                                                                    DOCUMENTGROUPID,
                                                                                    NORESIDENT,
                                                                                    INSERT_USER)
                                                    VALUES(:inNAME,
                                                           :inPTTRN,
                                                           :inPERSONTYPEID,
                                                           :inDOCUMENTGROUPID,
                                                           :inNORESIDENT,
                                                           :inINSERT_USER)";
                        command.Parameters.Add(new OracleParameter("inNAME", documentType.NAME));
                        command.Parameters.Add(new OracleParameter("inPTTRN", documentType.PTTRN));
                        command.Parameters.Add(new OracleParameter("inPERSONTYPEID", documentType.PERSONTYPEID));
                        command.Parameters.Add(new OracleParameter("inDOCUMENTGROUPID", documentType.DOCUMENTGROUPID));
                        command.Parameters.Add(new OracleParameter("inNORESIDENT", documentType.NORESIDENT));
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
                    GlobalProcedures.LogWrite("Sənəd bazaya daxil edilmədi.", commandSql, GlobalVariables.V_UserName, "DocumentTypeDAL", "InsertDocumentType", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }


        public static void UpdateDocumentType(DocumentType documentType)
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
                        command.CommandText = $@"UPDATE ELMS_USER.DOCUMENT_TYPE SET NAME = :inNAME,
                                                                                      PTTRN = :inPTTRN,
                                                                                      PERSONTYPEID = :inPERSONTYPEID,
                                                                                      DOCUMENTGROUPID = :inDOCUMENTGROUPID,
                                                                                      NORESIDENT = :inNORESIDENT,
                                                                                      USED_USER_ID = :inUSEDUSERID,
                                                                                      ORDER_ID = :inORDERID,
                                                                                      UPDATE_USER = :inUPDATEUSER,
                                                                                      UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inNAME", documentType.NAME));
                        command.Parameters.Add(new OracleParameter("inPTTRN", documentType.PTTRN));
                        command.Parameters.Add(new OracleParameter("inPERSONTYPEID", documentType.PERSONTYPEID));
                        command.Parameters.Add(new OracleParameter("inDOCUMENTGROUPID", documentType.DOCUMENTGROUPID));
                        command.Parameters.Add(new OracleParameter("inNORESIDENT", documentType.NORESIDENT));
                        command.Parameters.Add(new OracleParameter("inUSEDUSERID", documentType.USED_USER_ID));
                        command.Parameters.Add(new OracleParameter("inORDERID", documentType.ORDER_ID));
                        command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
                        command.Parameters.Add(new OracleParameter("inID", documentType.ID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Sənəd bazada dəyişdirilmədi.", commandSql, GlobalVariables.V_UserName, "DocumentTypeDAL", "UpdateDocumentType", exx);
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
            string s = $@"SELECT DT.ID,
                                 DT.NORESIDENT,
                                 DG.NAME GROUP_NAME,
                                 DT.NAME,
                                 DT.PTTRN,
                                 DT.ISPINCODE,
                                 PT.NAME PERSON_TYPE_NAME,
                                 DT.NOTE,
                                 DT.USED_USER_ID,
                                 DT.ORDER_ID
                            FROM ELMS_USER.DOCUMENT_TYPE DT,
                                 ELMS_USER.DOCUMENT_GROUP DG,
                                 ELMS_USER.PERSON_TYPE PT
                           WHERE DT.DOCUMENTGROUPID = DG.ID AND DT.PERSONTYPEID = PT.ID
                              AND DT.ID = {ID}";

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
                GlobalProcedures.LogWrite("Sənədin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "DocumentTypeDAL", "SelectViewData", exx);
                return null;
            }
        }
               
        public static void DeleteDocumentType(int professionID)
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
                        command.CommandText = $@"DELETE FROM ELMS_USER.DOCUMENT_TYPE WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", professionID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Sənəd bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "DocumentTypeDAL", "DeleteDocumentType", exx);
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
