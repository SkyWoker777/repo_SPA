using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibrarySPA.Core.Entities
{
    [Table("Newspapers")]
    public class Newspaper
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        public DateTime? PostedOn { get; set; }

        [Required(ErrorMessage = "Please enter a publishing house.")]
        public string PubHouse { get; set; }
        public double Cost { get; set; }
    }
}
