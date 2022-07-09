using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace EmployeeApp.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public EditModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }

        [BindProperty]
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
            var response = await apiClient.GetAsync("");
            var dataString = await response.Content.ReadAsStringAsync();
            Employee = JsonSerializer.Deserialize<Employee>(dataString, jsonOption);
            
            if (Employee == null)
            {
                return NotFound();
            }
           
            //TODO: fill in url
            var responseD = await apiClient.GetAsync("");
            var dataStringD = await responseD.Content.ReadAsStringAsync();
            var departments = JsonSerializer.Deserialize<IEnumerable<Department>>(dataStringD, jsonOption);
            ViewData["DepId"] = new SelectList(departments, "DepartmentId", "DepartmentName");
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await apiClient.PutAsJsonAsync($"", Employee);

            return RedirectToPage("./Index");
        }
    }
}
