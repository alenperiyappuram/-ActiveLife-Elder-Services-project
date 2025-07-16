namespace ActiveLifeElderServices.Models
{
    public class Service
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; } // Assuming you have this
        public string FullDescription { get; set; } // Assuming you have this
        public string IconClass { get; set; } // If you're using icon fonts
        public string ImagePath { get; set; } // <--- ADD THIS PROPERTY
        // Add any other properties your service details have
    }
}
