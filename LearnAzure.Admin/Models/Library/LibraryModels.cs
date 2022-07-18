using System;
using System.ComponentModel;

namespace LearnAzure.Admin
{
    public class BaseItem
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [DisplayName("Updated At")]
        public DateTime? UpdatedAt { get; set; }

        public int Version { get; set; } = 1;

        [DisplayName("User")]
        public string UserAdded { get; set; } 

        [DisplayName("Is Complete")]
        public bool IsComplete { get; set; }
    }

    public class LibraryBaseItem : BaseItem
    {
        public int Number { get; set; }                 // 1
        public string FullNumber { get; set; }          // 1.1.
        
        public string Title { get; set; }           // for section, category, topic, question

        public string TitleAndNumber => $"{FullNumber} {Title}";
    }



    public class Exam : BaseItem
    {
        // Constants for Azure
        public const int MaxScore = 1000;
        public const int PassingScore = 700;

        public const int AZ900 = 900, AZ204 = 204, AZ400 = 400, AZ104 = 104;

        public const string Beginner = "Beginner", Associate = "Associate", Expert = "Expert";


        // Properties
        public int Code { get; set; }                   // 204
        public string Name { get; set; }                // AZ-204

        public int TotalQuestionsFrom { get; set; }
        public int TotalQuestionsTo { get; set; }
        public string TotalQuestionsRangeStr => $"{TotalQuestionsFrom}-{TotalQuestionsTo}";
        public int ExamDurationMinutes { get; set; }
        public string Level { get; set; }

        public string Title { get; set; }               // Microsoft Azure Fundamentals
        public string Description { get; set; }         // obtained from official site
        public string Technology { get; set; } = "Azure";         // Azure, AWS
    }


    public class Section : LibraryBaseItem
    {
        public int ExamId { get; set; }

        public int PercentageFrom { get; set; }
        public int PercentageTo { get; set; }        
    }


    public class Category : LibraryBaseItem
    {
        public int ExamId { get; set; }
        public int SectionId { get; set; }

        [DisplayName("Parts")]
        public string CategoryParts { get; set; }

        [DisplayName("Part #")]
        public int PartNumber { get; set; }

        [DisplayName("Image")]
        public string CoverImage { get; set; }        
    }

    public class Topic : LibraryBaseItem
    {
        public int ExamId { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }

        public string URL { get; set; }
        public string DocumentationTitle { get; set; }

        public string Tags { get; set; }        
    }

    public class LearningContent : LibraryBaseItem
    {
        public int ExamId { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }

        public string Type { get; set; }

        public string URL { get; set; }
        public string WebsiteTitle { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }      // youtube, ms learn, udemy

        public string Description { get; set; }
        public string Tags { get; set; }
    }

    public class LearningArticle : LibraryBaseItem
    {
        public int ExamId { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }
        
        public string URL { get; set; }
        public string DocumentationTitle { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }      // youtube, ms learn, udemy

        public string Description { get; set; }
        public string Tags { get; set; }        
    }


    public class LearningVideo : LibraryBaseItem
    {
        public int ExamId { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }

        public string URL { get; set; }
        public string VideoTitle { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }      // youtube, ms learn, udemy

        public string Description { get; set; }
        public string Tags { get; set; }
        
        public string Duration { get; set; }
    }


    public class LearningPracticeLab : LibraryBaseItem
    {                
        public int ExamId { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }

        public string URL { get; set; }
        public string LabTitle { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }      // youtube, ms learn, udemy

        public string Description { get; set; }
        public string Tags { get; set; }

        public string Duration { get; set; }
    }


    public class Question : LibraryBaseItem
    {
        public int ExamId { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }

        public string Type { get; set; } = "Single";           // Single, MultiChoice, MultiYesNo, DropDowns, DragNdrop

        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }
        public string Answer6 { get; set; }

        public string CorrectAnswer { get; set; }

        public string Explanation { get; set; }
        public string ReferenceUrl { get; set; }

        public bool IsExamOnly { get; set; }

        public string Header => $"Question #{FullNumber}";
    }
}
