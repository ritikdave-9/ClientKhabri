using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ClientKhabri.Utils;

namespace ClientKhabri.Services
{
    public class LikeDislikeService : ILikeDislikeService
    {
        public async Task<bool> LikeNewsAsync(int newsId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "userId", Cache.UserID.ToString() },
                { "newsId", newsId.ToString() }
            };

            var response = await ServiceProvider.httpRequestManager.SendAsync<object>(
                HttpMethod.Post,
                ApiEndpoints.NewsLikeDislike.Like,
                queryParams
            );

            if (response.Response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                ConsolePrinter.PrintError(response.Error?.Message ?? "Failed to like news.");
                return false;
            }
        }

        public async Task<bool> DislikeNewsAsync(int newsId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "userId", Cache.UserID.ToString() },
                { "newsId", newsId.ToString() }
            };

            var response = await ServiceProvider.httpRequestManager.SendAsync<object>(
                HttpMethod.Post,
                ApiEndpoints.NewsLikeDislike.Dislike,
                queryParams
            );

            if (response.Response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                ConsolePrinter.PrintError(response.Error?.Message ?? "Failed to dislike news.");
                return false;
            }
        }

        public async Task<bool> RemoveLikeDislikeAsync(int newsId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "userId", Cache.UserID.ToString() },
                { "newsId", newsId.ToString() }
            };

            var response = await ServiceProvider.httpRequestManager.SendAsync<object>(
                HttpMethod.Delete,
                ApiEndpoints.NewsLikeDislike.Remove,
                queryParams
            );

            if (response.Response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                ConsolePrinter.PrintError(response.Error?.Message ?? "Failed to remove like/dislike.");
                return false;
            }
        }
    }
}
