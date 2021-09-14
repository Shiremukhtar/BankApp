﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppDemo.Shared.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }

        public virtual Account AccountNavigation { get; set; }

        public static Transaction CreateTransaction(int accountId, decimal amount, string type, string operation)
        {
            var newtransaction = new Transaction();

            newtransaction.AccountId = accountId;
            newtransaction.Amount = amount;
            newtransaction.Bank = "Banken";
            newtransaction.Balance = 0;
            newtransaction.Date = DateTime.Now;
            newtransaction.Type = type;
            newtransaction.Operation = operation;


            return newtransaction;
        }
    }
}
