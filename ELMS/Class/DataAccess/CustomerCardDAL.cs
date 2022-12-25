using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.DataAccess
{
    class CustomerCardDAL
    {
        public static DataTable SelectViewData(int? ID)
        {
            string s = $@"SELECT CC.ID,
                                   DG.NAME DOCUMENT_GROUP,
                                   DT.NAME DOCUMENT_TYPE,
                                   CC.CARD_NUMBER,
                                   CC.ISSUE_DATE,
                                   CI.NAME ISSUE_NAME,
                                   CC.RELIABLE_DATE,
                                   CC.PINCODE
                              FROM ELMS_USER_TEMP.CUSTOMER_CARDS_TEMP CC,
                                   ELMS_USER.DOCUMENT_TYPE DT,
                                   ELMS_USER.DOCUMENT_GROUP DG,
                                   ELMS_USER.CARD_ISSUING CI
                             WHERE CC.DOCUMENT_TYPE_ID = DT.ID
                                   AND CC.CARD_ISSUING_ID = CI.ID
                                   AND CC.DOCUMENT_GROUP_ID = DG.ID
                                   AND CC.IS_CHANGE<> 2 {(ID.HasValue ? $@" AND CC.CUSTOMER_ID = {ID}" : null)}
                        ORDER BY CC.ID";

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
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CustomerCardDAL", "SelectViewData", exx);
                return null;
            }
        }

        //SELECT CC.ID,
        //                           DG.NAME DOCUMENT_GROUP_NAME,
        //                           DT.NAME DOCUMENT_TYPE_NAME,
        //                           CC.CARD_NUMBER CARD,
        //                           CC.ISSUE_DATE,
        //                           CI.NAME ISSUE_NAME,
        //                           CC.RELIABLE_DATE
        //                      FROM ELMS_USER_TEMP.CUSTOMER_CARDS_TEMP CC,
        //                           ELMS_USER.DOCUMENT_TYPE DT,
        //                           ELMS_USER.DOCUMENT_GROUP DG,
        //                           ELMS_USER.CARD_ISSUING CI
        //                     WHERE CC.DOCUMENT_TYPE_ID = DT.ID
        //                           AND CC.CARD_ISSUING_ID = CI.ID
        //                           AND CC.DOCUMENT_GROUP_ID = DG.ID
        //                           AND CC.IS_CHANGE<> 2
        //                         AND CC.CUSTOMER_ID = '1'
        //                ORDER BY CC.ID
    }
}
