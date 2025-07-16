// ActiveLifeElderServices.Models/Appointment.cs
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System; // Ensure System is included for DateTime

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

        [Required(ErrorMessage = "Service Address is required.")]
        [StringLength(250)]
        [Display(Name = "Service Address")]
        public string Address { get; set; }

        [StringLength(250)]
        [Display(Name = "Contact Address (if different)")]
        public string ContactAddress { get; set; }

        // NEW: Contact Phone Number
        [Required(ErrorMessage = "Contact Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [StringLength(20)] // Adjust length as needed for international numbers
        [Display(Name = "Contact Phone Number")]
        public string ContactPhoneNumber { get; set; } // New field

        // NEW: Alternate Phone Number
   

 


        [Required(ErrorMessage = "Appointment Time is required.")]
        [DataType(DataType.Time)]
        [Display(Name = "PreferredTime")]
        public DateTime AppointmentTime { get; set; }

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
        public string ServiceType { get; set; }

        [Display(Name = "Additional Requirements")]
        [StringLength(1000)]
        public string? AdditionalRequirements { get; set; }

        [StringLength(20)]
        [Display(Name = "Caregiver Gender Preference")]
        public string CaregiverGenderPreference { get; set; }

        [StringLength(20)]
        [Display(Name = "Alternate Phone Number")]
        public string? AlternatePhoneNumber { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        [Required(ErrorMessage = "Appointment Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Preferred Date & Time")]
        public DateTime AppointmentDate { get;set; }
    }
}

