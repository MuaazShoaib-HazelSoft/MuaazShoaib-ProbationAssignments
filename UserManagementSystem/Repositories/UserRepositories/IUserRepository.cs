using UserManagementSystem.Models.UserModel;
using UserManagementSystem.Repositories.GenericRepositories;

namespace UserManagementSystem.Repositories.UserRepositories
{

    public interface IUserRepository: IGenericRepository<ApplicationUser>
    {

    }
}
