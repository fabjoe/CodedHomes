using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodedHomes.Models;
using System.Data.Entity;
using System.Configuration;
using CodedHomes.Data.Configuration;

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
        static DataContext()
        {
            Database.SetInitializer(new CustomDatabaseInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new HomeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            //base.OnModelCreating(modelBuilder);
        }
        private void ApplyRules()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e=>e.Entity is IAuditInfo && (e.State == EntityState.Added )|| (e.State == EntityState.Modified)))
            {
                IAuditInfo e = (IAuditInfo)entry.Entity;
                if(entry.State == EntityState.Added)
                {
                    e.CreatedOn = DateTime.Now;
                }
                e.ModifiedOn = DateTime.Now;
            }
        }
        public override int SaveChanges()
        {
            this.ApplyRules();
            return base.SaveChanges();
        }
    }
}
