using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FlowershopAPI.Models
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum Role
    {
        Admin,
        User,
    }
}
