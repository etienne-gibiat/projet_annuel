using UnityEngine;

namespace _Elementis.Scripts
{
    public class Interruptor : MonoBehaviour, IShootable, IPuzzlePiece
    {
        public Material onShootMaterial;
        public MeshRenderer meshRenderer;
        public Rigidbody rb;
        public ForceMode forceMode;
        public new Light light;
        public Color onShootLightColor;
        
        private InterruptorPuzzle _interruptorPuzzle;
        private bool _isActivated;

        public bool IsActivated => _isActivated;

        public void OnShot(IShooter shooter, Vector3 shootForce, Vector3 shootPoint)
        {
            rb.AddForceAtPosition(shootForce, shootPoint, forceMode);

            if (!IsActivated)
            {
                _isActivated = true;
                meshRenderer.sharedMaterial = onShootMaterial;
                light.color = onShootLightColor;

                if (_interruptorPuzzle)
                {
                    _interruptorPuzzle.OnInterruptorActivation(this);
                }
            }
        }

        public void SetPuzzle(InterruptorPuzzle interruptorPuzzle)
        {
            _interruptorPuzzle = interruptorPuzzle;
        }
    }
}