﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
