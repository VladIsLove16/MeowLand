namespace TestAppOnWpf.FileSaveSystem
{
    public  interface ISaveService
    {
        void SaveData<T>(T data);
        T LoadData<T>();
    } 

}
