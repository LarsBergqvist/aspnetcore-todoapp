using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Models;
using Core.Repositories;
using System.Security.Claims;

namespace TodoApp.Pages
{
    public class TodoListsModel : PageModel
    {
        private readonly ITodoListRepository _repo;
        public IEnumerable<TodoList> TodoLists;

        public TodoListsModel(ITodoListRepository repo)
        {
            _repo = repo;
        }

        public IList<TodoList> TodoList { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            TodoLists = await _repo.GetAll(userId.Value);

            return Page();
        }
    }
}
