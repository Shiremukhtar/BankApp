using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppDemo.Shared.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            Accounts = new HashSet<Account>();
        }

        public int AccountTypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public decimal Interest { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

        public static AccountType Add(AccountType accountType)
        {
            var _accountType = new AccountType();
            _accountType.TypeName = accountType.TypeName;
            _accountType.Description = accountType.Description;
            _accountType.Interest = accountType.Interest;

            return _accountType;

        }

    }
}
