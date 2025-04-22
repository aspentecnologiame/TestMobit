using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestMobit.Worker.WorkerService
{
    public class Worker : BackgroundService
    {
        private static IConfiguration? Configuration { get; set; }
        public static ServiceProvider? ServiceProvider { get; set; }

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Parando o Worker Consumer");
            await base.StopAsync(cancellationToken);
        }
    }
}
