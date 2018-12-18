using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Repositories
{
    public interface IUserRepository: IRepositoryBase<UserProfile>
    {
        Task<UserProfile> FindByLogin(string login);
        Task<UserProfile> FindByEmail(string email);
    }
}
