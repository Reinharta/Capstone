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
    public class SupportersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Supporters
        public ActionResult Index()
        {
            var supporters = db.Supporters.Include(s => s.Address);
            return View(supporters.ToList());
        }

        // GET: Supporters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supporter supporter = db.Supporters.Find(id);
            if (supporter == null)
            {
                return HttpNotFound();
            }
            return View(supporter);
        }

        // GET: Supporters/Create
        public ActionResult Create()
        {
            ViewBag.SupporterAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");
            return View();
        }

        // POST: Supporters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupporterId,FullName,Email,SupporterAddress")] Supporter supporter)
        {
            if (ModelState.IsValid)
            {
                db.Supporters.Add(supporter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupporterAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", supporter.SupporterAddress);
            return View(supporter);
        }

        // GET: Supporters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supporter supporter = db.Supporters.Find(id);
            if (supporter == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupporterAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", supporter.SupporterAddress);
            return View(supporter);
        }

        // POST: Supporters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupporterId,FullName,Email,SupporterAddress")] Supporter supporter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supporter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupporterAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", supporter.SupporterAddress);
            return View(supporter);
        }

        // GET: Supporters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supporter supporter = db.Supporters.Find(id);
            if (supporter == null)
            {
                return HttpNotFound();
            }
            return View(supporter);
        }

        // POST: Supporters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supporter supporter = db.Supporters.Find(id);
            db.Supporters.Remove(supporter);
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
