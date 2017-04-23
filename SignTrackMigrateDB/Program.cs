using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace SignTrackMigrateDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start migration..");
            SignTrack.Startup startup = new SignTrack.Startup(null);
            startup.GetContext().Database.Migrate();
            Console.WriteLine("done.");
        }
    }
}