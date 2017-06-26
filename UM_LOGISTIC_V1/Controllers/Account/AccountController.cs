using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UM_LOGISTIC_V1.Request.Account;
using UM_LOGISTIC_V1.Response.Account;
using UM_LOGISTIC_V1.Security;
using UM_LOGISTIC_V1.Services;
using UM_LOGISTIC_V1.Models.Account;

namespace UM_LOGISTIC_V1.Controllers.Account
{
    public class AccountController : ApiController
    {
        private AccountService accountService = new AccountService();

        [Route("api/account")]
        [HttpGet]
        public IHttpActionResult GetAccount(long id, string token, string user)
        {
            var getAccountResponse = new GetAccountResponse();
            var IsValidToken = TokenService.ValidateToken(user, token);
            if (!IsValidToken)
            {
                getAccountResponse.Success = false;
                getAccountResponse.Error = "Token is not valid";
                getAccountResponse.Result = null;
                return Ok(getAccountResponse);
            }
            var tokenRole = TokenService.GetRole(user, token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Read, tokenRole, Section.Accounts);
            if (isAccessedToResource)
            {
                var accountInfo = accountService.GetAccount(id);
                if (user != null)
                {
                    getAccountResponse.Success = true;
                    getAccountResponse.Error = "";
                    getAccountResponse.Result = accountInfo;
                    return Ok(getAccountResponse);
                }
                else
                {
                    getAccountResponse.Success = false;
                    getAccountResponse.Error = "";
                    getAccountResponse.Result = null;
                    return Ok(getAccountResponse);
                }
            }
            else
            {
                getAccountResponse.Success = false;
                getAccountResponse.Error = "Access is denied";
                getAccountResponse.Result = null;
                return Ok(getAccountResponse);
            }
        }

        [Route("api/account/create")]
        [HttpPost]
        public IHttpActionResult CreateAccount([FromBody]CreateAccountRequest request)
        {
            var createAccountResponse = new CreateAccountResponse();
            var isValidToken = TokenService.ValidateToken(request.user, request.token);
            if (!isValidToken)
            {
                createAccountResponse.Success = false;
                createAccountResponse.Error = "Token is not valid";
                createAccountResponse.Result = null;
                return Ok(createAccountResponse);
            }
            var tokenRole = TokenService.GetRole(request.user, request.token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Create, tokenRole, Section.Accounts);
            if (isAccessedToResource)
            {
                var accountToCreate = new UM_LOGISTIC_V1.Models.Account.Account()
                {
                    FullName = request.FullName,
                    HomePhone = request.HomePhone,
                    WorkPhone = request.WorkPhone,
                    Country = request.Country,
                    Region = request.Region,
                    City = request.City,
                    Street = request.Street
                };
                var isCreate = accountService.CreateAccount(accountToCreate);
                if (isCreate)
                {
                    createAccountResponse.Success = true;
                    createAccountResponse.Error = "";
                    createAccountResponse.Result = null;
                    return Ok(createAccountResponse);
                }
                else
                {
                    createAccountResponse.Success = false;
                    createAccountResponse.Error = "";
                    createAccountResponse.Result = null;
                    return Ok(createAccountResponse);
                }
            }
            else
            {
                createAccountResponse.Success = false;
                createAccountResponse.Error = "Access is denied";
                createAccountResponse.Result = null;
                return Ok(createAccountResponse);
            }
        }

        [Route("api/account/update")]
        [HttpPost]
        public IHttpActionResult UpdateAccount([FromBody]UpdateAccountRequest request)
        {
            var updateAccountResponse = new UpdateAccountResponse();
            var isValidToken = TokenService.ValidateToken(request.user, request.token);
            if (!isValidToken)
            {
                updateAccountResponse.Success = false;
                updateAccountResponse.Error = "Token is not valid";
                updateAccountResponse.Result = null;
                return Ok(updateAccountResponse);
            }
            var tokenRole = TokenService.GetRole(request.user, request.token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Update, tokenRole, Section.Accounts);
            if (isAccessedToResource)
            {
                var accountToUpdate = new UM_LOGISTIC_V1.Models.Account.Account()
                {
                    Id = request.Id,
                    FullName = request.FullName,
                    HomePhone = request.HomePhone,
                    WorkPhone = request.WorkPhone,
                    Country = request.Country,
                    Region = request.Region,
                    City = request.City,
                    Street = request.Street
                };
                var isUpdate = accountService.UpdateAccount(accountToUpdate);
                if (isUpdate)
                {
                    updateAccountResponse.Success = true;
                    updateAccountResponse.Error = "";
                    updateAccountResponse.Result = null;
                    return Ok(updateAccountResponse);
                }
                else
                {
                    updateAccountResponse.Success = false;
                    updateAccountResponse.Error = "";
                    updateAccountResponse.Result = null;
                    return Ok(updateAccountResponse);
                }
            }
            else
            {
                updateAccountResponse.Success = false;
                updateAccountResponse.Error = "Access is denied";
                updateAccountResponse.Result = null;
                return Ok(updateAccountResponse);
            }
        }

