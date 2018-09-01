using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class ItemCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}