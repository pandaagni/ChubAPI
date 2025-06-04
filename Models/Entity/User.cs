using Microsoft.EntityFrameworkCore;

namespace ChubAPI.Models.Entity
{
    [PrimaryKey(nameof(UserID))]
    [Index(nameof(Email), IsUnique = true)]  
    public class User
    {
        public Guid UserID { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public int? Phone { get; set; }
        public required string Password { get; set; }
    }
}