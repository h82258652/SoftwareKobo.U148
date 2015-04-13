using System;

namespace SoftwareKobo.U148.Models
{
    public static class FeedCategoryExtensions
    {
        public static string GetName(this FeedCategory category)
        {
            switch (category)
            {
                case FeedCategory.Home:
                    return "首页";

                case FeedCategory.Weibo:
                    return "微博";

                case FeedCategory.Video:
                    return "影像";

                case FeedCategory.Image:
                    return "图画";

                case FeedCategory.Game:
                    return "游戏";

                case FeedCategory.Audio:
                    return "音频";

                case FeedCategory.Text:
                    return "文字";

                case FeedCategory.Mix:
                    return "杂粹";

                case FeedCategory.Piao:
                    return "漂流";

                case FeedCategory.Fair:
                    return "集市";

                case FeedCategory.Tasty:
                    return "短品";

                default:
                    throw new ArgumentNullException(nameof(category));
            }
        }

        //public static FeedCategory GetCategory(this Feed feed)
        //{
        //    return (FeedCategory)feed.Category;
        //}
    }
}