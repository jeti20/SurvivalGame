 using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportActivate : MonoBehaviour
{
    public Color newColor = Color.green;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null)
        {
            Debug.LogError("Brak komponentu Renderer na obiekcie! Skrypt nie bêdzie dzia³aæ poprawnie.");
            return;
        }

        // Stwórz now¹ instancjê materia³u, aby unikn¹æ zmiany koloru innych obiektów
        objectRenderer.material = new Material(objectRenderer.material);
    }

    private bool isActivated = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            // Zmiana koloru obiektu
            if (objectRenderer != null)
            {
                // Ustawienie koloru materia³u (albedo)
                objectRenderer.material.SetColor("_Color", newColor);

                // Ustawienie koloru emisji (jeœli shader obs³uguje emisjê)
                if (objectRenderer.material.HasProperty("_EmissionColor"))
                {
                    Color emissionColor = new Color(newColor.r, newColor.g, newColor.b, 0.5f); // transparent
                    objectRenderer.material.SetColor("_EmissionColor", emissionColor);
                    
                }
            }
            isActivated = true;
        }
        if (other.CompareTag("Player") && isActivated == true)
        {
            SceneManager.LoadScene(3);
        }
    }
}
