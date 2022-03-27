using System;
using Unity.Notifications.Android;
using UnityEngine;
public class NotificationManager : MonoBehaviour
{
    private readonly AndroidNotification _goBackNotification = new AndroidNotification()
    {
        Title = "Please go back!",
        Text = "You haven't played me for a long time!",
        FireTime = DateTime.Now.AddMinutes(5),
        RepeatInterval = new TimeSpan(0,2,0,0),
        LargeIcon = "gamelogo",
        SmallIcon = "gamelogo"
    };

    public void SendNotification()
    {
        AndroidNotificationCenter.SendNotification(_goBackNotification, "main");
    }
    private void CreateNotificationChannel()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var channel = new AndroidNotificationChannel()
            {
                Id = "main",
                Name = "AndroidDefaultChannel",
                Importance = Importance.Default,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }
    }
    private void Awake()
    {
        CreateNotificationChannel();
        SendNotification();
    }
}
