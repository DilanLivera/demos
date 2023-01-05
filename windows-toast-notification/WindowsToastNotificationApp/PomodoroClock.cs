namespace WindowsToastNotificationApp;

internal sealed class PomodoroClock
{
    private readonly int _breakDurationInMinutes;
    private readonly NotificationManager _notificationManager;
    private readonly int _workDurationInMinutes;

    public PomodoroClock(
        int breakDurationInMinutes,
        NotificationManager notificationManager,
        int workDurationInMinutes)
    {
        if (workDurationInMinutes <= 0)
        {
            throw new ArgumentException(
                $"{nameof(workDurationInMinutes)} can not be less than or equal to 0");
        }

        if (breakDurationInMinutes <= 0)
        {
            throw new ArgumentException(
                $"{nameof(breakDurationInMinutes)} can not be less than or equal to 0");
        }

        _breakDurationInMinutes = breakDurationInMinutes;
        _workDurationInMinutes = workDurationInMinutes;
        _notificationManager = notificationManager;
    }

    internal async Task Start()
    {
        while (true)
        {
            Console.WriteLine($"Start Session: {DateTimeOffset.Now.LocalDateTime}");

            await Task.Delay(_workDurationInMinutes * 60 * 1000);

            _notificationManager.ShowBreakNotification();

            await Task.Delay(_breakDurationInMinutes * 60 * 1000);

            _notificationManager.ShowBackToWorkNotification();

            Console.WriteLine($"End Session: {DateTimeOffset.Now.LocalDateTime}");
        }
    }
}
