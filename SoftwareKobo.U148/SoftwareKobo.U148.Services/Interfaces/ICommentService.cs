using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDocument> GetCommentAsync(Feed feed, int page = 1);
    }
}