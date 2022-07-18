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
    public class CategoriesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Categories
        public ActionResult Index(int? sectionId = null)
        {
            ViewBag.Topics = new List<Topic>();

            if (sectionId == null || sectionId == 0)  return View(db.Categories.ToList());

            var section = db.Sections.Find(sectionId);
            var categories = db.Categories.Where(s => s.SectionId == section.Id).ToList();
            var exam = db.Exams.Find(section.ExamId);

            ViewBag.Title = $"{exam.Name} - {section.TitleAndNumber} - Categories";            
            ViewBag.Topics = db.Topics.AsEnumerable().Where(t => categories.Any(c => c.Id == t.CategoryId)).ToList();

            return View(categories);
        }


        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        
        // GET: Categories/Create
        public ActionResult Create(int? sectionId)
        {
            var section = db.Sections.Find(sectionId);

            // TODO: add number and full-number
            var category = new Category() { ExamId = section.ExamId, SectionId = sectionId.Value };

            return View(category);
        }


        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,SectionId,CategoryParts,PartNumber,CoverImage,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index", new { sectionId = category.SectionId });
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,SectionId,CategoryParts,PartNumber,CoverImage,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { sectionId = category.SectionId });
            }

            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index", new { sectionId = category.SectionId });
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
