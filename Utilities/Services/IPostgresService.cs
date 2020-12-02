using System.Threading.Tasks;

namespace Utilities.Services
{
    public interface IPostgresService
    {
        Task AddMessage(string messageString);
        Task GetMessage(string messageString);
        Task GetMessageById(int id);
    }
}