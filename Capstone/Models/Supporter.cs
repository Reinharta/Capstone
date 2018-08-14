using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class Supporter
    {
        [Key]
        public int SupporterId { get; set; }

        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey ("Address")]
        public int SupporterAddress { get; set; }
        public Address Address { get; set; }
    }
}