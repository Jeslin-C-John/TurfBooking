using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurfBooking.Models
{
    public class BookingModel
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }

        [NotMapped]
        //[DataType(DataType.Date)]
        [Required]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateOnly ShortBookingDate { get; set; }
        public int Ground { get; set; }
        public int Slot { get; set; }
        [NotMapped]
        public String? SlotPM { get; set; }
    }


}
