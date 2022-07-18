using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnAzure.Admin
{
    public class TopicsViewModel
    {
        [DisplayName("Topic")]
        public int SelectedTopicId { get; set; }
        public List<SelectListItem> DropDownTopics { get; set; } = new List<SelectListItem>();

        public List<Topic> Topics { get; set; } = new List<Topic>();
        public List<Question> Questions { get; set; } = new List<Question>();

        public Question NewQuestion { get; set; } = new Question();
    }
}