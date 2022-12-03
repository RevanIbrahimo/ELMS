using System;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;
using ELMS.Class.Tables;

namespace ELMS.Class
{
    class GlobalVariables
    {        
        public static string V_Version = "1.0.0.6";        
        public static string V_StyleName;
        public static string V_LastRate = null;
        public static int V_UserID = 0;
        public static int V_DoctorID = 0;
        public static int V_BranchID = 1;
        public static int V_UserGroupID = 1;
        public static string V_BranchName = null;
        public static string V_ExecutingFolder;
        public static string V_UserName = "SuperAdmin";
        public static string V_Connect_User;        
        public static int V_DefaultMenu = 0;
        public static int V_DefaultLanguage = 0;
        public static int V_DefaultDateSort = 0;    
        public static bool V_FConnect_BOK_Click = false;
        public static List<Users> lstUsers = null;
        public static List<Branch> lstBranch = null;
        public static CultureInfo V_CultureInfoEN = new CultureInfo("en-US");
        public static CultureInfo V_CultureInfoAZ = new CultureInfo("az-Latn-AZ");
        public static CultureInfo V_CultureInfoFR = new CultureInfo("fr-CA");
        public static bool WordDocumentUsed = false;
        public static int V_CommitmentCount = 0;
        public static TimeSpan V_StartTime = new TimeSpan(9, 0, 0);
        public static TimeSpan V_EndTime = new TimeSpan(18, 0, 0);

        //color
        public static int V_BlockColor1, V_BlockColor2;
        public static int V_CloseColor1, V_CloseColor2;
        public static int V_ConnectColor1, V_ConnectColor2;
        //Users
        public static bool User = false;
        public static bool NewUser = false;
        public static bool EditUser = false;
        public static bool DeleteUser = false;
        public static bool UnlockUser = false;
        
        public static bool UsersGroup = false;
        public static bool AddUserGroup = false;
        public static bool EditUserGroup = false;
        public static bool DeleteUserGroup = false;
        public static bool CopyUserGroup = false;        
    }
}
