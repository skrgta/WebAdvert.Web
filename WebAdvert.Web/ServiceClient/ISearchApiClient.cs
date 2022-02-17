using WebAdvert.Web.Models;

namespace WebAdvert.Web.ServiceClient
{
    public interface ISearchApiClient
    {
        Task<List<AdvertType>> Search(string keyword);
    }
}
