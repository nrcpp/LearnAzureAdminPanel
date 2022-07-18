using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnAzure.Admin.Controllers
{
    public class HomeController : Controller
    {
        private MainDbContext _db;

        public HomeController(MainDbContext db)
        {
            _db = db;
        }


        public ActionResult Index(int? examId = null, int? sectionId = null, int? categoryId = null, int? topicId = null)
        {
            var model = new HomeViewModel();
            model.Exams = _db.Exams.Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                }).ToList();
            
            if (examId == null) examId = 1;
            if (sectionId == null) sectionId = _db.Sections.First(s => s.ExamId == examId).Id;
            if (categoryId == null) categoryId = _db.Categories.First(s => s.SectionId == sectionId).Id;
            if (topicId == null) topicId = _db.Topics.First(s => s.CategoryId == categoryId).Id;

            if (examId != null)
            {
                model.SelectedExamId = examId.Value;

                model.Sections = _db.Sections.Where(e => e.ExamId == examId).ToList().Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.TitleAndNumber
                                }).ToList();
            }            

            if (sectionId != null)
            {
                model.SelectedSectionId = sectionId.Value;

                model.Categories = _db.Categories.Where(c => c.ExamId == examId && c.SectionId == sectionId).ToList().Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.TitleAndNumber
                                }).ToList();
            }

            if (categoryId != null)
            {
                model.SelectedCategoryId = categoryId.Value;
                model.Topics = _db.Topics.Where(c => c.ExamId == examId && c.SectionId == sectionId && c.CategoryId == categoryId)
                                  .ToList().Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.TitleAndNumber
                                }).ToList();
            }

            if (topicId != null)
            {                
                model.SelectedTopicId = topicId.Value;
                //return RedirectToAction("Index", "Topic", new { id = topicId.Value });
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            if (Request.Form["EditExam"] != null)
                return RedirectToAction("Edit", "Exams", new { id = model.SelectedExamId });

            else if (Request.Form["LoadSection"] != null)
                return RedirectToAction("Index", new { examId = model.SelectedExamId });

            else if (Request.Form["Sections"] != null)
                return RedirectToAction("SectionsList", "Sections", new { examId = model.SelectedExamId });

            else if (Request.Form["EditSection"] != null)
                return RedirectToAction("Edit", "Sections", new { id = model.SelectedExamId });

            else if (Request.Form["LoadCategory"] != null)
                return RedirectToAction("Index", new { examId = model.SelectedExamId, sectionId = model.SelectedSectionId });

            else if (Request.Form["Categories"] != null)
                return RedirectToAction("Index", "Categories", new { sectionId = model.SelectedSectionId });
            
            else if (Request.Form["EditCategory"] != null)
                return RedirectToAction("Edit", "Categories", new { id = model.SelectedCategoryId });

            else if (Request.Form["LoadTopic"] != null)
                return RedirectToAction("Index",
                    new
                    {
                        examId = model.SelectedExamId,
                        sectionId = model.SelectedSectionId,
                        categoryId = model.SelectedCategoryId
                    });

            else if (Request.Form["EditTopic"] != null)
                return RedirectToAction("Edit", "Topics", new { id = model.SelectedTopicId });
            
            else if (Request.Form["Topics"] != null)
                return RedirectToAction("Index", "Topics", new { categoryId = model.SelectedCategoryId });

            else if (Request.Form["LearningContent"] != null)
                return RedirectToAction("Index", "LearningContents", 
                    new { examId = model.SelectedExamId, sectionId = model.SelectedTopicId, 
                          categoryId = model.SelectedCategoryId, topicId = model.SelectedTopicId });


            // FillQuestions, ExportExam
            else if (Request.Form["FillQuestions"] != null)
                return RedirectToAction("FillQuestions", "Home", new
                {
                    examId = model.SelectedExamId,
                    sectionId = model.SelectedSectionId,
                    categoryId = model.SelectedCategoryId,
                    topicId = model.SelectedTopicId,
                });

            else if (Request.Form["ExportExam"] != null)
                return RedirectToAction("ExportExam", "Home", new { examId = model.SelectedExamId });

            return RedirectToAction("Index");
        }

        public ActionResult FillQuestions(int? examId = null, int? sectionId = null, int? categoryId = null, int? topicId = null)
        {
            return Content($"TODO: implement add question controller: {examId} | {sectionId} | {categoryId} | {topicId}");
        }


        public ActionResult ExportExam(int? examId = null)
        {
            var exam = _db.Exams.FirstOrDefault(e => e.Id == examId);
            return Content($"TODO: implement export questions functionality: {exam.Name}");
        }        
    }
}