using BankAppDemo.Shared;
using BankAppDemo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICustomerRepo
    {
        Customer UpdateNameOnSignUp(Customer customer, Guid IdentityId);
        Customer Save(Customer customer);

        Account SaveAccount(Account account);
        void CreatCustomer(string emailIdentifier, string iDIdentifier);
        Customer GetById(int id, bool ignore = true);

        Customer GetByAccountId(int id);
        Customer GetById(Guid id);

        Customer GetByEmailIdentifier(string emailIdentifier);

        List<Customer> GetUnregistered();
        List<Customer> GetCustomers();

        Account GetAccountById(int accountId);


    }

    public class CustomerRepository : ICustomerRepo
    {
        private readonly BankAppDemoContext _context;
        public CustomerRepository(BankAppDemoContext BankAppDataContext)
        {
            _context = BankAppDataContext;
        }



        public Customer GetByEmailIdentifier(string emailIdentifier)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Emailaddress == emailIdentifier);
            return customer;
        }


        public void CreatCustomer(string emailIdentifier, string iDIdentifier)
        {
            var customer = Customer.CreatNewCustomer(emailIdentifier, iDIdentifier);

            _context.Customers.Add(customer);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public Customer GetByAccountId(int id)
        {
            var account = _context.Accounts.Where(x => x.AccountId == id).FirstOrDefault();

            var customer = _context.Customers.Where(x => x.CustomerId == account.CustomerId).FirstOrDefault();
            return customer;
        }
        public Customer UpdateNameOnSignUp(Customer customer, Guid IdentityId)
        {

            var dbCustomer = GetById(IdentityId);

            dbCustomer.UpdateNameOnSignUp(customer);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dbCustomer;

        }

        public Customer GetById(int id, bool ignore)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.CustomerId == id);

            if (!ignore)
            {
                var accounts = _context.Accounts.Where(x => x.CustomerId == customer.CustomerId).ToList().DeepClone();
                var accountTypes = _context.AccountTypes.ToList();
                var accountsDto = new List<AccountDto>();


                foreach (var item in accounts)
                {
                    var transaction = _context.Transactions.Where(x => x.AccountId == item.AccountId).ToList();

                    var a = new AccountDto();
                    a.AccountId = item.AccountId;
                    a.AccountTypeName = accountTypes.Where(x => x.AccountTypeId == item.AccountTypesId).FirstOrDefault().TypeName;
                    a.Balance = item.Balance;

                    a.Transactions = new List<TransactionDto>();
                    foreach (var tran in transaction)
                    {
                        var dtoTran = new TransactionDto();
                        dtoTran.Amount = tran.Amount;
                        dtoTran.Date = tran.Date;
                        dtoTran.Operation = tran.Operation;
                        dtoTran.Type = tran.Type;


                        a.Transactions.Add(dtoTran);

                    }

                    accountsDto.Add(a);


                }

                customer.AccountsList = accountsDto.DeepClone();
            }

            return customer;
        }


        public Account GetAccountById(int accountId)
        {
            var dbAccount = _context.Accounts.Where(x => x.AccountId == accountId).FirstOrDefault();
            return dbAccount;
        }

        public Customer GetById(Guid id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.IdentityId == id);
            return customer;
        }

        public List<Customer> GetUnregistered()
        {
            var customers = _context.Customers.Where(x =>
            x.IsCustomer == false &&
            x.IsAdmin == false &&
            x.Givenname != null &&
            x.Surname != null).ToList();

            return customers;
        }



        public Account SaveAccount(Account account)
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return account;
        }

        public Customer Save(Customer customer)
        {

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customer;
        }

        public List<Customer> GetCustomers()
        {
            var customers = _context.Customers.Where(x => x.IsCustomer).ToList();
            return customers;
        }
    }
}
