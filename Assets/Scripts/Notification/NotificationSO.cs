using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName ="Notification")]
public class NotificationSO : ScriptableObject
{
    [Header("Message Customisation")]
    public Sprite yourIcon;
    [TextArea] public string notificationMessage;

    [Header("Notification Removal")]
    public bool removeAfterExit = false;
    public bool disableAfterTimer = false;
    public float disabletimer = 4f;
}
