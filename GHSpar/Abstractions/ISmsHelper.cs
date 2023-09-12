using GHSpar.Models;

namespace GHSpar.Abstractions
{
    public interface ISmsHelper
    {
        Task<SmsModel> SendSms(SmsModel smsModel);
    }
}