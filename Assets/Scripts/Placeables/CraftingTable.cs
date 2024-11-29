using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Building, IInteractable
{
    private CraftingWindow craftingWindow;
    private PlayerController player;

    void Start ()
    {
        craftingWindow = FindAnyObjectByType<CraftingWindow>(FindObjectsInactive.Include);
        player = FindAnyObjectByType<PlayerController>();
    }

    public string GetInteractPrompt ()
    {
        return "Craft";
    }

    public void OnInteract ()
    {
        craftingWindow.gameObject.SetActive(true);
        player.ToggleCursor(true);
    }
}
