using MVCDatabaseFirstProject_TrainingAcademy.Models;
using MVCDatabaseFirstProject_TrainingAcademy.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCDatabaseFirstProject_TrainingAcademy.Repositories
{
    public class StudentRepository
    {
        DBFirstTrainingDBEntities dbObj = new DBFirstTrainingDBEntities();
        public IEnumerable<StudentViewModel> GetAllStudents()
        {
            List<StudentViewModel> studentList = dbObj.Students.Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                StudentName = s.StudentName,
                AdmissionDate = s.AdmissionDate,
                Email = s.Email,
                ImageName =s.ImageName,
                ImageUrl = s.ImageUrl,
                TrainingId = s.TrainingId,
                TrainingName = s.Training.TrainingName,
                TrainerId=s.TrainerId,
                TrainerName=s.Trainer.TrainerName, 
                TrainingCost=s.Training.TrainingCost,
                PageTitle = "Student List"
            }).ToList();
            return studentList;
        }
        public Student GetStudent(int id)
        {
            Student obj = dbObj.Students.SingleOrDefault(s => s.StudentId == id);
            return obj;
        }
        public void SaveStudent(Student obj)
        {
            dbObj.Students.Add(obj);
            dbObj.SaveChanges();
        }
        public void UpdateStudent(Student upObj)
        {
            dbObj.Entry(upObj).State = EntityState.Modified;
            dbObj.SaveChanges();
        }
        public void DeleteStudent(int id)
        {
            Student dltStudent = GetStudent(id);
            dbObj.Students.Remove(dltStudent);
            dbObj.SaveChanges();
        }
        public List<Training> GetTrainings()
        {
            List<Training> list = dbObj.Trainings.ToList();
            return list;
        }
        public List<Trainer> GetTrainers()
        {
            List<Trainer> list = dbObj.Trainers.ToList();
            return list;
        }
    }
}