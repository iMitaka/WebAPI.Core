namespace JarvisEdge.DataTransferModels.User
{
    using System;

    public class UserGetDataModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Avatar { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
