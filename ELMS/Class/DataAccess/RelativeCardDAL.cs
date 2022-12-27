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
    class RelativeCardDAL
    {


        public static DataTable SelectRelativeByOwnerID(int ownerID, int? id = null)
        {
            string sql = $@"SELECT P.ID,
                                   P.CUSTOMER_ID,
                                   KR.NAME KIND_NAME,
                                   PN.NAME PROFESSION_NAME,
                                   P.PHONE_DESCRIPTION_ID,
                                   PD.NAME DESCRIPTION_NAME,
                                   P.PHONE_PREFIX_ID,
                                   P.PHONE_NUMBER,
                                   P.NAME,
                                   P.SALARY,
                                   P.NOTE
                              FROM ELMS_USER_TEMP.CUSTOMER_RELATIVE_TEMP P,
                                   ELMS_USER.PHONE_DESCRIPTIONS PD,
                                   ELMS_USER.KINDSHIP_RATE KR,
                                   ELMS_USER.PROFESSION PN
                             WHERE P.PHONE_DESCRIPTION_ID = PD.ID 
                               AND P.KINDSHIP_RATE_ID = KR.ID
                               AND P.PROFESSION_ID = PN.ID
                               AND P.IS_CHANGE != {(int)ChangeTypeEnum.Delete}    
                               AND P.CUSTOMER_ID = {ownerID}{(id.HasValue ? $@" AND P.ID = {id}" : null)}";

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
                GlobalProcedures.LogWrite("Telefon açılmadı.", sql, GlobalVariables.V_UserName, "RelativeDAL", "SelectRelativeByOwnerID", exx);
                return null;
            }
        }
    }
}
