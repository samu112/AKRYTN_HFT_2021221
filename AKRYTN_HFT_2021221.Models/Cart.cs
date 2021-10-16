using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Models
{
    [Table("cart")]
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int c_Id { get; set; } // PK: Id of the item

        [Required]
        public string c_creditcardNumber { get; set; } // Stores the credit card number

        [Required]
        public string c_billingAddress { get; set; } // Stores the billing address

        [Required]
        public bool c_deliver { get; set; } // Stores if it should be delivered or not (true==deliver, false==no deliver)

        [NotMapped]
        public virtual ICollection<OrderCart_Connector> Conn { get; }
        [NotMapped]
        public virtual User User { get; set; }
    }
}
