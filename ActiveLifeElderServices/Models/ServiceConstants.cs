namespace ActiveLifeElderServices.Models
{
    public static class ServiceConstants
    {
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

        // You can also define them with IDs if you prefer
        public static readonly Dictionary<string, string> ServiceDetails = new Dictionary<string, string>
        {
            { "Medical Escort & Support", "Assistance with hospital visits, doctor appointments, and medical procedures." },
            { "Personalized Transportation", "Reliable and safe transport for errands, social outings, and appointments." },
            { "In-Home Care & Assistance", "Daily living support including personal care, meal preparation, and companionship." },
            { "Home Deep Cleaning", "Thorough cleaning services to maintain a hygienic and comfortable living environment." },
            { "Errand & Delivery Services", "Assistance with picking up prescriptions, mail, and other essential deliveries." },
            { "Gardening & Outdoor Support", "Light gardening, yard work, and outdoor maintenance to keep surroundings tidy." },
            { "Grocery & Shopping Assistance", "Help with grocery shopping, meal planning, and delivery from any store." }
        };
    }
}
