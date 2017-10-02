using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodedHomes.Models;
using System.Data.Entity;
using System.Configuration;

namespace CodedHomes.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Home> Homes { get; set; }
        public DbSet<User> Users { get; set; }

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CodedHomesDEV"].ConnectionString;
            }
        }

        public DataContext() : base(nameOrConnectionString:DataContext.ConnectionString)
        {

        }
    }
}
