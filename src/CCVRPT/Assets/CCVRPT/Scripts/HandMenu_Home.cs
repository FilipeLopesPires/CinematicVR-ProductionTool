using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HandMenu_Home : MonoBehaviour {

    // Menu GameObjects

    public GameObject MainMenu;
    public GameObject LoadMenu;
    public GameObject SettingsMenu;

    // Scene Load Variables

    public ArrayList arr_items = new ArrayList();
    public ArrayList gameObjects = new ArrayList();
    public GameObject dropdown;
    private string fileName;

    // Start and Update Functions

    void Start() {
        fileName = "";
    }

    void Update() {
    }

    // Auxiliary Functions

    public void Reset() {
        Debug.Log("Reset");
        MainMenu.SetActive(true);
        LoadMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    // Main Menu Functions

    public void Create() {
        Debug.Log("Creating a new scene...");
        HandMenu_Playground.file = "";
        SceneManager.LoadScene("Playground"); 
    }

    public void LoadObjects() {
        Debug.Log("Loading scene...");
        Dropdown dd = dropdown.GetComponent<Dropdown>();
        Debug.Log(dd.options[dd.value].text);
        HandMenu_Playground.file = dd.options[dd.value].text;
        SceneManager.LoadScene("Playground"); 
    }

    public void OpenLoadMenu() {
        Debug.Log("OpenLoadMenu");

        string[] fileArray = Directory.GetFiles(Application.persistentDataPath,"*.dat");
        Dropdown dd = dropdown.GetComponent<Dropdown>();
        dd.ClearOptions();
        List<string> list = new List<string>();
        foreach (var item in fileArray) {
            var x = item.Split('/');
            list.Add(x[x.Length-1]);
        }
        dd.AddOptions(list);

        MainMenu.SetActive(false);
        LoadMenu.SetActive(true);
    }

    public void OpenSettingsMenu() {
        Debug.Log("OpenSettingsMenu");
        // MainMenu.SetActive(false);
        // SettingsMenu.SetActive(true);
    }

    // Load Menu Functions

    public void GoBackFromLoadToMainMenu() {
        MainMenu.SetActive(true);
        LoadMenu.SetActive(false);
    }

    public void DeleteOption(){
        Dropdown dd = dropdown.GetComponent<Dropdown>();
        dd.Hide();
        string fileName = "";
        List<string> list = new List<string>();

        for (int i = 0; i < dd.options.Count; i++) {
            if(i == dd.value){
                fileName = dd.options[i].text;
                continue;
            }
            list.Add(dd.options[i].text);
        }
        dd.ClearOptions();
        dd.AddOptions(list);
        if(File.Exists(Application.persistentDataPath + "/"+fileName)){
            File.Delete(Application.persistentDataPath + "/"+fileName);
        }
    }

    // Settings Menu Functions

    public void GoBackFromSettingsToMainMenu() {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    // Quit Menu Functions

    public void QuitApplication() {
        Debug.Log("Quitting application...");
        Application.Quit();
    }
}
