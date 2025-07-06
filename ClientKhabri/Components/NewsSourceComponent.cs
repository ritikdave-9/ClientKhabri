using ClientKhabri.Dtos;
using ClientKhabri.Enums;
using Spectre.Console;
using System.Collections.Generic;

namespace ClientKhabri.Components
{
    public static class NewsSourceComponent
    {
        public static NewsSourceDetails PromptForEdit(NewsSourceDetails original)
        {
            var updated = new NewsSourceDetails
            {
                Name = original.Name,
                BaseURL = original.BaseURL,
                Status = original.Status,
                Token = original.Token
            };

            if (InputComponent.Confirm($"Current Name: {original.Name}. Change?"))
                updated.Name = InputComponent.AskString("Enter new Name:");

            if (InputComponent.Confirm($"Current URL: {original.BaseURL}. Change?"))
                updated.BaseURL = InputComponent.AskString("Enter new URL:");

            if (InputComponent.Confirm($"Current Status: {original.Status}. Change?"))
            {
                var statusChoices = new List<string> { "Active", "Disabled", "Pending", "Error" };
                var selectedStatus = MenuComponent.ShowMenu( statusChoices, "Select new Status:");
                updated.Status = Enum.Parse<NewsSourceStatus>(selectedStatus);
            }

            if (InputComponent.Confirm("Change API Token?"))
                updated.Token = InputComponent.AskString("Enter new Token:");

            return updated;
        }
    }
}
