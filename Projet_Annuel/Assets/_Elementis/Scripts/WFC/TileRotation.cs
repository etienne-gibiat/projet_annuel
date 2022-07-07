using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    public class TileRotation
  {
    private readonly IDictionary<Tile, IDictionary<Rotation, Tile>> rotations;
    private readonly IDictionary<Tile, TileRotationTreatment> treatments;
    private readonly TileRotationTreatment defaultTreatment;
    private readonly RotationGroup rotationGroup;

    internal TileRotation(
      IDictionary<Tile, IDictionary<Rotation, Tile>> rotations,
      IDictionary<Tile, TileRotationTreatment> treatments,
      TileRotationTreatment defaultTreatment,
      RotationGroup rotationGroup)
    {
      this.rotations = rotations;
      this.treatments = treatments;
      this.defaultTreatment = defaultTreatment;
      this.rotationGroup = rotationGroup;
    }

    public TileRotation(int rotationalSymmetry, bool reflectionalSymmetry)
    {
      this.treatments = (IDictionary<Tile, TileRotationTreatment>) new Dictionary<Tile, TileRotationTreatment>();
      this.defaultTreatment = TileRotationTreatment.Unchanged;
      this.rotationGroup = new RotationGroup(rotationalSymmetry, reflectionalSymmetry);
    }

    public TileRotation()
      : this(1, false)
    {
    }

    public RotationGroup RotationGroup => this.rotationGroup;

    public bool Rotate(Tile tile, Rotation rotation, out Tile result)
    {
      if (this.rotationGroup != null && tile.Value is RotatedTile rotatedTile)
      {
        rotation = rotatedTile.Rotation * rotation;
        tile = rotatedTile.Tile;
      }
      IDictionary<Rotation, Tile> dictionary;
      if (this.rotations != null && this.rotations.TryGetValue(tile, out dictionary) && dictionary.TryGetValue(rotation, out result))
        return true;
      TileRotationTreatment defaultTreatment;
      if (!this.treatments.TryGetValue(tile, out defaultTreatment))
        defaultTreatment = this.defaultTreatment;
      switch (defaultTreatment)
      {
        case TileRotationTreatment.Missing:
          result = new Tile();
          return false;
        case TileRotationTreatment.Unchanged:
          result = tile;
          return true;
        case TileRotationTreatment.Generated:
          if (rotation.IsIdentity)
            result = tile;
          else
            result = new Tile((object) new RotatedTile()
            {
              Rotation = rotation,
              Tile = tile
            });
          return true;
        default:
          throw new Exception(string.Format("Unknown treatment {0}", (object) defaultTreatment));
      }
    }

    public IEnumerable<Tile> Rotate(IEnumerable<Tile> tiles, Rotation rotation)
    {
      foreach (Tile tile in tiles)
      {
        Tile result;
        if (this.Rotate(tile, rotation, out result))
          yield return result;
      }
    }

    public IEnumerable<Tile> RotateAll(Tile tile)
    {
      foreach (Rotation rotation in this.RotationGroup)
      {
        Tile result;
        if (this.Rotate(tile, rotation, out result))
          yield return result;
      }
    }

    public IEnumerable<Tile> RotateAll(IEnumerable<Tile> tiles) => tiles.SelectMany<Tile, Tile>((Func<Tile, IEnumerable<Tile>>) (tile => this.RotateAll(tile)));

    public Tile Canonicalize(Tile t)
    {
      if (!(t.Value is RotatedTile rotatedTile))
        return t;
      Tile result;
      if (!this.Rotate(rotatedTile.Tile, rotatedTile.Rotation, out result))
        throw new Exception(string.Format("No tile corresponds to {0}", (object) t));
      return result;
    }
  }
}