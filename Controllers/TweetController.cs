using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwitterApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterApi.Controllers
{

    [Route("api/[controller]")]
    public class TweetController : Controller
    {
        public class MyUser
        {
            public string sessionID { get; set; }
            public string userID { get; set; }
        }
        public ITweetRepository _tweets { get; set; }
        private static HttpClient client = new HttpClient();

        protected string GetUserIdFromSession(string sessionid)
        {
            //FIXME
            HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/session/" + sessionid).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                MyUser user = JsonConvert.DeserializeObject<MyUser>(result);
                return user.userID;
            }
            else
            {
                return null;
            }
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<Tweet> Get(DateTime recent, string session_id)
        {
            if(session_id != null && session_id != "" && recent != null) {
                return _tweets.GetAllUserRecent(GetUserIdFromSession(session_id), recent);
            } else if(recent != null) {
                return _tweets.GetAllRecent(recent);
            }
            return _tweets.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Tweet Get(int id)
        {
            return _tweets.Find(id);
        }
        

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]Tweet tweet, string sessionid)
        {
            if (sessionid == null || sessionid == "")
            {
                return BadRequest();
            }
            tweet.Owner = Int32.Parse(GetUserIdFromSession(sessionid));
            _tweets.Add(tweet);
            return Ok();
        }
    }
}
