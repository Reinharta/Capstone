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
    public class DonationItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DonationItems
        public ActionResult Index()
        {
            var donationItem = db.DonationItem.Include(d => d.Category);
            return View(donationItem.ToList());
        }

        // GET: DonationItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationItem donationItem = db.DonationItem.Find(id);
            if (donationItem == null)
            {
                return HttpNotFound();
            }
            return View(donationItem);
        }

        // GET: DonationItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.itemCategories, "CategoryId", "Name");
            return View();
        }

        // POST: DonationItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemName,ItemQuantity,ItemSize,CategoryId,ItemDescription,RequestingOrganizationId,MyProperty")] DonationItem donationItem)
        {
            if (ModelState.IsValid)
            {
                db.DonationItem.Add(donationItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.itemCategories, "CategoryId", "Name", donationItem.CategoryId);
            return View(donationItem);
        }

        // GET: DonationItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationItem donationItem = db.DonationItem.Find(id);
            if (donationItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.itemCategories, "CategoryId", "Name", donationItem.CategoryId);
            return View(donationItem);
        }

        // POST: DonationItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemName,ItemQuantity,ItemSize,CategoryId,ItemDescription,RequestingOrganizationId,MyProperty")] DonationItem donationItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donationItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.itemCategories, "CategoryId", "Name", donationItem.CategoryId);
            return View(donationItem);
        }

        // GET: DonationItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationItem donationItem = db.DonationItem.Find(id);
            if (donationItem == null)
            {
                return HttpNotFound();
            }
            return View(donationItem);
        }

        // POST: DonationItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonationItem donationItem = db.DonationItem.Find(id);
            db.DonationItem.Remove(donationItem);
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
