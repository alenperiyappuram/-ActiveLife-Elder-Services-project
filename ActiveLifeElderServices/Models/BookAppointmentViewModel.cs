using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ActiveLifeElderServices.Models
{
    public class BookAppointmentViewModel
    {
        [Required(ErrorMessage = "Client Name is required.")]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Appointment Date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Preferred Date & Time")]
        public DateTime AppointmentDateTime { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Nearest Landmark")]
        public string NearestLandmark { get; set; }

        [Required(ErrorMessage = "Please select a service type.")]
        [Display(Name = "Service Type")]
        public string SelectedServiceType { get; set; }

        // Property to hold the list of services for the dropdown
    
    }
}
