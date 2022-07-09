using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Models;

namespace EmployeeApp.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public CreateModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }

        public async Task<IActionResult> OnGet()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "Admin")
            {
                RedirectToPage("../Error");
            }
            
            var responseD = await apiClient.GetAsync("Department");
            var dataStringD = await responseD.Content.ReadAsStringAsync();
            var departments = JsonSerializer.Deserialize<IEnumerable<Department>>(dataStringD, jsonOption);
            ViewData["DepId"] = new SelectList(departments, "DepartmentId", "DepartmentName");
            
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //TODO: fill in url
            await apiClient.PostAsJsonAsync("Employee", Employee);

            return RedirectToPage("./Index");
        }
    }
}
