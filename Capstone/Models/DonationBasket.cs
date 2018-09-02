using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Capstone.Models;

namespace Capstone.Models
{
    public class DonationBasket
    {
        [Key]
        public int BasketId { get; set; }

        [ForeignKey("Supporter")]
        public int SupporterId { get; set; }
        public virtual Supporter Supporter { get; set; }

        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public virtual NonprofitOrganization Organization { get; set; }

        public System.DateTime DateCreated { get; set; }

        public bool BasketPending { get; set; }

        public bool Received { get; set; }

        public List<CartItem> BasketItems { get; set; }
    }
}