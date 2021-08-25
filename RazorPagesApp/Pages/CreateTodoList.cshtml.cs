using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Services;
using RazorPagesApp.Models;
using RazorPagesApp.Pages;

namespace TodoApp.Pages
{
    public class CreateTodoListModel : PageModelBase
    {
        private readonly ITodoService _service;

        public CreateTodoListModel(ITodoService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TodoListCreateModel TodoListModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page();  }

            var userId = GetUserId();
            if (userId == null) { return RedirectToPage("./Identity/Account/Login");  }

            await _service.CreateList(userId, TodoListModel.Title);

            return RedirectToPage("./TodoLists");
        }
    }
}
