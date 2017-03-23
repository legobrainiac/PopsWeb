using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace PopsWeb.Models
{
    public class UsersPopsModel
    {
        [Key]
        public int id { get; set; }
        public int id_user { get; set; }
        public int id_pop { get; set; }
    }
}