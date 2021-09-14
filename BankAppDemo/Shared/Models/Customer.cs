using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable
namespace BankAppDemo.Shared.Models
{
    public class TransactionDto
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }

        public int AccountId { get; set; }

        public int? CustomerAccountId { get; set; }

        public string Error { get; set; }

        public bool Succeed { get; set; }

    }

    public class AccountDto
    {
        public string AccountTypeName { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public bool ShowTransaction { get; set; }

        public List<TransactionDto> Transactions { get; set; }

    }
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new List<Account>();
        }

        public int CustomerId { get; set; }
        public string? Gender { get; set; }
        public string? Givenname { get; set; }
        public string? Surname { get; set; }
        public string? Streetaddress { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Telephonecountrycode { get; set; }
        public string? Telephonenumber { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCustomer { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<Account> Accounts { get; set; }

        [NotMapped]
        public virtual List<AccountDto> AccountsList { get; set; }

        public void SetBalance(decimal amount, int accountId)
        {
            var account = Accounts.Where(x => x.AccountId == accountId).FirstOrDefault();
            account.Balance = account.Balance + amount;
        }

        public Guid IdentityId { get; set; }

        public void UpdateNameOnSignUp(Customer customer)
        {


            Surname = customer.Surname;
            Givenname = customer.Givenname;

        }

        public string Emailaddress { get; set; }

        public static Customer CreatNewCustomer(string emailIdentifier, string iDIdentifier)
        {
            var customer = new Customer();
            customer.Emailaddress = emailIdentifier;
            customer.IdentityId = Guid.Parse(iDIdentifier);
            return customer;
        }
        public void CreateNewAccount(int accountType)
        {
            Account account = new Account();
            account.AccountTypesId = accountType;
            account.Balance = 0;



            Accounts.Add(account);
        }

        public void ApproveAsCustomer()
        {

            IsCustomer = true;
        }

        public void ApproveAsAdmin()
        {
            IsAdmin = true;
        }


    }
}
