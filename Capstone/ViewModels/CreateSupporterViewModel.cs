using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.ViewModels
{
    public class CreateSupporterViewModel
    {
        [Display(Name = "First & Last Name")]
        public string FullName { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }
    }
}