using Listing.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Listing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public RatingController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet("ListID/{list_id:int}")]
        public async Task<List<Rating>> GetByListId(int list_id)
        {
            List<Rating> list_ratings_of_list = new List<Rating>();
            list_ratings_of_list = await context.Ratings.Where(x => x.ListId == list_id).ToListAsync();
            return list_ratings_of_list;
        }

        [HttpGet("UserID/{user_id:int}")]
        public async Task<List<Rating>> GetByUserId(int user_id)
        {
            List<Rating> list_ratings_made_by_user = new List<Rating>();
            list_ratings_made_by_user = await context.Ratings.Where(x => x.UserId == user_id).ToListAsync();
            return list_ratings_made_by_user;
        }

        /*[HttpGet("TopRated/{top:int}")]
        public async Task<List<Rating>> GetTopRated(int top = 10)
        {
            List<Rating> list_ratings_made_by_user = new List<Rating>();
            list_ratings_made_by_user = await context.Ratings.Where(x => x.UserId == user_id).ToListAsync();
            return list_ratings_made_by_user;
        }*/

        [HttpPost]
        public async Task<ActionResult<int>> AddRating(Rating rating)
        {
            context.Add(rating);
            await context.SaveChangesAsync();
            return rating.Id;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<bool>> UpdateRating(int Id, float Stars)
        {
            var rating = context.Ratings.FirstOrDefault(x => x.Id == Id);
            if (rating == null) return false;

            rating.Stars = Stars;

            await context.SaveChangesAsync();
            return true;
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> RemoveRating(int Id)
        {
            var rating = context.Ratings.FirstOrDefault(x => x.Id == Id);
            if (rating == null) return NotFound();

            context.Remove(rating);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
