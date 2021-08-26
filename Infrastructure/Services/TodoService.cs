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

        public async Task<TodoList> GetListDetails(string userId, int id)
        {
            var matched = await _context.TodoLists
                .Where(l => l.TodoListEntityId == id && l.UserId == userId)
                .Select(l => new TodoList
                {
                    Title = l.Title,
                    Id = l.TodoListEntityId,
                    UserId = l.UserId,
                    Items = l.Items.Select(item => new TodoItem
                    {
                        Id = item.TodoItemEntityId,
                        Text = item.Text
                    }).ToList()
                })
                .ToListAsync();
            if (matched.Count != 1) { throw new Exception("Unable to find todo list for user"); }

            return matched[0];
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

            foreach(var item in matched[0].Items)
            {
                _context.Remove(item);
            }
            _context.Remove(matched[0]);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TodoList list)
        {
            var matched = await _context.TodoLists.Where(l => l.TodoListEntityId == list.Id && l.UserId == list.UserId)
                .Include("Items")
                .ToListAsync();
            if (matched.Count != 1) { throw new Exception("Unable to find todo list for user"); }

            var entity = matched[0];
            entity.UpdateEntityFromModel(list);

            await _context.SaveChangesAsync();
        }
    }
}
