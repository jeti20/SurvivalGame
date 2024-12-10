using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationTriggerScriptable : MonoBehaviour
{
    [Header("UI Content")]
    [SerializeField] private TextMeshProUGUI notificationTextUI;
    [SerializeField] private Image characterIconUI;

    [Header("ScriptableObject")]
    [SerializeField] public NotificationSO noteScriptable;

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
        if (other.CompareTag("Player") && noteScriptable.removeAfterExit && noteScriptable.disableTrigger)
        {
            RemoveNotificationWithTrigger();
        }
        else
        {
            RemoveNotificationWithTrigger();
        }
    }

    public IEnumerator EnableNotification()
    {
        notificationAnim.Play("NotificationFadeIn");
        notificationTextUI.text = noteScriptable.notificationMessage; // Przypisanie tekstu z pola `string`
        characterIconUI.sprite = noteScriptable.yourIcon;

        if (noteScriptable.disableAfterTimer && noteScriptable.disableTrigger)
        {
            Debug.Log("StartRutynyNotifikacji");
            yield return new WaitForSeconds(noteScriptable.disabletimer);
            RemoveNotificationWithTrigger();
        }
        else
        {
            yield return new WaitForSeconds(noteScriptable.disabletimer);
            RemoveNotificationWithOutTrigger();
        }
    }

    private  void RemoveNotificationWithTrigger()
    {
        Debug.Log("KoniecRutynyNotifikacji");
        notificationAnim.Play("NotificationFadeOut");
        gameObject.SetActive(false);
    }

    private void RemoveNotificationWithOutTrigger()
    {
        Debug.Log("KoniecRutynyNotifikacji");
        notificationAnim.Play("NotificationFadeOut");
        //gameObject.SetActive(false);
    }
}
