using Microsoft.AspNetCore.Mvc;

namespace NrSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhoneController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PhoneController(IConfiguration configuration)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://apilayer.net/api/")
        };
        _configuration = configuration;
    }

    [HttpGet("validate/{number}")]
    public async Task<IActionResult> ValidateNumber(string number)
    {
        try
        {
            var apiKey = _configuration["ApiSettings:NumverifyApiKey"];
            var response = await _httpClient.GetAsync(
                $"validate?access_key={apiKey}&number={number}&format=1");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PhoneNumberModel>();
                return Ok(result);
            }

            return BadRequest("Failed to validate number");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
} 