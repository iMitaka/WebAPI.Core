namespace JarvisEdge.Models
{
    using JarvisEdge.Models.EntityHelpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : BaseUserEntity
    {
        public override string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public override string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Avatar { get; set; }
    }
}
