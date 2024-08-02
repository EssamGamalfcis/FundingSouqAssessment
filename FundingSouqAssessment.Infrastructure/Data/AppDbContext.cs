using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FundingSouqAssessment.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using FundingSouqAssessment.Domain.Enums;

namespace FundingSouqAssessment.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            builder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(60);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(60);
                entity.Property(e => e.PersonalId).IsRequired().HasMaxLength(11);
                entity.Property(e => e.MobileNumber).IsRequired();
                entity.Property(e => e.Gender).IsRequired();
                entity.HasMany(c => c.Addresses)
                    .WithOne(a => a.Client)
                    .HasForeignKey(a => a.ClientId);
                entity.HasMany(c => c.Accounts)
                      .WithOne(a => a.Client)
                      .HasForeignKey(a => a.ClientId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Country).IsRequired();
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.Street).IsRequired();
                entity.Property(e => e.ZipCode).IsRequired();
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AccountNumber).IsRequired();
                entity.Property(e => e.Balance).IsRequired();
                entity.HasOne(a => a.Client)
                      .WithMany(c => c.Accounts)
                      .HasForeignKey(a => a.ClientId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });


            // Seed data
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();
            var adminUserId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            builder.Entity<ApplicationRole>().HasData(
                  new ApplicationRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                  new ApplicationRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
              );
             
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Admin",
                    LastName = "User",
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin@1234")
                },
                new ApplicationUser
                {
                    Id = userId,
                    UserName = "user@example.com",
                    NormalizedUserName = "USER@EXAMPLE.COM",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "USER",
                    LastName = "User",
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "User@1234")
                }
            );
            builder.Entity<IdentityUserRole<Guid>>().HasData(
                         new IdentityUserRole<Guid> { UserId = userId, RoleId = userRoleId },
                         new IdentityUserRole<Guid> { UserId = adminUserId, RoleId = adminRoleId });

            var addressId1 = 1L;  
            var addressId2 = 2L;
            var clientId = 1L;
            builder.Entity<Address>().HasData(
                new Address
                {
                    Id = addressId1,
                    Country = "Country1",
                    City = "City1",
                    Street = "Street1",
                    ZipCode = "12345",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    UpdatedOn = DateTime.Now,
                    ClientId = clientId
                },
                new Address
                {
                    Id = addressId2,
                    Country = "Country2",
                    City = "City2",
                    Street = "Street2",
                    ZipCode = "67890",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    UpdatedOn = DateTime.Now,
                    ClientId = clientId
                }
            );

            builder.Entity<Client>().HasData(
                new Client
                {
                    Id = clientId, 
                    Email = "client@example.com",
                    FirstName = "Client",
                    LastName = "Example",
                    PersonalId = "12345678901",
                    MobileNumber = "+1234567890",
                    ProfilePhotoUrl ="",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    UpdatedOn = DateTime.Now,
                    Gender = Gender.Male
                }
            );

            builder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1L,  
                    AccountNumber = "ACC123456789",
                    Balance = 1000.00m,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    UpdatedOn = DateTime.Now,
                    ClientId = clientId 
                }
            );
        }
    }
}
