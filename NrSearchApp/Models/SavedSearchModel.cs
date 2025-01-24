using System;

namespace NrSearchApp.Models
{
    public class SavedSearchModel
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Carrier { get; set; } = string.Empty;
        public string LineType { get; set; } = string.Empty;
        public bool Valid { get; set; }
        public DateTime SearchDate { get; set; } = DateTime.Now;
    }
}

