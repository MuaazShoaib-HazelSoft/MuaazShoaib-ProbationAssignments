using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.DTOS.RolesDto
{
    /// <summary>
    /// Dto for Roles having
    /// Id and Role Name.
    /// </summary>
    public class RoleDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
       
    }
}
