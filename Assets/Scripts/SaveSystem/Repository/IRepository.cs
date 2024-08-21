namespace TestAppOnWpf.SaveLoaderSystem
{
    public interface IRepository
    {
        void SetData<T>(T data);
        bool TryGetData<T>(out T data);
        public void Load();

        public void Save();
    }
}