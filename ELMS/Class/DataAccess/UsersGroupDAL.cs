using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.DataAccess
{
    class UsersGroupDAL
    {
        public static DataSet SelectUsersGroupByID(int? typeID)
        {
            string sql = null;
            
            if (typeID == null)
                
            sql = $@"SELECT G.ID,
                                 G.GROUP_NAME,
                                 G.NOTE,
                                 G.USED_USER_ID
                            FROM ELMS_USER.USER_GROUP G";
            else
                sql = $@"SELECT G.ID,
                                 G.GROUP_NAME,
                                 G.NOTE,
                                 G.USED_USER_ID
                            FROM ELMS_USER.USER_GROUP G 
                           WHERE G.ID = {typeID}";

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
                GlobalProcedures.LogWrite("İstifadəçi qrupları cədvələ yüklənmədi.", sql, GlobalVariables.V_UserName, "UsersGroupDAL", "SelectUsersGroupByID", exx);
                return null;
            }
        }
    }
}
