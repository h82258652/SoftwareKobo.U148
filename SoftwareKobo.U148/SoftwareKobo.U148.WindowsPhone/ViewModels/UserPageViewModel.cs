using GalaSoft.MvvmLight;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.ViewModels
{
    public class UserPageViewModel : ViewModelBase
    {
        private User _user;

        private IUserService _userService;

        public UserPageViewModel(IUserService userService)
        {
            _userService = userService;

            User = new User();
        }

        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged(nameof(User));
            }
        }

        public async Task<string> Login(string email, string password)
        {
            try
            {
                UserInfoDocument document = await _userService.Login(email, password);
                if (document.Code == 0)
                {
                    User.Data = document.Data;
                    return "success";
                }
                else
                {
                    return document.Message;
                }
            }
            catch
            {
                return "请检查网络连接";
            }
        }
    }
}