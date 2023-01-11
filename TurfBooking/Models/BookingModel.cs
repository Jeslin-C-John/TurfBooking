using System.ComponentModel.DataAnnotations;

namespace TurfBooking.Models
{
    public class BookingModel
    {
        [Key] public int BookingId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public bool Size { get; set; }
        [Required]
        public bool Game { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
