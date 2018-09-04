using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Capstone.ViewModels;

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
            var supporter = db.Supporters.Include(c => c.Address).Where(c => c.SupporterId == donationBasket.SupporterId).First();

            PackingSlipViewModel viewModel = new PackingSlipViewModel()
            {
                SupporterFullName = supporter.FullName,
                SupporterEmail = supporter.Email,
                SupporterStreetAddress = supporter.Address.StreetAddress,
                SupporterCityStateZip = supporter.Address.City + ", " + supporter.Address.State + " " + supporter.Address.Zipcode,
                OrganizationName = organization.OrganizationName,
                OrgPhone = organization.OrganizationPhone,
                OrgShipContact = organization.ShipAddress.ContactPerson,
                OrgShipStreetAddress = organization.ShipAddress.StreetAddress,
                OrgShipCityStateZip = organization.ShipAddress.City + ", " + organization.ShipAddress.State + " " + organization.ShipAddress.Zipcode,
                OrgDropContact = organization.DropAddress.ContactPerson,
                OrgDropStreetAddress = organization.DropAddress.StreetAddress,
                OrgDropCityStateZip = organization.DropAddress.City + ", " + organization.DropAddress.State + " " + organization.DropAddress.Zipcode,
                ItemsList = db.CartItem.Include(d => d.Product).Where(d => d.BasketId == donationBasket.BasketId).ToList()
        };


            return View(viewModel);
        }

        public ActionResult PrintPackingSlip (int id)
        {
            var packingSlip = new ActionAsPdf("PackingSlip", new { basketId = id });
            return packingSlip;
        }
    }
}