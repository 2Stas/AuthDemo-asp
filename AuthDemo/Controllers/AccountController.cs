using AuthDemo.Data;
using AuthDemo.DTOs;
using AuthDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        int _id = 0;
        public AccountController(AppDbContext obj) {
            _db = obj;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserDto objDto)
        {
            var isUserExist = _db.Users.FirstOrDefault(objUser => objUser.email == objDto.Email);
            if (isUserExist != null)
            {
                return View(objDto);
            }

            var user = new User {
                id = _id,
                name = objDto.Name,
                email = objDto.Email,
                password = objDto.Password,
                role = objDto.Role
            };

            _id++;

            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto obj)
        {
            var user = _db.Users.FirstOrDefault(user => user.email == obj.Email && user.password == obj.Password);
            if (user == null)
            {
                return View();
            }

            if (user.role == AuthDemo.Models.User.RolePro)
            {
                return Redirect($"/all?currentUserId={user.id}");
            }

            return Redirect($"/me/{user.id}");
        }

    }
}
