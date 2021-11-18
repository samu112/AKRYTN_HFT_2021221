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

        public string c_creditcardNumber { get; set; } // Stores the credit card number

        [Required]
        public string c_billingAddress { get; set; } // Stores the billing address

        [Required]
        public bool c_deliver { get; set; } // Stores if it should be delivered or not (true==deliver, false==no deliver)

        [Required]
        public bool status { get; set; } // Stores if it is paid for or not(true==unpaid, false==paid)

        // FK: Id of the user
        [ForeignKey(nameof(User))]
        public int c_user_id { get; set; }

        [NotMapped]
        public virtual User User { get; set; }

        [NotMapped]
        public virtual ICollection<CartItem> CartItem { get; set; }

        public Cart()
        {
            CartItem = new HashSet<CartItem>();
        }
    }
}
