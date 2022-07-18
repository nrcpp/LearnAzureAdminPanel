using System;
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
    public class LearningPracticeLabsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: LearningPracticeLabs
        public ActionResult Index()
        {
            return View(db.LearningPracticeLabs.ToList());
        }

        // GET: LearningPracticeLabs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningPracticeLab learningPracticeLab = db.LearningPracticeLabs.Find(id);
            if (learningPracticeLab == null)
            {
                return HttpNotFound();
            }
            return View(learningPracticeLab);
        }

        // GET: LearningPracticeLabs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LearningPracticeLabs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,SectionId,CategoryId,TopicId,URL,LabTitle,Author,Source,Description,Tags,Duration,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] LearningPracticeLab learningPracticeLab)
        {
            if (ModelState.IsValid)
            {
                db.LearningPracticeLabs.Add(learningPracticeLab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(learningPracticeLab);
        }

        // GET: LearningPracticeLabs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningPracticeLab learningPracticeLab = db.LearningPracticeLabs.Find(id);
            if (learningPracticeLab == null)
            {
                return HttpNotFound();
            }
            return View(learningPracticeLab);
        }

        // POST: LearningPracticeLabs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,SectionId,CategoryId,TopicId,URL,LabTitle,Author,Source,Description,Tags,Duration,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] LearningPracticeLab learningPracticeLab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learningPracticeLab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(learningPracticeLab);
        }

        // GET: LearningPracticeLabs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningPracticeLab learningPracticeLab = db.LearningPracticeLabs.Find(id);
            if (learningPracticeLab == null)
            {
                return HttpNotFound();
            }
            return View(learningPracticeLab);
        }

        // POST: LearningPracticeLabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LearningPracticeLab learningPracticeLab = db.LearningPracticeLabs.Find(id);
            db.LearningPracticeLabs.Remove(learningPracticeLab);
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
