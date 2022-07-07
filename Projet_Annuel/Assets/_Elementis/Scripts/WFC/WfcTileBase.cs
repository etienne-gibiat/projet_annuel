using PGSauce.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public abstract class WfcTileBase : PGMonoBehaviour
    {
        /// <summary>
        /// Set this to control the colors and names used for painting on the tile.
        /// </summary>
        public WfcPalette palette;
        
        /// <summary>
        /// A list of outward facing faces.
        /// For a normal cube tile, there are 6 faces. Each face contains adjacency information that indicates what other tiles can connect to it.
        /// </summary>
        public List<OrientedFace> faceDetails;
        
        /// <summary>
        /// A list of cells that this tile occupies.
        /// For a normal cube tile, this just contains Vector3Int.zero, but it will be more for "big" tiles.
        /// </summary>
        public List<Vector3Int> offsets = new List<Vector3Int>()
        {
            Vector3Int.zero
        };
        
        /// <summary>
        /// Where the center of tile is.
        /// For big tiles that occupy more than one cell, it's the center of the cell with offset (0, 0, 0). This tile may not actually occupy that cell!
        /// </summary>
        public Vector3 center = Vector3.zero;
        
        /// <summary>
        /// The size of one cell in the tile.
        /// </summary>
        public Vector3 tileSize = Vector3.one;
        
        /// <summary>
        /// If true, when generating, rotations of the tile will be used.
        /// </summary>
        public bool rotatable = true;
        
        /// <summary>
        /// If true, when generating, reflections in the x-axis will be used.
        /// </summary>
        public bool reflectable = true;
        
        /// <summary>
        /// If rotatable is on, specifies what sorts of rotations are used.
        /// </summary>
        public WfcRotationGroup rotationGroupType = WfcRotationGroup.XZ;
        
        /// <summary>
        /// If true, assumes that the tile paint matches the the symmetry of the tile.
        /// </summary>
        public bool symmetric = true;
        
        /// <summary>
        /// If set, when being instantiated by a Generator, only children will get constructed.
        /// If there are no children, then this effectively disables the tile from instantiation.
        /// </summary>
        public bool instantiateChildrenOnly = false;
        
        /// <summary>
        /// Finds the face details for a cell with a given offeset.
        /// </summary>
        public WfcFaceDetails Get(Vector3Int offset, CellFaceDirection faceDir)
        {
            if(TryGet(offset, faceDir, out var details))
            {
                return details;
            }
            throw new System.Exception($"Couldn't find face at offset {offset} in direction {faceDir}");
        }

        /// <summary>
        /// Finds the face details for a cell with a given offeset.
        /// </summary>
        public bool TryGet(Vector3Int offset, CellFaceDirection faceDir, out WfcFaceDetails details)
        {
            details = faceDetails.SingleOrDefault(x => x.offset == offset && x.faceDir == faceDir).faceDetails;
            return details != null;
        }
        
        public abstract ICell Cell { get; }
        
        /// <summary>
        /// Configures the tile as a "big" tile that occupies several cells.
        /// </summary>
        public abstract void AddOffset(Vector3Int o);

        /// <summary>
        /// Configures the tile as a "big" tile that occupies several cells.
        /// </summary>
        public abstract void RemoveOffset(Vector3Int o);
    }
}