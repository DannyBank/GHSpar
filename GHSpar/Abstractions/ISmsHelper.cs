using GHSpar.Models.Db;

namespace GHSpar.Abstractions
{
    public interface ISmsHelper
    {
        Task<SmsModel> SendSms(SmsModel smsModel);
    }
}