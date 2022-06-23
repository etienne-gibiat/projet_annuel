using UnityEngine;

namespace _Elementis.Scripts.Grass.Grass_V2
{
    public class GrassInteractor : MonoBehaviour
    {
        void Update()
        {
            Shader.SetGlobalVector("_PositionMoving", transform.position);
        }
    }
}