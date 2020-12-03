using System.Threading.Tasks;

namespace Utilities.Services
{
    public interface IRedisService
    {
        Task<string> GetString(string key);
        Task<string> SetString(string key, string value);
    }
}