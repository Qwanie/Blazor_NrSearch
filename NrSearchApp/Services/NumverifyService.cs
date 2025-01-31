using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NrSearchApp.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using NrSearchApp.Configuration;
using Microsoft.Extensions.Options;

namespace NrSearchApp.Services
{
    public class NumverifyService
    {
        private readonly HttpClient _httpClient;

        public NumverifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PhoneNumberModel?> ValidatePhoneNumber(string number)
        {
            try
            {
                number = number.TrimStart('+');
                return await _httpClient.GetFromJsonAsync<PhoneNumberModel>($"api/phone/validate/{number}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating number: {ex.Message}");
                return null;
            }
        }
    }
}

