using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.ViewModels
{
    public class PackingSlipViewModel
    {
        public string SupporterFullName { get; set; }
        public string SupporterEmail { get; set; }
        public string SupporterStreetAddress { get; set; }
        public string SupporterCityStateZip { get; set; }

        public string OrganizationName { get; set; }
        public string OrgPhone { get; set; }
        public string OrgShipContact { get; set; }
        public string OrgShipStreetAddress { get; set; }
        public string OrgShipCityStateZip { get; set; }

        public string OrgDropContact { get; set; }
        public string OrgDropStreetAddress { get; set; }
        public string OrgDropCityStateZip { get; set; }

        public IEnumerable<CartItem> ItemsList { get; set; }
    }
}