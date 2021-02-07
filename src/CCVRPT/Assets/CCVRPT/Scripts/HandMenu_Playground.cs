using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HandMenu_Playground : MonoBehaviour {

    // Menu GameObjects

    public GameObject MainMenu;
    public GameObject SaveMenu;
    public GameObject LoadMenu;
    public GameObject InventoryMenu;
    public GameObject RecordingMenu;
    public GameObject SettingsMenu;
    public GameObject QuitMenu;

    // Scene Load Variables

    public Dictionary<int,ArrayList> database;
    public GameObject[] items = new GameObject[2];
    public ArrayList arr_items = new ArrayList();
    public ArrayList gameObjects = new ArrayList();
    public GameObject text;
    public GameObject dropdown;
    private string fileName;

    // Object Variables
    private GameObject objItem;

    // Start and Update Functions

    void Start() {
        fileName = "";

        // Create database with all objects from inventory
        database  = new Dictionary<int, ArrayList>();
        for(int i = 0; i< items.Length; i++){
            database[i] = new ArrayList();
            arr_items.Add(items[i]);
        }
    }

    void Update() {
    }

    // Auxiliary Functions

    public void Reset() {
        Debug.Log("Reset");
        MainMenu.SetActive(true);
        SaveMenu.SetActive(false);
        LoadMenu.SetActive(false);
        InventoryMenu.SetActive(false);
        RecordingMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        QuitMenu.SetActive(false);
    }

    public void addObject(GameObject item, GameObject obj) {
        Debug.Log("addObject");
        Debug.Log($"{item.name} {obj.name}");
        Debug.Log($"{item.tag} {obj.tag}");
        for(int i =0;i<arr_items.Count; i++){
            if(item.tag == ((GameObject)arr_items[i]).tag){
                Debug.Log(i);
                database[i].Add(obj);
                break;
            }
        }
        
        // gameObjects.Add(obj);
    }

    public void removeObject(GameObject obj) {
        Debug.Log("removeObject");
        // needs to be removed from database...
        // TODO ...
    }

    public void EnableRemoveButton(GameObject btn) {
        Debug.Log("EnableRemoveButton");
        btn.SetActive(true);
    }

    public void DisableRemoveButton(GameObject btn) {
        Debug.Log("DisableRemoveButton");
        btn.SetActive(false);
    }

    public void ToggleRemoveButton(GameObject btn) {
        Debug.Log("toggleRemoveButton");
        Debug.Log(btn.activeSelf);
        btn.SetActive(btn.activeSelf);
    }

    // Main Menu Functions

    public void OpenSaveMenu() {
        Debug.Log("OpenSaveMenu");
        MainMenu.SetActive(false);
        QuitMenu.SetActive(false);
        SaveMenu.SetActive(true);

        int index = 0;
        while(File.Exists(Application.persistentDataPath + "/PlayerInfo"+index+".dat")) {
            index++;
        }
        fileName = "PlayerInfo"+index+".dat";
        text.transform.Find("Placeholder").GetComponent<TMP_Text>().text = "PlayerInfo"+index+".dat";
        //text.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "PlayerInfo"+index+".dat";
    }

    public void OpenLoadMenu() {
        Debug.Log("OpenLoadMenu");
        MainMenu.SetActive(false);
        LoadMenu.SetActive(true);

        string[] fileArray = Directory.GetFiles(Application.persistentDataPath,"*.dat");
        Dropdown dd = dropdown.GetComponent<Dropdown>();
        dd.ClearOptions();
        List<string> list = new List<string>();
        foreach (var item in fileArray) {
            var x = item.Split('/');
            list.Add(x[x.Length-1]);
        }
        dd.AddOptions(list);
    }

    public void GoHome() {
        Debug.Log("Going back to Home Menu...");
        SceneManager.LoadScene("Home"); 
    }

    public void OpenInventoryMenu() {
        Debug.Log("OpenInventoryMenu");
        MainMenu.SetActive(false);
        InventoryMenu.SetActive(true);
    }

    public void OpenRecordingMenu() {
        Debug.Log("OpenRecordingMenu");
        MainMenu.SetActive(false);
        RecordingMenu.SetActive(true);
    }

    public void OpenSettingsMenu() {
        Debug.Log("OpenSettingsMenu");
        // MainMenu.SetActive(false);
        // SettingsMenu.SetActive(true);
    }

    public void OpenQuitMenu() {
        Debug.Log("OpenQuitMenu");
        MainMenu.SetActive(false);
        QuitMenu.SetActive(true);
    }

    // Save Menu Functions

    public void GoBackFromSaveToMainMenu() {
        Debug.Log("GoBackFromSaveToMainMenu");
        SaveMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void SaveScene() {
        Debug.Log("Saving scene...");
        SaveLoad.control.Save(database,fileName);
        SaveMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    // Load Menu Functions

    public void GoBackFromLoadToMainMenu() {
        Debug.Log("GoBackFromLoadToMainMenu");
        LoadMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void LoadObjects(){
        Debug.Log("Loading scene...");
        Dropdown dd = dropdown.GetComponent<Dropdown>();
        Debug.Log(dd.options[dd.value].text);
        ArrayList arr = SaveLoad.control.Load(dd.options[dd.value].text);
        
        foreach (var item in arr) {
            Quaternion rot = Quaternion.identity;
            Vector3 pos = Vector3.one;
            Vector3 scale = Vector3.one;
            int id = ((PlayerData)item).getId();

            rot.w = ((PlayerData)item).getRot()[0];
            rot.x = ((PlayerData)item).getRot()[1];
            rot.y = ((PlayerData)item).getRot()[2];
            rot.z = ((PlayerData)item).getRot()[3];

            pos.x = ((PlayerData)item).getPos()[0];
            pos.y = ((PlayerData)item).getPos()[1];
            pos.z = ((PlayerData)item).getPos()[2];

            scale.x = ((PlayerData)item).getScale()[0];
            scale.y = ((PlayerData)item).getScale()[1];
            scale.z = ((PlayerData)item).getScale()[2];

            Debug.Log($"[{scale.ToString()}]");
            GameObject instantiatedObject = Instantiate(items[id], pos, rot);
            instantiatedObject.transform.localScale = scale;
            instantiatedObject.SetActive(true);

            Debug.Log($"[{instantiatedObject.transform.localScale.ToString()}]");
            addObject(items[id],instantiatedObject);
        }
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

    // Inventory Menu Functions

    public void GoBackFromInventoryToMainMenu() {
        Debug.Log("GoBackFromInventoryToMainMenu");
        InventoryMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    // Recording Menu Functions

    public void GoBackFromRecordingToMainMenu() {
        Debug.Log("GoBackFromRecordingToMainMenu");
        RecordingMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void StartRecording() {
        Debug.Log("Start recording");

    }

    public void StopRecording() {
        Debug.Log("Stop recording");

    }

    // Settings Menu Functions

    public void GoBackFromSettingsToMainMenu() {
        Debug.Log("GoBackFromSettingsToMainMenu");
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    // Quit Menu Functions

    public void GoBackFromQuitToMainMenu() {
        Debug.Log("GoBackFromQuitToMainMenu");
        QuitMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void QuitApplication() {
        Debug.Log("Quitting application...");
        Application.Quit();
    }
}
