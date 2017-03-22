using System.Collections.Generic;

namespace TwitterApi.Models
{

    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> GetAll();
        User Find(int key);
        User Remove(int key);
        void Update(User user);
        
    }
}