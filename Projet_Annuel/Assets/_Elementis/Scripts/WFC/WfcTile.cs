using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [AddComponentMenu("WFC/Tile")]
    public class WfcTile : WfcTileBase
    {
        public WfcTile()
        {
            faceDetails = new List<OrientedFace>()
            {
                new OrientedFace(Vector3Int.zero, CellFaceDirection.Left, new WfcFaceDetails()),
                new OrientedFace(Vector3Int.zero, CellFaceDirection.Right, new WfcFaceDetails()),
                new OrientedFace(Vector3Int.zero, CellFaceDirection.Up, new WfcFaceDetails()),
                new OrientedFace(Vector3Int.zero, CellFaceDirection.Down, new WfcFaceDetails()),
                new OrientedFace(Vector3Int.zero, CellFaceDirection.Forward, new WfcFaceDetails()),
                new OrientedFace(Vector3Int.zero, CellFaceDirection.Back, new WfcFaceDetails()),
            };
        }

        public override ICell Cell => CubeCell.Instance;

        public override void AddOffset(Vector3Int o)
        {
            if (offsets.Contains(o))
            {
                return;
            }

            offsets.Add(o);

            foreach (CellFaceDirection faceDir in Enum.GetValues(typeof(CellFaceDirection)))
            {
                var o2 = o + faceDir.Forward();
                if (offsets.Contains(o2))
                {
                    faceDetails.RemoveAll(x => x.offset == o2 && x.faceDir == faceDir.Inverted());
                }
                else
                {
                    faceDetails.Add(new OrientedFace(o, faceDir, new WfcFaceDetails()));
                }
            }
        }
        
        public override void RemoveOffset(Vector3Int o)
        {
            if (!offsets.Contains(o))
            {
                return;
            }
            offsets.Remove(o);
            foreach (CellFaceDirection faceDir in Enum.GetValues(typeof(CellFaceDirection)))
            {
                var o2 = o + faceDir.Forward();
                if (offsets.Contains(o2))
                {
                    faceDetails.Add(new OrientedFace(o2, faceDir.Inverted(), new WfcFaceDetails()));
                }
                else
                {
                    faceDetails.RemoveAll(x => x.offset == o && x.faceDir == faceDir);
                }
            }
        }
    }
}