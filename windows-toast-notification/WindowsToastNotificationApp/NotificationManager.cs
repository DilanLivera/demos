using Microsoft.Toolkit.Uwp.Notifications;

namespace WindowsToastNotificationApp;

internal sealed class NotificationManager
{
    private ToastContentBuilder? _toastContentBuilder;

    internal void ShowBreakNotification()
    {
        _toastContentBuilder = new ToastContentBuilder()
            // .AddArgument("action", "viewConversation")
            // .AddArgument("conversationId", 9813)
            .AddText("Time for a break");

        ShowNotification();
    }

    internal void ShowBackToWorkNotification()
    {
        _toastContentBuilder = new ToastContentBuilder()
            // .AddArgument("action", "viewConversation")
            // .AddArgument("conversationId", 9813)
            .AddText("Time to go back to work");

        ShowNotification();
    }

    private void ShowNotification()
    {
        if (_toastContentBuilder is null)
        {
            return;
        }

        // Not seeing the Show() method?
        // Make sure you have version 7.0,
        // and if you're using .NET 6 (or later),
        // then your TFM must be net6.0-windows10.0.17763.0 or greater
        _toastContentBuilder.Show(toast =>
        {
            toast.ExpirationTime = DateTimeOffset.Now.AddMinutes(1);
        });
    }
}
