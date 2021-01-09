using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuInteraction : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject InventoryMenu;
    public GameObject RecordingMenu;
    public GameObject SettingsMenu;
    public GameObject QuitMenu;

    // Start and Update Functions

    void Start() {
        
    }

    void Update() {
        
    }

    // Auxiliary Functions

    public void Reset() {
        MainMenu.SetActive(true);
        InventoryMenu.SetActive(false);
        RecordingMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        QuitMenu.SetActive(false);
    }

    // Main Menu Functions

    public void OpenInventoryMenu() {
        MainMenu.SetActive(false);
        InventoryMenu.SetActive(true);
    }

    public void OpenRecordingMenu() {
        MainMenu.SetActive(false);
        RecordingMenu.SetActive(true);
    }

    public void OpenSettingsMenu() {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void OpenQuitMenu() {
        MainMenu.SetActive(false);
        QuitMenu.SetActive(true);
    }

    // Inventory Menu Functions

    public void GoBackFromInventoryToMainMenu() {
        MainMenu.SetActive(true);
        InventoryMenu.SetActive(false);
    }

    // ...

    // Quit Menu Functions

    public void QuitApplication() {
        Debug.Log("Quitting application...");
        Application.Quit();
    }

    public void GoBackFromQuitToMainMenu() {
        MainMenu.SetActive(true);
        QuitMenu.SetActive(false);
    }
}
