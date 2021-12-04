using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDatabaseFirstProject_TrainingAcademy.Models.ViewModel
{
    public class TrainingViewModel
    {
        [Key]
        [Display(Name = "Training Id")]
        public int TrainingId { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Training Name")]
        public string TrainingName { get; set; }
        
        [Display(Name = "Training Cost/Hour")]
        public decimal TrainingCost { get; set; }
    }
}