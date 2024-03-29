﻿using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AKRYTN_HFT_2021221.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int u_id { get; set; } // PK: Own Id

        [Required]
        [MaxLength(100)]
        public string u_name { get; set; } // Stores the full name of the user

        [Required]
        public DateTime u_regDate { get; set; } // Stores the registration date of the user

        [Required]
        public string u_address { get; set; } // Stores the address of the user

        [Required]
        [MaxLength(320)]
        public string u_email { get; set; } // Stores the email address of the user

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual Cart Cart { get; set; }

        public override string ToString()
        {
            string data= $"Id: {u_id}\tName: {u_name}\tRegistration Date: {u_regDate}\tEmail: {u_email}\tAddress: {u_address} ";
            return data;
        }
    }
}
