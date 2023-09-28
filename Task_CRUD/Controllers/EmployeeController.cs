using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Linq;
using Task_CRUD.Context;
using Task_CRUD.Models;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace Task_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var Data = _context.Employees.ToList();
            return View(Data);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {

                string uniqueFileName = UploadImage(employee);
                var data = new Employee()
                {
                    Name = employee.Name,
                    Contact= employee.Contact,
                    Email = employee.Email,
                    Gender=employee.Gender,
                    Degidesignation=employee.Degidesignation,
                    Description=employee.Description,   
                    Date=employee.Date,
                    Path = uniqueFileName,
                    IsActive =employee.IsActive,

                 
                   
                };
                _context.Employees.Add(data);
                _context.SaveChanges();
                TempData["Success"] = "Record successfully Save!";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View();
        }

        private string UploadImage(Employee employee)
        {
            string uniqueFileName = string.Empty;
            if (employee.FormFile != null)
            {
                string UploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Contant/Images/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.FormFile.FileName;
                string filepath = Path.Combine(UploadFolder, uniqueFileName);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    employee.FormFile.CopyTo(filestream);

                }
            }
            return uniqueFileName;
        }
        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            else
            {
                var Data = _context.Employees.Where(x => x.Id == Id).SingleOrDefault();
                if (Data != null)
                {
                    string deleteFromfolder = Path.Combine(_webHostEnvironment.WebRootPath, "Contant/Images/");
                    string CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromfolder, Data.Path);
                    if (CurrentImage != null)
                    {
                        if (System.IO.File.Exists(CurrentImage))
                        {
                            System.IO.File.Delete(CurrentImage);
                        }
                    }
                    _context.Employees.Remove(Data);
                    _context.SaveChanges();
                    TempData["Success"] = "Record Deleted!";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }

            var Data = _context.Employees.Where(x => x.Id == Id).SingleOrDefault();
            return View(Data);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {

            var Data = _context.Employees.Where(x => x.Id == Id).SingleOrDefault();
            if (Data != null)
            {

                return View(Data);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            ModelState.Remove("Path");
            if (ModelState.IsValid) { }
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _context.Employees.Where(e => e.Id == employee.Id).SingleOrDefault();
                    string uniquefileName = string.Empty;
                    if (employee.FormFile != null)
                    {
                        if (data.Path != null)
                        {
                            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, "Contant/Images", data.Path);
                            if (System.IO.File.Exists(filepath))
                            {
                                System.IO.File.Delete(filepath);
                            }
                        }
                        uniquefileName = UploadImage(employee);
                    }

                    data.Name = employee.Name;
                    data.Id = employee.Id;
                    data.Name=employee.Name;
                    data.Contact=employee.Contact;
                    data.Email = employee.Email;
                    data.Gender = employee.Gender;
                    data.Description=employee.Description;
                    data.Degidesignation=employee.Degidesignation;
                    data.Date=employee.Date;
                    data.Path= employee.Path;
                    data.IsActive=employee.IsActive;    
                    if (employee.FormFile != null)
                    {
                        data.Path = uniquefileName;
                    }
                    _context.Employees.Update(data);
                    _context.SaveChanges();
                    TempData["success"] = "Record Updated Successfully!";
                    return RedirectToAction("Index");

                }
                else
                {
                    return View("model");
                }


            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }

    }
}
