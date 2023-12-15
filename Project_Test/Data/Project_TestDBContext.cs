using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.NewFolder
{
    public class Project_TestDBContext:IdentityDbContext
    {
        public Project_TestDBContext(DbContextOptions<Project_TestDBContext>options):base(options)
        {

        }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<ReciptionBank> Banks { get; set; }
        public DbSet<Donation_Box> Donations { get; set; }
        public DbSet<BloodStock> Stocks { get; set; }
        public DbSet<Blood> Bloods { get; set; }
        public DbSet<Driver> Drivers { get; set; }

    }
}
