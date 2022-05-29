using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PGSauce.NoDevSuite
{
    public struct GameObjectData
    {
        public Transform Transform;

        public static GameObjectData FromGameObject(GameObject go)
        {
            return new GameObjectData() {Transform = go.transform};
        }
        
        public static implicit operator GameObjectData(GameObject go) => GameObjectData.FromGameObject(go);
    }
}
