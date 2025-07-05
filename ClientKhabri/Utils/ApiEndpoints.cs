namespace ClientKhabri.Utils
{
    public static class ApiEndpoints
    {
        public static class Auth
        {
            public const string Login = "/api/auth/login";
        }

        public static class Category
        {
            public const string GetAll = "/api/category/all";
        }

        public static class News
        {
            public const string Page = "/api/news/page";
            public const string GetAllSaved = "/api/news/saved/all";
            public const string Search = "/api/news/search";
            public const string Save = "/api/news/save";
            public const string DeleteSaaved = "/api/news/saved";

        }

        public static class NewsLikeDislike
        {
            public const string Like = "/api/newslikedislike/like";
            public const string Dislike = "/api/newslikedislike/dislike";
            public const string Remove = "/api/newslikedislike/remove";
            public const string Count = "/api/newslikedislike/count";
            public const string UserReaction = "/api/newslikedislike/user-reaction";
        }

        public static class NewsSource
        {
            public const string Add = "/api/newssource/add";
        }

        public static class NewsSubscribe
        {
            public const string Subscribe = "/api/newssubscribe/subscribe";
            public const string Unsubscribe = "/api/newssubscribe/unsubscribe";
            public const string User = "/api/newssubscribe/user";
        }

        public static class Notification
        {
            public const string Add = "/api/notification/add";
            public const string User = "/api/notification/user";
            public const string Seen = "/api/notification/seen";
        }

        public static class Report
        {
            public const string News = "/api/report/news";
            public const string All = "/api/report/all";
        }

        public static class User
        {
            public const string Signup = "/api/user/signup";
        }
    }
}
