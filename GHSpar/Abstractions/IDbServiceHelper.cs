using GHSpar.Models;
using GHSpar.Models.Db;

namespace GHSpar.Abstractions
{
    public interface IDbServiceHelper
    {
        Task<PurchaseRequest> CoinPurchaseRequest(PurchaseRequest playerAccount);
        Task<OTPModel> RecordOtp(int otp, string msisdn);
        Task<PlayerAccount> CreateAccount(PlayerAccount playerAccount);
        Task<PlayerAccount> GetAccountByMsisdn(string msisdn);
        Task<PlayerAccount> GetAccountByUsername(string username);
        Task<PlayerAccount> GetAccountByUsernameAndMsisdn(string username, string msisdn);
        Task<PlayerAccount> GetAccountByUsernameAndPin(string username, string pin);
        Task<PlayerAccount> GetAccountByUsernameMsisdnPin(string username, string msisdn, string pin);
        Task<List<PlayerHistory>> GetAllPlayerHistoryByPlayer(string username, string msisdn);
        Task<OTPModel> GetOtp(string otp, string msisdn);
        Task<PlayerAccount> LockAccount(PlayerAccount playerAccount);
        Task<CardPlay> RecordCardPlayed(CardPlay round);
        Task<PlayingRound> RecordMatchWinner(PlayingRound round);
        Task<bool> RecordPlacedBet(long playerid, long matchid, decimal amount);
        Task<PlayingRound> RecordRoundWinner(PlayingRound round);
        Task<PlayerAccount> UpdateAccount(PlayerAccount? player);
        Task<PlayerHistory> UpdatePlayerHistory(PlayerHistory playerHistory);
        Task<TransactionHistory> UpdateTransactionHistory(TransactionHistory transactionHistory);
        Task<List<GameMatchData>> CreateMatch(long playerId, string playerName, DateTime dateCreated, DateTime dateJoined, int requiredPlayers, decimal amountGHS);
        Task<GameMatchDetail> GetMatchDetailByPlayer(long playerId);
        Task<GameMatch> GetMatchDetailByMatch(long matchId);
        Task<List<PlayerHand>> RecordCardsAssigned(List<PlayerHand> players);
    }
}