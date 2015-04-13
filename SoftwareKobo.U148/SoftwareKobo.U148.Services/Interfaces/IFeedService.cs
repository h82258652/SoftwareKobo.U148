using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services.Interfaces
{
    public interface IFeedService
    {
        Task<FeedListDocument> GetCategoryAsync(FeedCategory category, int page = 1);

        Task<FeedDetailDocument> GetDetailAsync(Feed feed);
    }
}