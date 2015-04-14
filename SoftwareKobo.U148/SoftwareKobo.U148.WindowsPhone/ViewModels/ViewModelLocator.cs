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
            SimpleIoc.Default.Register<ICommentService, CommentService>();

            // 注册 ViewModel。
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<DetailPageViewModel>();
            SimpleIoc.Default.Register<CommentPageViewModel>();
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

        public CommentPageViewModel Comment
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CommentPageViewModel>();
            }
        }
    }
}