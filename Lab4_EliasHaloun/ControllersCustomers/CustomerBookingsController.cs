using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab4_EliasHaloun.Models.ModelsResor;

namespace Lab4_EliasHaloun.ControllersCustomers
{
    [Authorize(Roles = "Customer")]
    public class CustomerBookingsController : Controller
    {
        private Entities db = new Entities();

        // GET: CustomerBookings/Create
        public ActionResult Create()
        {
            ViewBag.Flight_Id = new SelectList(db.Flights, "Id", "Id");
            ViewBag.RentACar_Id = new SelectList(db.RentACars, "Id", "CarType");
            return View();
        }

        // POST: CustomerBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserRef,Amount,IsPaid,Flight_Id,RentACar_Id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Flight_Id = new SelectList(db.Flights, "Id", "Id", booking.Flight_Id);
            ViewBag.RentACar_Id = new SelectList(db.RentACars, "Id", "CarType", booking.RentACar_Id);
            return View(booking);
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
