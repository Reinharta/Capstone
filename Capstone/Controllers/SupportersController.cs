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
            CreateSupporterViewModel viewModel = new CreateSupporterViewModel();
            return View(viewModel);
        }

        // POST: Supporters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FullName,StreetAddress,City,State,Zipcode")] CreateSupporterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Where(c => c.Id == userId).First();
                user.UserRole = "Supporter";
                Supporter supporter = new Supporter()
                {
                    FullName = viewModel.FullName,
                    Email = user.Email,
                    SupporterAddress = AddAddressGetId(viewModel),
                    UserId = userId
                };

                db.Supporters.Add(supporter);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        public int AddAddressGetId (CreateSupporterViewModel viewModel)
        {
            Address newAddress = new Address()
            {
                ContactPerson = viewModel.FullName,
                StreetAddress = viewModel.StreetAddress,
                City = viewModel.City,
                State = viewModel.State,
                Zipcode = viewModel.Zipcode
            };

            db.Addresses.Add(newAddress);
            db.SaveChanges();
            return newAddress.AddressId;
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
