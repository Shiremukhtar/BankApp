using BankAppDemo.Shared;
using BankAppDemo.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BankAppDemo.Client.Pages
{
    public class CustomerAccountDetailsBase:ComponentBase
    {
        [Inject]
        public HttpClient _httpClient { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        public Customer Customer { get; set; } = new Customer();

        [Parameter]
        public int CustomerId { get; set; }

        public TransactionDto Deposit { get; set; } = new TransactionDto();
     
        protected override async Task<Task> OnInitializedAsync()
        {
            await GetCustomer();
            return base.OnInitializedAsync();
        }
        public async Task GetCustomer()
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"customer/getcustomer/{CustomerId}");

            Customer = customer;
    

        }
        public void ShowTransactions(AccountDto account)
        {
            if (account.ShowTransaction)
            {
                account.ShowTransaction = false;

            }
            else
            {
                account.ShowTransaction = true;
            }
        }

        public async Task HandleValidSubmitDeposit(int accountId)
        {
            Deposit.AccountId = accountId;
            await _httpClient.PutAsJsonAsync($"customer/depositwithdraw/{Customer.CustomerId}", Deposit);
            Deposit = new TransactionDto();
            await GetCustomer();
        }


        public async Task HandleValidSubmitWithdraw(int accountId)
        {
            Deposit.AccountId = accountId;
            Deposit.Amount = Deposit.Amount * -1;

            await _httpClient.PutAsJsonAsync($"customer/depositwithdraw/{Customer.CustomerId}", Deposit);
            Deposit = new TransactionDto();
            await GetCustomer();

        }


        public async Task HandleValidSubmitCustomerTransfer(int accountId)
        {
            Deposit.AccountId = accountId;
            var response = await _httpClient.PutAsJsonAsync($"customer/depositwithdraw/{Customer.CustomerId}", Deposit);
            Deposit = await response.Content.ReadFromJsonAsync<TransactionDto>();
            Console.WriteLine(Deposit.Error + "TEST");
            if (Deposit.Succeed)
            {
                Deposit = new TransactionDto();
                await GetCustomer();
            }
        }
    }
}
