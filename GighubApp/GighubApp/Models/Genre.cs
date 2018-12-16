using System.ComponentModel.DataAnnotations;

namespace GighubApp.Models
{
    public class Genre
    {
        public byte GenreId { get; set; }

        [Required]
        [StringLength(255)]
        public string GenreName { get; set; }

    }
}