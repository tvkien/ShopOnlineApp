using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ShopOnline.Application.Commons
{
    public interface IFileHandlers
    {
        string GetFileUrl(string fileName);

        Task<string> SaveFileAsync(IFormFile file);

        Task DeleteFileAsync(string fileName);
    }
}