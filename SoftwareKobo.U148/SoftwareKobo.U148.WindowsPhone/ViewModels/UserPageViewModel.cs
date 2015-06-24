using GalaSoft.MvvmLight;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.ViewModels
{
    public class UserPageViewModel : ViewModelBase
    {
        private IUserService _userService;

        public UserPageViewModel(IUserService userService)
        {
            _userService = userService;
        }

        private bool _isLogining;

        public bool IsLogining
        {
            get
            {
                return _isLogining;
            }
            private set
            {
                _isLogining = value;
                RaisePropertyChanged("IsLogining");
            }
        }

        public async Task<string> Login(string email, string password)
        {
            IsLogining = true;
            try
            {
                UserInfoDocument document = await _userService.Login(email, password);
                if (document.Code == 0)
                {
                    User.Instance.Data = document.Data;
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
            finally
            {
                IsLogining = false;
            }
        }

        public void Logout()
        {
            User.Instance.Data = null;
        }
    }
}