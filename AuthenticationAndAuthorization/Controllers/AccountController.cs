using AuthenticationAndAuthorization.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Frozen;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace AuthenticationAndAuthorization.Controllers
{
    public class AccountController : Controller
    {
        private static readonly List<Role> roles = new List<Role>
        {
            new Role(){ Id=1, Name="Teacher",IsActive=true},
            new Role(){ Id=2, Name="Student",IsActive=true},
            new Role(){ Id=3, Name="Manager",IsActive=true},
        };
        private static readonly List<User> users = new List<User>()
            {
                new User()
                {
                    Id=Guid.NewGuid(),
                    RoleId = roles.First(p=>p.Name=="Student").Id,
                    Role=roles.First(p=>p.Name=="Student"),
                    Firstname="student1",
                    Surname="students",
                    Email="student1@gmail.com",
                    Password="student1",
                    Phone="0000 000 00 00",
                    RegistrationDate= new DateTime(1990,1,1,14,10,10),
                    Username="student1",
                    IsActive=true
                },
                new User()
                {
                    Id=Guid.NewGuid(),
                    RoleId = roles.First(p=>p.Name=="Teacher").Id,
                    Role=roles.First(p=>p.Name=="Teacher"),
                    Firstname="teacher1",
                    Surname="teachers",
                    Email="teacher1@gmail.com",
                    Password="teacher1",
                    Phone="1111 111 11 11",
                    RegistrationDate= new DateTime(1960,1,1,14,10,10),
                    Username="teacher1",
                    IsActive=true
                },
                new User()
                {
                    Id=Guid.NewGuid(),
                    RoleId = roles.First(p=>p.Name=="Manager").Id,
                    Role=roles.First(p=>p.Name=="Manager"),
                    Firstname="manager1",
                    Surname="managers",
                    Email="manager1@gmail.com",
                    Password="manager1",
                    Phone="2222 222 22 22",
                    RegistrationDate= new DateTime(1960,1,1,14,10,10),
                    Username="manager1",
                    IsActive=true
                }
            };
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            LoginRequestModel loginModel=new LoginRequestModel();
            return View(loginModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginModel, string returnUrl=null)
         {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Lütfen Boş Alanları Doldurunuz!";
                return View(loginModel);
            }

            var matchedUsers = users.Where(u =>
                u.Username == loginModel.Username &&
                u.Password == loginModel.Password).ToList();
            if (matchedUsers.Count() > 0 && matchedUsers.Count() < 2)
            {
                var matchedUser = matchedUsers.First();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,matchedUser.Firstname),
                    new Claim(ClaimTypes.Surname,matchedUser.Surname),
                    new Claim(ClaimTypes.Email,matchedUser.Email),
                    new Claim(ClaimTypes.MobilePhone,matchedUser.Phone),
                    new Claim(ClaimTypes.DateOfBirth,matchedUser.RegistrationDate.ToString("hh:mm:ss - dd/MM/yyyy")),
                    new Claim(ClaimTypes.NameIdentifier,matchedUser.Username),
                };
                if (matchedUser.Role.Name=="Teacher")
                {
                    var claim = new Claim("Role", "Teacher") ;
                    claims.Add(claim);
                }
                if (matchedUser.Role.Name== "Manager")
                {

                    var claim = new Claim("Role", "Manager");
                    claims.Add(claim);
                }
                var identity = new ClaimsIdentity(claims, "MyCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties()
                {
                    IsPersistent = loginModel.RememberMe
                };

                await HttpContext.SignInAsync("MyCookie", claimsPrincipal, authProperties);
                return Redirect(returnUrl ?? "/");
            }
            ViewBag.ErrorMessage = "Hatalı Giriş Denemesi!";
            return View(loginModel);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Account/Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
