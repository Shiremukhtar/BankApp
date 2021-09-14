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
    public class CustomerLoanBase : ComponentBase
    {

        [Inject]
        public HttpClient _httpClient { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        public Loan Loan { get; set; } = new Loan();
        public Customer Customer { get; set; }

        public List<AccountDto> Accounts { get; set; } = new List<AccountDto>();

        [Parameter]
        public int CustomerId { get; set; }

        
        public int SelectedAccountId { get; set; }

        protected override async Task<Task> OnInitializedAsync()
        {
            await GetCustomer();
            
            
            return base.OnInitializedAsync();
        }
        
        public async Task HandleValidSubmit()
        {
            Loan.AccountId = SelectedAccountId;


            Console.WriteLine(SelectedAccountId);

            await _httpClient.PostAsJsonAsync($"loan/addloan/{CustomerId}", Loan);
            Navigation.NavigateTo("/customersadmin");
        }


        public async Task GetCustomer()
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"customer/getcustomer/{CustomerId}");

            Customer = customer;
            Accounts = customer.AccountsList;
            SelectedAccountId = Accounts.FirstOrDefault().AccountId;

            
          

        }

    }
}
