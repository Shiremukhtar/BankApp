using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppDemo.Shared.Models;

namespace Repository
{
    public interface ITransactionRepo
    {
        Transaction Save(Transaction transaction);
    }
    public class TransactionRepository : ITransactionRepo
    {
        private readonly BankAppDemoContext _context;
        public TransactionRepository(BankAppDemoContext BankAppDataContext)
        {
            _context = BankAppDataContext;
        }



        public Transaction Save(Transaction transaction)
        {

            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return transaction;
        }
    }
}
