﻿namespace GHSpar.Models
{
    public class PlayerBet
    {
        public long PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}