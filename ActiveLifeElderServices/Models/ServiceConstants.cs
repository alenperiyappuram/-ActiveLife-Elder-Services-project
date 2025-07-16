

using Microsoft.AspNetCore.Mvc.Rendering;

namespace ActiveLifeElderServices.Models
{
    public static class ServiceConstants
    {
        // Keep this if you still need it for other purposes, but the dictionary is more robust
        public static readonly List<string> AllServiceTypes = new List<string>
        {
            "Medical Escort & Support",
            "Personalized Transportation",
            "In-Home Care & Assistance",
            "Home Deep Cleaning",
            "Errand & Delivery Services",
            "Gardening & Outdoor Support",
            "Grocery & Shopping Assistance"
        };

        // This ServiceDetail class needs to be REMOVED or replaced by your main Service model
        // public class ServiceDetail { ... } // <-- DELETE THIS INNER CLASS

        // This dictionary will now hold instances of your main Service model
        public static readonly Dictionary<string, Service> ServiceDetails = new Dictionary<string, Service>
        {
            { "Medical Escort & Support", new Service {
                Name = "Medical Escort & Support",
                ShortDescription = "Assistance with hospital visits, doctor appointments, and medical procedures.", // Using ShortDescription
                FullDescription = "Our medical escort and support service ensures that seniors have reliable transportation and compassionate assistance for all their medical appointments, therapy sessions, and other essential outings. We provide door-to-door service, help with check-ins, take notes during consultations (if requested), and offer companionship to ease anxiety. Our caregivers are trained to handle various mobility needs and can assist with communication with medical staff, ensuring a stress-free experience for the elder and peace of mind for their families.",
                IconClass = "fas fa-hospital-user",
                ImagePath = "/images/services/medical assistance.jpg" // Relative path from wwwroot
            }},
            { "Personalized Transportation", new Service {
                Name = "Personalized Transportation",
                ShortDescription = "Reliable and safe transport for errands, social outings, and appointments.",
                FullDescription = "Reliable and safe transport for errands, social outings, and appointments. Our drivers are trained in elderly care and assistance.",
                IconClass = "fas fa-car",
                ImagePath = "/images/services/personalized-transportation.jpg" // Example image path
            }},
            { "In-Home Care & Assistance", new Service {
                Name = "In-Home Care & Assistance",
                ShortDescription = "Comprehensive daily living support including personal care, meal preparation...",
                FullDescription = "Comprehensive daily living support including personal care, meal preparation, medication reminders, and companionship within the comfort of your home.",
                IconClass = "fas fa-home",
                ImagePath = "/images/services/in-home-care.jpg" // Example image path
            }},
            { "Home Deep Cleaning", new Service {
                Name = "Home Deep Cleaning",
                ShortDescription = "Thorough and meticulous cleaning services for a hygienic and comfortable living environment...",
                FullDescription = "Thorough and meticulous cleaning services for a hygienic and comfortable living environment, tailored to the needs of seniors.",
                IconClass = "fas fa-broom",
                ImagePath = "/images/services/home-deep-cleaning.jpg" // Example image path
            }},
            { "Delivery Services", new Service {
                Name = " Delivery Services",
                ShortDescription = "Assistance with picking up prescriptions, mail, essential documents, and other deliveries...",
                FullDescription = "Assistance with picking up prescriptions, mail, essential documents, and other deliveries, ensuring convenience and peace of mind.",
                IconClass = "fas fa-box",
                ImagePath = "/images/services/errand-delivery.jpg" // Example image path
            }},
            { "Gardening & Outdoor Support", new Service {
                Name = "Gardening & Outdoor Support",
                ShortDescription = "Light gardening, yard work, and outdoor maintenance to keep surroundings tidy and accessible...",
                FullDescription = "Light gardening, yard work, and outdoor maintenance to keep surroundings tidy and accessible, allowing elders to enjoy their outdoor spaces.",
                IconClass = "fas fa-seedling",
                ImagePath = "/images/services/gardening-outdoor.jpg" // Example image path
            }},
            { "Grocery & Shopping Assistance", new Service {
                Name = "Grocery & Shopping Assistance",
                ShortDescription = "Help with grocery shopping, meal planning, and delivery from any store...",
                FullDescription = "Help with grocery shopping, meal planning, and delivery from any store. We ensure fresh produce and pantry staples are always available.",
                IconClass = "fas fa-shopping-basket",
                ImagePath = "/images/services/grocery-shopping.jpg" // Example image path
            }}
            // Add other services with their respective ImagePaths
        }; 
        public static readonly List<SelectListItem> CaregiverGenderOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "-- Select Preference (Optional) --" },
            new SelectListItem { Value = "Male", Text = "Male" },
            new SelectListItem { Value = "Female", Text = "Female" },
            new SelectListItem { Value = "No Preference", Text = "No Preference" }
        };

        // Services where gender preference is applicable (for client-side JS)
        public static readonly List<string> GenderApplicableServices = new List<string>
        {
            "Medical Escort & Support",
            "In-Home Care & Assistance",
            "Gardening & Outdoor Support"
        };

    }
}