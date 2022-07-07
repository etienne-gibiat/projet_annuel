using System;

namespace _Elementis.Scripts.WFC
{
    /// <summary>
    /// Records the painted colors for a single face of one cube in a <see cref="WfcTile"/>
    /// </summary>
    [Serializable]
    public class WfcFaceDetails
    {
        public int topLeft;
        public int top;
        public int topRight;
        public int left;
        public int center;
        public int right;
        public int bottomLeft;
        public int bottom;
        public int bottomRight;
        
        public WfcFaceDetails Clone()
        {
            return (WfcFaceDetails)MemberwiseClone();
        }
        
        internal void ReflectX()
        {
            (topLeft, topRight) = (topRight, topLeft);
            (left, right) = (right, left);
            (bottomLeft, bottomRight) = (bottomRight, bottomLeft);
        }

        internal void RotateCw()
        {
            (topRight, bottomRight, bottomLeft, topLeft) = (topLeft, topRight, bottomRight, bottomLeft);
            (right, bottom, left, top) = (top, right, bottom, left);
        }

        internal void RotateCcw()
        {
            (topLeft, topRight, bottomRight, bottomLeft) = (topRight, bottomRight, bottomLeft, topLeft);
            (top, right, bottom, left) = (right, bottom, left, top);
        }
        
        public override string ToString()
        {
            return $"({topLeft},{top},{topRight};{left},{center},{right};{bottomLeft},{bottom},{bottomRight})";
        }

        public bool IsEquivalent(WfcFaceDetails other)
        {
            return
                topLeft == other.topLeft &&
                top == other.top &&
                topRight == other.topRight &&
                left == other.left &&
                center == other.center &&
                right == other.right &&
                bottomLeft == other.bottomLeft &&
                bottom == other.bottom &&
                bottomRight == other.bottomRight;
        }
    }
}