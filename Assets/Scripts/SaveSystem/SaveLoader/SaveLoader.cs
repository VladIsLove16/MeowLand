using System.Collections.Generic;
using System.Diagnostics;
namespace TestAppOnWpf.SaveLoaderSystem
{
    internal abstract class SaveLoader<TData,TService> : ISaveLoader
    {
        public void Load(IRepository repository)
        {
            var service = App.ServiceProvider.GetService<TService>();
            if (repository.TryGetData(out TData data))
            {
                SetupData(data, service);
                Debug.Write("Loaded to service" + service.ToString());
            }
            else
                SetupDefaultData(service);
            Debug.Write("Loaded to service" + "service");
        }

        public void Save(IRepository repository)
        {
            var service = App.ServiceProvider.GetService<TService>();
            TData data = GetData(service);
            repository.SetData(data);
        }
        public abstract TData GetData(TService service);
        public abstract void SetupData(TData data, TService service);
        public virtual void SetupDefaultData(TService service) { }
    }
}
