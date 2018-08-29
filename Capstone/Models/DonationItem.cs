using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class DonationItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public int ItemQuantity { get; set; }

        public string ItemSize { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public ItemCategory Category { get; set; }

        public string ItemDescription { get; set; }

        public int RequestingOrganizationId { get; set; }
        public NonprofitOrganization Organization { get; set; }

        public int MyProperty { get; set; }
    }
}