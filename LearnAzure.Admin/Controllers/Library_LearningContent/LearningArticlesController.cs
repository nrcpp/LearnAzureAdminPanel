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
    public class LearningArticlesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: LearningArticles
        public ActionResult Index()
        {
            return View(db.LearningArticles.ToList());
        }

        // GET: LearningArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningArticle learningArticle = db.LearningArticles.Find(id);
            if (learningArticle == null)
            {
                return HttpNotFound();
            }
            return View(learningArticle);
        }

        // GET: LearningArticles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LearningArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,SectionId,CategoryId,TopicId,URL,DocumentationTitle,Author,Source,Description,Tags,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] LearningArticle learningArticle)
        {
            if (ModelState.IsValid)
            {
                db.LearningArticles.Add(learningArticle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(learningArticle);
        }

        // GET: LearningArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningArticle learningArticle = db.LearningArticles.Find(id);
            if (learningArticle == null)
            {
                return HttpNotFound();
            }
            return View(learningArticle);
        }

        // POST: LearningArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,SectionId,CategoryId,TopicId,URL,DocumentationTitle,Author,Source,Description,Tags,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] LearningArticle learningArticle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learningArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(learningArticle);
        }

        // GET: LearningArticles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningArticle learningArticle = db.LearningArticles.Find(id);
            if (learningArticle == null)
            {
                return HttpNotFound();
            }
            return View(learningArticle);
        }

        // POST: LearningArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LearningArticle learningArticle = db.LearningArticles.Find(id);
            db.LearningArticles.Remove(learningArticle);
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
