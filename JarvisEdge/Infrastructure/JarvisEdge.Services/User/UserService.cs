namespace JarvisEdge.Services
{
    using JarvisEdge.Constants.User;
    using JarvisEdge.Data.Repositories;
    using JarvisEdge.DataTransferModels;
    using JarvisEdge.DataTransferModels.User;
    using JarvisEdge.Helpers.Jwt;
    using JarvisEdge.Models;
    using JarvisEdge.ServiceInterfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class UserService : IUserService
    {
        private const int tokenExpiryMinutes = 60;
        private const int passwordMinimumLenght = 4;
        private const string userAvatarsFolderName = "Avatar";

        private readonly IUowData data;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> rolesManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFileService fileService;

        public UserService(IUowData data,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> rolesManager,
            IHostingEnvironment hostingEnvironment,
            IFileService fileService
            )
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.rolesManager = rolesManager;
            this.hostingEnvironment = hostingEnvironment;
            this.fileService = fileService;
        }

        public async Task<ServiceResultModel<JwtToken>> LoginUser(UserLoginPostModel model)
        {
            var result = new ServiceResultModel<JwtToken>();

            var loginResult = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
            if (loginResult.Succeeded)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (!user.Deleted)
                {
                    result.DataResult = GenerateTokenForUser(user.UserName);
                }
                else
                {
                    result.Error = true;
                    result.ErrorMessage = UserMessages.InvalidLoginData();
                }

            }
            else if (loginResult.IsLockedOut)
            {
                result.Error = true;
                result.ErrorMessage = UserMessages.LockedAccount();
            }
            else
            {
                result.Error = true;
                result.ErrorMessage = UserMessages.InvalidLoginData();
            }

            return result;
        }

        public async Task<ServiceResultModel<IdentityResult>> RegisterUser(UserRegisterPostModel model, IFormFile file)
        {

            var result = new ServiceResultModel<IdentityResult>();

            var errorMessage = ValidateUserData(model.Email, model.Username, null);

            if (errorMessage != null)
            {
                result.Error = true;
                result.ErrorMessage = errorMessage;
            }
            else
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FullName = model.FullName,
                    CreatedAt = DateTime.Now,
                    Phone = model.Phone,
                    Address = model.Address,
                    Avatar = model.Avatar != null ? model.Avatar : await fileService.GenerateFileSource(file, userAvatarsFolderName, model.Username.ToLower()),
                    City = model.City,
                    Country = model.Country,
                    PostalCode = model.PostalCode,
                };

                result.DataResult = await userManager.CreateAsync(user, model.Password);
                result.SuccessMessage = UserMessages.AccountSuccessfullyCreate(model.Username);
            }

            return result;
        }

        public async Task<ServiceResultModel<UserGetDataModel>> GetUserDetails(string id)
        {
            var result = new ServiceResultModel<UserGetDataModel>();

            var user = await userManager.FindByIdAsync(id);

            if (user != null && !user.Deleted)
            {
                result.DataResult = new UserGetDataModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    CreatedAt = user.CreatedAt,
                    Avatar = user.Avatar,
                    Username = user.UserName,
                    Email = user.Email
                };
            }
            else
            {
                result.Error = true;
                result.ErrorMessage = UserMessages.UserNotExist();
            }

            return result;
        }

        public async Task<ServiceResultModel<UserModifyPostModel>> ModifyUser(UserModifyPostModel model, string id, IFormFile file)
        {
            var result = new ServiceResultModel<UserModifyPostModel>();

            var user = await userManager.FindByIdAsync(id);

            if (user != null && !user.Deleted)
            {
                var errorMessage = ValidateUserData(model.Email, model.Username, user.UserName);

                if (errorMessage != null)
                {
                    result.Error = true;
                    result.ErrorMessage = errorMessage;
                }
                else
                {
                    if (model.Password != null && model.Password.Length >= passwordMinimumLenght)
                    {
                        var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);
                        if (!isPasswordValid)
                        {
                            var token = await userManager.GeneratePasswordResetTokenAsync(user);
                            await userManager.ResetPasswordAsync(user, token, model.Password);
                        }
                    }

                    user.UpdatedAt = DateTime.Now;

                    user.UserName = model.Username;
                    user.Email = model.Email;
                    user.FullName = model.FullName;
                    user.Phone = model.Phone;
                    user.Address = model.Address;
                    user.Avatar = model.AvatarFile != null ?
                        model.Avatar : await fileService.GenerateFileSource(file, userAvatarsFolderName, model.Username.ToLower());
                    user.City = model.City;
                    user.Country = model.Country;
                    user.PostalCode = model.PostalCode;

                    var updateResult = await userManager.UpdateAsync(user);

                    if (updateResult.Succeeded)
                    {
                        result.DataResult = model;
                    }
                    else
                    {
                        result.Error = true;
                        result.ErrorMessage = UserMessages.ModifyUserError();
                    }
                }
            }
            else
            {
                result.Error = true;
                result.ErrorMessage = UserMessages.UserNotExist();
            }

            return result;
        }

        public async Task<bool> LogoutUser()
        {
            await signInManager.SignOutAsync();
            return true;
        }

        public async Task<ServiceResultModel<bool>> DeleteUser(string id)
        {
            var result = new ServiceResultModel<bool>();

            var user = await userManager.FindByIdAsync(id);

            if (id.ToLower() == user.Id.ToLower())
            {
                if (user != null)
                {
                    user.Deleted = true;
                    user.UserName = user.UserName + " " + DateTime.Now.Ticks;
                    user.Email = user.Email + " " + DateTime.Now.Ticks;
                    data.SaveChanges();
                }
                else
                {
                    result.Error = true;
                    result.ErrorMessage = UserMessages.UserNotExist();
                }
            }
            else
            {
                result.Error = true;
                result.ErrorMessage = UserMessages.ForbiddenDelete();
            }

            return result;
        }

        private string ValidateUserData(string email, string username, string currentUserUsername)
        {
            if (email != null && email.Length >= 1 && CheckForExistingEmail(email, currentUserUsername))
            {
                return UserMessages.EmailAlreadyExist();
            }
            else if (username != null && username.Length >= 1 && CheckForExistingUsername(username, currentUserUsername))
            {
                return UserMessages.UsernameAlreadyTaken();
            }

            return null;
        }

        private async void AddUserToRole(ApplicationUser user, string role)
        {
            var isRoleExist = await rolesManager.RoleExistsAsync(role);
            if (!isRoleExist)
            {
                var roleResult = await rolesManager.CreateAsync(new IdentityRole() { Name = role });
                if (roleResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
            else
            {
                await userManager.AddToRoleAsync(user, role);
            }

            data.SaveChanges();
        }

        private JwtToken GenerateTokenForUser(string username)
        {
            return new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(JwtConstants.GetSigningKey()))
                                .AddSubject(username)
                                .AddIssuer(JwtConstants.GetIssuer())
                                .AddAudience(JwtConstants.GetAudience())
                                .AddClaim(ClaimTypes.Name, username)
                                .AddExpiry(tokenExpiryMinutes)
                                .Build();
        }

        private bool CheckForExistingEmail(string email, string username)
        {
            var result = data.ApplicationUsers.All().Any(x => x.Email.ToLower() == email.ToLower() && !x.Deleted);
            if (username != null)
            {
                result = data.ApplicationUsers.All().Any(x => x.Email.ToLower() == email.ToLower() && x.UserName != username && !x.Deleted);
            }
            return result;
        }

        private bool CheckForExistingUsername(string username, string currentModifiedUsername)
        {
            var result = data.ApplicationUsers.All().Any(x => x.UserName.ToLower() == username.ToLower() && !x.Deleted);
            if (currentModifiedUsername != null)
            {
                result = data.ApplicationUsers.All().Any(x => x.UserName.ToLower() == username.ToLower() && x.UserName != currentModifiedUsername && !x.Deleted);
            }
            return result;
        }
    }
}
