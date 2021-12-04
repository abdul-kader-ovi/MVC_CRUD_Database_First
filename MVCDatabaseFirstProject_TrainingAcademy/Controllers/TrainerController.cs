using MVCDatabaseFirstProject_TrainingAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDatabaseFirstProject_TrainingAcademy.Controllers
{
    public class TrainerController : Controller
    {
        DBFirstTrainingDBEntities dbobj = new DBFirstTrainingDBEntities();
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateTrainerAjax()
        {
            var Model = dbobj.Trainers.ToList();
            return View(Model);
        }
        public ActionResult TrainerAjaxFormExample(Trainer obj)
        {
            
            dbobj.Trainers.Add(obj);
            dbobj.SaveChanges();
            IEnumerable<Trainer> list = dbobj.Trainers.ToList();
            return View(list);
        }
    }
}