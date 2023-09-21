using GHSpar.Abstractions;
using GHSpar.Models;
using GHSpar.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GHSpar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly ILogger<AccountController> _logger;
        public readonly IDbServiceHelper _dbHelper;
        public readonly ISmsHelper _smsHelper;
        public readonly AppSettings _appSettings;
        public readonly AppStrings _appStrings;

        public AccountController(ILogger<AccountController> logger, IDbServiceHelper dbHelper, ISmsHelper smsHelper, 
            IOptionsSnapshot<AppSettings> appSettings, IOptionsSnapshot<AppStrings> appStrings)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _smsHelper = smsHelper;
            _appSettings = appSettings.Value;
            _appStrings = appStrings.Value;
        }

        [HttpPost("create")]
        public async Task<ApiResponse> CreateAccount([FromBody] PlayerAccount playerAccount)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var result = await _dbHelper.CreateAccount(playerAccount);
            return new ApiResponse(StatusCodes.Status200OK, $"Successfully Created Account for {playerAccount.Username}", result);
        }

        [HttpGet("get/{username}")]
        public async Task<ApiResponse> GetByUsername(string username)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var data = await _dbHelper.GetAccountByUsername(username);
            return new ApiResponse(StatusCodes.Status200OK, $"GetByUsername Successful for {username}", data);
        }

        [HttpGet("get/{msisdn}")]
        public async Task<ApiResponse> GetByMsisdn(string msisdn)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var data = await _dbHelper.GetAccountByMsisdn(msisdn);
            return new ApiResponse(StatusCodes.Status200OK, $"GetByMsisdn Successful for {msisdn}", data);
        }

        [HttpGet("get/{username}/{msisdn}")]
        public async Task<ApiResponse> GetByUsernameMsisdn(string username, string msisdn)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var data = await _dbHelper.GetAccountByUsernameAndMsisdn(username, msisdn);
            return new ApiResponse(StatusCodes.Status200OK, $"GetByUsernameMsisdn Successful for {username}", data);
        }

        [HttpGet("get/{username}/{msisdn}/{pin}")]
        public async Task<ApiResponse> GetByUsernameMsisdnPin(string username, string msisdn, string pin)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var data = await _dbHelper.GetAccountByUsernameMsisdnPin(username, msisdn, pin);
            return new ApiResponse(StatusCodes.Status200OK, $"GetByUsernameMsisdn Successful for {username}", data);
        }

        [HttpPost("lock")]
        public async Task<ApiResponse> LockAccount([FromBody] PlayerAccount playerAccount)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var data = await _dbHelper.LockAccount(playerAccount);
            if (data.Status == PlayerStatus.LOCKED)
                return new ApiResponse(StatusCodes.Status200OK, $"LockAccount Successful for {playerAccount.Username}", data);
            else
                return new ApiResponse(StatusCodes.Status500InternalServerError, $"LockAccount failed for {playerAccount.Username}", null!);
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse> SendOtp(string username, string msisdn)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var data = await _dbHelper.GetAccountByMsisdn(msisdn);
            if (data is not null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "Phone Number already exists", null!);

            int otp = new Random().Next(10000, 99999);
            var otpResult = await _dbHelper.RecordOtp(otp, msisdn);
            if (otpResult is null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "OTP operation failed", null!);

            var sendResult = await _smsHelper.SendSms(
                new SmsModel { Msisdn = msisdn, Message = $"{otpResult.Otp} is you OTP", Origin = _appSettings.Origin});
            return new ApiResponse(StatusCodes.Status200OK, $"SendOtp Successful for {username}", sendResult);
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse> VerifyOtp(string otp, string msisdn)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var player = await _dbHelper.GetAccountByMsisdn(msisdn);
            if (player is not null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "Phone Number already exists", null!);

            var foundOtp = await _dbHelper.GetOtp(otp, msisdn);
            if (foundOtp is null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "OTP was not found", null!);
            if (!foundOtp.Otp.Equals(otp))
                return new ApiResponse(StatusCodes.Status500InternalServerError, "OTP mismatch, please try again", null!);

            if (player?.Status is PlayerStatus.UNCOMPLETED)
                player.Status = PlayerStatus.ACTIVE;

            var playerUpdate = await _dbHelper.UpdateAccount(player);                        
            return new ApiResponse(StatusCodes.Status200OK, $"VerifyOtp Successful for {playerUpdate}", null!);
        }
    }
}
