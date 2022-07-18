using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnAzure.Admin
{
    public class HomeViewModel
    {
        [DisplayName("Exam")]
        public int SelectedExamId { get; set; }
        public List<SelectListItem> Exams { get; set; } = new List<SelectListItem>();
        
        [DisplayName("Section")]
        public int SelectedSectionId { get; set; }
        public List<SelectListItem> Sections { get; set; } = new List<SelectListItem>();


        [DisplayName("Category")]
        public int SelectedCategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();


        [DisplayName("Topic")]
        public int SelectedTopicId { get; set; }
        public List<SelectListItem> Topics { get; set; } = new List<SelectListItem>();
        
    }
}