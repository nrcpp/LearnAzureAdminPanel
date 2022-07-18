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
    public class TopicsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Topics
        public ActionResult Index(int? categoryId = null, int? topicId = null)
        {
            if (categoryId == null || categoryId <= 0) return View(db.Topics.ToList());

            var topics = db.Topics.Where(t => t.CategoryId == categoryId).ToList();

            var category = db.Categories.Find(categoryId);
            var section = db.Sections.Find(category.Id);
            var exam = db.Exams.Find(section.ExamId);

            ViewBag.Title = $"{exam.Name} -> {section.TitleAndNumber} -> {category.TitleAndNumber} -> ";

            if (topicId == null) topicId = 1;

            /*
             *  public int SelectedTopicId { get; set; }
        public List<SelectListItem> DropDownTopics { get; set; } = new List<SelectListItem>();

        public List<Topic> Topics { get; set; } = new List<Topic>();
        public List<Question> Questions { get; set; } = new List<Question>();

        public Question NewQuestion { get; set; } = new Question();
             */

            var model = new TopicsViewModel()
            {
                SelectedTopicId = topicId.Value,
                DropDownTopics = topics.Select(t => new SelectListItem()
                {
                    Value = t.Id.ToString(),
                    Text = t.TitleAndNumber,
                }).ToList(),

                Topics = topics,
                Questions = db.Questions.Where(q => q.TopicId == topicId).ToList(),
            };

            return View(model);
        }


        // GET: Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        public ActionResult Create(int? categoryId)
        {
            var category = db.Categories.Find(categoryId);
            var section = db.Sections.First(s => s.Id == category.SectionId);
            var exam = db.Exams.First(e => e.Id == section.ExamId);

            var topic = new Topic()
            {
                ExamId = exam.Id,
                SectionId = section.Id,
                CategoryId = category.Id,
            };

            return View(topic);
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,SectionId,CategoryId,URL,DocumentationTitle,Tags,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }


        public ActionResult AddQuestion(int topicId)
        {
            // TODO: clear questions form

            var categoryId = db.Topics.Where(t => t.Id == topicId).First().CategoryId;
            return RedirectToAction("Index", new { categoryId });
        }


        [HttpPost]
        public ActionResult RefreshQuestionsForTopic(TopicsViewModel model)
        {
            var topicId = model.SelectedTopicId;
            var categoryId = db.Topics.Where(t => t.Id == topicId).First().CategoryId;

            //if (Request.Form["AddQuestion"] != null)
                

            return RedirectToAction("Index", new { categoryId, topicId });

        }


        [HttpPost]
        public ActionResult NewOrEditQuestion(TopicsViewModel model)
        {
            var topicId = model.SelectedTopicId;
            var categoryId = db.Topics.Where(t => t.Id == topicId).First().CategoryId;

            // TODO: если 

            return RedirectToAction("Index", new { categoryId, topicId });
        }


        // GET: Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,SectionId,CategoryId,URL,DocumentationTitle,Tags,Number,FullNumber,Title,CreatedAt,UpdatedAt,Version,UserAdded,IsComplete")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Topics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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
