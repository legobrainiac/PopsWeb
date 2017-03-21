using System.ComponentModel.DataAnnotations;

namespace PopsWeb.Models
{
    public class PopsCollectionModel
    {
        [Key]
        public int id { get; set; }

        [Required (ErrorMessage = "The collection must have a name!")]
        [StringLength (50)]
        public string collection_name { get; set; }

        [Required (ErrorMessage = "The collection must have a Description!")]
        [StringLength (50)]
        public string collection_description { get; set; }
    }
}