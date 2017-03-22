using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterApi.Models
{
    public class Tweet
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TweetID { get; set; }

        public int Owner { get; set; }
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime WhenCreated { get; set; }

    }

}