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
            var users = data.ApplicationUsers.All().ToList();
            return "User Data Returned!";
        }
    }
}
