using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebApi.Contexts;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SQLiteContext _context;

        public UsersController(SQLiteContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByName([FromQuery] string name)
        {
            //return await _context.User.ToListAsync();
            var users = from u in _context.User
                           select u;

            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(u => u.Name.ToLower().Contains(name.ToLower()));
            }

            return await users.ToListAsync();
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            bool updateControl = false;

            if (user.Name.Trim() != userDTO.Name.Trim())
            {
                user.Name = userDTO.Name;
                updateControl = true;
            }

            if (user.Age != userDTO.Age)
            {
                user.Age = userDTO.Age;
                updateControl = true;
            }

            string userDBAddress = String.IsNullOrEmpty(user.Address) ? "" : user.Address.Trim();
            string userDTOAddress = String.IsNullOrEmpty(userDTO.Address) ? "" : userDTO.Address.Trim();

            if (userDBAddress != userDTOAddress)
            {
                user.Address = userDTO.Address;
                updateControl = true;
            }

            if (updateControl)
            {
                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok();
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
            }

            return BadRequest();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userDTO)
        {
            User user = new User
            {
                Name = userDTO.Name,
                Age = userDTO.Age,
                Address = userDTO.Address,
            };

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
