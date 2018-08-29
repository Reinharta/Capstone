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

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool Active { get; set; }
        public bool RegistrationCompleted { get; set; }

        [Display(Name = "Name")]
        public string OrganizationName { get; set; }

        [ForeignKey("ShipAddress")]
        [Display(Name = "Shipping Address")]
        public int? ShippingAddress { get; set; }
        public Address ShipAddress { get; set; }

        [ForeignKey("DropAddress")]
        [Display(Name = "Drop-off Address")]
        public int? DropOffAddress { get; set; }
        public Address DropAddress { get; set; }

        [StringLength(250, ErrorMessage = "Descriptions have a maximum of 250 characters.")]
        public string OrganizationDescription { get; set; }

        [Display(Name = "Website")]
        public string OrganizationWebsite { get; set; }
       
        [Display(Name = "Phone")]
        public string OrganizationPhone { get; set; }

        //[ForeignKey("Category")]
        public int CategoryId { get; set; }
        public OrganizationCategory Category { get; set; }



    }
}