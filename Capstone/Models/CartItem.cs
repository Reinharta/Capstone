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
        public int Id { get; set; }

        [ForeignKey("DonationBasket")]
        public int? BasketId { get; set; }
        public DonationBasket DonationBasket { get; set; }

        [ForeignKey("Supporter")]
        public int SupporterId { get; set; }
        public virtual Supporter Supporter { get; set; }

        public System.DateTime DateCreated { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual DonationItem Product { get; set; }

        public int Quantity { get; set; }
    }
}