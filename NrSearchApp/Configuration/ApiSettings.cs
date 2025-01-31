namespace NrSearchApp.Configuration
{
    public class ApiSettings
    {
        private string _numverifyApiKey = string.Empty;
        
        public string NumverifyApiKey
        {
            get => _numverifyApiKey;
            init => _numverifyApiKey = value;
        }
    }
} 