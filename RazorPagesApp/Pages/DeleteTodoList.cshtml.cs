using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using RazorPagesApp.Pages;

namespace TodoApp.Pages
{
    public class DeleteTodoListModel : PageModelBase
    {
        private readonly ITodoService _service;

        public DeleteTodoListModel(ITodoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet([FromQuery] int id)
        {
            var userId = GetUserId();
            if (userId == null) { return RedirectToPage("./Identity/Account/Login"); }

            await _service.DeleteList(userId, id);

            return Page();

            // TODO: show status and back to list link
        }
    }
}
