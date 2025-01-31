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
        private readonly string _apiKey;

        public NumverifyService(HttpClient httpClient, ApiSettings settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://apilayer.net/api/");
            _apiKey = settings.NumverifyApiKey;
            Console.WriteLine($"NumverifyService initialized with API key length: {_apiKey.Length}");
        }

        public async Task<PhoneNumberModel?> ValidatePhoneNumber(string number)
        {
            try
            {
                number = number.TrimStart('+');
                var url = $"validate?access_key={_apiKey}&number={number}&format=1";
                Console.WriteLine($"Making request to: validate endpoint");

                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response status: {response.StatusCode}");
                Console.WriteLine($"Response content: {content}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<PhoneNumberModel>();
                    if (result != null)
                    {
                        Console.WriteLine($"Parsed result - Valid: {result.Valid}, Country: {result.Country}");
                    }
                    return result;
                }
                else
                {
                    Console.WriteLine($"Request failed with status: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ValidatePhoneNumber: {ex.Message}");
                return null;
            }
        }
    }
}

