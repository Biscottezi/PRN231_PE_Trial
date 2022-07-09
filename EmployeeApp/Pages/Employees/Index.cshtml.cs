using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace EmployeeApp.Pages.Employees
{
    
    public class IndexModel : PageModel
    {
        public int pageSize = 5;
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public IndexModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }

        public IList<Employee> Employee { get;set; }
        
        public string SearchString { get; set; }
        
        public int pageIndex { get; set; }
        
        public int totalPage { get; set; }
        
        public async Task OnGetAsync(string searchString)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "Admin")
            {
                RedirectToPage("../Error");
            }
            
            
            //TODO: fill in url
            var response = await apiClient.GetAsync("");
            var dataString = await response.Content.ReadAsStringAsync();
            Employee = JsonSerializer.Deserialize<IEnumerable<Employee>>(dataString, jsonOption).ToList();
        }
    }
}
