using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETweet.Data;
using NETweet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Controllers
{
    public class ProfileController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<NETUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<NETUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
