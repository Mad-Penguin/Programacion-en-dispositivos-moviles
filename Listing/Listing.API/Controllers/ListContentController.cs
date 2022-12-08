using Listing.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Listing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListContentController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public ListContentController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet("ListID/{list_id:int}")]
        public async Task<List<ListContent>> GetByListId(int list_id)
        {
            List<ListContent> list_list_content = new List<ListContent>();
            list_list_content = await context.ListsContents.Where(x => x.ListId == list_id).ToListAsync();
            return list_list_content;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddListContent(ListContent list_content)
        {
            context.Add(list_content);
            await context.SaveChangesAsync();
            return list_content.Id;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<bool>> UpdateListContent(int Id, string content)
        {
            var list_content = context.ListsContents.FirstOrDefault(x => x.Id == Id);
            if (list_content == null) return false;

            list_content.Content = content;

            await context.SaveChangesAsync();
            return true;
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> RemoveListContent(int Id)
        {
            var list_content = context.ListsContents.FirstOrDefault(x => x.Id == Id);
            if (list_content == null) return NotFound();

            context.Remove(list_content);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
