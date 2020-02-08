using System;
using System.Windows;
using Wis.WsClientAPI;
using Onix.Client.Model;
using Onix.Client.Helper;
using Onix.ClientCenter.Commons.Windows;
using Onix.ClientCenter.Windows;
using Onix.Client.Controller;
using System.IO;
using Onix.ClientCenter.Commons.Utils;

namespace Onix.ClientCenter.UI.HumanResource.EmployeeInfo
{
    public partial class WinAddEditEmployeeInfo : WinBase
    {
        public WinAddEditEmployeeInfo(CWinLoadParam param) : base(param)
        {               
            accessRightName = "HR_EMPLOYEE_EDIT";

            createAPIName = "CreateEmployee";
            updateAPIName = "UpdateEmployee";
            getInfoAPIName = "GetEmployeeInfo";

            InitializeComponent();

            //Need to be after InitializeComponent
            registerValidateControls(lblEmployeeCode, txtEmployeeCode, false);
            registerValidateControls(lblFingerPrintCode, txtFingerPrintCode, false);
            registerValidateControls(lblNameTh, txtNameTh, false);
            registerValidateControls(lblLastNameTh, txtLastNameTh, false);
            registerValidateControls(lblNameEn, txtNameEn, false);
            registerValidateControls(lblLastNameEn, txtLastNameEn, false);
            registerValidateControls(lblEmail, txtEmail, false);
            registerValidateControls(lblPhone, txtPhone, false);
            registerValidateControls(lblLineID, txtLineID, false);            
        }

        protected override MBaseModel createObject()
        {
            MEmployee mv = new MEmployee(new CTable("EMPLOYEE"));

            mv.CreateDefaultValue();
            mv.Category = "1";
            mv.IsMonthly = true;
            mv.IsMale = true;
            mv.HasResignedFlag = false;

            if (loadParam.Mode.Equals("A"))
            {
                mv.HiringDate = DateTime.Now;
            }
            
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

        private MEmployeeLeave GetEmployeeLeaveInfo()
        {
            MEmployee emp = (MEmployee) vw;            

            CTable o = OnixWebServiceAPI.SubmitObjectAPI("GetEmployeeLeaveInfo", emp.GetDbObject());
            MEmployeeLeave el = new MEmployeeLeave(o);
            el.InitializeAfterLoaded();

            el.DepartmentName = Path.GetFileName(emp.DepartmentObj.Description);
            el.PositionName = Path.GetFileName(emp.PositionObj.Description);

            return el;
        }

        private void CmdLeave_Click(object sender, RoutedEventArgs e)
        {
            MEmployeeLeave mv = GetEmployeeLeaveInfo();
            WinFormPrinting w = new WinFormPrinting("grpHRLeave", mv);
            w.ShowDialog();
        }

        private void cmdTaxReport_Click(object sender, RoutedEventArgs e)
        {
            bool result = CHelper.ValidateTextBox(lblEmployeeTax, txtTaxYear, false);
            if (!result)
            {
                return;
            }

            MEmployee emp = vw as MEmployee;

            emp.EmployeeTax.TaxAmount = "145.67";
            emp.EmployeeTax.TaxYear = "2020";
            emp.EmployeeTax.RevenueAmount = "6789.12";
            emp.EmployeeTax.SocialInsuranceAmount = "69.69";

            WinFormPrinting w = new WinFormPrinting("grpHRTax", emp);
            w.ShowDialog();
        }
    }
}
