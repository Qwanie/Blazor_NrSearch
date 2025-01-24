using System.Collections.Generic;
using NrSearchApp.Models;

namespace NrSearchApp.Services
{
    public interface ISearchStorage
    {
        IEnumerable<SavedSearchModel> GetSearches();
        void AddSearch(SavedSearchModel search);
        void RemoveSearch(SavedSearchModel search);
    }
}

