using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge.Models
{
    [Table("t_movie")]
    public class Movie
    {
        [Key]
        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("MovieId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is non-nullable")]
        [Column("Img")]
        public string Img { get; set; }

        [Required(ErrorMessage = "This field is non-nullable")]
        [Column("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is non-nullable")]
        [Column("DateOfCreation")]
        public DateTime DateOfCreation { get; set; }
        
        [Required(ErrorMessage = "This field is non-nullable")]
        [Column("Calification")]
        public int Calification { get; set; }

        [ForeignKey("CharactersId")]
        public int[] CharactersId { get; set; }

        [ForeignKey("GenresId")]
        public int[] GenresId { get; set; }
    }
}