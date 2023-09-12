using GHSpar.Abstractions;
using GHSpar.Models;

namespace GHSpar.Services
{
    public class SmsHelper : ISmsHelper
    {
        public async Task<SmsModel> SendSms(OTPModel otpResult)
        {
            return await Task.FromResult(new SmsModel());
        }

        public Task<SmsModel> SendSms(SmsModel smsModel)
        {
            throw new NotImplementedException();
        }
    }
}
