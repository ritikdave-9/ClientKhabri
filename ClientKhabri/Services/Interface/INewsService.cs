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
        Task<List<News>> GetNewsByCategoryAsync(string categoryId);
        Task<bool> SaveNewsAsync(string newsId);

    }
}
