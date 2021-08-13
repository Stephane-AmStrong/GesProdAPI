using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class AppUser : IdentityUser
    {
        public string ImgUrl { get; set; }
        [Required]
        public string Name { get; set; }
        public string IFU { get; set; }
        public string Address { get; set; }
        public bool IsCustomer { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DisabledAt { get; set; }
    }
}
