using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace Capstone.Controllers
{
    public class DonationBasketPrintingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: DonationBasketPrinting
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ShoppingList (int basketId)
        {
            var donationBasket = db.DonationBaskets.Where(c => c.BasketId == basketId).First();
            List<CartItem> basketItems = db.CartItem.Include(d => d.Product).Where(d => d.BasketId == donationBasket.BasketId).ToList();

            ViewBag.ItemsList = basketItems;
            ViewBag.OrganizationName = db.NonprofitOrganizations.Where(c => c.OrganizationId == donationBasket.OrganizationId).First().OrganizationName;

            return View(donationBasket);
        }

        public ActionResult PrintShoppingList(int id)
        {
            var shoppingList = new ActionAsPdf("ShoppingList", new { basketId = id });
            return shoppingList;
        }

        public ActionResult PackingSlip (int basketId)
        {
            var donationBasket = db.DonationBaskets.Where(c => c.BasketId == basketId).First();
            List<CartItem> basketItems = db.CartItem.Include(d => d.Product).Where(d => d.BasketId == donationBasket.BasketId).ToList();
            var organization = db.NonprofitOrganizations.Include(c => c.DropAddress).Include(c => c.ShipAddress).Where(c => c.OrganizationId == donationBasket.OrganizationId).First();

            ViewBag.ItemsList = basketItems;
            ViewBag.OrganizationName = db.NonprofitOrganizations.Where(c => c.OrganizationId == donationBasket.OrganizationId).First().OrganizationName;
            ViewBag.ShippingAddress = db.Addresses.Where(c => c.AddressId == organization.ShippingAddress).First();
            ViewBag.DropOffAddress = db.Addresses.Where(c => c.AddressId == organization.DropOffAddress).First();


            return View(donationBasket);
        }

        public ActionResult PrintPackingSlip (int id)
        {
            var packingSlip = new ActionAsPdf("PackingSlip", new { basketId = id });
            return packingSlip;
        }
    }
}