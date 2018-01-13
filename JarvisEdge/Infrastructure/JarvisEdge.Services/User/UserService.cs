namespace JarvisEdge.Services
{
    using JarvisEdge.Data;
    using JarvisEdge.ServiceInterfaces;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly JarvisDbContext data;

        public UserService(JarvisDbContext data)
        {
            this.data = data;
        }

        public string GetUserData()
        {
            var users = this.data.Users.ToList();
            return "User Data Returned!";
        }
    }
}
