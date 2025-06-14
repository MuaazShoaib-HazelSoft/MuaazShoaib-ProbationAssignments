using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Models
{
    /// <summary>
    /// Schema of User Class
    /// </summary>
    public class User
    {
        public int Id { get; set; } = 0;
        
        public string Name { get; set; } = "";
        
        public string Email { get; set; } = "";
        
        public string Password { get; set; } = "";
      
        public int Age { get; set; } = 0;
        

    }
}
