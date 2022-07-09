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
    public class DetailsModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public DetailsModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "Admin")
            {
                RedirectToPage("../Error");
            }
            
            if (id == null)
            {
                return NotFound();
            }

            //TODO: fill in url
            var response = await apiClient.GetAsync("Employee");
            var dataString = await response.Content.ReadAsStringAsync();
            Employee = JsonSerializer.Deserialize<Employee>(dataString, jsonOption);
            
            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
