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
    public class FavoriteColorsController : Controller
    {
        private FortuneTellerMVCContext db = new FortuneTellerMVCContext();

        // GET: FavoriteColors
        public ActionResult Index()
        {
            return View(db.FavoriteColors.ToList());
        }

        // GET: FavoriteColors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteColor favoriteColor = db.FavoriteColors.Find(id);
            if (favoriteColor == null)
            {
                return HttpNotFound();
            }
            return View(favoriteColor);
        }

        // GET: FavoriteColors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FavoriteColors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FavoriteColorID,FavoriteColor1")] FavoriteColor favoriteColor)
        {
            if (ModelState.IsValid)
            {
                db.FavoriteColors.Add(favoriteColor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(favoriteColor);
        }

        // GET: FavoriteColors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteColor favoriteColor = db.FavoriteColors.Find(id);
            if (favoriteColor == null)
            {
                return HttpNotFound();
            }
            return View(favoriteColor);
        }

        // POST: FavoriteColors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FavoriteColorID,FavoriteColor1")] FavoriteColor favoriteColor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favoriteColor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(favoriteColor);
        }

        // GET: FavoriteColors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteColor favoriteColor = db.FavoriteColors.Find(id);
            if (favoriteColor == null)
            {
                return HttpNotFound();
            }
            return View(favoriteColor);
        }

        // POST: FavoriteColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavoriteColor favoriteColor = db.FavoriteColors.Find(id);
            db.FavoriteColors.Remove(favoriteColor);
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
