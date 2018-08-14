namespace JarvisEdge.DataTransferModels.User
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class UserModifyPostModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Avatar { get; set; }

        public FileStream AvatarFile { get; set; }
    }
}
