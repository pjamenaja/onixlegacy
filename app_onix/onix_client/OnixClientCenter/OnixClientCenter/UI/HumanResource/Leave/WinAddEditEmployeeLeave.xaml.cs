using System;
using System.Windows;
using Wis.WsClientAPI;
using Onix.Client.Model;
using Onix.Client.Helper;
using Onix.ClientCenter.Commons.Windows;
using System.Windows.Controls;

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
            
            mv.CreateDefaultValue();
            mv.AddLeaveRecord(new MLeaveRecord(new CTable("")));
            
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

        private void CboPoc_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void CmdAdd_Click(object sender, RoutedEventArgs e)
        {
            MLeaveRecord item = new MLeaveRecord(new CTable(""));
            item.LeaveDate = DateTime.Now;

            (vw as MEmployeeLeave).AddLeaveRecord(item);
        }

        private void CmdDelete_Click(object sender, RoutedEventArgs e)
        {
            MEmployeeLeave mv = (MEmployeeLeave) vw;

            MLeaveRecord pp = (MLeaveRecord)(sender as Button).Tag;
            mv.RemoveLeaveRecord(pp);
            mv.IsModified = true;
        }
    }
}
