using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {

    public GameObject spawnPosition;
    public HandMenu_Playground handMenu;

    // Start and Update Functions

    void Start() {
       
    }

    void Update() {
        
    }

    // Spawn Functions 

    public void Spawn(GameObject item) {
        
       if(!item && !handMenu){
           //Debug.Log("Failed to ")
           return;
       }
        GameObject instantiatedObject = Instantiate(item, spawnPosition.transform.position, Quaternion.identity);
        instantiatedObject.SetActive(true);
        // instantiatedObject.tag = +"";
        if(handMenu != null) {
            handMenu.addObject(item,instantiatedObject);
        }
        
        Debug.Log("Creating object");
        //SaveLoad.control.Save();
    }
}
