using System.Collections.Generic;
using System.Threading.Tasks;
using ClientKhabri.Dtos;

namespace ClientKhabri.Services.Interface
{
    public interface INewsSourceService
    {
        Task<List<NewsSourceDetails>> GetAllNewsSourcesAsync();
        Task<bool> UpdateNewsSourceAsync(string id, NewsSourceDetails updated);
        Task<NewsSourceDetails> GetNewsSourceByIdAsync(string id);

    }
}
