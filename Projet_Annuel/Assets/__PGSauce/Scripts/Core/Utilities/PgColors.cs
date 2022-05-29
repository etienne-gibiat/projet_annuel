using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PGSauce.Core
{
    public static class PgColors
    {
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=64,255,136
        /// </summary>
        public static Color Greenish = ColorFromInt(64, 255, 136);
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=64,234,255
        /// </summary>
        public static Color Blueish =  ColorFromInt(64, 234, 255);
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=223,95,255
        /// </summary>
        public static Color Purple = ColorFromInt(223, 95, 255);
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=255,220,78
        /// </summary>
        public static Color Yellowish = ColorFromInt(255, 220, 78);
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=255,121,57
        /// </summary>
        public static Color Redish = ColorFromInt(255, 121, 57);
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=237,173,168
        /// </summary>
        public static Color Pink = ColorFromInt(237, 173, 168);
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=131,105,83
        /// </summary>
        public static Color Brown = ColorFromInt(131, 105, 83);
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=255,179,71
        /// </summary>
        public static Color Orange = ColorFromInt(255, 179, 71);
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=0,0,255
        /// </summary>
        public static Color Blue = Color.blue;
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=0,255,0
        /// </summary>
        public static Color Green = Color.green;
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=255,0,0
        /// </summary>
        public static Color Red = Color.red;
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=128,128,128
        /// </summary>
        public static Color Grey = Color.gray;
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=0,255,255
        /// </summary>
        public static Color Cyan = Color.cyan;
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=255,255,255
        /// </summary>
        public static Color White = Color.white;
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=0,0,0
        /// </summary>
        public static Color Black = Color.black;
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=255,235,4
        /// </summary>
        public static Color Yellow = Color.yellow;
        /// <summary>
        /// https://www.thecolorapi.com/id?format=html&rgb=255,0,255
        /// </summary>
        public static Color Magenta = Color.magenta;
        
        private static Color ColorFromInt(int r, int g, int b)
        {
            return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
        }
    }
}
