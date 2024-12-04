using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cutscene_01 : MonoBehaviour
{
    [SerializeField] GameObject player;
    //public GameObject TextBox;
    public AudioSource line1;
    //public AudioSource line2;

    void Start()
    {
        
        player.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(ScenePlayer());
    }

    IEnumerator ScenePlayer()
    {
        yield return new WaitForSeconds(1.5f);
        //TextBox.GetComponent<TextMeshProUGUI>().text = "... wherer am I?";
        line1.Play();
        yield return new WaitForSeconds(2); 
        //TextBox.GetComponent<TextMeshProUGUI>().text = ""; 
        yield return new WaitForSeconds(0.5f);
        //GetComponent<TextMeshProUGUI>().text = "I need to get out of here.";
        //line2.Play();
        yield return new WaitForSeconds(4);
        //TextBox.GetComponent<TextMeshProUGUI>().text = "";
        player.GetComponent<PlayerController>().enabled = true;

    }
}
