using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheraBytes.BetterUi
{
    [Serializable]
    public class DpiManager
    {
        [Serializable]
        public class DpiOverride
        {
            [SerializeField] float dpi = 96;
            [SerializeField] string deviceModel;

            public float Dpi { get { return dpi; } }
            public string DeviceModel { get { return deviceModel; } }

            public DpiOverride(string deviceModel, float dpi)
            {
                this.deviceModel = deviceModel;
                this.dpi = dpi;
            }
        }

        [SerializeField]
        List<DpiOverride> overrides = new List<DpiOverride>();

        public float GetDpi()
        {
            DpiOverride ov = overrides.FirstOrDefault(o => o.DeviceModel == SystemInfo.deviceModel);

            if (ov != null)
                return ov.Dpi;

            return Screen.dpi;    
        }
    }
}
