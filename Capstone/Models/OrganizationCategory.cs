using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class OrganizationCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}