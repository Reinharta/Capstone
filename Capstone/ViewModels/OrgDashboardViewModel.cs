using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.ViewModels
{
    public class OrgDashboardViewModel
    {
        public int OrganizationId { get; set; }

        public string UserId { get; set; }

        public bool Active { get; set; }
        public bool RegistrationCompleted { get; set; }

        [Display(Name = "Name")]
        public string OrganizationName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Street Address")]
        public string ShipStreetAddress { get; set; }
        [Display(Name = "City")]
        public string ShipCity { get; set; }
        [Display(Name = "State")]
        public string ShipState { get; set; }
        [Display(Name = "Zipcode")]
        public string ShipZipcode { get; set; }
        [Display(Name = "Contact Person")]
        public string ShipContact { get; set; }

        [Display(Name = "Street Address")]
        public string DropStreetAddress { get; set; }
        [Display(Name = "City")]
        public string DropCity { get; set; }
        [Display(Name = "State")]
        public string DropState { get; set; }
        [Display(Name = "Zipcode")]
        public string DropZipcode { get; set; }
        [Display(Name = "Contact Person")]
        public string DropContact { get; set; }

        public string OrganizationDescription { get; set; }

        [Display(Name = "Organization Website")]
        public string OrganizationWebsite { get; set; }
    }
}