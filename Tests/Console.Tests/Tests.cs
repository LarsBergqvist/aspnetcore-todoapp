using Core.Services;
using Core.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Console.Tests
{
    public class Tests
    {
        private readonly ILogger<Tests> _logger;
        private readonly ITodoService _service;

        public Tests(ILogger<Tests> logger, ITodoService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task Run()
        {
            var userId = "testuserid";

            var storedList = await CreateANewTodoList(userId);

            await UpdateList(userId, storedList.Id);

            await FetchAllListsForUser(userId);
        }

        private async Task<TodoList> CreateANewTodoList(string userId)
        {
            _logger.LogInformation("Adds a new list");
            var storedList = await _service.CreateList(userId, "test list 1");
            _logger.LogInformation($"Stored list with id: {storedList.Id}");
            return storedList;
        }

        private async Task UpdateList(string userId, int listId)
        {
            _logger.LogInformation("Updates list");
            var listToUpdate = await _service.GetListDetails(userId, listId);
            listToUpdate.Title = "test list 1_1";
            listToUpdate.Items.Add(new TodoItem { Text = "todo1" });
            listToUpdate.Items.Add(new TodoItem { Text = "todo2" });
            await _service.Update(listToUpdate);

            _logger.LogInformation("Updates an item in the list");
            listToUpdate.Items[1].Text = "todo2 updated";
            await _service.Update(listToUpdate);
        }

        private async Task FetchAllListsForUser(string userId)
        {
            _logger.LogInformation("Fetch all lists for the user");
            var allLists = await _service.GetAllLists(userId);
            foreach (var list in allLists)
            {
                _logger.LogInformation($"Id: {list.Id}, Title {list.Title}, UserId {list.UserId}");
                foreach (var item in list.Items)
                {
                    _logger.LogInformation($"ItemId: {item.Id}, Text: {item.Text}");
                }
            }
        }

        private async Task DeleteList(string userId, int listId)
        {
            _logger.LogInformation("Deletes the list");
            await _service.DeleteList(userId, listId);
            return;
        }
    }
}
