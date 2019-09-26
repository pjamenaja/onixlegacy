﻿using System;
using System.Collections;
using System.Collections.ObjectModel;

using Onix.Client.Helper;
using Wis.WsClientAPI;

namespace Onix.Client.Model
{
    public class MEmployeeLeave : MBaseModel
    {
        private ObservableCollection<MLeaveRecord> leaveItems = new ObservableCollection<MLeaveRecord>();
        private int internalSeq = 0;

        public ObservableCollection<MLeaveRecord> LeaveRecords
        {
            get
            {
                return (leaveItems);
            }
        }

        public override void InitializeAfterLoaded()
        {
            leaveItems.Clear();

            CTable o = GetDbObject();
            if (o == null)
            {
                return;
            }

            ArrayList arr = o.GetChildArray("EMPLOYEE_LEAVE_RECORDS");

            if (arr == null)
            {
                return;
            }

            foreach (CTable t in arr)
            {
                MLeaveRecord v = new MLeaveRecord(t);
                leaveItems.Add(v);

                v.Seq = internalSeq;

                v.ExtFlag = "I";
            }
        }

        public void RemoveLeaveRecord(MLeaveRecord item)
        {
            removeAssociateItems(item, "EMPLOYEE_LEAVE_RECORDS", "INTERNAL_SEQ", "EMP_LEAVE_REC_ID");
            leaveItems.Remove(item);
        }

        public void AddLeaveRecord(MLeaveRecord item)
        {
            CTable o = GetDbObject();
            ArrayList arr = o.GetChildArray("EMPLOYEE_LEAVE_RECORDS");
            if (arr == null)
            {
                arr = new ArrayList();
                o.AddChildArray("EMPLOYEE_LEAVE_RECORDS", arr);
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
        
        public String EmployeeLastName
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("EMPLOYEE_LASTNAME"));
            }

            set
            {
                GetDbObject().SetFieldValue("EMPLOYEE_LASTNAME", value);
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

        #region LEave Month Year

        public String LeaveYear
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("LEAVE_YEAR"));
            }

            set
            {
                GetDbObject().SetFieldValue("LEAVE_YEAR", value);
            }
        }

        public String LeaveYearBD
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                int y = CUtil.StringToInt(LeaveYear);
                y = y + 543;
                return (y.ToString());
            }

            set
            {

            }
        }

        public MMasterRef LeaveMonthObj
        {
            set
            {
                if (value == null)
                {
                    return;
                }

                MMasterRef m = value as MMasterRef;
                LeaveMonth = m.MasterID;
                LeaveMonthName = m.Description;
            }

            get
            {
                if (GetDbObject() == null)
                {
                    return (null);
                }

                ObservableCollection<MMasterRef> items = CMasterReference.Instance.Months;
                if (items == null)
                {
                    return (null);
                }

                String tm = LeaveMonth;
                return (CUtil.MasterIDToObject(items, tm));
            }
        }

        public String LeaveMonth
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                return (GetDbObject().GetFieldValue("LEAVE_MONTH"));
            }

            set
            {
                GetDbObject().SetFieldValue("LEAVE_MONTH", value);
            }
        }

        public String LeaveMonthName
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                String month = LeaveMonth;
                if (month.Equals(""))
                {
                    return ("");
                }

                String tmp = CUtil.IDToMonth(CUtil.StringToInt(month));
                return (tmp);
            }

            set
            {
                GetDbObject().SetFieldValue("LEAVE_MONTH_NAME", value);
                //NotifyPropertyChanged();
            }
        }

        #endregion Tax Month   


        #region DocumentDate

        public DateTime? FromDocumentDate
        {
            set
            {
                String str = CUtil.DateTimeToDateStringInternalMin(value);

                GetDbObject().SetFieldValue("FROM_DOCUMENT_DATE", str);
            }
        }

        public DateTime? ToDocumentDate
        {
            set
            {
                String str = CUtil.DateTimeToDateStringInternalMax(value);

                GetDbObject().SetFieldValue("TO_DOCUMENT_DATE", str);
            }
        }

        public DateTime DocumentDate
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return (DateTime.Now);
                }

                String str = GetDbObject().GetFieldValue("DOCUMENT_DATE");
                DateTime dt = CUtil.InternalDateToDate(str);

                return (dt);
            }

            set
            {
                String str = CUtil.DateTimeToDateStringInternal(value);

                GetDbObject().SetFieldValue("DOCUMENT_DATE", str);
                NotifyPropertyChanged();
            }
        }

        public String DocumentDateFmt
        {
            get
            {
                if (GetDbObject() == null)
                {
                    return ("");
                }

                String str = GetDbObject().GetFieldValue("DOCUMENT_DATE");
                DateTime dt = CUtil.InternalDateToDate(str);
                String str2 = CUtil.DateTimeToDateString(dt);

                return (str2);
            }

            set
            {
            }
        }

        #endregion

        public Boolean? HasResignedFlag
        {
            get
            {
                String flag = GetDbObject().GetFieldValue("RESIGNED_FLAG");
                if (flag.Equals(""))
                {
                    return (false);
                }

                return (flag.Equals("Y"));
            }

            set
            {
                String flag = "N";
                if (value == null)
                {
                    flag = "N";
                }
                else if ((Boolean)value)
                {
                    flag = "Y";
                }

                GetDbObject().SetFieldValue("RESIGNED_FLAG", flag);
                NotifyPropertyChanged();
            }
        }
    }
}
