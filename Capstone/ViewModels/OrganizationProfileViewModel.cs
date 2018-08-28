using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Capstone.Models;

namespace Capstone.ViewModels
{
    public class OrganizationProfileViewModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public NonprofitOrganization Organization { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        [ForeignKey("ShippingAddress")]
        public int ShipAddressId { get; set; }
        public Address ShippingAddress { get; set; }

        [ForeignKey("DropOffAddress")]
        public int DropAddressId { get; set; }
        public Address DropOffAddress { get; set; }



    }
}