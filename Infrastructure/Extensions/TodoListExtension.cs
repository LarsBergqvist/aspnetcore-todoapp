using Infrastructure.Data;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class TodoListExtension
    {
        internal static TodoList ToModel(this TodoListEntity entity)
        {
            var model = new TodoList
            {
                Id = entity.TodoListEntityId,
                Title = entity.Title,
                UserId = entity.UserId
            };

            return model;
        }
    }
}
