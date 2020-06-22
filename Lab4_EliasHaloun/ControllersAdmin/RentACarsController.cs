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
    public class RentACarsController : Controller
    {
        private Entities db = new Entities();

        // GET: RentACars
        public ActionResult Index()
        {
            var rentACars = db.RentACars.Include(r => r.RentalCompany);
            return View(rentACars.ToList());
        }

        // GET: RentACars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentACar rentACar = db.RentACars.Find(id);
            if (rentACar == null)
            {
                return HttpNotFound();
            }
            return View(rentACar);
        }

        // GET: RentACars/Create
        public ActionResult Create()
        {
            ViewBag.RentalCompany_Id = new SelectList(db.RentalCompanies, "Id", "Name");
            return View();
        }

        // POST: RentACars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarType,Fare,RentalCompany_Id")] RentACar rentACar)
        {
            if (ModelState.IsValid)
            {
                db.RentACars.Add(rentACar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RentalCompany_Id = new SelectList(db.RentalCompanies, "Id", "Name", rentACar.RentalCompany_Id);
            return View(rentACar);
        }

        // GET: RentACars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentACar rentACar = db.RentACars.Find(id);
            if (rentACar == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentalCompany_Id = new SelectList(db.RentalCompanies, "Id", "Name", rentACar.RentalCompany_Id);
            return View(rentACar);
        }

        // POST: RentACars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarType,Fare,RentalCompany_Id")] RentACar rentACar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentACar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentalCompany_Id = new SelectList(db.RentalCompanies, "Id", "Name", rentACar.RentalCompany_Id);
            return View(rentACar);
        }

        // GET: RentACars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentACar rentACar = db.RentACars.Find(id);
            if (rentACar == null)
            {
                return HttpNotFound();
            }
            return View(rentACar);
        }

        // POST: RentACars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentACar rentACar = db.RentACars.Find(id);
            db.RentACars.Remove(rentACar);
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
