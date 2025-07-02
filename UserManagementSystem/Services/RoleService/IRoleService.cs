using UserManagementSystem.DTOS.RolesDto;
using UserManagementSystem.DTOS.UsersDTO;

namespace UserManagementSystem.Services.RoleService
{
    /// <summary>
    /// All the role interfaces that has to be implemented.
    /// </summary>
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRolesAsync();
        Task CreateRoleAsync(RoleDto dto);
        Task UpdateRoleAsync(string id, string newName);
        Task DeleteRoleAsync(string Id);
        Task<List<GetUsersDto>> GetUsersInRoleAsync(string roleName);
    }
}
