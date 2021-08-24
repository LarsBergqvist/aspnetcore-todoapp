using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly TodoDbContext _context;
        public TodoListRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoList> Create(string userId, string title)
        {
            var list = new TodoList
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Title = title
            };
            await _context.TodoLists.AddAsync(list);
            await _context.SaveChangesAsync();
            return list;
        }

        public async Task Delete(string userId, string listId)
        {
            var lists = await _context.TodoLists.Where(l => l.UserId == userId && l.Id == listId).ToListAsync();
            if (lists.Count != 1)
            {
                throw new Exception("Could not delete list");
            }

            _context.TodoLists.Remove(lists[0]);

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<TodoList>> GetAll(string userId)
        {
            return await _context.TodoLists.Where(l => l.UserId == userId).ToListAsync();
        }

        public TodoList Get(string userId, string listId)
        {
            throw new NotImplementedException();
        }

        public Task<TodoList> Update(string userId, string title)
        {
            throw new NotImplementedException();
        }
    }
}
