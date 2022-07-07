using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public abstract class WfcConstraint : MonoBehaviour
    {
        internal abstract IEnumerable<ITileConstraint> GetTileConstraint(TileModelInfo tileModelInfo, IWfcGrid grid);

        internal IEnumerable<ModelTile> GetModelTiles(IEnumerable<WfcTileBase> tiles)
        {
            foreach (var tile in tiles)
            {
                if (tile == null)
                    continue;

                foreach (var rot in tile.Cell.GetRotations(tile.rotatable, tile.reflectable, tile.rotationGroupType))
                {
                    foreach (var offset in tile.offsets)
                    {
                        var modelTile = new ModelTile(tile, rot, offset);
                        yield return modelTile;
                    }
                }
            }
        }
    }
}