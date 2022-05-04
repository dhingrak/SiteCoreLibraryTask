using System.ComponentModel.DataAnnotations;

namespace SiteCoreLibraryTask.Models
{
    public class Rental
    {
       


        [Required]
        public string UserEmailId { get; set; }
        [Required]
       
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Required]
        public int BookId { get; set; }
       
    }
}
