﻿using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using Wis.WsClientAPI;
using Onix.Client.Model;
using Onix.Client.Helper;
using Onix.ClientCenter.Commons.Utils;

namespace Onix.ClientCenter
{
    public partial class WinAddEditExportItem : Window
    {
        private MInventoryTransaction vw = null;
        private MInventoryTransaction actualView = null;
        private MInventoryDoc parentView = null;

        private ObservableCollection<MInventoryTransaction> parentItemsSource = null;
        private MLocation locationObj = null;
        private DateTime documentDate = DateTime.Now;

        private InventoryDocumentType DocType = InventoryDocumentType.InvDocExport;

        public String Caption = "";
        public String Mode = "";
        public Boolean HasModified = false;

        public WinAddEditExportItem(InventoryDocumentType dt)
        {
            DocType = dt;

            vw = new MInventoryTransaction(new CTable(""));
            vw.LocationObj = this.locationObj;
            vw.CreateDefaultValue();

            vw.TxType = "E";
            if (DocType == InventoryDocumentType.InvDocXfer)
            {
                vw.TxType = "X";
            }

            DataContext = vw;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public MLocation LocationObj
        {
            set
            {
                locationObj = value;
            }

            get
            {
                return (locationObj);
            }
        }

        public DateTime DocumentDate
        {
            get
            {
                return (documentDate);
            }

            set
            {
                documentDate = value;
            }
        }

        public MInventoryTransaction ViewData
        {
            set
            {
                actualView = (MInventoryTransaction) value;
            }
        }

        public MInventoryDoc ParentView
        {
            set
            {
                parentView = (MInventoryDoc)value;
            }
        }

        public ObservableCollection<MInventoryTransaction> ParentItemSource
        {
            set
            {
                parentItemsSource = value;
            }
        }

        private Boolean ValidateData()
        {
            Boolean result = false;

            result = CHelper.ValidateLookup(lblCode, lkupItem, false);
            if (!result)
            {
                return (result);
            }

            result = CHelper.ValidateTextBox(lblQuantity, txtQuantity, false, InputDataType.InputTypeZeroPossitiveDecimal);
            if (!result)
            {
                return (result);
            }

            result = CHelper.ValidateTextBox(lblUnitPrice, txtUnitPrice, false);
            if (!result)
            {
                return (result);
            }

            return (result);
        }

        private Boolean SaveToView( )
        {
            if (!ValidateData())
            {
                return(false);
            }

            return (true);
        }

        private Boolean SaveData()
        {
            if (Mode.Equals("A"))
            {
                if (SaveToView())
                {
                    parentView.AddTxItem(vw, DocType);
                    return (true);
                }

                return (false);
            }
            else if (Mode.Equals("E"))
            {
                if (vw.IsModified)
                {
                    Boolean result = SaveToView();
                    if (result)
                    {
                        CTable o = actualView.GetDbObject();
                        o.CopyFrom(vw.GetDbObject());
                        actualView.NotifyAllPropertiesChanged();

                        return (true);
                    }

                    return (false);
                }

                return (true);
            }

            return (true);
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            Boolean r = SaveData();
            if (r)
            {
                HasModified = true;

                vw.IsModified = false;
                CUtil.EnableForm(true, this);

                this.Close();
            }
        }

        private void txtText_TextChanged(object sender, TextChangedEventArgs e)
        {
            vw.IsModified = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (vw.IsModified)
            {
                Boolean result = CHelper.AskConfirmSave();
                if (result)
                {
                    //Yes, save it
                    Boolean r = SaveData();
                    e.Cancel = !r;

                    if (r)
                    {
                        HasModified = true;
                    }
                }
            }
        }

        private void LoadData()
        {
            this.Title = Caption;

            CUtil.EnableForm(false, this);

            if (Mode.Equals("E"))
            {
                CTable newDB = actualView.GetDbObject().Clone();
                vw.SetDbObject(newDB);
                vw.NotifyAllPropertiesChanged();
            }

            vw.IsModified = false;

            CUtil.EnableForm(true, this);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            vw.IsModified = true;
        }

        private void lkupItem_SelectedObjectChanged(object sender, EventArgs e)
        {
            vw.IsModified = true;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CUtil.RegisterScreen(this.GetType().ToString());
        }

        private void txtLotSerial1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxLotSerial_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cboReason_SelectedObjectChanged(object sender, EventArgs e)
        {
            vw.IsModified = true;
        }
    }
}
