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


    public GameObject recipeToActivate;

    private void Awake()
    {
        objectColider = GetComponent<BoxCollider>();
    }
    public bool talkedWithNPC = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EnableNotification());

            //checking if trigger has teleport tag
            if (gameObject.CompareTag("Teleport"))
            {
                talkedWithNPC = true;

                if (recipeToActivate != null)
                {
                    recipeToActivate.SetActive(true);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.CompareTag("Player"))
        {
            // Jeœli removeAfterExit i disableTrigger s¹ oba true, usuñ powiadomienie
            if (noteScriptable.removeAfterExit && noteScriptable.disableTrigger)
            {
                RemoveNotificationWithTrigger();
            }
            // Jeœli disableTrigger jest false, pozwól powiadomieniu dzia³aæ bez usuwania obiektu
            else if (!noteScriptable.disableTrigger)
            {
                RemoveNotificationWithOutTrigger();
            }
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
