using System.Threading.Tasks;

namespace Utilities.Services
{
    public interface IPostgresService
    {
        Task<string> Create(string messageString);
        Task<string> Get(string messageString);
        Task<string> GetById(int id);
    }
}