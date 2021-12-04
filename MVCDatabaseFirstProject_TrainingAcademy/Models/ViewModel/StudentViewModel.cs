using MVCDatabaseFirstProject_TrainingAcademy.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDatabaseFirstProject_TrainingAcademy.Models.ViewModel
{
    public class StudentViewModel
    {
        [Display(Name = "Student Id")]
        [Key]
        public int StudentId { get; set; }

        [Display(Name = "Student Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Student Name is required")]
        //[Remote("IsStudentNameAvailable", "Student", ErrorMessage = "Student Name already is in use.")]
        public string StudentName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [ValidateAdmissionDate]
        [Display(Name = "Admission Date")]
        [DataType(DataType.DateTime)]
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Training")]
        //[Required(ErrorMessage = "Please select Training")]
        public int TrainingId { get; set; }

        [Display(Name = "Trainer")]
        //[Required(ErrorMessage = "Please select Trainer")]
        public int TrainerId { get; set; }

        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Image File")]
        public HttpPostedFileBase ImageFile { get; set; }

        public string PageTitle { get; set; }

        [Display(Name = "Training Name")]
        public string TrainingName { get; set; }

        [Display(Name = "Trainer Name")]
        public string TrainerName { get; set; }

        [Display(Name = "Training Cost")]
        public decimal TrainingCost { get; set; }
    }
}