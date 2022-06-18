using PGSauce.Core.PGDebugging;
using PGSauce.Unity;
using UnityEngine;

namespace _Elementis.Scripts
{
    public class Explosion : PGMonoBehaviour, IShootable, IPuzzlePiece
    {
        private InterruptorPuzzle _puzzle;
        private bool _isActivated;

        public void OnShot(IShooter shooter, Vector3 shootForce, Vector3 shootPoint)
        {
            gameObject.SetActive(false);
            PGDebug.Message("DO EXPLODE PARTICLES").LogTodo();
            _isActivated = true;
            _puzzle.OnInterruptorActivation(this);
        }

        public void SetPuzzle(InterruptorPuzzle interruptorPuzzle)
        {
            _puzzle = interruptorPuzzle;
        }

        public bool IsActivated => _isActivated;
    }
}