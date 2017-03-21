using System.ComponentModel.DataAnnotations;

namespace PopsWeb.Models
{
    public class PopsRating
    {
        [Key]
        public int id { get; set; }
        public int id_user { get; set; }
        public int id_pop { get; set; }

        [MaxLength(5)]
        [MinLength (0)]
        public int rating_pos { get; set; } //Suppose to be pop but typo
    }
}