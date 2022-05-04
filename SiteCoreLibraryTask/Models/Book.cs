using System.ComponentModel.DataAnnotations;

namespace SiteCoreLibraryTask.Models
{
    public class Book
    {
       
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }       
        [Required]
        public string FirstAuthorName { get; set; }
        [Required]
        public string SecondAuthorName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int Price { get; set; }
        public Boolean IsAvailable { get; set; } = false;

        [Required]
        public int Quantity { get; set; }

    }
}
