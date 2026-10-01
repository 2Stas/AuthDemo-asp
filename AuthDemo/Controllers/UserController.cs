using AuthDemo.Data;
using AuthDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _db;
        public UserController(AppDbContext obj)
        {
            _db = obj;
        }

        [HttpGet("me/{id}")]
        public ActionResult GetMyData(int id)
        {
            var user = _db.Users.FirstOrDefault(obj => id ==obj.id);
            if (user ==null)
            {
                return NotFound();
            }

            var userView = new UserDto
            {
                Name = user.name,
                Email = user.email,
                Role = user.role
            };
            return View(userView);
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers(int currentUserId)
        {
            var currentUser = _db.Users.FirstOrDefault(curUs => curUs.id == currentUserId);
            if(currentUser == null)
            {
                return View();
            }
            var users = _db.Users.Select(u => new { u.id, u.name, u.email, u.role }).ToList();
            return View("GetAll");
        }
    }
}
