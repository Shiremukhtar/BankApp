using BankAppDemo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountTypeRepo
    {
        List<AccountType> GetAccountTypes();


        AccountType CreatAccountType(AccountType type);
    }
    public class AccountRepository : IAccountTypeRepo
    {
        private readonly BankAppDemoContext _context;

        public AccountRepository(BankAppDemoContext bankAppDataContext)
        {
            _context = bankAppDataContext;
        }

        public List<AccountType> GetAccountTypes()
        {
            return _context.AccountTypes.ToList();

        }


        public AccountType CreatAccountType(AccountType type)
        {

            _context.AccountTypes.Add(type);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return type;

        }
    }
}
