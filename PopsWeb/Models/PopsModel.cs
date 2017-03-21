using System.ComponentModel.DataAnnotations;

namespace PopsWeb.Models
{
    public class PopsModel
    {
        [Key]
        public int id { get; set; }

        [Required (ErrorMessage = "The figurine must have a name!")]
        [StringLength (50)]
        public string pop_name { get; set; }

        [Required (ErrorMessage = "The figurine must have a Description!")]
        [StringLength (50)]
        public string pop_description { get; set; }

        public int pop_collection_id { get; set; }

        [DataType (DataType.Currency)]
        public decimal price { get; set; }
    }
}