using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<TodoList>> GetAll(string userId);
        TodoList Get(string userId, string listId);
        Task<TodoList> Create(string userId, string title);
        Task<TodoList> Update(string userId, string title);
        Task Delete(string userId, string listId);
    }
}
