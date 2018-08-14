namespace JarvisEdge.ServiceInterfaces
{
    using JarvisEdge.DataTransferModels;
    using JarvisEdge.DataTransferModels.User;
    using JarvisEdge.Helpers.Jwt;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<ServiceResultModel<JwtToken>> LoginUser(UserLoginPostModel model);
        Task<ServiceResultModel<IdentityResult>> RegisterUser(UserRegisterPostModel model, IFormFile file);
        Task<ServiceResultModel<bool>> DeleteUser(string id);
        Task<ServiceResultModel<UserModifyPostModel>> ModifyUser(UserModifyPostModel model, string id, IFormFile file);
        Task<bool> LogoutUser();
        Task<ServiceResultModel<UserGetDataModel>> GetUserDetails(string id);
    }
}
