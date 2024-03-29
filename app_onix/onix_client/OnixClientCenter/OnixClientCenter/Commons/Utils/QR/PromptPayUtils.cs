﻿using System;
using System.Linq;
using System.Text;

namespace Onix.ClientCenter.Commons.Utils.QR
{
    public static class PromptPayUtils
    {
        private static string len(string str)
        {
            return str.Length.ToString().PadLeft(2, '0');
        }

        public static string QRCodePayload(string promptpayId, string amount)
        {
            if (amount.Length > 0) amount = string.Format("{0:##.00}", Convert.ToDouble(amount));

            var POI_METHOD = amount.Length > 0 ? EMVcoConstants.POI_METHOD_DYNAMIC : EMVcoConstants.POI_METHOD_STATIC;

            var TARGET_TYPE = promptpayId.Length >= 13
                ? EMVcoConstants.BOT_ID_MERCHANT_TAX_ID
                : EMVcoConstants.BOT_ID_MERCHANT_PHONE_NUMBER;

            promptpayId = TARGET_TYPE == EMVcoConstants.BOT_ID_MERCHANT_PHONE_NUMBER
                ? $"0066{promptpayId.Substring(1)}"
                : promptpayId;

            var builder = new StringBuilder();

            builder.Append(EMVcoConstants.ID_PAYLOAD_FORMAT)
                .Append(len(EMVcoConstants.PAYLOAD_FORMAT_EMV_QRCPS_MERCHANT_PRESENTED_MODE))
                .Append(EMVcoConstants.PAYLOAD_FORMAT_EMV_QRCPS_MERCHANT_PRESENTED_MODE)
                .Append(EMVcoConstants.ID_POI_METHOD)
                .Append(len(POI_METHOD))
                .Append(POI_METHOD)
                .Append(EMVcoConstants.ID_MERCHANT_INFORMATION_BOT)
                .Append("37")
                .Append(EMVcoConstants.MERCHANT_INFORMATION_TEMPLATE_ID_GUID)
                .Append(len(EMVcoConstants.GUID_PROMPTPAY))
                .Append(EMVcoConstants.GUID_PROMPTPAY)
                .Append(TARGET_TYPE)
                .Append(len(promptpayId))
                .Append(promptpayId)
                .Append(EMVcoConstants.ID_COUNTRY_CODE)
                .Append(len(EMVcoConstants.COUNTRY_CODE_TH))
                .Append(EMVcoConstants.COUNTRY_CODE_TH)
                .Append(EMVcoConstants.ID_TRANSACTION_CURRENCY)
                .Append(len(EMVcoConstants.TRANSACTION_CURRENCY_THB))
                .Append(EMVcoConstants.TRANSACTION_CURRENCY_THB)
                .Append(EMVcoConstants.ID_TRANSACTION_AMOUNT)
                .Append(len(amount))
                .Append(amount)
                .Append(EMVcoConstants.ID_CRC)
                .Append("04");

            var payload = builder.ToString().Select(x => Convert.ToByte(x)).ToArray();
            var crc = new CRC16().ComputeCheckSum(payload);

            builder.Append(crc.ToString("X").PadLeft(4, '0'));
            return builder.ToString();
        }
    }
}
