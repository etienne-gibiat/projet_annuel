using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public class WfcTilePaintingState : ScriptableSingleton<WfcTilePaintingState>
    {
        [SerializeField]
        private bool m_showBackface = false;

        [SerializeField]
        private float m_opacity = 0.8f;

        [SerializeField]
        private bool m_showAll = false;

        [SerializeField]
        private bool m_hideAll = false;

        [SerializeField]
        private int m_paintIndex = 1;

        [SerializeField]
        private int m_lastPaintIndex = 2;

        public static bool showBackface
        {
            get => instance.m_showBackface;
            set => instance.m_showBackface = value;
        }

        public static float opacity
        {
            get => instance.m_opacity;
            set => instance.m_opacity = value;
        }

        public static bool showAll
        {
            get => instance.m_showAll;
            set => instance.m_showAll = value;
        }

        public static bool hideAll
        {
            get => instance.m_hideAll;
            set => instance.m_hideAll = value;
        }

        public static int paintIndex
        {
            get => instance.m_paintIndex;
            set => instance.m_paintIndex = value;
        }

        public static int lastPaintIndex
        {
            get => instance.m_lastPaintIndex;
            set => instance.m_lastPaintIndex = value;
        }
    }
}