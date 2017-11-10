using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySPA.Core.Entities
{
    [Table("Magazines")]
    public class Magazine
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a language.")]
        public string Language { get; set; }
        public DateTime? Published { get; set; }

        [Required(ErrorMessage = "Please enter a publishing house.")]
        public string PubHouse { get; set; }
        public double Cost { get; set; }
    }
}
