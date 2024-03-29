﻿using System;
using System.Windows;
using System.Collections;
using Onix.Client.Model;
using Onix.Client.Report;
using Onix.Client.Helper;
using System.Windows.Controls;

namespace Onix.ClientCenter.Forms.AcDesign.Inventory
{
    public class CFormInventoryExportDoc : CBaseReport
    {
        public CFormInventoryExportDoc() : base()
        {
        }

        protected override ArrayList createPageParam()
        {           
            int itemPerPage = CUtil.StringToInt(rptCfg.GetConfigValue("ItemPerPage"));

            ArrayList arr = createPageParamComplex((dataSource as MInventoryDoc).TxItems, itemPerPage);
            return (arr);
        }

        protected override UserControl createPageObject(Size s, int pageIdx, int pageCount, CReportPageParam param)
        {
            UFormInventoryExportDoc page = new UFormInventoryExportDoc(dataSource, pageIdx, pageCount, rptCfg, param);

            page.Width = rptCfg.AreaWidthDot;
            page.Height = rptCfg.AreaHeightDot;
            page.Measure(s);

            return (page);
        }

        protected override void initPageCreateFlow()
        {
            IsPageRangeSupport = true;
        }

        public override MReportConfig CreateDefaultConfigValues()
        {
            MReportConfig rc = new MReportConfig(new Wis.WsClientAPI.CTable(""));

            rc.SetConfigValue("DocumentTypeThai", "ใบเบิกพัสดุ", "String", "Document Type Thai");
            rc.SetConfigValue("DocumentTypeEng", "INVENTORY EXPORT DOCUMENT", "String", "Document Type Eng");

            populateDefaultReportConfig(rc);

            return (rc);
        }
    }
}
