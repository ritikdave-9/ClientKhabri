using System.Threading.Tasks;
using Spectre.Console;
using ClientKhabri.Components;

namespace ClientKhabri.Pages
{
    public static class NewsPage
    {
        public static async Task ShowAsync()
        {
            HeaderComponent.Welcome();

            var categoryResponse = await ServiceProvider.CategoryService.GetAllCategoriesAsync();
            var selectedCategory = CategoryComponent.ShowCategory(categoryResponse);
            AnsiConsole.Clear();
            var dateRange = DateAndTimeComponentInput.GetDateRange();
            var newsResponse = await ServiceProvider.NewsService.GetNewsByCategoryAsync(selectedCategory.CategoryID, dateRange);

            if (newsResponse == null)
            {
                MessageComponent.ShowMessage("No news found.", "red");
                return;
            }
            HeaderComponent.Render("Please Check the Latest News");
            foreach (var news in newsResponse)
            {
                NewsComponent.ShowNews(news);
            }
            while (true)
            {
                var menuChoice = MenuComponent.ShowMenu(
                new[]
                {
                    "Back",
                    "Save Article",
                    "Like/Dislike Article",
                    "Report Article"
                },
                "\n[bold]What would you like to do?[/]"
            );
            
                switch (menuChoice)
                {
                    case "Back":
                        await UserPage.ShowUserPageAsync();

                        return;
                    
                    case "Save Article":
                        bool saveMore;
                        do
                        {
                            var inputNewsId = InputComponent.AskString("[bold]Enter the News ID to save:[/]");
                            var saveResult = await ServiceProvider.NewsService.SaveNewsAsync(inputNewsId);
                            saveMore = InputComponent.Confirm("Do you want to save another news article?");
                        } while (saveMore);
                        break;
                    case "Report Article":
                        bool reportMore;
                        do
                        {
                            var inputNewsId = InputComponent.AskInt("[bold]Enter the News ID to report:[/]");
                            var reason = InputComponent.AskString("[bold]Enter the reason for reporting:[/]");
                            var reportResult = await ServiceProvider.NewsService.ReportNewsAsync(inputNewsId, reason);

                            if (reportResult)
                                MessageComponent.ShowMessage("News article reported successfully!", "green");
                            else
                                MessageComponent.ShowMessage("Failed to report the news article. Please try again.", "red");

                            reportMore = InputComponent.Confirm("Do you want to report another news article?");
                        } while (reportMore);
                        break;
                    case "Like/Dislike Article":
                        bool likeDislikeMore;
                        do
                        {
                            var inputNewsId = InputComponent.AskInt("[bold]Enter the News ID:[/]");
                            var likeDislikeChoice = MenuComponent.ShowMenu(
                                new[] { "Like", "Dislike", "Remove Like/Dislike" },
                                "[bold]Would you like to Like or Dislike this article?[/]"
                            );

                            bool result = false;
                            switch (likeDislikeChoice)
                            {
                                case "Like":
                                    result = await ServiceProvider.LikeDislikeService.LikeNewsAsync(inputNewsId);
                                    MessageComponent.ShowMessage(
                                        result ? "News article liked successfully!" : "Failed to like the news article. Please try again.",
                                        result ? "green" : "red"
                                    );
                                    break;
                                case "Dislike":
                                    result = await ServiceProvider.LikeDislikeService.DislikeNewsAsync(inputNewsId);
                                    MessageComponent.ShowMessage(
                                        result ? "News article disliked successfully!" : "Failed to dislike the news article. Please try again.",
                                        result ? "green" : "red"
                                    );
                                    break;
                                case "Remove Like/Dislike":
                                    result = await ServiceProvider.LikeDislikeService.RemoveLikeDislikeAsync(inputNewsId);
                                    MessageComponent.ShowMessage(
                                        result ? "Like/Dislike removed successfully!" : "Failed to remove like/dislike. Please try again.",
                                        result ? "green" : "red"
                                    );
                                    break;
                            }

                            likeDislikeMore = InputComponent.Confirm("Do you want to like/dislike another news article?");
                        } while (likeDislikeMore);
                        break;
                }
            }
        }
        public static async Task ShowPersonalizedAsync()
        {
            AnsiConsole.Clear();
            HeaderComponent.Render("Personalized News");

            int userId = Cache.UserID ?? 0;
            int pageNo = 1, pageSize = 10;

            try
            {
                var response = await ServiceProvider.NewsService.GetPersonalizedNewsAsync(pageNo, pageSize);

                if (response.Items == null || response.Items.Count == 0)
                {
                    MessageComponent.ShowWarning("No personalized news found.");
                    MessageComponent.ShowInfo("Press any key to return...");
                    Console.ReadKey(true);
                    return;
                }

                foreach (var newsItem in response.Items)
                {
                    NewsComponent.ShowNews(newsItem);
                }

                MessageComponent.ShowInfo($"\nPage {response.Page} of {Math.Ceiling((double)response.TotalCount / response.PageSize)}");
            }
            catch (Exception ex)
            {
                MessageComponent.ShowError($"Failed to fetch personalized news: {ex.Message}");
            }
            
            MenuComponent.ShowMenu(
                    new[] { "Back" },
                    "\n[bold]What would you like to do?[/]"
                );
            MessageComponent.ShowInfo("Press any key to return...");
            Console.ReadKey(true);
        }

    }
}