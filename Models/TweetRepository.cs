using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitterApi.Models
{
    class TweetRepository : ITweetRepository
    {
        private readonly TwitterContext _context;
        public TweetRepository(TwitterContext context)
        {
            _context = context;
        }
        public void Add(Tweet tweet)
        {
            tweet.WhenCreated = DateTime.Now;
            _context.Tweets.Add(tweet);
            _context.SaveChanges();
        }

        public Tweet Find(int key)
        {
            return _context.Tweets.Find(key);
        }

        public IEnumerable<Tweet> GetAll()
        {
            return _context.Tweets;
        }

        public IEnumerable<Tweet> GetAllRecent(DateTime minDateTime)
        {
            return _context.Tweets
                    .Where(b => b.WhenCreated >= minDateTime)
                    .ToList();
        }

        public IEnumerable<Tweet> GetAllUserRecent(string userid, DateTime minDateTime)
        {
            int id = Int32.Parse(userid);
            return _context.Tweets
                    .Where(b => b.WhenCreated >= minDateTime)
                    .Where(c => c.Owner == id)
                    .ToList();
        }

        public Tweet Remove(int key)
        {
            Tweet tweet = Find(key);
            _context.Tweets.Remove(tweet);
            return tweet;
        }

        public void Update(Tweet tweet)
        {
            _context.Tweets.Update(tweet);
        }
    }
}