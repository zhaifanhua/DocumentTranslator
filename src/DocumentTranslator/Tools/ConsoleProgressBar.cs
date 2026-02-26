using System.Diagnostics;

namespace DocumentTranslator.Tools;

/// <summary>
/// Simple console progress bar.
/// </summary>
public sealed class ConsoleProgressBar : IDisposable
{
    private readonly int _total;
    private readonly int _barWidth;
    private readonly object _renderLock = new();
    private readonly Stopwatch _stopwatch;
    private readonly TimeSpan _renderInterval;
    private int _current;
    private DateTime _lastRenderAt;

    public ConsoleProgressBar(int total, int barWidth = 30, TimeSpan? renderInterval = null)
    {
        _total = Math.Max(total, 1);
        _barWidth = Math.Clamp(barWidth, 10, 60);
        _renderInterval = renderInterval ?? TimeSpan.FromMilliseconds(120);
        _stopwatch = Stopwatch.StartNew();
        _lastRenderAt = DateTime.MinValue;
        Render(0);
    }

    public void Increment(int delta = 1)
    {
        var current = Interlocked.Add(ref _current, delta);
        Render(current);
    }

    private void Render(int current)
    {
        current = Math.Clamp(current, 0, _total);
        var now = DateTime.UtcNow;
        if (current < _total && now - _lastRenderAt < _renderInterval)
        {
            return;
        }

        lock (_renderLock)
        {
            now = DateTime.UtcNow;
            if (current < _total && now - _lastRenderAt < _renderInterval)
            {
                return;
            }
            _lastRenderAt = now;

            var percent = (double)current / _total;
            var filled = (int)Math.Round(percent * _barWidth);
            var bar = new string('#', filled) + new string('-', _barWidth - filled);

            var elapsed = _stopwatch.Elapsed;
            var eta = current == 0
                ? TimeSpan.Zero
                : TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds / current * (_total - current));

            var elapsedStr = elapsed.ToString(@"hh\:mm\:ss");
            var etaStr = current == 0 ? "--:--:--" : eta.ToString(@"hh\:mm\:ss");

            Console.Write($"\r[{bar}] {current}/{_total} {percent:P0}  Elapsed:{elapsedStr}  ETA:{etaStr}");
            if (current >= _total)
            {
                Console.WriteLine();
            }
        }
    }

    public void Dispose()
    {
        Render(_total);
    }
}