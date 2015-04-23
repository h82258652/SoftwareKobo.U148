using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserInfoDocument> Login(string email, string password);
    }
}