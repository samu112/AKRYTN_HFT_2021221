﻿using System;
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
    [Table("cartitem")]
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ci_id { get; set; } // PK: Own id

        [Required]
        public int ci_quantity { get; set; } // How many the user want to buy from that book

        // FK: Id of the book
        [ForeignKey(nameof(Book))]

        public int ci_book_id { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual Book Book { get; set; }

        // FK: Id of the book
        [ForeignKey(nameof(Cart))]
        public int ci_cart_id { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual Cart Cart { get; set; }

        public override string ToString()
        {
            string data = $"Id: {ci_id}\tBook ID: {ci_book_id}\tCart ID: {ci_cart_id}\tQuantity: {ci_quantity}";
            return data;
        }
    }
}
