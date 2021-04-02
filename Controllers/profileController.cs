using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETweet.Data;
using NETweet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Controllers
{
    public class profileController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<NETUser> _userManager;

        public profileController(ApplicationDbContext context, UserManager<NETUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task <IActionResult> Index(string u)
        {
            var user = new NETUser();
            if(String.IsNullOrEmpty(u))
            {
                if (User.Identity.IsAuthenticated)
                {
                    user = await _userManager.GetUserAsync(User);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                user = await _userManager.FindByNameAsync(u);
            }

            var userTweets = await _context.Users
                .Where(f => f.Equals(user))
                .Include(i => i.Following)
                .Include(i => i.Follower)
                .Include(i => i.Tweets.OrderByDescending(d=>d.Date))
                    .ThenInclude(i=>i.Reacts)
                .Include(i => i.Tweets)
                    .ThenInclude(i => i.NETUser)
                .FirstOrDefaultAsync();
            return View(userTweets);
        }

        public IActionResult Edit()
        {
            return PartialView("_Edit");
        }
    }
}
