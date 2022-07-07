using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public interface ICellDrawing
    {
        Vector3[] GetSubFaceVertices(Vector3Int offset, CellFaceDirection dir, SubFace subface, Vector3 center, Vector3 tileSize);

        Vector3[] GetFaceVertices(Vector3Int offset, CellFaceDirection dir, Vector3 center, Vector3 tileSize);

        void GetFaceCenterAndNormal(Vector3Int offset, CellFaceDirection dir, Vector3 center, Vector3 tileSize, out Vector3 faceCenter, out Vector3 faceNormal);

        RaycastCellHit Raycast(Ray ray, Vector3Int offset, Vector3 center, Vector3 tileSize, float? minDistance, float? maxDistance);

        SubFace RoundSubFace(Vector3Int offset, CellFaceDirection dir, Vector2 subface, PaintMode paintMode);

        bool IsAffected(Vector3Int parentOffset, CellFaceDirection parentFaceDir, SubFace parentSubface, Vector3Int childOffset, CellFaceDirection childFaceDir, SubFace childSubface, PaintMode paintMode);

        IEnumerable<SubFace> GetSubFaces(Vector3Int offset, CellFaceDirection dir);

        void SetSubFaceValue(WfcFaceDetails faceDetails, SubFace subface, int color);

        int GetSubFaceValue(WfcFaceDetails faceDetails, SubFace subface);

        Vector3Int Move(Vector3Int offset, CellFaceDirection dir);

        bool Is2D { get; }
    }

    public enum SubFace
    {
        // Square subfaces
        BottomLeft,
        Bottom,
        BottomRight,
        Left,
        Center,
        Right,
        TopLeft,
        Top,
        TopRight,

        // Clockwise from the the subface at the center of the edge facing right.
        // XAndY means adjacent to the vertex joining those two sides
        HexRight,
        HexRightAndTopRight,
        HexTopRight,
        HexTopRightAndTopLeft,
        HexTopLeft,
        HexTopLeftAndLeft,
        HexLeft,
        HexLeftAndBottomLeft,
        HexBottomLeft,
        HexBottomLeftAndBottomRight,
        HexBottomRight,
        HexBottomRightAndRight,
        HexCenter,

        TriangleBottom,
        TriangleBottomRight,
        TriangleTopRight,
        TriangleTop,
        TriangleTopLeft,
        TriangleBottomLeft,
        TriangleCenter
    }

    public class RaycastCellHit
    {
        public CellFaceDirection dir;
        public Vector3 point;
        // Varies from -1 to 1
        public Vector2 subface;
    }
}