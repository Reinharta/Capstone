using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class CartItem
    {
        [Key]
        public string BasketItemId { get; set; }

        [ForeignKey("DonationBasket")]
        public int BasektId { get; set; }
        public DonationBasket DonationBasket { get; set; }

        public System.DateTime DateCreated { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual DonationItem Product { get; set; }

        public int Quantity { get; set; }
    }
}