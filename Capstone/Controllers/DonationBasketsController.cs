using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;

namespace Capstone.Controllers
{
    public class DonationBasketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DonationBaskets
        public ActionResult Index(int supporterId, int organizationId)
        {
            if (!db.DonationBaskets.Any(c => c.SupporterId == supporterId))
            {
                CreateBasket(organizationId, supporterId);
            }

            var donationBasket = db.DonationBaskets.Include(d => d.BasketItems).Include(d => d.Organization).Include(d => d.Supporter).Where(c => c.SupporterId == supporterId);
         
            return View(donationBasket);
        }

        public ActionResult AddToBasket(CartItem cartItem)
        {
            var supporter = db.Supporters.Where(c => c.SupporterId == cartItem.SupporterId).First();
            DonationBasket basket = null;

            if (!db.DonationBaskets.Any(c => c.SupporterId == supporter.SupporterId))
            {
                basket = CreateBasket(cartItem.Product.RequestingOrganizationId, supporter.SupporterId);
            }
            if (db.DonationBaskets.Any(c => c.SupporterId == supporter.SupporterId))
            {
                basket = db.DonationBaskets.Where(c => c.SupporterId == supporter.SupporterId).First();
            }

            var list = basket.BasketItems;
            list.Add(cartItem);
            db.Entry(basket).State = EntityState.Modified;
            db.SaveChanges();

            return Index(basket.SupporterId, basket.OrganizationId);
        }


        public DonationBasket CreateBasket(int organizationId, int supporterId)
        {
            DonationBasket newBasket = new DonationBasket()
            {
                SupporterId = supporterId,
                OrganizationId = organizationId,
                DateCreated = System.DateTime.Today,

            };
            
                db.DonationBaskets.Add(newBasket);
                db.SaveChanges();
            

            return newBasket;
        }
        // GET: DonationBaskets/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DonationBasket donationBasket = db.DonationBaskets.Find(id);
        //    if (donationBasket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(donationBasket);
        //}

        // GET: DonationBaskets/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationId = new SelectList(db.NonprofitOrganizations, "OrganizationId", "UserId");
            ViewBag.SupporterId = new SelectList(db.Supporters, "SupporterId", "FullName");
            return View();
        }

        // POST: DonationBaskets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BasketId,SupporterId,OrganizationId,DateCreated,BasketPending,Received")] DonationBasket donationBasket)
        {
            if (ModelState.IsValid)
            {
                db.DonationBaskets.Add(donationBasket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationId = new SelectList(db.NonprofitOrganizations, "OrganizationId", "UserId", donationBasket.OrganizationId);
            ViewBag.SupporterId = new SelectList(db.Supporters, "SupporterId", "FullName", donationBasket.SupporterId);
            return View(donationBasket);
        }

        // GET: DonationBaskets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationBasket donationBasket = db.DonationBaskets.Find(id);
            if (donationBasket == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(db.NonprofitOrganizations, "OrganizationId", "UserId", donationBasket.OrganizationId);
            ViewBag.SupporterId = new SelectList(db.Supporters, "SupporterId", "FullName", donationBasket.SupporterId);
            return View(donationBasket);
        }

        // POST: DonationBaskets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BasketId,SupporterId,OrganizationId,DateCreated,BasketPending,Received")] DonationBasket donationBasket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donationBasket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(db.NonprofitOrganizations, "OrganizationId", "UserId", donationBasket.OrganizationId);
            ViewBag.SupporterId = new SelectList(db.Supporters, "SupporterId", "FullName", donationBasket.SupporterId);
            return View(donationBasket);
        }

        // GET: DonationBaskets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationBasket donationBasket = db.DonationBaskets.Find(id);
            if (donationBasket == null)
            {
                return HttpNotFound();
            }
            return View(donationBasket);
        }

        // POST: DonationBaskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonationBasket donationBasket = db.DonationBaskets.Find(id);
            db.DonationBaskets.Remove(donationBasket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
