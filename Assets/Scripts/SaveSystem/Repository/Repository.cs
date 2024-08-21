using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TestAppOnWpf.FileSaveSystem;
using Unity.VisualScripting;
using static UnityEngine.AudioSettings;
namespace TestAppOnWpf.SaveLoaderSystem
{
    public class Repository : IRepository
    {
        private Dictionary<string, string> repository=new();
        ISaveService saveService;
        public void Construct(ISaveService saveService)
        {
            this.saveService = saveService;
        }
        public void SetData<T>(T data)
        {
            string key = typeof(T).ToString();
            
            string serializedData = JsonConvert.SerializeObject(data);
            repository[key] = serializedData;
        }

        public bool TryGetData<T>(out T data)
        {
            string key = typeof(T).ToString();
            if(repository==null)
                repository = new Dictionary<string, string>();
            if (repository.TryGetValue(key,out string serializedString))
            {
                data = JsonConvert.DeserializeObject<T>(serializedString);
                return true;
            }
            data = default;
            return false;
        }
        public void Load()
        {
            repository = saveService.LoadData<Dictionary<string, string>>();
        }

        public void Save()
        {
           saveService.SaveData(repository);
        }
    }
}
