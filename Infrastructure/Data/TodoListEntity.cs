using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data
{
    [Table("TodoItem")]
    internal class TodoItemEntity
    {
        [Column("Id")]
        public int TodoItemEntityId { get; set; }
        public string Text { get; set; }
        [Column("TodoListId")]
        public int TodoListEntityId { get; set; }
    }

    [Table("TodoList")]
    internal class TodoListEntity
    {
        public TodoListEntity()
        {
            Items = new List<TodoItemEntity>();
        }
        [Column("Id")]
        public int TodoListEntityId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public List<TodoItemEntity> Items { get; set; }
    }
}
