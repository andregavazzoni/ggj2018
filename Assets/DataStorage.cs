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
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + this.fileName);
        Data data = new Data();
        data.userId = userId;
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void Load() 
    {
        Debug.Log(Application.persistentDataPath + this.fileName);
        if (File.Exists(Application.persistentDataPath + this.fileName)) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + this.fileName, FileMode.Open);
            Data data = (Data)binaryFormatter.Deserialize(file);
            file.Close();

            userId = data.userId;
        }
    }
}

[Serializable]
class Data 
{
    //public Firebase.Auth.FirebaseUser user;
    public string userId;
}