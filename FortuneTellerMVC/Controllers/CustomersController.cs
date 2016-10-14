using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCContext db = new FortuneTellerMVCContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth).Include(c => c.FavoriteColor);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            if (customer.Age % 2 == 0)
            {
                  ViewBag.retire = 10;
            }
            else
            {
                 ViewBag.retire = 20;
            }
            /*Created BirthMonth Object Due to Separate table and this determines
             the amount of savings the fortune will reveal*/
            BirthMonth birthmonth = db.BirthMonths.Find(id);
            if (birthmonth.BirthMonth1 >= 1 && birthmonth.BirthMonth1 <= 4)
            {
                ViewBag.savings = 100000;

            }
            else if (birthmonth.BirthMonth1 >= 5 && birthmonth.BirthMonth1 <= 8)
            {

                ViewBag.savings = 200000;
            }

            else if (birthmonth.BirthMonth1 >= 9 && birthmonth.BirthMonth1 <= 12)
            {
                ViewBag.savings = 500000;
            }
            else
            {
                ViewBag.savings = 0;
            }

            /*Created Favorite Color Object Due to Separate table and this determines
             the vehicle the fortune will reveal*/
            FavoriteColor favcolor = db.FavoriteColors.Find(id);

            if (favcolor.FavoriteColor1 == "Red")
            {
                ViewBag.vehicle = "yaht";
            }

            else if (favcolor.FavoriteColor1 == "Orange")
            {
                ViewBag.vehicle = "classic Mustang";
            }

            else if (favcolor.FavoriteColor1 == "yellow")
            {
                ViewBag.vehicle = "private Jet";
            }

            else if (favcolor.FavoriteColor1 == "green")
            {
                ViewBag.vehicle = "Harley Davidson";
            }

            else if (favcolor.FavoriteColor1 == "blue")
            {
                ViewBag.vehicle = "glider";
            }

            else if (favcolor.FavoriteColor1 == "indigo")
            {
                ViewBag.vehicle = "Shwinn bicycle";
            }

            else if (favcolor.FavoriteColor1 == "violet")
            {
                ViewBag.vehicle = "sea plane";
            }
            /*this determines the vacation location based on #siblings*/
            if (customer.NumberofSiblings == 0)
            {

                ViewBag.vacationLocation = "Italy";
            }

            else if (customer.NumberofSiblings == 1)
            {

                ViewBag.vacationLocation = "Scotland";
            }

            else if (customer.NumberofSiblings == 2)
            {

                ViewBag.vacationLocation = "Florida Coast";
            }

            else if (customer.NumberofSiblings == 3)
            {

                ViewBag.vacationLocation = "the moon";
            }

            else if (customer.NumberofSiblings > 3)
            {
                ViewBag.vacationLocation = "South Dakota";
            }


            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthID");
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,FavoriteColorID,BirthMonthID,NumberofSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthID", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthID", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,FavoriteColorID,BirthMonthID,NumberofSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthID", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
