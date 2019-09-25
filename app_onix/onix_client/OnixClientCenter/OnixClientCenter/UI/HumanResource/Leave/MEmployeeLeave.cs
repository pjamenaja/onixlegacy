using System;
using System.Collections;
using System.Collections.ObjectModel;

using Onix.Client.Helper;
using Wis.WsClientAPI;

namespace Onix.Client.Model
{
    public class MEmployeeLeave : MBaseModel
    {
        private ObservableCollection<MLeaveRecord> leaveItems = new ObservableCollection<MLeaveRecord>();

        public ObservableCollection<MLeaveRecord> LeaveRecords
        {
            get
            {
                return (leaveItems);
            }
        }

        public void RemoveLeaveRecord(MLeaveRecord item)
        {
            removeAssociateItems(item, "LEAVE_RECORD_ITEM", "INTERNAL_SEQ", "LEAVE_RECORD_ID");
            leaveItems.Remove(item);
        }

        public void AddLeaveRecord(MLeaveRecord item)
        {
            CTable o = GetDbObject();
            ArrayList arr = o.GetChildArray("LEAVE_RECORD_ITEM");
            if (arr == null)
            {
                arr = new ArrayList();
                o.AddChildArray("LEAVE_RECORD_ITEM", arr);
            }

            item.ExtFlag = "A";
            arr.Add(item.GetDbObject());
            leaveItems.Add(item);
        }

        public MEmployeeLeave(CTable obj) : base(obj)
        {
        }

        public void CreateDefaultValue()
        {
        }

        public override void createToolTipItems()
        {
            ttItems.Clear();

            CToolTipItem ct = new CToolTipItem("employee_code", EmployeeCode);
            ttItems.Add(ct);

            ct = new CToolTipItem("employee_name", EmployeeName);
            ttItems.Add(ct);
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

        public DateTime LeaveMonth
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return (DateTime.Now);
                }

                String str = GetDbObject().GetFieldValue("LEAVE_MONTH");
                DateTime dt = CUtil.InternalDateToDate(str);

                return (dt);
            }

            set
            {
                String str = CUtil.DateTimeToDateStringInternal(value);

                GetDbObject().SetFieldValue("LEAVE_MONTH", str);
                NotifyPropertyChanged();
            }
        }

        public String EmployeeCode
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("EMPLOYEE_CODE"));
            }

            set
            {
                GetDbObject().SetFieldValue("EMPLOYEE_CODE", value);
                //updateFlag();
                NotifyPropertyChanged();
            }
        }

        public String EmployeeNameLastname
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("EMPLOYEE_NAME_LASTNAME"));
            }

            set
            {
                GetDbObject().SetFieldValue("EMPLOYEE_NAME_LASTNAME", value);
                NotifyPropertyChanged();
            }
        }

        public String EmployeeName
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("EMPLOYEE_NAME"));
            }

            set
            {
                GetDbObject().SetFieldValue("EMPLOYEE_NAME", value);
                NotifyPropertyChanged();
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
