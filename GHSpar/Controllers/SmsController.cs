using GHSpar.Abstractions;
using GHSpar.Models.Db;
using GHSpar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GHSpar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        public readonly ILogger<AccountController> _logger;
        public readonly ISmsHelper _smsHelper;
        public readonly AppSettings _appSettings;
        public readonly AppStrings _appStrings;

        public SmsController(ILogger<AccountController> logger, ISmsHelper smsHelper, 
            IOptionsSnapshot<AppSettings> appSettings, IOptionsSnapshot<AppStrings> appStrings)
        {
            _logger = logger;
            _smsHelper = smsHelper;
            _appSettings = appSettings.Value;
            _appStrings = appStrings.Value;
        }

        [HttpPost("queue")]
        public async Task<ApiResponse> Queue([FromBody] SmsModel input)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var result = await _smsHelper.Queue(input);
            return new ApiResponse(StatusCodes.Status200OK, $"Successfully Queued Sms for {input.Msisdn}", result);
        }

        [HttpPost("schedule")]
        public async Task<ApiResponse> Schedule([FromBody] SmsModel input)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var result = await _smsHelper.Schedule(input);
            return new ApiResponse(StatusCodes.Status200OK, $"Successfully Scheduled Sms for {input.Msisdn}", result);
        }
    }
}
