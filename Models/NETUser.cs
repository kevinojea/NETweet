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
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Tweet> Tweets { get; set; }
        public virtual ICollection<React> Reacts { get; set; }
    }
}
