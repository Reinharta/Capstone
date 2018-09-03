using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Models;

namespace Capstone.ViewModels
{
    public class OrgRequestItemsListViewModel
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public string OrganizationDescription { get; set; }

        public int? SupporterId { get; set; }

        public IEnumerable<DonationItem> ItemsList { get; set; }
      
    }
}