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
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeRepo _accountTypeRepository;
        public AccountTypeController(IAccountTypeRepo IAccountRepository)
        {
            _accountTypeRepository = IAccountRepository;
        }


        [HttpGet("getaccounttypes")]
        public IActionResult GetAccountTypes()
        {
            var accountypes = _accountTypeRepository.GetAccountTypes();
            return Ok(accountypes);
        }


        [HttpPost("addaccounttype")]
        public IActionResult AddAccountType([FromBody] AccountType accounttype)
        {
            var newAccount = AccountType.Add(accounttype);

            var dbAccountType = _accountTypeRepository.CreatAccountType(newAccount);

            return Ok(dbAccountType);
        }
    }
}
