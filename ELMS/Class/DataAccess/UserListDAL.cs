using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.DataAccess
{
    class UserListDAL
    {

        //public static DataSet SelectUserListByID(int? ID)
        //{
        //    string sql = $@"SELECT 1 SS,
        //                           ID,
        //                           FULL_NAME,
        //                           NOTE,
        //                           IS_ACTIVE,
        //                           USED_USER_ID,
        //                           SESSION_ID
        //                      FROM ELMS_USER.SYSTEM_USER";

        //    try
        //    {
        //        using (OracleDataAdapter adapter = new OracleDataAdapter(sql, GlobalFunctions.GetConnectionString()))
        //        {
        //            DataSet dsAdapter = new DataSet();
        //            adapter.Fill(dsAdapter);
        //            return dsAdapter;
        //        }
        //    }
        //    catch (Exception exx)
        //    {
        //        GlobalProcedures.LogWrite("İstifadəçinin məlumatları açılmadı.", sql, GlobalVariables.V_UserName, "UserListDAL", "SelectUserListByID", exx);
        //        return null;
        //    }
        //}

        public static DataTable SelectUserListByID(int? ID)
        {
            string s = $@"SELECT ID,
                                   FULL_NAME,
                                   NOTE,
                                   IS_ACTIVE,
                                   USED_USER_ID,
                                   SESSION_ID
                              FROM ELMS_USER.SYSTEM_USER";

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
                GlobalProcedures.LogWrite("İstifadəçinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "UserListDAL", "SelectUserListByID", exx);
                return null;
            }
        }

    }
}
