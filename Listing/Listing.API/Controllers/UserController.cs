using Listing.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Listing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public UserController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            List<User> list_users = new List<User>();
            list_users = await context.Users.ToListAsync();
            return list_users;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetById(int Id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == Id);
            return user;
        }

        [HttpGet("Email/{email}")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddUser(User user)
        {
            context.Add(user);
            await context.SaveChangesAsync();
            return user.Id;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<bool>> UpdateUserImage(int Id, string? url_image, string? institution)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == Id);
            if (user == null) return false;

            if(url_image != null)
            {
                user.UrlImage = url_image;
                context.Attach(user).State = EntityState.Modified;
            }

            if(institution != null)
            {
                user.Institution = institution;
                context.Attach(user).State = EntityState.Modified;
            }
            
            await context.SaveChangesAsync();
            return true;
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == Id);
            if (user == null) return NotFound();

            context.Remove(user);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
