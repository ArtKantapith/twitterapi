using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwitterApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public class MyUser
        {
            public string sessionID { get; set; }
            public string userID { get; set; }
        }
        public IUserRepository _users { get; set; }
        private static HttpClient client = new HttpClient();
        public UserController(IUserRepository users)
        {
            _users = users;
        }

        protected User GetUserFromSession(string sessionid)
        {
            //FIXME
            HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/session/" + sessionid).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                MyUser user = JsonConvert.DeserializeObject<MyUser>(result);
                return _users.Find(Int32.Parse(user.userID));
            }
            else
            {
                return null;
            }
        }
        // GET api/values
        [HttpGet]
        public IActionResult GetAll(string sessionid)
        {
            if(sessionid == null || sessionid == "") {
                return new ObjectResult(_users.GetAll());
            } else {
                User user = GetUserFromSession(sessionid);
                if(user == null) {
                    return NotFound();
                }
                return new ObjectResult(user);
            }
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            User user = _users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]User user)
        {
            _users.Add(user);
            return CreatedAtRoute("GetById", new {id = user.UserID}, user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if(user == null) {
                return BadRequest();
            }
            User _user = _users.Find(id);
            _user.UserID = id;
            _user.Name = user.Name;
            _users.Update(_user);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _users.Remove(id);
            return new NoContentResult();
        }
    }
}
