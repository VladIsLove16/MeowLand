using System.Collections.Generic;

namespace CapybaraAdventure.Save
{
    public interface ISaveSystem
    {
        public void Save(SaveData saveData);

        SaveData Load();
    }
}