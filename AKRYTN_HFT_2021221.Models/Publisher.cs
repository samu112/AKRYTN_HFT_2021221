using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Models
{

    [Table("publisher")]
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int p_id { get; set; } // PK: Own Id

        [Required]
        public string p_name { get; set; } // Stores the name of the publisher

        [Required]
        public string p_address { get; set; } // Stores the addres of the publisher


        public string? p_website { get; set; } // Stores the website of the publisher

        [Required]
        public string p_email { get; set; } // Stores the email of the publisher

        [NotMapped]
        public virtual ICollection<Book> Books { get; set; }
        // IEnumerable, ICollection, IList, IDictionary

        public Publisher()
        {
            Books = new HashSet<Book>();
        }
    }
}
