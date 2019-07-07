using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InspiritoTestWebApi.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
