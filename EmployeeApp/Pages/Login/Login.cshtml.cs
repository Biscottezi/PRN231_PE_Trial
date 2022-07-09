using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeApp.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public LoginModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }
        
        [BindProperty]
        [Required]
        public string Id { get; set; }
        
        [BindProperty]
        [Required]
        public string Password { get; set; }
        
        public string Message { get; set; }
        
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login/Login");
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            //TODO: fill in url
            var response = await apiClient.PostAsJsonAsync("Account/login", new LoginRequestModel()
            {
                Id = this.Id,
                Password = this.Password,
            });
            
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Message = "Wrong email or password";
                ViewData["Message"] = Message;
                return Page();
            }
            
            var dataString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<AccountDb>(dataString, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });
            
            if (user.AccountRole == 2)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Manager"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToPage("../Error"); //TODO: dien url
            }

            if (user.AccountRole == 1)
            {
                var claims = new List<Claim>
                { 
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                return RedirectToPage("../Employees/Index"); //TODO: fill in url
            }
            
            if (user.AccountRole == 3)
            {
                var claims = new List<Claim>
                { 
                    new Claim(ClaimTypes.Role, "Staff"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                return RedirectToPage("../Error"); //TODO: fill in url
            }
            return Page();
        }
    }
}