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
    public class LearningContentsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: LearningContents
        // examId = model.SelectedExamId, sectionId = model.SelectedTopicId, 
        // categoryId = model.SelectedCategoryId, topicId = model.SelectedTopicId
    
        public ActionResult Index()
        {
            return View(db.LearningContents.ToList());
        }

        // GET: LearningContents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningContent learningContent = db.LearningContents.Find(id);
            if (learningContent == null)
            {
                return HttpNotFound();
            }
            return View(learningContent);
        }

        // GET: LearningContents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LearningContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,SectionId,CategoryId,TopicId,Type,URL,WebsiteTitle,Author,Source,Description,Tags,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] LearningContent learningContent)
        {
            if (ModelState.IsValid)
            {
                db.LearningContents.Add(learningContent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(learningContent);
        }

        // GET: LearningContents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningContent learningContent = db.LearningContents.Find(id);
            if (learningContent == null)
            {
                return HttpNotFound();
            }
            return View(learningContent);
        }

        // POST: LearningContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,SectionId,CategoryId,TopicId,Type,URL,WebsiteTitle,Author,Source,Description,Tags,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] LearningContent learningContent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learningContent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(learningContent);
        }

        // GET: LearningContents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningContent learningContent = db.LearningContents.Find(id);
            if (learningContent == null)
            {
                return HttpNotFound();
            }
            return View(learningContent);
        }

        // POST: LearningContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LearningContent learningContent = db.LearningContents.Find(id);
            db.LearningContents.Remove(learningContent);
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
