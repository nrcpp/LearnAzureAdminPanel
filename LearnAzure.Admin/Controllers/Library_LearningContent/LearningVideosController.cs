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
    public class LearningVideosController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: LearningVideos
        public ActionResult Index()
        {
            return View(db.LearningVideos.ToList());
        }

        // GET: LearningVideos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningVideo learningVideo = db.LearningVideos.Find(id);
            if (learningVideo == null)
            {
                return HttpNotFound();
            }
            return View(learningVideo);
        }

        // GET: LearningVideos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LearningVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,SectionId,CategoryId,TopicId,URL,VideoTitle,Author,Source,Description,Tags,Duration,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] LearningVideo learningVideo)
        {
            if (ModelState.IsValid)
            {
                db.LearningVideos.Add(learningVideo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(learningVideo);
        }

        // GET: LearningVideos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningVideo learningVideo = db.LearningVideos.Find(id);
            if (learningVideo == null)
            {
                return HttpNotFound();
            }
            return View(learningVideo);
        }

        // POST: LearningVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,SectionId,CategoryId,TopicId,URL,VideoTitle,Author,Source,Description,Tags,Duration,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] LearningVideo learningVideo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learningVideo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(learningVideo);
        }

        // GET: LearningVideos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningVideo learningVideo = db.LearningVideos.Find(id);
            if (learningVideo == null)
            {
                return HttpNotFound();
            }
            return View(learningVideo);
        }

        // POST: LearningVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LearningVideo learningVideo = db.LearningVideos.Find(id);
            db.LearningVideos.Remove(learningVideo);
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
