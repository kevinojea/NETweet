using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Models
{
    public class NETUser : IdentityUser
    {
        [PersonalData]
        public string Image { get; set; }
        [PersonalData]
        public string Description { get; set; }
        public ICollection<Tweet> Tweets { get; set; }
        public ICollection<Follow> Follower { get; set; }
        public ICollection<Follow> Following { get; set; }
    }
}
