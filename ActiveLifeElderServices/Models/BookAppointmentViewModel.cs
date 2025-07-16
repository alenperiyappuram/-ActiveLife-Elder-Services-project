// ActiveLifeElderServices.Models/BookAppointmentViewModel.cs
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System;

namespace ActiveLifeElderServices.Models
{
    public class BookAppointmentViewModel
    {
        [Required(ErrorMessage = "Client Name is required.")]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Service Address is required.")]
        [Display(Name = "Service Address")]
        public string Address { get; set; }

        [Display(Name = "Contact Address (if different from Service Address)")]
        public string ContactAddress { get; set; }

        [Required(ErrorMessage = "Contact Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Contact Phone Number")]
        public string ContactPhoneNumber { get; set; }

        [Phone(ErrorMessage = "Invalid Alternate Phone Number.")]
        [Display(Name = "Alternate Phone Number")]
        public string AlternatePhoneNumber { get; set; }

        // These are the new separate properties
        [Required(ErrorMessage = "Appointment Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Preferred Date")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment Time is required.")]
        [DataType(DataType.Time)]
        [Display(Name = "Preferred Time")]
        public DateTime AppointmentTime { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Nearest Landmark")]
        public string NearestLandmark { get; set; }

        [Required(ErrorMessage = "Please select a service type.")]
        [Display(Name = "Service Type")]
        public string SelectedServiceType { get; set; }

        [Display(Name = "Additional Service Requirements")]
        [StringLength(500, ErrorMessage = "Additional requirements cannot exceed 500 characters.")]
        [DataType(DataType.MultilineText)]
        public string AdditionalRequirements { get; set; }

        [Display(Name = "Caregiver Gender Preference")]
        [StringLength(50)]
        public string? CaregiverGenderPreference { get; set; }
    }
}