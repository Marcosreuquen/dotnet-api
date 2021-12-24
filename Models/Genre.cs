using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge.Models
{
    [Table("t_genre")]
    public class Genre
    {
        [Key]
        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("GenreId")]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("Img")]
        public string Img { get; set; }
        
        [ForeignKey("MoviesId")]
        public int[] MoviesId { get; set; }
    }
}