using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.ViewModels
{
    public class FinishRegistrationViewModel
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public string PhoneNumber { get; set; }

        public string ShipStreetAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZipcode { get; set; }
        public string ShipContact { get; set; }

        public string DropStreetAddress { get; set; }
        public string DropCity { get; set; }
        public string DropState { get; set; }
        public string DropZipcode { get; set; }
        public string DropContact { get; set; }

        public string OrganizationDescription { get; set; }

        public string OrganizationWebsite { get; set; }
    }
}