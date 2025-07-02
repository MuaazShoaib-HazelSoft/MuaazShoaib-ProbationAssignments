using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Data;
using UserManagementSystem.Models.UserModel;
using UserManagementSystem.Repositories.GenericRepositories;

namespace UserManagementSystem.Repositories.UserRepositories
{
    public class UserRepository : GenericRepository<ApplicationUser>,IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// Method for overriding ,
        /// the Queryable method
        /// for including users,
        /// user roles and roles.
        /// </summary>
        public override IQueryable<ApplicationUser> QueryAble()
        {
            return _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role);
        }
    }
}
