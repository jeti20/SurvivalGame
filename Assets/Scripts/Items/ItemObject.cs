using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    [Header("ScriptableObjectNotification")]
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

        //check if the notificationTrigger component is assigned and has the appropriate configuration
        if (notificationTrigger != null && notificationTrigger.noteScriptable != null)
        {
            
            StartCoroutine(notificationTrigger.EnableNotification());
            DestroyAllChildren();  // Zniszczenie wszystkich dzieci obiektu
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to destroy all children of an object, only used when after picking up item  notification have to pops out
    private void DestroyAllChildren()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);  
        }
    }
}
