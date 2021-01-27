using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CreateTweet([Bind("Text")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                tweet.UserID = _userManager.GetUserId(User);
                tweet.Date = DateTime.Now;
                _context.Add(tweet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tweet);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
