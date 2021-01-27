using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {

    public GameObject spawnPosition;
    public HandMenuInteraction handMenuInteraction;
    // Start and Update Functions

    void Start() {
       
    }

    void Update() {
        
    }

    // Spawn Functions 

    public void Spawn(GameObject item) {
        
       if(!item && !handMenuInteraction){
           //Debug.Log("Failed to ")
           return;
       }
        GameObject instantiatedObject = Instantiate(item, spawnPosition.transform.position, Quaternion.identity);
        instantiatedObject.SetActive(true);
       // instantiatedObject.tag = +"";
       if(handMenuInteraction != null)
        handMenuInteraction.addObject(item,instantiatedObject);
        
        Debug.Log("Creating object");
        //SaveLoad.control.Save();
    }
}
