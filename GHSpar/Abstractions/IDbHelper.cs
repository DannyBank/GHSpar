using GHSpar.Models;

namespace GHSpar.Abstractions
{
    public interface IDbHelper
    {
        Task<PurchaseRequest> AddCoinPurchaseRequest(PurchaseRequest playerAccount);
        Task<OTPModel> AddOtp(int otp, string msisdn);
        Task<PlayerAccount> CreateAccount(PlayerAccount playerAccount);
        Task<PlayerAccount> GetAccountByMsisdn(string msisdn);
        Task<PlayerAccount> GetAccountByUsername(string username);
        Task<PlayerAccount> GetAccountByUsernameAndMsisdn(string username, string msisdn);
        Task<List<PlayerHistory>> GetAllPlayerHistoryByPlayer(string username, string msisdn);
        Task<OTPModel> GetOtp(string otp, string msisdn);
        Task<PlayerAccount> LockAccount(PlayerAccount playerAccount);
        Task<CardPlay> RecordCardPlayed(CardPlay round);
        Task<PlayingRound> RecordMatchWinner(PlayingRound round);
        Task<bool> RecordPlaceBet(long playerid, long matchid, decimal amount);
        Task<PlayingRound> RecordRoundWinner(PlayingRound round);
        Task<PlayerAccount> UpdateAccount(PlayerAccount? player);
        Task<PlayerHistory> UpdatePlayerHistory(PlayerHistory playerHistory);
        Task<TransactionHistory> UpdateTransactionHistory(TransactionHistory transactionHistory);
    }
}