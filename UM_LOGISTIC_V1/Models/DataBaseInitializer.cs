using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.ClientTask;

namespace UM_LOGISTIC_V1.Models
{
    public class DataBaseInitializer<T> : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            IList<Role.Role> roles = new List<Role.Role>();

            roles.Add(new Role.Role()
            {
                Id = 1,
                Name = "admin",
                Number = 0,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });
            roles.Add(new Role.Role()
            {
                Id = 2,
                Name = "manager",
                Number = 1,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });
            roles.Add(new Role.Role()
            {
                Id = 3,
                Name = "client",
                Number = 2,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });
            foreach (Role.Role role in roles)
                context.Roles.Add(role);

            var account = new UM_LOGISTIC_V1.Models.Account.Account();
            account.FullName = "Федюк Сергій Миколайович";
            account.WorkPhone = "0984294989";
            account.Country = "УКРАЇНА";
            account.Region = "Львівська область";
            account.City = "Львів";
            account.Street = "вул. Польова 29/a";
            account.Id = 1;
            account.CreatedOn = DateTime.Now;
            account.ModifiedOn = account.CreatedOn;
            context.Accounts.Add(account);
            context.SaveChanges();

            IList<User.User> users = new List<User.User>();

            users.Add(new User.User()
            {
                UserName = "Supervisor",
                UserPassword = "Supervisor",
                AccountId = 1,
                RoleId = 1,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });

            foreach (User.User user in users)
                context.Users.Add(user);

            IList<CooperationApplication.ApplicationWorkType> applicationWorkTypes = new List<CooperationApplication.ApplicationWorkType>();

            applicationWorkTypes.Add(new CooperationApplication.ApplicationWorkType()
            {
                Id = 1,
                Name = "грн/год",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });
            applicationWorkTypes.Add(new CooperationApplication.ApplicationWorkType()
            {
                Id = 2,
                Name = "один підйом",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });

            foreach (CooperationApplication.ApplicationWorkType applicationWorkType in applicationWorkTypes)
                context.ApplicationWorkTypes.Add(applicationWorkType);

            IList<ClientTaskType> taskTypes = new List<ClientTaskType>();

            taskTypes.Add(new ClientTaskType()
            {
                Id = 1,
                Name = "Дзвінок",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });
			
			taskTypes.Add(new ClientTaskType()
            {
                Id = 2,
                Name = "Заявка на перевезення",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });
			
			taskTypes.Add(new ClientTaskType()
            {
                Id = 3,
                Name = "Заявка на співробітництво",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });
			
            foreach (var taskType in taskTypes)
                context.ClientTaskTypes.Add(taskType);

            base.Seed(context);
        }
    }
}