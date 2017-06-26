using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models;
using UM_LOGISTIC_V1.Models.Account;
using UM_LOGISTIC_V1.Models.Role;
using UM_LOGISTIC_V1.Models.User;
using UM_LOGISTIC_V1.Request.Account;

namespace UM_LOGISTIC_V1.Services
{
    public class AccountService
    {
        private DataBaseContext db = new DataBaseContext();

        public Account GetAccount(long id)
        {
            var account = db.Accounts.Find(id);
            return account;
        }

        public bool CreateAccount(Account account)
        {
            account.CreatedOn = DateTime.Now;
            account.ModifiedOn = account.CreatedOn;
            if (account != null)
            {
                db.Accounts.Add(account);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool UpdateAccount(Account account)
        {
            var accountToUpdate = db.Accounts.Find(account.Id);
            if (accountToUpdate != null)
            {
                accountToUpdate.ModifiedOn = DateTime.Now;
                accountToUpdate.FullName = account.FullName;
                accountToUpdate.HomePhone = account.HomePhone;
                accountToUpdate.WorkPhone = account.WorkPhone;
                accountToUpdate.Country = account.Country;
                accountToUpdate.Region = account.Region;
                accountToUpdate.City = account.City;
                accountToUpdate.Street = account.Street;
                db.Entry(accountToUpdate).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool RemoveAccount(long id)
        {
            var accountToDelete = db.Accounts.Find(id);
            if (accountToDelete != null)
            {
                db.Accounts.Remove(accountToDelete);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public List<Account> GetAccounts(int page, int count)
        {
            var accounts = from a in db.Accounts
                        orderby a.Id ascending
                        select a;
            if (accounts == null)
            {
                return null;
            }
            var limitedAccounts = accounts.Skip(count * page).Take(count).ToList();
            return limitedAccounts;
        }

        public User RegisterAccount(RegisterAccountRequest userToRegister)
        {
            var user = new User();
            var account = new Account();
            user.UserName = userToRegister.Login;
            user.UserPassword = userToRegister.Password;
            user.CreatedOn = DateTime.Now;
            user.ModifiedOn = DateTime.Now;
            account.FullName = userToRegister.FullName;
            account.WorkPhone = userToRegister.WorkPhone;
            account.City = userToRegister.City;
            account.CreatedOn = DateTime.Now;
            account.ModifiedOn = DateTime.Now;
            account.Image = userToRegister.Image;
            user.Account = account;
            user.Role = db.Roles.Find(3);
            db.Users.Add(user);
            try
            {
                db.SaveChanges();
                return user;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public bool IsEmailExist(string email)
        {
            return db.Users.Any(u => u.UserName == email);
        }

        public User AddAccountUser(AddAccountAndLoginRequest userToAdd)
        {
            var user = new User();
            var account = new Account();
            user.UserName = userToAdd.Login;
            user.UserPassword = userToAdd.Password;
            user.CreatedOn = DateTime.Now;
            user.ModifiedOn = DateTime.Now;
            account.FullName = userToAdd.FullName;
            account.WorkPhone = userToAdd.WorkPhone;
            account.City = userToAdd.City;
            account.CreatedOn = DateTime.Now;
            account.ModifiedOn = DateTime.Now;
            user.Account = account;
            user.Role = db.Roles.Find(userToAdd.RoleId);
            db.Users.Add(user);
            try
            {
                db.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Role> GetRoles()
        {
            return db.Roles.ToList();
        }
    }
}