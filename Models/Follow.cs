using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Models
{
    public class Follow
    {
        public string FollowingId { get; set; }   
        public string FollowerId { get; set; }
        public NETUser Follower { get; set; }
        public NETUser Following { get; set; }
    }
}
