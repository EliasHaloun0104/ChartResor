﻿using System;
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
    public class RoutesController : Controller
    {
        private Entities db = new Entities();

        // GET: Routes
        public ActionResult Index()
        {
            var routes = db.Routes.Include(r => r.Destination).Include(r => r.Destination1);
            return View(routes.ToList());
        }

        // GET: Routes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // GET: Routes/Create
        public ActionResult Create()
        {
            ViewBag.DestinationFrom_Id = new SelectList(db.Destinations, "Id", "Name");
            ViewBag.DestinationTo_Id = new SelectList(db.Destinations, "Id", "Name");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DestinationFrom_Id,DestinationTo_Id")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DestinationFrom_Id = new SelectList(db.Destinations, "Id", "Name", route.DestinationFrom_Id);
            ViewBag.DestinationTo_Id = new SelectList(db.Destinations, "Id", "Name", route.DestinationTo_Id);
            return View(route);
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.DestinationFrom_Id = new SelectList(db.Destinations, "Id", "Name", route.DestinationFrom_Id);
            ViewBag.DestinationTo_Id = new SelectList(db.Destinations, "Id", "Name", route.DestinationTo_Id);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DestinationFrom_Id,DestinationTo_Id")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DestinationFrom_Id = new SelectList(db.Destinations, "Id", "Name", route.DestinationFrom_Id);
            ViewBag.DestinationTo_Id = new SelectList(db.Destinations, "Id", "Name", route.DestinationTo_Id);
            return View(route);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Route route = db.Routes.Find(id);
            db.Routes.Remove(route);
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
