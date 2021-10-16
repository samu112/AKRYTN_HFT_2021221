using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Models
{
    [Table("cartitem")]
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ci_id { get; set; } // PK: Own id

        public int ci_quantity { get; set; } // How many the user want to buy from that book

        // FK: Id of the book
        [ForeignKey(nameof(Book))]
        public int ci_book_id { get; set; }

        [NotMapped]
        public virtual Book Book { get; set; }

        [NotMapped]
        public virtual ICollection<OrderCart_Connector> Conn { get; }

    }
}
