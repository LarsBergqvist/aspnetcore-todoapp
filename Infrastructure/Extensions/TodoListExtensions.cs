using Infrastructure.Data;
using Core.Models;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class TodoListExtensions
    {
        internal static TodoList ToModel(this TodoListEntity entity)
        {
            var model = new TodoList
            {
                Id = entity.TodoListEntityId,
                Title = entity.Title,
                UserId = entity.UserId
            };

            foreach(var item in entity.Items)
            {
                model.Items.Add(item.ToModel());
            }

            return model;
        }

        internal static TodoItem ToModel(this TodoItemEntity entity)
        {
            var model = new TodoItem
            {
                Id = entity.TodoItemEntityId,
                Text = entity.Text
            };

            return model;
        }

        internal static void UpdateEntityFromModel(this TodoListEntity entity, TodoList model)
        {
            entity.Title = model.Title;

            // Handle removed items
            var itemIdsToRemove = new HashSet<int>();
            foreach(var item in model.Items)
            {
                if (item.Removed && item.Id != 0)
                {
                    itemIdsToRemove.Add(item.Id);
                }
            }
            entity.Items.RemoveAll(i => itemIdsToRemove.Contains(i.TodoItemEntityId));

            foreach (var item in model.Items.Where(i => !i.Removed))
            {
                if (item.Id == 0)
                {
                    // Handle new item
                    entity.Items.Add(item.ToEntity());
                }
                else
                {
                    var matched = entity.Items.FirstOrDefault(ent => ent.TodoItemEntityId == item.Id);
                    if (matched != null)
                    {
                        // Handle updated item
                        matched.Text = item.Text;
                    }
                }
            }
        }

        internal static TodoItemEntity ToEntity(this TodoItem model)
        {
            var entity = new TodoItemEntity
            {
                TodoItemEntityId = model.Id,
                Text = model.Text
            };

            return entity;
        }

    }
}
