using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Save
{
    public abstract class SavedData<T> : SavedDataBase
    {
        [SerializeField] private T defaultValue;
        [SerializeField, CalledByOdin] private bool showCurrentValue;
        public virtual string Key => name;
        public virtual T DefaultValue => defaultValue;

        [ShowInInspector, ShowIf("@showCurrentValue")]
        public virtual T SavedValue => Load();
        
        public void SaveData(T value)
        {
            CustomBeforeSave(value);
            PgSave.Instance.Save(this, value);
            CustomAfterSave(value);
        }

        public T Load()
        {
            CustomBeforeLoad();
            var value = PgSave.Instance.Load(this);
            CustomAfterLoad();
            return value;
        }
        
        [Button]
        public void ResetSave()
        {
            SaveData(DefaultValue);
        }
        
        protected virtual void CustomAfterSave(T value)
        {
        }

        protected virtual void CustomBeforeSave(T value)
        {
        }
        
        protected virtual void CustomBeforeLoad()
        {
        }
        
        protected virtual void CustomAfterLoad()
        {
        }
    }
}