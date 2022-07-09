using BusinessLayer.Repository;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Formatter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ODataController
    {
        private DepartmentEmployeeContext _dbContext;
        public EmployeeController(DepartmentEmployeeContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            EmployeeRepo repo = new EmployeeRepo(_dbContext);
            return Ok(repo.Get().Include(x => x.Department));
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public IActionResult GetById(string id)
        {
            EmployeeRepo repo = new EmployeeRepo(_dbContext);
            return Ok(repo.GetById(id));
        }

        [HttpPost]
        [EnableQuery]
        public void Post([FromBody]Employee book)
        {
            EmployeeRepo repo = new EmployeeRepo(_dbContext);
            repo.Get();
            repo.Add(book);
        }

        [HttpPut("{id}")]
        [EnableQuery]
        public void Put(int id, [FromBody] Employee book)
        {
            EmployeeRepo repo = new EmployeeRepo(_dbContext);
            repo.Update(book);
        }

        [HttpDelete("{id}")]
        [EnableQuery]
        public void Delete(string id)
        {
            EmployeeRepo repo = new EmployeeRepo(_dbContext);
            repo.Delete(id);
        }
    }
}
