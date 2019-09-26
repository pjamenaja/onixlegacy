using System;
using System.Windows;
using Wis.WsClientAPI;
using Onix.Client.Model;
using Onix.Client.Helper;
using Onix.ClientCenter.Commons.Windows;
using System.Windows.Controls;
using Onix.ClientCenter.Commons.Utils;

namespace Onix.ClientCenter.UI.HumanResource.Leave
{
    public partial class WinAddEditEmployeeLeave : WinBase
    {
        public WinAddEditEmployeeLeave(CWinLoadParam param) : base(param)
        {               
            accessRightName = "HR_LEAVE_EDIT";

            createAPIName = "SaveEmployeeLeave";
            updateAPIName = "SaveEmployeeLeave";
            getInfoAPIName = "GetEmployeeLeaveInfo";

            InitializeComponent();

            //Need to be after InitializeComponent
            registerValidateControls(lblNameTh, txtNameTh, false);
            registerValidateControls(lblLastNameTh, txtLastNameTh, false);  
        }

        protected override MBaseModel createObject()
        {
            MEmployeeLeave mv = new MEmployeeLeave(new CTable("EMPLOYEE"));
            mv.LeaveMonth = DateTime.Now;
            
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
            MEmployeeLeave mv = (MEmployeeLeave)vw;            

            DateTime parentLeaveDate = mv.LeaveMonth;
            var items = mv.LeaveRecords;

            foreach (var item in items)
            {
                if (item.ExtFlag.Equals("D"))
                {
                    continue;
                }

                DateTime leaveDate = item.LeaveDate;
                if ((leaveDate.Month != parentLeaveDate.Month) || (leaveDate.Year != parentLeaveDate.Year))
                {
                    CHelper.ShowErorMessage(leaveDate.ToString(), "ERROR_NOT_IN_SAME_MONTH", null);
                    return false;
                }
            }

            return (true);
        }

        private void CboPoc_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void CmdAdd_Click(object sender, RoutedEventArgs e)
        {
            MEmployeeLeave mv = (MEmployeeLeave)vw;
            mv.IsModified = true;

            MLeaveRecord item = new MLeaveRecord(new CTable(""));
            item.LeaveDate = mv.LeaveMonth;

            mv.AddLeaveRecord(item);
        }

        private void CmdDelete_Click(object sender, RoutedEventArgs e)
        {
            MEmployeeLeave mv = (MEmployeeLeave) vw;

            MLeaveRecord pp = (MLeaveRecord)(sender as Button).Tag;
            mv.RemoveLeaveRecord(pp);
            mv.IsModified = true;
        }

        private void CmdAdd2_Click(object sender, RoutedEventArgs e)
        {
            postValidate();
        }
    }
}
