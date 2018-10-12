using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS_MiniProject.Models;

namespace LMS_MiniProject.Controllers
{
    public class BookController : Controller
    {
        private LMSDBEntities db = new LMSDBEntities();

        // GET: Book
        public ActionResult Index()
        {
            return View(db.BookTbls.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTbl bookTbl = db.BookTbls.Find(id);
            if (bookTbl == null)
            {
                return HttpNotFound();
            }
            return View(bookTbl);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Tag,Image,Year_of_publishing")] BookTbl bookTbl)
        {
            if (ModelState.IsValid)
            {
                db.BookTbls.Add(bookTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookTbl);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTbl bookTbl = db.BookTbls.Find(id);
            if (bookTbl == null)
            {
                return HttpNotFound();
            }
            return View(bookTbl);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Tag,Image,Year_of_publishing")] BookTbl bookTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookTbl);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTbl bookTbl = db.BookTbls.Find(id);
            if (bookTbl == null)
            {
                return HttpNotFound();
            }
            return View(bookTbl);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookTbl bookTbl = db.BookTbls.Find(id);
            db.BookTbls.Remove(bookTbl);
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
