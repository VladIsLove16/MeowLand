
using System.Collections.Generic;
namespace TestAppOnWpf.SaveLoaderSystem
{
    public class SaveLoadersToRepository
    {
        List<ISaveLoader> saveLoaders=new();
        IRepository repository;
        public SaveLoadersToRepository(IRepository repository)
        {
            this.repository = repository;
        }
        public void Load()
        {
            repository.Load();
            foreach (ISaveLoader loader in saveLoaders)
            {
                loader.Load(repository);
            }
        }
        public void Save()
        {
            foreach (ISaveLoader loader in saveLoaders)
            {
                loader.Save(repository);
            }
            repository.Save();
        }
    }
}
