using Onix.Client.Helper;
using System;
using Wis.WsClientAPI;

namespace Onix.Client.Model
{
    public class MLeaveRecord : MBaseModel
    {
        public MLeaveRecord(CTable obj) : base(obj)
        {
        }

        public void CreateDefaultValue()
        {
        }


        public DateTime LeaveDate
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return (DateTime.Now);
                }

                String str = GetDbObject().GetFieldValue("LEAVE_DATE");
                DateTime dt = CUtil.InternalDateToDate(str);

                return (dt);
            }

            set
            {
                String str = CUtil.DateTimeToDateStringInternal(value);

                GetDbObject().SetFieldValue("LEAVE_DATE", str);
                NotifyPropertyChanged();
            }
        }

        public String EmployeeID
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("EMPLOYEE_ID"));
            }

            set
            {
                GetDbObject().SetFieldValue("EMPLOYEE_ID", value);
            }
        }

        public String LeaveRecordID
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("LEAVE_RECORD_ID"));
            }

            set
            {
                GetDbObject().SetFieldValue("LEAVE_RECORD_ID", value);
            }
        }

        public String Late
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("LATE"));
            }

            set
            {
                GetDbObject().SetFieldValue("LATE", value);
            }
        }

        public String SickLeave
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("SICK_LEAVE"));
            }

            set
            {
                GetDbObject().SetFieldValue("SICK_LEAVE", value);
            }
        }

        public String PersonalLeave
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("PERSONAL_LEAVE"));
            }

            set
            {
                GetDbObject().SetFieldValue("PERSONAL_LEAVE", value);
            }
        }

        public String ExtraLeave
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("EXTRA_LEAVE"));
            }

            set
            {
                GetDbObject().SetFieldValue("EXTRA_LEAVE", value);
            }
        }

        public String AnnualLeave
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("ANNUAL_LEAVE"));
            }

            set
            {
                GetDbObject().SetFieldValue("ANNUAL_LEAVE", value);
            }
        }

        public String AbnormalLeave
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("ABNORMAL_LEAVE"));
            }

            set
            {
                GetDbObject().SetFieldValue("ABNORMAL_LEAVE", value);
            }
        }

        public String DeductionLeave
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("DEDUCTION_LEAVE"));
            }

            set
            {
                GetDbObject().SetFieldValue("DEDUCTION_LEAVE", value);
            }
        }
    }
}
