﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Models
{
    public class React
    {
        public string UserID { get; set; }
        public int TweetID { get; set; }
        public Tweet Tweet { get; set; }
    }
}
