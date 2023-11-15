using System.Timers;
using Timer = System.Timers.Timer;

namespace TimerRepro.Components.ReusableComponents;

public sealed partial class TimerExample : IDisposable
{
    private int _ticks = 0;

    private readonly Timer _timer = new(1000);
    private bool _disposed = false;

    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        _ticks += 1;
        InvokeAsync(StateHasChanged);
    }

    protected override void OnInitialized()
    {
        _timer.Elapsed += OnTimedEvent;
        _timer.Enabled = true;
        _timer.AutoReset = true;
        _timer.Start();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _timer.Elapsed -= OnTimedEvent;
            _timer.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}