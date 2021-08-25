using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class TodoList
    {
        public TodoList()
        {
            Items = new List<TodoItem>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public IList<TodoItem> Items { get; set; }
    }
}
