﻿using System;
using System.Collections.ObjectModel;
using Onix.Client.Model;
using System.Windows;
using Onix.Client.Report;

namespace Onix.ClientCenter.Forms.AcDesign.DrCrNote
{
    public partial class UFormDrCrNote : UFormBase
    {
        public UFormDrCrNote(MBaseModel model, int page, int totalPage, MReportConfig cfg, CReportPageParam param)
        {
            if (model == null)
            {
                model = new MAccountDoc(new Wis.WsClientAPI.CTable(""));
            }

            dataSource = model;
            pageNo = page;
            pageCount = totalPage;
            pageParam = param;
            rptConfig = cfg;

            init();

            MAccountDoc ad = (dataSource as MAccountDoc);
            numberTextAmount = ad.CashReceiptAmtFmt;

            primaryColumns.Clear();

            primaryColumns.Add(new GridLength(7, GridUnitType.Star));
            primaryColumns.Add(new GridLength(39, GridUnitType.Star));
            primaryColumns.Add(new GridLength(7, GridUnitType.Star));
            primaryColumns.Add(new GridLength(7, GridUnitType.Star));
            primaryColumns.Add(new GridLength(10, GridUnitType.Star));
            primaryColumns.Add(new GridLength(7, GridUnitType.Star));
            primaryColumns.Add(new GridLength(13, GridUnitType.Star));

            DataContext = model;
            InitializeComponent();

            //These 2 lines are important to place here after InitializeComponent();
            headerPanel = grdBody;
            tablePanel = dckBody;

            descriptionColumnIndex = 1;
        }

        private ObservableCollection<CAccountDocItem> filterItems()
        {
            ObservableCollection<CAccountDocItem> temp = new ObservableCollection<CAccountDocItem>();

            int i = 0;
            foreach (MAccountDocItem m in pageParam.Items)
            {
                CAccountDocItem d = new CAccountDocItem(m, Lang, rptConfig);
                d.ItemNo = (pageParam.StartIndex + i).ToString();
                temp.Add(d);

                i++;
            }

            //int left = itemPerPage - temp.Count;

            for (i = 1; i <= pageParam.PatchRow; i++)
            {
                temp.Add(new CAccountDocItem(null, Lang, rptConfig));
            }

            return (temp);
        }        

        public ObservableCollection<CAccountDocItem> ItemChunks
        {
            get
            {
                return (filterItems());
            }
        }        
    }
}
