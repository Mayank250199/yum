using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YumApi.Data;
using YumApi.Models;
using YumApi.Services;

namespace YumApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly YumDbContext _context;
        private readonly IUserService _userService;

        public UsersController(YumDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<Token> AuthenticateUser([FromBody]LoginModel model)
        {
            var user = _userService.AuthenticateUser(model.Username, model.Password);

            if (user == null)
                return null;

            return await user;


        }


        [Authorize(Roles = "Admin")]
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostUser([FromBody]User user)
        {
            UserProfile userProfile = new UserProfile();

            userProfile.Username = user.Username;
            userProfile.Email = user.Email;


            _context.UserProfile.Add(userProfile);
            _context.SaveChanges();

            var role = _context.Role.SingleOrDefault(r => r.RoleName == "User");

            user.Role = role;
            //var hashpassword = _userService.EncodePasswordToBase64(user.Password);
            //user.Password = hashpassword;

            var UserProfile = _context.UserProfile.FirstOrDefault(r => r.Email == user.Email);
            Console.WriteLine(UserProfile.Id);
            user.UserProfileId = UserProfile.Id;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
