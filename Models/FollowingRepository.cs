using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitterApi.Models
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly TwitterContext _context;
        public FollowingRepository(TwitterContext context)
        {
            _context = context;
        }
        public void Add(int userid, int followingid)
        {
            Following fl = new Following();
            fl.User = _context.Users.Find(userid);
            fl.Follow = _context.Users.Find(followingid);
            _context.Followings.Add(fl);
        }

        public Following Find(int key)
        {
            return _context.Followings.Find(key);
        }

        public IEnumerable<Following> GetAll()
        {
            return _context.Followings;
        }

        public IEnumerable<Following> GetAllByUser(string userid)
        {
            int id = Int32.Parse(userid);
            return _context.Followings
                    .Where(b => b.User.UserID == id)
                    .ToList();
        }

        public Following Remove(int key)
        {
            Following following = Find(key);
            _context.Followings.Remove(following);
            return following;
        }

        public void Update(Following following)
        {
            _context.Followings.Update(following);
        }
    }
}