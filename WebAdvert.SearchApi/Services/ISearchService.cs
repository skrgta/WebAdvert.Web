using WebAdvert.SearchApi.Models;

namespace WebAdvert.SearchApi.Services
{
    public interface ISearchService
    {
        Task<List<AdvertType>> Search(string keyword);
    }
}
