using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab4_EliasHaloun.Models.ModelsResor;

namespace Lab4_EliasHaloun.ControllersAdmin
{
    [Authorize(Roles = "Admin")]
    public class RentalCompaniesController : Controller
    {
        private Entities db = new Entities();

        // GET: RentalCompanies
        public ActionResult Index()
        {
            return View(db.RentalCompanies.ToList());
        }

        // GET: RentalCompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalCompany rentalCompany = db.RentalCompanies.Find(id);
            if (rentalCompany == null)
            {
                return HttpNotFound();
            }
            return View(rentalCompany);
        }

        // GET: RentalCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RentalCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] RentalCompany rentalCompany)
        {
            if (ModelState.IsValid)
            {
                db.RentalCompanies.Add(rentalCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentalCompany);
        }

        // GET: RentalCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalCompany rentalCompany = db.RentalCompanies.Find(id);
            if (rentalCompany == null)
            {
                return HttpNotFound();
            }
            return View(rentalCompany);
        }

        // POST: RentalCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] RentalCompany rentalCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentalCompany);
        }

        // GET: RentalCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalCompany rentalCompany = db.RentalCompanies.Find(id);
            if (rentalCompany == null)
            {
                return HttpNotFound();
            }
            return View(rentalCompany);
        }

        // POST: RentalCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalCompany rentalCompany = db.RentalCompanies.Find(id);
            db.RentalCompanies.Remove(rentalCompany);
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
