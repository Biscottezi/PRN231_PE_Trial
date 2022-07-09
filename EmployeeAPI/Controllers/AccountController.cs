using BusinessLayer.Repository;
using BusinessObject.RequestModels;
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
    public class AccountController : ODataController
    {
        private DepartmentEmployeeContext _dbContext;
        public AccountController(DepartmentEmployeeContext dbContext)
        {
            _dbContext = dbContext;
        }
        // POST api/<EmployeeController>
        [HttpPost("login")]
        [EnableQuery]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            AccountRepo repo = new AccountRepo(_dbContext);
            var acc = repo.Get().Where(x => x.UserId == model.Id && x.AccountPassword == model.Password).FirstOrDefault();
            if (acc==null)
            {
                return Unauthorized();
            } else
            {
                return Ok(acc);
            }
        }
    }
}
