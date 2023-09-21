using GHSpar.Abstractions;
using GHSpar.Models;
using GHSpar.Models.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GHSpar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        public readonly ILogger<PurchaseController> _logger;
        public readonly IDbServiceHelper _dbHelper;
        public readonly ISmsHelper _smsHelper;
        public readonly IMomoHelper _momoHelper;
        public readonly AppSettings _appSettings;
        public readonly AppStrings _appStrings;

        public PurchaseController(ILogger<PurchaseController> logger, IDbServiceHelper dbHelper, 
            ISmsHelper smsHelper, IMomoHelper momoHelper,IOptionsSnapshot<AppSettings> appSettings, IOptionsSnapshot<AppStrings> appStrings)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _smsHelper = smsHelper;
            _momoHelper = momoHelper;
            _appSettings = appSettings.Value;
            _appStrings = appStrings.Value;
        }

        [HttpPost("coin")]
        public async Task<ApiResponse> BuyCoin([FromBody] PurchaseRequest purchaseRequest)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var result = await _dbHelper.CoinPurchaseRequest(purchaseRequest);
            if (result is null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "Purchase Request Failed", null!);

            var purchaseResult = await _momoHelper.SendPurchaseRequest(purchaseRequest);
            if (purchaseResult is null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "Purchase Request Failed", null!);

            if (purchaseResult.Success)
                return new ApiResponse(StatusCodes.Status200OK, $"Successfully Initiated purchase request of {purchaseRequest.AmountCoins} Coins for {purchaseRequest.PlayerName}", result);
            else
                return new ApiResponse(StatusCodes.Status200OK, $"Purchased Failed for {purchaseRequest.PlayerName}", result);
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse> Callback(string msisdn, bool success, decimal amountGhs, string transactionId)
        {
            if (!success)
            {
                _logger.LogError("Purchase Failed for {msisdn} for {amount}", msisdn, amountGhs);
                return new ApiResponse(StatusCodes.Status200OK, $"Request Acknowledged", null!);
            }

            var player = await _dbHelper.GetAccountByMsisdn(msisdn);
            var playerHistory = await _dbHelper.UpdatePlayerHistory(new PlayerHistory
            {
                EntryDate = DateTime.Now,
                PlayerId = player.Id,
                PlayerName = player.Username,
                SparCoin = amountGhs/_appSettings.CoinRate
            });
            if (playerHistory is null)
            {
                _logger.LogError("Purchase for {msisdn} for {amount} was Successful but Update Failed", msisdn, amountGhs);
                return new ApiResponse(StatusCodes.Status200OK, $"Request Acknowledged", null!);
            }

            var transHistory = await _dbHelper.UpdateTransactionHistory(new TransactionHistory
            {
                Amount = amountGhs,
                EntryDate = DateTime.Now,
                Fee = 0,
                PlayerId = player.Id,
                PlayerName = player.Username,
                TransactionDate = DateTime.Now,
                TransactionId = transactionId
            });
            if (transHistory is null)
            {
                _logger.LogError("Purchase for {msisdn} for {amount} was Successful but Update Failed", msisdn, amountGhs);
                return new ApiResponse(StatusCodes.Status200OK, $"Request Acknowledged", null!);
            }

            return new ApiResponse(StatusCodes.Status200OK, $"Request Acknowledged", null!);
        }
    }
}
