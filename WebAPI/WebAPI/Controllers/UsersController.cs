using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DB;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AutorizationWEBContext _context;

        public UsersController(AutorizationWEBContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserId/{id}")]
        public async Task<ActionResult> GetUserId(int id)
        {
            var selectUser = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
            return Ok(selectUser == null ? 0 : new { selectUser.FirstName, selectUser.LastName, selectUser.MiddleName, selectUser.Photo });
        }

        [HttpPost("RestorePassword/{email}")]
        public async Task<IActionResult> RestorePassword(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(us => us.Email == email);
            return Ok(user == null ? -1 : user.IdUser);
        }

        [HttpPost("ChangePassword/{email}/{password}")]
        public async Task<IActionResult> ChangePassword(string email, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(us => us.Email == email);

            user.Password = password;

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'AutorizationWEBContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        [HttpPost("IsLogged/{email}/{password}")]
        public async Task<ActionResult<User>> IsLogged(string email, string password)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AutorizationWEBContext.Users'  is null.");
            }
            var searchUser = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            await _context.SaveChangesAsync();
            return Ok(searchUser == null ? 0 : searchUser.IdUser);
        }

        [HttpPost("Registration/{fio}/{email}/{password}")]
        public async Task<ActionResult<User>> Registration(string fio, string email, string password)
        {
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'AutorizationWEBContext.Users'  is null.");
                }
                var FIO = fio.Split(" ");
                User addUser = new User()
                {
                    Email = email,
                    FirstName = FIO[0],
                    LastName = FIO[1],
                    MiddleName = FIO[2],
                    Password = password,
                };
                _context.Users.Add(addUser);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
