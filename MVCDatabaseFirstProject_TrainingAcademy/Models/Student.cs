//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCDatabaseFirstProject_TrainingAcademy.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public System.DateTime AdmissionDate { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int TrainingId { get; set; }
        public int TrainerId { get; set; }
    
        public virtual Trainer Trainer { get; set; }
        public virtual Training Training { get; set; }
    }
}