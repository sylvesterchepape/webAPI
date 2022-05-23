using Management_API.Data;
using Management_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(ApplicationDbContext db ,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            IEnumerable<Employee> objEmployeeList = _db.Employees;
            return new JsonResult(objEmployeeList);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Post(Employee obj)
        {
            _db.Employees.Add(obj);
            _db.SaveChanges();
            return new JsonResult("successfully loaded");
        }
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public JsonResult Put(Employee obj)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(obj);
                _db.SaveChanges();
                new JsonResult("updated successfully");
            }

            return new JsonResult("updated successfully");

        }
        // delete  Routes
        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {
           
            var obj = _db.Employees.Find(id);
            if (obj==null)
            {
                return new JsonResult("Delete unsuccessfully");

            }
            _db.Employees.Remove(obj);
            _db.SaveChanges();
            return new JsonResult("Delete successfully");
            

        }

        //Post file Routes
        [Route("SaveFile")]
        [HttpPost]
        
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    
                }
                return new JsonResult(filename);
            }
            catch(Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllDepartmentNames")]
      

        public JsonResult GetAllDepartmentNames()
        {
            IEnumerable<Department> objDepartmentList = _db.Departments;
            return new JsonResult(objDepartmentList);
        }
    }
}
