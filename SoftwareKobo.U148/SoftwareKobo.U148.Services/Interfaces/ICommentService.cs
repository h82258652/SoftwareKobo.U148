using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDocument> GetCommentAsync(Feed feed, int page = 1);

        Task<SendCommentResult> SendCommentAsync(Feed feed, UserInfo user, string content, Device device = Device.Android, Comment reviewComment = null);
    }
}