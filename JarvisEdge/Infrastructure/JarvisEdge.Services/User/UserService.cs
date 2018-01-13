namespace JarvisEdge.Services
{
    using JarvisEdge.ServiceInterfaces;

    public class UserService : IUserService
    {
        public string GetUserData()
        {
            return "User Data Returned!";
        }
    }
}
