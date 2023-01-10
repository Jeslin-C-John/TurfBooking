using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurfBooking.Models
{
    public class UserModel
    {
        [Key]public int Id { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? EncryptPass { get; set; }
        [NotMapped]
        public bool RememberMe { get; set; }
    }
}
