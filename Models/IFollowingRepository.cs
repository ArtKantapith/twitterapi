using System.Collections.Generic;

namespace TwitterApi.Models
{
    public interface IFollowingRepository
    {
        void Add(int user, int following);
        IEnumerable<Following> GetAll();
        IEnumerable<Following> GetAllByUser(string userid);
        Following Find(int key);
        Following Remove(int key);
        void Update(Following following);

    }
}
                    