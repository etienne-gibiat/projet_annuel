using System;
using System.Collections.Generic;
using I2.Loc;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;

namespace PGSauce.Localization
{
    public class PgMonoLocalization : MonoSingleton<PgMonoLocalization>,  ILocalizationParamsManager
    {
        [ShowInInspector, ReadOnly]
        private Dictionary<string, string> _params;

        public override void Init()
        {
            base.Init();
            _params = new Dictionary<string, string>();
        }

        public string GetParameterValue(string param)
        {
            if (_params == null) return null;
            if (_params.TryGetValue(param, out var value))
            {
                return value;
            }
            return null; 
        }

        public void SetParameterValue(string key, string value)
        {
            if (!_params.ContainsKey(key))
            {
                _params.Add(key, value);
            }
            else
            {
                _params[key] = value;
            }
        }

        private void OnEnable()
        {
            if (!LocalizationManager.ParamManagers.Contains(this))
            {
                LocalizationManager.ParamManagers.Add(this);
                LocalizationManager.LocalizeAll(true);
            }
        }

        private void OnDisable()
        {
            LocalizationManager.ParamManagers.Remove(this);
        }
    }
}