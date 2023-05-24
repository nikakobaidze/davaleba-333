using davaleba_333.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace davaleba_333.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<User> Users { get; set;}
        public DbSet<Phone> Phones { get; set;}
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<PhoneAccessory> PhoneAccessories { get; set; }
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           

                builder.Entity<Phone>(entity =>
                {
                    entity.HasKey(e => e.ID);
                    entity.Property(e => e.ID)
                        .ValueGeneratedOnAdd();
                    entity.HasMany(e => e.PhoneAccessories)
                        .WithOne(e => e.Phone)
                        .HasForeignKey(e => e.PhoneID);
                });
            builder.Entity<Accessory>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();
                entity.HasMany(e => e.PhoneAccessory)
                    .WithOne(e => e.Accessory)
                    .HasForeignKey(e => e.AccessoryID);
            });
            builder.Entity<PhoneAccessory>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();
                entity.HasOne(e => e.Phone)
                    .WithMany(e => e.PhoneAccessories)
                    .HasForeignKey(e => e.PhoneID);
                entity.HasOne(e => e.Accessory)
                    .WithMany(e => e.PhoneAccessory)
                    .HasForeignKey(e => e.AccessoryID);
            });

        }
    }
    
}
