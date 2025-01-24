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
        private readonly string _accessKey;

        public NumverifyService(HttpClient httpClient, ApiSettings apiSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://apilayer.net/api/");
            _accessKey = apiSettings.NumverifyApiKey;
            
            Console.WriteLine($"NumverifyService initialized with key: {_accessKey}");
        }

        public async Task<PhoneNumberModel?> ValidatePhoneNumber(string number)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessKey))
                {
                    Console.WriteLine("Error: API access key is missing");
                    return null;
                }

                // Remove any + prefix as the API doesn't expect it
                number = number.TrimStart('+');
                Console.WriteLine($"Processing number: {number}");

                // Format URL exactly as the API expects it
                var url = $"validate?access_key={_accessKey}&number={number}&format=1";
                Console.WriteLine($"Calling API with URL: {url}");

                // Make the API call and get raw response first
                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Raw API Response: {content}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<PhoneNumberModel>();
                    if (result != null)
                    {
                        Console.WriteLine($"Parsed response - Valid: {result.Valid}, Country: {result.Country}");
                    }
                    return result;
                }
                else
                {
                    Console.WriteLine($"API returned error status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ValidatePhoneNumber: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return null;
            }
        }
    }
}

