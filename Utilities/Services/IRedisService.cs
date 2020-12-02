using System.Threading.Tasks;

namespace Utilities.Services
{
    public interface IRedisService
    {
        Task GetString(string key);
        Task SetString(string key, string value);
    }
}