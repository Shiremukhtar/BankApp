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
    public class CustomerAdminBase:ComponentBase
    {

        [Inject]
        public HttpClient _httpClient { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        public List<Customer> PendingCustomer { get; set; } = new List<Customer>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
       

        protected override async Task<Task> OnInitializedAsync()
        {
            await GetPendingCustomers();
            await GetCustomers();
            return base.OnInitializedAsync();
        }
        public async Task GetPendingCustomers()
        {
            var customers = await _httpClient.GetFromJsonAsync<List<Customer>>($"customer/getunregistered");

            PendingCustomer = customers;
        }
        public async Task ApproveAsAdmin(Customer customer)
        {
            await _httpClient.PutAsJsonAsync($"customer/approveasadmin/",customer);
            await GetPendingCustomers();
            await GetCustomers();
        }
        public async Task ApproveAsCustomer(Customer customer)
        {
            await _httpClient.PutAsJsonAsync($"customer/approveascustomer/", customer);
            await GetPendingCustomers();
            await GetCustomers();
        }
        public async Task GetCustomers()
        {
            var customers = await _httpClient.GetFromJsonAsync<List<Customer>>($"customer/getcustomers");

            Customers = customers;
        }
        public async Task ShowAccountFormClicked(int customerId)
        {
            Navigation.NavigateTo($"/createaccount/{customerId}");
        }
        public async Task ApplyLoanOnClick(int CustomerId)
        {
            Navigation.NavigateTo($"/loanpage/{CustomerId}");
        }
        
    }
}
