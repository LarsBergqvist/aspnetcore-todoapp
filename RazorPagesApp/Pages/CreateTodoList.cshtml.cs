using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Repositories;
using System.Security.Claims;
using RazorPagesApp.Models;

namespace TodoApp.Pages
{
    public class CreateTodoListModel : PageModel
    {
        private readonly ITodoListRepository _repo;

        public CreateTodoListModel(ITodoListRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TodoListCreateModel TodoListModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("./Identity/Account/Login");
            }

            await _repo.Create(userId.Value, TodoListModel.Title);

            return RedirectToPage("./TodoLists");
        }
    }
}
