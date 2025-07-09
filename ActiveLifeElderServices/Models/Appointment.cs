using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ActiveLifeElderServices.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Client Name is required.")]
        [StringLength(100)]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Appointment Date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Preferred Date & Time")]
        public DateTime AppointmentDateTime { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [StringLength(250)]
        [Display(Name = "Nearest Landmark")]
        public string NearestLandmark { get; set; }

        [Required(ErrorMessage = "Type of Service is required.")]
        [StringLength(100)]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; } // Will store the selected service name

        // Optional: Link to the User who booked the appointment (if applicable)
        public int? UserId { get; set; } // Nullable if guests can book
        [ForeignKey("UserId")]
        public User User { get; set; } // Navigation property

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending"; // e.g., Pending, Confirmed, Completed, Cancelled
    }
}

