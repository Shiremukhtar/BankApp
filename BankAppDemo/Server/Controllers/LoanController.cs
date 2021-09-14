using BankAppDemo.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepo _loanRepository;
        private readonly ITransactionRepo _transactionRepository;
        private readonly ICustomerRepo _customerRepository;
        public LoanController(ILoanRepo loanRepository, ICustomerRepo customerRepository,
            ITransactionRepo transactionRepository)
        {
            _loanRepository = loanRepository;
            _transactionRepository = transactionRepository;
            _customerRepository = customerRepository;
        }


        [HttpPost("addloan/{CustomerId:int}")]
        public IActionResult AddLoan(int CustomerId, [FromBody] Loan loan)
        {
            var dbLoan = _loanRepository.Save(loan);

            var tran = Transaction.CreateTransaction(loan.AccountId, loan.Amount, "Insättning banklån " + dbLoan.LoanId.ToString(), "Deposit");

            _transactionRepository.Save(tran);

            var account = _customerRepository.GetAccountById(loan.AccountId);
            account.SetBalance(loan.Amount);

            _customerRepository.SaveAccount(account);
            return Ok(loan);
        }
    }
}
