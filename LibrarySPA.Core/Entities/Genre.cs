using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibrarySPA.Core.Entities
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        //public virtual IEnumerable<Book> Books { get; set; }
    }
}
