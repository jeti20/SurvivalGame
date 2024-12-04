using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationtriggerEvent : MonoBehaviour
{
    [Header("UI Content")]
    [SerializeField] private TextMeshProUGUI notificationTextUI;
    [SerializeField] private Image characterIconUI;

    [Header("Message Customisation")]
    [SerializeField] private Sprite yourIcon;
    [SerializeField] [TextArea] private string notificationMessage;

    [Header("Notification Removal")]
    [SerializeField] private bool removeAfterExit = false;
    [SerializeField] private bool disableAfterTimer = false;
    [SerializeField] float disabletimer = 4f;

    [Header("Notification Animation")]
    [SerializeField] private Animator notificationAnim;
    private BoxCollider objectColider;

    private void Awake()
    {
        objectColider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EnableNotification());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.CompareTag("Player") && removeAfterExit)
        {
            RemoveNotification();
        }
    }

    IEnumerator EnableNotification()
    {
        notificationAnim.Play("NotificationFadeIn");
        notificationTextUI.text = notificationMessage; // Przypisanie tekstu z pola `string`
        characterIconUI.sprite = yourIcon;

        if (disableAfterTimer)
        {
            yield return new WaitForSeconds(disabletimer);
            RemoveNotification();
        }
    }

    private void RemoveNotification()
    {
        notificationAnim.Play("NotificationFadeOut");
        gameObject.SetActive(false);
    }
}
