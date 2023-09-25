using GHSpar.Abstractions;
using GHSpar.Models.Db;

namespace GHSpar.Services
{
    public class SmsHelper : ISmsHelper
    {
        private IDbServiceHelper _dbServiceHelper;

        public SmsHelper(IDbServiceHelper dbServiceHelper)
        {
            _dbServiceHelper = dbServiceHelper;
        }

        public async Task<SmsModel?> Queue(string msisdn, string message, string origin)
        {
            return await _dbServiceHelper.Queue(new SmsModel
            {
                Origin = origin,
                Message = message,
                Msisdn = msisdn
            });
        }

        public async Task<SmsModel?> Queue(SmsModel input)
        {
            if (input == null) return null;
            return await _dbServiceHelper.Queue(input);
        }

        public async Task<SmsModel?> Schedule(SmsModel input)
        {
            if (input == null) return null;
            return await _dbServiceHelper.Schedule(input);
        }
    }
}
