using System.Threading.Tasks;
using Core.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RazorPagesApp.Pages
{
    public class EditTodoListModel : PageModelBase
    {
        private readonly ITodoService _service;

        public EditTodoListModel(ITodoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var userId = GetUserId();
            if (userId == null) { return RedirectToPage("./Identity/Account/Login"); }

            TodoList = await _service.GetListDetails(userId, id);
            return Page();
        }

        [BindProperty]
        public TodoList TodoList { get; set; }

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

            await _service.Update(TodoList);

            return RedirectToPage("./TodoLists");
        }
    }
}
