using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {

    public GameObject spawnPosition;

    // Start and Update Functions

    void Start() {
        
    }

    void Update() {
        
    }

    // Spawn Functions 

    public void Spawn(GameObject item) {
        GameObject instantiatedObject = Instantiate(item, spawnPosition.transform.position, Quaternion.identity);
        instantiatedObject.SetActive(true);
    }
}
