using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using Capstone.ViewModels;

namespace Capstone.Controllers
{
    public class DonationItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DonationItems
        public ActionResult Index(int organizationId, int? categoryId)
        {
            var organization = db.NonprofitOrganizations.Find(organizationId);
            OrgRequestItemsListViewModel viewModel = new OrgRequestItemsListViewModel()
            {
                OrganizationId = organizationId,
                OrganizationDescription = organization.OrganizationDescription,
                OrganizationName = organization.OrganizationName,
            };
            if(categoryId != null)
            {
                viewModel.ItemsList = db.DonationItem.Include(d => d.Category).Where(c => c.RequestingOrganizationId == organization.OrganizationId && c.CategoryId == categoryId);
            }
            if(categoryId == null || categoryId == 0)
            {

                viewModel.ItemsList = db.DonationItem.Include(d => d.Category).Where(c => c.RequestingOrganizationId == organization.OrganizationId);
            }
            ViewBag.Categories = db.ItemCategories.Distinct();
            return View(viewModel);
        }

        // GET: DonationItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationItem donationItem = db.DonationItem.Find(id);
            ViewBag.CategoryName = db.ItemCategories.Find(donationItem.CategoryId).Name;
            if (donationItem == null)
            {
                return HttpNotFound();
            }
            return View(donationItem);
        }

        // GET: DonationItems/Create
        public ActionResult Create(int organizationId)
        {
            DonationItemCreateViewModel viewModel = new DonationItemCreateViewModel()
            {
                OrganizationId = organizationId,
                Organization = db.NonprofitOrganizations.Where(c => c.OrganizationId == organizationId).First(),
                //Categories = db.ItemCategories.Distinct().ToList()
            };
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "CategoryId", "Name", viewModel.CategoryId);

            return View(viewModel);
        }

        // POST: DonationItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemName,ItemQuantity,ItemSize,CategoryId,Brand,Color,ItemDescription,OrganizationId,Organization,ImageUpload")] DonationItemCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                DonationItem item = new DonationItem()
                {
                    ItemName = vm.ItemName,
                    ItemQuantity = vm.ItemQuantity,
                    ItemSize = vm.ItemSize,
                    CategoryId = vm.CategoryId,
                    Category = db.ItemCategories.Where(c => c.CategoryId == vm.CategoryId).First(),
                    Brand = vm.Brand,
                    Color = vm.Color,
                    ItemDescription = vm.ItemDescription,
                    RequestingOrganizationId = vm.OrganizationId,
                    Organization = vm.Organization
                };

                if (vm.ImageUpload != null && vm.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/Content/ImageUploads";
                    var imagePath = Path.Combine(Server.MapPath("~/Content/ImageUploads"), vm.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, vm.ImageUpload.FileName);
                    vm.ImageUpload.SaveAs(imagePath);
                    item.ImageFilePath = imageUrl;
                }


                db.DonationItem.Add(item);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = item.ItemId });
            }

            
            return View(vm);
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
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "CategoryId", "Name", donationItem.CategoryId);
            return View(donationItem);
        }

        // POST: DonationItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemName,ItemQuantity,ItemSize,CategoryId,Brand,Color,ItemDescription,OrganizationId,Organization,ImageUpload")] DonationItem donationItem)
        {
            if (ModelState.IsValid)
            {
                if (donationItem.ImageUpload != null && donationItem.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/Content/ImageUploads";
                    var imagePath = Path.Combine(Server.MapPath("~/Content/ImageUploads"), donationItem.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, donationItem.ImageUpload.FileName);
                    donationItem.ImageUpload.SaveAs(imagePath);
                    donationItem.ImageFilePath = imageUrl;
                }

                db.Entry(donationItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "CategoryId", "Name", donationItem.CategoryId);
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
