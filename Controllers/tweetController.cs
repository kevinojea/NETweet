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

        public async Task<IActionResult> Like(int ID)
        {
            React react = new React
            {
                UserID = _userManager.GetUserId(User),
                TweetRefID = ID
            };
            _context.Add(react);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Dislike(int ID)
        {
            React react = await _context.React
                .FirstOrDefaultAsync(t => t.TweetRefID.Equals(ID) && t.UserID.Equals(_userManager.GetUserId(User)));
            _context.Remove(react);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> DeleteTweet (int ID)
        {
            Tweet tweet = await _context.Tweet
                .Include(i => i.Reacts)
                .FirstOrDefaultAsync(t => t.ID.Equals(ID));
            _context.Remove(tweet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
