using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard (ApplicationUser user)
        {
            return View("AdminDashboard", user);
        }

        public ActionResult GetPendingAccounts()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<NonprofitOrganization> pendingList = db.NonprofitOrganizations.Where(c => c.Active == false).ToList();
            return View("PendingAccounts", pendingList);
        }

        public ActionResult AuthorizeAccount(int accountId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                NonprofitOrganization account = db.NonprofitOrganizations.Where(c => c.OrganizationId == accountId).First();
                account.Active = true;
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("GetPendingAccounts");
        }
    }
}