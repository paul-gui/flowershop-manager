using Microsoft.AspNetCore.Identity;

namespace FlowershopAPI.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum Role
    {
        Admin,
        User,
    }
}
