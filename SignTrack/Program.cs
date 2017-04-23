using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace SignTrack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 1 && args[0].ToLower() == "migrate")
            {
                Console.WriteLine("start migration..");
                var ctx = GetContext(args[1]);
                CreateMigrationTable(ctx);
                ctx.Database.Migrate();
                Console.WriteLine("done.");
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }

        private static void CreateMigrationTable(SignContext ctx)
        {
            /*
            string sql = "select 1 information_schema.Tables where table_schema='__efmigrationshistory' and table_name='__efmigrationshistory'";
            var dbcmd = ctx.Database.GetDbConnection().CreateCommand();
            dbcmd.CommandText = sql;
             dbcmd.ExecuteReader();
             */
            try
            {
                string sql = @"CREATE TABLE `__EFMigrationsHistory` (
                            `MigrationId` varchar(150) NOT NULL,
                            `ProductVersion` varchar(32) NOT NULL,
                            PRIMARY KEY(`MigrationId`)
                            ) ENGINE = InnoDB DEFAULT CHARSET = utf8;";
                ctx.Database.ExecuteSqlCommand(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static SignContext GetContext(string cnnStr)
        {
            DbContextOptionsBuilder<SignTrack.SignContext> builder = new DbContextOptionsBuilder<SignContext>();
            builder.UseMySQL<SignTrack.SignContext>(cnnStr);
            return new SignContext(builder.Options);
        }
    }
}
