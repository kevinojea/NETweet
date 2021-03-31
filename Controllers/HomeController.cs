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
                var userTweets = await _context.Tweet
                    .Where(t => t.UserRefID.Equals(_userManager.GetUserId(User)))
                    .Include(i => i.NETUser)
                    .Include(r => r.Reacts)
                    .ToListAsync();

                var followingTweets = await _context.Follows
                    .Where(f => f.FollowerId.Equals(_userManager.GetUserId(User)))
                    .SelectMany(s=>s.Following.Tweets)
                    .Include(i=>i.Reacts)
                    .Include(i=>i.NETUser)
                    .ToListAsync();

                followingTweets.AddRange(userTweets);

                return View(followingTweets.OrderByDescending(d=>d.Date).ToList());
            }
            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
