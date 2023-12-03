using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SquidLabs.Platform.ConsoleHost;

public abstract class BaseConsoleHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly ILogger _logger;
    private int? _exitCode;
    private Task? _process;

    public BaseConsoleHostedService(
        ILogger<BaseConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _appLifetime = appLifetime;
    }

#pragma warning disable CS1998
    public async Task StartAsync(CancellationToken cancellationToken)
#pragma warning restore CS1998
    {
        _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

        CancellationTokenSource? _cancellationTokenSource = null;

        _appLifetime.ApplicationStarted.Register(async () =>
        {
            _logger.LogDebug("Application started");
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                _process = Process(_cancellationTokenSource.Token);
                await _process;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "The Task was canceled while still running. Waiting for it to return.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
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
            _logger.LogDebug("Application is stopping");
            _cancellationTokenSource?.Cancel();
        });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_process != null) await _process;
        _logger.LogDebug("Stopped Application with exit code");
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
    }

    public abstract Task Process(CancellationToken cancellationToken);
}