using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class DonationItem
    {
        [Key]
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int ItemQuantity { get; set; }

        public string ItemDescription { get; set; }

        public int RequestingOrganization { get; set; }
        public NonprofitOrganization Organization { get; set; }

        public int MyProperty { get; set; }
    }
}