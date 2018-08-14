namespace JarvisEdge.DataTransferModels.User
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class UserRegisterPostModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
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
