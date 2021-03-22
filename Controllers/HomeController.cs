using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NETweet.Data;
using NETweet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<NETUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<NETUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
            
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var follow = await _context.Follows
                .Where(f => f.FollowerId.Equals(_userManager.GetUserId(User)))
                .SelectMany(s => s.Follower.Tweets)
                .Include(i => i.NETUser)
                .ToListAsync();

                var usertweets = await _context.Tweet
                    .Where(t => t.UserRefID.Equals(_userManager.GetUserId(User)))
                    .Include(i => i.NETUser)
                    .ToListAsync();

                follow.AddRange(usertweets);

                follow = follow.OrderByDescending(d => d.Date).ToList();
                return View(follow);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }


        public async Task<IActionResult> CreateTweet(string Text, int IsReply, int FromID)
        {
            if (ModelState.IsValid)
            {
                int? fromID = (IsReply == 0) ? null : FromID;

                Tweet tweet = new()
                {
                    Text = Text,
                    IsReply = Convert.ToBoolean(IsReply),
                    FromID = fromID,
                    UserRefID = _userManager.GetUserId(User),
                    Date = DateTime.Now,
                };
                _context.Add(tweet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