        [Route("api/account/delete")]
        [HttpPost]
        public IHttpActionResult RemoveAccount([FromBody]RemoveAccountRequest request)
        {
            var deleteAccountResponse = new DeleteAccountResponse();
            var isValidToken = TokenService.ValidateToken(request.user, request.token);
            if (!isValidToken)
            {
                deleteAccountResponse.Success = false;
                deleteAccountResponse.Error = "Token is not valid";
                deleteAccountResponse.Result = null;
                return Ok(deleteAccountResponse);
            }
            var tokenRole = TokenService.GetRole(request.user, request.token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Remove, tokenRole, Section.Accounts);
            if (isAccessedToResource)
            {
                var accountIdToDelete = request.Id;
                var isDeleted = accountService.RemoveAccount(accountIdToDelete);
                if (isDeleted)
                {
                    deleteAccountResponse.Success = true;
                    deleteAccountResponse.Error = "";
                    deleteAccountResponse.Result = null;
                    return Ok(deleteAccountResponse);
                }
                else
                {
                    deleteAccountResponse.Success = false;
                    deleteAccountResponse.Error = "";
                    deleteAccountResponse.Result = null;
                    return Ok(deleteAccountResponse);
                }
            }
            else
            {
                deleteAccountResponse.Success = false;
                deleteAccountResponse.Error = "Access is denied";
                deleteAccountResponse.Result = null;
                return Ok(deleteAccountResponse);
            }
        }

        [Route("api/accounts")]
        [HttpGet]
        public IHttpActionResult GetAccountsByPageAndCount(int page, int count, string token, string user)
        {
            var getAccountsByPageAndCountResponse = new GetAccountsByPageAndCountResponse();
            var isValidToken = TokenService.ValidateToken(user, token);
            if (!isValidToken)
            {
                getAccountsByPageAndCountResponse.Success = false;
                getAccountsByPageAndCountResponse.Error = "Token is not valid";
                getAccountsByPageAndCountResponse.Result = null;
                return Ok(getAccountsByPageAndCountResponse);
            }
            var tokenRole = TokenService.GetRole(user, token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Read, tokenRole, Section.Accounts);
            if (isAccessedToResource)
            {
                var users = accountService.GetAccounts(page, count);
                if (users != null)
                {
                    getAccountsByPageAndCountResponse.Success = true;
                    getAccountsByPageAndCountResponse.Error = "";
                    getAccountsByPageAndCountResponse.Result = users;
                    return Ok(getAccountsByPageAndCountResponse);
                }
                else
                {
                    getAccountsByPageAndCountResponse.Success = false;
                    getAccountsByPageAndCountResponse.Error = "";
                    getAccountsByPageAndCountResponse.Result = null;
                    return Ok(getAccountsByPageAndCountResponse);
                }
            }
            else
            {
                getAccountsByPageAndCountResponse.Success = false;
                getAccountsByPageAndCountResponse.Error = "Access is denied";
                getAccountsByPageAndCountResponse.Result = null;
                return Ok(getAccountsByPageAndCountResponse);
            }
        }

        [Route("api/account/register")]
        [HttpPost]
        public IHttpActionResult RegisterAccount([FromBody]RegisterAccountRequest request)
        {
            var response = new RegisterAccountResponse();
            var isEmailExist = accountService.IsEmailExist(request.Login);
            if(isEmailExist)
            {
                response.Success = false;
                response.Error = "Такий логін вже існує в базі";
                return Ok(response);
            }
            response.Result = accountService.RegisterAccount(request);
            response.Success = response.Result != null ? true : false;
            response.Token = TokenService.GenerateToken(request.Login, response.Result != null ? response.Result.RoleId : 0);
            return Ok(response);
        }

        [Route("api/account/add")]
        [HttpPost]
        public IHttpActionResult AddUserAccount([FromBody]AddAccountAndLoginRequest request)
        {
            var response = new RegisterAccountResponse();
            var isValidToken = TokenService.ValidateToken(request.user, request.token);
            if (!isValidToken)
            {
                response.Success = false;
                response.Error = "Token is not valid";
                response.Result = null;
                return Ok(response);
            }
            var tokenRole = TokenService.GetRole(request.user, request.token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Create, tokenRole, Section.Accounts);
            if (isAccessedToResource)
            {
                var user = accountService.AddAccountUser(request);
                if (user != null)
                {
                    response.Success = true;
                    response.Error = "";
                    response.Result = user;
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Error = "";
                    response.Result = null;
                    return Ok(response);
                }
            }
            else
            {
                response.Success = false;
                response.Error = "Access is denied";
                response.Result = null;
                return Ok(response);
            }
        }

        [Route("api/roles")]
        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            var roles = accountService.GetRoles();
            return Ok(roles);

        }
    }
}