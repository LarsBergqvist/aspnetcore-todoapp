using System.ComponentModel.DataAnnotations;

namespace RazorPagesApp.Models
{
    public class TodoListUpdateModel
    {
        [Required]
        public string Title { get; set; }
    }
}
