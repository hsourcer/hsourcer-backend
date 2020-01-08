using HSourcer.Domain.Entities;
using System.Threading.Tasks;

namespace HSourcer.Application.UserIdentity
{
    public interface IUserResolve
    {
        Task<User> GetUserIdentity();
    }
}