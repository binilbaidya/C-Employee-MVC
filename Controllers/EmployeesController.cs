using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CRUDEmployeeMVC.Model;
using CRUDEmployeeMVC.Repository;

namespace CRUDEmployeeMVC.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private EmployeeDatabase db = new EmployeeDatabase();

        // GET: Employees
        public ActionResult Index(string empFilter, string sortBy, string searchQuery, int page = 1)
        {
            if (string.IsNullOrEmpty(empFilter))
            {
                empFilter = "isNotDeleted";
            }
            ViewBag.SortByName = string.IsNullOrEmpty(sortBy) ? "NameDesc" : "";
            ViewData["EmpFilters"] = new List<SelectListItem>
            {
                new SelectListItem {Text = "Not Deleted Employees", Value = "Not Deleted Employees"},
                new SelectListItem {Text = "Deleted Employees", Value = "Deleted Employees"},
                new SelectListItem {Text = "All Employees", Value = "All Employees"},
            };
            var employees = db.Employee.AsQueryable();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                employees = employees.Where(e => e.FullName.Contains(searchQuery));
            }

            switch (empFilter)
            {
                case "Not Deleted Employees":
                    employees = employees.Where(e => e.IsDeleted.Equals(false));
                    break;
                case "Deleted Employees":
                    employees = employees.Where(e => e.IsDeleted.Equals(true));
                    break;
                case "All Employees":
                    employees = employees.Where(e => e.IsDeleted.Equals(true) || e.IsDeleted.Equals(false));
                    break;
                default:
                    employees = employees.Where(e => e.IsDeleted.Equals(false));
                    break;
            }
            switch (sortBy)
            {
                case "NameDesc":
                    employees = employees.OrderByDescending(x => x.FullName);
                    break;
                default:
                    employees = employees.OrderBy(x => x.FullName);
                    break;
            }
            
            var pageSize = 3;
            if (page < 1)
            {
                page = 1;
            }
            int empCount = employees.Count();

            var pagination = new Pagination(empCount, page, pageSize);

            int empSkip = (page - 1) * pageSize;
            var data = employees.Skip(empSkip).Take(pagination.PageSize).ToList();

            this.ViewBag.Pagination = pagination;

            return View(data);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem {Text = Gender.Male.ToString(), Value = Gender.Male.ToString()},
                new SelectListItem {Text = Gender.Female.ToString(), Value = Gender.Female.ToString()},
                new SelectListItem {Text = Gender.Other.ToString(), Value = Gender.Other.ToString()},
            };
            ViewData["Gender"] = items;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                string profileFileName;
                List<string> fileList = new List<string>();
                if (employee.ProfilePicFile != null)
                {
                    profileFileName = Path.GetFileNameWithoutExtension(employee.ProfilePicFile.FileName);
                    string extension = Path.GetExtension(employee.ProfilePicFile.FileName);
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                    if (!supportedTypes.Contains(extension))
                    {
                        TempData["ProfileMessage"] = "File Extension Is Invalid - Only Upload Image File";
                        return RedirectToAction("Create");
                    }
                    else if (employee.ProfilePicFile.ContentLength > 2072865)
                    {
                        TempData["ProfileMessage"] = "File Size Exceeds 2MB";
                        return RedirectToAction("Create");
                    }
                    profileFileName = DateTime.Now.ToString("yymmssfff") + profileFileName + extension;
                    employee.ProfilePic = "/EmpImages/" + profileFileName;
                    profileFileName = Path.Combine(Server.MapPath("/EmpImages/"), profileFileName);
                    TempData.Remove("ProfileMessage");
                } else
                {
                    profileFileName = "no-user.png";
                    employee.ProfilePic = "/EmpImages/no-user.png";
                }
                foreach (HttpPostedFileBase empfile in employee.EmpDocFile)
                {
                    if (empfile != null)
                    {
                        string docFileName = Path.GetFileNameWithoutExtension(empfile.FileName);
                        string extension = Path.GetExtension(empfile.FileName);
                        var supportedTypes = new[] { ".jpg", ".doc", ".docx", ".pdf", ".jpeg", ".png" };
                        if (!supportedTypes.Contains(extension))
                        {
                            TempData["DocMessage"] = "File Extension Is Invalid - Only Document Files";
                            return RedirectToAction("Create");
                        }
                        else if (empfile.ContentLength > 10364325)
                        {
                            TempData["DocMessage"] = "File Size Exceeds 10MB";
                            return RedirectToAction("Create");
                        }
                        docFileName = DateTime.Now.ToString("yymmssfff") + docFileName + extension;
                        
                        fileList.Add("/EmpDocs/" + docFileName);
                        employee.EmpDoc = string.Join(", ", fileList);
                        docFileName = Path.Combine(Server.MapPath("/EmpDocs/"), docFileName);
                        empfile.SaveAs(docFileName);
                        TempData.Remove("DocMessage");
                    }
                    else
                    {
                        employee.EmpDoc = "/EmpDocs/no-document.png";
                    }
                }
                if(employee.ProfilePicFile != null)
                {
                        employee.ProfilePicFile.SaveAs(profileFileName);
                }
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var items = new List<SelectListItem>
            {
                new SelectListItem {Text = Gender.Male.ToString(), Value = Gender.Male.ToString()},
                new SelectListItem {Text = Gender.Female.ToString(), Value = Gender.Female.ToString()},
                new SelectListItem {Text = Gender.Other.ToString(), Value = Gender.Other.ToString()},
            };
            ViewData["Gender"] = items;
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
