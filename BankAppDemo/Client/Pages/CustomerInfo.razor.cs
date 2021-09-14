using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankAppDemo.Client.Pages
{
    public class CustomerInfoBase:ComponentBase
    {
        [Inject]
        public HttpClient _httpClient { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        
    }
}
