using AuthDemo.Data;
using AuthDemo.DTOs;
using AuthDemo.Models;
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
                Id = user.id,
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
            if (currentUser == null)
            {
                return NotFound();
            }

            if (currentUser.role != AuthDemo.Models.User.RolePro)
            {
                return Forbid();
            }

            ViewData["CurrentUserId"] = currentUserId;
            return View("GetAll", _db.Users.ToList());
        }

        [HttpPost]
        public IActionResult Delete(int id, int currentUserId)
        {
            var currentUser = _db.Users.FirstOrDefault(user => user.id == currentUserId);
            if (currentUser == null)
            {
                return NotFound();
            }

            if (currentUser.role != AuthDemo.Models.User.RolePro)
            {
                return Forbid();
            }

            var user = _db.Users.FirstOrDefault(user => user.id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }

            return RedirectToAction("GetAllUsers", new { currentUserId });
        }
    }
}
