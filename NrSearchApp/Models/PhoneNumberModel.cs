using System.Text.Json.Serialization;

namespace NrSearchApp.Models
{
    public class PhoneNumberModel
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; } = string.Empty;

        [JsonPropertyName("local_format")]
        public string LocalFormat { get; set; } = string.Empty;

        [JsonPropertyName("international_format")]
        public string InternationalFormat { get; set; } = string.Empty;

        [JsonPropertyName("country_prefix")]
        public string CountryPrefix { get; set; } = string.Empty;

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; } = string.Empty;

        [JsonPropertyName("country_name")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("carrier")]
        public string Carrier { get; set; } = string.Empty;

        [JsonPropertyName("line_type")]
        public string LineType { get; set; } = string.Empty;
    }
}

