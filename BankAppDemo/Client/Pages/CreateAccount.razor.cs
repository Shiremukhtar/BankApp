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
    public class CreateAccountBase:ComponentBase
    {
        [Inject]
        public HttpClient _httpClient { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public int CustomerId { get; set; }


        public AccountType AccountType { get; set; } = new AccountType();
        public Customer Customer { get; set; } = new Customer();
        public List<AccountType> AccountTypes { get; set; } = new List<AccountType>();
        public int SelectedAccountType { get; set; }

   
        protected override async Task<Task> OnInitializedAsync()
        {
           await GetCustomer();
           await GetAccountTypes();
            return base.OnInitializedAsync();
        }
        public async Task GetCustomer()
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"customer/getcustomer/{CustomerId}");


            Customer = customer;
        }
        public async Task GetAccountTypes()
        {
            var accounttypes = await _httpClient.GetFromJsonAsync<List<AccountType>>($"accounttype/getaccounttypes");
            AccountTypes = accounttypes;
        }
        public async Task HandleValidSubmit()
        {
            await _httpClient.PutAsJsonAsync($"customer/addaccount/{Customer.CustomerId}/{SelectedAccountType}","");
            
            Navigation.NavigateTo("/customersadmin");
        
        }

       public async Task HandleValidSubmitAccountType()
        {
            await _httpClient.PostAsJsonAsync($"AccountType/addaccounttype/", AccountType);
            await GetAccountTypes();

            AccountType = new AccountType();
        }


    }
}
