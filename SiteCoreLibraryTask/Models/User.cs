using System.ComponentModel.DataAnnotations;

namespace SiteCoreLibraryTask.Models
{
    public class User
    {

        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

       
    }
}
