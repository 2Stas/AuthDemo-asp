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

        public ActionResult Index(int id)
        {
            var user = _db.Users.FirstOrDefault(obj => id ==obj.id);
            if (user ==null)
            {
                return NotFound();
            }

            var userView = new UserDto
            {
                Name = user.name,
                Email = user.email
            };
            return View(userView);
        }
    }
}
