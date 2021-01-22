using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Models
{
    public class React
    {
        public string UserID { get; set; }
        public int TweetID { get; set; }
        public virtual Tweet Tweet { get; set; }
        public virtual NETUser User { get; set; }
    }
}
