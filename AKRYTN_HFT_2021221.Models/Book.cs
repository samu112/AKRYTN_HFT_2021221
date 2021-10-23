using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Models
{
    [Table("book")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int b_id { get; set; } // PK: Own Id

        [Required]
        public string b_title { get; set; } // Stores the title of the book

        [Required]
        public string b_author { get; set; } // Stores the author of the book

        [Required]
        public double b_price { get; set; } // Stores the price of the book

        [Required]
        public DateTime b_releaseDate { get; set; } // Stores the release date of the book


        // FK: Id of the publisher
        [ForeignKey(nameof(Publisher))]
        public int? b_publisher_id { get; set; }

        [NotMapped]
        public virtual Publisher Publisher { get; set; }

        [NotMapped]
        public virtual ICollection<CartItem> CartItem { get; set; }
    }
}
