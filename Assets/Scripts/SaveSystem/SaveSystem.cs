using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public static class SaveSystem
    {
        public static void Save(ShopData data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/shopdata.fun";
            FileStream stream = new FileStream ( path,FileMode.Create, FileAccess.Write );
            formatter.Serialize( stream, data );
            stream.Close();
        }
        public static ShopData Load()
        {
            string path = Application.persistentDataPath + "/shopdata.fun";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

                ShopData data = formatter.Deserialize(stream) as ShopData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
            }
            return null;
        }
    }
   
}
