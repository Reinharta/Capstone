﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using Capstone.ViewModels;
using GoogleMaps.LocationServices;
using Microsoft.AspNet.Identity;

namespace Capstone.Controllers
{
    public class NonprofitOrganizationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Dashboard(int? organizationId)
        {
            if(organizationId == null)
            {
                var userId = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Where(c => c.Id == userId).First();
                var organization = db.NonprofitOrganizations.Where(c => c.UserId == user.Id).Include(d => d.ShipAddress).Include(d => d.DropAddress).First();
                return View("OrgDashboard", organization);
            }
            else if(organizationId != 0)
            {
                var organization = db.NonprofitOrganizations.Where(c => c.OrganizationId == organizationId).Include(d => d.ShipAddress).Include(d => d.DropAddress).First();
                return View("OrgDashboard", organization);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult OrganizationProfile (int organizationId)
        {
            NonprofitOrganization organization = db.NonprofitOrganizations.Where(c => c.OrganizationId == organizationId).Include(d => d.ShipAddress).Include(d => d.DropAddress).First();
            ApplicationUser user = db.Users.Where(c => c.Id == organization.UserId).First();

            OrganizationProfileViewModel viewModel = new OrganizationProfileViewModel()
            {
                Organization = organization,
                Phone = organization.OrganizationPhone,
                Email = user.Email,
                Website = organization.OrganizationWebsite,
                ShippingAddress = organization.ShipAddress,
                DropOffAddress = organization.DropAddress
            };
            ViewBag.GoogleKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GoogleMapsApiKey"];

            return View(viewModel);
        }

        // GET: NonprofitOrganizations
        public ActionResult Index(string searchString, string category)
        {
            var nonprofitOrganizations = db.NonprofitOrganizations.Include(d => d.ShipAddress).Include(d => d.DropAddress).Where(c => c.RegistrationCompleted == true);

            if (!String.IsNullOrEmpty(category) && !String.IsNullOrEmpty(searchString))
            {
                int categoryId = Int32.Parse(category);
                nonprofitOrganizations = nonprofitOrganizations.Where(c => c.RegistrationCompleted == true).Where(c => c.CategoryId == categoryId && c.DropAddress.Zipcode.Contains(searchString) || c.ShipAddress.Zipcode.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(category))
            {
                int categoryId = Int32.Parse(category);
                nonprofitOrganizations = nonprofitOrganizations.Where(c => c.RegistrationCompleted == true).Where(c => c.CategoryId == categoryId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                nonprofitOrganizations = nonprofitOrganizations.Where(c => c.RegistrationCompleted == true).Where(s => s.DropAddress.Zipcode.Contains(searchString) || s.ShipAddress.Zipcode.Contains(searchString));                
            }

            ViewBag.Categories = db.OrganizationCategories.Distinct();
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

        //[ValidateAntiForgeryToken]
        //public ActionResult PreCreate(ApplicationUser user)
        //{
        //    var organization = new NonprofitOrganization()
        //    {
        //        OrganizationName = user.OrganizationName,
        //        UserId = user.Id,
        //        User = user,
        //        Active = false,
        //        RegistrationCompleted = false,
        //        ShipAddress = null,
        //        DropAddress = null
        //    };

        //    if (ModelState.IsValid)
        //    {
        //        db.NonprofitOrganizations.Add(organization);
        //        db.SaveChanges(); //THROWING EXCEPTION HERE
        //        return View("RegistrationLanding");
        //    }

        //    return View("RegisterOrganization", "Account", organization);
        //}

        // GET: NonprofitOrganizations/Create
        public ActionResult Create()
        {
            //ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");
            //ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");

            BeginRegistrationViewModel viewModel = new BeginRegistrationViewModel()
            {
                Categories = db.OrganizationCategories.Distinct().ToList()
            };
            return View(viewModel);
        }

        // POST: NonprofitOrganizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrganizationName,OrganizationWebsite,OrganizationPhone,CategoryId")] BeginRegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                NonprofitOrganization nonprofitOrganization = new NonprofitOrganization() {
                    Active = false,
                    RegistrationCompleted = false,
                    UserId = userId,
                    User = db.Users.Where(c => c.Id == userId).First(),
                    CategoryId = viewModel.CategoryId,
                    Category = db.OrganizationCategories.Where(c => c.CategoryId == viewModel.CategoryId).First(),
                    OrganizationName = viewModel.OrganizationName,
                    OrganizationPhone = viewModel.OrganizationPhone,
                    OrganizationWebsite = viewModel.OrganizationWebsite,

            };
                db.NonprofitOrganizations.Add(nonprofitOrganization);
                db.SaveChanges();
                return View("RegistrationLanding");
            }
          
            return View(viewModel);
        }


        public ActionResult FinishRegistration(int id)
        {
            //FinishRegistrationViewModel viewModel = new FinishRegistrationViewModel()
            //{
            //    OrganizationId = id,
            //    OrganizationName = db.NonprofitOrganizations.Where(c => c.OrganizationId == id).First().OrganizationName
            //};
            var organization = db.NonprofitOrganizations.Where(c => c.OrganizationId == id).First();
            ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");
            ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson");

            FinishRegistrationViewModel viewModel = new FinishRegistrationViewModel() {
                OrganizationId = organization.OrganizationId,
                OrganizationName = organization.OrganizationName,
                PhoneNumber = organization.OrganizationPhone,
                OrganizationWebsite = organization.OrganizationWebsite,
            };
            return View(viewModel);
        }

        // POST: NonprofitOrganizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishRegistration([Bind(Include = "OrganizationId,Active,OrganizationName,ShipContact,DropContact,ShipZipcode,ShipState,ShipCity,DropZipcode,DropState,DropCity,DropStreetAddress,ShipStreetAddress,OrganizationDescription,OrganizationWebsite,PhoneNumber")] FinishRegistrationViewModel newOrganizationInfo)
        {
            var userId = User.Identity.GetUserId();
            NonprofitOrganization organization = db.NonprofitOrganizations.Where(c => c.UserId == userId).Include(d => d.ShipAddress).Include(d => d.DropAddress).First();

            if (ModelState.IsValid)
            { 
                var dropAddId = AddDropAddressGetId(newOrganizationInfo);
                var shipAddId = AddShipAddressGetId(newOrganizationInfo);

                organization.DropAddress = db.Addresses.Where(c => c.AddressId == dropAddId).First();
                organization.ShipAddress = db.Addresses.Where(c => c.AddressId == shipAddId).First();
                organization.OrganizationDescription = newOrganizationInfo.OrganizationDescription;
                organization.RegistrationCompleted = true;

                
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
               
                return RedirectToAction("Dashboard", "NonprofitOrganizations", organization.OrganizationId);

            }

            ViewBag.DropOffAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", organization.DropOffAddress);
            ViewBag.ShippingAddress = new SelectList(db.Addresses, "AddressId", "ContactPerson", organization.ShippingAddress);
            return View(newOrganizationInfo);
        }

        public int AddShipAddressGetId (FinishRegistrationViewModel newOrganizationInfo)
        {
         


            Address newShipAddress = new Address()
            {
                ContactPerson = newOrganizationInfo.ShipContact,
                StreetAddress = newOrganizationInfo.ShipStreetAddress,
                City = newOrganizationInfo.ShipCity,
                State = newOrganizationInfo.ShipState,
                Zipcode = newOrganizationInfo.ShipZipcode
            };

            db.Addresses.Add(newShipAddress);
            db.SaveChanges();
            return newShipAddress.AddressId;
        
        }

        public int AddDropAddressGetId (FinishRegistrationViewModel newOrganizationInfo)
        {
            //var gls = new GoogleLocationService();
            //AddressData address = new AddressData()
            //{
            //    Address = newOrganizationInfo.DropStreetAddress,
            //    City = newOrganizationInfo.DropCity,
            //    State = newOrganizationInfo.DropState,
            //    Zip = newOrganizationInfo.DropZipcode
            //};

            //var latlong = gls.GetLatLongFromAddress(address);
            //var latitude = latlong.Latitude;
            //var longitude = latlong.Longitude;



            var address = newOrganizationInfo.DropStreetAddress + " " + newOrganizationInfo.DropCity + ", " + newOrganizationInfo.DropState + " " + newOrganizationInfo.DropZipcode;

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            var latitude = point.Latitude;
            var longitude = point.Longitude;


            Address newDropAddress = new Address()
            {
                ContactPerson = newOrganizationInfo.DropContact,
                StreetAddress = newOrganizationInfo.DropStreetAddress,
                City = newOrganizationInfo.DropCity,
                State = newOrganizationInfo.DropState,
                Zipcode = newOrganizationInfo.DropZipcode,
                Latitude = latitude,
                Longitude = longitude
            };

            db.Addresses.Add(newDropAddress);
            db.SaveChanges();
            return newDropAddress.AddressId;
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
