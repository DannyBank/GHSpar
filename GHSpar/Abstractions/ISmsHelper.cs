using GHSpar.Models.Db;

namespace GHSpar.Abstractions
{
    public interface ISmsHelper
    {
        Task<SmsModel> Queue(SmsModel input);
        Task<SmsModel> Schedule(SmsModel input);
    }
}