using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETweet.Data;
using NETweet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Controllers
{
    public class searchController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<NETUser> _userManager;

        public searchController(ApplicationDbContext context, UserManager<NETUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string q)
        {
            if (!String.IsNullOrEmpty(q))
            {
                var users = await _context.Users
                    .Where(u => u.UserName.Contains(q))
                    .Include(i => i.Follower)
                    .ToListAsync();
                return View(users);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<JsonResult> Follow(string Id)
        {
            Follow follow = new Follow()
            {
                FollowerId = _userManager.GetUserId(User),
                FollowingId = Id
            };
            _context.Add(follow);
            await _context.SaveChangesAsync();
            return Json(follow);
        }

        public async Task<JsonResult> Unfollow (string Id)
        {
            var unfollow = await _context.Follows
                .FirstOrDefaultAsync(u => u.FollowerId.Equals(_userManager.GetUserId(User)) &&
                                          u.FollowingId.Equals(Id));
            _context.Remove(unfollow);
            await _context.SaveChangesAsync();
            return Json(unfollow);
        }

    }
}
