using Listing.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Listing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public ListController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<List>> GetTopRecent(int top = 10)
        {
            List<List> list_lists = new List< List >();
            list_lists = await context.Lists.ToListAsync();
            list_lists = list_lists.OrderByDescending(x => x.RegistrationDate).ToList();

            if (list_lists.Count >= top) list_lists = list_lists.Take(top).ToList();
            return list_lists;
        }

        [HttpGet("OwnerId/{owner_id:int}")]
        public async Task<List<List>> GetByOwnerId(int owner_id)
        {
            List<List> list_lists = new List<List>();
            list_lists = await context.Lists.ToListAsync();
            list_lists = list_lists.Where(x => x.OwnerId==owner_id).ToList();
            return list_lists;
        }

        [HttpGet("ListId/{list_id:int}")]
        public async Task<List> GetByListId(int list_id)
        {
            var list = context.Lists.FirstOrDefault(x => x.Id == list_id);
            return list;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddList(List list)
        {
            context.Add(list);
            await context.SaveChangesAsync();
            return list.Id;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<bool>> UpdateListTitle(int Id, string title)
        {
            var list = context.Lists.FirstOrDefault(x => x.Id == Id);
            if (list == null) return false;

            list.Title = title;
            context.Attach(list).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return true;
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteList(int Id)
        {
            var list = context.Lists.FirstOrDefault(x => x.Id == Id);
            if (list == null) return NotFound();

            context.Remove(list);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
