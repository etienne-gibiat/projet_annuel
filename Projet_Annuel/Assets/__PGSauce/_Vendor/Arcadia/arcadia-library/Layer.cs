using System.Collections.Generic;
using UnityEngine;

namespace GameTroopers.UI
{
    internal class Layer
    {
        public string           name;
        public RectTransform    transform;
        public List<Menu>       openedMenus = new List<Menu>();
    }
}
