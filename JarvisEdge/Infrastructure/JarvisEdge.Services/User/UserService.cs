namespace JarvisEdge.Services
{
    using JarvisEdge.Data.Repositories;
    using JarvisEdge.ServiceInterfaces;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly IUowData data;

        public UserService(IUowData data)
        {
            this.data = data;
        }

        public string GetUserData()
        {
            var username = data.ApplicationUsers.All().FirstOrDefault().UserName;
            return  "Data returned for: " + username;
        }
    }
}
