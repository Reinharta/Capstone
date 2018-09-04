using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Models;

namespace Capstone.ViewModels
{
    public class SuppporterBasketIndexViewModel
    {
        public int SupporterId { get; set; }
        public Supporter Supporter { get; set; }

        public IEnumerable<DonationBasket> BasketsList { get; set; }
    }
}