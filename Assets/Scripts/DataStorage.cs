using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataStorage : MonoBehaviour {

    public static DataStorage storage;
    private string fileName = "/storage1.dat";
    //public static Firebase.Auth.FirebaseUser user;
    public static string userId;
    public static Data Data = new Data();

    private void Awake()
	{
        if (storage == null) {
            storage = this;
            DontDestroyOnLoad(gameObject);
        } else if (storage != this) {
            Destroy(gameObject);
        }
	}

    public void Save() {
        Debug.Log(Application.persistentDataPath + this.fileName);

        Debug.Log("Saving Data in Local Storage");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + this.fileName);
        binaryFormatter.Serialize(file, Data);
        file.Close();

        Debug.Log("User Saved Data: " + JsonUtility.ToJson(Data));
    }

    public void Load() 
    {
        Debug.Log(Application.persistentDataPath + this.fileName);
        if (File.Exists(Application.persistentDataPath + this.fileName)) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + this.fileName, FileMode.Open);
            Data = (Data)binaryFormatter.Deserialize(file);
            file.Close();
        }

        Debug.Log("User Loaded Data: " + JsonUtility.ToJson(Data));
    }
}

[Serializable]
public class Data 
{
    //public Firebase.Auth.FirebaseUser user;
    public string userId;
    public List<Stage> stages = new List<Stage>();
}

[Serializable]
public class Stage
{
    public string id;
    public bool completed;
    public int score;
}