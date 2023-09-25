using Dapper;
using GHSpar.Abstractions;
using GHSpar.Models;
using GHSpar.Models.Db;
using System.Data;

namespace GHSpar.Services
{
    public class DbServiceHelper : IDbServiceHelper
    {
        private readonly DbHelper _dbHelper;
        private readonly ILogger<DbServiceHelper> _logger;

        public DbServiceHelper(DbHelper helper, ILogger<DbServiceHelper> logger)
        {
            _dbHelper = helper;
            _logger = logger;
        }

        public async Task<PurchaseRequest> CoinPurchaseRequest(PurchaseRequest playerAccount)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<PurchaseRequest>("createpurchaserequest",
                new {
                    playerid = playerAccount.PlayerId,
                    playername = playerAccount.PlayerName,
                    sparcoins = playerAccount.AmountCoins,
                    amountghs = playerAccount.AmountGHS,
                    amountusd = playerAccount.AmountUSD,
                    purchasedate = playerAccount.PurchaseDate
                }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<OTPModel> RecordOtp(int otp, string msisdn)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> CreateAccount(PlayerAccount playerAccount)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<PlayerAccount>("createplayer",
                new
                {
                    username = playerAccount.Username,
                    msisdn = playerAccount.Msisdn,
                    pin = playerAccount.Pin,
                    status = playerAccount.Status.ToString(),
                    datejoined = DateTime.Now
                }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<PlayerAccount> GetAccountByMsisdn(string msisdn)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> GetAccountByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> GetAccountByUsernameAndPin(string username, string pin)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<PlayerAccount>("getplayerbynamepin",
                new { username, pin }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<PlayerAccount> GetAccountByUsernameAndMsisdn(string username, string msisdn)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PlayerHistory>> GetAllPlayerHistoryByPlayer(string username, string msisdn)
        {
            throw new NotImplementedException();
        }

        public async Task<OTPModel> GetOtp(string otp, string msisdn)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> LockAccount(PlayerAccount playerAccount)
        {
            throw new NotImplementedException();
        }

        public async Task<CardPlay> RecordCardPlayed(CardPlay round)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<CardPlay>("createcardplay",
            new
            {
                playerid = round.PlayerId,
                playername = round.PlayerName,
                matchid = round.MatchId,
                round = round.Round,
                suit = round.Suit,
                rank = round.Rank,
                dateplayed = round.DatePlayed
                }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<PlayingRound> RecordMatchWinner(PlayingRound round)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<PlayingRound>("recordplayinground",
            new
            {
                round = round.Round,
                matchid = round.MatchId,
                daterecorded = DateTime.Now,
                winner = round.Winner
            }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<PlayingRound> RecordRoundWinner(PlayingRound round)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<PlayingRound>("recordplayinground",
            new
            {
                round = round.Round,
                matchid = round.MatchId,
                daterecorded = DateTime.Now,
                winner = round.Winner
            }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public Task<bool> RecordPlacedBet(long playerid, long matchid, decimal amount)
        {
            //deduct player's coin
            //record deduction in player history
            //record addition in game history
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> UpdateAccount(PlayerAccount? player)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerHistory> UpdatePlayerHistory(PlayerHistory playerHistory)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionHistory> UpdateTransactionHistory(TransactionHistory transactionHistory)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> GetAccountByUsernameMsisdnPin(string username, string msisdn, string pin)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<PlayerAccount>("getplayer",
                new { username, msisdn, pin }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<List<GameMatchData>> CreateMatch(
            long playerId, string playerName, DateTime dateCreated, DateTime dateJoined, int requiredPlayers, decimal amountGHS)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryAsync<GameMatchData>("creategamematch",
                new
                {
                    playerid = playerId,
                    playername = playerName,
                    datecreated = dateCreated,
                    datejoined = dateJoined,
                    requiredplayers = requiredPlayers,
                    amount = amountGHS
                }, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<GameMatchDetail> GetMatchDetailByPlayer(long playerId)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<GameMatchDetail>(
                "getgamematchdetailsbyplayer", new { playerid = playerId }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<GameMatch> GetMatchDetailByMatch(long matchId)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<GameMatch>(
                "getgamematchdetails", new { matchid = matchId }, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<List<PlayerHand>> RecordCardsAssigned(List<PlayerHand> playerHands)
        {
            using var connection = _dbHelper.CreateConnection();
            var playerData = new List<PlayerHand>();
            foreach (var ph in playerHands)
            {
                foreach (var card in ph.Hands)
                {
                    var data = await connection.QueryFirstOrDefaultAsync<PlayerHand>("recordcardsassigned",
                        new
                        {
                            matchid = ph.MatchId,
                            playerid = ph.PlayerId,
                            suit = card.Suit,
                            rank = card.Rank
                        }, commandType: CommandType.StoredProcedure);
                    playerData.Add(data);
                }
            }
            return playerData;
        }

        public async Task<SmsModel> Queue(SmsModel input)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<SmsModel>(
                "queuesms", new { msisdn = input.Msisdn, message = input.Message, origin = input.Origin }, 
                commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<SmsModel> Schedule(SmsModel input)
        {
            using var connection = _dbHelper.CreateConnection();
            var data = await connection.QueryFirstOrDefaultAsync<SmsModel>(
                "schedulesms", new { msisdn = input.Msisdn, message = input.Message, origin = input.Origin }, 
                commandType: CommandType.StoredProcedure);
            return data;
        }
    }
}
