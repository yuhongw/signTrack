using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
        { }

        public DbSet<Student> Students { get; set; }
        public DbSet<SignIn> SignIns { get; set; }
    }
       
}
