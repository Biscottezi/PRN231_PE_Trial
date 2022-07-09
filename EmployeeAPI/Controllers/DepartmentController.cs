using BusinessLayer.Repository;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ODataController
    {
        private DepartmentEmployeeContext _dbContext;
        public DepartmentController(DepartmentEmployeeContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            DepartmentRepo repo = new DepartmentRepo(_dbContext);
            return Ok(repo.Get());
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        [EnableQuery]
        public IActionResult Get(string id)
        {
            DepartmentRepo repo = new DepartmentRepo(_dbContext);
            return Ok(repo.GetById(id));
        }
    }
}
