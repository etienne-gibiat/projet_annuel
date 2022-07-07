using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public interface ICell
    {
        // Directions

        IEnumerable<CellFaceDirection> GetFaceDirs();

        IEnumerable<(CellFaceDirection, CellFaceDirection)> GetFaceDirPairs();

        CellFaceDirection Invert(CellFaceDirection faceDir);

        // Rotations

        IList<CellRotation> GetRotations(bool rotatable = true, bool reflectable = true, WfcRotationGroup rotationGroupType = WfcRotationGroup.All);

        CellRotation Multiply(CellRotation a, CellRotation b);

        CellRotation Invert(CellRotation a);

        CellRotation GetIdentity();

        CellFaceDirection Rotate(CellFaceDirection faceDir, CellRotation rotation);

        (CellFaceDirection, WfcFaceDetails) RotateBy(CellFaceDirection faceDir, WfcFaceDetails faceDetails, CellRotation rot);

        Matrix4x4 GetMatrix(CellRotation cellRotation);
        
        bool TryMove(Vector3Int offset, CellFaceDirection dir, out Vector3Int dest);

        IEnumerable<CellFaceDirection> FindPath(Vector3Int startOffset, Vector3Int endOffset);

        Vector3 GetCellCenter(Vector3Int offset, Vector3 center, Vector3 tileSize);

        /// <summary>
        /// Note startCell/destCell are actually offsets, but naming is hard...
        /// </summary>
        bool TryMoveByOffset(Vector3Int startCell, Vector3Int startOffset, Vector3Int destOffset, CellRotation rotation, out Vector3Int destCell);


        /// <summary>
        /// Given a shape, and a rotation, finds the translation that puts the rotated shape back on itself,
        /// and applies that translation to each of the offsets in the shape.
        /// Returns null if no such mapping is possible.
        /// </summary>
        IDictionary<Vector3Int, Vector3Int> Realign(ISet<Vector3Int> shape, CellRotation rotation);
    }
}