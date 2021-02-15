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
        Debug.Log("Creating object");
        
        if(!item && !handMenu){
            Debug.Log("Failed to spawn item");
            return;
        }

        GameObject instantiatedObject = Instantiate(item, spawnPosition.transform.position, Quaternion.identity);
        instantiatedObject.SetActive(true);
        if(handMenu != null) {
            handMenu.addObject(item,instantiatedObject);
        }
    }
}
