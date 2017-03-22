using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwitterApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterApi.Controllers
{
    [Route("api/[controller]")]
    public class FollowingController : Controller
    {
        public class MyUser
        {
            public string sessionID { get; set; }
            public string userID { get; set; }
        }
        public IFollowingRepository _followings { get; set; }
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
        public IEnumerable<Following> GetAll(string sessionid)
        {
            string userid;
            if (sessionid == null || sessionid == "")
            {
                return _followings.GetAll();
            }
            else
            {
                userid = GetUserIdFromSession(sessionid);
                return _followings.GetAllByUser(userid);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Following GetById(int id)
        {
            return _followings.Find(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]int followingid, string sessionid)
        {
            string userid = GetUserIdFromSession(sessionid);
            if (userid == null || userid == "")
            {
                return BadRequest();
            }
            _followings.Add(Int32.Parse(userid), followingid);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _followings.Remove(id);
        }
    }
}
