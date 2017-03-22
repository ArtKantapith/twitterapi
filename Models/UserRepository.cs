using System.Collections.Generic;

namespace TwitterApi.Models
{
    class UserRepository : IUserRepository
    {
        private readonly TwitterContext _context;

        public UserRepository(TwitterContext context)
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User Find(int key)
        {
            return _context.Users.Find(key);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User Remove(int key)
        {
            User user = Find(key);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}