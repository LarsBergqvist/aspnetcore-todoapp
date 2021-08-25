using Core.Services;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Extensions;

namespace Infrastructure.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;
        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoList> GetListForUpdate(string userId, int id)
        {
            var matched = await _context.TodoLists.Where(l => l.TodoListEntityId == id && l.UserId == userId).ToListAsync();
            if (matched.Count != 1) { throw new Exception("Unable to find todo list for user"); }

            return matched[0].ToModel();
        }

        public async Task<TodoList> CreateList(string userId, string title)
        {
            var entity = new TodoListEntity
            {
                UserId = userId,
                Title = title
            };
            await _context.TodoLists.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }

        public async Task<IEnumerable<TodoList>> GetAllLists(string userId)
        {
            return await _context.TodoLists
                .Where(l => l.UserId == userId)
                .Select(e => new TodoList
                {
                    Id = e.TodoListEntityId,
                    Title = e.Title,
                    UserId = e.UserId
                })
                .ToListAsync();
        }

        public async Task DeleteList(string userId, int id)
        {
            var matched = await _context.TodoLists.Where(l => l.TodoListEntityId == id && l.UserId == userId).ToListAsync();
            if (matched.Count != 1) { throw new Exception("Unable to find todo list for user"); }

            _context.Remove(matched[0]);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TodoList list)
        {
            var matched = await _context.TodoLists.Where(l => l.TodoListEntityId == list.Id && l.UserId == list.UserId).ToListAsync();
            if (matched.Count != 1) { throw new Exception("Unable to find todo list for user"); }

            var entity = matched[0];

            entity.Title = list.Title;

            await _context.SaveChangesAsync();
        }
    }
}
