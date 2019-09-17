using System;
using System.Collections.Generic;
using System.Text;
using ForThePeople.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForThePeople.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<PoliticalParty> PoliticalParty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Addam",
                LastName = "Joor",
                UserName = "ajoor",
                State = "Tennessee",
                PoliticalParty = "Independent",
                NormalizedUserName = "AJOOR",
                Email = "addam.joor@email.com",
                NormalizedEmail = "ADDAM.JOOR@EMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<State>().HasData(
                new State()
                {
                    Id = 1,
                    Name = "Alabama"
                },
                new State()
                {
                    Id = 2,
                    Name = "Alaska"
                },
                new State()
                {
                    Id = 3,
                    Name = "Arizona"
                },
                new State()
                {
                    Id = 4,
                    Name = "Arkansas"
                },
                new State()
                {
                    Id = 5,
                    Name = "California"
                },
                new State()
                {
                    Id = 6,
                    Name = "Colorado"
                },
                new State()
                {
                    Id = 7,
                    Name = "Connecticut"
                },
                new State()
                {
                    Id = 8,
                    Name = "Delaware"
                },
                new State()
                {
                    Id = 9,
                    Name = "Florida"
                },
                new State()
                {
                    Id = 10,
                    Name = "Georgia"
                },
                new State()
                {
                    Id = 11,
                    Name = "Hawaii"
                },
                new State()
                {
                    Id = 12,
                    Name = "Idaho"
                },
                new State()
                {
                    Id = 13,
                    Name = "Illinois"
                },
                new State()
                {
                    Id = 14,
                    Name = "Indiana"
                },
                new State()
                {
                    Id = 15,
                    Name = "Iowa"
                },
                new State()
                {
                    Id = 16,
                    Name = "Kansas"
                },
                new State()
                {
                    Id = 17,
                    Name = "Kentucky"
                },
                new State()
                {
                    Id = 18,
                    Name = "Louisiana"
                },
                new State()
                {
                    Id = 19,
                    Name = "Maine"
                },
                new State()
                {
                    Id = 20,
                    Name = "Maryland"
                },
                new State()
                {
                    Id = 21,
                    Name = "Massachusettes"
                },
                new State()
                {
                    Id = 22,
                    Name = "Michigan"
                },
                new State()
                {
                    Id = 23,
                    Name = "Minnesota"
                },
                new State()
                {
                    Id = 24,
                    Name = "Mississippi"
                },
                new State()
                {
                    Id = 25,
                    Name = "Missouri"
                },
                new State()
                {
                    Id = 26,
                    Name = "Montana"
                },
                new State()
                {
                    Id = 27,
                    Name = "Nebraska"
                },
                new State()
                {
                    Id = 28,
                    Name = "Nevada"
                },
                new State()
                {
                    Id = 29,
                    Name = "New Hampshire"
                },
                new State()
                {
                    Id = 30,
                    Name = "New Jersey"
                },
                new State()
                {
                    Id = 31,
                    Name = "New Mexico"
                },
                new State()
                {
                    Id = 32,
                    Name = "New York"
                },
                new State()
                {
                    Id = 33,
                    Name = "North Carolina"
                },
                new State()
                {
                    Id = 34,
                    Name = "North Dakota"
                },
                new State()
                {
                    Id = 35,
                    Name = "Ohio"
                },
                new State()
                {
                    Id = 36,
                    Name = "Oklahoma"
                },
                new State()
                {
                    Id = 37,
                    Name = "Oregon"
                },
                new State()
                {
                    Id = 38,
                    Name = "Pennsylvania"
                },
                new State()
                {
                    Id = 39,
                    Name = "Rhode Island"
                },
                new State()
                {
                    Id = 40,
                    Name = "South Carolina"
                },
                new State()
                {
                    Id = 41,
                    Name = "South Dakota"
                },
                new State()
                {
                    Id = 42,
                    Name = "Tennessee"
                },
                new State()
                {
                    Id = 43,
                    Name = "Texas"
                },
                new State()
                {
                    Id = 44,
                    Name = "Utah"
                },
                new State()
                {
                    Id = 45,
                    Name = "Vermont"
                },
                new State()
                {
                    Id = 46,
                    Name = "Virginia"
                },
                new State()
                {
                    Id = 47,
                    Name = "Washington"
                },
                new State()
                {
                    Id = 48,
                    Name = "West Virginia"
                },
                new State()
                {
                    Id = 49,
                    Name = "Wisconsin"
                },
                new State()
                {
                    Id = 50,
                    Name = "Wyoming"
                }
            );

            modelBuilder.Entity<PoliticalParty>().HasData(
                new PoliticalParty()
                {
                    Id = 1,
                    Name = "Democrat"
                },
                new PoliticalParty()
                {
                    Id = 2,
                    Name = "Republican"
                },
                new PoliticalParty()
                {
                    Id = 3,
                    Name = "Independent"
                }
            );
        }

        public DbSet<ForThePeople.Models.Legislature> Legislature { get; set; }

        public DbSet<ForThePeople.Models.Note> Note { get; set; }

        public DbSet<ForThePeople.Models.Bill> Bill { get; set; }
    }
}
