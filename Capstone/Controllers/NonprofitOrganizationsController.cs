using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using Capstone.ViewModels;
using Microsoft.AspNet.Identity;

namespace Capstone.Controllers
{
    public class NonprofitOrganizationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Dashboard(NonprofitOrganization organization)
        {
            if(organization == null)
            {
                ApplicationUser user = db.Users.Where(c => c.Id == User.Identity.GetUserId()).First();
                organization = db.NonprofitOrganizations.Where(c => c.UserId == user.Id).First();
            }
            return View("OrgDashboard", organization);
        }

        // GET: NonprofitOrganizations
        public ActionResult Index()
        {
            var nonprofitOrganizations = db.NonprofitOrganizations.Include(n => n.DropAddress).Include(n => n.ShipAddress);
            return View(nonprofitOrganizations.ToList());
        }

        // GET: NonprofitOrganizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonprofitOrganization nonprofitOrganization = db.NonprofitOrganizations.Find(id);
            if (nonprofitOrganization == null)
            {
                return HttpNotFound();
            }
            return View(nonprofitOrganization);
        }

        public ActionResult PreCreate(ApplicationUser user)
        {
            var organization = new NonprofitOrganization()
            {
                OrganizationName = user.OrganizationName,
                UserId = user.Id,
                User = user,
                Active = false,
                RegistrationCompleted = false,
                ShipAddress = null,
                DropAddress = null
            };
            db.NonprofitOrganizations.Add(organization);
            db.SaveChanges();
            return View("RegistrationLanding");
        }

        // GET: NonprofitOrganizations/Create
        public ActionResult Create()
        {
            ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");
            ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");
            return View();
        }

        // POST: NonprofitOrganizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrganizationId,Active,OrganizationName,ShippingAddress,DropOffAddress,OrganizationDescription,OrganizationWebsite,OrganizationPhone")] NonprofitOrganization nonprofitOrganization)
        {
            if (ModelState.IsValid)
            {
                db.NonprofitOrganizations.Add(nonprofitOrganization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", nonprofitOrganization.DropOffAddress);
            ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", nonprofitOrganization.ShippingAddress);
            return View(nonprofitOrganization);
        }


        public ActionResult FinishRegistration(int id)
        {
            //FinishRegistrationViewModel viewModel = new FinishRegistrationViewModel()
            //{
            //    OrganizationId = id,
            //    OrganizationName = db.NonprofitOrganizations.Where(c => c.OrganizationId == id).First().OrganizationName
            //};

            ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");
            ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");
            return View();
        }

        // POST: NonprofitOrganizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishRegistration([Bind(Include = "OrganizationId,Active,OrganizationName,ShippingAddress,DropOffAddress,OrganizationDescription,OrganizationWebsite,OrganizationPhone")] NonprofitOrganization nonprofitOrganization)
        {
            if (ModelState.IsValid)
            {
                db.NonprofitOrganizations.Add(nonprofitOrganization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", nonprofitOrganization.DropOffAddress);
            ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", nonprofitOrganization.ShippingAddress);
            return View(nonprofitOrganization);
        }


        // GET: NonprofitOrganizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonprofitOrganization nonprofitOrganization = db.NonprofitOrganizations.Find(id);
            if (nonprofitOrganization == null)
            {
                return HttpNotFound();
            }
            ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", nonprofitOrganization.DropOffAddress);
            ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", nonprofitOrganization.ShippingAddress);
            return View(nonprofitOrganization);
        }

        // POST: NonprofitOrganizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrganizationId,Active,OrganizationName,ShippingAddress,DropOffAddress,OrganizationDescription,OrganizationWebsite,OrganizationPhone")] NonprofitOrganization nonprofitOrganization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nonprofitOrganization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", nonprofitOrganization.DropOffAddress);
            ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", nonprofitOrganization.ShippingAddress);
            return View(nonprofitOrganization);
        }

        // GET: NonprofitOrganizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonprofitOrganization nonprofitOrganization = db.NonprofitOrganizations.Find(id);
            if (nonprofitOrganization == null)
            {
                return HttpNotFound();
            }
            return View(nonprofitOrganization);
        }

        // POST: NonprofitOrganizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NonprofitOrganization nonprofitOrganization = db.NonprofitOrganizations.Find(id);
            db.NonprofitOrganizations.Remove(nonprofitOrganization);
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
