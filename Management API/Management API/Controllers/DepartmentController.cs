using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Management_API.Data;
using Management_API.Models;

namespace Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public JsonResult Get()
        {
            IEnumerable<Department> objDepartmentList = _db.Departments;
            return new JsonResult(objDepartmentList);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Post(Department obj)
        {
            _db.Departments.Add(obj);
            _db.SaveChanges();
            return new JsonResult("successfully loaded");
        }
       
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public JsonResult Put( Department obj)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Update(obj);
                _db.SaveChanges();
                new JsonResult("updated successfully");
            }
            
            return new JsonResult("updated successfully");

        }

        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {
            var obj = _db.Departments.Find(id);
            if (obj == null)
            {
                return new JsonResult("Delete unsuccessfully");

            }
            _db.Departments.Remove(obj);
            _db.SaveChanges();
            return new JsonResult("Delete successfully");

        }

    }
    
}
