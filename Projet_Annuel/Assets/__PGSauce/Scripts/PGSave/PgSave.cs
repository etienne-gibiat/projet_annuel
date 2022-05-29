using PGSauce.Core.PGDebugging;
using UnityEngine;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;

namespace PGSauce.Save
{
    [CreateAssetMenu(menuName = MenuPaths.Saves + "PG Save")]
    public class PgSave : SOSingleton<PgSave>
    {
        [SerializeField] private SavedDataBool gameLaunchedForTheFirstTime;
        
        public string SavePath => "PG Saves/saves.es3";

        public bool IsGameLaunchedForTheFirstTime => Load(gameLaunchedForTheFirstTime);

        public SavedDataBool GameLaunchedForTheFirstTime => gameLaunchedForTheFirstTime;

        public bool KeyExists(string key)
        {
            return ES3.KeyExists(key, SavePath);
        }

        public T Load<T>(SavedData<T> savedData)
        {
            if (!KeyExists(savedData.Key))
            {
                savedData.SaveData(savedData.DefaultValue);
            }

            return ES3.Load<T>(savedData.Key, SavePath);
        }

        public void Save<T>(SavedData<T> savedData, T value)
        {
            ES3.Save(savedData.Key, value, SavePath);
        }

        [Button]
        public void DeleteSave()
        {
            ES3.DeleteFile(SavePath);
        }
    }
}
