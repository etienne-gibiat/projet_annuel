using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.Extensions;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;

namespace _Elementis.Scripts.WFC
{
    public class WfcCastle : MonoSingleton<WfcCastle>
    {
        public List<WfcTileBase> tilesToUnlock;
        public WfcGenerator generator;

        private HashSet<WfcTileBase> _unlockedTiles;

        public override void Init()
        {
            base.Init();
            _unlockedTiles = new HashSet<WfcTileBase>();
        }

        public void OnChestOpen(List<WfcTileBase> tilesUnlocked)
        {
            _unlockedTiles.AddRange(tilesUnlocked);
        }

        [Button]
        public void Generate()
        {
            if (LackATileOrMore)
            {
                return;
            }
            
            generator.Generate();
        }

        [ShowInInspector]
        public bool LackATileOrMore
        {
            get { return tilesToUnlock.Any(t => !_unlockedTiles.Contains(t)); }
        }

        public int MissingPieces => tilesToUnlock.Count - _unlockedTiles.Count;
    }
}