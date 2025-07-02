using AutoMapper;
using UserManagementSystem.DTOS.RolesDto;
using UserManagementSystem.DTOS.UsersDTO;
using UserManagementSystem.Repositories.RolesRepositories;

namespace UserManagementSystem.Services.RoleService
{
    /// <summary>
    /// Role service containing all role methods.
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _roleMapper;

        public RoleService(IRoleRepository roleRepo,IMapper roleMapper)
        {
            _roleRepo = roleRepo;
            _roleMapper = roleMapper;
        }
        /// <summary>
        /// A service to create Roles.
        /// </summary>
        public async Task CreateRoleAsync(RoleDto dto)
        {
            var result = await _roleRepo.CreateRoleAsync(dto.Name);
            if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        /// <summary>
        /// To delete the roles by
        /// role id .
        /// </summary>
        public async Task DeleteRoleAsync(string Id)
        {
            
            var roleExists = await _roleRepo.GetRoleByIdAsync(Id);
            if (roleExists is null )
                throw new Exception(MessagesConstants.RoleNotFound);

            var result = await _roleRepo.DeleteRoleAsync(roleExists);
            if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        /// <summary>
        /// Role service containing all role methods..
        /// </summary>
        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepo.GetAllRolesAsync();
            if (roles is null)
                throw new Exception(MessagesConstants.NoRoleFound);

            return _roleMapper.Map<List<RoleDto>>(roles);
        }
        /// <summary>
        /// To get all the users from 
        /// role name.
        /// </summary>
        public async Task<List<GetUsersDto>> GetUsersInRoleAsync(string roleName)
        {
            var users = await _roleRepo.GetUsersInRoleAsync(roleName);
            if (users.Count == 0)
                throw new Exception(MessagesConstants.UnmatchedCriteria);

            return _roleMapper.Map<List<GetUsersDto>>(users);
        }
        /// <summary>
        /// To update the role name from 
        /// role id.
        /// </summary>
        public async Task UpdateRoleAsync(string Id, string newName)
        {
            var roleExists = await _roleRepo.GetRoleByIdAsync(Id);
            if (roleExists is null)
                throw new Exception(MessagesConstants.RoleNotFound);

            roleExists.Name = newName;
            var result = await _roleRepo.UpdateRoleAsync(roleExists);
            if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
