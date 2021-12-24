using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge.Models
{
    [Table("t_user")]
    public class User
    {
        [Key]
        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("UserId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("Token")]
        public string Token { get; set; }
    }
}