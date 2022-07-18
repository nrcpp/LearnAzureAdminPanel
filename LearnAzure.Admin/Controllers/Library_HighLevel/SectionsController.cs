﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using LearnAzure.Admin;

namespace LearnAzure.Admin.Controllers
{
    public class SectionsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Sections
        public ActionResult Index(int? examId = null)
        {
            if (examId != null && examId > 0) return RedirectToAction("SectionsList", new { examId });

            return View(db.Sections.ToList());
        }

        public ActionResult SectionsList(int examId)
        {
            var exam = db.Exams.Find(examId);
            var sections = db.Sections.Where(s => s.ExamId == examId).ToList();

            ViewBag.Title = $"{exam.Name} Sections";
            var categories = new List<Category>();
            ViewBag.Categories = db.Categories.AsEnumerable().Where(c => sections.Any(s => s.Id == c.SectionId)).ToList();

            return View(sections);
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }


        // GET: Sections/Create
        public ActionResult Create(int examId)
        {
            var section = new Section() { ExamId = examId };

            return View(section);
        }


        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,PercentageFrom,PercentageTo,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index", new { examId = section.ExamId});
            }

            return View(section);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,PercentageFrom,PercentageTo,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { examId = section.ExamId });
            }
            return View(section);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = db.Sections.Find(id);
            
            db.Sections.Remove(section);
            db.SaveChanges();

            return RedirectToAction("Index", new { examId = section.ExamId });
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
