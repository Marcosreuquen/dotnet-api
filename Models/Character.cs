using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge.Models
{
    [Table("t_character")]
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("CharacterId")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("Img")]
        public string Img { get; set; }

        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "This field is non-nullable.")]
        [Column("History")]
        public string History { get; set; }

        [ForeignKey("MoviesId")]
        // [Column("MoviesId")]
        public int[] MoviesId { get; set; }
    }
}