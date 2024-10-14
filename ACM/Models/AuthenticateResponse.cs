using System;
using System.Collections.Generic;

namespace ACM.Models
{
    public class AuthenticateResponse
    {
        public string access_token { get; set; }
        public long expires_in { get; set; }
        public DateTime expires_on { get; set; }
    }

    public class SingoLogin
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class SingoTokenResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public dataMessage data { get; set; }
    }
    public class SingoVoucherResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public dataMessages data { get; set; }
    }
    public class SingoVoucheMessage
    {
        public string Token { get; set; }
        public string Expirytime { get; set; }
        public string Plan { get; set; }
    }
    public class dataMessages
    {
        public Dictionary<int, string> codes;
    }


    public class dataMessage
    {
        public string Token { get; set; }
        public int Expirytime { get; set; }
        public int batch_template_id { get; set; }
        public int batch_id { get; set; }
    }
    public class VoucherCreationTemplate
    {
        public string token { get; set; }
        public string voucher_template_name { get; set; }
        public string login_policy_name { get; set; }
        public int price { get; set; }
        public int size_of_vouchers { get; set; }
        public int code_type { get; set; }
    }

    public class VoucherBatchCreation
    {
        public string token { get; set; }
        public string voucher_batch_name { get; set; }
        public string login_policy_name { get; set; }
        public int price { get; set; }
        public int voucher_location_ids { get; set; }
    }

    public class VoucherCreation
    {
        public string token { get; set; }
        public string batch_id { get; set; }
        public string activation_date { get; set; }
        public string expiry_date { get; set; }
        public int no_of_voucher { get; set; }
        public int size_of_vouchers { get; set; }
        public int code_type { get; set; }
        public int send_sms { get; set; }
        public string mobile_no { get; set; }
    }
}
