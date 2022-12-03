using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class
{
    public class Enum
    {
        public enum TransactionTypeEnum
        {
            Insert = 1,
            Update = 2
        }

        public enum ChangeTypeEnum
        {
            Default = 0,
            Change = 1,
            Delete = 2
        }

        public enum AppointmentStatusEnum
        {
            New = 1,
            Confirm = 2,
            Finis = 3,
            Cancel = 4
        }

        public enum TreatmentTypeEnum
        {
            Treatment = 1,
            Consultation = 2,
            XRay = 3
        }

        public enum UserGroupEnum
        {
            Admin = 1,
            Operator = 2,
            Doctor = 3
        }

        public enum DentalRoleEnum
        {
            Home = 1,
            Customer = 2,
            Doctor = 3,
            Schedule = 4,
            Treatment = 5,
            Report = 6,
            Cabinet = 7,
            CallCenter = 8
        }

        public enum PhoneOwnerEnum
        {
            Customer = 1,
            Doctor = 2,
            Technician = 3
        }

        public enum CurrencyEnum
        {
            AZN = 1,
            USd = 2,
            EUR = 3,
            JPY = 4,
            GBP = 5,
            RUB = 6,
            SDR = 7,
            CHF = 8,
            KZT = 9,
            TRL = 10,
            GEL = 11
        }

        public enum CountryPhoneCodeEnum
        {
            AZE = 994,
            TUR = 90,
            RUS = 7
        }
        
        public enum PersonTypeEnum
        {
            Fiziki = 1,
            Huquqi = 2
        }

        public enum HorzalignmentEnum
        {
            Default = 0,
            Center = 1,
            Left = 2,
            Right = 3
        }
    }
}
