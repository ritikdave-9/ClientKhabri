using Spectre.Console;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClientKhabri.Components;

namespace ClientKhabri.Pages
{
    public static class SubscribePage
    {
        private static async Task<string?> SelectCategoryAsync(string prompt)
        {
            var categories = await ServiceProvider.CategoryService.GetAllCategoriesAsync();
            var choices = categories.Select(c => $"{c.CategoryID}: {c.CategoryName}").ToList();
            choices.Add("Back");
            return MenuComponent.ShowMenu(choices, prompt);
        }

        private static async Task<string?> SelectKeywordAsync(int categoryId, string prompt)
        {
            var keywordResponse = await ServiceProvider.KeywordService.GetKeywordsByCategoryAsync(categoryId);
            var choices = keywordResponse.Items.Select(k => $"{k.KeywordID}: {k.KeywordName}").ToList();
            choices.Add("Back");
            return MenuComponent.ShowMenu(choices, prompt);
        }

        private static async Task<string?> SelectSubscriptionAsync(int userId, string prompt)
        {
            var subscriptions = await ServiceProvider.NewsSubscribeService.GetUserSubscriptionsAsync(userId);
            if (subscriptions == null || subscriptions.Count == 0)
            {
                HeaderComponent.Render("You have no subscriptions.");
                return null;
            }
            var choices = subscriptions.Select(s =>
                s.KeywordID != null
                    ? $"Category: {s.CategoryName}, Keyword: {s.KeywordName} (ID: {s.KeywordID})"
                    : $"Category: {s.CategoryName} (ID: {s.CategoryID})"
            ).ToList();
            choices.Add("Back");
            return MenuComponent.ShowMenu(choices, prompt);
        }

        public static async Task ShowSubscribeAsync()
        {
            while (true)
            {
                var categoryChoice = await SelectCategoryAsync("Select a category to subscribe:");
                if (categoryChoice == "Back" || categoryChoice == null)
                    return;

                int categoryId = int.Parse(categoryChoice.Split(':')[0]);

                if (InputComponent.Confirm("Do you want to subscribe to a keyword in this category?"))
                {
                    var keywordChoice = await SelectKeywordAsync(categoryId, "Select a keyword:");
                    if (keywordChoice == "Back" || keywordChoice == null)
                        continue;

                    int keywordId = int.Parse(keywordChoice.Split(':')[0]);
                    var result = await ServiceProvider.NewsSubscribeService.SubscribeAsync(
                        userId: Cache.UserID.Value,
                        categoryId: categoryId,
                        keywordId: keywordId);
                    HeaderComponent.Render(result);
                }
                else
                {
                    var result = await ServiceProvider.NewsSubscribeService.SubscribeAsync(
                        userId: Cache.UserID.Value,
                        categoryId: categoryId,
                        keywordId: null);
                    HeaderComponent.Render(result);
                }
            }
        }

        public static async Task ShowUnsubscribeAsync()
        {
            while (true)
            {
                var userId = Cache.UserID.Value;
                var subChoice = await SelectSubscriptionAsync(userId, "Select a subscription to unsubscribe:");
                if (subChoice == "Back" || subChoice == null)
                    return;

                var subscriptions = await ServiceProvider.NewsSubscribeService.GetUserSubscriptionsAsync(userId);
                var selected = subscriptions[
                    subscriptions.FindIndex(s =>
                        (s.KeywordID != null
                            ? $"Category: {s.CategoryName}, Keyword: {s.KeywordName} (ID: {s.KeywordID})"
                            : $"Category: {s.CategoryName} (ID: {s.CategoryID})") == subChoice)];

                var result = await ServiceProvider.NewsSubscribeService.UnsubscribeAsync(userId, selected.CategoryID, selected.KeywordID);
                HeaderComponent.Render(result);
            }
        }
    }
}
