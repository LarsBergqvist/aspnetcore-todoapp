using System.Collections.Generic;

namespace Infrastructure.Data
{
    internal class TodoItemEntity
    {
        public int TodoItemEntityId { get; set; }
        public string Text { get; set; }
    }

    internal class TodoListEntity
    {
        public int TodoListEntityId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public ICollection<TodoItemEntity> Items { get; set; }
    }
}
