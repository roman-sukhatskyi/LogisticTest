using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UM_LOGISTIC_V1.Models;
using UM_LOGISTIC_V1.Models.User;
using UM_LOGISTIC_V1.Request.User;
using UM_LOGISTIC_V1.Response.User;
using UM_LOGISTIC_V1.Security;
using UM_LOGISTIC_V1.Services;

namespace UM_LOGISTIC_V1.Controllers.Login
{
    public class LoginController : ApiController
    {

        private UserService userService = new UserService();
        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody]AuthenticateUserRequest request)
        {
            var authenticateUserResponse = new AuthenticateUserResponse();
            var userToAuthenticate = new User()
            {
                UserName = request.UserName,
                UserPassword = request.UserPassword
            };
            var user = userService.AuthenticateUser(userToAuthenticate);
            if(user != null)
            {
                authenticateUserResponse.Success = true;
                authenticateUserResponse.Token = TokenService.GenerateToken(request.UserName, user.RoleId);
                authenticateUserResponse.Error = "";
                authenticateUserResponse.Result = user;
                return Ok(authenticateUserResponse);
            }
            else
            {
                authenticateUserResponse.Success = false;
                authenticateUserResponse.Token = "";
                authenticateUserResponse.Error = "Неправильний логін або(і) пароль";
                authenticateUserResponse.Result = null;
                return Ok(authenticateUserResponse);
            }
        }

        [Route("api/user")]
        [HttpGet]
        public IHttpActionResult GetUser(long id, string token, string user)
        {
            var getUserResponse = new GetUserResponse();
            var IsValidToken = TokenService.ValidateToken(user, token);
            if(!IsValidToken)
            {
                getUserResponse.Success = false;
                getUserResponse.Error = "Token is not valid";
                getUserResponse.Result = null;
                return Ok(getUserResponse);
            }
            var tokenRole = TokenService.GetRole(user, token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Read, tokenRole, Section.Users);
            if (isAccessedToResource)
            {
                var userInfo = userService.GetUser(id);
                if (user != null)
                {
                    getUserResponse.Success = true;
                    getUserResponse.Error = "";
                    getUserResponse.Result = userInfo;
                    return Ok(getUserResponse);
                }
                else
                {
                    getUserResponse.Success = false;
                    getUserResponse.Error = "";
                    getUserResponse.Result = null;
                    return Ok(getUserResponse);
                }
            }
            else
            {
                getUserResponse.Success = false;
                getUserResponse.Error = "Access is denied";
                getUserResponse.Result = null;
                return Ok(getUserResponse);
            }
        }

        [Route("api/user/create")]
        [HttpPost]
        public IHttpActionResult CreateUser([FromBody]CreateUserRequest request)
        {
            var createUserResponse = new CreateUserResponse();
            var isValidToken = TokenService.ValidateToken(request.user, request.token);
            if(!isValidToken)
            {
                createUserResponse.Success = false;
                createUserResponse.Error = "Token is not valid";
                createUserResponse.Result = null;
                return Ok(createUserResponse);
            }
            var tokenRole = TokenService.GetRole(request.user, request.token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Create, tokenRole, Section.Users);
            if (isAccessedToResource)
            {
                var userToCreate = new User()
                {
                    UserName = request.UserName,
                    UserPassword = request.UserPassword,
                    AccountId = request.AccountId,
                    RoleId = request.RoleId
                };
                var isCreate = userService.CreateUser(userToCreate);
                if (isCreate)
                {
                    createUserResponse.Success = true;
                    createUserResponse.Error = "";
                    createUserResponse.Result = null;
                    return Ok(createUserResponse);
                }
                else
                {
                    createUserResponse.Success = false;
                    createUserResponse.Error = "";
                    createUserResponse.Result = null;
                    return Ok(createUserResponse);
                }
            }
            else
            {
                createUserResponse.Success = false;
                createUserResponse.Error = "Access is denied";
                createUserResponse.Result = null;
                return Ok(createUserResponse);
            }
        }

        [Route("api/user/update")]
        [HttpPost]
        public IHttpActionResult UpdateUser([FromBody]UpdateUserRequest request)
        {
            var updateUserResponse = new UpdateUserResponse();
            var isValidToken = TokenService.ValidateToken(request.user, request.token);
            if (!isValidToken)
            {
                updateUserResponse.Success = false;
                updateUserResponse.Error = "Token is not valid";
                updateUserResponse.Result = null;
                return Ok(updateUserResponse);
            }
            var tokenRole = TokenService.GetRole(request.user, request.token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Update, tokenRole, Section.Users);
            if (isAccessedToResource)
            {
                var userToUpdate = new User()
                {
                    Id = request.Id,
                    UserName = request.UserName,
                    UserPassword = request.UserPassword,
                    AccountId = request.AccountId,
                    RoleId = request.RoleId
                };
                var isUpdated = userService.UpdateUser(userToUpdate);
                if (isUpdated)
                {
                    updateUserResponse.Success = true;
                    updateUserResponse.Error = "";
                    updateUserResponse.Result = null;
                    return Ok(updateUserResponse);
                }
                else
                {
                    updateUserResponse.Success = false;
                    updateUserResponse.Error = "";
                    updateUserResponse.Result = null;
                    return Ok(updateUserResponse);
                }
            }
            else
            {
                updateUserResponse.Success = false;
                updateUserResponse.Error = "Access is denied";
                updateUserResponse.Result = null;
                return Ok(updateUserResponse);
            }
        }

        [Route("api/user/delete")]
        [HttpPost]
        public IHttpActionResult RemoveUser([FromBody]RemoveUserRequest request)
        {
            var deleteUserResponse = new DeleteUserResponse();
            var isValidToken = TokenService.ValidateToken(request.user, request.token);
            if (!isValidToken)
            {
                deleteUserResponse.Success = false;
                deleteUserResponse.Error = "Token is not valid";
                deleteUserResponse.Result = null;
                return Ok(deleteUserResponse);
            }
            var tokenRole = TokenService.GetRole(request.user, request.token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Remove, tokenRole, Section.Users);
            if (isAccessedToResource)
            {
                var userIdToDelete = request.Id;
                var isDeleted = userService.RemoveUser(userIdToDelete);
                if (isDeleted)
                {
                    deleteUserResponse.Success = true;
                    deleteUserResponse.Error = "";
                    deleteUserResponse.Result = null;
                    return Ok(deleteUserResponse);
                }
                else
                {
                    deleteUserResponse.Success = false;
                    deleteUserResponse.Error = "";
                    deleteUserResponse.Result = null;
                    return Ok(deleteUserResponse);
                }
            }
            else
            {
                deleteUserResponse.Success = false;
                deleteUserResponse.Error = "Access is denied";
                deleteUserResponse.Result = null;
                return Ok(deleteUserResponse);
            }
        }

        [Route("api/users")]
        [HttpGet]
        public IHttpActionResult GetUsersByPageAndCount(int page, int count, string token, string user)
        {
            var getUsersByPageAndCountResponse = new GetUsersByPageAndCountResponse();
            var isValidToken = TokenService.ValidateToken(user, token);
            if (!isValidToken)
            {
                getUsersByPageAndCountResponse.Success = false;
                getUsersByPageAndCountResponse.Error = "Token is not valid";
                getUsersByPageAndCountResponse.Result = null;
                return Ok(getUsersByPageAndCountResponse);
            }
            var tokenRole = TokenService.GetRole(user, token);
            var isAccessedToResource = RoleApiManager.CheckAccess(Operation.Read, tokenRole, Section.Users);
            if (isAccessedToResource)
            {
                var users = userService.GetUsers(page, count);
                if (users != null)
                {
                    getUsersByPageAndCountResponse.Success = true;
                    getUsersByPageAndCountResponse.Error = "";
                    getUsersByPageAndCountResponse.Result = users;
                    return Ok(getUsersByPageAndCountResponse);
                }
                else
                {
                    getUsersByPageAndCountResponse.Success = false;
                    getUsersByPageAndCountResponse.Error = "";
                    getUsersByPageAndCountResponse.Result = null;
                    return Ok(getUsersByPageAndCountResponse);
                }
            }
            else
            {
                getUsersByPageAndCountResponse.Success = false;
                getUsersByPageAndCountResponse.Error = "Access is denied";
                getUsersByPageAndCountResponse.Result = null;
                return Ok(getUsersByPageAndCountResponse);
            }
        }
    }
}