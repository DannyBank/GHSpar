using GHSpar.Abstractions;
using GHSpar.Models;
using GHSpar.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GHSpar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public readonly ILogger<GameController> _logger;
        public readonly IDbServiceHelper _dbHelper;
        public readonly ISmsHelper _smsHelper;
        public readonly AppSettings _appSettings;
        public readonly AppStrings _appStrings;
        public readonly IGameService _gameService;

        public GameController(ILogger<GameController> logger, IDbServiceHelper dbHelper, ISmsHelper smsHelper,
            IOptionsSnapshot<AppSettings> appSettings, IOptionsSnapshot<AppStrings> appStrings, IGameService gameService)
        {
            _logger = logger;
            _dbHelper = dbHelper;
            _smsHelper = smsHelper;
            _appSettings = appSettings.Value;
            _appStrings = appStrings.Value;
            _gameService = gameService;
        }

        [HttpGet("create/match/{players}/{playerid}")]
        public async Task<ApiResponse> CreateMatch(int players, long playerid)
        {
            if (!ModelState.IsValid)
                    return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var game = new GameMatch
            {
                Active = true,
                CurrentPlayers = 0,
                DateCreated = DateTime.Now,
                RequiredPlayers = players
            };
            var gameDeets = new GameMatchDetail
            {
                Amount = 0,
                DateJoined = DateTime.Now,
                PlayerId = playerid,
                PlayerName = ""
            };
            var generatedMatch = await _gameService.CreateMatch(game, gameDeets);
            return new ApiResponse(StatusCodes.Status200OK, "Game Created/Joined", generatedMatch);
        }

        [HttpGet("match/{matchid}")]
        public async Task<ApiResponse> GetMatch(long matchid)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var match = await _gameService.GetMatch(matchid);
            return new ApiResponse(StatusCodes.Status200OK, $"Game {matchid} found", match);
        }

        [HttpGet("check/play/{reqcoins}/{username}/{msisdn}")]
        public async Task<ApiResponse> CheckPlayerForGame(string reqcoins, string username, string msisdn)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);
            if (string.IsNullOrEmpty(reqcoins))
                return new ApiResponse(StatusCodes.Status400BadRequest, "Reqcoins is required", null!);
            if (string.IsNullOrEmpty(username))
                return new ApiResponse(StatusCodes.Status400BadRequest, "username is required", null!);
            if (string.IsNullOrEmpty(msisdn))
                return new ApiResponse(StatusCodes.Status400BadRequest, "msisdn is required", null!);

            var playerHistory = await _dbHelper.GetAllPlayerHistoryByPlayer(username, msisdn);
            if (playerHistory is null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "Player Account Not Found", null!);

            var currentPlayerCoins = playerHistory.OrderByDescending(r => r.EntryDate).FirstOrDefault()?.SparCoin ?? 0;
            if (decimal.Parse(reqcoins) > currentPlayerCoins)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "Player coins are insufficient for game", null!);
            else
                return new ApiResponse(StatusCodes.Status200OK, $"Player {username}:{msisdn} coins are sufficient for game", null!);
        }

        [HttpPost("play/card")]
        public async Task<ApiResponse> PlayCard([FromBody] CardPlay round)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var playHistory = await _dbHelper.RecordCardPlayed(round);
            if (playHistory is null || playHistory.DatePlayed.Hour != DateTime.Now.Hour)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "An error occurred", null!);
            return new ApiResponse(StatusCodes.Status200OK, "Player card play recorded successfully", playHistory);
        }

        [HttpPost("record/winner/round")]
        public async Task<ApiResponse> RecordRoundWinner([FromBody] PlayingRound round)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var result = await _dbHelper.RecordRoundWinner(round);
            if (result is null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "An error occurred", null!);
            return new ApiResponse(StatusCodes.Status200OK, "Player round win recorded successfully", result);
        }

        [HttpPost("record/winner/match")]
        public async Task<ApiResponse> RecordMatchWinner([FromBody] PlayingRound round)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            if (round.Round != 5)
                return new ApiResponse(StatusCodes.Status500InternalServerError, $"Invalid round {round.Round} to declare winner", null!);
            var result = await _dbHelper.RecordMatchWinner(round);
            if (result is null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "An error occurred", null!);
            return new ApiResponse(StatusCodes.Status200OK, "Player round win recorded successfully", result);
        }

        [HttpGet("place/bet/{playerid}/{matchid}/{amount}")]
        public async Task<ApiResponse> PlaceBet(long playerid, long matchid, decimal amount)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var result = await _dbHelper.RecordPlacedBet(playerid, matchid, amount);
            if (!result)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "An error occurred", null!);
            return new ApiResponse(StatusCodes.Status200OK, "Player round win recorded successfully", result);
        }

        [HttpPost("record/player/cards")]
        public async Task<ApiResponse> RecordPlayersCards([FromBody] List<PlayerHand> players)
        {
            if (!ModelState.IsValid)
                return new ApiResponse(StatusCodes.Status400BadRequest, "Bad Request", null!);

            var result = await _dbHelper.RecordCardsAssigned(players);
            if (result is null)
                return new ApiResponse(StatusCodes.Status500InternalServerError, "An error occurred", null!);
            return new ApiResponse(StatusCodes.Status200OK, "Player round win recorded successfully", result);
        }
    }
}
