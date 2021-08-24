using System.ComponentModel.DataAnnotations;

namespace RazorPagesApp.Models
{
    public class TodoListCreateModel
    {
        [Required]
        public string Title { get; set; }
    }
}
