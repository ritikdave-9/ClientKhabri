using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientKhabri.Services.Interface
{

    public interface IKeywordService
    {
        Task<KeywordResponseDto> GetKeywordsByCategoryAsync(int categoryId);
    }
}