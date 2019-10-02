using System;
using System.Windows;
using Wis.WsClientAPI;
using Onix.Client.Model;
using Onix.Client.Helper;
using Onix.ClientCenter.Commons.Windows;
using System.Windows.Controls;
using Onix.ClientCenter.Commons.Utils;
using System.Collections;

namespace Onix.ClientCenter.UI.HumanResource.Leave
{
    public partial class WinAddEditEmployeeLeave : WinBase
    {
        public WinAddEditEmployeeLeave(CWinLoadParam param) : base(param)
        {               
            accessRightName = "HR_LEAVE_EDIT";

            createAPIName = "SaveEmployeeLeaveDoc";
            updateAPIName = "SaveEmployeeLeaveDoc";
            getInfoAPIName = "GetEmployeeLeaveDocInfo";

            InitializeComponent();

            //Need to be after InitializeComponent
            registerValidateControls(lblNameTh, txtNameTh, false);
            registerValidateControls(lblLastNameTh, txtLastNameTh, false);  
        }

        protected override MBaseModel createObject()
        {
            MEmployeeLeave mv = new MEmployeeLeave(new CTable("EMPLOYEE"));
            mv.DocumentDate = DateTime.Now;
            
            mv.CreateDefaultValue();
            //mv.AddLeaveRecord(new MLeaveRecord(new CTable("")));
            
            return (mv);
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            Boolean r = saveData();
            if (r)
            {
                vw.IsModified = false;
                CUtil.EnableForm(true, this);
                this.Close();
            }
        }

        protected override Boolean postValidate()
        {
            Hashtable hashDups = new Hashtable();

            MEmployeeLeave mv = (MEmployeeLeave)vw;            

            DateTime parentLeaveDate = mv.DocumentDate;
            var items = mv.LeaveRecords;

            foreach (var item in items)
            {
                if (item.ExtFlag.Equals("D"))
                {
                    continue;
                }

                DateTime leaveDate = item.LeaveDate;

                string key = CUtil.DateTimeToDateString(leaveDate);
                if (hashDups.ContainsKey(key))
                {
                    CHelper.ShowErorMessage(leaveDate.ToString(), "ERROR_DUPLICATE_DATE", null);
                    return false;
                }
                else
                {
                    hashDups.Add(key, key);
                }
                
                if ((leaveDate.Month != parentLeaveDate.Month) || (leaveDate.Year != parentLeaveDate.Year))
                {
                    CHelper.ShowErorMessage(leaveDate.ToString(), "ERROR_NOT_IN_SAME_MONTH", null);
                    return false;
                }

                double sum = CUtil.StringToDouble(item.AnnualLeave) + CUtil.StringToDouble(item.PersonalLeave) +
                    CUtil.StringToDouble(item.ExtraLeave) + CUtil.StringToDouble(item.SickLeave);
                if (sum > 1)
                {
                    CHelper.ShowErorMessage(leaveDate.ToString(), "ERROR_LEAVE_OVER_DAY", null);
                    return false;
                }
            }

            return (true);
        }

        private void CboPoc_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            MEmployeeLeave mv = (MEmployeeLeave)vw;

            mv.CalculateLeaveTotal();
            vw.IsModified = true;
        }

        private void CmdAdd_Click(object sender, RoutedEventArgs e)
        {
            MEmployeeLeave mv = (MEmployeeLeave)vw;            

            MLeaveRecord item = new MLeaveRecord(new CTable(""));
            item.LeaveDate = mv.DocumentDate;

            mv.CalculateLeaveTotal();
            mv.AddLeaveRecord(item);

            mv.IsModified = true;
        }

        private void CmdDelete_Click(object sender, RoutedEventArgs e)
        {
            MEmployeeLeave mv = (MEmployeeLeave) vw;

            MLeaveRecord pp = (MLeaveRecord)(sender as Button).Tag;

            bool result = CHelper.AskConfirmMessage(pp.LeaveDate.ToString(), "CONFIRM_DELETE_ITEM");
            if (result)
            {
                mv.RemoveLeaveRecord(pp);
                mv.CalculateLeaveTotal();
                mv.IsModified = true;
            }
        }

        private void CmdAdd2_Click(object sender, RoutedEventArgs e)
        {
            postValidate();
        }

        private void CmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!vw.IsModified)
            {
                return;
            }

            Boolean r = saveData();
            if (r)
            {
                loadParam.ActualView = vw;
                loadParam.Mode = "E";

                loadData();
                vw.IsModified = false;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MEmployeeLeave mv = (MEmployeeLeave)vw;

            mv.CalculateLeaveTotal();
            vw.IsModified = true;
        }
    }
}
