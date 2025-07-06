namespace ClientKhabri.Services
{
    public interface ILikeDislikeService
    {
        Task<bool> LikeNewsAsync(int newsId);
        Task<bool> DislikeNewsAsync(int newsId);
        Task<bool> RemoveLikeDislikeAsync(int newsId);
    }
}
