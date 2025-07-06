using ClientKhabri.Dtos;

public interface INewsSubscribeService
{
    Task<string> SubscribeAsync(int userId, int? categoryId, int? keywordId);
    Task<string> UnsubscribeAsync(int userId, int? categoryId, int? keywordId);
    Task<List<Subscription>> GetUserSubscriptionsAsync(int userId);

}
