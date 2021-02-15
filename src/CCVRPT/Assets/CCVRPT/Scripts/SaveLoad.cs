using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour {
    
    public static SaveLoad control = new SaveLoad();
    private string sceneName;
 
    public void Save( Dictionary<int,ArrayList> database, string filename) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath +"/"+filename);
        ArrayList data = new ArrayList();
        
        Debug.Log("N keys:"+database.Count);  
        for(int key = 0; key< database.Count; key++) {
            Debug.Log("length is "+database[key].Count);
            foreach (var item in database[key]) {
                data.Add(new PlayerData(key,(GameObject)item));
                Debug.Log($" Object id [{key}] pos [{((GameObject)item).transform.position.ToString()}] scale [{((GameObject)item).transform.localScale.ToString()}] rot [{((GameObject)item).transform.rotation.ToString()}] ");
            }
        }

        bf.Serialize (file, data);
        file.Close ();
    }

    public ArrayList Load(string filename){

        ArrayList data = null;
        if (File.Exists (Application.persistentDataPath + "/"+ filename)) {
            BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Open (Application.persistentDataPath + "/"+ filename , FileMode.Open);
            data = (ArrayList)bf.Deserialize (file);
            file.Close();
            Debug.Log($"Length {data.Count}");
        }
        return data;
    }
}

[Serializable]
class PlayerData {

    float[] pos;
    float[] rotation;
    float[] scale;
    int id;

    public PlayerData(int id, GameObject obj){
        pos = new float[3];
        scale = new float[3];
        rotation = new float[4];
        scale[0] = obj.transform.localScale.x;
        scale[1] = obj.transform.localScale.y;
        scale[2] = obj.transform.localScale.z;
        pos[0] = obj.transform.position.x;
        pos[1] = obj.transform.position.y;
        pos[2] = obj.transform.position.z;
        this.id = id;
        
        rotation[0] = obj.transform.rotation.w;
        rotation[1] = obj.transform.rotation.x;
        rotation[2] = obj.transform.rotation.y;
        rotation[3] = obj.transform.rotation.z;
       
       // mesh = obj.GetComponent<MeshFilter>().mesh.;
        
    }

    public float[] getPos(){
        return pos;
    }

    public float[] getRot(){
        return rotation;
    }

    public float[] getScale(){
        return scale;
    }
    
    public int getId(){
        return id;
    }
}

