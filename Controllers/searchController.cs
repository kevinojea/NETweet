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
                dynamic searchModel = new System.Dynamic.ExpandoObject();

                var users = await _context.Users
                    .Where(w => w.UserName
                        .Contains(q))
                    .Select(s => new NETUser
                    {
                        Id = s.Id,
                        UserName = s.UserName,
                        Following = _context.Follows
                            .Where(f=>f.FollowingId.Equals(s.Id) &&
                                      f.FollowerId.Equals(_userManager.GetUserId(User)))
                            .ToList(),
                        ConcurrencyStamp = null,
                        SecurityStamp = null
                    })
                    .ToListAsync();

                searchModel.Users = users.ToList();
                return View(searchModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetFollow (string Id)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(u => u.FollowerId.Equals(_userManager.GetUserId(User)) &&
                                          u.FollowingId.Equals(Id));
            return Json(follow);
        }

        public async Task<IActionResult> Follow(string Id)
        {
            Follow follow = new Follow()
            {
                FollowerId = _userManager.GetUserId(User),
                FollowingId = Id
            };
            _context.Add(follow);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Unfollow (string Id)
        {
            var unfollow = await _context.Follows
                .FirstOrDefaultAsync(u => u.FollowerId.Equals(_userManager.GetUserId(User)) &&
                                          u.FollowingId.Equals(Id));
            _context.Remove(unfollow);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
