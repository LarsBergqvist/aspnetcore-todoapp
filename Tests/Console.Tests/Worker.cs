using Core.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Console.Tests
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITodoListRepository _repo;

        public Worker(ILogger<Worker> logger, ITodoListRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var userId = "testuserid";

            _logger.LogInformation("Adds a new list");
            var storedList = await _repo.Create(userId, "test list 1");
            _logger.LogInformation($"Stored list with id: {storedList.Id}");

            _logger.LogInformation("Fetch all lists for the user");
            var allLists = await _repo.GetAll(userId);
            foreach(var list in allLists)
            {
                _logger.LogInformation($"Id: {list.Id}, Title {list.Title}, UserId {list.UserId}");
            }

            _logger.LogInformation("Deletes the list");
            await _repo.Delete("testuserid", storedList.Id);
            return;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
