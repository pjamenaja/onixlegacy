using System;
using Onix.Client.Model;
using Onix.Client.Report;
using Onix.ClientCenter.UI.HumanResource.PayrollDocument;

namespace Onix.ClientCenter.Forms.AcDesign.HREmployeeLeave
{
    public partial class UFormEmployeeLeave : UFormBase
    {
        private MEmployeeLeave leaveDoc = null;

        public UFormEmployeeLeave(MBaseModel model, int page, int totalPage, MReportConfig cfg, CReportPageParam param)
        {
            if (model == null)
            {
                model = new MEmployeeLeave(new Wis.WsClientAPI.CTable(""));
            }

            dataSource = model;
            leaveDoc = (MEmployeeLeave) model;

            pageNo = page;
            pageCount = totalPage;
            pageParam = param;
            rptConfig = cfg;

            init();

            int idx = pageNo - 1;

            //item = payrollDoc.LeaveRecords(idx);
            //if (item == null)
            //{
            //    item = new MLeaveRecord(new Wis.WsClientAPI.CTable(""));
            //}

            //item.InitializeAfterLoaded();
            PopulateDummyRecords(leaveDoc);

            DataContext = leaveDoc;
            InitializeComponent();
        }

        private void PopulateDummyRecords(MEmployeeLeave leaveDoc)
        {
            leaveDoc.EmployeeCode = "dt74696";
            leaveDoc.EmployeeName = "สืบพงษ์";
            leaveDoc.EmployeeLastName = "มนต์สา";
            leaveDoc.DepartmentName = "แผนกไอที";
            leaveDoc.PositionName = "โปรแกรมเมอร์";
            leaveDoc.LeaveYear = "2019";
            leaveDoc.Salary = "20000";

            for (int i = 1; i <= 12; i++)
            {
                MLeaveRecord lr = new MLeaveRecord(new Wis.WsClientAPI.CTable(""));
                lr.Late = "100";
                lr.LeaveMonth = i.ToString();
                leaveDoc.AddLeaveRecord(lr);
            }
        }
    }
}
