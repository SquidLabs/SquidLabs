using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SquidLabs.Platform.Console;

public abstract class ConsoleHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly ILogger _logger;
    private int? _exitCode;
    private Task _process;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _appLifetime = appLifetime;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

        CancellationTokenSource? _cancellationTokenSource = null;

        _appLifetime.ApplicationStarted.Register(async () =>
        {
            _logger.LogDebug("SquidLabs.Tentacles.Application has started");
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);


            try
            {
                _process = Process(_cancellationTokenSource.Token);
                await _process;
            }
            catch (TaskCanceledException)
            {
                // This means the application is shutting down, so just swallow this exception
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception!");
                _exitCode = 1;
            }
            finally
            {
                // Stop the application once the work is done
                _appLifetime.StopApplication();
            }
        });

        _appLifetime.ApplicationStopping.Register(() =>
        {
            _logger.LogDebug("SquidLabs.Tentacles.Application is stopping");
            _cancellationTokenSource?.Cancel();
        });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_process != null) await _process;
        _logger.LogDebug($"Exiting with return code: {_exitCode}");

        // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
    }

    public abstract Task Process(CancellationToken cancellationToken);
}