using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services
{
    public interface ITodoService
    {
        Task<TodoList> GetListDetails(string userId, int id);

        Task<TodoList> CreateList(string userId, string title);

        Task Update(TodoList list);

        Task<IEnumerable<TodoList>> GetAllLists(string userId);

        Task DeleteList(string userId, int id);
    }
}
