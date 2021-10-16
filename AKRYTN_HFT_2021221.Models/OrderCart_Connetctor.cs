using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Models
{
    [Table("ordercartconnector")]
    public class OrderCart_Connector
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int occ_id { get; set; } // PK: Own id


        // FK Id of the item
        [ForeignKey(nameof(CartItem))]
        public int occ_cartItem_id { get; set; }

        [NotMapped]
        public virtual Cart CartItem { get; set; }

        // FK: Id of the cart
        [ForeignKey(nameof(Cart))]
        public int occ_cart_id { get; set; }

        [NotMapped]
        public virtual Cart Cart { get; set; }
    }
}
