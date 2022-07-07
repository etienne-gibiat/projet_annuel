using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
     /// <summary>
    /// Represents a arrangement of cells, including their adjacency and locations.
    /// Cells are uniquely identified by a Vector3Int.
    /// </summary>
    public interface IWfcGrid
    {
         /// <summary>
        /// Describes what sort of cell can be found in this grid
        /// </summary>
        ICell CellType { get; }

        // This regions is about associating each cell with an integer index.
        // Indexes are non-negative, and usually fairly dense, so are useful for 
        // array storage of data-per-cell.
        #region Index

        /// <summary>
        /// Finds a number one larger than the maximum index for an in bounds cell.
        /// </summary>
        int IndexCount { get; }

        /// <summary>
        /// Finds the index associated with a given cell. Cell must be in bounds.
        /// </summary>
        int GetIndex(Vector3Int cell);

        /// <summary>
        /// Finds the cell associated with a given index.
        /// </summary>
        Vector3Int GetCell(int index);

        #endregion

        // This region relates to moving between cells
        #region Topology

        /// <summary>
        /// Gets a full list of cells in bounds.
        /// </summary>
        IEnumerable<Vector3Int> GetCells();

        /// <summary>
        /// Returns true if the cell is actually in the size specified.
        /// Some grids support out of bounds cells, in which case operations like TryMove may need to be filtered.
        /// </summary>
        bool InBounds(Vector3Int cell);

        /// <summary>
        /// Attempts to move from a face in a given direction, and returns information about the move if successful.
        /// Note that for some grids, this can succeed, and return a cell outside of bounds.
        /// </summary>
        bool TryMove(Vector3Int cell, CellFaceDirection faceDir, out Vector3Int dest, out CellFaceDirection inverseFaceDir, out CellRotation rotation);

        /// <summary>
        /// Maps between cell offsets and cells in the grid.
        /// </summary>
        bool TryMoveByOffset(Vector3Int startCell, Vector3Int startOffset, Vector3Int destOffset, CellRotation startRotation, out Vector3Int destCell, out CellRotation destRotation);

        /// <summary>
        /// Returns directions we might expect TryMove to work for (though it is not guaranteed).
        /// This is mostly useful for heterogenous grids where not every cell is identical.
        /// </summary>
        IEnumerable<CellFaceDirection> GetValidFaceDirs(Vector3Int cell);

        /// <summary>
        /// Returns the full set of rotations that TryMove can output. 
        /// Can just default to CellType.GetRotations();
        /// </summary>
        IEnumerable<CellRotation> GetMoveRotations();

        #endregion

        // This region relations to the 3d positioning of cells
        // All dimensions are in "local" space, i.e. relative to 
        // an appropriate object, usually the generator.
        #region Spatial

        /// <summary>
        /// Returns the center of the cell in local space
        /// </summary>
        Vector3 GetCellCenter(Vector3Int cell);

        /// <summary>
        /// Returns the appropriate transform for the cell.
        /// The translation will always be to GetCellCenter.
        /// Not inclusive of cell rotation, that should be applied first.
        /// </summary>
        TRS GetTRS(Vector3Int cell);

        /// <summary>
        /// Returns the cell and rotation for a tile placed in the grid.
        /// NB: The cell returned corresponds to offset (0,0,0). The tile may not actually occupy that offset.
        /// </summary>
        bool FindCell(
            Vector3 tileCenter,
            Matrix4x4 tileLocalToGridMatrix,
            out Vector3Int cell,
            out CellRotation rotation);

        /// <summary>
        /// Finds the cell containg the give position
        /// </summary>
        bool FindCell(Vector3 position, out Vector3Int cell);

        /// <summary>
        /// Gets the set of cells that potentially overlap bounds.
        /// </summary>
        IEnumerable<Vector3Int> GetCellsIntersectsApprox(Bounds bounds, bool useBounds);
        #endregion
    }
}