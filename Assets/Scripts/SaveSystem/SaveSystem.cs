using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SaveSystem
{
    public static void Save<T>(T data) where T : class
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+typeof(T).ToString()+".fun";
        FileStream stream = new FileStream ( path,FileMode.Create, FileAccess.Write );
        formatter.Serialize( stream, data );
        stream.Close();
        Debug.Log("Saved");
    }
    public static T  Load<T>() where T : class
    {
        string path = Application.persistentDataPath + "/"+typeof(T).ToString()+".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            T data = formatter.Deserialize(stream) as T;
            stream.Close();
            Debug.Log("Loaded");
            return data;
        }
        else
        {
            Debug.LogAssertion("Save file not found in " + path);
        }
        return null;
    }
}
