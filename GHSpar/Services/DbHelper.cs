using GHSpar.Abstractions;
using GHSpar.Models;

namespace GHSpar.Services
{
    public class DbHelper : IDbHelper
    {
        public async Task<PurchaseRequest> AddCoinPurchaseRequest(PurchaseRequest playerAccount)
        {
            throw new NotImplementedException();
        }

        public async Task<OTPModel> AddOtp(int otp, string msisdn)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> CreateAccount(PlayerAccount playerAccount)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> GetAccountByMsisdn(string msisdn)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerAccount> GetAccountByUsername(string username)
        {
            throw new NotImplementedException();
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

        public Task<CardPlay> RecordCardPlayed(CardPlay round)
        {
            throw new NotImplementedException();
        }

        public Task<PlayingRound> RecordMatchWinner(PlayingRound round)
        {
            if (round.Id != 5) return null!; // to make sure it's the last round
            throw new NotImplementedException();
        }

        public Task<bool> RecordPlaceBet(long playerid, long matchid, decimal amount)
        {
            //deduct player's coin
            //record deduction in player history
            //record addition in game history
            throw new NotImplementedException();
        }

        public Task<PlayingRound> RecordRoundWinner(PlayingRound round)
        {
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
    }
}
