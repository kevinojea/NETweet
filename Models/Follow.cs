using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Models
{
    public class Follow
    {
        public string UserID { get; set; }
        public string Following { get; set; }
        public virtual NETUser User { get; set; }
    }
}
