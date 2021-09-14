using BankAppDemo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ILoanRepo
    {

        Loan Save(Loan loan);
    }
    public class LoanRepository : ILoanRepo
    {

        private readonly BankAppDemoContext _context;
        public LoanRepository(BankAppDemoContext BankAppDataContext)
        {
            _context = BankAppDataContext;
        }

        public Loan Save(Loan loan)
        {
            var newLone = Loan.CreateNewLoan(loan);
            try
            {
                _context.Loans.Add(newLone);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newLone;
        }
    }
}
