﻿namespace GHSpar.Models.Db
{
    public class SmsModel
    {
        public string Msisdn { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}