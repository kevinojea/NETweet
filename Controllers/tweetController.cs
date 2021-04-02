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
    public class tweetController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<NETUser> _userManager;
        public tweetController(ApplicationDbContext context, UserManager<NETUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<JsonResult> Like(int ID)
        {
            React react = new React
            {
                UserID = _userManager.GetUserId(User),
                TweetID = ID
            };
            _context.Add(react);
            await _context.SaveChangesAsync();
            return Json(react);
        }

        public async Task<JsonResult> Dislike(int ID)
        {
            React react = await _context.React
                .FirstOrDefaultAsync(t => t.TweetID.Equals(ID) && t.UserID.Equals(_userManager.GetUserId(User)));
            _context.Remove(react);
            await _context.SaveChangesAsync();
            return Json(react);
        }

        public async Task<IActionResult> CreateTweet(string Text)
        {
            if (ModelState.IsValid)
            {
                Tweet tweet = new()
                {
                    Text = Text,
                    //FromID = fromID,
                    UserRefID = _userManager.GetUserId(User),
                    Date = DateTime.Now,
                    NETUser = await _userManager.FindByIdAsync(_userManager.GetUserId(User)),
                    Reacts = new List<React>()
                };
                _context.Add(tweet);
                await _context.SaveChangesAsync();
                return PartialView("_Tweet", tweet);
            }
            return View();
        }

        public async Task<JsonResult> DeleteTweet (int ID)
        {
            Tweet tweet = await _context.Tweet
                .Include(i => i.Reacts)
                .FirstOrDefaultAsync(t => t.ID.Equals(ID));
            _context.Remove(tweet);
            await _context.SaveChangesAsync();
            return Json(tweet);
        }
    }
}
