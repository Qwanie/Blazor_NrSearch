using System.Collections.Generic;
using System.Linq;
using NrSearchApp.Models;

namespace NrSearchApp.Services
{
    public class LocalSearchStorage : ISearchStorage
    {
        private List<SavedSearchModel> _searches = new();

        public IEnumerable<SavedSearchModel> GetSearches() => _searches;

        public void AddSearch(SavedSearchModel search)
        {
            _searches.Add(search);
        }

        public void RemoveSearch(SavedSearchModel search)
        {
            _searches.Remove(search);
        }
    }
}

