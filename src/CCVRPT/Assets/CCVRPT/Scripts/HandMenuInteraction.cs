using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class HandMenuInteraction : MonoBehaviour {

    

    public GameObject MainMenu;
    public GameObject InventoryMenu;
    public GameObject RecordingMenu;
    public GameObject SettingsMenu;
    public GameObject QuitMenu;

    public GameObject LoadMenu;
    public GameObject SaveMenu;
    public Dictionary<int,ArrayList> database;
    public GameObject[] items = new GameObject[2];
    public ArrayList arr_items = new ArrayList();
    public ArrayList gameObjects = new ArrayList();
    // Start and Update Functions
    public GameObject text;
    public GameObject dropdown;
    private string fileName;
    void Start() {
        fileName = "";
        database  = new Dictionary<int, ArrayList>();
        for(int i = 0; i< items.Length; i++){
            database[i] = new ArrayList();
            arr_items.Add(items[i]);
        }
    }

    void Update() {
        
    }

    // Auxiliary Functions

    public void addObject(GameObject item,GameObject obj){
        Debug.Log($"{item.name} {obj.name}");
        Debug.Log($"{item.tag} {obj.tag}");
        for(int i =0;i<arr_items.Count; i++){
            if(item.tag == ((GameObject)arr_items[i]).tag){
                Debug.Log(i);
                database[i].Add(obj);
                break;
            }
        }
        
        
        //gameObjects.Add(obj);
    }

    public void Reset() {
        MainMenu.SetActive(true);
        InventoryMenu.SetActive(false);
        RecordingMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        QuitMenu.SetActive(false);
        SaveMenu.SetActive(false);
        LoadMenu.SetActive(false);
    }

    // Main Menu Functions

    public void DeleteOption(){

        Dropdown dd = dropdown.GetComponent<Dropdown>();
        dd.Hide();
        string fileName = "";
        List<string> list = new List<string>();
        for (int i = 0; i < dd.options.Count; i++)
        {
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

    public void OpenInventoryMenu() {
        MainMenu.SetActive(false);
        InventoryMenu.SetActive(true);
    }

    public void OpenRecordingMenu() {
        MainMenu.SetActive(false);
        RecordingMenu.SetActive(true);
    }

    public void OpenLoadMenu(){
        string[] fileArray = Directory.GetFiles(Application.persistentDataPath,"*.dat");
        Dropdown dd = dropdown.GetComponent<Dropdown>();
        dd.ClearOptions();
        List<string> list = new List<string>();
        foreach (var item in fileArray)
        {
            var x = item.Split('/');
            list.Add(x[x.Length-1]);
        }
        dd.AddOptions(list);
        
        MainMenu.SetActive(false);
        LoadMenu.SetActive(true);
    }

    public void LoadObjects(){
        Debug.Log("Loading scene///////////////////////////////");
        Dropdown dd = dropdown.GetComponent<Dropdown>();
        Debug.Log(dd.options[dd.value].text);
        ArrayList arr = SaveLoad.control.Load(dd.options[dd.value].text);
        
        foreach (var item in arr)
        {
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

    public void SaveScene(){
        SaveMenu.SetActive(false);
        Debug.Log("Saving scene///////////////////////////////");
        SaveLoad.control.Save(database,fileName);
    }

    public void OpenSaveMenu(){
        int index = 0;
        while(File.Exists(Application.persistentDataPath + "/PlayerInfo"+index+".dat")){
            index++;
        }
        fileName = "PlayerInfo"+index+".dat";
        text.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "PlayerInfo"+index+".dat";
        MainMenu.SetActive(false);
        SaveMenu.SetActive(true);
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
