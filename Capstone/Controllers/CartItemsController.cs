using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class CartItemsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: CartItems
        public ActionResult Index()
        {
            return View();
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

      
        public ActionResult Create(int donationItemId, int supporterId, int quantity)
        {
            var donationItem = db.DonationItem.Where(c => c.ItemId == donationItemId).First();

            CartItem item = new CartItem()
            {
                SupporterId = supporterId,
                Supporter = db.Supporters.Where(c => c.SupporterId == supporterId).First(),
                DateCreated = System.DateTime.Today,
                ProductId = donationItem.ItemId,
                Product = donationItem,
                Quantity = quantity
            };

            db.CartItem.Add(item);
            db.SaveChanges();

            return RedirectToAction("AddToBasket", "DonationBaskets", item);
        }


        // GET: CartItems/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartItems/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartItems/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
