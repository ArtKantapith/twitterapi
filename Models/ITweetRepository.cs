using System;
using System.Collections.Generic;

namespace TwitterApi.Models
{

    public interface ITweetRepository
    {
        void Add(Tweet tweet);
        IEnumerable<Tweet> GetAll();
        IEnumerable<Tweet> GetAllRecent(DateTime minDateTime);
        IEnumerable<Tweet> GetAllUserRecent(string user, DateTime minDateTime);
        Tweet Find(int key);
        Tweet Remove(int key);
        void Update(Tweet tweet);
    }
}