using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETweet.Models
{
    public class Tweet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public bool IsReply { get; set; }

        public int? FromID { get; set; }

        public string Text { get; set; }

        public string Picture { get; set; }

        public DateTime Date { get; set; }

        public ICollection<React> Reacts { get; set; }

        public string UserRefID { get; set; }

        [ForeignKey("UserRefID")]
        public NETUser NETUser { get; set; }
    }
}
