﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    [Header("ScriptableObjectNotification")]
    [SerializeField] private NotificationSO noteScriptable;
    private NotificationTriggerScriptable notificationTrigger;

    private void Awake()
    {
        notificationTrigger = GetComponent<NotificationTriggerScriptable>();
    }

    // returns the prompt we want to show on-screen when hovering over the item
    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.displayName);
    }

    // called when we interact with the item
    public void OnInteract()
    {
        Inventory.instance.AddItem(item);
        if (noteScriptable != null)
        {
            
            StartCoroutine(notificationTrigger.EnableNotification());
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}