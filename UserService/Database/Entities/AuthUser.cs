using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Entities
{
    public class AuthUser
    {
        [Key]
        public int Id { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
