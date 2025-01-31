using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using NrSearchApp.Models;  // Lägg till referens till shared models

namespace NrSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhoneController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PhoneController> _logger;

    public PhoneController(IConfiguration configuration, ILogger<PhoneController> logger)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://apilayer.net/api/")
        };
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet("validate/{number}")]
    public async Task<IActionResult> ValidateNumber(string number)
    {
        try
        {
            var apiKey = _configuration["ApiSettings:NumverifyApiKey"];
            _logger.LogInformation($"Validating number: {number}");

            var url = $"validate?access_key={apiKey}&number={number}&format=1";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            _logger.LogInformation($"API Response: {content}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PhoneNumberModel>();
                return Ok(result);
            }

            return BadRequest($"Failed to validate number: {content}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error validating number: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }
} 