using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SoftwareKobo.U148.Services;
using SoftwareKobo.U148.Services.Interfaces;

namespace SoftwareKobo.U148.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // 注册服务。
            SimpleIoc.Default.Register<IFeedService, FeedService>();

            // 注册 ViewModel。
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<DetailPageViewModel>();
        }

        public MainPageViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public DetailPageViewModel Detail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DetailPageViewModel>();
            }
        }
    }
}