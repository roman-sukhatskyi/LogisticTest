using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.ClientTask;

namespace UM_LOGISTIC_V1.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User.User> Users { get; set; }
        public DbSet<Account.Account> Accounts { get; set; }
        public DbSet<Role.Role> Roles { get; set; }
        public DbSet<CooperationApplication.CooperationApplication> CooperationApplications { get; set; }
        public DbSet<CooperationApplication.ApplicationWorkType> ApplicationWorkTypes { get; set; }
        public DbSet<TransportationApplication.TransportationApplication> TransportationApplications { get; set; }
        public DbSet<TransportationApplication.ShipmentType> ShipmentTypes { get; set; }
        public DbSet<TransportationPicture.TransportationPicture> TransportationPictures { get; set; }
        public DbSet<CooperationPicture.CooperationPicture> CooperationPictures { get; set; }
        public DbSet<ClientTaskType> ClientTaskTypes { get; set; }
        public DbSet<ClientTask.ClientTask> ClientTasks { get; set; }
        public DbSet<ApplicationTrash.ApplicationTrash> ApplicationsTrash { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientTask.ClientTask>()
                .HasOptional(a => a.TransportationApplication)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ClientTask.ClientTask>()
               .HasOptional(a => a.CooperationApplication)
               .WithOptionalDependent()
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicationTrash.ApplicationTrash>()
                .HasOptional(a => a.CooperationApplication)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicationTrash.ApplicationTrash>()
                .HasOptional(a => a.TransportationApplication)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);
        }
    }
}