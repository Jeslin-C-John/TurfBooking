using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurfBooking.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required!")]
        [StringLength(25)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, MinimumLength = 6)]
        [NotMapped]
        public string Password { get; set; }


        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; }

        public string? EncryptPass { get; set; }


        [NotMapped]
        public bool RememberMe { get; set; }
    }
}
