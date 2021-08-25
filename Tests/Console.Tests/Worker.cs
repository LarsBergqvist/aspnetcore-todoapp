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
            var listToUpdate = await _service.GetListForUpdate(userId, storedList.Id);
            listToUpdate.Title = "test list 1_1";
            await _service.Update(listToUpdate);

            _logger.LogInformation("Fetch all lists for the user");
            var allLists = await _service.GetAllLists(userId);
            foreach(var list in allLists)
            {
                _logger.LogInformation($"Id: {list.Id}, Title {list.Title}, UserId {list.UserId}");
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
