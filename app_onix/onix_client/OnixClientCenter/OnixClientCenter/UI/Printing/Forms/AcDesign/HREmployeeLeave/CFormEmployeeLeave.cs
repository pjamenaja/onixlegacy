using System;
using System.Windows;
using System.Collections;
using Onix.Client.Model;
using Onix.Client.Report;
using System.Printing;
using System.Windows.Controls;

namespace Onix.ClientCenter.Forms.AcDesign.HREmployeeLeave
{
    public class CFormEmployeeLeave : CBaseReport
    {
        public CFormEmployeeLeave() : base()
        {
        }

        protected override UserControl createPageObject(Size s, int pageIdx, int pageCount, CReportPageParam param)
        {
            UFormEmployeeLeave page = new UFormEmployeeLeave(dataSource, pageIdx, pageCount, rptCfg, param);

            page.Width = rptCfg.AreaWidthDot;
            page.Height = rptCfg.AreaHeightDot;
            page.Measure(s);

            return (page);
        }

        public override PageOrientation GetPageOrientation()
        {
            return (PageOrientation.Portrait);
        }

        protected override ArrayList createPageParam()
        {
            MEmployeeLeave ad = (MEmployeeLeave)dataSource;

            ArrayList arr = createPageParamEasy(ad.LeaveRecords, 1);
            return (arr);
        }

        protected override void initPageCreateFlow()
        {
            IsPageRangeSupport = true;
        }
        
        public override MReportConfig CreateDefaultConfigValues()
        {
            MReportConfig rc = new MReportConfig(new Wis.WsClientAPI.CTable(""));

            rc.SetConfigValue("Column1Width", "10", "String", "Width");
            rc.SetConfigValue("Column2Width", "10", "String", "Width");
            rc.SetConfigValue("Column3Width", "10", "String", "Width");
            rc.SetConfigValue("Column4Width", "10", "String", "Width");
            rc.SetConfigValue("Column5Width", "10", "String", "Width");
            rc.SetConfigValue("Column6Width", "10", "String", "Width");
            rc.SetConfigValue("Column7Width", "10", "String", "Width");
            rc.SetConfigValue("Column8Width", "10", "String", "Width");

            populateDefaultReportConfig(rc);

            return (rc);
        }
    }
}
