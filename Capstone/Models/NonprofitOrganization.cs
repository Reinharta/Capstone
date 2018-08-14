using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class NonprofitOrganization
    {
        [Key]
        public int OrganizationId { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Name")]
        public string OrganizationName { get; set; }

        [ForeignKey("ShipAddress")]
        [Display(Name = "Shipping Address")]
        public int ShippingAddress { get; set; }
        public Address ShipAddress { get; set; }

        [ForeignKey("DropAddress")]
        public int DropOffAddress { get; set; }
        public Address DropAddress { get; set; }

        [StringLength(200, ErrorMessage = "Descriptions have a maximum of 100 characters.")]
        public string OrganizationDescription { get; set; }




    }
}