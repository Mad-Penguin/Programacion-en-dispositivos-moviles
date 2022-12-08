using Listing.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Listing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public FavoriteController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet("UserID/{user_id:int}")]
        public async Task<List<Favorite>> GetByUserId(int user_id)
        {
            List<Favorite> list_favorite_list_user = new List<Favorite>();
            list_favorite_list_user = await context.Favorites.Where(x => x.UserId==user_id).ToListAsync();
            return list_favorite_list_user;
        }

        [HttpGet("ListID/{list_id:int}")]
        public async Task<List<Favorite>> GetByListId(int list_id)
        {
            List<Favorite> list_favorite_list_id = new List<Favorite>();
            list_favorite_list_id = await context.Favorites.Where(x => x.ListId == list_id).ToListAsync();
            return list_favorite_list_id;
        }

        [HttpGet("UserIDListID/{user_id:int}&{list_id:int}")]
        public async Task<ActionResult<Favorite>> GetByUserIdListId(int user_id, int list_id)
        {
            var row = await context.Favorites.FirstOrDefaultAsync(x => (x.ListId == list_id && x.UserId==user_id));
            return row;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddFavorite(Favorite fav)
        {
            context.Add(fav);
            await context.SaveChangesAsync();
            return fav.Id;
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> RemoveFavorite(int Id)
        {
            var fav = context.Favorites.FirstOrDefault(x => x.Id == Id);
            if (fav == null) return NotFound();

            context.Remove(fav);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
