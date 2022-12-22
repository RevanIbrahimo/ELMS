using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using System.Data;
using System.Diagnostics;
using DevExpress.Utils;
using System.Xml;
using System.Drawing;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using DevExpress.XtraSplashScreen;
using System.Net;
using Tulpep.NotificationWindow;
using static ELMS.Class.Enum;
using DevExpress.XtraPivotGrid;
using ELMS.Class.Tables;

namespace ELMS.Class
{
    class GlobalProcedures
    {

        public static void SetSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var entry = config.AppSettings.Settings[key];
            if (entry == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static void InsertErrorMessage(string Sql, string UserName, string FormName, string ProcedureName, Exception ex)
        {
            if (ex.Message == null)
                return;

            using (OracleConnection connection = new OracleConnection())
            {
                string message = ("Message:" + ex.Message + ((ex.InnerException != null) ? "\r\nInnerException:\r\n" + ex.InnerException : null) + ((ex.StackTrace != null) ? "\r\nStack Trace:\r\n" + ex.StackTrace : null)).Trim(),
                            error_text = (message.Length > 4000) ? message.ToString().Substring(0, 3999) : message,
                sql = $@"INSERT INTO DENTAL_USER.ELMS_ERRORS(USER_NAME,FORM_NAME,PROCEDURE_NAME,ERROR_TEXT,SQL)
                                    VALUES('{UserName}','{FormName}','{ProcedureName}','{error_text}',:SQL)";

                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = sql;
                    command.Parameters.Add("SQL", OracleDbType.Clob, Sql, ParameterDirection.Input);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.ShowErrorMessage(exx.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        private static void CreateErrorMessage(string logMessage, TextWriter txtWriter, string Sql, string UserName, string FormName, string ProcedureName, Exception ex)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  ");
                if (UserName != null)
                    txtWriter.WriteLine("   {0} {1}", "UserName = ", UserName);
                txtWriter.WriteLine("   {0} {1}", "FormName = ", FormName);
                txtWriter.WriteLine("   {0} {1}", "ProcedureName = ", ProcedureName);
                txtWriter.WriteLine("  ");
                txtWriter.WriteLine("   {0}", GlobalFunctions.GetAllMessages(ex, logMessage));
                if (Sql != null)
                    txtWriter.WriteLine("   {0} {1}", "SQL Text      : ", Sql);
                txtWriter.WriteLine("-------------------------------------------------");
            }
            catch (Exception exx)
            {
                LogWrite("Səhv log fayla yazılmadı.", null, GlobalVariables.V_UserName, "GlobalProcedures", System.Reflection.MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void AddToFile(string logMessage)
        {
            string filename = "\\Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string path = GlobalVariables.V_ExecutingFolder + "\\Logs";
            string fullfilepath = path + filename;

            if (!Directory.Exists(path)) //No Folder? Create 
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fullfilepath)) //No File? Create
            {
                StreamWriter sw = File.CreateText(fullfilepath);
                sw.Close();
            }
            using (StreamWriter w = File.AppendText(fullfilepath))
            {
                w.WriteLine(logMessage);
            }
        }

        public static void LogWrite(string logMessage, string Sql, string UserName, string FormName, string ProcedureName, Exception exception)
        {
            string filename = "\\ErrorLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string path = GlobalVariables.V_ExecutingFolder + "\\Logs";
            string fullfilepath = path + filename;

            if (!Directory.Exists(path)) //No Folder? Create 
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(fullfilepath)) //No File? Create
            {
                StreamWriter sw = File.CreateText(fullfilepath);
                sw.Close();
            }
            using (StreamWriter w = File.AppendText(fullfilepath))
            {
                CreateErrorMessage(logMessage, w, Sql, UserName, FormName, ProcedureName, exception);
                InsertErrorMessage(Sql, UserName, FormName, ProcedureName, exception);
                ShowErrorMessage(logMessage);
            }
        }

        public static void GridCustomColumnDisplayText(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "S/s")
                e.DisplayText = (e.ListSourceRowIndex + 1).ToString();
        }

        public static void ShowErrorMessage(string message, Exception exx = null)
        {
            XtraMessageBox.Show(message + (exx != null ? "\r\n\r\nErrorMessage: " + exx.Message : null), "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarningMessage(string message)
        {
            XtraMessageBox.Show(message, "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowNotification(string titleText, string text)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.info_32;
            popup.TitleText = titleText;
            popup.ContentText = text;
            popup.TitleColor = Color.Red;
            popup.TitleFont = new Font(popup.TitleFont, FontStyle.Bold);
            popup.AnimationDuration = 2000;
            popup.ShowCloseButton = true;
            popup.Popup();
        }

        public static void ExecuteQuery(string sql_text, string message_text = null, string procedure_name = null)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = sql_text;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message_text, sql_text, GlobalVariables.V_UserName, "GlobalProcedures", (procedure_name == null) ? MethodBase.GetCurrentMethod().Name : MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteQuery(OracleTransaction tran, string sql_text)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = sql_text;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void ExecuteTwoQuery(OracleTransaction tran, string sql_text1, string sql_text2)
        {
            OracleCommand command = tran.Connection.CreateCommand();

            if (tran != null)
                command.Transaction = tran;

            command.CommandText = sql_text1;
            command.ExecuteNonQuery();

            command.CommandText = sql_text2;
            command.ExecuteNonQuery();

            command.Dispose();
        }

        public static void ExecuteQueryWithBlob(OracleTransaction tran, string sql, string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return;

            OracleCommand command = tran.Connection.CreateCommand();

            if (tran != null)
                command.Transaction = tran;

            FileStream fls = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] blob = new byte[fls.Length];
            fls.Read(blob, 0, Convert.ToInt32(fls.Length));
            fls.Close();

            command.CommandText = sql;

            OracleParameter blobParameter = new OracleParameter();
            blobParameter.OracleDbType = OracleDbType.Blob;
            blobParameter.ParameterName = "BlobFile";
            blobParameter.Value = blob;
            command.Parameters.Add(blobParameter);
            command.ExecuteNonQuery();

            command.Dispose();
        }

        public static void ExecuteTwoQueryWithBlob(OracleTransaction tran, string sql1, string sql2, string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return;

            OracleCommand command = tran.Connection.CreateCommand();

            if (tran != null)
                command.Transaction = tran;

            command.CommandText = sql1;
            command.ExecuteNonQuery();

            FileStream fls = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] blob = new byte[fls.Length];
            fls.Read(blob, 0, Convert.ToInt32(fls.Length));
            fls.Close();

            command.CommandText = sql2;

            OracleParameter blobParameter = new OracleParameter();
            blobParameter.OracleDbType = OracleDbType.Blob;
            blobParameter.ParameterName = "BlobFile";
            blobParameter.Value = blob;
            command.Parameters.Add(blobParameter);
            command.ExecuteNonQuery();

            command.Dispose();
        }

        public static void ExecuteTwoQuery(string sql_text1, string sql_text2, string message_text, string procedure_name = null)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = sql_text1;
                    command.ExecuteNonQuery();
                    command.CommandText = sql_text2;
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message_text, "sql_text1 = " + sql_text1 + ",\r\n sql_text2 = " + sql_text2, GlobalVariables.V_UserName, "GlobalProcedures", (procedure_name == null) ? MethodBase.GetCurrentMethod().Name : MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteThreeQuery(string sql_text1, string sql_text2, string sql_text3, string message_text, string procedure_name = null)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = sql_text1;
                    command.ExecuteNonQuery();
                    command.CommandText = sql_text2;
                    command.ExecuteNonQuery();
                    command.CommandText = sql_text3;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message_text + " { " + exx.Message + " }", "sql_text1 = " + sql_text1 + ", sql_text2 = " + sql_text2 + ", sql_text3 = " + sql_text3, GlobalVariables.V_UserName, "GlobalProcedures", (procedure_name == null) ? MethodBase.GetCurrentMethod().Name : MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteFourQuery(string sql_text1, string sql_text2, string sql_text3, string sql_text4, string message_text, string procedure_name = null)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = sql_text1;
                    command.ExecuteNonQuery();
                    command.CommandText = sql_text2;
                    command.ExecuteNonQuery();
                    command.CommandText = sql_text3;
                    command.ExecuteNonQuery();
                    command.CommandText = sql_text4;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message_text + " { " + exx.Message + " }", "sql_text1 = " + sql_text1 + ", sql_text2 = " + sql_text2 + ", sql_text3 = " + sql_text3 + ", sql_text4 = " + sql_text4, GlobalVariables.V_UserName, "GlobalProcedures", (procedure_name == null) ? MethodBase.GetCurrentMethod().Name : MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteProcedure(string procedure_name, string message)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = procedure_name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message, command.CommandText, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteProcedureWithParametr(string procedure_name, string parametr_name, object parametr_value, string message)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = procedure_name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parametr_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value)).Value = parametr_value;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message, command.CommandText, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteProcedureWithParametr(OracleTransaction tran, string procedure_name, string parametr_name, object parametr_value)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = procedure_name;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(parametr_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value)).Value = parametr_value;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void ExecuteProcedureWithTwoParametr(string procedure_name, string parametr1_name, object parametr1_value, string parametr2_name, object parametr2_value, string message)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = procedure_name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parametr1_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr1_value)).Value = parametr1_value;
                    command.Parameters.Add(parametr2_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr2_value)).Value = parametr2_value;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message, command.CommandText, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteProcedureWithTwoParametr(OracleTransaction tran, string procedure_name, string parametr1_name, object parametr1_value, string parametr2_name, object parametr2_value)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = procedure_name;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(parametr1_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr1_value)).Value = parametr1_value;
            command.Parameters.Add(parametr2_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr2_value)).Value = parametr2_value;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void ExecuteProcedureWithUser(string procedure_name, string parametr_name, object parametr_value, string message)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = procedure_name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parametr_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value)).Value = parametr_value;
                    command.Parameters.Add("P_USED_USER_ID", OracleDbType.Int32).Value = GlobalVariables.V_UserID;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message, command.CommandText, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteProcedureWithUser(OracleTransaction tran, string procedure_name, string parametr_name, object parametr_value)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = procedure_name;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(parametr_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value)).Value = parametr_value;
            command.Parameters.Add("P_USED_USER_ID", OracleDbType.Int32).Value = GlobalVariables.V_UserID;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void ExecuteProcedureWithUser(OracleConnection connection, OracleTransaction tran, string procedure_name, string parametr_name, object parametr_value)
        {
            OracleCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedure_name;
            command.Parameters.Add(parametr_name, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value)).Value = parametr_value;
            command.Parameters.Add("P_USED_USER_ID", OracleDbType.Int32).Value = GlobalVariables.V_UserID;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();

            command.Dispose();
        }

        public static void ExecuteProcedureWithTwoParametrAndUser(string procedure_name, string parametr_name1, object parametr_value1, string parametr_name2, object parametr_value2, string message)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = procedure_name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parametr_name1, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value1)).Value = parametr_value1;
                    command.Parameters.Add(parametr_name2, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value2)).Value = parametr_value2;
                    command.Parameters.Add("P_USED_USER_ID", OracleDbType.Int32).Value = GlobalVariables.V_UserID;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    SplashScreenClose();
                    LogWrite(message, command.CommandText, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteProcedureWithTwoParametrAndUser(OracleTransaction tran, string procedure_name, string parametr_name1, object parametr_value1, string parametr_name2, object parametr_value2)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = procedure_name;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(parametr_name1, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value1)).Value = parametr_value1;
            command.Parameters.Add(parametr_name2, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value2)).Value = parametr_value2;
            command.Parameters.Add("P_USED_USER_ID", OracleDbType.Int32).Value = GlobalVariables.V_UserID;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void ExecuteProcedureWithThreeParametrs(string procedure_name, string parametr_name1, object parametr_value1, string parametr_name2, object parametr_value2, string parametr_name3, object parametr_value3, string message)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = procedure_name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parametr_name1, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value1)).Value = parametr_value1;
                    command.Parameters.Add(parametr_name2, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value2)).Value = parametr_value2;
                    command.Parameters.Add(parametr_name3, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value3)).Value = parametr_value3;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message, command.CommandText, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteProcedureWithThreeParametrAndUser(string procedure_name, string parametr_name1, object parametr_value1, string parametr_name2, object parametr_value2, string parametr_name3, object parametr_value3, string message)
        {
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                OracleCommand command = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = procedure_name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parametr_name1, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value1)).Value = parametr_value1;
                    command.Parameters.Add(parametr_name2, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value2)).Value = parametr_value2;
                    command.Parameters.Add(parametr_name3, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value3)).Value = parametr_value3;
                    command.Parameters.Add("P_USED_USER_ID", OracleDbType.Int32).Value = GlobalVariables.V_UserID;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception exx)
                {
                    LogWrite(message, command.CommandText, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name + "/" + procedure_name, exx);
                    transaction.Rollback();
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void ExecuteProcedureWithThreeParametrAndUser(OracleTransaction tran, string procedure_name, string parametr_name1, object parametr_value1, string parametr_name2, object parametr_value2, string parametr_name3, object parametr_value3)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = procedure_name;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(parametr_name1, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value1)).Value = parametr_value1;
            command.Parameters.Add(parametr_name2, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value2)).Value = parametr_value2;
            command.Parameters.Add(parametr_name2, GlobalFunctions.ConvertObjectToOracleDBType(parametr_value3)).Value = parametr_value3;
            command.Parameters.Add("P_USED_USER_ID", OracleDbType.Int32).Value = GlobalVariables.V_UserID;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void CalculatedTotal(OracleTransaction tran, object contract_id)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = "DENTAL_USER.PROC_CREDIT_TOTAL";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("P_CONTRACT_ID", OracleDbType.Int64).Value = contract_id;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void CalculatedAttractedFundsTotal(OracleTransaction tran, object contract_id)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = "DENTAL_USER.PROC_ATTRACTED_FUNDS_TOTAL";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("P_CONTRACT_ID", OracleDbType.Int64).Value = contract_id;

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void GridMouseUpForPopupMenu(GridView View, PopupMenu Menu, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GridHitInfo hi = View.CalcHitInfo(e.Location);
                GridFooterCellInfoArgs footerInfo = hi.FooterCell;
                if (footerInfo == null)
                {
                    if (!hi.InColumn)
                        Menu.ShowPopup(Control.MousePosition);
                }
            }
        }

        public static void GridMouseUpForPopupMenu(object sender, PopupMenu Menu, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Button == MouseButtons.Right)
            {
                GridHitInfo hi = view.CalcHitInfo(e.Location);
                GridFooterCellInfoArgs footerInfo = hi.FooterCell;
                if (footerInfo == null)
                {
                    if (!hi.InColumn)
                        Menu.ShowPopup(Control.MousePosition);
                }
            }
        }

        public static void GridMouseUpForRadialMenu(GridView View, RadialMenu Menu, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GridHitInfo hi = View.CalcHitInfo(e.Location);
                GridFooterCellInfoArgs footerInfo = hi.FooterCell;
                if (footerInfo == null)
                {
                    if (!hi.InColumn)
                        Menu.ShowPopup(Control.MousePosition);
                }
            }
        }

        public static void GridExportToFile(GridControl gridControl, string fileExtenstion, bool showPrintable = true)
        {
            bool selectedColumn = true;
            if (gridControl == null)
                return;

            var view = (gridControl.MainView as GridView);

            string filter = null, formText = "İxracda göstəriləcək sütünları seçin";
            switch (fileExtenstion)
            {
                case "xls":
                    filter = "Excel (2003)(.xls)|*.xls";
                    formText = "XLS formatına ixrac olunacaq sütunları seçin";
                    break;
                case "xlsx":
                    filter = "Excel (2010) (.xlsx)|*.xlsx";
                    formText = "XLSX formatına ixrac olunacaq sütunları seçin";
                    break;
                case "rtf":
                    filter = "RichText faylı (.rtf)|*.rtf";
                    formText = "RTF formatına ixrac olunacaq sütunları seçin";
                    break;
                case "pdf":
                    filter = "Pdf faylı (.pdf)|*.pdf";
                    formText = "PDF formatına ixrac olunacaq sütunları seçin";
                    break;
                case "html":
                    filter = "Html faylı (.html)|*.html";
                    formText = "HTML formatına ixrac olunacaq sütunları seçin";
                    break;
                case "mht":
                    filter = "Mht faylı (.mht)|*.mht";
                    formText = "Mht formatına ixrac olunacaq sütunları seçin";
                    break;
                case "txt":
                    filter = "Text faylı (.txt)|*.txt";
                    formText = "TXT formatına ixrac olunacaq sütunları seçin";
                    break;
                case "csv":
                    filter = "Csv faylı (.csv)|*.csv";
                    formText = "CSV formatına ixrac olunacaq sütunları seçin";
                    break;
                default: break;
            }

            if (showPrintable)
            {
                List<ColumnPrintable> lstColumnName = new List<ColumnPrintable>();
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    if (view.Columns[i].Visible == false)
                        continue;
                    lstColumnName.Add(new ColumnPrintable(i, view.Columns[i].Caption));
                }

                FPrintableCheck fp = new FPrintableCheck();
                fp.FormText = formText;
                fp.ListColumnName = lstColumnName;
                fp.RefreshDataGridView += new FPrintableCheck.DoEvent(RefreshPrintable);
                fp.ShowDialog();

                void RefreshPrintable(List<int> lstColumnIndex)
                {
                    if (lstColumnIndex.Count == 0)
                    {
                        selectedColumn = false;
                        return;
                    }

                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        if (view.Columns[i].Visible == false)
                            continue;

                        if (!lstColumnIndex.Contains(i))
                            view.Columns[i].OptionsColumn.Printable = DefaultBoolean.False;
                    }
                }
            }

            if (!selectedColumn)
                return;

            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    if (!String.IsNullOrWhiteSpace(view.ViewCaption))
                        saveDialog.FileName = view.ViewCaption.Replace(":", "") + "." + fileExtenstion;

                    saveDialog.Filter = filter;
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        switch (fileExtenstion)
                        {
                            case "xls":
                                gridControl.ExportToXls(exportFilePath);
                                break;
                            case "xlsx":
                                gridControl.ExportToXlsx(exportFilePath);
                                break;
                            case "rtf":
                                gridControl.ExportToRtf(exportFilePath);
                                break;
                            case "pdf":
                                gridControl.ExportToPdf(exportFilePath);
                                break;
                            case "html":
                                gridControl.ExportToHtml(exportFilePath);
                                break;
                            case "mht":
                                gridControl.ExportToMht(exportFilePath);
                                break;
                            case "txt":
                                gridControl.ExportToTextOld(exportFilePath);
                                break;
                            case "csv":
                                gridControl.ExportToCsv(exportFilePath);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception exx)
            {
                LogWrite(gridControl.Views[0].ViewCaption + "." + fileExtenstion + " faylı yaradılmadı.", gridControl.Name, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
            finally
            {
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    if (view.Columns[i].Visible == false)
                        continue;

                    view.Columns[i].OptionsColumn.Printable = DefaultBoolean.Default;
                }
            }
        }

        //public static void TreeListExportToFile(TreeList treeList, string fileExtenstion)
        //{
        //    if (treeList == null)
        //        return;

        //    string filter = null;
        //    switch (fileExtenstion)
        //    {
        //        case "xls":
        //            filter = "Excel (2003)(.xls)|*.xls";
        //            break;
        //        case "xlsx":
        //            filter = "Excel (2010) (.xlsx)|*.xlsx";
        //            break;
        //        case "rtf":
        //            filter = "RichText faylı (.rtf)|*.rtf";
        //            break;
        //        case "pdf":
        //            filter = "Pdf faylı (.pdf)|*.pdf";
        //            break;
        //        case "html":
        //            filter = "Html faylı (.html)|*.html";
        //            break;
        //        case "mht":
        //            filter = "Mht faylı (.mht)|*.mht";
        //            break;
        //        case "txt":
        //            filter = "Text faylı (.txt)|*.txt";
        //            break;
        //        case "csv":
        //            filter = "Csv faylı (.csv)|*.csv";
        //            break;
        //        default: break;
        //    }

        //    try
        //    {
        //        using (SaveFileDialog saveDialog = new SaveFileDialog())
        //        {
        //            saveDialog.Filter = filter;
        //            if (saveDialog.ShowDialog() != DialogResult.Cancel)
        //            {
        //                string exportFilePath = saveDialog.FileName;
        //                switch (fileExtenstion)
        //                {
        //                    case "xls":
        //                        treeList.ExportToXls(exportFilePath);
        //                        break;
        //                    case "xlsx":
        //                        treeList.ExportToXlsx(exportFilePath);
        //                        break;
        //                    case "rtf":
        //                        treeList.ExportToRtf(exportFilePath);
        //                        break;
        //                    case "pdf":
        //                        treeList.ExportToPdf(exportFilePath);
        //                        break;
        //                    case "html":
        //                        treeList.ExportToHtml(exportFilePath);
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exx)
        //    {
        //        LogWrite("TreeList üçün " + fileExtenstion + " faylı yaradılmadı.", treeList.Name, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
        //    }
        //}

        public static void GridRowCellStyleForBlock(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int used_user_id = int.Parse((sender as GridView).GetRowCellDisplayText(e.RowHandle, (sender as GridView).Columns["USED_USER_ID"]));
                if (used_user_id >= 0)
                {
                    e.Appearance.BackColor = GlobalFunctions.CreateColor(GlobalVariables.V_BlockColor1);
                    e.Appearance.BackColor2 = GlobalFunctions.CreateColor(GlobalVariables.V_BlockColor1);
                }
            }
        }

        public static void GridRowCellStyleForConnect(GridView View, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int connect_id = int.Parse(View.GetRowCellDisplayText(e.RowHandle, View.Columns["SESSION_ID"]));
                if (connect_id > 0)
                {
                    e.Appearance.BackColor = GlobalFunctions.CreateColor(GlobalVariables.V_ConnectColor1); //GlobalFunctions.CreateColor(GlobalVariables.V_ConnectColor_A, GlobalVariables.V_ConnectColor_R, GlobalVariables.V_ConnectColor_G, GlobalVariables.V_ConnectColor_B, GlobalVariables.V_ConnectColor_Type, GlobalVariables.V_ConnectColor_Name);
                    e.Appearance.BackColor2 = GlobalFunctions.CreateColor(GlobalVariables.V_ConnectColor2); //GlobalFunctions.CreateColor(GlobalVariables.V_ConnectColor2_A, GlobalVariables.V_ConnectColor2_R, GlobalVariables.V_ConnectColor2_G, GlobalVariables.V_ConnectColor2_B, GlobalVariables.V_ConnectColor2_Type, GlobalVariables.V_ConnectColor2_Name);
                }
            }
        }

        public static void GridRowCellStyleForClose(int statusid, GridView View, RowCellStyleEventArgs e)
        {

            if (e.RowHandle >= 0)
            {
                int status_id = int.Parse(View.GetRowCellDisplayText(e.RowHandle, View.Columns["STATUS_ID"]));
                if (status_id == statusid)
                {
                    e.Appearance.BackColor = GlobalFunctions.CreateColor(GlobalVariables.V_CloseColor1);
                    e.Appearance.BackColor2 = GlobalFunctions.CreateColor(GlobalVariables.V_CloseColor2);
                }
            }
        }

        public static void GridRowCellStyleForClose(object sender, string columnName, int statusid, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int status_id = int.Parse((sender as GridView).GetRowCellDisplayText(e.RowHandle, (sender as GridView).Columns[columnName]));
                if (status_id == statusid)
                {
                    e.Appearance.BackColor = GlobalFunctions.CreateColor(GlobalVariables.V_CloseColor1);
                    e.Appearance.BackColor2 = GlobalFunctions.CreateColor(GlobalVariables.V_CloseColor2);
                }
            }
        }

        public static void GridCustomDrawFooterCell(string fieldname, string horzalignment, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == fieldname)
            {
                switch (horzalignment)
                {
                    case "Center":
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center; //merkez
                        break;
                    case "Default":
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                        break;
                    case "Far":
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far; //sag
                        break;
                    case "Near":
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;//sol
                        break;
                }
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            }
        }

        public static void GridCustomDrawFooterCell(DevExpress.XtraGrid.Columns.GridColumn column, string horzalignment, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column == column)
            {
                switch (horzalignment)
                {
                    case "Center":
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center; //merkez
                        break;
                    case "Default":
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                        break;
                    case "Far":
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far; //sag
                        break;
                    case "Near":
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;//sol
                        break;
                }
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            }
        }

        public static void GridCustomDrawFooterCell(DevExpress.XtraGrid.Columns.GridColumn column, HorzalignmentEnum horzalignmentEnum, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column == column)
            {
                int alignment = (int)horzalignmentEnum;
                switch (alignment)
                {
                    case 1:
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center; //merkez
                        break;
                    case 0:
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                        break;
                    case 3:
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far; //sag
                        break;
                    case 2:
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;//sol
                        break;
                }
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            }
        }

        public static void ShowGridPreview(GridControl grid, bool showPrintable = true)
        {
            if (!grid.IsPrintingAvailable)
            {
                XtraMessageBox.Show("'DevExpress.XtraPrinting' kitabxanası tapılmadı", "Xəta");
                return;
            }

            var view = (grid.MainView as GridView);
            bool selectedColumn = true;

            if (showPrintable)
            {
                List<ColumnPrintable> lstColumnName = new List<ColumnPrintable>();
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    if (view.Columns[i].Visible == false)
                        continue;
                    lstColumnName.Add(new ColumnPrintable(i, view.Columns[i].Caption));
                }

                FPrintableCheck fp = new FPrintableCheck();
                fp.FormText = "Çapda göstəriləcək sütunları seçin";
                fp.ListColumnName = lstColumnName;
                fp.RefreshDataGridView += new FPrintableCheck.DoEvent(RefreshPrintable);
                fp.ShowDialog();

                void RefreshPrintable(List<int> lstColumnIndex)
                {
                    if (lstColumnIndex.Count == 0)
                    {
                        selectedColumn = false;
                        return;
                    }

                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        if (view.Columns[i].Visible == false)
                            continue;

                        if (!lstColumnIndex.Contains(i))
                            view.Columns[i].OptionsColumn.Printable = DefaultBoolean.False;
                    }
                }
            }

            if (!selectedColumn)
                return;

            grid.ShowPrintPreview();

            if (showPrintable)
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    if (view.Columns[i].Visible == false)
                        continue;

                    view.Columns[i].OptionsColumn.Printable = DefaultBoolean.Default;
                }
        }

        //public static void ShowTreeListPreview(TreeList treeList)
        //{
        //    // Check whether the Tree List can be previewed. 
        //    if (!treeList.IsPrintingAvailable)
        //    {
        //        MessageBox.Show("The Printing Library is not found", "Error");
        //        return;
        //    }

        //    // Open the Preview window. 
        //    treeList.ShowRibbonPrintPreview();
        //}

        public static void InsertUserConnection()
        {
            ExecuteQuery($@"INSERT INTO DENTAL_USER.USER_CONNECTIONS(USER_ID,
                                                                    IPADDRESS,
                                                                    MACADDRESS,
                                                                    CONNECT_DATE,
                                                                    COMPUTER_NAME,
                                                                    VERSION) 
                                VALUES({GlobalVariables.V_UserID},
                                        '{GlobalFunctions.GetIPAddress()}',
                                        '{GlobalFunctions.GetMACAddress()}',
                                        SYSDATE,
                                        '{GlobalFunctions.GetComputerName()}',
                                        '{GlobalVariables.V_Version}')",
                          "İstifadəçinin sistemə qoşulma vaxtı cədvələ daxil edilmədi.",
                          "InsertUserConnection");
        }

        public static void UpdateUserConnected()
        {
            ExecuteQuery($@"UPDATE DENTAL_USER.DENTAL_USERS SET SESSION_ID = SYS_CONTEXT ('userenv', 'sessionid') WHERE ID = {GlobalVariables.V_UserID}",
                             "İstifadəçinin sistemə qoşulması istifadəçilər cədvəlində qeyd olunmadı.",
                             "UpdateUserConnected");
        }

        public static void UpdateUserDisconnected()
        {
            ExecuteQuery($@"UPDATE DENTAL_USER.DENTAL_USERS SET SESSION_ID = 0 WHERE ID = {GlobalVariables.V_UserID}",
                            "İstifadəçinin sistemdən çıxması istifadəçilər cədvəlində qeyd olunmadı.",
                            "UpdateUserDisconnected");
        }

        public static void UpdateUserCloseConnection()
        {
            if (GlobalVariables.V_FConnect_BOK_Click)
                ExecuteQuery($@"UPDATE DENTAL_USER.USER_CONNECTIONS SET DISCONNECT_DATE = SYSDATE WHERE USER_ID = {GlobalVariables.V_UserID} AND DISCONNECT_DATE IS NULL",
                    "İstifadəçinin sistemdən çıxma vaxtı cədvəldə dəyişdirilmədi.",
                    "UpdateUserCloseConnection");
        }

        public static void Lock_or_UnLock_UserID(string tablename, int userID, string where)
        {
            ExecuteQuery($@"UPDATE {tablename} SET USED_USER_ID = {userID} {where}",
                            tablename + " cədvəli bloka düşmədi.",
                            "Lock_or_UnLock_UserID");
        }

        public static void Lock_or_UnLock_UserID(OracleTransaction tran, string tablename, int userID, string where)
        {
            ExecuteQuery(tran, $@"UPDATE {tablename} SET USED_USER_ID = {userID} {where}");
        }

        public static void SortTableByName(string tablename)
        {
            ExecuteQuery($@"UPDATE {tablename} T
                                   SET ORDER_ID =
                                          (SELECT ORDERID
                                             FROM (  SELECT ID,                                                                                                                     
                                                            ROW_NUMBER () OVER (ORDER BY NAME) ORDERID
                                                       FROM {tablename}
                                                   ORDER BY NAME)
                                            WHERE ID = T.ID)",
                          tablename + " cədvəli əlifba sırası ilə düzülmədi.",
                          "SortTableByName");
        }

        public static void FillCheckedComboBox(CheckedComboBoxEdit cb, string TableName, string DisplayMember, string Where, string selectedText = null)
        {
            string s = null;
            try
            {
                if (Where == null)
                    s = "SELECT " + DisplayMember + " FROM DENTAL_USER." + TableName;
                else
                    s = "SELECT " + DisplayMember + " FROM DENTAL_USER." + TableName + " WHERE " + Where;

                cb.Properties.Items.Clear();
                foreach (DataRow dr in GlobalFunctions.GenerateDataTable(s, "FillCheckedComboBox").Rows)
                {
                    if (!String.IsNullOrEmpty(dr[0].ToString()))
                    {
                        cb.Properties.Items.Add(dr[0].ToString(), CheckState.Unchecked, true);
                        cb.Properties.SeparatorChar = ';';
                    }

                    if (!String.IsNullOrWhiteSpace(selectedText))
                        cb.SetEditValue(selectedText);
                }
            }
            catch (Exception exx)
            {
                LogWrite(TableName + " " + cb.Name + " siyahısına yüklənə bilmədi.", s, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void FillCheckedComboBox(CheckedComboBoxEdit cb, DataTable dataTable, string DisplayMember, string selectedText = null)
        {
            cb.Properties.Items.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                if (!String.IsNullOrEmpty(dr[DisplayMember].ToString()))
                {
                    cb.Properties.Items.Add(dr[DisplayMember].ToString(), CheckState.Unchecked, true);
                    cb.Properties.SeparatorChar = ';';
                }

                if (!String.IsNullOrWhiteSpace(selectedText))
                    cb.SetEditValue(selectedText);
            }
        }

        public static void FillCheckedComboBoxWithSqlText(CheckedComboBoxEdit cb, string sqltext)
        {
            try
            {
                cb.Properties.Items.Clear();
                foreach (DataRow dr in GlobalFunctions.GenerateDataTable(sqltext, "FillCheckedComboBoxWithSqlText").Rows)
                {
                    if (!String.IsNullOrEmpty(dr[0].ToString()))
                    {
                        cb.Properties.Items.Add(dr[0].ToString(), CheckState.Unchecked, true);
                        cb.Properties.SeparatorChar = ';';
                    }
                }
            }
            catch (Exception exx)
            {
                LogWrite("SQL " + cb.Name + " siyahısına yüklənə bilmədi.", sqltext, GlobalVariables.V_UserName, "GlobalProcedures", System.Reflection.MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void FillComboBoxEdit(ComboBoxEdit cb, string TableName, string DisplayMember, string Where, int? SelectedIndex = null, bool AddFirstItemIsNull = false)
        {
            string s = null;
            try
            {
                if (Where == null)
                    s = "SELECT " + DisplayMember + " FROM DENTAL_USER." + TableName;
                else
                    s = "SELECT " + DisplayMember + " FROM DENTAL_USER." + TableName + " WHERE " + Where;

                cb.Properties.Items.Clear();

                if (AddFirstItemIsNull)
                    cb.Properties.Items.Add(String.Empty);

                foreach (DataRow dr in GlobalFunctions.GenerateDataTable(s, "FillComboBoxEdit").Rows)
                {
                    if (!String.IsNullOrEmpty(dr[0].ToString()))
                        cb.Properties.Items.Add(dr[0].ToString());
                }

                if (SelectedIndex != null)
                    cb.SelectedIndex = (int)SelectedIndex;
            }
            catch (Exception exx)
            {
                LogWrite(TableName + " " + cb.Name + " siyahısına yüklənə bilmədi.", s, GlobalVariables.V_UserName, "GlobalProcedures", System.Reflection.MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void FillComboBoxEditWithSqlText(ComboBoxEdit cb, string sqltext)
        {
            try
            {
                cb.Properties.Items.Clear();
                foreach (DataRow dr in GlobalFunctions.GenerateDataTable(sqltext, "FillComboBoxEditWithSqlText").Rows)
                {
                    if (!String.IsNullOrEmpty(dr[0].ToString()))
                        cb.Properties.Items.Add(dr[0].ToString());
                }
                //cb.SelectedIndex = 0;
            }
            catch (Exception exx)
            {
                LogWrite("SQL " + cb.Name + " siyahısına yüklənə bilmədi.", sqltext, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void FillLookUpEdit(LookUpEdit luk, string TableName, string DisplayID, string DisplayMember, string Where)
        {
            string s = null;
            try
            {
                if (Where == null)
                    s = $@"SELECT {DisplayID},{DisplayMember} FROM ELMS_USER.{TableName}";
                else
                    s = $@"SELECT {DisplayID},{DisplayMember} FROM ELMS_USER.{TableName} WHERE {Where}";
                luk.Properties.DataSource = null;
                luk.Properties.DataSource = GlobalFunctions.GenerateDataTable(s, "FillLookUpEdit");
                luk.Properties.DisplayMember = DisplayMember;
                luk.Properties.ValueMember = DisplayID;
            }
            catch (Exception exx)
            {
                LogWrite(TableName + " " + luk.Name + " siyahısına yüklənə bilmədi.", s, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void FillLookUpEdit(LookUpEdit luk, DataTable dt, int? itemIndex = null)
        {
            luk.Properties.DataSource = null;
            luk.Properties.DataSource = dt;
            if (itemIndex.HasValue)
                luk.ItemIndex = itemIndex.Value;
        }

        public static void FillLookUpEditWithSqlText(LookUpEdit luk, string sqltext, string DisplayID, string DisplayMember)
        {
            try
            {
                luk.Properties.DataSource = GlobalFunctions.GenerateDataTable(sqltext, "FillLookUpEditWithSqlText");
                luk.Properties.DisplayMember = DisplayMember;
                luk.Properties.ValueMember = DisplayID;
            }
            catch (Exception exx)
            {
                LogWrite("SQL " + luk.Name + " siyahısına yüklənə bilmədi.", sqltext, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void DeleteAllFilesInDirectory(string directorypath)
        {
            if (String.IsNullOrWhiteSpace(directorypath))
                return;

            try
            {
                DirectoryInfo dir = new DirectoryInfo(directorypath);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(directorypath, file.Name);
                    if (File.Exists(temppath))
                    {
                        if (!GlobalFunctions.IsFileLocked(file))
                            File.Delete(temppath);
                    }
                }
            }
            catch (Exception exx)
            {
                LogWrite(directorypath + " ünvanında olan fayllar silinmədi.", directorypath, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void DeleteFile(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    FileInfo fileInfo = new FileInfo(filepath);
                    if (GlobalFunctions.IsFileLocked(fileInfo))
                        GlobalProcedures.KillWord();

                    File.Delete(filepath);
                }
            }
            catch (Exception exx)
            {
                LogWrite(filepath + " faylı silinmədi.", filepath, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void KillWord()
        {
            foreach (Process p in Process.GetProcessesByName("winword"))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit();
                }
                catch
                {
                    // process was terminating or can't be terminated - deal with it
                }
            }
        }

        public static void Calculator()
        {
            Process.Start("Calc");
        }

        public static void CalcEditFormat(CalcEdit c_edit)
        {
            c_edit.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            c_edit.Properties.DisplayFormat.FormatString = "### ### ### ### ##0.00";
        }

        public static void DateEditFormat(DateEdit d_edit)
        {
            d_edit.Properties.Mask.EditMask = "dd.MM.yyyy";
            d_edit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        public static void ShowUserControl(SplitContainerControl scc, XtraUserControl module)
        {
            if (scc.Panel2.Controls.Count > 0)
            {
                scc.Panel2.Controls.RemoveAt(0);
            }
            module.Dock = DockStyle.Fill;
            module.Bounds = scc.Panel2.DisplayRectangle;

            scc.Panel2.Controls.Add(module);
        }

        public static void LoadCurrencyRateFromCBAR(string date)
        {
            int i = 0;
            string filePath = @"http://cbar.az/currencies/" + date + ".xml", s = null;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(filePath);
                try
                {
                    s = "SELECT ID,ALPHA3CODE FROM DENTAL_USER.CURRENCY ORDER BY ORDER_ID";

                    foreach (DataRow dr in GlobalFunctions.GenerateDataTable(s, "LoadCurrencyRateFromCBAR").Rows)
                    {
                        i++;
                        XmlNodeList xnList = xml.SelectNodes($@"/ValCurs[@*]/ValType/Valute[@Code='{dr["ALPHA3CODE"]}']");
                        foreach (XmlNode xn in xnList)
                        {
                            ExecuteTwoQuery($@"DELETE FROM DENTAL_USER.CURRENCY_RATES WHERE CURRENCY_ID = {dr["ID"]} AND RATE_DATE = TO_DATE('{date}','DD/MM/YYYY')",
                                            $@"INSERT INTO DENTAL_USER.CURRENCY_RATES(ID,CURRENCY_ID,RATE_DATE,AMOUNT) VALUES(CURRENCY_RATE_SEQUENCE.NEXTVAL,{dr["ID"]},TO_DATE('{date}','DD/MM/YYYY'),{xn["Value"].InnerText.Trim()})",
                                                                "Məzənnə internetdən bazaya daxil olmadı.",
                                            "LoadCurrencyRateFromCBAR");
                        }
                    }
                }
                catch (Exception exx)
                {
                    LogWrite("Valyutalar tapılmadı.", s, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
                }
            }
            catch (Exception exx)
            {
                LogWrite(filePath + " faylı Mərkəzi Bankın bazasında tapılmadı.", null, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void ChangeOrderID(string tablename, int id, string type, out int orderid)
        {
            int selected_order_id, previous_order_id, next_order_id;
            selected_order_id = GlobalFunctions.GetID($@"SELECT ORDER_ID FROM ELMS_USER.{tablename} WHERE ID = {id}");
            previous_order_id = GlobalFunctions.GetMax($@"SELECT NVL(MAX(ORDER_ID),0) FROM ELMS_USER.{tablename} WHERE ORDER_ID < {selected_order_id}");
            next_order_id = GlobalFunctions.GetMax($@"SELECT NVL(MIN(ORDER_ID),0) FROM ELMS_USER.{tablename} WHERE ORDER_ID > {selected_order_id}");
            if (type == "up")
            {
                if (previous_order_id > 0)
                    ExecuteTwoQuery($@"UPDATE ELMS_USER.{tablename} SET ORDER_ID = {selected_order_id} WHERE ORDER_ID = {previous_order_id}",
                                    $@"UPDATE ELMS_USER.{tablename} SET ORDER_ID = {previous_order_id} WHERE ID = {id}",
                                                        tablename + " cədvəlində sıralama dəyişmədi.",
                                                        "ChangeOrderID");
                orderid = GlobalFunctions.GetID($@"SELECT R FROM (SELECT ORDER_ID,ROW_NUMBER() OVER (ORDER BY ORDER_ID) R FROM ELMS_USER.{tablename} ORDER BY ORDER_ID) WHERE ORDER_ID = {previous_order_id}");
            }
            else
            {
                if (next_order_id > 0)
                    GlobalProcedures.ExecuteTwoQuery($@"UPDATE ELMS_USER.{tablename} SET ORDER_ID = {selected_order_id} WHERE ORDER_ID = {next_order_id}",
                                                     $@"UPDATE ELMS_USER.{tablename} SET ORDER_ID = {next_order_id} WHERE ID = {id}",
                                                        tablename + " cədvəlində sıralama dəyişmədi.",
                                                        "ChangeOrderID");
                orderid = GlobalFunctions.GetID($@"SELECT R FROM (SELECT ORDER_ID,ROW_NUMBER() OVER (ORDER BY ORDER_ID) R FROM ELMS_USER.{tablename} ORDER BY ORDER_ID) WHERE ORDER_ID = {next_order_id}");
            }
        }

        public static void ChangeOrderIDforTEMP(string tablename, int id, string type, out int orderid)
        {
            int selected_order_id, previous_order_id, next_order_id;
            selected_order_id = GlobalFunctions.GetID("SELECT ORDER_ID FROM DENTAL_USER_TEMP." + tablename + " WHERE ID = " + id);
            previous_order_id = GlobalFunctions.GetMax("SELECT NVL(MAX(ORDER_ID),0) FROM DENTAL_USER_TEMP." + tablename + " WHERE ORDER_ID < " + selected_order_id);
            next_order_id = GlobalFunctions.GetMax("SELECT NVL(MIN(ORDER_ID),0) FROM DENTAL_USER_TEMP." + tablename + " WHERE ORDER_ID > " + selected_order_id);
            if (type == "up")
            {
                if (previous_order_id > 0)
                    ExecuteTwoQuery("UPDATE DENTAL_USER_TEMP." + tablename + " SET ORDER_ID = " + selected_order_id + ",IS_CHANGE = 1 WHERE ORDER_ID = " + previous_order_id,
                                                     "UPDATE DENTAL_USER_TEMP." + tablename + " SET ORDER_ID = " + previous_order_id + ",IS_CHANGE = 1 WHERE ID = " + id,
                                                        tablename + " cədvəlində sıralama dəyişmədi.",
                                                        "ChangeOrderIDforTEMP");
                orderid = GlobalFunctions.GetID("SELECT R FROM (SELECT ORDER_ID,ROW_NUMBER() OVER (ORDER BY ORDER_ID) R FROM DENTAL_USER_TEMP." + tablename + " ORDER BY ORDER_ID) WHERE ORDER_ID = " + previous_order_id);
            }
            else
            {
                if (next_order_id > 0)
                    ExecuteTwoQuery("UPDATE DENTAL_USER_TEMP." + tablename + " SET ORDER_ID = " + selected_order_id + ",IS_CHANGE = 1 WHERE ORDER_ID = " + next_order_id,
                                                     "UPDATE DENTAL_USER_TEMP." + tablename + " SET ORDER_ID = " + next_order_id + ",IS_CHANGE = 1 WHERE ID = " + id,
                                                        tablename + " cədvəlində sıralama dəyişmədi.",
                                                        "ChangeOrderIDforTEMP");
                orderid = GlobalFunctions.GetID("SELECT R FROM (SELECT ORDER_ID,ROW_NUMBER() OVER (ORDER BY ORDER_ID) R FROM DENTAL_USER_TEMP." + tablename + " ORDER BY ORDER_ID) WHERE ORDER_ID = " + next_order_id);
            }
        }

        public static void LoadFontStyleComboBox(ComboBoxEdit cb)
        {
            cb.Properties.Items.Add(FontStyle.Regular.ToString());
            cb.Properties.Items.Add(FontStyle.Bold.ToString());
            cb.Properties.Items.Add(FontStyle.Italic.ToString());
        }

        public static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                XtraMessageBox.Show("Excel faylı bağlanmadı. " + "\nError - " + ex.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GC.Collect();
            }
        }

        //public static void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        //{
        //    //options
        //    object matchCase = false;
        //    object matchWholeWord = true;
        //    object matchWildCards = false;
        //    object matchSoundsLike = false;
        //    object matchAllWordForms = false;
        //    object forward = true;
        //    object format = false;
        //    object matchKashida = false;
        //    object matchDiacritics = false;
        //    object matchAlefHamza = false;
        //    object matchControl = false;
        //    object read_only = false;
        //    object visible = true;
        //    object replace = 2;
        //    object wrap = 1;
        //    //execute find and replace
        //    doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
        //        ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
        //        ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        //}

        //public static void ReplaceTextInWordDoc(Microsoft.Office.Interop.Word.Application doc, Object findMe, Object replaceMe)
        //{
        //    object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
        //    object missing = System.Reflection.Missing.Value;
        //    doc.Selection.Find.ClearFormatting();
        //    doc.Selection.Find.Text = (string)findMe;
        //    doc.Selection.Find.Replacement.ClearFormatting();

        //    if (replaceMe != null && replaceMe.ToString().Length < 256)
        //    {
        //        doc.Selection.Find.Replacement.Text = (string)replaceMe;
        //        doc.Selection.Find.Execute(
        //        ref missing, ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing, ref missing,
        //        ref replaceAll, ref missing, ref missing, ref missing, ref missing);
        //    }
        //    else
        //    {
        //        //this cycle do replacing for EVERY example of 'findMe'text.
        //        while (doc.Selection.Find.Execute(
        //              ref findMe, ref missing, ref missing, ref missing, ref missing,
        //              ref missing, ref missing, ref missing, ref missing, ref missing,
        //              ref missing, ref missing, ref missing, ref missing, ref missing))
        //        {

        //            doc.Selection.Text = (string)replaceMe;
        //            doc.Selection.Collapse();
        //        }
        //    }
        //}

        public static void GridViewPrintInitializeByLandscape(DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e, float? top = null, float? bottom = null, float? right = null, float? left = null)
        {
            DevExpress.XtraPrinting.PrintingSystemBase pb = e.PrintingSystem as DevExpress.XtraPrinting.PrintingSystemBase;
            pb.PageSettings.Landscape = true;
            pb.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;

            if (top != null)
                (e.PrintingSystem as DevExpress.XtraPrinting.PrintingSystemBase).PageSettings.TopMarginF = (float)top;
            if (bottom != null)
                (e.PrintingSystem as DevExpress.XtraPrinting.PrintingSystemBase).PageSettings.BottomMarginF = (float)bottom;
            if (right != null)
                (e.PrintingSystem as DevExpress.XtraPrinting.PrintingSystemBase).PageSettings.RightMarginF = (float)right;
            if (left != null)
                (e.PrintingSystem as DevExpress.XtraPrinting.PrintingSystemBase).PageSettings.LeftMarginF = (float)left;
        }

        public static void ShowWordFileFromDB(string sql, string filePath, string blobColumn)
        {
            DataTable dt = GlobalFunctions.GenerateDataTable(sql, "ShowWordFileFromDB");

            if (dt == null)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                if (!DBNull.Value.Equals(dr[blobColumn]))
                {
                    Byte[] BLOBData = (byte[])dr[blobColumn];
                    MemoryStream stmBLOBData = new MemoryStream(BLOBData);
                    DeleteFile(filePath);
                    FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    stmBLOBData.WriteTo(fs);
                    fs.Close();
                    stmBLOBData.Close();
                    Process.Start(filePath);
                }
            }
        }

        public static void ShowWordFileWithExtensionFromDB(string sql, string filePath, string blobColumn, string extensionColumn)
        {
            DataTable dt = GlobalFunctions.GenerateDataTable(sql, "ShowWordFileFromDB");

            if (dt == null)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                filePath = filePath + dr[extensionColumn];

                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                    break;
                }

                if (!DBNull.Value.Equals(dr[blobColumn]))
                {

                    Byte[] BLOBData = (byte[])dr[blobColumn];
                    MemoryStream stmBLOBData = new MemoryStream(BLOBData);
                    DeleteFile(filePath);
                    FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    stmBLOBData.WriteTo(fs);
                    fs.Close();
                    stmBLOBData.Close();
                    Process.Start(filePath);
                }
            }
        }

        public static void TextEditCharCount(TextEdit txt, LabelControl lbl)
        {
            if (txt.Text.Trim().Length == 0)
                lbl.Visible = false;
            else
            {
                lbl.Visible = true;
                lbl.Text = txt.Text.Trim().Length.ToString();
            }
        }

        //public static void LoadColor()
        //{
        //    List<Colors> lstColors = ColorsDAL.SelectColor(GlobalVariables.V_UserID).ToList<Colors>();
        //    if (lstColors.Count == 0)
        //        return;

        //    GlobalVariables.V_BlockColor1 = lstColors.Find(item => item.COLOR_TYPE_ID == 1).COLOR_VALUE_1;
        //    GlobalVariables.V_BlockColor2 = lstColors.Find(item => item.COLOR_TYPE_ID == 1).COLOR_VALUE_2;

        //    GlobalVariables.V_CloseColor1 = lstColors.Find(item => item.COLOR_TYPE_ID == 2).COLOR_VALUE_1;
        //    GlobalVariables.V_CloseColor2 = lstColors.Find(item => item.COLOR_TYPE_ID == 2).COLOR_VALUE_2;

        //    GlobalVariables.V_ConnectColor1 = lstColors.Find(item => item.COLOR_TYPE_ID == 4).COLOR_VALUE_1;
        //    GlobalVariables.V_ConnectColor2 = lstColors.Find(item => item.COLOR_TYPE_ID == 4).COLOR_VALUE_2;
        //}

        public static void ChangeCheckStyle(CheckEdit ce)
        {
            if (ce.Checked)
                ce.Font = new Font(ce.Font.FontFamily, ce.Font.Size, FontStyle.Bold);
            else
                ce.Font = new Font(ce.Font.FontFamily, ce.Font.Size, FontStyle.Regular);
        }

        public static void SplashScreenShow(Form form, Type t)
        {
            SplashScreenClose();
            SplashScreenManager.ShowForm(form, t, true, true, ParentFormState.Locked);
        }

        public static void SplashScreenClose()
        {
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
                SplashScreenManager.CloseForm();
        }

        public static void DeleteLookUpEditSelectedValue(LookUpEdit lookup)
        {
            if (lookup != null)
                lookup.EditValue = null;
        }

        public static void LookUpEditValue(LookUpEdit lookup, string value)
        {
            lookup.EditValue = lookup.Properties.GetKeyValueByDisplayText(value);
        }

        public static void GenerateAutoRowNumber(object sender, DevExpress.XtraGrid.Columns.GridColumn column, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column == column && e.IsGetData)
                e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
        }

        public static void ShowPivotGridPreview(PivotGridControl pivotGrid)
        {
            if (!pivotGrid.IsPrintingAvailable)
            {
                XtraMessageBox.Show("'DevExpress.XtraPrinting' kitabxanası tapılmadı", "Xəta");
                return;
            }
            pivotGrid.ShowPrintPreview();
        }

        public static void PivotGridExportToFile(PivotGridControl gridControl, string fileExtenstion)
        {
            if (gridControl == null)
                return;

            string filter = null;
            switch (fileExtenstion)
            {
                case "xls":
                    filter = "Excel (2003)(.xls)|*.xls";
                    break;
                case "xlsx":
                    filter = "Excel (2010) (.xlsx)|*.xlsx";
                    break;
                case "rtf":
                    filter = "RichText faylı (.rtf)|*.rtf";
                    break;
                case "pdf":
                    filter = "Pdf faylı (.pdf)|*.pdf";
                    break;
                case "html":
                    filter = "Html faylı (.html)|*.html";
                    break;
                case "mht":
                    filter = "Mht faylı (.mht)|*.mht";
                    break;
                case "txt":
                    filter = "Text faylı (.txt)|*.txt";
                    break;
                case "csv":
                    filter = "Csv faylı (.csv)|*.csv";
                    break;
                default: break;
            }

            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = filter;
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        switch (fileExtenstion)
                        {
                            case "xls":
                                gridControl.ExportToXls(exportFilePath);
                                break;
                            //case "xlsx":
                            //    {
                            //        var pivotExportOptions = new PivotXlsxExportOptions();
                            //        pivotExportOptions.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            //        gridControl.ExportToXlsx(exportFilePath, pivotExportOptions);
                            //    }
                            //    break;
                            case "rtf":
                                gridControl.ExportToRtf(exportFilePath);
                                break;
                            case "pdf":
                                gridControl.ExportToPdf(exportFilePath);
                                break;
                            case "html":
                                gridControl.ExportToHtml(exportFilePath);
                                break;
                            case "mht":
                                gridControl.ExportToMht(exportFilePath);
                                break;
                            case "txt":
                                gridControl.ExportToText(exportFilePath);
                                break;
                            case "csv":
                                gridControl.ExportToCsv(exportFilePath);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception exx)
            {
                LogWrite(fileExtenstion + " faylı yaradılmadı.", gridControl.Name, GlobalVariables.V_UserName, "GlobalProcedures", MethodBase.GetCurrentMethod().Name, exx);
            }
        }

        public static void GridSaveLayout(GridView view, string ModulName)
        {
            string path = GlobalVariables.V_ExecutingFolder + "\\Layouts\\" + ModulName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = path + "\\" + view.Name + ".xml";
            view.SaveLayoutToXml(fileName);
        }

        public static void GridRestoreLayout(GridView view, string ModulName)
        {
            string path = GlobalVariables.V_ExecutingFolder + "\\Layouts\\" + ModulName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = path + "\\" + view.Name + ".xml";
            if (File.Exists(fileName))
                view.RestoreLayoutFromXml(fileName);
        }        
    }
}
