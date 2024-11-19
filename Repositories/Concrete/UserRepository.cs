using ActivityManagerAPI.Data;
using ActivityManagerAPI.Models;
using ActivityManagerAPI.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ActivityManagerAPI.Repositories.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
