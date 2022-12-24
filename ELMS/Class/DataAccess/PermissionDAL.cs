using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.DataAccess
{
    class PermissionDAL
    {
        
               
            public static DataSet SelectGroupPermissionByID(int? GroupID)
            {
            string sql = null;

            if (GroupID == null)

                sql = $@"SELECT R.DESCRIPTION ROLES_DESCRIPTION,
                        RD.DETAIL_NAME ROLE_DETAIL_NAME 
                        FROM ELMS_USER.ALL_USER_GROUP_ROLE_DETAILS UGRD,
                        ELMS_USER.ROLES R,
                        ELMS_USER.ALL_ROLE_DETAILS RD
                        WHERE RD.ID = UGRD.ROLE_DETAIL_ID AND R.ID = RD.ROLE_ID";
            else
                sql = $@"SELECT R.DESCRIPTION ROLES_DESCRIPTION,
                        RD.DETAIL_NAME ROLE_DETAIL_NAME
                        FROM ELMS_USER.ALL_USER_GROUP_ROLE_DETAILS UGRD,
                        ELMS_USER.ROLES R,
                        ELMS_USER.ALL_ROLE_DETAILS RD
                        WHERE RD.ID = UGRD.ROLE_DETAIL_ID AND R.ID = RD.ROLE_ID AND UGRD.GROUP_ID = {GroupID}";
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
                GlobalProcedures.LogWrite("Hüquqlar cədvələ yüklənmədi.", sql, GlobalVariables.V_UserName, "PermissionDAL", "SelectGroupPermissionByID", exx);
                return null;
            }
        }


        public static DataSet SelectPermissionByID(int? GroupID)
        {
            string sql = null;

            if (GroupID == null)

                sql = $@"SELECT R.DESCRIPTION ROLES_DESCRIPTION, RD.DETAIL_NAME ROLE_DETAIL_NAME, RD.ID
                                  FROM ELMS_USER.ALL_USER_GROUP_ROLE_DETAILS RDT,
                                       ELMS_USER.ROLES R,
                                       ELMS_USER.ALL_ROLE_DETAILS RD
                                 WHERE     RD.ID = RDT.ROLE_DETAIL_ID
                                       AND R.ID = RD.ROLE_ID";
            else
                sql = $@"SELECT R.DESCRIPTION ROLES_DESCRIPTION, RD.DETAIL_NAME ROLE_DETAIL_NAME, RD.ID
                                  FROM ELMS_USER.ALL_USER_GROUP_ROLE_DETAILS RDT,
                                       ELMS_USER.ROLES R,
                                       ELMS_USER.ALL_ROLE_DETAILS RD
                                 WHERE     RD.ID = RDT.ROLE_DETAIL_ID
                                       AND R.ID = RD.ROLE_ID
                                       AND RDT.GROUP_ID = {GroupID}";
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
                GlobalProcedures.LogWrite("Hüquqlar cədvələ yüklənmədi.", sql, GlobalVariables.V_UserName, "PermissionDAL", "SelectPermissionByID", exx);
                return null;
            }
        }
    }
}
