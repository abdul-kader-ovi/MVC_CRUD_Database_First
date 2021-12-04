
using CrystalDecisions.CrystalReports.Engine;
using MVCDatabaseFirstProject_TrainingAcademy.Models;
using MVCDatabaseFirstProject_TrainingAcademy.Models.ViewModel;
using MVCDatabaseFirstProject_TrainingAcademy.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCDatabaseFirstProject_TrainingAcademy.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        // GET: Students
        StudentRepository repo = new StudentRepository();
        public JsonResult IsStudentNameAvailable(string StudentName)
        {
            return Json(!repo.GetAllStudents().Any(e => e.StudentName == StudentName), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [OutputCache(Duration =10)]
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            System.Threading.Thread.Sleep(3000);
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = SearchString;
            IEnumerable<StudentViewModel> list = repo.GetAllStudents();
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(e => e.StudentName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(e => e.StudentName).ToList();
                    break;
                default:
                    list = list.OrderBy(e => e.StudentName).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        [ChildActionOnly]
        [OutputCache(Duration =10)]
        public string GetStudentCount()
        {
            return "Total Student count is:" + repo.GetAllStudents().Count().ToString() + "@" + DateTime.Now.ToString();
        }
        [HttpGet]
        public ViewResult Create()
        {
            List<Training> list = repo.GetTrainings();
            List<Trainer> list1 = repo.GetTrainers();
            ViewBag.Trainings = list;
            ViewBag.Trainers = list1;
            return View();
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            List<Training> list = repo.GetTrainings();
            List<Trainer> list1 = repo.GetTrainers();
            ViewBag.Trainings = list;
            ViewBag.Trainers = list1;
            Student stu = repo.GetStudent(id);
            StudentViewModel obj = new StudentViewModel();
            obj.StudentId = stu.StudentId;
            obj.StudentName = stu.StudentName;
            obj.Email = stu.Email;
            obj.ImageName = stu.ImageName;
            obj.ImageUrl = stu.ImageUrl;
            obj.TrainingId = stu.TrainingId;
            obj.TrainerId = stu.TrainerId;
            obj.AdmissionDate = stu.AdmissionDate;
            ViewBag.Edit = "StudentEdit";
            return PartialView("_EditPartial", obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(StudentViewModel obj)
        {
            var result = false;
            Student obj1;
            if (obj.StudentId == 0)
            {
                obj1 = new Student();
                obj1.StudentName = obj.StudentName;
                obj1.Email = obj.Email;
                obj1.AdmissionDate = obj.AdmissionDate;
                obj1.TrainingId = obj.TrainingId;
                obj1.TrainerId = obj.TrainerId;
                string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                string extension = Path.GetExtension(obj.ImageFile.FileName);
                obj1.ImageName = fileName + extension;
                obj1.ImageUrl = "~/Images/" + obj1.ImageName;
                fileName = Path.Combine(Server.MapPath(obj1.ImageUrl));
                obj.ImageFile.SaveAs(fileName);
                if (ModelState.IsValid)
                {
                    repo.SaveStudent(obj1);
                    result = true;
                }

            }
            else
            {
                obj1 = repo.GetStudent(obj.StudentId);
                obj1.StudentName = obj.StudentName;
                obj1.Email = obj.Email;
                obj1.AdmissionDate = obj.AdmissionDate;
                obj1.TrainingId = obj.TrainingId;
                obj1.TrainerId = obj.TrainerId;
                string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                string extension = Path.GetExtension(obj.ImageFile.FileName);
                obj1.ImageName = fileName + extension;
                obj1.ImageUrl = "~/Images/" + obj1.ImageName;
                fileName = Path.Combine(Server.MapPath(obj1.ImageUrl));
                obj.ImageFile.SaveAs(fileName);
                repo.UpdateStudent(obj1);
                result = true;

            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<Training> list = repo.GetTrainings();
                List<Trainer> list1 = repo.GetTrainers();
                ViewBag.Trainings = list;
                ViewBag.Trainers = list1;
                return View();
            }
        }

        [HttpGet]
        public PartialViewResult Delete(int id)
        {
            List<Training> list = repo.GetTrainings();
            List<Trainer> list1 = repo.GetTrainers();
            ViewBag.Trainings = list;
            ViewBag.Trainers = list1;
            Student emp = repo.GetStudent(id);
            StudentViewModel obj = new StudentViewModel();
            obj.StudentId = emp.StudentId;
            obj.StudentName = emp.StudentName;
            obj.Email = emp.Email;
            obj.ImageName = emp.ImageName;
            obj.ImageUrl = emp.ImageUrl;
            obj.TrainingId = emp.TrainingId;
            obj.TrainerId = emp.TrainerId;
            obj.TrainingName = emp.Training.TrainingName;
            obj.TrainerName = emp.Trainer.TrainerName;
            obj.AdmissionDate = emp.AdmissionDate;
            ViewBag.Delete = "StudentDelete";
            return PartialView("_DeletePartial", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(StudentViewModel viewObj)
        {
            Student obj = repo.GetStudent(viewObj.StudentId);
            string imageName = viewObj.ImageName;
            //DeleteExistingImage(imageName);
            repo.DeleteStudent(obj.StudentId);
            return RedirectToAction("Index");
        }
        public PartialViewResult Details(int id)
        {
            List<Training> list = repo.GetTrainings();
            List<Trainer> list1 = repo.GetTrainers();
            ViewBag.Trainings = list;
            ViewBag.Trainers = list1;
            Student emp = repo.GetStudent(id);
            StudentViewModel obj = new StudentViewModel();
            obj.StudentId = emp.StudentId;
            obj.StudentName = emp.StudentName;
            obj.Email = emp.Email;
            obj.ImageName = emp.ImageName;
            obj.ImageUrl = emp.ImageUrl;
            obj.TrainingId = emp.TrainingId;
            obj.TrainingName = emp.Training.TrainingName;
            obj.TrainerId = emp.TrainerId;
            obj.TrainerName = emp.Trainer.TrainerName;
            obj.AdmissionDate = emp.AdmissionDate;
            ViewBag.Details = "Show";
            return PartialView("_DetailsRecord", obj);
        }
        //private DBFirstTrainingDBEntities db = new DBFirstTrainingDBEntities();
        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            string path = Server.MapPath("~") + "Report//" + "CrystalReport.rpt";
            rd.Load(path);
            List<StudentViewModel> list = repo.GetAllStudents().ToList();
            rd.SetDataSource(ListToDataTable(list));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "apllication/pdf", "Student.pdf");
        }
        private DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        
        
    }
}