using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientKhabri.Dtos;

namespace ClientKhabri.Services.Interface
{
    public interface INewsService
    {
        Task<List<News>> GetNewsByCategoryAsync(int categoryId ,DateRangeDto dateRange);
        Task<bool> SaveNewsAsync(string newsId);
        Task<List<News>> GetSavedNewsAsync();
        Task<bool> DeleteSavedNewsAsync(string newsId);

    }
}
