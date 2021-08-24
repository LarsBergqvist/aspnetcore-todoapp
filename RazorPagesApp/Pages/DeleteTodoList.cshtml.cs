using System.Security.Claims;
using System.Threading.Tasks;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TodoApp.Pages
{
    public class DeleteTodoListModel : PageModel
    {
        private readonly ITodoListRepository _repo;

        public DeleteTodoListModel(ITodoListRepository repo)
        {
            _repo = repo;
        }

        public async Task OnGet([FromQuery] string id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                // Todo: handle error
                return;
            }

            await _repo.Delete(userId.Value, id);
        }
    }
}
