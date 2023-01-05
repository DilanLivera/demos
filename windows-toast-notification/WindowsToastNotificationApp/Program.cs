// See https://aka.ms/new-console-template for more information
// https://learn.microsoft.com/windows/apps/design/shell/tiles-and-notifications/send-local-toast

using WindowsToastNotificationApp;

Console.WriteLine("Pomodoro Clock");

const int workDurationInMinutes = 20;
const int breakDurationInMinutes = 5;

var notification = new NotificationManager();
var clock = new PomodoroClock(
    breakDurationInMinutes,
    notification,
    workDurationInMinutes);

Console.WriteLine("Starting the clock...");
await clock.Start();
