using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySQL.Data.EntityFrameworkCore.Extensions;
using SignTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignTrack
{
    public class SignContext:DbContext
    {
        public SignContext(DbContextOptions<SignContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }

    public static class SignContextFactory
    {
        public static SignContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SignContext>();
            optionsBuilder.UseMySQL("server=localhost;userid=root;pwd=1111;database=yu");
            var context = new SignContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
