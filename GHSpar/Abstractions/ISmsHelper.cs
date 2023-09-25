using GHSpar.Models.Db;

namespace GHSpar.Abstractions
{
    public interface ISmsHelper
    {
        Task<SmsModel> Queue(SmsModel input);
        Task<SmsModel?> Queue(string msisdn, string message, string origin);
        Task<SmsModel> Schedule(SmsModel input);
    }
}