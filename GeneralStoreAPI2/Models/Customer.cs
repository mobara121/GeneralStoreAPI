﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI2.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FullName
        {
            get
            {
                return $"{FirstName}{LastName}";
            }

        }
    }
}