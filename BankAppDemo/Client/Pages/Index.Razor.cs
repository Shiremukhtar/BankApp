using BankAppDemo.Shared;
using BankAppDemo.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BankAppDemo.Client.Pages
{
    public  class IndexBase : ComponentBase
    {
        [Inject]
        HttpClient _httpClient { get; set; }
        [Inject]
        NavigationManager Navigation { get; set; }
        public string TEST { get; set; } = "CP";

        public Customer NyCustomer { get; set; } = new Customer();


        [Inject]
         AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool VisaInteEditForm; 
    
        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();


            IsAuthenticated = authenticationState.User.Identity.IsAuthenticated;

            if (authenticationState.User.Identity.IsAuthenticated)
            {
                Console.WriteLine(authenticationState.User.Identity.Name);

                var customer  = await _httpClient.GetFromJsonAsync<Customer>($"customer/getcustomer");
                if(customer.Surname != null)
                {
                    NyCustomer = customer;
                    VisaInteEditForm = true;
                }
            }
            else
            {
                Console.WriteLine("Inte inloggad");
            }

                
        }


        public async Task HandleValidSubmit()
        {
            Console.WriteLine("SPARA");
            var response = await _httpClient.PutAsJsonAsync("customer/editcustomeronsignup", NyCustomer);
            var customer = await response.Content.ReadFromJsonAsync<Customer>();
            NyCustomer = customer;
            VisaInteEditForm = true;

        }
        public void CustomerAccountDetailsClick()
        {
            Navigation.NavigateTo($"/customeraccountdetails/{NyCustomer.CustomerId}");
        }
    }
}
