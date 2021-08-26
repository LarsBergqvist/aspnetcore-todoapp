using Core.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Console.Tests
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITodoService _service;

        public Worker(ILogger<Worker> logger, ITodoService service)
        {
            _logger = logger;
            _service = service;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var userId = "testuserid";

            _logger.LogInformation("Adds a new list");
            var storedList = await _service.CreateList(userId, "test list 1");
            _logger.LogInformation($"Stored list with id: {storedList.Id}");

            _logger.LogInformation("Updates list");
            var listToUpdate = await _service.GetListDetails(userId, storedList.Id);
            listToUpdate.Title = "test list 1_1";
            listToUpdate.Items.Add(new Core.Models.TodoItem { Text = "todo1" });
            listToUpdate.Items.Add(new Core.Models.TodoItem { Text = "todo2" });
            await _service.Update(listToUpdate);

            _logger.LogInformation("Updates an item in the list");
            listToUpdate.Items[1].Text = "todo2 updated";
            await _service.Update(listToUpdate);

            _logger.LogInformation("Fetch all lists for the user");
            var allLists = await _service.GetAllLists(userId);
            foreach(var list in allLists)
            {
                _logger.LogInformation($"Id: {list.Id}, Title {list.Title}, UserId {list.UserId}");
                foreach(var item in list.Items)
                {
                    _logger.LogInformation($"ItemId: {item.Id}, Text: {item.Text}");
                }
            }

           _logger.LogInformation("Deletes the list");
            await _service.DeleteList(userId, storedList.Id);
            return;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
