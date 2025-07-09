namespace ActiveLifeElderServices.Data
{
    using Microsoft.EntityFrameworkCore;
   // Reference your User model
    using global::ActiveLifeElderServices.Models;

  
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            // DbSet for your User model, representing the Users table
            public DbSet<User> Users { get; set; }
            public DbSet<Appointment> Appointments { get; set; }

            // You can add more DbSets for other models/tables here
            // public DbSet<Appointment> Appointments { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Optional: Configure specific model properties or relationships here
                // For example, ensuring unique username:
                modelBuilder.Entity<User>()
                    .HasIndex(u => u.Username)
                    .IsUnique();
            }
        }
    }

