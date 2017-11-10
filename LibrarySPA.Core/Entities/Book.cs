using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibrarySPA.Core.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter authors.")]
        public string Author { get; set; }
        public int? Year { get; set; }

        [Required(ErrorMessage = "Please enter a publishing house.")]
        public string PubHouse { get; set; }
        public int? PageCount { get; set; }
        public double? Cost { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ForeignKey("Genre")]
        public int Genre_ID { get; set; }

        [Required(ErrorMessage = "Please specify a genre.")]
        public Genre Genre { get; set; }

    }
}
