using AKRYTN_HFT_2021221.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKRYTN_HFT_2021221.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int u_Id { get; set; } // PK: Own Id

        [Required]
        [MaxLength(100)]
        public string u_Name { get; set; } // Stores the full name of the user

        [Required]
        public DateTime u_RegDate { get; set; } // Stores the registration date of the user

        
        public string? u_Address { get; set; } // Stores the address of the user

        [Required]
        [MaxLength(320)]
        public string u_Email { get; set; } // Stores the email address of the user

        // FK: Id of the cart
        [ForeignKey(nameof(Cart))]
        public int u_CartId { get; set; } 

        [NotMapped]
        public virtual Cart Cart { get; set; } 

    }
}
