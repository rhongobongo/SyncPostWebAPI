using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyncPostWebAPI.DBContext;
using SyncPostWebAPI.Model;

namespace SyncPostWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly SyncPostDbContext _context;

        public UserAccountController(SyncPostDbContext context)
        {
            _context = context;
        }


        // POST: api/UserAccount
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserAccount>> PostUserAccount(UserAccount userAccount)
        {
            // Check if a user with the same username already exists
            if (UserAccountExists(userAccount.username))
            {
                return Conflict("User with the same username already exists."); // Return a 409 Conflict status code
            }

            _context.UserAccount.Add(userAccount);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Exception handling code if necessary
                throw; // You may want to handle or log the exception appropriately
            }

            // Return a response with a status code of 201 and the newly created resource
            return StatusCode(201, userAccount);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserAccount>> AuthenticateUser(LoginCredentials credentials)
        {
            var userAccount = await _context.UserAccount.FirstOrDefaultAsync(u => u.username == credentials.Username);

            if (userAccount == null || userAccount.password != credentials.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(userAccount);
        }

        private bool UserAccountExists(string userName)
        {
            return _context.UserAccount.Any(e => e.username == userName);
        }
    }
}
