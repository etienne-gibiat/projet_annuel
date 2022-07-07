using System;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    /// <summary>
    /// Records the painted colors and location of single face of one cube in a <see cref="WfcTile"/>
    /// </summary>
    [Serializable]
    public struct OrientedFace
    {
        public Vector3Int offset;
        public CellFaceDirection faceDir;
        public WfcFaceDetails faceDetails;


        public OrientedFace(Vector3Int offset, CellFaceDirection faceDir, WfcFaceDetails faceDetails)
        {
            this.offset = offset;
            this.faceDir = faceDir;
            this.faceDetails = faceDetails;
        }

        public void Deconstruct(out Vector3Int offset, out CellFaceDirection faceDir, out WfcFaceDetails faceDetails)
        {
            offset = this.offset;
            faceDir = this.faceDir;
            faceDetails = this.faceDetails;
        }
    }
}