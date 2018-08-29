using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;

namespace Capstone.ViewModels
{
    public class BeginRegistrationViewModel
    {
        public string OrganizationName { get; set; }
        public string OrganizationWebsite { get; set; }
        public string OrganizationPhone { get; set; }
        public int CategoryId { get; set; }
        public OrganizationCategory Category { get; set; }

        public SelectList CategoriesSelectList { get; set; }
        public List<OrganizationCategory> Categories { get; set; }
    }
}