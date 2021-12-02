using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Models
{
    [Table("cart")]
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int c_id { get; set; } // PK: Id of the item

        [Required]
        public string c_creditcardNumber { get; set; } // Stores the credit card number

        [Required]
        public string c_billingAddress { get; set; } // Stores the billing address

        [Required]
        public bool c_deliver { get; set; } // Stores if it should be delivered or not (true==deliver, false==no deliver)

        // FK: Id of the user
        [ForeignKey(nameof(User))]
        public int c_user_id { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual User User { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<CartItem> CartItem { get; set; }

        public Cart()
        {
            CartItem = new HashSet<CartItem>();
        }

        public override string ToString()
        {
            string data= $"Id: {c_id}\tCreditcard: {c_creditcardNumber}\tBilling: {c_billingAddress}\tDeliver: {c_deliver}\tUser ID: {c_user_id}";
            return data;
        }
    }
}
